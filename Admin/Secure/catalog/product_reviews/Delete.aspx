<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master"  AutoEventWireup="true" CodeFile="Delete.aspx.cs" Inherits="Admin_Secure_catalog_product_reviews_Delete" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">    
    <ZNode:DemoMode ID="DemoMode1" runat="server" />
    <h1>Please Confirm</h1>
    <p>Please confirm if you want to delete the product review titled "<b><%=ProductReviewTitle%></b>". This change cannot be undone.
    </p>
    <asp:Label ID="lblMsg" runat="server" CssClass="Error"></asp:Label><br /><br />
    <asp:button CssClass="Button" id="btnCancel" CausesValidation="False" Text="Cancel" Runat="server" OnClick="btnCancel_Click" />
    <asp:button CssClass="Button" id="btnDelete" CausesValidation="False" Text="Delete" Runat="server" OnClick="btnDelete_Click" />
    <div><ZNode:spacer id="Spacer1" SpacerHeight="20" SpacerWidth="3" runat="server"></ZNode:spacer></div>
</asp:Content>

