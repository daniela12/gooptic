<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Admin_Secure_settings_QuickBooks_edit" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">    
    <div><asp:XmlDataSource ID="QBXmlDataSource" runat="server"></asp:XmlDataSource></div>
    <div class="Form">
        <ZNode:DemoMode ID="DemoMode1" runat="server" />
        <table width="100%" cellSpacing="0" cellPadding="0" >
		    <tr>
                <td><h1>QuickBooks&reg; configuration setup</h1></td>
			    <td align="right">
				    <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>
	    
	    <!-- display Error Message -->
	    <div><ZNode:spacer id="Spacer" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>
	    <div><asp:Label ID="lblMsg" CssClass="Error" runat="server"></asp:Label></div>	    
        <div><ZNode:spacer id="Spacer1" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>
        
        
        <h4>General</h4>
        <div class="FieldStyle">Customer Sales Receipt Message</div>
        <div class="ValueStyle"><asp:TextBox ID="txtSalesCustomerMessage" Columns="60" runat="server"/></div>
        
        <div class="FieldStyle">QB Company File full path</div>
        <small>If your web service wants to try a different company, supply the company pathname here, otherwise it uses the currently open company file in the Quickbooks.</small>
        <div class="ValueStyle"><asp:TextBox ID="txtCompanyFile" Columns="60" runat="server"/></div>
        
        <div class="FieldStyle">Order download request type</div>
        <div class="HintStyle">Sales Order feature are available only in the Premier and Enterprise Solutions editions of Quickbooks&reg;.</div>
        <div class="ValueStyle">
            <asp:DropDownList ID="lstorderDownloadType" runat="server">
                <asp:ListItem Selected="True" Text="Sales Receipt" Value="SalesReceipt"></asp:ListItem>
                <asp:ListItem Text="Sales Order" Value="SalesOrder"></asp:ListItem>
            </asp:DropDownList>
        </div>
        
        <h4>Inventory Item List</h4>
        <small>Order line items are tracked in Quickbooks&reg; using the Item List. The values below must match exactly the Item names that you have used in Quickbooks&reg;.</small>
        <div><ZNode:spacer id="Spacer9" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>

        <div class="FieldStyle">Sales Tax Item Name<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtTaxItem" runat="server" ErrorMessage="Enter Sales tax item name"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:TextBox ID="txtTaxItem" runat="server"/></div>

        <div class="FieldStyle">Discount Item Name<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtDiscountItem" runat="server" ErrorMessage="Enter discount item name"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:TextBox ID="txtDiscountItem" runat="server"/></div>

        <div class="FieldStyle">Shipping Item Name<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ControlToValidate="txtShippingItem" runat="server" ErrorMessage="Enter shipping item name"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:TextBox ID="txtShippingItem" runat="server"/></div>

        <h4>Accounts</h4>
        <small>These settings determine what accounts are used in Quickbooks&reg; for tracking your orders. The valus below must match exactly the Account names that you have used in Quickbooks&reg;.</small>
        <div><ZNode:spacer id="Spacer8" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>

        <div class="FieldStyle">Sales Income Account type<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" ControlToValidate="txtIncomeAccount" runat="server" ErrorMessage="Enter sales income account name"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:TextBox ID="txtIncomeAccount" runat="server"/></div>

        <div class="FieldStyle">Cost of Goods Sold Account type<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" ControlToValidate="txtCOGSAccount" runat="server" ErrorMessage="Enter cost of goods account name"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:TextBox ID="txtCOGSAccount" runat="server"/></div>

        <div class="FieldStyle">Inventory Asset Account type<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" ControlToValidate="txtAssetAccount" runat="server" ErrorMessage="Enter inventory asset account name"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:TextBox ID="txtAssetAccount" runat="server"/></div>
        
        <h4>Customers</h4>
        <small>In Quickbooks&reg; you will need to set up a Customer Type for each Znode Storefront Profile that you sell to. Each must be set up exactly as they appear here.</small>
        <div><ZNode:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>
        <asp:PlaceHolder ID="ControlPlaceHolder" runat="server"></asp:PlaceHolder>
        
        <div>
	        <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
	        <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	    </div>
    </div>
</asp:Content>