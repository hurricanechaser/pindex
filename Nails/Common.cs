using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.IO;
using System.Drawing;
using LinqKit;
using Newtonsoft.Json.Linq;
using System.Net;

using System.Collections;
using System.Security.Cryptography;
using HashLib;

using Newtonsoft.Json;
using System.Xml;
using System.Xml.Linq;

/// <summary>
/// Summary description for Common
/// </summary>
namespace Nails
{
    public static partial class Common
    {

        public static string PointsSignUp = "SU";
        public static string PointsNewBoard = "NB";
        public static string PointsNewPin = "NP";
        public static string PointsRePin = "RP";

        
        public class LocalizationInfo
        {
            public string Flag;
            public string DisplayName;
            public CultureInfo CultureInfo;
        }

        private static CultureInfo BaseCultureInfo = new CultureInfo("en-US");
        private static List<LocalizationInfo> _LocalizationCultures = PopulateLocalizationInfos();
        public static List<LocalizationInfo> CurrentLocalizationCultures
        {
            get
            {
                LocalizationInfo[] temp = new LocalizationInfo[_LocalizationCultures.Count];
                _LocalizationCultures.CopyTo(temp);
                List<LocalizationInfo> ret = temp.ToList();
                ret.Remove(ret.Single(o => o.Flag == CurrentLocalizationInfo.Flag));
                return ret;
            }
        }
        public static string FixAvatar(string Avatar)
        {
            return string.IsNullOrWhiteSpace(Avatar) ? null : UploadedImageRelPath + Avatar;
        }
        public static LocalizationInfo CurrentLocalizationInfo
        {
            get
            {
                return _LocalizationCultures.Single(o => o.CultureInfo.IetfLanguageTag == Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);
            }
        }

        private static List<LocalizationInfo> PopulateLocalizationInfos()
        {
            List<LocalizationInfo> _LocalizationCultures = new List<LocalizationInfo>();
            _LocalizationCultures.Add(new LocalizationInfo() { CultureInfo = new CultureInfo("zh-CN"), Flag = "cn", DisplayName = "中文" });
            _LocalizationCultures.Add(new LocalizationInfo() { CultureInfo = new CultureInfo("en-US"), Flag = "us", DisplayName = "En" });
            return _LocalizationCultures;
        }

        public static JsonSerializerSettings JsonSerializerSettings
        {
            get
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Include;
                settings.MissingMemberHandling = MissingMemberHandling.Error;
                return settings;
            }
        }


        public static Dictionary<string, object> CookieValue
        {
            get
            {
                return (Dictionary<string, object>)context.Items["cookies"];
            }
            set
            {
                context.Items["cookies"] = value;
            }
        }
        public static string CategoryEscapedUri(this string str)
        {
            return Uri.EscapeUriString(cat.ResourceManager.GetString(str, BaseCultureInfo));
        }

        public static void WriteJsonP(this HttpContext context, string output)
        {
            string pre = context.Request.Params["pre"];
            if (string.IsNullOrEmpty(pre))
                context.Response.Write(output);
            else context.Response.Write(string.Format("{0}={1}", pre, output));
            context.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.Cache.SetNoStore();
            context.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        }
        public static void WriteError(this HttpResponse response, string output)
        {
            response.TrySkipIisCustomErrors = true;
            response.StatusCode = 550;
            response.Write(output);
        }
        public static string RemoveSpecialCharacters(string input)
        {
            Regex r = new Regex("(?:[^a-z0-9_]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, RandomString(1));
        }
        public static void RemoveValueinCookie(string cookieName, string[] values)
        {
            Dictionary<string, object> _dict = CookieValue;
            if (_dict.ContainsKey(cookieName))
            {
                NameValueCollection coll = (NameValueCollection)_dict[cookieName];
                values.ForEach(coll.Remove);
            }
        }
        private static readonly Random _rng = new Random();
        private static readonly string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string RandomString(int size)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }
        public static string GetHash(string str)
        {
            IHash hash = Hash;
            HashResult res = hash.ComputeString(str);
            return GetString(res.GetBytes());
        }
        public static string MakeRelativeUri(string absolutePath, string relativePath)
        {
            System.Uri uri = new Uri(absolutePath);
            System.Uri uri1 = new Uri(relativePath);
            return uri.MakeRelativeUri(uri1).ToString();
        }
        public static string MakeAbsoluteUri(string absolutePath, string relativePath)
        {
            if (!relativePath.StartsWith("http:"))
            {
                Uri baseUri = new Uri(absolutePath);
                Uri absoluteUri = new Uri(baseUri, relativePath);
                return absoluteUri.ToString();
            }
            return relativePath;
        }
        public static JArray XmlToJArray(string xml)
        {
            JArray ret = new JArray();
            if (xml != null)
            {
                XElement doc = XElement.Parse(xml);
                IEnumerable<XNode> nodes = doc.Nodes();
                foreach (XNode node in nodes)
                {
                    ret.Add(JObject.Parse(JsonConvert.SerializeXNode(node, Newtonsoft.Json.Formatting.Indented, true).Replace("@", string.Empty)));
                }
                return ret;

            }
            return ret;
        }
        public static Uri Append(this Uri uri, params string[] paths)
        {
            return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => string.Format("{0}/{1}", current.TrimEnd('/'), path.TrimStart('/'))));
        }
        public static JArray XmlToJArray(XElement xml)
        {
            if (xml != null)
            {
                IEnumerable<XNode> nodes = xml.Nodes();
                JArray ret = new JArray();
                foreach (XNode node in nodes)
                {
                    JObject obj = JObject.Parse(JsonConvert.SerializeXNode(node, Newtonsoft.Json.Formatting.Indented, true).Replace("@", string.Empty));

                    ret.Add(obj);
                }
                return ret;
            }
            else
                return null;
        }
        public static JArray XmlToJArray(XElement xml, System.Func<JObject, JObject> proc)
        {
            if (xml != null)
            {
                IEnumerable<XNode> nodes = xml.Nodes();
                JArray ret = new JArray();
                foreach (XNode node in nodes)
                {
                    JObject obj = proc(JObject.Parse(JsonConvert.SerializeXNode(node, Newtonsoft.Json.Formatting.Indented, true).Replace("@", string.Empty)));
                    ret.Add(obj);
                }
                return ret;
            }
            else
                return null;
        }
        public static string GetAbsoluteUri(string Path)
        {
            if (!string.IsNullOrWhiteSpace(Path))
            {

                System.Uri uri;
                if (System.Uri.TryCreate(Path, UriKind.Absolute, out uri))
                    return uri.Authority;

            }
            return null;
        }
        public static string MoveAndRenameFileIfExists(FileInfo fInfo, string dest)
        {
            string destFileNameWithoutExt = fInfo.Name.Split('.')[0];
            string destFileNameExt = fInfo.Extension;
            string destFileName = fInfo.Name;
            int counter = 0;
            while (File.Exists(dest + "\\" + destFileName))
            {
                counter++;
                destFileName = string.Format("{0}({1}){2}", destFileNameWithoutExt, counter, destFileNameExt);
            }
            if (!Directory.Exists(dest))
                Directory.CreateDirectory(dest);
            string fdest = dest + "\\" + destFileName;
            fInfo.MoveTo(fdest);
            return destFileName;
        }
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            Random rnd = new Random();
            return source.OrderBy<T, int>((item) => rnd.Next());
        }
        public static string[] GetStringBetween(string strBegin, string strEnd, string strSource, bool includeBegin, bool includeEnd)
        {
            string[] result = { "", "" };
            int iIndexOfBegin = strSource.IndexOf(strBegin);
            if (iIndexOfBegin != -1)
            {
                // include the Begin string if desired
                if (includeBegin)
                    iIndexOfBegin -= strBegin.Length;
                strSource = strSource.Substring(iIndexOfBegin
                    + strBegin.Length);
                int iEnd = strSource.IndexOf(strEnd);
                if (iEnd != -1)
                {
                    // include the End string if desired
                    if (includeEnd)
                        iEnd += strEnd.Length;
                    result[0] = strSource.Substring(0, iEnd);
                    // advance beyond this segment
                    if (iEnd + strEnd.Length < strSource.Length)
                        result[1] = strSource.Substring(iEnd
                            + strEnd.Length);
                }
            }
            else
                // stay where we are
                result[1] = strSource;
            return result;
        }
        public static string GetStringInBetween(string strBegin, string strEnd, string strSource, bool includeBegin, bool includeEnd)
        {
            string[] result = { "", "" };
            int iIndexOfBegin = strSource.IndexOf(strBegin);
            if (iIndexOfBegin != -1)
            {
                // include the Begin string if desired
                if (includeBegin)
                    iIndexOfBegin -= strBegin.Length;
                strSource = strSource.Substring(iIndexOfBegin
                    + strBegin.Length);
                int iEnd = strSource.IndexOf(strEnd);
                if (iEnd != -1)
                {
                    // include the End string if desired
                    if (includeEnd)
                        iEnd += strEnd.Length;
                    result[0] = strSource.Substring(0, iEnd);
                    // advance beyond this segment
                    if (iEnd + strEnd.Length < strSource.Length)
                        result[1] = strSource.Substring(iEnd
                            + strEnd.Length);
                }
            }
            else
                // stay where we are
                result[1] = strSource;
            return result[0];
        }
        public static byte[] ReadFully(Stream stream, int initialLength)
        {
            if (initialLength < 1)
            {
                initialLength = 32768;
            }
            byte[] buffer = new byte[initialLength];
            int read = 0;
            int chunk;
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;
                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();
                    if (nextByte == -1)
                    {
                        return buffer;
                    }
                    byte[] newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }
            byte[] ret = new byte[read];
            Array.Copy(buffer, ret, read);
            return ret;
        }
        public static string GetString(byte[] bytes)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bytes)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }
        public static HttpContext context
        {
            get
            {
                return HttpContext.Current;
            }
        }


        public static int? VUserID
        {
            get
            {
                string rv = ReadValue(InfoCookie, "vuID");
                return string.IsNullOrEmpty(rv) ? null : (int?)Convert.ToInt32(rv);
            }
        }
        public static int? Points
        {
            get
            {
                string rv = ReadValue(InfoCookie, "pts");
                return string.IsNullOrEmpty(rv) ? null : (int?)(JObject.Parse(rv)["total"]);
            }
        }
        public static int? UserID
        {
            get
            {
                Dictionary<string, object> _dict = CookieValue;
                return _dict.ContainsKey(AuthCookie) ? (int?)int.Parse((string)_dict[AuthCookie]) : null;
            }
        }
        public static string SessionID
        {
            get
            {
                string rv = ReadValue(InfoCookie, "id");
                return rv;
            }
        }
        public static string ReadValue(string propertyName)
        {
            Dictionary<string, object> _dict = CookieValue;
            string cookieName = InfoCookie;
            if (_dict.ContainsKey(cookieName))
            {

                NameValueCollection _cookie = (NameValueCollection)_dict[cookieName];
                return _cookie[propertyName];
            }
            return null;
        }
        public static void WriteValue(string propertyName, object value)
        {
            string cookieName = InfoCookie;
            Dictionary<string, object> _dict = CookieValue;
            NameValueCollection _cookie;
            if (_dict.ContainsKey(cookieName))
                _cookie = (NameValueCollection)_dict[cookieName];
            else
            {
                _cookie = new NameValueCollection();
                _dict[cookieName] = _cookie;
            }
            _cookie[propertyName] = value.ToString();
        }
        public static void WriteValue(string cookieName, string value)
        {
            Dictionary<string, object> _dict = CookieValue;
            _dict[cookieName] = value;
        }
        public static string ReadValue(string cookieName, string propertyName)
        {
            Dictionary<string, object> _dict = CookieValue;
            if (_dict.ContainsKey(cookieName))
            {
                NameValueCollection _cookie = (NameValueCollection)_dict[cookieName];
                return _cookie[propertyName];
            }
            return null;
        }
        public static void ReadCookieValues()
        {
            Dictionary<string, object> _dict = new Dictionary<string, object>();
            string[] appCookies = Common.AppCookies.Split(',');
            foreach (string cookieName in context.Request.Cookies)
            {
                if (appCookies.Contains(cookieName))
                {
                    HttpCookie cookie = context.Request.Cookies[cookieName];
                    if (cookie.HasKeys)
                    {
                        NameValueCollection src = cookie.Values;
                        NameValueCollection dest = new NameValueCollection();
                        src.AllKeys.ForEach(str => dest.Add(str, HttpUtility.UrlDecode(src[str])));
                        _dict[cookieName] = dest;
                    }
                    else
                        _dict[cookieName] = HttpUtility.UrlDecode(cookie.Value);
                }
            }
            Common.CookieValue = _dict;
        }
        public static void WriteCookieValues()
        {
            Dictionary<string, object> _dict = Common.CookieValue;
            if (_dict != null)
                foreach (var value in _dict)
                {
                    HttpCookie cookie = context.Response.Cookies[value.Key];
                    if (cookie == null)
                        if (value.Value is NameValueCollection)
                        {
                            cookie = new HttpCookie(value.Key);
                            NameValueCollection src = (NameValueCollection)value.Value;
                            NameValueCollection dest = cookie.Values;
                            src.AllKeys.ForEach(str => dest.Add(str, HttpUtility.UrlEncode(src[str])));
                            context.Response.Cookies.Add(cookie);
                        }
                        else
                            context.Response.Cookies.Add(new HttpCookie(value.Key, (string)value.Value)
                            {
                                HttpOnly = false
                            });
                    else if (value.Value is NameValueCollection)
                    {
                        NameValueCollection src = (NameValueCollection)value.Value;
                        NameValueCollection dest = cookie.Values;
                        src.AllKeys.ForEach(str => dest.Add(str, HttpUtility.UrlEncode(src[str])));
                        context.Response.Cookies.Add(cookie);
                    }
                    else cookie.Value = (string) value.Value;
                }
        }
        public static string ConstructQueryString(NameValueCollection parameters)
        {
            List<string> items = new List<string>();
            foreach (string name in parameters)
                items.Add(String.Concat(name, "=", HttpUtility.UrlEncode(parameters[name])));
            return String.Join("&", items.ToArray());
        }
        public static void WriteValue(string cookieName, string propertyName, object value)
        {
            Dictionary<string, object> _dict = CookieValue;
            NameValueCollection _cookie;
            if (_dict.ContainsKey(cookieName))
                _cookie = (NameValueCollection)_dict[cookieName];
            else
            {
                _cookie = new NameValueCollection();
                _dict[cookieName] = _cookie;
            }
            _cookie[propertyName] = value.ToString();
        }


        public static void WriteValue(string cookieName, JObject jobj)
        {
            Dictionary<string, object> _dict = CookieValue;
            NameValueCollection _cookie;
            if (_dict.ContainsKey(cookieName))
                _cookie = (NameValueCollection)_dict[cookieName];
            else
            {
                _cookie = new NameValueCollection();
                _dict[cookieName] = _cookie;
            }
            foreach (KeyValuePair<string, JToken> tok in jobj)
            {
                _cookie[tok.Key] = tok.Value.ToString().Trim('"');
            }
        }
        public static string LimitText(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            if (str.Length > 12)
            {
                str = str.Substring(0, 11) + "...";
            }
            return str;
        }
        [ThreadStatic]
        static IHash _Hash;
        public static IHash Hash
        {
            get
            {
                if (_Hash == null) _Hash = HashLib.HashFactory.Crypto.CreateSHA512();
                return _Hash;
            }
        }

        public sealed class image
        {
            public int ID;
            public int BIMID;
            public int? BoardID;
            public decimal? PinID;
            public XElement Comments;
            public XElement Cats;
            public bool? liked;
            public string Image_Title;
            public string RelativeImage_Path;
            public string Image_Source_URL;
            public short? Image_Height;
            public bool? Uploaded;
            public short? Image_Width;
            public int? UserID;
            public string Source;
        }
        public static class Compiled
        {
            public static System.Func<Data.dbml.DataContext, int, int, IEnumerable<image>> Images =
         System.Data.Linq.CompiledQuery.Compile<Data.dbml.DataContext, int, int, IEnumerable<image>>
             ((ctx, p, ps) =>
                 (from o in ctx.Vw_Images
                  orderby o.Rating descending
                  select new image
                  {
                      ID = o.ID,
                      UserID = o.UserID,
                      BoardID = o.BoardID,
                      PinID = o.PinID,
                      Image_Title = o.Image_Title,
                      BIMID = o.BIMID,
                      RelativeImage_Path = o.RelativeImage_Path,
                      Comments = ctx.ForXML_vw_UserComments(o.BIMID),
                      Cats = ctx.ForXML_vw_Cat(o.ID),
                      Source = o.Source,
                      Image_Height = o.Image_Height,
                      Uploaded = o.Uploaded,
                      Image_Width = o.Image_Width
                  }).Skip(p * ps).Take(ps));
            public static System.Func<Data.dbml.DataContext, string, int, int, IEnumerable<image>> ImagesStyle =
     System.Data.Linq.CompiledQuery.Compile<Data.dbml.DataContext, string, int, int, IEnumerable<image>>
         ((ctx, style, p, ps) =>
             (from o in ctx.Vw_Images
              where (from o1 in ctx.Vw_ImgCategory where o1.Name.ToLower() == style.ToLower() select o1.ID).Contains(o.ID)
              orderby o.Rating descending
              select new image
              {
                  ID = o.ID,
                  UserID = o.UserID,
                  BoardID = o.BoardID,
                  PinID = o.PinID,
                  Image_Title = o.Image_Title,
                  BIMID = o.BIMID,
                  RelativeImage_Path = o.RelativeImage_Path,
                  Comments = ctx.ForXML_vw_UserComments(o.BIMID),
                  Cats = ctx.ForXML_vw_Cat(o.ID),
                  Source = o.Source,
                  Image_Height = o.Image_Height,
                  Uploaded = o.Uploaded,
                  Image_Width = o.Image_Width
              }).Skip(p * ps).Take(ps));
            public static System.Func<Data.dbml.DataContext, int, int, int?, string, bool, IEnumerable<image>> BoardImages =
        System.Data.Linq.CompiledQuery.Compile<Data.dbml.DataContext, int, int, int?, string, bool, IEnumerable<image>>
            ((ctx, p, ps, userId, board, visitor) =>
               (from o in ctx.Vw_Images
                where o.BoardName == board
                orderby o.Rating descending
                select new image
                {
                    ID = o.ID,
                    liked = visitor ? false : ctx.Liked(userId, o.BIMID),
                    UserID = o.UserID,
                    BoardID = o.BoardID,
                    PinID = o.PinID,
                    Image_Title = o.Image_Title,
                    BIMID = o.BIMID,
                    RelativeImage_Path = o.RelativeImage_Path,
                    Comments = ctx.ForXML_vw_UserComments(o.BIMID),
                    Cats = ctx.ForXML_vw_Cat(o.ID),
                    Source = o.Source,
                    Image_Height = o.Image_Height,
                    Uploaded = o.Uploaded,
                    Image_Width = o.Image_Width
                }).OrderByDescending(o => o.ID).Skip(p * ps).Take(ps));
            public static System.Func<Data.dbml.DataContext, int, int, string, IEnumerable<image>> Query =
        System.Data.Linq.CompiledQuery.Compile<Data.dbml.DataContext, int, int, string, IEnumerable<image>>
            ((ctx, p, ps, q) =>
               (from o in ctx.Vw_Images
                where o.Image_Title.StartsWith(q)
                orderby o.Rating descending
                select new image
                {
                    ID = o.ID,
                    UserID = o.UserID,
                    BoardID = o.BoardID,
                    PinID = o.PinID,
                    Image_Title = o.Image_Title,
                    BIMID = o.BIMID,
                    RelativeImage_Path = o.RelativeImage_Path,
                    Comments = ctx.ForXML_vw_UserComments(o.BIMID),
                    Cats = ctx.ForXML_vw_Cat(o.ID),
                    Source = o.Source,
                    Image_Height = o.Image_Height,
                    Uploaded = o.Uploaded,
                    Image_Width = o.Image_Width
                }).OrderByDescending(o => o.ID).Skip(p * ps).Take(ps));

            public static System.Func<Data.dbml.DataContext, int, int, int?, string, IEnumerable<image>> QueryUser =
       System.Data.Linq.CompiledQuery.Compile<Data.dbml.DataContext, int, int, int?, string, IEnumerable<image>>
           ((ctx, p, ps, userId, q) =>
              (from o in ctx.Vw_Images
               where o.Image_Title.StartsWith(q)
               orderby o.Rating descending
               select new image
               {
                   ID = o.ID,
                   liked = userId.HasValue ? ctx.Liked(userId, o.BIMID) : false,
                   UserID = o.UserID,
                   BoardID = o.BoardID,
                   PinID = o.PinID,
                   Image_Title = o.Image_Title,
                   BIMID = o.BIMID,
                   RelativeImage_Path = o.RelativeImage_Path,
                   Comments = ctx.ForXML_vw_UserComments(o.BIMID),
                   Cats = ctx.ForXML_vw_Cat(o.ID),
                   Source = o.Source,
                   Image_Height = o.Image_Height,
                   Uploaded = o.Uploaded,
                   Image_Width = o.Image_Width
               }).OrderByDescending(o => o.ID).Skip(p * ps).Take(ps));

            public static System.Func<Data.dbml.DataContext, int, int, string, string, IEnumerable<image>> CategoryImages =
           System.Data.Linq.CompiledQuery.Compile<Data.dbml.DataContext, int, int, string, string, IEnumerable<image>>
               ((ctx, p, ps, cats, style) =>
                   (from o in ctx.Vw_ImagesforCategories
                    where (from o1 in ctx.Vw_ImgCategory join o2 in ctx.String_Split(cats, ",") on o1.CategoryID equals Convert.ToInt32(o2.Item) select o1.ID).Contains(o.ID)
                    && (style == null || style == "All" || (from o1 in ctx.Vw_ImgCategory where (o1.Name.ToLower() == style) select o1.ID).Contains(o.ID))
                    orderby o.Rating descending
                    select new image
                    {
                        ID = o.ID,
                        UserID = o.UserID,
                        BoardID = o.BoardID,
                        PinID = o.PinID,
                        Image_Title = o.Image_Title,
                        BIMID = o.BIMID,
                        RelativeImage_Path = o.RelativeImage_Path,
                        Comments = ctx.ForXML_vw_UserComments(o.BIMID),
                        Cats = ctx.ForXML_vw_Cat(o.ID),
                        Source = o.Source,
                        Image_Height = o.Image_Height,
                        Uploaded = o.Uploaded,
                        Image_Width = o.Image_Width
                    }).Skip(p * ps).Take(ps));

            public static System.Func<Data.dbml.DataContext, int, int, int?, string, string, IEnumerable<image>> CategoryImagesUser =
    System.Data.Linq.CompiledQuery.Compile<Data.dbml.DataContext, int, int, int?, string, string, IEnumerable<image>>
        ((ctx, p, ps, userId, cats, style) =>
            (from o in ctx.Vw_ImagesforCategories
             where (from o1 in ctx.Vw_ImgCategory join o2 in ctx.String_Split(cats, ",") on o1.CategoryID equals Convert.ToInt32(o2.Item) select o1.ID).Contains(o.ID)
              && (style == null || style == "All" || (from o1 in ctx.Vw_ImgCategory where (o1.Name.ToLower() == style) select o1.ID).Contains(o.ID))
             orderby o.Rating descending
             select new image
             {
                 ID = o.ID,
                 UserID = o.UserID,
                 liked = userId.HasValue ? ctx.Liked(userId, o.BIMID) : false,
                 BoardID = o.BoardID,
                 PinID = o.PinID,
                 Image_Title = o.Image_Title,
                 BIMID = o.BIMID,
                 RelativeImage_Path = o.RelativeImage_Path,
                 Comments = ctx.ForXML_vw_UserComments(o.BIMID),
                 Cats = ctx.ForXML_vw_Cat(o.ID),
                 Source = o.Source,
                 Image_Height = o.Image_Height,
                 Uploaded = o.Uploaded,
                 Image_Width = o.Image_Width
             }).Skip(p * ps).Take(ps));

            public static System.Func<Data.dbml.DataContext, int, int, string, IEnumerable<image>> ImagesinCategories =
          System.Data.Linq.CompiledQuery.Compile<Data.dbml.DataContext, int, int, string, IEnumerable<image>>
              ((ctx, p, ps, cats) =>
                  (from o in ctx.Vw_ImagesforCategories
                   where (from o1 in ctx.Vw_ImgCategory join o2 in ctx.String_Split(cats, ",") on o1.CategoryID.ToString() equals o2.Item select o1.ID).Contains(o.ID)
                   orderby o.Rating descending
                   select new image
                   {
                       ID = o.ID,
                       UserID = o.UserID,
                       BoardID = o.BoardID,
                       PinID = o.PinID,
                       Image_Title = o.Image_Title,
                       BIMID = o.BIMID,
                       RelativeImage_Path = o.RelativeImage_Path,
                       Comments = ctx.ForXML_vw_UserComments(o.BIMID),
                       Cats = ctx.ForXML_vw_Cat(o.ID),
                       Source = o.Source,
                       Image_Height = o.Image_Height,
                       Uploaded = o.Uploaded,
                       Image_Width = o.Image_Width
                   }).Skip(p * ps).Take(ps));
            public static System.Func<Data.dbml.DataContext, int, int, int?, string, bool, IEnumerable<image>> ImagesinCategoriesUsers =
       System.Data.Linq.CompiledQuery.Compile<Data.dbml.DataContext, int, int, int?, string, bool, IEnumerable<image>>
           ((ctx, p, ps, userId, cats, visitor) =>
               (from o in ctx.Vw_ImagesforCategories
                where (from o1 in ctx.Vw_ImgCategory join o2 in ctx.String_Split(cats, ",") on o1.CategoryID.ToString() equals o2.Item select o1.ID).Contains(o.ID)
                orderby o.Rating descending
                select new image
                {
                    ID = o.ID,
                    UserID = o.UserID,
                    BoardID = o.BoardID,
                    liked = visitor ? false : ctx.Liked(userId, o.BIMID),
                    PinID = o.PinID,
                    Image_Title = o.Image_Title,
                    BIMID = o.BIMID,
                    RelativeImage_Path = o.RelativeImage_Path,
                    Comments = ctx.ForXML_vw_UserComments(o.BIMID),
                    Cats = ctx.ForXML_vw_Cat(o.ID),
                    Source = o.Source,
                    Image_Height = o.Image_Height,
                    Uploaded = o.Uploaded,
                    Image_Width = o.Image_Width
                }).Skip(p * ps).Take(ps));
            public static System.Func<Data.dbml.DataContext, int, int, int?, int?, IEnumerable<image>> Likes =
          System.Data.Linq.CompiledQuery.Compile<Data.dbml.DataContext, int, int, int?, int?, IEnumerable<image>>
              ((ctx, p, ps, userId, visitedruserId) =>
                  (from o in ctx.Vw_Images
                   where (from o1 in ctx.Likes
                          where o1.UserID == (visitedruserId ?? userId)
                          select o1.BoardsImagesMappingID).Contains(o.BIMID)
                   select new image
                   {
                       ID = o.ID,
                       UserID = o.UserID,
                       liked = userId.HasValue ? ctx.Liked(userId.Value, o.BIMID) : false,
                       BoardID = o.BoardID,
                       PinID = o.PinID,
                       Image_Title = o.Image_Title,
                       BIMID = o.BIMID,
                       RelativeImage_Path = o.RelativeImage_Path,
                       Comments = ctx.ForXML_vw_UserComments(o.BIMID),
                       Cats = ctx.ForXML_vw_Cat(o.ID),
                       Source = o.Source,
                       Image_Height = o.Image_Height,
                       Uploaded = o.Uploaded,
                       Image_Width = o.Image_Width
                   }).Skip(p * ps).Take(ps));
            public static System.Func<Data.dbml.DataContext, int, int, int?, int?, IEnumerable<image>> Pins =
         System.Data.Linq.CompiledQuery.Compile<Data.dbml.DataContext, int, int, int?, int?, IEnumerable<image>>
             ((ctx, p, ps, userId, visitedruserId) =>
                 (from o in ctx.Vw_Images
                  where o.UserID == (visitedruserId ?? userId)
                  select new image
                  {
                      ID = o.ID,
                      UserID = o.UserID,
                      liked = userId.HasValue ? ctx.Liked(userId.Value, o.BIMID) : false,
                      BoardID = o.BoardID,
                      PinID = o.PinID,
                      Image_Title = o.Image_Title,
                      BIMID = o.BIMID,
                      RelativeImage_Path = o.RelativeImage_Path,
                      Comments = ctx.ForXML_vw_UserComments(o.BIMID),
                      Cats = ctx.ForXML_vw_Cat(o.ID),
                      Source = o.Source,
                      Image_Height = o.Image_Height,
                      Uploaded = o.Uploaded,
                      Image_Width = o.Image_Width
                  }).Skip(p * ps).Take(ps));
            public static System.Func<Data.dbml.DataContext, int?, IEnumerable<object>> Boards =
                System.Data.Linq.CompiledQuery.Compile<Data.dbml.DataContext, int?, IEnumerable<object>>
                ((ctx, userId) =>
                    (from o in ctx.Boards
                     where o.UserID == userId
                     select new
                     {
                         id = o.ID,
                         name = o.Name,
                         catname = o.Category.Name,
                         catid = o.Category.ID,
                         pins = o.BoardsImagesMapping.Count(),
                         boardCollaborators = (from o1 in ctx.BoardContributor
                                               where o.ID == o1.BoardID
                                               select new
                                               {
                                                   o1.AppUsers.Email,
                                                   o1.AppUsers.Avatar,
                                                   o1.AppUsers.Name,
                                                   o1.AppUsers.FirstName
                                               }),
                         images = (from o1 in o.BoardsImagesMapping
                                   select new
                                   {
                                       o1.ID,
                                       url = ((o1.Images.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o1.Images.RelativeImage_Path,
                                       height = o1.Images.Image_Height,
                                       width = o1.Images.Image_Width
                                   }).Take(5)
                     }));
            public static System.Func<Data.dbml.DataContext, string, IEnumerable<object>> QBoards = System.Data.Linq.CompiledQuery.Compile<Data.dbml.DataContext, string, IEnumerable<object>>
    ((ctx, name) =>
        (from o in ctx.Boards
         where o.Name.StartsWith(name)
         select new
         {
             id = o.ID,
             name = o.Name,
             catname = o.Category.Name,
             catid = o.Category.ID,
             pins = o.BoardsImagesMapping.Count(),
             boardCollaborators = (from o1 in ctx.BoardContributor where o.ID == o1.BoardID select new { o1.AppUsers.Email, o1.AppUsers.Avatar, o1.AppUsers.Name, o1.AppUsers.FirstName }),
             images = (from o1 in o.BoardsImagesMapping
                       select new
                       {
                           o1.ID,
                           url = ((o1.Images.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o1.Images.RelativeImage_Path,
                           height = o1.Images.Image_Height,
                           width = o1.Images.Image_Width
                       }).Take(5)
         }));
        }

        internal static string GetBase64Image(string path, short height)
        {
            byte[] file = File.ReadAllBytes(path);
            Image img = Image.FromStream(new MemoryStream(file));
            MemoryStream stream = new MemoryStream();
            img = img.GetThumbnailImage(190, height, null, IntPtr.Zero);
            img.Save(stream, ImageFormat.Png);
            return "data:image/png;base64," + Convert.ToBase64String(stream.ToArray());
        }

    }
}
