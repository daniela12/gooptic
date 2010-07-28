<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CreateUser.ascx.cs" Inherits="Admin_Secure_Enterprise_OrderDesk_CreateUser" %>
<%@ Register Src="~/Controls/spacer.ascx" TagName="spacer" TagPrefix="uc1" %>
<div class="Form">
     <h1>Customer Account</h1>
     <div><uc1:spacer id="Spacer1" SpacerHeight="4" SpacerWidth="10" runat="server"></uc1:spacer></div>
     <asp:UpdatePanel ID="pnlCustomerDetail" runat="server" UpdateMode="Conditional">
         <ContentTemplate>
             <table cellpadding="0" cellspacing="0" class="Form">
                <tr>
                    <td valign="top" width="50%">                
                        <table>	
	                        <tr class="Row">
		                        <td class="HeaderStyle" colspan="2">Billing Address</td>
	                        </tr>
	                        <tr>
	                            <td><uc1:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></td>
	                        </tr>		            
	                        <tr class="Row">
		                        <td class="FieldStyle">First Name</td>
		                        <td><asp:textbox id="txtBillingFirstName" ValidationGroup="groupCreateUser" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox><div><asp:requiredfieldvalidator id="req1" ErrorMessage="Enter First Name" ControlToValidate="txtBillingFirstName" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupCreateUser"></asp:requiredfieldvalidator></div></td>
	                        </tr>
	                        <tr class="Row">
		                        <td class="FieldStyle">Last Name</td>
		                        <td><asp:textbox id="txtBillingLastName" ValidationGroup="groupCreateUser" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox><div><asp:requiredfieldvalidator id="Requiredfieldvalidator1" ErrorMessage="Enter Last Name" ControlToValidate="txtBillingLastName" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupCreateUser"></asp:requiredfieldvalidator></div></td>
	                        </tr>
	                        <tr class="Row">
		                        <td class="FieldStyle">Company Name</td>
		                        <td><asp:textbox id="txtBillingCompanyName" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox></td>
	                        </tr>	
	                        <tr class="Row">
		                        <td class="FieldStyle">Phone Number</td>
		                        <td><asp:textbox id="txtBillingPhoneNumber" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox><div><asp:requiredfieldvalidator id="Requiredfieldvalidator2" ErrorMessage="Enter Phone Number" ControlToValidate="txtBillingPhoneNumber" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupCreateUser"></asp:requiredfieldvalidator></div></td>
	                        </tr>   
	                        <tr class="Row">
                                <td class="FieldStyle">Email Address</td>
                                <td>
                                    <asp:TextBox ID="txtBillingEmail" runat="server"></asp:TextBox>
                                    <div><asp:regularexpressionvalidator id="regemailID" runat="server" ControlToValidate="txtBillingEmail" ErrorMessage="*Please use a valid Email Address."
						                        Display="Dynamic" ValidationExpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+" CssClass="Error" ValidationGroup="groupCreateUser"></asp:regularexpressionvalidator></div>
						        </td>
                            </tr>
	                        <tr class="Row">
		                        <td class="FieldStyle">Street 1</td>
		                        <td><asp:textbox id="txtBillingStreet1" runat="server" width="130" columns="30" MaxLength="100" ></asp:textbox><div><asp:requiredfieldvalidator id="Requiredfieldvalidator3" ErrorMessage="Enter Street" ControlToValidate="txtBillingStreet1" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupCreateUser"></asp:requiredfieldvalidator></div></td>
	                        </tr>  
	                        <tr class="Row">
		                        <td class="FieldStyle">Street 2</td>
		                        <td><asp:textbox id="txtBillingStreet2" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox></td>
	                        </tr>  
	                        <tr class="Row">
		                        <td class="FieldStyle">City</td>
		                        <td><asp:textbox id="txtBillingCity" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox><div><asp:requiredfieldvalidator id="Requiredfieldvalidator4" ErrorMessage="Enter City" ControlToValidate="txtBillingCity" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupCreateUser"></asp:requiredfieldvalidator></div></td>
	                        </tr> 
	                        <tr class="Row">
		                        <td class="FieldStyle">State</td>
		                        <td>
		                            <asp:textbox id="txtBillingState" runat="server" width="30" columns="10" MaxLength="2"></asp:textbox><div><asp:requiredfieldvalidator id="Requiredfieldvalidator6" ErrorMessage="Enter State" ControlToValidate="txtBillingState" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupCreateUser"></asp:requiredfieldvalidator></div>
		                        </td>
	                        </tr>
	                        <tr class="Row">
		                        <td class="FieldStyle">Postal Code</td>
		                        <td><asp:textbox id="txtBillingPostalCode" runat="server" width="130" columns="30" MaxLength="10"></asp:textbox><div><asp:requiredfieldvalidator id="Requiredfieldvalidator12" ErrorMessage="Enter Postal Code" ControlToValidate="txtBillingPostalCode" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupCreateUser"></asp:requiredfieldvalidator></div></td>
	                        </tr> 
	                         <tr class="Row">
		                        <td class="FieldStyle">Country</td>
		                        <td>
		                            <asp:DropDownList ID="lstBillingCountryCode" Width="200" runat="server"></asp:DropDownList><div><asp:requiredfieldvalidator id="Requiredfieldvalidator13" ErrorMessage="Select Country Code" ControlToValidate="lstBillingCountryCode" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupCreateUser"></asp:requiredfieldvalidator></div>
		                        </td>
	                        </tr>
	                        <tr>
	                            <td><uc1:spacer id="Spacer5" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></td>
	                            <td></td>
	                        </tr> 
	                        <tr class="Row">	                    
	                            <td colspan="2" align="center"><asp:CheckBox ID="chkSameAsBilling" runat="server" Text="Shipping Address is same as Billing" Checked="false" OnCheckedChanged="chkSameAsBilling_CheckedChanged" AutoPostBack="true" /> </td>
	                        </tr> 
	                        <tr>
	                            <td><uc1:spacer id="Spacer4" SpacerHeight="15" SpacerWidth="10" runat="server"></uc1:spacer></td>
	                            <td></td>
	                        </tr> 
	                        <tr>
	                            <td></td>
	                            <td><asp:Button ID="btnUpdate" runat="server" Text="Create Account" OnClick="btnUpdate_Click" CssClass="Button" ValidationGroup="groupCreateUser" /></td>
	                            <td><asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="Button" CausesValidation="false" /></td>
	                        </tr>         	
                        </table>                
                    </td>
                    <td><div><uc1:spacer id="Spacer8" SpacerHeight="20" SpacerWidth="50" runat="server"></uc1:spacer></div></td>
                    <td valign="top" width="50%">
                        <asp:Panel ID="pnlShipping" runat="server" Visible="true">            
                            <div class="Form">
                                <table>	
	                                <tr class="Row">
		                                <td class="HeaderStyle" colspan="2">Shipping Address</td>
	                                </tr>
	                                <tr>
	                                    <td><uc1:spacer id="Spacer6" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></td>
	                                </tr>		            
	                                <tr class="Row">
		                                <td class="FieldStyle">First Name</td>
		                                <td><asp:textbox id="txtShippingFirstName" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox><div><asp:requiredfieldvalidator id="Requiredfieldvalidator5" ErrorMessage="Enter First Name" ControlToValidate="txtShippingFirstName" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupCreateUser"></asp:requiredfieldvalidator></div></td>
	                                </tr>
	                                <tr class="Row">
		                                <td class="FieldStyle">Last Name</td>
		                                <td><asp:textbox id="txtShippingLastName" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox><div><asp:requiredfieldvalidator id="Requiredfieldvalidator7" ErrorMessage="Enter Last Name" ControlToValidate="txtShippingLastName" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupCreateUser"></asp:requiredfieldvalidator></div></td>
	                                </tr>
	                                <tr class="Row">
		                                <td class="FieldStyle">Company Name</td>
		                                <td><asp:textbox id="txtShippingCompanyName" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox></td>
	                                </tr>	
	                                <tr class="Row">
		                                <td class="FieldStyle">Phone Number</td>
		                                <td><asp:textbox id="txtShippingPhoneNumber" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox><div><asp:requiredfieldvalidator id="Requiredfieldvalidator8" ErrorMessage="Enter Phone Number" ControlToValidate="txtShippingPhoneNumber" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupCreateUser"></asp:requiredfieldvalidator></div></td>
	                                </tr> 
	                                <tr class="Row">
                                        <td class="FieldStyle">Email Address</td>
                                        <td>
                                            <asp:TextBox ID="txtShippingEmail" runat="server"></asp:TextBox>
                                            <div><asp:regularexpressionvalidator id="Regularexpressionvalidator1" runat="server" ControlToValidate="txtShippingEmail" ErrorMessage="*Please use a valid Email Address."
                                                    Display="Dynamic" ValidationExpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+" CssClass="Error" ValidationGroup="groupCreateUser"></asp:regularexpressionvalidator></div>
                                        </td>
                                    </tr>	                      
	                                <tr class="Row">
		                                <td class="FieldStyle">Street 1</td>
		                                <td><asp:textbox id="txtShippingStreet1" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox><div><asp:requiredfieldvalidator id="Requiredfieldvalidator9" ErrorMessage="Enter Street" ControlToValidate="txtShippingStreet1" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupCreateUser"></asp:requiredfieldvalidator></div></td>
	                                </tr>  
	                                <tr class="Row">
		                                <td class="FieldStyle">Street 2</td>
		                                <td><asp:textbox id="txtShippingStreet2" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox></td>
	                                </tr>  
	                                <tr class="Row">
		                                <td class="FieldStyle">City</td>
		                                <td><asp:textbox id="txtShippingCity" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox><div><asp:requiredfieldvalidator id="Requiredfieldvalidator10" ErrorMessage="Enter City" ControlToValidate="txtShippingCity" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupCreateUser"></asp:requiredfieldvalidator></div></td>
	                                </tr> 
	                                <tr class="Row">
		                                <td class="FieldStyle">State</td>
		                                <td>
		                                    <asp:textbox id="txtShippingState" runat="server" width="30" columns="10" MaxLength="2"></asp:textbox><div><asp:requiredfieldvalidator id="Requiredfieldvalidator11" ErrorMessage="Enter State" ControlToValidate="txtShippingState" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupCreateUser"></asp:requiredfieldvalidator></div>
		                                </td>
	                                </tr>  
	                                 <tr class="Row">
		                                <td class="FieldStyle">Postal Code</td>
		                                <td><asp:textbox id="txtShippingPostalCode" runat="server" width="130" columns="30" MaxLength="10"></asp:textbox><div><asp:requiredfieldvalidator id="Requiredfieldvalidator14" ErrorMessage="Enter Postal Code" ControlToValidate="txtShippingPostalCode" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupCreateUser"></asp:requiredfieldvalidator></div></td>
	                                </tr> 
	                                 <tr class="Row">
		                                <td class="FieldStyle">Country</td>
		                                <td>
		                                    <asp:DropDownList ID="lstShippingCountryCode" Width="200" runat="server"></asp:DropDownList><div><asp:requiredfieldvalidator id="Requiredfieldvalidator15" ErrorMessage="Select Country Code" ControlToValidate="lstShippingCountryCode" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupCreateUser"></asp:requiredfieldvalidator></div>
		                                </td>
	                                </tr>         	
                                </table>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>        
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
     <!--During Update Process -->
    <asp:UpdateProgress ID="UpdateProgressMozilla" runat="server" DisplayAfter="0" DynamicLayout="true" Visible="false">
    <ProgressTemplate>                                    
         <asp:Panel ID="pnlOverlay" CssClass="overlay" runat="server">
            <asp:Panel ID="pnlLoader" CssClass="loader" runat="server">
                Loading...<img id="Img1" align="absmiddle" src="~/Images/buttons/loading.gif" runat="server" />
            </asp:Panel>
        </asp:Panel>
    </ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:UpdateProgress ID="UpdateProgressIE" runat="server" DisplayAfter="0" DynamicLayout="true" Visible="false">                                
        <ProgressTemplate>
            <div id="updateProgress">
                <iframe frameborder="0" src="about:blank" style="border:0px;position:absolute;z-index:9;left:0px;top:0px;width:expression(this.offsetParent.scrollWidth);height:expression(this.offsetParent.scrollHeight);filter:progid:DXImageTransform.Microsoft.Alpha(Opacity=75, FinishOpacity=0, Style=0, StartX=0, FinishX=100, StartY=0, FinishY=100);"></iframe>
                <div style="position:absolute;z-index:10;left:expression((this.offsetParent.clientWidth/2)-(this.clientWidth/2)+this.offsetParent.scrollLeft);top:expression((this.offsetParent.clientHeight/2)-(this.clientHeight/2)+this.offsetParent.scrollTop);"><div>Loading...<img id="Img1" align="absmiddle" src="~/Images/buttons/loading.gif" runat="server" /></div></div>
            </div>
        </ProgressTemplate>                                                             
    </asp:UpdateProgress>
</div>