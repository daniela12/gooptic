<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="capture.aspx.cs" Inherits="Admin_Secure_sales_orders_capture" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <div class="Form">
      <h1>Capture Credit Card Payment</h1>
      
        <asp:Panel ID="pnlEdit" runat="server" Visible="true">
      
            <p>Please confirm if you want to capture this previously authorized payment. </p> 
            <p>Note that this function works only with <b>Authorize.Net and PayFlow Pro Gateways.</b></p>
          
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
          
            <div style="margin-bottom:20px; margin-top:20px;">
                <div><b>Order ID: </b> <asp:Label ID="lblOrderID" runat="server" /></div>
                <div><b>Transaction ID: </b> <asp:Label ID="lblTransactionID" runat="server" /></div>
                <div><b>Customer Name: </b> <asp:Label ID="lblCustomerName" runat="server" /></div> 
                <div><b>Order Total: </b> <asp:Label ID="lblTotal" runat="server" /></div>  
            </div>
                                    
            <div class="ValueField">               
                <asp:Button ID="Capture" runat="Server" CssClass="Button" Text="CAPTURE" OnClick="Capture_Click" />
                <asp:Button ID="Cancel" runat="Server" CssClass="Button" Text="CANCEL" OnClick="Cancel_Click" CausesValidation=false />
            </div>
        
        </asp:Panel>
        
        <asp:Panel ID="pnlConfirm" runat="server" Visible="false">
            <div>The transaction was successfully captured. </div>
            <div style=" margin-top:20px;"><a href="~/admin/secure/sales/orders/list.aspx" runat="server">Back to order list >></a></div>
        </asp:Panel> 
        
    </div> 
</asp:Content>

