<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Secure_settings_Default" Title="Untitled Page" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">    
    <asp:ScriptManager id="ScriptManager" runat="server"></asp:ScriptManager>
    <ZNode:Spacer ID="topSpacing" SpacerHeight="10" SpacerWidth="5" runat="server" />
    <h1>Storefront Maintenance</h1>
    <ZNode:Spacer ID="Spacer14" SpacerHeight="10" SpacerWidth="5" runat="server" />    
    <ajaxToolKit:TabContainer ID="tabSettings" runat="server">
    
        <!-- Change password section -->
        <ajaxToolKit:TabPanel ID="tabPanelChangePassword" runat="server">        
            <HeaderTemplate>Security</HeaderTemplate>
            <ContentTemplate>                
                <div><ZNode:spacer id="Spacer23" SpacerHeight="10" SpacerWidth="3" runat="server" /></div>
                <div><a id="A2" href="~/Admin/Secure/ChangePassword.aspx" runat="server">Change Password</a></div>
                <small>Your password must be changed on a regular basis in order to conform to credit card industry security standards.</small>
                <div><ZNode:spacer id="Spacer7" SpacerHeight="10" SpacerWidth="3" runat="server" /></div>
                <div class="ValueStyle">Last login - <asp:Label ID="lblLogInDate" runat="server"></asp:Label></div>
                <div><ZNode:spacer id="Spacer1" SpacerHeight="10" SpacerWidth="3" runat="server" /></div>
                <div class="FieldStyle">Last password change - <asp:Label ID="lblLastPwdUpdated" runat="server" /></div>
                <div><ZNode:spacer id="Spacer5" SpacerHeight="10" SpacerWidth="3" runat="server" /></div>
                <div>Password expires in <strong><asp:Label ID="lbldaysCount" runat="server"></asp:Label> day(s)</strong></div>
                <div><ZNode:spacer id="Spacer6" SpacerHeight="10" SpacerWidth="3" runat="server" /></div>
                <hr />
                <div><a id="A7" href="~/admin/secure/settings/General/Key.aspx" runat="server">Rotate Key</a></div>
                <small>You should create a new encryption key instead of using the key that is included out of the box with Znode Storefront.</small>
                <div><ZNode:spacer id="Spacer22" SpacerHeight="10" SpacerWidth="3" runat="server" /></div>
            </ContentTemplate>
        </ajaxToolKit:TabPanel>
        
        <!-- FedEx settings -->     
        <ajaxToolKit:TabPanel ID="TabPanel3" runat="server">        
            <HeaderTemplate>FedEx&reg;</HeaderTemplate>
            <ContentTemplate>                
                <div><ZNode:spacer id="Spacer3" SpacerHeight="10" SpacerWidth="3" runat="server" /></div>
                <div><a href="~/admin/secure/Settings/FedEx/RegisterCSPUSer.aspx" runat="server">FedEx&reg; Register User</a></div>
                <small>Use this option to Register FedEx&reg; user</small>
                <div><ZNode:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="3" runat="server" /></div>
                <div><a id="A1" href="~/admin/secure/Settings/FedEx/SubScribeUser.aspx" runat="server">FedEx&reg; Meter Number</a></div>
                <div><ZNode:spacer id="Spacer4" SpacerHeight="5" SpacerWidth="3" runat="server" /></div>
                <small>This is a one-time request, meaning that a customer needs to subscribe to the FedEx&reg; services only once. A unique meter number specific to the customer’s FedEx&reg; account number will be returned to the client.</small>
            </ContentTemplate>
        </ajaxToolKit:TabPanel>
        
        <!-- Quick books settings -->
        <ajaxToolKit:TabPanel ID="tabPanelQuickBooks" runat="server">        
            <HeaderTemplate>Quickbooks&reg;</HeaderTemplate>
            <ContentTemplate>
                <p>Use the links below to set up your Storefront to download information to Quickbooks&reg;.</p>
                <div><ZNode:spacer id="Spacer13" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>                
                <div><a href='~/admin/secure/settings/Quickbooks/edit.aspx' runat="server">Setup Quickbooks&reg; company information</a></div>
                <small>Use this option to tell the Storefront how to map information to your Quickbooks&reg; company file.</small>                
                <div><ZNode:spacer id="Spacer12" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>
                <div><a href="~/admin/secure/Settings/QuickBooks/Default.aspx" runat="server">Generate and download QWC file</a></div>
                <small>To connect your storefront with QuickBooks® you must first have QuickBooks® and the QuickBooks Web Connector installed on your local computer. You will then need to generate a Quickbooks Web Connector (QWC) file.</small>
                <div><ZNode:spacer id="Spacerbottom" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>
            </ContentTemplate>
        </ajaxToolKit:TabPanel>
        
         <ajaxToolKit:TabPanel ID="tabPanelIPCommerce" Enabled="false" runat="server">        
            <HeaderTemplate>IP Commerce</HeaderTemplate>
            <ContentTemplate>
                <p>Use the links below to perform maintenance on your IP Commerce settings. Note that you would need a valid IP Commerce Merchant configuration for this.</p>
                <div><ZNode:spacer id="Spacer16" SpacerHeight="10" SpacerWidth="3" runat="server" /></div>
                <div><a id="A3" href="~/admin/secure/settings/ipcommerce/edit.aspx" runat="server">Edit IP Commerce Settings</a></div>
                <div><ZNode:spacer id="Spacer15" SpacerHeight="10" SpacerWidth="3" runat="server" /></div>
                <div><a id="A4" href="~/admin/secure/settings/ipcommerce/key.aspx" runat="server">Rotate Key</a></div>
                <div><ZNode:spacer id="Spacer17" SpacerHeight="10" SpacerWidth="3" runat="server" /></div>
                <div><a id="A8" href="~/admin/secure/settings/ipcommerce/password.aspx" runat="server">Rotate Password</a></div>
                <div><ZNode:spacer id="Spacer18" SpacerHeight="10" SpacerWidth="3" runat="server" /></div>

            </ContentTemplate>
        </ajaxToolKit:TabPanel>
        
        <!-- Batch Image resizer section -->
        <ajaxToolKit:TabPanel ID="ajaxtabPnlBatchImage" runat="server">        
            <HeaderTemplate>Miscellaneous</HeaderTemplate>
            <ContentTemplate>                    
                <div><ZNode:spacer id="Spacer19" SpacerHeight="10" SpacerWidth="3" runat="server" /></div>
                <div><a id="A5" href="~/admin/secure/settings/BatchImageResizer/BatchImageResizer.aspx" runat="server">Batch Resize Product Images</a></div>
                <small>If you have bulk uploaded images or changed the image display sizes in the Global Settings you may want to do a bulk resize of all the Product images.</small>
                <div><ZNode:spacer id="Spacer20" SpacerHeight="10" SpacerWidth="3" runat="server" /></div>
                
                <div><a id="A6" href="~/admin/secure/Catalog/DeleteCatalog.aspx" runat="server">Delete Catalog Data</a></div>
                <small>Use this option to delete your current catalog and start fresh with an empty catalog.</small>
                <div><ZNode:spacer id="Spacer21" SpacerHeight="10" SpacerWidth="3" runat="server" /></div>
            </ContentTemplate>
        </ajaxToolKit:TabPanel>
        

    </ajaxToolKit:TabContainer>
</asp:Content>

