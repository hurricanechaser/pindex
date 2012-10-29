using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System;

namespace Pinjimu
{
    public abstract class HttpHandlerBase : IHttpHandler
    {
        protected static readonly string _null = "null";
        public virtual bool IsReusable
        {
            get { return false; }
        }

        Data.dbml.DataContext _GetDataContext2;
        public Data.dbml.DataContext GetDataContext2
        {
            get
            {
                if (_GetDataContext2 == null) _GetDataContext2 = new Data.dbml.DataContext(Common.DataConnectionString);
                return _GetDataContext2;
            }
        }

        Data.POCOS.DB _GetDataContext1;
        public Data.POCOS.DB GetDataContext1
        {
            get
            {
                if (_GetDataContext1 == null) _GetDataContext1 = new Data.POCOS.DB();
                return _GetDataContext1;
            }
        }
        SubSonic.POCOS.PinjimuDB _GetDataContext3;
        public SubSonic.POCOS.PinjimuDB GetDataContext3
        {
            get
            {
                if (_GetDataContext3 == null) _GetDataContext3 = new SubSonic.POCOS.PinjimuDB();
                return _GetDataContext3;
            }
        }
        SubSonic.POCOS1.PinjimuDB _GetDataContext4;
        public SubSonic.POCOS1.PinjimuDB GetDataContext4
        {
            get
            {
                if (_GetDataContext4 == null) _GetDataContext4 = new SubSonic.POCOS1.PinjimuDB();
                return _GetDataContext4;
            }
        }
        SqlConnection _SqlConnection;
        public SqlConnection SqlConnection
        {
            get
            {
                if (_SqlConnection == null) (_SqlConnection = new SqlConnection(Common.DataConnectionString)).Open();
                return _SqlConnection;
            }
        }
        static List<Category> _all;
        public List<Category> all
        {
            get
            {
                if (_all == null)
                    _all = (from o in GetDataContext2.Category
                            select new Category
                            {
                                ID = o.ID,
                                Name = o.Name,
                                ParentID = o.ParentID
                            }).ToList();
                return _all;
            }
        }
        public sealed class Category
        {
            public int ID, index;
            public int? ParentID;
            public string Name;
            public IEnumerable<Category> SubCategories;
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            Common.ReadCookieValues();
            string culture = Common.ReadValue(Common.InfoCookie, "culture");
            if (!string.IsNullOrEmpty(culture))
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
        }
        protected void EndRequest(HttpContext context)
        {
            if (_GetDataContext4 != null)
                _GetDataContext4 = null;
            if (_GetDataContext3 != null)
                _GetDataContext3 = null;
            if (_GetDataContext2 != null)
            {
                _GetDataContext2.Dispose();
                _GetDataContext2 = null;
            }
            if (_GetDataContext1 != null)
            {
                _GetDataContext1.Dispose();
                _GetDataContext1 = null;
            }
            Common.WriteCookieValues();
        }

    }
}