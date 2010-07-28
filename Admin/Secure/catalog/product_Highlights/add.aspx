<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="Admin_Secure_catalog_product_Highlights_add" ValidateRequest="false" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Src="~/Controls/HtmlTextBox.ascx" TagName="HtmlTextBox" TagPrefix="ZNode" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">    
    <div class="Form">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td><h1><asp:Label ID="lblHeading" Text='Add Product Highlight' runat="server" /></h1></td>
                <td align="right">
                    <asp:Button ID="btnSubmitTop" runat="server" CausesValidation="True" class="Button"  OnClick="btnSubmit_Click" Text="SUBMIT" style="margin-right:20px;"/>
                    <asp:Button ID="btnCancelTop" runat="server" CausesValidation="False" class="Button" OnClick="btnCancel_Click" Text="CANCEL" />
                </td>
            </tr>
        </table>
        <ZNode:DemoMode id="DemoMode1" runat="server"></ZNode:DemoMode>
    
        <div><uc1:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></div>
        <asp:Label ID="lblError" CssClass= "Error" runat="server"></asp:Label>
        
        <h4>General Settings</h4>
        <div class="FieldStyle">Name<span class="Asterix">*</span></div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="HighlightName" ErrorMessage="* Enter name" CssClass="Error" Display="dynamic"></asp:RequiredFieldValidator>
        <div class="ValueStyle"><asp:TextBox ID="HighlightName" runat="server"></asp:TextBox></div>
        
        <div class="FieldStyle">Highlight Type</div>
        <div class="HintStyle">Select a type for this Highlight. Depending on your storefront implementation different Highlight Types will display differently. Set to FEATURE if you are unsure</div>
        <div class="ValueStyle">
        <asp:DropDownList ID="HighlightType" runat="server"></asp:DropDownList>
        </div>
        
        <div class="FieldStyle">Display Order<span class="Asterix">*</span></div>
        <div class="HintStyle">Enter a number. This determines the order in which the Highlight will be displayed in the product page. A Highlight with the lower display order will be displayed first.</div>        
        <asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" Display="Dynamic" ErrorMessage="* Enter a Display Order" ControlToValidate="DisplayOrder"></asp:requiredfieldvalidator>
        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="DisplayOrder" Display="Dynamic" ErrorMessage="Enter a whole number." MaximumValue="999999999" MinimumValue="1" Type="Integer"></asp:RangeValidator>
        <div class="ValueStyle"><asp:TextBox ID="DisplayOrder" runat="server" MaxLength="9" Columns="5"></asp:TextBox></div>
                
        <div class="FieldStyle">Enable this Highlight</div>
	    <div class="ValueStyle"><asp:CheckBox ID='VisibleInd' runat='server' Checked="true" Text="Check this box if this Highlight should be displayed on the product page." ></asp:CheckBox></div> 

        <asp:Panel ID="HighlightImageHeader" runat="server">
            <h4>Highlight Image</h4>       
            <small><asp:Label runat="server" ID="textadd" Text="Upload a suitable image for this Highlight. Only jpeg, gif and png images are supported. The image you upload should be pre-sized properly to display correctly on your product page."></asp:Label></small>
            <small><asp:Label runat="server" ID="textedit" Text="If you would like to show this Highlight as a link then select 'No Image'. If instead you would like to display an icon for this Highlight choose 'Upload New Image'. Images must be pre-sized before uploading and should be in JPEG, GIF or PNG format."></asp:Label></small>

            <table id="tblShowImage" border="0" cellpadding="0" cellspacing="20" runat="server" visible="false">
                <tr>
                    <td><asp:Image ID="HighlightImage" runat="server" /></td>
                    <td>
                        <div class="FieldStyle">Select an Image</div> 
                        <div class="ValueStyle">
                            <asp:RadioButton ID="RadioHighlightCurrentImage" Text="Keep Current Image" runat="server" GroupName="Highlight Image" AutoPostBack="True" OnCheckedChanged="RadioHighlightCurrentImage_CheckedChanged" Checked="True" />
                            <asp:RadioButton ID="RadioHighlightNewImage"  Text="Upload New Image" runat="server" GroupName="Highlight Image" AutoPostBack="True" OnCheckedChanged="RadioHighlightNewImage_CheckedChanged" />
                            <asp:RadioButton ID="RadioHighlightNoImage"  Text="No Image" runat="server" GroupName="Highlight Image" AutoPostBack="True" OnCheckedChanged="RadioHighlightNoImage_CheckedChanged" />
                        </div>
                    </td>
                </tr>
             </table>                    
             <table  id="tblHighlight" border="0" cellpadding="0" cellspacing="0" width="100%" runat="server" visible="false">
                <tr>
                    <td>
                        <div><asp:Label ID="llblErrorMsg" runat="server" CssClass="Error" ForeColor="Red" Text="" Visible="False"></asp:Label></div>
                        <div class="ValueStyle">Select an Image:&nbsp;<asp:FileUpload ID="UploadHighlightImage" runat="server" Width="300px" BorderStyle="Inset" EnableViewState="true" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="UploadHighlightImage" CssClass="Error" 
                                Display="dynamic" ValidationExpression=".*(\.[Jj][Pp][Gg]|\.[Gg][Ii][Ff]|\.[Jj][Pp][Ee][Gg]|\.[Pp][Nn][Gg])" 
                                ErrorMessage="Please select a valid JPEG, JPG, PNG or GIF image"></asp:RegularExpressionValidator>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>         
	    
	    <div class="FieldStyle">Product Image ALT Text</div>
        <small>Enter short descriptive text for this product to be used in the image ALT text. This text is displayed if the image does not download correctly.</small>
        <div class="ValueStyle"><asp:TextBox ID="txtImageAltTag" runat="server"></asp:TextBox></div>
        
        <h4>Hyperlink Settings</h4>
        
        <div class="FieldStyle">Enable Hyperlink</div>        
        <div class="ValueStyle"><asp:CheckBox AutoPostBack="True" Checked="false" OnCheckedChanged="DisplayPopup_CheckedChanged" ID="chkEnableHyperlink" Text='Check this option to hyperlink this Highlight to another page.' runat="server" /></div>
        <div class="ValueStyle"><asp:CheckBox ID="chkHyperlinkExternal" Text='Hyperlink this Highlight to an external web site.' Checked="false" AutoPostBack="true" runat="server" OnCheckedChanged="HyperlinkChk_CheckedChanged" /></div>
        
        <asp:Panel id="pnlHyperLinkInternal" runat="server" Visible="false">
            <div class="FieldStyle">Linked Page Text</div>
            <div class="HintStyle">Enter the text that will be displayed when this Highlight is clicked.</div>
            <div class="ValueStyle"><ZNode:HtmlTextBox id="Description" runat="server"></ZNode:HtmlTextBox></div>       
        </asp:Panel>            
        <asp:Panel id="pnlHyperLinkExternal" runat="server" Visible="false">    
            <div class="FieldStyle">Hyperlink</div>
            <div class="HintStyle">Enter a web address to link this Highlight to. The address must be the full path to the page you want to go to (ex: http://www.znode.com).</div>
            <div class="ValueStyle"><asp:TextBox ID="Hyperlink" runat="server"></asp:TextBox></div>
        </asp:Panel>
        <br />
        <br />
        <div>
            <asp:Button ID="btnSubmit" runat="server" CausesValidation="True" CssClass="Button" OnClick="btnSubmit_Click" Text="SUBMIT" style="margin-right:10px;"/>
            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="Button" OnClick="btnCancel_Click" Text="CANCEL" />
        </div>
    </div>
</asp:Content>