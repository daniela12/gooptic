<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="add_next.aspx.cs" Inherits="Admin_Secure_catalog_product_add_next" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
<h1>Select Next Step</h1>
<p>Your product was successfully added. Select your next step below:</p>
    <br /><br />
    <asp:button CssClass="Button" id="btnAddProduct" CausesValidation="False" Text="Add another Product" Runat="server" OnClick="btnAddProduct_Click1"></asp:button>
    <br /><br />
    <asp:button CssClass="Button" id="btnProductList" CausesValidation="False" Text="Back to Product List" Runat="server" OnClick="btnProductList_Click"></asp:button>  
    <br /><br />
</asp:Content>

