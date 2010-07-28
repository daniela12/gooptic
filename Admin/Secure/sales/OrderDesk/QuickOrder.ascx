<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuickOrder.ascx.cs" Inherits="Admin_Secure_Enterprise_OrderDesk_QuickOrder" %>
<%@ Register Src="~/Themes/Default/Product/ProductAttributeGrid.ascx" TagName="ProductAttributeGrid"  TagPrefix="ZNode" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<div class="ProductDetail">
    <h1>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>Select Product</td>
                <td class="CloseLink"><asp:LinkButton ID="btnClose" CausesValidation="false" runat="server" OnClick="btnClose_Click">Close X</asp:LinkButton></td>
            </tr>
        </table>
    </h1>
    
    <asp:HiddenField ID="PageIndex" Value="1" runat="server" />
	<asp:HiddenField ID="TotalPages" Value="0" runat="server" />
	    
    <!-- Product Search -->
    <asp:Panel ID="pnlSearchParams" runat="server" DefaultButton="btnSearch">    
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div class="FieldStyle">Partial Product Name</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtProductName" runat="server" ValidationGroup="groupSearch"></asp:TextBox></div>
                </td>
                <td>
                    <div class="FieldStyle">Partial Item#</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtProductNum" runat="server" ValidationGroup="groupSearch"></asp:TextBox></div>
                </td>
                <td>
                    <div class="FieldStyle">Partial SKU</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtProductSku" runat="server" ValidationGroup="groupSearch"></asp:TextBox></div>
                </td>
                 <td>
                    <div class="FieldStyle">Partial Brand</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtBrand" runat="server" ValidationGroup="groupSearch"></asp:TextBox></div>
                </td>
                 <td>
                    <div class="FieldStyle">Partial Category</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtCategory" runat="server" ValidationGroup="groupSearch"></asp:TextBox></div>
                </td>
                <td valign="bottom">
                    <div class="ButtonStyle"><asp:Button ID="btnSearch" ValidationGroup="grpSearch" runat="server" Text="Search" OnClick="Search_Click"  /></div>
                </td>                        
            </tr>
            <tr><td colspan="4"><ZNode:spacer id="Spacer5" SpacerHeight="10" SpacerWidth="1" runat="server"></ZNode:spacer></td></tr>
            <tr><td colspan="4"><asp:Label CssClass="Error" ID="lblSearhError" runat="server" EnableViewState="false" /></td></tr>
        </table>
    </asp:Panel>
    
    <!-- Product List -->
    <asp:Panel ID="pnlProductList" runat="server" Visible="false">
        <div><ZNode:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="1" runat="server"></ZNode:spacer></div>
        <div class="HintText">Click on the "Select" link in the first column to select a product.</div>
        
        <asp:GridView ID="uxGrid" PageSize="10" CellSpacing="0" CellPadding="6" CssClass="SearchGrid" runat="server" AutoGenerateColumns="False" GridLines="None" OnSelectedIndexChanged="uxGrid_SelectedIndexChanged">
            <Columns>
                <asp:CommandField CausesValidation="false" ShowSelectButton="true" ValidationGroup="grpCartItems" ButtonType="Link" SelectText="Select" />
                <asp:TemplateField HeaderText="Id" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="ui_CustomerIdSelect" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ProductId") %>'></asp:Label></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Name" DataField="Name" />
                <asp:BoundField HeaderText="Product Number" DataField="ProductNum" />
                <asp:BoundField HeaderText="Short Description" DataField="ShortDescription" />
                <asp:BoundField HeaderText="Retail Price" DataField="RetailPrice" DataFormatString="{0:c}" />
            </Columns>
            <FooterStyle CssClass="FooterStyle" />
            <RowStyle CssClass="RowStyle" />
            <HeaderStyle CssClass="HeaderStyle" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
            <PagerStyle CssClass="PagerStyle" />            
        </asp:GridView>
         <div class="PagingField">
            <asp:LinkButton ID="FirstPageLink" Text="&laquo; First" OnClick="MoveToFirstPage" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="PreviousPageLink" Text="&laquo; Prev" runat="server" OnClick="PrevRecord"></asp:LinkButton>        
            &nbsp;| Page <%= PageIndex.Value + " of " + TotalPages.Value %> |&nbsp;
            <asp:LinkButton ID="NextPageLink" Text="Next &raquo;" OnClick="NextRecord" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LastPageLink" Text="Last &raquo;" OnClick="MoveToLastPage" runat="server"></asp:LinkButton>
        </div>        
    </asp:Panel>    

    <!-- Product Detail section -->
    <asp:Panel ID="pnlProductDetails" runat="server" Visible="false">    
        <table id="Table1" width="100%" runat="server">
            <tr>
                <td>    
                    <table width="100%" class="Grid" cellpadding="0" cellspacing="0">    
                    <tr class="HeaderStyle">
                        <td>Qty</td>                    
                        <td>Options</td>
                        <td>Unit Price</td>
                        <td>Total Price</td>
                    </tr>
                    <tr class="RowStyle">
                        <td>
                            <asp:DropDownList ID="Qty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Qty_SelectedIndexChanged"></asp:DropDownList>
                          
                            <div>
                                <asp:RequiredFieldValidator  ValidationGroup="grpCartItems" ID="req1" ControlToValidate="Qty"  CssClass="Error" ErrorMessage="*required" runat="server" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="grpCartItems" ValidationExpression="^\d+$" ID="val1" CssClass="Error" ControlToValidate="Qty" ErrorMessage="*invalid" runat="server" Display="Dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                <div><asp:RangeValidator ValidationGroup="grpCartItems" ID="QuantityRangeValidator" CssClass="Error" MinimumValue="0" ControlToValidate="Qty" ErrorMessage="*Out of Stock" MaximumValue="1" Display="Dynamic" SetFocusOnError="true" runat="server" Type="Integer"></asp:RangeValidator></div>
                            </div>
                        </td>
                        <td>
                            <asp:PlaceHolder ID='ControlPlaceHolder' runat="server"></asp:PlaceHolder>
                            <div><ZNode:spacer id="Spacer3" SpacerHeight="1" SpacerWidth="1" runat="server"></ZNode:spacer></div>
                        </td>
                        <td><asp:Label ID="lblUnitPrice" runat="server" Text='&nbsp;'></asp:Label></td>
                        <td><asp:Label ID="lblTotalPrice" runat="server" Text='&nbsp;'></asp:Label></td>
                    </tr>
                    </table>    
                    <div align="right"><asp:Label ID="uxStatus" CssClass="Error" EnableViewState="false" runat="server" Text=''></asp:Label></div>
                    <div><ZNode:spacer id="Spacer1" SpacerHeight="5" SpacerWidth="5" runat="server"></ZNode:spacer></div>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="2">
                        <tr id="tablerow" runat="server" visible="false">
                            <td class="OuterBorder"><asp:Image ID="CatalogItemImage" AlternateText="NA" runat="server" /></td>
                            <td class="OuterBorder">
                                <asp:Label ID="ProductDescription" Text='' runat="server"></asp:Label>
                                <div><ZNode:ProductAttributeGrid ID="uxProductAttributeGrid" CssClassName="SizeGrid" runat="server" /></div>
                            </td>
                        </tr>
                        <tr>
                        <td colspan="2"><div><ZNode:spacer id="Spacer4" SpacerHeight="10" SpacerWidth="10" runat="server"></ZNode:spacer></div></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <div><asp:Button ID="btnSubmit" CssClass="Button" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="grpCartItems" />
                                <asp:Button ID="btnAddProduct" CssClass="Button" runat="server" Text="Add Another Product" OnClick="btnSubmitAnother_Click" ValidationGroup="grpCartItems" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="Button" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" /></div>
                            </td>
                        </tr>
                    </table>    
                </td>
            </tr>
        </table>    
    </asp:Panel>
</div>

<!--During Update Process -->
<asp:UpdateProgress ID="UpdateProgressMozilla" runat="server" DisplayAfter="0" DynamicLayout="true" Visible="false">
    <ProgressTemplate>                                    
         <asp:Panel ID="Panel1" CssClass="overlay" runat="server">
            <asp:Panel ID="Panel2" CssClass="loader" runat="server">
                Loading...<img id="Img1" align="absmiddle" src="~/Images/buttons/loading.gif" runat="server" />
            </asp:Panel>
        </asp:Panel>
    </ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:UpdateProgress ID="UpdateProgressIE" runat="server" DisplayAfter="0" DynamicLayout="true" Visible="false">                                
    <ProgressTemplate>
        <div id="updateProgress">
        <iframe frameborder="0" src="about:blank" style="border:0px;position:absolute;z-index:9;left:0px;top:0px;width:expression(this.offsetParent.scrollWidth);height:expression(this.offsetParent.scrollHeight);filter:progid:DXImageTransform.Microsoft.Alpha(Opacity=75, FinishOpacity=0, Style=0, StartX=0, FinishX=100, StartY=0, FinishY=100);"></iframe>
        <div style="position:absolute;z-index:10;left:expression((this.offsetParent.clientWidth/2)-(this.clientWidth/2)+this.offsetParent.scrollLeft);top:expression((this.offsetParent.clientHeight/2)-(this.clientHeight/2)+this.offsetParent.scrollTop);"><div>Loading...<img id="Img1" align="absmiddle" src="~/Images/buttons/loading.gif" runat="server" /></div></div>
        </div>
    </ProgressTemplate>                                                             
</asp:UpdateProgress>