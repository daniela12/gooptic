<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="Delete.aspx.cs" Inherits="Admin_Secure_sales_customers_Delete" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <h1>Please Confirm<ZNode:DemoMode ID="DemoMode1" runat="server" />
    </h1>
    <p>Please confirm if you want to delete this account. Note that this change cannot be undone.</p>
    <asp:Label ID="lblMsg" runat="server" Width="450px" CssClass="Error"></asp:Label><br /><br />
    <asp:button CssClass="Button" id="btnDelete" CausesValidation="False" Text="Delete" Runat="server" OnClick="btnDelete_Click" />
    <asp:button CssClass="Button" id="btnCancel" CausesValidation="False" Text="Cancel" Runat="server" OnClick="btnCancel_Click" />
    <br /><br /><br />
</asp:Content>