<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="view.aspx.cs" Inherits="Admin_Secure_products_view" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">   
   
    <table>
        <tr>
            <td><h1>Product Details - <%= lblProdName.Text.Trim() %></h1></td>
            <td> <div style="margin-left:500px;"><asp:Button ID="btnBack" OnClick="btnBack_Click" runat="server" Text="<< Back to List page" CssClass="Button" /></div> </td>
        </tr>
    </table>
    <div><uc1:spacer id="Spacer8" SpacerHeight="15" SpacerWidth="3" runat="server"></uc1:spacer></div>
    
    <asp:ScriptManager id="ScriptManager" runat="server"></asp:ScriptManager>
    <ajaxToolKit:TabContainer ID="tabProductDetails" runat="server">
        <ajaxToolKit:TabPanel ID="pnlGeneral" runat="server">        
        <HeaderTemplate>Product Information</HeaderTemplate>
        <ContentTemplate>
            <div><uc1:spacer id="Spacer7" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
            <div align="left"><asp:Button ID="EditProduct" runat="server" CssClass="Button" OnClick="EditProduct_Click" Text="Edit Information" /></div>
	        <h4>General Information</h4>	
	        <table cellspacing="0" cellpadding="0" class="ViewForm" width="100%">
	            <tr class="RowStyle">
	                <td class="FieldStyle" nowrap="nowrap">
		                Product Name
	                </td>
	                <td class="ValueStyle" width="100%">
	                    <asp:Label ID="lblProdName" runat="server"></asp:Label>
	                </td>
	            </tr>
	            <tr class="AlternatingRowStyle">
	                <td class="FieldStyle" nowrap="nowrap">
		                Product Code
	                </td>
	                <td class="ValueStyle">
	                    <asp:Label ID="lblProdNum" runat="server"></asp:Label>
	                </td>
	            </tr>
	            <tr class="RowStyle">
	                <td class="FieldStyle" nowrap="nowrap">
		                SKU or Part#
	                </td>
	                <td class="ValueStyle">
	                    <asp:Label ID="lblProductSKU" runat="server"></asp:Label>
	                </td>
	            </tr>
	            <tr class="AlternatingRowStyle">
	                <td class="FieldStyle" nowrap="nowrap">
		                Quantity on Hand
	                </td>
	                <td class="ValueStyle">
	                    <asp:Label ID="lblQuantity" runat="server"></asp:Label>
	                </td>
	            </tr>
	            <tr class="RowStyle">
	                <td class="FieldStyle" nowrap="nowrap">
		                Re-Order Level
	                </td>
	                <td class="ValueStyle">
	                    <asp:Label ID="lblReOrder" runat="server"></asp:Label>
	                </td>
	            </tr>
	            <tr class="AlternatingRowStyle">
	                <td class="FieldStyle" nowrap="nowrap">
		                Max Selectable Quantity
	                </td>
	                <td class="ValueStyle">
	                    <asp:Label ID="lblMaxQuantity" runat="server"></asp:Label>
	                </td>
	            </tr>	            
	            <tr class="RowStyle">
	                <td class="FieldStyle" nowrap="nowrap">
	                    Product Type
	                </td>
	                <td valign="top" class="ValueStyle">
	                    <asp:Label ID="lblProdType" runat="server"></asp:Label>
                    </td>
	            </tr>
	            <tr class="AlternatingRowStyle">
	                <td class="FieldStyle" nowrap="nowrap">
	                    Manufacturer Name
	                </td>
	                <td valign="top" class="ValueStyle">
	                    <asp:Label ID="lblManufacturerName" runat="server"></asp:Label>
                    </td>
	            </tr>
	            <tr class="RowStyle">
	                <td class="FieldStyle" nowrap="nowrap">
	                    Supplier Name
	                </td>
	                <td valign="top" class="ValueStyle">
	                    <asp:Label ID="lblSupplierName" runat="server"></asp:Label>
                    </td>
	            </tr>
	            <tr class="AlternatingRowStyle">
	                <td class="FieldStyle" nowrap="nowrap">
	                    Retail Price
	                </td>
	                <td valign="top" class="ValueStyle" width="100%">
	                    <asp:Label ID="lblRetailPrice" runat="server"></asp:Label>
	                </td>
	           </tr>
	           <tr class="RowStyle">
	                <td class="FieldStyle" nowrap="nowrap">
	                    Sale Price
	                </td>
	                <td valign="top" class="ValueStyle">
	                    <asp:Label ID="lblSalePrice" runat="server"></asp:Label>
	                </td>
	           </tr>
	           <tr class="AlternatingRowStyle">
	               <td class="FieldStyle" nowrap="nowrap">
		                WholeSale Price
	               </td>
	               <td valign="top" class="ValueStyle">
	                    <asp:Label ID="lblWholeSalePrice" runat="server" CssClass="Price"></asp:Label>
	               </td>
	           </tr>
	            <tr class="RowStyle">
	               <td class="FieldStyle" nowrap="nowrap">
		                Tax Class
	               </td>
	               <td valign="top" class="ValueStyle">
	                   <asp:Label ID="lblTaxClass" runat="server"></asp:Label>
	               </td>
	           </tr>	           
               <tr class="AlternatingRowStyle">
                    <td class="FieldStyle" valign="top" nowrap="nowrap">
	                    Product Categories
                    </td>
                    <td>
                        <asp:Label ID="lblProductCategories" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr class="RowStyle">
                    <td class="FieldStyle" valign="top" nowrap="nowrap">
	                    Download Link
                    </td>
                    <td>
                        <asp:Label ID="lblDownloadLink" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
	        </table>
            
            <h4>Display Settings</h4>
	        <table border="0" cellpadding="0" cellspacing="0" class="ViewForm" width="100%">
	            <tr class="RowStyle">
	                   <td class="FieldStyle" nowrap="nowrap">Page Template</td>
	                   <td class="ValueStyle" width="100%"><asp:Label ID="lblMastePageName" runat="server"></asp:Label></td>
	              </tr>
	            <tr class="AlternatingRowStyle">
	              <td class="FieldStyle" nowrap="nowrap">
		              Display Order
	              </td>
	              <td valign="top" class="ValueStyle">
	                  <asp:Label ID="lblProdDisplayOrder" runat="server"></asp:Label>
	              </td>
	            </tr>	           
            </table>
            
            <h4>Shipping Settings</h4>
            
            <table border="0" cellpadding="0" cellspacing="0" class="ViewForm" width="100%">
                <tr class="AlternatingRowStyle">
	                   <td class="FieldStyle" nowrap="nowrap"> Free Shipping</td>
	                   <td valign="top" class="ValueStyle"><img id="freeShippingInd" runat="server" alt="" src=""/>	                       
	                   </td>
                </tr> 
                <tr class="RowStyle">
	                   <td class="FieldStyle" nowrap="nowrap">Ship Separately</td>
	                   <td valign="top" class="ValueStyle"><img id="shipSeparatelyInd" runat="server" alt="" src=""/>	                       
	                   </td>
                </tr> 
                <tr class="AlternatingRowStyle">
	                   <td class="FieldStyle">Shipping Rule Type Name</td>
	                   <td valign="top" class="ValueStyle" width="100%"><asp:Label ID="lblShippingRuleTypeName" runat="server" /></td>
                </tr>                 
                <tr class="RowStyle">
	               <td class="FieldStyle" nowrap="nowrap">Weight</td>
	               <td valign="top" class="ValueStyle">
	                   <asp:Label ID="lblWeight" runat="server"></asp:Label>
	               </td>
                </tr>
                <tr class="AlternatingRowStyle">
	               <td class="FieldStyle" nowrap="nowrap">Height</td>
	               <td valign="top" class="ValueStyle">
	                   <asp:Label ID="lblHeight" runat="server"></asp:Label>	                   
	               </td>
                </tr> 
                <tr class="RowStyle">
	               <td class="FieldStyle" nowrap="nowrap">Width</td>
	               <td valign="top" class="ValueStyle"><asp:Label ID="lblWidth" runat="server"></asp:Label></td>
                </tr> 
                <tr class="AlternatingRowStyle">
	               <td class="FieldStyle" nowrap="nowrap">Length</td>
	               <td valign="top" class="ValueStyle"><asp:Label ID="lblLength" runat="server"></asp:Label></td>
                </tr> 
            </table>
            
	        <h4>Product Image</h4>
	        <div class="Image"><asp:Image ID="ItemImage"  runat="server" /></div>
	        
            <h4>Short Description</h4>        	
	        <div class="ShortDescription"><asp:Label ID="lblShortDescription" runat="server"></asp:Label></div>   

	        <h4>Product Description</h4>        	
	        <div class="Description"><asp:Label ID="lblProductDescription" runat="server"></asp:Label></div>   
	        
	        <div><uc1:spacer id="Spacer2" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>
	        
	    </ContentTemplate>
    </ajaxToolKit:TabPanel>
    
    <ajaxToolKit:TabPanel ID="pnlAdvancedSettings" runat="server">        
        <HeaderTemplate>Advanced Settings</HeaderTemplate>
        <ContentTemplate>
            <div><uc1:spacer id="Spacer9" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
            <div align="left"><asp:Button ID="Button2" runat="server" CssClass="Button" OnClick="EditAdvancedSettings_Click" Text="Edit Advanced Settings" /></div>
            <h4>Display Settings</h4>
	        <table border="0" cellpadding="0" cellspacing="0" class="ViewForm">
	            <%--<tr class="AlternatingRowStyle">
	                <td class="FieldStyle" valign="top" nowrap="nowrap">
		                Product Categories
	                </td>
	                <td>
                        <asp:Label ID="lblProductCategories" runat="server" Text="Label"></asp:Label>
                    </td>
	           </tr>--%>
	           <tr class="RowStyle">
	               <td class="FieldStyle" nowrap="nowrap">
    	                Enabled?
	               </td>
	               <td valign="top" class="ValueStyle" width="100%">
	                    <img id="chkProductEnabled" runat="server" alt="" src=""/>
	               </td>
	          </tr>	          
	          <tr class="AlternatingRowStyle">
	             <td class="FieldStyle" nowrap="nowrap">
	                 Home Page Special
                 </td>
                 <td valign="top" class="ValueStyle">
                    <img id="chkIsSpecialProduct" runat="server" alt="" src=""/>
                 </td>
	        </tr>
	        <tr class="RowStyle">
	             <td class="FieldStyle" nowrap="nowrap">
	                 New Item?
                 </td>
                 <td valign="top" class="ValueStyle">
                    <img id="chkIsNewItem" runat="server" alt="" src=""/>
                 </td>
	        </tr>
	        <tr class="AlternatingRowStyle">
	             <td class="FieldStyle" nowrap="nowrap">Featured Item Icon</td>
	              <td valign="top" class="ValueStyle"><img id="FeaturedProduct" runat="server" alt="" src=""/>	                       
	              </td>
             </tr> 
	        <tr class="RowStyle">
                <td class="FieldStyle" nowrap="nowrap">
	                Call For Pricing
                </td>
                <td valign="top" class="ValueStyle">
                    <img id="chkProductPricing" runat="server" alt="" src=""/>
                </td>
            </tr>
	        <tr class="AlternatingRowStyle">
                <td class="FieldStyle" nowrap="nowrap">
	                Display Inventory
                </td>
    	        <td valign="top" class="ValueStyle">
	                <img id="chkproductInventory" runat="server" alt="" src="" />
	            </td>
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
                    <tr class="RowStyle">
                        <td class="FieldStyle" style="display:none;" nowrap="nowrap">Drop Ship</td>
                        <td class="ValueStyle" style="display:none;"><img id="IsDropShipEnabled" runat="server" alt="" src=''/></td>
                    </tr>
            </table>          
            
            <h4>Recurring Billing Settings</h4>
            <table cellspacing="0" cellpadding="0" class="ViewForm" width="100%">
                    <tr class="AlternatingRowStyle">
                        <td class="FieldStyle" nowrap="nowrap">Recurring Billing enabled?</td>
                        <td class="ValueStyle" width="100%"><img id="imgRecurringBillingInd" runat="server" alt="" src=''/></td>
                    </tr>
                    <tr class="RowStyle">
                        <td class="FieldStyle" nowrap="nowrap">Period</td>
                        <td class="ValueStyle"><asp:Label ID="lblBillingPeriod" runat="server" ></asp:Label></td>
                    </tr>
                    <tr class="AlternatingRowStyle">
                        <td class="FieldStyle" nowrap="nowrap">Frequency</td>
                        <td class="ValueStyle"><asp:Label ID="lblFrequency" runat="server" ></asp:Label></td>
                    </tr>
                    <tr class="RowStyle">
                        <td class="FieldStyle" nowrap="nowrap">Total Cycles</td>
                        <td class="ValueStyle"><asp:Label ID="lblTotalCycles" runat="server" ></asp:Label></td>
                    </tr>
            </table>
            
        </ContentTemplate>
    </ajaxToolKit:TabPanel>
     <ajaxToolKit:TabPanel ID="pnlManageinventory" runat="server">
            <HeaderTemplate>Manage SKUs</HeaderTemplate>        
            <ContentTemplate>         
               <div class="Form">
               <div><uc1:spacer id="Spacer15" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
                <div align="left">
                    <asp:Button CssClass="Button" ID="butAddNewSKU" Text="Add SKU or Part#" runat="server" OnClick="btnAddSKU_Click" />
                </div>
                <div><uc1:spacer id="Spacer21" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
                 <table border="0" width="70%" cellpadding="0" cellspacing="0">
                  <tr>                                                                    
                   <td>
                    <div class="FieldStyle">Search SKUs</div>
                    <div class="ValueStyle"><asp:PlaceHolder id="ControlPlaceHolder" runat="server"></asp:PlaceHolder></div>
                   </td>
                 </tr>
                 <tr>            
                  <td colspan="2">
                   <div class="ValueStyle">
                    <asp:Button ID="btnSearch" runat="server" CssClass="Button" OnClick="btnSearch_Click" Text="Search" />
                    <asp:Button ID="btnClear" CausesValidation="false" runat="server" OnClick="btnClear_Click" Text="Clear Search" CssClass="Button" />            
                   </div> 
                  </td>
                 </tr>
                </table>
                </div>    
                <h4>Current Inventory</h4>
                <asp:UpdatePanel ID="updPnlInventoryDisplayGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="uxGridInventoryDisplay" Width="100%" CssClass="Grid" CellPadding="4" CaptionAlign="Left" GridLines="None" runat="server" AutoGenerateColumns="False" AllowPaging="True" ForeColor="Black" OnPageIndexChanging="uxGridInventoryDisplay_PageIndexChanging" OnRowCommand="uxGridInventoryDisplay_RowCommand" OnRowDeleting="uxGridInventoryDisplay_RowDeleting" PageSize="25">
                           <FooterStyle CssClass="FooterStyle" />
                           <Columns>
                               <asp:BoundField DataField="sku" HeaderText="SKU or Part#" />
                               <asp:BoundField DataField="skuid" HeaderText="ID" />
                               <asp:BoundField DataField="quantityonhand" HeaderText="QuantityOnHand" />  
                               <asp:BoundField DataField="reorderlevel" HeaderText="Re-Order Level"/>                             
                               <asp:TemplateField HeaderText="Is Active?">
                                    <ItemTemplate>                                
                                        <img alt="" id="Img3" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "ActiveInd").ToString()))%>' runat="server" />
                                    </ItemTemplate>
                               </asp:TemplateField>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </ajaxToolKit:TabPanel>
     <ajaxToolKit:TabPanel ID="tabpnlSEO" runat="server">
            <HeaderTemplate>SEO Settings</HeaderTemplate>        
            <ContentTemplate>  
                <div><uc1:spacer id="Spacer20" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>              
                <div align="left"><asp:Button ID="btnEditSEO" runat="server" CssClass="Button" OnClick="EditSEOSettings_Click" Text="Edit SEO Settings" /></div>
                <uc1:spacer id="Spacer4" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer>
	            <table border="0" cellpadding="0" cellspacing="0" class="ViewForm">
	                <tr class="RowStyle">
	                    <td class="FieldStyle" nowrap="nowrap">
	                    Search Engine Title
	                    </td>
	                    <td valign="top" class="ValueStyle" width="100%">
	                    <asp:Label ID="lblSEOTitle" runat="server"></asp:Label>
	                    </td>
	                </tr>
	                <tr class="AlternatingRowStyle">
	                    <td class="FieldStyle" nowrap="nowrap">
		                    Search Engine Keywords
	                    </td>
	                    <td valign="top" class="ValueStyle">
	                    <asp:Label ID="lblSEOKeywords" runat="server" CssClass="Price"></asp:Label>
	                    </td>
	                </tr>
	                <tr class="RowStyle">
                        <td class="FieldStyle" nowrap="nowrap">
                            Search Engine Description
                        </td>
                        <td valign="top" class="ValueStyle">
                        <asp:Label ID="lblSEODescription" runat="server"></asp:Label>
	                    </td>
	                </tr>
	                 <tr class="AlternatingRowStyle">
                        <td class="FieldStyle" nowrap="nowrap">
                            SEO URL
                        </td>
                        <td valign="top" class="ValueStyle">
                        <asp:Label ID="lblSEOURL" runat="server"></asp:Label>
	                    </td>
	                </tr>
	            </table>            
            </ContentTemplate>
        </ajaxToolKit:TabPanel>
    <ajaxToolKit:TabPanel ID="pnlRelatedItems" runat="server">
        <HeaderTemplate>Related Items</HeaderTemplate>
        <ContentTemplate>	
            <div><uc1:spacer id="Spacer6" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
            <table border="0" cellpadding="0" cellspacing="0"width="100%">
                <tr>
                    <td><asp:Button CssClass="Button" ID="AddRelatedItems" Text="Add Related Items to this Product" runat="server" OnClick="AddRelatedItems_Click" /></td>
                </tr>               
                <tr>
                    <td colspan="2">
                        <uc1:spacer id="Spacer5" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer>
                    </td>
                </tr>
	            <tr>
	                <td colspan="2" valign="middle">
	                    <!-- Update Panel for grid paging that are used to avoid the postbacks -->
	                    <asp:UpdatePanel ID="updPnlRealtedGrid" runat="server" UpdateMode="Conditional">
	                        <ContentTemplate>	                    
                                <asp:GridView ID="uxGrid" ShowFooter="true"  ShowHeader="true" CaptionAlign="Left" runat="server" ForeColor="Black" CellPadding="4"  AutoGenerateColumns="False" CssClass="Grid" Width="100%" GridLines="None" OnRowCommand="uxGrid_RowCommand" AllowPaging="True" OnPageIndexChanging="uxGrid_PageIndexChanging" PageSize="5" >
                                    <Columns>
                                        <asp:BoundField DataField="productid" HeaderText="ID" />
                                        <asp:BoundField DataField="name" HeaderText="Product Name" />                      
                                        <asp:TemplateField>
                                        <ItemTemplate> <asp:Button Id="Delete" Text="Remove"  CommandArgument='<%# Eval("productid") %>' CommandName="RemoveItem" CssClass="Button"  runat="server" />
                                        </ItemTemplate>                  
                                        </asp:TemplateField>                        
                                        </Columns>
                                        <EmptyDataTemplate>
                                                        No related items found
                                        </EmptyDataTemplate>
                                        
                                       <RowStyle CssClass="RowStyle" />
                                       <HeaderStyle CssClass="HeaderStyle" />
                                       <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                  <FooterStyle CssClass="FooterStyle" />
                                </asp:GridView>
                            </ContentTemplate>
	                    </asp:UpdatePanel> 
	                </td>
	            </tr>	    
	        </table>
        </ContentTemplate>
        </ajaxToolKit:TabPanel>
        
        <ajaxToolKit:TabPanel ID="pnlAlternateImages" runat="server">
            <HeaderTemplate>Alternate Images</HeaderTemplate>
            <ContentTemplate>         
                <div><uc1:spacer id="Spacer13" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
	             <table border="0" cellpadding="0" cellspacing="0"width="100%"> 
	                <tr>
                        <td><asp:Button CssClass="Button" ID="Button1" Text="Add Alternate Product Image" runat="server" OnClick="AddProductView_Click" /></td>
                    </tr>      
                    <tr>
                        <td colspan="2">
                            <uc1:spacer id="Spacer1" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer>
                        </td>
                    </tr>	
	                <tr>
	                    <td colspan="2" valign="middle">
	                        <!-- Update Panel for grid paging that are used to avoid the postbacks -->
	                        <asp:UpdatePanel ID="updPnlGridThumb" runat="server" UpdateMode="Conditional">
	                            <ContentTemplate>
                                    <asp:GridView ID="GridThumb" ShowFooter="true"  ShowHeader="true" CaptionAlign="Left" runat="server" ForeColor="Black" CellPadding="4"  AutoGenerateColumns="False" CssClass="Grid" Width="100%" GridLines="None" OnRowCommand="GridThumb_RowCommand" AllowPaging="True" OnPageIndexChanging="GridThumb_PageIndexChanging"  OnRowDeleting="GridThumb_RowDeleting"  PageSize="5" >
                                      <Columns>     
                                         <asp:BoundField DataField="productimageid" HeaderText="ID" />               
                                        <asp:TemplateField HeaderText="Image">
                                            <ItemTemplate>
                                             <img id="SwapImage" alt="" src='<%# ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.ThumbnailImagePath + DataBinder.Eval(Container.DataItem, "ImageFile").ToString()%>' runat="server" />         
                                            </ItemTemplate>
                                        </asp:TemplateField>                      
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:BoundField DataField="ImageTypeName" HeaderText="Image Type" />               
                                        <asp:TemplateField HeaderText="Product Page">
                                            <ItemTemplate>                        
                                                <img id="Img1" alt="" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "ActiveInd").ToString()))%>' runat="server" />                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category Page">
                                            <ItemTemplate>                        
                                                <img id="Img2" alt="" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "ShowOnCategoryPage").ToString()))%>' runat="server" />                    
                                            </ItemTemplate>
                                        </asp:TemplateField>                    
                                        <asp:BoundField DataField="DisplayOrder" HeaderText="Display Order" />
                                        <asp:TemplateField>
                                          <ItemTemplate>
                                                <asp:Button CssClass="Button" ID="EditProductView" Text="Edit" CommandArgument='<%# Eval("productimageid") %>' CommandName="Edit" runat="Server" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button Id="Delete" Text="Delete"  CommandArgument='<%# Eval("productimageid") %>' CommandName="RemoveItem" CssClass="Button"  runat="server" />
                                            </ItemTemplate>                  
                                        </asp:TemplateField>                                                                
                                     </Columns>
                                    <EmptyDataTemplate>
                                        No alternate product image found
                                    </EmptyDataTemplate>
                                            
                                        <RowStyle CssClass="RowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                        <FooterStyle CssClass="FooterStyle" />
                                  </asp:GridView>
                              </ContentTemplate>
                          </asp:UpdatePanel> 
	                    </td>
	                </tr>
	             </table>
            
            </ContentTemplate>
        </ajaxToolKit:TabPanel>
        
        <ajaxToolKit:TabPanel ID="pnlProductOptions" runat="server">
            <HeaderTemplate>Product Add-Ons</HeaderTemplate>                  
            <ContentTemplate> 
                <div><uc1:spacer id="Spacer14" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>                      
	            <table border="0" cellpadding="0" cellspacing="0"width="100%"> 
	                 <tr>
                        <td>
                            <div align="left"><asp:Button ID="btnAddNewAddOn" runat="server" CssClass="Button" Text="Add an Add-On to this Product" OnClick="btnAddNewAddOn_Click" /></div>
                        </td>
                    </tr>      
                    <tr>
                        <td colspan="2">
                            <uc1:spacer id="Spacer3" SpacerHeight="15" SpacerWidth="3" runat="server"></uc1:spacer>
                        </td>
                    </tr>
                </table>
                <!-- Update Panel for grid paging that are used to avoid the postbacks -->
                <asp:UpdatePanel ID="updPnlProductAddOnsGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView OnRowDataBound="uxGridProductAddOns_RowDataBound" ID="uxGridProductAddOns" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="uxGridProductAddOns_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGridProductAddOns_RowCommand" Width="100%" EnableSortingAndPagingCallbacks="False" PageSize="15" AllowSorting="True" EmptyDataText="No Add-Ons associated with this Product.">
                            <Columns>
                                <asp:BoundField DataField="ProductAddOnId" HeaderText="ID" />                                   
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate><%# GetAddOnName(Eval("AddonId")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Title">
                                    <ItemTemplate><%# GetAddOnTitle(Eval("AddonId")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button CommandName="Remove" CausesValidation="false" ID="btnDelete"  runat="server" Text="Remove" CssClass="Button" />
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
                    </ContentTemplate>
                </asp:UpdatePanel>
             
            </ContentTemplate>            
        </ajaxToolKit:TabPanel>       
        
       
       
        <ajaxToolKit:TabPanel ID="pnlTieredPricing" runat="server">
            <HeaderTemplate>Tiered Pricing</HeaderTemplate>        
            <ContentTemplate>  
                <div><uc1:spacer id="Spacer16" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>      
	            <table border="0" cellpadding="0" cellspacing="0"width="100%"> 
	                 <tr>
                        <td>
                        <div align="left"><asp:Button ID="btnAddTieredPrice" runat="server" CssClass="Button" Text="Add Tiered Pricing" OnClick="AddTieredPricing_Click" /></div>
                        </td>
                    </tr>      
                    <tr>
                        <td colspan="2">
                            <uc1:spacer id="Spacer10" SpacerHeight="15" SpacerWidth="3" runat="server"></uc1:spacer>
                        </td>
                    </tr>
                </table>
                <!-- Update Panel for grid paging that are used to avoid the postbacks -->
                <asp:UpdatePanel ID="updPnlTieredPricingGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="uxGridTieredPricing" Width="100%" CssClass="Grid" CellPadding="4" CaptionAlign="Left" GridLines="None" runat="server" AutoGenerateColumns="False" AllowPaging="True" ForeColor="Black" OnPageIndexChanging="uxGridTieredPricing_PageIndexChanging" OnRowDataBound="uxGridTieredPricing_RowDataBound" OnRowCommand="uxGridTieredPricing_RowCommand"  OnRowDeleting="uxGridTieredPricing_RowDeleting" PageSize="25" EmptyDataText="No Tiered pricing found for this Product.">
                           <FooterStyle CssClass="FooterStyle" />
                           <Columns>
                               <asp:BoundField DataField="ProductTierID" HeaderText="ID" />
                                <asp:TemplateField HeaderText="Profile">
                                    <ItemTemplate><%# GetProfileName(DataBinder.Eval(Container.DataItem, "ProfileID"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:BoundField DataField="TierStart" HeaderText="Tier Start" />
                               <asp:BoundField DataField="TierEnd" HeaderText="Tier End" />
                               <asp:TemplateField HeaderText="Price">                               
                                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem,"Price","{0:c}") %></ItemTemplate> 
                               </asp:TemplateField>                                
                               <asp:TemplateField>
                                        <ItemTemplate>
                                            <div id="Div1" runat="server" >
                                                <asp:Button CssClass="Button" ID="EditTieredPricing" Text="Edit" CommandArgument='<%# Eval("ProductTierID") %>' CommandName="Edit" runat="Server" />
                                            </div>
                                        </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField>
                                        <ItemTemplate>
                                                <asp:Button ID="btnRemoveTieredPricing"  CssClass="Button" Text="Delete" CommandArgument='<%# Eval("ProductTierID") %>' CommandName="Delete" runat="Server" />
                                        </ItemTemplate>
                               </asp:TemplateField>
                           </Columns>
                           <RowStyle CssClass="RowStyle" />
                           <EditRowStyle CssClass="EditRowStyle" />
                           <PagerStyle CssClass="PagerStyle" />
                           <HeaderStyle CssClass="HeaderStyle" />
                           <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </ajaxToolKit:TabPanel>
        
        <ajaxToolKit:TabPanel ID="AjaxPnlHighlights" runat="server">
            <HeaderTemplate>Highlights</HeaderTemplate>        
            <ContentTemplate> 
                <div><uc1:spacer id="Spacer17" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
                <div align="left"><asp:Button CssClass="Button" ID="btnAddNewHighlight" Text="Add New Highlight" runat="server" OnClick="btnAddHighlight_Click"  /></div>
                <uc1:spacer id="Spacer12" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer>
                <div>
                    <asp:GridView ID="uxGridHighlights" Width="100%" CssClass="Grid" CellPadding="4" CaptionAlign="Left" GridLines="None" runat="server" AutoGenerateColumns="False" AllowPaging="True" ForeColor="Black" OnPageIndexChanging="uxGridHighlights_PageIndexChanging" OnRowDataBound="uxGridHighlights_RowDataBound" OnRowCommand="uxGridHighlights_RowCommand"  OnRowDeleting="uxGridHighlights_RowDeleting" PageSize="25" EmptyDataText="No highlights found for this Product.">
                       <FooterStyle CssClass="FooterStyle" />
                       <Columns>
                            <asp:TemplateField Visible="true">
                                <HeaderTemplate>ID</HeaderTemplate>
                                <ItemTemplate>                                
                                    <%# DataBinder.Eval(Container.DataItem,"HighlightId")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>Name</HeaderTemplate>
                                <ItemTemplate>                                
                                    <%# DataBinder.Eval(Container.DataItem,"Name") %>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:BoundField DataField="ImageTypeName" HeaderText="Type" />
                            <asp:TemplateField>
                                <HeaderTemplate>Display Order</HeaderTemplate>
                                <ItemTemplate>                                
                                    <%# DataBinder.Eval(Container.DataItem,"DisplayOrder")%>
                                </ItemTemplate>
                            </asp:TemplateField>                                                        
                            <asp:TemplateField>
                                    <ItemTemplate>
                                            <asp:Button ID="DeleteHighlight"  CssClass="Button" Text="Remove" CommandArgument='<%# Eval("ProductHighlightID") %>' CommandName="Delete" runat="Server" />
                                    </ItemTemplate>
                           </asp:TemplateField>
                       </Columns>
                       <RowStyle CssClass="RowStyle" />
                       <EditRowStyle CssClass="EditRowStyle" />
                       <PagerStyle CssClass="PagerStyle" />
                       <HeaderStyle CssClass="HeaderStyle" />
                       <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                    </asp:GridView>               
                </div>	            
	        </ContentTemplate>
	    </ajaxToolKit:TabPanel>
        
        <ajaxToolKit:TabPanel ID="TabPanel1" runat="server">
            <HeaderTemplate>Digital Asset</HeaderTemplate>        
            <ContentTemplate>   
                <div><uc1:spacer id="Spacer18" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>         
                <div align="left">
                    <asp:Button CssClass="Button" ID="btnAddDigitalAsset" Text="Add Digital Asset" runat="server" OnClick="btnAddDigitalAsset_Click" />
                </div>
              <uc1:spacer id="Spacer11" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer>
                <asp:UpdatePanel ID="updPnlDigitalAsset" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="uxGridDigitalAsset" Width="100%" CssClass="Grid" CellPadding="4" CaptionAlign="Left" GridLines="None" runat="server" AutoGenerateColumns="False" AllowPaging="True" ForeColor="Black" OnPageIndexChanging="uxGridDigitalAsset_PageIndexChanging" OnRowCommand="uxGridDigitalAsset_RowCommand" OnRowDeleting="uxGridDigitalAsset_RowDeleting" PageSize="25" EmptyDataText="No digital assets found for this Product.">
                           <FooterStyle CssClass="FooterStyle" />
                           <Columns>                               
                               <asp:BoundField DataField="DigitalAssetID" HeaderText="ID" />
                               <asp:BoundField DataField="DigitalAsset" HeaderText="Digital Asset" />
                               <asp:TemplateField HeaderText="Assigned">
                                        <ItemTemplate>
                                                <img alt="" id="Img2" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(IsDigitalAssetAssigned(DataBinder.Eval(Container.DataItem, "OrderLineItemId")))%>' runat="server" />
                                        </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField>
                                        <ItemTemplate>
                                                <asp:Button ID="btnRemoveDigitalAsset"  CssClass="Button" Text="Remove" CommandArgument='<%# Eval("DigitalAssetID") %>' CommandName="Delete" runat="Server" visible='<%# !(IsDigitalAssetAssigned(DataBinder.Eval(Container.DataItem, "OrderLineItemId")))%>' />
                                        </ItemTemplate>
                               </asp:TemplateField>
                           </Columns>
                           <RowStyle CssClass="RowStyle" />
                           <EditRowStyle CssClass="EditRowStyle" />
                           <PagerStyle CssClass="PagerStyle" />
                           <HeaderStyle CssClass="HeaderStyle" />
                           <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </ajaxToolKit:TabPanel>
        
        <ajaxToolKit:TabPanel ID="tabpnlAdditionalInfo" runat="server">
            <HeaderTemplate>Additional Info</HeaderTemplate>      
            <ContentTemplate>
                <div><uc1:spacer id="Spacer19" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>	  
                <div align="left"><asp:Button ID="btnEditAddtionalInfo" runat="server" CssClass="Button" OnClick="EditAdditionalInfo_Click" Text="Edit Additional Info" /></div>
                <h4>Product Features</h4>
	            <div class="Features">
                     <asp:Label ID="lblProductFeatures" runat="server"></asp:Label>
                </div>
                
                <h4>Product Specification</h4>
                <div class="Features">
                     <asp:Label ID="lblproductspecification" runat="server"></asp:Label>
                </div>
                
                <h4>Shipping Information</h4>
                <div class="Features">
                     <asp:Label ID="lbladditionalinfo" runat="server"></asp:Label>
                </div> 
            </ContentTemplate>
        </ajaxToolKit:TabPanel>
        
       
        
    </ajaxToolKit:TabContainer>
    
</asp:Content>


