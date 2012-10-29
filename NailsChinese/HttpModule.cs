using System;
using System.Web;
using System.Linq;
using System.IO;
using System.Configuration;
using System.Threading;
using Newtonsoft.Json;
namespace Nails
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
            context.EndRequest += new EventHandler(context_EndRequest);
        }

        #endregion

        private void context_EndRequest(object sender, EventArgs e)
        {
            HttpApplication context = (HttpApplication)sender;
            if (!CookieUtil.CookieExists("session"))
                CookieUtil.WriteCookie("session", JsonConvert.SerializeObject(new { id = Common.GetHash(Guid.NewGuid().ToString()), app = "nails" }), false);
        }
    }
}
