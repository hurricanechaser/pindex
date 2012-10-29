using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;

namespace PindexProd
{
    public abstract class PageHandlerBase : System.Web.UI.Page
    {
      
     

        private PindexProd.dbml.PindexProdDataContext _GetPindexProdContext2;

        public PindexProd.dbml.PindexProdDataContext GetPindexProdContext2
        {
            get
            {
                if (_GetPindexProdContext2 == null) _GetPindexProdContext2 = new PindexProd.dbml.PindexProdDataContext(Common.PindexProdConnectionString);
                return _GetPindexProdContext2;
            }
        }

        private POCOS.PindexProd _GetPindexProdContext1;

        public POCOS.PindexProd GetPindexProdContext1
        {
            get
            {
                if (_GetPindexProdContext1 == null) _GetPindexProdContext1 = new POCOS.PindexProd();
                return _GetPindexProdContext1;
            }
        }

       

        public sealed class Category
        {
            public int ID, index;
            public string Name;
            public IEnumerable<Category> SubCategories;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            if (_GetPindexProdContext1 != null) _GetPindexProdContext1.Dispose();
            if (_GetPindexProdContext2 != null) _GetPindexProdContext2.Dispose();
            base.OnLoadComplete(e);
        }
    }
}
     
    
