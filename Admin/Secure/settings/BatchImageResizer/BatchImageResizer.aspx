<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" Inherits="Admin_Secure_settings_BatchImageResizer_BatchImageResizer" CodeFile="BatchImageResizer.aspx.cs"  Title="Untitled Page" validateRequest="false"   %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div class="Form">
        <h1>Batch Image Resizer</h1>        
        <p>Please click the submit button if you want to resize all the images in the “Original” folder to create multiple sizes – thumbnail, small, medium and large. Note that existing images in these folders will be overwritten and cannot be recovered!</p>
        <br />
        <b>WARNING! </b>
        <p>This function will overwrite any previously resized images in your catalog and can not be undone.</p>
        
        <div>
            <br /><asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="True" ForeColor="Green"></asp:Label><br /><br />
        </div>
        <div><asp:button CssClass="Button" id="btnsubmit" CausesValidation="False" Text="Submit" Runat="server" OnClick="btnsubmit_click" />
        <asp:button CssClass="Button" id="btnCancel" CausesValidation="False" Text="Cancel" Runat="server" OnClick="btncancel_click"  />
        <asp:Button CssClass="Button" ID="btnback" CausesValidation="false" Text="Back" runat="server" Visible="false" OnClick="btnback_click"/>
        </div>
    </div>
</asp:Content>



