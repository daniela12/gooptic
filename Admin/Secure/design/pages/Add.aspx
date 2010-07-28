<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Admin_Secure_design_Page_Add" Title="Untitled Page" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Src="~/Controls/HtmlTextBox.ascx" TagName="HtmlTextBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <div class="Form">
	    <table width="100%" cellspacing="0" cellpadding="0" >
		    <tr>
			    <td>
			        <h1><asp:Label ID="lblTitle" Runat="server"></asp:Label>
                    <uc2:DemoMode ID="DemoMode1" runat="server" />
                    </h1>
                </td>
			    <td align="right">
				    <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>
			    
	    <div><uc1:spacer id="Spacer8" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>
    	<div><asp:Label ID="lblMsg" runat="server" CssClass="Error"></asp:Label></div>
    	<div><uc1:spacer id="Spacer1" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>
    	
    	<h4>General Settings</h4>

        <div class="FieldStyle">Enter a  descriptive name for this page (ex: AboutUs)<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Enter a name for the page" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ErrorMessage="The page name can only contain letters, numbers, '_' and '-'." ControlToValidate="txtName" ValidationExpression="^[a-zA-Z0-9_-]+$"></asp:RegularExpressionValidator></div>
        <div class="ValueStyle"><asp:TextBox ID="txtName" runat="server" MaxLength="50" Columns="50"></asp:TextBox></div>        
        
        <div class="FieldStyle">Enter a title for this page (ex: "Welcome to my site")</div>
        <div class="ValueStyle"><asp:TextBox ID="txtTitle" runat="server" MaxLength="500" Columns="50"></asp:TextBox></div>
 
        <div class="FieldStyle">Page Template</div>
    	<div class="ValueStyle"><asp:DropDownList id="ddlPageTemplateList" runat="server"></asp:DropDownList></div>
    	
        <h4>SEO Settings</h4>
        
        <div class="FieldStyle">Enter a SEO title for the page</div>
        <div class="ValueStyle"><asp:TextBox ID="txtSEOTitle" runat="server" MaxLength="500" Columns="50"></asp:TextBox></div>
  
        <div class="FieldStyle">Enter Meta Keywords</div>
        <div class="ValueStyle"><asp:TextBox ID="txtSEOMetaKeywords" runat="server" MaxLength="500" Columns="50"></asp:TextBox></div>
  
        <div class="FieldStyle">Enter Meta Description</div>
        <div class="ValueStyle"><asp:TextBox ID="txtSEOMetaDescription" runat="server" MaxLength="500" Columns="50"></asp:TextBox></div>
        
        <asp:Panel ID="pnlSEOURL" runat="server">
            <div class="FieldStyle">Enter a SEO friendly name for this Page</div>
            <div class="HintStyle">Use only characters a-z and 0-9. Use "-" instead of spaces. Do not use a file extension or parameters in your product name.</div>
            <div class="ValueStyle">
            <asp:TextBox ID="txtSEOUrl" runat="server" MaxLength="100" Columns="50"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSEOUrl"
                    CssClass="Error" Display="Dynamic" ErrorMessage="Enter valid SEO firendly URL"
                    SetFocusOnError="True" ValidationExpression="([A-Za-z0-9-_]+)"></asp:RegularExpressionValidator>
            </div>
            
            <div class="FieldStyle"><asp:CheckBox ID="chkAddURLRedirect" runat="server" Text=' Add 301 redirect on URL changes.' /></div>
        </asp:Panel>
        
        <h4>Page Content</h4>
        <div class="ValueStyle"><uc1:HtmlTextBox id="ctrlHtmlText" runat="server"></uc1:HtmlTextBox></div>
	    
	    <div>
	        <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
	        <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	    </div>	
	</div>
</asp:Content>

