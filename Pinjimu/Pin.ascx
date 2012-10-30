<%@ Control Language="C#" AutoEventWireup="true" %>
<div id="pin" style="position: fixed; box-shadow: 0 1px 3px rgba(34,25,25,0.4); -moz-box-shadow: 0 1px 3px rgba(34,25,25,0.4);
        display: none; -webkit-box-shadow: 0 1px 3px rgba(34,25,25,0.4); margin-top: 0;
        margin-right: auto; margin-bottom: 32px;  padding-top: 0; padding-right: 0;
        padding-bottom: 0; padding-left: 0; background-color: #FFFFFF; text-align: center;
        overflow-x: auto; overflow-y: scroll;top:104px;z-index:4; max-height:100%;"></div>
<script id="pintmpl" type="text/template">
     <# if(!(Contact && Address && Avatar && Phone && Website) && !(Comments && Comments.length>0)){ #>
        <p id="pintitle" style="margin: 0px; color: #524D4D; font-size: 13px; font-weight: bold;
            word-break: break-word; padding-top: 0; padding-right: 0; padding-bottom: 0;
            padding-left: 0;width:<#=width+60 #> px;"><#= title #>
        </p> 
        <div style="clear: both; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
            padding-top: 20px; padding-right: 0; padding-bottom: 0; padding-left: 0;">
        </div>
        <div id="PinActionButtons" style="height: 26px; width: 280px; float: left; overflow: hidden;
            margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 30px; padding-top: 0;
            padding-right: 0; padding-bottom: 0; padding-left: 13px; background-image: url('http://cdn.pinjimu.com/img/pinjimu/detailiconBg.jpg');
            background-repeat: no-repeat; background-position: left top;">
            <a <# if (FreshPin.authenticated()){ #>  id="likepint" <# } else {#>   name="buttons"<# } #> href="javascript:void(0)" style="font-weight: bold; color: #221919;
                text-decoration: none; outline: none;">
                <div class="pinLike" align="left">
                    <#=strings.Like #></div>
            </a><a  <# if (FreshPin.authenticated())
                  { #>  id="editpint" <# } else { #>   name="buttons"
                                                             <# } #>  href="javascript:void(0);" style="font-weight: bold; color: #221919;
                text-decoration: none; outline: none;">
                <div style="height: 26px; font-size: 12px; font-weight: bold; float: left; display: block;
                    color: #777176; font-family: Helvetica, Arial, Sans-Serif; text-align: left;
                    line-height: 26px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                    padding-top: 0; padding-right: 14px; padding-bottom: 0; padding-left: 30px; background-image: url('http://cdn.pinjimu.com/img/pinjimu/repinIconDetailPage.png');
                    background-repeat: no-repeat; background-position: left 5px;" align="left">
                    <#=strings.Edit #></div>
            </a><a  <#  if (FreshPin.authenticated())
                  { #>  id="repint" <# } else {#>  name="buttons"
                                                             <# } #>  href="javascript:void(0);" style="font-weight: bold; color: #221919;
                text-decoration: none; outline: none;">
                <div style="height: 26px; font-size: 12px; font-weight: bold; float: left; display: block;
                    color: #777176; font-family: Helvetica, Arial, Sans-Serif; text-align: left;
                    line-height: 26px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                    padding-top: 0; padding-right: 14px; padding-bottom: 0; padding-left: 23px; background-image: url('http://cdn.pinjimu.com/img/pinjimu/repinIconDetailPage.png');
                    background-repeat: no-repeat; background-position: left 4px;" align="left">
                    <#=strings.Repin #></div>
            </a><a id="commentt" href="javascript:void(0);" style="font-weight: bold; color: #221919;
                text-decoration: none; outline: none;">
                <div style="height: 26px; font-size: 12px; font-weight: bold; float: left; display: block;
                    color: #777176; font-family: Helvetica, Arial, Sans-Serif; text-align: left;
                    line-height: 26px; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
                    padding-top: 0; padding-right: 14px; padding-bottom: 0; padding-left: 25px; background-image: url('http://cdn.pinjimu.com/img/pinjimu/commentIconDetailPage.png');
                    background-repeat: no-repeat; background-position: left 4px;" align="left">
                    <#=strings.Comment #></div>
            </a>
        </div>
        <div style="clear: both; margin-top: 0; margin-right: 0; margin-bottom: 0; margin-left: 0;
            padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0;">
        </div>
        <div style="display: block; position: relative; overflow: hidden; margin-top: 20px;
            margin-right: 30px; margin-bottom: 30px; margin-left: 30px; padding-top: 0; padding-right: 0;
            padding-bottom: 0; padding-left: 0; background-color: #fff;">
            <a id="pinimgsource" target="_blank">
                <img id="pinCloseupImage" src="<#=url #>"  style="display: block; margin-top: 0; margin-right: auto;
                    margin-bottom: 0; margin-left: auto; border-top-width: 0; border-right-width: 0;
                    border-bottom-width: 0; border-left-width: 0;width:<#=width #> px;" />
           </a>
        </div>
       <div class="rightPanel">
            <div class="rpWrapper">             
                <div class="rpcontent floated">
                    <div class="rpcuserImage">
                        <img src="images/user-image1.jpg" alt="Image Not Loaded">
                    </div>
                    <div class="rpcPanelTriangle">
                        <img src="images/left-user-triangle.png" />
                    </div>
                    <div class="rpcPanel">
                        <div class="rpcPanelHeader">
                            <!--<p>Lorem <span class="daySpan"> 12 </span>/<span class="monthSpan"> 09 </span>/<span class="yearSpan">2012 </span> | <span class="hrSpan">16 </span>: <span class="minSpan">45</span></p>-->
                            <p>Lorem <span class="dateSpan">12 / 09/ 2012 </span>| <span class="timeSpan">16:45</span> </p>
                            <div class="countUser">#1</div>

                        </div>
                        <div class="rpcPanelTxt">
                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, </p>
                        </div>
                        <div class="rpcPanelText">
                            text
                        </div>
                    </div>
                </div>               
            </div>
        </div>   
    <# } else if((Comments && Comments.length>0) && !(Contact && Address && Avatar && Phone && Website)) { #>
    <div class="bodyContent"  style="z-index: 5; position: fixed;">
        <div class="leftPanel">      
            <div class="photograph">
                <a id="pinimgsource">
                    <img id="pinCloseupImage" src="<#=url #>"  /></a>
            </div>
        </div>      
        <div class="rightPanel">
            <div class="rpWrapper">             
                <div class="rpcontent floated">
                    <div class="rpcuserImage">
                        <img src="images/user-image1.jpg" alt="Image Not Loaded">
                    </div>
                    <div class="rpcPanelTriangle">
                        <img src="images/left-user-triangle.png" />
                    </div>
                    <div class="rpcPanel">
                        <div class="rpcPanelHeader">
                            <!--<p>Lorem <span class="daySpan"> 12 </span>/<span class="monthSpan"> 09 </span>/<span class="yearSpan">2012 </span> | <span class="hrSpan">16 </span>: <span class="minSpan">45</span></p>-->
                            <p>Lorem <span class="dateSpan">12 / 09/ 2012 </span>| <span class="timeSpan">16:45</span> </p>
                            <div class="countUser">#1</div>

                        </div>
                        <div class="rpcPanelTxt">
                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, </p>
                        </div>
                        <div class="rpcPanelText">
                            text
                        </div>
                    </div>
                </div>               
            </div>
        </div>   
    </div>   
   <# } #>
</script>
