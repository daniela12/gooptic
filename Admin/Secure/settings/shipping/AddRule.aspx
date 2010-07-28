<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="AddRule.aspx.cs" Inherits="Admin_Secure_settings_shipping_AddRule" Title="Untitled Page" %>

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
    	<div><asp:Label ID="lblMsg" runat="server" CssClass="Error"></asp:Label></div>
    	<div><uc1:spacer id="Spacer1" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>
    	
    	<h4>Select a Rule Type</h4>
        <div class="ValueStyle"><asp:DropDownList ID="lstShippingRuleType" runat="server" OnSelectedIndexChanged="lstShippingRuleType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div> 
        
        <h4>Set Pricing</h4>
        
        <div class="FieldStyle">Base Cost</div>
        <p>Enter the base cost which is applied irrespective of the number of items.</p>
 	    <div><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBaseCost" Display="Dynamic" ErrorMessage="Enter a Base Cost" SetFocusOnError="True"></asp:RequiredFieldValidator></div> 
 	    <div><asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtBaseCost" Type="Currency" Display="Dynamic" Operator="DataTypeCheck" /> </div>
        <div class="ValueStyle"><span class="DollarSymbol">$ </span><asp:TextBox ID="txtBaseCost" runat="server"></asp:TextBox></div>
     
        <div class="FieldStyle">Per Unit Cost</div>
        <p>Enter the shipping cost to be applied to each unit in the order.</p>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPerItemCost" Display="Dynamic" ErrorMessage="Enter a Per Item Cost" SetFocusOnError="True"></asp:RequiredFieldValidator></div> 
        <div><asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtPerItemCost" Type="Currency" Display="Dynamic" Operator="DataTypeCheck" /></div>
        <div class="ValueStyle"><span class="DollarSymbol">$ </span><asp:TextBox ID="txtPerItemCost" runat="server"></asp:TextBox></div>
     
        <asp:Panel ID="pnlNonFlat" runat="server" Visible="false"> 	
            <h4>Enter Limits for tiered pricing</h4>
            <p>The lower and upper limits will create a tiered pricing scheme. For example you may
               want to create a rule where shipping for items from 0-10 cost $10. In this case
               you would set the lower limit to 0 and upper limit to 10
            </p>
            
    	    <div class="FieldStyle">Lower Limit (<% =TierText %>)</div>    	    
 	        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtLowerLimit" Display="Dynamic" ErrorMessage="Enter a Lower Limit" SetFocusOnError="True"></asp:RequiredFieldValidator></div> 
 	        <div><asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtLowerLimit" Type="Double" Operator="DataTypeCheck" Display="Dynamic" ErrorMessage="You must enter a valid lower limit (ex: 2)" /> </div>
            <div class="ValueStyle"><asp:TextBox ID="txtLowerLimit" runat="server">0.00</asp:TextBox></div>  
        
    	    <div class="FieldStyle">Upper Limit (<% =TierText %>)</div>
 	        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtUpperLimit" Display="Dynamic" ErrorMessage="Enter a Upper Limit" SetFocusOnError="True"></asp:RequiredFieldValidator></div> 
 	        <div><asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtUpperLimit" Type="Double" Operator="DataTypeCheck" ErrorMessage="You must enter a valid upper limit (ex: 2)" Display="Dynamic" /> </div>
            <div class="ValueStyle"><asp:TextBox ID="txtUpperLimit" runat="server"></asp:TextBox></div>
        </asp:Panel>

	    <div>
	        <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
	        <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	    </div>	
	</div>
</asp:Content>

