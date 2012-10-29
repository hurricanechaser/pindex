<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editboard.aspx.cs" Inherits="HairStyle.editboard" %>


<div  style="width: 852px; margin-top: 0; margin-right: auto; margin-bottom: 0; margin-left: auto;
    padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
    <div style="border-bottom-width: 3px; border-bottom-color: rgba(34,25,25,0.1); border-bottom-style: double;
        margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0;
        padding-right: 0; padding-bottom: 0; padding-left: 0;">
        <a href="http://pinterest.com/jagadeesan/me-2/settings/#" id="delete_confirm" onclick="Modal.show('DeleteBoard'); return false"
            style="font-weight: bold; color: #524d4d; text-decoration: none; outline: none;
            position: relative; display: inline-block; text-align: center; line-height: 1em;
            border-radius: 6px; -moz-border-radius: 6px; -webkit-border-radius: 6px; -moz-transition-property: color,
-moz-box-shadow, text-shadow;
            -moz-transition-duration: .05s; -moz-transition-timing-function: ease-in-out;
            -webkit-transition-property: color, -webkit-box-shadow, text-shadow; -webkit-transition-duration: .05s;
            -webkit-transition-timing-function: ease-in-out; box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
            -moz-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
            -webkit-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
            cursor: pointer; font-size: 18px; text-shadow: 0 1px rgba(255,255,255,0.9); float: right;
            margin-left: 10px; padding-top: .45em; padding-right: .825em; padding-bottom: .45em;
            padding-left: .825em; border-top-color: transparent; border-right-color: transparent;
            border-bottom-color: transparent; border-left-color: transparent; border-top-style: solid;
            border-right-style: solid; border-bottom-style: solid; border-left-style: solid;
            border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px;">
            <strong style="font-style: normal; font-weight: bold; position: relative; z-index: 2;">
                Delete Board</strong><span style="position: absolute; z-index: 1; top: -1px; right: -1px;
                    bottom: -1px; left: -1px; display: block; opacity: 1; border-radius: 6px; -moz-border-radius: 6px;
                    -webkit-border-radius: 6px; box-shadow: inset 0 1px rgba(255,255,255,0.35); -moz-box-shadow: inset 0 1px rgba(255,255,255,0.35);
                    -webkit-box-shadow: inset 0
1px rgba(255,255,255,0.35); -moz-transition-property: opacity;
                    -moz-transition-duration: 0.5s; -moz-transition-timing-function: ease-in-out;
                    -webkit-transition-property: opacity; -webkit-transition-duration: 0.5s; -webkit-transition-timing-function: ease-in-out;
                    font-size: 18px; filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfafb',
endColorstr='#f0eded');
                    border-top-color: #bbb; border-right-color: #bbb; border-bottom-color: #bbb;
                    border-left-color: #bbb; border-top-style: solid; border-right-style: solid;
                    border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                    border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #f0eded;
                    background-position: 0% 0%;"></span></a>
        <h3 style="font-size: 28px; font-weight: bold; line-height: 1.1em; color: #524d4d;
            text-shadow: 0 1px rgba(255,255,255,0.9); border-bottom-width: 0; border-bottom-color: rgba(34,25,25,0.1);
            border-bottom-style: double; overflow: hidden; margin-top: 0; margin-right: 0;
            margin-bottom: 0; margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 18px;
            padding-left: 0;">
            Edit Board / <a href="http://pinterest.com/jagadeesan/me-2/" style="font-weight: 300;
                color: #221919; text-decoration: none; outline: none;">me 2</a>
        </h3>
    </div>
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
                    Title</label></div>
            <div style="float: left; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                <input type="text" name="name" value="me 2" id="id_name" style="font-family: inherit;
                    font-size: 18px; font-weight: 300; resize: none; outline: none; line-height: 1.4;
                    color: #221919; box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px #fff; -moz-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px #fff;
                    -webkit-box-shadow: inset 0 1px rgba(34,25,25,0.15),
0 1px #fff; display: inline-block;
                    box-sizing: border-box; -moz-box-sizing: border-box; -ms-box-sizing: border-box;
                    -webkit-box-sizing: border-box; border-radius: 6px; -moz-border-radius: 6px;
                    -webkit-border-radius: 6px; -moz-transition: all 0.08s
ease-in-out; -webkit-transition: all 0.08s ease-in-out;
                    min-width: 375px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                    padding-top: 6px; padding-right: 12px; padding-bottom: 6px; padding-left: 12px;
                    border-top-color: #ad9c9c; border-right-color: #ad9c9c; border-bottom-color: #ad9c9c;
                    border-left-color: #ad9c9c; border-top-style: solid; border-right-style: solid;
                    border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                    border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #fff;" />
            </div>
        </li>
        <!-- Board Description
			-->
        <li style="display: block; font-size: 21px; font-weight: 300; clear: both; color: #8c7e7e;
            text-shadow: 0 1px rgba(255,255,255,0.9); border-top-style: solid; border-top-width: 1px;
            border-top-color: rgba(255,255,255,0.7); border-bottom-style: solid; border-bottom-width: 1px;
            border-bottom-color: rgba(34,25,25,0.1); float: left; width: 100%; margin-top: 0;
            margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 15px; padding-right: 0;
            padding-bottom: 15px; padding-left: 0; list-style-type: none;">
            <div style="width: 150px; float: left; margin-top: 0; margin-right: 0; margin-bottom: 0;
                margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                <label for="id_description" style="display: inline-block; line-height: 1.4em; font-size: 18px;
                    float: left; width: 150px; padding-top: 7px; vertical-align: top;">
                    Description</label></div>
            <div style="float: left; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                <textarea id="id_description" rows="3" cols="54" name="description" maxlength="500"
                    style="font-family: inherit; font-size: 18px; font-weight: 300; resize: none;
                    outline: none; line-height: 1.4; color: #221919; box-shadow: inset 0 1px rgba(34,25,25,0.15),
0 1px #fff;
                    -moz-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px #fff; -webkit-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px #fff;
                    display: inline-block; box-sizing: border-box; -moz-box-sizing: border-box; -ms-box-sizing: border-box;
                    -webkit-box-sizing: border-box; border-radius: 6px; -moz-border-radius: 6px;
                    -webkit-border-radius: 6px; -moz-transition: all 0.08s ease-in-out; -webkit-transition: all 0.08s ease-in-out;
                    min-height: 90px; min-width: 375px; margin-top: 0; margin-right: 0; margin-bottom: 0;
                    margin-left: 0; padding-top: 6px; padding-right: 12px; padding-bottom: 6px; padding-left: 12px;
                    border-top-color: #ad9c9c; border-right-color: #ad9c9c; border-bottom-color: #ad9c9c;
                    border-left-color: #ad9c9c; border-top-style: solid; border-right-style: solid;
                    border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                    border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #fff;"></textarea></div>
        </li>
        <!-- Board Category -->
        <li style="display: block; font-size: 21px; font-weight: 300; clear: both; color: #8c7e7e;
            text-shadow: 0 1px
rgba(255,255,255,0.9); border-top-style: solid; border-top-width: 1px;
            border-top-color: rgba(255,255,255,0.7); border-bottom-style: solid; border-bottom-width: 1px;
            border-bottom-color: rgba(34,25,25,0.1); float: left; width: 100%; margin-top: 0;
            margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 15px; padding-right: 0;
            padding-bottom: 15px; padding-left: 0; list-style-type: none;">
            <input type="hidden" name="category" value="film_music_books" id="id_category" style="font-family: inherit;
                font-size: inherit; font-weight: inherit; resize: none; outline: none; line-height: 1em;
                color: #8c7e7e; box-shadow: inset
0 0 2px rgba(255,255,255,0.75); -moz-box-shadow: inset 0 0 2px rgba(255,255,255,0.75);
                -webkit-box-shadow: inset 0 0 2px rgba(255,255,255,0.75); margin-top: 0; margin-right: 0;
                margin-bottom: 0; margin-left: 0; padding-top: 5px; padding-right: 5px; padding-bottom: 5px;
                padding-left: 5px; border-top-color: #d1cdcd; border-right-color: #d1cdcd; border-bottom-color: #d1cdcd;
                border-left-color: #d1cdcd; border-top-style: solid; border-right-style: solid;
                border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #fcf9f9;" />
            <div style="width: 150px; float: left; margin-top: 0; margin-right: 0; margin-bottom: 0;
                margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                <label style="display: inline-block; line-height: 1.4em; font-size: 18px; float: left;
                    width: 150px; padding-top: 7px; vertical-align: top;">
                    Category</label></div>
            <div style="float: left; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                <div style="display: none; position: fixed; z-index: 9998; top: 0; right: 0; bottom: 0;
                    left: 0; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0;
                    padding-right: 0; padding-bottom: 0; padding-left: 0;">
                </div>
                <div id="CategoryPicker" style="width: 337px; position: relative; display: block;
                    filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fffcfc',
endColorstr='#f0eded');
                    cursor: pointer; border-radius: 6px; -moz-border-radius: 6px; -webkit-border-radius: 6px;
                    box-shadow: inset 0 1px 1px rgba(34,25,25,0.1),
0 1px #fff; -moz-box-shadow: inset 0 1px 1px rgba(34,25,25,0.1), 0 1px #fff;
                    -webkit-box-shadow: inset 0 1px 1px rgba(34,25,25,0.1), 0 1px #fff; margin-top: 0;
                    margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 6px; padding-right: 24px;
                    padding-bottom: 6px; padding-left: 12px; border-top-color: #ad9c9c; border-right-color: #ad9c9c;
                    border-bottom-color: #ad9c9c; border-left-color: #ad9c9c; border-top-style: solid;
                    border-right-style: solid; border-bottom-style: solid; border-left-style: solid;
                    border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px;
                    background-color: #f0eded;">
                    <div style="margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0;
                        padding-right: 0; padding-bottom: 0; padding-left: 0;">
                        <span style="display: block; white-space: nowrap; overflow: hidden; font-size: 18px;">
                            Film, Music &amp; Books</span> <span style="position: absolute; top: 14px; right: 14px;
                                width: 11px; height: 9px; background-image: url(../images/downArrow.png); background-repeat: no-repeat;
                                background-position: center top;"></span>
                    </div>
                    <!--
						.BoardList -->
                </div>
                <!-- #CategoryPicker.BoardSelector.BoardPicker -->
            </div>
        </li>
        <!-- Board Collaborators -->
        <li style="display: block; font-size: 21px; font-weight: 300; clear: both; color: #8c7e7e;
            text-shadow: 0 1px rgba(255,255,255,0.9); border-top-style: solid; border-top-width: 1px;
            border-top-color: rgba(255,255,255,0.7); border-bottom-style: solid; border-bottom-width: 1px;
            border-bottom-color: rgba(34,25,25,0.1); float: left; width: 100%; margin-top: 0;
            margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 15px; padding-right: 0;
            padding-bottom: 15px; padding-left: 0; list-style-type: none;">
            <label style="display: inline-block; line-height: 1.4em; font-size: 18px; float: left;
                width: 150px; padding-top: 7px; vertical-align: top;">
                Who can pin?</label>
            <div style="float: left; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                <div style="display: none; border-top-width: 0; margin-top: 0; margin-right: 0; margin-bottom: 0;
                    margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                </div>
                <ul style="float: left; border-top-style: none; margin-top: 0; margin-right: 0; margin-bottom: 10px;
                    margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                </ul>
            </div>
            <div style="display: none; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                <input type="hidden" name="csrfmiddlewaretoken" value="7080d22c6ce418cfa6da813554306e1e"
                    style="font-family: inherit; font-size: inherit; font-weight: inherit; resize: none;
                    outline: none; line-height: 1em; color: #8c7e7e; box-shadow: inset 0 0 2px rgba(255,255,255,0.75);
                    -moz-box-shadow: inset 0 0 2px rgba(255,255,255,0.75); -webkit-box-shadow: inset
0 0 2px rgba(255,255,255,0.75);
                    margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 5px;
                    padding-right: 5px; padding-bottom: 5px; padding-left: 5px; border-top-color: #d1cdcd;
                    border-right-color: #d1cdcd; border-bottom-color: #d1cdcd; border-left-color: #d1cdcd;
                    border-top-style: solid; border-right-style: solid; border-bottom-style: solid;
                    border-left-style: solid; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px;
                    border-left-width: 1px; background-color: #fcf9f9;" /></div>
            <div style="font-size: 18px; float: left; margin-top: 0; margin-right: 0; margin-bottom: 0;
                margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
                <div style="width: 700px; font-size: 18px; overflow: hidden; margin-top: 0; margin-right: 0;
                    margin-bottom: 10px; margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 7px;
                    padding-left: 0; border-top-style: none; border-right-style: none; border-bottom-style: none;
                    border-left-style: none;">
                    <a href="http://pinterest.com/jagadeesan/" style="font-weight: bold; color: #221919;
                        text-decoration: none; outline: none; float: left; font-size: inherit; height: 36px;
                        width: 36px;">
                        <img src="./me 2 - settings_files/jagadeesan_1331297962.jpg" alt="Collaborator
Image"
                            style="display: block; width: 36px !important; height: auto !important; border-top-width: 0;
                            border-right-width: 0; border-bottom-width: 0; border-left-width: 0;" /></a>
                    <a href="http://pinterest.com/jagadeesan/" style="font-weight: bold; color: #221919;
                        text-decoration: none; outline: none; float: left; font-size: inherit; width: 261px;
                        overflow: hidden; white-space: nowrap; text-overflow: ellipsis; margin-top: 7px;
                        margin-right: 18px; margin-bottom: 0; margin-left: 10px;">Jagadeesan Thiru</a>
                    <span style="float: left; margin-right: 0; margin-top: 7px; margin-left: 8px; margin-bottom: 0;
                        font-size: 14px;">Creator </span>
                </div>
                <span id="invite_response" style="background-color: #FFA; padding-bottom: 10px; padding-left: 20px;
                    padding-right: 20px; padding-top: 12px; float: left; margin-bottom: 10px; width: 429px;
                    margin-top: -10px; color: #2A1919; font-size: 18px; display: none;"></span>
                <div style="margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0;
                    padding-right: 0; padding-bottom: 0; padding-left: 0;">
                    <input type="text" name="collaborator_name" value="Add another
pinner" autocomplete="off"
                        role="textbox" aria-autocomplete="list" aria-haspopup="true" style="font-family: inherit;
                        font-size: 18px; font-weight: 300; resize: none; outline: none; line-height: 1.4;
                        color: #221919; box-shadow: inset 0 1px rgba(34,25,25,0.15),
0 1px #fff; -moz-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px #fff;
                        -webkit-box-shadow: inset 0 1px rgba(34,25,25,0.15), 0 1px #fff; min-width: 301px;
                        float: left; clear: both; display: inline-block; box-sizing: border-box; -moz-box-sizing: border-box;
                        -ms-box-sizing: border-box; -webkit-box-sizing: border-box; border-radius: 6px;
                        -moz-border-radius: 6px; -webkit-border-radius: 6px; -moz-transition: all 0.08s
ease-in-out;
                        -webkit-transition: all 0.08s ease-in-out; margin-top: 0; margin-right: 0; margin-bottom: 0;
                        margin-left: 0; padding-top: 6px; padding-right: 12px; padding-bottom: 6px; padding-left: 12px;
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
                    <input type="hidden" name="collaborator_username" value="" style="float: left; font-family: inherit;
                        font-size: inherit; font-weight: inherit; resize: none; outline: none; line-height: 1em;
                        color: #8c7e7e; box-shadow: inset 0 0 2px rgba(255,255,255,0.75); -moz-box-shadow: inset 0 0 2px rgba(255,255,255,0.75);
                        -webkit-box-shadow: inset
0 0 2px rgba(255,255,255,0.75); margin-top: 0; margin-right: 0;
                        margin-bottom: 0; margin-left: 0; padding-top: 5px; padding-right: 5px; padding-bottom: 5px;
                        padding-left: 5px; border-top-color: #d1cdcd; border-right-color: #d1cdcd; border-bottom-color: #d1cdcd;
                        border-left-color: #d1cdcd; border-top-style: solid; border-right-style: solid;
                        border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                        border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #fcf9f9;" />
                    <a href="http://pinterest.com/jagadeesan/me-2/settings/#" style="font-weight: bold;
                        color: #524d4d; text-decoration: none; outline: none; position: relative; display: inline-block;
                        text-align: center; line-height: 1em; border-radius: 6px; -moz-border-radius: 6px;
                        -webkit-border-radius: 6px; -moz-transition-property: color,
-moz-box-shadow, text-shadow;
                        -moz-transition-duration: .05s; -moz-transition-timing-function: ease-in-out;
                        -webkit-transition-property: color, -webkit-box-shadow, text-shadow; -webkit-transition-duration: .05s;
                        -webkit-transition-timing-function: ease-in-out; box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                        -moz-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                        -webkit-box-shadow: 0 1px rgba(255,255,255,0.8), inset 0 1px rgba(255,255,255,0.35);
                        cursor: pointer; font-size: 18px; text-shadow: 0 1px rgba(255,255,255,0.9); float: left;
                        margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 10px; padding-top: .45em;
                        padding-right: .825em; padding-bottom: .45em; padding-left: .825em; border-top-color: transparent;
                        border-right-color: transparent; border-bottom-color: transparent; border-left-color: transparent;
                        border-top-style: solid; border-right-style: solid; border-bottom-style: solid;
                        border-left-style: solid; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px;
                        border-left-width: 1px;"><strong style="font-style: normal; font-weight: bold; position: relative;
                            z-index: 2;">Add</strong><span style="position: absolute; z-index: 1; top: -1px;
                                right: -1px; bottom: -1px; left: -1px; display: block; opacity: 1; border-radius: 6px;
                                -moz-border-radius: 6px; -webkit-border-radius: 6px; box-shadow: inset 0 1px rgba(255,255,255,0.35);
                                -moz-box-shadow: inset 0 1px
rgba(255,255,255,0.35); -webkit-box-shadow: inset 0 1px rgba(255,255,255,0.35);
                                -moz-transition-property: opacity; -moz-transition-duration: 0.5s; -moz-transition-timing-function: ease-in-out;
                                -webkit-transition-property: opacity; -webkit-transition-duration: 0.5s; -webkit-transition-timing-function: ease-in-out;
                                font-size: 18px; filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfafb', endColorstr='#f0eded');
                                border-top-color: #bbb; border-right-color: #bbb; border-bottom-color: #bbb;
                                border-left-color: #bbb; border-top-style: solid; border-right-style: solid;
                                border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                                border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #f0eded;
                                background-position: 0%
0%;"></span></a>
                </div>
                <ul id="CurrentCollaborators" style="border-top-width: 0
!important; margin-top: 10px;
                    margin-right: 0; margin-bottom: 0; margin-left: 0; padding-top: 0; padding-right: 0;
                    padding-bottom: 0; padding-left: 0;">
                </ul>
            </div>
        </li>
    </ul>
    <div style="border-top-color: rgba(255,255,255,0.7); border-top-width: 1px; border-top-style: solid;
        float: left; margin-top: 0; margin-right: 0; margin-bottom: 20px; margin-left: 0;
        padding-top: 24px; padding-right: 0; padding-bottom: 0; padding-left: 150px;">
        <a href="http://pinterest.com/jagadeesan/me-2/settings/#" onclick="$('#BoardEdit').submit();
return false;"
            style="font-weight: bold; color: #fcf9f9; text-decoration: none; outline: none;
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
                z-index: 2;">Save Settings</strong> <span style="position: absolute; z-index: 1;
                    top: -1px; right: -1px; bottom: -1px; left: -1px; display: block; opacity: 1;
                    border-radius: 8px; -moz-border-radius: 8px; -webkit-border-radius: 8px; box-shadow: inset 0 1px rgba(255,255,255,0.35);
                    -moz-box-shadow: inset 0 1px
rgba(255,255,255,0.35); -webkit-box-shadow: inset 0 1px rgba(255,255,255,0.35);
                    -moz-transition-property: opacity; -moz-transition-duration: 0.5s; -moz-transition-timing-function: ease-in-out;
                    -webkit-transition-property: opacity; -webkit-transition-duration: 0.5s; -webkit-transition-timing-function: ease-in-out;
                    font-size: 24px; filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#eb5367', endColorstr='#d43638');
                    border-top-color: #910101; border-right-color: #910101; border-bottom-color: #910101;
                    border-left-color: #910101; border-top-style: solid; border-right-style: solid;
                    border-bottom-style: solid; border-left-style: solid; border-top-width: 1px;
                    border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: #d43638;
                    background-position: 0% 0%;"></span></a>
    </div>
    <div id="delete_loader" style="display: none; position: absolute; top: 25%; left: 38%;
        border-top-width: 0; z-index: 999999999; margin-top: 0; margin-right: 0; margin-bottom: 0;
        margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;
        background-color: #333;">
        <img src="./me 2 - settings_files/ajax-loader.gif" alt="Loading" style="border-top-width: 0;
            border-right-width: 0; border-bottom-width: 0; border-left-width: 0;" />
        <p style="color: white; line-height: 1.35em; margin-top: 0; margin-right: 0; margin-bottom: .8em;
            margin-left: 0; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
            Deleting...</p>
    </div>
</div>
