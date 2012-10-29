using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;

namespace HairStyle
{
    public abstract class HttpHandlerBase : IHttpHandler
    {
        protected static readonly string _null = "null";
        public virtual bool IsReusable
        {
            get { return false; }
        }
        
        HairStyle.dbml.HairStyleDataContext _GetHairStyleContext2;
        public HairStyle.dbml.HairStyleDataContext GetHairStyleContext2
        {
            get
            {
                if (_GetHairStyleContext2 == null) _GetHairStyleContext2 = new HairStyle.dbml.HairStyleDataContext(Common.HairStyleConnectionString);
                return _GetHairStyleContext2;
            }
        }
        POCOS.HairStyle _GetHairStyleContext1;
        public POCOS.HairStyle GetHairStyleContext1
        {
            get
            {
                if (_GetHairStyleContext1 == null) _GetHairStyleContext1 = new POCOS.HairStyle();
                return _GetHairStyleContext1;
            }
        }
        SubSonic.POCOS.HairStyleDB _GetHairStyleContext3;
        public SubSonic.POCOS.HairStyleDB GetHairStyleContext3
        {
            get
            {
                if (_GetHairStyleContext3 == null) _GetHairStyleContext3 = new SubSonic.POCOS.HairStyleDB();
                return _GetHairStyleContext3;
            }
        }
        SubSonic.POCOS1.HairStyleDB _GetHairStyleContext4;
        public SubSonic.POCOS1.HairStyleDB GetHairStyleContext4
        {
            get
            {
                if (_GetHairStyleContext4 == null) _GetHairStyleContext4 = new SubSonic.POCOS1.HairStyleDB();
                return _GetHairStyleContext4;
            }
        }
        static List<Category> _all;
        public List<Category> all
        {
            get
            {
                if (_all == null)
                    _all = (from o in GetHairStyleContext2.Category
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
            if (_GetHairStyleContext1 != null)
                _GetHairStyleContext1.Dispose();
            if (_GetHairStyleContext2 != null)
                _GetHairStyleContext2.Dispose();
        }
    }
}