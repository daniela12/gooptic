<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="edit_seo.aspx.cs" Inherits="Admin_Secure_catalog_product_edit_seo" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc1" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div class="Form">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <h1><asp:label ID="lblTitle" runat="server" Text="Edit SEO for" ></asp:label></h1>
                        <uc1:DemoMode id="DemoMode1" runat="server"></uc1:DemoMode>
                </td>
                <td align="right">
                    <asp:Button ID="btnSubmitTop" runat="server" CausesValidation="True" class="Button"
                        OnClick="btnSubmit_Click" Text="SUBMIT" />
                    <asp:Button ID="btnCancelTop" runat="server" CausesValidation="False" class="Button"
                        OnClick="btnCancel_Click" Text="CANCEL" />
                </td>
            </tr>
        </table>
        <div><ZNode:spacer id="Spacer1" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>
        <div><asp:Label ID="lblError" CssClass= "Error" runat="server"></asp:Label></div>
        <div><ZNode:spacer id="Spacer2" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>
        <div>
            <p>These settings are meant for Search Engine Optimization. Leave this section blank if unsure.</p>

            <div class="FieldStyle">Enter a title for Search Engines</div>
            <div class="ValueStyle"><asp:TextBox ID="txtSEOTitle" runat="server" MaxLength="500" Columns="50"></asp:TextBox></div>

            <div class="FieldStyle">Enter Keywords for Search Engines</div>
            <div class="ValueStyle"><asp:TextBox ID="txtSEOMetaKeywords" runat="server" MaxLength="500" Columns="50"></asp:TextBox></div>

            <div class="FieldStyle">Enter Description for Search Engines</div>
            <div class="ValueStyle"><asp:TextBox ID="txtSEOMetaDescription" runat="server" MaxLength="500" Columns="50"></asp:TextBox></div>            
        
            <div class="FieldStyle">Enter a SEO friendly name for this Product</div>
            <div class="HintStyle">Use only characters a-z and 0-9. Use "-" instead of spaces. Do not use a file extension or parameters in your product name.</div>
            <div class="ValueStyle">
                <asp:TextBox ID="txtSEOUrl" runat="server" MaxLength="100" Columns="50"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSEOUrl"
                    CssClass="Error" Display="Dynamic" ErrorMessage="Enter a valid SEO friendly name"
                    SetFocusOnError="True" ValidationExpression="([A-Za-z0-9-_]+)"></asp:RegularExpressionValidator>
            </div>
            
            <div class="FieldStyle"><asp:CheckBox ID="chkAddURLRedirect" runat="server" Text=' Add 301 redirect on URL changes.' /></div>
        </div>
        <div><ZNode:spacer id="Spacer3" SpacerHeight="20" SpacerWidth="3" runat="server"></ZNode:spacer></div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" CausesValidation="True" class="Button"
                OnClick="btnSubmit_Click" Text="SUBMIT" />
            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" class="Button"
                OnClick="btnCancel_Click" Text="CANCEL" />
        </div>
    </div>    
</asp:Content>
