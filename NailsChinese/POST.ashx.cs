﻿using System;
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
using SubSonic.DataProviders;


namespace Nails
{
    /// <summary>
    /// Summary description for GET
    /// </summary>
    /// 

    public class POST : HttpHandlerBase
    {
        public override void ProcessRequest(HttpContext context)
        {
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
                case "save":
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
            }
            base.ProcessRequest(context);
        }

        private void DelBoard(HttpContext context)
        {
            int boardid = int.Parse(context.Request.Params["boardid"]);
            GetNailsProdContext3.DeleteBoard(boardid).Execute();
        }

        private void SaveEditBoard(HttpContext context)
        {
            int boardid = int.Parse(context.Request.Params["boardid"]);
            string catid = context.Request.Params["catid"];
            string name = context.Request.Params["name"];
            JArray removebc = JArray.Parse(context.Request.Params["removebc"]);
            JArray bc = JArray.Parse(context.Request.Params["bc"]);
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    foreach (JObject obj in removebc)
                    {
                        GetNailsProdContext3.DeleteBoardContributor((string)obj["Name"], boardid).Execute();
                    }
                    foreach (JObject obj in bc)
                    {
                        GetNailsProdContext3.AddBoardContributor((string)obj["Name"], boardid).Execute();
                    }
                    SubSonic.POCOS.Board board = GetNailsProdContext3.Boards.SingleOrDefault(o => o.ID == boardid);
                    if (!string.IsNullOrEmpty(catid) && catid != _null)
                        board.CatID = int.Parse(catid);
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

        private void DelContributor(HttpContext context)
        {
            string un = context.Request.Params["un"];
            string id = context.Request.Params["boardid"];
            GetNailsProdContext1.Execute(string.Format(@"DELETE FROM BoardContributor
FROM     BoardContributor INNER JOIN
                  AppUsers ON BoardContributor.ContributorID = AppUsers.ID
WHERE  (BoardContributor.BoardID = {0}) AND (AppUsers.Name = '{1}')", id, un));
        }

        private void UserNameAvail(HttpContext context)
        {
            string _un = context.Request.Params["un"];
            if (Common.ReadValue("name") == _un)
                context.Response.Write("This is your Current User Name");
            else
            {
                int un = GetNailsProdContext1.Single<int>("Select count(*) from AppUsers where Name=@0", _un);
                context.Response.Write(string.Format(un > 0 ? @"{0} not available" : @"{0} is available", _un));
            }
        }

        private void SaveEditPin(HttpContext context)
        {
            int BIMID = int.Parse(context.Request.Params["BIMID"]);
            POCOS.BoardsImagesMapping o = POCOS.BoardsImagesMapping.Single("Select * from BoardsImagesMapping WHERE ID=@0 AND USERID=@1", BIMID, Common.UserID);
            o.Image_Title = context.Request.Params["desc"];
            o.Source = context.Request.Params["source"];
            o.BoardID = int.Parse(context.Request.Params["board"]);
            o.Update();
        }

        private void DeletePin(HttpContext context)
        {
            int BIMID = int.Parse(context.Request.Params["BIMID"]);
            GetNailsProdContext3.DeletePin(BIMID, Common.UserID.Value);
        }
        private void ResetPass(HttpContext context)
        {
            string email = context.Request.Params["email"];
            Nails.edmx.AppUsers user = this.GetNailsProdContext.AppUsers.FirstOrDefault(o => o.Email == email);
            if (user != null)
            {
                SmtpClient client = new SmtpClient();
                MailMessage mess = new MailMessage("support@pinpolish.com", email);
                string pass = Common.RandomString(Common.PassMinChars);
                mess.Body = string.Format(File.ReadAllText(context.Server.MapPath("resetpasstmpl.html")), user.Name, user.Email, pass);
                mess.Subject = "Password reset for your PinPolish account";
                mess.IsBodyHtml = true;
                client.Send(mess);
                user.Password = Common.GetHash(pass);
                GetNailsProdContext.SaveChanges();
                context.Response.Write("新密碼已發送到您的電子郵件地址");
            }
            else
                context.Response.Write("未註冊的電子郵件地址與我們的應用程序");
        }
        private void fbLogin(HttpContext context)
        {
            string token = context.Request.Params["token"];
            Facebook.FacebookClient client = new Facebook.FacebookClient(token);
            //client.Post()
            Facebook.JsonObject o = (Facebook.JsonObject)client.Get("/me");
            var db = new PetaPoco.Database(Common.NailsProdConnectionString, "System.Data.SqlClient");
            using (var scope = db.GetTransaction())
            {

                try
                {
                    string first_name = (string)o["first_name"];
                    string name = (string)o["name"];
                    decimal id = Convert.ToDecimal(o["id"]);
                    POCOS.Facebook fb = new POCOS.Facebook();
                    fb.name = name;
                    fb.first_name = first_name;
                    fb.gender = (string)o["gender"];
                    fb.id = id;
                    fb.last_name = (string)o["last_name"];
                    fb.link = (string)o["link"];
                    fb.locale = (string)o["locale"];
                    fb.timezone = Convert.ToDouble(o["timezone"]);
                    string updatedtime = (string)o["updated_time"];
                    DateTime dt;
                    if (DateTime.TryParse(updatedtime, out dt))
                        fb.updated_time = dt;
                    if (db.Exists<POCOS.Facebook>(id))
                        db.Update(fb);
                    else
                        db.Insert(fb);
                    POCOS.AppUser au = POCOS.AppUser.FirstOrDefault("Select top 1 * from AppUsers where facebookid=@0", id);
                    if (au == null)
                    {
                        au = new POCOS.AppUser();
                        au.FirstName = first_name;
                        au.facebookid = id;
                        db.Insert(au);
                    }
                    scope.Complete();
                    CookieUtil.WriteCookie(Common.AuthCookie, EncDec.Encrypt(JsonConvert.SerializeObject(new { ID = au.ID }), Common.DefaultPassword), false);
                    CookieUtil.WriteCookie(Common.InfoCookie, JsonConvert.SerializeObject(new { email = au.Email, name = au.Name, avatar = string.IsNullOrWhiteSpace(au.Avatar) ? null : Common.UploadedImageRelPath + au.Avatar }), false);
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
            Nails.edmx.Comments c = new Nails.edmx.Comments();
            c.Comments1 = comments;
            c.BoardsImagesMappingID = id;
            c.UserID = Common.UserID.Value;
            this.GetNailsProdContext.Comments.AddObject(c);
            this.GetNailsProdContext.SaveChanges();
        }

        private void ChangePassword(HttpContext context)
        {
            string pass = context.Request.Params["pass"];
            if (!string.IsNullOrEmpty(pass))
            {
                Nails.edmx.AppUsers u = this.GetNailsProdContext.AppUsers.First(o => o.ID == Common.UserID);
                IHash hash = Common.Hash;
                HashResult res = hash.ComputeString(pass);
                u.Password = Common.GetString(res.GetBytes());
                this.GetNailsProdContext.SaveChanges();
                context.Response.Write("Password has been updated");
            }
        }
        private void SaveProfile(HttpContext context)
        {
            Nails.edmx.AppUsers u = this.GetNailsProdContext.AppUsers.First(o => o.ID == Common.UserID);
            if (string.IsNullOrEmpty(u.Password))
            {
                context.Response.WriteError("Password not updated");
            }
            else
            {
                string email = context.Request.Params["email"];
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
                u.Email = email;
                u.FirstName = first_name;
                u.Website = website;
                u.Location = location;
                u.About = about;
                u.Name = name;
                GetNailsProdContext.SaveChanges();
                CookieUtil.WriteCookie(Common.AuthCookie, EncDec.Encrypt(JsonConvert.SerializeObject(new { ID = u.ID }), Common.DefaultPassword), false);
                CookieUtil.WriteCookie(Common.InfoCookie, JsonConvert.SerializeObject(new { email = u.Email, name = u.Name, avatar = string.IsNullOrWhiteSpace(u.Avatar) ? null : Common.UploadedImageRelPath + u.Avatar }), false);

            }

        }

        private void SaveLike(HttpContext context)
        {
            int id = int.Parse(context.Request.Params["id"]);
            bool liked = bool.Parse(context.Request.Params["liked"]);
            if (liked)
            {
                Nails.edmx.Likes l = new Nails.edmx.Likes();
                l.BoardsImagesMappingID = id;
                l.UserID = Common.UserID.Value;
                this.GetNailsProdContext.Likes.AddObject(l);
                this.GetNailsProdContext.SaveChanges();
            }
            else
            {
                this.GetNailsProdContext.ExecuteStoreCommand(string.Format("Delete From Likes where BoardsImagesMappingID={0}", id));
            }
        }

        private void SaveRepin(HttpContext context)
        {
            int id = int.Parse(context.Request.Params["id"]);
            int board = int.Parse(context.Request.Params["board"]);
            string desc = context.Request.Params["desc"];
            string source = context.Request.Params["source"];
            Nails.edmx.BoardsImagesMapping bim = new Nails.edmx.BoardsImagesMapping();
            bim.ImageID = id;
            bim.Source = source;
            bim.Image_Title = desc;
            bim.UserID = Common.UserID;
            bim.BoardID = board;
            this.GetNailsProdContext.BoardsImagesMapping.AddObject(bim);
            this.GetNailsProdContext.SaveChanges();
        }

        private void SaveCreatedBoard(HttpContext context)
        {

            string name = context.Request.Params["name"];
            Nails.edmx.Boards b = this.GetNailsProdContext.Boards.FirstOrDefault(o => o.Name == name && o.UserID == Common.UserID);
            if (b != null)
                context.Response.Write("You already have a board with that name. Please use a different board name");
            else
                SaveBoard(context);

        }

        private void SaveBoard(HttpContext context)
        {
            int cat = int.Parse(context.Request.Params["cat"]);
            string name = context.Request.Params["name"];
            string contributors = context.Request.Params["contributors[]"];
            Nails.edmx.Boards board = new Nails.edmx.Boards()
            {
                Name = name,
                CatID = cat,
                UserID = Common.UserID
            };
            this.GetNailsProdContext.Boards.AddObject(board);
            this.GetNailsProdContext.SaveChanges();
            if (!string.IsNullOrEmpty(contributors))
            {
                string[] acontributors = contributors.Split(',');
                foreach (string contributor in acontributors)
                {
                    this.GetNailsProdContext.BoardContributor.AddObject(new Nails.edmx.BoardContributor()
                    {
                        BoardID = board.ID,
                        ContributorID = GetNailsProdContext3.AppUsers.First(o => o.Email == contributor).ID
                    });
                }
                this.GetNailsProdContext.SaveChanges();
            }
            context.Response.Write(board.ID);
        }

        private void ValidateContributor(HttpContext context)
        {
            string contributor = context.Request.Params["contributor"];
            Nails.edmx.AppUsers user = this.GetNailsProdContext.AppUsers.FirstOrDefault(o => o.Email == contributor || o.Name == contributor);
            if (user != null)
                context.Response.Write(JsonConvert.SerializeObject(new
                {
                    user.Email,
                    user.Avatar,
                    user.FirstName,
                    user.Name
                }));
            else
                context.Response.WriteError("User does not exist");
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
            CalcHash(_d, out _crc64, out _murmur2, out _fnv1a, out height, out width);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Nails.edmx.Images i = new Nails.edmx.Images();
                    i.CRC64 = _crc64;
                    i.MURMUR2 = _murmur2;
                    i.Tagged = true;
                    i.FNV1a = _fnv1a;
                    i.Image_Height = height;
                    i.Image_Width = width;
                    i.Uploaded = true;
                    i.RelativeImage_Path = relPath;
                    Nails.edmx.NailsProdEntities _context = this.GetNailsProdContext;
                    _context.Images.AddObject(i);
                    _context.SaveChanges();
                    Nails.edmx.BoardsImagesMapping bim = new Nails.edmx.BoardsImagesMapping();
                    bim.BoardID = board;
                    bim.ImageID = i.ID;
                    bim.Image_Title = desc;
                    bim.Source = img;
                    bim.UserID = Common.UserID.Value;
                    _context.BoardsImagesMapping.AddObject(bim);
                    _context.SaveChanges();
                    scope.Complete();
                    context.Response.Write(JsonConvert.SerializeObject(new { PinID = (_crc64 + (ulong)bim.ID).ToString() }));
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
            CalcHash(_d, out _crc64, out _murmur2, out _fnv1a, out height, out width);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Nails.edmx.Images i = new Nails.edmx.Images();
                    i.CRC64 = _crc64;
                    i.MURMUR2 = _murmur2;
                    i.FNV1a = _fnv1a;
                    i.Image_Height = height;
                    i.Image_Width = width;
                    i.Uploaded = true;
                    i.Tagged = true;
                    i.RelativeImage_Path = relPath;
                    this.GetNailsProdContext.Images.AddObject(i);
                    this.GetNailsProdContext.SaveChanges();
                    Nails.edmx.BoardsImagesMapping bim = new Nails.edmx.BoardsImagesMapping();
                    bim.BoardID = board;
                    bim.ImageID = i.ID;
                    bim.Image_Title = desc;
                    bim.UserID = Common.UserID.Value;
                    this.GetNailsProdContext.BoardsImagesMapping.AddObject(bim);
                    this.GetNailsProdContext.SaveChanges();
                    scope.Complete();
                    context.Response.Write(JsonConvert.SerializeObject(new { PinID = ((ulong)bim.ID + _crc64).ToString() }));
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
            context.Response.Write(JsonConvert.SerializeObject(new { file = fp, fn = nfn }));
        }

        private void AppLogin(HttpContext context)
        {
            string user = context.Request.Params["user"];
            string pass = context.Request.Params["pass"];
            string match = Common.GetHash(pass);
            SubSonic.POCOS.AppUser obj = SubSonic.POCOS.AppUser.SingleOrDefault(o => o.Email == user);
            if (obj == null)
                context.Response.WriteError("無效的電子郵件地址和/或密碼");
            else
            {
                CookieUtil.WriteCookie(Common.AuthCookie, EncDec.Encrypt(JsonConvert.SerializeObject(new { ID = obj.ID }), Common.DefaultPassword), false);
                CookieUtil.WriteCookie(Common.InfoCookie, JsonConvert.SerializeObject(new
                {
                    email = obj.Email,
                    name = obj.Name,
                    avatar = string.IsNullOrWhiteSpace(obj.Avatar) ? null : Common.UploadedImageRelPath + obj.Avatar
                }), false);
            }
        }

        private void RequestInvite(HttpContext context)
        {
            string email = context.Request.Params["email"];

            Nails.edmx.AppUsers user = this.GetNailsProdContext.AppUsers.FirstOrDefault(o => o.Email == email);
            if (user == null)
            {
                user = new Nails.edmx.AppUsers();
                user.Email = email;
                SmtpClient client = new SmtpClient();
                string hash = Common.GetString(Common.Hash.ComputeString(Guid.NewGuid().ToString()).GetBytes());
                MailMessage mess = new MailMessage("support@pinpolish.com", email);
                mess.Body = string.Format(File.ReadAllText(context.Server.MapPath("signuptmpl.html")), string.Format("<a href='{0}{1}?s={2}'>Click here to register</a>", Common.Domain, Common.InviteUrl, hash));
                mess.Subject = "Sign Up for PinPolish";
                mess.IsBodyHtml = true;
                client.Send(mess);
                user.Invite = hash;
                this.GetNailsProdContext.AppUsers.AddObject(user);
                this.GetNailsProdContext.SaveChanges();
                context.Response.Write("An Invitation has been sent to your email address.Please follow the instructions to complete the registration");
            }
            else
                context.Response.Write("The email address has already been registered with our application");

        }
        private void CreateBoard(HttpContext context)
        {
            Nails.edmx.Boards boards = new Nails.edmx.Boards();
            boards.Name = context.Request.Params["name"];
            this.GetNailsProdContext.AddToBoards(boards);
            this.GetNailsProdContext.SaveChanges();
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
                        this.GetNailsProdContext.BoardsImagesMapping.AddObject(new Nails.edmx.BoardsImagesMapping()
                        {
                            ImageID = imageId,
                            BoardID = catid,
                            UserID = Common.UserID
                        });
                    }
                    else
                    {
                        var query = this.GetNailsProdContext.BoardsImagesMapping.Where(o => o.BoardID == catid && o.ImageID == imageId);
                        foreach (var obj in query)
                        {
                            this.GetNailsProdContext.DeleteObject(obj);
                        }
                    }
                    this.GetNailsProdContext.SaveChanges();
                    SqlHelper.ExecuteNonQuery((SqlConnection)((EntityConnection)this.GetNailsProdContext.Connection).StoreConnection, "dbo.SetTaggedFlag", imageId);
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
            Nails.edmx.User obj = this.GetNailsProdContext.User.FirstOrDefault(o => o.Name == user && o.Password == pass);
            if (obj != null)
            {
                CookieUtil.WriteCookie(Common.AuthCookie, EncDec.Encrypt(JsonConvert.SerializeObject(new { obj.ID }), Common.DefaultPassword), false);
                CookieUtil.WriteCookie(Common.InfoCookie, JsonConvert.SerializeObject(new { obj.Name }), false);
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
                        this.GetNailsProdContext.CategoryImagesMapping.AddObject(new Nails.edmx.CategoryImagesMapping()
                        {
                            ImageID = imageId,
                            CategoryID = catid,
                            UserID = Common.UserID
                        });
                    }
                    else
                    {
                        var query = this.GetNailsProdContext.CategoryImagesMapping.Where(o => o.CategoryID == catid && o.ImageID == imageId);
                        foreach (var obj in query)
                        {
                            this.GetNailsProdContext.DeleteObject(obj);
                        }
                    }
                    this.GetNailsProdContext.SaveChanges();
                    SqlHelper.ExecuteNonQuery((SqlConnection)((EntityConnection)this.GetNailsProdContext.Connection).StoreConnection, "dbo.SetTaggedFlag", imageId);
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
    }
}
