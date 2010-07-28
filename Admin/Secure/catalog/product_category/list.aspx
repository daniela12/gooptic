<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="Admin_Secure_categories_search" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
<div class="Form">
	<table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td><h1>Product Categories</h1></td>
			<td align="right"><asp:button CssClass="Button" id="btnAddCategory" CausesValidation="False" Text="Add New Category"
					Runat="server" onclick="btnAddCategory_Click"></asp:button></td>
		</tr>
	</table>
    <p>Use this page to manage your product categories (ex: Gifts, Clothing, Jewellery, etc). Categories allow you to create hierarchical grouping of products so customers can
    easily find what they are looking for.</p>
	<h4>Search Categories</h4>    
    <asp:Panel ID="Test" DefaultButton="btnSearch" runat="server">  
        <table border="0" width="70%">
            <tr>                
                <td>
                    <div class="FieldStyle">Name</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtCategoryName" runat="server"></asp:TextBox></div>
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
    <h4>Category List</h4> 
    <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" EnableSortingAndPagingCallbacks="False" PageSize="25" OnRowDeleting="uxGrid_RowDeleting" OnSorting="uxGrid_Sorting" AllowSorting="True" EmptyDataText="No categories exist in the database.">
        <Columns>
            <asp:BoundField DataField="CategoryID" HeaderText="ID" />
            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                <ItemTemplate>
                    <a href='add.aspx?itemid=<%# DataBinder.Eval(Container.DataItem, "CategoryId").ToString()%>'><%# GetRootCategory(DataBinder.Eval(Container.DataItem, "categoryid")) %></a>
                </ItemTemplate>                          
            </asp:TemplateField>
            <asp:BoundField DataField="DisplayOrder" HeaderText="DisplayOrder" /> 
               <asp:TemplateField HeaderText="Enabled">
                <ItemTemplate>                                
                    <img id="Img1" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "VisibleInd").ToString()))%>' runat="server" />
                </ItemTemplate>                          
            </asp:TemplateField>                                     
             <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID = "btnEdit" CssClass="Button"  runat="server"  CommandName="Edit" Text="Edit" CommandArgument='<%# Eval("CategoryID") %>' />
                </ItemTemplate>
             </asp:TemplateField>  
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID = "btnDelete" CssClass="Button"  runat="server"  CommandName="Delete" Text="Delete" CommandArgument='<%# Eval("CategoryID") %>' />
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

