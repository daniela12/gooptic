<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="Delete.aspx.cs" Inherits="Admin_Secure_settings_Profile_Delete" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc1" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <h1>Please Confirm<uc1:DemoMode id="DemoMode1" runat="server"></uc1:DemoMode></h1>
    
    <p>Please confirm if you want to delete the Profile <b><%=ProfileName%></b>. This change cannot be undone.</p>
    <div><asp:Label ID="lblErrorMessage" CssClass="Error" runat="server" Text=''></asp:Label></div>
    <div><uc1:spacer id="Spacer8" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
    <div><asp:button CssClass="Button" id="btnDelete" CausesValidation="False" Text="Delete" Runat="server" OnClick="btnDelete_Click" />
    <asp:button CssClass="Button" id="btnCancel" CausesValidation="False" Text="Cancel" Runat="server" OnClick="btnCancel_Click" /></div>
</asp:Content>

