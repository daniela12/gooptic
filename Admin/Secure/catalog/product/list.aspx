<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="Admin_Secure_products_list" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
<div class="Form">
    <table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td><h1>
                Products</h1></td>
			<td align="right"><asp:button CssClass="Button" id="btnAddProduct" CausesValidation="False" Text="Add New Product"
					Runat="server" onclick="btnAddProduct_Click"></asp:button></td>
		</tr>
	</table>
	<p>Use this page to search and manage products in your catalog.</p> 
	<h4>Search Products</h4>
  	
        <asp:Panel ID="Test" DefaultButton="btnSearch" runat="server" >  
        <table border="0" width="70%" cellpadding="0" cellspacing="0">
        <tr>                
        <td>
        <div class="FieldStyle">Product Name</div>
        <div class="ValueStyle"><asp:TextBox ID="txtproductname" runat="server"></asp:TextBox></div>
        </td> 
                        
        <td>
        <div class="FieldStyle">Product Number</div>
        <div class="ValueStyle"><asp:TextBox ID="txtproductnumber" runat="server"></asp:TextBox></div>
        </td> 
           
        <td>               
        <div class="FieldStyle">SKU</div>
        <div class="ValueStyle"><asp:TextBox ID="txtsku" runat="server"></asp:TextBox></div>
        </td>                         
        </tr>
        
        <tr>
        <td>   
        <div class="FieldStyle">Manufacturer</div>  
        <div class="ValueStyle"><asp:DropDownList ID="dmanufacturer" runat="server"></asp:DropDownList></div>
        </td>        
           
        <td>
        <div class="FieldStyle">Product Type</div>
        <div class="ValueStyle"><asp:DropDownList ID="dproducttype" runat="server"></asp:DropDownList></div>
        </td>        
        
        <td>
        <div class="FieldStyle">Product Category</div>   
        <div class="ValueStyle"><asp:DropDownList ID="dproductcategory" runat="server"></asp:DropDownList></div>
        </td>
        </tr>     
        
        <tr>            
        <td colspan="2">
        <div class="ValueStyle">
        <asp:Button ID="btnSearch" runat="server" CssClass="Button" OnClick="btnSearch_Click" Text="Search" />
        <asp:Button ID="btnClear" CausesValidation="false" runat="server" OnClick="btnClear_Click" Text="Clear Search" CssClass="Button" />            
        </div> 
        </td>
        <td></td>
        </tr>
        
      </table>  
   </asp:Panel>
   
 <br />
 <h4>Product List</h4>
    <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" CaptionAlign="Left" AutoGenerateColumns="False" GridLines="None"  AllowPaging="True" PageSize="10" OnPageIndexChanging="uxGrid_PageIndexChanging" OnRowCommand="uxGrid_RowCommand" EmptyDataText="No Products exist in the database." Width="100%" CellPadding="4">
        <Columns>
            <asp:BoundField DataField="productid" HeaderText="ID" />
            
            <asp:TemplateField HeaderText="Image">
                <ItemTemplate><a  href='<%# "view.aspx?itemid=" + DataBinder.Eval(Container.DataItem,"productid")%>' id="LinkView">
                <img alt=" " src='<%#  ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.ThumbnailImagePath + DataBinder.Eval(Container.DataItem, "ImageFile")%>' runat="server" style="border:none" />
                </a>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:HyperLinkField DataNavigateUrlFields="productid" DataNavigateUrlFormatString="view.aspx?itemid={0}" DataTextField="name" HeaderText="Name" />
            <asp:TemplateField HeaderText="Retail Price">
                <ItemTemplate>                                
                    <%# FormatPrice(DataBinder.Eval(Container.DataItem,"RetailPrice")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sale Price">
                <ItemTemplate>                                
                    <%# FormatPrice(DataBinder.Eval(Container.DataItem,"SalePrice")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Wholesale Price">
                <ItemTemplate>                                
                    <%# FormatPrice(DataBinder.Eval(Container.DataItem,"WholesalePrice")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Quantity Available"  DataField="Quantityonhand"/>
            <asp:BoundField HeaderText="Display Order"  DataField="displayorder"/>
            <asp:TemplateField HeaderText="Is Active?">
                <ItemTemplate>                                
                    <img alt="" id="Img1" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "ActiveInd").ToString()))%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="" ItemStyle-Wrap="false" >
                <ItemTemplate>                
                <asp:Button ID="Button1" CommandName="Manage"  CommandArgument ='<%#DataBinder.Eval(Container.DataItem,"productid")%>' Text="Manage" runat="server" CssClass="Button"  />                
                &nbsp;<asp:Button ID="Button5" CommandName="Delete"  CommandArgument ='<%#DataBinder.Eval(Container.DataItem,"productid")%>' Text="Delete" runat="server" CssClass="Button" />
                </ItemTemplate>                
            </asp:TemplateField>            
        </Columns>
        <FooterStyle CssClass="FooterStyle" />
        <RowStyle CssClass="RowStyle" />
        <PagerStyle CssClass="PagerStyle" />
        <HeaderStyle CssClass="HeaderStyle" />
        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
    </asp:GridView>
    </div> 
</asp:Content>


