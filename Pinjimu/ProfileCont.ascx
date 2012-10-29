<%@ Control Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="Pinjimu" %>
<script runat="server">
    string img = "", img1 = "", pi = "profile-info",name="";
    protected void Page_Load(object sender, EventArgs e)
    {
        Pinjimu.Data.Standalone.AppUsers user = null;
        if (Common.UserID.HasValue)
            user = Common.User;
        if (Common.VUserID.HasValue)
            user = Common.VUser;
        name = user.FirstName;
        if (user != null && !string.IsNullOrEmpty(user.Speciality))
        {
            switch (user.Speciality)
            {
                case "C":
                    img = "img/C-Icon.jpg";
                    img1 = "img/prof-Contractor-icon.jpg";
                    pi = "profile-info-Contractor";
                    break;
                case "D":
                    img = "img/D-Icon.jpg";
                    img1 = "img/prof-designer-icon.jpg";
                    pi = "profile-info-designer";
                    break;
                case "M":
                    img = "img/M-Icon.jpg";
                    img1 = "img/prof-FurnManu-icon.jpg";
                    pi = "profile-info-FurnManu";
                    break;
            }
        }
        else
            cSymbolCont.Style["display"] =
            cLogoCont.Style["display"] = "none";

    }
</script>
<div id="profileCont" class="Profile-Container-PJ" style="display: none">
    <div class="profile-Wrraper-PJ">
        <div id="profileWr" class="<%=pi %>">
            <div class="profile-pict">
                <img id="p_image_src" alt="Profile Pic" style="max-height: 135px;" />
            </div>
            <div class="profile-name">
                <span id="p_name">
                    <% =name%></span> <span id="cSymbolCont" runat="server">
                        <img name="cSymbol" src="<%=img %>" style="width: 20px; height: 20px;" alt="Symbol" />
                    </span>
            </div>
            <div class="prof-lable" id="cLogoCont" runat="server">
                <img id="cLogo" src="<%=img1 %>" style="width: 221px; height: 45px;" alt="Logo" />
            </div>
            <div class="profile-Desc" id="aboutcont">
                <textarea maxlength="200" id="jipeditable" style="display: none; width: 100%; overflow: visible;
                    max-height: 70px; max-width: 450px">
                    </textarea><span style="margin: 5px 0px 0px 0px; display: none;" id="EPCharCount">200</span>
                <span id="p_about" style="width: 100%"></span>&nbsp&nbsp<img id="p_edit_icon" src="http://cdn.pinjimu.com/img/ProfileEditIcon.png" />
            </div>
            <div class="editBtn">
                <button type="button" class="p_btn" name="save" onclick="javascript:$(location).attr('href', '#settings');">
                    <img src="http://cdn.pinjimu.com/img/pinjimu/Edit-btn-small.png" />
                    <%=strings.Edit_Profile %>
                </button>
            </div>
        </div>
    </div>
</div>
