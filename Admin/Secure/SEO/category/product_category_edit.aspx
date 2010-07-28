<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="product_category_edit.aspx.cs" Inherits="Admin_Secure_catalog_SEO_product_category_edit" %>
<%@ Register TagPrefix="uc2" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Src="~/Controls/HtmlTextBox.ascx" TagName="HtmlTextBox" TagPrefix="ZNode" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
<div class="Form">
    <table cellpadding="0" cellspacing="0" width="100%">
      <tr>     
        <td valign="middle">
            <h1><asp:Label ID="lblTitle" runat="server"></asp:Label></h1>
        </td>
        <td align="right">
            <asp:Button ID="btnSubmitTop" runat="server" CausesValidation="True" class="Button" OnClick="btnSubmit_Click" Text="SUBMIT" />
            <asp:Button ID="btnCancelTop" runat="server" CausesValidation="False" class="Button" OnClick="btnCancel_Click" Text="CANCEL" />
        </td>
      </tr>        
    </table>
    <asp:Label ID="lblError" CssClass= "Error" runat="server"></asp:Label>
    
    <h4>General Settings</h4>
    <div class="FieldStyle">Category Title</div>
    <div class="ValueStyle"><asp:TextBox ID='txtTitle' runat='server' MaxLength="255" Columns="50"></asp:TextBox></div>
    
    <h4>SEO Settings</h4>
        
    <div class="FieldStyle">Enter a Title for Search Engines</div>
    <div class="ValueStyle"><asp:TextBox ID="txtSEOTitle" runat="server" MaxLength="500" Columns="50"></asp:TextBox></div>
  
    <div class="FieldStyle">Enter Keywords for Search Engines</div>
    <div class="ValueStyle"><asp:TextBox ID="txtSEOMetaKeywords" runat="server" MaxLength="500" Columns="50"></asp:TextBox></div>
  
    <div class="FieldStyle">Enter Description for Search Engines</div>
    <div class="ValueStyle"><asp:TextBox ID="txtSEOMetaDescription" runat="server" MaxLength="500" Columns="50"></asp:TextBox></div>
               
    <div class="FieldStyle">Enter a SEO friendly name for the Category URL</div>
    <div class="HintStyle">Use only characters a-z and 0-9. Use "-" instead of spaces. Do not use a file extension or parameters in your url name.</div>
    <div class="ValueStyle"><asp:TextBox ID="txtSEOURL" runat="server" MaxLength="100" Columns="50"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSEOURL" CssClass="Error" Display="Dynamic" ErrorMessage="Enter valid SEO firendly URL"
                    SetFocusOnError="True" ValidationExpression="([A-Za-z0-9-_]+)"></asp:RegularExpressionValidator>
    </div>
    
    <div class="FieldStyle"><asp:CheckBox ID="chkAddURLRedirect" runat="server" Text=' Add 301 redirect on URL changes.' /></div>
    
    <h4>Description</h4>
    <div class="FieldStyle">Short Description</div>
 
    <div class="ValueStyle">
    <asp:TextBox ID="txtshortdescription" runat="server" Width="300px" TextMode="MultiLine" Height="75px" MaxLength="100"></asp:TextBox>
    </div>           
    <div class="FieldStyle">Long Description<span class="Asterix">*</span></div>

    <div class="ValueStyle"><ZNode:HtmlTextBox id="ctrlHtmlText" runat="server"></ZNode:HtmlTextBox></div> 
	<div>
	   <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
	   <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	</div>        
</div>
</asp:Content>