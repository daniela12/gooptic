<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Payment.ascx.cs" Inherits="Admin_Secure_Enterprise_OrderDesk_Payment" %>
<%@ Register Src="~/Controls/spacer.ascx" TagName="spacer" TagPrefix="uc1" %>
<%@ Register TagPrefix="ZNode" Assembly="ZNode.Libraries.ECommerce.Catalog" Namespace="ZNode.Libraries.ECommerce.Catalog"  %>


<div class="Form">
<h4>Shipping</h4>
    <asp:Panel ID="pnlShipping" runat="server" Visible="true">
        <table cellpadding="0" cellspacing="0" border="0">	
            <tr class="Row">
                <td></td>
                <td><uc1:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="5" runat="server"></uc1:spacer></td>
            </tr>
            <tr class="Row">
                <td class="FieldStyle">Shipping Option</td>
                <td><asp:DropDownList ID="lstShipping" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstShipping_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>
            <tr class="Row">
                <td></td>
                <td><div class="Error"><asp:Literal ID="uxErrorMsg" EnableViewState="false" runat="server"></asp:Literal></div></td>
            </tr>
        </table>
    </asp:Panel>    
    <asp:Panel ID="pnlPayment" runat="server" Visible="true">
        <h4>Payment</h4>
        <table cellpadding="0" cellspacing="0" border="0">
            <tr class="Row">
                <td></td>
                <td><uc1:spacer id="Spacer3" SpacerHeight="5" SpacerWidth="5" runat="server"></uc1:spacer></td>
            </tr>          
            <tr class="Row">
                <td class="FieldStyle">Payment Option</td>
                <td><asp:DropDownList ID="lstPaymentType" runat="server" OnSelectedIndexChanged="lstPaymentType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td> 
            </tr>
             <tr class="Row">
                <td></td>
                <td><uc1:spacer id="Spacer8" SpacerHeight="5" SpacerWidth="5" runat="server"></uc1:spacer></td>
            </tr>  
        </table>
        <!-- COD payment section -->
        <asp:Panel ID="pnlCOD" runat="server" Visible="false">
            You have selected the COD option for payment. You would need to pay the entire amount on delivery.
        </asp:Panel>
        <!-- purchase order payment section -->
        <asp:Panel ID="pnlPurchaseOrder" runat="server" Visible="false">
            <table cellpadding="0" cellspacing="0" border="0">	                                                                                 
                <tr class="Row">
                    <td></td>
                    <td><uc1:spacer id="Spacer4" SpacerHeight="5" SpacerWidth="5" runat="server"></uc1:spacer></td>
                </tr>      
                <tr class="Row">
                    <td class="FieldStyle">Purchase Order Number</td>
                    <td><asp:textbox id="txtPONumber" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox>
                        <div><asp:requiredfieldvalidator Id="Requiredfieldvalidator1" ErrorMessage="Enter Purchase Order Number" ControlToValidate="txtPONumber" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupPayment"></asp:requiredfieldvalidator></div>                       
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <!-- Credit card payment section-->
        <asp:Panel ID="pnlCreditCard" runat="server">
            <table cellpadding="0" cellspacing="0" border="0">	                                                                                 
                <tr class="Row">
                    <td></td>
                    <td><uc1:spacer id="Spacer1" SpacerHeight="5" SpacerWidth="5" runat="server"></uc1:spacer></td>
                </tr>      
                <tr class="Row">
                    <td class="FieldStyle">Card Number</td>
                    <td><asp:textbox id="txtCreditCardNumber" runat="server" width="130" columns="30" MaxLength="20" autoComplete="off"></asp:textbox>&nbsp;&nbsp;<img id="imgVisa" src="~/images/shopping_cart/card_visa.gif" runat="server" align="absmiddle" /><img id="imgMastercard" src="~/images/shopping_cart/card_mastercard.gif" runat="server" align="absmiddle" /><img id="imgAmex" src="~/images/shopping_cart/card_amex.gif" align="absmiddle" runat="server" /><img id="imgDiscover" src="~/images/shopping_cart/card_discover.gif" align="absmiddle" runat="server" />
                        <div><asp:requiredfieldvalidator id="req1" ErrorMessage="Enter Credit Card Number" ControlToValidate="txtCreditCardNumber" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupPayment"></asp:requiredfieldvalidator></div> 
                        <div><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCreditCardNumber"
                        Display="Dynamic" ErrorMessage="* Enter Valid Credit Card Number" ValidationExpression="^[3|4|5|6]([0-9]{15}$|[0-9]{12}$|[0-9]{13}$|[0-9]{14}$)" ValidationGroup="groupPayment"></asp:RegularExpressionValidator></div>
                        <div><ZNode:ZNodeCreditCardValidator Id="MyValidator" ControlToValidate="txtCreditCardNumber" ErrorMessage="Please enter a Valid Credit Card Number" Display="Dynamic" Runat="server" ValidateCardType="True" ValidationGroup="groupPayment" /></div>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldStyle">Expiration Date</td>
                    <td>
                        <asp:DropDownList ID="lstMonth" runat="server">
                            <asp:ListItem Value="">-- Month --</asp:ListItem>    
                            <asp:ListItem Value="01">Jan</asp:ListItem>
                            <asp:ListItem Value="02">Feb</asp:ListItem>
                            <asp:ListItem Value="03">Mar</asp:ListItem>
                            <asp:ListItem Value="04">Apr</asp:ListItem>
                            <asp:ListItem Value="05">May</asp:ListItem>
                            <asp:ListItem Value="06">Jun</asp:ListItem>
                            <asp:ListItem Value="07">Jul</asp:ListItem>
                            <asp:ListItem Value="08">Aug</asp:ListItem>
                            <asp:ListItem Value="09">Sep</asp:ListItem>
                            <asp:ListItem Value="10">Oct</asp:ListItem>
                            <asp:ListItem Value="11">Nov</asp:ListItem>
                            <asp:ListItem Value="12">Dec</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                        <asp:DropDownList ID="lstYear" runat="server"></asp:DropDownList>
                   </td>
                </tr>	          
                 <tr class="Row">
                    <td class="FieldStyle">Security Code</td>
                    <td><asp:textbox id="txtCVV" runat="server" width="30" autoComplete="off" columns="30" MaxLength="4"></asp:textbox>&nbsp;&nbsp;<a href="javascript:popupWin=window.open('/web/cvv.htm','EIN','scrollbars,resizable,width=515,height=300,left=50,top=50');popupWin.focus();" runat="server">help</a><div> <asp:requiredfieldvalidator id="Requiredfieldvalidator2" ErrorMessage="Enter Credit Card Security Code" ControlToValidate="txtCVV" Runat="server" Display="Dynamic" CssClass="Error" ValidationGroup="groupPayment"></asp:requiredfieldvalidator></div></td>
                </tr>
            </table>
        </asp:Panel>
        </asp:Panel>
        <table width="100%">
          <tr>
            <td colspan="2">
                <uc1:spacer id="Spacer9" SpacerHeight="15" SpacerWidth="5" runat="server" />
                <div class="HintStyle">Please enter any comments or special instructions for your order</div>
                <div>
                    <uc1:spacer id="Spacer15" SpacerHeight="15" SpacerWidth="5" runat="server" />
                    <asp:TextBox Columns="45" Rows="3" ID="txtAdditionalInstructions" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
            </td>
        </tr>
    </table>
</div>    
