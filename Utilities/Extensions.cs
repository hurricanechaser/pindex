using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Collections;
using System.Runtime.Serialization;
using Newtonsoft.Json.Serialization;
using System.Data.Common;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Dynamic;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Newtonsoft.Json.Converters;

public static class Extensions
{
    public static void WriteJson(this HttpResponse httpResponse, IQueryable filteredQuery, bool process = true)
    {
        int total = 0;
        httpResponse.Write(JsonConvert.SerializeObject(new
        {
            success = true,
            res = process ? ExtJs4.Process(filteredQuery, ref total) : filteredQuery,
            total = process ? total : filteredQuery.Count(),
        }));
    }
    public static int? ToInt(this string str)
    {
        int val;
        return int.TryParse(str,out val)?(int?)val:null;
    }
    public static void WriteJson(this HttpResponse httpResponse, DataSet ds, int total)
    {
        httpResponse.Write(JsonConvert.SerializeObject(new
        {
            success = true,
            total = total,
            res = ds
        }, new DataSetConverter()));
    }
    public static void WriteJson(this HttpResponse httpResponse, DataSet ds)
    {
        httpResponse.Write(JsonConvert.SerializeObject(ds, new DataSetConverter()));
    }
    public static void WriteJson(this HttpResponse httpResponse, DataTable dt, int total)
    {
        httpResponse.Write(JsonConvert.SerializeObject(new
        {
            success = true,
            total = total,
            res = dt
        }, new DataTableConverter()));
    }
    public static void WriteJson(this HttpResponse httpResponse, DataTable dt)
    {
        httpResponse.Write(JsonConvert.SerializeObject(dt, new DataTableConverter()));
    }
    public static void WriteJson(this HttpResponse httpResponse, IEnumerable filteredQuery, int total)
    {
        httpResponse.Write(JsonConvert.SerializeObject(new
        {
            success = true,
            total = total,
            res = filteredQuery
        }));
    }
    public static void WriteJson(this HttpResponse httpResponse, IEnumerable filteredQuery)
    {
        httpResponse.Write(JsonConvert.SerializeObject(new
        {
            success = true,
            res = filteredQuery
        }));
    }
    public static void WriteJson(this HttpResponse httpResponse, IQueryable filteredQuery, Func<IQueryable, List<object>> postFilter, bool process = true)
    {
        int total = 0;
        IQueryable fil = process ? ExtJs4.Process(filteredQuery, ref total) : filteredQuery;
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };
        httpResponse.Write(JsonConvert.SerializeObject(new { success = true, res = postFilter(fil), total = process ? total : filteredQuery.Count() }, Formatting.None, settings));
    }
    public static void WriteJsonObjectWithRes(this HttpResponse httpResponse, object obj)
    {
        httpResponse.Write(JsonConvert.SerializeObject(new
        {
            success = true,
            res = obj
        }));
    }
    public static void WriteJson(this HttpResponse httpResponse, IEnumerable filteredQuery, string replace, string with)
    {
        string res = JsonConvert.SerializeObject(new
          {
              success = true,
              res = filteredQuery
          });
        httpResponse.Write(res.Replace(replace, with));
    }
    public static void WriteJsonArray(this HttpResponse httpResponse, IEnumerable filteredQuery)
    {
        httpResponse.Write(JsonConvert.SerializeObject(filteredQuery));
    }
    public static void WriteJsonArray(this HttpResponse httpResponse, IEnumerable filteredQuery, string replace, string with)
    {
        string res = JsonConvert.SerializeObject(filteredQuery);
        httpResponse.Write(res.Replace(replace, with));
    }
    public static void WriteJsonObject(this HttpResponse httpResponse, object obj)
    {
        httpResponse.Write(JsonConvert.SerializeObject(obj));
    }
    public static void WriteJsonSuccess(this HttpResponse httpResponse)
    {
        httpResponse.Write("{success:true}");
    }
    public static void WriteJsonFailure(this HttpResponse httpResponse, string message)
    {
        httpResponse.StatusCode = 500;
        httpResponse.StatusDescription = "Application Error";
        httpResponse.Write(string.Concat("{", string.Format("success:false,message:'{0}'", message), "}"));
    }
    public static DataSet ToDataSet<T>(this IEnumerable<T> list)
    {
        StringBuilder sb = new StringBuilder();
        XmlSerializer xmlSerializer = new XmlSerializer(list.GetType());
        StringWriter sw = new StringWriter(sb);
        xmlSerializer.Serialize(sw, list);
        StringReader stream = new StringReader(sb.ToString());
        DataSet ds = new DataSet();
        ds.ReadXml(stream);
        return ds;
    }
    public static DataSet ToDataSet(this IEnumerable list)
    {
        StringBuilder sb = new StringBuilder();
        XmlSerializer xmlSerializer = new XmlSerializer(list.GetType());
        StringWriter sw = new StringWriter(sb);
        xmlSerializer.Serialize(sw, list);
        StringReader stream = new StringReader(sb.ToString());
        DataSet ds = new DataSet();
        ds.ReadXml(stream);
        return ds;
    }
    public static string ToDNRFormat(this DateTime dt)
    {
        return dt.ToString("dd-MMM-yyyy HH:mm");
    }
    public static string ToDNRRHFormat(this DateTime dt)
    {
        return dt.ToString("dd-MMM-yyyy");
    }
    public static void PopulateParameters(this SqlCommand o, JObject jObj)
    {
        foreach (SqlParameter p in o.Parameters)
        {
            DateTime dt;
            switch (p.SqlDbType)
            {
                default:
                    p.Value = (string)jObj[p.ParameterName.TrimStart('@')];
                    break;
                case SqlDbType.Bit:
                    bool b;
                    p.Value = bool.TryParse(jObj[p.ParameterName.TrimStart('@')].ToString(), out b) ? (object)b : DBNull.Value;
                    break;
                case SqlDbType.TinyInt:
                    p.Value = (short?)jObj[p.ParameterName.TrimStart('@')];
                    break;
                case SqlDbType.Int:
                    JToken jTok = jObj[p.ParameterName.TrimStart('@')];
                    p.Value = (jTok == null || string.IsNullOrEmpty(jTok.ToString().Trim('"')) || jTok.ToString() == "null") ? DBNull.Value : (object)int.Parse(jTok.ToString().Trim('"'));
                    break;
                case SqlDbType.BigInt:
                    p.Value = (long?)jObj[p.ParameterName.TrimStart('@')];
                    break;
                case SqlDbType.SmallDateTime:
                    p.Value = DateTime.TryParse((string)jObj[p.ParameterName.TrimStart('@')], out dt) ? (object)dt : DBNull.Value;
                    break;
                case SqlDbType.DateTime2:
                    p.Value = DateTime.TryParse((string)jObj[p.ParameterName.TrimStart('@')], out dt) ? (object)dt : DBNull.Value;
                    break;
                case SqlDbType.DateTime:
                    p.Value = DateTime.TryParse((string)jObj[p.ParameterName.TrimStart('@')], out dt) ? (object)dt : DBNull.Value;
                    break;
                case SqlDbType.Float:
                    string svalue = jObj[p.ParameterName.TrimStart('@')].ToString().Trim('"');
                    p.Value = string.IsNullOrEmpty(svalue) ? DBNull.Value : (object)float.Parse(svalue);
                    break;
                case SqlDbType.UniqueIdentifier:
                    p.Value = new Guid((string)jObj[p.ParameterName.TrimStart('@')]);
                    break;
            }
        }
    }
    public static object GetFilterValue(this HttpRequest req, string prop)
    {
        string jArr = req.Params["appFilter"];
        if (jArr != null)
        {
            JArray arr = JArray.Parse(jArr);
            JToken obj = arr.Last(o => (string)o["property"] == prop);
            object ret = null;
            if (obj.Count() > 0)
            {
                JToken val = (JToken)obj["value"];
                switch (val.Type)
                {
                    case JTokenType.Object:
                        ret = obj["value"];
                        break;
                    case JTokenType.Float:
                        ret = (float)obj["value"];
                        break;
                    case JTokenType.Integer:
                        ret = (int)obj["value"];
                        break;
                    case JTokenType.Boolean:
                        ret = (bool)obj["value"];
                        break;
                    case JTokenType.Date:
                        ret = (DateTime)obj["value"];
                        break;
                    case JTokenType.Raw:
                        ret = obj["value"];
                        break;
                    case JTokenType.String:
                        ret = (string)obj["value"];
                        break;
                    case JTokenType.Array:
                        ret = obj["value"];
                        break;
                }
            }
            return ret;
        }
        else
            return null;
    }
    private static string Append_LIKE_Symbol(string value, bool startAndEnd)
    {
        return value.Contains('%') ? startAndEnd ? string.Format("%{0}%", value.Trim('%')) : value : startAndEnd ? string.Format("%{0}%", value.Trim('%')) : string.Format("{0}%", value.Trim('%'));
    }
    public static string Truncate(this string input, int length)
    {
        return Truncate(input, length, "...");
    }
    public static string Truncate(this string input, int length, string suffix)
    {
        if (input == null)
            return "";
        if (input.Length <= length)
            return input;
        if (suffix == null)
            suffix = "...";
        return input.Substring(0, length - suffix.Length) + suffix;
    }
    public static string[] ToLineArray(this string input)
    {
        if (input == null)
            return new string[] { };
        return System.Text.RegularExpressions.Regex.Split(input, "\r\n");
    }
    public static List<string> ToLineList(this string input)
    {
        List<string> output = new List<string>();
        output.AddRange(input.ToLineArray());
        return output;
    }
    public static string ReplaceBreaksWithBR(this string input)
    {
        return string.Join("<br/>", input.ToLineArray());
    }
    public static string DoubleApostrophes(this string input)
    {
        return Regex.Replace(input, "'", "''");
    }
    public static string ToHTMLEncoded(this string input)
    {
        return HttpContext.Current.Server.HtmlEncode(input);
    }
    public static string ToURLEncoded(this string input)
    {
        return HttpContext.Current.Server.UrlEncode(input);
    }
    public static string FromHTMLEncoded(this string input)
    {
        return HttpContext.Current.Server.HtmlDecode(input);
    }
    public static string FromURLEncoded(this string input)
    {
        return HttpContext.Current.Server.UrlDecode(input);
    }
    public static string StripHTML(this string input)
    {
        return Regex.Replace(input, @"<(style|script)[^<>]*>.*?</\1>|</?[a-z][a-z0-9]*[^<>]*>|<!--.*?-->", "");
    }
}

public class encrtypor
{
    #region Properties

    public static string encrtypkey
    {
        get;
        set;
    }

    public static string saltkey
    {
        get;
        set;
    }

    #endregion Properties
}
