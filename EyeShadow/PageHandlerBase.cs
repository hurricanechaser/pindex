using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;

namespace EyeShadow
{
    public abstract class PageHandlerBase : System.Web.UI.Page
    {
      
     

        private EyeShadow.dbml.EyeShadowDataContext _GetEyeShadowContext2;

        public EyeShadow.dbml.EyeShadowDataContext GetEyeShadowContext2
        {
            get
            {
                if (_GetEyeShadowContext2 == null) _GetEyeShadowContext2 = new EyeShadow.dbml.EyeShadowDataContext(Common.EyeShadowConnectionString);
                return _GetEyeShadowContext2;
            }
        }

        private POCOS.EyeShadow _GetEyeShadowContext1;

        public POCOS.EyeShadow GetEyeShadowContext1
        {
            get
            {
                if (_GetEyeShadowContext1 == null) _GetEyeShadowContext1 = new POCOS.EyeShadow();
                return _GetEyeShadowContext1;
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
            if (_GetEyeShadowContext1 != null) _GetEyeShadowContext1.Dispose();
            if (_GetEyeShadowContext2 != null) _GetEyeShadowContext2.Dispose();
            base.OnLoadComplete(e);
        }
    }
}
     
    
