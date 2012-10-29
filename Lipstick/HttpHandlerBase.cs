using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;

namespace Lipstick
{
    public abstract class HttpHandlerBase : IHttpHandler
    {
        protected static readonly string _null = "null";
        public virtual bool IsReusable
        {
            get { return false; }
        }
        
        Lipstick.dbml.LipstickDataContext _GetLipstickContext2;
        public Lipstick.dbml.LipstickDataContext GetLipstickContext2
        {
            get
            {
                if (_GetLipstickContext2 == null) _GetLipstickContext2 = new Lipstick.dbml.LipstickDataContext(Common.LipstickConnectionString);
                return _GetLipstickContext2;
            }
        }
        POCOS.Lipstick _GetLipstickContext1;
        public POCOS.Lipstick GetLipstickContext1
        {
            get
            {
                if (_GetLipstickContext1 == null) _GetLipstickContext1 = new POCOS.Lipstick();
                return _GetLipstickContext1;
            }
        }
        SubSonic.POCOS.LipstickDB _GetLipstickContext3;
        public SubSonic.POCOS.LipstickDB GetLipstickContext3
        {
            get
            {
                if (_GetLipstickContext3 == null) _GetLipstickContext3 = new SubSonic.POCOS.LipstickDB();
                return _GetLipstickContext3;
            }
        }
        SubSonic.POCOS1.LipstickDB _GetLipstickContext4;
        public SubSonic.POCOS1.LipstickDB GetLipstickContext4
        {
            get
            {
                if (_GetLipstickContext4 == null) _GetLipstickContext4 = new SubSonic.POCOS1.LipstickDB();
                return _GetLipstickContext4;
            }
        }
        static List<Category> _all;
        public List<Category> all
        {
            get
            {
                if (_all == null)
                    _all = (from o in GetLipstickContext2.Category
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
            if (_GetLipstickContext1 != null)
                _GetLipstickContext1.Dispose();
            if (_GetLipstickContext2 != null)
                _GetLipstickContext2.Dispose();
        }
    }
}