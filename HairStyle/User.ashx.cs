using System;
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




namespace HairStyle
{
    /// <summary>
    /// Summary description for GET
    /// </summary>
    /// 

    public class User : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string name = context.Request.Params["name"];
            POCOS.AppUser obj = POCOS.AppUser.Single(string.Format("Select * from AppUsers WHERE Name='{0}'", name));
            Common.UpdateCookie(Common.InfoCookie, JObject.FromObject(new
            {
                vuID = obj.ID,
                vuemail = obj.Email,
                vuname = obj.Name,
                vuavatar = Common.UploadedImageRelPath + obj.Avatar
            }));
            context.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.Cache.SetNoStore();
            context.Response.WriteFile("LoggedIn.html");
        }
    }
}