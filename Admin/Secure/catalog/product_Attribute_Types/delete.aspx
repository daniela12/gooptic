<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="delete.aspx.cs" Inherits="Admin_Secure_catalog_product_Attribute_Types_delete" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">

<h1>Please Confirm<uc1:DemoMode ID="DemoMode1" runat="server" />
</h1>
    <p>
        Please confirm if you want to
        delete this Product Attribute Type titled "<b><%=ProductAttributeTypeName%></b>". Note that you will be unable to delete this type if there are products currently associated with this attribute type.
        This change cannot be undone.
    </p>
    <p><asp:Label ID="lblErrorMsg" runat="server" Visible="False" CssClass="Error"></asp:Label></p>
    <asp:button CssClass="Button" id="btnCancel" CausesValidation="False" Text="Cancel" Runat="server" OnClick="btnCancel_Click" />
    <asp:button CssClass="Button" id="btnDelete" CausesValidation="False" Text="Delete" Runat="server" OnClick="btnDelete_Click" />
    <br /><br /><br />
 
</asp:Content>


