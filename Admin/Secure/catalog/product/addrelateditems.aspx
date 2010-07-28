<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Themes/Standard/edit.master" CodeFile="addrelateditems.aspx.cs" Inherits="Admin_Secure_catalog_product_addrelateditems" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div class="Form">
        <table width="100%">
            <tr>
                <td><h1>Add a Related Item</h1><p>Use this page to add related items to the product. The related items will show up on the Related Items Tab.</p></td>
                <td align="right" valign="top"><asp:Button ID="btnBack" runat="server" CssClass="Button" OnClick="btnBack_Click" Text="<< Back to Product Detail" /></td>
            </tr>
        </table>
        <ZNode:DemoMode id="DemoMode1" runat="server"></ZNode:DemoMode>
        <!-- Search product panel -->
        <asp:Panel ID="Test" DefaultButton="btnSearch" runat="server">  
            <h4>Search Product</h4>
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
        
        <!-- Product List -->
        <asp:Panel ID="pnlProductList" runat="server" Visible="false">
            <h4>Product List</h4>
            <p>Select related product to add.</p>
            <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" CaptionAlign="Left" AutoGenerateColumns="False" GridLines="None"  AllowPaging="True" PageSize="10" OnPageIndexChanging="uxGrid_PageIndexChanging" EmptyDataText="No Products exist in the database." Width="100%" CellPadding="4">
            <Columns>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkProduct" runat="server" Enabled='<%# !(int.Parse(DataBinder.Eval(Container.DataItem, "ProductId").ToString()) == ItemID) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                            
                <asp:BoundField DataField="ProductId" HeaderText="ID" />
                <asp:TemplateField HeaderText="Image">
                <ItemTemplate><img id="prodImage" alt=" " src='<%#  ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.ThumbnailImagePath + DataBinder.Eval(Container.DataItem, "ImageFile").ToString()%>' runat="server" style="border:none" /></ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:BoundField DataField="Name" HeaderText="Name" />                
                <asp:TemplateField HeaderText="Is Active?">
                    <ItemTemplate>                                
                        <img alt="" id="chMark" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "ActiveInd").ToString()))%>' runat="server" />
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
            <div><ZNode:spacer id="Spacer1" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>
            <div><asp:Label ID="lblError" runat="server" Text="Label" Visible="false"  CssClass="Error" ForeColor="red"></asp:Label></div>
            <div><ZNode:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>
            <div>
                <asp:Button ID="butAddNew" CssClass="Button" Text="Submit" OnClick="Update_Click" runat="server" />
                <asp:Button ID="Cancel" CssClass="Button" Text="Cancel" OnClick="Cancel_Click" runat="server" />
            </div>            
        </asp:Panel>
        <div><ZNode:spacer id="Spacer3" SpacerHeight="20" SpacerWidth="3" runat="server"></ZNode:spacer></div>
    </div>
</asp:Content>


