<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="SettingsManager.aspx.cs" Inherits="Admin_SettingsManager" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <h1>Manage Settings</h1>
    <p>Use the links on this page to manage various storefront settings including security, display, shipping, taxes, payment options and more...</p>
    
    <div class="LandingPage">
        <hr />
        
        <div class="Shortcut"><span class="Icon"><a id="A1" href="~/admin/secure/settings/general/default.aspx" runat="server">Global Settings</a></div>
        <p>Use the global settings to manage settings like store contact information, SMTP settings, catalog display settings, etc</p>
        

        <div class="Shortcut"><span class="Icon"><a id="A4" href="~/admin/secure/settings/payment/default.aspx" runat="server">Payments</a></div>
        <p>Add and configure payment options such as credit cards, purchase orders and PayPal. </p>
        

        <div class="Shortcut"><span class="Icon"><a id="A6" href="~/admin/secure/settings/shipping/default.aspx" runat="server">Shipping</a></div>
        <p>Add custom shipping options and rules to your storefront. You can also configure these options to retrieve shipping rates from UPS or FedEx.</p>
        

        <div class="Shortcut"><span class="Icon"><a id="A7" href="~/admin/secure/settings/taxes/default.aspx" runat="server">Tax Classes</a></div>
        <p>Configure tax classes & rules for your catalog.</p>
        
        <div class="Shortcut"><a id="A11" href="~/admin/secure/settings/Profile/default.aspx" runat="server">Profiles</a></div>
        <p>Use Profile settings to manage the list of user profiles in the storefront.</p>
        
        <div class="Shortcut"><a id="A2" href="~/admin/secure/settings/storelocator/list.aspx" runat="server">Store Locator</a></div>
        <p>Manage the store locator database.</p>
        
        <div class="Shortcut"><a id="A3" href="~/admin/secure/settings/default.aspx" runat="server">Maintenance</a></div>
        <p>Perform general storefront maintenance such as password changes.</p>
        
    </div>
</asp:Content>

