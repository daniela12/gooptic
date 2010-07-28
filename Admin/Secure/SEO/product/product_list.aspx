<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="product_list.aspx.cs" Inherits="Admin_Secure_catalog_SEO_product_list" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
<div class="SEO">
  <div class="Form">
    <table cellspacing="0" cellpadding="0" width="100%">
		<tr><td><h1>Product SEO Settings</h1></td></tr>
    </table>
    <p>Use this page to update various attributes of products that are used by search engines such as Description, Title, Meta Tags and URL.</p>
    <br />
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
            <asp:BoundField DataField="productid" HeaderText="ID"/>            
            <asp:TemplateField HeaderText="Image">
                <ItemTemplate><a  href='<%# "product_edit.aspx?itemid=" + DataBinder.Eval(Container.DataItem,"productid")%>' id="LinkView">
                <img id="Img1" alt=" " src='<%#  ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.ThumbnailImagePath + DataBinder.Eval(Container.DataItem, "ImageFile").ToString()%>' runat="server" style="border:none" />
                </a>
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:HyperLinkField DataNavigateUrlFields="productid" DataNavigateUrlFormatString="product_edit.aspx?itemid={0}" DataTextField="name" HeaderText="Name" /> 
            <asp:TemplateField HeaderText="Is Active?">
                <ItemTemplate>                                
                    <img alt="" id="Img2" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "ActiveInd").ToString()))%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="" ItemStyle-Wrap="false" >
                <ItemTemplate>                
                <asp:Button ID="Button1" CommandName="Manage"  CommandArgument ='<%#DataBinder.Eval(Container.DataItem,"productid")%>' Text="Manage" runat="server" CssClass="Button"  />                                
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
</div>
</asp:Content>