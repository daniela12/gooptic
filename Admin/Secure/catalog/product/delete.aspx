<%@ Page Language="C#"  MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="delete.aspx.cs" Inherits="Admin_Secure_products_delete" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
 <h1>Please Confirm<uc1:DemoMode id="DemoMode1" runat="server"></uc1:DemoMode></h1>
   <p>Please confirm if you want to delete the Product <b><%=ProductName%></b>. This change cannot be undone.</p>
    <asp:Label CssClass="Error" ID="lblErrorMessage" runat="server" Text=''></asp:Label>
    <div><asp:button CssClass="Button" id="btnDelete" CausesValidation="False" Text="Delete" Runat="server" OnClick="btnDelete_Click" />
      <asp:button CssClass="Button" id="btnCancel" CausesValidation="False" Text="Cancel" Runat="server" OnClick="btnCancel_Click" /></div>
    </asp:Content>


