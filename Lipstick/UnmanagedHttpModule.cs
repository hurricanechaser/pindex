using System;
using System.Text;
using System.Web;
using System.Linq;
using System.IO;
using System.Configuration;
using System.Threading;
using System.Web.Configuration;
using Newtonsoft.Json;
namespace Lipstick
{
    public class UnManagedHttpModule : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members
        //pindex.Lipstick.dbml.LipstickDataContext _GetLipstickContext1;
        //public pindex.Lipstick.dbml.LipstickDataContext GetLipstickContext1
        //{
        //    get
        //    {
        //        if (_GetLipstickContext1 == null) _GetLipstickContext1 = new pindex.Lipstick.dbml.LipstickDataContext(Common.LipstickConnectionString);
        //        return _GetLipstickContext1;
        //    }
        //}
        //pindex.Lipstick.edmx.LipstickEntities _GetLipstickContext;
        //public pindex.Lipstick.edmx.LipstickEntities GetLipstickContext
        //{
        //    get
        //    {
        //        if (_GetLipstickContext == null) _GetLipstickContext = new pindex.Lipstick.edmx.LipstickEntities(Common.LipstickEntities);
        //        return _GetLipstickContext;
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
