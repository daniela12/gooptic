<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Secure_design_page_Default" Title="Untitled Page" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
	<table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td><h1>Manage Content Pages</h1></td>
			<td align="right"><asp:button CssClass="Button" id="btnAdd" CausesValidation="False" Text="Add New Page"
					Runat="server" onclick="btnAdd_Click"></asp:button></td>
		</tr>
	</table>

	<h4>Page List</h4>    
    <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" PageSize="25" OnRowDeleting="uxGrid_RowDeleting" AllowSorting="True" EmptyDataText="No content pages exist in the database.">
        <Columns>
            <asp:BoundField DataField="ContentPageID" HeaderText="ID" />
            <asp:BoundField DataField="Name" HeaderText="Page Name" />
            <asp:BoundField DataField="MasterPage" HeaderText="Page Template" />
            <asp:TemplateField HeaderText="Preview">
                <ItemTemplate>  
                    <asp:HyperLink NavigateUrl='<%# GetPageURL(DataBinder.Eval(Container.DataItem, "Name").ToString(),DataBinder.Eval(Container.DataItem, "SEOURL"))%>' Text="Preview" Target="_blank" runat="server"></asp:HyperLink>
                </ItemTemplate>                          
            </asp:TemplateField>
            <asp:ButtonField CommandName="Edit" Text="Edit" ButtonType="Button">
                <ControlStyle CssClass="Button" />
            </asp:ButtonField>           
            <asp:ButtonField CommandName="Revert" Text="View Revisions" ButtonType="Button">
                <ControlStyle CssClass="Button" />
            </asp:ButtonField>
            <asp:ButtonField CommandName="Delete" Text="Delete" ButtonType="Button">
                <ControlStyle CssClass="Button" />
            </asp:ButtonField>
        </Columns>
        <FooterStyle CssClass="FooterStyle"/>
        <RowStyle CssClass="RowStyle"/>                    
        <PagerStyle CssClass="PagerStyle" Font-Underline="True" />
        <HeaderStyle CssClass="HeaderStyle"/>
        <AlternatingRowStyle CssClass="AlternatingRowStyle"/>
        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
    </asp:GridView>	    
	<div><uc1:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></div>
</asp:Content>


