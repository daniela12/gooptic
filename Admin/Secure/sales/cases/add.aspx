<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master"  AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="Admin_Secure_sales_cases_Default" validateRequest="false" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">

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
    	
       
        <h4>Service Request Information</h4>
        
        <div class="FieldStyle"><asp:Label ID="lblCreateDate" Runat="server" /> <asp:Label id="lblCaseDate" runat="server" /></div>
        <div class="ValueStyle"></div>
    	<div class="FieldStyle">Status</div>
        <div class="ValueStyle"><asp:DropDownList ID="lstCaseStatus" runat="server"></asp:DropDownList></div> 
        
        <div class="FieldStyle">Priority</div>
        <div class="ValueStyle"><asp:DropDownList ID="lstCasePriority" runat="server"></asp:DropDownList></div>       	
        
        <div class="FieldStyle">Title<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCaseTitle" Display="Dynamic" ErrorMessage="Enter Case Title" SetFocusOnError="True" CssClass="Error"></asp:RequiredFieldValidator></div> 
        <div class="ValueStyle"><asp:TextBox ID="txtCaseTitle" runat="server" MaxLength="50" Columns="20"></asp:TextBox></div>
        
        <div class="FieldStyle">Origin</div>
        <div class="ValueStyle"><asp:TextBox ID="txtCaseOrigin" runat="server" MaxLength="50" Columns="20"></asp:TextBox></div>
 
 	    <div class="FieldStyle">Description<span class="Asterix">*</span></div>
 	    <div><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCaseDescription" Display="Dynamic" ErrorMessage="Enter  Case Description" SetFocusOnError="True" CssClass="Error"></asp:RequiredFieldValidator></div> 
 	    <div class="ValueStyle"><asp:TextBox ID="txtCaseDescription" runat="server" TextMode="MultiLine" Height="190px" MaxLength="10000" Width="812px"></asp:TextBox></div>
        
        <h4>Contact Information</h4>
        <div class="FieldStyle">If existing customer, select account</div>
        <div class="ValueStyle"><asp:DropDownList ID="lstAccountList" runat="server"></asp:DropDownList></div> 
        
        <div class="FieldStyle">First Name<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName" Display="Dynamic" ErrorMessage="Enter First Name" SetFocusOnError="True" CssClass="Error"></asp:RequiredFieldValidator></div> 
 	    <div class="ValueStyle"><asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></div>
        
        <div class="FieldStyle">Last Name<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName" Display="Dynamic" ErrorMessage="Enter Last Name" SetFocusOnError="True" CssClass="Error"></asp:RequiredFieldValidator></div> 
 	    <div class="ValueStyle"><asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></div>
        
        <div class="FieldStyle">Company Name (Optional)</div>
        <div class="ValueStyle"><asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox></div>
        
        <div class="FieldStyle">Email ID <span class="Asterix">*</span></div>
        <div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmailID" Display="Dynamic" ErrorMessage="Enter a Email ID" SetFocusOnError="True" CssClass="Error"></asp:RequiredFieldValidator>
                <asp:regularexpressionvalidator id="regemailID" runat="server" ControlToValidate="txtEmailID" ErrorMessage="*Please use a valid email address."
						                Display="Dynamic" ValidationExpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+" ValidationGroup="1" CssClass="Error" SetFocusOnError="True"></asp:regularexpressionvalidator>
        </div> 
        <div class="ValueStyle"><asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox></div>
        
        <div class="FieldStyle">Phone Number <span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPhoneNo" Display="Dynamic" ErrorMessage="Enter phone Number" SetFocusOnError="True" CssClass="Error"></asp:RequiredFieldValidator></div> 
        <div class="ValueStyle"><asp:TextBox ID="txtPhoneNo" runat="server"></asp:TextBox></div>
        
        <div>
	        <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
	        <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	    </div>	
</div> 

</asp:Content>


