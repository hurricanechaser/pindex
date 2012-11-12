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
public class ExtJs4
{
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

    public static IQueryable Filter(IQueryable p, string sfilter)
    {
        try
        {
            JArray filter = JArray.Parse(sfilter);
            StringBuilder builder = new StringBuilder();
            bool first = true;
            foreach (JObject obj in filter)
            {
                JToken comparison = obj["comparison"];
                builder.AppendFormat("{0} {1}", first ? "" : " AND ", SetWhereClause((string)obj["type"], (string)obj["field"], Decode(comparison == null ? null : (string)comparison), obj["value"]));
                first = false;
            }
            p = p.Where(builder.ToString());
        }
        finally
        {
           
        }        
        return p;

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

    public static IQueryable Process(IQueryable query, ref int total)
    {
        string filter = context.Request.Params["filter"];
        if (!string.IsNullOrEmpty(filter))
            query = Filter(query, filter);
        string sort = context.Request.Params["sort"];
        if (!string.IsNullOrEmpty(sort))
            query = Sort(query, sort);
        total = query.Count();
        string start = context.Request.Params["start"];
        string limit = context.Request.Params["limit"];
        if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(limit))
            query = Page(query, Convert.ToInt32(start), Convert.ToInt32(limit));
        return query;
    }
    public static IQueryable Sort(IQueryable query, string sort)
    {
        JArray jsort = JArray.Parse(sort);
        foreach (JObject obj in jsort)
        {
            query = query.OrderBy(string.Format("{0} {1}", (string)obj["property"], (string)obj["direction"]));
        }
        return query;
    }
    public static IQueryable Page(IQueryable query, int start, int limit)
    {
        return query.Skip(start).Take(limit);
    }
    private static string SetWhereClause(string type, string field, string operatorString, JToken value)
    {
        switch (type)
        {
            case "numeric":
                return string.Format("{0} {1} {2}", field, operatorString, (int)value);
            case "date":
                return string.Format("{0} {1} \"{2}\"", field, operatorString, (string)value);
            case "boolean":
                return string.Format("{0} = {1}", field, (string)value);
            case "string":
                return string.Format("{0}.StartsWith(\"{1}\")", field, value.ToString());
        }
        return null;
    }
    #endregion Methods

}

