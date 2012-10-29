<%@ Control Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="Nails" %>
<script runat="server">
    private string pre;
    protected void Page_Init(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        followall.Visible = false;
        Common.RemoveValueinCookie(Common.InfoCookie, new string[]{
                "vuID" ,
                "vuemail" ,
                "vuname" ,
                "vuavatar",
                "vuboards",
                "vupins", 
                "vulikes",
                "vupoints",
                "vufollower",
                "vufollowing"
            });
    }
    public string getAV(string avatar)
    {
        return string.IsNullOrEmpty(avatar) ? Common.CDN + Common.UserBlankImg : avatar;
    }
</script>
<!DOCTYPE html>
<html>
<head>
    <title>PinPolish /
        <%= strings.Home %></title>
    <meta http-equiv="Content-Type" content="text/html">
    <link rel="icon" href="favicon.ico" />
    <% if (Request.Browser.IsBrowser("IE") && Request.Browser.MajorVersion == 7)
       { %>
    <link href="http://cdn.pinpolish.com/nails_IE7.css" rel="stylesheet"
        type="text/css" />
    <% }
       else
       { %>
    <link href="http://cdn.pinpolish.com/nails.css" rel="stylesheet" type="text/css" />
    <% } %>
    <link href='http://fonts.googleapis.com/css?family=Chivo:400,900' rel='stylesheet'
        type='text/css' />
         <script type="text/javascript">
             var _gaq = _gaq || [];
             _gaq.push(['_setAccount', 'UA-31949192-1']);
             (function () {
                 var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
                 ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
                 var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
             })();
    </script>
    <script id="_webengage_script_tag" type="text/javascript">
        window.webengageWidgetInit = window.webengageWidgetInit || function () {
            webengage.init({
                licenseCode: "~71680a6b"
            }).onReady(function () {
                webengage.render();
            });
        };
        (function (d) {
            var _we = d.createElement('script');
            _we.type = 'text/javascript';
            _we.async = true;
            _we.src = (d.location.protocol == 'https:' ? "//ssl.widgets.webengage.com" : "//cdn.widgets.webengage.com") + "/js/widget/webengage-min-v-3.0.js";
            var _sNode = d.getElementById('_webengage_script_tag');
            _sNode.parentNode.insertBefore(_we, _sNode);
        })(document);
    </script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
    <script src="freshpin.js" type="text/javascript"></script>
    <script src="freshpinhome.js" type="text/javascript"></script>
    <script src="freshpinloggedin.js" type="text/javascript"></script>
    <script src="http://cdn.pinpolish.com/build/files/homeincludes.js"
        type="text/javascript"></script>
    <script src="http://cdn.pinpolish.com/build/files/loggedinincludes.js"
        type="text/javascript"></script>
    <script id="boards" type="text/x-jquery-tmpl">
    <div class="pinBoard">
        <h3>
            <a href="#board=${name}" >${name}</a></h3>
        <h4>
            ${pins} <%=strings.Pins %></h4>
        <div class="board">          
          {{each images}}            
             {{if $index == 0}} 
                <img src="${getVal($value,'url')}?width=222" style="height:${getHeight($value,222)}px" />
             {{else}}
                <span><img src="${getVal($value,'url')}?width=55" style="height:${getHeight($value,55)}px"  /></span>
             {{/if}}
          {{/each}}
            <div style="clear: both;">
            </div>
            {{if !FreshPin.visited()  }}
            <div class="boardfooter" style="cursor:pointer;" name="edit" boardid="${id}">
                <strong><%=strings.Edit %></strong></div>
            {{/if}}
        </div>
    </div>
    </script>
    <script id="articles" type="text/x-jquery-tmpl">
    <div class="containerBox">
        <a href="${source}" target="_blank" style="text-decoration: none;"><h2>${title} </h2></a>
        <div class="desc">${desc}</div>
        <div class="img-cont"><a href="${source}" target="_blank"><img src="${url}?width=340" style="width:340px;height:${height}px" /></a></div>
    </div>
    </script>
    <script id="stores" type="text/x-jquery-tmpl">
    <ul class="box" style="width:190px;">
        <li class="img" style="width:190px;height:${height}px"><a href="${source}" target="_blank">
            <img src="${url}?width=190" style="width:190px;height:${height}px" alt="${title}" /></a>
        </li>
        <li><div class="name"><span>${title}</span></div></li>
    </ul>
    </script>
    <script id="picTemplate" type="text/x-jquery-tmpl">
        <ul class="box" style="width:${getWidth()}px" >
        {{if FreshPin.authenticated() == true  }}       
            <li class="buttons" >
                {{if editable }}
                <a href="javascript:void(0)" imageid="${ID}" bimid="${BIMID}" class="button" name="edit" ><%=strings.Edit %></a>
                {{else}}
                <a href="javascript:void(0)" imageid="${ID}" bimid="${BIMID}" name="like"  class="buttonLike{{if liked}} buttonLikeDis{{/if}}">${strings.Like}</a>
                {{/if}}
                <a href="javascript:void(0)" imageid="${ID}" bimid="${BIMID}" url="${url}?width=190" name="pin" class="buttonPin">${strings.Repin}</a>
                <a href="javascript:void(0)" imageid="${ID}"  bimid="${BIMID}" name="comment" class="buttonComment">${strings.Comment}</a>
            </li>
             {{/if}}
            {{if FreshPin.authenticated() == false && FreshPin.visited() == true }}              
            <li class="buttons"><a name="buttons" class="buttonLike" href="javascript:void(0)">${strings.Like}</a> <a class="buttonPin" name="buttons"
                                                                                href="javascript:void(0)">${strings.Repin}</a> <a name="buttons" class="buttonComment" href="javascript:void(0)">${strings.Comment}</a>
            </li>
              {{/if}}
            <li class="img" style="height:${getHeight($data)}px"><a href="#pin=${PinID}"> 
                                <img src="${url}?width=${getWidth()}" style="height:${getHeight($data)}px;width:${getWidth()};"  /></a>
            </li>         
             <li class="boardsPinBoards">
           <div class="post-pin">
                <div class="post-text"><%=strings.Post_This_To %></div>
                <div class="post-icons">
                 &nbsp;<img href="${getUrl($data,'p')}" class="pinit" src="http://cdn.pinpolish.com/img/nails/pinterest_logo.png" width="24" height="24" title="Pinterest" border="0" />&nbsp; 
                <img  class="pinit" src="http://cdn.pinpolish.com/img/nails/facebook_logo.png" width="24" height="24" title="Facebook" border="0" />&nbsp; 
                <img  class="pinit" src="http://cdn.pinpolish.com/img/nails/tumblr_logo.png" width="24" height="24" title="Tumblr" border="0" />
                </div>
           </div>
        </li>
        <li> <div class="name"><span>${title}</span></div> </li>
         {{if comments != null}}
        <li class="comments">
        {{tmpl(comments) "#comments"}}       
        </li>
        {{/if}}
    </ul>
    </script>
    <script id="cats" type="text/x-jquery-tmpl">
   <a href="#cat=${escape(getVal($data,'Name'))}" style="overflow:hidden;" >${Name}</a>   
    </script>
    <script id="comments" type="text/x-jquery-tmpl">
     <div class="commentBox">
      <a href="${UN}" ><img src="${getUplUImg($data)}?height=20" /></a>  <p> : <a href="${UN}" style="float:left;text-align:left;" >${Name}</a> ${Comment}</p> 
     </div>
    </script>
    <script id="cattpl" type="text/x-jquery-tmpl">
    <li catid="${ID}" index="${index}"><a href="javascript:void(0)" >${Name}</a></li>
    </script>
    <script id="boardstpl" type="text/x-jquery-tmpl">
    <li boardid="${ID}" style="cursor:pointer;" index="${index}">${Name}</li>
    </script>
    <script id="subcattpl" type="text/x-jquery-tmpl"> 
        <ul class="subNav">
            <li catid="${ID}" index="${index}"><a href="javascript:void(0)" >${Name}</a></li>
        </ul>
    </script>
    <script id="ebboardCollaborators" type="text/x-jquery-tmpl"> 
    <div name="cont"  >
    <a href="${getUD($data)}" style="font-weight: bold; color: #221919;text-decoration: none; outline: none; float: left; font-size: inherit; height: 36px;width: 36px;">
    <img src="${getUplUImg($data)}?width=36" style="display: block; width: 36px !important; height: auto !important;"/></a>
    <a href="${getUD($data)}" style="font-weight: bold; color: #221919;text-decoration: none; outline: none; float: left; font-size: inherit; width: 198px;overflow: hidden; white-space: nowrap; text-overflow: ellipsis; margin-top: 7px; margin-right: 18px; margin-bottom: 0; margin-left: 10px;">${Name}</a>
    <a name="rem" un="${UN}"  style="font-weight: bold;color: #524d4d; text-decoration: none; outline: none; position: relative; display: inline-block;
     text-align: center; line-height: 1em; border-radius: 6px; -moz-border-radius: 6px;
     -webkit-border-radius: 6px; -moz-transition-property: color,
     -moz-box-shadow, text-shadow; -moz-transition-duration: .05s; -moz-transition-timing-function: ease-in-out; -webkit-transition-property: color, -webkit-box-shadow, text-shadow;
     -webkit-transition-duration: .05s; -webkit-transition-timing-function: ease-in-out;
     box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
     -moz-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
     -webkit-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
     cursor: pointer; font-size: 18px; text-shadow: 0 1px rgba(255,255,255,0.9); float: left;
     margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 10px; padding-top: .45em;
     padding-right: .825em; padding-bottom: .45em; padding-left: .825em; border-top-color: transparent;
     border-right-color: transparent; border-bottom-color: transparent; border-left-color: transparent;
     border-top-style: solid; border-right-style: solid; border-bottom-style: solid;
     border-left-style: solid; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px;
     border-left-width: 1px;" ><strong style="font-style: normal; font-weight: bold; position: relative;
    "><%=strings.Remove%></strong><span style="position: absolute; z-index: 1; top: -1px;
    right: -1px; bottom: -1px; left: -1px; display: block; opacity: 1; border-radius: 6px;
    -moz-border-radius: 6px; -webkit-border-radius: 6px; box-shadow: inset 0 1px rgba(255,255,255,0.35);
    -moz-box-shadow: inset 0 1px rgba(255,255,255,0.35); -webkit-box-shadow: inset 0 1px rgba(255,255,255,0.35); -moz-transition-property: opacity; -moz-transition-duration: 0.5s;-moz-transition-timing-function: ease-in-out; -webkit-transition-property: opacity;
    -webkit-transition-duration: 0.5s; -webkit-transition-timing-function: ease-in-out;
    font-size: 18px; filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfafb', endColorstr='#f0eded');
    border-top-color: #bbb; border-right-color: #bbb; border-bottom-color: #bbb;
    border-left-color: #bbb; border-top-style: solid; border-right-style: solid;
    border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
    border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px;
    background-position: 0%
    0%;"></span></a>
    <div style="clear:both;" />
    </div>
    </script>
    <script id="usertpl" type="text/x-jquery-tmpl">
        <div id="following" class="Following_Container">
            <div class="Profile-pict"><img id="fbtn_img_p_${F_ID}" src="${getAvatar($data,'F_Avatar')}?width=100&height=100"  /></div>
            <div style="border:0px solid red; width:320px; float:left;">
                <div style="float:left">
                    <h4 class="link" style="margin-top:10px;"><a href="${F_UserName}#boards">${F_FullName}</a></h4>
                </div>
                <div style="float:right; margin-top:10px;">
                    <div class="unfolwBtn">
                    <button type="button" class="p_btn" name="save" onclick="javascript:FollowUnFollowUser('${F_ID}','fbtn_img_${F_ID}','fbtn_text_${F_ID}');">     
                        {{if F_Status}}
                            <img id="fbtn_img_${F_ID}" src=${GetFImage('Unfollow')} />
                            <span id="fbtn_text_${F_ID}" ><%=strings.UnFollow %></span>
                        {{else}}
                            <img id="fbtn_img_${F_ID}" src=${GetFImage('follow')} />
                            <span id="fbtn_text_${F_ID}" ><%=strings.Follow %></span>
                        {{/if}}
                    </button>
                </div>
                </div>
                <div class="clear"></div>
                <div style="float:left">
                <h5>
                    <a>${Fg_Count} <%=strings.Following %> </a>
                    <a>${Fr_Count} <%=strings.Follower %> </a>
                </h5>
                </div>
                <div class="emptySpace"></div>
            </div>
            <div>
            {{each F_Pin}}
                <a class="thumbnails"><img src="${getVal($value, 'RelativeImage_Path')}?height=60" /></a>
            {{/each}}            
            </div>
        </div>
    </script>
    <script type="text/javascript">
        strings = {
            Boards: '<%=strings.Boards%>',
            Pins: '<%=strings.Pins%>',
            Like: '<%=strings.Like%>',
            Settings: '<%=strings.Settings%>',
            Log_Out: '<%=strings.Log_Out%>',
            Login: '<%=strings.Login%>',
            Reset: '<%=strings.Reset%>',
            Points: '<%=strings.Points%>',
            Repin: '<%=strings.Repin%>',
            Comment: '<%=strings.Comment %>',
            Email: '<%=strings.Email %>',
            Email_UN: '<%=strings.Email_UN %>',
            InvalidEmail: '<%=strings.InvalidEmail %>',
            Login_Error: '<%=strings.Login_Error %>',
            Login_Back: '<%=strings.Login_Back %>',
            Pass_Forgot: '<%=strings.Pass_Forgot %>',
            Search_Slogan: '<% =strings.Search_Slogan %>',
            Pin_Alert_Title: '<% =strings.Pin_Alert_Title %>',
            Search_Alert_1: '<%=strings.Search_Alert_1 %>',
            Search_Alert_2: '<%=strings.Search_Alert_2 %>',
            Edit: '<%=strings.Edit %>',
            Enter_Board_Name: '<%=strings.Enter_Board_Name %>',
            Select_Category: '<%=strings.Select_Category %>',
            Contributor_Add: '<%=strings.Contributor_Add %>',
            Contributor_Warn: '<%=strings.Contributor_Warn %>',
            Contributor_Usr_Warn: '<%=strings.Contributor_Usr_Warn %>',
            Board_Success: '<%=strings.Board_Success %>',
            Following: '<%=strings.Following %>',
            Follower: '<%=strings.Follower %>',
            UnFollow: '<%=strings.UnFollow %>',
            Follow: '<%=strings.Follow %>',
            Follow_Me: '<%=strings.Follow_Me %>',
            UnFollow_Me: '<%=strings.UnFollow_Me %>',
            All: '<%=strings.All %>',
            Password_New: '<%=strings.Password_New %>',
            Pass: '<%=strings.Pass %>',
            Fields_Red: '<%=strings.Fields_Red %>',
            Password_Mismatch: '<%=strings.Password_Mismatch %>',
            Enter_User_Name: '<%=strings.Enter_User_Name %>',
            Enter_Full_Name: '<%=strings.Enter_Full_Name %>',
            UserName_Alert: '<%=strings.UserName_Alert %>',
            Cat_Select: '<%=strings.Cat_Select%>',
            Board_Blank: '<%=strings.Board_Blank %>',
            Board_Update_Msg: '<%=strings.Board_Update_Msg %>',
            Board_First: '<%=strings.Board_First %>',
            Comment_Enter: '<%=strings.Comment_Enter %>',
            Pin_Upload_Msg: '<%=strings.Pin_Upload_Msg %>',
            Pin_Add_Msg: '<%=strings.Pin_Add_Msg %>',
            Pin_Del_Msg: '<%=strings.Pin_Del_Msg %>',
            Image_Invalid: '<%=strings.Image_Invalid %>',
            Image_Success_Upload: '<%=strings.Image_Success_Upload %>',
            Image_Success_Added: '<%=strings.Image_Success_Added %>',
            File_Fail: '<%=strings.File_Fail %>',
            Cat_DD: '<%=strings.Cat_DD %>',
            Edit_Board: '<%=strings.Edit_Board %>',
            Update_Board: '<%=strings.Update_Board %>',
            Create_Board: '<%=strings.Create_Board %>',
            Save_Board: '<%=strings.Save_Board %>',
            Del_Board_Confirm: '<%=strings.Del_Board_Confirm %>',
            Remove: '<%=strings.Remove %>'
        };
        FreshPin.constants.cdn = '<%=Common.CDN%>';
        FreshPin.constants.domain = '<%=Common.Domain%>';
        FreshPin.constants.upl = '<%=Common.UploadedImageRelPath%>';
        FreshPin.constants.authcookie = '<%=Common.AuthCookie%>';
        FreshPin.constants.infocookie = '<%=Common.InfoCookie%>';
    </script>
</head>
<body>
    <div class="header" id="header">
        <div class="line1">
            <div class="queryBox">
                <input class="searchfield" type="text" placeholder="<% =strings.Search_Slogan %>"
                    id="query" />
                <input class="searchbutton" type="button" id="query_button" />
                <input class="scrollbutton" type="button" id="scroll_button" />
            </div>
            <div class="freshpinlogo">
                <a href="." name="noblock">
                    <img src="logo.png" /></a>
                <div class="freshpinlogoTagline">
                    <% =strings.Banner %></div>
            </div>
            <div style="float: right; position: absolute; margin-right: 25px; top: 5; right: 0;">
                <div class="aboutus">
                    <ul class="navigation">
                        <li><span class="nav" id="addtrigger" style="cursor: pointer;">
                            <%=strings.Add %>+</span> </li>
                        <li><a id="about" name="noblock" href="about" class="nav">
                            <% =strings.About %><span></span></a>
                            <ul>
                                <li><a name="noblock" href="about#help">
                                    <%=strings.Help %></a></li>
                                <li class="beforeDivider"><a name="noblock" href="about#copyright">
                                    <% =strings.Copyright %></a></li>
                                <li><a href="javascript:void(0);" id="reqin" style="display: none">
                                    <%=strings.Register%></a></li>
                            </ul>
                        </li>
                        <li id="logininfo" class="logininfo"><a class="nav" style="padding: 20px 27px 11px 40px;"
                            href="<%= pre %>#boards"><span class="userProfilePic">
                                <img id="userProfilePic" src="<%= getAV(Common.ReadValue("avatar")) %>?width=20" />
                            </span>
                            <p id="username">
                                <% =Server.UrlDecode(Common.ReadValue("name"))%>
                            </p>
                            <span></span></a>
                            <ul>
                                <li><a href="#boards">
                                    <%=strings.Boards %></a></li><li class="beforeDivider"><a href="<%= pre %>#filter=pins">
                                        <%=strings.Pins %></a></li><li class="divider"><a href="<%= pre %>#filter=likes">
                                            <%=strings.Likes %></a></li><li><a href="<%= pre %>#settings">
                                                <%=strings.Settings %></a></li><li><a id="logout" href="javascript:void(0);">
                                                    <%=strings.Log_Out %></a></li></ul>
                            <script type="text/javascript">
                                $("#logout").click(function () {
                                    $.post('POST?t=logout', function () {
                                        $(window.location).attr('href', FreshPin.constants.logoutUrl);
                                    });
                                });
                            </script>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="line2">
            <div class="ImageSizes">
                <ul>
                    <li id='S' name="sizeEl"><a href="javascript:h('size','S');"><span>
                        <%=strings.S %></span></a></li>
                    <li id='M' name="sizeEl"><a href="javascript:h('size','M');"><span>
                        <%=strings.M %></span></a></li>
                    <li id='L' name="sizeEl"><a href="javascript:h('size','L');"><span>
                        <%=strings.L %></span></a></li>
                </ul>
            </div>
            <div class="menu3">
                <div style="margin: 0 auto; border: 0px solid red; width: 800px;">
                    <ul class="menu">
                        <li><a href="#articles">
                            <% =cat.Articles %></a><span class="bgNone"></span></li>
                        <li id="mlevel1" class="menu"><a href=".">
                            <%=cat.Styles %></a><span></span></li>
                        <li class="menu"><a href="#stores">
                            <%=cat.Stores%></a><span class="bgNone"></span></li>
                        <li class="menu"><a href="javascript:void(0)" id="earnpoints">
                            <%=cat.Points %></a><span class="bgNone"></span></li>
                    </ul>
                </div>
            </div>
            <div style="float: right; margin-right: 25PX; position: absolute; top: 0; right: 0;">
                <div class="user-points" id="p_points">
                </div>
                <div style="float: right; margin-top: 13px; font-size: 12px;" class="localization"
                    id="mlevel2">
                    <a href="javascript:void(0)">
                        <img src="http://cdn.pinpolish.com/img/famfamfam_flag_icons/png/<%=Common.CurrentLocalizationInfo.Flag %>.png" />
                        <%=Common.CurrentLocalizationInfo.DisplayName %></a>
                </div>
            </div>
        </div>
        <div class="submenu" id="submenu1" style="display: none">
            <div>
                <a class="none" href="javascript:h('cat','Color');"><strong>
                    <%=cat.Color %></strong></a>
                <br />
                <a href="javascript:h('cat','Multi (3+)');">
                    <%=cat.Multi %></a> <a href="javascript:h('cat','Two-Tone');">
                        <%=cat.Two_Tone %></a> <a href="javascript:h('cat','Metallic');">
                            <%=cat.Metallic %></a> <a href="javascript:h('cat','Red');">
                                <%=cat.Red %></a> <a href="javascript:h('cat','Orange');">
                                    <%=cat.Orange %></a> <a href="javascript:h('cat','Yellow');">
                                        <%=cat.Yellow %></a> <a href="javascript:h('cat','Green');">
                                            <%=cat.Green %></a> <a href="javascript:h('cat','Teal');">
                                                <%=cat.Teal %></a> <a href="javascript:h('cat','Blue');">
                                                    <%=cat.Blue %></a> <a href="javascript:h('cat','Purple');">
                                                        <%=cat.Purple %></a> <a href="javascript:h('cat','Pink');">
                                                            <%=cat.Pink %></a> <a href="javascript:h('cat','White');">
                                                                <%=cat.White %></a> <a href="javascript:h('cat','Gray');">
                                                                    <%=cat.Gray %></a> <a href="javascript:h('cat','Black');">
                                                                        <%=cat.Black %></a> <a href="javascript:h('cat','Brown');">
                                                                            <%=cat.Brown %></a>
                <br />
                <a class="none" href="javascript:h('cat','Finish');"><strong>
                    <%=cat.Finish %></strong></a><br />
                <a href="javascript:h('cat','Shiny');">
                    <%=cat.Shiny %></a> <a href="javascript:h('cat','Matte');">
                        <%=cat.Matte %></a> <a href="javascript:h('cat','Glitter');">
                            <%=cat.Glitter %></a>
                <br />
                <a class="none" href="javascript:h('cat','Difficulty');"><strong>
                    <%=cat.Difficulty %></strong></a><br />
                <a href="javascript:h('cat','Simple');">
                    <%=cat.Simple %></a> <a href="javascript:h('cat','Medium');">
                        <%=cat.Medium %></a> <a href="javascript:h('cat','Hard');">
                            <%=cat.Hard %></a> <a href="javascript:h('cat','Crazy');">
                                <%=cat.Crazy %></a>
                <br />
                <a class="none" href="javascript:h('cat','Style');"><strong>
                    <%=cat.Style %></strong></a>
                <br />
                <a href="javascript:h('cat','Pattern');">
                    <%=cat.Pattern %></a> <a href="javascript:h('cat','Picture');">
                        <%=cat.Picture %></a> <a href="javascript:h('cat','Gradient');">
                            <%=cat.Gradient %></a> <a href="javascript:h('cat','3D');">
                                <%=cat.ThreeD %></a> <a href="javascript:h('cat','Airbrush');">
                                    <%=cat.Airbrush %></a> <a href="javascript:h('cat','FreeStyle');">
                                        <%=cat.FreeStyle %></a> <a href="javascript:h('cat','Stamp');">
                                            <%=cat.Stamp %></a> <a href="javascript:h('cat','Stilleto');">
                                                <%=cat.Stilleto %></a>
                <br />
                <a class="none" href="javascript:h('cat','With People');"><strong>
                    <%=cat.With_People %></strong></a><br />
                <a href="javascript:h('cat','Face');">
                    <%=cat.Face %></a> <a href="javascript:h('cat','Outfit');">
                        <%=cat.Outfit %></a> <a href="javascript:h('cat','Pedicures');">
                            <%=cat.Pedicures %></a> <a href="javascript:h('cat','Accessories');">
                                <%=cat.Accessories %></a>
                <br />
                <a class="none" href="javascript:h('cat','How-To');"><strong>
                    <%=cat.How_To %></strong></a><br />
                <a href="javascript:h('cat','HowToGallery');">
                    <%=cat.How_To_Gallery %></a>
                <br />
                <a class="none" href="javascript:h('cat','Products');"><strong>
                    <%=cat.Products %></strong></a><br />
                <a href="javascript:h('cat','Nail Polish');">
                    <%=cat.Nail_Polish %></a> <a href="javascript:h('cat','Pattern Tools');">
                        <%=cat.Pattern_Tools %></a> <a href="javascript:h('cat','Manicure');">
                            <%=cat.Manicure %></a>
                <br />
            </div>
        </div>
        <div class="submenu" id="submenu2" style="display: none">
            <div>
                <%foreach (Common.LocalizationInfo linfo in Common.CurrentLocalizationCultures)
                  {%>
                <a href="javascript:FreshPin.writeCookieValue('culture','<%=linfo.CultureInfo.IetfLanguageTag%>');location.reload(true);">
                    <img src="http://cdn.pinpolish.com/img/famfamfam_flag_icons/png/<%=linfo.Flag%>.png" />
                    <%=linfo.DisplayName%></a>
                <br />
                <%}%>
            </div>
        </div>
    </div>
    <div id="sautocomplete" class="sAutoComplete" style="display: none; position: fixed;
        top: 35px; left: 20px;">
        <input name="st" id="rbpins" type="radio" value="pins"><label for="rbpins" style="font-family: 'Arial',arial,sans-serif;
            font-size: 13px; font-weight: bold;"><%=strings.Pins %></label>
        <input name="st" id="rbboards" type="radio" value="boards" /><label for="rbboards"
            style="font-family: 'Arial',arial,sans-serif; font-size: 13px; font-weight: bold;"><%=strings.Boards %></label>
        <input name="st" id="rbpeople" type="radio" value="people" /><label for="rbpeople"
            style="font-family: 'Arial',arial,sans-serif; font-size: 13px; font-weight: bold;"><%=strings.People %></label>
    </div>
    <%--Profile Page Start--%>
    <div id="profileCont" class="Profile-Container-PJ" style="display: none">
        <div class="profile-Wrraper-PJ">
            <div class="profile-info">
                <div class="profile-pict">
                    <img id="p_image_src" alt="Profile Pic" style="max-height: 135px;width:116px;" />
                </div>
                <span class="profile-name" id="p_name">
                    <% =Server.UrlDecode(Common.ReadValue("name"))%>
                </span>
                <div class="profile-Desc" id="aboutcont">
                    <textarea maxlength="200" id="jipeditable" style="display: none; width: 100%; overflow: visible;
                        max-height: 70px; max-width: 450px">
                    </textarea><span style="margin: 5px 0px 0px 0px; display: none;" id="EPCharCount">200</span>
                    <span id="p_about" style="width: 100%"></span>&nbsp&nbsp<img id="p_edit_icon" src="http://cdn.pinpolish.com/img/ProfileEditIcon.png" />
                </div>
                <div class="editBtn">
                    <button type="button" class="p_btn" name="save" onclick="javascript:$(location).attr('href', '#settings');">
                        <img src="http://cdn.pinpolish.com/img/nails/Edit-btn-small.png" />
                        <%=strings.Edit_Profile %>
                    </button>
                </div>
            </div>
        </div>
    </div>
    <div id="ContextBar" class="profile-row2" style="display: none">
        <div class="row2-wrapper">
            <div class="row2Left">
                <div class="btn">
                    <button id="p_boards" name="p_btn" type="button" class="p_btn" onclick="javascript:$(location).attr('href', '#boards');">
                        <img src="http://cdn.pinpolish.com/img/nails/Idea-Icons-Small.png" />
                        <span class="text-btn" id="p_board_cnt"></span>
                        <%=strings.IdeaBoards %>
                    </button>
                </div>
                <div class="btn">
                    <button id="p_pins" name="p_btn" type="button" class="p_btn" onclick="javascript:$(location).attr('href', '#filter=pins');">
                        <img src="http://cdn.pinpolish.com/img/nails/pin3-Icon-Small.png" />
                        <span class="text-btn" id="p_pins_cnt"></span>
                        <%=strings.Pins %>
                    </button>
                </div>
                <div class="btn">
                    <button id="p_likes" name="p_btn" type="button" class="p_btn" onclick="javascript:$(location).attr('href', '#filter=likes');">
                        <img src="http://cdn.pinpolish.com/img/nails/like2-icon-small.png" />
                        <span class="text-btn" id="p_likes_cnt"></span>
                        <%=strings.Likes %>
                    </button>
                </div>
            </div>
            <div class="followdiv">
                <div class="followall" id="followall" runat="server">
                    <button class="p_btn" name="p_btn" name="save" id="followallbtn" onclick="javascript:FollowUnFollowUser('0','followallimg','followallspan');"
                        style="display: none">
                        <img src="" id="followallimg" />
                        <span id="followallspan"></span>
                    </button>
                </div>
                <div class="btn">
                    <button id="p_followings" name="p_btn" type="button" class="p_btn" onclick="javascript:$(location).attr('href', '#followings');">
                        <span class="text-btn" id="p_followings_cnt"></span>
                        <%=strings.Followings %>
                    </button>
                </div>
                <div class="btn">
                    <button id="p_followers" name="p_btn" type="button" class="p_btn" onclick="javascript:$(location).attr('href', '#followers');">
                        <span class="text-btn" id="p_followers_cnt"></span>
                        <%=strings.Followers %>
                    </button>
                </div>
            </div>
        </div>
    </div>
    <%--Profile Page End--%>
    <%--Roulette Wheel Start --%>
    <%--<div class="rouletteWheel-Container" id="rwd">
        <div class="close">
            <img src="http://cdn.pinpolish.com/img/close-rw.jpg" id="rwclose"
                style="cursor: pointer;" />
        </div>
        <div class="wheel">
            <img id="rw" src="http://cdn.pinpolish.com/img/nails/lottery-wheel-3.png"
                style="-moz-transform: rotate(0 deg);" /></div>
        <div class="pointer">
            <img src="http://cdn.pinpolish.com/img/nails/pointer.png" />
        </div>
        <div class="spin-btn" id="rwt">
            <div class="text">
                <%=strings.Spin %>
                &
                <%=strings.Go %>
                !</div>
            <img style="cursor: pointer;" src="http://cdn.pinpolish.com/img/nails/Spin-btn2.png"
                id="" /></div>
    </div>--%>
    <%--Roulette Wheel End --%>
    <%--Points Page Start --%>
    <div class="Points-Wrraper-PJ" id="earnPointsCriteria" style="display: none;">
        <div class="points-title">
            <%=strings.Ways_to_Earn %></div>
        <div class="point-desc">
            <%=strings.PNTS %>
        </div>
        <div class="clear">
        </div>
        <div class="clear">
        </div>
        <div class="point-btn-container">
            <div class="point-btn-bg">
                + 100</div>
            <div class="point-ways">
                <%=strings.Point_SignUp %></div>
        </div>
        <div class="point-btn-container">
            <div class="point-btn-bg">
                + 50</div>
            <div class="point-ways">
                <%=strings.Point_Nib %></div>
        </div>
        <div class="point-btn-container">
            <div class="point-btn-bg">
                + 10</div>
            <div class="point-ways">
                <%=strings.Point_Pin %></div>
        </div>
        <div class="point-btn-container">
            <div class="point-btn-bg">
                + 5</div>
            <div class="clear">
            </div>
            <div class="point-ways">
                <%=strings.Point_RePin %></div>
        </div>
        <div class="clear">
        </div>
        <%--<div class="point-lable">
            <img src="http://cdn.pinpolish.com/img/nails/points-bar.jpg" width="650"
                height="36" /></div>
        <div class="points-title">
            <%=strings.Ways_to_Redeem %></div>
        <div class="RedeemContianer">
            <div class="RedeemBox">
                <div class="RedeemIcon">
                    <div class="icon">
                        <img src="http://cdn.pinpolish.com/img/nails/donate-icon.png" /></div>
                    <div class="RedeemsPoints">
                        500</div>
                </div>
                <div class="Redeem-text">
                    <div class="title">
                        <%=strings.Donate %></div>
                    <div class="Redeemdesc">
                        <%=strings.Donate_Book %></div>
                </div>
                <div style="float: left;">
                    <input type="button" class="styled-button-1" value="Submit" />
                </div>
            </div>
            <div class="RedeemBox">
                <div class="RedeemIcon">
                    <div class="icon">
                        <img src="http://cdn.pinpolish.com/img/nails/gift-icon.png" />
                    </div>
                    <div class="RedeemsPoints">
                        500</div>
                </div>
                <div class="Redeem-text">
                    <div class="title">
                        <%=strings.Get %></div>
                    <div class="Redeemdesc" style="">
                        <%=strings.Get_Magazine %></div>
                </div>
                <div style="float: left;">
                    <input type="button" class="styled-button-1" value="Submit" />
                </div>
            </div>
            <div class="RedeemBox">
                <div class="RedeemIcon">
                    <div class="icon">
                        <img src="http://cdn.pinpolish.com/img/nails/roullete-icon2.png" /></div>
                    <div class="RedeemsPoints">
                        500</div>
                </div>
                <div class="Redeem-text">
                    <div class="title">
                        <%=strings.Win %></div>
                    <div class="Redeemdesc">
                        <%=strings.Win_IPhone %></div>
                </div>
                <div style="float: left;">
                    <input type="button" class="styled-button-1" id="rwdt" style="cursor: pointer;" value="Play" />
                </div>
            </div>
        </div>--%>
    </div>
    <%--Points Page End --%>
    <div id="gallery" class="gallery">
    </div>
    <div id="articlesCont" style="display: none; top: 94px; position: absolute; width: 100%;">
        <h1 id="articlesheader" class="art-heading1">
            <%=strings.Articles%>
        </h1>
        <div id="articlesLayout">
        </div>
    </div>
    <div id="boardsCont" class="boards">
    </div>
    <div id="content" style="display: none; margin-top: 120px; position: absolute; width: 100%;">
    </div>
    <div id="users" class="user">
    </div>
    <div id="storesCont" class="gallery">
    </div>
    <div id="add" class="popup add">
        <div class="modal">
            <div class="header1">
                <div class="title">
                    <h2>
                        <%=strings.Add %></h2>
                </div>
                <div id="addclose" class="close">
                    <a href="javascript:void(0)">
                        <img src="http://cdn.pinpolish.com/img/nails/close-alone.png" /></a></div>
            </div>
            <%--<div class="yellowStripe">
                <%=strings.Pin_1 %>
                <a href="javascript:void(0)">
                    <%=strings.Pin_2 %></a>.</div>--%>
            <div class="openlink">
                <div id="apcont">
                    <a class="cell1" id="addpint" href="javascript:void(0)">
                        <div id="addpinicon" class="icon pin">
                        </div>
                        <span>
                            <%=strings.Pin_Add %></span> </a>
                </div>
                <div id="upcont">
                    <a class="cell1" id="uploadpint" href="javascript:void(0)">
                        <div id="uploadpinicon" class="icon arrow">
                        </div>
                        <span>
                            <%=strings.Pin_Upload %></span> </a>
                </div>
                <div>
                    <a class="cell1" href="javascript:void(0)" id="createboardt">
                        <div class="icon board">
                        </div>
                        <span>
                            <%=strings.Board_Create %></span> </a>
                </div>
                <div style="clear: both;">
                </div>
            </div>
        </div>
    </div>
    <div id="comment" class="popup comment">
        <div class="modal">
            <div class="header1">
                <div class="title">
                    <h2>
                        <%=strings.Comment %></h2>
                </div>
                <div id="commentclose" class="close">
                    <a href="javascript:void(0)">
                        <img src="http://cdn.pinpolish.com/img/nails/close-alone.png" /></a></div>
            </div>
            <div class="cont">
                <div class="addPinRightPane">
                    <textarea id="commentDesc" placeholder="Enter your comment"></textarea>
                    <span class="button"><a id="saveComment" href="javascript:void(0)"><span>
                        <%=strings.Add_Comment %></span></a></span>
                    <label id="commentCharCount">
                        500</label>
                </div>
            </div>
        </div>
    </div>
    <div id="Repin" class="popup Repin">
        <div class="modal">
            <div class="header1">
                <div class="title">
                    <h2>
                        <%=strings.Repin %></h2>
                </div>
                <div id="Repinclose" class="close">
                    <a href="javascript:void(0)">
                        <img src="http://cdn.pinpolish.com/img/nails/close-alone.png" /></a></div>
            </div>
            <div class="cont">
                <div style="clear: both;">
                </div>
                <div class="pinIt">
                    <div class="uploadCont">
                        <div class="uploadImages">
                            <img id="imgRP" /></div>
                    </div>
                </div>
                <div class="addPinRightPane">
                    <div class="PinForm">
                        <div class="selectBox">
                            <a class="select" style="height: 30px; display: block;" id="selectRP" href="javascript:void(0)">
                                <span class="title" id="titleRP"></span><span class="rightArrow"></span></a>
                            <div class="dropDownBox" id="dropDownBoxRP">
                                <ul>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <textarea id="RPDesc" placeholder="Enter your comments"></textarea>
                    <span class="button"><a id="saveRP" href="javascript:void(0)"><span>
                        <%=strings.Pin_It %></span></a></span>
                    <label id="RPCharCount">
                        500</label>
                </div>
            </div>
        </div>
    </div>
    <div id="uploadPin" class="popup uploadPin">
        <div class="modal">
            <div class="header1">
                <div class="title">
                    <h2>
                        <%=strings.Pin_Upload %></h2>
                </div>
                <div id="uploadPinclose" class="close">
                    <a href="javascript:void(0)">
                        <img src="http://cdn.pinpolish.com/img/nails/close-alone.png" /></a></div>
            </div>
            <div class="cont">
                <div class="findImage">
                    <input class="whiteBut" id="fu" href="javascript:void(0)" value="<%=strings.Browse %>.."
                        type="file" />
                    <div class="loaderImg" id="lmuplimg" style="display: none">
                        <img src="http://cdn.pinpolish.com/img/load2.gif" /></div>
                </div>
                <div style="clear: both;">
                </div>
                <div class="pinIt">
                    <div class="uploadCont">
                        <div class="uploadImages">
                            <img id="uploadedImage" src="http://cdn.pinpolish.com/img/nails/default_thumbnail.jpg" /></div>
                    </div>
                </div>
                <div class="addPinRightPane">
                    <div class="PinForm">
                        <div class="selectBox">
                            <a class="select" style="height: 30px; display: block;" id="selectUP" href="javascript:void(0)">
                                <span class="title" id="titleUP"></span><span class="rightArrow"></span></a>
                            <div class="dropDownBox" id="dropDownBoxUP">
                                <ul>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <textarea id="uplPDesc" elastic="true" placeholder="Describe the pin"></textarea>
                    <span class="button"><a id="saveUploadPin" href="javascript:void(0)"><span>
                        <%=strings.Pin_It %></span></a></span>
                    <label id="uplPCharCount">
                        500</label>
                </div>
            </div>
        </div>
    </div>
    <div id="addPin" class="popup addPin">
        <div class="modal">
            <div class="header1">
                <div class="title">
                    <h2>
                        <%=strings.Pin_Add %></h2>
                </div>
                <div id="addPinclose" class="close">
                    <a href="javascript:void(0)">
                        <img src="http://cdn.pinpolish.com/img/nails/close-alone.png" /></a></div>
            </div>
            <div class="cont">
                <div class="findImage" id="fImages">
                    <input type="text" name="textfield" id="url_upload" />
                    <span><a class="whiteBut" id="FindImages" href="javascript:void(0)"><span>
                        <%=strings.Images_1 %></span></a></span>
                    <div class="loaderImg" id="lmfindimages" style="display: none">
                        <img src="http://cdn.pinpolish.com/img/load2.gif" /></div>
                </div>
                <div style="clear: both;">
                </div>
                <div class="pinIt">
                    <div class="uploadCont">
                        <div class="uploadImages">
                            <img id="urlimages" src="http://cdn.pinpolish.com/img/nails/default_thumbnail.jpg" />
                        </div>
                        <div class="prevNext">
                            <div class="prev" id="prev">
                                <a href="javascript:void(0)">
                                    <%=strings.Prev %></a></div>
                            <div class="next" id="next">
                                <a href="javascript:void(0)">
                                    <%=strings.Next %></a></div>
                        </div>
                    </div>
                    <div class="loading" id="lmpreloadimages" style="display: none">
                        <img src="http://cdn.pinpolish.com/img/load2.gif" /><br />
                        <span>
                            <%=strings.Loading %>....</span>
                    </div>
                </div>
                <div class="addPinRightPane">
                    <div class="PinForm">
                        <div class="selectBox">
                            <a class="select" id="selectAP" href="javascript:void(0)"><span class="title" id="titleAP">
                            </span><span class="rightArrow"></span></a>
                            <div class="dropDownBox" id="dropDownBoxAP">
                                <ul>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <textarea id="addPDesc" elastic="true" placeholder="Describe the pin"></textarea>
                    <span class="button"><a id="saveAddPin" href="javascript:void(0)"><span>
                        <%=strings.Pin_It %></span></a></span>
                    <label id="addPCharCount">
                        500</label>
                </div>
            </div>
            <div style="clear: both;">
            </div>
        </div>
    </div>
    <div id="editboard" class="modal-EditBrd" style="position: fixed;">
        <div class="header1">
            <div class="title">
                <h2>
                    <span id="board_label">Create Board</span></h2>
            </div>
            <div id="editboardclose" class="close">
                <a href="javascript:void(0)">
                    <img src="http://cdn.pinpolish.com/img/nails/close-alone.png" /></a></div>
        </div>
        <div class="cont">
            <div class="bedit_div">
                <ul>
                    <!-- Board Title -->
                    <li class="bedit_div_li">
                        <label for="board_name">
                            <%=strings.Title %></label>
                        <div style="float: left;">
                            <input type="text" name="name" id="board_name" />
                        </div>
                    </li>
                    <li class="bedit_div_li">
                        <div style="width: 150px; float: left; margin-top: 0; margin-right: 0; margin-bottom: 0;
                            margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                            <label style="display: inline-block; line-height: 1.4em; font-size: 18px; float: left;
                                width: 150px; padding-top: 7px; vertical-align: top;">
                                <%=strings.Cat %></label></div>
                        <div style="float: left;">
                            <div class="selectBox" id="selectBox">
                                <a class="select" id="catselect" href="javascript:void(0)"><span class="title" id="catseltext">
                                    <%=strings.Cat_Select%>
                                </span><span class="rightArrow"></span></a>
                            </div>
                            <div class="dropdownContainer" id="catdropdown" style="position: absolute;">
                                <div style="width: 185px; height: auto; float: left;">
                                    <div class="type">
                                        <% =cat.Color%>
                                    </div>
                                    <ul>
                                        <li><a href="javascript:void(0);" catid="17" name="sellist">
                                            <% =cat.Black %></a></li>
                                        <li><a href="javascript:void(0);" catid="12" name="sellist">
                                            <% =cat.Blue %></a></li>
                                        <li><a href="javascript:void(0);" catid="18" name="sellist">
                                            <% =cat.Brown %></a></li>
                                        <li><a href="javascript:void(0);" catid="16" name="sellist">
                                            <% =cat.Gray %></a></li>
                                        <li><a href="javascript:void(0);" catid="10" name="sellist">
                                            <% =cat.Green %></a></li>
                                        <li><a href="javascript:void(0);" catid="6" name="sellist">
                                            <% =cat.Metallic %></a></li>
                                        <li><a href="javascript:void(0);" catid="2" name="sellist">
                                            <% =cat.Multi %></a></li>
                                        <li><a href="javascript:void(0);" catid="8" name="sellist">
                                            <% =cat.Orange %></a></li>
                                        <li><a href="javascript:void(0);" catid="14" name="sellist">
                                            <% =cat.Pink %></a></li>
                                        <li><a href="javascript:void(0);" catid="13" name="sellist">
                                            <% =cat.Purple %></a></li>
                                        <li><a href="javascript:void(0);" catid="7" name="sellist">
                                            <% =cat.Red %></a></li>
                                        <li><a href="javascript:void(0);" catid="11" name="sellist">
                                            <% =cat.Teal %></a></li>
                                        <li><a href="javascript:void(0);" catid="3" name="sellist">
                                            <% =cat.Two_Tone %></a></li>
                                        <li><a href="javascript:void(0);" catid="15" name="sellist">
                                            <% =cat.White %></a></li>
                                        <li><a href="javascript:void(0);" catid="9" name="sellist">
                                            <% =cat.Yellow %></a></li>
                                    </ul>
                                </div>
                                <div style="width: 185px; height: auto; float: left;">
                                    <div class="type">
                                        <% =cat.Finish%>
                                    </div>
                                    <ul>
                                        <li><a href="javascript:void(0);" catid="20" name="sellist">
                                            <% =cat.Glitter %></a></li>
                                        <li><a href="javascript:void(0);" catid="19" name="sellist">
                                            <% =cat.Matte %></a></li>
                                        <li><a href="javascript:void(0);" catid="5" name="sellist">
                                            <% =cat.Shiny %></a></li>
                                    </ul>
                                </div>
                                <div style="width: 185px; height: auto; float: left;">
                                    <div class="type">
                                        <% =cat.Difficulty%>
                                    </div>
                                    <ul>
                                        <li><a href="javascript:void(0);" catid="25" name="sellist">
                                            <% =cat.Crazy %></a></li>
                                        <li><a href="javascript:void(0);" catid="24" name="sellist">
                                            <% =cat.Hard %></a></li>
                                        <li><a href="javascript:void(0);" catid="23" name="sellist">
                                            <% =cat.Medium %></a></li>
                                        <li><a href="javascript:void(0);" catid="22" name="sellist">
                                            <% =cat.Simple %></a></li>
                                    </ul>
                                </div>
                                <div style="width: 185px; height: auto; float: left;">
                                    <div class="type">
                                        <% =cat.Style%>
                                    </div>
                                    <ul>
                                        <li><a href="javascript:void(0);" catid="30" name="sellist">
                                            <% =cat.ThreeD %></a></li>
                                        <li><a href="javascript:void(0);" catid="31" name="sellist">
                                            <% =cat.Airbrush %></a></li>
                                        <li><a href="javascript:void(0);" catid="32" name="sellist">
                                            <% =cat.FreeStyle %></a></li>
                                        <li><a href="javascript:void(0);" catid="29" name="sellist">
                                            <% =cat.Gradient %></a></li>
                                        <li><a href="javascript:void(0);" catid="27" name="sellist">
                                            <% =cat.Pattern %></a></li>
                                        <li><a href="javascript:void(0);" catid="28" name="sellist">
                                            <% =cat.Picture %></a></li>
                                        <li><a href="javascript:void(0);" catid="33" name="sellist">
                                            <% =cat.Stamp %></a></li>
                                        <li><a href="javascript:void(0);" catid="34" name="sellist">
                                            <% =cat.Stilleto %></a></li>
                                    </ul>
                                </div>
                                <div style="width: 185px; height: auto; float: left;">
                                    <div class="type">
                                        <% =cat.With_People%>
                                    </div>
                                    <ul>
                                        <li><a href="javascript:void(0);" catid="40" name="sellist">
                                            <% =cat.Accessories %></a></li>
                                        <li><a href="javascript:void(0);" catid="36" name="sellist">
                                            <% =cat.Face %></a></li>
                                        <li><a href="javascript:void(0);" catid="37" name="sellist">
                                            <% =cat.Outfit %></a></li>
                                        <li><a href="javascript:void(0);" catid="39" name="sellist">
                                            <% =cat.Pedicures %></a></li>
                                    </ul>
                                </div>
                                <div style="width: 185px; height: auto; float: left;">
                                    <div class="type">
                                        <% =cat.How_To%>
                                    </div>
                                    <ul>
                                        <li><a href="javascript:void(0);" catid="42" name="sellist">
                                            <% =cat.How_To_Gallery %></a></li>
                                    </ul>
                                </div>
                                <div style="width: 185px; height: auto; float: left;">
                                    <div class="type">
                                        <% =cat.Products%>
                                    </div>
                                    <ul>
                                        <li><a href="javascript:void(0);" catid="46" name="sellist">
                                            <% =cat.Manicure %></a></li>
                                        <li><a href="javascript:void(0);" catid="44" name="sellist">
                                            <% =cat.Nail_Polish %></a></li>
                                        <li><a href="javascript:void(0);" catid="45" name="sellist">
                                            <% =cat.Pattern_Tools %></a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li style="display: block; font-size: 21px; font-weight: 300; clear: both; color: #8c7e7e;
                        text-shadow: 0 1px rgba(255,255,255,0.9); border-top-style: solid; border-top-width: 1px;
                        border-top-color: rgba(255,255,255,0.7); border-bottom-style: solid; border-bottom-width: 1px;
                        border-bottom-color: rgba(34,25,25,0.1); float: left; width: 100%; margin-top: 0;
                        margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 15px; padding-right: 0;
                        padding-bottom: 15px; padding-left: 0; list-style-type: none;">
                        <label style="display: inline-block; line-height: 1.4em; font-size: 18px; float: left;
                            width: 150px; padding-top: 7px; vertical-align: top;">
                            <%=strings.Pin_6 %>?</label>
                        <div style="font-size: 18px; float: left; margin-top: 0; margin-right: 0; margin-bottom: 0;
                            margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;
                            width: 400px;">
                            <div id="boardCollaborators" style="font-size: 18px; overflow: hidden; margin-top: 0;
                                margin-right: 0; margin-bottom: 10px; margin-left: 0; padding-top: 0; padding-right: 0;
                                padding-bottom: 7px; padding-left: 0; border-top-style: none; border-right-style: none;
                                border-bottom-style: none; border-left-style: none;">
                            </div>
                            <span id="invite_response" style="background-color: #FFA; padding-bottom: 10px; padding-left: 20px;
                                padding-right: 20px; padding-top: 12px; float: left; margin-bottom: 10px; width: 429px;
                                margin-top: -10px; color: #2A1919; font-size: 18px; display: none;"></span>
                            <div style="margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0;
                                padding-right: 0; padding-bottom: 0; padding-left: 0;">
                                <input type="text" id="ebcontributor" title="Enter Email Address or Username" autocomplete="off"
                                    role="textbox" aria-autocomplete="list" aria-haspopup="true" style="font-family: inherit;
                                    font-size: 18px; font-weight: 300; resize: none; outline: none; line-height: 1.4;
                                    color: #221919; box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px #fff; -moz-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px #fff;
                                    -webkit-box-shadow: inset 0 1px rgba(34,25,25,0.15),
    0 1px #fff; min-width: 301px; float: left; clear: both; display: inline-block; box-sizing: border-box; -moz-box-sizing: border-box;
                                    -ms-box-sizing: border-box; -webkit-box-sizing: border-box; border-radius: 6px;
                                    -moz-border-radius: 6px; -webkit-border-radius: 6px; -moz-transition: all 0.08s ease-in-out;
                                    -webkit-transition: all 0.08s ease-in-out; margin-top: 0; margin-right: 0; margin-bottom: 0;
                                    margin-left: 0; padding-top: 6px; padding-right: 12px; padding-bottom: 6px; padding-left: 12px;
                                    border-top-color: #ad9c9c; border-right-color: #ad9c9c; border-bottom-color: #ad9c9c;
                                    border-left-color: #ad9c9c; border-top-style: solid; border-right-style: solid;
                                    border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                                    border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #fff;" />
                                <div style="margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0;
                                    padding-right: 0; padding-bottom: 0; padding-left: 0;">
                                    <ul role="listbox" aria-activedescendant="ui-active-menuitem" style="top: 460px !important;
                                        top: 0px; left: 0px; display: none; border-radius: 6px; -moz-border-radius: 6px;
                                        -webkit-border-radius: 6px; font-size: 13px; color: #524d4d; position: absolute;
                                        max-height: 311px; cursor: default; overflow: hidden; box-shadow: 0 3px 8px rgba(0,0,0,.3);
                                        -moz-box-shadow: 0 3px 8px rgba(0,0,0,.3); -webkit-box-shadow: 0 3px 8px rgba(0,0,0,.3);
                                        margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 3px;
                                        padding-right: 0; padding-bottom: 0; padding-left: 0; border-top-color: #ad9c9c;
                                        border-right-color: #ad9c9c; border-bottom-color: #ad9c9c; border-left-color: #ad9c9c;
                                        border-top-style: solid; border-right-style: solid; border-bottom-style: solid;
                                        border-left-style: solid; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px;
                                        border-left-width: 1px; background-color: #fff;">
                                    </ul>
                                </div>
                                <a href="javascript:void(0);" id="ebcontribute" style="font-weight: bold; color: #524d4d;
                                    text-decoration: none; outline: none; position: relative; display: inline-block;
                                    text-align: center; line-height: 1em; border-radius: 6px; -moz-border-radius: 6px;
                                    -webkit-border-radius: 6px; -moz-transition-property: color,
    -moz-box-shadow, text-shadow; -moz-transition-duration: .05s; -moz-transition-timing-function: ease-in-out; -webkit-transition-property: color, -webkit-box-shadow, text-shadow;
                                    -webkit-transition-duration: .05s; -webkit-transition-timing-function: ease-in-out;
                                    box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                                    -moz-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                                    -webkit-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                                    cursor: pointer; font-size: 18px; text-shadow: 0 1px rgba(255,255,255,0.9); float: left;
                                    margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 10px; padding-top: .45em;
                                    padding-right: .825em; padding-bottom: .45em; padding-left: .825em; border-top-color: transparent;
                                    border-right-color: transparent; border-bottom-color: transparent; border-left-color: transparent;
                                    border-top-style: solid; border-right-style: solid; border-bottom-style: solid;
                                    border-left-style: solid; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px;
                                    border-left-width: 1px;"><strong style="font-style: normal; font-weight: bold; position: relative;">
                                        <%=strings.Add %></strong><span style="position: absolute; top: -1px; right: -1px;
                                            bottom: -1px; left: -1px; display: block; opacity: 1; border-radius: 6px; -moz-border-radius: 6px;
                                            -webkit-border-radius: 6px; box-shadow: inset 0 1px rgba(255,255,255,0.35); -moz-box-shadow: inset 0 1px rgba(255,255,255,0.35);
                                            -webkit-box-shadow: inset 0 1px rgba(255,255,255,0.35); -moz-transition-property: opacity;
                                            -moz-transition-duration: 0.5s; -moz-transition-timing-function: ease-in-out;
                                            -webkit-transition-property: opacity; -webkit-transition-duration: 0.5s; -webkit-transition-timing-function: ease-in-out;
                                            font-size: 18px; filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfafb',
    endColorstr='#f0eded'); border-top-color: #bbb; border-right-color: #bbb; border-bottom-color: #bbb; border-left-color: #bbb;
                                            border-top-style: solid; border-right-style: solid; border-bottom-style: solid;
                                            border-left-style: solid; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px;
                                            border-left-width: 1px; background-position: 0% 0%;"></span></a>
                            </div>
                            <ul id="CurrentCollaborators" style="border-top-width: 0 !important; margin-top: 10px;
                                margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0; padding-right: 0;
                                padding-bottom: 0; padding-left: 0;">
                            </ul>
                        </div>
                    </li>
                </ul>
                <div id="saveEB" class="button">
                    <a href="javascript:void(0)"><strong style="font-style: normal; font-weight: bold;
                        position: relative;"><span id="board_lbl_upd">
                            <%=strings.Save_Board %></span></strong> <span style="position: absolute; right: -1px;
                                bottom: -1px; left: -1px; display: block; opacity: 1; border-radius: 8px; -moz-border-radius: 8px;
                                -webkit-border-radius: 8px; box-shadow: inset 0 1px
    rgba(255,255,255,0.35); -moz-box-shadow: inset 0 1px rgba(255,255,255,0.35); -webkit-box-shadow: inset 0 1px rgba(255,255,255,0.35);
                                -moz-transition-property: opacity; -moz-transition-duration: 0.5s; -moz-transition-timing-function: ease-in-out;
                                -webkit-transition-property: opacity; -webkit-transition-duration: 0.5s; -webkit-transition-timing-function: ease-in-out;
                                font-size: 24px; filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#eb5367',
    endColorstr='#d43638'); background-position: 0% 0%;"></span></a>
                </div>
                <div id="delEB" style="display: none" class="button">
                    <a href="javascript:void(0)"><strong style="font-style: normal; font-weight: bold;
                        position: relative;"><span id="Span1">
                            <%=strings.Delete_Board %></span></strong> <span style="position: absolute; right: -1px;
                                bottom: -1px; left: -1px; display: block; opacity: 1; border-radius: 8px; -moz-border-radius: 8px;
                                -webkit-border-radius: 8px; box-shadow: inset 0 1px
    rgba(255,255,255,0.35); -moz-box-shadow: inset 0 1px rgba(255,255,255,0.35); -webkit-box-shadow: inset 0 1px rgba(255,255,255,0.35);
                                -moz-transition-property: opacity; -moz-transition-duration: 0.5s; -moz-transition-timing-function: ease-in-out;
                                -webkit-transition-property: opacity; -webkit-transition-duration: 0.5s; -webkit-transition-timing-function: ease-in-out;
                                font-size: 24px; filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#eb5367',
    endColorstr='#d43638'); background-position: 0% 0%;"></span></a>
                </div>
            </div>
        </div>
    </div>
    <div id="editpin" class="popup" style="position: absolute; top: -50px; left: 50px;">
        <div class="modal">
            <div class="header1">
                <div class="title">
                    <h2>
                        <%=strings.Edit_Pin %></h2>
                </div>
                <div class="close" id="closeedit">
                    <a href="javascript:void(0)">
                        <img src="http://cdn.pinpolish.com/img/nails/close-alone.png" /></a></div>
            </div>
            <div class="contEditpin">
                <div id="PinEditPreview" class="EditPinImg">
                    <%--left panel--%>
                    <a id="pinlink" style="font-weight: bold; color: #221919; text-decoration: none;
                        outline: none;">
                        <img id="pinimg" style="border-top-width: 0; border-right-width: 0; border-bottom-width: 0;
                            border-left-width: 0;" /></a>
                </div>
                <%--left panel--%>
                <div style="float: left; width: 490px;">
                    <%--right panel--%>
                    <p id="postDescription" style="float: left">
                    </p>
                    <div id="PinEdit" class="contEditPinUI">
                        <ul>
                            <li>
                                <label>
                                    <%=strings.Desc %></label>
                                <div>
                                    <div id="ta_holder">
                                        <textarea class="textBox" rows="2" name="details" maxlength="500" id="description_pin_edit"
                                            cols="40" placeholder="Write your comment">
                                        </textarea>
                                        <div>
                                        </div>
                                    </div>
                                    <span id="editPCharCount"></span>
                                </div>
                            </li>
                            <li>
                                <label for="id_link">
                                    <%=strings.Link%></label>
                                <div>
                                    <input class="LinkBox" name="link" id="id_link" type="text" />
                                </div>
                            </li>
                            <li>
                                <label for="id_board">
                                    <%=strings.Board %></label>
                                <div>
                                    <select name="board" id="id_board" style="font-family: inherit; font-size: 18px;
                                        font-weight: inherit; resize: none; outline: none;">
                                    </select>
                                </div>
                            </li>
                        </ul>
                        <div>
                            <p class="button" style="margin-left: 40px;">
                                <a id="saveeditpin" href="javascript:void(0)"><strong>
                                    <%=strings.Save_Pin%></strong> <span></span></a>
                            </p>
                        </div>
                        <div>
                            <p class="button">
                                <a id="deletepin" href="javascript:void(0)"><strong>
                                    <%=strings.Delete_Pin %></strong> <span></span></a>
                            </p>
                        </div>
                    </div>
                </div>
                <%--right panel--%>
            </div>
        </div>
    </div>
    <div id="pin" style="position: absolute; box-shadow: 0 1px 3px rgba(34,25,25,0.4);
        -moz-box-shadow: 0 1px 3px rgba(34,25,25,0.4); display: none; -webkit-box-shadow: 0 1px 3px rgba(34,25,25,0.4);
        margin-top: 0; margin-right: auto; margin-bottom: 32px; margin-left: auto; padding-top: 0;
        padding-right: 0; padding-bottom: 0; padding-left: 0; background-color: #FFFFFF;
        text-align: center; left: 50%;">
        <div style="clear: both; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
            padding-top: 20px; padding-right: 0; padding-bottom: 0; padding-left: 0;">
        </div>
        <div id="PinActionButtons" style="height: 26px; width: 280px; float: left; overflow: hidden;
            margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 30px; padding-top: 0;
            padding-right: 0; padding-bottom: 0; padding-left: 13px; background-image: url('http://cdn.pinpolish.com/img/nails/detailiconBg.jpg');
            background-repeat: no-repeat; background-position: left top;">
            <a id="likepint" href="javascript:void(0)" style="font-weight: bold; color: #221919;
                text-decoration: none; outline: none;">
                <div class="pinLike" align="left">
                    <%=strings.Like
                    %></div>
            </a><a id="editpint" href="javascript:void(0)" style="font-weight: bold; color: #221919;
                text-decoration: none; outline: none;">
                <div style="height: 26px; font-size: 12px; font-weight: bold; float: left; display: block;
                    color: #777176; font-family: Helvetica, Arial, Sans-Serif; text-align: left;
                    line-height: 26px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                    padding-top: 0; padding-right: 14px; padding-bottom: 0; padding-left: 30px; background-image: url('http://cdn.pinpolish.com/img/nails/repinIconDetailPage.png');
                    background-repeat: no-repeat; background-position: left 5px;" align="left">
                    <%=strings.Edit
                    %></div>
            </a><a id="repint" href="javascript:void(0)" style="font-weight: bold; color: #221919;
                text-decoration: none; outline: none;">
                <div style="height: 26px; font-size: 12px; font-weight: bold; float: left; display: block;
                    color: #777176; font-family: Helvetica, Arial, Sans-Serif; text-align: left;
                    line-height: 26px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                    padding-top: 0; padding-right: 14px; padding-bottom: 0; padding-left: 23px; background-image: url('http://cdn.pinpolish.com/img/nails/repinIconDetailPage.png');
                    background-repeat: no-repeat; background-position: left 4px;" align="left">
                    <%=strings.Repin
                    %></div>
            </a><a id="commentt" href="javascript:void(0)" style="font-weight: bold; color: #221919;
                text-decoration: none; outline: none;">
                <div style="height: 26px; font-size: 12px; font-weight: bold; float: left; display: block;
                    color: #777176; font-family: Helvetica, Arial, Sans-Serif; text-align: left;
                    line-height: 26px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                    padding-top: 0; padding-right: 14px; padding-bottom: 0; padding-left: 25px; background-image: url('http://cdn.pinpolish.com/img/nails/commentIconDetailPage.png');
                    background-repeat: no-repeat; background-position: left 4px;" align="left">
                    <%=strings.Comment
                    %></div>
            </a>
        </div>
        <div style="clear: both; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
            padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
        </div>
        <div style="display: block; position: relative; overflow: hidden; margin-top: 20px;
            margin-right: 30px; margin-bottom: 30px; margin-left: 30px; padding-top: 0; padding-right: 0;
            padding-bottom: 0; padding-left: 0; background-color: #fff;">
            <a id="pinimgsource" target="_blank">
                <img id="pinCloseupImage" style="display: block; margin-top: 0; margin-right: auto;
                    margin-bottom: 0; margin-left: auto; border-top-width: 0; border-right-width: 0;
                    border-bottom-width: 0; border-left-width: 0;" /></a></div>
        <p id="pintitle" style="margin: 0px; color: #524D4D; font-size: 13px; font-weight: bold;
            word-break: break-word; padding-top: 0; padding-right: 0; padding-bottom: 0;
            padding-left: 0;">
        </p>
    </div>
    
    
    <%--<div id="fbd" class="popup fbd">
        <div class="modal">
            <div class="header1">
                <div class="title">
                    <h2>
                        <%=strings.Comment
                        %></h2>
                </div>
                <div id="fbdclose" class="close">
                    <a href="javascript:void(0)">
                        <img src="http://cdn.pinpolish.com/img/nails/close-alone.png" /></a></div>
            </div>
            <div class="cont">
                <ul>
                    <li>
                        <p class="ques">
                            sdfdsfds...</p>
                    </li>
                    <li class="choice">
                        <input type="radio" name="choosefbd" /><span>dsfds</span> </li>
                    <li class="choice">
                        <input type="radio" name="choosefbd" /><span>dsfdsf</span> </li>
                    <li class="choice">
                        <input type="radio" name="choosefbd" /><span>dsfds</span> </li>
                    <li class="choice">
                        <input type="radio" name="choosefbd" /><span>dsfdsfds</span> </li>
                </ul>
            </div>
        </div>
    </div>--%>
    <div id="topcontrol" class="topcontrol">
        </div>
</body>
</html>
