using System;
using System.Globalization;
using System.Web;
using System.Linq;
using System.IO;
using System.Configuration;
using System.Threading;
using Newtonsoft.Json;
namespace PindexProd
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

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
            context.EndRequest += new EventHandler(context_EndRequest);
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            string culture = Common.ReadValue(Common.sessioncookie, "culture");
            if(!string.IsNullOrEmpty(culture))
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
        }

        #endregion

        private void context_EndRequest(object sender, EventArgs e)
        {
            HttpApplication context = (HttpApplication)sender;
            if (!CookieUtil.CookieExists(Common.sessioncookie))
            {
                CookieUtil.WriteCookie(Common.sessioncookie, JsonConvert.SerializeObject(new
                {
                    id = Common.GetHash(Guid.NewGuid().ToString()),
                    app = "PindexProd",
                    pts = new { total = 0, ids = new int[0] }
                }), false);
            }
        }
    }
}
