using System;
using System.Dynamic;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using Newtonsoft.Json;
using System.Transactions;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Net.Mail;
using HashLib;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.ApplicationBlocks.Data;
using Dapper;
using DapperExtensions;

namespace Nails
{
    /// <summary>
    /// Summary description for GET
    /// </summary>

    public class POST : HttpHandlerBase
    {
        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            switch (context.Request.QueryString["t"])
            {
                case "cat":
                    Cat(context);
                    break;
                case "board":
                    Board(context);
                    break;
                case "createboard":
                    CreateBoard(context);
                    break;
                case "login":
                    Login(context);
                    break;
                case "fblogin":
                    fbLogin(context);
                    break;
                case "ri":
                    RequestInvite(context);
                    break;
                case "applogin":
                    AppLogin(context);
                    break;
                case "up":
                    Upload(context);
                    break;
                case "saveuploadedpin":
                    SaveUploadedPin(context);
                    break;
                case "saveaddedpin":
                    SaveAddedPin(context);
                    break;
                case "validatecontributor":
                    ValidateContributor(context);
                    break;
                case "savecreatedboard":
                    SaveCreatedBoard(context);
                    break;
                case "saverepin":
                    SaveRepin(context);
                    break;
                case "savelike":
                    SaveLike(context);
                    break;
                case "saveprofile":
                    SaveProfile(context);
                    break;
                case "changepassword":
                    ChangePassword(context);
                    break;
                case "addcomment":
                    AddComment(context);
                    break;
                case "resetpass":
                    ResetPass(context);
                    break;
                case "deletepin":
                    DeletePin(context);
                    break;
                case "saveeditpin":
                    SaveEditPin(context);
                    break;
                case "usernameavail":
                    UserNameAvail(context);
                    break;
                case "delcontributor":
                    DelContributor(context);
                    break;
                case "saveeditboard":
                    SaveEditBoard(context);
                    break;
                case "delboard":
                    DelBoard(context);
                    break;
                case "review":
                    Review(context);
                    break;
                case "updateprize":
                    UpdatePrize(context);
                    break;
                case "logout":
                    Logout(context);
                    break;
                case "updateabout":
                    UpdateAbout(context);
                    break;
                case "unfollowuser":
                    UnFollowUser(context);
                    break;
                case "followuser":
                    FollowUser(context);
                    break;
            }
            base.EndRequest(context);
        }

        private void UpdateAbout(HttpContext context)
        {
            string about = context.Request.Params["about"];
            GetDataContext1.Update(new Data.POCOS.AppUser() { About = about, ID = Common.UserID.Value }, new string[] { "About" });
        }

        private void Logout(HttpContext context)
        {
            CookieUtil.DeleteAllCookies();
            Common.CookieValue = null;
        }

        private void Review(HttpContext context)
        {
            string answer = context.Request.Params["ans"];
            string question = context.Request.Params["ques"];
            int bimid = int.Parse(context.Request.Params["bimid"]);
            Data.dbml.Review review = GetDataContext2.Review.FirstOrDefault(o => o.BIMID == bimid && (o.UserID == Common.UserID || o.SessionID == Common.SessionID));
            if (review == null)
            {
                review = new Data.dbml.Review();
                review.Answer = answer;
                review.BIMID = bimid;
                review.Question = question;
                review.UserID = Common.UserID;
                review.SessionID = Common.SessionID;
                GetDataContext2.Review.InsertOnSubmit(review);
                if (Common.UserID.HasValue)
                {
                    GetDataContext1.Update(new Data.POCOS.AppUser() { ID = Common.UserID.Value, Points = Common.Points }, new[] { "Points" });
                }
            }
            else
            {
                review.Answer = answer;
                review.Question = question;
            }
            GetDataContext2.SubmitChanges();
            var q = from o in GetDataContext2.Review where (o.UserID == Common.UserID || o.SessionID == Common.SessionID) select o.BIMID;
            context.WriteJsonP(JsonConvert.SerializeObject(q));
        }

        private void DelBoard(HttpContext context)
        {
            int boardid = int.Parse(context.Request.Params["boardid"]);
            GetDataContext2.DeleteBoard(boardid);
            UpdateUPCount();
        }

        private void SaveEditBoard(HttpContext context)
        {
            string name = context.Request.Params["name"].Trim();
            int boardid = int.Parse(context.Request.Params["boardid"]);
            Data.dbml.Boards b = GetDataContext2.Boards.FirstOrDefault(o => o.Name == name && o.UserID == Common.UserID && o.ID != boardid);
            if (b != null)
                context.Response.WriteError(strings.ErrSameBoardName);
            else
            {
                int cat = int.Parse(context.Request.Params["catid"]);
                JArray removebc = JArray.Parse(context.Request.Params["removebc"]);
                JArray bc = JArray.Parse(context.Request.Params["bc"]);
                using (TransactionScope ts = new TransactionScope())
                {
                    try
                    {
                        foreach (JObject obj in removebc)
                        {
                            GetDataContext3.DeleteBoardContributor((string)obj["Email"], boardid).Execute();
                        }
                        foreach (JObject obj in bc)
                        {
                            GetDataContext3.AddBoardContributor((string)obj["Email"], boardid).Execute();
                        }
                        SubSonic.POCOS.Board board = GetDataContext3.Boards.Single(o => o.ID == boardid);
                        board.CatID = cat;
                        board.Name = name;
                        board.Save();
                        ts.Complete();
                    }
                    catch (Exception ex)
                    {
                        context.Response.WriteError(ex.Message);
                    }
                    finally
                    {
                        ts.Dispose();
                    }
                }
            }
        }

        private void DelContributor(HttpContext context)
        {
            string un = context.Request.Params["un"];
            string id = context.Request.Params["boardid"];
            GetDataContext1.Execute(string.Format(@"DELETE FROM BoardContributor
FROM     BoardContributor INNER JOIN
                  AppUsers ON BoardContributor.ContributorID = AppUsers.ID
WHERE  (BoardContributor.BoardID = {0}) AND (AppUsers.Name = '{1}')", id, un));
        }

        private void UserNameAvail(HttpContext context)
        {
            string _un = context.Request.Params["un"];
            string currentname = (from o in GetDataContext2.AppUsers where o.ID == Common.UserID select o.Name).Single();
            if (currentname == _un)
                context.Response.Write(Common.Domain + _un);
            else
            {
                int un = GetDataContext1.Single<int>("Select count(*) from AppUsers where Name=@0", _un);
                if (un == 0)
                    context.Response.Write(string.Format(@"{0} is available", _un));
                else context.Response.WriteError(string.Format(@"{0} not available", _un));

            }
        }

        private void SaveEditPin(HttpContext context)
        {
            int BIMID = int.Parse(context.Request.Params["BIMID"]);
            var pin = GetDataContext2.BoardsImagesMapping.Where(bim => bim.ID == BIMID && bim.UserID == Common.UserID).FirstOrDefault();
            if (pin != null)
            {
                pin.Image_Title = context.Request.Params["desc"];
                pin.Source = context.Request.Params["source"];
                pin.BoardID = int.Parse(context.Request.Params["board"]);
            }
            GetDataContext2.SubmitChanges();
        }

        private void DeletePin(HttpContext context)
        {
            int BIMID = int.Parse(context.Request.Params["BIMID"]);
            GetDataContext2.DeletePin(BIMID, Common.UserID.Value);
            UpdateUPCount();
        }
        private void ResetPass(HttpContext context)
        {
            string email = context.Request.Params["email"];
            Data.dbml.AppUsers user = this.GetDataContext2.AppUsers.FirstOrDefault(o => o.Email == email);
            if (user != null)
            {
                SmtpClient client = new SmtpClient();
                MailMessage mess = new MailMessage("support@pinjimu.com", email);
                string pass = Common.RandomString(Common.PassMinChars);
                mess.Body = string.Format(File.ReadAllText(context.Server.MapPath("resetpasstmpl.html")), user.Name, user.Email, pass);
                mess.Subject = "Password reset for your Pinjimu account";
                mess.IsBodyHtml = true;
                client.Send(mess);
                user.Password = Common.GetHash(pass);
                GetDataContext2.SubmitChanges();
                context.Response.Write(strings.Pass_Reset);
            }
            else
                context.Response.Write(strings.Email_Not_Reg);
        }
        private void fbLogin(HttpContext context)
        {
            string token = context.Request.Params["token"];
            Facebook.FacebookClient client = new Facebook.FacebookClient(token);
            client.IsSecureConnection = true;
            var me = client.Get("/me");
            Facebook.JsonObject o = (Facebook.JsonObject)me;
            var db = GetDataContext1;
            using (var scope = db.GetTransaction())
            {

                try
                {
                    string first_name = (string)o["first_name"];
                    string name = (string)o["name"];
                    decimal id = Convert.ToDecimal(o["id"]);
                    string email = (string)o["email"];

                    Data.POCOS.Facebook fb = new Data.POCOS.Facebook();
                    fb.name = name;
                    fb.first_name = first_name;
                    fb.gender = (string)o["gender"];
                    fb.id = id;
                    fb.last_name = (string)o["last_name"];
                    fb.link = (string)o["link"];
                    fb.locale = (string)o["locale"];
                    fb.email = email;
                    fb.timezone = Convert.ToDouble(o["timezone"]);
                    string updatedtime = (string)o["updated_time"];
                    DateTime dt;
                    if (DateTime.TryParse(updatedtime, out dt))
                        fb.updated_time = dt;
                    if (db.Exists<Data.POCOS.Facebook>(id))
                        db.Update(fb);
                    else
                        db.Insert(fb);
                    Data.POCOS.AppUser au = Data.POCOS.AppUser.FirstOrDefault("Select top 1 * from AppUsers where facebookid=@0", id);
                    if (au == null)
                    {
                        au = new Data.POCOS.AppUser();
                        au.Email = email;
                        au.FirstName = first_name;
                        au.Create_date = DateTime.Now;
                        string _name = Common.RemoveSpecialCharacters(first_name);
                        var names = from o1 in GetDataContext2.AppUsers select o1.Name;
                        string rs = null;
                        while (names.Any(o1 => o1 == _name))
                        {
                            if (!string.IsNullOrEmpty(rs)) _name = _name.TrimEnd(rs.ToCharArray());
                            rs = Common.RandomString(1);
                            _name += rs;
                        }
                        au.Name = _name;
                        au.facebookid = id;
                        try
                        {
                            var query = string.Format(@"SELECT id, width, height, url, is_silhouette, real_width, real_height
FROM profile_pic
WHERE id={0}", id);

                            dynamic parameters = new ExpandoObject();
                            parameters.q = query;
                            dynamic obj = client.Get("/fql", parameters);
                            Facebook.JsonObject det = (Facebook.JsonObject)obj;
                            if (det["data"] != null)
                            {
                                Facebook.JsonArray jarray = ((Facebook.JsonArray)det["data"]);
                                Facebook.JsonObject pic = (Facebook.JsonObject)jarray.First();
                                string url = (string)pic["url"];
                                WebClient webclient = new WebClient();
                                byte[] data = webclient.DownloadData(url);
                                Image img = Image.FromStream(new MemoryStream(data));
                                string fn = Guid.NewGuid().ToString() + ".jpg";
                                string fp = Common.UploadedImagePath + fn;
                                img.Save(fp, ImageFormat.Jpeg);
                                au.Avatar = fn;
                            }
                        }
                        catch
                        {
                        }
                        db.Insert(au);
                    }

                    scope.Complete();
                    Common.WriteValue(Common.AuthCookie, au.ID.ToString());
                    Common.WriteValue(Common.InfoCookie, JObject.FromObject(new { email = au.Email, name = string.IsNullOrEmpty(au.FirstName) ? au.Name : au.FirstName, avatar = string.IsNullOrWhiteSpace(au.Avatar) ? null : Common.UploadedImageRelPath + au.Avatar, points = au.Points }));
                }
                finally
                {
                    scope.Dispose();
                }
            }

        }

        private void AddComment(HttpContext context)
        {
            string comments = context.Request.Params["comments"];
            int id = int.Parse(context.Request.Params["id"]);
            Data.dbml.Comments c = new Data.dbml.Comments();
            c.Comment = comments;
            c.BoardsImagesMappingID = id;
            c.UserID = Common.UserID.Value;
            Data.dbml.AppUsers user = this.GetDataContext2.AppUsers.Single(o => o.ID == Common.UserID);
            this.GetDataContext2.Comments.InsertOnSubmit(c);
            this.GetDataContext2.SubmitChanges();
            context.WriteJsonP(JsonConvert.SerializeObject(new
            {
                Comment = comments,
                Name = user.FirstName,
                Avatar = user.Avatar,
                UN = user.Name
            }));
        }

        private void ChangePassword(HttpContext context)
        {

            string pass = context.Request.Params["pass"];
            if (!string.IsNullOrEmpty(pass))
            {
                Data.dbml.AppUsers u = this.GetDataContext2.AppUsers.First(o => o.ID == Common.UserID);
                IHash hash = Common.Hash;
                HashResult res = hash.ComputeString(pass);
                u.Password = Common.GetString(res.GetBytes());
                GetDataContext2.SubmitChanges();
                context.Response.Write(strings.Pass_Updated);
            }
        }
        private void SaveProfile(HttpContext context)
        {
            Data.dbml.AppUsers u = this.GetDataContext2.AppUsers.First(o => o.ID == Common.UserID);
            if (string.IsNullOrEmpty(u.Password))
            {
                context.Response.WriteError(strings.Pass_Created);
            }
            else
            {
                string first_name = context.Request.Params["first_name"];
                string about = context.Request.Params["about"];
                string location = context.Request.Params["location"];
                string fn = context.Request.Params["fn"];
                string website = context.Request.Params["website"];
                string name = context.Request.Params["name"];
                if (!string.IsNullOrEmpty(fn))
                {
                    Uri uri = new Uri(fn);
                    string filename = uri.Segments.Last();
                    string fp = Path.Combine(Common.Temp, Common.UserID.ToString(), filename);
                    string uploadedpath = Common.UploadedImagePath;
                    FileInfo fInfo = new FileInfo(fp);
                    string nfn = fInfo.Name;
                    if (fInfo.DirectoryName != uploadedpath)
                    {
                        string dest = Path.Combine(uploadedpath, nfn);
                        fInfo.MoveTo(dest);
                    }
                    u.Avatar = nfn;
                }
                u.Location = location;
                u.FirstName = first_name;
                u.Website = website;
                u.Location = location;
                u.About = about;
                u.Name = name;
                GetDataContext2.SubmitChanges();
                Common.WriteValue(Common.InfoCookie, JObject.FromObject(new { email = u.Email, name = string.IsNullOrEmpty(u.FirstName) ? u.Name : u.FirstName, avatar = string.IsNullOrWhiteSpace(u.Avatar) ? null : Common.UploadedImageRelPath + u.Avatar }));
            }
        }

        private void SaveLike(HttpContext context)
        {
            int id = int.Parse(context.Request.Params["id"]);
            bool liked = bool.Parse(context.Request.Params["liked"]);
            if (liked)
            {
                Data.dbml.Likes l = new Data.dbml.Likes();
                l.BoardsImagesMappingID = id;
                l.UserID = Common.UserID.Value;
                GetDataContext2.Likes.InsertOnSubmit(l);
                GetDataContext2.SubmitChanges();
            }
            else
            {
                PredicateGroup group = new PredicateGroup() { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                group.Predicates.Add(Predicates.Field<Data.Standalone.Likes>(f => f.BoardsImagesMappingID, Operator.Eq, id));
                group.Predicates.Add(Predicates.Field<Data.Standalone.Likes>(f => f.UserID, Operator.Eq, Common.UserID));
                SqlConnection.Delete<Data.Standalone.Likes>(group);
            }
            UpdateUPCount();
        }

        private void SaveRepin(HttpContext context)
        {
            int id = int.Parse(context.Request.Params["id"]);
            int board = int.Parse(context.Request.Params["board"]);
            string desc = context.Request.Params["desc"];
            string source = context.Request.Params["source"];
            Data.dbml.BoardsImagesMapping bim = new Data.dbml.BoardsImagesMapping();
            bim.ImageID = id;
            bim.Source = source;
            bim.Image_Title = desc;
            bim.UserID = Common.UserID;
            bim.BoardID = board;
            this.GetDataContext2.BoardsImagesMapping.InsertOnSubmit(bim);
            UpdatePoints(Common.PointsRePin);
            this.GetDataContext2.SubmitChanges();
            UpdateUPCount();
        }

        private void SaveCreatedBoard(HttpContext context)
        {

            string name = context.Request.Params["name"].Trim();
            Data.dbml.Boards b = this.GetDataContext2.Boards.FirstOrDefault(o => o.Name == name && o.UserID == Common.UserID);
            if (b != null)
                context.Response.WriteError(strings.ErrSameBoardName);
            else
            {
                int catid = int.Parse(context.Request.Params["catid"]);
                string contributors = context.Request.Params["contributors[]"];
                Data.dbml.Boards board = new Data.dbml.Boards()
                {
                    Name = name,
                    CatID = catid,
                    UserID = Common.UserID.Value
                };
                this.GetDataContext2.Boards.InsertOnSubmit(board);
                this.GetDataContext2.SubmitChanges();
                if (!string.IsNullOrEmpty(contributors))
                {
                    string[] acontributors = contributors.Split(',');
                    foreach (string contributor in acontributors)
                    {
                        this.GetDataContext2.BoardContributor.InsertOnSubmit(new Data.dbml.BoardContributor()
                        {
                            BoardID = board.ID,
                            ContributorID = GetDataContext2.AppUsers.First(o => o.Email == contributor).ID
                        });
                    }
                    this.GetDataContext2.SubmitChanges();
                }
                UpdatePoints(Common.PointsNewBoard);
                UpdateUPCount();
                context.WriteJsonP(JsonConvert.SerializeObject(new
                {
                    boards = (from o in GetDataContext2.Boards
                              where o.UserID == Common.UserID ||
                              (from o1 in this.GetDataContext2.BoardContributor where o1.ContributorID == Common.UserID select o1.BoardID).Contains(o.ID)
                              select new
                              {
                                  o.ID,
                                  o.Name
                              }).Distinct()
                }));
            }
        }



        private void UpdatePoints(string PointsName)
        {
            this.GetDataContext2.UpdatePoints(Convert.ToInt32(Common.UserID), PointsName);
            this.GetDataContext2.SubmitChanges();
            var pts = (from o in this.GetDataContext2.AppUsers where o.ID == Common.UserID select o.Points).First();
            Common.WriteValue(Common.InfoCookie, JObject.FromObject(new { points = pts.Value }));
        }

        private void UpdatePrize(HttpContext context)
        {
            Random rnd = new Random();
            double angle = rnd.Next(45, 360);
            var prize = (from r in GetDataContext2.Roulette
                         join p in GetDataContext2.Prize
                         on r.PrizeID equals p.ID
                         where (r.End_Angle >= angle && r.Start_Angle <= angle)
                         select new
                         {
                             r.PrizeID,
                             p.Prize_Name,
                             p.User_Alert
                         }).First();
            var user = GetDataContext2.AppUsers.First(o => o.ID == Common.UserID);
            int user_points = Convert.ToInt32(user.Points);
            int rwpointded = Common.RWPointsDed;
            if (user_points >= rwpointded)
            {
                user_points = user_points - rwpointded;
                Common.WriteValue(Common.InfoCookie, JObject.FromObject(new { points = user_points }));
                user.Points = user_points + PrizeValue(prize.Prize_Name);
                GetDataContext2.SubmitChanges();
                GetDataContext2.PrizeHistory.InsertOnSubmit(new Data.dbml.PrizeHistory()
                {
                    PrizeID = prize.PrizeID,
                    UserID = Common.UserID.Value,
                    Create_date = DateTime.Now
                });
                GetDataContext2.SubmitChanges();
                context.WriteJsonP(JsonConvert.SerializeObject(new
                {
                    angle = angle,
                    alert = prize.User_Alert,
                    points = user.Points
                }, Formatting.Indented, Common.JsonSerializerSettings));
            }
            else
            {
                context.Response.WriteError(string.Format(misc.NoPointsAlert, rwpointded));
            }
        }

        private int PrizeValue(string prizename)
        {
            int val = 0;
            switch (prizename)
            {
                case "5 Points":
                    val = 5;
                    break;
                case "50 Points":
                    val = 50;
                    break;
                case "20 Points":
                    val = 20;
                    break;
                default:
                    val = 0;
                    break;
            }
            return val;
        }

        private void ValidateContributor(HttpContext context)
        {
            string contributor = context.Request.Params["contributor"];
            Data.dbml.AppUsers user = this.GetDataContext2.AppUsers.FirstOrDefault(o => o.Email == contributor || o.Name == contributor);
            if (user != null)
            {
                if (user.ID == Common.UserID)
                    context.Response.WriteError(strings.Contributor_Warn);
                else
                {
                    context.WriteJsonP(JsonConvert.SerializeObject(new
                    {
                        user.Email,
                        user.Avatar,
                        Name = string.IsNullOrEmpty(user.FirstName) ? user.Name : user.FirstName,
                        UN = user.Name
                    }));
                }
            }
            else
                context.Response.WriteError(strings.User_Not_Exist);
        }
        private void SaveAddedPin(HttpContext context)
        {
            int board = int.Parse(context.Request.Params["board"]);
            string desc = context.Request.Params["desc"];
            string img = context.Request.Params["img"];
            string destRel = Common.UploadedImageRelPath + Common.UserID;
            string dest = Common.UploadedImagePath + Common.UserID;
            string destFileName = Guid.NewGuid().ToString() + ".jpg";
            string destFileFullPath = Path.Combine(dest, destFileName);
            WebClient client = new WebClient();
            MemoryStream ms = new MemoryStream(client.DownloadData(img));
            using (Image image = Image.FromStream(ms))
            {
                if (!Directory.Exists(dest))
                    Directory.CreateDirectory(dest);
                image.Save(destFileFullPath, ImageFormat.Jpeg);
            }
            string relPath = Common.UserID + "/" + destFileName;
            byte[] _d = File.ReadAllBytes(destFileFullPath);
            ulong _crc64;
            ulong _murmur2;
            ulong _fnv1a;
            short height, width;
            int imageID = 0;
            CalcHash(_d, out _crc64, out _murmur2, out _fnv1a, out height, out width);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Data.dbml.DataContext _context = this.GetDataContext2;
                    var imgtb = _context.Images.Where(o => o.FNV1a == _fnv1a).FirstOrDefault();
                    var theboard = _context.Boards.Where(o => o.ID == board).Single();
                    if (imgtb == null) //only one image entry can exists
                    {
                        Data.dbml.Images i = new Data.dbml.Images();
                        i.CRC64 = _crc64;
                        i.MURMUR2 = _murmur2;
                        i.Tagged = true;
                        i.FNV1a = _fnv1a;
                        i.Image_Height = height;
                        i.Image_Width = width;
                        i.Uploaded = true;
                        i.RelativeImage_Path = relPath;
                        _context.Images.InsertOnSubmit(i);
                        _context.SubmitChanges();
                        imageID = i.ID;
                    }
                    else
                    {
                        imageID = imgtb.ID;
                    }
                    Data.dbml.BoardsImagesMapping bim = new Data.dbml.BoardsImagesMapping();
                    bim.BoardID = board;
                    bim.ImageID = imageID;
                    bim.Image_Title = desc;
                    bim.Source = img;
                    bim.UserID = Common.UserID.Value;
                    _context.BoardsImagesMapping.InsertOnSubmit(bim);
                    Data.dbml.CategoryImagesMapping cim = new Data.dbml.CategoryImagesMapping();
                    cim.CategoryID = theboard.CatID;
                    cim.ImageID = imageID;
                    _context.CategoryImagesMapping.InsertOnSubmit(cim);
                    UpdatePoints(Common.PointsNewPin);
                    _context.SubmitChanges();
                    UpdateUPCount();
                    scope.Complete();
                    context.WriteJsonP(JsonConvert.SerializeObject(new { PinID = (_crc64 + (ulong)bim.ID).ToString() }));
                }
                catch (Exception e)
                {
                    throw;
                }
                finally
                {
                    scope.Dispose();
                }
            }
        }

        private static void CalcHash(byte[] _d, out ulong _crc64, out ulong _murmur2, out ulong _fnv1a, out short height, out short width)
        {
            IHash fnv1a = HashFactory.Hash64.CreateFNV1a();
            IHash crc64 = HashFactory.Checksum.CreateCRC64b();
            IHash murmur2 = HashFactory.Hash64.CreateMurmur2();
            _crc64 = crc64.ComputeBytes(_d).GetULong();
            _murmur2 = murmur2.ComputeBytes(_d).GetULong();
            _fnv1a = fnv1a.ComputeBytes(_d).GetULong();
            using (Image image = Image.FromStream(new MemoryStream(_d)))
            {
                height = (short)image.Height;
                width = (short)image.Width;
            }
        }
        private void SaveUploadedPin(HttpContext context)
        {
            int board = int.Parse(context.Request.Params["board"]);
            string desc = context.Request.Params["desc"];
            string img = context.Request.Params["img"];
            IHash fnv1a = HashFactory.Hash64.CreateFNV1a();
            IHash crc64 = HashFactory.Checksum.CreateCRC64b();
            IHash murmur2 = HashFactory.Hash64.CreateMurmur2();
            Uri url = new Uri(img);
            string image = url.Segments.Last();
            string fp = Path.Combine(Common.Temp, Common.UserID.ToString(), image);
            FileInfo fInfo = new FileInfo(fp);
            string destRel = Common.UploadedImageRelPath + Common.UserID;
            string dest = Common.UploadedImagePath + Common.UserID;
            string destFileName = Common.MoveAndRenameFileIfExists(fInfo, dest);
            string relPath = Common.UserID + "/" + destFileName;
            byte[] _d = File.ReadAllBytes(Path.Combine(dest, destFileName));
            ulong _crc64;
            ulong _murmur2;
            ulong _fnv1a;
            short height, width;
            int imageID = 0;
            CalcHash(_d, out _crc64, out _murmur2, out _fnv1a, out height, out width);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Data.dbml.DataContext _context = this.GetDataContext2;
                    var imgtb = _context.Images.Where(o => o.FNV1a == _fnv1a && o.CRC64 == _crc64 && o.MURMUR2 == _murmur2).FirstOrDefault();
                    var theboard = _context.Boards.Where(o => o.ID == board).Single();

                    if (imgtb == null) //only one image entry can exists
                    {
                        Data.dbml.Images i = new Data.dbml.Images();
                        i.CRC64 = _crc64;
                        i.MURMUR2 = _murmur2;
                        i.FNV1a = _fnv1a;
                        i.Image_Height = height;
                        i.Image_Width = width;
                        i.Uploaded = true;
                        i.Tagged = true;
                        i.RelativeImage_Path = relPath;
                        _context.Images.InsertOnSubmit(i);
                        _context.SubmitChanges();
                        imageID = i.ID;
                    }
                    else
                    {
                        imageID = imgtb.ID;
                    }
                    Data.dbml.BoardsImagesMapping bim = new Data.dbml.BoardsImagesMapping();
                    bim.BoardID = board;
                    bim.ImageID = imageID;
                    bim.Image_Title = desc;
                    bim.UserID = Common.UserID;
                    _context.BoardsImagesMapping.InsertOnSubmit(bim);
                    Data.dbml.CategoryImagesMapping cim = new Data.dbml.CategoryImagesMapping();
                    cim.CategoryID = theboard.CatID;
                    cim.ImageID = imageID;
                    _context.CategoryImagesMapping.InsertOnSubmit(cim);
                    UpdatePoints(Common.PointsNewPin);
                    _context.SubmitChanges();
                    UpdateUPCount();
                    scope.Complete();
                    context.WriteJsonP(JsonConvert.SerializeObject(new { PinID = ((ulong)bim.ID + _crc64).ToString() }));
                }
                catch (Exception e)
                {
                    throw;
                }
                finally
                {
                    scope.Dispose();
                }
            }
        }
        private void Upload(HttpContext context)
        {
            string tpath = Common.Temp + Common.UserID;
            if (!Directory.Exists(tpath))
                Directory.CreateDirectory(tpath);
            HttpPostedFile file = context.Request.Files[0];
            string nfn = Guid.NewGuid().ToString() + "." + file.FileName.Split('.')[1];
            string fp = Common.RelTemp + Common.UserID + "/" + nfn;
            file.SaveAs(tpath + "\\" + nfn);
            context.WriteJsonP(JsonConvert.SerializeObject(new { file = fp, fn = nfn }));
        }

        private void AppLogin(HttpContext context)
        {
            string user = context.Request.Params["user"];
            string pass = context.Request.Params["pass"];
            string match = Common.GetHash(pass);
            var obj = (from o in GetDataContext2.AppUsers
                       where (o.Email == user || o.Name == user) && o.Password == match
                       select o).SingleOrDefault();
            if (obj == null)
                context.Response.Write(strings.Login_Error);
            else
            {
                var Fobj = (from f in GetDataContext2.Vw_FollowCount
                            where f.UserID == obj.ID
                            select f).Single();
                Common.WriteValue(Common.AuthCookie, obj.ID.ToString());
                Common.WriteValue(Common.InfoCookie, JObject.FromObject(new
                {
                    email = obj.Email,
                    name = string.IsNullOrEmpty(obj.FirstName) ? obj.Name : obj.FirstName,
                    avatar = string.IsNullOrWhiteSpace(obj.Avatar) ? null : Common.UploadedImageRelPath + obj.Avatar,
                    boards = obj.Boards.Count(),
                    pins = obj.BoardsImagesMapping.Count(),
                    likes = obj.Likes.Count(),
                    points = obj.Points,
                    flcnt = Fobj.Following_Count,
                    frcnt = Fobj.Follower_Count
                }));
            }
        }

        private void RequestInvite(HttpContext context)
        {
            string email = context.Request.Params["email"];
            Data.dbml.AppUsers user = GetDataContext2.AppUsers.FirstOrDefault(o => o.Email == email);
            if (GetDataContext2.AppUsers.Any(o => o.Email == email))
                context.Response.WriteError(strings.Email_Registered);
            else
                context.Server.Execute(string.Format("signup.aspx?email={0}", email));
        }
        private void CreateBoard(HttpContext context)
        {
            Data.dbml.Boards boards = new Data.dbml.Boards();
            boards.Name = context.Request.Params["name"];
            GetDataContext2.Boards.InsertOnSubmit(boards);
            GetDataContext2.SubmitChanges();
        }
        private void Board(HttpContext context)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    int catid = int.Parse(context.Request.Params["catid"]);
                    int imageId = int.Parse(context.Request.Params["imageId"]);
                    bool _checked = bool.Parse(context.Request.Params["checked"]);
                    if (_checked)
                    {
                        GetDataContext2.BoardsImagesMapping.InsertOnSubmit(new Data.dbml.BoardsImagesMapping()
                        {
                            ImageID = imageId,
                            BoardID = catid,
                            UserID = Common.UserID
                        });
                    }
                    else
                    {
                        var query = GetDataContext2.BoardsImagesMapping.Where(o => o.BoardID == catid && o.ImageID == imageId);
                        GetDataContext2.BoardsImagesMapping.DeleteAllOnSubmit(query);

                    }
                    GetDataContext2.SubmitChanges();
                    SqlHelper.ExecuteNonQuery((SqlConnection)((EntityConnection)this.GetDataContext2.Connection).StoreConnection, "dbo.SetTaggedFlag", imageId);
                    scope.Complete();
                }
                catch (Exception e)
                {
                    throw;
                }
                finally
                {
                    scope.Dispose();
                }
            }
        }
        private void Login(HttpContext context)
        {
            string user = context.Request.Params["user"];
            string pass = context.Request.Params["pass"];
            Data.dbml.User obj = this.GetDataContext2.User.FirstOrDefault(o => o.Name == user && o.Password == pass);
            if (obj != null)
            {
                Common.WriteValue(Common.AuthCookie, obj.ID.ToString());
                Common.WriteValue(Common.InfoCookie, JObject.FromObject(new { obj.Name }));
                context.Response.Write("success");
            }
        }
        private void Cat(HttpContext context)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    int catid = int.Parse(context.Request.Params["catid"]);
                    int imageId = int.Parse(context.Request.Params["imageId"]);
                    bool _checked = bool.Parse(context.Request.Params["checked"]);
                    if (_checked)
                    {
                        GetDataContext2.CategoryImagesMapping.InsertOnSubmit(new Data.dbml.CategoryImagesMapping()
                        {
                            ImageID = imageId,
                            CategoryID = catid,
                            UserID = Common.UserID
                        });
                    }
                    else
                    {
                        var query = GetDataContext2.CategoryImagesMapping.Where(o => o.CategoryID == catid && o.ImageID == imageId);
                        GetDataContext2.CategoryImagesMapping.DeleteAllOnSubmit(query);

                    }
                    GetDataContext2.SubmitChanges();
                    GetDataContext3.SetTaggedFlag(imageId);
                    scope.Complete();
                }
                catch (Exception e)
                {
                    throw;
                }
                finally
                {
                    scope.Dispose();
                }
            }
        }

        private void UnFollowUser(HttpContext context)
        {
            int? UnFollowID = context.Request.Params["F_ID"].ToInt() ?? Common.VUserID;
            var fdel = GetDataContext2.FollowingUser.Single(a => a.UserID == Common.UserID && a.FollowingID == UnFollowID);
            GetDataContext2.FollowingUser.DeleteOnSubmit(fdel);
            GetDataContext2.SubmitChanges();
            UpdateFlCount();
        }

        private void FollowUser(HttpContext context)
        {
            int? FollowID = context.Request.Params["F_ID"].ToInt() ?? Common.VUserID;
            GetDataContext2.FollowingUser.InsertOnSubmit(new Data.dbml.FollowingUser()
            {
                FollowingID = FollowID.Value,
                UserID = Common.UserID.Value,
                Create_Date = DateTime.Now
            });
            GetDataContext2.SubmitChanges();
            UpdateFlCount();
        }

        private void UpdateUPCount()
        {
            var obj = (from o in GetDataContext2.AppUsers
                       where (o.ID == Convert.ToInt32(Common.UserID))
                       select o).SingleOrDefault();

            Common.WriteValue(Common.InfoCookie, JObject.FromObject(new
            {
                boards = obj.Boards.Count(),
                pins = obj.BoardsImagesMapping.Count(),
                likes = obj.Likes.Count()
            }));
        }

        private void UpdateFlCount()
        {
            var o = GetDataContext2.Vw_FollowCount.Single(o1 => o1.UserID == Common.UserID);
            Common.WriteValue(Common.InfoCookie, JObject.FromObject(new { flcnt = o.Following_Count, frcnt = o.Follower_Count }));
        }
    }
}
