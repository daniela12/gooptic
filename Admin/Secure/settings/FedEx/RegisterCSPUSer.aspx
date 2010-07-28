<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master"  AutoEventWireup="true" CodeFile="RegisterCSPUSer.aspx.cs" Inherits="Admin_Secure_settings_FedEx_add" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode"%>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div class="Form">        
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <h1>FedEx Register User</h1>
                    <ZNode:demomode id="DemoMode1" runat="server"></ZNode:demomode>
                </td>
                <td align="right">
                    <asp:Button ID="btnSubmitTop" runat="server" CausesValidation="True" class="Button" OnClick="btnSubmit_Click" Text="SUBMIT" />
                    <asp:Button ID="btnCancelTop" runat="server" CausesValidation="False" class="Button" OnClick="btnCancel_Click" Text="CANCEL" />
                </td>
            </tr>
        </table>
        
        <div><asp:Label ID="lblErrorMsg" runat="server" CssClass="Error"></asp:Label></div>
        
        <div><ZNode:spacer id="Spacer8" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>
        	    
	    <div class="FieldStyle">First Name<span class="Asterix">*</span></div>	    
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="FirstName" Display="Dynamic" CssClass="Error" ErrorMessage="* Enter a profile name" SetFocusOnError="True"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:TextBox ID="FirstName" runat="server" Columns="25" ></asp:TextBox></div>
        
        <div class="FieldStyle">Last Name<span class="Asterix">*</span></div>	    
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="LastName" Display="Dynamic" CssClass="Error" ErrorMessage="* Enter a profile name" SetFocusOnError="True"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:TextBox ID="LastName" runat="server" Columns="25" ></asp:TextBox></div>
        
        <div class="FieldStyle">Phone Number<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Enter Phone Number" ControlToValidate="PhoneNumber" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:textbox id="PhoneNumber" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox></div>
        
        
        <div class="FieldStyle">Email Address<span class="Asterix">*</span></div>
        <div>
            <asp:regularexpressionvalidator id="regemailID" runat="server" ControlToValidate="EmailId" ErrorMessage="*Please use a valid email address." Display="Dynamic" ValidationExpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+" ValidationGroup="EditContact" CssClass="Error"></asp:regularexpressionvalidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="* Enter Email ID" ControlToValidate="EmailId" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="ValueStyle"><asp:TextBox ID="EmailId" runat="server"></asp:TextBox></div>
                
        
        <h4>Billing Address</h4>
                    
        <div class="FieldStyle">Company Name</div>
        <div class="ValueStyle"><asp:textbox id="txtBillingCompanyName" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox></div>        
		
        <div class="FieldStyle">Street1<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* Enter Address line 1" ControlToValidate="txtBillingStreet1" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:textbox id="txtBillingStreet1" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox></div>
	   
        <div class="FieldStyle">Street2</div>
       <div class="ValueStyle"><asp:textbox id="txtBillingStreet2" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox></div>	               
        
        <div class="FieldStyle">City<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="* Enter city" ControlToValidate="txtBillingCity" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:textbox id="txtBillingCity" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox></div>
	  
        <div class="FieldStyle">State<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="* Enter state" ControlToValidate="txtBillingState" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:textbox id="txtBillingState" runat="server" width="30" columns="10" MaxLength="2"></asp:textbox></div>
        
        <div class="FieldStyle">Postal Code<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="* Enter postal code" ControlToValidate="txtBillingPostalCode" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:textbox id="txtBillingPostalCode" runat="server" width="130" columns="30" MaxLength="20"></asp:textbox></div>
	  
        <div class="FieldStyle">Country<span class="Asterix">*</span></div>
        <div class="ValueStyle"><asp:DropDownList ID="lstBillingCountryCode" runat="server"></asp:DropDownList></div>        
        
        
        <div><ZNode:spacer id="Spacer1" SpacerHeight="20" SpacerWidth="3" runat="server" /></div>
        
        <asp:button class="Button" id="SubmitButton" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click" ></asp:button>
	    <asp:button class="Button" id="CancelButton" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	    
    </div>

</asp:Content>