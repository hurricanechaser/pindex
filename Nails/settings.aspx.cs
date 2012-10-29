using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nails
{
    public partial class settings : PageHandlerBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Data.dbml.AppUsers o = GetDataContext2.AppUsers.Single(o1=>o1.ID== Common.UserID);
            aboutu.Value = o.About;
            email.Value = o.Email;
            first_name.Value = o.FirstName;
            username1.Value = o.Name;
            location.Value = o.Location;
            website.Value = o.Website;
            uploadedUserImage.Alt = strings.Image_1;
            if (!string.IsNullOrWhiteSpace(o.Avatar))            
                uploadedUserImage.Src = Common.UploadedImageRelPath + o.Avatar + "?width=170";
            uploadedUserImage.Style.Add("display", "block");
            usernameview.InnerHtml = Common.Domain  + o.Name;
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);

        }
    }
}