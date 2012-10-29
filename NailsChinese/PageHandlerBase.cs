using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;

namespace Nails
{
    public abstract class PageHandlerBase : System.Web.UI.Page
    {
      
        private Nails.edmx.NailsProdEntities _GetNailsProdContext;

        public Nails.edmx.NailsProdEntities GetNailsProdContext
        {
            get
            {
                if (_GetNailsProdContext == null) _GetNailsProdContext = new Nails.edmx.NailsProdEntities(Common.NailsProdEntities);
                return _GetNailsProdContext;
            }
        }

        private Nails.edmx.PinStatsEntities _GetPinStatsEntitiesContext;

        public Nails.edmx.PinStatsEntities GetPinStatsEntitiesContext
        {
            get
            {
                if (_GetPinStatsEntitiesContext == null) _GetPinStatsEntitiesContext = new Nails.edmx.PinStatsEntities(Common.PinStatsEntities);
                return _GetPinStatsEntitiesContext;
            }
        }

        private Nails.dbml.NailsProdDataContext _GetNailsProdContext2;

        public Nails.dbml.NailsProdDataContext GetNailsProdContext2
        {
            get
            {
                if (_GetNailsProdContext2 == null) _GetNailsProdContext2 = new Nails.dbml.NailsProdDataContext(Common.NailsProdConnectionString);
                return _GetNailsProdContext2;
            }
        }

        private POCOS.NailsProd _GetNailsProdContext1;

        public POCOS.NailsProd GetNailsProdContext1
        {
            get
            {
                if (_GetNailsProdContext1 == null) _GetNailsProdContext1 = new POCOS.NailsProd();
                return _GetNailsProdContext1;
            }
        }

        //private static List<Nails.edmx.Category> _all;

        //public List<Nails.edmx.Category> all
        //{
        //    get
        //    {
        //        if (_all == null) _all = this.GetNailsProdContext.Category.ToList();
        //        return _all;
        //    }
        //}

        //public sealed class Category
        //{
        //    public int ID, index;
        //    public string Name;
        //    public IEnumerable<Category> SubCategories;
        //}

        protected override void OnLoadComplete(EventArgs e)
        {
            if (_GetNailsProdContext != null) _GetNailsProdContext.Dispose();
            base.OnLoadComplete(e);
        }
    }
}
     
    
