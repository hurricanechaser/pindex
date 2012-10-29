using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;

namespace Nails
{
    public abstract class HttpHandlerBase : IHttpHandler
    {
        protected static readonly string _null = "null";
        public virtual bool IsReusable
        {
            get { return false; }
        }       
        Nails.edmx.NailsProdEntities _GetNailsProdContext;
        public Nails.edmx.NailsProdEntities GetNailsProdContext
        {
            get
            {
                if (_GetNailsProdContext == null) _GetNailsProdContext = new Nails.edmx.NailsProdEntities(Common.NailsProdEntities);
                return _GetNailsProdContext;
            }
        }
        Nails.edmx.PinStatsEntities _GetPinStatsEntitiesContext;
        public Nails.edmx.PinStatsEntities GetPinStatsEntitiesContext
        {
            get
            {
                if (_GetPinStatsEntitiesContext == null) _GetPinStatsEntitiesContext = new Nails.edmx.PinStatsEntities(Common.PinStatsEntities);
                return _GetPinStatsEntitiesContext;
            }
        }
        Nails.dbml.NailsProdDataContext _GetNailsProdContext2;
        public Nails.dbml.NailsProdDataContext GetNailsProdContext2
        {
            get
            {
                if (_GetNailsProdContext2 == null) _GetNailsProdContext2 = new Nails.dbml.NailsProdDataContext(Common.NailsProdConnectionString);
                return _GetNailsProdContext2;
            }
        }
        POCOS.NailsProd _GetNailsProdContext1;
        public POCOS.NailsProd GetNailsProdContext1
        {
            get
            {
                if (_GetNailsProdContext1 == null) _GetNailsProdContext1 = new POCOS.NailsProd();
                return _GetNailsProdContext1;
            }
        }
        SubSonic.POCOS.NailsProdDB _GetNailsProdContext3;
        public SubSonic.POCOS.NailsProdDB GetNailsProdContext3
        {
            get
            {
                if (_GetNailsProdContext3 == null) _GetNailsProdContext3 = new SubSonic.POCOS.NailsProdDB();
                return _GetNailsProdContext3;
            }
        }
        SubSonic.POCOS1.NailsProdDB _GetNailsProdContext4;
        public SubSonic.POCOS1.NailsProdDB GetNailsProdContext4
        {
            get
            {
                if (_GetNailsProdContext4 == null) _GetNailsProdContext4 = new SubSonic.POCOS1.NailsProdDB();
                return _GetNailsProdContext4;
            }
        } 
        static List<Nails.edmx.Category> _all;
        public List<Nails.edmx.Category> all
        {
            get
            {
                if (_all == null) _all = this.GetNailsProdContext.Category.ToList();
                return _all;
            }
        }
        public sealed class Category
        {
            public int ID, index;
            public string Name;
            public IEnumerable<Category> SubCategories;
        }
        public virtual void ProcessRequest(HttpContext context)
        {
            if (_GetNailsProdContext != null)
                _GetNailsProdContext.Dispose();         
        }
    }
}