<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true"  MasterPageFile="~/Admin/Themes/Standard/edit.master" CodeFile="product_edit.aspx.cs" Inherits="Admin_Secure_catalog_SEO_product_edit" %>
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
    <div>
        <h4>General Information</h4>
    
        <div class="FieldStyle">Product Name<span class="Asterix">*</span></div>

        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProductName" ErrorMessage="Enter Product Name" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator> </div>
        <div class="ValueStyle">
            <asp:TextBox ID="txtProductName" runat="server" Width="152px"></asp:TextBox>                       
        </div>
        
        <h4>SEO Settings</h4>
        <div>

            <div class="FieldStyle">Enter a Title for Search Engines</div>
            <div class="ValueStyle"><asp:TextBox ID="txtSEOTitle" runat="server" MaxLength="500" Columns="50"></asp:TextBox></div>

            <div class="FieldStyle">Enter Keywords for Search Engines</div>
            <div class="ValueStyle"><asp:TextBox ID="txtSEOMetaKeywords" runat="server" MaxLength="500" Columns="50"></asp:TextBox></div>

            <div class="FieldStyle">Enter Description for Search Engines</div>
            <div class="ValueStyle"><asp:TextBox ID="txtSEOMetaDescription" runat="server" MaxLength="500" Columns="50"></asp:TextBox></div>            
        
            <div class="FieldStyle">Enter a SEO friendly name for the Product URL</div>
            <div class="HintStyle">Use only characters a-z and 0-9. Use "-" instead of spaces. Do not use a file extension or parameters in your product name.</div>
            <div class="ValueStyle">
                <asp:TextBox ID="txtSEOUrl" runat="server" MaxLength="100" Columns="50"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSEOUrl"
                    CssClass="Error" Display="Dynamic" ErrorMessage="Enter a valid SEO friendly name"
                    SetFocusOnError="True" ValidationExpression="([A-Za-z0-9-_]+)"></asp:RegularExpressionValidator>
            </div>
            
            <div class="FieldStyle"><asp:CheckBox ID="chkAddURLRedirect" runat="server" Text=' Add 301 redirect on URL changes.' /></div>
        </div>
    
        <h4>Description</h4>    
        
        <div class="FieldStyle">Short Description</div>
 
        <div class="ValueStyle">
            <asp:TextBox ID="txtshortdescription" runat="server" Width="300px" TextMode="MultiLine" Height="75px" MaxLength="100"></asp:TextBox>
        </div>   
        
        <div class="FieldStyle">Long Description</div>
        <div class="ValueStyle"><ZNode:HtmlTextBox id="ctrlHtmlDescription" runat="server"></ZNode:HtmlTextBox></div>
        
        <h4>Enter Product Features</h4>
 
        <div class="ValueStyle"><ZNode:HtmlTextBox id="ctrlHtmlPrdFeatures" runat="server"></ZNode:HtmlTextBox></div>
        
        <h4>Enter Product Specifications</h4>

        <div class="ValueStyle"><ZNode:HtmlTextBox id="ctrlHtmlPrdSpec" runat="server"></ZNode:HtmlTextBox></div>
            
        <h4>Enter Additional Information</h4>

        <div class="ValueStyle"><ZNode:HtmlTextBox id="ctrlHtmlProdInfo" runat="server"></ZNode:HtmlTextBox></div>        
    </div>
    <div><uc2:Spacer id="Spacer8" SpacerHeight="20" SpacerWidth="3" runat="server"></uc2:spacer></div>
    <div>
        <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
        <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
    </div>
</div>
</asp:Content>