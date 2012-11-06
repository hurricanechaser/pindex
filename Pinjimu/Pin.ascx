<%@ Control Language="C#" AutoEventWireup="true" %>
<div id="pin" style="position: fixed; box-shadow: 0 1px 3px rgba(34,25,25,0.4); -moz-box-shadow: 0 1px 3px rgba(34,25,25,0.4); display: none; -webkit-box-shadow: 0 1px 3px rgba(34,25,25,0.4); margin-top: 0; margin-right: auto; margin-bottom: 32px; padding-top: 0; padding-right: 0; padding-bottom: 0; padding-left: 0; background-color: #FFFFFF; text-align: center; overflow: hidden; top: 104px; z-index: 4;">
</div>
<script id="pintmpl" type="text/template">
    <div class="bodyContent"  style="z-index: 5; ">
        <div class="leftPanel"> 
    <#if(Contacts && Contacts.length>0) { var contact=Contacts[0] #>
                        <div class="contactInfoImg">
                            <%--<img src="<#=(contact.Avatar== null?FreshPin.constants.cdn + FreshPin.constants.userBlankImg:contact.Avatar) #>" alt="Contact Image" />--%>
                            <img src="<#=FreshPin.constants.cdn + FreshPin.constants.userBlankImg #>" alt="Contact Image" />
                            <p><#=(contact.Contact == null?'':contact.Contact) #></p>
                        </div>                       
                        <div class="contactInfoContent">
                            <div class="mpcTitle">
    <div style="display:block">    <label class="label">Contact:</label><p class="mpcTxt"><#=(contact.Contact == null?'N/A':contact.Contact) #></p>       </div>  <div style="clear:both;" />
                             <div >      <label class="label">Address:</label><p class="mpcTxt"><#=(contact.Address == null?'N/A':contact.Address) #></p>    </div>    <div style="clear:both;" />
                        <div >   <label class="label">Phone:</label><p class="mpcTxt"><#=(contact.Phone == null?'N/A':contact.Phone) #></p>      </div>    <div style="clear:both;" />
                        <div >  <label class="label">Website:</label><a class="mpcTxt" target="_blank" href="http://<#=(contact.Website == null?'javascript:void(0);':contact.Website) #>"><#=(contact.Website == null?'N/A':contact.Website) #></a> </div>  
                            </div>                           
                        </div>
                        <div class="contactInfoGlobe">
                            <img src="img/globe.jpg" alt="World Globe" />
                        </div>      
     <# } #>             
         <div class="clear" />
            <div class="photograph">               
                    <img id="pinCloseupImage" src="<#=url #>"  />
                    <p style="margin: 0px; color: #524D4D; font-size: 13px; font-weight: bold;
            word-break: break-word; padding-top: 0; padding-right: 0; padding-bottom: 0;max-width:<#=width +10 #>px;
            padding-left: 0;"><#=title #></p>
            </div> 
    </div>
       <# if(Comments){ #>
        <div class="rightPanel">
            <div class="rpWrapper">          
               <# for(var i=0;i<Comments.length;i++){ var comment=Comments[i]; var date=new Date(comment.Date) #>   
                <div class="rpcontent floated">
                    <div class="rpcuserImage">
                        <img src=" <#= ( comment.Avatar == null?FreshPin.constants.cdn + FreshPin.constants.userBlankImg:FreshPin.constants.upl+comment.Avatar+'?height=58&width=58') #>"  alt="Image Not Loaded">
                    </div>
                    <div class="rpcPanelTriangle">
                        <img src="img/left-user-triangle.png" />
                    </div>
                    <div class="rpcPanel">
                        <div class="rpcPanelHeader">                           
                            <p><span style="margin:2px 15px;"><#= ( comment.Name == null?'': comment.Name) #></span> <span class="dateSpan" style="margin:2px 5px;"><#= String.format('{0}/{1}/{2}', date.getDate(),date.getMonth()+1,date.getFullYear()) #></span> <span class="timeSpan" style="margin:2px 5px;"><#=String.format('{0}:{1}:{2}', date.getHours(),date.getMinutes()+1,date.getSeconds())#></span> </p>
                            

                        </div>
                        <div class="rpcPanelTxt">
                            <p> <#= ( comment.Comment == null?'': comment.Comment) #></p>
                        </div>                      
                    </div>
                </div>    
             <#  }
             }#>       
            
        </div>      
  
</script>
