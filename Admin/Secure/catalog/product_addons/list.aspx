<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="Admin_Secure_catalog_product_addons_list" Title="Untitled Page" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
<div class="Form">
	<table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td><h1>Product Add-Ons</h1></td>
			<td align="right"><asp:button CssClass="Button" id="btnAddCategory" CausesValidation="False" Text="Create New Add-On"
					Runat="server" onclick="btnAddCategory_Click"></asp:button></td>
		</tr>
	</table>
    <p>Use this page to manage the master list of Product Add-Ons in your catalog (ex: "Color") and their corresponding values.
    When you add a new product, you can then associate these add-ons with your product.
    </p>
    
	<h4>Search Add-Ons</h4>
	<asp:Panel ID="Test" DefaultButton="btnSearch" runat="server" >  
        <table border="0" width="70%">
            <tr>                
                <td>
                    <div class="FieldStyle"> Name</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtAddonName" runat="server"></asp:TextBox></div>
                </td>
                <td>
                    <div class="FieldStyle">Title</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtAddOnTitle" runat="server"></asp:TextBox></div>
                </td>                              
                <td>
                    <div class="FieldStyle">SKU or Part#</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtsku" runat="server"></asp:TextBox></div>
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
    
    <h4>Product Add-On List</h4>    
    <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" EnableSortingAndPagingCallbacks="False" PageSize="25" AllowSorting="True" EmptyDataText="No product Add-Ons exist in the database.">
        <Columns>
            <asp:BoundField DataField="AddOnId" HeaderText="ID" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <a href='view.aspx?itemid=<%# DataBinder.Eval(Container.DataItem, "AddOnId").ToString()%>'><%# Eval("Name") %></a>
                </ItemTemplate>                          
            </asp:TemplateField>
            <asp:BoundField DataField="DisplayOrder" HeaderText="DisplayOrder" />
            <asp:TemplateField HeaderText="Is Optional?">
                <ItemTemplate>                                
                    <img alt="" id="Img1" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "OptionalInd").ToString()))%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:ButtonField CommandName="Manage" Text="Manage" ButtonType="Button">
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
</div>
</asp:Content>

