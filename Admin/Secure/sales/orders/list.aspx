<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master"  AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="Admin_Secure_sales_orders_list" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
<asp:ScriptManager id="ScriptManager" runat="server"></asp:ScriptManager>
<div class="Form">
<table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td><h1>View Orders</h1></td>
			<td align="right"></td>
		</tr>
		<tr>
		    <td colspan="2">Use this page to search and download orders.</td>
		</tr>
</table>
<h4>Search Orders</h4> 

<asp:Panel ID="Test" DefaultButton="btnSearch" runat="server" >  
        <table border="0" width="70%">
        <tr>                
        <td style="height: 45px">
        <div class="FieldStyle">Order ID</div>
        <div class="ValueStyle"><asp:TextBox ID="txtorderid" runat="server"></asp:TextBox></div>
        </td> 
                        
        <td style="height: 45px">
        <div class="FieldStyle">First Name</div>
        <div class="ValueStyle"><asp:TextBox ID="txtfirstname" runat="server"></asp:TextBox></div>
        </td> 
           
        <td style="height: 45px">               
        <div class="FieldStyle">Last Name</div>
        <div class="ValueStyle"><asp:TextBox ID="txtlastname" runat="server"></asp:TextBox></div>
        </td>                         
        </tr>
        
        <tr>
         <td style="height: 45px">   
        <div class="FieldStyle">Company Name</div>  
        <div class="ValueStyle"><asp:TextBox ID="txtcompanyname" runat="server"></asp:TextBox></div>
        </td>      
           
        <td style="height: 45px">
        <div class="FieldStyle">Account Number</div>
        <div class="ValueStyle"><asp:TextBox ID="txtaccountnumber" runat="server"></asp:TextBox></div>
        </td>        
        
        <td>
        <div class="FieldStyle">Order Status</div>                           
        <div class="ValueStyle"><asp:DropDownList ID="ListOrderStatus" runat="server"></asp:DropDownList></div>        
        </td>
        </tr> 
        
        <tr>
        <td><div class="FieldStyle">Begin Date</div>
        <div class="ValueStyle">
                        <asp:TextBox id="txtStartDate" Text='' runat="server" />&nbsp;<asp:ImageButton ID="imgbtnStartDt" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/icons/SmallCalendar.gif" /><br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Begin date" ControlToValidate="txtEndDate" ValidationGroup="grpReports" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator></div>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtStartDate"
                            CssClass="Error" Display="Dynamic" ErrorMessage="* Enter Valid Date in MM/DD/YYYY format"
                            ValidationExpression="((^(10|12|0?[13578])([/])(3[01]|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(11|0?[469])([/])(30|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(2[0-8]|1[0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(29)([/])([2468][048]00)$)|(^(0?2)([/])(29)([/])([3579][26]00)$)|(^(0?2)([/])(29)([/])([1][89][0][48])$)|(^(0?2)([/])(29)([/])([2-9][0-9][0][48])$)|(^(0?2)([/])(29)([/])([1][89][2468][048])$)|(^(0?2)([/])(29)([/])([2-9][0-9][2468][048])$)|(^(0?2)([/])(29)([/])([1][89][13579][26])$)|(^(0?2)([/])(29)([/])([2-9][0-9][13579][26])$))" ValidationGroup="grpReports"></asp:RegularExpressionValidator>                                        
                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" Enabled="true" PopupButtonID="imgbtnStartDt" runat="server" TargetControlID="txtStartDate"></ajaxToolKit:CalendarExtender>
        </td>
        
        <td><div class="FieldStyle">End Date</div>
        <div class="ValueStyle">
                        <asp:TextBox id="txtEndDate" Text='' runat="server" />&nbsp;<asp:ImageButton ID="ImgbtnEndDt" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/icons/SmallCalendar.gif" /><br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtStartDate" ErrorMessage="Enter End date" ValidationGroup="grpReports" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div> 
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEndDate"
                            CssClass="Error" Display="Dynamic" ErrorMessage="* Enter Valid Date in MM/DD/YYYY format"
                            ValidationExpression="((^(10|12|0?[13578])([/])(3[01]|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(11|0?[469])([/])(30|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(2[0-8]|1[0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(29)([/])([2468][048]00)$)|(^(0?2)([/])(29)([/])([3579][26]00)$)|(^(0?2)([/])(29)([/])([1][89][0][48])$)|(^(0?2)([/])(29)([/])([2-9][0-9][0][48])$)|(^(0?2)([/])(29)([/])([1][89][2468][048])$)|(^(0?2)([/])(29)([/])([2-9][0-9][2468][048])$)|(^(0?2)([/])(29)([/])([1][89][13579][26])$)|(^(0?2)([/])(29)([/])([2-9][0-9][13579][26])$))" ValidationGroup="grpReports"></asp:RegularExpressionValidator>                    
                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" Enabled="true" PopupButtonID="ImgbtnEndDt" runat="server" TargetControlID="txtEndDate"></ajaxToolKit:CalendarExtender>
        <div class="ValueStyle">
         <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
            ControlToValidate="txtEndDate" CssClass="Error" Display="Dynamic" ErrorMessage=" End Date must be greater than Begin date"
            Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
        </div>
        </td>
        </tr>    
        
        <tr>            
        <td colspan="2">       
         <div class="ValueStyle">
         <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" CssClass="Button" />
         <asp:Button ID="btnClearSearch" runat="server" OnClick="btnClearSearch_Click" Text="Clear Search" CssClass="Button" CausesValidation="False" />
         </div>        
        </td>
        
        <td>
        </td>
        </tr>        
      </table>  
    </asp:Panel>	             
    <h4> Order List </h4>
    <div align="left">
    <table> 
    <tr>
    <td>
        <div class="FieldStyle">Beginning Order ID</div>
        <small>Enter the beginning order ID that you want to download. All orders after this order ID will be downloaded.</small>
    </td>
    </tr>       
        <tr>
           <td>
                <asp:TextBox ID="OrderNumber" runat="server"></asp:TextBox>            
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="OrderNumber" ValidationGroup="Download" CssClass="Error" Display="Dynamic" ErrorMessage="You must specify a starting Order ID"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="OrderNumber" ValidationGroup="Download" CssClass="Error" Display="Dynamic" ErrorMessage="Enter Valid starting Order ID" ValidationExpression="^[0-9]*"></asp:RegularExpressionValidator>                
           </td>
        </tr>
        <tr>
            <td><small></small><br /><br /></td>
        </tr>
        <tr>
            <td><div class="Error"><asp:Literal ID="ltrlError" runat="server"></asp:Literal></div></td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="ButDownload" runat="server" OnClick="ButDownload_Click" Text="Download Orders to Excel" CssClass="Button"  ValidationGroup="Download"/>&nbsp;&nbsp;<asp:Button ID="ButOrderLineItems" runat="server" OnClick="ButOrderLineItemsDownload_Click" Text="Download Order Line Items to Excel" CssClass="Button" Width="250px" ValidationGroup="Download" /><br /><br />
            </td>
        </tr>
    </table>
    </div>
    <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" CaptionAlign="Left" AutoGenerateColumns="False" GridLines="None"  AllowPaging="True" PageSize="25" OnPageIndexChanging="uxGrid_PageIndexChanging" OnRowCommand="uxGrid_RowCommand" EmptyDataText="No Orders exist in the database." Width="100%" DataKeyNames="orderid" OnSorting="uxGrid_Sorting" CellPadding="4" AllowSorting="True">
        <Columns>
         <asp:HyperLinkField DataNavigateUrlFields="orderid" DataNavigateUrlFormatString="view.aspx?itemid={0}"
                DataTextField="orderid" HeaderText="ID" SortExpression="orderid" />
            
            <asp:TemplateField SortExpression="OrderStateID" HeaderText="Order Status">
            <ItemTemplate>
            <asp:Label ID="OrderStatus" Text='<%# DisplayOrderStatus(Eval("OrderStateID")) %>' runat="server" Font-Bold=true Font-Size=Smaller></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Payment Status">
            <ItemTemplate>
            <asp:Label ID="PaymentStatus" Text='<%# DisplayPaymentStatus(Eval("PaymentStatusID")) %>' runat="server" Font-Bold=true Font-Size=Smaller></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField SortExpression="ShipFirstName">
            <HeaderTemplate>
             <asp:Label ID="headerTotal" text="Name" runat="server"></asp:Label>
            </HeaderTemplate>
            <ItemTemplate>
            <asp:Label ID="CustomerName" Text='<%# ReturnName(Eval("BillingFirstName"),Eval("BillingLastName")) %>' runat="server" ></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField DataField="Orderdate" HeaderText="Date" SortExpression="OrderDate" DataFormatString="{0:d}" HtmlEncode="False" />
            <asp:TemplateField SortExpression="total" HeaderText="Amount">
            <ItemTemplate>
                       <%# DataBinder.Eval(Container.DataItem, "total","{0:c}").ToString()%>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField SortExpression="PaymentTypeID" HeaderText="Payment Type">
            <ItemTemplate>
            <asp:Label ID="PaymentType" Text='<%# DisplayPaymentType(Eval("PaymentTypeID")) %>' runat="server" ></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <ItemTemplate>
                    <asp:Button runat="server" CausesValidation="False" ID="ViewOrder" Text="View"  CssClass="Button" CommandArgument='<%# Eval("orderid") %>' CommandName="ViewOrder" />
            </ItemTemplate>
            </asp:TemplateField>           
            <asp:TemplateField>
            <ItemTemplate>
                    <asp:Button runat="server" Enabled='<%# EnableRefund(Eval("PaymentTypeID")) %>' CausesValidation="False" ID="RefundOrder" Text="Refund"  CssClass="Button" CommandArgument='<%# Eval("orderid") %>' CommandName="RefundOrder" />
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <ItemTemplate>
                    <asp:Button runat="server"  Enabled='<%# EnableCapture(Eval("PaymentStatusID")) %>' CausesValidation="false" ID="Capture" Text="Capture"  CssClass="Button" CommandArgument='<%# Eval("orderid") %>' CommandName="Capture" />
            </ItemTemplate>
            </asp:TemplateField>               
            <asp:TemplateField>
            <ItemTemplate>
                    <asp:Button runat="server" CausesValidation="false" ID="ChangeStatus" Text="Status"  CssClass="Button" CommandArgument='<%# Eval("orderid") %>' CommandName="Status" />
            </ItemTemplate>
            </asp:TemplateField>                          
        </Columns>
        <RowStyle CssClass="RowStyle" />
        <EditRowStyle CssClass="EditRowStyle" />
        <HeaderStyle CssClass="HeaderStyle" />
        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
        
     </asp:GridView> 
    
           
    
    </div>
</asp:Content>


