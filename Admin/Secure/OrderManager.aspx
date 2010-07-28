<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="OrderManager.aspx.cs" Inherits="Admin_OrderManager" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <h1>Manage Sales</h1>
    <p>Use the links on this page to manage your customers, orders, dealers, affiliates and service requests.</p>
    
    <div class="LandingPage">
        <hr />
        
        <div class="Shortcut"><a id="A2" href="~/admin/secure/sales/orders/list.aspx" runat="server">View Orders</a></div>
        <p>Search and download orders, change order status and authorize returns.</p>
        
        
        <div class="Shortcut"><a id="A3" href="~/admin/secure/sales/OrderDesk/orderdesk.aspx" runat="server">Create an Order</a></div>
        <p>Customer service representatives can use this feature to manually create orders.</p> 
        
        <div class="Shortcut"><a id="A4" href="~/admin/secure/sales/customers/list.aspx" runat="server">Accounts</a></div>
        <p>Search through accounts (customer, dealers, vendors, etc). You can also view the order history and add customer service notes for each account.</p>
        
        <div class="Shortcut"><a id="A1" href="~/admin/secure/sales/cases/list.aspx" runat="server">Service Requests</a></div>
        <p>Service Requests are support requests submitted by your customers using the Contact-Us form on your website. You 
        can search through customer service requests and respond to them directly using the admin.</p>        
                            
        <div class="Shortcut"><a id="A10" href="~/admin/secure/catalog/product_reviews/default.aspx" runat="server">Customer Reviews</a></div>
        <p>Approve or disable reviews submitted by customers.</p>    
        
        <div class="Shortcut"><a id="A8" href="~/admin/secure/catalog/product_suppliers/list.aspx" runat="server">Suppliers</a></div>
        <p>Manage a reference list of suppliers for products in your catalog. Create fulfilment rules for suppliers. </p>  
    </div>
</asp:Content>

