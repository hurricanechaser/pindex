using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairStyle
{
    public partial class settings : PageHandlerBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            POCOS.AppUser o = POCOS.AppUser.Single("Select * from AppUsers where ID=@0", Common.UserID);
            aboutu.Value= o.About;
            email.Value = o.Email;
            first_name.Value = o.FirstName;
            username1.Value = o.Name;
            location.Value = o.Location;
            website.Value = o.Website;
            uploadedUserImage.Src = Common.UploadedImageRelPath+o.Avatar + "?width=170";
            usernameview.InnerHtml = Common.Domain +"users/"+ o.Name;
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        }
    }
}