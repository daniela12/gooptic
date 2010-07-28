<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="orderStatus.aspx.cs" Inherits="Admin_Secure_sales_orders_orderStatus" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div class="Form">
      <h1>Change Order Status</h1>
      
      <div style="margin-bottom:20px;">
        <div><b>Order ID: </b> <asp:Label ID="lblOrderID" runat="server" /></div>
        <div><b>Customer Name: </b> <asp:Label ID="lblCustomerName" runat="server" /></div> 
        <div><b>Order Total: </b> <asp:Label ID="lblTotal" runat="server" /></div>  
      </div>
      
      <p>Use this page to change the order's status. Note that changing the status can trigger other actions (such as email alerts) based on your store configuration.</p><br />
      
      <p>Note: Changing the status will not Void or Refund credit card transactions. Please use the "Refund" feature for this.</p>
      
      <div class="FieldStyle"><asp:DropDownList ID="ListOrderStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OrderStatus_SelectedIndexChanged"/></div>
      <br />
      
      <asp:Panel ID="pnlTrack" runat="server">      
      <div class="FieldStyle"><asp:Label ID="TrackTitle" runat="server"></asp:Label></div>
      <div class="ValueStyle"><asp:TextBox ID="TrackingNumber" runat="server"></asp:TextBox>&nbsp&nbsp&nbsp&nbsp&nbsp<asp:Button ID="EmailStatus" runat="Server" CssClass="Button" Text="Email Status" OnClick="EmailStatus_Click"/></div>               
      <div><asp:Label ID="trackmessage" runat="server"></asp:Label></div>
      </asp:Panel>
      
      <br />     
      
      <div class="ValueField">
          <asp:Button ID="UpdateOrderStatus" runat="Server" CssClass="Button" Text="Update" OnClick="UpdateOrderStatus_Click" />
          <asp:Button ID="Cancel" runat="Server" CssClass="Button" Text="Cancel" OnClick="CancelStatus_Click" />
      </div>

      <uc1:spacer id="LongSpace" SpacerHeight="20" SpacerWidth="3" runat="server"></uc1:spacer>
        
    </div> 
    
    <uc1:spacer id="Spacer2" SpacerHeight="200" SpacerWidth="3" runat="server"></uc1:spacer>
</asp:Content>


