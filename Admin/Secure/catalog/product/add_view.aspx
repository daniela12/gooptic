<%@ Page Language="C#"  MasterPageFile="~/Admin/Themes/Standard/edit.master"AutoEventWireup="true" CodeFile="add_view.aspx.cs" Inherits="Admin_Secure_catalog_product_add_view" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div class="Form">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td><h1><asp:Label ID="lblHeading" runat="server" /><uc1:DemoMode id="DemoMode1" runat="server"></uc1:DemoMode></h1></td>
                <td align="right">
                    <asp:Button ID="btnSubmitTop" runat="server" CausesValidation="True" class="Button" OnClick="btnSubmit_Click" Text="SUBMIT" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancelTop" runat="server" CausesValidation="False" class="Button" OnClick="btnCancel_Click" Text="CANCEL" />
                </td>
            </tr>
        </table>
        
        <div><ZNode:Spacer id="Spacer1" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:Spacer></div>
        <p>This Product Image will be displayed on the product page as a thumbnail. Your customers can click on this thumbnail to see a larger version. Use this image to show different views of your product</p>
        
        <div><ZNode:Spacer id="Spacer2" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:Spacer></div>
        <asp:Label ID="lblError" runat="server"></asp:Label>
        <div><ZNode:Spacer id="Spacer3" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:Spacer></div>        
        
        <div class="FieldStyle">Title</div>
        <small>Enter the title to be displayed for this product image. Leave blank for no title.</small>
        <div class="ValueStyle"><asp:TextBox ID="txttitle" runat="server" Columns="30" MaxLength="30"></asp:TextBox></div>
        
        <div class="FieldStyle">Image Type</div>
        <small>Select a type for this Image. Set to Alternate Image to show a small version of the image. Set to Swatch to show a small detailed section of the image.</small>
        <div class="ValueStyle"><asp:DropDownList ID="ImageType" runat="server" OnSelectedIndexChanged="ImageType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
        
        <div class="FieldStyle"><asp:Label ID ="lblImage" runat="server"></asp:Label><span class="Asterix">*</span></div>
        <div id="ProductHint" runat="server" visible="false"><small>Upload a suitable image for your product. Only JPG, GIF and PNG images are supported. The file size should be less than 1.5 Meg. Your image will automatically be scaled so it displays correctly in the catalog.</small></div>
        <div id="SwatchHint" runat="server" visible="false"><small>Upload a suitable image for your product swatch. Only JPG, GIF and PNG images are supported. The file size should be less than 1.5 Meg. Your image will automatically be cropped so it displays correctly in the catalog.</small></div>
        
        <table  id="tblShowImage" border="0" cellpadding="0" cellspacing="20" runat="server" visible="false">
            <tr>
                <td><asp:Image ID="Image1" runat="server" /></td>
                <td>
                    <div class="FieldStyle">Select an Option</div> 
                    <div class="ValueStyle">
                        <asp:RadioButton ID="RadioProductCurrentImage" Text="Keep Current Image" runat="server" GroupName="Product Image" AutoPostBack="True" OnCheckedChanged="RadioProductCurrentImage_CheckedChanged" Checked="True" />
                        <asp:RadioButton ID="RadioProductNewImage"  Text="Upload New Image" runat="server" GroupName="Product Image" AutoPostBack="True" OnCheckedChanged="RadioProductNewImage_CheckedChanged" />
                    </div>
                </td>
            </tr>
        </table>
        <table  id="tblProductDescription" border="0" cellpadding="0" cellspacing="0" width="100%" runat="server" visible="false">
            <tr>
                <td>
                   <div>
                        <asp:RequiredFieldValidator CssClass="Error" ID="RequiredFieldValidator4" runat="server" ControlToValidate="UploadProductImage" ErrorMessage="Select Image" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblProductImageError" runat="server" CssClass="Error" ForeColor="Red" Text="" Visible="False"></asp:Label>
                   </div>
                   <div class="ValueStyle">Select an Image:&nbsp;<asp:FileUpload ID="UploadProductImage" runat="server" Width="300px" BorderStyle="Inset" EnableViewState="true" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="UploadProductImage" CssClass="Error" 
                                   Display="dynamic" ValidationExpression=".*(\.[Jj][Pp][Gg]|\.[Gg][Ii][Ff]|\.[Jj][Pp][Ee][Gg]|\.[Pp][Nn][Gg])" 
                                   ErrorMessage="Please select a valid JPEG, JPG, PNG or GIF image"></asp:RegularExpressionValidator>
                   </div>
                </td> 
            </tr>
        </table>
        <div class="FieldStyle" visible="false" id="ImagefileName" runat="server"><asp:Label ID ="lblImageName" runat="server"></asp:Label>
            <div class="ValueStyle"><asp:TextBox ID="txtimagename" runat="server" Visible="false" Columns="30" MaxLength="30"></asp:TextBox></div>
        </div>
        
        <asp:Panel ID="ProductSwatchPanel" runat="server" Visible ="false">
            <div class="FieldStyle"><asp:Label ID ="lblProductSwatchImage" runat="server" Text="Product Image"></asp:Label></div>
            <div><small>Upload a product image that will be displayed when the swatch is clicked. Leave this blank to show a larger version of your swatch image. Only JPG, GIF and PNG images are supported. The file size should be less than 1.5 Meg. Your image will automatically be scaled so it displays correctly in the catalog.</small></div>
		
		    <table  id="tblShowProductSwatchImage" border="0" cellpadding="0" cellspacing="20" runat="server" visible="false">
            <tr>
                <td><asp:Image ID="ProductSwatchImage" runat="server" /></td>
                <td>
                    <div class="FieldStyle">Select an Option</div> 
                    <div class="ValueStyle">
                        <asp:RadioButton ID="RbtnProductSwatchCurrentImage" Text="Keep Current Image" runat="server" GroupName="Product Swatch Image" AutoPostBack="True" OnCheckedChanged="RbtnProductSwatchCurrentImage_CheckedChanged" Checked="true"/>
                        <asp:RadioButton ID="RbtnProductSwatchNewImage"  Text="Upload New Image" runat="server" GroupName="Product Swatch Image" AutoPostBack="True" OnCheckedChanged="RbtnProductSwatchNewImage_CheckedChanged" />
                        <asp:RadioButton ID="RbtnProductSwatchNoImage"  Text="No Image" runat="server" GroupName="Product Swatch Image" AutoPostBack="True" OnCheckedChanged="RbtnProductSwatchNoImage_CheckedChanged" />
                    </div>
                </td>
            </tr>
            </table>
            <table  id="tblProductSwatchDescription" border="0" cellpadding="0" cellspacing="0" width="100%" runat="server" visible="false">
            <tr>
                <td>
                   <div>                       
                        <asp:Label ID="lblProductSwatchImageError" runat="server" CssClass="Error" ForeColor="Red" Text="" Visible="False"></asp:Label>
                   </div>
                   <div class="ValueStyle">Select an Image:&nbsp;<asp:FileUpload ID="UploadProductSwatchImage" runat="server" Width="300px" BorderStyle="Inset" EnableViewState="true" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="UploadProductSwatchImage" CssClass="Error" 
                                   Display="dynamic" ValidationExpression=".*(\.[Jj][Pp][Gg]|\.[Gg][Ii][Ff]|\.[Jj][Pp][Ee][Gg]|\.[Pp][Nn][Gg])" 
                                   ErrorMessage="Please select a valid JPEG, JPG, PNG or GIF image"></asp:RegularExpressionValidator>
                   </div>
                </td> 
            </tr>
            </table>      
        </asp:Panel>        
        
        <div class="FieldStyle" id="AlternateThumbnail" runat="server" visible="false">Product Image File Name
            <div class="ValueStyle"><asp:TextBox ID="txtAlternateThumbnail" runat="server" Columns="30" MaxLength="30"></asp:TextBox></div>            
        </div>
        
        <div class="FieldStyle">Product Image ALT Text</div>
        <small>Enter short descriptive text for this product to be used in the image ALT text. This text is displayed if the image does not download correctly.</small>
        <div class="ValueStyle"><asp:TextBox ID="txtImageAltTag" runat="server"></asp:TextBox></div>
   
        <div class="FieldStyle">Display on Product Page</div>        
        <div class="ValueStyle"><asp:CheckBox ID='VisibleInd' runat='server' Checked="true" Text="Check this box to show a thumbnail of this image on the Product Page" ></asp:CheckBox></div>
        
        <div class="FieldStyle">Display on Category Page</div>
        <div class="ValueStyle"><asp:CheckBox ID='VisibleCategoryInd' runat='server' Text="Check this box to display a thumbnail of this image on the Category Page" /></div>
   
   	    <div class="FieldStyle">Display Order<span class="Asterix">*</span></div>
        <small>Enter a number. This determines the order in which the product images will be displayed in the storefront. A product image with the lower display order will be displayed first.</small>
        <div class="ValueStyle">
             <asp:TextBox ID="DisplayOrder" runat="server" MaxLength="9">500</asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DisplayOrder" CssClass="Error" Display="Dynamic" ErrorMessage="Enter a Display Order"></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="DisplayOrder"
                 CssClass="Error" Display="Dynamic" ErrorMessage="Enter a whole number." ValidationExpression="^[0-9]*"></asp:RegularExpressionValidator>
        </div>
	       
        <div><asp:Button ID="btnSubmit" runat="server" CausesValidation="True" class="Button" OnClick="btnSubmit_Click" Text="SUBMIT" />
        &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False" class="Button" OnClick="btnCancel_Click" Text="CANCEL" /></div>
    </div>
</asp:Content>
