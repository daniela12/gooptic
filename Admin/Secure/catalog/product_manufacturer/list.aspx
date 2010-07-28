<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master"  AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="Admin_Secure_catalog_product_manufacturer_list" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
<div class="Form">
<table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td><h1>Brands</h1></td>
			<td align="right" style="width: 371px"><asp:button CssClass="Button" id="btnAddManufacturer" CausesValidation="False" Text="Add Brand"
					Runat="server" onclick="btnAddManufacturer_Click"></asp:button></td>
		</tr>
	</table>
	<p>Use this page to manage the different brands in your database. You can then associate products with a particular brand.
	</p>
    <h4>Search Brands</h4>  
    <asp:Panel ID="Test" DefaultButton="btnSearch" runat="server" >  
        <table border="0" width="70%">
            <tr>                
                <td>
                    <div class="FieldStyle">Name</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtManufacturerName" runat="server"></asp:TextBox></div>
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
    
    <h4>Brands List</h4>  
      <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" PageSize="25" OnRowDeleting="uxGrid_RowDeleting" OnSorting="uxGrid_Sorting" AllowSorting="True" EmptyDataText="No Manufacturer exist in the database.">
        <Columns>
            <asp:BoundField DataField="manufacturerid" HeaderText="ID" />            
            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                <ItemTemplate>
                    <a href='add.aspx?itemid=<%# DataBinder.Eval(Container.DataItem, "Manufacturerid").ToString()%>'><%# DataBinder.Eval(Container.DataItem, "Name").ToString()%></a>
                </ItemTemplate> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Is Active">
                  <ItemTemplate>                                
                    <img id="Img1" alt="" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "ActiveInd").ToString()))%>' runat="server" />
                </ItemTemplate>                          
            </asp:TemplateField>     
             <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID = "btnEdit" CssClass="Button"  runat="server"  CommandName="Edit" Text="Edit" CommandArgument='<%# Eval("manufacturerid") %>' />
                </ItemTemplate>
             </asp:TemplateField>  
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID = "btnDelete" CssClass="Button"  runat="server"  CommandName="Delete" Text="Delete" CommandArgument='<%# Eval("manufacturerid") %>' />
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
	 <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label> 
</div>
</asp:Content>


