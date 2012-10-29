using System;
using System.Text;
using System.Web;
using System.Linq;
using System.IO;
using System.Configuration;
using System.Threading;
using System.Web.Configuration;
using Newtonsoft.Json;
namespace EyeShadow
{
    public class UnManagedHttpModule : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members
        //pindex.EyeShadow.dbml.EyeShadowDataContext _GetEyeShadowContext1;
        //public pindex.EyeShadow.dbml.EyeShadowDataContext GetEyeShadowContext1
        //{
        //    get
        //    {
        //        if (_GetEyeShadowContext1 == null) _GetEyeShadowContext1 = new pindex.EyeShadow.dbml.EyeShadowDataContext(Common.EyeShadowConnectionString);
        //        return _GetEyeShadowContext1;
        //    }
        //}
        //pindex.EyeShadow.edmx.EyeShadowEntities _GetEyeShadowContext;
        //public pindex.EyeShadow.edmx.EyeShadowEntities GetEyeShadowContext
        //{
        //    get
        //    {
        //        if (_GetEyeShadowContext == null) _GetEyeShadowContext = new pindex.EyeShadow.edmx.EyeShadowEntities(Common.EyeShadowEntities);
        //        return _GetEyeShadowContext;
        //    }
        //}
        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication context)
        {
           //context.EndRequest+=new EventHandler(context_EndRequest);
        }

        //private void context_EndRequest(object sender, EventArgs e)
        //{
        //    HttpApplication context = (HttpApplication) sender;
        //    string url= context.Request.Url.Segments.Last();
        //   switch(url)
        //   {
        //       case "freshpin.js":
        //           context.Response.Flush();
        //           StringBuilder sb = new StringBuilder();
        //           sb.AppendFormat("domain={0}",WebConfigurationManager.AppSettings["domain"]);
        //           context.Response.Write(sb.ToString());
        //           break;
        //   }
           

        //}

        #endregion
    }
}
