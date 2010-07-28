<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="Admin_Secure_categories_add" Title="Untitled Page"  ValidateRequest="false" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Src="~/Controls/HtmlTextBox.ascx" TagName="HtmlTextBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <div class="Form">
	    <table width="100%" cellSpacing="0" cellPadding="0" >
		    <tr>
			    <td><h1><asp:Label ID="lblTitle" Runat="server"></asp:Label>
                    <uc2:DemoMode ID="DemoMode1" runat="server" />
                </h1></td>
			    <td align="right">
				    <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>
	    
		<div><asp:Label ID="lblMsg" CssClass="Error" runat="server"></asp:Label></div>	    
	    <div><uc1:spacer id="Spacer8" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>
    	
    	<h4>General Settings</h4>        
    	
        <div class="FieldStyle">Product Category Name<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="* Enter a category name"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:TextBox ID='txtName' runat='server' MaxLength="50" Columns="50"></asp:TextBox></div>

        <div class="FieldStyle">Product Category Title</div>
        <small>The category title is displayed on the category detail page.</small>
        <div class="ValueStyle"><asp:TextBox ID='txtTitle' runat='server' MaxLength="255" Columns="50"></asp:TextBox></div>

        <div class="FieldStyle">Select Parent Category<span class="Asterix">*</span></div>
	    <div class="ValueStyle"><asp:DropDownList ID='ParentCategoryID' runat='server'></asp:DropDownList>
	    </div>
	    <div class="FieldStyle">Select number of rows and columns:<span class="Asterix">*</span>
	    <div class="ValueStyle"><span class="FieldStyle">Rows:</span><asp:DropDownList ID='RowDropDown' runat='server'>
            <asp:ListItem>0</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
            <asp:ListItem>13</asp:ListItem>
            <asp:ListItem>14</asp:ListItem>
            <asp:ListItem>15</asp:ListItem>
            </asp:DropDownList>
	   <span class="FieldStyle">Columns:</span>
	    <asp:DropDownList ID='ColumnDropDown' runat='server'>
            <asp:ListItem>0</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            </asp:DropDownList></div> 
    	
    	<h4>Display Settings</h4>
    	
    	<div class="FieldStyle">Page Template</div>
    	<div class="ValueStyle"><asp:DropDownList id="ddlPageTemplateList" runat="server"></asp:DropDownList></div>
    	
        <div class="FieldStyle">Display Order<span class="Asterix">*</span></div> 	    
        <div class="HintStyle">Enter a number. Items with a lower number are displayed first on the page.</div>                
        <div class="ValueStyle">
            <asp:TextBox ID="DisplayOrder" runat="server" MaxLength="9" Columns="9"></asp:TextBox>
            <asp:requiredfieldvalidator id="Requiredfieldvalidator5" runat="server" Display="Dynamic"  ErrorMessage="* Enter a Display Order" ControlToValidate="DisplayOrder"></asp:requiredfieldvalidator>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="DisplayOrder" Display="Dynamic" 
            ErrorMessage="Enter a whole number." MaximumValue="999999999" MinimumValue="1" Type="Integer"></asp:RangeValidator>
        </div>
    
        <div class="FieldStyle">Enable this Category</div>
	    <div class="ValueStyle"><asp:CheckBox ID='VisibleInd' runat='server' Checked="true" Text="Check this box if this category item should be displayed on the navigation menu." ></asp:CheckBox></div> 
    	
        <div class="FieldStyle">Display Subcategories</div>
	    <div class="ValueStyle"><asp:CheckBox ID='chkSubCategoryGridVisibleInd' runat='server' Checked="true" Text="Check this box if sub-categories should be displayed on the category page." ></asp:CheckBox></div> 
	    
        <h4>Category Image</h4>
        <small>Upload a suitable image for your category. Only JPG, GIF and PNG images are supported. The file size should be less than 1.5 Meg. Your image will automatically be scaled so it displays correctly in the catalog.</small>
         <table id="tblShowImage" border="0" cellpadding="0" cellspacing="20" runat="server" visible="false">
           <tr>
             <td><asp:Image ID="Image1" runat="server" /></td>
             <td>
               <div class="FieldStyle">Select an Option</div> 
               <div class="ValueStyle">
               <asp:RadioButton ID="RadioCategoryCurrentImage" Text="Keep Current Image" runat="server" GroupName="Category Image" AutoPostBack="True" OnCheckedChanged="RadioCategoryCurrentImage_CheckedChanged" Checked="True" />
               <asp:RadioButton ID="RadioCategoryNewImage"  Text="Upload New Image" runat="server" GroupName="Category Image" AutoPostBack="True" OnCheckedChanged="RadioCategoryNewImage_CheckedChanged" />
               </div>
             </td>
          </tr>
         </table>
         <table  id="tblCategoryDescription" border="0" cellpadding="0" cellspacing="0" width="100%" runat="server" visible="false">
          <tr>
            <td>
               <div>
               <asp:Label ID="lblCategoryImageError" runat="server" CssClass="Error" ForeColor="Red" Text="" Visible="False"></asp:Label>
               </div>
               <div class="ValueStyle">Select an Image:&nbsp;<asp:FileUpload ID="UploadCategoryImage" runat="server" Width="300px" BorderStyle="Inset" EnableViewState="true" />
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="UploadCategoryImage" CssClass="Error" 
                  Display="dynamic" ValidationExpression=".*(\.[Jj][Pp][Gg]|\.[Gg][Ii][Ff]|\.[Jj][Pp][Ee][Gg]|\.[Pp][Nn][Gg])" 
                  ErrorMessage="Please select a valid JPEG, JPG, PNG or GIF image"></asp:RegularExpressionValidator>
               </div>
            </td> 
         </tr>
        </table>
	    
        <div class="FieldStyle">Product Image ALT Text</div>
        <small>Enter short descriptive text for this product to be used in the image ALT text. This text is displayed if the image does not download correctly.</small>
        <div class="ValueStyle"><asp:TextBox ID="txtImageAltTag" runat="server"></asp:TextBox></div>
    
        <h4>SEO Settings</h4>
        <p>These settings are meant for Search Engine Optimization. Leave this section blank if unsure.</p>
        
        <div class="FieldStyle">Enter a title for Search Engines</div>
        <div class="ValueStyle"><asp:TextBox ID="txtSEOTitle" runat="server" MaxLength="500" Columns="50"></asp:TextBox></div>
  
        <div class="FieldStyle">Enter Keywords for Search Engines</div>
        <div class="ValueStyle"><asp:TextBox ID="txtSEOMetaKeywords" runat="server" MaxLength="500" Columns="50"></asp:TextBox></div>
  
        <div class="FieldStyle">Enter Description for Search Engines</div>
        <div class="ValueStyle"><asp:TextBox ID="txtSEOMetaDescription" runat="server" MaxLength="500" Columns="50"></asp:TextBox></div>
               
        <div class="FieldStyle">Enter a SEO friendly name for this Category </div>
        <div class="HintStyle">Use only characters a-z and 0-9. Use "-" instead of spaces. Do not use a file extension or parameters in your url name.</div>
        <div class="ValueStyle">
            <asp:TextBox ID="txtSEOURL" runat="server" MaxLength="100" Columns="50"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSEOURL"
                    CssClass="Error" Display="Dynamic" ErrorMessage="Enter valid SEO firendly URL"
                    SetFocusOnError="True" ValidationExpression="([A-Za-z0-9-_]+)"></asp:RegularExpressionValidator>
        </div>
        
        <div class="FieldStyle"><asp:CheckBox ID="chkAddURLRedirect" runat="server" Text=' Add 301 redirect on URL changes.' /></div>
    
    	<h4>Description</h4>
    	<div class="FieldStyle">Short Description</div>
        <small>Enter an optional short description (less than 100 characters) to be displayed in the category listing grid</small>
        <div class="ValueStyle">
        <asp:TextBox ID="txtshortdescription" runat="server" Width="300px" TextMode="MultiLine" Height="75px" MaxLength="100"></asp:TextBox>
        </div>           
        <div class="FieldStyle">Long Description<span class="Asterix">*</span></div>
        <small>You can enter rich text and images for the category description. Hint: To upload images to your description
        click on the image upload button.</small>
	    <div class="ValueStyle">
	        <uc1:HtmlTextBox id="ctrlHtmlText" runat="server"></uc1:HtmlTextBox>
	    </div> 
	    <div>
	        <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
	        <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	    </div>	
	</div>
</asp:Content>

