<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="Views.aspx.cs" Inherits="Admin_Secure_catalog_product_Views" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div><h1>Images for  "<asp:Label ID="lblProdName" runat="server"></asp:Label>"</h1></div>
    <small>Use this page to manage the available images for a product. These images are optional and are displayed as thumbnails on the product page. The customer will be able to click on each thumbnail to see a larger version of the image. This feature can be used to display different color versions of your product or different views (side, top, bottom, etc).</small>
    <div align="right">
        <asp:Button ID="ProductList" runat="server" CssClass="Button" Text="< Back" OnClick="ProductList_Click" />&nbsp;&nbsp;<asp:Button CssClass="Button" ID="btaddprodview" Text="Add Product Image" runat="server" OnClick="AddProduct_Click" /> <br /><br />
        <asp:Label ID="lblmessage" runat="server"></asp:Label><br /><br />
    </div> 
	<table border="0" cellpadding="0" cellspacing="0"width="100%">	
	    <tr>
	        <td>
                <asp:GridView ID="uxGridProductViews" Width="100%" CssClass="Grid" CellPadding="4" CaptionAlign="Left" GridLines="None" runat="server" AutoGenerateColumns="False" AllowPaging="True" ForeColor="Black" OnPageIndexChanging="uxGridProductViews_PageIndexChanging" OnRowCommand="uxGridProductViews_RowCommand" OnRowDeleting="uxGridProductViews_RowDeleting">
                    <FooterStyle CssClass="FooterStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                   <img id="Img2" alt=" " src='<%# ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.ThumbnailImagePath + DataBinder.Eval(Container.DataItem, "ImageFile").ToString()%>' runat="server" style="border:none" />                
                                </ItemTemplate>
                            </asp:TemplateField>              
                            <asp:BoundField DataField="Name" HeaderText="Title" />
                            <asp:TemplateField HeaderText="Is Active?">
                                <ItemTemplate>                                
                                    <img id="Img1" alt="" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "ActiveInd").ToString()))%>' runat="server" />
                                </ItemTemplate>                          
                           </asp:TemplateField>
                           <asp:TemplateField>
                                <ItemTemplate>
                                    <div id="Test" runat="server" >
                                    <asp:Button CssClass="Button" ID="EditProductView" Text="Edit" CommandArgument='<%# Eval("productimageid") %>' CommandName="Edit" runat="Server" />
                                    </div>
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="RemoveProductView"  CssClass="Button" Text="Remove" CommandArgument='<%# Eval("productimageid") %>' CommandName="Delete" runat="Server" />
                                </ItemTemplate>
                          </asp:TemplateField>
                       </Columns>
                    <EmptyDataTemplate>
                        No Product Image available to display.
                    </EmptyDataTemplate>
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



