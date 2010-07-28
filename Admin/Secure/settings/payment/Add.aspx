<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Admin_Secure_settings_payment_Add" Title="Untitled Page" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <div class="Form">
	    <table width="100%" cellspacing="0" cellpadding="0" >
		    <tr>
			    <td><h1><asp:Label ID="lblTitle" Runat="server"></asp:Label>
                    <uc2:DemoMode ID="DemoMode1" runat="server" />
                </h1></td>
			    <td align="right">
				    <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>
			    
	    <div><uc1:spacer id="Spacer8" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>
    	<div><asp:Label ID="lblMsg" EnableViewState="false" runat="server" CssClass="Error"></asp:Label></div>
    	<div><uc1:spacer id="Spacer1" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>
    	
    	<h4>General Settings</h4>
    	
    	<div class="FieldStyle">Select a profile to which this setting should be applied<span class="Asterix">*</span></div>
        <div class="ValueStyle"><asp:DropDownList ID="lstProfile" runat="server"></asp:DropDownList></div> 
        
    	<div class="FieldStyle">Select a payment type<span class="Asterix">*</span></div>
        <div class="ValueStyle"><asp:DropDownList ID="lstPaymentType" runat="server" OnSelectedIndexChanged="lstPaymentType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div> 
        <div class="Error" runat="server" id="googleNote" visible="false"><p>Note: For the Google checkout payment type, ensure that FedEx or UPS are specified as a Shipping Option.</p></div>
        <div class="FieldStyle">Check the box below to enable this payment option</div>
        <div class="ValueStyle"><asp:CheckBox ID="chkActiveInd" runat="server" Checked="true" Text="Enable" /></div>

        <div class="FieldStyle">Display Order<span class="Asterix">*</span></div> 	    
        <div class="HintStyle">Enter a number. Items with a lower number are displayed first on the page.</div>                
        <div class="ValueStyle">
            <asp:TextBox ID="txtDisplayOrder" runat="server" MaxLength="9" Columns="5"></asp:TextBox>
            <asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" Display="Dynamic" ErrorMessage="* Enter a Display Order" ControlToValidate="txtDisplayOrder"></asp:requiredfieldvalidator>
            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtDisplayOrder" Display="Dynamic" 
            ErrorMessage="Enter a whole number." MaximumValue="999999999" MinimumValue="1" Type="Integer"></asp:RangeValidator>
        </div>
        
        <asp:Panel ID="pnlCreditCard" runat="server" Visible="false">
    	    <h4>Merchant Gateway Settings</h4>
    	    
        	<asp:Panel ID="pnlGatewayList" runat="server" Visible="true"> 
    	    <div class="FieldStyle">Select a gateway<span class="Asterix">*</span></div>
            <div class="ValueStyle"><asp:DropDownList ID="lstGateway" runat="server" OnSelectedIndexChanged="lstGateway_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>         
        	</asp:Panel> 
        	
        	<asp:Panel ID="pnlLogin" runat="server" Visible="true">
    	        <div class="FieldStyle"><asp:Label ID="lblGatewayUserName" runat="server" Text="Merchant account login"/><span class="Asterix">*</span></div>
    	        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtGatewayUserName" Display="Dynamic" ErrorMessage="* Enter merchant login name" SetFocusOnError="True"></asp:RequiredFieldValidator></div>
                <div class="ValueStyle"><asp:TextBox ID="txtGatewayUserName" runat="server" MaxLength="50" Columns="50"></asp:TextBox></div>
    	    </asp:Panel>
    	        
    	    <asp:Panel ID="pnlPassword" runat="server" Visible="true">    
      	        <div class="FieldStyle"><asp:Label ID="lblGatewaypassword" runat="server" Text="Merchant account password"/><span class="Asterix">*</span></div>                        
                <div><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtGatewayPassword" Display="Dynamic" ErrorMessage="* Enter merchant password" SetFocusOnError="True"></asp:RequiredFieldValidator></div>
                <div class="ValueStyle"><asp:TextBox ID="txtGatewayPassword" runat="server" MaxLength="50" Columns="50" TextMode="Password"></asp:TextBox></div>
      	    </asp:Panel> 
      	    
      	    <asp:Panel ID="pnlAuthorizeNet" runat="server" Visible="false">
       	        <div class="FieldStyle"><asp:Label ID="lblTransactionKey" runat="server">Transaction key (Authorize.Net only)</asp:Label></div>
                <div class="ValueStyle"><asp:TextBox ID="txtTransactionKey" runat="server" MaxLength="70" Columns="50"></asp:TextBox></div>
           	</asp:Panel>
           	
           	<asp:Panel ID="pnlVerisignGateway" runat="server" Visible="false">
       	        <div class="FieldStyle"><asp:Label ID="lblPartner" runat="server">Partner</asp:Label></div>
       	        <div class="HintStyle">Enter your Verisign partner. If you don't know your partner try using paypal. Note this field is case sensitive.</div>
                <div class="ValueStyle"><asp:TextBox ID="txtPartner" runat="server" MaxLength="70" Columns="50"></asp:TextBox></div>
                
                <div class="FieldStyle"><asp:Label ID="lblVendor" runat="server">Vendor</asp:Label></div>
                <div class="HintStyle">Enter your Verisign Vendor ID. If you do not know your Vendor ID try using your Merchant account login.</div>
                <div class="ValueStyle"><asp:TextBox ID="txtVendor" runat="server" MaxLength="70" Columns="50"></asp:TextBox></div>
           	</asp:Panel>
           	            
           	<div class="FieldStyle">Gateway TEST mode</div>
            <div class="ValueStyle"><asp:CheckBox ID="chkTestMode" runat="server" Checked="false" Text="Enable Test Mode (Your gateway may not support this mode)" /></div>

            <asp:Panel ID="pnlGatewayOptions" runat="server" Visible="false">
       	        <div class="FieldStyle">Do not capture</div>
                <div class="ValueStyle"><asp:CheckBox ID="chkPreAuthorize" runat="server" Checked="false" Text="Pre-Authorize transactions without capturing" /></div>
            </asp:Panel>
           
            <asp:Panel ID="pnlCreditCardOptions" runat="server">
            <div class="FieldStyle">Select the credit cards that will be accepted</div>
            <div class="ValueStyle"><asp:CheckBox ID="chkEnableVisa" runat="server" Checked="true" Text="Visa" />&nbsp;&nbsp;<asp:CheckBox ID="chkEnableMasterCard" runat="server" Checked="true" Text="MasterCard" />&nbsp;&nbsp;<asp:CheckBox ID="chkEnableAmex" runat="server" Checked="true" Text="American Express" />&nbsp;&nbsp;<asp:CheckBox ID="chkEnableDiscover" runat="server" Checked="true" Text="Discover" /></div>                  
            </asp:Panel>
            
	    </asp:Panel>
	    
	    <div>
	        <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
	        <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	    </div>	
	</div>
</asp:Content>

