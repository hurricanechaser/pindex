using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;

namespace PindexProd
{
    public abstract class HttpHandlerBase : IHttpHandler
    {
        protected static readonly string _null = "null";
        public virtual bool IsReusable
        {
            get { return false; }
        }
        
        PindexProd.dbml.PindexProdDataContext _GetPindexProdContext2;
        public PindexProd.dbml.PindexProdDataContext GetPindexProdContext2
        {
            get
            {
                if (_GetPindexProdContext2 == null) _GetPindexProdContext2 = new PindexProd.dbml.PindexProdDataContext(Common.PindexProdConnectionString);
                return _GetPindexProdContext2;
            }
        }
        POCOS.PindexProd _GetPindexProdContext1;
        public POCOS.PindexProd GetPindexProdContext1
        {
            get
            {
                if (_GetPindexProdContext1 == null) _GetPindexProdContext1 = new POCOS.PindexProd();
                return _GetPindexProdContext1;
            }
        }
        SubSonic.POCOS.PindexProdDB _GetPindexProdContext3;
        public SubSonic.POCOS.PindexProdDB GetPindexProdContext3
        {
            get
            {
                if (_GetPindexProdContext3 == null) _GetPindexProdContext3 = new SubSonic.POCOS.PindexProdDB();
                return _GetPindexProdContext3;
            }
        }
        SubSonic.POCOS1.PindexProdDB _GetPindexProdContext4;
        public SubSonic.POCOS1.PindexProdDB GetPindexProdContext4
        {
            get
            {
                if (_GetPindexProdContext4 == null) _GetPindexProdContext4 = new SubSonic.POCOS1.PindexProdDB();
                return _GetPindexProdContext4;
            }
        }
        static List<Category> _all;
        public List<Category> all
        {
            get
            {
                if (_all == null)
                    _all = (from o in GetPindexProdContext2.Category
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
            if (_GetPindexProdContext1 != null)
                _GetPindexProdContext1.Dispose();
            if (_GetPindexProdContext2 != null)
                _GetPindexProdContext2.Dispose();
        }
    }
}