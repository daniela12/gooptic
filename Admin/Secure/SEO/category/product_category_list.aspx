<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Themes/Standard/content.master" CodeFile="product_category_list.aspx.cs" Inherits="Admin_Secure_catalog_SEO_product_category_list" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
  <div class="SEO">
  <div class="Form">
    <table cellspacing="0" cellpadding="0" width="100%">
		<tr>
		<td><h1>Category SEO Settings</h1></td>		
	    </tr>
    </table>
    <p>Use this page to update various attributes of categories that are used by search engines such as Description, Title, Meta Tags and URL.</p>
    <br />
    <h4>Search Categories</h4>    
    <asp:Panel ID="Test" DefaultButton="btnSearch" runat="server" >  
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
    <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" OnSorting="uxGrid_Sorting" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" EnableSortingAndPagingCallbacks="False" PageSize="25" AllowSorting="True" EmptyDataText="No categories exist in the database.">
        <Columns>
            <asp:BoundField DataField="CategoryID" HeaderText="ID" />
            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                <ItemTemplate>
                    <a href='product_category_edit.aspx?itemid=<%# DataBinder.Eval(Container.DataItem, "CategoryId").ToString()%>'><%# GetRootCategory(DataBinder.Eval(Container.DataItem, "categoryid")) %></a>
                </ItemTemplate>                          
            </asp:TemplateField>            
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
        </Columns>
        <FooterStyle CssClass="FooterStyle"/>
        <RowStyle CssClass="RowStyle"/>                    
        <PagerStyle CssClass="PagerStyle" Font-Underline="True" />
        <HeaderStyle CssClass="HeaderStyle"/>
        <AlternatingRowStyle CssClass="AlternatingRowStyle"/>
        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
    </asp:GridView>	    
	<div><uc1:Spacer id="Spacer2" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:Spacer></div>
  </div>
  </div>
</asp:Content>