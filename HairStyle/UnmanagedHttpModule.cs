using System;
using System.Text;
using System.Web;
using System.Linq;
using System.IO;
using System.Configuration;
using System.Threading;
using System.Web.Configuration;
using Newtonsoft.Json;
namespace HairStyle
{
    public class UnManagedHttpModule : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members
        //pindex.PindexProd.dbml.PindexProdDataContext _GetPindexProdContext1;
        //public pindex.PindexProd.dbml.PindexProdDataContext GetPindexProdContext1
        //{
        //    get
        //    {
        //        if (_GetPindexProdContext1 == null) _GetPindexProdContext1 = new pindex.PindexProd.dbml.PindexProdDataContext(Common.PindexProdConnectionString);
        //        return _GetPindexProdContext1;
        //    }
        //}
        //pindex.PindexProd.edmx.PindexProdEntities _GetPindexProdContext;
        //public pindex.PindexProd.edmx.PindexProdEntities GetPindexProdContext
        //{
        //    get
        //    {
        //        if (_GetPindexProdContext == null) _GetPindexProdContext = new pindex.PindexProd.edmx.PindexProdEntities(Common.PindexProdEntities);
        //        return _GetPindexProdContext;
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
