﻿<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="Nails" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>PinPolish Create Account</title>
    <link rel="icon" href="favicon.ico" /> 
    
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.7.2.min.js"></script>
     <script src="http://cdn.pinpolish.com/scripts/cookies.js" type="text/javascript"></script>
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
    <style type="text/css">
       
    </style>
    <script type="text/javascript">
        var emailregex = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@(([0-9a-zA-Z])+([-\w]*[0-9a-zA-Z])*\.)+[a-zA-Z]{2,9})$/;
        function EmailProcess() {
            var email = $('#invitationBox').val();
            if (email != '' && emailregex.test(email)) {
                FreshPin.trackGACEvents('requestinvite', 'sendrequest');
                $.post('POST.ashx?t=ri', { email: email }, function(data, res) { $('#err').html(data); }, 'text');
            }
            else
                $('#err').html('Please enter a valid email address');
        }

        $(function () {
            $('#invitationBox').keypress(function (e) {
                var k = (e.keyCode ? e.keyCode : e.which);
                if (k == 13) {
                    EmailProcess();
                }
            });
        
            $('.BlueButton').click(function () {                
                EmailProcess();
            });

        });
    </script>
</head>
<body>
    <div id="signUpWraper">
        <div class="LogoHeader">
            <div style="margin: 0 auto; margin-left: 300px;">
                <a href="."> <img src="logo-login.png" /> </a>
            </div>
        </div>
        <div style="text-align: center; line-height: 80px; font-size: 32px; font-weight: bold;">
            <span><%=strings.Sign_Up %>&nbsp; <%=strings.PinPolish %>.</span></div>
       
        <div style="text-align: center; line-height: 40px; font-size:15px; ">
            <span>or <a style="color:#a19191" href="login"><%=strings.Login %> </a> <%=strings.Login_1 %>.</span></div>
        <div>          
            <div class="inputBox">
                <input type="text" id="invitationBox" class="" />
            </div>
            <div class="BlueButton">
                <a href="javascript:void(0)"><%=strings.Request_Invitation %></a>
            </div>
            <div style="clear:both">
            </div>
            <div style="text-align: center;">
                <span id="err" style="color: red; font-size: 14px; font-weight: bold;"></span>
            </div>
        </div>
    </div>
</body>
</html>
