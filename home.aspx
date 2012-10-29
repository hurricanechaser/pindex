<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="PindexProd" %>
<!DOCTYPE html>

<html>
<head>
   <title><%=strings.FreshPin %> / <%=strings.Home %></title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-5">    
    <link rel="icon" href="favicon.ico" />
    <link href="http://freshpindev.botcodelocal.com/cdn/style.css" rel="stylesheet" type="text/css" />
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
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.7.2.js"></script>
    <script src="freshpin.js" type="text/javascript"></script>
    <script src="freshpinhome.js" type="text/javascript"></script>
    <script src="http://freshpin.com/cdn/scripts/jquery.easing.1.3.js" type="text/javascript"></script>
    <script src="http://freshpin.com/cdn/scripts/jeresig-jquery.hotkeys-0451de1/jquery.hotkeys.js"
        type="text/javascript"></script>
    <script src="http://freshpin.com/cdn/scripts/scrolltopcontrol.js" type="text/javascript"></script>
    <script src="http://freshpin.com/cdn/scripts/jquery-jquery-tmpl/jquery.tmpl.min.js"
        type="text/javascript"></script>
    <script src="http://freshpin.com/cdn/scripts/desandro-masonry/jquery.masonry.min.js"
        type="text/javascript"></script>
    <script src="http://freshpin.com/cdn/scripts/farinspace-jquery.imgpreload-6e0e307/jquery.imgpreload.min.js"
        type="text/javascript"></script>
    <script src="http://freshpin.com/cdn/scripts/cookies.js" type="text/javascript"></script>
    <script src="http://freshpin.com/cdn/scripts/jqmodal/jqModal.js" type="text/javascript"></script>
    <script src="http://freshpin.com/cdn/scripts/jquery.easing.1.3.js" type="text/javascript"></script>
    <script src="http://freshpin.com/cdn/scripts/jquery-hashchange/jquery.ba-hashchange.min.js"
        type="text/javascript"></script>
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
    <script id="rank" type="text/x-jquery-tmpl">
    <ul class="box" >       
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
            <img src="${url}?width=190" style="height:height:${getHeight($data)}pxpx" alt="${title}" /></a>
        </li>
        <li><div class="name"><span>${title}</span></div></li>
    </ul>
    </script>
    <script id="picTemplate" type="text/x-jquery-tmpl">
        <ul class="box">
            <li class="buttons"><a name="buttons" class="buttonLike" href="javascript:void(0)">Like</a> <a class="buttonPin" name="buttons"
                                                                                href="javascript:void(0)">Re-pin</a> <a name="buttons" class="buttonComment" href="javascript:void(0)">Comment</a>
            </li>
            <li class="img"><a href="#pin=${PinID}">
                                <img src="${url}?width=190" style="height:${getHeight($data)}px" alt="${title}" /></a>
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
        <li>
            <div class="name">
                <span>${title}</span></div>
        </li>
        {{if comments && comments.length}}
        <li class="comments">{{tmpl(comments) "#comments"}} </li>
        {{/if}} 
     </ul>
    </script>
    <script id="comments" type="text/x-jquery-tmpl">
      <div>
         <a href="javascript:void(0)" ><img src="${getAvatar($data)}" /></a><p><a href="javascript:void(0)" >${Name}</a>${Comments}</p> 
      </div>
    </script>
    <script id="cats" type="text/x-jquery-tmpl">
        <a href="#cat=${escape(getVal($data,'Name'))}" style="overflow:hidden;" >${Name}</a>   
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
</head>
<body>
    <div class="header">
        <div class="line1">
            <div class="queryBox">
                <input class="searchfield" type="text" value="Search over 500,000 Pins" id="query" />
                <input class="searchbutton" type="button" id="query_button" />
                <input class="scrollbutton" type="button" id="scroll_button" />
            </div>
            <div class="freshpinlogo">
                <a href="." name="noblock">
                    <img src="logo.png" /></a>
                <div class="freshpinlogoTagline"> <% =strings.Banner %> </div>
            </div>
            <div class="aboutus">
                <ul class="navigation">
                    <li><a id="about" name="noblock" href="about.aspx#about" class="nav">
                        <% =strings.About %><span></span></a>
                        <ul>
                            <li><a href="about.aspx#help"> <%=strings.Help %></a></li>
                            <li class="beforeDivider"><a href="copyright.html"><% =strings.Copyright %></a></li>
                            <li><a id="reqin" href="javascript:void(0)"><%=strings.Request_an_Invite %></a></li>
                        </ul>
                    </li>
                    <li id="logininfo" class="logininfo"></li>
                </ul>
            </div>
        </div>
        <div class="line2">
            <div class="menu3">
                <li id="mlevel1"><a href="javascript:rcat('Wedding');"><%=cat.Wedding%></a><span></span></li>
                <li s id="mlevel2" class="menu"><a href="javascript:rcat('Fashion');"><%=cat.Fashion%></a><span></span></li>
                <!-- <li style="width: 64px;" id="mlevel3" class="menu"><a href="">Nails</a> <span></span></li>-->
                <li id="mlevel4" class="menu"><a href="javascript:rcat('Jewelry');"><%=cat.Jewelry%></a><span></span></li>
                <li id="mlevel5" class="menu"><a href="javascript:rcat('Beauty');"><%=cat.Beauty%></a> <span></span></li>
                <li id="mlevel6" class="menu"><a href="javascript:rcat('Architectural');"><%=cat.Architectural%></a><span></span></li>
                <li id="mlevel7" class="menu"><a href="javascript:rcat('Interiors');"><%=cat.Interiors%></a><span class="bgNone"></span></li>
                <li id="mlevel8" class="menu"><a href="javascript:rcat('Landscapes');"><%=cat.Landscapes%></a><span class="bgNone"></span></li>
                <li id="mlevel9" class="menu" style="float: right;"><a href="javascript:void(0)">Languages</a><span></span></li>
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
                <a href="javascript:rcat('Setting');"><strong><%=cat.Setting%></strong></a>
                <br />
                <a href="javascript:rcat('Beach Wedding');"><%=cat.Beach_Wedding%></a> 
                <a href="javascript:rcat('Boat Wedding');"><%=cat.Boat_Wedding%></a> 
                <a href="javascript:rcat('Mountain Wedding');"><%=cat.Mountain_Wedding%></a>
                <a href="javascript:rcat('Park Wedding');"><%=cat.Park_Wedding%></a> 
                <a href="javascript:rcat('Other Settings');"><%=cat.Other_Settings%></a>
                <br />
                <a href="javascript:rcat('Essentials');"><strong><%=cat.Essentials%></strong></a>
                <br />
                <a href="javascript:rcat('Wedding Dress');"><%=cat.Wedding_Dress%></a> 
                <a href="javascript:rcat('Bridesmaid Dress');"><%=cat.Bridesmaid_Dress%></a> 
                <a href="javascript:rcat('Wedding Invitation');"><%=cat.Wedding_Invitation%></a>
                <a href="javascript:rcat('Tuxedos');"><%=cat.Tuxedos%></a> 
                <a href="javascript:rcat('Wedding Flowers');"><%=cat.Wedding_Flowers%></a> 
                <a href="javascript:rcat('Wedding Cake');"><%=cat.Wedding_Cake%></a>
                <br />
                <a href="javascript:rcat('Special Touches');"><strong><%=cat.Special_Touches%></strong></a>
                <br />
                <a href="javascript:rcat('Photo Ideas');"><%=cat.Photo_Ideas %></a>
                <a href="javascript:rcat('Reception Decor');"><%=cat.Reception_Decor %></a> 
                <a href="javascript:rcat('Decoration Ideas');"><%=cat.Decoration_Ideas %></a>
                <a href="javascript:rcat('Other Touches');"><%=cat.Other_Touches%></a>
                <br />
            </div>
        </div>
        <div class="submenu" id="submenu2" style="display: none">
            <div>
                <a href="javascript:rcat('Women's Clothes');"><strong><%=cat.Womens_Clothes %></strong></a>
                <br />
                <a href="javascript:rcat('Jeans');"><%=cat.Jeans %></a> 
                <a href="javascript:rcat('Dresses');"><%=cat.Dresses %></a>
                <a href="javascript:rcat('Intimates');"><%=cat.Intimates %></a>
                <a href="javascript:rcat('Jackets');"><%=cat.Jackets %></a>
                <a href="javascript:rcat('Outerwear');"><%=cat.Outerwear %></a>
                <a href="javascript:rcat('Pants & Shorts');"><%=cat.Pants_Shorts %></a>
                <a href="javascript:rcat('Skirts');"><%=cat.Skirts %></a>
                <a href="javascript:rcat('Sportswear');"><%=cat.Sportswear %></a>
                <a href="javascript:rcat('Suits');"><%=cat.Suits %></a>
                <a href="javascript:rcat('Sweaters');"><%=cat.Sweaters %></a>
                <a href="javascript:rcat('Swimwear');"><%=cat.Swimwear %></a>
                <a href="javascript:rcat('Tops');"><%=cat.Tops %></a>
                <a href="javascript:rcat('Maternity');"><%=cat.Maternity %></a>
                <br />
                <a href="javascript:rcat('Women/'s Bags');"><strong><%=cat.Womens_Bags %></strong></a>
                <br />
                <a href="javascript:rcat('Backpacks');"><%=cat.Backpacks %></a>
                <a href="javascript:rcat('Clutches');"><%=cat.Clutches %></a>
                <a href="javascript:rcat('Evening');"><%=cat.Evening %></a>
                <a href="javascript:rcat('Hobos');"><%=cat.Hobos %></a>
                <a href="javascript:rcat('Satchels');"><%=cat.Satchels %></a>
                <a href="javascript:rcat('Shoulder');"><%=cat.Shoulder %></a>
                <a href="javascript:rcat('Totes');"><%=cat.Totes %></a>
                <a href="javascript:rcat('Wallets');"><%=cat.Wallets %></a>
                <br />
                <a href="javascript:rcat('Accessories');"><strong><%=cat.Accessories %></strong></a><br />
                <a href="javascript:rcat('Belts');"><%=cat.Belts %></a>
                <a href="javascript:rcat('Gloves');"><%=cat.Gloves %></a>
                <a href="javascript:rcat('Hats');"><%=cat.Hats %></a>
                <a href="javascript:rcat('Scarves');"><%=cat.Scarves %></a>
                <a href="javascript:rcat('Sunglasses');"><%=cat.Sunglasses %></a>
                <br />
                <a href="javascript:rcat('Shoes');"><strong><%=cat.Shoes %></strong></a><br />
                <a href="javascript:rcat('Athletic');"><%=cat.Athletic %></a> 
                <a href="javascript:rcat('Boots');"><%=cat.Boots %></a> 
                <a href="javascript:rcat('Evening');"><%=cat.Evening %></a>
                <a href="javascript:rcat('Flats');"><%=cat.Flats %></a>
                <a href="javascript:rcat('Mules&Clogs');"><%=cat.Mules_Clogs %></a>
                <a href="javascript:rcat('Platform');"><%=cat.Platform %></a>
                <a href="javascript:rcat('Pumps');"><%=cat.Pumps %></a>
                <a href="javascript:rcat('Sandals');"><%=cat.Sandals %></a>
                <a href="javascript:rcat('Wedges');"><%=cat.Wedges %></a>
                <br />
                <a href="javascript:rcat('Kids');"><strong><%=cat.Kids %></strong></a><br />
                <a href="javascript:rcat('BoysClothes');"><%=cat.Boys_Clothes %></a>
                <a href="javascript:rcat('BoysShoes');"><%=cat.Boys_Shoes %></a>
                <a href="javascript:rcat('GirlsClothes');"><%=cat.Girls_Clothes %></a>
                <a href="javascript:rcat('Girl's Shoes');"><%=cat.Girls_Shoes %></a>
                <br />
                <a href="javascript:rcat('Mens');"><strong><%=cat.Mens %></strong></a><br />
                <a href="javascript:rcat('Jeans');"><%=cat.Jeans %></a>
                <a href="javascript:rcat('Outerwear');"><%=cat.Outerwear %></a>
                <a href="javascript:rcat('Pants');"><%=cat.Pants %></a>
                <a href="javascript:rcat('Shirts');"><%=cat.Shirts %></a>
                <a href="javascript:rcat('Shoes');"><%=cat.Shoes %></a>
                <a href="javascript:rcat('Shorts');"><%=cat.Shorts %></a>
                <a href="javascript:rcat('Sweater');"><%=cat.Sweater %></a>
                <a href="javascript:rcat('Swimwear');"><%=cat.Swimwear %></a>
                <a href="javascript:rcat('Tie');"><%=cat.Tie %></a>
                <br />
                <a href="javascript:rcat('Ensembles & Sets');"><strong><%=cat.Ensembles_Sets %></strong></a><br />
                <a href="javascript:rcat('Womens');"><%=cat.Womens %></a>
                <a href="javascript:rcat('Mens');"><%=cat.Mens %></a>
                <a href="javascript:rcat('Kids');"><%=cat.Kids %></a>
                <a href="javascript:rcat('Other');"><%=cat.Other %></a>
            </div>
        </div>
        <div class="submenu" id="submenu3" style="display: none">
            <div>
                <a href="javascript:rcat('Color');"><strong><%=cat.Color %></strong></a><br />
                <a href="javascript:rcat('Multi(3+)');"><%=cat.Multi %></a> 
                <a href="javascript:rcat('Two-Tone');"><%=cat.Two_Tone %></a>
                <a href="javascript:rcat('Metallic');"><%=cat.Metallic %></a>
                <a href="javascript:rcat('Red');"><%=cat.Red %></a> 
                <a href="javascript:rcat('Orange');"><%=cat.Orange %></a>
                <a href="javascript:rcat('Yellow');"><%=cat.Yellow %></a>
                <a href="javascript:rcat('Green');"><%=cat.Green %></a>
                <a href="javascript:rcat('Teal');"><%=cat.Teal %></a> 
                <a href="javascript:rcat('Blue');"><%=cat.Blue %></a> 
                <a href="javascript:rcat('Purple');"><%=cat.Purple %></a> 
                <a href="javascript:rcat('Pink');"><%=cat.Pink %></a>
                <a href="javascript:rcat('White');"><%=cat.White %></a>
                <a href="javascript:rcat('Gray');"><%=cat.Gray %></a>
                <a href="javascript:rcat('Black');"><%=cat.Black %></a>
                <a href="javascript:rcat('Brown');"><%=cat.Brown %></a>
                <br />
                <a href="javascript:rcat('Finish');"><strong><%=cat.Finish %></strong></a><br />
                <a href="javascript:rcat('Shiny');"><%=cat.Shiny %></a>
                <a href="javascript:rcat('Matte');"><%=cat.Matte %></a>
                <a href="javascript:rcat('Glitter');"><%=cat.Glitter %></a>
                <br />
                <a href="javascript:rcat('Difficulty');"><strong><%=cat.Difficulty %></strong></a><br />
                <a href="javascript:rcat('Simple');"><%=cat.Simple %></a>
                <a href="javascript:rcat('Medium');"><%=cat.Medium %></a>
                <a href="javascript:rcat('Hard');"><%=cat.Hard %></a>
                <a href="javascript:rcat('Crazy');"><%=cat.Crazy %></a>
                <br />
                <a href="javascript:rcat('Style');"><strong><%=cat.Style %></strong></a><br />
                <a href="javascript:rcat('Pattern');"><%=cat.Pattern %></a>
                <a href="javascript:rcat('Picture');"><%=cat.Picture %></a>
                <a href="javascript:rcat('Gradient');"><%=cat.Gradient %></a>
                <a href="javascript:rcat('3D');"><%=cat.ThreeD %></a>
                <a href="javascript:rcat('Airbrush');"><%=cat.Airbrush %></a> 
                <a href="javascript:rcat('FreeStyle');"><%=cat.FreeStyle %></a>
                <a href="javascript:rcat('Stamp');"><%=cat.Stamp %></a>
                <a href="javascript:rcat('Stilleto');"><%=cat.Stilleto %></a>
                <br />
                <a href="javascript:rcat('With People');"><strong><%=cat.With_People %></strong></a><br />
                <a href="javascript:rcat('Face');"><%=cat.Face %></a>
                <a href="javascript:rcat('Outfit');"><%=cat.Outfit %></a>
                <a href="javascript:rcat('Celebrities');"><%=cat.Celebrities %></a> 
                <a href="javascript:rcat('Pedicures');"><%=cat.Pedicures %></a>
                <a href="javascript:rcat('Accessories');"><%=cat.Accessories %></a>
                <br />
                <a href="javascript:rcat('How-To');"><strong><%=cat.How_To %></strong></a><br />
                <a href="javascript:rcat('HowToGallery');"><%=cat.How_To_Gallery %></a>
                <br />
                <a href="javascript:rcat('Products');"><strong><%=cat.Products %></strong></a><br />
                <a href="javascript:rcat('Nail Polish');"><%=cat.Nail_Polish %></a>
                <a href="javascript:rcat('Pattern Tools');"><%=cat.Pattern_Tools %></a>
                <a href="javascript:rcat('Manicure');"><%=cat.Manicure %></a>
                <a href="javascript:rcat('Other');"><%=cat.Other %></a>
                <br />
            </div>
        </div>
        <div class="submenu" id="submenu4" style="display: none">
            <div>
                <a href="javascript:rcat('Wedding Jewelry');"><strong><%=cat.Wedding_Jewelry %></strong></a>
                <br />
                <a href="javascript:rcat('Engagement Rings');"><%=cat.Engagement_Rings %></a>
                <a href="javascript:rcat('Wedding Sets');"><%=cat.Wedding_Sets %></a>
                <a href="javascript:rcat('Anniversary Rings');"><%=cat.Anniversary_Rings %></a>
                <br />
                <a href="javascript:rcat('Regular Jewelry');"><strong><%=cat.Regular_Jewellery %></strong></a><br />
                <a href="javascript:rcat('Rings');"><%=cat.Rings %></a>
                <a href="javascript:rcat('Necklaces');"><%=cat.Necklaces %></a>
                <a href="javascript:rcat('Earrings');"><%=cat.Earrings %></a>
                <a href="javascript:rcat('Bracelets');"><%=cat.Bracelets %></a>
                <a href="javascript:rcat('Watches');"><%=cat.Watches %></a>
                <br />
            </div>
        </div>
        <div class="submenu" id="submenu5" style="display: none">
            <div>
                <%--<a href="javascript:rcat('Beauty');"><strong><%=cat.Beauty %></strong></a>
                <br />--%>
                <a href="javascript:rcat('Beauty Products');"><strong><%=cat.Beauty_Products %></strong></a>
                <br />
                <a href="javascript:rcat('Bath & Body');"><%=cat.Bath_Body %></a>
                <a href="javascript:rcat('Fragrances');"><%=cat.Fragrances %></a>
                <a href="javascript:rcat('Hair Accessories');"><%=cat.Hair_Accessories %></a>
                <a href="javascript:rcat('HairCare');"><%=cat.Hair_Care %></a>
                <a href="javascript:rcat('Make-Up');"><%=cat.Make_Up %></a>
                <a href="javascript:rcat('Skin Care');"><%=cat.Skin_Care %></a>
                <a href="javascript:rcat('Tools');"><%=cat.Tools %></a>
                <br />
                <a href="javascript:rcat('Beauty Styles');"><strong><%=cat.Beauty_Styles %></strong></a><br />
                <a href="javascript:rcat('HairStyles');"><%=cat.Hair_Styles %></a>
                <a href="javascript:rcat('Hair - How To');"><%=cat.Hair_How_To %></a>
                <a href="javascript:rcat('Nails');"><%=cat.Nails %></a>
                <a href="javascript:rcat('Toes');"><%=cat.Toes %></a>
                <a href="javascript:rcat('Others');"><%=cat.Others %></a>
                <br />
                <a href="javascript:rcat('Tatoos');"><strong><%=cat.Tatoos %></strong></a>
                <br />
                <a href="javascript:rcat('Men');"><%=cat.Men %></a>
                <a href="javascript:rcat('Women');"><%=cat.Women %></a>
                <br />
            </div>
        </div>
        <div class="submenu" id="submenu6" style="display: none">
            <div>
                <a href="javascript:rcat('Styles');"><strong><%=cat.Styles %></strong></a><br />
                <a href="javascript:rcat('Exterior');"><%=cat.Exterior %></a> 
                <a href="javascript:rcat('Interior');"><%=cat.Interior %></a>
                <br />
                <a href="javascript:rcat('Essentials');"><strong><%=cat.Essentials %></strong></a><br />
                <a href="javascript:rcat('Doors');"><%=cat.Doors %></a>
                <a href="javascript:rcat('Windows');"><%=cat.Windows %></a>
                <a href="javascript:rcat('Stairs');"><%=cat.Stairs %></a>
                <a href="javascript:rcat('Flooring');"><%=cat.Flooring %></a>
                <a href="javascript:rcat('Lighting');"><%=cat.Lighting %></a>
                <a href="javascript:rcat('Roof');"><%=cat.Roof %></a>
                <a href="javascript:rcat('Tiles');"><%=cat.Tiles %></a>
                <a href="javascript:rcat('Color Schemes');"><%=cat.Color_Schemes %></a>
                <br />
                <a href="javascript:rcat('Details');"><strong><%=cat.Details %></strong></a><br />
                <a href="javascript:rcat('Fixtures');"><%=cat.Fixtures %></a> 
                <a href="javascript:rcat('Trim');"><%=cat.Trim %></a>
                <br />
            </div>
        </div>
        <div class="submenu" id="submenu7" style="display: none">
            <div>
                <a href="javascript:{FreshPin.writeCookieValue('culture', 'en-US');location.reload(true);}">English</a>
                    <br /> 
                <a href="javascript:FreshPin.writeCookieValue('culture','zh-CN');location.reload(true);">Chinese</a>
                <br />
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
    <div id="gamify" class="popup">
        <div class="modal">
            <div class="header1">
                <div class="title">
                    <h2>
                        <%=cat.Comment%></h2>
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
        z-index: 2; text-align: center; left: 50%;">
        <div style="clear: both; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
            padding-top: 20px; padding-right: 0; padding-bottom: 0; padding-left: 0;">
        </div>
        <div id="PinActionButtons" style="height: 26px; width: 280px; float: left; overflow: hidden;
            margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 30px; padding-top: 0;
            padding-right: 0; padding-bottom: 0; padding-left: 13px; background-image: url('http://freshpin.com/cdn/img/detailiconBg.jpg');
            background-repeat: no-repeat; background-position: left top;">
            <a name="buttons" href="javascript:void(0)" style="font-weight: bold; color: #221919;
                text-decoration: none; outline: none;">
                <div style="height: 26px; font-size: 12px; font-weight: bold; float: left; display: block;
                    color: #777176; font-family: Helvetica, Arial, Sans-Serif; text-align: left;
                    line-height: 26px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                    padding-top: 0; padding-right: 14px; padding-bottom: 0; padding-left: 30px; background-image: url('http://freshpin.com/cdn/img/likeIconDetailPage.png');
                    background-repeat: no-repeat; background-position: left 5px;" align="left">
                    <%=strings.Like %></div>
            </a><a name="buttons" href="javascript:void(0)" style="font-weight: bold; color: #221919;
                text-decoration: none; outline: none;">
                <div style="height: 26px; font-size: 12px; font-weight: bold; float: left; display: block;
                    color: #777176; font-family: Helvetica, Arial, Sans-Serif; text-align: left;
                    line-height: 26px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                    padding-top: 0; padding-right: 14px; padding-bottom: 0; padding-left: 23px; background-image: url('http://freshpin.com/cdn/img/repinIconDetailPage.png');
                    background-repeat: no-repeat; background-position: left 4px;" align="left">
                    <%=strings.Repin %></div>
            </a><a name="buttons" href="javascript:void(0)" style="font-weight: bold; color: #221919;
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
            <div style="margin: 0 auto; margin-left: 300px;">
                <a href=".">
                    <img src="logo-login.png" />
                </a>
            </div>
        </div>
        <div style="text-align: center; line-height: 80px; font-size: 32px; font-weight: bold;">
            <span>Sign up for an invite to join FreshPin.</span></div>
        <div>
            <div style="text-align: center; line-height: 40px; font-size: 15px;">
                <span>or <a href="javascript:void(0)" id="rilogint" style="color: #a19191">
                    <%=strings.Login %>
                </a>
                    <%=strings.Login_1 %>.</span></div>
            <div>
                <div class="inputBox">
                    <input type="text" class="" id="invitationBox">
                </div>
                <div class="BlueButton">
                    <a href="javascript:void(0)" id="reqinvite">
                        <%=strings.Request_Invitation %></a>
                </div>
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
                <a name="noblock" href="https://www.facebook.com/dialog/oauth?client_id=446480175379142&redirect_uri=http%3A%2F%2Ffreshpin.com%2Ffbloggedin.aspx&scope=user_about_me&response_type=token">
                    <img id="logwfb" style="cursor: pointer;" src="http://freshpin.com/cdn/img/logInWithFB.gif" /></a>
            </div>
            <!--  <div class="fbTw fr">
                <img class="logwtw" style="cursor: pointer;" src="http://freshpin.com/cdn/img/logInWithTW.gif" /></div>-->
        </div>
        <div class="loginBar">
            <img id="logbar" src="http://freshpin.com/cdn/img/login_bar.png" /></div>
        <div class="loginCont">
            <fieldset class="inputs">
                <input type="text" required="" autofocus="" placeholder='<%=strings.Email %>' id="user" />
            </fieldset>
            <!--<label for="user" style="color: #FF0000">*</label>-->
            <fieldset class="inputs">
                <input type="password" required="" placeholder='<%=strings.Pass %>' id="pass" />
            </fieldset>
            <!--<label for="pass" style="color: #FF0000">*</label-->
            <div class="loginBut fl">
                <input type="image" value="Login" id="loginbutton" src="http://freshpin.com/cdn/img/loginBut.gif" /></div>
            <div class="resetPassword fr">
                <a id="fup" href="javascript:void(0)">
                    <%=strings.Forgot_Pass %></a></div>
            <div style="clear: both;">
            </div>
            <span style="color: red; font-family: arial; font-size: 12px; padding: 0;" id="err1">
            </span>
        </div>
    </div>
    <div id="fbd" class="popup fbd">
        <div class="modal">
            <div class="header1">
                <div class="title">
                    <h2>
                        <%=cat.Comment %></h2>
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
                        <input type="radio" name="choosefbd" /><span><%=cat.Comment_Ans_1 %></span>
                    </li>
                    <li class="choice">
                        <input type="radio" name="choosefbd" /><span><%=cat.Comment_Ans_2 %></span>
                    </li>
                    <li class="choice">
                        <input type="radio" name="choosefbd" /><span><%=cat.Comment_Ans_3 %></span>
                    </li>
                    <li class="choice">
                        <input type="radio" name="choosefbd" /><span><%=cat.Comment_Ans_4 %></span>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</body>
</html>
