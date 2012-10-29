using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;


namespace Lipstick
{
    public partial class Pin : PageHandlerBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string pin = Request.QueryString["pin"];
            var imageUrl = (from o in this.GetLipstickContext2.Vw_Images
                            where o.PinID.Value.ToString() == pin
                            select new
                            {
                                title = o.Image_Title,
                                //imgsource = o.Image_Source_URL,
                                //source = Common.GetAbsoluteUri(o.Image_Source_URL),
                                cats = (from o1 in this.GetLipstickContext2.Vw_Cat where o1.ID == o.ID select o1.Name).Distinct().FirstOrDefault(),
                                url = ((o.Uploaded ?? false) ? Common.UploadedImageRelPath : Common.ContentUrl) + "/" + o.RelativeImage_Path,
                                PinID = o.CRC64
                            }).First();
            pinCloseupImage.Src = imageUrl.url;
            PinCaption.InnerHtml = imageUrl.title;
            //source.HRef = imageUrl.imgsource;
            //source.InnerHtml = imageUrl.source;
            cat.HRef = string.Format("home.html?cat={0}", imageUrl.cats, Request.Url.AbsoluteUri);
            cat.InnerHtml = imageUrl.cats;
            pinit.HRef = Uri.EscapeUriString("http://pinterest.com/pin/create/bookmarklet/?media=" + Path.Combine(Common.ContentUrl, imageUrl.url) + "&url=Pin.aspx?pin=" + imageUrl.PinID + "&alt=" + imageUrl.title + "&title=" + imageUrl.title + "&is_video=false");
            grabit.HRef = Uri.EscapeUriString("http://lockerz.com/grab/?media=" + Path.Combine(Common.ContentUrl, imageUrl.url) + "&url=Pin.aspx?pin=" + imageUrl.PinID + "&alt=" + imageUrl.title);

        }
    }
}
