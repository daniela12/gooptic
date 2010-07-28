<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="Admin_Secure_catalog_product_Attribute_Types_add" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">

<div class="Form">
	    <table width="100%" cellSpacing="0" cellPadding="0" >
		    <tr>
			    <td><h1><asp:Label ID="lblTitle" Runat="server"></asp:Label>
                    <uc1:DemoMode ID="DemoMode1" runat="server" />
                </h1></td>
			    <td align="right">
				    <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>
	   	    
	    <asp:Label ID="lblError" runat="server" />
	    
        <div class="FieldStyle">
                Attribute Type Name (ex: Color)<span class="Asterix">*</span>
        <div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Name" Display="Dynamic" ErrorMessage="* Enter a Attribute Type name" SetFocusOnError="True"></asp:RequiredFieldValidator>
        </div>
        
        <div class="ValueStyle">
            <asp:TextBox ID="Name" runat="server" MaxLength="50" Columns="30"></asp:TextBox>
        </div>
        
	    <div class="FieldStyle">Display Order<span class="Asterix">*</span></div> 	    
        <div class="HintStyle">Enter a number. Items with a lower number are displayed first on the page.</div>                
        <div class="ValueStyle">
            <asp:TextBox ID="DisplayOrder" runat="server" MaxLength="9" Columns="5"></asp:TextBox>
            <asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" Display="Dynamic" ErrorMessage="* Enter a Display Order" ControlToValidate="DisplayOrder"></asp:requiredfieldvalidator>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="DisplayOrder" Display="Dynamic" 
            ErrorMessage="Enter a whole number." MaximumValue="999999999" MinimumValue="1" Type="Integer"></asp:RangeValidator>
        </div>
                
        <div>
	        <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
	        <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	    </div>	

</div> 
</asp:Content>

