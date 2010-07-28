<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="edit_additionalInfo.aspx.cs" Inherits="Admin_Secure_catalog_product_edit_additionalInfo"  ValidateRequest="false" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/HtmlTextBox.ascx" TagName="HtmlTextBox" TagPrefix="ZNode" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div class="Form">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <h1><asp:label ID="lblTitle" runat="server" Text="Edit Additional Info for" ></asp:label></h1>
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
        <asp:Label ID="lblError" CssClass= "Error" runat="server"></asp:Label>
        <div>
            <p>Use this page to edit product features,specifications for this product.</p>

            <h4>Enter Product Features</h4>
            <p class="HintStyle">Add product features here to have a "Features" tab be displayed on the product page <br />(you must use the StoreTabs.master template with this feature).</p>
            <div class="ValueStyle"><ZNode:HtmlTextBox id="ctrlHtmlPrdFeatures" runat="server"></ZNode:HtmlTextBox></div>
        
            <h4>Enter Product Specifications</h4>
            <p class="HintStyle">Add product specifications here to have a "Specifications" tab be displayed on the product page <br />(you must use the StoreTabs.master template with this feature).</p>
            <div class="ValueStyle"><ZNode:HtmlTextBox id="ctrlHtmlPrdSpec" runat="server"></ZNode:HtmlTextBox></div>
            
            <h4>Enter Shipping Information</h4>
            <p class="HintStyle">Add additional product information here to have a "Shipping Information" tab be displayed on the product page <br />(you must use the StoreTabs.master template with this feature).</p>
            <div class="ValueStyle"><ZNode:HtmlTextBox id="CtrlHtmlProdInfo" runat="server"></ZNode:HtmlTextBox></div>
            
        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" CausesValidation="True" class="Button"
                OnClick="btnSubmit_Click" Text="SUBMIT" />
            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" class="Button"
                OnClick="btnCancel_Click" Text="CANCEL" />
        </div>
    </div>
</asp:Content>

