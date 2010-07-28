<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master"  AutoEventWireup="true" CodeFile="view.aspx.cs" Inherits="Admin_Secure_catalog_product_addons_view" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div class="Form">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <h1>Product Add-On : <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label></h1>
                </td>
                <td align="right">                        
                        <asp:Button ID="btneditAddon" runat="server" CssClass="Button" OnClick="EditAddOn_Click" Text="Edit Add-On" />        
                </td>
            </tr>
        </table>
        
        <h4>General Information</h4>
        <table cellspacing="0" cellpadding="0" class="ViewForm" width="100%">
	            <tr class="RowStyle">
	                <td class="FieldStyle" nowrap="nowrap">Name</td>
	                <td class="ValueStyle" width="100%">
	                    &nbsp;<asp:Label ID="lblName" runat="server"></asp:Label>
	                </td>
	            </tr>
	            <tr class="AlternatingRowStyle">
	                <td class="FieldStyle" nowrap="nowrap">Title</td>
	                <td class="ValueStyle" width="100%">
	                    <asp:Label ID="lblAddOnTitle" runat="server"></asp:Label>
	                </td>
	            </tr>
	            <tr class="RowStyle">
	                <td class="FieldStyle" nowrap="nowrap">Display Order</td>
	                <td class="ValueStyle"><asp:Label ID="lblDisplayOrder" runat="server"></asp:Label></td>
	            </tr>
	            <tr class="AlternatingRowStyle">
	                <td class="FieldStyle" nowrap="nowrap">Display Type</td>
	                <td class="ValueStyle"><asp:Label ID="lblDisplayType" runat="server"></asp:Label></td>
	            </tr>
	            <tr class="RowStyle">
	                <td class="FieldStyle" nowrap="nowrap">Optional</td>
	                <td class="ValueStyle"><img id="chkOptionalInd" runat="server" alt="" src=""/></td>
	            </tr>
	   </table>
	   
	   <h4>Inventory Settings</h4>
	   <table cellspacing="0" cellpadding="0" class="ViewForm" width="100%">
	            <tr class="RowStyle">
	                <td class="FieldStyle" nowrap="nowrap"><img id="chkCartInventoryEnabled" runat="server" alt="" src=''/></td>
	                <td class="ValueStyle" width="100%">
	                   Only Sell if Inventory Available (User can only add to cart if inventory is above 0)
	                </td>
	            </tr>
	            <tr class="AlternatingRowStyle">
	                <td class="FieldStyle" nowrap="nowrap"><img id="chkIsBackOrderEnabled" runat="server" alt="" src=''/></td>
	                <td class="ValueStyle">Allow Back Order (items can always be added to the cart. Inventory is reduced)</td>
	            </tr>
	            <tr class="RowStyle">
	                <td class="FieldStyle" nowrap="nowrap"><img id="chkIstrackInvEnabled" runat="server" alt="" src="" /></td>
	                <td class="ValueStyle">Don't Track Inventory (items can always be added to the cart and inventory is not reduced)</td>
	            </tr>
       </table>
       <table cellspacing="0" cellpadding="0" class="ViewForm" width="100%">         
	            <tr class="AlternatingRowStyle">
	                <td class="FieldStyle" nowrap="nowrap">In Stock Message</td> 
	                <td class="ValueStyle" width="100%"><asp:Label ID="lblInStockMsg" runat="server" ></asp:Label></td>	                
	            </tr> 
	            <tr class="RowStyle">
	                <td class="FieldStyle" nowrap="nowrap">Out Of Stock Message</td>
	                <td class="ValueStyle"><asp:Label ID="lblOutofStock" runat="server" ></asp:Label></td>
	            </tr>
	            <tr class="AlternatingRowStyle">
	                <td class="FieldStyle" nowrap="nowrap">Back Order Message</td> 
	                <td class="ValueStyle"><asp:Label ID="lblBackOrderMsg" runat="server" ></asp:Label></td>	                
	            </tr>
	   </table>
	   
	   <div>    	            
            <h4>Add-On Values</h4>
            <div align="right"><asp:Button CssClass="Button" ID="btnAddNewAddOnValues" runat="server" Text="Add Value" OnClick="btnAddNewAddOnValues_Click" /></div>
            <p></p>
            <div>
            <asp:GridView OnRowDataBound="uxGrid_RowDataBound" ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" EnableSortingAndPagingCallbacks="False" PageSize="25" AllowSorting="True" EmptyDataText="No product Add-Ons exist in the database." OnRowDeleting="uxGrid_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="AddOnValueId" HeaderText="ID" />
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <a href='add_Addonvalues.aspx?itemid=<%=ItemId %>&AddOnValueId=<%# DataBinder.Eval(Container.DataItem, "AddOnvalueId").ToString()%>'><%# Eval("Name") %></a>
                        </ItemTemplate>                          
                    </asp:TemplateField>
                    <asp:BoundField DataField="SKU" HeaderText="SKU" />
                    <asp:BoundField DataField="QuantityOnHand" HeaderText="Quantity" />
                    <asp:BoundField DataField="ReOrderLevel" HeaderText="Re-Order Level" />
                    <asp:BoundField DataField="DisplayOrder" HeaderText="DisplayOrder" />                            
                    <asp:TemplateField HeaderText="Price">
                        <ItemTemplate>                                                        
                            <%# DataBinder.Eval(Container.DataItem, "Retailprice","{0:c}").ToString()%>
                        </ItemTemplate>                          
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Default">
                        <ItemTemplate>
                        <img alt="" id="Img1" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "DefaultInd").ToString()))%>' runat="server" />                           
                        </ItemTemplate>                          
                    </asp:TemplateField>                            
                    <asp:ButtonField CommandName="Edit" Text="Edit" ButtonType="Button">
                        <ControlStyle CssClass="Button" />                                
                    </asp:ButtonField>                           
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button CommandName="Delete" CausesValidation="false" ID="btnDelete"  runat="server" Text="Delete" CssClass="Button" />
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
          </div>
    </div>
</asp:Content>

