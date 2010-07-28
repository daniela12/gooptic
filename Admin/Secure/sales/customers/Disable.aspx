<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="Disable.aspx.cs" Inherits="Admin_Secure_sales_customers_Disable" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <h1>Please Confirm<ZNode:DemoMode ID="DemoMode1" runat="server" />
    </h1>
    <p>Please confirm that you want to <strong>Disable</strong> this account.</p>
    <asp:Label ID="lblMsg" runat="server" Width="450px" CssClass="Error"></asp:Label><br /><br />
    <asp:button CssClass="Button" id="btnDelete" CausesValidation="False" Text="Disable Online Account" Runat="server" OnClick="btnDisable_Click" />
    <asp:button CssClass="Button" id="btnCancel" CausesValidation="False" Text="Cancel" Runat="server" OnClick="btnCancel_Click" />
    <br /><br /><br />
</asp:Content>
