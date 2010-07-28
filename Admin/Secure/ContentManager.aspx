<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="ContentManager.aspx.cs" Inherits="Admin_ContentManager" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <h1>Manage Storefront Design</h1>
    <p>Use the links on this page to manage your storefront's design including templates, styles and content</p>
    
    <div class="LandingPage">
        <hr />
         <div class="Shortcut"><a id="A1" href="~/admin/secure/design/template/sitetheme.aspx" runat="server">Site Theme</a></div>
        <p>The site theme controls the overall layout and color scheme of your storefront.</p> 
        
        <div class="Shortcut"><a id="A4" href="~/admin/secure/settings/messages/default.aspx" runat="server">Edit Storefront Messages</a></div>
        <p>Your storefront has several customizable message sections (ex: "Home page welcome message"). You can use a WYSIWYG editor to add rich content to these message sections.</p>
        
        
        <div class="Shortcut"><a id="A2" href="~/admin/secure/design/pages/default.aspx" runat="server">Manage Content Pages</a></div>
        <p>You can create or edit static content pages (ex: "About Us") in your storefront. You can add rich text, images, flash and other media
        to these pages using a WYSIWYG editor.</p>
        
                 
        <div class="Shortcut"><a id="A5" href="~/admin/secure/design/template/CSSEditor.aspx" runat="server">Edit CSS Style Sheets</a></div>
        <p>This function is for advanced users who are familiar with cascading style sheets (CSS). </p>       
        
    </div>
</asp:Content>

