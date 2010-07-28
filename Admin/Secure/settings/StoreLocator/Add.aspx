<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Admin_Secure_settings_StoreLocator_Add" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/spacer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">    
    <div class="Form">
	    <table width="100%" cellspacing="0" cellpadding="0" >
		    <tr>
			    <td><h1><asp:Label ID="lblTitle" Runat="server"></asp:Label>
                    <uc2:DemoMode ID="DemoMode1" runat="server" />
                </h1></td>
			    <td align="right">
				    <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"  ></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>		   
	    <div>
	        <uc1:spacer id="Spacer8" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer>
	    </div>	          
        <h4>
            General Information
        </h4> 	 
        <div class="FieldStyle">Select Account ID</div>
	    <div class="HintStyle"></div>
        <div class="ValueStyle"><asp:DropDownList ID="ListAccounts" runat="server" Width="141px"></asp:DropDownList></div>       
	    <div class="FieldStyle">Store Name</div>	    
        <div class="ValueStyle">
            <asp:TextBox ID="txtstorename" runat="server" MaxLength="30" Columns="25" ></asp:TextBox>
        </div>  
        <div class="FieldStyle">Address Line 1</div>	  
        <div class="ValueStyle">
            <asp:TextBox ID="txtaddress1" runat="server" MaxLength="35" Columns="25" ></asp:TextBox>
        </div>    
        <div class="FieldStyle">Address Line 2</div>
        <div class="ValueStyle">
            <asp:TextBox ID="txtaddress2" runat="server" MaxLength="35" Columns="25" ></asp:TextBox>
        </div>  
        <div class="FieldStyle">Address Line 3</div>
        <div class="ValueStyle">
            <asp:TextBox ID="txtaddress3" runat="server" MaxLength="35" Columns="25" ></asp:TextBox>
        </div>    
        <div class="FieldStyle">City</div>	    
        <div class="ValueStyle">
            <asp:TextBox ID="txtcity" runat="server" MaxLength="15" Columns="25" ></asp:TextBox>
        </div>  
        <div class="FieldStyle">State</div>	    
        <div class="ValueStyle">
            <asp:TextBox ID="txtstate" runat="server" MaxLength="15" Columns="25" ></asp:TextBox>
        </div>  
        <div class="FieldStyle">Zip Code</div>	    
        <div class="ValueStyle">
            <asp:TextBox ID="txtzip" runat="server" MaxLength="15" Columns="25" ></asp:TextBox>
        </div>    
	    <div class="FieldStyle">Phone Number</div>	    
        <div class="ValueStyle">
            <asp:TextBox ID="txtphone" runat="server" MaxLength="25" Columns="25" ></asp:TextBox>
        </div>
        <div class="FieldStyle">Fax Number</div>
        <div class="ValueStyle">
            <asp:TextBox ID="txtfax" runat="server" MaxLength="25" Columns="25" ></asp:TextBox>
        </div>
        <div class="FieldStyle">Contact Name</div>	    
        <div class="ValueStyle">
            <asp:TextBox ID="txtcname" runat="server" MaxLength="30" Columns="25" ></asp:TextBox>
        </div>      
        <div class="FieldStyle">Display Order</div>
        <div class="HintStyle">Enter the order that this store should be displayed in the search results. Stores are sorted from lowest to highest.</div>               
        <div class="ValueStyle">
            <asp:TextBox ID="txtdisplayorder" runat="server" MaxLength="9" Columns="5"></asp:TextBox>        
            <asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" Display="Dynamic" ErrorMessage="* Enter a Display Order" ControlToValidate="txtdisplayorder"></asp:requiredfieldvalidator>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtdisplayorder" Display="Dynamic" 
            ErrorMessage="Enter a whole number." MaximumValue="999999999" MinimumValue="1" Type="Integer"></asp:RangeValidator>
        </div>
                
        <div class="FieldStyle">Display Store</div>
        <div class="HintStyle">Check this box to display this store in the store locator search results.</div>
        <div class="ValueStyle">
            <asp:CheckBox ID="chkActiveInd" runat="server" Checked="true" Text="Enable" />
        </div>      
	</div>	
	<h4>Store Image</h4>
        <small>Upload a suitable image for your Store. Only JPG, GIF and PNG images are supported. The file size should be less than 1.5 Meg. Your image will automatically be scaled so it displays correctly.</small>
         <table id="tblShowImage" border="0" cellpadding="0" cellspacing="20" runat="server" visible="false">
           <tr>
             <td><asp:Image ID="StoreImage" runat="server" /></td>
             <td>
               <div class="FieldStyle">Select an Option</div> 
               <div class="ValueStyle">
               <asp:RadioButton ID="RadioStoreCurrentImage" Text="Keep Current Image" runat="server" GroupName="Store Image" AutoPostBack="True" OnCheckedChanged="RadioStoreCurrentImage_CheckedChanged" Checked="True" />
               <asp:RadioButton ID="RadioStoreNewImage"  Text="Upload New Image" runat="server" GroupName="Store Image" AutoPostBack="True" OnCheckedChanged="RadioStoreNewImage_CheckedChanged" />
               <asp:RadioButton ID="RadioStoreNoImage"  Text="No Image" runat="server" GroupName="Store Image" AutoPostBack="True" OnCheckedChanged="RadioStoreNoImage_CheckedChanged" />
               </div>
             </td>
          </tr>
         </table>                    
         <table  id="tblStoreDescription" border="0" cellpadding="0" cellspacing="0" width="100%" runat="server" visible="false">
          <tr>
            <td>
               <div>
               <asp:Label ID="lblStoreImageError" runat="server" CssClass="Error" ForeColor="Red" Text="" Visible="False"></asp:Label>
               </div>
               <div class="ValueStyle">Select an Image:&nbsp;<asp:FileUpload ID="UploadStoreImage" runat="server" Width="300px" BorderStyle="Inset" EnableViewState="true" />
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="UploadStoreImage" CssClass="Error" 
                  Display="dynamic" ValidationExpression=".*(\.[Jj][Pp][Gg]|\.[Gg][Ii][Ff]|\.[Jj][Pp][Ee][Gg]|\.[Pp][Nn][Gg])" 
                  ErrorMessage="Please select a valid JPEG, JPG, PNG or GIF image"></asp:RegularExpressionValidator>
               </div>
            </td> 
         </tr>
        </table>        
         <div><asp:Label ID="lblError" runat="server" Visible="true"></asp:Label></div>         
	 <p></p>
	 <br />
	 <br />
        <asp:button class="Button" id="SubmitButton" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click" ></asp:button>
	    <asp:button class="Button" id="CancelButton" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>          
</asp:Content>