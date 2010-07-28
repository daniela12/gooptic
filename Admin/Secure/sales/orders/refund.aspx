<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="refund.aspx.cs" Inherits="Admin_Secure_sales_orders_refund" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <div class="Form">
      <h1>Refund Customer's Credit Card</h1>
      
        <asp:Panel ID="pnlEdit" runat="server" Visible="true">
      
            <p>Use this page to void this transaction or apply a refund to your customer's credit card. </p> 
            <p>Note that this function works only with <b>Authorize.Net and PayFlow Pro Gateways.</b></p>
          
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
          
            <div style="margin-bottom:20px; margin-top:20px;">
                <div><b>Order ID: </b> <asp:Label ID="lblOrderID" runat="server" /></div>
                <div><b>Transaction ID: </b> <asp:Label ID="lblTransactionID" runat="server" /></div>
                <div><b>Customer Name: </b> <asp:Label ID="lblCustomerName" runat="server" /></div> 
                <div><b>Order Total: </b> <asp:Label ID="lblTotal" runat="server" /></div>  
            </div>
              
            <div>      
                <div class="FieldStyle"><asp:Label ID="lblCardNumber" runat="server">Credit Card Number (last 4 digits)</asp:Label></div>
                <div class="ValueStyle"><asp:textbox id="txtCardNumber" runat="server" width="130" columns="30" MaxLength="100" /></div>        
                
                <asp:Panel ID="pnlCreditCardInfo" runat="server" Visible="false">
                    <div class="FieldStyle">Expiration Date</div>
                    <div class="ValueStyle">
                        <div>Select Month<span style="margin-left:15px;">Select year</span></div>
                        <asp:DropDownList ID="lstMonth" runat="server" Width="80">                                   
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
                        </asp:DropDownList><span style="margin-left:5px;">
                        <asp:DropDownList ID="lstYear" runat="server" width="80"></asp:DropDownList></span>                        
                    </div>
                    
                    <div class="FieldStyle">Security Code</div>                    
                    <div class="ValueStyle">
                        <asp:Textbox id="txtSecurityCode" runat="server" columns="3" MaxLength="4" />
                        <asp:RequiredFieldValidator ControlToValidate="txtSecurityCode" CssClass="Error" Display="Dynamic" ID="SecurityCodeValidator" runat="server" ErrorMessage="Enter Security Code"></asp:RequiredFieldValidator>
                    </div>
                </asp:Panel>
                
                <div class="FieldStyle">Amount to Refund (does not apply to Voids)<span class="Asterix">*</span> </div>
                <div class="ValueStyle"><asp:textbox id="txtAmount" runat="server" width="130" columns="30" MaxLength="100" /> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAmount" runat="server" ErrorMessage="Amount is Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAmount" ErrorMessage="You must enter a valid amount" MaximumValue="99999999" Type="Currency" MinimumValue="0" Display="Dynamic" CssClass="Error"></asp:RangeValidator></div> 
            </div>  
              
            <div class="ValueField">
                
                <asp:Button ID="Void" runat="Server" CssClass="Button" Text="VOID" OnClick="Void_Click" />
                <asp:Button ID="Refund" runat="Server" CssClass="Button" Text="REFUND" OnClick="Refund_Click" />
                <asp:Button ID="Cancel" runat="Server" CssClass="Button" Text="CANCEL" OnClick="Cancel_Click" CausesValidation=false />
            </div>
        
        </asp:Panel>
        
        <asp:Panel ID="pnlConfirm" runat="server" Visible="false">
            <div>The transaction was successfully voided/refunded. </div>
            <div style=" margin-top:20px;"><a href="~/admin/secure/sales/orders/list.aspx" runat="server">Back to order list >></a></div>
        </asp:Panel> 
        
    </div> 
</asp:Content>

