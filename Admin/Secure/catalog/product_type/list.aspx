<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="Admin_Secure_ProductTypes_search" Title="Untitled Page" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
<div class="Form">
	<table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td><h1>Product Types</h1></td>
			<td align="right"><asp:button CssClass="Button" id="btnAddProductType" CausesValidation="False" Text="Add New Product Type"
					Runat="server" onclick="btnAddProductType_Click"></asp:button></td>
		</tr>
	</table>
    <p>Use this page to manage Product Types (ex: Clothes, Shoes, etc). Product Types are internal classifications used to
    create logical product groupings. </p>
	<h4>Search Product Types</h4>
	<asp:Panel ID="Test" DefaultButton="btnSearch" runat="server" >  
        <table border="0" width="70%">
            <tr>                
                <td>
                    <div class="FieldStyle">Product Type</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtproductType" runat="server"></asp:TextBox></div>
                </td>
                <td>
                    <div class="FieldStyle">Description</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtDescription" runat="server"></asp:TextBox></div>
                </td>
            </tr>
            <tr>            
                <td colspan="3">
                    <div class="ValueStyle">
                        <asp:Button ID="btnSearch" runat="server" CssClass="Button" OnClick="btnSearch_Click" Text="Search" />
                        <asp:Button ID="btnClear" CausesValidation="false" runat="server" OnClick="btnClear_Click" Text="Clear Search" CssClass="Button" />            
                    </div> 
                </td>        
            </tr>
        </table>        
    </asp:Panel> 
    
    <h4>Product Type List</h4>
    <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" OnSorting="uxGrid_Sorting"  Width="100%" PageSize="25" OnRowDeleting="uxGrid_RowDeleting" AllowSorting="True" EmptyDataText="No Product Type exist in the database.">
        <Columns>
            <asp:BoundField DataField="producttypeid" HeaderText="ID" />
            <asp:TemplateField HeaderText="Product Type" SortExpression="Name">
                <ItemTemplate>
                    <a href='add.aspx?itemid=<%# DataBinder.Eval(Container.DataItem, "ProductTypeId").ToString()%>'><%# DataBinder.Eval(Container.DataItem, "Name").ToString()%></a>
                </ItemTemplate>                          
            </asp:TemplateField>
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="DisplayOrder" HeaderText="Display Order" />          
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID = "btnView" runat="server" CssClass="Button" CommandName="View" Text="Attributes" CommandArgument='<%# Eval("producttypeid") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID = "btnEdit" runat="server"  CssClass="Button" CommandName="Edit" Text="Edit" CommandArgument='<%# Eval("producttypeid") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID = "btnDelete" runat="server" CssClass="Button" CommandName="Delete" Text="Delete" CommandArgument='<%# Eval("producttypeid") %>' />
                </ItemTemplate>
            </asp:TemplateField>
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

