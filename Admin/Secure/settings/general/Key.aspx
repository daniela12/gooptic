<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Key.aspx.cs" Inherits="Admin_Secure_settings_general_Key" MasterPageFile="~/Admin/Themes/Standard/edit.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    
    <h1>Key Rotation - Confirmation Required</h1>
    <p>Please confirm if you want to generate a new encryption key for this storefront. You should create a new encryption key
    instead of using the key that is shipped out of the box with Znode Storefront for enhanced security.</p>
    <br />
    <b>CAUTION!</b>
    <p>Generating a new key will make previously encrypted data un-readable (for ex: Merchant Gateway Settings, SMTP Settings, etc). Key generation should only be done for a new storefront and should never be attempted on a storefront running in production.</p>
    <p>Key generation will not impact customer or product data.</p>
    <div style="margin-top:20px;">
        <asp:button CssClass="Button" id="btnGenerate" CausesValidation="False" Text="Generate" Runat="server" OnClick="btnGenerate_Click" />
        <asp:button CssClass="Button" id="btnCancel" CausesValidation="False" Text="Cancel" Runat="server" OnClick="btnCancel_Click" />        
    </div> 
    <div style="margin-top:20px; margin-bottom:50px; font-weight:bold; color:#009900;">
        <asp:Label runat="server" ID="lblMsg"></asp:Label>    
    </div>    
    <a href="~/admin/secure/Settings/default.aspx" runat="server" visible="true">Back to Advanced Tools page</a>
</asp:Content>
