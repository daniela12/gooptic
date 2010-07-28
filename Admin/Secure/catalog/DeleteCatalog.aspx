<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="DeleteCatalog.aspx.cs" Inherits="Admin_Secure_Catalog_DeleteCatalog" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div class="Form">
        <h1>Delete Catalog Data - Please Confirm</h1>
        <ZNode:DemoMode id="DemoMode1" runat="server" />
        
        <div class="FieldStyle">Caution:</div> 
        <p>
        This action will delete all the catalog data from the database.
        This action cannot be reversed. Click on the confirm button to complete this action.
        </p>        
        <div>
            <asp:Label ID="lblErrorMessage" CssClass="Error" runat="server" Text=""></asp:Label><br /><br />
        </div>
        <div><asp:button CssClass="Button" id="btnDelete" CausesValidation="False" Text="Submit" Runat="server" OnClick="btnDelete_Click" />
        <asp:button CssClass="Button" id="btnCancel" CausesValidation="False" Text="Cancel" Runat="server" OnClick="btnCancel_Click" /></div>
    </div>
</asp:Content>


