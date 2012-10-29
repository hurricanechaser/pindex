using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.IO;
using System.Drawing;
using Newtonsoft.Json.Linq;
using System.Net;

using System.Collections;
using System.Security.Cryptography;
using HashLib;

using Newtonsoft.Json;
using System.Xml;
using System.Xml.Linq;
using Amib.Threading;

/// <summary>
/// Summary description for Common
/// </summary>
namespace Lipstick
{
    public static class Common
    {
        public const string sessioncookie = "session";

        public static int PageSize
        {
            get
            {
                return Convert.ToInt32(WebConfigurationManager.AppSettings["pageSize"]);
            }
        }
        public static string ImagePath
        {
            get
            {
                return WebConfigurationManager.AppSettings["imagepath"];
            }
        }
        public static string Domain
        {
            get
            {
                return WebConfigurationManager.AppSettings["domain"];
            }
        }
        public static string UploadedImagePath
        {
            get
            {
                return WebConfigurationManager.AppSettings["uploadedimagepath"];
            }
        }
        public static string UploadedImageRelPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["uploadedimagerelpath"];
            }
        }
        public static string RelTemp
        {
            get
            {
                return WebConfigurationManager.AppSettings["reltemp"];
            }
        }
        public static string Temp
        {
            get
            {
                return WebConfigurationManager.AppSettings["temp"];
            }
        }
        public static string AuthCookie
        {
            get
            {
                return WebConfigurationManager.AppSettings["authcookie"];
            }
        }
        public static string InfoCookie
        {
            get
            {
                return WebConfigurationManager.AppSettings["infocookie"];
            }
        }
        public static string LipstickConnectionString
        {
            get
            {
                return WebConfigurationManager.ConnectionStrings["LipstickConnectionString"].ConnectionString;
            }
        }
        public static string PinStatsConnectionString
        {
            get
            {
                return WebConfigurationManager.ConnectionStrings["PinStatsConnectionString"].ConnectionString;
            }
        }
        public static string LipstickEntities
        {
            get
            {
                return WebConfigurationManager.ConnectionStrings["LipstickEntities"].ConnectionString;
            }
        }
        public static string PinStatsEntities
        {
            get
            {
                return WebConfigurationManager.ConnectionStrings["PinStatsEntities"].ConnectionString;
            }
        }
        public static string ContentUrl
        {
            get
            {
                return WebConfigurationManager.AppSettings["contenturl"];
            }
        }
        public static string ContentUrl1
        {
            get
            {
                return WebConfigurationManager.AppSettings["contenturl1"];
            }
        }
        public static int PassMinChars
        {
            get
            {
                return Convert.ToInt32(WebConfigurationManager.AppSettings["passminchars"]);
            }
        }
        public static int FirstPageSize
        {
            get
            {
                return Convert.ToInt32(WebConfigurationManager.AppSettings["firstPageSize"]);
            }
        }
        public static string InviteUrl
        {
            get
            {
                return WebConfigurationManager.AppSettings["inviteurl"];
            }
        }
        static SmartThreadPool _STP;
        public static SmartThreadPool STP
        {
            get
            {
                if (_STP == null)
                    _STP = new SmartThreadPool(100);
                return _STP;
            }
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

        public static void WriteJsonP(this HttpContext context, string output)
        {
            string pre = context.Request.Params["pre"];
            if (string.IsNullOrEmpty(pre))
                context.Response.Write(output);
            else context.Response.Write(string.Format("{0}={1}", pre, output));
        }
        public static void WriteError(this HttpResponse response, string output)
        {
            response.StatusCode = 500;
            response.Write(output);
        }
        public static string RemoveSpecialCharacters(string input)
        {
            Regex r = new Regex("(?:[^a-z0-9._]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, RandomString(1));
        }
        public static void UpdateCookie(string cookieName, JObject values)
        {
            string json = context.Server.UrlDecode(CookieUtil.ReadCookie(cookieName));
            if (!string.IsNullOrEmpty(json))
            {
                JObject obj = JObject.Parse(json);
                foreach (var tk in obj)
                {
                    values[tk.Key] = tk.Value;
                }
            }
            CookieUtil.WriteCookie(cookieName, values.ToString(), false);
        }
        public static void RemoveValueinCookie(string cookieName, string[] values)
        {
            string json = context.Server.UrlDecode(CookieUtil.ReadCookie(cookieName));
            if (!string.IsNullOrEmpty(json))
            {
                JObject obj = JObject.Parse(json);
                foreach (string tk in values)
                {
                    obj.Remove(tk);
                }
                CookieUtil.WriteCookie(cookieName, obj.ToString(), false);
            }
        }
        private static readonly Random _rng = new Random();
        private static readonly string _chars = "!@#$%^&*()ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

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
                    ret.Add(JObject.Parse(JsonConvert.SerializeXNode(node, Newtonsoft.Json.Formatting.Indented, true).Replace("@", string.Empty)));
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
                string rv = ReadValue(sessioncookie, "pts");
                return string.IsNullOrEmpty(rv) ? null : (int?)(JObject.Parse(rv)["total"]);
            }
        }
        public static int? UserID
        {
            get
            {
                string rv = ReadValue(AuthCookie, "ID", true);
                return string.IsNullOrEmpty(rv) ? null : (int?)Convert.ToInt32(rv);
            }
        }
        public static string SessionID
        {
            get
            {
                string rv = ReadValue(sessioncookie, "id");
                return rv;
            }
        }
        public static string ReadValue(string propertyName)
        {
            if (CookieUtil.CookieExists(InfoCookie))
            {
                JObject _cookie = JObject.Parse(context.Server.UrlDecode(CookieUtil.ReadCookie(InfoCookie)));
                JToken tok = _cookie[propertyName];
                return (tok == null) || (tok.Type == JTokenType.Null) || (tok.Type == JTokenType.Undefined) || (tok.Type == JTokenType.None) ? null : _cookie[propertyName].ToString().Trim('"');
            }
            return null;
        }
        public static string ReadValue(string cookieName, string propertyName)
        {
            if (CookieUtil.CookieExists(cookieName))
            {
                JObject _cookie = JObject.Parse(context.Server.UrlDecode(CookieUtil.ReadCookie(cookieName)));
                JToken tok = _cookie[propertyName];
                return (tok == null) || (tok.Type == JTokenType.Null) || (tok.Type == JTokenType.Undefined) || (tok.Type == JTokenType.None) ? null : _cookie[propertyName].ToString().Trim('"');
            }
            return null;
        }
        public static string ReadValue(string cookieName, string propertyName, bool decrypt)
        {
            if (CookieUtil.CookieExists(cookieName))
            {
                JObject _cookie;
                if (decrypt)
                {

                    string val = EncDec.Decrypt(CookieUtil.ReadCookie(cookieName), DefaultPassword);
                    _cookie = JObject.Parse(val);
                    JToken tok = _cookie[propertyName];
                    return (tok == null) || (tok.Type == JTokenType.Null) || (tok.Type == JTokenType.Undefined) || (tok.Type == JTokenType.None) ? null : _cookie[propertyName].ToString().Trim('"');

                }
                return ReadValue(cookieName, propertyName);
            }
            return null;
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
        public static string DefaultPassword
        {
            get
            {
                return WebConfigurationManager.AppSettings["defaultpassword"];
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

            public static System.Func<Lipstick.dbml.LipstickDataContext, int, int, int?, string, IEnumerable<image>> BoardImages =
        System.Data.Linq.CompiledQuery.Compile<Lipstick.dbml.LipstickDataContext, int, int, int?, string, IEnumerable<image>>
            ((ctx, p, ps, userId, board) =>
               (from o in ctx.Vw_Images
                where o.BoardName == board
                orderby o.Rating descending
                select new image
                {
                    ID = o.ID,
                    liked = ctx.Liked(userId, o.BIMID),
                    UserID = o.UserID,
                    BoardID = o.BoardID,
                    PinID = o.PinID,
                    Image_Title = o.Image_Title,
                    BIMID = o.BIMID,
                    RelativeImage_Path = o.RelativeImage_Path,
                    Comments = o.Comments,
                    Cats = o.Cats,
                    Source = o.Source,
                    Image_Height = o.Image_Height,
                    Uploaded = o.Uploaded,
                    Image_Width = o.Image_Width
                }).OrderByDescending(o => o.ID).Skip(p * ps).Take(ps));
            public static System.Func<Lipstick.dbml.LipstickDataContext, int, int, string, IEnumerable<image>> Query =
        System.Data.Linq.CompiledQuery.Compile<Lipstick.dbml.LipstickDataContext, int, int, string, IEnumerable<image>>
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
                    Comments = o.Comments,
                    Cats = o.Cats,
                    Source = o.Source,
                    Image_Height = o.Image_Height,
                    Uploaded = o.Uploaded,
                    Image_Width = o.Image_Width
                }).OrderByDescending(o => o.ID).Skip(p * ps).Take(ps));

            public static System.Func<Lipstick.dbml.LipstickDataContext, int, int, int?, string, IEnumerable<image>> QueryUser =
       System.Data.Linq.CompiledQuery.Compile<Lipstick.dbml.LipstickDataContext, int, int, int?, string, IEnumerable<image>>
           ((ctx, p, ps, userId, q) =>
              (from o in ctx.Vw_Images
               where o.Image_Title.StartsWith(q)
               orderby o.Rating descending
               select new image
               {
                   ID = o.ID,
                   liked = ctx.Liked(userId, o.BIMID),
                   UserID = o.UserID,
                   BoardID = o.BoardID,
                   PinID = o.PinID,
                   Image_Title = o.Image_Title,
                   BIMID = o.BIMID,
                   RelativeImage_Path = o.RelativeImage_Path,
                   Comments = o.Comments,
                   Cats = o.Cats,
                   Source = o.Source,
                   Image_Height = o.Image_Height,
                   Uploaded = o.Uploaded,
                   Image_Width = o.Image_Width
               }).OrderByDescending(o => o.ID).Skip(p * ps).Take(ps));

            public static System.Func<Lipstick.dbml.LipstickDataContext, int, int, string, IEnumerable<image>> CategoryImages =
           System.Data.Linq.CompiledQuery.Compile<Lipstick.dbml.LipstickDataContext, int, int, string, IEnumerable<image>>
               ((ctx, p, ps, name) =>
                   (from o in ctx.Vw_Images
                    where (from o1 in ctx.Vw_ImgCategory where o1.Name == name select o1.ID).Contains(o.ID)
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
                        Comments = o.Comments,
                        Cats = o.Cats,
                        Source = o.Source,
                        Image_Height = o.Image_Height,
                        Uploaded = o.Uploaded,
                        Image_Width = o.Image_Width
                    }).Skip(p * ps).Take(ps));

            public static System.Func<Lipstick.dbml.LipstickDataContext, int, int, int?, string, IEnumerable<image>> CategoryImagesUser =
    System.Data.Linq.CompiledQuery.Compile<Lipstick.dbml.LipstickDataContext, int, int, int?, string, IEnumerable<image>>
        ((ctx, p, ps, userId, name) =>
            (from o in ctx.Vw_Images
             where (from o1 in ctx.Vw_ImgCategory where o1.Name == name select o1.ID).Contains(o.ID)
             orderby o.Rating descending
             select new image
             {
                 ID = o.ID,
                 UserID = o.UserID,
                 liked = ctx.Liked(userId, o.BIMID),
                 BoardID = o.BoardID,
                 PinID = o.PinID,
                 Image_Title = o.Image_Title,
                 BIMID = o.BIMID,
                 RelativeImage_Path = o.RelativeImage_Path,
                 Comments = o.Comments,
                 Cats = o.Cats,
                 Source = o.Source,
                 Image_Height = o.Image_Height,
                 Uploaded = o.Uploaded,
                 Image_Width = o.Image_Width
             }).Skip(p * ps).Take(ps));
            public static System.Func<Lipstick.dbml.LipstickDataContext, int, int,  IEnumerable<image>> Images =
         System.Data.Linq.CompiledQuery.Compile<Lipstick.dbml.LipstickDataContext, int, int,  IEnumerable<image>>
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
                      Comments = o.Comments,
                      Cats = o.Cats,
                      Source = o.Source,
                      Image_Height = o.Image_Height,
                      Uploaded = o.Uploaded,
                      Image_Width = o.Image_Width
                  }).Skip(p * ps).Take(ps));
            public static System.Func<Lipstick.dbml.LipstickDataContext, int, int, int?,  IEnumerable<image>> ImagesUsers =
     System.Data.Linq.CompiledQuery.Compile<Lipstick.dbml.LipstickDataContext, int, int, int?,  IEnumerable<image>>
         ((ctx, p, ps, userId) =>
             (from o in ctx.Vw_Images
              orderby o.Rating descending
              select new image
              {
                  ID = o.ID,
                  UserID = o.UserID,
                  BoardID = o.BoardID,
                  liked = ctx.Liked(userId, o.BIMID),
                  PinID = o.PinID,
                  Image_Title = o.Image_Title,
                  BIMID = o.BIMID,
                  RelativeImage_Path = o.RelativeImage_Path,
                  Comments = o.Comments,
                  Cats = o.Cats,
                  Source = o.Source,
                  Image_Height = o.Image_Height,
                  Uploaded = o.Uploaded,
                  Image_Width = o.Image_Width
              }).Skip(p * ps).Take(ps));
            public static System.Func<Lipstick.dbml.LipstickDataContext, int, int, string, IEnumerable<image>> ImagesinCategories =
          System.Data.Linq.CompiledQuery.Compile<Lipstick.dbml.LipstickDataContext, int, int, string, IEnumerable<image>>
              ((ctx, p, ps, cats) =>
                  (from o in ctx.Vw_Images
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
                       Comments = o.Comments,
                       Cats = o.Cats,
                       Source = o.Source,
                       Image_Height = o.Image_Height,
                       Uploaded = o.Uploaded,
                       Image_Width = o.Image_Width
                   }).Skip(p * ps).Take(ps));
            public static System.Func<Lipstick.dbml.LipstickDataContext, int, int, int?, string, IEnumerable<image>> ImagesinCategoriesUsers =
       System.Data.Linq.CompiledQuery.Compile<Lipstick.dbml.LipstickDataContext, int, int, int?, string, IEnumerable<image>>
           ((ctx, p, ps, userId, cats) =>
               (from o in ctx.Vw_Images
                where (from o1 in ctx.Vw_ImgCategory join o2 in ctx.String_Split(cats, ",") on o1.CategoryID.ToString() equals o2.Item select o1.ID).Contains(o.ID)
                orderby o.Rating descending
                select new image
                {
                    ID = o.ID,
                    UserID = o.UserID,
                    BoardID = o.BoardID,
                    liked = ctx.Liked(userId, o.BIMID),
                    PinID = o.PinID,
                    Image_Title = o.Image_Title,
                    BIMID = o.BIMID,
                    RelativeImage_Path = o.RelativeImage_Path,
                    Comments = o.Comments,
                    Cats = o.Cats,
                    Source = o.Source,
                    Image_Height = o.Image_Height,
                    Uploaded = o.Uploaded,
                    Image_Width = o.Image_Width
                }).Skip(p * ps).Take(ps));
            public static System.Func<Lipstick.dbml.LipstickDataContext, int, int, int?, IEnumerable<image>> Likes =
          System.Data.Linq.CompiledQuery.Compile<Lipstick.dbml.LipstickDataContext, int, int, int?, IEnumerable<image>>
              ((ctx, p, ps, userId) =>
                  (from o in ctx.Vw_Images
                   where (from o1 in ctx.Likes
                          where o1.UserID == userId
                          select o1.BoardsImagesMappingID).Contains(o.BIMID)
                   select new image
                   {
                       ID = o.ID,
                       UserID = o.UserID,
                       liked = true,
                       BoardID = o.BoardID,
                       PinID = o.PinID,
                       Image_Title = o.Image_Title,
                       BIMID = o.BIMID,
                       RelativeImage_Path = o.RelativeImage_Path,
                       Comments = o.Comments,
                       Cats = o.Cats,
                       Source = o.Source,
                       Image_Height = o.Image_Height,
                       Uploaded = o.Uploaded,
                       Image_Width = o.Image_Width
                   }).Skip(p * ps).Take(ps));
            public static System.Func<Lipstick.dbml.LipstickDataContext, int, int, int?, IEnumerable<image>> Pins =
         System.Data.Linq.CompiledQuery.Compile<Lipstick.dbml.LipstickDataContext, int, int, int?, IEnumerable<image>>
             ((ctx, p, ps, userId) =>
                 (from o in ctx.Vw_Images
                  where o.BoardUserID == userId
                  select new image
                  {
                      ID = o.ID,
                      UserID = o.UserID,
                      liked = ctx.Liked(userId, o.BIMID),
                      BoardID = o.BoardID,
                      PinID = o.PinID,
                      Image_Title = o.Image_Title,
                      BIMID = o.BIMID,
                      RelativeImage_Path = o.RelativeImage_Path,
                      Comments = o.Comments,
                      Cats = o.Cats,
                      Source = o.Source,
                      Image_Height = o.Image_Height,
                      Uploaded = o.Uploaded,
                      Image_Width = o.Image_Width
                  }).Skip(p * ps).Take(ps));
            public static System.Func<Lipstick.dbml.LipstickDataContext, int?, IEnumerable<object>> Boards =
        System.Data.Linq.CompiledQuery.Compile<Lipstick.dbml.LipstickDataContext, int?, IEnumerable<object>>
            ((ctx, userId) =>
                (from o in ctx.Boards
                 where o.UserID == userId
                 select new
                 {
                     id = o.ID,
                     name = o.Name,
                     catname=o.Category.Name,
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
    }
}