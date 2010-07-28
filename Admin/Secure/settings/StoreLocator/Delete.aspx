<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="Delete.aspx.cs" Inherits="Admin_Secure_settings_StoreLocator_Delete" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">    
    <h1><asp:Label ID="deletestore" runat="server">Delete Store</asp:Label>
        <uc1:DemoMode ID="DemoMode1" runat="server" />
    </h1>
    <p>Please confirm if you want to delete the Store "<b><%=Name%></b>".</p>
    <p><asp:Label ID="lblErrorMsg" runat="server" Visible="False" CssClass="Error"></asp:Label></p>
    <asp:button CssClass="Button" id="btnCancel" CausesValidation="False" Text="Cancel" Runat="server" OnClick="btnCancel_Click" />
    <asp:button CssClass="Button" id="btnDelete" CausesValidation="False" Text="Delete" Runat="server" OnClick="btnDelete_Click" />
    <br /><br /><br /> 
</asp:Content>

