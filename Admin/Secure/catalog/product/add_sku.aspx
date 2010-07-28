<%@ Page Language="C#"  MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="add_sku.aspx.cs" Inherits="Admin_Secure_catalog_product_add_sku" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div class="Form">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <h1>
                        <asp:Label ID="lblHeading" runat="server" />
                        <uc1:DemoMode id="DemoMode1" runat="server">
                        </uc1:DemoMode></h1>
                </td>
                <td align="right">
                    <asp:Button ID="btnSubmitTop" runat="server" CausesValidation="True" class="Button"
                        OnClick="btnSubmit_Click" Text="SUBMIT" />
                    <asp:Button ID="btnCancelTop" runat="server" CausesValidation="False" class="Button"
                        OnClick="btnCancel_Click" Text="CANCEL" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblError" CssClass= "Error" runat="server"></asp:Label>
        <div class="FieldStyle">&nbsp;</div>
        <div class="FieldStyle">SKU or Part#<span class="Asterix">*</span></div>
        <div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="SKU"
                Display="Dynamic" ErrorMessage="* Enter a valid SKU" SetFocusOnError="True" CssClass="Error"></asp:RequiredFieldValidator>
        </div>
        <div class="ValueStyle">
            <asp:TextBox ID="SKU" runat="server" Columns="30" MaxLength="100"></asp:TextBox>
        </div>
        <div class="FieldStyle">
            Quantity On Hand<span class="Asterix">*</span>
        </div>
        <p>
            Quantity should be a number between 1-9999</p>
        <div>
            <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ControlToValidate="Quantity"
                Display="Dynamic" ErrorMessage="* Enter a valid Quantity" CssClass="Error"></asp:RequiredFieldValidator>
        </div>
        <div class="ValueStyle">
            <asp:TextBox ID="Quantity" runat="server" Columns="5" MaxLength="4">1</asp:TextBox>&nbsp;
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="Quantity"
                Display="Dynamic" ErrorMessage="* Enter a number between 1-9999" MaximumValue="9999"
                MinimumValue="0" Type="Integer" CssClass="Error"></asp:RangeValidator>
        </div> 
        <div class="FieldStyle">Re-Order Level</div> 
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="ReOrder" Display="Dynamic" ErrorMessage="Enter a valid ReOrder level. Only numbers are allowed" SetFocusOnError="true" ValidationExpression="(^N/A$)|(^[-]?(\d+)(\.\d{0,3})?$)|(^[-]?(\d{1,3},(\d{3},)*\d{3}(\.\d{1,3})?|\d{1,3}(\.\d{1,3})?)$)" CssClass="Error"></asp:RegularExpressionValidator>      
        <div class="ValueStyle">
        <asp:TextBox ID="ReOrder" runat="server" Columns="5" MaxLength="3"></asp:TextBox>
        </div>
        <div class="FieldStyle">Additional Weight</div>           
        <asp:CompareValidator ID="CompareValidator4" SetFocusOnError="true" runat="server" ControlToValidate="WeightAdditional" Type="Double" Operator="DataTypeCheck" ErrorMessage="* Enter a valid additional weight. Only numbers are allowed" CssClass="Error" Display="Dynamic" />
        <div class="ValueStyle"><asp:TextBox ID="WeightAdditional" runat="server" Columns="10" MaxLength="7"></asp:TextBox></div>
        
        <div class="FieldStyle">Retail Price</div>
        <div class="HintStyle">If you provide a retail price, this will override the product's retail price.</div>
        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="RetailPrice" Type="Currency" Operator="DataTypeCheck" ErrorMessage="* Enter a valid override retail Price. Only numbers are allowed" CssClass="Error" Display="Dynamic" />
        <div class="ValueStyle"><asp:TextBox ID="RetailPrice" runat="server" Columns="10" MaxLength="7"></asp:TextBox></div>
        
        <div class="FieldStyle">Sale Price</div>
        <div class="HintStyle">If you provide a sale price, this will override the product's sale price.</div>
        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="SalePrice" Type="Currency" Operator="DataTypeCheck" ErrorMessage="* Enter a valid override sale Price. Only numbers are allowed" CssClass="Error" Display="Dynamic" />
        <div class="ValueStyle"><asp:TextBox ID="SalePrice" runat="server" Columns="10" MaxLength="7"></asp:TextBox></div>            
        
        <div class="FieldStyle">Wholesale Price</div>
        <div class="HintStyle">If you provide a wholesale price, this will override the product's wholesale price.</div>
        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="WholeSalePrice" Type="Currency" Operator="DataTypeCheck" ErrorMessage="* Enter a valid override wholesale Price. Only numbers are allowed" CssClass="Error" Display="Dynamic" />
        <div class="ValueStyle"><asp:TextBox ID="WholeSalePrice" runat="server" Columns="10" MaxLength="7"></asp:TextBox></div>
        
        <div class="FieldStyle">Enable Inventory</div>
	    <div class="ValueStyle"><asp:CheckBox ID='VisibleInd' runat='server' Text="Check this box to enable this inventory" Checked="true"></asp:CheckBox></div>        
        <div class="FieldStyle" id="DivAttributes" runat="Server">
            Product Attributes<span class="Asterix">*</span>
        </div>
        <div class="ValueStyle">
            <asp:PlaceHolder id="ControlPlaceHolder" runat="server"></asp:PlaceHolder>
        </div>  
        <div class="FieldStyle">Select Supplier</div>   
         <small>Select where you get this product from.</small>    
        <div class="ValueStyle"><asp:DropDownList ID="ddlSupplier" runat="server" /></div>
        
        <h4>SKU Image</h4>
        <small>Upload a suitable image for this SKU. Only JPG, GIF and PNG images are supported. The file size should be less than 1.5 Meg. Your image will automatically be scaled so it displays correctly.</small>
         <table id="tblShowImage" border="0" cellpadding="0" cellspacing="20" runat="server" visible="false">
           <tr>
             <td><asp:Image ID="SKUImage" runat="server" /></td>
             <td>
               <div class="FieldStyle">Select an Option</div> 
               <div class="ValueStyle">
               <asp:RadioButton ID="RadioSKUCurrentImage" Text="Keep Current Image" runat="server" GroupName="SKU Image" AutoPostBack="True" OnCheckedChanged="RadioSKUCurrentImage_CheckedChanged" Checked="True" />
               <asp:RadioButton ID="RadioSKUNewImage"  Text="Upload New Image" runat="server" GroupName="SKU Image" AutoPostBack="True" OnCheckedChanged="RadioSKUNewImage_CheckedChanged" />
               <asp:RadioButton ID="RadioSKUNoImage"  Text="No Image" runat="server" GroupName="SKU Image" AutoPostBack="True" OnCheckedChanged="RadioSKUNoImage_CheckedChanged" />
               </div>
             </td>
          </tr>
         </table>                    
         <table  id="tblSKUDescription" border="0" cellpadding="0" cellspacing="0" width="100%" runat="server" visible="false">
          <tr>
            <td>
               <div>
               <asp:Label ID="lblSKUImageError" runat="server" CssClass="Error" ForeColor="Red" Text="" Visible="False"></asp:Label>
               </div>
               <div class="ValueStyle">Select an Image:&nbsp;<asp:FileUpload ID="UploadSKUImage" runat="server" Width="300px" BorderStyle="Inset" EnableViewState="true" />
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="UploadSKUImage" CssClass="Error" 
                  Display="dynamic" ValidationExpression=".*(\.[Jj][Pp][Gg]|\.[Gg][Ii][Ff]|\.[Jj][Pp][Ee][Gg]|\.[Pp][Nn][Gg])" 
                  ErrorMessage="Please select a valid JPEG, JPG, PNG or GIF image"></asp:RegularExpressionValidator>
               </div>
            </td> 
         </tr>
        </table>
        
        <div class="FieldStyle">Product Image ALT Text</div>
        <small>Enter short descriptive text for this product to be used in the image ALT text. This text is displayed if the image does not download correctly.</small>
        <div class="ValueStyle"><asp:TextBox ID="txtImageAltTag" runat="server"></asp:TextBox></div>
        
        
        <asp:Panel ID="pnlRecurringBilling" runat="server" Visible="false">
            <h4>Recurring Billing</h4>
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
            
        <div>
            <asp:Button ID="btnSubmit" runat="server" CausesValidation="True" class="Button"
                OnClick="btnSubmit_Click" Text="SUBMIT" />
            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" class="Button"
                OnClick="btnCancel_Click" Text="CANCEL" />
        </div>
    </div>
</asp:Content>

