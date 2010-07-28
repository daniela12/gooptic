<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="delete.aspx.cs" Inherits="Admin_Secure_catalog_Promotions_delete" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc1" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <uc1:DemoMode ID="DemoMode1" runat="server" />
    <h1><asp:Label ID="deletecoupon" runat="server">Delete Promotion - <b><%=PromotionName%></b></asp:Label></h1>
    <div><uc1:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
    <p>Please confirm if you want to delete this promotion.</p>    
    <p><asp:Label ID="lblErrorMsg" runat="server" Visible="False" CssClass="Error"></asp:Label></p>    
    <div><uc1:spacer id="Spacer1" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
    <asp:button CssClass="Button" id="btnCancel" CausesValidation="False" Text="Cancel" Runat="server" OnClick="btnCancel_Click" />
    <asp:button CssClass="Button" id="btnDelete" CausesValidation="False" Text="Delete" Runat="server" OnClick="btnDelete_Click" />
    
    <div><uc1:spacer id="Spacer8" SpacerHeight="15" SpacerWidth="3" runat="server"></uc1:spacer></div>
</asp:Content>
