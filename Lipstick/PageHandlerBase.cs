using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;

namespace Lipstick
{
    public abstract class PageHandlerBase : System.Web.UI.Page
    {
      
     

        private Lipstick.dbml.LipstickDataContext _GetLipstickContext2;

        public Lipstick.dbml.LipstickDataContext GetLipstickContext2
        {
            get
            {
                if (_GetLipstickContext2 == null) _GetLipstickContext2 = new Lipstick.dbml.LipstickDataContext(Common.LipstickConnectionString);
                return _GetLipstickContext2;
            }
        }

        private POCOS.Lipstick _GetLipstickContext1;

        public POCOS.Lipstick GetLipstickContext1
        {
            get
            {
                if (_GetLipstickContext1 == null) _GetLipstickContext1 = new POCOS.Lipstick();
                return _GetLipstickContext1;
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
            if (_GetLipstickContext1 != null) _GetLipstickContext1.Dispose();
            if (_GetLipstickContext2 != null) _GetLipstickContext2.Dispose();
            base.OnLoadComplete(e);
        }
    }
}
     
    
