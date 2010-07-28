<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Admin_Secure_settings_taxes_Add" Title="Untitled Page"%>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <div class="Form">
	    <table cellspacing="0" cellpadding="0" width="100%">
		    <tr>
			    <td><h1><asp:Label ID="lblTitle" Runat="server"></asp:Label></h1></td>
			    <td align="right">
			        <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>
	
        <div Class="Error"><asp:Label ID="lblMsg" Runat="server"></asp:Label></div>	
	    <div class="FieldStyle">Tax Class Name</div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTaxClassName" Display="Dynamic" ErrorMessage="Enter a Tax class name." SetFocusOnError="True"></asp:RequiredFieldValidator></div>         
        <div class="ValueStyle"><asp:TextBox ID="txtTaxClassName" runat="server" MaxLength="15"></asp:TextBox></div>
        
        <div class="FieldStyle">Display Order<span class="Asterix">*</span></div> 	    
        <div class="HintStyle">Enter a whole number.. This determines the order in which this tax rule will be processed.</div>                
        <div class="ValueStyle">
            <asp:TextBox ID="txtDisplayOrder" runat="server" MaxLength="9" Columns="5"></asp:TextBox>
            <asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" Display="Dynamic" ErrorMessage="* Enter a Display Order" ControlToValidate="txtDisplayOrder"></asp:requiredfieldvalidator>
            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtDisplayOrder" Display="Dynamic" 
            ErrorMessage="Enter a whole number." MaximumValue="999999999" MinimumValue="1" Type="Integer"></asp:RangeValidator>
        </div>
                
        <div class="FieldStyle">Enable</div>        
        <div class="ValueStyle"><asp:CheckBox Checked="true" ID="chkActiveInd" Text="Check this box to enable this Tax Class."  runat="server" /></div>
        
        <div>
	        <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
	        <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	    </div>
	    <div><uc1:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></div>
	</div>
</asp:Content>