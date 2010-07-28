<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master"  AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="admin_secure_settings_ipcommerce_edit" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div class="Form">        
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <h1>IP Commerce Settings</h1>
                </td>
                <td align="right">
                    <asp:Button ID="btnSubmitTop" runat="server" CausesValidation="True" class="Button" OnClick="btnSubmit_Click" Text="SUBMIT" />
                    <asp:Button ID="btnCancelTop" runat="server" CausesValidation="False" class="Button" OnClick="btnCancel_Click" Text="CANCEL" />
                </td>
            </tr>
        </table>
        
        <div><asp:Label ID="lblErrorMsg" runat="server" CssClass="Error"></asp:Label></div>
        
        <div><ZNode:spacer id="Spacer8" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>
        
        <h4>Merchant Information</h4>	    
	    <div class="FieldStyle">Merchant Name</div>	    
        <div class="ValueStyle"><asp:TextBox ID="MerchantName" runat="server" Columns="25" ></asp:TextBox></div>
             
        <div class="FieldStyle">Customer Service Phone Number</div>
        <div class="ValueStyle"><asp:textbox id="PhoneNumber" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox></div>  
        
        <div class="FieldStyle">Socket ID</div>	    
        <div class="ValueStyle"><asp:TextBox ID="SocketID" runat="server" Columns="25" ></asp:TextBox></div>
        
        <div class="FieldStyle">SIC Code(Standard Industry Code)</div>        
        <div class="ValueStyle">
            <asp:TextBox MaxLength="4" ID="SIC" runat="server" Columns="25" ></asp:TextBox>
            <div><asp:RequiredFieldValidator Display="Dynamic" CssClass="Error" ID="regFieldValidator" runat="server" ControlToValidate="SIC">SIC Code is required.</asp:RequiredFieldValidator> </div>
        </div>
        
        <div class="FieldStyle">Serial Number</div>
        <div class="ValueStyle"><asp:TextBox ID="SerialNumber" runat="server" Columns="25"></asp:TextBox></div>
        
        <div class="FieldStyle">Merchant ID</div>	    
        <div class="ValueStyle"><asp:TextBox ReadOnly="true" ID="MerchantID" runat="server" Columns="25"></asp:TextBox></div>
        
        <div class="FieldStyle">Store ID</div>	    
        <div class="ValueStyle"><asp:TextBox ReadOnly="true" ID="StoreID" runat="server" Columns="25" ></asp:TextBox></div>
        
        <div class="FieldStyle">Socket Number</div>	    
        <div class="ValueStyle"><asp:TextBox ReadOnly="true" ID="SocketNumber" runat="server" Columns="25" ></asp:TextBox></div>
        
        <div class="FieldStyle">AVS Data</div>
        <div class="ValueStyle"><asp:CheckBox ID="chkAVSData" Text="Check this to verify the cardholder’s account number against address information." runat="server" /></div>
        
        <div class="FieldStyle">CVV Data</div>
        <div class="ValueStyle"><asp:CheckBox ID="chkCVVData" Text="Check this feature to include CVV data for every transaction." runat="server" /></div>
                
        <h4>Login Information</h4>
        <div  class="FieldStyle">User Id</div>                        
        <div class="ValueStyle"><asp:TextBox ID="UserID" runat="server" columns="40" /></div>
        
        <div  class="FieldStyle">Password</div>
        <div class="ValueStyle"><asp:TextBox ID="Password" ValidationGroup="PasswordField" runat="server" />
        <div><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Password"
                Display="Dynamic" ErrorMessage="New Password length should be minimum 8" SetFocusOnError="True"
                ToolTip="New Password length should be minimum 8 and should contain atleast 1 number." ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,})"
                ValidationGroup="PasswordField" CssClass="Error">Password must be 8 or more characters and should contain only alphanumeric values with at least 1 numeric character.</asp:RegularExpressionValidator></div>
        <asp:Label ID="lblPwdErrorMsg" runat="server" CssClass="Error" EnableViewState="false"></asp:Label></div>       
                  
        
        <h4>Billing Address</h4>
                    	
        <div class="FieldStyle">Street1</div>        
        <div class="ValueStyle"><asp:textbox id="txtBillingStreet1" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox></div>
	   
        <div class="FieldStyle">Street2</div>
       <div class="ValueStyle"><asp:textbox id="txtBillingStreet2" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox></div>	               
        
        <div class="FieldStyle">City</div>
        <div class="ValueStyle"><asp:textbox id="txtBillingCity" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox></div>
	  
        <div class="FieldStyle">State</div>
        <div class="ValueStyle"><asp:textbox id="txtBillingState" runat="server" width="30" columns="10" MaxLength="2"></asp:textbox></div>
        
        <div class="FieldStyle">Postal Code</div>
        <div class="ValueStyle"><asp:textbox id="txtBillingPostalCode" runat="server" width="130" columns="30" MaxLength="20"></asp:textbox></div>
	  
        <div class="FieldStyle">Country</div>
        <div class="ValueStyle"><asp:DropDownList ID="lstBillingCountryCode" runat="server"></asp:DropDownList></div>        
        
        
        <div><ZNode:spacer id="Spacer1" SpacerHeight="20" SpacerWidth="3" runat="server" /></div>
        
        <asp:button class="Button" id="SubmitButton" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click" ></asp:button>
	    <asp:button class="Button" id="CancelButton" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	    
    </div>

</asp:Content>