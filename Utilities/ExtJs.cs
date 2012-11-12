using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections.Generic;
using System.Linq.Dynamic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
/// <summary>
/// Summary description for Ext
/// </summary>
public class ExtJs
{
    struct Filter
    {
        public string text;
        public List<object> parameters;
    }
    #region Methods
    private static HttpContext _context;

    public static HttpContext context
    {
        get
        {
            return _context ?? HttpContext.Current;
        }
        set
        {
            _context = value;
        }
    }
    public static IQueryable ApplyFilter(IQueryable p, bool orderby, bool page)
    {
        try
        {
            int i = 0;
            string field, type, comparison, value;
            StringBuilder sFilter = new StringBuilder();
            Filter filter = new Filter();
            filter.parameters = new List<object>();
            while (!string.IsNullOrEmpty((field = context.Request.Params[string.Format("filter[{0}][field]", i)])) && !string.IsNullOrEmpty((type = context.Request.Params[string.Format("filter[{0}][data][type]", i)])) && !string.IsNullOrEmpty((value = context.Request.Params[string.Format("filter[{0}][data][value]", i)])))
            {
                if (type == "list")
                {
                    comparison = context.Request.Params[string.Format("filter[{0}][data][comparison]", i)];
                    string[] values = value.Split(',');
                    foreach (string single in values)
                        Process(ref i, field, type, comparison, single, sFilter, filter);
                }
                else
                {
                    comparison = context.Request.Params[string.Format("filter[{0}][data][comparison]", i)];
                    Process(ref i, field, type, comparison, value, sFilter, filter);
                }

            }
            filter.text = sFilter.ToString();
            return orderby ? OrderBy( GetFilter(p, filter), page) : page ? Page(GetFilter(p, filter)) : GetFilter(p, filter);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private static string Decode(string comparison)
    {
        switch (comparison)
        {
            case "lt":
                return "<";
                break;
            case "gt":
                return ">";
                break;
            default:
                return "=";
                break;
        }
    }
    private static IQueryable GetFilter(IQueryable query, Filter filter)
    {
        return filter.text.Length > 0 ? query.Where(filter.text, filter.parameters.ToArray()) : query;
    }

    private static IQueryable OrderBy( IQueryable query, bool page)
    {
        IQueryable ordered = query.OrderBy(string.Format("{0} {1}", context.Request.Params["sort"], context.Request.Params["dir"]));
        return page ? Page( ordered) : ordered;
    }
    private static IQueryable Page(IQueryable query)
    {
        return query.Skip(Convert.ToInt32(context.Request.Params["start"])).Take(Convert.ToInt32(context.Request.Params["limit"]));
    }
    private static void Process(ref int i, string field, string type, string comparison, string value, StringBuilder sFilter, Filter filter)
    {
        string operatorString = Decode(comparison);
        sFilter.AppendFormat("{0}{1}{2}", i > 0 ? " AND " : string.Empty, Field(type, field), OtherOperators(type, operatorString, i));
        filter.parameters.Add(Value(type, value));
        i++;
    }
    private static string Field(string type, string field)
    {
        switch (type)
        {
            default:
                return field;
        }

    }
    private static string OtherOperators(string type, string operatorString, int i)
    {
        switch (type)
        {
            default:
                return string.Format("{0}@{1}", operatorString, i);
            case "string":
                return string.Format(".StartsWith(@{0})", i);
        }
    }
    private static object Value(string type, string value)
    {
        switch (type)
        {
            case "numeric":
                return Convert.ToInt32(value);
            case "boolean":
                return Convert.ToBoolean(value);
            case "date":
                return Convert.ToDateTime(value);
            default:
                return value;
        }
    }
    #endregion Methods

}

