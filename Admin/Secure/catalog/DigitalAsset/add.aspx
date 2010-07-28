<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="Admin_Secure_catalog_DigitalAsset_add" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Src="~/Controls/HtmlTextBox.ascx" TagName="HtmlTextBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <div class="Form">
	    <table width="100%" cellSpacing="0" cellPadding="0" >
		    <tr>
			    <td><uc2:DemoMode ID="DemoMode1" runat="server" />
			    <h1><asp:Label ID="lblTitle" Text='Add a Digital Asset for Product - ' Runat="server"></asp:Label></h1></td>
			    <td align="right">
				    <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>
	    
	    <div><uc1:spacer id="Spacer2" SpacerHeight="15" SpacerWidth="3" runat="server"></uc1:spacer></div>
	    <div class="HintStyle">Use this page to add assignable digital assets to this product. Digital assets can include serial numbers or other digital information.</div>
	    
		<div><asp:Label ID="lblMsg" CssClass="Error" runat="server"></asp:Label></div>	    
	    <div><uc1:spacer id="Spacer8" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>
    
    	<h4>Digital Asset<span class="Asterix">*</span></h4>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDigitalAsset" Display="Dynamic" ErrorMessage="* Enter a digital asset information" CssClass="Error" SetFocusOnError="True"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle">
        <asp:TextBox ID="txtDigitalAsset" runat="server" columns="50" TextMode="MultiLine" rows="12"></asp:TextBox>
        
        <div><uc1:spacer id="Spacer1" SpacerHeight="15" SpacerWidth="3" runat="server"></uc1:spacer></div>
	    <div>
	        <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
	        <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	    </div>	
	</div>
</asp:Content>

