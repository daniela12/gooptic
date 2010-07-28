<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="SiteTheme.aspx.cs" Inherits="Admin_Secure_design_template_SiteTheme" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<asp:Content id="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
   <div class="Form">
     <h1>Site Theme</h1>
      Select one of the themes displayed below to change the theme of your site. If you wish to create a custom theme, you can create it using Visual Studio under the “Themes” folder.
   </div>
        <asp:Label ID="lblError" runat="server" CssClass="Error" Visible="False"></asp:Label>
        <asp:Label ID="lblmessage" runat="server" Text="" Font-Bold="true" ForeColor="green"></asp:Label><br /><br />
        <div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="themeslist" CssClass="Error"
            Display="Dynamic" ErrorMessage="* Select a theme" SetFocusOnError="True"></asp:RequiredFieldValidator>
        </div>
        <div class="ValueStyle">
            <asp:DropDownList  ID="themeslist" runat="server" EnableViewState="True" />
        </div><br />        
        <asp:Button CssClass="Button" ID="btnback" CausesValidation="false" Text="Back" runat="server" Visible="false" OnClick="btnback_click"/><br />
        <div>
            <asp:Button ID="btnSubmit" runat="server" CausesValidation="True" class="Button" OnClick="btnSubmit_Click" Text="SUBMIT" />&nbsp;                      
            <asp:Button ID="btncancel" runat="server" CausesValidation="true" class="Button" OnClick="btnCancel_Click" Text="Cancel" />
            <div>
                <uc1:Spacer ID="Spacer9" runat="server" SpacerHeight="10" SpacerWidth="1" />
            </div>
        </div>
</asp:Content>
