using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;

namespace EyeShadow
{
    public abstract class HttpHandlerBase : IHttpHandler
    {
        protected static readonly string _null = "null";
        public virtual bool IsReusable
        {
            get { return false; }
        }
        
        EyeShadow.dbml.EyeShadowDataContext _GetEyeShadowContext2;
        public EyeShadow.dbml.EyeShadowDataContext GetEyeShadowContext2
        {
            get
            {
                if (_GetEyeShadowContext2 == null) _GetEyeShadowContext2 = new EyeShadow.dbml.EyeShadowDataContext(Common.EyeShadowConnectionString);
                return _GetEyeShadowContext2;
            }
        }
        POCOS.EyeShadow _GetEyeShadowContext1;
        public POCOS.EyeShadow GetEyeShadowContext1
        {
            get
            {
                if (_GetEyeShadowContext1 == null) _GetEyeShadowContext1 = new POCOS.EyeShadow();
                return _GetEyeShadowContext1;
            }
        }
        SubSonic.POCOS.EyeShadowDB _GetEyeShadowContext3;
        public SubSonic.POCOS.EyeShadowDB GetEyeShadowContext3
        {
            get
            {
                if (_GetEyeShadowContext3 == null) _GetEyeShadowContext3 = new SubSonic.POCOS.EyeShadowDB();
                return _GetEyeShadowContext3;
            }
        }
        SubSonic.POCOS1.EyeShadowDB _GetEyeShadowContext4;
        public SubSonic.POCOS1.EyeShadowDB GetEyeShadowContext4
        {
            get
            {
                if (_GetEyeShadowContext4 == null) _GetEyeShadowContext4 = new SubSonic.POCOS1.EyeShadowDB();
                return _GetEyeShadowContext4;
            }
        }
        static List<Category> _all;
        public List<Category> all
        {
            get
            {
                if (_all == null)
                    _all = (from o in GetEyeShadowContext2.Category
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
            if (_GetEyeShadowContext1 != null)
                _GetEyeShadowContext1.Dispose();
            if (_GetEyeShadowContext2 != null)
                _GetEyeShadowContext2.Dispose();
        }
    }
}