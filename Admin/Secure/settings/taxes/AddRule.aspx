<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="AddRule.aspx.cs" Inherits="Admin_Secure_settings_tax_AddRule" Title="Untitled Page" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
  <asp:ScriptManager id="ScriptManager" runat="server" EnablePartialRendering="true"></asp:ScriptManager> 
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
    	
    	<h4>General</h4>
    	<div class="FieldStyle">Rule Type</div>
    	<div class="ValueStyle"><asp:DropDownList ID="ddlRuleTypes" runat="server"></asp:DropDownList></div>
    	
    	<h4>Tax Region</h4>
    	<div><p>Apply this rule based on where this item is being shipped to.</p></div>
    	<asp:UpdatePanel ID="UpdatePanelTaxRegion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" RenderMode="Inline">
        <ContentTemplate>      	
            <div class="FieldStyle">Destination Country</div>
            <div class="ValueStyle"><asp:DropDownList ID="lstCountries" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstCountries_SelectedIndexChanged"></asp:DropDownList></div> 
                 
            <div class="FieldStyle">Destination State</div>
            <div class="ValueStyle"><asp:DropDownList ID="lstStateOption" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstStateOption_SelectedIndexChanged" Width="160px">
            <asp:ListItem Text="Apply to ALL States" Value ="0"></asp:ListItem></asp:DropDownList></div>
            
            <div class="FieldStyle">Destination County</div>
            <div class="ValueStyle"><asp:DropDownList ID="lstCounty" runat="server" Width="160px">
            <asp:ListItem Text="Apply to ALL Counties" Value ="0"></asp:ListItem></asp:DropDownList></div>
        </ContentTemplate>
        </asp:UpdatePanel>
                
        <h4>Tax Rate</h4> 
        <div><p>Specify one or more tax rates below based on your regional requirements.</p></div>
        <div class="Form">
        <table cellpadding="0" cellspacing="0" border="0">	
	            <tr class="Row">
		            <td colspan="2"><uc1:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></td>
	            </tr>		            
	            <tr class="Row">
		            <td class="FieldStyle" align="right">Sales Tax</td>
		            <td>
		                <div><asp:TextBox ID="txtSalesTax" runat="server" Columns="6" MaxLength="5">0.00</asp:TextBox>%</div>
		                <div><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSalesTax" Display="Dynamic" ErrorMessage="Enter a Sales Tax" SetFocusOnError="True"></asp:RequiredFieldValidator></div>
		            </td>
	            </tr>
	            <tr class="Row">
		            <td colSpan="2"><uc1:spacer id="Spacer3" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></td>
	            </tr>
	            <tr class="Row">
		            <td class="FieldStyle" align="right">VAT Tax</td>
		            <td>
		                <div><asp:TextBox ID="txtVAT" runat="server" Columns="6" MaxLength="5">0.00</asp:TextBox>%</div>
                        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtVAT" Display="Dynamic" ErrorMessage="Enter a Vat Tax"  SetFocusOnError="True"></asp:RequiredFieldValidator></div> 
		            </td>
	            </tr>
	            <tr class="Row">
		            <td colSpan="2"><uc1:spacer id="Spacer4" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></td>
	            </tr>
	            <tr class="Row">
		            <td class="FieldStyle" align="right">GST Tax</td>
		            <td>
		                <div><asp:TextBox ID="txtGST" runat="server" Columns="6" MaxLength="5">0.00</asp:TextBox>%</div>
		                <div><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtGST" Display="Dynamic" ErrorMessage="Enter a GST Tax" SetFocusOnError="True"></asp:RequiredFieldValidator></div> 
                        <div><asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtGST" CssClass="Error" Display="Dynamic" ErrorMessage="Enter a valid GST Tax" MaximumValue="50" MinimumValue="0" SetFocusOnError="True" Type="Double"></asp:RangeValidator></div>
	                </td>
	            </tr>
	            <tr class="Row">
		            <td colSpan="2"><uc1:spacer id="Spacer5" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></td>
	            </tr>	
	            <tr class="Row">
	                <td class="FieldStyle" align="right">PST Tax</td>
	                <td>
	                    <div><asp:TextBox ID="txtPST" runat="server" Columns="6" MaxLength="5">0.00</asp:TextBox>%</div>
                        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPST" Display="Dynamic" ErrorMessage="Enter a PST Tax" SetFocusOnError="True"></asp:RequiredFieldValidator></div> 
                        <div><asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtPST" CssClass="Error" Display="Dynamic" ErrorMessage="Enter a valid PST Tax" MaximumValue="50" MinimumValue="0" SetFocusOnError="True" Type="Double"></asp:RangeValidator></div>
	                </td>
	            </tr>
	            <tr class="Row">
		            <td colSpan="2"><uc1:spacer id="Spacer6" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></td>
	            </tr>
	            <tr class="Row">
	                <td class="FieldStyle" align="right">HST Tax</td>
	                <td colspan="2">
	                    <div><asp:TextBox ID="txtHST" runat="server" Columns="6" MaxLength="5">0.00</asp:TextBox>%</div>
                        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtHST" Display="Dynamic" ErrorMessage="Enter a HST Tax" SetFocusOnError="True"></asp:RequiredFieldValidator></div> 
                        <div><asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="txtHST" CssClass="Error" Display="Dynamic" ErrorMessage="Enter a valid HST Tax" MaximumValue="50" MinimumValue="0" SetFocusOnError="True" Type="Double"></asp:RangeValidator></div>
	                </td>
	            </tr>
	     </table>
	     </div>
	    <div class="FieldStyle">Precedence<span class="Asterix">*</span></div>
        <p> This is the order in which this tax rule will be processed.</p>        
        <div class="ValueStyle"><asp:TextBox ID="txtPrecedence" runat="server" MaxLength="9" Columns="5">1</asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPrecedence" Display="Dynamic" ErrorMessage="* Enter precedence" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtPrecedence" Display="Dynamic" ErrorMessage="Enter a whole number." MaximumValue="999999999" MinimumValue="1" SetFocusOnError="True" Type="Integer"></asp:RangeValidator></div>
        
        <div class="FieldStyle"><asp:CheckBox ID="chkTaxInclusiveInd" Text=""  runat="server" /> Include Taxes in Product Pricing</div>
        <div class="ValueStyle"></div>
        
	    <div>
	        <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
	        <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	    </div>	
	</div>
</asp:Content>

