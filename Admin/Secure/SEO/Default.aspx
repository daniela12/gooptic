<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Secure_settings_SEO_Default" Title="Untitled Page" ValidateRequest="false" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
<div class="Form">
    <div>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td valign="middle">
                    <h1>Default SEO Settings</h1>
                </td>
                <td align="right">
                    <asp:Button ID="btnSubmitTop" runat="server" CausesValidation="True" class="Button"
                        OnClick="btnSubmit_Click" Text="SUBMIT" />
                    <asp:Button ID="btnCancelTop" runat="server" CausesValidation="False" class="Button"
                        OnClick="btnCancel_Click" Text="CANCEL" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div><asp:Label ID="lblMsg" runat="server" CssClass="Error"></asp:Label></div>
                </td>
            </tr>
         </table>
    </div> 
    <div><ZNode:spacer id="Spacer8" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>
        <div>Set the default meta tag values that will be used on each page. These settings can be overridden in the SEO section of the Product, Category and Content admin pages.</div><br />
        <div>You can add substitution variables in your default meta tag text that will be substituted with the appropriate values at runtime when the page is displayed. For example " Shop now at mystore.com for &lt;NAME&gt; " will have the Product Name substituted into the meta tag when the product page is displayed.</div><br />
        <div><b>Substitution Variables:</b><br />
           &lt;NAME&gt; - Will substitute the Product Name, Category Name or Content Page title.<br />
           &lt;PRODUCT_NUM&gt; - Will substitute the Product Number (only for Product Pages).<br />
           &lt;SKU&gt; - Will substitute the Product SKU (only for Product Pages).<br />
           &lt;MANUFACTURER&gt; - Will substitute the Product Manufacturer (only for Product Pages).<br />
        </div>
        <h4>SEO Product Settings</h4>
        <div class="FieldStyle">SEO Product Title</div>
        <div class="ValueStyle"><asp:TextBox ID="txtSEOProductTitle" runat="server" Width="152px"></asp:TextBox></div>

        <div class="FieldStyle">SEO Product Description</div>
        <div class="ValueStyle"><asp:TextBox ID="txtSEOProductDescription" runat="server" Width="450px" TextMode="MultiLine" Height="150px"></asp:TextBox></div>

        <div class="FieldStyle">SEO Product Keyword</div>
        <div class="ValueStyle"><asp:TextBox ID="txtSEOProductKeyword" runat="server" Width="152px"></asp:TextBox></div>
        
        <h4>SEO Category Settings</h4>
        <div class="FieldStyle">SEO Category Title</div>
        <div class="ValueStyle"><asp:TextBox ID="txtSEOCategoryTitle" runat="server" Width="152px"></asp:TextBox></div>

        <div class="FieldStyle">SEO Category Description</div>
        <div class="ValueStyle"><asp:TextBox ID="txtSEOCategoryDescription" runat="server" Width="450px" TextMode="MultiLine" Height="150px"></asp:TextBox></div>

        <div class="FieldStyle">SEO Category Keyword</div>
        <div class="ValueStyle"><asp:TextBox ID="txtSEOCategoryKeyword" runat="server" Width="152px"></asp:TextBox></div>
        
        <h4>SEO Content Settings</h4>
        <div class="FieldStyle">SEO Content Title</div>
        <div class="ValueStyle"><asp:TextBox ID="txtSEOContentTitle" runat="server" Width="152px"></asp:TextBox></div>

        <div class="FieldStyle">SEO Content Description</div>
        <div class="ValueStyle"><asp:TextBox ID="txtSEOContentDescription" runat="server" Width="450px" TextMode="MultiLine" Height="150px"></asp:TextBox></div>

        <div class="FieldStyle">SEO Content Keyword</div>
        <div class="ValueStyle"><asp:TextBox ID="txtSEOContentKeyword" runat="server" Width="152px"></asp:TextBox></div>
   
   
    <div><ZNode:spacer id="Spacer1" SpacerHeight="15" SpacerWidth="3" runat="server"></ZNode:spacer></div>
    
    <div>
        <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
        <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
    </div>	
</div> 
</asp:Content>

