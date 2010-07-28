<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="add_Addonvalues.aspx.cs" Inherits="Admin_Secure_catalog_product_addons_add_Addonvalues" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div class="Form">
          <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <h1><asp:Label ID="lblTitle" Runat="server"></asp:Label></h1> <uc2:DemoMode ID="DemoMode1" runat="server" />
                        <div><uc1:spacer id="Spacer3" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>
                        <div><asp:Label ID="lblAddonValueMsg" CssClass="Error" EnableViewState="false" runat="server"></asp:Label></div>	    
    	            </td>
    	            <td>
    	                <div align="right">
                            <asp:button class="Button" ValidationGroup="grpAddOnValue" id="btnTopSubmit" CausesValidation="True" Text="Submit" Runat="server" onclick="btnAddOnValueSubmit_Click"></asp:button>				          
                            <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="Cancel" Runat="server" onclick="btnCancel_Click"></asp:button>
                        </div>
    	            </td>
    	        </tr> 
	        </table>
    	                	                
            <h4>General Settings</h4>
             
            <div class="FieldStyle">Label<span class="Asterix">*</span></div>
            <small>Enter the label for this option value (Ex : "Fire engine red").</small>
            <div><asp:RequiredFieldValidator ValidationGroup="grpAddOnValue" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddOnValueName" ErrorMessage="Enter Product Name" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator> </div>
            <div class="ValueStyle"><asp:TextBox ID="txtAddOnValueName" runat="server"></asp:TextBox></div>
             
            <div class="FieldStyle">Retail Price<span class="Asterix">*</span></div>
            <small>Enter the retail price for this product Add-On.</small>
            <div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="grpAddOnValue" runat="server" ErrorMessage="Enter a Retail Price for this Product" ControlToValidate="txtAddOnValueRetailPrice" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" ValidationGroup="grpAddOnValue" runat="server" ControlToValidate="txtAddOnValueRetailPrice" Type="Currency" Operator="DataTypeCheck" ErrorMessage="You must enter a valid Retail Price (ex: 123.45)" CssClass="Error" Display="Dynamic" />                        
				<asp:RangeValidator ID="RangeValidator1" ValidationGroup="grpAddOnValue" runat="server" ControlToValidate="txtAddOnValueRetailPrice" ErrorMessage="Enter a price between 0-999999" MaximumValue="999999" MinimumValue="-999999" Type="Currency" Display="Dynamic"></asp:RangeValidator>
            </div>
            <div class="ValueStyle"><%= ZNode.Libraries.ECommerce.Catalog.ZNodeCurrencyManager.GetCurrencyPrefix() %>&nbsp;<asp:TextBox Text="0" ID="txtAddOnValueRetailPrice" runat="server" MaxLength="7"></asp:TextBox></div>
            
            <div class="FieldStyle">Sale Price</div>
            <small>Enter the sale price for this product Add-On.</small>
            <div><asp:RangeValidator CssClass="Error" ID="RangeValidator3" ValidationGroup="grpAddOnValue" runat="server" ControlToValidate="txtSalePrice" ErrorMessage="Enter a sale price between 0-999999" MaximumValue="999999" MinimumValue="-999999" Type="Currency" Display="Dynamic"></asp:RangeValidator></div> 
            <div class="ValueStyle"><%= ZNode.Libraries.ECommerce.Catalog.ZNodeCurrencyManager.GetCurrencyPrefix() %>&nbsp;<asp:TextBox ID="txtSalePrice" runat="server" MaxLength="7"></asp:TextBox></div>
            
            <div class="FieldStyle">Wholesale Price</div>
            <small>Enter the wholesale price for this product Add-On.</small>
            <div><asp:RangeValidator CssClass="Error" ID="RangeValidator4" ValidationGroup="grpAddOnValue" runat="server" ControlToValidate="txtWholeSalePrice" ErrorMessage="Enter a wholesaleprice between 0-999999" MaximumValue="999999" MinimumValue="-999999" Type="Currency" Display="Dynamic"></asp:RangeValidator></div> 
            <div class="ValueStyle"><%= ZNode.Libraries.ECommerce.Catalog.ZNodeCurrencyManager.GetCurrencyPrefix() %>&nbsp;<asp:TextBox ID="txtWholeSalePrice" runat="server" MaxLength="7"></asp:TextBox></div>
            
            <div class="FieldStyle">Select Supplier</div>
            <small>Select where you get this product Add-On from.</small>       
            <div class="ValueStyle"><asp:DropDownList ID="ddlSupplier" runat="server" /></div>
            
            <div class="FieldStyle">Tax Class</div>
            <small>Select tax class for this product Add-on.</small>   
            <div class="ValueStyle"><asp:DropDownList ID="ddlTaxClass" runat="server" /></div>
            
            <h4>Display Settings</h4>
            
            <div class="FieldStyle">Display Order<span class="Asterix">*</span></div> 	    
            <div class="HintStyle">Enter a number. Items with a lower number are displayed first on the page.</div>                
            <div class="ValueStyle">
                <asp:TextBox ID="txtAddonValueDispOrder" runat="server" MaxLength="9" Columns="5"></asp:TextBox>
                <asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" Display="Dynamic" ValidationGroup="grpAddOn" ErrorMessage="* Enter a Display Order" ControlToValidate="txtAddonValueDispOrder"></asp:requiredfieldvalidator>
                <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="txtAddonValueDispOrder" Display="Dynamic" ValidationGroup="grpAddOn"
                ErrorMessage="Enter a whole number." MaximumValue="999999999" MinimumValue="1" Type="Integer"></asp:RangeValidator>
            </div>      
           
            <div>            
            <asp:CheckBox ID="chkIsDefault" runat="server" Text="" />
            Check here to make this the default selected item in the list.
            </div>
            
            <h4>Inventory Settings</h4>

            <div class="FieldStyle">SKU or Part#<span class="Asterix">*</span></div>
            <div><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtAddOnValueSKU" CssClass="Error" ValidationGroup="grpAddOnValue" Display="Dynamic" ErrorMessage="Enter a Valid SKU or Part#"></asp:RequiredFieldValidator></div>
            <div class="ValueStyle"><asp:TextBox ID="txtAddOnValueSKU" runat="server" MaxLength="100"></asp:TextBox></div>
            
            <div class="FieldStyle">Quantity On Hand<span class="Asterix">*</span></div>
            <div>
                <asp:RangeValidator ValidationGroup="grpAddOnValue" ID="RangeValidator2" runat="server" ControlToValidate="txtAddOnValueQuantity" CssClass="Error" Display="Dynamic" ErrorMessage="Enter a number between 0-999999" MaximumValue="999999" MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                <asp:RequiredFieldValidator ValidationGroup="grpAddOnValue" ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAddOnValueQuantity" CssClass="Error" Display="Dynamic" ErrorMessage="You Must Enter a Valid Quantity"></asp:RequiredFieldValidator>
            </div>
            <div class="ValueStyle"><asp:TextBox ID="txtAddOnValueQuantity" runat="server" Rows="3">9999</asp:TextBox></div> 
            
            <div class="FieldStyle">Re-Order Level</div>
            <div>
                <asp:RangeValidator ValidationGroup="grpAddOnValue" ID="RangeValidator5" runat="server" ControlToValidate="txtReOrder" CssClass="Error" Display="Dynamic" ErrorMessage="Enter a number between 0-999999" MaximumValue="999999" MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>                               
            </div>
            <div class="ValueStyle"><asp:TextBox ID="txtReOrder" runat="server"></asp:TextBox></div>
            
            
            <h4>Shipping Settings</h4>
            
            <div class="FieldStyle">Free Shipping</div>
            <div class="HintStyle">This applies only for custom shipping options</div>
            <div class="ValueStyle"><asp:CheckBox ID="chkFreeShippingInd" Text="Check this box to enable free shipping for this product. All other shipping rules will be ignored."  runat="server" /></div>        
            
            <div class="FieldStyle">Select Shipping Type<span class="Asterix">*</span></div>
            <small>This setting determines the shipping rules that will be applied to this product. For ex: if you select "Flat Rate" then shipping will be calculated based on a flat rate per item.</small>
            <div class="ValueStyle"><asp:DropDownList ID="ShippingTypeList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ShippingTypeList_SelectedIndexChanged"></asp:DropDownList></div>
            
            <div class="FieldStyle">Item Weight</div>
            <small>Leave blank if this does not apply. Note that the weight can be used to determine shipping cost.</small>
            <div><asp:CompareValidator ID="CompareValidator3" ValidationGroup="grpAddOnValue" runat="server" ControlToValidate="txtAddOnValueWeight" Type="Currency" Operator="DataTypeCheck" ErrorMessage="You must enter a valid Weight(ex: 2.5)" CssClass="Error" Display="Dynamic" /></div>
            <div class="ValueStyle"><asp:TextBox ID="txtAddOnValueWeight" runat="server" Width="46px"></asp:TextBox>&nbsp;<%= ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.WeightUnit %>            
                <asp:RangeValidator Enabled="false" ID="weightBasedRangeValidator" runat="server" ControlToValidate="txtAddOnValueWeight" CssClass="Error"
                Display="Dynamic" ErrorMessage="This Shipping Type requires that Weight be greater than 0."
                MaximumValue="9999999" MinimumValue="0.1" CultureInvariantValues="true" Type="Double" ValidationGroup="grpAddOnValue"></asp:RangeValidator>
                <asp:RequiredFieldValidator Enabled="false" ID="RequiredForWeightBasedoption" runat="server" ErrorMessage="You must enter weight for this Shipping Type." ControlToValidate="txtAddOnValueWeight" CssClass="Error" Display="Dynamic" ValidationGroup="grpAddOnValue"></asp:RequiredFieldValidator>
            </div>
            
            <div class="FieldStyle">Height</div>
            <small>Leave blank if this does not apply. Note that the height can be used to determine shipping cost.</small>
            <div><asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="Height" Type="Currency" Operator="DataTypeCheck" ErrorMessage="You must enter a valid Add-onvalue height(ex: 2.5)" CssClass="Error" Display="Dynamic" /></div>
            <div class="ValueStyle"><asp:TextBox ID="Height" runat="server" Width="46px"></asp:TextBox>&nbsp;<%= ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.DimensionUnit %></div>
            
            <div class="FieldStyle">Width</div>
            <small>Leave blank if this does not apply. Note that the width can be used to determine shipping cost.</small>
            <div><asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="Width" Type="Currency" Operator="DataTypeCheck" ErrorMessage="You must enter a valid Add-onvalue width(ex: 2.5)" CssClass="Error" Display="Dynamic" /></div>
            <div class="ValueStyle"><asp:TextBox ID="Width" runat="server" Width="46px"></asp:TextBox>&nbsp;<%= ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.DimensionUnit %></div>
            
            <div class="FieldStyle">Length</div>
            <small>Leave blank if this does not apply. Note that the length can be used to determine shipping cost.</small>
            <div><asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="Length" Type="Currency" Operator="DataTypeCheck" ErrorMessage="You must enter a valid Add-onvalue length" CssClass="Error" Display="Dynamic" /></div>
            <div class="ValueStyle"><asp:TextBox ID="Length" runat="server" Width="46px"></asp:TextBox>&nbsp;<%= ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.DimensionUnit %></div>
            
            
            <h4>Recurring Billing Settings</h4>
            
            <div><small>Check this box to enable recurring billing subscription for this product add-On value.</small></div>
            <div class="ValueStyle"><asp:CheckBox ID="chkRecurringBillingInd" AutoPostBack="true" OnCheckedChanged="chkRecurringBillingInd_CheckedChanged" runat="server" Text="Recurring Billing" /></div>
            
            <asp:Panel ID="pnlRecurringBilling" runat="server" Visible="false">                
                <div class="FieldStyle">Billing Period<span class="Asterix">*</span></div>
                <small>Select a value for billing during this subscription period.</small>
                <div class="ValueStyle">
                     <asp:DropDownList ID="ddlBillingPeriods" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlBillingPeriods_SelectedIndexChanged">
                        <asp:ListItem Text="Day(s)" Value="DAY"></asp:ListItem>
                        <asp:ListItem Text="Weekly" Value="WEEK"></asp:ListItem>                        
                        <asp:ListItem Text="Monthly" Value="MONTH"></asp:ListItem>
                        <asp:ListItem Text="Yearly" Value="YEAR"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                
                <div class="FieldStyle">Billing Frequency<span class="Asterix">*</span></div>
                <small>Select number of billing periods that make up one billing cycle.</small>
                <div class="ValueStyle"><asp:DropDownList ID="ddlBillingFrequency" runat="server"></asp:DropDownList></div>
                
                <div class="FieldStyle">Billing Cycles<span class="Asterix">*</span></div>
                <small>Enter a number of billing cycles for payment period.</small>
                <div class="ValueStyle"><asp:TextBox ID="txtSubscrptionCycles" runat="server">1</asp:TextBox></div>                
            </asp:Panel>
            
            <div><uc1:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>                    
            <p></p>
            <div>
                <asp:button class="Button" ValidationGroup="grpAddOnValue" id="btnBottomSubmit" CausesValidation="True" Text="Submit" Runat="server" onclick="btnAddOnValueSubmit_Click"></asp:button>
                <asp:button class="Button" id="btnCancelBottom" CausesValidation="False" Text="Cancel" Runat="server" onclick="btnCancel_Click"></asp:button>
            </div>           
	</div>
</asp:Content>


