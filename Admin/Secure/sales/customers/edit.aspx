<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Admin/Themes/Standard/edit.master"  AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Admin_Secure_sales_customers_edit" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
<div class="Form">
        <table width="100%" cellSpacing="0" cellPadding="0">
		    <tr>
			    <td>
			        <h1><asp:Label ID="lblTitle" Runat="server"></asp:Label></h1>
                    <uc2:DemoMode ID="DemoMode1" runat="server" />			        
			    </td>
			    <td align="right">
				    <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click" ValidationGroup="EditContact"></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>
		 
		<div><asp:Label ID="lblErrorMsg" runat="server" CssClass="Error" EnableViewState="false"></asp:Label></div>
	    <div><uc1:spacer id="Spacer8" SpacerHeight="2" SpacerWidth="3" runat="server"></uc1:spacer></div>
       
        <h4>
            General Information
        </h4>
       
       <asp:ScriptManager id="ScriptManager" runat="server"></asp:ScriptManager>
       <!-- Update Panel for profiles drop down list to avoid the postbacks -->
	   <asp:UpdatePanel ID="updPnlRealtedGrid" runat="server" UpdateMode="Conditional">
	        <ContentTemplate>
                <div class="FieldStyle">Select Profile</div><div class="HintStyle">Select a profile to associate with this account. For example if you select "Dealer" then the account will be entitled to dealer specific promotions and options on the catalog when they sign in.</div><div class="ValueStyle"><asp:DropDownList id="ListProfileType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ListProfileType_SelectedIndexChanged"></asp:DropDownList> </div><div class="FieldStyle">Account Number</div><div class="HintStyle">This is the account number in your internal accounting system that corresponds to this customer. Leave blank if you dont have one.</div><div class="ValueStyle"><asp:TextBox id="txtExternalAccNumber" runat="server" Width="150px"></asp:TextBox></div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table width="100%" border="0">
            <tr>
                <td>
                    <div  class="FieldStyle">Company Name</div>                        
                    <div class="ValueStyle">
                                <asp:TextBox ID="txtCompanyName" runat="server" />
                    </div>
                </td>
                </tr>
                <tr>
                <td>
                    <div  class="FieldStyle">Website</div>                        
                    <div class="ValueStyle">
                                <asp:TextBox ID="txtWebSite" runat="server" />
                    </div>                    
                </td>        
            </tr>
            <tr>
                <td>
                 <div  class="FieldStyle">Source</div>  
                    <div class="ValueStyle">
                                <asp:TextBox ID="txtSource" runat="server" />
                    </div>
                </td>
            </tr>  
             
        </table>
              
        <uc1:spacer id="Spacer1" SpacerHeight="1" SpacerWidth="3" runat="server"></uc1:spacer>
        
        
        <h4>Login Information</h4>
        <div  class="FieldStyle">User Id</div>                        
        <div class="ValueStyle"><asp:TextBox ID="UserID" runat="server" columns="40" Width="150px" /></div>
        
        <div  class="FieldStyle">Password</div>
        <div class="ValueStyle"><asp:TextBox ID="Password" ValidationGroup="PasswordField" runat="server" />
        <div><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Password"
                Display="Dynamic" ErrorMessage="New Password length should be minimum 8" SetFocusOnError="True"
                ToolTip="New Password length should be minimum 8 and should contain atleast 1 number." ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,})"
                ValidationGroup="EditContact" CssClass="Error">Password must be 8 or more characters and should contain only alphanumeric values with at least 1 numeric character.</asp:RegularExpressionValidator></div>
        <asp:Label ID="lblPwdErrorMsg" runat="server" CssClass="Error" EnableViewState="false"></asp:Label></div>       
        
        <div class="FieldStyle"><asp:Label ID="lblSecretQuestion" runat="server" AssociatedControlID="ddlSecretQuestions">Security Question</asp:Label></div>
        <div class="ValueStyle">
            <asp:DropdownList ID="ddlSecretQuestions" runat="server">                                        
                <asp:ListItem Enabled="true" Selected="True" Text="What is the name of your favorite pet?"></asp:ListItem>
                <asp:ListItem Enabled="true" Text="In what city were you born?"></asp:ListItem>
                <asp:ListItem Enabled="true" Text="What high school did you attend?"></asp:ListItem>
                <asp:ListItem Enabled="true" Text="What is your favorite movie?"></asp:ListItem>
                <asp:ListItem Enabled="true" Text="What is your mother's maiden name?"></asp:ListItem>
                <asp:ListItem Enabled="true" Text="What was the make of your first car?"></asp:ListItem>
                <asp:ListItem Enabled="true" Text="What is your favorite color?"></asp:ListItem>
            </asp:DropdownList>                     
        </div>
        
        <div class="FieldStyle"><asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Security Answer</asp:Label></div>
        <div class="ValueStyle">
            <asp:TextBox ID="Answer" runat="server"></asp:TextBox>
            <asp:Label ID="lblAnswerErrorMsg" runat="server" CssClass="Error" EnableViewState="false"></asp:Label>
        </div>        
            
        <div class="FieldStyle">Select Role</div>   
        <div><asp:CheckBoxList id="RolesCheckboxList" runat="server" Enabled="false" RepeatDirection="Horizontal" RepeatColumns="3" CellPadding="5"> </asp:CheckBoxList></div>  
        
        <div><uc1:spacer id="SmallSpace" SpacerHeight="1" SpacerWidth="3" runat="server"></uc1:spacer></div>
        
        <table border="0" width="100%">
            <tr>
                <td id="tblBillingAddr" runat="server">
                    <h4>Billing Address</h4>
    
                    <div class="FieldStyle">First Name</div>
                    <div class="ValueStyle">
	                <asp:textbox id="txtBillingFirstName" runat="server" width="130" columns="30" MaxLength="100" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBillingFirstName" ValidationGroup="EditContact" ErrorMessage="First Name required." CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                    
                    <div class="FieldStyle">Last Name</div>
                    <div class="ValueStyle">
	                 <asp:textbox id="txtBillingLastName" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox>
	                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBillingLastName" ValidationGroup="EditContact" ErrorMessage="Last Name required." CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                     <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="Error" DisplayMode="List"
                     ShowMessageBox="True" ShowSummary="False" ValidationGroup="EditContact" />
	                </div>
                    
                    <div class="FieldStyle">Company Name</div>
                    <div class="ValueStyle">
	                 <asp:textbox id="txtBillingCompanyName" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox>
	                </div>   
	                
	                                
            	    
                    <div class="FieldStyle">Phone Number</div>
                    <div class="ValueStyle">
	                <asp:textbox id="txtBillingPhoneNumber" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox>
                    </div>
                    
                    <div class="FieldStyle">Email Address</div>
                    <div class="ValueStyle">
                    <asp:TextBox ID="txtBillingEmail" runat="server" Width="131px"></asp:TextBox>
                    <asp:regularexpressionvalidator id="regemailID" runat="server" ControlToValidate="txtBillingEmail" ErrorMessage="*Please use a valid email address."
					  Display="Dynamic" ValidationExpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+" ValidationGroup="EditContact" CssClass="Error"></asp:regularexpressionvalidator>
                    </div>
            		
	                <div class="FieldStyle">Street 1</div>
                    <div class="ValueStyle">
                     <asp:textbox id="txtBillingStreet1" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox>
                    </div>
            	   
                    <div class="FieldStyle">Street 2</div>
                    <div class="ValueStyle">
                       <asp:textbox id="txtBillingStreet2" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox>
                     </div>	               
                    
                    <div class="FieldStyle">City</div>
                    <div class="ValueStyle">
                      <asp:textbox id="txtBillingCity" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox>
                    </div>
            	  
                    <div class="FieldStyle">State</div>
                    <div class="ValueStyle">
	                  <asp:textbox id="txtBillingState" runat="server" width="30" columns="10" MaxLength="2"></asp:textbox>
                    </div>
                    <div class="FieldStyle">Postal Code</div>
                    <div class="ValueStyle">
                       <asp:textbox id="txtBillingPostalCode" runat="server" width="130" columns="30" MaxLength="20"></asp:textbox>
                    </div>
            	  
                    <div class="FieldStyle">Country</div>
                    <div class="ValueStyle">
                       <asp:DropDownList ID="lstBillingCountryCode" runat="server"></asp:DropDownList>
                    </div>                                             
                </td>
                
                <td valign="top" width="50%">
                <asp:Panel ID="pnlShipping" runat="server" Visible="true">
                    <h4>Shipping Address</h4>
      
                    <div class="FieldStyle">First Name</div>
                    <div class="ValueStyle">
                      <asp:textbox id="txtShippingFirstName" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox>
                    </div> 
                	  
                    <div class="FieldStyle">Last Name</div>
                    <div class="ValueStyle">
                       <asp:textbox id="txtShippingLastName" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox>
                    </div>
                        
                    <div class="FieldStyle">Company Name</div>
                    <div class="ValueStyle">
                       <asp:textbox id="txtShippingCompanyName" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox>
                    </div>
                	  
                    <div class="FieldStyle">Phone Number</div>
                    <div class="ValueStyle">
                       <asp:textbox id="txtShippingPhoneNumber" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox>
                    </div>
                	  
                    <div class="FieldStyle">Email Address</div>
                    <div class="ValueStyle">
                        <asp:TextBox ID="txtShippingEmail" runat="server" Width="131px"></asp:TextBox>
                        <asp:regularexpressionvalidator id="Regularexpressionvalidator1" runat="server" ControlToValidate="txtShippingEmail" ErrorMessage="*Please use a valid email address."
					     Display="Dynamic" ValidationExpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+" ValidationGroup="EditContact" CssClass="Error"></asp:regularexpressionvalidator>
                    </div>	                      
                	       
                    <div class="FieldStyle">Street 1</div>
                    <div class="ValueStyle">
                        <asp:textbox id="txtShippingStreet1" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox>
                    </div>
                	  
                    <div class="FieldStyle">Street 2</div>
                    <div class="ValueStyle">
                        <asp:textbox id="txtShippingStreet2" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox>
                    </div>
                	  
                    <div class="FieldStyle">City</div>
                    <div class="ValueStyle">
                        <asp:textbox id="txtShippingCity" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox>
                    </div>
                	  
                    <div class="FieldStyle">State</div>
                    <div class="ValueStyle">
	                     <asp:textbox id="txtShippingState" runat="server" width="30" columns="10" MaxLength="2"></asp:textbox>
                    </div>
                	
                    <div class="FieldStyle">Postal Code</div>
                    <div class="ValueStyle">
                        <asp:textbox id="txtShippingPostalCode" runat="server" width="130" columns="30" MaxLength="20"></asp:textbox>
                    </div>
                	  
                    <div class="FieldStyle">Country</div>
                    <div class="ValueStyle">
                        <asp:DropDownList ID="lstShippingCountryCode" runat="server"></asp:DropDownList>
                    </div>
                    </asp:Panel>
                </td>            
            </tr>
            <tr>
                <td align="left"><asp:CheckBox ID="chkSameAsBilling" runat="server" Text="Shipping Address is same as Billing" Checked="false" OnCheckedChanged="chkSameAsBilling_CheckedChanged" AutoPostBack="true" /> </td>
            </tr>
            <tr>
                <td align="left"><asp:CheckBox ID="chkOptIn" runat="server" Text="Email Opt In" /></td>
            </tr>  
        </table>
         
	  
      <uc1:spacer id="Spacer2" SpacerHeight="1" SpacerWidth="3" runat="server"></uc1:spacer>
        
      
     
      <uc1:spacer id="Spacer5" SpacerHeight="1" SpacerWidth="3" runat="server"></uc1:spacer>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
	        <ContentTemplate>
      <h4>Tracking Information</h4>
      <div class="HintStyle">Use this section to set up tracking codes for this account. These tracking codes will attribute purchases made by other customers that have been referred by this account.<br />
      </div>
       <uc1:spacer id="Spacer6" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer>
        <div class="FieldStyle"><asp:Label runat="server" ID="lblAffiliateLink">Tracking Link (URL)</asp:Label></div>
        <div class="HintStyle">Customers who visit your site using this link will automatically be logged as being referred by this account when they purchase.</div>
        <div class="ValueStyle"><asp:HyperLink ID="hlAffiliateLink" runat="server" Target="_blank">NA</asp:HyperLink></div>    

      <div  class="FieldStyle"> Commission Type</div>    
    <div class="HintStyle">Enter how much you will pay this account for referred sales. Leave blank if not applicable.</div>   
      <div class="ValueStyle">
        <asp:DropDownList ID="lstReferral" runat="server" OnSelectedIndexChanged="DiscountType_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
          <asp:TextBox ID="Discount" runat="server" MaxLength="7" Columns="25"></asp:TextBox>
       <asp:RangeValidator ID="discAmountValidator" runat="server" ControlToValidate="Discount" ValidationGroup="EditContact"
            CssClass="Error" Enabled="false" Display="Dynamic" MaximumValue="9999999" MinimumValue="0" CultureInvariantValues="true" Type="Currency"></asp:RangeValidator>
        <asp:RangeValidator ID="discPercentageValidator" Enabled="false" runat="server" ControlToValidate="Discount" ValidationGroup="EditContact"
            CssClass="Error" Display="Dynamic" MaximumValue="100" CultureInvariantValues="true" MinimumValue="0" SetFocusOnError="True" Type="Double"></asp:RangeValidator></div>
        <div class="FieldStyle"> Tax ID</div>
        <div class="ValueStyle">
            <asp:TextBox ID="txtTaxId" runat="server" Width="179px"></asp:TextBox>
        </div>           
      <div class="FieldStyle">Partner Approval Status</div>
       <div class="HintStyle">Set the Partner Approval Status to ACTIVE to start attributing visitors to this account.</div>
      <div class="ValueStyle">
        <asp:DropDownList ID="lstReferralStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstReferralStatus_SelectedIndexChanged">                      
        </asp:DropDownList>
      </div>        
       <div class="FieldStyle" runat="server" id="amountOwed">Amount Owed</div>  
    <div class="ValueStyle"><asp:Label ID="lblAmountOwed" runat="server" Text="" /></div>
      </ContentTemplate>
       </asp:UpdatePanel>
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
      
      <uc1:spacer id="Spacer4" SpacerHeight="1" SpacerWidth="3" runat="server"></uc1:spacer>
        
      <h4>Description</h4>
        
      <div class="FieldStyle">
                              <asp:textbox id="txtDescription" runat="server" TextMode="MultiLine"  width="400" height="200" MaxLength="2" />
      </div>
                      
       <uc1:spacer id="Spacer3" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer>
        
       <table>
                <tr>
                     <td align="right">
				                         <asp:button class="Button" id="Submit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click" ValidationGroup="EditContact"></asp:button>
				                         <asp:button class="Button" id="Cancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			         </td>
                </tr>
       </table>
         
</div> 
</asp:Content>


