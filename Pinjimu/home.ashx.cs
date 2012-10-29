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




namespace Pinjimu
{
    /// <summary>
    /// Summary description for GET
    /// </summary>
    /// 

    public class home : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.Cache.SetNoStore();
            if (CookieUtil.CookieExists(Common.AuthCookie))
                context.Response.WriteFile("LoggedIn.aspx", true);
            else
                context.Response.WriteFile("home.aspx", true);
        }
    }
}