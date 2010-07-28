<%@ Page Language="C#" AutoEventWireup="true" CodeFile="key.aspx.cs" Inherits="admin_Secure_settings_ipcommerce_key" MasterPageFile="~/Admin/Themes/Standard/edit.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    
    <h1>Key Rotation - Confirmation Required</h1>
    <p>Please confirm if you want to rotate the IP Commerce Encryption key. You are required to rotate the key every 60 days</p>
    <br />
    <b>WARNING! </b>
    <p>You must backup your IP Commerce config files from the Data folder before performing this operation as it may result in potential data loss.</p>
    
    <div style="margin-top:20px;">
        <asp:button CssClass="Button" id="btnGenerate" CausesValidation="False" Text="Generate" Runat="server" OnClick="btnGenerate_Click" />
        <asp:button CssClass="Button" id="btnCancel" CausesValidation="False" Text="Cancel" Runat="server" OnClick="btnCancel_Click" />        
    </div> 
    <div style="margin-top:20px; margin-bottom:50px; font-weight:bold; color:#009900;">
        <asp:Label runat="server" ID="lblMsg"></asp:Label>    
    </div>    
    <a href="~/admin/Secure/Settings/Default.aspx?mode=ipcommerce" runat="server">Back to Advanced Tools page</a>
</asp:Content>
