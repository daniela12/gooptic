<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Secure_settings_QuickBooks_Default" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">    
    <div class="Form">
        <h1>Quickbooks&reg; Web Connector</h1>
        <p>To connect your storefront with QuickBooks&reg; you must first have QuickBooks&reg; and the QuickBooks Web Connector installed on your local computer. You will then need to generate a Quickbooks Web Connector (QWC) file. Enter the required information below and download the QWC file.  Once you have downloaded the QWC file, start QuickBooks&reg; and open your company file. 
           Then double click on the QWC file to install it.</p>
           
        <div><ZNode:spacer id="divTopSpacer" SpacerHeight="15" SpacerWidth="3" runat="server"></ZNode:spacer></div>
        <div class="FieldStyle">Application Name<span class="Asterix">*</span></div>
        <small>Choose an application name that is meaningful to you when setting up the QuickBooks&reg; Web Connector.</small>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="AppName" Display="Dynamic" ErrorMessage="* Enter a application name" SetFocusOnError="True"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:TextBox ID="AppName" runat="server" Columns="25" ></asp:TextBox></div>
        
        <div class="FieldStyle">Service type<span class="Asterix">*</span></div>        
        <small>Select the type of download you would like to perform using the QuickBooks&reg; Web Connector. The <b>Order Download</b> will create everything you need in Quickbooks based on Orders in your storefront. It will add Orders, Inventory Items and Customer Accounts in QuickBooks&reg;. The <b>Product Download</b> will just create Inventory Items in QuickBooks&reg; from the Products you have defined in your storefront. The <b>Item Inventory Download</b> will update the inventory levels of the Items you have in Quickbooks. Use the <b>Account Download</b> to download all accounts from the storefront to Quickbooks. The Product Download, Item Inventory Download and Account Download are optional and should be run manually from your Web Connector rather than being scheduled.</small>
        <div class="ValueStyle">
            <asp:DropDownList ID="ddlServiceType" runat="server">
                <asp:ListItem Selected="True" Text="Order Download" Value="Order"></asp:ListItem>
                <asp:ListItem Text="Product Download" Value="Product"></asp:ListItem>
                <asp:ListItem Text="Item Inventory Download" Value="Inventory"></asp:ListItem>
                <asp:ListItem Text="Account Download" Value="Account"></asp:ListItem>
            </asp:DropDownList>
        </div>
        
        <div class="FieldStyle">User Name<span class="Asterix">*</span></div>
        <small>You must create a User Account in Znode Storefront with the WEB SERVICES role enabled. Enter that user name here.</small>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="UserName" Display="Dynamic" ErrorMessage="* Enter a user name" SetFocusOnError="True"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:TextBox ID="UserName" runat="server" Columns="25" ></asp:TextBox></div>
        
        <div class="FieldStyle">Auto Run Interval</div>
        <small>Enter how often the QuickBooks&reg; Web Connector should download orders or Inventory parts. Specifying too short a time will cause extra load on your web site.</small>
        <div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TimeInterval" Display="Dynamic" ErrorMessage="* Enter a time interval value" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TimeInterval" CssClass="Error" Display="Dynamic" ErrorMessage="You Must Enter a Valid interval time."
                    ValidationExpression="^[1-9][0-9]*"></asp:RegularExpressionValidator>
        </div>
        <div class="ValueStyle"><asp:TextBox ID="TimeInterval" runat="server" Columns="25" >15</asp:TextBox></div>
        
        <div><ZNode:spacer id="Spacer1" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>
        
        <div>
            <asp:Button ID="DownloadQWC" CssClass="Button" Text="Download QWC file" runat="server" OnClick="DownloadQWC_Click" />
            <asp:button CssClass="Button" id="btnCancel" CausesValidation="False" Text="Cancel" Runat="server" OnClick="btnCancel_Click" />
        </div>
                
        <div><ZNode:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>
    </div>
</asp:Content>