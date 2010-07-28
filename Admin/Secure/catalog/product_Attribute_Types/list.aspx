<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="Admin_Secure_catalog_product_Attribute_Types_list" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
<div class="Form">
    <table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td><h1>Product Attributes</h1></td>
			<td align="right">
			    <asp:button CssClass="Button" id="btnAddAttributeTypes" CausesValidation="False" Text="Add Attribute Type" runat="server" onclick="btnAdd_Click"></asp:button>
            </td>
		</tr>
	</table>
    <p>Use this page to manage product attributes (ex: Color, Size, etc). You must first create an attribute (ex: Color) and then add multiple values
    to that attribute (for example: "red", "blue", etc). You can then associate that attribute with a specific product type.</p>
	
	<h4>Search Attributes</h4>
    <asp:Panel ID="Test" DefaultButton="btnSearch" runat="server" >  
        <table border="0" width="70%">
            <tr>                
                <td>
                    <div class="FieldStyle">Name</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtAttributeName" runat="server"></asp:TextBox></div>
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
    
    <h4>Attribute List</h4>
    <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4"  GridLines="None" OnSorting="uxGrid_Sorting" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" EnableSortingAndPagingCallbacks="False" PageSize="25" AllowSorting="True" EmptyDataText="No Product Attributes exist in the database.">
        <Columns>
            <asp:BoundField DataField="AttributeTypeId" HeaderText="ID" />
            <asp:BoundField SortExpression="Name" DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="DisplayOrder" HeaderText="Display Order" />               
             <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID = "btnView" CssClass="Button"  runat="server"  CommandName="View" Text="View" ButtonType="Button" CommandArgument='<%# Eval("AttributeTypeId") %>' />
                </ItemTemplate>
             </asp:TemplateField>     
             <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID = "btnEdit" CssClass="Button"  runat="server"  CommandName="Edit" Text="Edit" ButtonType="Button" CommandArgument='<%# Eval("AttributeTypeId") %>' />
                </ItemTemplate>
             </asp:TemplateField>                      
             <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID = "btndelete" CssClass="Button"  runat="server"  CommandName="Delete" Text="Delete" ButtonType="Button" CommandArgument='<%# Eval("AttributeTypeId") %>' />
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


