<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="Delete.aspx.cs" Inherits="Admin_Secure_settings_ship_Delete" Title="Untitled Page" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <h1>Please Confirm<uc1:DemoMode ID="DemoMode1" runat="server" />
    </h1>
    <p>Please confirm if you want to delete the shipping option named <b>"<% = ShippingOptionName %>"</b>. Note that this change cannot be undone.</p>
    <div><asp:Label ID="lblMsg" runat="server" Width="450px" CssClass="Error"></asp:Label><br /><br /></div>
    <asp:button CssClass="Button" id="btnCancel" CausesValidation="False" Text="Cancel" Runat="server" OnClick="btnCancel_Click" />
    <asp:button CssClass="Button" id="btnDelete" CausesValidation="False" Text="Delete" Runat="server" OnClick="btnDelete_Click" />
    <br /><br /><br />
</asp:Content>

