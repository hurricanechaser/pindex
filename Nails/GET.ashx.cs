using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.Data;
using Newtonsoft.Json;

using System.Threading;
using System.Diagnostics;

using System.ComponentModel;
using System.Dynamic;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Xml.Linq;
using Microsoft.ApplicationBlocks.Data;
using LinqKit;
using System.Data.Objects;
using Utilities;
using System.Collections;

namespace Nails
{
    /// <summary>
    /// Summary description for GET
    /// </summary>
    public class GET : HttpHandlerBase
    {
        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            switch (context.Request.QueryString["t"])
            {
                case "getimages":
                    GetImages(context);
                    break;
                case "getimagesandcategoriespaged":
                    GetImagesAndCategoriesPaged(context);
                    break;
                case "getpages":
                    GetPages(context);
                    break;
                case "getimagesinurl":
                    GetImagesinUrl2(context);
                    break;
                case "getcontentinurl":
                    GetContentinUrl(context);
                    break;
                case "getcategories":
                    GetCategories(context);
                    break;
                case "getboards":
                    GetBoards(context);
                    break;
                case "getqboards":
                    GetQBoards(context);
                    break;
                case "getloggedinloaddata":
                    GetLoggedinLoadData(context);
                    break;
                case "invite":
                    Invite(context);
                    break;
                case "getstores":
                    GetStores(context);
                    break;
                case "getarticles":
                    GetArticles(context);
                    break;
                case "getpin":
                    GetPin(context);
                    break;
                case "getfollowing":
                    GetFollowing(context);
                    break;
                case "getfollower":
                    GetFollower(context);
                    break;
                case "getfollowstatus":
                    GetFollowStatus(context);
                    break;
                case "getqpeople":
                    GetQPeople(context);
                    break;
            }
            base.EndRequest(context);
        }

        private void GetContentinUrl(HttpContext context)
        {
            string url = context.Request.QueryString["url"];
            WebClient client = new WebClient();
            string html=client.DownloadString(url);
            context.Response.Write(html);
        }

        private void GetQPeople(HttpContext context)
        {

            string name = context.Request.QueryString["name"];
            Func<JObject, JObject> proc = o1 =>
                                              {
                                                  string path = Common.ContentUrl + (string)o1["RelativeImage_Path"];
                                                  o1["RelativeImage_Path"] = path;
                                                  return o1;
                                              };
            int cur_user = Convert.ToInt32(Common.UserID) > 0 ? Convert.ToInt32(Common.UserID) : 0;

            var FgUsers = (from f in GetDataContext2.Vw_User
                           where f.Name.StartsWith(name) && (f.ID != cur_user)
                           select new
                           {
                               F_FullName = f.FirstName,
                               F_ID = f.ID,
                               F_UserName = f.Name,
                               F_Avatar = Common.FixAvatar(f.Avatar),
                               Fg_Count = f.Following_Count,
                               Fr_Count = f.Follower_Count,
                               F_Pin = Common.XmlToJArray(GetDataContext2.ForXML_vw_UserPins(f.ID), proc),
                               F_Status = GetDataContext2.FollowStatus(Common.UserID, f.ID)
                           });
            context.WriteJsonP(JsonConvert.SerializeObject(FgUsers, Formatting.Indented, Common.JsonSerializerSettings));
        }

        private void GetQBoards(HttpContext context)
        {
            string name = context.Request.QueryString["name"];
            var boards = Common.Compiled.QBoards(GetDataContext2, name);
            context.WriteJsonP(JsonConvert.SerializeObject(boards, Formatting.Indented, Common.JsonSerializerSettings));
        }

        private void GetPin(HttpContext context)
        {
            string pin = context.Request.Params["pin"];
            int? userID = Common.UserID;
            if (userID.HasValue)
                context.WriteJsonP(JsonConvert.SerializeObject((from o in GetDataContext2.Vw_Images
                                                                where o.PinID.Value.ToString() == pin
                                                                select new
                                                                {
                                                                    o.ID,
                                                                    o.BoardID,
                                                                    editable = o.UserID.HasValue && (o.UserID == userID),
                                                                    title = o.Image_Title,
                                                                    source = Common.GetAbsoluteUri(o.Source),
                                                                    imgsource = o.Source,
                                                                    Comments = Common.XmlToJArray(GetDataContext2.ForXML_vw_UserComments(o.BIMID)),
                                                                    o.BIMID,
                                                                    width = o.Image_Width,
                                                                    url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()
                                                                }).First(), Formatting.Indented, Common.JsonSerializerSettings));
            else
                context.WriteJsonP(JsonConvert.SerializeObject((from o in GetDataContext2.Vw_Images
                                                                where o.PinID.Value.ToString() == pin
                                                                select new
                                                                {
                                                                    o.ID,
                                                                    o.BoardID,
                                                                    title = o.Image_Title,
                                                                    source = Common.GetAbsoluteUri(o.Source),
                                                                    imgsource = o.Source,
                                                                    Comments = Common.XmlToJArray(GetDataContext2.ForXML_vw_UserComments(o.BIMID)),
                                                                    o.BIMID,
                                                                    width = o.Image_Width,
                                                                    url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()
                                                                }).First(), Formatting.Indented, Common.JsonSerializerSettings));
        }
        private void GetArticles(HttpContext context)
        {
            string cat = context.Request.QueryString["cat"];
            string q = context.Request.QueryString["q"];
            int p = int.Parse(context.Request.QueryString["p"]);
            int ps = Common.PageSize;
            var _q = (from o in this.GetDataContext2.Articles
                      select new
                      {
                          o.ID,
                          o.Description,
                          o.ArticleContent,
                          o.RelImagePath,
                          o.Title,
                          o.Url,
                          o.Image_Height,
                          o.Image_Width
                      }).OrderByDescending(o => o.ID).Skip(p * ps).Take(ps).AsEnumerable().Select((o, i) => new
                      {
                          url = Common.ContentUrl + o.RelImagePath,
                          title = o.Title,
                          content = o.ArticleContent,
                          desc = o.Description,
                          source = o.Url,
                          height = Math.Ceiling(380.00 * (Convert.ToDouble(o.Image_Height) / Convert.ToDouble(o.Image_Width)))
                      });
            context.WriteJsonP(JsonConvert.SerializeObject(_q, Formatting.Indented, Common.JsonSerializerSettings));
        }

        private void GetStores(HttpContext context)
        {
            int p = int.Parse(context.Request.QueryString["p"]);
            int ps = Common.PageSize;
            var _q = (from o in this.GetDataContext2.Stores
                      select new
                      {
                          o.ID,
                          o.RelImagePath,
                          o.Title,
                          o.Url,
                          o.Image_Height,
                          o.Image_Width
                      }).OrderByDescending(o => o.ID).Skip(p * ps).Take(ps).AsEnumerable().Select((o, i) => new
                      {
                          url = Common.ContentUrl + o.RelImagePath,
                          title = o.Title,
                          source = o.Url,
                          height = Math.Ceiling(190.00 * (Convert.ToDouble(o.Image_Height) / Convert.ToDouble(o.Image_Width)))
                      });
            context.WriteJsonP(JsonConvert.SerializeObject(_q, Formatting.Indented, Common.JsonSerializerSettings));
        }

        private void GetLoggedinLoadData(HttpContext context)
        {
            //List<object> boards = new List<object>();
            var boards = (from o in GetDataContext2.Boards
                          where o.UserID == Common.UserID ||
                          (from o1 in this.GetDataContext2.BoardContributor where o1.ContributorID == Common.UserID select o1.BoardID).Contains(o.ID)
                          select new
                          {
                              o.ID,
                              o.Name
                          }).Distinct();
            string about = (from o in GetDataContext2.AppUsers where o.ID == Common.UserID select o.About).First();
            context.WriteJsonP(JsonConvert.SerializeObject(new
            {
                boards,
                about
            }, Formatting.Indented, Common.JsonSerializerSettings));
        }

        private void Invite(HttpContext context)
        {
            string invite = context.Request.QueryString["s"];
            if (!string.IsNullOrEmpty(invite))
            {
                Data.dbml.AppUsers au = GetDataContext2.AppUsers.FirstOrDefault(o1 => o1.Invite == invite);
                if (au != null)
                {
                    Common.WriteValue(Common.AuthCookie, au.ID.ToString());
                    Common.WriteValue(Common.InfoCookie, JObject.FromObject(new
                    {
                        email = au.Email,
                        name = string.IsNullOrEmpty(au.FirstName) ? au.Name : au.FirstName,
                        avatar = string.IsNullOrWhiteSpace(au.Avatar) ? null : Common.UploadedImageRelPath + au.Avatar,
                        points = au.Points,
                        boards = 0,
                        pins = 0,
                        likes = 0,
                        flcnt = 0,
                        frcnt = 0
                    }));
                    context.Response.Redirect("~/#settings", false);
                }
            }
        }

        private void GetBoards(HttpContext context)
        {
            var boards = Common.Compiled.Boards(GetDataContext2, (Common.VUserID ?? Common.UserID));
            context.WriteJsonP(JsonConvert.SerializeObject(boards, Formatting.Indented, Common.JsonSerializerSettings));
        }

        private void GetImagesinUrl1(HttpContext context)
        {
            string url = context.Request.QueryString["url"];
            Regex img = new Regex("<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase);
            Regex src = new Regex(@"src=(?:(['""])(?<src>(?:(?!\1).)*)\1|(?<src>[^\s>]+))", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            WebClient source = new WebClient();
            string value = source.DownloadString(new Uri(url));
            if (!string.IsNullOrEmpty(value))
            {
                MatchCollection matches = img.Matches(value);
                if (matches.Count > 0)
                {
                    string rpath = Common.RelTemp + Common.UserID;
                    string path = Common.Temp + Common.UserID;
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    else
                    {
                        string[] files = Directory.GetFiles(path);
                        foreach (string file in files)
                        {
                            File.Delete(file);
                        }
                    }
                    int i = 0;
                    Dictionary<int, bool> _dict = new Dictionary<int, bool>();
                    List<string> paths = new List<string>();
                    AsyncCompletedEventHandler callback = (sender, e) =>
                                                              {
                                                                  dynamic dyn = (dynamic)e.UserState;
                                                                  _dict[dyn.i] = true;
                                                                  string rfn = rpath + dyn.i + ".jpg";
                                                                  paths.Add(rfn);
                                                              };
                    foreach (Match match in matches)
                    {
                        string imgUrl = match.Groups[1].Value;
                        WebClient client = new WebClient();
                        client.DownloadFileCompleted += callback;
                        string fn = path + "\\" + i + ".jpg";
                        dynamic dyn = new ExpandoObject();
                        dyn.fn = fn;
                        dyn.i = i;
                        client.DownloadFileAsync(new Uri(imgUrl), fn, dyn);
                        _dict.Add(i, false);
                        i++;
                    }
                    while (_dict.ContainsValue(false)) Thread.Sleep(500);
                    context.WriteJsonP(JsonConvert.SerializeObject(paths, Formatting.Indented, Common.JsonSerializerSettings));
                }
            }
        }

        private void GetImagesinUrl2(HttpContext context)
        {
            string url = context.Request.QueryString["url"];
            WebRequest source = WebRequest.Create(new Uri(url));
            WebResponse res = source.GetResponse();
            byte[] data = Common.ReadFully(res.GetResponseStream(), 0);
            string value = Encoding.Default.GetString(data);

            if (!string.IsNullOrEmpty(value))
            {
                if (res.ContentType.Contains("text/html"))
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(value);
                    var NoAltElements = doc.DocumentNode.SelectNodes("//img");
                    int i = 0;
                    List<string> paths = new List<string>();
                    string rpath = Common.RelTemp + Common.UserID;
                    string path = Common.Temp + Common.UserID;
                    if (NoAltElements != null)
                    {
                        foreach (HtmlNode HN in NoAltElements)
                        {
                            string imgUrl = HN.GetAttributeValue("src", "");
                            if (imgUrl != "") paths.Add(Common.MakeAbsoluteUri(url, imgUrl));
                        }

                        context.WriteJsonP(JsonConvert.SerializeObject(paths, Formatting.Indented, Common.JsonSerializerSettings));
                    }
                }
                else
                    context.WriteJsonP(JsonConvert.SerializeObject(new string[] { url }, Formatting.Indented, Common.JsonSerializerSettings));
            }
        }

        public void GetPages(HttpContext context)
        {
            int count;
            string s = context.Request.QueryString["s"];
            if (string.IsNullOrEmpty(s))
            {
                string q;
                count = (from o in this.GetDataContext2.Vw_Images4Tagging
                         where o.UserID == Common.UserID
                         select new
                         {
                             o.ID,
                             o.Image_Height,
                             o.Image_Width,
                             o.RelativeImage_Path
                         }).Distinct().Count();
                q = string.Format("Select TOP 1 SlNO  from (Select distinct Row_Number() over (order by Tagged desc,ID) as SlNO,Tagged from vw_Images4Tagging where UserID={0}) as tmp where  (tmp.Tagged IS NULL or tmp.Tagged=0)", Common.UserID);
                long? index = (long?)SqlHelper.ExecuteScalar(Common.DataConnectionString, CommandType.Text, q);
                int ps = int.Parse(context.Request.QueryString["ps"]);
                var categories = (from o in this.GetDataContext2.Category
                                  where o.ParentID == null
                                  select new Category
                                             {
                                                 ID = o.ID,
                                                 Name = o.Name,
                                                 ParentID = o.ParentID
                                             }).AsEnumerable().Select((o, i) => new
                {
                    index = i,
                    ID = o.ID,
                    Name = o.Name,
                    SubCategories = SubCategories(o, all)
                });
                context.WriteJsonP(JsonConvert.SerializeObject(new
                {
                    categories = categories,
                    pages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(count) / Convert.ToDouble(ps))),
                    currentpage = index.HasValue && index > ps ? (Math.Floor((Convert.ToDouble(index) / Convert.ToDouble(ps)))) + 1 : 1
                }, Formatting.Indented, Common.JsonSerializerSettings));
            }
            else
            {
                s = Common.MakeRelativeUri(Common.ContentUrl, Uri.UnescapeDataString(s)).TrimStart('/');
                string q;
                count = (from o in this.GetDataContext2.Vw_Images4Tagging
                         where o.UserID == Common.UserID && o.RelativeImage_Path == s
                         select new
                         {
                             o.ID,
                             o.Image_Height,
                             o.Image_Width,
                             o.RelativeImage_Path
                         }).Distinct().Count();
                q = string.Format("Select TOP 1 SlNO  from (Select distinct Row_Number() over (order by Tagged desc,ID) as SlNO,Tagged from vw_Images4Tagging where UserID={0} and RelativeImage_Path = '{1}') as tmp where  (tmp.Tagged IS NULL or tmp.Tagged=0)", Common.UserID, s);
                long? index = (long?)SqlHelper.ExecuteScalar(Common.DataConnectionString, CommandType.Text, q);
                int ps = int.Parse(context.Request.QueryString["ps"]);
                var categories = (from o in this.GetDataContext2.Category
                                  where o.ParentID == null
                                  select new Category
                                             {
                                                 ID = o.ID,
                                                 Name = o.Name,
                                                 ParentID = o.ParentID
                                             }).AsEnumerable().Select((o, i) => new Category
                                             {
                                                 index = i,
                                                 ID = o.ID,
                                                 Name = o.Name,
                                                 SubCategories = SubCategories(o, all)
                                             });
                context.WriteJsonP(JsonConvert.SerializeObject(new
                {
                    categories = categories,
                    pages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(count) / Convert.ToDouble(ps))),
                    currentpage = index.HasValue && index > ps ? (Math.Floor((Convert.ToDouble(index) / Convert.ToDouble(ps)))) + 1 : 1
                }, Formatting.Indented, Common.JsonSerializerSettings));
            }
        }

        public void GetImagesAndCategoriesPaged(HttpContext context)
        {
            string cat = context.Request.QueryString["cat"];
            int start = int.Parse(context.Request.QueryString["start"]);
            int page = int.Parse(context.Request.QueryString["page"]);
            object files = null;
            string s = context.Request.QueryString["s"];
            if (!string.IsNullOrEmpty(s))
            {
                s = Common.MakeRelativeUri(Common.ContentUrl, Uri.UnescapeDataString(s)).TrimStart('/');
                files = (from o in this.GetDataContext2.Vw_Images4Tagging
                         where o.UserID == Common.UserID && o.RelativeImage_Path == s
                         orderby o.Rating descending
                         select new
                         {
                             o.ID,
                             o.Tagged,
                             o.Image_Height,
                             o.Image_Title,
                             o.Image_Width,
                             o.RelativeImage_Path
                         }).Distinct().Skip(start).Take(page).AsEnumerable().Select((o, i) => new
                         {
                             index = i,
                             ID = o.ID,
                             title = o.Image_Title,
                             height = Math.Ceiling(190.00 * (Convert.ToDouble(o.Image_Height) / Convert.ToDouble(o.Image_Width))),
                             file = o.RelativeImage_Path,
                             contenturl = Common.ContentUrl,
                             cat = (from o1 in this.GetDataContext2.Vw_ImgCategory where o1.ID == o.ID select o1).AsEnumerable().Select((o2, i1) => new
                             {
                                 index = i1,
                                 o2.CategoryID,
                                 o2.Name
                             })
                         });
                context.WriteJsonP(Newtonsoft.Json.JsonConvert.SerializeObject(files, Formatting.Indented, Common.JsonSerializerSettings));
                return;
            }
            if (!string.IsNullOrEmpty(cat))
            {
                cat = Uri.UnescapeDataString(cat);

                files = (from o in this.GetDataContext2.Vw_Images4Tagging
                         where o.UserID == Common.UserID && o.CategoryName == cat
                         orderby o.Rating descending
                         select new
                         {
                             o.ID,
                             o.Tagged,
                             o.Image_Title,
                             o.Image_Height,
                             o.Image_Width,
                             o.RelativeImage_Path
                         }).Skip(start).Take(page).AsEnumerable().Select((o, i) => new
                         {
                             index = i,
                             ID = o.ID,
                             title = o.Image_Title,
                             height = Math.Ceiling(190.00 * (Convert.ToDouble(o.Image_Height) / Convert.ToDouble(o.Image_Width))),
                             file = o.RelativeImage_Path,
                             contenturl = Common.ContentUrl,
                             cat = (from o1 in this.GetDataContext2.Vw_ImgCategory where o1.ID == o.ID select o1).AsEnumerable().Select((o2, i1) => new
                             {
                                 index = i1,
                                 o2.CategoryID,
                                 o2.Name
                             })
                         });
                context.WriteJsonP(Newtonsoft.Json.JsonConvert.SerializeObject(files, Formatting.Indented, Common.JsonSerializerSettings));
                return;
            }
            files = (from o in this.GetDataContext2.Vw_Images4Tagging
                     where o.UserID == Common.UserID
                     orderby o.Rating descending
                     select new
                     {
                         o.ID,
                         o.Tagged,
                         o.Image_Title,
                         o.Image_Height,
                         o.Image_Width,
                         o.RelativeImage_Path
                     }).Skip(start).Take(page).AsEnumerable().Select((o, i) => new
                     {
                         index = i,
                         ID = o.ID,
                         title = o.Image_Title,
                         height = Math.Ceiling(190.00 * (Convert.ToDouble(o.Image_Height) / Convert.ToDouble(o.Image_Width))),
                         file = o.RelativeImage_Path,
                         contenturl = Common.ContentUrl,
                         cat = (from o1 in this.GetDataContext2.Vw_ImgCategory where o1.ID == o.ID select o1).AsEnumerable().Select((o2, i1) => new
                         {
                             index = i1,
                             o2.CategoryID,
                             o2.Name
                         })
                     });
            context.WriteJsonP(Newtonsoft.Json.JsonConvert.SerializeObject(files, Formatting.Indented, Common.JsonSerializerSettings));
        }

        public IEnumerable<Category> SubCategories(Category category, List<Category> all)
        {
            if (category != null)
            {
                int subcategoriescnt = (from o in all where o.ParentID == category.ID select o).Count();
                if (subcategoriescnt > 0)
                {
                    return (from o in all
                            where o.ParentID == category.ID
                            select new Category
                                {
                                    ID = o.ID,
                                    Name = o.Name
                                }).AsEnumerable<Category>().Select((o, i) => new Category
                                {
                                    index = i,
                                    ID = o.ID,
                                    Name = o.Name,
                                    SubCategories = SubCategories(o, all)
                                });
                }
            }
            return null;
        }

        public void GetCategories(HttpContext context)
        {
            var categories = (from o in all
                              where o.ParentID == null
                              select new Category
                              {
                                  ID = o.ID,
                                  Name = o.Name,
                                  ParentID = o.ParentID
                              }).AsEnumerable().Select((o, i) => new Category
                              {
                                  index = i,
                                  ID = o.ID,
                                  Name = o.Name,
                                  SubCategories = SubCategories(o, all)
                              });
            context.WriteJsonP(JsonConvert.SerializeObject(categories, Formatting.Indented, Common.JsonSerializerSettings));
        }

        public void GetImages(HttpContext context)
        {

            if (Common.VUserID.HasValue || Common.UserID.HasValue) User(context);
            else Default(context);
        }

        private void User(HttpContext context)
        {
            int? userId = Common.UserID;
            int? vuserid = Common.VUserID;
            List<object> files = new List<object>();
            string cat = context.Request.QueryString["cat"];
            string q = context.Request.QueryString["q"];
            string filter = context.Request.QueryString["filter"];
            string style = context.Request.QueryString["style"];
            string board = context.Request.QueryString["board"];
            int p = int.Parse(context.Request.QueryString["p"]);
            int ps = Common.PageSize;
            if (board != null)
            {
                var imgs = Common.Compiled.BoardImages(this.GetDataContext2, p, ps, userId, board, vuserid.HasValue).Select((o, i) => new
                {
                    title = o.Image_Title,
                    editable = (!vuserid.HasValue) && (o.UserID == userId),
                    o.ID,
                    o.BoardID,
                    height = o.Image_Height,
                    PinID = o.PinID.ToString(),
                    o.liked,
                    width = o.Image_Width,
                    imgsource = o.Source,
                    cats = Common.XmlToJArray(o.Cats),
                    comments = Common.XmlToJArray(o.Comments),
                    o.BIMID,
                    source = Common.GetAbsoluteUri(o.Source),
                    url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()

                });
                context.WriteJsonP(JsonConvert.SerializeObject(imgs, Formatting.Indented, Common.JsonSerializerSettings));
                return;
            }
            if (filter != null)
            {
                if (filter == "likes")
                {
                    var imgs = Common.Compiled.Likes(this.GetDataContext2, p, ps, userId, vuserid).Select((o, i) => new
                    {
                        title = o.Image_Title,
                        editable = (!vuserid.HasValue) && (o.UserID == userId),
                        o.ID,
                        o.BoardID,
                        height = o.Image_Height,
                        PinID = o.PinID.ToString(),
                        o.liked,
                        width = o.Image_Width,
                        imgsource = o.Source,
                        cats = Common.XmlToJArray(o.Cats),
                        comments = Common.XmlToJArray(o.Comments),
                        o.BIMID,
                        source = Common.GetAbsoluteUri(o.Source),
                        url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()
                    });
                    context.WriteJsonP(JsonConvert.SerializeObject(imgs, Formatting.Indented, Common.JsonSerializerSettings));
                    return;
                }
                if (filter == "pins")
                {
                    var imgs = Common.Compiled.Pins(this.GetDataContext2, p, ps, userId, vuserid).Select((o, i) => new
                    {
                        title = o.Image_Title,
                        editable = (!vuserid.HasValue) && (o.UserID == userId),
                        o.ID,
                        o.BoardID,
                        width = o.Image_Width,
                        height = o.Image_Height,
                        PinID = o.PinID.ToString(),
                        o.liked,
                        imgsource = o.Source,
                        cats = Common.XmlToJArray(o.Cats),
                        comments = Common.XmlToJArray(o.Comments),
                        o.BIMID,
                        source = Common.GetAbsoluteUri(o.Source),
                        url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()
                    });
                    context.WriteJsonP(JsonConvert.SerializeObject(imgs, Formatting.Indented, Common.JsonSerializerSettings));
                    return;
                }
            }
            if (cat != null)
            {
                List<int> cats = new List<int>();
                Action<IEnumerable<Category>> addSubCats = null;
                addSubCats = (subcats) =>
                {
                    foreach (var category in subcats)
                    {
                        cats.Add(category.ID);
                        if (all.Any(o => o.ParentID == category.ID))
                            addSubCats(all.Where(o => o.ParentID == category.ID));
                    }
                };
                addSubCats(all.Where(o => o.Name == cat));
                if (cats.Any())
                {
                    var catimgs = Common.Compiled.CategoryImagesUser(GetDataContext2, p, ps, userId, string.Join(",", cats.ToArray()), style).Select((o, i) => new
                    {
                        title = o.Image_Title,
                        editable = (!vuserid.HasValue) && (o.UserID == userId),
                        o.ID,
                        o.BoardID,
                        height = o.Image_Height,
                        PinID = o.PinID.ToString(),
                        o.liked,
                        width = o.Image_Width,
                        imgsource = o.Source,
                        cats = Common.XmlToJArray(o.Cats),
                        comments = Common.XmlToJArray(o.Comments),
                        o.BIMID,
                        source = Common.GetAbsoluteUri(o.Source),
                        url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()
                    });
                    context.WriteJsonP(JsonConvert.SerializeObject(catimgs, Formatting.Indented, Common.JsonSerializerSettings));

                }
                else context.WriteJsonP("[]");
                return;
            }
            if (q != null)
            {
                var _q = Common.Compiled.QueryUser(this.GetDataContext2, p, ps, userId, q).Select((o, i) => new
                {
                    o.ID,
                    editable = (!vuserid.HasValue) && (o.UserID == userId),
                    o.BoardID,
                    o.liked,
                    title = o.Image_Title,
                    source = Common.GetAbsoluteUri(o.Source),
                    imgsource = o.Source,
                    width = o.Image_Width,
                    cats = Common.XmlToJArray(o.Cats),
                    comments = Common.XmlToJArray(o.Comments),
                    o.BIMID,
                    height = o.Image_Height,
                    PinID = o.PinID.ToString(),
                    url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()
                });
                context.WriteJsonP(JsonConvert.SerializeObject(_q, Formatting.Indented, Common.JsonSerializerSettings));
                return;
            }
            if (style != null)
            {
                context.WriteJsonP(JsonConvert.SerializeObject(Common.Compiled.ImagesStyle(this.GetDataContext2, style, p, ps).Select((o, i) => new
                {
                    o.ID,
                    editable = false,
                    title = o.Image_Title,
                    o.liked,
                    o.BoardID,
                    imgsource = o.Source,
                    cats = Common.XmlToJArray(o.Cats),
                    comments = Common.XmlToJArray(o.Comments),
                    o.BIMID,
                    width = o.Image_Width,
                    source = Common.GetAbsoluteUri(o.Source),
                    height = o.Image_Height,
                    PinID = o.PinID.ToString(),
                    url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()
                }).Randomize(), Formatting.Indented, Common.JsonSerializerSettings));
                return;
            }
            context.WriteJsonP(JsonConvert.SerializeObject(Common.Compiled.Images(this.GetDataContext2, p, ps).Randomize().Select((o, i) => new
            {
                o.ID,
                editable = false,
                title = o.Image_Title,
                o.liked,
                o.BoardID,
                imgsource = o.Source,
                cats = Common.XmlToJArray(o.Cats),
                comments = Common.XmlToJArray(o.Comments),
                o.BIMID,
                width = o.Image_Width,
                source = Common.GetAbsoluteUri(o.Source),
                height = o.Image_Height,
                PinID = o.PinID.ToString(),
                url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()
                //url = Common.GetBase64Image(Uri.UnescapeDataString(Path.Combine(Common.ImagePath,  o.RelativeImage_Path.Replace("/", "\\"))), (short)(190.00 * o.Image_Height / o.Image_Width))
            }), Formatting.Indented, Common.JsonSerializerSettings));
        }

        private void Default(HttpContext context)
        {
            List<object> files = new List<object>();
            string cat = context.Request.QueryString["cat"];
            string q = context.Request.QueryString["q"];
            int p = int.Parse(context.Request.QueryString["p"]);
            string style = context.Request.QueryString["style"];
            int ps = Common.PageSize;
            if (cat != null)
            {
                List<int> cats = new List<int>();
                Action<IEnumerable<Category>> addSubCats = null;
                addSubCats = (subcats) =>
                {
                    foreach (var category in subcats)
                    {
                        cats.Add(category.ID);
                        if (all.Any(o => o.ParentID == category.ID))
                            addSubCats(all.Where(o => o.ParentID == category.ID));
                    }
                };
                addSubCats(all.Where(o => o.Name == cat));
                if (cats.Any())
                {
                    var catimgs = Common.Compiled.CategoryImages(GetDataContext2, p, ps, string.Join(",", cats.ToArray()), style).Select((o, i) => new
                    {
                        o.ID,
                        editable = false,
                        o.BoardID,
                        title = o.Image_Title,
                        height = o.Image_Height,
                        PinID = o.PinID.ToString(),
                        imgsource = o.Source,
                        width = o.Image_Width,
                        cats = Common.XmlToJArray(o.Cats),
                        comments = Common.XmlToJArray(o.Comments),
                        bimid = o.BIMID,
                        source = Common.GetAbsoluteUri(o.Source),
                        url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()
                    });
                    context.WriteJsonP(JsonConvert.SerializeObject(catimgs, Formatting.Indented, Common.JsonSerializerSettings));
                }
                else context.WriteJsonP("[]");
                return;
            }
            if (q != null)
            {
                var _q = Common.Compiled.Query(this.GetDataContext2, p, ps, q).Select((o, i) => new
                {
                    o.ID,
                    editable = false,
                    o.BoardID,
                    title = o.Image_Title,
                    source = Common.GetAbsoluteUri(o.Source),
                    imgsource = o.Source,
                    width = o.Image_Width,
                    cats = Common.XmlToJArray(o.Cats),
                    comments = Common.XmlToJArray(o.Comments),
                    o.BIMID,
                    height = o.Image_Height,
                    PinID = o.PinID.ToString(),
                    url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()
                });
                context.WriteJsonP(JsonConvert.SerializeObject(_q, Formatting.Indented, Common.JsonSerializerSettings));
                return;
            }
            if (style != null)
            {
                context.WriteJsonP(JsonConvert.SerializeObject(Common.Compiled.ImagesStyle(this.GetDataContext2, style, p, ps).Select((o, i) => new
                {
                    o.ID,
                    editable = false,
                    title = o.Image_Title,
                    o.liked,
                    o.BoardID,
                    imgsource = o.Source,
                    cats = Common.XmlToJArray(o.Cats),
                    comments = Common.XmlToJArray(o.Comments),
                    o.BIMID,
                    width = o.Image_Width,
                    source = Common.GetAbsoluteUri(o.Source),
                    height = o.Image_Height,
                    PinID = o.PinID.ToString(),
                    url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()
                }).Randomize(), Formatting.Indented, Common.JsonSerializerSettings));
                return;
            }
            context.WriteJsonP(JsonConvert.SerializeObject(Common.Compiled.Images(this.GetDataContext2, p, ps).Randomize().Select((o, i) => new
            {
                o.ID,
                editable = false,
                title = o.Image_Title,
                o.liked,
                o.BoardID,
                imgsource = o.Source,
                cats = Common.XmlToJArray(o.Cats),
                comments = Common.XmlToJArray(o.Comments),
                o.BIMID,
                width = o.Image_Width,
                source = Common.GetAbsoluteUri(o.Source),
                height = o.Image_Height,
                PinID = o.PinID.ToString(),
                url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()
                //url = Common.GetBase64Image(Uri.UnescapeDataString(Path.Combine(Common.ImagePath,  o.RelativeImage_Path.Replace("/", "\\"))), (short)(190.00 * o.Image_Height / o.Image_Width))
            }), Formatting.Indented, Common.JsonSerializerSettings));
        }

        private void GetFollowing(HttpContext context)
        {
            Func<JObject, JObject> proc = o1 =>
            {
                string path = Common.ContentUrl + (string)o1["RelativeImage_Path"];
                o1["RelativeImage_Path"] = path;
                return o1;
            };
            var FgUsers = (from f in GetDataContext2.Vw_FollowingUser
                           where f.UserID == (Common.VUserID ?? Common.UserID)
                           select new
                           {
                               F_FullName = f.FullName,
                               F_ID = f.FollowingID,
                               F_UserName = f.UserName,
                               F_Avatar = Common.FixAvatar(f.Avatar),
                               Fg_Count = f.Following_Count,
                               Fr_Count = f.Follower_Count,
                               F_Pin = Common.XmlToJArray(f.Pins_XML, proc),
                               F_Status = GetDataContext2.FollowStatus(Common.UserID, f.FollowingID),
                           });
            context.WriteJsonP(JsonConvert.SerializeObject(FgUsers, Formatting.Indented, Common.JsonSerializerSettings));

        }

        private void GetFollower(HttpContext context)
        {
            Func<JObject, JObject> proc = o1 =>
            {
                string path = Common.ContentUrl + (string)o1["RelativeImage_Path"];
                o1["RelativeImage_Path"] = path;
                return o1;
            };
            var FrUsers = (from f in GetDataContext2.Vw_FollowerUser
                           where f.UserID == (Common.VUserID ?? Common.UserID)
                           select new
                           {
                               F_FullName = f.FullName,
                               F_ID = f.FollowerID,
                               F_UserName = f.UserName,
                               F_Avatar = Common.FixAvatar(f.Avatar),
                               Fg_Count = f.Following_Count,
                               Fr_Count = f.Follower_Count,
                               F_Pin = Common.XmlToJArray(f.Pins_XML, proc),
                               F_Status = GetDataContext2.FollowStatus(Common.UserID, f.FollowerID),
                           });
            context.WriteJsonP(JsonConvert.SerializeObject(FrUsers, Formatting.Indented, Common.JsonSerializerSettings));

        }

        private void GetFollowStatus(HttpContext context)
        {
            var Flst = (bool)GetDataContext2.FollowStatus(Common.UserID, Common.VUserID);
            context.WriteJsonP(JsonConvert.SerializeObject(Flst, Formatting.Indented, Common.JsonSerializerSettings));
        }

        private string FixAvatar(string Avatar)
        {
            return string.IsNullOrWhiteSpace(Avatar) ? "" : Common.UploadedImageRelPath + Avatar;
        }

        //private void CachedMashupsUsers(HttpContext context, int ps, int p)
        //{
        //    Queue<object> group1 = new Queue<object>();
        //    Queue<object> group2 = new Queue<object>();
        //    Queue<object> group3 = new Queue<object>();
        //    Common.Compiled.ImagesinCategoriesUsers(this.GetDataContext2, p, ps / 2, Common.UserID, @"2,3,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,22,23,24,25,27,28,29,30,31,32,33,34,44,45,46,47").Select((o, i) => new
        //    {
        //        o.ID,
        //        editable = false,
        //        title = o.Image_Title,
        //        o.liked,
        //        o.BoardID,
        //        imgsource = o.Source,
        //        cats = Common.XmlToJArray(o.Cats),
        //        comments = Common.XmlToJArray(o.Comments),
        //        o.BIMID,
        //        width = o.Image_Width,
        //        source = Common.GetAbsoluteUri(o.Source),
        //        height = o.Image_Height,
        //        PinID = o.PinID.ToString(),
        //        url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()
        //    }).Randomize().ForEach(o => group1.Enqueue(o));
        //    Common.Compiled.ImagesinCategoriesUsers(this.GetDataContext2, p, ps / 2, Common.UserID, "36,37,38,40").Select((o, i) => new
        //    {
        //        o.ID,
        //        editable = false,
        //        o.liked,
        //        width = o.Image_Width,
        //        title = o.Image_Title,
        //        o.BoardID,
        //        imgsource = o.Source,
        //        cats = Common.XmlToJArray(o.Cats),
        //        comments = Common.XmlToJArray(o.Comments),
        //        o.BIMID,
        //        source = Common.GetAbsoluteUri(o.Source),
        //        height = o.Image_Height,
        //        PinID = o.PinID.ToString(),
        //        url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()
        //    }).Randomize().ForEach(o => group2.Enqueue(o));
        //    Common.Compiled.ImagesinCategoriesUsers(this.GetDataContext2, p, ps / 2, Common.UserID, "42").Select((o, i) => new
        //    {
        //        o.ID,
        //        editable = false,
        //        width = o.Image_Width,
        //        o.liked,
        //        title = o.Image_Title,
        //        o.BoardID,
        //        imgsource = o.Source,
        //        gamify = true,
        //        gamifytype = "ht",
        //        cats = Common.XmlToJArray(o.Cats),
        //        comments = Common.XmlToJArray(o.Comments),
        //        o.BIMID,
        //        source = Common.GetAbsoluteUri(o.Source),
        //        height = o.Image_Height,
        //        PinID = o.PinID.ToString(),
        //        url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()
        //    }).Randomize().ForEach(o => group3.Enqueue(o));

        //    List<object> images = new List<object>();
        //    while (images.Count < ps && (group1.Count + group2.Count + group3.Count) > 0)
        //    {
        //        if (group1.Count > 0) images.Add(group1.Dequeue());
        //        if (group2.Count > 0) images.Add(group2.Dequeue());
        //        if (group1.Count > 0) images.Add(group1.Dequeue());
        //        if (group3.Count > 0) images.Add(group3.Dequeue());
        //    }
        //    context.WriteJsonP(JsonConvert.SerializeObject(images, Formatting.Indented, Common.JsonSerializerSettings));
        //}

        //private void CachedMashups(HttpContext context, int ps, int p)
        //{
        //    Queue<object> group1 = new Queue<object>();
        //    Queue<object> group2 = new Queue<object>();
        //    Queue<object> group3 = new Queue<object>();
        //    Common.Compiled.ImagesinCategories(this.GetDataContext2, p, ps / 2, @"2,3,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,22,23,24,25,27,28,29,30,31,32,33,34,44,45,46,47").Select((o, i) => new
        //    {
        //        o.ID,
        //        editable = false,
        //        title = o.Image_Title,
        //        o.liked,
        //        o.BoardID,
        //        imgsource = o.Source,
        //        cats = Common.XmlToJArray(o.Cats),
        //        comments = Common.XmlToJArray(o.Comments),
        //        o.BIMID,
        //        width = o.Image_Width,
        //        source = Common.GetAbsoluteUri(o.Source),
        //        height = o.Image_Height,
        //        PinID = o.PinID.ToString(),
        //        url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()
        //    }).Randomize().ForEach(o => group1.Enqueue(o));
        //    Common.Compiled.ImagesinCategories(this.GetDataContext2, p, ps / 2, "36,37,38,40").Select((o, i) => new
        //    {
        //        o.ID,
        //        editable = false,
        //        width = o.Image_Width,
        //        title = o.Image_Title,
        //        o.BoardID,
        //        imgsource = o.Source,
        //        cats = Common.XmlToJArray(o.Cats),
        //        comments = Common.XmlToJArray(o.Comments),
        //        o.BIMID,
        //        source = Common.GetAbsoluteUri(o.Source),
        //        height = o.Image_Height,
        //        PinID = o.PinID.ToString(),
        //        url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()
        //    }).Randomize().ForEach(o => group2.Enqueue(o));
        //    Common.Compiled.ImagesinCategories(this.GetDataContext2, p, ps / 2, "42").Select((o, i) => new
        //    {
        //        o.ID,
        //        editable = false,
        //        width = o.Image_Width,
        //        title = o.Image_Title,
        //        o.BoardID,
        //        gamify = true,
        //        gamifytype = "ht",
        //        imgsource = o.Source,
        //        cats = Common.XmlToJArray(o.Cats),
        //        comments = Common.XmlToJArray(o.Comments),
        //        o.BIMID,
        //        source = Common.GetAbsoluteUri(o.Source),
        //        height = o.Image_Height,
        //        PinID = o.PinID.ToString(),
        //        url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + o.RelativeImage_Path.Trim()
        //    }).Randomize().ForEach(o => group3.Enqueue(o));
        //    List<object> images = new List<object>();
        //    while (images.Count < ps && (group1.Count + group2.Count + group3.Count) > 0)
        //    {
        //        if (group1.Count > 0) images.Add(group1.Dequeue());
        //        if (group2.Count > 0) images.Add(group2.Dequeue());
        //        if (group1.Count > 0) images.Add(group1.Dequeue());
        //        if (group3.Count > 0) images.Add(group3.Dequeue());
        //    }
        //    context.WriteJsonP(JsonConvert.SerializeObject(images, Formatting.Indented, Common.JsonSerializerSettings));

        //}
    }
}
