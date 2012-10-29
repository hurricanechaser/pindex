<%@ Control Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="Pinjimu" %>
<%@ Register TagName="Pin" TagPrefix="ctl" Src="~/Pin.ascx" %>
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
    <title>Pinjimu /
        <%=strings.Home%></title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-5">
    <% if (Request.Browser.IsBrowser("IE") && Request.Browser.MajorVersion == 7)
       {
    %>
    <link href="http://cdn.pinjimu.com/pinjimu_IE7.min.css" rel="stylesheet"
        type="text/css" />
    <% }
       else
       { %>
    <link href="http://cdn.pinjimu.com/pinjimu.min.css" rel="stylesheet"
        type="text/css" />
    <% } %>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js" type="text/javascript"></script>
    <script src="freshpin.js" type="text/javascript"></script>
    <script src="freshpinhome.js" type="text/javascript"></script>
    <script src="http://cdn.pinjimu.com/build/files/homeincludes1.js"
        type="text/javascript"></script>
    <script id="rank" type="text/x-jquery-tmpl">
    <ul class="box">
        <li class="img"><a href="javascript:void(0)" name="${i}"></a>
        </li> 
        <li style="display: block;font-size: 11px;color: #211922;font-family: "helvetica neue" ,arial,sans-serif;text-align: left;list-style: none;"><span>${Name}</span></li>        
        <li style="display: block;font-size: 11px;color: #211922;font-family: "helvetica neue" ,arial,sans-serif;text-align: left;list-style: none;"><span>${type}:${count}</span></li>        
    </ul>
    </script>
    <script id="articles" type="text/x-jquery-tmpl">
    <div class="containerBox">
        <a href="${source}" target="_blank" style="text-decoration: none;"><h2>${title} </h2></a>
        <div class="desc">${desc}</div>
        <div class="img-cont"><a href="${source}" target="_blank"><img src="${url}?width=380" style="width:380px;height:${height}px" /></a></div>
    </div>
    </script>
    <script id="stores" type="text/x-jquery-tmpl">
    <ul class="box" >
        <li class="img"><a href="${source}" target="_blank">
            <img src="${url}?width=${getWidth()}px" style="height:${getHeight($data)}px" alt="${title}" /></a>
        </li>
        <li><div class="name"><span>${title}</span></div></li>
    </ul>
    </script>
    <script id="picTemplate" type="text/x-jquery-tmpl">
        <ul class="box"  name="box" bimid="${BIMID}" >
            <li class="buttons">
             <a name="buttons" class="buttonLike" href="javascript:void(0)">${strings.Like}</a>
             <a class="buttonPin" name="buttons" href="javascript:void(0)">${strings.Repin}</a>
             <a name="buttons" class="buttonComment" href="javascript:void(0)">${strings.Comment}</a>
            </li>
            <li class="img" style="height:${getHeight($data) + 7 }px;width:${getWidth()}px;" ><a href="#pin=${PinID}" ><img  src="${url}?width=${getWidth()}" style="height:${getHeight($data)}px;width:${getWidth()}px;" alt="${title}" /></a>
            </li>
       <li class="boardsPinBoards" style="top:${getHeight($data) - 15}px;">
           <div class="post-pin">
                <div class="post-text"><%=strings.Post_This_To %></div>
                <div class="post-icons">
                   &nbsp <img style="cursor:pointer;" href="${getUrl($data,'w')}" name="social" src="http://cdn.pinjimu.com/img/pinjimu/sina-weibo.png" />
                   &nbsp <img style="cursor:pointer;" href="${getUrl($data,'t')}" name="social" src="http://cdn.pinjimu.com/img/pinjimu/tecent.png" />
                   &nbsp <img style="cursor:pointer;" href="${getUrl($data,'r')}" name="social" src="http://cdn.pinjimu.com/img/pinjimu/Renren.png" />
                </div>
           </div>
        </li>
        <li class="name" style="width:${getWidth()}px;">           
                <span>${title}</span>
        </li>   
          {{if comments != null}}
        <li class="comments" style="width:${getWidth()}px;">
        {{tmpl(comments) "#comments"}}       
        </li>
        {{/if}} 
     </ul>
    </script>
    <script id="comments" type="text/x-jquery-tmpl">
     <div class="commentBox">
     <a href="${UN}" ><img src="${getUplUImg($data)}?height=20" />  </a>  <p> : <a href="${UN}" style="float:left;text-align:left;" >${Name} {{if Speciality }} <img name="cSymbol" src="${getSpecialityImg($data)}" style="width: 15px; margin: -20px 0 0 0;float:right;"  alt="Symbol" /> {{/if}}</a> ${Comment}</p> 
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
        <div class="line1" id="line1">
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
                        <li style="display: none"><a href="javascript:void(0)">
                            <% =cat.Following%></a></li>
                        <li><a href=".">
                            <% =cat.Popular%></a></li>
                        <li id="mlevel1">
                            <div class="menu">
                                <a href="javascript:h('cat','Spaces');">
                                    <% =cat.Spaces%></a><span></span>
                            </div>
                        </li>
                        <li id="mlevel3" class="menu"><a href="javascript:h('cat','Trends');">
                            <% =cat.Trends%></a><span></span></li>
                        <li id="mlevel4" class="menu"><a href="javascript:void(0)">
                            <% =cat.Style%></a><span></span></li>
                        <li class="menu" id="earnpoints"><a href="javascript:void(0)">
                            <% =strings.Points%></a></li>
                    </ul>
                </div>
            </div>
            <div style="float: right; margin-right: 25PX; position: absolute; top: 0; right: 0;">
                <div class="user-points" id="p_points" style="display: none">
                </div>
                <div style="float: right; margin-top: 13px; font-size: 12px;" class="localization"
                    id="mlevel2">
                    <a href="javascript:void(0)">
                        <img src="http://cdn.pinjimu.com/img/famfamfam_flag_icons/png/<%=Common.CurrentLocalizationInfo.Flag %>.png" />
                        <%=Common.CurrentLocalizationInfo.DisplayName %></a>
                </div>
            </div>
            <div class="submenu" id="submenu2" style="display: none">
                <div>
                    <%foreach (Common.LocalizationInfo linfo in Common.CurrentLocalizationCultures)
                      {%>
                    <a href="javascript:FreshPin.writeCookieValue('culture','<%=linfo.CultureInfo.IetfLanguageTag%>');location.reload(true);">
                        <img src="http://cdn.pinjimu.com/img/famfamfam_flag_icons/png/<%=linfo.Flag%>.png" />
                        <%=linfo.DisplayName%></a>
                    <br />
                    <%}%>
                </div>
            </div>
            <div class="submenu" id="submenu1" style="float: left; display: none; border: 0px solid red;
                width: 446px;">
                <div style="float: left; width: 446px; border: 0px solid green;">
                    <a href="javascript:h('cat','Basement');">
                        <% =cat.Basement%></a> <a href="javascript:h('cat','Bathroom');">
                            <% =cat.Bathroom%></a> <a href="javascript:h('cat','Bedroom');">
                                <% =cat.Bedroom%></a> <a href="javascript:h('cat','Closet');">
                                    <% =cat.Closet%></a> <a href="javascript:h('cat','Dining Room');">
                                        <% =cat.Dining_Room%></a> <a href="javascript:h('cat','Entry');">
                                            <% =cat.Entry%></a> <a href="javascript:h('cat','Exterior');">
                                                <% =cat.Exterior%></a> <a href="javascript:h('cat','Family Room');">
                                                    <% =cat.Family_Room%></a> <a href="javascript:h('cat','Garage and Shed');">
                                                        <% =cat.Garage_and_Shed%></a> <a href="javascript:h('cat','Hall');">
                                                            <% =cat.Hall%></a><br />
                    <a href="javascript:h('cat','Home Gym');">
                        <% =cat.Home_Gym%></a> <a href="javascript:h('cat','Home Office');">
                            <% =cat.Home_Office%></a> <a href="javascript:h('cat','Kitchen');">
                                <% =cat.Kitchen%></a> <a href="javascript:h('cat','Kids');">
                                    <% =cat.Kids%></a><br />
                    <a href="javascript:h('cat','Landscape');">
                        <% =cat.Landscape%></a> <a href="javascript:h('cat','Laundry Room');">
                            <% =cat.Laundry_Room%></a> <a href="javascript:h('cat','Living Room');">
                                <% =cat.Living_Room%></a> <a href="javascript:h('cat','Media Room');">
                                    <% =cat.Media_Room%></a> <a href="javascript:h('cat','Patio');">
                                        <% =cat.Patio%></a> <a href="javascript:h('cat','Pool');">
                                            <% =cat.Pool%></a> <a href="javascript:h('cat','Porch');">
                                                <% =cat.Porch%></a> <a href="javascript:h('cat','Powder Room');">
                                                    <% =cat.Powder_Room%></a> <a href="javascript:h('cat','Staircase');">
                                                        <% =cat.Staircase%></a> <a href="javascript:h('cat','Wine Cellar');">
                                                            <% =cat.Wine_Cellar%></a>
                </div>
            </div>
            <div class="submenu" id="submenu3" style="display: none; width: 446px;">
                <div>
                    <a href="javascript:h('cat','Accessories and Decor');">
                        <% =cat.Accessories_and_Decor%></a> <a href="javascript:h('cat','Bath Products');">
                            <% =cat.Bath_Products%></a> <a href="javascript:h('cat','Bedroom Products');">
                                <% =cat.Bedroom_Products%></a> <a href="javascript:h('cat','Fabric');">
                                    <% =cat.Fabric%></a> <a href="javascript:h('cat','Floors');">
                                        <% =cat.Floors%></a> <a href="javascript:h('cat','Furniture');">
                                            <% =cat.Furniture%></a> <a href="javascript:h('cat','Hardware');">
                                                <% =cat.Hardware%></a> <a href="javascript:h('cat','Home Office Products');">
                                                    <% =cat.Home_Office_Products%></a> <a href="javascript:h('cat','Housekeeping');">
                                                        <% =cat.Housekeeping%></a> <a href="javascript:h('cat','Kids Products');">
                                                            <% =cat.Kids_Products%></a> <a href="javascript:h('cat','Kitchen Products');">
                                                                <% =cat.Kitchen_Products%></a> <a href="javascript:h('cat','Lighting');">
                                                                    <% =cat.Lighting%></a><br />
                    <a href="javascript:h('cat','Outdoor Products');">
                        <% =cat.Outdoor_Products%></a> <a href="javascript:h('cat','Storage and Organization');">
                            <% =cat.Storage_and_Organization%></a> <a href="javascript:h('cat','Tabletop');">
                                <% =cat.Tabletop%></a> <a href="javascript:h('cat','Window Treatment');">
                                    <% =cat.Window_Treatment%></a> <a href="javascript:h('cat','Windows and Doors');">
                                        <% =cat.Windows_and_Doors%></a>
                </div>
            </div>
            <div class="submenu" id="submenu4" style="display: none">
                <div>
                    <a href="javascript:rh('style');">
                        <% =cat.All%></a><br />
                    <a href="javascript:h('style','Asian');">
                        <% =cat.Asian%></a><br />
                    <a href="javascript:h('style','Contemporary');">
                        <% =cat.Contemporary%></a><br />
                    <a href="javascript:h('style','Eclectic');">
                        <% =cat.Eclectic%></a><br />
                    <a href="javascript:h('style','Mediterranean');">
                        <% =cat.Mediterranean%></a><br />
                    <a href="javascript:h('style','Modern');">
                        <% =cat.Modern%></a><br />
                    <a href="javascript:h('style','Traditional');">
                        <% =cat.Traditional%></a><br />
                    <a href="javascript:h('style','Tropical');">
                        <% =cat.Tropical%></a><br />
                </div>
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
        <div class="point-lable">
            <img src="http://cdn.pinjimu.com/img/pinjimu/points-bar.jpg" width="650"
                height="36" /></div>
        <div class="points-title">
            <%=strings.Ways_to_Redeem %></div>
        <div class="RedeemContianer">
            <div class="RedeemBox">
                <div class="RedeemIcon">
                    <div class="icon">
                        <img src="http://cdn.pinjimu.com/img/pinjimu/donate-icon.png" /></div>
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
                        <img src="http://cdn.pinjimu.com/img/pinjimu/gift-icon.png" />
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
                        <img src="http://cdn.pinjimu.com/img/pinjimu/roullete-icon2.png" /></div>
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
                    <input type="button" class="styled-button-1" name="logint" style="cursor: pointer;"
                        value="Play" />
                </div>
            </div>
        </div>
    </div>
    <ctl:Pin runat="server"/>
    <div id="gallery" class="gallery">
    </div>
    <div id="boardsCont" class="boards" style="display: none; top: 0px;">
    </div>
    <div id="content" style="display: none; margin-top: 120px; position: absolute; width: 100%;">
    </div>
    <div id="users" class="user" style="display: none; top: 0px;">
    </div>
    <div id="articlesCont" style="display: none;">
        <h1 id="articlesheader" class="art-heading1">
            <%=strings.Articles%>
        </h1>
        <div id="articlesLayout">
        </div>
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
    <
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
        <%-- <div class="socialButtons">
    <div class="fbTw fl"> <a name="noblock" href="https://www.facebook.com/dialog/oauth?client_id=446480175379142&redirect_uri=http%3A%2F%2Ffreshpin.com%2Fhomedecor%2Ffbloggedin.html&scope=user_about_me&response_type=token">
    <img id="logwfb" style="cursor: pointer;" src="http://cdn.pinjimu.com/img/logInWithFB.gif"
    /></a> </div> </div>--%>
        <div class="loginBar">
            <img id="logbar" src="http://cdn.pinjimu.com/img/pinjimu/login_bar.png" /></div>
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
        <%=strings.Scroll_To_Top%></div>
</body>
</html>
