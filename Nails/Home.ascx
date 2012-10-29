<%@ Control Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="Nails" %>
<script runat="server">
    protected void Page_Init(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
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
        return string.IsNullOrEmpty(avatar) ? null : Common.CDN + Common.UserBlankImg;
    }
</script>
<!DOCTYPE html>
<html>
<head>
    <title>PinPolish /
        <%=strings.Home%></title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-5">
    <% if (Request.Browser.IsBrowser("IE") && Request.Browser.MajorVersion == 7) { %>
    <link href="http://cdn.pinpolish.com/nails_IE7.css" rel="stylesheet" type="text/css" />
    <% } else { %>
    <link href="http://cdn.pinpolish.com/nails.css" rel="stylesheet" type="text/css" />
    <% } %>
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
    <script src="http://cdn.pinpolish.com/build/files/homeincludes.js" type="text/javascript"></script>
    
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
        <ul class="box" style="width:${getWidth()}px">         
            <li class="buttons"   >
             <a name="buttons" class="buttonLike" href="javascript:void(0)">${strings.Like}</a>
             <a class="buttonPin" name="buttons" href="javascript:void(0)">${strings.Repin}</a>
             <a name="buttons" class="buttonComment" href="javascript:void(0)">${strings.Comment}</a>
            </li>
            <li class="img" style="height:${getHeight($data) }px;" ><a href="#pin=${PinID}" ><img style="height:${getHeight($data)}px;width:${getWidth()};"   src="${url}?width=${getWidth()}"  alt="${title}" /></a>
            </li>
       <li class="boardsPinBoards">
           <div class="post-pin">
                <div class="post-text"><%=strings.Post_This_To %></div>
                <div class="post-icons">
                 &nbsp;   <img href="${getUrl($data,'p')}" class="pinit" src="http://cdn.pinpolish.com/img/nails/pinterest_logo.png" width="24" height="24" title="Pinterest" border="0" />&nbsp; 
                <img  class="pinit" src="http://cdn.pinpolish.com/img/nails/facebook_logo.png" width="24" height="24" title="Facebook" border="0" />&nbsp; 
                <img  class="pinit" src="http://cdn.pinpolish.com/img/nails/tumblr_logo.png" width="24" height="24" title="Tumblr" border="0" />
                </div>
           </div>
        </li>
        <li>
            <div class="name">
                <span>${title}</span></div>
        </li>   
          {{if comments != null}}
        <li class="comments">
        {{tmpl(comments) "#comments"}}       
        </li>
        {{/if}} 
     </ul>
    </script>
    <script id="comments" type="text/x-jquery-tmpl">
     <div class="commentBox">
      <a href="${UN}" ><img src="${getUplUImg($data)}?height=20" /></a>  <p> : <a href="${UN}" style="float:left;text-align:left;" >${Name}</a> ${Comment}</p> 
     </div>
    </script>
    <script id="cats" type="text/x-jquery-tmpl">
        <a href="#cat=${escape(getVal($data,'Name'))}" style="overflow:hidden;" >${Name}</a>   
    </script>
    <script id="boards" type="text/x-jquery-tmpl">
    <div class="pinBoard">
        <h3>
            <a href="#board=${name}" >${name}</a></h3>
        <h4>
            ${pins} pins</h4>
        <div class="board">          
          {{each images}}            
             {{if $index == 0}} 
                <img src="${getVal($value,'url')}?width=222" style="height:${getHeight($value,222)}px" />
             {{else}}
                <span><img src="${getVal($value,'url')}?width=55"  /></span>
             {{/if}}
          {{/each}}
            <div style="clear: both;">
            </div>
            {{if FreshPin.visited() == false  }}
            <div class="boardfooter" style="cursor:pointer;" name="edit" boardid="${id}">
                <strong><%=strings.Edit %></strong></div>
            {{/if}}
        </div>
    </div>
    </script>
    <script id="usertpl" type="text/x-jquery-tmpl">
        <div id="following" class="Following_Container">
            <div class="Profile-pict"> <img id="fbtn_img_p_${F_ID}" src="${getAvatar($data,'F_Avatar')}?width=100&height=100" /></div>
            <div style="border:0px solid red; width:320px; float:left;">
                <div style="float:left">
                    <h4 class="link" style="margin-top:10px;"><a href="${F_UserName}">${F_FullName}</a></h4>
                </div>
                <div style="float:right; margin-top:10px;">
                    <div class="unfolwBtn">                    
                    <button type="button" class="positive" name="logint"  >                     
                        {{if F_Status}}
                            <img id="fbtn_img_${F_ID}" src=${GetFImage('Unfollow')} />
                            <span id="fbtn_text_${F_ID}" ><%=strings.UnFollow %></span>
                        {{else}}
                            <img id="fbtn_img_${F_ID}"  src=${GetFImage('follow')} />
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
            <%--<a class="thumbnails" href="#" ><img src="HomeDecorCategory/Landscape/10344274114008028_0k7FTeUy_c.jpg" /></a> --%>
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
                    <% =strings.Banner%></div>
            </div>
            <div style="float: right; margin-right: 25px; position: absolute; top: 5; right: 0;">
                <div class="aboutus">
                    <ul class="navigation">
                        <li><span class="nav" id="addtrigger" style="cursor: pointer; display: none">
                            <%=strings.Add%>+</span> </li>
                        <li><a id="about" name="noblock" href="about" class="nav">
                            <% =strings.About%><span></span></a>
                            <ul>
                                <li><a name="noblock" href="about#help">
                                    <%=strings.Help%></a></li>
                                <li class="beforeDivider"><a name="noblock" href="about#copyright">
                                    <% =strings.Copyright%></a></li>
                            </ul>
                        </li>
                        <li id="logininfo" class="logininfo"><a name="logint" href="javascript:void(0);"
                            class="nav">
                            <%=strings.Login%></a>
                            <ul>
                                <li><a href="javascript:void(0);" id="reqin">
                                    <%=strings.Register%></a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="line2">
            <div class="ImageSizes">
                <ul>
                    <li id='S' name="sizeEl"><a href="javascript:h('size','S');"><span>
                        <%=strings.S%></span></a></li>
                    <li id='M' name="sizeEl"><a href="javascript:h('size','M');"><span>
                        <%=strings.M%></span></a></li>
                    <li id='L' name="sizeEl"><a href="javascript:h('size','L');"><span>
                        <%=strings.L%></span></a></li>
                </ul>
            </div>
             <div class="menu3">
                <div style="margin: 0 auto; border: 0px solid red; width: 800px;">
                    <ul class="menu">
                         <li ><a href="#articles">
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
                <div class="user-points" id="p_points" style="display: none">
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
            font-size: 13px; font-weight: bold;"><%=strings.Pins%></label>
        <input name="st" id="rbboards" type="radio" value="boards" /><label for="rbboards"
            style="font-family: 'Arial',arial,sans-serif; font-size: 13px; font-weight: bold;"><%=strings.Boards%></label>
        <input name="st" id="rbpeople" type="radio" value="people" /><label for="rbpeople"
            style="font-family: 'Arial',arial,sans-serif; font-size: 13px; font-weight: bold;"><%=strings.People%></label>
    </div>
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
            <img src="http://cdn.pinpolish.com/img/nails/login_bar.jpg" width="650" height="36" /></div>
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
    
    <div id="gallery" class="gallery">
    </div>
    <div id="boardsCont" class="boards" style="display: none; top: 0px;">
    </div>
    <div id="content" style="display: none; margin-top: 120px; position: absolute; width: 100%;">
    </div>
    <div id="users" class="user" style="display: none; top: 0px;">
    </div>
    <div id="articlesCont" style="display: none;top :94px;position:absolute;width:100%;">
        <h1 id="articlesheader" class="art-heading1">
            <%=strings.Articles%>
        </h1>
        <div id="articlesLayout">
        </div>
    </div>
    <div id="storesCont" class="gallery">
    </div>
    <div id="gamify" class="popup">
        <div class="modal">
            <div class="header1">
                <div class="title">
                    <h2>
                        <%=strings.Comment%></h2>
                </div>
                <div id="commentclose" class="close">
                    <a href="javascript:void(0)">x</a></div>
            </div>
            <div class="cont">
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
            <a name="buttons" href="javascript:void(0)" style="font-weight: bold; color: #221919;
                text-decoration: none; outline: none;">
                <div class="pinLike" align="left">
                    <%=strings.Like
                    %></div>
            </a><a name="buttons" href="javascript:void(0)" style="font-weight: bold; color: #221919;
                text-decoration: none; outline: none;">
                <div style="height: 26px; font-size: 12px; font-weight: bold; float: left; display: block;
                    color: #777176; font-family: Helvetica, Arial, Sans-Serif; text-align: left;
                    line-height: 26px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                    padding-top: 0; padding-right: 14px; padding-bottom: 0; padding-left: 23px; background-image: url('http://cdn.pinpolish.com/img/nails/repinIconDetailPage.png');
                    background-repeat: no-repeat; background-position: left 4px;" align="left">
                    <%=strings.Repin
                    %></div>
            </a><a name="buttons" href="javascript:void(0)" style="font-weight: bold; color: #221919;
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
                <img id="pinCloseupImage" alt="Pinned Image" style="display: block; margin-top: 0;
                    margin-right: auto; margin-bottom: 0; margin-left: auto; border-top-width: 0;
                    border-right-width: 0; border-bottom-width: 0; border-left-width: 0;" /></a></div>
        <p id="pintitle" style="margin: 0px; color: #524D4D; font-size: 13px; font-weight: bold;
            word-break: break-word; padding-top: 0; padding-right: 0; padding-bottom: 0;
            padding-left: 0;">
        </p>
    </div>
    <div id="signUpWraper" class="signUpWraper">
        <div class="LogoHeader">
            <div style="margin: 0 auto; margin-left: 150px;">
                <a href=".">
                    <img src="logo-login.png" />
                </a>
            </div>
        </div>
        <div style="text-align: center; line-height: 80px; font-size: 32px; font-weight: bold;">
            <span>
                <%--<%=cat.Sign_Up
    %>--%></span></div>
        <div class="RegisterDiv">
            <div style="text-align: center; line-height: 40px; font-size: 15px;">
                <span>or <a href="javascript:void(0)" id="rilogint" style="color: #a19191">
                    <%=strings.Login%>
                </a>
                    <%=strings.Login_1%>.</span></div>
            <div class="regisCont">
                <%--<div class="inputBox">
                   <input type="text" class="" id="invitationBox"></div>--%>
                <fieldset class="inputs">
                    <input type="text" required="" autofocus="" placeholder='<%=strings.Email %>' id="invitationBox" />
                    <div class="BlueButton" style="border: 0px solid red; float: left;">
                        <a href="javascript:void(0)" id="reqinvite">
                            <%=strings.Register%></a>
                    </div>
                </fieldset>
                <div style="clear: both">
                </div>
                <div style="text-align: center;">
                    <span style="color: red; font-size: 14px; font-weight: bold;" id="err"></span>
                </div>
            </div>
        </div>
    </div>
    <div id="login" class="login">
        <div class="freshpinlogoLoginPage">
            <a href="." name="noblock">
                <img src="logo-login.png" /></a>
        </div>
         <div class="socialButtons">
            <div class="fbTw fl">
                <a name="noblock" href="https://www.facebook.com/dialog/oauth?client_id=345515548837116&redirect_uri=http%3A%2F%2Fpinpolish.com%2Ffbloggedin.html&scope=user_about_me,user_location,user_website,email,publish_stream&response_type=token">
                    <img id="logwfb" style="cursor: pointer;" src="http://cdn.pinpolish.com/img/logInWithFB.gif" /></a>
            </div>
        </div>
        <div class="loginBar">
            <img id="logbar" src="http://cdn.pinpolish.com/img/nails/login_bar.png" /></div>
        <div class="loginCont">
            <fieldset class="inputs">
                <input type="text" required placeholder='<%=strings.Email_UN %>' id="user" />
            </fieldset>
            <fieldset class="inputs">
                <input type="password" required placeholder='<%=strings.Pass %>' id="pass" />
            </fieldset>
            <div class="loginBut fl">
                <input type="button" value="<%=strings.Login %>" id="loginbutton" /></div>
            <div class="resetPassword fr">
                <a id="fup" href="javascript:void(0)">
                    <%=strings.Forgot_Pass%></a></div>
            <div style="clear: both;">
            </div>
            <span style="color: red; font-family: arial; font-size: 12px; padding: 0;" id="err1">
            </span>
        </div>
    </div>
    <div id="topcontrol" class="topcontrol">
        </div>
</body>
</html>
