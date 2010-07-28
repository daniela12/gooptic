<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" %>
<%@ Register Src="UploadZipcode.ascx" TagName="UploadZipcode" TagPrefix="ZNode" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <ZNode:UploadZipcode ID="UploadZipcode1" runat="server" />   
</asp:Content>
