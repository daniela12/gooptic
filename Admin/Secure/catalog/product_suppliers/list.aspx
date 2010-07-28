<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="Admin_Secure_catalog_product_suppliers_list" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="uxMainContent">
<div class="Form">
    <table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td><h1>Suppliers</h1></td>
			<td align="right" style="width: 371px"><asp:button CssClass="Button" id="btnAddSupplier" CausesValidation="False" Text="Add Supplier"
					Runat="server" onclick="btnAddSupplier_Click"></asp:button></td>
		</tr>
	</table>
	<p>Use this page to manage your suppliers. Products can be associated with a supplier who can then be notified when an order is placed for that product.</p>

    <h4>Search Suppliers</h4>   
    <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
        <table border="0" width="60%">
            <tr>
                <td>
                    <div class="FieldStyle">Name</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtName" runat="server" ValidationGroup="grpSearch"></asp:TextBox></div>                            
                </td>
                <td>
                    <div class="FieldStyle">Status</div>
                    <div class="ValueStyle">
                        <asp:DropDownlist ID="ddlSupplierStatus" runat="server">
                            <asp:ListItem Selected="True" Text="All" Value=""></asp:ListItem>                            
                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>                            
                        </asp:DropDownlist>
                    </div>
                 </td>    
           </tr>
           <tr>
                <td align="left" colspan="2">
                    <div class="ValueStyle">
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" CssClass="Button" ValidationGroup="grpSearch" />           
                    <asp:Button ID="btnClearSearch" runat="server" OnClick="btnClearSearch_Click" Text="Clear Search" CssClass="Button" CausesValidation="False" ValidationGroup="grpSearch" />
                    </div>        
                </td>        
           </tr>
        </table>
     </asp:Panel>
        
    <h4>Supplier List</h4>  
    <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" PageSize="25" OnRowDeleting="uxGrid_RowDeleting" AllowSorting="True" EmptyDataText="No supplier exist in the database.">
        <Columns>
            <asp:BoundField DataField="supplierid" HeaderText="ID" />            
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <a href='add.aspx?itemid=<%# DataBinder.Eval(Container.DataItem, "Supplierid").ToString()%>'><%# DataBinder.Eval(Container.DataItem, "Name").ToString()%></a>
                    </ItemTemplate> 
                </asp:TemplateField>   
                <asp:TemplateField HeaderText="Is Active?">
                    <ItemTemplate>                                
                        <img id="Img1" alt="" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "ActiveInd").ToString()))%>' runat="server" />
                    </ItemTemplate>                          
                </asp:TemplateField>                          
                <asp:ButtonField CommandName="Edit" Text="Edit" ButtonType="Button">
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
	 <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label> 
	 </div>
</asp:Content>
