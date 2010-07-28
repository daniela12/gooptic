<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="inventory.aspx.cs" Inherits="Admin_Secure_products_inventory" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div><h1>Inventory for "<asp:Label ID="lblProdName" runat="server"></asp:Label>"</h1></div>
    <div align="right">
        <asp:Button ID="ProductList" runat="server" CssClass="Button" Text="< Back" OnClick="ProductList_Click" />&nbsp;&nbsp;<asp:Button CssClass="Button" ID="butAddNewSKU" Text="Add SKU or Part#" runat="server" OnClick="AddSKU_Click" /> <br /><br />
        
    </div>
    <div align="left">
     <asp:Label ID="lblmessage" runat="server"></asp:Label><br /><br />
    </div>
 
	<table border="0" cellpadding="0" cellspacing="0"width="100%">
	
	<tr>
	<td>
       <asp:GridView ID="uxGridInventoryDisplay" Width="100%" CssClass="Grid" CellPadding="4" CaptionAlign="Left" GridLines="None" runat="server" AutoGenerateColumns="False" AllowPaging="True" ForeColor="Black" OnPageIndexChanging="uxGridInventoryDisplay_PageIndexChanging" OnRowCommand="uxGridInventoryDisplay_RowCommand" OnRowDeleting="uxGridInventoryDisplay_RowDeleting">
           <FooterStyle CssClass="FooterStyle" />
           <Columns>
               <asp:BoundField DataField="sku" HeaderText="SKU or Part#" />
               <asp:BoundField DataField="quantityonhand" HeaderText="QuantityOnHand" />
               <asp:TemplateField>
                        <ItemTemplate>
                            <div id="Test" runat="server" >
                                <asp:Button CssClass="Button" ID="EditSKU" Text="Edit" CommandArgument='<%# Eval("skuid") %>' CommandName="Edit" runat="Server" />
                            </div>
                        </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField>
                        <ItemTemplate>
                                <asp:Button ID="RemoveSKU"  CssClass="Button" Text="Remove" CommandArgument='<%# Eval("skuid") %>' CommandName="Delete" runat="Server" />
                        </ItemTemplate>
               </asp:TemplateField>
           </Columns>
           <RowStyle CssClass="RowStyle" />
           <EditRowStyle CssClass="EditRowStyle" />
           <PagerStyle CssClass="PagerStyle" />
           <HeaderStyle CssClass="HeaderStyle" />
           <AlternatingRowStyle CssClass="AlternatingRowStyle" />
       </asp:GridView>
		
	</td>
	</tr>
	</table>
	
    <br /><br /><br /><br />
</asp:Content>


