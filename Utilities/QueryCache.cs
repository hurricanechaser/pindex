﻿#region Licenced under the New BSD Licence
/*
Code from http://querycache.codeplex.com
 
Copyright (c) 2011, John Rusk
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided 
that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and 
  the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and
  the following disclaimer in the documentation and/or other materials provided with the distribution.

* Neither the name of the organization(s) they work for nor the names of its contributors may be used to
  endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED 
WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR 
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT 
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR 
TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF 
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 

 */
#endregion

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

// ****** By default, this file works with Linq to Entities (Entity Framwork)  ***** 
using ObjectContext = System.Data.Objects.ObjectContext;
using CompiledQuery = System.Data.Objects.CompiledQuery;
// **** But, if you replace the two lines above with the two that follow, it SHOULD work with LINQ to SQL instead
// using ObjectContext = System.Data.Linq.DataContext;
// using CompiledQuery = System.Data.Linq.CompiledQuery;
// **********

namespace Utilities
{
    /// <summary>
    /// Contains extension method to use QueryCache in Line to Entities
    /// </summary>
    public static class ObjectContextExtensions
    {
        // When should you compile queries with Entity Framework? See Craig Stuntz's reply here:
        // http://stackoverflow.com/questions/4932594/when-should-i-use-a-compiledquery

        // When should you compile queries with Linq to SQL? Bascially whenever you can, as far as I can tell.
        // (I.e. while there may be some simple cases where QueryCache offers little performance benefit in Entity Framwork,
        // as far as I can tell compiling queries virtually always gives a significant speed up in Linq to SQL.)

        /// <summary>
        /// Accepts the context and parameter (typically an anonymous type), and returns a 
        /// <see cref="QueryCacheHelper"/>
        /// </summary>
        /// <remarks>Why return a <see cref="QueryCacheHelper"/>?  Why not just do everything in this routine?
        /// Because the two-stage approach lets us get better type inference.  
        /// (By the way, we could get perfect
        /// inference in a single call IFF we took in an expression, instead of a func that returns and expression
        /// (see <see cref="QueryCacheHelper.ToCompiledQuery"/>) but I figure that if we want the speed of compiled queries,
        /// then we probably also want to avoid even constructing the expression tree, if possible.</remarks>
        public static QueryCacheHelper<TObjectContext, TParam> Pass<TObjectContext, TParam>(this TObjectContext context, TParam param)
            where TObjectContext : ObjectContext
        {
            return new QueryCacheHelper<TObjectContext, TParam>(context, param);
        }
    }

    /// <summary>
    /// Serves as the return value of <see cref="ObjectContextExtensions.Pass"/>.
    /// That method "bakes" the parameter type and value into this object.  Ditto for the data context.
    /// This object can then be used to cache the query (if necessary) and (always) to execute it.
    /// </summary>
    public class QueryCacheHelper<TObjectContext, TParam> where TObjectContext : ObjectContext
    {
        public QueryCacheHelper(TObjectContext objectContextToUse, TParam parameter)
        {
            _objectContext = objectContextToUse;
            _param = parameter;
        }

        private readonly TObjectContext _objectContext;
        private readonly TParam _param;

        /// <summary>
        /// Gets compiled query, that matches the expression returned by <paramref name="funcToSupplyExpressionOnFirstUse"/>,
        /// and executes they compiled query.
        /// For each particular function passed in as <paramref name="funcToSupplyExpressionOnFirstUse"/>,
        /// the function is only executed on the first call.  Subsequent to that, it is not evaluated and
        /// only serves as a key into a dictionary, to retrieve the compiled query that was created on the first call.
        /// </summary>
        public IEnumerable<TEntity> ToCompiledQuery<TEntity>(Func<Expression<Func<TObjectContext, TParam, IQueryable<TEntity>>>> funcToSupplyExpressionOnFirstUse)
        {
            // get the compiled query from our cache
            var compiledQuery = QueryCache.Default.GetCompiledQuery(funcToSupplyExpressionOnFirstUse);

            // execute it
            // and make doubly sure to return it as enumerable so users can accidentially NOT use the complied query 
            // (since even though our static typing says its IEnumerable, it will actually still be IQueryable at this point with Entity Framework)
            // see http://stackoverflow.com/questions/2626152/why-doesnt-this-compiledquery-give-a-performance-improvement
            // TODO: double-check the above.  is the AsEnumerable _really_ necessary?
            return compiledQuery(_objectContext, _param)
                .AsEnumerable<TEntity>();
        }
    }

    public class QueryCache
    {
        public static readonly QueryCache Default = new QueryCache();

        private readonly ConcurrentDictionary<MethodInfo, Delegate> _cache = new ConcurrentDictionary<MethodInfo, Delegate>();

        /// <summary>
        /// Operates a thread-safe cache of compiled queries.
        /// Call this method to get a compiled query.
        /// For each particular function passed in as <paramref name="funcToSupplyExpressionOnFirstUse"/>,
        /// the function is only executed on the first call(*).  Subsequent to that, it is not evaluated and
        /// only serves as a key into a dictionary, to retrieve the compiled query that was created on the first call.
        /// (*) or calls, plural, if the first ones are "simulataneous"
        /// </summary>
        internal Func<TObjectContext, TParam, IEnumerable<TEntity>> GetCompiledQuery<TObjectContext, TParam, TEntity>(Func<Expression<Func<TObjectContext, TParam, IQueryable<TEntity>>>> funcToSupplyQueryOnFirstUse)
            where TObjectContext : ObjectContext
        {
            // We only accept static methods.
            // Both to keep our code simple and also as protection against expressions that capture locals from the 
            // containing scope. (Since such lambdas are currently turned into INSTANCE methods (of the compiler-generated capture-helper-class))
            // It doesn't make sense to include such captures in compiled expressions.
            if (funcToSupplyQueryOnFirstUse.Target != null)
                throw new ArgumentException("Function to create expression must not be an instance method.  Check, are you capturing any variables.  If so, don't. Include them in the anon type that contains the parameters instead");

            MethodInfo method = funcToSupplyQueryOnFirstUse.Method;
            Delegate untypedResult;
            if (!_cache.TryGetValue(method, out untypedResult))
            {
                // TODO consider some checks on the function to ensure it contains to conditionals,
                // and is therefore (more likely?) to always return the same expression. (e.g. using ActiveSharp.Inspection.MethodInspector)

                // Get the expression that we need to compile
                // The expression is not passed directly into us, but instead we get a function that is capable of creating it.
                // Why? To save on even having to create the expression tree, if what we need is already in our cache.
                // If we too the expression in, then the caller would have to construct the expression tree every time 
                // (and then have it garbage collected) even tho we don't need it.
                var expressionToCompile = funcToSupplyQueryOnFirstUse();

                // compile it
                untypedResult = CompiledQuery.Compile<TObjectContext, TParam, IQueryable<TEntity>>(expressionToCompile);

                // cache it, so we can use it again (don't worry if a parallel thread is doing the same thing as us,
                // the _cache is threadsafe, so the only downside is that two threads have compiled it instead of one.
                // AFTER this point, all threads will get the cached value correctly
                _cache.TryAdd(method, untypedResult);
            }

            // return the compiled query
            // (The code here doesn't actually know whether we are using Entity Framework (and therefore really the 
            // return type of the func is IQueryable<TEntity>, or Linq to SQL, in which case it is IEnumerable<TEntity>.
            // Fortunately, as of C# 4' co-and-contra-variance, we don't need to care.
            return (Func<TObjectContext, TParam, IEnumerable<TEntity>>)untypedResult;
        }

        public void Clear()
        {
            _cache.Clear();
        }

    }
}