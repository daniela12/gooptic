<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master"  AutoEventWireup="true" CodeFile="delete.aspx.cs" Inherits="Admin_Secure_catalog_product_Highlights_delete" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">    
    <h1>Please Confirm</h1><ZNode:DemoMode id="DemoMode1" runat="server"></ZNode:DemoMode>
    <p>Please confirm if you want to delete the highlight named <b><%=HighlightName%></b>. This change cannot be undone.</p>
    <asp:Label CssClass="Error" ID="lblErrorMessage" runat="server" Text=''></asp:Label>
    <div><ZNode:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="10" runat="server"></ZNode:spacer></div>
    <div><asp:button CssClass="Button" id="btnDelete" CausesValidation="False" Text="Delete" Runat="server" OnClick="btnDelete_Click" />    
    <asp:button CssClass="Button" id="btnCancel" CausesValidation="False" Text="Cancel" Runat="server" OnClick="btnCancel_Click" /></div>
    <div><ZNode:spacer id="Spacer1" SpacerHeight="25" SpacerWidth="10" runat="server"></ZNode:spacer></div>
</asp:Content>

