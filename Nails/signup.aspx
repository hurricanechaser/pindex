<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="Nails.signup" %>

<%@ Import Namespace="Nails" %>
<!DOCTYPE html>
<div id="settingscontainer" class="usersettings">
    <h3>
        <%=strings.Edit_Profile %></h3>
    <ul>
        <li>
            <label for="email">
                <%=strings.Email %></label>
            <div>
                <input type="text" readonly="true" id="email" name="email" runat="server" />
                <span>
                    <%=strings.Set_1 %></span>
            </div>
        </li>
        <li>
            <label for="username">
                <%=strings.UserName %></label>
            <div>
                <input type="text" id="username1" runat="server" name="username" />
                <span id="usernameview" runat="server"></span>
            </div>
        </li>
        <li>
            <label>
                <%=strings.Pass %></label>
            <div>
                <input type="password" id="pass2" /><span><%=strings.New_1 %>!</span>
            </div>
        </li>
        <li>
            <label>
                <%=strings.New_2%></label>
            <div>
                <input type="password" id="pass3" /><span> <%=strings.New_3 %>!</span>
            </div>
        </li>
        <li>
            <label for="first_name">
                <%=strings.Full_Name %></label>
            <div>
                <input type="text" id="first_name" runat="server" name="first_name" />
            </div>
        </li>
        <li>
            <label for="about">
                <%=strings.About %></label>
            <div>
                <textarea name="about" runat="server" cols="54" rows="3" id="aboutu"></textarea>
            </div>
        </li>
        <li>
            <label for="location">
                <%=strings.Location %></label>
            <div>
                <input type="text" id="location" runat="server" name="location" />
                <span>
                    <%=strings.Loc_Ex %></span>
            </div>
        </li>
        <li>
            <label for="website">
                <%=strings.Website %></label>
            <div>
                <input type="text" id="website" name="website" runat="server" />
            </div>
        </li>
        <li>
            <label for="id_img">
                <%=strings.Image %></label>
            <div class="up_image_div">
                <div>
                    <img src="http://cdn.pinjimu.com/img/load2.gif" alt="Loading..." />
                    <img id="uploadedUserImage" class="up_image_upload" runat="server" src="http://cdn.pinjimu.com/img/userImage.gif"
                        alt="" />
                </div>
                <div class="up_image_div2">
                    <%if (Request.Browser.IsBrowser("IE"))
                      { %>
                        <input type="file" id="ftsettings"  />
                    <% }
                      else
                      { %>
                    <input type="button" style="top: 0px; position: absolute; min-width: 170px; max-width: 170px;"
                        value= '<%=strings.Browse %>' />
                    <input type="file" id="ftsettings" style="filter: alpha(opacity=0); -moz-opacity: 0;
                        opacity: 0; cursor: pointer; top: 0px; position: absolute; width: 170px;" size="2" />
                        <% } %>
                </div>
            </div>
        </li>
        <!--   <li class="NoInput">
                <label>
                    Facebook</label>
                <div class="Right NoInput">
                    <label for="facebook_connect" class="large">
                        <input type="checkbox" checked="checked" id="facebook_connect">
                        Link to Facebook</label>
                </div>
            </li>
            <li class="NoInput">
                <label>
                    Twitter</label>
                <div class="Right NoInput">
                    <label for="twitter_connect" class="large">
                        <input type="checkbox" id="twitter_connect">
                        Link to Twitter</label>
                </div>
            </li>
            <li>
                <label for="id_dont_search_index">
                    Visibility</label>
                <div class="Right NoInput">
                    <label for="id_dont_search_index" class="large">
                        <input type="checkbox" id="id_dont_search_index" name="dont_search_index">
                        Hide your Pinterest profile from search engines</label>
                </div>
            </li>
                <li class="Delete">
                    <label>
                        Delete</label>
                    <div class="Right">
                        <a name="delete_user_account" id="delete_user_account"
                            href="javascript:void(0);"><strong>Delete Account</strong><span></span></a>
                    </div>
                </li>-->
    </ul>
    <div class="button">
        <a id="sp" href="javascript:void(0)"><span>
            <%=strings.Save_Profile %></span></a></div>
</div>
