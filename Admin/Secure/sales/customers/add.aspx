<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="Admin_Secure_sales_customers_add" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
<div class="Form">
        <table width="100%" cellSpacing="0" cellPadding="0">
		    <tr>
			    <td>
			        <h1>Quick Add Account</h1>
                    <ZNode:DemoMode ID="DemoMode1" runat="server" />			        
			    </td>
			    <td align="right">
				    <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>
	    <div><ZNode:spacer id="Spacer8" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>
	    <div class="FieldStyle">Select a Profile for this Account</div>
          
        <div class="ValueStyle">
                                <asp:DropDownList ID="ListProfileType" runat="server" Width="100px" />
        </div>
        <div class="FieldStyle">First Name<span class="Asterix">*</span></div>
        <div class="ValueStyle">
		                        <asp:textbox id="txtBillingFirstName" runat="server" width="130" columns="30" MaxLength="100" />
		                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Enter BillingFirstName" ControlToValidate="txtBillingFirstName" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
		</div>
        
        <div class="FieldStyle">Last Name<span class="Asterix">*</span></div>
        <div class="ValueStyle">
		                        <asp:textbox id="txtBillingLastName" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox>
		                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* Enter Billing LastName" ControlToValidate="txtBillingLastName" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="Error" DisplayMode="List"
                ShowMessageBox="True" ShowSummary="False" />
		</div>
        
	    <div class="FieldStyle">Phone Number<span class="Asterix">*</span></div>
	    <div class="ValueStyle">
		                        <asp:textbox id="txtBillingPhoneNumber" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox>
		                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Enter Billing Phone Number" ControlToValidate="txtBillingPhoneNumber" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        
	    <div class="FieldStyle">Email Address<span class="Asterix">*</span></div>
	    <div class="ValueStyle">
                                <asp:TextBox ID="txtBillingEmail" runat="server"></asp:TextBox>
                                <asp:regularexpressionvalidator id="regemailID" runat="server" ControlToValidate="txtBillingEmail" ErrorMessage="*Please use a valid email address."
						                Display="Dynamic" ValidationExpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+" ValidationGroup="1" CssClass="Error"></asp:regularexpressionvalidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Enter Email ID" ControlToValidate="txtBillingEmail" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div>
            <asp:Label ID="ErrorMessage" runat="server"></asp:Label>
            <ZNode:spacer id="Spacer3" SpacerHeight="10" SpacerWidth="3" runat="server" />
        </div>
        <table>
                <tr>
                     <td align="right">
				                         <asp:button class="Button" id="Submit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
				                         <asp:button class="Button" id="Cancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			         </td>
                </tr>
       </table>
</asp:Content>


