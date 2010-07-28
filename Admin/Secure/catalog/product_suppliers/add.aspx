<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="Admin_Secure_catalog_product_suppliers_add" validateRequest="false" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/spacer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <div class="Form">
	    <table width="100%" cellspacing="0" cellpadding="0" >
		    <tr>
			    <td><h1><asp:Label ID="lblTitle" Runat="server"></asp:Label>
                    <uc2:DemoMode id="DemoMode1" runat="server">
                    </uc2:DemoMode></h1></td>
			    <td align="right">
				    <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>
        
        <div><uc1:spacer id="Spacer8" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
    	        
        <div class="FieldStyle">Supplier Notification Method</div>
        <div class="ValueStyle"><asp:DropDownList ID="ddlSupplierTypes" runat="server"></asp:DropDownList></div>
        
        <div class="FieldStyle">Supplier Name<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Name" Display="Dynamic" ErrorMessage="* Enter a Supplier Name" SetFocusOnError="True"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:TextBox ID='Name' runat='server' MaxLength="50" Columns="50"></asp:TextBox></div>
        
        <div class="FieldStyle">Enter a Description <br />
            <asp:TextBox ID="Description" runat="server" rows="10" MaxLength="4000" TextMode="MultiLine" columns="50"></asp:TextBox>
        </div>  <br />              
        <div class="FieldStyle">Contact First Name</div>
        <div class="ValueStyle"><asp:TextBox ID="ContactFirstName" runat="server"></asp:TextBox></div>
        
        <div class="FieldStyle">Contact Last Name</div>
        <div class="ValueStyle"><asp:TextBox ID="ContactLastName" runat="server"></asp:TextBox></div>
        
        <div class="FieldStyle">Contact Phone</div>
        <div class="ValueStyle"><asp:TextBox ID="ContactPhone" runat="server"></asp:TextBox></div>
        
        <div class="FieldStyle">Contact Email</div>
        <div class="ValueStyle"><asp:TextBox ID='EmailId' runat='server' MaxLength="50" Columns="50"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="EmailId"
                CssClass="Error" Display="Dynamic" ErrorMessage="Enter Valid EmailId" SetFocusOnError="True"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></div>     
                
        <div class="FieldStyle">Notification Email</div>
        <small>This email address can be used to send order notifications directly to this supplier when customers buy on your site. Add multiple emails by separating them with a comma.</small>
        <div class="ValueStyle"><asp:TextBox ID='NotifyEmail' runat='server' Columns="50" TextMode="MultiLine"></asp:TextBox>
         
          <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="NotifyEmail"
                CssClass="Error" Display="Dynamic" ErrorMessage="Enter Valid EmailId" SetFocusOnError="True"
                ValidationExpression="^((([a-zA-Z\'\.\-]+)?)((,\s*([a-zA-Z]+))?)|([A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})))(,{1}(((([a-zA-Z\'\.\-]+){1})((,\s*([a-zA-Z]+))?))|([A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})){1}))*$"></asp:RegularExpressionValidator> 
         </div>  
        <div class="FieldStyle">Email Notification Template</div>
        <small>Enter the XSL email template to use when sending an order to this supplier. An example template can be found at Data/Default/Config/Receipt.xsl</small>
        <div class="ValueStyle"><asp:TextBox ID="NotificationTemplate" runat="server" rows="15" columns="50" TextMode="MultiLine"></asp:TextBox></div>         
       
        <div class="FieldStyle">Display Order<span class="Asterix">*</span></div>
        <small>Enter a number. This determines the order in which the supplier will be displayed in the admin. A supplier with the lower display order will be displayed first.</small>
        <div>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DisplayOrder" CssClass="Error" Display="Dynamic" ErrorMessage="Enter a Display Order"></asp:RequiredFieldValidator>
             <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="DisplayOrder" Display="Dynamic" 
                ErrorMessage="Enter a whole number." MaximumValue="999999999" MinimumValue="1" Type="Integer"></asp:RangeValidator>
        </div>
        <div class="ValueStyle"><asp:TextBox ID="DisplayOrder" runat="server" MaxLength="9" Columns="5">500</asp:TextBox></div>      
        
        <div>
	        <uc1:spacer id="Spacer1" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer>        
            <asp:CheckBox ID="ChkEmailNotify" runat="server" Text="Enable Email Notification?" CssClass="FieldStyle" Checked="true" Visible="false" />
        </div>
	    <div>
	        <uc1:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer>        
            <asp:CheckBox ID="CheckActiveInd" runat="server" Text="Enable this Supplier" CssClass="FieldStyle" Checked="true" />
	        <asp:Label ID="lblError" runat="server"></asp:Label>	   
        </div>
         <h4>Custom Information</h4>
      
      <div class="FieldStyle">Custom1</div>
      <div class="ValueStyle">
		                      <asp:textbox id="txtCustom1" runat="server" TextMode="MultiLine"  width="400" height="100" MaxLength="2"></asp:textbox>
      </div>
      <div class="FieldStyle">Custom2</div>
      <div class="ValueStyle">
		                      <asp:textbox id="txtCustom2" runat="server" TextMode="MultiLine"  width="400" height="100" MaxLength="2"></asp:textbox>
      </div>
      <div class="FieldStyle">Custom3</div>
      <div class="ValueStyle">
		                      <asp:textbox id="txtCustom3" runat="server" TextMode="MultiLine"  width="400" height="100" MaxLength="2"></asp:textbox>
      </div>
        <div class="FieldStyle">
            Custom4</div>
        <div class="ValueStyle">
            <asp:TextBox ID="txtCustom4" runat="server" Height="100" MaxLength="2" TextMode="MultiLine"
                Width="400"></asp:TextBox>
        </div>
        <div class="FieldStyle">
            Custom5</div>
        <div class="ValueStyle">
            <asp:TextBox ID="txtCustom5" runat="server" Height="100" MaxLength="2" TextMode="MultiLine"
                Width="400"></asp:TextBox>
        </div>
    <p></p>
    <asp:button class="Button" id="SubmitButton" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
	<asp:button class="Button" id="CancelButton" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	
</div>
                
</asp:Content>

