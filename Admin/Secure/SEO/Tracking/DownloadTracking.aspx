<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" %>
<%@ Register Src="DownloadTracking.ascx" TagName="DownloadTracking" TagPrefix="ZNode" %>

<asp:Content ID="DownloadContent" runat="server" ContentPlaceHolderID="uxMainContent">
    <ZNode:DownloadTracking ID="DownloadTracking" runat="server" />
</asp:Content>