<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="Admin_Secure_ProductTypes_add" Title="Untitled Page" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <div class="Form">
	    <table width="100%" cellSpacing="0" cellPadding="0" >
		    <tr>
			    <td><h1><asp:Label ID="lblTitle" Runat="server"></asp:Label>
                    <uc2:DemoMode id="DemoMode1" runat="server">
                    </uc2:DemoMode></h1></td>
			    <td align="right">
				    <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>
    
	    <div><uc1:spacer id="Spacer8" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
    	        
        <div class="FieldStyle">Product Type Name (ex: Apparel)<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Name" Display="Dynamic" ErrorMessage="* Enter a Product Type name" SetFocusOnError="True"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:TextBox ID='Name' runat='server' MaxLength="50" Columns="50"></asp:TextBox></div>
	    
	    <div class="FieldStyle">Display Order<span class="Asterix">*</span></div> 	                   
        <div class="ValueStyle">
            <asp:TextBox ID="DisplayOrder" runat="server" MaxLength="9" Columns="5"></asp:TextBox>
            <asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" Display="Dynamic" ErrorMessage="* Enter a Display Order" ControlToValidate="DisplayOrder"></asp:requiredfieldvalidator>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="DisplayOrder" Display="Dynamic" 
            ErrorMessage="Enter a whole number." MaximumValue="999999999" MinimumValue="1" Type="Integer"></asp:RangeValidator>
        </div>

        <div class="FieldStyle">Enter a Description</div>
        <div class="ValueStyle"><asp:TextBox ID="Description" runat="server" Height="172px" MaxLength="4000" TextMode="MultiLine" Width="420px"></asp:TextBox></div>
	    
	    <div><asp:Label ID="lblError" CssClass="Error" Runat="server"></asp:Label></div>
	    <div>
	        <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
	        <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	    </div>	
	</div>
</asp:Content>

