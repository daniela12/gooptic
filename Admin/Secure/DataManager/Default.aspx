<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master"  AutoEventWireup="true" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">    
    <h1>Znode Storefront Data Manager</h1>
    <p>Use the links below to download and upload data to and from your storefront.
    You can find example upload/download file formats in the Maintenance directory of your web project.</p>
    <hr />
    <div class="LandingPage">   
    
        <h4>Download Functions</h4>
    
        <div class="Shortcut"><a id="A5" href="~/admin/secure/DataManager/download.aspx?filter=Inventory" runat="server">Download Inventory</a></div>
        <p>Download inventory levels for Products, SKUs and Add-Ons. You can then edit this file and re-upload it to update inventory levels if required.</p>
   
        <div class="Shortcut"><a id="A4" href="~/admin/secure/DataManager/download.aspx?filter=pricing" runat="server">Download Pricing</a></div>
        <p>Download pricing for Products, SKUs and Add-Ons. This downloaded file can be updated by you and re-uploaded using the Update Pricing function.</p>        
        
        <div class="Shortcut"><a id="A1" href="~/admin/secure/DataManager/download.aspx?filter=Product" runat="server">Download Products</a></div>
        <p>Download Product details. This function will only download product details not SKUs or Add-Ons </p>
            
    	<div class="Shortcut"><a id="A11" href="~/admin/secure/DataManager/DownloadAttributes.aspx" runat="server">Download Attributes</a></div>
        <p>Download Product Attributes filtered by attributes. When you upload new SKUs you will need to enter the appropriate Attribute IDs. The file downloaded using this function will have this information.</p>
         
        <div class="Shortcut"><a id="A8" href="~/admin/secure/DataManager/DownloadSku.aspx" runat="server">Download SKUs</a></div>
        <p>Download the selected SKUs by using attributes and product options. This downloaded file can be updated by you and re-uploaded using the Update Modified SKUs function.</p>        
  
         
        <h4>Upload Functions</h4>
              
        <div class="Shortcut"><a id="A3" href="~/admin/secure/DataManager/UpdateOrderShipping.aspx" runat="server">Upload Shipping Status</a></div>
        <p>Upload a list of shipment tracking codes (ex: FedEx or UPS tracking codes). Once the tracking code has been uploaded the order status will automatically be set to "Shipped".</p>
 
        <div class="Shortcut"><a id="A2" href="~/admin/secure/DataManager/UpdateInventory.aspx" runat="server">Upload Inventory</a></div>
        <p>Upload inventory levels for Products, SKUs and Add-Ons. You can use the Download Inventory function to get an initial inventory file that you can update and re-upload.</p>
        
        
        <div class="Shortcut"><a id="A6" href="~/admin/secure/DataManager/UpdatePricing.aspx" runat="server">Upload Pricing</a></div> 
        <p>Upload pricing levels for Products, SKUs and Add-Ons. You can use the Download Pricing function to get initial prices that you can re-upload using this function.</p>
            
        <div class="Shortcut"><a id="A20" href="~/admin/secure/DataManager/uploadproduct.aspx" runat="server">Upload Products</a></div>
        <p>Upload new products to your catalog. This function will only upload products to the catalog, not SKUs or Add-Ons.</p>    
		     
        <div class="Shortcut"><a id="A9" href="~/admin/secure/DataManager/UpdateSku.aspx" runat="server">Upload SKUs</a></div>
        <p>Upload SKU data. You can use the Download SKUs functionality to get the initial SKU file which you can then update and upload.</p>        
        
        <div class="Shortcut"><a id="A7" href="~/admin/secure/DataManager/uploadzipcode.aspx" runat="server">Upload Zip Code Data</a></div>
        <p>Upload new Zip Code Data to your catalog to be used for the Store Locator functionality</p>
    </div>
</asp:Content>
