<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Admin_Secure_settings_msg_Add" Title="Untitled Page" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Src="~/Controls/HtmlTextBox.ascx" TagName="HtmlTextBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">    
    <uc2:DemoMode ID="DemoMode1" runat="server" />
    <div class="Form">
	    <h1>Custom Messages</h1>
    	<div><p>Custom messages are customizable content areas in the storefront (ex: "Customer Service Information"). You can edit this content using the WYSIWYG editor in this section.
    	    </p></div>

     	<div><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label></div>    	
        <h4>Available Custom Messages</h4>
        <asp:GridView ID="uxGrid" runat="server" PageSize="100" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnRowCommand="uxGrid_RowCommand" GridLines="None" CaptionAlign="Left" Width="100%" AllowSorting="True" EmptyDataText="No custom messages available.">
        <FooterStyle CssClass="FooterStyle"/>
        <RowStyle CssClass="RowStyle"/>                    
        <PagerStyle CssClass="PagerStyle" Font-Underline="True" />
        <HeaderStyle CssClass="HeaderStyle"/>
        <AlternatingRowStyle CssClass="AlternatingRowStyle"/>
        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
            <Columns>               
             <asp:BoundField DataField="MsgDescription" HeaderText="Message" />              
             <asp:TemplateField>
             <ItemTemplate>
             <asp:Button  ID="Button" runat="server" Text="Edit Message" CommandName="Edit" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"MsgKey") %>' CssClass="Button"/>
             </ItemTemplate>
             </asp:TemplateField>            
            </Columns>            
        </asp:GridView>
        </div>
</asp:Content>

