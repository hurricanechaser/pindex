using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Pinjimu
{
    public abstract class PageHandlerBase : System.Web.UI.Page
    {


        private Data.dbml.DataContext _GetDataContext2;

        public Data.dbml.DataContext GetDataContext2
        {
            get
            {
                if (_GetDataContext2 == null) _GetDataContext2 = new Data.dbml.DataContext(Common.DataConnectionString);
                return _GetDataContext2;
            }
        }

        private Data.POCOS.DB _GetDataContext1;

        public Data.POCOS.DB GetDataContext1
        {
            get
            {
                if (_GetDataContext1 == null) _GetDataContext1 = new Data.POCOS.DB();
                return _GetDataContext1;
            }
        }

        //private static List<Nails.edmx.Category> _all;

        //public List<Nails.edmx.Category> all
        //{
        //    get
        //    {
        //        if (_all == null) _all = this.GetDataContext.Category.ToList();
        //        return _all;
        //    }
        //}

        //public sealed class Category
        //{
        //    public int ID, index;
        //    public string Name;
        //    public IEnumerable<Category> SubCategories;
        //}

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Common.ReadCookieValues();
            string culture = Common.ReadValue(Common.InfoCookie, "culture");
            if (!string.IsNullOrEmpty(culture))
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            if (_GetDataContext1 != null) _GetDataContext1.Dispose();
            if (_GetDataContext2 != null) _GetDataContext2.Dispose();

            Common.WriteCookieValues();
        }
    }
}


