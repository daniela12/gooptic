<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="delete.aspx.cs" Inherits="Admin_Secure_categories_type_delete" Title="Untitled Page" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <h1>Please Confirm<uc1:DemoMode id="DemoMode1" runat="server"></uc1:DemoMode></h1>
    <p>Please confirm if you want to delete the Product Type titled <b>"<%=ProductCategoryName%>"</b>.
        Note that this will also delete all the products of this Type. This change cannot be undone.</p>
    <p><asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label></p>
    <asp:button CssClass="Button" id="btnCancel" CausesValidation="False" Text="Cancel" Runat="server" OnClick="btnCancel_Click" />
    <asp:button CssClass="Button" id="btnDelete" CausesValidation="False" Text="Delete" Runat="server" OnClick="btnDelete_Click" />
    <br /><br /><br />
</asp:Content>

