<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginStatus.ascx.cs" Inherits="Controls_LoginStatus" %>
<asp:LoginStatus ID="uxUserLoginStatus" runat="server" LoginText="Login" OnLoggingOut="uxUserLoginStatus_LoggingOut" OnLoggedOut="uxUserLoginStatus_LoggedOut" />