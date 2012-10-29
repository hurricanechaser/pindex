<%@ Page Language="C#" AutoEventWireup="true"%>
<%@ Import Namespace="PindexProd" %>
<!DOCTYPE html>
<html>
<head>
    <title><%=strings.FreshPin %> / <%=strings.Home %></title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-5">
    <!--[if (gt IE 6)&(lt IE 9)]><link rel="stylesheet" href="ie7-and-up_fa603afa.css" type="text/css" media="all" /><![endif]-->
    <link rel="icon" href="favicon.ico" />
    <link href="http://freshpin.com/cdn/style.css" rel="stylesheet" type="text/css" />
    <link href="http://freshpin.com/cdn/font.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-31949192-1']);
        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
    </script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.7.2.min.js"></script>
    <link href="http://freshpin.com/cdn/add.css" rel="stylesheet" type="text/css" />
    <script src="freshpin.js" type="text/javascript"></script>
    <link href="http://freshpin.com/cdn/dropdownMenu.css" rel="stylesheet" type="text/css" />
    <link href="http://freshpin.com/cdn/loggedin.css" rel="stylesheet" type="text/css" />
    <script src="freshpinloggedin.js" type="text/javascript"></script>
    <script src="freshpinhome.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://freshpin.com/cdn/scripts/jeresig-jquery.hotkeys-0451de1/jquery.hotkeys.js"></script>
    <script type="text/javascript" src="http://freshpin.com/cdn/scripts/scrolltopcontrol.js"></script>
    <script type="text/javascript" src="http://freshpin.com/cdn/scripts/jquery-jquery-tmpl/jquery.tmpl.min.js"></script>
    <script type="text/javascript" src="http://freshpin.com/cdn/scripts/desandro-masonry/jquery.masonry.min.js"></script>
    <script type="text/javascript" src="http://freshpin.com/cdn/scripts/jqmodal/jqModal.js"></script>
    <script type="text/javascript" src="http://freshpin.com/cdn/scripts/jquery-ui-1-1.8.18.custom/development-bundle/ui/minified/jquery.ui.core.min.js"></script>
    <script src="http://freshpin.com/cdn/scripts/jquery-ui-1-1.8.18.custom/development-bundle/ui/minified/jquery.ui.widget.min.js"
        type="text/javascript"></script>
    <script src="http://freshpin.com/cdn/scripts/jquery-ui-1-1.8.18.custom/development-bundle/ui/minified/jquery.ui.mouse.min.js"
        type="text/javascript"></script>
    <script src="http://freshpin.com/cdn/scripts/jquery-ui-1-1.8.18.custom/development-bundle/ui/minified/jquery.ui.draggable.min.js"
        type="text/javascript"></script>
    <script type="text/javascript" src="http://freshpin.com/cdn/scripts/jeresig-jquery.hotkeys-0451de1/jquery.hotkeys.js"></script>
    <script type="text/javascript" src="http://freshpin.com/cdn/scripts/cookies.js"></script>
    <script type="text/javascript" src="http://freshpin.com/cdn/scripts/jQuery-File-Upload/js/vendor/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="http://freshpin.com/cdn/scripts/jQuery-File-Upload/js/jquery.fileupload.js"></script>
    <script type="text/javascript" src="http://freshpin.com/cdn/scripts/jQuery-File-Upload/js/jquery.iframe-transport.js"></script>
    <script type="text/javascript" src="http://freshpin.com/cdn/scripts/farinspace-jquery.imgpreload-6e0e307/jquery.imgpreload.min.js"></script>
    <script type="text/javascript" src="http://freshpin.com/cdn/scripts/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="http://freshpin.com/cdn/scripts/jquery-hashchange/jquery.ba-hashchange.min.js"></script>
    <script id="_webengage_script_tag" type="text/javascript">
        /*window.webengageWidgetInit = window.webengageWidgetInit || function () {
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
        })(document);*/
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
            <div class="boardfooter" style="cursor:pointer;" name="edit" boardid="${id}">
                <strong>Edit</strong></div>
        </div>
    </div>
    </script>
    <script id="rank" type="text/x-jquery-tmpl">
    <ul class="box" >       
        <li class="img"><a href="javascript:void(0)" name="${i}"></a>
        </li>       
        <li style="display: block;paddingfont-size: 11px;color: #211922;font-family: "helvetica neue" ,arial,sans-serif;text-align: left;list-style: none;"><span>${Name}</span></li>        
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
            <img src="${url}?width=190" style="height:${height}px" alt="${title}" /></a>
        </li>
        <li> <div class="name"><span>${title}</span></div> </li>
    </ul>
    </script>
    <script id="picTemplate" type="text/x-jquery-tmpl">
        <ul class="box" >
            <li class="buttons">
                {{if editable == true && FreshPin.authenticated() == true}}
                <a href="javascript:void(0)" imageid="${ID}" bimid="${BIMID}" class="button" name="edit" >Edit</a>
                {{else}}
                <a href="javascript:void(0)" imageid="${ID}" bimid="${BIMID}" name="like"  class="buttonLike{{if liked}} buttonLikeDis{{/if}}">Like</a>
                {{/if}}
                <a href="javascript:void(0)" imageid="${ID}" bimid="${BIMID}" url="${url}?width=190" name="pin" class="buttonPin">Re-pin</a>
                <a href="javascript:void(0)" imageid="${ID}" style="display:none;" bimid="${BIMID}" name="comment" class="buttonComment">Comment</a>
            </li>
            <li class="img"><a href="#pin=${PinID}"> 
                                <img src="${url}?width=190" style="height:${getHeight($data)}px" /></a>
            </li>
            {{if isgamify($data) == true }}
            <li class="gamify">
                <img name="gmfht" src="http://freshpin.com/cdn/img/prize.png" bimid="${BIMID}" />
            </li>
            {{/if}} 
            <li class="boards">
                <div class="post-pin">
<div class="post-text">Post This To</div>
<div class="post-icons"> <img href="${getUrl($data,'p')}" class="pinit" src="http://freshpin.com/cdn/img/pinterest_logo.png" width="24" height="24" title="Pinterest" border="0" />&nbsp; 
  <img  class="pinit" src="http://freshpin.com/cdn/img/facebook_logo.png" width="24" height="24" title="Facebook" border="0" />&nbsp; 
  <img  class="pinit" src="http://freshpin.com/cdn/img/tumblr_logo.png" width="24" height="24" title="Tumblr" border="0" /></div>
</div>                       
        </li>
        <li> <div class="name"><span>${title}</span></div> </li>
         {{if comments && comments.length}}
                <li class="comments" >     
       {{tmpl(comments) "#comments"}}
       </li>  
       {{/if}}       
      <li class="addComment">       
        <span>
            <img src="${FreshPin.getAV()}" width="22" height="22" /></span>
        <textarea name="addc"  ></textarea>
        <a sc="sc" bimid="${BIMID}" href="javascript:void(0)">Comment</a>         
    </ul>
    </script>
    <script id="cats" type="text/x-jquery-tmpl">
   <a  href="#cat=${escape(getVal($data,'Name'))}" style="overflow:hidden;" >${Name}</a>   
    </script>
    <script id="comments" type="text/x-jquery-tmpl">
     <div>
      <a href="javascript:void(0)" ><img src="${getUplUImg($data)}?height=20"  /></a> <p><a href="javascript:void(0)" title="Flickr">${Name}</a> ${Comments}</p> 
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
<img src="${getUplUImg($data)}?width=36" style="display: block; width: 36px !important; height: auto !important; border-top-width: 0;border-right-width: 0; border-bottom-width: 0; border-left-width: 0;" /></a>
<a href="${getUD($data)}" style="font-weight: bold; color: #221919;text-decoration: none; outline: none; float: left; font-size: inherit; width: 198px;overflow: hidden; white-space: nowrap; text-overflow: ellipsis; margin-top: 7px;
 margin-right: 18px; margin-bottom: 0; margin-left: 10px;">${FirstName}</a><a name="rem" un="${Name}"  style="font-weight: bold;color: #524d4d; text-decoration: none; outline: none; position: relative; display: inline-block;
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
z-index: 2;">Remove</strong><span style="position: absolute; z-index: 1; top: -1px;
right: -1px; bottom: -1px; left: -1px; display: block; opacity: 1; border-radius: 6px;
-moz-border-radius: 6px; -webkit-border-radius: 6px; box-shadow: inset 0 1px rgba(255,255,255,0.35);
-moz-box-shadow: inset 0 1px
rgba(255,255,255,0.35); -webkit-box-shadow: inset 0 1px rgba(255,255,255,0.35); -moz-transition-property: opacity; -moz-transition-duration: 0.5s;
-moz-transition-timing-function: ease-in-out; -webkit-transition-property: opacity;
-webkit-transition-duration: 0.5s; -webkit-transition-timing-function: ease-in-out;
font-size: 18px; filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfafb', endColorstr='#f0eded');
border-top-color: #bbb; border-right-color: #bbb; border-bottom-color: #bbb;
border-left-color: #bbb; border-top-style: solid; border-right-style: solid;
border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #f0eded;
background-position: 0%
0%;"></span></a>
        <div style="clear:both;" />
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
            Points: '<%=strings.Points%>'
        };
    </script>

	<style id="profile_css" type="text/css">

 html { background: url("http://photogallery.botcodelocal.com/cdn/img/profile_bg.jpg") repeat scroll 0 0 #F7F5F5;}
        div, ul, ol, li, pre, fieldset, textarea, p {
        margin: 0;    padding: 0;}
        em, th {    font-style: normal;    font-weight: normal;}
        li {    list-style: none outside none;}    
    
        q:before, q:after {    content: "";}
        textarea {    font-family: inherit;    font-size: inherit;    font-weight: inherit;    outline: medium none;    resize: none;}
    
        body {    color: #211922;    font-family: "helvetica neue",arial,sans-serif;    font-size: 10px;}
    
        em {    font-style: italic;}
        strong {    font-weight: bold;}
  
        p {    line-height: 1.35em;    margin: 0 0 0.8em;}
        a {    color: #221919;    font-weight: bold;    outline: medium none;    text-decoration: none;}
        a:hover {    color: #CB2027;    text-decoration: underline;}
    
        textarea:focus {    background-color: #FFFFFF;    box-shadow: 0 1px 1px rgba(34, 29, 29, 0.1) inset;}
        .hidden {    display: none !important;}    
    
        .Button { min-width: 18px;  padding: 0.5em 0.625em 0.3em;}
        .Button.WhiteButton { background-color: #F0EDED; background-image: -moz-linear-gradient(center top , #FDFAFB, #F9F7F7 50%, #F6F3F4 50%, #F0EDED);    border-color: #BBBBBB; color: #524D4D; text-shadow: 0 1px rgba(255, 255, 255, 0.9);}
        .Button.Button11 { border-radius: 3px 3px 3px 3px; font-size: 11px;}
        .Button {-moz-transition: all 0.05s ease-in-out 0s;    border: 1px solid transparent;    border-radius: 0.3em 0.3em 0.3em 0.3em;
        box-shadow: 0 1px rgba(255, 255, 255, 0.8), 0 1px rgba(255, 255, 255, 0.35) inset;
        cursor:default;    display: inline-block;    font-family: "helvetica neue",arial,sans-serif;    font-weight: bold;
        line-height: 1em;    margin: 0;    padding: 0.45em 0.825em;    text-align: center;}
    
        #userProfile #ProfileHeader {background: none repeat scroll 0 0 rgba(34, 25, 25, 0.05); color: #534F4F; font-size: 13px; overflow: hidden; padding: 10px 0 8px; margin-top: 100px;}
        #userProfile #ProfileHeader .info {  height: 165px;   position: relative;    width: 698px;}
        #userProfile #ProfileHeader .info .ProfileImage {    float: left;    height: 165px;    max-width: 160px;    overflow: hidden;}
        #userProfile #ProfileHeader .info .ProfileImage img {    height: 100%;}
        #userProfile #ProfileHeader .info .content {    margin-left: 180px;    padding-right: 15px;}
        #userProfile #ProfileHeader .info h1 {    color: #534F4F;    font-size: 30px;    margin: 15px 0 10px;}
        #userProfile #ProfileHeader .info p {    line-height: 18px;}
        #userProfile #ProfileHeader .info p.noDescription:hover {    border: 1px solid #C9B5B5;    cursor: text;    margin: -4px 0 6px -4px;    padding: 3px;}
        #userProfile #ProfileHeader .info p.noDescription img {    margin-left: 8px;    margin-top: -4px;    vertical-align: middle;}
        #userProfile #ProfileHeader .info #editDescription {    display: none;}
        #userProfile #ProfileHeader .info #editDescription textarea {    font-size: 13px; height: 50px; margin-bottom: 5px;  min-height: 50px; width: 100%;}
        #userProfile #ProfileHeader .info #editDescription .Button {  float: right;}
        #userProfile #ProfileHeader .info #editDescription .CharacterCount {    float: right;    margin-right: 10px;    margin-top: 4px;}
        #userProfile #ProfileHeader .info #editDescription .CharacterCount.error {    font-size: 13px;    margin-top: 3px;}    
    
        .row > div {
        background: none repeat scroll 0 0 #FFFFFF;
        box-shadow: 0 1px 3px rgba(34, 25, 25, 0.5);
        float: left;
        }

    
        #ProfileLinks {    bottom: 18px;    color: #8C7E7E;    font-weight: 600;    overflow: hidden;    padding-top: 5px;    position: absolute;}
        #ProfileLinks li {    float: left;    margin-right: 3px;}
    
        .icons .addIcons {    display: inline-block;    height: 20px;    margin-top: -9px;    min-width: 130px;    padding: 8px 5px 0 0;}
        .icons .addIcons.Existing {    border-left: 1px solid #C9B5B5;    margin-left: 5px;    padding-left: 5px;}
        .icons .addIcons li {    margin: -3px 5px 0 0;}
        .icons .addIcons li .Button {    min-width: 18px;    padding: 0.5em 0.625em 0.3em;}
        .icons .addIcons li .WhiteButton {    color: #8C7E7E;}
        .icons .addIcons li .Form {    position: relative;}
        .icons .addIcons li .Form img.inputIcon {    left: 6px;    position: absolute;    top: 6px;}
        .icons .addIcons li .SplitInput { border-radius: 3px 0 0 3px;    font-size: 11px;    padding: 2px 8px 3px 18px;    width: 180px;}
        .icons .addIcons li .SplitButton { border-bottom-left-radius: 0; border-top-left-radius: 0; min-height: 22px; padding: 0.4em .625em 0.25em;    vertical-align: bottom;}
        .icons .addIcons li .SplitButton img {    height: 11px;}
    
        #userProfile #ContextBar {    margin-top: 15px;}
        .FixedContainer {    width: 934px;}
        .FixedContainer {    margin: 0 auto;    width: 852px;}
        .FixedContainer .StaticForm {    margin-top: 96px;}
    
        #userProfile #ContextBar { margin: 59px 0 15px; min-height: 24px; padding: 10px 0 12px;    position: relative;    text-shadow: 0 1px rgba(255, 255, 255, 0.9);}
        #userProfile #ContextBar a {    font-weight: 300;    text-decoration: underline;}
        #userProfile #ContextBar {    background: none repeat scroll 0 0 #FAF7F7;    border-bottom: medium none;    box-shadow: 0 1px #FFFFFF inset, 0 1px 3px rgba(34, 25, 25, 0.4);    line-height: 48px;    margin: 0;    overflow: hidden;    padding: 0;  }
        #userProfile #ContextBar.fixed {    left: 0;    right: 0;    top: -1px;}
        #userProfile #ContextBar .FixedContainer {    margin: 0 auto -1px;    overflow: hidden;}
        #userProfile #ContextBar a {    text-decoration: none;}
        #userProfile #ContextBar ul {    float: left;}
        #userProfile #ContextBar ul li {    float: left;}
        #userProfile #ContextBar ul li a {    border-top: 1px solid #FFFFFF;    color: #554747;    display: block;    font-size: 13px;    height: 48px;    padding: 0 15px;}
        #userProfile #ContextBar ul li a:hover {    background-color: #E1DFDF;    border-top-color: #E1DFDF;    color: #554747;}
        #userProfile #ContextBar ul li a:active {    background-color: #CB2027;    border-top-color: #CB2027;    color: #FFFFFF;    text-shadow: none;}
        #userProfile #ContextBar ul li a.selected {    background: -moz-linear-gradient(center top , #E0DCDB 0%, #F4F1F1 100%) repeat scroll 0 0 transparent;
        border-left: 1px solid #CDC5C5;    border-right: 1px solid #CDC5C5;    border-top: 1px solid #F3F3F2;    overflow: hidden;    position: relative;
        text-shadow: 0 1px 0 #FFFFFF;}
        #userProfile #ContextBar ul li a.selected:active {    color: #554747;}
        #userProfile #ContextBar ul li a.selected:before {    bottom: -10px;    box-shadow: 0 0 10px rgba(34, 25, 25, 0.1) inset;    content: "";    left: 0;
        position: absolute;    right: 0;    top: -10px;}
        #userProfile #ContextBar .action {    letter-spacing: -0.31em;    margin: 1px auto;    text-align: center;    width: 160px;    word-spacing: -0.43em;}
        #userProfile #ContextBar .action a {    letter-spacing: normal;    word-spacing: normal;}
        #userProfile #ContextBar .action .Tab, #ContextBar .action #RearrangeCancel {    display: inline-block;    height: 24px;    line-height: 24px;
        margin-top: -4px;    vertical-align: middle;}
        #userProfile #ContextBar .action .Tab {    margin-right: -1px;    padding: 0 8px;}
        #userProfile #ContextBar .action .Tab.Left {    border-radius: 4px 0 0 4px;}
        #userProfile #ContextBar .action .Tab.Right {    border-radius: 0 4px 4px 0;}
        #userProfile #ContextBar .action #RearrangeButton {    padding: 0 5px;}
        #userProfile #ContextBar .action #RearrangeButton em {    background: url("http://photogallery.botcodelocal.com/cdn/img/board_icon.png") no-repeat scroll center center transparent;
        display: block;    height: 24px;    width: 25px;}
        #userProfile #ContextBar .action #RearrangeButton.RedButton em {    background-image: url("http://photogallery.botcodelocal.com/cdn/img/board_confirm.png");}
        #userProfile #ContextBar .action #RearrangeCancel {    background: url("http://photogallery.botcodelocal.com/cdn/img/board_cancel.png") no-repeat scroll center center transparent;    margin-right: -25px;    overflow: hidden;    text-indent: -999px;    width: 25px;}
    
        #userProfile #ContextBar .Button {    font-weight: bold;}
    
    
        .tipsy {    background-image: url("tipsy.gif");    background-repeat: no-repeat;    font-size: 11px;    line-height: 13px;    opacity: 0.9;    padding: 5px;}
        .tipsy-inner {    background-color: #221919;    color: #FFFFFF;    max-width: 150px;    padding: 5px 8px;    text-align: center;}
        .tipsy-north {    background-position: center top;}
        .tipsy-south {    background-position: center bottom;}
        .tipsy-east {    background-position: right center;}
        .tipsy-west {    background-position: left center;}
</style>
    
</head>
<body>
    <div class="header">
        <div class="line1">
            <div class="queryBox">
                <input class="searchfield" type="text" value="Search over 500,000 Pins" id="query" />
                <input class="lg searchbutton" type="button" id="query_button" />
            </div>
            <div class="freshpinlogo">
                <a href=".">
                    <img src="logo.png" /></a>
                <div class="freshpinlogoTagline">
                     <% =strings.Banner %></div>
            </div>
            <div class="aboutus">
                <ul class="navigation">
                    <li><span class="nav" id="addtrigger" style="cursor: pointer;"><%=strings.Add %>+</span> </li>
                    <li><a id="about" href="about.aspx#about" class="nav"><% =strings.About %><span></span></a>
                        <ul>
                            <li><a href="about.aspx#help"><%=strings.Help %></a></li>
                            <li class="beforeDivider"><a href="about.aspx#copyright"><% =strings.Copyright %></a></li>
                        </ul>
                    </li>
                    <li id="logininfo" class="logininfo"></li>
                </ul>
            </div>
        </div>    
        <div class="line2">
            <div class="menu3">
                <li id="mlevel1"><a href="javascript:rcat('Wedding');">Wedding</a><span></span></li>
                <li id="mlevel2" class="menu"><a href="javascript:rcat('Fashion');">Fashion</a><span></span></li>
                <!-- <li style="width: 64px;" id="mlevel3" class="menu"><a href="">Nails</a> <span></span>
                </li>-->
                <li id="mlevel4" class="menu"><a href="javascript:rcat('Jewelry');">Jewelry</a> <span>
                </span></li>
                <li id="mlevel5" class="menu"><a href="javascript:rcat('Beauty');">Beauty</a> <span>
                </span></li>
                <li id="mlevel6" class="menu"><a href="javascript:rcat('Architectural');">Architectural</a>
                    <span></span></li>
                <li id="mlevel7" class="menu"><a href="javascript:rcat('Interiors');">Interiors</a><span
                    class="bgNone"></span></li>
                <li id="mlevel8" class="menu"><a href="javascript:rcat('Landscapes');">Landscapes</a><span
                    class="bgNone"></span></li>
                <!--               <li id="mlevel9" class="menu"><a href="">Marketing Agency</a> <span></span></li>
                <li id="mlevel10" class="menu"><a href="">Travel Agency</a> <span></span></li>
                <li class="menu"><a href="">Trade Agency</a> <span class="bgNone"></span></li>
                <li id="mlevel12" class="menu"><a href="">Family</a><span></span></li>
                <li><a href="">Showrooms</a><span class="bgNone"></span></li>
                <li id="mlevel13" style="font-weight: bold;"><a href="">Education Planner</a><span></span></li>-->
            </div>
        </div>
        <div class="submenu" id="submenu1" style="display: none">
            <div>
                <a class="none" href="javascript:rcat('Setting');"><strong>Setting</strong></a><br />
                <a href="javascript:rcat('Beach Wedding');">Beach Wedding</a> <a href="javascript:rcat('Boat Wedding');">
                    Boat Wedding</a> <a href="javascript:rcat('Mountain Wedding');">Mountain Wedding</a>
                <a href="javascript:rcat('Park Wedding');">Park Wedding</a> <a href="javascript:rcat('Other Settings');">
                    Other Settings</a>
                <br />
                <a class="none" href="javascript:rcat('Essentials');"><strong>Essentials</strong></a><br />
                <a href="javascript:rcat('Wedding Dress');">Wedding Dress</a> <a href="javascript:rcat('Bridesmaid Dress');">
                    Bridesmaid Dress</a> <a href="javascript:rcat('Wedding Invitation');">Wedding Invitation</a>
                <a href="javascript:rcat('Tuxedos');">Tuxedos</a> <a href="javascript:rcat('Wedding Flowers');">
                    Wedding Flowers</a> <a href="javascript:rcat('Wedding Cake');">Wedding Cake</a>
                <br />
                <a class="none" href="javascript:rcat('Special Touches');"><strong>Special Touches</strong></a><br />
                <a href="javascript:rcat('Photo Ideas');">Photo Ideas</a> <a href="javascript:rcat('Reception Decor');">
                    Reception Decor</a> <a href="javascript:rcat('Decoration Ideas');">Decoration Ideas</a>
                <a href="javascript:rcat('Other Touches');">Other Touches</a>
                <br />
            </div>
        </div>
        <div class="submenu" id="submenu2" style="display: none">
            <div>
                <a class="none" href="javascript:rcat('Women's Clothes');"><strong>Women's Clothes</strong></a><br />
                <a href="javascript:rcat('Jeans');">Jeans</a> <a href="javascript:rcat('Dresses');">
                    Dresses</a> <a href="javascript:rcat('Intimates');">Intimates</a> <a href="javascript:rcat('Jackets');">
                        Jackets</a> <a href="javascript:rcat('Outerwear');">Outerwear</a> <a href="javascript:rcat('Pants & Shorts');">
                            Pants & Shorts</a> <a href="javascript:rcat('Skirts');">Skirts</a> <a href="javascript:rcat('Sportswear');">
                                Sportswear</a> <a href="javascript:rcat('Suits');">Suits</a> <a href="javascript:rcat('Sweaters');">
                                    Sweaters</a> <a href="javascript:rcat('Swimwear');">Swimwear</a>
                <a href="javascript:rcat('Tops');">Tops</a> <a href="javascript:rcat('Maternity');">
                    Maternity</a>
                <br />
                <a class="none" href="javascript:rcat('Women/'s Bags');"><strong>Women's Bags</strong></a><br />
                <a href="javascript:rcat('Backpacks');">Backpacks</a> <a href="javascript:rcat('Clutches');">
                    Clutches</a> <a href="javascript:rcat('Evening');">Evening</a> <a href="javascript:rcat('Hobos');">
                        Hobos</a> <a href="javascript:rcat('Satchels');">Satchels</a> <a href="javascript:rcat('Shoulder');">
                            Shoulder</a> <a href="javascript:rcat('Totes');">Totes</a> <a href="javascript:rcat('Wallets');">
                                Wallets</a>
                <br />
                <a class="none" href="javascript:rcat('Accessories');"><strong>Accessories</strong></a><br />
                <a href="javascript:rcat('Belts');">Belts</a> <a href="javascript:rcat('Gloves');">Gloves</a>
                <a href="javascript:rcat('Hats');">Hats</a> <a href="javascript:rcat('Scarves');">Scarves</a>
                <a href="javascript:rcat('Sunglasses');">Sunglasses</a>
                <br />
                <a class="none" href="javascript:rcat('Shoes');"><strong>Shoes</strong></a><br />
                <a href="javascript:rcat('Athletic');">Athletic</a> <a href="javascript:rcat('Boots');">
                    Boots</a> <a href="javascript:rcat('Evening');">Evening</a> <a href="javascript:rcat('Flats');">
                        Flats</a> <a href="javascript:rcat('Mules&Clogs');">Mules & Clogs</a> <a href="javascript:rcat('Platform');">
                            Platform</a> <a href="javascript:rcat('Pumps');">Pumps</a> <a href="javascript:rcat('Sandals');">
                                Sandals</a> <a href="javascript:rcat('Wedges');">Wedges</a>
                <br />
                <a class="none" href="javascript:rcat('Kids');"><strong>Kids</strong></a><br />
                <a href="javascript:rcat('BoysClothes');">Boy's Clothes</a> <a href="javascript:rcat('BoysShoes');">
                    Boy's Shoes</a> <a href="javascript:rcat('GirlsClothes');">Girl's Clothes</a>
                <a href="javascript:rcat('Girl's Shoes');">Girl's Shoes</a>
                <br />
                <a class="none" href="javascript:rcat('Mens');"><strong>Mens</strong></a><br />
                <a href="javascript:rcat('Jeans');">Jeans</a> <a href="javascript:rcat('Outerwear');">
                    Outerwear</a> <a href="javascript:rcat('Pants');">Pants</a> <a href="javascript:rcat('Shirts');">
                        Shirts</a> <a href="javascript:rcat('Shoes');">Shoes</a> <a href="javascript:rcat('Shorts');">
                            Shorts</a> <a href="javascript:rcat('Sweater');">Sweater</a> <a href="javascript:rcat('Swimwear');">
                                Swimwear</a> <a href="javascript:rcat('Tie');">Tie</a>
                <br />
                <a class="none" href="javascript:rcat('Ensembles & Sets');"><strong>Ensembles & Sets</strong></a><br />
                <a href="javascript:rcat('Womens');">Womens</a> <a href="javascript:rcat('Mens');">Mens</a>
                <a href="javascript:rcat('Kids');">Kids</a> <a href="javascript:rcat('Other');">Other</a>
            </div>
        </div>
        <div class="submenu" id="submenu3" style="display: none">
            <div>
                <a class="none" href="javascript:rcat('Color');"><strong>Color</strong></a><br />
                <a href="javascript:rcat('Multi(3+)');">Multi (3+)</a> <a href="javascript:rcat('Two-Tone');">
                    Two-Tone</a> <a href="javascript:rcat('Metallic');">Metallic</a> <a href="javascript:rcat('Red');">
                        Red</a> <a href="javascript:rcat('Orange');">Orange</a> <a href="javascript:rcat('Yellow');">
                            Yellow</a> <a href="javascript:rcat('Green');">Green</a> <a href="javascript:rcat('Teal');">
                                Teal</a> <a href="javascript:rcat('Blue');">Blue</a> <a href="javascript:rcat('Purple');">
                                    Purple</a> <a href="javascript:rcat('Pink');">Pink</a> <a href="javascript:rcat('White');">
                                        White</a> <a href="javascript:rcat('Gray');">Gray</a> <a href="javascript:rcat('Black');">
                                            Black</a> <a href="javascript:rcat('Brown');">Brown</a>
                <br />
                <a class="none" href="javascript:rcat('Finish');"><strong>Finish</strong></a><br />
                <a href="javascript:rcat('Shiny');">Shiny</a> <a href="javascript:rcat('Matte');">Matte</a>
                <a href="javascript:rcat('Glitter');">Glitter</a>
                <br />
                <a class="none" href="javascript:rcat('Difficulty');"><strong>Difficulty</strong></a><br />
                <a href="javascript:rcat('Simple');">Simple</a> <a href="javascript:rcat('Medium');">
                    Medium</a> <a href="javascript:rcat('Hard');">Hard</a> <a href="javascript:rcat('Crazy');">
                        Crazy</a>
                <br />
                <a class="none" href="javascript:rcat('Style');"><strong>Style</strong></a><br />
                <a href="javascript:rcat('Pattern');">Pattern</a> <a href="javascript:rcat('Picture');">
                    Picture</a> <a href="javascript:rcat('Gradient');">Gradient</a> <a href="javascript:rcat('3D');">
                        3D</a> <a href="javascript:rcat('Airbrush');">Airbrush</a> <a href="javascript:rcat('FreeStyle');">
                            FreeStyle</a> <a href="javascript:rcat('Stamp');">Stamp</a> <a href="javascript:rcat('Stilleto');">
                                Stilleto</a>
                <br />
                <a class="none" href="javascript:rcat('With People');"><strong>With People</strong></a><br />
                <a href="javascript:rcat('Face');">Face</a> <a href="javascript:rcat('Outfit');">Outfit</a>
                <a href="javascript:rcat('Celebrities');">Celebrities</a> <a href="javascript:rcat('Pedicures');">
                    Pedicures</a> <a href="javascript:rcat('Accessories');">Accessories</a>
                <br />
                <a class="none" href="javascript:rcat('How-To');"><strong>How-To</strong></a><br />
                <a href="javascript:rcat('HowToGallery');">How To Gallery</a>
                <br />
                <a class="none" href="javascript:rcat('Products');"><strong>Products</strong></a><br />
                <a href="javascript:rcat('Nail Polish');">Nail Polish</a> <a href="javascript:rcat('Pattern Tools');">
                    Pattern Tools</a> <a href="javascript:rcat('Manicure');">Manicure</a> <a href="javascript:rcat('Other');">
                        Other</a>
                <br />
            </div>
        </div>
        <div class="submenu" id="submenu4" style="display: none">
            <div>
                <a class="none" href="javascript:rcat('Wedding Jewelry');"><strong>Wedding Jewelry</strong></a><br />
                <a href="javascript:rcat('Engagement Rings');">Engagement Rings</a> <a href="javascript:rcat('Wedding Sets');">
                    Wedding Sets</a> <a href="javascript:rcat('Anniversary Rings');">Anniversary Rings</a>
                <br />
                <a class="none" href="javascript:rcat('Regular Jewelry');"><strong>Regular Jewelry</strong></a><br />
                <a href="javascript:rcat('Rings');">Rings</a> <a href="javascript:rcat('Necklaces');">
                    Necklaces</a> <a href="javascript:rcat('Earrings');">Earrings</a> <a href="javascript:rcat('Bracelets');">
                        Bracelets</a> <a href="javascript:rcat('Watches');">Watches</a>
                <br />
            </div>
        </div>
        <div class="submenu" id="submenu5" style="display: none">
            <div>
                <a class="none" href="javascript:rcat('Beauty');"><strong>Beauty</strong></a>
                <br />
                <a class="none" href="javascript:rcat('Beauty Products');"><strong>Beauty Products</strong></a><br />
                <a href="javascript:rcat('Bath & Body');">Bath & Body</a> <a href="javascript:rcat('Fragrances');">
                    Fragrances</a> <a href="javascript:rcat('Hair Accessories');">Hair Accessories</a>
                <a href="javascript:rcat('HairCare');">Hair Care</a> <a href="javascript:rcat('Make-Up');">
                    Make-Up</a> <a href="javascript:rcat('Skin Care');">Skin Care</a> <a href="javascript:rcat('Tools');">
                        Tools</a>
                <br />
                <a class="none" href="javascript:rcat('Beauty Styles');"><strong>Beauty Styles</strong></a><br />
                <a href="javascript:rcat('HairStyles');">HairStyles</a> <a href="javascript:rcat('Hair - How To');">
                    Hair - How To</a> <a href="javascript:rcat('Nails');">Nails</a> <a href="javascript:rcat('Toes');">
                        Toes</a> <a href="javascript:rcat('Others');">Others</a>
                <br />
                <a class="none" href="javascript:rcat('Tatoos');"><strong>Tatoos</strong></a><br />
                <a href="javascript:rcat('Men');">Men</a> <a href="javascript:rcat('Women');">Women</a>
                <br />
            </div>
        </div>
        <div class="submenu" id="submenu6" style="display: none">
            <div>
                <a class="none" href="javascript:rcat('Styles');"><strong>Styles</strong></a><br />
                <a href="javascript:rcat('Exterior');">Exterior</a> <a href="javascript:rcat('Interior');">
                    Interior</a>
                <br />
                <a class="none" href="javascript:rcat('Essentials');"><strong>Essentials</strong></a><br />
                <a href="javascript:rcat('Doors');">Doors</a> <a href="javascript:rcat('Windows');">
                    Windows</a> <a href="javascript:rcat('Stairs');">Stairs</a> <a href="javascript:rcat('Flooring');">
                        Flooring</a> <a href="javascript:rcat('Lighting');">Lighting</a> <a href="javascript:rcat('Roof');">
                            Roof</a> <a href="javascript:rcat('Tiles');">Tiles</a> <a href="javascript:rcat('Color Schemes');">
                                Color Schemes</a>
                <br />
                <a class="none" href="javascript:rcat('Details');"><strong>Details</strong></a><br />
                <a href="javascript:rcat('Fixtures');">Fixtures</a> <a href="javascript:rcat('Trim');">
                    Trim</a>
                <br />
            </div>
        </div>
        <div class="submenu" id="submenu7" style="display: none">
            <div>
            </div>
        </div>
        <div class="submenu" id="submenu8" style="display: none">
            <div>
            </div>
        </div>
        <!-- <div class="submenu9" id="submenu9" style="display: none">
            <div>
                <a class="none" href="javascript:rcat('Products');"><strong>Products</strong></a><br />
            </div>
        </div>
        <div class="submenu10" id="submenu10" style="display: none">
            <div>
                <a class="none" href="javascript:rcat('Places');"><strong>Places</strong></a><br />
                <a href="javascript:rcat('Honeymoons');">Honeymoons</a> <a href="javascript:rcat('Family Vacations');">
                    Family Vacations</a> <a href="javascript:rcat('Singles Spots');">Singles Spots</a>
                <a href="javascript:rcat('Cruises');">Cruises</a>
                <br />
            </div>
        </div>
        <div class="submenu12" id="submenu12" style="display: none">
            <div>
                <a class="none" href="javascript:rcat('Core Values');"><strong>Core Values</strong></a><br />
                <a href="javascript:rcat('Words of Love');">Words of Love</a> <a href="javascript:rcat('Words to Live By');">
                    Words to Live By</a>
                <br />
                <a class="none" href="javascript:rcat('Food');"><strong>Food</strong></a><br />
                <a href="javascript:rcat('Breakfast');">Breakfast</a> <a href="javascript:rcat('Lunch');">
                    Lunch</a> <a href="javascript:rcat('Dinner');">Dinner</a>
                <br />
                <a class="none" href="javascript:rcat('Fun');"><strong>Fun</strong></a><br />
                <a href="javascript:rcat('Games');">Games</a> <a href="javascript:rcat('Activities');">
                    Activities</a> <a href="javascript:rcat('Dates');">Dates</a> <a href="javascript:rcat('Vacations');">
                        Vacations</a>
                <br />
                <a class="none" href="javascript:rcat('Learning');"><strong>Learning</strong></a><br />
                <a href="javascript:rcat('Books');">Books</a> <a href="javascript:rcat('Character');">
                    Character</a>
            </div>
        </div>
        <div class="submenu13" id="submenu13" style="display: none">
            <div>
                <a class="none" href="javascript:rcat('Goal');"><strong>Goal</strong></a><br />
                <a href="javascript:rcat('Remedial');">Remedial</a> <a href="javascript:rcat('Strengths');">
                    Strengths</a> <a href="javascript:rcat('Core Skills');">Core Skills</a>
                <br />
                <a class="none" href="javascript:rcat('Books');"><strong>Books</strong></a><br />
                <a href="javascript:rcat('Reference');">Reference</a> <a href="javascript:rcat('Exploratory');">
                    Exploratory</a> <a href="javascript:rcat('Curriculum');">Curriculum</a>
                <br />
                <a class="none" href="javascript:rcat('Exercises');"><strong>Exercises</strong></a><br />
                <a href="javascript:rcat('Workbooks');">Workbooks</a>
                <br />
                <a class="none" href="javascript:rcat('Mental Games');"><strong>Mental Games</strong></a><br />
                <a href="javascript:rcat('Math');">Math</a> <a href="javascript:rcat('Word');">Word</a>
                <a href="javascript:rcat('Vocabulary');">Vocabulary</a>
            </div>
        </div>-->
   </div>

    <div id="userProfile">
        <div id="ProfileHeader">
         <div class="FixedContainer row clearfix">
            <div class="info">                
                <a href="" class="ProfileImage" target="_blank" id="p_image_href">
                    <!--<img src="" alt="" width=200 height=200 id="p_image">-->
                </a>
                    <div class="content">                    
                    <h1 id="p_name"></h1> 
                    <p class="colormuted" id="p_about"></p>
                    <ul id="ProfileLinks" class="icons">
                        <li>
                            <ul class="addIcons">
                                    <li>
                                        <button class="Button Button11 WhiteButton addFacebook" type="button">
                                            <img src="http://freshpindev.botcodelocal.com/cdn/img/profile_fb.png" />
                                        </button>
                                    </li>
                                    <li>
                                        <button class="Button Button11 WhiteButton addTwitter" type="button">
                                            <img src="http://freshpindev.botcodelocal.com/cdn/img/profile_tw.png" />
                                        </button>
                                    </li>
                                    <li>
                                        <button class="Button Button11 WhiteButton addWebsite" type="button">
                                            <img src="http://freshpindev.botcodelocal.com/cdn/img/profile_web.png" />
                                        </button>
                                    </li>
                                    <li>
                                        <button class="Button Button11 WhiteButton addLocation" type="button">
                                            <img src="http://freshpindev.botcodelocal.com/cdn/img/profile_loc.png" />
                                        </button>
                                    </li>
                            </ul>
                        </li>                        
                    </ul>
                </div>
            </div>           
           </div>     
        </div>  

        <div id="ContextBar" class="container sticky">
        <div class="FixedContainer">
            <ul class="links">             
                <li> <a href=".#boards" id="p_board_cnt" class=""></a></li>
                <li> <a href=".#filter=pins" id="p_pins_cnt" class=""></a> </li>
                <li> <a href=".#filter=likes" id="p_likes_cnt" class=""></a></li>
                <!--<li> <a href="/soura/activity/"> Activity </a> </li>-->
            </ul>        

            <div class="action">                
                <a class="Button13 Button WhiteButton Left Tab" href=".#settings">Edit Profile</a>
                <!--<a original-title="" id="RearrangeButton" class="Button13 Button WhiteButton Right Tab" href="" tooltip="&lt;strong&gt; Rearrange Boards&lt; /strong&gt;"><em></em></a>-->
              <!--<a id="RearrangeCancel" class="close hidden" tooltip="&lt;strong&gt;Cancel&lt;/strong&gt;" href="#">close</a>                -->
            </div>            
        </div>
    </div>      
    </div>
	
    <div class="gallery">
    </div>
    <div id="articlesCont" style="display: none;">
        <h1 id="articlesheader" class="art-heading1">
             <%=cat.Articles %>
        </h1>
        <div id="articlesLayout">
        </div>
    </div>
    <div id="boardsCont" class="boards">
    </div>
    <div id="content" style="margin-top: 100px;">
    </div>
    <!-- <div class="pinterest">
        <img src="http://freshpin.com/cdn/img/load2.gif" class="load" />
        <img class="close" id="close" src="http://freshpin.com/cdn/img/closediag.png"
            src="http://freshpin.com/cdn/img/closediag.png" src="http://freshpin.com/cdn/img/closediag.png" />
        <iframe class="popup" src="about:blank;" name="popup" id="popup"></iframe>
    </div>-->
    <div id="add" class="popup add">
        <div class="modal">
            <div class="header1">
                <div class="title">
                    <h2>
                        <%=strings.Add %></h2>
                </div>
                <div id="addclose" class="close">
                    <a href="javascript:void(0)">x</a></div>
            </div>
            <div class="yellowStripe">
                <%=strings.Pin_1 %> <a href="javascript:void(0)">
                    <%=strings.Pin_2 %></a>.</div>
            <div class="openlink">
                <div id="apcont">
                    <a class="cell1" id="addpint" href="javascript:void(0)">
                        <div class="icon pin">
                        </div>
                        <span><%=strings.Pin_3 %></span> </a>
                </div>
                <div id="upcont">
                    <a class="cell1" id="uploadpint" href="javascript:void(0)">
                        <div class="icon arrow">
                        </div>
                        <span><%=strings.Pin_4 %></span> </a>
                </div>
                <div>
                    <a class="cell1" href="javascript:void(0)" id="createboard">
                        <div class="icon board">
                        </div>
                        <span><%=strings.Board_1 %></span> </a>
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
                    <a href="javascript:void(0)">x</a></div>
            </div>
            <div class="cont">
                <div class="addPinRightPane">
                    <textarea id="commentDesc"></textarea>
                    <span><a class="redButton" id="saveComment" href="javascript:void(0)"><span><%=strings.Comment %></span></a></span>
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
                    <a href="javascript:void(0)">x</a></div>
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
                    <textarea id="RPDesc"></textarea>
                    <span><a class="redButton" id="saveRP" href="javascript:void(0)"><span><%=strings.Pin_5 %></span></a></span>
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
                        <%=strings.Pin_4 %></h2>
                </div>
                <div id="uploadPinclose" class="close">
                    <a href="javascript:void(0)">x</a></div>
            </div>
            <div class="cont">
                <div class="findImage">
                    <input class="whiteBut" id="fu" href="javascript:void(0)" value="<%=strings.Browse %>".." type="file" />
                    <div class="loaderImg" id="lmuplimg" style="display: none">
                        <img src="http://freshpin.com/cdn/img/load2.gif" /></div>
                </div>
                <div style="clear: both;">
                </div>
                <div class="pinIt">
                    <div class="uploadCont">
                        <div class="uploadImages">
                            <img id="uploadedImage" /></div>
                    </div>
                </div>
                <div class="addPinRightPane">
                    <div class="PinForm">
                        <div class="selectBox">
                            <a class="select" style="height: 30px; display: block;" id="selectUP" href="javascript:void(0)">
                                <span class="title" id="titleUP">Wedding Planner</span> <span class="rightArrow">
                                </span></a>
                            <div class="dropDownBox" id="dropDownBoxUP">
                                <ul>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <textarea id="uplPDesc" elastic="true"></textarea>
                    <span><a class="redButton" id="saveUploadPin" href="javascript:void(0)"><span><%=strings.Pin_5 %></span></a></span>
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
                        <%=strings.Pin_3 %></h2>
                </div>
                <div id="addPinclose" class="close">
                    <a href="javascript:void(0)">x</a></div>
            </div>
            <div class="cont">
                <div class="findImage" id="fImages">
                    <input type="text" name="textfield" id="url" />
                    <span><a class="whiteBut" id="fImages" href="javascript:void(0)"><span><%=strings.Images_1 %></span></a></span>
                    <div class="loaderImg" id="lmfindimages" style="display: none">
                        <img src="http://freshpin.com/cdn/img/load2.gif" /></div>
                </div>
                <div style="clear: both;">
                </div>
                <div class="pinIt">
                    <div class="uploadCont">
                        <div class="uploadImages">
                            <img id="urlimages" />
                        </div>
                        <div class="prevNext">
                            <div class="prev" id="prev">
                                <a href="javascript:void(0)"><%=strings.Prev %></a></div>
                            <div class="next" id="next">
                                <a href="javascript:void(0)"><%=strings.Next %></a></div>
                        </div>
                    </div>
                    <div class="loading" id="lmpreloadimages" style="display: none">
                        <img src="http://freshpin.com/cdn/img/load2.gif" /><br />
                        <span><%=strings.Loading %>....</span>
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
                    <textarea id="addPDesc" elastic="true"></textarea>
                    <span><a class="redButton" id="saveAddPin" href="javascript:void(0)"><span><%=strings.Pin_5 %></span></a></span>
                    <label id="addPCharCount">
                        500</label>
                </div>
            </div>
            <div style="clear: both;">
            </div>
        </div>
    </div>
    <div id="CreateBoard" class="popup CreateBoard">
        <div class="modal">
            <div class="header1">
                <div class="title">
                    <h2>
                        <%=strings.Board_1 %></h2>
                </div>
                <div id="CreateBoardclose" class="close">
                    <a href="javascript:void(0)">x</a></div>
            </div>
            <div class="cont">
                <div class="pinIt">
                    <p>
                        <label>
                            <%=strings.Board_2 %></label>
                        <input type="text" name="textfield" id="cbname" />
                    </p>
                    <label>
                        <%=strings.Pin_6 %>?</label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <input type="radio" checked="checked" validation="none" value="me" name="change_BoardCollaborators">
                        <span style="padding-left: 25px;"><%=strings.Just_Me %></span>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <input type="radio" value="multiple" validation="block" name="change_BoardCollaborators">
                        <span style="padding-left: 25px;"><%=strings.Me %> + <%=strings.Contributors %></span>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <label id="contributorslist">
                    </label>
                    <div style="clear: both;">
                    </div>
                    <div class="whoCanpin" id="emailBox">
                        <input type="text" name="textfield" id="contributor" />
                        <span><a class="whiteBut" id="contribute" href="javascript:void(0)"><span><%=strings.Add %></span></a></span>
                    </div>
                    <div>
                        <a class="redButton" id="saveCreateBoard" href="javascript:void(0)"><span><%=strings.Board_1 %></span></a></div>
                    <label class="labelBox" id="cbselcat">
                        <%=strings.Cat_1 %></label>
                </div>
                <div class="category" id="boardcategory" style="visibility: hidden">
                    <h2>
                        <%=strings.Cat_2 %></h2>
                    <ul class="scroll" id="cbcat">
                    </ul>
                    <div class="subBox" id="cbsubcat" style="display: none">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="editboard" class="popup editboard">
        <div class="modal">
            <div class="header1">
                <div class="title">
                    <h2>
                        <%=strings.Board_3 %></h2>
                </div>
                <div id="editboardclose" class="close">
                    <a href="javascript:void(0)">x</a></div>
            </div>
            <div class="cont">
                <div style="margin-top: 0; margin-right: auto; margin-bottom: 0; margin-left: auto;
                    float: left; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                    <ul style="margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0;
                        padding-right: 0; padding-bottom: 0; padding-left: 0;">
                        <!-- Board Title -->
                        <li style="display: block; font-size: 21px; font-weight: 300; clear: both; color: #8c7e7e;
                            text-shadow: 0 1px rgba(255,255,255,0.9); border-top-style: solid; border-top-width: 1px;
                            border-top-color: rgba(255,255,255,0.7); border-bottom-style: solid; border-bottom-width: 1px;
                            border-bottom-color: rgba(34,25,25,0.1); float: left; width: 100%; margin-top: 0;
                            margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 15px; padding-right: 0;
                            padding-bottom: 15px; padding-left: 0; list-style-type: none;">
                            <div style="width: 150px; float: left; margin-top: 0; margin-right: 0; margin-bottom: 0;
                                margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                                <label for="id_name" style="display: inline-block; line-height: 1.4em; font-size: 18px;
                                    float: left; width: 150px; padding-top: 7px; vertical-align: top;">
                                    <%=strings.Title %></label></div>
                            <div style="float: left; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                                padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                                <input type="text" name="name" value="me 2" id="id_name" style="font-family: inherit;
                                    font-size: 18px; font-weight: 300; resize: none; outline: none; line-height: 1.4;
                                    color: #221919; box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px #fff; -moz-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px #fff;
                                    -webkit-box-shadow: inset 0 1px rgba(34,25,25,0.15),
0 1px #fff; display: inline-block; box-sizing: border-box; -moz-box-sizing: border-box; -ms-box-sizing: border-box;
                                    -webkit-box-sizing: border-box; border-radius: 6px; -moz-border-radius: 6px;
                                    -webkit-border-radius: 6px; -moz-transition: all 0.08s
ease-in-out; -webkit-transition: all 0.08s ease-in-out; min-width: 375px; margin-top: 0; margin-right: 0; margin-bottom: 0;
                                    margin-left: 0; padding-top: 6px; padding-right: 12px; padding-bottom: 6px; padding-left: 12px;
                                    border-top-color: #ad9c9c; border-right-color: #ad9c9c; border-bottom-color: #ad9c9c;
                                    border-left-color: #ad9c9c; border-top-style: solid; border-right-style: solid;
                                    border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                                    border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #fff;" />
                            </div>
                        </li>
                        <li style="display: block; font-size: 21px; font-weight: 300; clear: both; color: #8c7e7e;
                            text-shadow: 0 1px
rgba(255,255,255,0.9); border-top-style: solid; border-top-width: 1px; border-top-color: rgba(255,255,255,0.7);
                            border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: rgba(34,25,25,0.1);
                            float: left; width: 100%; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                            padding-top: 15px; padding-right: 0; padding-bottom: 15px; padding-left: 0; list-style-type: none;">
                            <input type="hidden" name="category" value="film_music_books" id="id_category" style="font-family: inherit;
                                font-size: inherit; font-weight: inherit; resize: none; outline: none; line-height: 1em;
                                color: #8c7e7e; box-shadow: inset
0 0 2px rgba(255,255,255,0.75); -moz-box-shadow: inset 0 0 2px rgba(255,255,255,0.75); -webkit-box-shadow: inset 0 0 2px rgba(255,255,255,0.75);
                                margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 5px;
                                padding-right: 5px; padding-bottom: 5px; padding-left: 5px; border-top-color: #d1cdcd;
                                border-right-color: #d1cdcd; border-bottom-color: #d1cdcd; border-left-color: #d1cdcd;
                                border-top-style: solid; border-right-style: solid; border-bottom-style: solid;
                                border-left-style: solid; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px;
                                border-left-width: 1px; background-color: #fcf9f9;" />
                            <div style="width: 150px; float: left; margin-top: 0; margin-right: 0; margin-bottom: 0;
                                margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                                <label style="display: inline-block; line-height: 1.4em; font-size: 18px; float: left;
                                    width: 150px; padding-top: 7px; vertical-align: top;">
                                    <%=strings.Cat %></label></div>
                            <div style="float: left; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                                padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                                <div style="display: none; position: fixed; z-index: 9998; top: 0; right: 0; bottom: 0;
                                    left: 0; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0;
                                    padding-right: 0; padding-bottom: 0; padding-left: 0;">
                                </div>
                                <div id="CategoryPicker" style="width: 337px; position: relative; display: block;
                                    filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fffcfc',
endColorstr='#f0eded'); border-radius: 6px; -moz-border-radius: 6px; -webkit-border-radius: 6px; box-shadow: inset 0 1px 1px rgba(34,25,25,0.1),
0 1px #fff; -moz-box-shadow: inset 0 1px 1px rgba(34,25,25,0.1), 0 1px #fff; -webkit-box-shadow: inset 0 1px 1px rgba(34,25,25,0.1), 0 1px #fff;
                                    margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 6px;
                                    padding-right: 24px; padding-bottom: 6px; padding-left: 12px; border-top-color: #ad9c9c;
                                    border-right-color: #ad9c9c; border-bottom-color: #ad9c9c; border-left-color: #ad9c9c;
                                    border-top-style: solid; border-right-style: solid; border-bottom-style: solid;
                                    border-left-style: solid; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px;
                                    border-left-width: 1px; background-color: #f0eded;">
                                    <div style="margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0;
                                        padding-right: 0; padding-bottom: 0; padding-left: 0;">
                                        <span id="ecbselcat" style="display: block; white-space: nowrap; overflow: hidden;
                                            font-size: 18px;"><%=strings.Cat_3 %></span>
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
                                margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
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
                                        color: #221919; box-shadow: inset 0 1px rgba(34,25,25,0.15),
0 1px #fff; -moz-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px #fff; -webkit-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px #fff;
                                        min-width: 301px; float: left; clear: both; display: inline-block; box-sizing: border-box;
                                        -moz-box-sizing: border-box; -ms-box-sizing: border-box; -webkit-box-sizing: border-box;
                                        border-radius: 6px; -moz-border-radius: 6px; -webkit-border-radius: 6px; -moz-transition: all 0.08s
ease-in-out; -webkit-transition: all 0.08s ease-in-out; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                                        padding-top: 6px; padding-right: 12px; padding-bottom: 6px; padding-left: 12px;
                                        border-top-color: #ad9c9c; border-right-color: #ad9c9c; border-bottom-color: #ad9c9c;
                                        border-left-color: #ad9c9c; border-top-style: solid; border-right-style: solid;
                                        border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                                        border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #fff;" />
                                    <div style="margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0;
                                        padding-right: 0; padding-bottom: 0; padding-left: 0;">
                                        <ul role="listbox" aria-activedescendant="ui-active-menuitem" style="z-index: 1000000top: 460px !important;
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
                                        border-left-width: 1px;"><strong style="font-style: normal; font-weight: bold; position: relative;
                                            z-index: 2;"><%=strings.Add %></strong><span style="position: absolute; z-index: 1; top: -1px;
                                                right: -1px; bottom: -1px; left: -1px; display: block; opacity: 1; border-radius: 6px;
                                                -moz-border-radius: 6px; -webkit-border-radius: 6px; box-shadow: inset 0 1px rgba(255,255,255,0.35);
                                                -moz-box-shadow: inset 0 1px
rgba(255,255,255,0.35); -webkit-box-shadow: inset 0 1px rgba(255,255,255,0.35); -moz-transition-property: opacity; -moz-transition-duration: 0.5s;
                                                -moz-transition-timing-function: ease-in-out; -webkit-transition-property: opacity;
                                                -webkit-transition-duration: 0.5s; -webkit-transition-timing-function: ease-in-out;
                                                font-size: 18px; filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfafb', endColorstr='#f0eded');
                                                border-top-color: #bbb; border-right-color: #bbb; border-bottom-color: #bbb;
                                                border-left-color: #bbb; border-top-style: solid; border-right-style: solid;
                                                border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                                                border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #f0eded;
                                                background-position: 0%
0%;"></span></a>
                                </div>
                                <ul id="CurrentCollaborators" style="border-top-width: 0 !important; margin-top: 10px;
                                    margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0; padding-right: 0;
                                    padding-bottom: 0; padding-left: 0;">
                                </ul>
                            </div>
                        </li>
                    </ul>
                    <div id="saveEB" style="border-top-color: rgba(255,255,255,0.7); border-top-width: 1px;
                        border-top-style: solid; float: left; margin-top: 0; margin-right: 0; margin-bottom: 20px;
                        margin-left: 0; padding-top: 24px; padding-right: 0; padding-bottom: 0; padding-left: 15px;">
                        <a href="javascript:void(0)" style="font-weight: bold; color: #fcf9f9; text-decoration: none;
                            outline: none; position: relative; display: inline-block; text-align: center;
                            line-height: 1em; border-radius: 8px; -moz-border-radius: 8px; -webkit-border-radius: 8px;
                            -moz-transition-property: color, -moz-box-shadow, text-shadow; -moz-transition-duration: .05s;
                            -moz-transition-timing-function: ease-in-out; -webkit-transition-property: color, -webkit-box-shadow, text-shadow;
                            -webkit-transition-duration: .05s; -webkit-transition-timing-function: ease-in-out;
                            box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                            -moz-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                            -webkit-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                            cursor: pointer; font-size: 24px; text-shadow: 0 -1px rgba(34,25,25,0.5); padding-top: .45em;
                            padding-right: .825em; padding-bottom: .45em; padding-left: .825em; border-top-color: transparent;
                            border-right-color: transparent; border-bottom-color: transparent; border-left-color: transparent;
                            border-top-style: solid; border-right-style: solid; border-bottom-style: solid;
                            border-left-style: solid; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px;
                            border-left-width: 1px;"><strong style="font-style: normal; font-weight: bold; position: relative;
                                z-index: 2;"><%=strings.Save %></strong> <span style="position: absolute; z-index: 1;
                                    top: -1px; right: -1px; bottom: -1px; left: -1px; display: block; opacity: 1;
                                    border-radius: 8px; -moz-border-radius: 8px; -webkit-border-radius: 8px; box-shadow: inset 0 1px rgba(255,255,255,0.35);
                                    -moz-box-shadow: inset 0 1px rgba(255,255,255,0.35); -webkit-box-shadow: inset 0 1px rgba(255,255,255,0.35);
                                    -moz-transition-property: opacity; -moz-transition-duration: 0.5s; -moz-transition-timing-function: ease-in-out;
                                    -webkit-transition-property: opacity; -webkit-transition-duration: 0.5s; -webkit-transition-timing-function: ease-in-out;
                                    font-size: 24px; filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#eb5367', endColorstr='#d43638');
                                    border-top-color: #910101; border-right-color: #910101; border-bottom-color: #910101;
                                    border-left-color: #910101; border-top-style: solid; border-right-style: solid;
                                    border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                                    border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #d43638;
                                    background-position: 0% 0%;"></span></a>
                    </div>
                    <div id="delEB" style="border-top-color: rgba(255,255,255,0.7); border-top-width: 1px;
                        border-top-style: solid; float: left; margin-top: 0; margin-right: 0; margin-bottom: 20px;
                        margin-left: 0; padding-top: 24px; padding-right: 0; padding-bottom: 0; padding-left: 15px;">
                        <a style="font-weight: bold; color: #fcf9f9; text-decoration: none; outline: none;
                            position: relative; display: inline-block; text-align: center; line-height: 1em;
                            border-radius: 8px; -moz-border-radius: 8px; -webkit-border-radius: 8px; -moz-transition-property: color, -moz-box-shadow, text-shadow;
                            -moz-transition-duration: .05s; -moz-transition-timing-function: ease-in-out;
                            -webkit-transition-property: color, -webkit-box-shadow, text-shadow; -webkit-transition-duration: .05s;
                            -webkit-transition-timing-function: ease-in-out; box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                            -moz-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                            -webkit-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                            cursor: pointer; font-size: 24px; text-shadow: 0 -1px rgba(34,25,25,0.5); padding-top: .45em;
                            padding-right: .825em; padding-bottom: .45em; padding-left: .825em; border-top-color: transparent;
                            border-right-color: transparent; border-bottom-color: transparent; border-left-color: transparent;
                            border-top-style: solid; border-right-style: solid; border-bottom-style: solid;
                            border-left-style: solid; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px;
                            border-left-width: 1px;"><strong style="font-style: normal; font-weight: bold; position: relative;
                                z-index: 2;"><%=strings.Board_4 %></strong> <span style="position: absolute; z-index: 1; top: -1px;
                                    right: -1px; bottom: -1px; left: -1px; display: block; opacity: 1; border-radius: 8px;
                                    -moz-border-radius: 8px; -webkit-border-radius: 8px; box-shadow: inset 0 1px rgba(255,255,255,0.35);
                                    -moz-box-shadow: inset 0 1px
rgba(255,255,255,0.35); -webkit-box-shadow: inset 0 1px rgba(255,255,255,0.35); -moz-transition-property: opacity;
                                    -moz-transition-duration: 0.5s; -moz-transition-timing-function: ease-in-out;
                                    -webkit-transition-property: opacity; -webkit-transition-duration: 0.5s; -webkit-transition-timing-function: ease-in-out;
                                    font-size: 24px; filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#eb5367', endColorstr='#d43638');
                                    border-top-color: #910101; border-right-color: #910101; border-bottom-color: #910101;
                                    border-left-color: #910101; border-top-style: solid; border-right-style: solid;
                                    border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                                    border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #d43638;
                                    background-position: 0% 0%;"></span></a>
                    </div>
                </div>
                <div class="category" id="eboardcategory">
                    <h2>
                        <%=strings.Cat_2 %></h2>
                    <ul class="scroll" id="ecbcat">
                    </ul>
                    <div class="subBox" id="ecbsubcat" style="display: none">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="popup" id="editpin" style="width: 900px;">
        <div class="modal">
            <div class="header1">
                <div class="title">
                    <h2>
                        <%=strings.Pin_7 %></h2>
                </div>
                <div class="close" id="closeedit">
                    <a href="javascript:void(0)">x</a></div>
            </div>
            <div class="cont">
                <div style="padding: 15; margin: 0; z-index: 2; width: 852px; top: 100%; left: 20%;
                    background-color: #FFFFFF;">
                    <div id="PinEditPreview" style="position: relative; width: 192px; font-size: 11px;
                        box-shadow: 0 1px 3px rgba(34,25,25,0.4); -moz-box-shadow: 0 1px 2px rgba(34,25,25,0.4);
                        -webkit-box-shadow: 0 1px 3px rgba(34,25,25,0.4); float: right; overflow: hidden;
                        margin-top: 36px; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 15px;
                        padding-right: 15px; padding-bottom: 0; padding-left: 15px; border-top-color: #dedcdd#c9c7c8#c9c7c8;
                        border-right-color: #dedcdd#c9c7c8#c9c7c8; border-bottom-color: #dedcdd#c9c7c8#c9c7c8;
                        border-left-color: #dedcdd#c9c7c8#c9c7c8; border-top-style: solid; border-right-style: solid;
                        border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                        border-right-width: 1px; border-bottom-width: 2px; border-left-width: 1px; background-color: #fff;">
                        <div style="position: absolute; z-index: 3; top: -70px; left: -117px; width: 125px;
                            height: 22px; text-align: center; font-size: 11px; color: #524d4d; overflow: hidden;
                            -webkit-transform: rotate(-45deg); -moz-transform: rotate(-45deg); -o-transform: rotate(-45deg);
                            -ms-transform: rotate(-45deg); -moz-transition: all 0.5s ease-in-out; -webkit-transition: all 0.5s ease-in-out;
                            margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 8px;
                            padding-right: 0; padding-bottom: 0; padding-left: 0; background-color: #f2f0f0;"
                            align="center">
                        </div>
                        <a id="pinlink" style="font-weight: bold; color: #221919; text-decoration: none;
                            outline: none;">
                            <img id="pinimg" style="border-top-width: 0; border-right-width: 0; border-bottom-width: 0;
                                border-left-width: 0;" /></a>
                        <p id="postDescription" style="line-height: 1.35em; margin-top: 0; margin-right: 0;
                            margin-bottom: .8em; margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0;
                            padding-left: 0;">
                        </p>
                    </div>
                    <div id="PinEdit" style="float: left; width: 614px; font-size: 13px; margin-top: 36px;
                        margin-right: 0; margin-bottom: 36px; margin-left: 0; padding-top: 0; padding-right: 0;
                        padding-bottom: 0; padding-left: 0;">
                        <ul style="border-top-style: solid; border-top-width: 1px; border-top-color: #ccc;
                            margin-top: 1px; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0;
                            padding-right: 0; padding-bottom: 0; padding-left: 15px;">
                            <li style="border-top-width: 1px; border-top-color: rgba(255,255,255,0.7); border-top-style: solid;
                                border-bottom-width: 1px; border-bottom-color: rgba(34,25,25,0.1); border-bottom-style: solid;
                                position: relative; display: block; font-size: 21px; font-weight: 300; clear: both;
                                color: #8c7e7e; text-shadow: 0 1px rgba(255,255,255,0.9); float: left; width: 100%;
                                margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 15px;
                                padding-right: 0; padding-bottom: 15px; padding-left: 0; list-style-type: none;">
                                <label style="display: inline-block; line-height: 1.4em; font-size: 18px; float: left;
                                    width: 150px; padding-top: 7px; vertical-align: top;">
                                    <%=strings.Desc %></label>
                                <div style="float: left; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                                    padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                                    <div id="ta_holder" style="position: relative; margin-top: 0; margin-right: 0; margin-bottom: 0;
                                        margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                                        <textarea rows="2" name="details" maxlength="500" id="description_pin_edit" cols="40"
                                            style="font-family: inherit; font-size: 18px; font-weight: 300; resize: none;
                                            outline: none; line-height: 1.3em; color: #221919; box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
                                            -moz-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
                                            -webkit-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
                                            min-height: 3.95em; display: inline-block; box-sizing: border-box; -moz-box-sizing: border-box;
                                            -ms-box-sizing: border-box; -webkit-box-sizing: border-box; border-radius: 6px;
                                            -moz-border-radius: 6px; -webkit-border-radius: 6px; -moz-transition: all 0.08s ease-in-out;
                                            -webkit-transition: all 0.08s ease-in-out; min-width: 375px; margin-top: 0; margin-right: 0;
                                            margin-bottom: 0; margin-left: 0; padding-top: 6px; padding-right: 12px; padding-bottom: 6px;
                                            padding-left: 12px; border-top-color: #a4a2a2; border-right-color: #a4a2a2; border-bottom-color: #a4a2a2;
                                            border-left-color: #a4a2a2; border-top-style: solid; border-right-style: solid;
                                            border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                                            border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #fff;"> </textarea>
                                        <div style="position: absolute; display: none; z-index: 3; top: 81px; right: 0; left: 0;
                                            max-height: 200px; border-radius: 6px; -moz-border-radius: 6px; -webkit-border-radius: 6px;
                                            box-shadow: 0 3px 8px rgba(35,24,24,.33); -moz-box-shadow: 0 3px 8px rgba(35,24,24,.33);
                                            -webkit-box-shadow: 0 3px 8px rgba(35,24,24,.33); overflow-y: scroll; margin-top: 0;
                                            margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0; padding-right: 0;
                                            padding-bottom: 0; padding-left: 0; border-top-color: #ad9c9c; border-right-color: #ad9c9c;
                                            border-bottom-color: #ad9c9c; border-left-color: #ad9c9c; border-top-style: solid;
                                            border-right-style: solid; border-bottom-style: solid; border-left-style: solid;
                                            border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px;
                                            background-color: #fff;">
                                        </div>
                                    </div>
                                    <span id="editPCharCount" style="color: #8c7e7e; width: 78px; -moz-transition: all 0.5s ease-in-out;
                                        -webkit-transition: all 0.5s ease-in-out;"></span>
                                </div>
                            </li>
                            <li style="border-top-width: 1px; border-top-color: rgba(255,255,255,0.7); border-top-style: solid;
                                border-bottom-width: 1px; border-bottom-color: rgba(34,25,25,0.1); border-bottom-style: solid;
                                position: relative; display: block; font-size: 21px; font-weight: 300; clear: both;
                                color: #8c7e7e; text-shadow: 0 1px rgba(255,255,255,0.9); float: left; width: 100%;
                                margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 15px;
                                padding-right: 0; padding-bottom: 15px; padding-left: 0; list-style-type: none;">
                                <label for="id_link" style="display: inline-block; line-height: 1.4em; font-size: 18px;
                                    float: left; width: 150px; padding-top: 7px; vertical-align: top;">
                                    <%=strings.Link %></label>
                                <div style="float: left; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                                    padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                                    <input name="link" id="id_link" type="text" style="font-family: inherit; font-size: 18px;
                                        font-weight: 300; resize: none; outline: none; line-height: 1.4; color: #221919;
                                        box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8); -moz-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
                                        -webkit-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
                                        display: inline-block; box-sizing: border-box; -moz-box-sizing: border-box; -ms-box-sizing: border-box;
                                        -webkit-box-sizing: border-box; border-radius: 6px; -moz-border-radius: 6px;
                                        -webkit-border-radius: 6px; -moz-transition: all 0.08s ease-in-out; -webkit-transition: all 0.08s ease-in-out;
                                        min-width: 375px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                                        padding-top: 6px; padding-right: 12px; padding-bottom: 6px; padding-left: 12px;
                                        border-top-color: #a4a2a2; border-right-color: #a4a2a2; border-bottom-color: #a4a2a2;
                                        border-left-color: #a4a2a2; border-top-style: solid; border-right-style: solid;
                                        border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                                        border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #fff;" />
                                </div>
                            </li>
                            <li style="border-top-width: 1px; border-top-color: rgba(255,255,255,0.7); border-top-style: solid;
                                border-bottom-width: 1px; border-bottom-color: rgba(34,25,25,0.1); border-bottom-style: solid;
                                position: relative; display: block; font-size: 21px; font-weight: 300; clear: both;
                                color: #8c7e7e; text-shadow: 0 1px rgba(255,255,255,0.9); float: left; width: 100%;
                                margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 15px;
                                padding-right: 0; padding-bottom: 15px; padding-left: 0; list-style-type: none;">
                                <label for="id_board" style="display: inline-block; line-height: 1.4em; font-size: 18px;
                                    float: left; width: 150px; padding-top: 7px; vertical-align: top;">
                                    <%=strings.Board %></label>
                                <div style="float: left; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                                    padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                                    <select name="board" id="id_board" style="font-family: inherit; font-size: 18px;
                                        font-weight: inherit; resize: none; outline: none;">
                                    </select>
                                </div>
                            </li>
                            <li style="border-top-width: 1px; border-top-color: rgba(255,255,255,0.7); border-top-style: solid;
                                border-bottom-width: 1px; border-bottom-color: rgba(34,25,25,0.1); border-bottom-style: solid;
                                position: relative; display: block; font-size: 21px; font-weight: 300; clear: both;
                                color: #8c7e7e; text-shadow: 0 1px rgba(255,255,255,0.9); float: left; width: 100%;
                                margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 15px;
                                padding-right: 0; padding-bottom: 15px; padding-left: 0; list-style-type: none;">
                                <label style="display: inline-block; line-height: 1.4em; font-size: 18px; float: left;
                                    width: 150px; padding-top: 7px; vertical-align: top;">
                                    <%=strings.Del %></label>
                                <div style="float: left; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                                    padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                                    <a href="javascript:void(0)" id="delete" style="font-weight: bold; color: #524d4d;
                                        text-decoration: none; outline: none; position: relative; display: inline-block;
                                        text-align: center; line-height: 1em; border-radius: 6px; -moz-border-radius: 6px;
                                        -webkit-border-radius: 6px; -moz-transition-property: color, -moz-box-shadow, text-shadow;
                                        -moz-transition-duration: .05s; -moz-transition-timing-function: ease-in-out;
                                        -webkit-transition-property: color, -webkit-box-shadow, text-shadow; -webkit-transition-duration: .05s;
                                        -webkit-transition-timing-function: ease-in-out; box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                                        -moz-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                                        -webkit-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                                        cursor: pointer; font-size: 18px; text-shadow: 0 1px rgba(255,255,255,0.9); padding-top: .45em;
                                        padding-right: .825em; padding-bottom: .45em; padding-left: .825em; border-top-color: transparent;
                                        border-right-color: transparent; border-bottom-color: transparent; border-left-color: transparent;
                                        border-top-style: solid; border-right-style: solid; border-bottom-style: solid;
                                        border-left-style: solid; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px;
                                        border-left-width: 1px;"><strong style="font-style: normal; font-weight: bold; position: relative;
                                            z-index: 2;"><%=strings.Pin_8 %></strong><span style="white-space: nowrap; position: absolute;
                                                z-index: 1; top: -1px; right: -1px; bottom: -1px; left: -1px; display: block;
                                                opacity: 1; border-radius: 6px; -moz-border-radius: 6px; -webkit-border-radius: 6px;
                                                box-shadow: inset 0 1px rgba(255,255,255,0.35); -moz-box-shadow: inset 0 1px rgba(255,255,255,0.35);
                                                -webkit-box-shadow: inset 0 1px rgba(255,255,255,0.35); -moz-transition-property: opacity;
                                                -moz-transition-duration: 0.5s; -moz-transition-timing-function: ease-in-out;
                                                -webkit-transition-property: opacity; -webkit-transition-duration: 0.5s; -webkit-transition-timing-function: ease-in-out;
                                                font-size: 18px; filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfafb', endColorstr='#f0eded');
                                                border-top-color: #bbb; border-right-color: #bbb; border-bottom-color: #bbb;
                                                border-left-color: #bbb; border-top-style: solid; border-right-style: solid;
                                                border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                                                border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #f0eded;
                                                background-position: 0% 0%;"></span></a>
                                </div>
                            </li>
                        </ul>
                        <div style="border-top-color: rgba(255,255,255,0.7); border-top-width: 1px; border-top-style: solid;
                            float: left; margin-top: 0; margin-right: 0; margin-bottom: 20px; margin-left: 0;
                            padding-top: 24px; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                            <p style="line-height: 1.35em; margin-top: 0; margin-right: 0; margin-bottom: .8em;
                                margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 15px;">
                                <a id="saveeditpin" href="javascript:void(0)" style="font-weight: bold; color: #fcf9f9;
                                    text-decoration: none; outline: none; position: relative; display: inline-block;
                                    text-align: center; line-height: 1em; border-radius: 8px; -moz-border-radius: 8px;
                                    -webkit-border-radius: 8px; -moz-transition-property: color, -moz-box-shadow, text-shadow;
                                    -moz-transition-duration: .05s; -moz-transition-timing-function: ease-in-out;
                                    -webkit-transition-property: color, -webkit-box-shadow, text-shadow; -webkit-transition-duration: .05s;
                                    -webkit-transition-timing-function: ease-in-out; box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                                    -moz-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                                    -webkit-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                                    cursor: pointer; font-size: 24px; text-shadow: 0 -1px rgba(34,25,25,0.5); padding-top: .45em;
                                    padding-right: .825em; padding-bottom: .45em; padding-left: .825em; border-top-color: transparent;
                                    border-right-color: transparent; border-bottom-color: transparent; border-left-color: transparent;
                                    border-top-style: solid; border-right-style: solid; border-bottom-style: solid;
                                    border-left-style: solid; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px;
                                    border-left-width: 1px;"><strong style="font-style: normal; font-weight: bold; position: relative;
                                        z-index: 2;"><%=strings.Pin_9 %></strong> <span style="white-space: nowrap; position: absolute;
                                            z-index: 1; top: -1px; right: -1px; bottom: -1px; left: -1px; display: block;
                                            opacity: 1; border-radius: 8px; -moz-border-radius: 8px; -webkit-border-radius: 8px;
                                            box-shadow: inset 0 1px rgba(255,255,255,0.35); -moz-box-shadow: inset 0 1px rgba(255,255,255,0.35);
                                            -webkit-box-shadow: inset 0 1px rgba(255,255,255,0.35); -moz-transition-property: opacity;
                                            -moz-transition-duration: 0.5s; -moz-transition-timing-function: ease-in-out;
                                            -webkit-transition-property: opacity; -webkit-transition-duration: 0.5s; -webkit-transition-timing-function: ease-in-out;
                                            font-size: 24px; filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#eb5367', endColorstr='#d43638');
                                            border-top-color: #910101; border-right-color: #910101; border-bottom-color: #910101;
                                            border-left-color: #910101; border-top-style: solid; border-right-style: solid;
                                            border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                                            border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #d43638;
                                            background-position: 0% 0%;"></span></a>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="pin" style="position: absolute; box-shadow: 0 1px 3px rgba(34,25,25,0.4);
        -moz-box-shadow: 0 1px 3px rgba(34,25,25,0.4); display: none; -webkit-box-shadow: 0 1px 3px rgba(34,25,25,0.4);
        margin-top: 0; margin-right: auto; margin-bottom: 32px; margin-left: auto; padding-top: 0;
        padding-right: 0; padding-bottom: 0; padding-left: 0; background-color: #FFFFFF;
        z-index: 2; text-align: center; left: 50%;">
        <div style="clear: both; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
            padding-top: 20px; padding-right: 0; padding-bottom: 0; padding-left: 0;">
        </div>
        <div id="PinActionButtons" style="height: 26px; width: 280px; float: left; overflow: hidden;
            margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 30px; padding-top: 0;
            padding-right: 0; padding-bottom: 0; padding-left: 13px; background-image: url('http://freshpin.com/cdn/img/detailiconBg.jpg');
            background-repeat: no-repeat; background-position: left top;">
            <a id="likepint" href="javascript:void(0)" style="font-weight: bold; color: #221919;
                text-decoration: none; outline: none;">
                <div style="height: 26px; font-size: 12px; font-weight: bold; float: left; display: block;
                    color: #777176; font-family: Helvetica, Arial, Sans-Serif; text-align: left;
                    line-height: 26px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                    padding-top: 0; padding-right: 14px; padding-bottom: 0; padding-left: 30px; background-image: url('http://freshpin.com/cdn/img/likeIconDetailPage.png');
                    background-repeat: no-repeat; background-position: left 5px;" align="left">
                    <%=strings.Like %></div>
            </a><a id="editpint" href="javascript:void(0)" style="font-weight: bold; color: #221919;
                text-decoration: none; outline: none;">
                <div style="height: 26px; font-size: 12px; font-weight: bold; float: left; display: block;
                    color: #777176; font-family: Helvetica, Arial, Sans-Serif; text-align: left;
                    line-height: 26px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                    padding-top: 0; padding-right: 14px; padding-bottom: 0; padding-left: 30px; background-image: url('http://freshpin.com/cdn/img/repinIconDetailPage.png');
                    background-repeat: no-repeat; background-position: left 5px;" align="left">
                    <%=strings.Edit %></div>
            </a><a id="repint" href="javascript:void(0)" style="font-weight: bold; color: #221919;
                text-decoration: none; outline: none;">
                <div style="height: 26px; font-size: 12px; font-weight: bold; float: left; display: block;
                    color: #777176; font-family: Helvetica, Arial, Sans-Serif; text-align: left;
                    line-height: 26px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                    padding-top: 0; padding-right: 14px; padding-bottom: 0; padding-left: 23px; background-image: url('http://freshpin.com/cdn/img/repinIconDetailPage.png');
                    background-repeat: no-repeat; background-position: left 4px;" align="left">
                    <%=strings.Repin %></div>
            </a><a id="commentt" href="javascript:void(0)" style="font-weight: bold; color: #221919;
                text-decoration: none; outline: none;">
                <div style="height: 26px; font-size: 12px; font-weight: bold; float: left; display: block;
                    color: #777176; font-family: Helvetica, Arial, Sans-Serif; text-align: left;
                    line-height: 26px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                    padding-top: 0; padding-right: 14px; padding-bottom: 0; padding-left: 25px; background-image: url('http://freshpin.com/cdn/img/commentIconDetailPage.png');
                    background-repeat: no-repeat; background-position: left 4px;" align="left">
                    <%=strings.Comment %></div>
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
    <div id="fbd" class="popup fbd">
        <div class="modal">
            <div class="header1">
                <div class="title">
                    <h2>
                        <%=strings.Comment %></h2>
                </div>
                <div id="fbdclose" class="close">
                    <a href="javascript:void(0)">x</a></div>
            </div>
            <div class="cont">
                <ul>
                    <li>
                        <p class="ques">
                            <%=cat.Comment_Ques %>...</p>
                    </li>
                    <li class="choice">
                        <input type="radio" name="choosefbd" /><span><%=cat.Comment_Ans_1 %></span> </li>
                    <li class="choice">
                        <input type="radio" name="choosefbd" /><span><%=cat.Comment_Ans_2 %></span> </li>
                    <li class="choice">
                        <input type="radio" name="choosefbd" /><span><%=cat.Comment_Ans_3 %></span> </li>
                    <li class="choice">
                        <input type="radio" name="choosefbd" /><span><%=cat.Comment_Ans_4 %></span>
                    </li>            
                </ul>
             </div>           
        </div>
    </div>
</body>
</html>

