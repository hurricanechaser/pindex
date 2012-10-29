using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;

namespace HairStyle
{
    public abstract class PageHandlerBase : System.Web.UI.Page
    {
        

        private HairStyle.dbml.HairStyleDataContext _GetHairStyleContext2;

        public HairStyle.dbml.HairStyleDataContext GetHairStyleContext2
        {
            get
            {
                if (_GetHairStyleContext2 == null) _GetHairStyleContext2 = new HairStyle.dbml.HairStyleDataContext(Common.HairStyleConnectionString);
                return _GetHairStyleContext2;
            }
        }

        private POCOS.HairStyle _GetHairStyleContext1;

        public POCOS.HairStyle GetHairStyleContext1
        {
            get
            {
                if (_GetHairStyleContext1 == null) _GetHairStyleContext1 = new POCOS.HairStyle();
                return _GetHairStyleContext1;
            }
        }
        protected override void OnLoadComplete(EventArgs e)
        {
            if (_GetHairStyleContext1 != null) _GetHairStyleContext1.Dispose();
            if (_GetHairStyleContext2 != null) _GetHairStyleContext2.Dispose();
            base.OnLoadComplete(e);
        }
    }
}
     
    
