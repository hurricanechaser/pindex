


using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using SubSonic.DataProviders;
using SubSonic.Extensions;
using SubSonic.Linq.Structure;
using SubSonic.Query;
using SubSonic.Schema;
using System.Data.Common;
using System.Collections.Generic;

namespace SubSonic.POCOS
{
    public partial class PindexProdDB : IQuerySurface
    {

        public IDataProvider DataProvider;
        public DbQueryProvider provider;
        
        public static IDataProvider DefaultDataProvider { get; set; }

        public bool TestMode
		{
            get
			{
                return DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public PindexProdDB() 
        {
            if (DefaultDataProvider == null) {
                DataProvider = ProviderFactory.GetProvider("PindexProdConnectionString");
            }
            else {
                DataProvider = DefaultDataProvider;
            }
            Init();
        }

        public PindexProdDB(string connectionStringName)
        {
            DataProvider = ProviderFactory.GetProvider(connectionStringName);
            Init();
        }

		public PindexProdDB(string connectionString, string providerName)
        {
            DataProvider = ProviderFactory.GetProvider(connectionString,providerName);
            Init();
        }

		public ITable FindByPrimaryKey(string pkName)
        {
            return DataProvider.Schema.Tables.SingleOrDefault(x => x.PrimaryKey.Name.Equals(pkName, StringComparison.InvariantCultureIgnoreCase));
        }

        public Query<T> GetQuery<T>()
        {
            return new Query<T>(provider);
        }
        
        public ITable FindTable(string tableName)
        {
            return DataProvider.FindTable(tableName);
        }
               
        public IDataProvider Provider
        {
            get { return DataProvider; }
            set {DataProvider=value;}
        }
        
        public DbQueryProvider QueryProvider
        {
            get { return provider; }
        }
        
        BatchQuery _batch = null;
        public void Queue<T>(IQueryable<T> qry)
        {
            if (_batch == null)
                _batch = new BatchQuery(Provider, QueryProvider);
            _batch.Queue(qry);
        }

        public void Queue(ISqlQuery qry)
        {
            if (_batch == null)
                _batch = new BatchQuery(Provider, QueryProvider);
            _batch.Queue(qry);
        }

        public void ExecuteTransaction(IList<DbCommand> commands)
		{
            if(!TestMode)
			{
                using(var connection = commands[0].Connection)
				{
                   if (connection.State == ConnectionState.Closed)
                        connection.Open();
                   
                   using (var trans = connection.BeginTransaction()) 
				   {
                        foreach (var cmd in commands) 
						{
                            cmd.Transaction = trans;
                            cmd.Connection = connection;
                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                    connection.Close();
                }
            }
        }

        public IDataReader ExecuteBatch()
        {
            if (_batch == null)
                throw new InvalidOperationException("There's nothing in the queue");
            if(!TestMode)
                return _batch.ExecuteReader();
            return null;
        }
			
        public Query<CategoryImagesMapping> CategoryImagesMappings { get; set; }
        public Query<AppUser> AppUsers { get; set; }
        public Query<User> Users { get; set; }
        public Query<UserBatchAssigned> UserBatchAssigneds { get; set; }
        public Query<Like> Likes { get; set; }
        public Query<Article> Articles { get; set; }
        public Query<Rating> Ratings { get; set; }
        public Query<Review> Reviews { get; set; }
        public Query<Store> Stores { get; set; }
        public Query<BoardContributor> BoardContributors { get; set; }
        public Query<Image> Images { get; set; }
        public Query<Board> Boards { get; set; }
        public Query<BoardsImagesMapping> BoardsImagesMappings { get; set; }
        public Query<Comment> Comments { get; set; }
        public Query<Category> Categories { get; set; }
        public Query<Facebook> Facebooks { get; set; }

			

        #region ' Aggregates and SubSonic Queries '
        public Select SelectColumns(params string[] columns)
        {
            return new Select(DataProvider, columns);
        }

        public Select Select
        {
            get { return new Select(this.Provider); }
        }

        public Insert Insert
		{
            get { return new Insert(this.Provider); }
        }

        public Update<T> Update<T>() where T:new()
		{
            return new Update<T>(this.Provider);
        }

        public SqlQuery Delete<T>(Expression<Func<T,bool>> column) where T:new()
        {
            LambdaExpression lamda = column;
            SqlQuery result = new Delete<T>(this.Provider);
            result = result.From<T>();
            result.Constraints=lamda.ParseConstraints().ToList();
            return result;
        }

        public SqlQuery Max<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = DataProvider.FindTable(objectName).Name;
            return new Select(DataProvider, new Aggregate(colName, AggregateFunction.Max)).From(tableName);
        }

        public SqlQuery Min<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Min)).From(tableName);
        }

        public SqlQuery Sum<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Sum)).From(tableName);
        }

        public SqlQuery Avg<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Avg)).From(tableName);
        }

        public SqlQuery Count<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Count)).From(tableName);
        }

        public SqlQuery Variance<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Var)).From(tableName);
        }

        public SqlQuery StandardDeviation<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.StDev)).From(tableName);
        }

        #endregion

        void Init()
        {
            provider = new DbQueryProvider(this.Provider);

            #region ' Query Defs '
            CategoryImagesMappings = new Query<CategoryImagesMapping>(provider);
            AppUsers = new Query<AppUser>(provider);
            Users = new Query<User>(provider);
            UserBatchAssigneds = new Query<UserBatchAssigned>(provider);
            Likes = new Query<Like>(provider);
            Articles = new Query<Article>(provider);
            Ratings = new Query<Rating>(provider);
            Reviews = new Query<Review>(provider);
            Stores = new Query<Store>(provider);
            BoardContributors = new Query<BoardContributor>(provider);
            Images = new Query<Image>(provider);
            Boards = new Query<Board>(provider);
            BoardsImagesMappings = new Query<BoardsImagesMapping>(provider);
            Comments = new Query<Comment>(provider);
            Categories = new Query<Category>(provider);
            Facebooks = new Query<Facebook>(provider);
            #endregion


            #region ' Schemas '
        	if(DataProvider.Schema.Tables.Count == 0)
			{
            	DataProvider.Schema.Tables.Add(new CategoryImagesMappingTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new AppUsersTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new UserTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new UserBatchAssignedTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new LikesTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new ArticlesTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new RatingTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new ReviewTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new StoresTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new BoardContributorTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new ImagesTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new BoardsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new BoardsImagesMappingTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new CommentsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new CategoryTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new FacebookTable(DataProvider));
            }
            #endregion
        }
        

        #region ' Helpers '
            
        internal static DateTime DateTimeNowTruncatedDownToSecond() {
            var now = DateTime.Now;
            return now.AddTicks(-now.Ticks % TimeSpan.TicksPerSecond);
        }

        #endregion

    }
}