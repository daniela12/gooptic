<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="continuetrial.aspx.cs" Inherits="ContinueTrial" Title="Znode Storefront Trial" %>
<%@ Register Src="~/Controls/spacer.ascx" TagName="spacer" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <div class="License">
        <h1>Znode Storefront Trial - <%=DaysRemaining %></h1>
        <div><img src="~/images/clear.gif" width="1" height="20" alt=""/></div>  
        
        <div class="Status" style=" margin-bottom: 30px;">
            You are seeing this message because your storefront is running in the trial mode. This message will not appear in the fully
            registered version.
        </div>
        
      
        <div class="ActionLink" style="margin-left:50px; margin-bottom:20px;"><b><img id="Img3" src="~/images/icons/16/400-right.gif" border="0" align="absmiddle" runat="server" />  <a id="A2" href="~/" runat="server">Continue with Trial</a></b></div> 
        
        
        <div class="ActionLink" style="margin-left:50px; margin-bottom:20px;"><b><img id="Img4" src="~/images/icons/16/400-right.gif" border="0" align="absmiddle" runat="server" />  <a id="A4" href="~/admin/" runat="server">Go to Storefront Admin</a></b></div> 
        
      
        <div class="ActionLink" style="margin-left:50px; margin-bottom:100px;"><b><img id="Img5" src="~/images/icons/16/400-right.gif" border="0" align="absmiddle" runat="server" />  <a id="A1" href="~/activate/default.aspx" runat="server">Activate your License</a></b></div> 
        

         <div>Issues or questions? Email us at support@znode.com. <a id="A3" href="http://www.znode.com/buy" target="_blank">Purchase licenses from Znode.com</a></div>
     </div> 
</asp:Content>

