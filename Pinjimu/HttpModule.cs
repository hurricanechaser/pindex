using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Web;
using System.Linq;
using System.IO;
using System.Configuration;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Pinjimu
{
    public class HttpModule : IHttpModule
    {

        /// <summary>
        /// You will need to configure this module in the web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }
        private Data.dbml.DataContext _GetDataContext2;

        public Data.dbml.DataContext GetDataContext2
        {
            get
            {
                if (_GetDataContext2 == null) _GetDataContext2 = new Data.dbml.DataContext(Common.DataConnectionString);
                return _GetDataContext2;
            }
        }
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
            context.EndRequest += new EventHandler(context_EndRequest);
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication context = (HttpApplication)sender;
            Dictionary<string, string> _dict = new Dictionary<string, string>();
            string[] appCookies = Common.AppCookies.Split(',');
            foreach (string cookieName in context.Request.Cookies)
            {
                if (appCookies.Contains(cookieName))
                {
                    HttpCookie cookie = context.Request.Cookies[cookieName];
                    if (_dict.ContainsKey(cookieName)) _dict[cookieName] = context.Server.UrlDecode(cookie.Value);
                    else _dict.Add(cookieName, context.Server.UrlDecode(cookie.Value));
                }
            }
            Common.CookieValue = _dict;
            string culture = Common.ReadValue(Common.sessioncookie, "culture");
            if (!string.IsNullOrEmpty(culture))
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
            if (context.Request.Url.Segments.Length > 1)
            {
                string user = context.Request.Url.Segments.Last().ToLower();
                if (!user.Contains(".") && !user.Contains("pinjimu"))
                {
                   
                    context.Server.TransferRequest(string.Format("User.aspx?user={0}", user));
                  
                }
            }
        }

        #endregion

        private void context_EndRequest(object sender, EventArgs e)
        {
            HttpApplication context = (HttpApplication)sender;
            Dictionary<string, string> _dict = Common.CookieValue;
            if (_dict != null)
                foreach (var value in _dict)
                {
                    HttpCookie cookie = context.Response.Cookies[value.Key];
                    if (cookie == null)
                        context.Response.Cookies.Add(new HttpCookie(value.Key, (value.Value))
                        {
                            HttpOnly = false
                        });
                    else cookie.Value = value.Value;
                }
        }
    }
}
