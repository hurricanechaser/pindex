using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nails
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Common.RemoveValueinCookie(Common.InfoCookie, new string[]{
                "vuID" ,
                "vuemail" ,
                "vuname" ,
                "vuavatar" 
            });
            if (CookieUtil.CookieExists(Common.AuthCookie))
                Response.WriteFile("LoggedIn.html");
            else
                Response.WriteFile("home.html");
            Response.End();
        }
    }
}