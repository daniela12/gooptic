<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" %>
<%@ Register Src="UploadProduct.ascx" TagName="UploadProduct" TagPrefix="ZNode" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">    
    <ZNode:UploadProduct ID="UploadProduct1" runat="server" />
</asp:Content>