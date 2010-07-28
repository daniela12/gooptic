<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="CatalogManager.aspx.cs" Inherits="Admin_Catalog" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <h1>Manage your Catalog</h1>
    <p>Use the links on this page to manage all aspects of your product catalog including products, categories, attributes, promotions and more...</p>
    
    <div class="LandingPage">
        <hr />
        
        <div class="Shortcut"><a id="A1" href="~/admin/secure/catalog/product_category/list.aspx" runat="server">Categories</a></div>
        <p>Product categories are the hierarchical groupings for the products in your catalog (ex: "Apparel"). Product
        categories can also be used to create "departments" to help customers find what they are looking for.</p>
        
        
        <div class="Shortcut"><a id="A3" href="~/admin/secure/catalog/product/list.aspx" runat="server">Products</a></div>
        <p>Manage products, descriptions, images, related items and inventory in your catalog. </p>  
        
        <div class="Shortcut"><a id="A2" href="~/admin/secure/catalog/promotions/list.aspx" runat="server">Promotions</a></div>
        <p>Manage promotions and discount coupons.</p> 
               
        <div class="Shortcut"><a id="A5" href="~/admin/secure/catalog/product_type/list.aspx" runat="server">Product Types</a></div>
        <p>Product Types (ex: "Grocery") are product groupings used to apply attributes. Product Types are not displayed to customers and are used only for internal management.</p>
  
        <div class="Shortcut"><a id="A7" href="~/admin/secure/catalog/product_attribute_types/list.aspx" runat="server">Attributes</a></div>
        <p>Product attributes (ex: "Size" or "Color") are used to create inter-dependant product variations in your catalog. Use Product Add-Ons instead if you do not need to map unique product
        combinations to SKUs</p>
          
        <div class="Shortcut"><a id="A6" href="~/admin/secure/catalog/product_addons/list.aspx" runat="server">Add-Ons</a></div>
        <p>Product Add-Ons (ex: "Batteries" or "Gift Wrapping") allow you to add independant options to your product.</p>
  
        <div class="Shortcut"><a id="A9" href="~/admin/secure/catalog/product_highlights/default.aspx" runat="server">Highlights</a></div>
        <p>Associate common features (such as awards, certifications, etc) with products.</p>
      
        <div class="Shortcut"><a id="A4" href="~/admin/secure/catalog/product_manufacturer/list.aspx" runat="server">Brands</a></div>
        <p>Manage a reference list of manufacturers or brands for products in your catalog. </p>    
             
             
         
    </div>
</asp:Content>

