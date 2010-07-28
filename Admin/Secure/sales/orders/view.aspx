<%@ Page Language="C#"  MasterPageFile="~/Admin/Themes/Standard/edit.master"  AutoEventWireup="true" CodeFile="view.aspx.cs" Inherits="Admin_Secure_sales_orders_view" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
<div class="Form">
    <table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td><h1>Order ID: <asp:Label ID="lblOrderHeader" runat="server" Text="Label" /></h1></td>
			<td align="right">
			      <asp:Button ID="List" CssClass="Button" runat="server" text="< Back to Order List" OnClick="List_Click" />
			      <asp:Button ID="Refund" CssClass="Button" runat="server" text="Void or Refund Transaction" OnClick="Refund_Click" />
			      <asp:Button ID="ChangeStatus" CssClass="Button" runat="server" text="Change Order Status" OnClick="ChangeStatus_Click" />
			      
            </td>
		</tr>
	</table>
	
    
    <table cellpadding="10" width="100%">
        <tr>
            <td valign="top">
                <!-- Order Info -->
                <h4>Order Information</h4>
                <table cellpadding="3" border="0" cellspacing="0" class="ViewForm">
                    <tr class="RowStyle"> 
                        <td class="FieldStyle">Order Status</td>
                        <td class="ValueStyle">
                            <asp:Label ID="lblOrderStatus" Font-Bold="true" runat="server"/>
                        </td>
                    </tr>
                    <tr class="RowStyle">
                           <td class="FieldStyle">Payment Status</td>
                           <td class="ValueStyle"><asp:Label ID="lblPaymentStatus" Font-Bold="true" runat="server" /></td>
                    </tr>
                    
                    <tr class="AlternatingRowStyle">
                        <td class="FieldStyle">Order Date</td>
                        <td class="ValueField">
                                    <asp:Label ID="lblOrderDate" runat="server"/>
                        </td>
                    </tr>
                    
                 
                    
                    <tr class="AlternatingRowStyle">
                        <td class="FieldStyle">Order Amount</td>
                        <td class="ValueStyle">
                                <asp:Label ID="lblOrderAmount" runat="server" />
                        </td>
                    </tr>
                    
                    <tr class="RowStyle">
                        <td class="FieldStyle">Shipping Amount</td>
                        <td class="ValueStyle">
                            <asp:Label ID="lblShipAmount" runat="server" />
                        </td>
                    </tr>
                    
                    <tr class="AlternatingRowStyle">
                        <td class="FieldStyle">Tax Amount</td>
                        <td class="ValueStyle">
                                                <asp:Label ID="lblTaxAmount" runat="server" />
                        </td>
                    </tr>
                    
                    <tr class="RowStyle">
                        <td class="FieldStyle">Discount Amount</td>
                        <td class="ValueStyle"><asp:Label ID="lblDiscountAmt" runat="server" /></td>
                    </tr>
                    
                    <tr class="AlternatingRowStyle">
                           <td class="FieldStyle">Payment Method</td>
                           <td class="ValueStyle"><asp:Label ID="lblPaymentType" runat="server" /></td>
                    </tr>
                    <tr class="RowStyle">
                        <td class="FieldStyle">Transaction ID</td>
                        <td class="ValueStyle"><asp:Label ID="lblTransactionId" runat="server" /></td>
                    </tr> 
                    
                    <tr class="AlternatingRowStyle">
                           <td class="FieldStyle">Purchase Order</td>
                           <td class="ValueStyle"><asp:Label ID="lblPurchaseOrder" runat="server" /></td>
                    </tr>
                    
                   
                    
                    <tr class="AlternatingRowStyle">
                        <td class="FieldStyle">Shipping Method</td>
                        <td class="ValueStyle"><asp:Label ID="lblShippingMethod" runat="server" /></td>
                    </tr>
                    <tr class="RowStyle">
                        <td class="FieldStyle">Tracking Number</td>
                        <td class="ValueStyle"><asp:Label ID="lblTrackingNumber" runat="server" /></td>
                    </tr>
                    <tr class="AlternatingRowStyle">
                        <td class="FieldStyle">Promotion Codes</td>
                        <td class="ValueStyle"><asp:Label ID="lblCouponCode" runat="server" /></td>
                    </tr>               
                </table>
                  
            </td>
            
            <td valign="top">
                <!-- Address -->
                <h4>Customer Information</h4>
    
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td width="50%"  valign="top">
                        <div class="FieldStyle">Billing Address</div>
                        <div class="ValueStyle">
                                <asp:Label ID="lblBillingAddress" runat="server" Text="Label"></asp:Label></div> 
                    </td>
                    <td align="left" width="50%"  valign="top">
                        <div class="FieldStyle">Shipping Address</div>
                        <div class="ValueStyle">
                                    <asp:Label ID="lblShippingAddress" runat="server" Text="Label"></asp:Label>
                        </div>
                    </td>
                </tr>     
                </table>                     
            
            </td>    
        </tr>    
    </table>
        
     
             <asp:Panel ID="ShippingErrorPanel" runat="server">
    <h4><asp:Label ID= "ErrorHeader" Text="Shipping Errors" runat="server"></asp:Label></h4>
    <div align="justify"><asp:Label ID="ShippingErrors" runat="server" CssClass="Error" ></asp:Label></div>
    </asp:Panel>
    
    <h4>Ship Together Order Items</h4>
    <asp:GridView ID="uxGrid" ShowFooter="true"  ShowHeader="true" CaptionAlign="Left" runat="server" ForeColor="Black" CellPadding="4"  AutoGenerateColumns="False" CssClass="Grid" Width="100%" GridLines="None" EmptyDataText="No orderline items found" AllowPaging="True" OnPageIndexChanging="uxGrid_PageIndexChanging" OnRowCommand="uxGrid_RowCommand" PageSize="10" >
        <Columns>
        <asp:BoundField DataField="OrderLineItemID" HeaderText="Line Item ID"/>
        <asp:BoundField DataField="Name" HeaderText="Product Name" />
        <asp:BoundField DataField="ProductNum" HeaderText="Product Code" />
          <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                   <%# DataBinder.Eval(Container.DataItem, "Description") %>
                </ItemTemplate> 
            </asp:TemplateField>
        <asp:BoundField DataField="SKU" HeaderText="SKU" />
        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
        <asp:TemplateField HeaderText="Price">
        <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "price","{0:c}").ToString()%>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="ShipDate" HeaderText="Ship Date" DataFormatString="{0:d}" HtmlEncode="False" />
        <asp:BoundField DataField="TrackingNumber" HeaderText="Tracking Number" />   
        </Columns>
        <EmptyDataTemplate>
            No orderline items found
         </EmptyDataTemplate>
           <RowStyle CssClass="RowStyle" />
           <HeaderStyle CssClass="HeaderStyle" />
           <AlternatingRowStyle CssClass="AlternatingRowStyle" />
      <FooterStyle CssClass="FooterStyle" />
        <PagerStyle CssClass="PagerStyle" />
    </asp:GridView>
<asp:Panel ID="ShippingPanel" runat="server">
<asp:Panel ID ="DemensionPanel" runat="server">
<br />

<table width="50%">
<tr>
<td>We have rougly estimated your package size and weight based on information in the Product database. If this estimate is incorrect please update it before creating your shipment. </td>
</tr>
</table>
<br />
                   <asp:RegularExpressionValidator ID="EstimatedWeightValidator" ControlToValidate="EstimatedWeight" ErrorMessage="Please enter a Decimal Value for Weight" runat="server" ValidationExpression="^(?!0*\.?0*$)([0-9]+(\.)?[0-9]?[0-9]?)$" ValidationGroup="OrderEstimate"></asp:RegularExpressionValidator><br />
                   <asp:RegularExpressionValidator ID="EstimatedHeightValidator" ControlToValidate="EstimatedHeight" ErrorMessage="Please enter a whole number for Height" runat="server" ValidationExpression="^(?!0*\.?0*$)([0-9]+?[0-9]?[0-9]?)$" ValidationGroup="OrderEstimate"></asp:RegularExpressionValidator><br />
                   <asp:RegularExpressionValidator ID="EstimatedLengthValidator" ControlToValidate="EstimatedLength" ErrorMessage="Please enter a whole number for Length" runat="server" ValidationExpression="^(?!0*\.?0*$)([0-9]+?[0-9]?[0-9]?)$" ValidationGroup="OrderEstimate"></asp:RegularExpressionValidator><br />
                   <asp:RegularExpressionValidator ID="EstimatedWidthValidator" ControlToValidate="EstimatedWidth" ErrorMessage="Please enter a whole number for Width" runat="server" ValidationExpression="^(?!0*\.?0*$)([0-9]+?[0-9]?[0-9]?)$" ValidationGroup="OrderEstimate"></asp:RegularExpressionValidator><br />
                   <asp:RequiredFieldValidator ID="RequiredEstimatedWidth" ControlToValidate="EstimatedWidth" ErrorMessage="Width must be specified to create a shipment" runat="server" ValidationGroup="RequiredEstimate"></asp:RequiredFieldValidator><br />
                   <asp:RequiredFieldValidator ID="RequiredEstimatedLength" ControlToValidate="EstimatedLength" ErrorMessage="Length must be specified to create a shipment" runat="server" ValidationGroup="RequiredEstimate"></asp:RequiredFieldValidator><br />
                   <asp:RequiredFieldValidator ID="RequiredEstimatedHeight" ControlToValidate="EstimatedHeight" ErrorMessage="Height must be specified to create a shipment" runat="server" ValidationGroup="RequiredEstimate"></asp:RequiredFieldValidator><br />
                   <asp:RequiredFieldValidator ID="RequiredEstimatedWeight" ControlToValidate="EstimatedWeight" ErrorMessage="Weight must be specified to create a shipment" runat="server" ValidationGroup="RequiredEstimate"></asp:RequiredFieldValidator><br />
<table border="0" cellpadding="0" cellspacing="0" width="50%">
                
                
                
                <tr>
                    <td width="25%"  valign="top">
                        <div class="FieldStyle">Weight</div>
                        <div class="ValueStyle">
                               <asp:TextBox ID="EstimatedWeight" runat="server" Width="50"></asp:TextBox> 
                               </div>
                    </td>
                    <td align="left" width="25%"  valign="top">
                        <div class="FieldStyle">Height</div>
                        <div class="ValueStyle">
                           <asp:TextBox ID="EstimatedHeight" runat="server" Width="50"></asp:TextBox>      
                        </div>
                    </td>
                <td width="25%"  valign="top">
                        <div class="FieldStyle">Length</div>
                        <div class="ValueStyle">
                               <asp:TextBox ID="EstimatedLength" runat="server" Width="50"></asp:TextBox>
                               </div>
                    </td>
                    <td align="left" width="25%"  valign="top">
                        <div class="FieldStyle">Width</div>
                        <div class="ValueStyle">
                           <asp:TextBox ID="EstimatedWidth" runat="server" Width="50"></asp:TextBox>
                        </div>
                    </td>
                </tr>      
                </table>        
    </asp:Panel>
    
    
    <br />
    <br />
    <asp:Button runat="server" CausesValidation="true" ID="Shipping" Text="Ship Order"  CssClass="Button" CommandArgument="" OnClick="Shipping_Click" />
    <asp:Button runat="server" CausesValidation="false" ID="LabelButton" Text="Print Label"  CssClass="Button" OnClick="Label_Click"/>
</asp:Panel>
        <h4>Ship Seperate Order Items</h4>
    <asp:GridView ID="uxGrid2" ShowFooter="true"  ShowHeader="true" CaptionAlign="Left" runat="server" ForeColor="Black" CellPadding="4"  AutoGenerateColumns="False" CssClass="Grid" Width="100%" GridLines="None" EmptyDataText="No orderline items found" AllowPaging="True" OnPageIndexChanging="uxGrid_PageIndexChanging" OnRowCommand="uxGrid_RowCommand" PageSize="10" >
        <Columns>
        <asp:BoundField DataField="OrderLineItemID" HeaderText="Line Item ID"/>
        <asp:BoundField DataField="Name" HeaderText="Product Name" />
        <asp:BoundField DataField="ProductNum" HeaderText="Product Code" />
          <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                   <%# DataBinder.Eval(Container.DataItem, "Description") %>
                </ItemTemplate> 
            </asp:TemplateField>
        <asp:BoundField DataField="SKU" HeaderText="SKU" />
        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
        <asp:TemplateField HeaderText="Price">
        <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "price","{0:c}").ToString()%>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="ShipDate" HeaderText="Ship Date" DataFormatString="{0:d}" HtmlEncode="False" />
        <asp:TemplateField HeaderText="Shipping Cost">
        <ItemTemplate>
        <%#DataBinder.Eval(Container.DataItem, "ShippingCost", "{0:c}").ToString()%>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="TrackingNumber" HeaderText="Tracking Number" />
         <asp:TemplateField>
            <ItemTemplate>
                    <asp:Button runat="server" CausesValidation="true" ID="CancelShipping" Text='<%# ButtonText(Eval("TrackingNumber"),Eval("OrderLineItemID")) %>'  CssClass="Button" CommandArgument='<%# Eval("OrderLineItemID") %>' CommandName='<%# ShippingCommand(Eval("TrackingNumber"),Eval("OrderLineItemID")) %>' Visible = '<%#isAdvancedShipping() %>' />
            </ItemTemplate>
            </asp:TemplateField>     
            <asp:TemplateField>
            <ItemTemplate>
            <asp:Button runat="server" CausesValidation="false" ID="Label" Text="Print Label"  CssClass="Button" CommandArgument='<%# Eval("TrackingNumber") %>' CommandName="Label" Visible='<%# ShowLabelButton(Eval("TrackingNumber")) %>' />
            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No orderline items found
         </EmptyDataTemplate>
           <RowStyle CssClass="RowStyle" />
           <HeaderStyle CssClass="HeaderStyle" />
           <AlternatingRowStyle CssClass="AlternatingRowStyle" />
      <FooterStyle CssClass="FooterStyle" />
        <PagerStyle CssClass="PagerStyle" />
    </asp:GridView>
    <asp:HiddenField runat="server" ID="EstimatedLineItemID" />
    
    <asp:Panel ID ="LineItemDemensions" runat="server">
<br />
<table width="50%">
<tr>
<td>To Estimate shipping for a ship seperate order item click "Estimate Dimensions".  If this estimate is incorrect please update it before creating your shipment. </td>
</tr>
</table>
<br />
<asp:RequiredFieldValidator ID="LineItemWidthRequired" ControlToValidate="LineItemWidth" ErrorMessage="Width must be specified to create a shipment" runat="server" ValidationGroup="RequiredLineItems"></asp:RequiredFieldValidator><br />
                   <asp:RequiredFieldValidator ID="LineItemLengthRequired" ControlToValidate="LineItemLength" ErrorMessage="Length must be specified to create a shipment" runat="server" ValidationGroup="RequiredLineItems"></asp:RequiredFieldValidator><br />
                   <asp:RequiredFieldValidator ID="LineItemHeightRequired" ControlToValidate="LineItemHeight" ErrorMessage="Height must be specified to create a shipment" runat="server" ValidationGroup="RequiredLineItems"></asp:RequiredFieldValidator><br />
                   <asp:RequiredFieldValidator ID="LineItemWeightRequired" ControlToValidate="LineItemWeight" ErrorMessage="Weight must be specified to create a shipment" runat="server" ValidationGroup="RequiredLineItems"></asp:RequiredFieldValidator><br />

<asp:RegularExpressionValidator Display="static" ID="LineItemWeightValidator" ControlToValidate="LineItemWeight" ErrorMessage="Please enter a decimal for Weight" runat="server" ValidationExpression="^(?!0*\.?0*$)([0-9]+(\.)?[0-9]?[0-9]?)$" ValidationGroup="1OrderEstimate"></asp:RegularExpressionValidator><br />
                <asp:RegularExpressionValidator ID="LineItemHeightValidator" ControlToValidate="LineItemHeight" ErrorMessage="Please enter a whole number for Height" runat="server" ValidationExpression="^(?!0*\.?0*$)([0-9]+?[0-9]?[0-9]?)$" ValidationGroup="1OrderEstimate"></asp:RegularExpressionValidator><br />
                <asp:RegularExpressionValidator ID="LineItemLengthValidator" ControlToValidate="LineItemLength" ErrorMessage="Please enter a whole number for Length" runat="server" ValidationExpression="^(?!0*\.?0*$)([0-9]+?[0-9]?[0-9]?)$" ValidationGroup="1OrderEstimate"></asp:RegularExpressionValidator><br />
                <asp:RegularExpressionValidator ID="LineItemWidthValidator" ControlToValidate="LineItemWidth" ErrorMessage="Please enter a whole number for Width" runat="server" ValidationExpression="^(?!0*\.?0*$)([0-9]+?[0-9]?[0-9]?)$" ValidationGroup="1OrderEstimate"></asp:RegularExpressionValidator><br />
<table border="0" cellpadding="0" cellspacing="0" width="50%">
                <tr>
                    <td width="25%"  valign="top">
                        <div class="FieldStyle">Weight</div>
                        <div class="ValueStyle">
                               <asp:TextBox ID="LineItemWeight" runat="server" Width="50"></asp:TextBox> 
                               </div>
                    </td>
                    <td align="left" width="25%"  valign="top">
                        <div class="FieldStyle">Height</div>
                        <div class="ValueStyle">
                           <asp:TextBox ID="LineItemHeight" runat="server" Width="50"></asp:TextBox>      
                        </div>
                    </td>
                <td width="25%"  valign="top">
                        <div class="FieldStyle">Length</div>
                        <div class="ValueStyle">
                               <asp:TextBox ID="LineItemLength" runat="server" Width="50"></asp:TextBox></div>
                    </td>
                    <td align="left" width="25%"  valign="top">
                        <div class="FieldStyle">Width</div>
                        <div class="ValueStyle">
                           <asp:TextBox ID="LineItemWidth" runat="server" Width="50"></asp:TextBox>
                        </div>
                    </td>
                </tr>      
                </table>        
    </asp:Panel>
    <h4>Additional Instructions</h4>
    <div align="justify"><asp:Label ID="lblAdditionalInstructions" runat="server"></asp:Label></div>
    

</div>
</asp:Content>

