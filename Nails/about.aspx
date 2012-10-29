<%@ Page Language="C#" AutoEventWireup="true" Inherits="Nails.PageHandlerBase" %>

<%@ Import Namespace="Nails" %>
<script runat="server">
    //private string pre;
    //protected void Page_Init(object sender, EventArgs e)
    //{
    //    pre = Request.Url.Segments.Last().ToLower().Contains("about") ? "." : "";
    //    Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    Response.Cache.SetNoStore();
    //    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);        
    //    Common.RemoveValueinCookie(Common.InfoCookie, new string[]{
    //            "vuID" ,
    //            "vuemail" ,
    //            "vuname" ,
    //            "vuavatar",
    //            "vuboards",
    //            "vupins",
    //            "vulikes",
    //            "vupoints",
    //            "vufollower",
    //            "vufollowing"
    //        });
    //    if (pre == ".")
    //    {
    //        beforeloginA.Visible = false;
    //        afterloginA.Visible = true;
    //    }
    //    else
    //    {
    //        beforeloginA.Visible = true;
    //        afterloginA.Visible = false;
    //    }
    //}
    //public string getAV(string avatar)
    //{
    //    return string.IsNullOrEmpty(avatar) ? null : Common.CDN + Common.UserBlankImg;
    //}

</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>Pinjimu /
        <%=strings.Help %></title>
    <link rel="icon" href="favicon.ico" />
    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-31949192-1']);
        _gaq.push(['_trackPageview', location.pathname + location.search + location.hash]);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>
    <link href="http://cdn.pinpolish.com/pinjimu_about.css" rel="stylesheet"
        type="text/css" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
    <script src="freshpin.js" type="text/javascript"></script>
    <script src="http://cdn.pinpolish.com/build/files/homeincludes.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //replace 'Sitename' with 'Pinjimu'
            $('a, p, h1, h2, h3').html(function () {
                return $(this).html().replace(/SiteName/g, 'Pinjimu');
            });

            show(h() || 'about');

            function show(id) {
                $('#AboutLeft a').prop('class', '');
                $('#AboutRight div').hide(); // Hide All Divs
                $("#" + id + "content").show(); // Show only the selected Div                    
                $('#' + id).prop('class', 'selected');
                $('#AboutLeft .selected').addClass('loaded');
                return false;
            }
            $(window).bind('hashchange', function () {
                show(h());
                FreshPin.trackGACPV();
            });
        });          
    </script>
    <script type="text/javascript">
        strings = {
            Boards: '<%=strings.Boards%>',
            Pins: '<%=strings.Pins%>',
            Like: '<%=strings.Like%>',
            Settings: '<%=strings.Settings%>',
            Log_Out: '<%=strings.Log_Out%>',
            Login: '<%=strings.Login%>',
            Points: '<%=strings.Points%>',
            Repin: '<%=strings.Repin%>',
            Comment: '<%=strings.Comment %>',
            Search_Slogan: '<% =strings.Search_Slogan %>',
            Search_Alert_1: '<%=strings.Search_Alert_1 %>',
            Search_Alert_2: '<%=strings.Search_Alert_2 %>'
        };
        FreshPin.constants.cdn = '<%=Common.CDN%>';
        FreshPin.constants.domain = '<%=Common.Domain%>';
        FreshPin.constants.upl = '<%=Common.UploadedImageRelPath%>';
        FreshPin.constants.authcookie = '<%=Common.AuthCookie%>';
        FreshPin.constants.infocookie = '<%=Common.InfoCookie%>';
    </script>
</head>
<body>
    <div class="header">
        <%--<div class="FixedContainer HeaderContainer">
            <div class="freshpinlogo">
                <a name="noblock" href=".">
                    <img src="logo.png" /></a>
                <div class="freshpinlogoTagline">
                    <%=strings.Banner %></div>
            </div>
            <ul id="Navigation">
                <li><a name="noblock" href="#about" class="nav">
                    <%=strings.About %><span></span></a>
                    <ul>
                        <li><a name="noblock" href="#help">
                            <%=strings.Help %></a></li>
                        <li class="beforeDivider"><a name="noblock" href="about#copyright">
                            <%=strings.Copyright %></a></li>
                    </ul>
                </li>
                <li id="logininfo" class="logininfo">
                    </li>
            </ul>
            <div id="Search">
                <input type="text" id="query" name="q" size="27" value="<% =strings.Search_Slogan %>" />
                <a id="query_button" href="#" class="lg">
                    <img src="http://cdn.pinpolish.com/img/search.gif" alt="" /></a>
            </div>
             <div id="sautocomplete" class="sAutoComplete" style="display: none; position: fixed;text-align:center;
                    top: 35px;">
                    <input name="st" id="rbpins" type="radio" value="pins" ><label for="rbpins" style="font-family: 'Arial',arial,sans-serif;
                        font-size: 13px; font-weight: bold;padding-right:10px;">Pins</label>
                    <input name="st" id="rbboards" type="radio" value="boards"   /><label for="rbboards"
                        style="font-family: 'Arial',arial,sans-serif; font-size: 13px; font-weight: bold;padding-right:10px;">Boards</label>
                    <input name="st" id="rbpeople" type="radio" value="people"  /><label for="rbpeople"
                        style="font-family: 'Arial',arial,sans-serif; font-size: 13px; font-weight: bold;">People</label>
             </div>
        </div>--%>
        <div class="line1">
            <div class="queryBox">
                <input class="searchfield" type="text" placeholder="<% =strings.Search_Slogan %>" id="query" />
                <input class="searchbutton" type="button" id="query_button" />
                <input class="scrollbutton" type="button" id="scroll_button" />
            </div>
            <div class="freshpinlogo">
                <a href="." name="noblock">
                    <img src="logo.png" /></a>
                <div class="freshpinlogoTagline">
                    <% =strings.Banner %></div>
            </div>
     <%--        <div style="float: right; margin-right: 50px; position: absolute; top: 63px; right: -100px;">
              <div class="aboutus">
                    <ul class="navigation">
                    <li id="logininfo" class="logininfo">
                        <div id="beforeloginA" runat="server"> <a name="logint"  href="javascript:void(0);" class="nav"><%=strings.Login %></a></div>
                        <div id="afterloginA" runat="server"><a class="nav" style="padding: 20px 27px 11px 40px;" href="<%= pre %>#boards"><span class="userProfilePic">
                                <img id="userProfilePic" src="<%= getAV(Common.ReadValue("avatar")) %>?width=20" />
                            </span>
                            <p id="username">
                                <% = Common.ReadValue("name") %>
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
                                $(FreshPin.constants.logout).click(function () {
                                    $.post('POST?t=logout', function () {
                                        $(window.location).attr('href', FreshPin.constants.logoutUrl);
                                    });
                                });
                             </script>
                             </div>
                        </li>
                    </ul>
              </div>
             </div>--%>
            <div id="sautocomplete" class="sAutoComplete" style="display: none; position: fixed;
                top: 35px;left:20px;">
                <input name="st" id="rbpins" type="radio" value="pins"><label for="rbpins" style="font-family: 'Arial',arial,sans-serif;
                    font-size: 13px; font-weight: bold;"><%=strings.Pins %></label>
                <input name="st" id="rbboards" type="radio" value="boards" /><label for="rbboards"
                    style="font-family: 'Arial',arial,sans-serif; font-size: 13px; font-weight: bold;"><%=strings.Boards %></label>
                <input name="st" id="rbpeople" type="radio" value="people" /><label for="rbpeople"
                    style="font-family: 'Arial',arial,sans-serif; font-size: 13px; font-weight: bold;"><%=strings.People %></label>
            </div>
        </div>
    </div>
 
    <div class="clear"></div>
    <div id="AboutContainer">
    
        <div id="AboutContent">
            <div id="AboutLeft">
                <ul>
                    <li><a name="noblock" href="#about" id="about"><span>
                        <%=strings.Help_1 %></span> <span class="pointer normal"></span><span class="pointer hover">
                        </span></a></li>
                    <li><a name="noblock" href="#pinpractices" id="pinpractices"><span>
                        <%=strings.Help_2 %></span> <span class="pointer normal"></span><span class="pointer hover">
                        </span></a></li>
                    <li><a name="noblock" href="#help" id="help"><span>
                        <%=strings.Help %></span> <span class="pointer normal"></span><span class="pointer hover">
                        </span></a></li>
                </ul>
                <img src="http://cdn.pinpolish.com/img/NavRule.png" alt="Navigation Rule"
                    id="NavRule" />
                <ul>
                    <li><a name="noblock" href="#privacy" id="privacy"><span>
                        <%=strings.Help_3 %></span> <span class="pointer normal"></span><span class="pointer hover">
                        </span></a></li>
                    <li><a name="noblock" href="#copyright" id="copyright"><span>
                        <%=strings.Copyright%></span> <span class="pointer normal"></span><span class="pointer hover">
                        </span></a></li>
                    <li><a name="noblock" href="#terms" id="terms"><span>
                        <%=strings.Help_5 %></span> <span class="pointer normal"></span><span class="pointer hover">
                        </span></a></li>
                </ul>
            </div>
            <!-- #AboutLeft -->
            <div id="AboutRight">
                <div id="aboutcontent">
                    <%=strings.Help_About %>
                </div>
                <div id="pinpracticescontent">
                    <%=strings.Help_Pin_Practices %>
                </div>
                <div id="helpcontent">
                    <h1>
                        <%=strings.Help %></h1>
                    <%=strings.Help_Main %>
                </div>
                <div id="privacycontent">
                    <%=strings.Help_Privacy %>
                </div>
                <div id="copyrightcontent">
                    <%=strings.Help_CopyRight %>
                </div>
                <div id="termscontent">
                    <%=strings.Help_Term %>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
