﻿using System;
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
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication context = (HttpApplication)sender;              
            if (context.Request.Url.Segments.Length > 1)
            {
                string user = context.Request.Url.Segments.Last().ToLower();
                if (!user.Contains("."))
                {
                    StringWriter writer = new StringWriter();
                    context.Server.Execute(string.Format("User.aspx?user={0}", user), writer);
                    context.Response.Write(writer.ToString());
                    context.Response.End();
                }
            }
        }

        #endregion
  
    }
}
