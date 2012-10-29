﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="settings.aspx.cs" Inherits="PindexProd.settings" %>

<div style="width: 900px; margin-top: 0; margin-right: auto; margin-bottom: 0; margin-left: auto;
    padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0; border-top-color: red;
    border-right-color: red; border-bottom-color: red; border-left-color: red; border-top-style: solid;
    border-right-style: solid; border-bottom-style: solid; border-left-style: solid;
    border-top-width: 0px; border-right-width: 0px; border-bottom-width: 0px; border-left-width: 0px;">
    <h3 style="font-size: 28px; font-weight: bold; line-height: 1.1em; color: #524d4d;
        text-shadow: 0 1px rgba(255,255,255,0.9); border-bottom-width: 3px; border-bottom-style: double;
        border-bottom-color: rgba(34,25,25,0.1); margin-top: 0; margin-right: 0; margin-bottom: 0;
        margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 18px; padding-left: 0;">
        Edit Profile</h3>
    <ul style="margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0;
        padding-right: 0; padding-bottom: 0; padding-left: 0;">
        <li style="display: block; font-size: 21px; font-weight: 300; clear: both; color: #8c7e7e;
            text-shadow: 0 1px rgba(255,255,255,0.9); border-top-style: solid; border-top-width: 1px;
            border-top-color: rgba(255,255,255,0.7); border-bottom-style: solid; border-bottom-width: 1px;
            border-bottom-color: rgba(34,25,25,0.1); float: left; width: 100%; margin-top: 0;
            margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 15px; padding-right: 0;
            padding-bottom: 15px; padding-left: 0; list-style-type: none;">
            <label for="email" style="display: inline-block; line-height: 1.4em; font-size: 18px;
                float: left; width: 150px; padding-top: 7px; vertical-align: top;">
                Email</label>
            <div style="float: left; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                <input type="text" id="email" name="email" runat="server" style="font-family: inherit;
                    font-size: 18px; font-weight: 300; resize: none; outline: none; line-height: 1.4;
                    color: #221919; box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
                    -moz-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
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
                <span style="display: inline-block; margin-top: 5px; color: #8C7e7e; font-size: 13px;
                    max-width: 199px; margin-left: 10px;">Not shown publicly</span>
            </div>
        </li>
        <!-- <li>
                    <label>
                        Notifications</label>
                    <div class="Right">
                        <a href="javascript:void(0);"><strong>Change Email
                            Settings</strong><span></span></a>
                    </div>
                </li>-->
        <li style="display: block; font-size: 21px; font-weight: 300; clear: both; color: #8c7e7e;
            text-shadow: 0 1px rgba(255,255,255,0.9); border-top-style: solid; border-top-width: 1px;
            border-top-color: rgba(255,255,255,0.7); border-bottom-style: solid; border-bottom-width: 1px;
            border-bottom-color: rgba(34,25,25,0.1); float: left; width: 100%; margin-top: 0;
            margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 15px; padding-right: 0;
            padding-bottom: 15px; padding-left: 0; list-style-type: none;">
            <label style="display: inline-block; line-height: 1.4em; font-size: 18px; float: left;
                width: 150px; padding-top: 7px; vertical-align: top;">
                Password</label>
            <div style="float: left; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                <a href="#changepassword" style="font-weight: bold; color: #524d4d;
                    text-decoration: none; outline: none; position: relative; display: inline-block;
                    text-align: center; line-height: 1em; border-radius: 6px; -moz-border-radius: 6px;
                    -webkit-border-radius: 6px; -moz-transition-property: color, -moz-box-shadow, text-shadow;
                    -moz-transition-duration: .05s; -moz-transition-timing-function: ease-in-out;
                    -webkit-transition-property: color, -webkit-box-shadow, text-shadow; -webkit-transition-duration: .05s;
                    -webkit-transition-timing-function: ease-in-out; box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                    -moz-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                    -webkit-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                    font-size: 18px; text-shadow: 0 1px rgba(255,255,255,0.9); padding-top: .45em;
                    padding-right: .825em; padding-bottom: .45em; padding-left: .825em; border-top-color: transparent;
                    border-right-color: transparent; border-bottom-color: transparent; border-left-color: transparent;
                    border-top-style: solid; border-right-style: solid; border-bottom-style: solid;
                    border-left-style: solid; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px;
                    border-left-width: 1px;"><strong style="font-style: normal; font-weight: bold; position: relative;
                        z-index: 2;">Change Password</strong><span style="position: absolute; z-index: 1;
                            top: -1px; right: -1px; bottom: -1px; left: -1px; display: block; opacity: 1;
                            border-radius: 6px; -moz-border-radius: 6px; -webkit-border-radius: 6px; box-shadow: inset 0 1px rgba(255,255,255,0.35);
                            -moz-box-shadow: inset 0 1px rgba(255,255,255,0.35); -webkit-box-shadow: inset 0 1px rgba(255,255,255,0.35);
                            -moz-transition-property: opacity; -moz-transition-duration: 0.5s; -moz-transition-timing-function: ease-in-out;
                            -webkit-transition-property: opacity; -webkit-transition-duration: 0.5s; -webkit-transition-timing-function: ease-in-out;
                            font-size: 18px; filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfafb', endColorstr='#f0eded');
                            border-top-color: #bbb; border-right-color: #bbb; border-bottom-color: #bbb;
                            border-left-color: #bbb; border-top-style: solid; border-right-style: solid;
                            border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                            border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #f0eded;
                            background-position: 0% 0%;"></span></a>
            </div>
        </li>
        <li style="display: block; font-size: 21px; font-weight: 300; clear: both; color: #8c7e7e;
            text-shadow: 0 1px rgba(255,255,255,0.9); border-top-style: solid; border-top-width: 1px;
            border-top-color: rgba(255,255,255,0.7); border-bottom-style: solid; border-bottom-width: 1px;
            border-bottom-color: rgba(34,25,25,0.1); float: left; width: 100%; margin-top: 0;
            margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 15px; padding-right: 0;
            padding-bottom: 15px; padding-left: 0; list-style-type: none;">
            <label for="first_name" style="display: inline-block; line-height: 1.4em; font-size: 18px;
                float: left; width: 150px; padding-top: 7px; vertical-align: top;">
                First Name</label>
            <div style="float: left; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                <input type="text" id="first_name" runat="server" name="first_name" style="font-family: inherit;
                    font-size: 18px; font-weight: 300; resize: none; outline: none; line-height: 1.4;
                    color: #221919; box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
                    -moz-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
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
        <li style="display: block; font-size: 21px; font-weight: 300; clear: both; color: #8c7e7e;
            text-shadow: 0 1px rgba(255,255,255,0.9); border-top-style: solid; border-top-width: 1px;
            border-top-color: rgba(255,255,255,0.7); border-bottom-style: solid; border-bottom-width: 1px;
            border-bottom-color: rgba(34,25,25,0.1); float: left; width: 100%; margin-top: 0;
            margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 15px; padding-right: 0;
            padding-bottom: 15px; padding-left: 0; list-style-type: none;">
            <label for="username" style="display: inline-block; line-height: 1.4em; font-size: 18px;
                float: left; width: 150px; padding-top: 7px; vertical-align: top;">
                Username</label>
            <div style="float: left; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                <input type="text" id="username1" runat="server" name="username" style="font-family: inherit;
                    font-size: 18px; font-weight: 300; resize: none; outline: none; line-height: 1.4;
                    color: #221919; box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
                    -moz-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
                    -webkit-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
                    display: inline-block; box-sizing: border-box; -moz-box-sizing: border-box; -ms-box-sizing: border-box;
                    -webkit-box-sizing: border-box; border-radius: 6px; -moz-border-radius: 6px;
                    -webkit-border-radius: 6px; -moz-transition: all 0.08s ease-in-out; -webkit-transition: all 0.08s ease-in-out;
                    margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                    padding-top: 6px; padding-right: 12px; padding-bottom: 6px; padding-left: 12px;
                    border-top-color: #a4a2a2; border-right-color: #a4a2a2; border-bottom-color: #a4a2a2;
                    border-left-color: #a4a2a2; border-top-style: solid; border-right-style: solid;
                    border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                    border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #fff;" />
                <span id="usernameview" runat="server"></span>
            </div>
        </li>
        <li style="display: block; font-size: 21px; font-weight: 300; clear: both; color: #8c7e7e;
            text-shadow: 0 1px rgba(255,255,255,0.9); border-top-style: solid; border-top-width: 1px;
            border-top-color: rgba(255,255,255,0.7); border-bottom-style: solid; border-bottom-width: 1px;
            border-bottom-color: rgba(34,25,25,0.1); float: left; width: 100%; margin-top: 0;
            margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 15px; padding-right: 0;
            padding-bottom: 15px; padding-left: 0; list-style-type: none;">
            <label for="about" style="display: inline-block; line-height: 1.4em; font-size: 18px;
                float: left; width: 150px; padding-top: 7px; vertical-align: top;">
                About</label>
            <div style="float: left; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                <textarea name="about" runat="server" cols="54" rows="3" id="aboutu" style="font-family: inherit;
                    font-size: 18px; font-weight: 300; resize: none; outline: none; line-height: 1.4;
                    color: #221919; box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
                    -moz-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
                    -webkit-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
                    display: inline-block; box-sizing: border-box; -moz-box-sizing: border-box; -ms-box-sizing: border-box;
                    -webkit-box-sizing: border-box; border-radius: 6px; -moz-border-radius: 6px;
                    -webkit-border-radius: 6px; -moz-transition: all 0.08s ease-in-out; -webkit-transition: all 0.08s ease-in-out;
                    min-height: 90px; min-width: 375px; margin-top: 0; margin-right: 0; margin-bottom: 0;
                    margin-left: 0; padding-top: 6px; padding-right: 12px; padding-bottom: 6px; padding-left: 12px;
                    border-top-color: #a4a2a2; border-right-color: #a4a2a2; border-bottom-color: #a4a2a2;
                    border-left-color: #a4a2a2; border-top-style: solid; border-right-style: solid;
                    border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                    border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #fff;"></textarea>
            </div>
        </li>
        <li style="display: block; font-size: 21px; font-weight: 300; clear: both; color: #8c7e7e;
            text-shadow: 0 1px rgba(255,255,255,0.9); border-top-style: solid; border-top-width: 1px;
            border-top-color: rgba(255,255,255,0.7); border-bottom-style: solid; border-bottom-width: 1px;
            border-bottom-color: rgba(34,25,25,0.1); float: left; width: 100%; margin-top: 0;
            margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 15px; padding-right: 0;
            padding-bottom: 15px; padding-left: 0; list-style-type: none;">
            <label for="location" style="display: inline-block; line-height: 1.4em; font-size: 18px;
                float: left; width: 150px; padding-top: 7px; vertical-align: top;">
                Location</label>
            <div style="float: left; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                <input type="text" id="location" runat="server" name="location" style="font-family: inherit;
                    font-size: 18px; font-weight: 300; resize: none; outline: none; line-height: 1.4;
                    color: #221919; box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
                    -moz-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
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
                <span style="display: inline-block; margin-top: 5px; color: #8C7e7e; font-size: 13px;
                    max-width: 199px; margin-left: 10px;">e.g. Palo Alto, CA</span>
            </div>
        </li>
        <li style="display: block; font-size: 21px; font-weight: 300; clear: both; color: #8c7e7e;
            text-shadow: 0 1px rgba(255,255,255,0.9); border-top-style: solid; border-top-width: 1px;
            border-top-color: rgba(255,255,255,0.7); border-bottom-style: solid; border-bottom-width: 1px;
            border-bottom-color: rgba(34,25,25,0.1); float: left; width: 100%; margin-top: 0;
            margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 15px; padding-right: 0;
            padding-bottom: 15px; padding-left: 0; list-style-type: none;">
            <label for="website" style="display: inline-block; line-height: 1.4em; font-size: 18px;
                float: left; width: 150px; padding-top: 7px; vertical-align: top;">
                Website</label>
            <div style="float: left; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                <input type="text" id="website" name="website" runat="server" style="font-family: inherit;
                    font-size: 18px; font-weight: 300; resize: none; outline: none; line-height: 1.4;
                    color: #221919; box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
                    -moz-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px rgba(255,255,255,0.8);
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
        <li style="display: block; font-size: 21px; font-weight: 300; clear: both; color: #8c7e7e;
            text-shadow: 0 1px rgba(255,255,255,0.9); border-top-style: solid; border-top-width: 1px;
            border-top-color: rgba(255,255,255,0.7); border-bottom-style: solid; border-bottom-width: 1px;
            border-bottom-color: rgba(34,25,25,0.1); float: left; width: 100%; margin-top: 0;
            margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 15px; padding-right: 0;
            padding-bottom: 15px; padding-left: 0; list-style-type: none;">
            <label for="id_img" style="display: inline-block; line-height: 1.4em; font-size: 18px;
                float: left; width: 150px; padding-top: 7px; vertical-align: top;">
                Image</label>
            <div style="float: left; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                <div style="box-shadow: 0 1px 1px rgba(34, 25, 25, 0.4); float: left; min-height: 168px;
                    min-width: 168px; position: relative; margin-top: 0; margin-right: 0; margin-bottom: 0;
                    margin-left: 0; padding-top: 12px; padding-right: 12px; padding-bottom: 12px;
                    padding-left: 12px; background-image: none; background-attachment: scroll; background-repeat: repeat;
                    background-color: #FFFFFF;">
                    <img src="http://freshpin.com/cdn/img/load2.gif" alt="Loading..."
                        style="display: none; border-top-width: 0; border-right-width: 0; border-bottom-width: 0;
                        border-left-width: 0;" />
                    <img alt="Current profile picture" id="uploadedUserImage" runat="server" src="http://freshpin.com/cdn/img/userImage.gif"
                        style="float: left !important; border-top-width: 0; border-right-width: 0; border-bottom-width: 0;
                        border-left-width: 0;" align="left !important" />
                </div>
                <div style="float: left !important; margin-top: 0; margin-right: 0; margin-bottom: 0;
                    margin-left: 0; padding-top: 6px; padding-right: 0; padding-bottom: 6px; padding-left: 12px;"
                    class="floatLeft NoInput">
                    <p style="line-height: 1.35em; margin-top: 0; margin-right: 0; margin-bottom: .8em;
                        margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                        <input type="file" id="ftsettings"  style="font-weight: bold; color: #524d4d; text-decoration: none; outline: none;
                            position: relative; display: inline-block; text-align: center; line-height: 1em;
                            border-radius: 6px; -moz-border-radius: 6px; -webkit-border-radius: 6px; -moz-transition-property: color, -moz-box-shadow, text-shadow;
                            -moz-transition-duration: .05s; -moz-transition-timing-function: ease-in-out;
                            -webkit-transition-property: color, -webkit-box-shadow, text-shadow; -webkit-transition-duration: .05s;
                            -webkit-transition-timing-function: ease-in-out; box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                            -moz-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                            -webkit-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                            font-size: 18px; text-shadow: 0 1px rgba(255,255,255,0.9); padding-top: .45em;
                            padding-right: .825em; padding-bottom: .45em; padding-left: .825em; border-top-color: transparent;
                            border-right-color: transparent; border-bottom-color: transparent; border-left-color: transparent;
                            border-top-style: solid; border-right-style: solid; border-bottom-style: solid;
                            border-left-style: solid; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px;
                            border-left-width: 1px;"></input>
                    </p>
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
    <div style="border-top-color: rgba(255,255,255,0.7); border-top-width: 1px; border-top-style: solid;
        float: left; margin-top: 0; margin-right: 0; margin-bottom: 20px; margin-left: 0;
        padding-top: 24px; padding-right: 0; padding-bottom: 0; padding-left: 150px;">
        <a id="sp" href="javascript:void(0)" style="font-weight: bold; color: #FCF9F9; text-decoration: none; outline: none;
            display: inline-block; clear: both; line-height: 48px; font-size: 24px; text-shadow: 0 -1px rgba(34, 25, 25, 0.5);
            font-family: 'helvetica neue' ,arial,sans-serif; margin-top: 10px; margin-right: 0;
            margin-bottom: 0; margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0;
            padding-left: 20px; background-image: url('http://freshpin.com/cdn/img/blue_Btn_Lft.gif');
            background-repeat: no-repeat; background-position: left top;"><span style="display: block;
                clear: both; background-repeat: no-repeat; background-image: url('http://freshpin.com/cdn/img/blue_Btn_Rgt.gif');
                background-position: right top; line-height: 48px; padding-top: 0; padding-left: 0;
                padding-right: 20px; padding-bottom: 0;">Save Profile</span></a></div>
</div>
