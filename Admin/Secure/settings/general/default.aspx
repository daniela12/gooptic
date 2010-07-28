<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Admin_Secure_settings_storesettings" Title="Untitled Page" validateRequest="false"   %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc1" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
<div class="Form">
    <div>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td valign="middle">
                    <h1>Global Settings<uc1:DemoMode ID="DemoMode1" runat="server" />
                    </h1>
                </td>
                <td align="right">
                    <asp:Button ID="btnSubmitTop" runat="server" CausesValidation="True" class="Button"
                        OnClick="btnSubmit_Click" Text="SUBMIT" />
                    <asp:Button ID="btnCancelTop" runat="server" CausesValidation="False" class="Button"
                        OnClick="btnCancel_Click" Text="CANCEL" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div><asp:Label ID="lblMsg" runat="server" CssClass="Error"></asp:Label></div>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />    
                </td>
            </tr>
        </table>
    </div> 
    <div><ZNode:spacer id="Spacer8" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>
    
    <ajaxToolKit:TabContainer ID="tabGlobalSettings" runat="server">
        <ajaxToolKit:TabPanel ID="pnlGeneral" runat="server">        
            <HeaderTemplate>General</HeaderTemplate>
            <ContentTemplate>
                <h4>Storefront Identity</h4>
                <div class="FieldStyle">Domain Name<span class="Asterix">*</span></div>
                <div class="ValueStyle"><asp:TextBox ID="txtDomainName" runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDomainName" ErrorMessage="* Enter Domain Name" CssClass="Error" display="dynamic"></asp:RequiredFieldValidator></div>
                
                <div class="FieldStyle">Company Name<span class="Asterix">*</span></div>
                <div class="ValueStyle"><asp:TextBox ID="txtCompanyName" runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCompanyName" ErrorMessage="* Enter Company Name" CssClass="Error" display="dynamic"></asp:RequiredFieldValidator></div>

                <div class="FieldStyle">Store Name<span class="Asterix">*</span></div>
                <div class="ValueStyle"><asp:TextBox ID="txtStoreName" runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtStoreName" ErrorMessage="* Enter Store Name" CssClass="Error" display="dynamic"></asp:RequiredFieldValidator></div>

                <h4>Upload a Logo</h4>
                <table id="tblShowImage" border="0" cellpadding="0" cellspacing="10" runat="server" visible="true">
                    <tr>
                        <td><asp:Image ID="imgLogo" runat="server" /></td>
                        <td>
                            <div class="FieldStyle">Select an Option</div> 
                            &nbsp;
                            <asp:RadioButton ID="radCurrentImage" Text="Keep Current Image" runat="server" GroupName="LogoImage" AutoPostBack="True" OnCheckedChanged="radCurrentImage_CheckedChanged" Checked="True" />
                            <asp:RadioButton ID="radNewImage"  Text="Upload New Image" runat="server" GroupName="LogoImage" AutoPostBack="True" OnCheckedChanged="radNewImage_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <table  id="tblLogoUpload" border="0" cellpadding="0" cellspacing="0" width="100%" runat="server" visible="false">
                    <tr>
                        <td style="height: 41px">
                            <div class="HintStyle">Select a Logo Image</div>
                            <div class="ValueStyle">
                                <asp:FileUpload ID="UploadImage" runat="server" Width="300px" /><asp:RequiredFieldValidator CssClass="Error" ID="RequiredFieldValidator14" runat="server" ControlToValidate="UploadImage" ErrorMessage="* Select an Image to Upload" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblImageError" runat="server" CssClass="Error" ForeColor="Red" Text="Select Valid Product Image (JPEG or PNG or GIF)" Visible="False"></asp:Label>
                            </div>      
                        </td> 
                    </tr>
                </table>
                
                <h4>Security Setting</h4>
                <p>Check the box below if you want to use a Secure Certificate (SSL) for checkout. <b>Important:</b> Your storefront
                will fail to function if a valid certificate is not installed. Contact your administrator if you have doubts about this setting.<br /><br />
                </p>
                <div class="FieldStyle"><asp:CheckBox ID="chkEnableSSL" runat="server" Text="Enable SSL?" /></div>
                
                <h4>Storefront Contact Information</h4>
                <p>
                The contact information is displayed in different areas and is also used for sending email notifications. Use a "," to separate multiple email addresses.
                </p>
                 
                <div class="FieldStyle">Administrator's Email<span class="Asterix">*</span></div>
                <div class="ValueStyle"><asp:TextBox ID="txtAdminEmail" runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAdminEmail" ErrorMessage="* Enter Admin Email" CssClass="Error" display="dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator id="valRegEx" runat="server" ControlToValidate="txtAdminEmail" ValidationExpression="^((([a-zA-Z\'\.\-]+)?)((,\s*([a-zA-Z]+))?)|([A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})))(,{1}(((([a-zA-Z\'\.\-]+){1})((,\s*([a-zA-Z]+))?))|([A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})){1}))*$" ErrorMessage="* Enter a valid e-mail address." display="dynamic" CssClass="Error"></asp:RegularExpressionValidator></div>

                <div class="FieldStyle">Sales Department Email<span class="Asterix">*</span></div>
                <div class="ValueStyle"><asp:TextBox ID="txtSalesEmail" runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSalesEmail" ErrorMessage="* Enter Sales Email" CssClass="Error" display="dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtSalesEmail" ValidationExpression="^((([a-zA-Z\'\.\-]+)?)((,\s*([a-zA-Z]+))?)|([A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})))(,{1}(((([a-zA-Z\'\.\-]+){1})((,\s*([a-zA-Z]+))?))|([A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})){1}))*$"  ErrorMessage="* Enter a valid e-mail address." CssClass="Error" display="dynamic"></asp:RegularExpressionValidator></div>

                <div class="FieldStyle">Customer Service Email<span class="Asterix">*</span></div>
                <div class="ValueStyle"><asp:TextBox ID="txtCustomerServiceEmail" runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCustomerServiceEmail" ErrorMessage="* Enter Customer Service Email" CssClass="Error" display="dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator id="RegularExpressionValidator2" runat="server" ControlToValidate="txtCustomerServiceEmail" ValidationExpression="^((([a-zA-Z\'\.\-]+)?)((,\s*([a-zA-Z]+))?)|([A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})))(,{1}(((([a-zA-Z\'\.\-]+){1})((,\s*([a-zA-Z]+))?))|([A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})){1}))*$"  ErrorMessage="* Enter a valid e-mail address." display="dynamic" CssClass="Error"></asp:RegularExpressionValidator></div>

                <div class="FieldStyle">Sales Department Phone Number<span class="Asterix">*</span></div>
                <div class="ValueStyle"><asp:TextBox ID="txtSalesPhoneNumber" runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtSalesPhoneNumber" ErrorMessage="* Enter Sales Phone Number" CssClass="Error" display="dynamic"></asp:RequiredFieldValidator></div>

                <div class="FieldStyle">Customer Service Phone Number<span class="Asterix">*</span></div>
                <div class="ValueStyle"><asp:TextBox ID="txtCustomerServicePhoneNumber" runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtCustomerServicePhoneNumber" ErrorMessage="* Enter Customer Service Phone Number" CssClass="Error" display="dynamic"></asp:RequiredFieldValidator></div>
                
                <h4>Customer Review Settings</h4> 
                <div class="FieldStyle">Default Customer Review Status</div>
                <div class="HintStyle">Select the default status for customer reviews. Selecting "Active" will publish the review on the site immediately. Selecting "New" or "Inactive" will require administrator approval before they are published.</div>
                <div class="ValueStyle">
                    <asp:DropDownList ID="ListReviewStatus" runat="server">            
                        <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                        <asp:ListItem Text="Inactive" Value="I"></asp:ListItem>
                        <asp:ListItem Text="New" Value="N"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                
                <h4>Order Status Settings</h4> 
                <div class="FieldStyle">Default Order Status</div>                
                <div class="ValueStyle"><asp:DropDownList ID="ddlOrderStateList" runat="server" /></div>
                
                <h4>Tax Setting</h4>
                <div class="FieldStyle"><asp:CheckBox ID="chkInclusiveTax" runat="server" Text="Include taxes in product price" /></div>
            </ContentTemplate>
        </ajaxToolKit:TabPanel>        
    
        <ajaxToolKit:TabPanel ID="pnlDisplay" runat="server">
        <HeaderTemplate>Display</HeaderTemplate>
            <ContentTemplate>
                <h4>Grid Display Settings</h4>
                 
                <div class="FieldStyle">Maximum number of columns to display in the catalog<span class="Asterix">*</span></div>
                <div class="ValueStyle"><asp:TextBox ID="txtMaxCatalogDisplayColumns" runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMaxCatalogDisplayColumns" ErrorMessage="* Enter Max Catalog Display Columns" CssClass="Error" display="dynamic"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtMaxCatalogDisplayColumns"
                                        CssClass="Error" Display="Dynamic" ErrorMessage="* Enter a number between 1-10"
                                        MaximumValue="10" MinimumValue="1" SetFocusOnError="True" Type="Integer"></asp:RangeValidator></div>
                
                <div class="FieldStyle">Maximum number of catalog items to display per page<span class="Asterix">*</span></div>
                <div class="ValueStyle"><asp:TextBox ID="txtMaxCatalogDisplayItems" runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtMaxCatalogDisplayItems" ErrorMessage="* Enter Max Catalog Display Items" CssClass="Error" display="dynamic"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtMaxCatalogDisplayItems"
                                        CssClass="Error" Display="Dynamic" ErrorMessage="* Enter a number between 1-1000"
                                        MaximumValue="1000" MinimumValue="1" SetFocusOnError="True" Type="Integer"></asp:RangeValidator></div>                                                     
                
                <div class="FieldStyle">Maximum number of thumbnail columns<span class="Asterix">*</span></div>

                <div class="ValueStyle">
                    <asp:TextBox ID="txtMaxSmallThumbnailsDisplay" runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
                        ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtMaxSmallThumbnailsDisplay"
                        CssClass="Error" Display="dynamic" ErrorMessage="* Enter Max Small Thumbnail Image Width"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator8" runat="server" ControlToValidate="txtMaxSmallThumbnailsDisplay"
                        CssClass="Error" Display="dynamic" ErrorMessage="Enter a whole number"
                        MaximumValue="1000" MinimumValue="1" SetFocusOnError="true" Type="Integer"></asp:RangeValidator>
                 </div>
                                                         
                <h4>Image Settings</h4>

                <div class="FieldStyle">Maximum Width or Height for LARGE Image type<span class="Asterix">*</span></div>
                <div class="ValueStyle"><asp:TextBox ID="txtMaxCatalogItemLargeWidth" runat="server" Width="152px"></asp:TextBox>&nbsp;pixels&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtMaxCatalogItemLargeWidth" ErrorMessage="* Enter Max Large Image Width" CssClass="Error" display="dynamic"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtMaxCatalogItemLargeWidth"
                                        CssClass="Error" Display="Dynamic" ErrorMessage="* Enter a number between 1-1000"
                                        MaximumValue="1000" MinimumValue="1" SetFocusOnError="True" Type="Integer"></asp:RangeValidator></div>

                <div class="FieldStyle">Maximum Width or Height for MEDIUM Image type<span class="Asterix">*</span></div>
               <div class="ValueStyle"><asp:TextBox ID="txtMaxCatalogItemMediumWidth" runat="server" Width="152px"></asp:TextBox>&nbsp;pixels&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtMaxCatalogItemMediumWidth" ErrorMessage="* Enter Max Medium Image Width" CssClass="Error" display="dynamic"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtMaxCatalogItemMediumWidth"
                                        CssClass="Error" Display="Dynamic" ErrorMessage="* Enter a number between 1-1000"
                                        MaximumValue="1000" MinimumValue="1" SetFocusOnError="True" Type="Integer"></asp:RangeValidator></div>

                                        
                <div class="FieldStyle">Maximum Width or Height for SMALL Image type<span class="Asterix">*</span></div>  
                <div class="ValueStyle"><asp:TextBox ID="txtMaxCatalogItemSmallWidth" runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtMaxCatalogItemSmallWidth" ErrorMessage="* Enter Max Small Image Width" CssClass="Error" Display="dynamic"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="txtMaxCatalogItemSmallWidth"
                                        CssClass="Error" Display="dynamic" ErrorMessage="*Enter a whole number"
                                        MaximumValue="1000" MinimumValue="1" SetFocusOnError="true" Type="Integer"></asp:RangeValidator></div>
                                        
                <div class="FieldStyle">Maximum Width or Height for CROSS-SELL Image type<span class="Asterix">*</span></div>  
                <div class="ValueStyle"><asp:TextBox ID="txtMaxCatalogCrossSellWidth" runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtMaxCatalogCrossSellWidth" ErrorMessage="* Enter Max Cross sell Image Width" CssClass="Error" Display="dynamic"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator12" runat="server" ControlToValidate="txtMaxCatalogCrossSellWidth"
                                        CssClass="Error" Display="dynamic" ErrorMessage="*Enter a whole number"
                                        MaximumValue="1000" MinimumValue="1" SetFocusOnError="true" Type="Integer"></asp:RangeValidator></div>
                                      
                                        
                <div class="FieldStyle">Maximum Width or Height for THUMBNAIL Image type<span class="Asterix">*</span></div> 
                <div class="ValueStyle"><asp:TextBox ID="txtMaxCatalogItemThumbnailWidth" runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtMaxCatalogItemThumbnailWidth" ErrorMessage="* Enter Max Thumbnail Image Width" CssClass="Error" Display="dynamic"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator7" runat="server" ControlToValidate="txtMaxCatalogItemThumbnailWidth"
                                        CssClass="Error" Display="dynamic" ErrorMessage="Enter a whole number."
                                        MaximumValue="1000" MinimumValue="1" SetFocusOnError="true" Type="Integer"></asp:RangeValidator></div>
                      
                <div class="FieldStyle">Maximum Width or Height for SWATCH Image type<span class="Asterix">*</span></div>
                
                <div class="ValueStyle">
                    <asp:TextBox ID="txtMaxCatalogItemSwatchesWidth" runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
                        ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtMaxCatalogItemSwatchesWidth"
                        CssClass="Error" Display="dynamic" ErrorMessage="* Enter Max Small Thumbnail Image Width"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtMaxCatalogItemSwatchesWidth"
                        CssClass="Error" Display="dynamic" ErrorMessage="Enter a whole number"
                        MaximumValue="1000" MinimumValue="1" SetFocusOnError="true" Type="Integer"></asp:RangeValidator>
                 </div>
                 
                
                        
                        
                        
                <h4>Shop By Price Settings</h4>
                
                <div class="FieldStyle">Price Range Minimum<span class="Asterix">*</span></div>
                <div class="ValueStyle"><%= ZNode.Libraries.ECommerce.Catalog.ZNodeCurrencyManager.GetCurrencyPrefix() %><asp:TextBox ID="txtShopByPriceMin" runat="server" Width="152px">0</asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtShopByPriceMin" ErrorMessage="* Enter Minimum Price" CssClass="Error" Display="dynamic"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator9" runat="server" ControlToValidate="txtShopByPriceMin" CssClass="Error" Display="dynamic" ErrorMessage="Enter a whole number" MaximumValue="10000" MinimumValue="0" SetFocusOnError="true" Type="Integer"></asp:RangeValidator></div> 
                
                <div class="FieldStyle">Price Range Maximum<span class="Asterix">*</span></div>
                <div class="ValueStyle"><%= ZNode.Libraries.ECommerce.Catalog.ZNodeCurrencyManager.GetCurrencyPrefix() %><asp:TextBox ID="txtShopByPriceMax" runat="server" Width="152px">100</asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtShopByPriceMax" ErrorMessage="* Enter Maximum Price" CssClass="Error" Display="dynamic"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator10" runat="server" ControlToValidate="txtShopByPriceMax" CssClass="Error" Display="dynamic" ErrorMessage="Enter a whole number" MaximumValue="10000" MinimumValue="1" SetFocusOnError="true" Type="Integer"></asp:RangeValidator></div> 
                
                <div class="FieldStyle">Price Increment<span class="Asterix">*</span></div>
                <div class="ValueStyle"><%= ZNode.Libraries.ECommerce.Catalog.ZNodeCurrencyManager.GetCurrencyPrefix() %><asp:TextBox ID="txtShopByPriceIncrement" runat="server" Width="152px">20</asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtShopByPriceIncrement" ErrorMessage="* Enter Incremental Price" CssClass="Error" Display="dynamic"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator11" runat="server" ControlToValidate="txtShopByPriceIncrement" CssClass="Error" Display="dynamic" ErrorMessage="Enter a whole number." MaximumValue="1000" MinimumValue="1" SetFocusOnError="true" Type="Integer"></asp:RangeValidator></div> 
             
           </ContentTemplate>
        </ajaxToolKit:TabPanel>            
    
        <ajaxToolKit:TabPanel ID="pnlAdvanced" HeaderText="Units" runat="server">
            <ContentTemplate>
                <h4>Unit settings</h4>
                <div class="FieldStyle">Weight Unit</div>
                <div class="ValueStyle">
                    <asp:DropDownList ID="ddlWeightUnits" runat="server">
                        <asp:ListItem Selected="True" Text="LBS"></asp:ListItem>
                        <asp:ListItem Text="KGS"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                
                <div class="FieldStyle">Dimensions Unit</div>
                <div class="ValueStyle">
                    <asp:DropDownList ID="ddlDimensions" runat="server">
                        <asp:ListItem Selected="True" Text="IN"></asp:ListItem>
                        <asp:ListItem Text="CM"></asp:ListItem>
                    </asp:DropDownList>
                </div>               
                
                
                <h4>Currency Settings</h4>
                
                <asp:ScriptManager id="ScriptManager" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="updPnlcurrencySettings" runat="server" UpdateMode="Conditional">
	                <ContentTemplate>
                    <div class="FieldStyle">Active Currency</div>
                    <div class="ValueStyle"><asp:DropDownList id="ddlCurrencyTypes" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrencyTypes_SelectedIndexChanged"></asp:DropDownList></div>
                    <div class="FieldStyle">Currency Suffix</div><div class="ValueStyle"><asp:TextBox id="txtCurrencySuffix" runat="server" AutoPostBack="True" OnTextChanged="txtCurrencySuffix_TextChanged"></asp:TextBox></div>
                    <div class="FieldStyle">Preview: <asp:Label id="lblPrice" runat="server"></asp:Label></div>
                    </ContentTemplate>
                </asp:UpdatePanel>        
            </ContentTemplate>
        </ajaxToolKit:TabPanel>
    
        <ajaxToolKit:TabPanel HeaderText="Shipping" runat="server">
            <ContentTemplate>                                 
                <h4>Shipping Origin Settings</h4>                
                <div class="FieldStyle">Shipping Origin Address 1</div>
                <div class="ValueStyle"><asp:TextBox ID="txtShippingAddress1" runat="server" Width="152px"></asp:TextBox></div>
                
                <div class="FieldStyle">Shipping Origin Address 2</div>
                <div class="ValueStyle"><asp:TextBox ID="txtShippingAddress2" runat="server" Width="152px"></asp:TextBox></div>
                
                <div class="FieldStyle">Shipping Origin City</div>
                <div class="ValueStyle"><asp:TextBox ID="txtShippingCity" runat="server" Width="152px"></asp:TextBox></div>
                
                <div class="FieldStyle">Shipping Origin State Code</div>
                <div class="ValueStyle"><asp:TextBox ID="txtShippingStateCode" runat="server" Width="152px"></asp:TextBox></div>
                
                <div class="FieldStyle">Shipping Origin Zip Code</div>
                <div class="ValueStyle"><asp:TextBox ID="txtShippingZipCode" runat="server" Width="152px"></asp:TextBox></div>

                <div class="FieldStyle">Shipping Origin Country Code</div>
                <div class="ValueStyle"><asp:TextBox ID="txtShippingCountryCode" runat="server" Width="152px"></asp:TextBox></div>
                   
                   <div class="FieldStyle">Shipping Origin Phone</div>
                <div class="ValueStyle"><asp:TextBox ID="txtShippingPhone" runat="server" Width="152px"></asp:TextBox></div>
                
                <h4>FedEx Settings</h4>
                <p>Required to retrieve FedEx shipping rates. You would need to signup for an API account with FedEx&reg;.<br /><br /></p>
                
                <div class="FieldStyle">FedEx&reg; Account Number</div>
                <div class="ValueStyle"><asp:TextBox ID="txtAccountNum" runat="server" Width="152px"></asp:TextBox></div>
             
                <div class="FieldStyle">FedEx&reg; Meter Number</div>
                <div class="ValueStyle"><asp:TextBox ID="txtMeterNum" runat="server" Width="152px"></asp:TextBox></div>
                
                <div class="FieldStyle">FedEx Production key</div>
                <div class="ValueStyle"><asp:TextBox ID="txtProductionAccessKey" runat="server" Width="152px"></asp:TextBox></div>
                
                <div class="FieldStyle">FedEx Security Code</div>
                <div class="ValueStyle"><asp:TextBox ID="txtSecurityCode" runat="server" Width="152px"></asp:TextBox></div>                 
                
                <div class="FieldStyle">Select FedEx Drop-Off Type</div>
                <div class="ValueStyle">
                    <asp:DropDownList id="ddldropOffTypes" runat="server">
	                    <asp:ListItem value="BUSINESS_SERVICE_CENTER">BUSINESS SERVICE CENTER</asp:ListItem>
	                    <asp:ListItem value="DROP_BOX">DROP BOX</asp:ListItem>
	                    <asp:ListItem selected="true" value="REGULAR_PICKUP">REGULAR PICKUP</asp:ListItem>
                	    <asp:ListItem value="REQUEST_COURIER">REQUEST COURIER</asp:ListItem>
	                    <asp:ListItem value="STATION">STATION</asp:ListItem>	                
                    </asp:DropDownList>                
                </div> 
                
                <div class="FieldStyle">Select FedEx Packaging Type</div>
                <div class="ValueStyle">
                    <asp:DropDownList id="ddlPackageTypeCodes" runat="server">
	                    <asp:ListItem  Selected="true" value="YOUR_PACKAGING">Your Packaging</asp:ListItem>
	                    <asp:ListItem value="FEDEX_10KG_BOX" Enabled="false">FedEx&reg; 10KG Box</asp:ListItem>
	                    <asp:ListItem value="FEDEX_25KG_BOX" Enabled="false">FedEx&reg; 25KG Box</asp:ListItem>
	                    <asp:ListItem value="FEDEX_BOX">FedEx&reg; Box</asp:ListItem>
	                    <asp:ListItem value="FEDEX_ENVELOPE">FedEx&reg; Envelope</asp:ListItem>
	                    <asp:ListItem value="FEDEX_TUBE">FedEx&reg; Tube</asp:ListItem>
	                    <asp:ListItem value="FEDEX_PAK">FedEx&reg; Pak</asp:ListItem>
                    </asp:DropDownList>                
                </div>  
                
                <div class="FieldStyle"><asp:CheckBox ID="chkFedExDiscountRate" runat="server" Text="Use FedEx Discount Rate" /></div>
                <br />
                <div class="FieldStyle"><asp:CheckBox ID="chkAddInsurance" runat="server" Text="Add Insurance" /></div>
                                
                
                <h4>UPS Settings</h4>
                <p>Required to retrieve UPS shipping rates. You would need to signup for an API account with UPS.<br /><br /></p>
                
                <div class="FieldStyle">UPS User Name</div>
                <div class="ValueStyle"><asp:TextBox ID="txtUPSUserName" runat="server" Width="152px"></asp:TextBox></div>
             
                <div class="FieldStyle">UPS Password</div>
                <div class="ValueStyle"><asp:TextBox ID="txtUPSPassword" runat="server" Width="152px"></asp:TextBox></div>
                
                <div class="FieldStyle">UPS Access key</div>
                <div class="ValueStyle"><asp:TextBox ID="txtUPSKey" runat="server" Width="152px"></asp:TextBox></div>   
                
            </ContentTemplate>
        </ajaxToolKit:TabPanel>    
     
        <ajaxToolKit:TabPanel ID="pnlGoogleAnalystics" HeaderText="Analytics Code" runat="server">
            <ContentTemplate>
            <h4>Analytics Code Settings</h4>
                 <div class="FieldStyle">
                    Site Wide Javascript (Top)</div>
                <div class="HintStyle">
                    The contents of this field will be added to the top of every page on the site.</div>
                <div class="ValueStyle">
                    <asp:TextBox ID="txtSiteWideTopJavaScript" runat="server" Height="150px" TextMode="MultiLine" Width="450px"></asp:TextBox><br />
                </div>
              
                <div class="FieldStyle">Site Wide Javascript (Bottom)</div>
                <div class="HintStyle">
                    The contents of this field will be added to the bottom of every page on the site.</div>
                <div class="ValueStyle">
                    <asp:TextBox ID="txtSiteWideBottomJavaScript" runat="server" Height="150px" TextMode="MultiLine" Width="450px"></asp:TextBox><br />
                </div>
                
                <div class="FieldStyle">
                    Site Wide Javascript (except Order Receipt page)</div>
                <div class="HintStyle">
                    The contents of this field will be placed at the bottom of every page on the site <strong>except</strong> the receipt page.</div>
                <div class="ValueStyle">
                    <asp:TextBox ID="txtSiteWideAnalyticsJavascript" runat="server" Height="150px" TextMode="MultiLine" Width="450px"></asp:TextBox><br />
                </div>
                
                <div class="FieldStyle">
                    Order Receipt Javascript</div>
                <div class="HintStyle">
                    The contents of this field will be added to the order receipt page.</div>
                <div class="ValueStyle">
                    <asp:TextBox ID="txtOrderReceiptJavaScript" runat="server" Height="150px" TextMode="MultiLine" Width="450px"></asp:TextBox><br />
                </div>
            </ContentTemplate>
        </ajaxToolKit:TabPanel>    
        
               
        <ajaxToolKit:TabPanel ID="pnlSMTPSettings" HeaderText="SMTP" runat="server">
            <ContentTemplate>
    
                <h4>Mail Server Settings</h4>
                <p>The following settings are required to send email receipts and notifications from your storefront.<br /><br /></p>
    
                <div class="FieldStyle">SMTP Server</div>
                <div class="ValueStyle"><asp:TextBox ID="txtSMTPServer" runat="server" Width="152px"></asp:TextBox></div>

                <div class="FieldStyle">SMTP Server UserName</div>
                <div class="ValueStyle"><asp:TextBox ID="txtSMTPUserName" runat="server" Width="152px"></asp:TextBox></div>

                <div class="FieldStyle">SMTP Server Password</div>
                <div class="ValueStyle"><asp:TextBox ID="txtSMTPPassword" runat="server" Width="152px"></asp:TextBox></div>
                
            </ContentTemplate>
        </ajaxToolKit:TabPanel>
        
        
        
    </ajaxToolKit:TabContainer>
    
    <div><ZNode:spacer id="Spacer1" SpacerHeight="15" SpacerWidth="3" runat="server"></ZNode:spacer></div>
    
    <div>
        <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
        <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
    </div>	
</div> 
</asp:Content>

