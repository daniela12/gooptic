<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" Title="Maintenance"%>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <h1>Storefront Maintenance</h1>
    <p>Use the links in this section to perform general maintenance for your storefront.</p>
    
    <div class="LandingPage">
        <hr />
        
        <div class="Shortcut"><a id="A1" href="~/admin/secure/settings/batchimageresizer/batchimageresizer.aspx" runat="server" target="_self">Batch Image Resizer</a></div>
        <p>Use this option to automatically resize all your catalog images and create multiple sizes (Thumbnail, small, medium and large).</p>
        
        <div class="Shortcut"><a id="A2" href="~/admin/secure/catalog/DeleteCatalog.aspx" runat="server" target="_self">Delete Catalog Data</a></div>
        <p>Use this option to delete all the data in your catalog. Use this option with caution since the data will be permananatly deleted from the database!</p>
                 
        <div class="Shortcut"><a id="A4" href="~/admin/secure/ChangePassword.aspx" runat="server">Change Password</a></div>
        <p>Use this option to change your administration password.</p>
        
        <div class="Shortcut"><a id="A3" href="~/admin/secure/settings/StoreLocator/List.aspx" runat="server">Store Locator</a></div>
        <p>Use this option to manage stores.</p>
        
        <div class="Shortcut"><a id="A5" href="~/admin/secure/settings/FedEx/RegisterCSPUSer.aspx" runat="server">FedEx® Register User</a></div>
        <p>Use this option to Register FedEx® User.</p>
        
        <div class="Shortcut"><a id="A6" href="~/admin/secure/settings/FedEx/SubScribeUser.aspx" runat="server">FedEx® Meter Number</a></div>
        <p>Use this option to subscribe the registered user and to return the Meter Number.</p>
        
    </div>
</asp:Content>