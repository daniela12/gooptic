<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="AdvanceToolsManager.aspx.cs" Inherits="Admin_Secure_AdvanceToolsManager" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
 <h1>Advanced Tools</h1>
<p>Use the links below to add custom rulesets to the storefront rules engine. This is advanced functionality intended for experienced developers.</p>
    
    <div class="LandingPage">
        <hr />
        
        <div class="Shortcut"><span class="Icon"><a id="A1" runat="server" href="~/admin/secure/settings/ruleTypes/taxRuleList.aspx">Tax Rule Engine</a></div>
        <p></p>
        

        <div class="Shortcut"><span class="Icon"><a id="A4" runat="server" href="~/admin/secure/settings/ruletypes/list.aspx">Promotion Rule Engine</a></div>
        <p></p>
        

        <div class="Shortcut"><span class="Icon"><a id="A6" runat="server" href="~/admin/secure/settings/ruletypes/default.aspx">Shipping Rule Engine</a></div>
        <p></p>
        

        <div class="Shortcut"><span class="Icon"><a id="A7" href="~/admin/secure/settings/ruletypes/SupplierTypeList.aspx" runat="server">Supplier Rule Engine</a></div>
        <p></p>
        
       </div>
</asp:Content>

