<%@ Page Language="C#" AutoEventWireup="true" Inherits="Pinjimu.PageHandlerBase" %>
<%@ Import Namespace="System.Data.Linq" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="Newtonsoft.Json" %>
<%@ Import Namespace="Newtonsoft.Json.Linq" %>
<%@ Import Namespace="Pinjimu" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        Dictionary<string, object> _dict = Common.CookieValue;
        if (!_dict.ContainsKey(Common.InfoCookie))
        {
            NameValueCollection info = new NameValueCollection();
            info.Add("id", Common.GetHash(Guid.NewGuid().ToString()));
            _dict[Common.InfoCookie] = info;
        }
        if (_dict.ContainsKey(Common.AuthCookie))
        {
            Page.Controls.Add(Page.LoadControl("LoggedIn.ascx"));
        }
        else
        {
            Page.Controls.Add(Page.LoadControl("home.ascx"));
        }

    }
</script>
