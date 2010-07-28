<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="Denied.aspx.cs" Inherits="admin_secure_denied" Title="Access Denied" %>
<%@ Register Src="~/Controls/spacer.ascx" TagName="spacer" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
<h1>Access Denied!</h1>
You do not currently have access to this functionality. This may be because you do not have an appropriate license
or your permissions are restricted.
<br />
<br />
<a href="javascript: history.go(-1)">< Back</a>
<br />
<uc1:spacer id="Spacer8" SpacerHeight="300" SpacerWidth="10" runat="server"></uc1:spacer>
</asp:Content>

