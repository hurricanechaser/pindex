<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pin.aspx.cs" Inherits="Pinjimu.Pin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="en" style="background-image: url('http://cdn.pinjimu.com/img/paper.jpg');
background-repeat: repeat; background-color: #f7f5f5;" xml:lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>Clothes and Such / Pinjimu.</title>
    <meta name="medium" content="image" />
    <link rel="icon" href="favicon.ico" />
      <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="freshpin.js" type="text/javascript"></script>
    <!--[if (gt IE 6)&(lt IE 9)]><link rel="stylesheet" href="ie7-and-up_fa603afa.css" type="text/css" media="all" /><![endif]-->
</head>
<body style="font-family: 'helvetica neue',arial,sans-serif; font-size: 10px; color: #211922;
    margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0;
    padding-right: 0; padding-bottom: 0; padding-left: 0;">
    <div id="Header" style="position: fixed; z-index: 103; top: 0; right: 0; left: 0;
        height: 60px; box-shadow: inset 0 1px #fff, 0 1px 3px rgba(34,25,25,0.4); -moz-box-shadow: inset 0 1px #fff, 0 1px 3px rgba(34,25,25,0.4);
        -webkit-box-shadow: inset 0 1px #fff, 0 1px 3px rgba(34,25,25,0.4); margin-top: 0;
        margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0; padding-right: 0;
        padding-bottom: 0; padding-left: 0; background-color: #ffffff;">
        <div style="width: 900px; margin-top: 0; margin-right: auto; margin-bottom: 0; margin-left: auto;
            padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0; border-top-color: red;
            border-right-color: red; border-bottom-color: red; border-left-color: red; border-top-style: solid;
            border-right-style: solid; border-bottom-style: solid; border-left-style: solid;
            border-top-width: 0px; border-right-width: 0px; border-bottom-width: 0px; border-left-width: 0px;">
            <div style="width: 355px; height: 60px; left: 41%; position: absolute; top: 0; margin-top: 0;
                margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0; padding-right: 0;
                padding-bottom: 0; padding-left: 0;">
                <a href="." style="font-weight: bold; color: #221919; text-decoration: none; outline: none;">
                    <img src="logo.png" style="border-top-width: 0; border-right-width: 0; border-bottom-width: 0;
                        border-left-width: 0;" /></a>
                <div style="font-family: 'helvetica neue' ,arial,sans-serif; font-size: 12px; color: #6e6868;
                    text-align: center; font-weight: bold; margin-top: 0; margin-right: 0; margin-bottom: 0;
                    margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;"
                    align="center">
                    450 Sources, Fresh Daily, Original Sources - Pin On!</div>
            </div>
            <ul id="Navigation" style="position: relative; float: right; z-index: 105; margin-top: 0;
                margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0; padding-right: 0;
                padding-bottom: 0; padding-left: 0;">
                <li style="position: relative; display: inline; font-size: 13px; margin-top: 0; margin-right: 0;
                    margin-bottom: 0; margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0;
                    padding-left: 0; list-style-type: none;"><a href="http://pinpolish.com/about" style="font-weight: bold;
                        color: #524d4d; text-decoration: none; outline: none; position: relative; display: inline-block;
                        text-shadow: 0 1px rgba(255,255,255,1); height: 37px; padding-top: 23px; padding-right: 27px;
                        padding-bottom: 0; padding-left: 14px;">About<span style="position: absolute; top: 27px;
                            right: 14px; width: 7px; height: 6px; background-image: url('http://cdn.pinjimu.com/img/DownArrowGray7.png');
                            background-repeat: no-repeat; background-position: top center;"></span></a>
                    <ul style="position: absolute; display: none; width: 140px; border-top-color: #cccaca;
                        border-top-width: 1px; border-top-style: solid; box-shadow: 0 2px 4px rgba(34,25,25,0.5);
                        -moz-box-shadow: 0 2px 4px rgba(34,25,25,0.5); -webkit-box-shadow: 0 2px 4px rgba(34,25,25,0.5);
                        z-index: 105; top: 35px; left: 0; margin-top: 0; margin-right: 0; margin-bottom: 0;
                        margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;
                        background-color: #fff;">
                        <li style="position: relative; display: inline; font-size: 13px; margin-top: 0; margin-right: 0;
                            margin-bottom: 0; margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0;
                            padding-left: 0; list-style-type: none;"><a href="http://pinpolish.com/help" style="font-weight: normal;
                                color: #524d4d; text-decoration: none; outline: none; display: block; text-align: left;
                                padding-top: 10px; padding-right: 10px; padding-bottom: 5px; padding-left: 10px;">
                                Help</a></li>
                        <li style="position: relative; display: inline; font-size: 13px; margin-top: 0; margin-right: 0;
                            margin-bottom: 0; margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0;
                            padding-left: 0; list-style-type: none;"><a href="" style="font-weight: normal; color: #524d4d;
                                text-decoration: none; outline: none; display: block; text-align: left; padding-top: 5px;
                                padding-right: 10px; padding-bottom: 10px; padding-left: 10px;">Copyright</a></li>
                        <li style="position: relative; display: inline; font-size: 13px; margin-top: 0; margin-right: 0;
                            margin-bottom: 0; margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0;
                            padding-left: 0; list-style-type: none;"><a href="" style="font-weight: normal; color: #524d4d;
                                text-decoration: none; outline: none; display: block; text-align: left; border-top-color: #e1dfdf;
                                border-top-width: 1px; border-top-style: solid; padding-top: 10px; padding-right: 10px;
                                padding-bottom: 5px; padding-left: 10px;">Careers</a></li>
                        <li style="position: relative; display: inline; font-size: 13px; margin-top: 0; margin-right: 0;
                            margin-bottom: 0; margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0;
                            padding-left: 0; list-style-type: none;"><a href="" style="font-weight: normal; color: #524d4d;
                                text-decoration: none; outline: none; display: block; text-align: left; padding-top: 5px;
                                padding-right: 10px; padding-bottom: 5px; padding-left: 10px;">Team</a></li>
                        <li style="position: relative; display: inline; font-size: 13px; margin-top: 0; margin-right: 0;
                            margin-bottom: 0; margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0;
                            padding-left: 0; list-style-type: none;"><a href="" style="font-weight: normal; color: #524d4d;
                                text-decoration: none; outline: none; display: block; text-align: left; padding-top: 5px;
                                padding-right: 10px; padding-bottom: 10px; padding-left: 10px;">Blog</a></li>
                    </ul>
                </li>
                <li style="position: relative; display: inline; font-size: 13px; margin-top: 0; margin-right: 0;
                    margin-bottom: 0; margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0;
                    padding-left: 0; list-style-type: none;"></li>
            </ul>
            <div id="Search" style="float: left; margin-top: 16px; margin-right: 0; margin-bottom: 0;
                margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                <input type="text" id="query" name="q" size="27" value="Search over 500,000 Pins"
                    style="font-family: inherit; font-size: 13px; font-weight: inherit; resize: none;
                    outline: none; line-height: 1.3; color: #8c7e7e; box-shadow: 0 1px #fff, inset 0 1px rgba(34,25,25,0.05);
                    -moz-box-shadow: 0 1px #fff, inset 0 1px rgba(34,25,25,0.05); -webkit-box-shadow: 0 1px #fff, inset 0 1px rgba(34,25,25,0.05);
                    float: left; width: 183px; margin-top: 0; margin-right: 0; margin-bottom: 0;
                    margin-left: 0; padding-top: 5px; padding-right: 5px; padding-bottom: 5px; padding-left: 5px;
                    border-top-color: #d1cfcf; border-right-color: #d1cfcf; border-bottom-color: #d1cfcf;
                    border-left-color: #d1cfcf; border-top-style: solid; border-right-style: solid;
                    border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                    border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #ffffff;" /><a
                        id="query_button" style="cursor: pointer; font-weight: bold; color: #221919;
                        text-decoration: none; outline: none; filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fffcfc', endColorstr='#f0eded');
                        float: left; margin-left: -1px; min-height: 17px; box-shadow: 0 1px rgba(255,255,255,0.9), inset 0 0 2px rgba(255,255,255,0.75);
                        -moz-box-shadow: 0 1px rgba(255,255,255,0.9), inset 0 0 2px rgba(255,255,255,0.75);
                        -webkit-box-shadow: 0 1px rgba(255,255,255,0.9), inset 0 0 2px rgba(255,255,255,0.75);
                        padding-top: 7px; padding-right: 7px; padding-bottom: 2px; padding-left: 7px;
                        border-top-color: #d1cfcf; border-right-color: #d1cfcf; border-bottom-color: #d1cfcf;
                        border-left-color: #d1cfcf; border-top-style: solid; border-right-style: solid;
                        border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                        border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #f0eded;">
                        <img id="s" alt="" src="http://cdn.pinjimu.com/img/search.gif" style="border-top-width: 0;
                            border-right-width: 0; border-bottom-width: 0; border-left-width: 0;" /></a>
            </div>
        </div>
    </div>
    <div style="width: 900px; margin-top: 0; margin-right: auto; margin-bottom: 0; margin-left: auto;
        padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0; border-top-color: red;
        border-right-color: red; border-bottom-color: red; border-left-color: red; border-top-style: solid;
        border-right-style: solid; border-bottom-style: solid; border-left-style: solid;
        border-top-width: 0px; border-right-width: 0px; border-bottom-width: 0px; border-left-width: 0px;">
        <div style="position: fixed; top: 60px; left: 50%; width: 222px; margin-top: 0; margin-right: 0;
            margin-bottom: 0; margin-left: -425px; padding-top: 0; padding-right: 0; padding-bottom: 0;
            padding-left: 0;">
            <div id="SocialShare" style="position: fixed; left: 50%; height: 230px; top: 162px;
                width: 180px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 340px;
                padding-top: 15px; padding-right: 0; padding-bottom: 0; padding-left: 0; background-image: url('http://cdn.pinjimu.com/img/rightSideIconBg.png');
                background-repeat: no-repeat; background-position: left top;">
                <div style="width: 150px !important; clear: both !important; margin-top: 0; margin-right: 0;
                    margin-bottom: 0; margin-left: 15px; padding-top: 0; padding-right: 0; padding-bottom: 0;
                    padding-left: 0;">
                    <ul style="width: 100% !important; float: left !important; background-color: transparent !important;
                        background-image: none !important; outline: 0 none !important; margin-top: 0;
                        margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0; padding-right: 0;
                        padding-bottom: 0; padding-left: 0; border-top-style: none; border-right-style: none;
                        border-bottom-style: none; border-left-style: none; border-top-width: 0; border-right-width: 0;
                        border-bottom-width: 0; border-left-width: 0;">
                        <li style="list-style-type: none !important; display: inline !important; overflow: hidden;
                            background-image: url('http://cdn.pinjimu.com/img/shr-custom-sprite.png') !important;
                            background-repeat: no-repeat !important; float: left !important; height: 40px !important;
                            width: 75px !important; cursor: pointer !important; background-color: transparent !important;
                            outline: 0 none !important; clear: none !important; background-position: 0px bottom !important;
                            margin-top: 3px; margin-right: 0; margin-bottom: 10px; margin-left: 0; padding-top: 0;
                            padding-right: 0; padding-bottom: 0; padding-left: 0; border-top-style: none;
                            border-right-style: none; border-bottom-style: none; border-left-style: none;
                            border-top-width: 0; border-right-width: 0; border-bottom-width: 0; border-left-width: 0;">
                            <a id="pinit" runat="server" style="font-weight: bold; color: #221919; text-decoration: none !important;
                                outline: none; display: block !important; width: 75px !important; height: 40px !important;
                                text-indent: -9999px !important; background-color: transparent !important; border-top-style: none;
                                border-right-style: none; border-bottom-style: none; border-left-style: none;
                                border-top-width: 0; border-right-width: 0; border-bottom-width: 0; border-left-width: 0;">
                            </a></li>
                        <li style="list-style-type: none !important; display: inline !important; overflow: hidden;
                            background-image: url('http://cdn.pinjimu.com/img/shr-custom-sprite.png') !important;
                            background-repeat: no-repeat !important; float: left !important; height: 40px !important;
                            width: 75px !important; cursor: pointer !important; background-color: transparent !important;
                            outline: 0 none !important; clear: none !important; background-position: -75px bottom !important;
                            margin-top: 3px; margin-right: 0; margin-bottom: 10px; margin-left: 0; padding-top: 0;
                            padding-right: 0; padding-bottom: 0; padding-left: 0; border-top-style: none;
                            border-right-style: none; border-bottom-style: none; border-left-style: none;
                            border-top-width: 0; border-right-width: 0; border-bottom-width: 0; border-left-width: 0;">
                            <a runat="server" id="grabit" style="font-weight: bold; color: #221919; text-decoration: none !important;
                                outline: none; display: block !important; width: 75px !important; height: 40px !important;
                                text-indent: -9999px !important; background-color: transparent !important; border-top-style: none;
                                border-right-style: none; border-bottom-style: none; border-left-style: none;
                                border-top-width: 0; border-right-width: 0; border-bottom-width: 0; border-left-width: 0;">
                            </a></li>
                        <li style="list-style-type: none !important; display: inline !important; overflow: hidden;
                            background-image: url('http://cdn.pinjimu.com/img/shr-custom-sprite.png') !important;
                            background-repeat: no-repeat !important; float: left !important; height: 40px !important;
                            width: 75px !important; cursor: pointer !important; background-color: transparent !important;
                            outline: 0 none !important; clear: none !important; background-position: -147px bottom !important;
                            margin-top: 3px; margin-right: 0; margin-bottom: 10px; margin-left: 0; padding-top: 0;
                            padding-right: 0; padding-bottom: 0; padding-left: 0; border-top-style: none;
                            border-right-style: none; border-bottom-style: none; border-left-style: none;
                            border-top-width: 0; border-right-width: 0; border-bottom-width: 0; border-left-width: 0;">
                            <a href="#" style="font-weight: bold; color: #221919; text-decoration: none !important;
                                outline: none; display: block !important; width: 75px !important; height: 40px !important;
                                text-indent: -9999px !important; background-color: transparent !important; border-top-style: none;
                                border-right-style: none; border-bottom-style: none; border-left-style: none;
                                border-top-width: 0; border-right-width: 0; border-bottom-width: 0; border-left-width: 0;">
                            </a></li>
                        <li style="list-style-type: none !important; display: inline !important; overflow: hidden;
                            background-image: url('http://cdn.pinjimu.com/img/shr-custom-sprite.png') !important;
                            background-repeat: no-repeat !important; float: left !important; height: 40px !important;
                            width: 75px !important; cursor: pointer !important; background-color: transparent !important;
                            outline: 0 none !important; clear: none !important; background-position: -220px bottom !important;
                            margin-top: 3px; margin-right: 0; margin-bottom: 10px; margin-left: 0; padding-top: 0;
                            padding-right: 0; padding-bottom: 0; padding-left: 0; border-top-style: none;
                            border-right-style: none; border-bottom-style: none; border-left-style: none;
                            border-top-width: 0; border-right-width: 0; border-bottom-width: 0; border-left-width: 0;">
                            <a href="#" style="font-weight: bold; color: #221919; text-decoration: none !important;
                                outline: none; display: block !important; width: 75px !important; height: 40px !important;
                                text-indent: -9999px !important; background-color: transparent !important; border-top-style: none;
                                border-right-style: none; border-bottom-style: none; border-left-style: none;
                                border-top-width: 0; border-right-width: 0; border-bottom-width: 0; border-left-width: 0;">
                            </a></li>
                    </ul>
                </div>
                <div style="color: #524D4D; font-size: 13px; margin-top: 0; margin-right: 0; margin-bottom: 0;
                    margin-left: 0; padding-top: 10px; padding-right: 0; padding-bottom: 0; padding-left: 18px;">
                    <span style="font-weight: bold; color: #524D4D;">Related Searches</span> <a runat="server"
                        id="source" target="_blank" style="font-weight: normal !important; color: #524D4D;
                        text-decoration: underline !important; outline: none;">weheartit.com</a>
                    <br />
                    <a runat="server" id="cat" style="font-weight: normal !important; color: #524D4D;
                        text-decoration: underline !important; outline: none;">3D</a>
                </div>
            </div>
        </div>
        <div style="float: left; width: 766px; margin-top: 100px; margin-right: 0; margin-bottom: 0;
            margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
            <div style="position: relative; box-shadow: 0 1px 3px rgba(34,25,25,0.4); -moz-box-shadow: 0 1px 3px rgba(34,25,25,0.4);
                -webkit-box-shadow: 0 1px 3px rgba(34,25,25,0.4); margin-top: 0; margin-right: auto;
                margin-bottom: 32px; margin-left: auto; padding-top: 0; padding-right: 0; padding-bottom: 0;
                padding-left: 0; background-color: #fff;">
                <div style="clear: both; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                    padding-top: 20px; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                </div>
                <div id="PinActionButtons" style="height: 26px; width: 280px; float: left; overflow: hidden;
                    margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 30px; padding-top: 0;
                    padding-right: 0; padding-bottom: 0; padding-left: 13px; background-image: url('http://cdn.pinjimu.com/img/detailiconBg.jpg');
                    background-repeat: no-repeat; background-position: left top;">
                    <a href="" style="font-weight: bold; color: #221919; text-decoration: none; outline: none;">
                        <div style="height: 26px; font-size: 12px; font-weight: bold; float: left; display: block;
                            color: #777176; font-family: Helvetica, Arial, Sans-Serif; text-align: left;
                            line-height: 26px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                            padding-top: 0; padding-right: 14px; padding-bottom: 0; padding-left: 30px; background-image: url('http://cdn.pinjimu.com/img/likeIconDetailPage.png');
                            background-repeat: no-repeat; background-position: left 5px;" align="left">
                            Like</div>
                    </a><a href="" style="font-weight: bold; color: #221919; text-decoration: none; outline: none;">
                        <div style="height: 26px; font-size: 12px; font-weight: bold; float: left; display: block;
                            color: #777176; font-family: Helvetica, Arial, Sans-Serif; text-align: left;
                            line-height: 26px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                            padding-top: 0; padding-right: 14px; padding-bottom: 0; padding-left: 23px; background-image: url('http://cdn.pinjimu.com/img/repinIconDetailPage.png');
                            background-repeat: no-repeat; background-position: left 4px;" align="left">
                            Repin</div>
                    </a><a href="" style="font-weight: bold; color: #221919; text-decoration: none; outline: none;">
                        <div style="height: 26px; font-size: 12px; font-weight: bold; float: left; display: block;
                            color: #777176; font-family: Helvetica, Arial, Sans-Serif; text-align: left;
                            line-height: 26px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                            padding-top: 0; padding-right: 14px; padding-bottom: 0; padding-left: 25px; background-image: url('http://cdn.pinjimu.com/img/commentIconDetailPage.png');
                            background-repeat: no-repeat; background-position: left 4px;" align="left">
                            Comment</div>
                    </a>
                </div>
                <div id="PinCaption" runat="server" style="color: #524D4D; float: right; width: 400px;
                    font-size: 13px; font-weight: bold; word-wrap: break-word; margin-top: 8px; margin-right: 30px;
                    margin-bottom: 0; margin-left: 10px; padding-top: 0; padding-right: 0; padding-bottom: 0;
                    padding-left: 0;">
                    zebra Pinjimu</div>
                <div style="clear: both; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                    padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                </div>
                <div id="PinImage" style="display: block; position: relative; overflow: hidden; margin-top: 20px;
                    margin-right: 30px; margin-bottom: 30px; margin-left: 30px; padding-top: 0; padding-right: 0;
                    padding-bottom: 0; padding-left: 0; background-color: #fff;">
                    <img runat="server" id="pinCloseupImage" alt="Pinned Image" style="display: block;
                        max-width: 554px; margin-top: 0; margin-right: auto; margin-bottom: 0; margin-left: auto;
                        border-top-width: 0; border-right-width: 0; border-bottom-width: 0; border-left-width: 0;" /></div>
            </div>
        </div>
    </div>
</body>
</html>
