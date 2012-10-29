<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="PindexProd" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>FreshPin / <%=strings.Help %>?</title>
    <link rel="icon" href="favicon.ico" />
    <link href="http://freshpin.com/cdn/CreateDetails.css" rel="stylesheet" type="text/css" />    
    <link href="http://freshpin.com/cdn/about_dc0f3cd9.css" rel="stylesheet" type="text/css" />   
    <link href="http://freshpin.com/cdn/font.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.7.2.min.js"></script>
    <script src="http://freshpin.com/cdn/scripts/cookies.js" type="text/javascript"></script>
    <script src="http://freshpin.com/cdn/scripts/jquery-hashchange/jquery.ba-hashchange.min.js"  type="text/javascript"></script>
    <script src="freshpin.js" type="text/javascript"></script>
   
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
    <script type="text/javascript">
        $(document).ready(function () {
            //replace 'Sitename' with 'FreshPin'
            $('a, p, h1, h2, h3').html(function () {
                return $(this).html().replace(/SiteName/g, 'FreshPin');
            });

            show(h() || 'about');

            function show(id) {
                $('#AboutLeft a').prop('class', '');
                $('#AboutRight div').hide(); // Hide All Divs

                if (id == "help") {
                    $('#helpcontent div').show();
                }

                $("#" + id + "content").show(); // Show Div                    
                $('#' + id).prop('class', 'selected');
                $('#AboutLeft .selected').addClass('loaded');

                return false;
            }
            $(window).bind('hashchange', function () {
                show(h());
            });
        });            
       
    </script>
</head>
<body>
        <div id="Header">
        <div class="FixedContainer HeaderContainer">
            <div class="freshpinlogo">
                <a name="noblock" href=".">
                    <img src="logo.png" /></a>
                <div class="freshpinlogoTagline">
                    <%=strings.Banner %></div>
            </div>
            <ul id="Navigation">
                <li><a name="noblock" href="about.aspx#about" class="nav"><%=strings.About %><span></span></a>
                    <ul>
                        <li><a name="noblock" href="about.aspx#help"><%=strings.Help %></a></li>
                        <li class="beforeDivider"><a name="noblock" href="about.aspx#copyright"><%=strings.Copyright %></a></li>
                    </ul>
                </li>
                <li id="logininfo" class="logininfo"></li>
            </ul>
            <div id="Search">
                <input type="text" id="query" name="q" size="27" value="Search over 500,000 Pins" />
                <a id="query_button" href="#" class="lg">
                    <img src="http://freshpin.com/cdn/img/search.gif" alt="" /></a>
            </div>
        </div>
    </div>

    <div id="AboutContainer">
        <div id="AboutContent">
            <div id="AboutLeft">
                <ul>
                    <li><a name="noblock" href="#about" id="about"><span><%=strings.Help_1 %>?</span> <span class="pointer normal">
                    </span><span class="pointer hover"></span></a></li>
                    <li><a name="noblock" href="#pinpractices" id="pinpractices"><span><%=strings.Help_2 %></span> <span class="pointer normal">
                    </span><span class="pointer hover"></span></a></li>
                    <li><a name="noblock" href="#help" id="help"><span><%=strings.Help %></span> <span class="pointer normal">
                    </span><span class="pointer hover"></span></a></li>
                </ul>
                <img src="http://freshpin.com/cdn/img/NavRule.png" alt="Navigation Rule" id="NavRule" />
                <ul>
                    <li><a name="noblock" href="#privacy" id="privacy"><span><%=strings.Help_3 %></span> <span class="pointer normal">
                    </span><span class="pointer hover"></span></a></li>
                    <li><a name="noblock" href="#copyright" id="copyright"><span><%=strings.Copyright%></span> <span class="pointer normal"></span>
                        <span class="pointer hover"></span></a></li>
                    <li><a name="noblock" href="#terms" id="terms"><span><%=strings.Help_5 %></span> <span class="pointer normal"></span>
                        <span class="pointer hover"></span></a></li>
                </ul>
            </div>
            <!-- #AboutLeft -->
            <div id="AboutRight">

                <div id="aboutcontent">
                    <h1>                       
                        <%=strings.Help_1 %>?</h1>
                        <p> <%=strings.Help_6 %>.</p>
                        <p> <%=strings.Help_7 %>.</p>
                        <p> <%=strings.Help_8 %>.</p>
                        <p> <%=strings.Help_9 %>.</p>
                    <h2><%=strings.Help_4 %>?</h2>
                    <h3><%=strings.About_1 %></h3>
                        <p><%=strings.About_2 %>.</p>
                    <h3><%=strings.About_3 %></h3>
                        <p><%=strings.About_4 %>.</p>
                    <h3> <%=strings.About_5 %></h3>
                        <p> <%=strings.About_6 %>.</p>
                    <h3><%=strings.About_7 %></h3>
                        <p><%=strings.About_8 %>.</p>
                </div>

                <div id = "pinpracticescontent">
                    <%=strings.Help_Pin_Practices %>
                </div>

                <div id ="helpcontent">
                    <h1> <%=strings.Help %></h1>
                    <!-- This is the generic FAQ section -->
                   <div id="faqs">                                                                                                                                            <div id="pinning101">
                        <h3><%=strings.Help_1 %>?</h3>
                            <%--<a href="#" class="FAQTitle"><%=strings.Help_1 %>?</a></h3>--%>
                       <%-- <div class="FAQContent">--%>
                            <p><%=strings.Help_6 %>.</p>
                            <p><%=strings.Help_7 %>.</p>
                            <p><%=strings.Help_8 %>.</p>
                            <p><%=strings.Help_9 %>.</p>
                            <div class="AboutRule">
                            <%--</div>--%>
                        </div>
                        <h3><%=strings.Help_10 %>?</h3>
                            <%--<a href="#" class="FAQTitle"><%=strings.Help_10 %>?</a></h3>--%>
                        <%--<div class="FAQContent">--%>
                            <p><%=strings.Help_11 %>.</p>
                            <div class="AboutRule">
                            <%--</div>--%>
                        </div>
                        <h3><%=strings.Help_12 %>?</h3>
                            <%--<a href="#" class="FAQTitle"><%=strings.Help_12 %>?</a></h3>--%>
                        <%--<div class="FAQContent">--%>
                            <p><%=strings.Help_13 %>.</p>
                            <div class="AboutRule">
                            <%--</div>--%>
                        </div>
                        <h3><%=strings.Help_14 %>?</h3>
                            <%--<a href="#" class="FAQTitle"><%=strings.Help_14 %>?</a></h3>--%>
                       <%-- <div class="FAQContent">--%>
                            <p><%=strings.Help_15 %>.
                            </p>
                            <div class="AboutRule">
                            <%--</div>--%>
                        </div>
                     </div>
                     <%-- </div>--%>
                </div>
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
