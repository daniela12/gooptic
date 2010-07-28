<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="add_next.aspx.cs" Inherits="Admin_Secure_catalog_product_category_add_next" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
<h1>Select Next Step</h1>
<p>Your product category was successfully added. Select your next step below:</p>
    <br /><br />
    <asp:button CssClass="Button" id="btnAddCategory" CausesValidation="False" Text="Add another Category" Runat="server" OnClick="btnAddCategory_Click"></asp:button>
    <br /><br />
    <asp:button CssClass="Button" id="btnAddProduct" CausesValidation="False" Text="Add a Product to this Category" Runat="server" OnClick="btnAddProduct_Click"></asp:button>
    <br /><br />
    <asp:button CssClass="Button" id="btnCategoryList" CausesValidation="False" Text="Back to Category List" Runat="server" OnClick="btnCategoryList_Click"></asp:button>  
    <br /><br />
</asp:Content>

