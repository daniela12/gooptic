<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Themes/Standard/content.master" CodeFile="pages_list.aspx.cs" Inherits="Admin_Secure_catalog_SEO_pages_list" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
<div class="SEO">
	<table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td><h1>Content Page SEO Settings</h1></td>		
		</tr>
	</table>
    <p>
    Using this section you can manage the SEO settings for your static content pages (ex: "About Us").
    </p>
	<h4>Page List</h4>    
    <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" PageSize="25" AllowSorting="True" EmptyDataText="No content pages exist in the database.">
        <Columns>
            <asp:BoundField DataField="ContentPageID" HeaderText="ID" />
            <asp:BoundField DataField="Name" HeaderText="Page Name" />
            <asp:TemplateField HeaderText="Preview">
                <ItemTemplate>  
                    <asp:HyperLink NavigateUrl='<%# GetPageURL(DataBinder.Eval(Container.DataItem, "Name").ToString(),DataBinder.Eval(Container.DataItem, "SEOURL"))%>' Text="Preview" Target="_blank" runat="server"></asp:HyperLink>
                </ItemTemplate>                          
            </asp:TemplateField>              
            <asp:ButtonField CommandName="Edit" Text="Edit" ButtonType="Button">
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
</div>
</asp:Content>


