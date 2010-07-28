<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Admin_Secure_sales_Affiliates_Edit" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
<div class="Form">
        <table width="100%" cellspacing="0" cellpadding="0">
		    <tr>
			    <td>
			        <h1><asp:Label ID="lblTitle" Runat="server"></asp:Label></h1>
                    <uc2:DemoMode ID="DemoMode1" runat="server" />			        
			    </td>
			    <td align="right">
				    <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click" ValidationGroup="EditContact"></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>
		 
		<div><asp:Label ID="lblErrorMsg" runat="server" CssClass="Error" EnableViewState="false"></asp:Label></div>
	    <div><uc1:spacer id="Spacer8" SpacerHeight="2" SpacerWidth="3" runat="server"></uc1:spacer></div>
       
        
        
   <h4>Affiliate Information</h4>
   
      <div class="FieldStyle">Affiliate ID</div>                        
      <div class="ValueStyle"><asp:TextBox ID="txtAccountID" runat="server"/></div>
      
      <div class="FieldStyle">First Name</div>                        
      <div class="ValueStyle"><asp:TextBox ID="txtFisrtName" runat="server"/></div>
      
      <div class="FieldStyle">Last Name</div>                        
      <div class="ValueStyle"><asp:TextBox ID="txtLastName" runat="server"/></div>
           
      <div class="FieldStyle">Company Name</div>                        
      <div class="ValueStyle"><asp:TextBox ID="txtCompanyName" runat="server"/></div>
      
      <div class="FieldStyle">Email ID</div>                        
      <div class="ValueStyle"><asp:TextBox ID="txtEmailID" runat="server"/></div>
      
      <div class="FieldStyle">Phone Number</div>                        
      <div class="ValueStyle"><asp:TextBox ID="txtPhoneNumber" runat="server"/></div>
      
      <div  class="FieldStyle">Referral Commision Type</div>                        
      <div class="ValueStyle">
        <asp:DropDownList ID="lstReferral" runat="server" OnSelectedIndexChanged="DiscountType_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
      </div> 
  
      <div class="FieldStyle">Referral Commission<span class="Asterix">*</span></div>
      <div class="ValueStyle"><asp:TextBox ID="Discount" runat="server" MaxLength="7" Columns="25"></asp:TextBox>
       <asp:RangeValidator ID="discAmountValidator" runat="server" ControlToValidate="Discount" ValidationGroup="EditContact"
            CssClass="Error" Enabled="false" Display="Dynamic" MaximumValue="9999999" MinimumValue="0.01" CultureInvariantValues="true" Type="Currency"></asp:RangeValidator>
        <asp:RangeValidator ID="discPercentageValidator" Enabled="true" runat="server" ControlToValidate="Discount" ValidationGroup="EditContact"
            CssClass="Error" Display="Dynamic" MaximumValue="100" CultureInvariantValues="true" MinimumValue="0.01" SetFocusOnError="True" Type="Double"></asp:RangeValidator></div>
                  
           
      <div class="FieldStyle">TaxID</div>                        
      <div class="ValueStyle"><asp:TextBox ID="txtTaxId" runat="server"/></div>
             
       <table>
        <tr>
            <td align="right">
			    <asp:button class="Button" id="Submit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click" ValidationGroup="EditContact"></asp:button>
			    <asp:button class="Button" id="Cancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			</td>
        </tr>
       </table>
         
</div> 
</asp:Content>