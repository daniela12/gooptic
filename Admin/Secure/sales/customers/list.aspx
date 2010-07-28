<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="Admin_Secure_sales_customers_list" Title="Untitled Page" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <asp:ScriptManager id="ScriptManager" runat="server"></asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <h1>
                    Accounts
                </h1>
                
            </td>            
            <td align="right">
                <asp:button class="Button" id="AddContact" CausesValidation="True" Text="Add Account" Runat="server" OnClick="AddContact_Click"></asp:button>
            </td>
        </tr>
    </table>
    <div>Use this page to manage your accounts (leads, customers, vendors, dealers, partners, employees, etc)</div>
    <div><uc1:spacer id="Spacer8" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>
    
    <h4>Search Accounts</h4>        
    
    <asp:Panel ID="SearchCustomers" CssClass="Form" DefaultButton="btnSearch" runat="Server">
        <table border="0" width="75%">
            <tr>
                <td>
                    <div class="FieldStyle">First Name</div>        
                    <div class="ValueStyle"><asp:TextBox ID="txtFName" runat="server"></asp:TextBox></div>            
                </td>
                
                <td>
                    <div class="FieldStyle">Last Name</div>        
                    <div class="ValueStyle"><asp:TextBox ID="txtLName" runat="server"></asp:TextBox></div>
                </td>
                
                <td>
                    <div class="FieldStyle">Company Name</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtComapnyNM" runat="server"></asp:TextBox></div>    
                </td>
                         
            </tr>
            
            <tr>
                <td>
                    <div class="FieldStyle">Login Name</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtLoginName" runat="server"></asp:TextBox></div>                
                </td>                 
                <td> 
                    <div class="FieldStyle">Account Number</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtExternalaccountno" runat="server"></asp:TextBox></div>            
                </td>
                <td>
                    <div class="FieldStyle">Contact ID</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtContactID" runat="server"></asp:TextBox></div>
                </td>
            </tr>
            
            <tr>
                <td>
                    <div class="FieldStyle">Phone Number</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox></div>
                </td>
                <td>
                    <div class="FieldStyle">Email ID</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox></div>            
                </td>
                <td>
                    <div class="FieldStyle">Select a Profile</div>    
                    <div class="ValueStyle"><asp:DropDownList ID="lstProfile" runat="server"></asp:DropDownList></div> 
                </td>
            </tr>
            <tr>
                <td>
                    <div class="FieldStyle">Start Date</div>
                    <div class="HintStyle">Use format MM/DD/YYYY</div>
                    <div class="ValueStyle">
                        <asp:TextBox  id="txtStartDate" Text="" runat="server"></asp:TextBox>
                        <asp:image id="imgbtnStartDt" runat="server"  style="cursor:pointer;  vertical-align:top" ImageUrl="~/images/icons/SmallCalendar.gif"></asp:image>                        
                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" Enabled="true" PopupButtonID="imgbtnStartDt" runat="server" TargetControlID="txtStartDate"></ajaxToolKit:CalendarExtender>
                    </div>
                </td>
                
                <td>
                    <div class="FieldStyle">End Date</div>
                    <div class="HintStyle">Use format MM/DD/YYYY</div>
                    <div class="ValueStyle">
                    	<asp:TextBox id="txtEndDate" Text='' runat="server" />&nbsp;<asp:ImageButton ID="ImgbtnEndDt" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/icons/SmallCalendar.gif" />
                    	<ajaxToolKit:CalendarExtender ID="CalendarExtender2" Enabled="true" PopupButtonID="ImgbtnEndDt" runat="server" TargetControlID="txtEndDate"></ajaxToolKit:CalendarExtender>
                    </div>
                </td>
                <td><div class="FieldStyle">Partner Approval Status</div>
                <div><uc1:spacer id="Spacer1" SpacerHeight="14" SpacerWidth="3" runat="server"></uc1:spacer></div>
      <div class="ValueStyle">
        <asp:DropDownList ID="lstReferralStatus" runat="server">              
        </asp:DropDownList>
      </div>        </td>
            </tr>
            
            <tr>
                <td colspan="2">
                    <span class="ValueStyle">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtStartDate"
                            CssClass="Error" Display="Dynamic" ErrorMessage="* Enter Valid Date in MM/DD/YYYY format"
                            ValidationExpression="((^(10|12|0?[13578])([/])(3[01]|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(11|0?[469])([/])(30|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(2[0-8]|1[0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(29)([/])([2468][048]00)$)|(^(0?2)([/])(29)([/])([3579][26]00)$)|(^(0?2)([/])(29)([/])([1][89][0][48])$)|(^(0?2)([/])(29)([/])([2-9][0-9][0][48])$)|(^(0?2)([/])(29)([/])([1][89][2468][048])$)|(^(0?2)([/])(29)([/])([2-9][0-9][2468][048])$)|(^(0?2)([/])(29)([/])([1][89][13579][26])$)|(^(0?2)([/])(29)([/])([2-9][0-9][13579][26])$))"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEndDate"
                            CssClass="Error" Display="Dynamic" ErrorMessage="* Enter Valid Date in MM/DD/YYYY format"
                            ValidationExpression="((^(10|12|0?[13578])([/])(3[01]|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(11|0?[469])([/])(30|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(2[0-8]|1[0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(29)([/])([2468][048]00)$)|(^(0?2)([/])(29)([/])([3579][26]00)$)|(^(0?2)([/])(29)([/])([1][89][0][48])$)|(^(0?2)([/])(29)([/])([2-9][0-9][0][48])$)|(^(0?2)([/])(29)([/])([1][89][2468][048])$)|(^(0?2)([/])(29)([/])([2-9][0-9][2468][048])$)|(^(0?2)([/])(29)([/])([1][89][13579][26])$)|(^(0?2)([/])(29)([/])([2-9][0-9][13579][26])$))"></asp:RegularExpressionValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                            ControlToValidate="txtEndDate" CssClass="Error" Display="Dynamic" ErrorMessage=" End Date must be greater than Begin date <br/>"
                            Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                    </span>
                </td>
                <td></td>               
            </tr>
            
            
            
            <tr>            
                <td colspan="2">
                    <div class="ValueStyle">
                        <asp:Button ID="btnSearch" runat="server" CssClass="Button" OnClick="btnSearch_Click" Text="Search" />
                        <asp:Button ID="btnClearSearch" CausesValidation="false" runat="server" OnClick="btnClearSearch_Click" Text="Clear Search" CssClass="Button" />            
                    </div> 
                </td>
                <td></td>
            </tr>
        </table>
    </asp:Panel>      
    
    <h4>Account List</h4>    

    <div align="right">
            <asp:Button ID="download" runat="server"  Text="Download to Excel" CssClass="Button" OnClick="download_Click" /><br /><br />
    </div>
    
    <asp:GridView id="uxGrid" runat="server" CssClass="Grid" Width="100%" ForeColor="#333333" CellPadding="4" DataKeyNames="accountid" EmptyDataText="No contacts exist in the database." OnRowCommand="uxGrid_RowCommand" OnPageIndexChanging="uxGrid_PageIndexChanging" PageSize="50" AllowPaging="True" GridLines="None" AutoGenerateColumns="False" CaptionAlign="Left">    
        <Columns>        
            <asp:HyperLinkField HeaderText="ID" DataTextField = "accountid"  DataNavigateUrlFields="accountid" DataNavigateUrlFormatString="~/admin/secure/sales/customers/view.aspx?itemid={0}" />                       
            <asp:TemplateField HeaderText="Profile">
              <ItemTemplate>
                  <%# GetProfileName(DataBinder.Eval(Container.DataItem, "ProfileID"))%>
              </ItemTemplate>
            </asp:TemplateField>  
            <asp:TemplateField HeaderText="Full Name">
                <ItemTemplate><asp:Label ID="lblFullName" runat="Server" Text='<%# ConcatName(Eval("BillingFirstName"),Eval("BillingLastName")) %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>            
                      
            <asp:BoundField DataField="BillingPhoneNumber" HeaderText="Phone Number" />            
            <asp:TemplateField HeaderText="Email ID"> 
                <ItemTemplate><a href='<%# "mailto:" + Eval("BillingEmailID") %>'><%# DataBinder.Eval(Container.DataItem,"BillingEmailID") %></a></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField> 
                <ItemTemplate><asp:Button ID="ViewCustomer" Text="View" CommandName="View"  CssClass="Button" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"AccountId") %>' runat="server" /></ItemTemplate>
            </asp:TemplateField>            
            <asp:TemplateField> 
                <ItemTemplate><asp:Button ID="EditCustomer" Text="Edit" CommandName="Edit"  CssClass="Button" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"AccountId") %>' Visible='<%# HideEditButton(DataBinder.Eval(Container.DataItem,"UserId")) %>' runat="server" /></ItemTemplate> 
            </asp:TemplateField>
            <asp:TemplateField> 
                <ItemTemplate><asp:Button ID="UnlockCustomer" Text="Enable" CommandName="Unlock"  CssClass="Button" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"AccountId") %>' Enabled='<%# HideLockUnlockButton(DataBinder.Eval(Container.DataItem,"UserId"),true) %>' Visible='<%# HideLockUnlockButton(DataBinder.Eval(Container.DataItem,"UserId"),true) ||  HideLockUnlockButton(DataBinder.Eval(Container.DataItem,"UserId"),false) %>' runat="server" /></ItemTemplate> 
            </asp:TemplateField>            
            <asp:TemplateField> 
                <ItemTemplate><asp:Button ID="DisableCustomer" Text="Disable" CommandName="Disable"  CssClass="Button" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"AccountId") %>' Enabled='<%# HideLockUnlockButton(DataBinder.Eval(Container.DataItem,"UserId"),false) %>' Visible='<%# HideLockUnlockButton(DataBinder.Eval(Container.DataItem,"UserId"),true) ||  HideLockUnlockButton(DataBinder.Eval(Container.DataItem,"UserId"),false) %>'  runat="server" /></ItemTemplate> 
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate><asp:Button ID="DeleteCustomer" Text="Delete" CommandName="Delete"  CssClass="Button" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"AccountId") %>' Visible='<%# HideDeleteButton(DataBinder.Eval(Container.DataItem,"UserId")) %>'  runat="server" /></ItemTemplate> 
            </asp:TemplateField>
        </Columns>
        
        <RowStyle CssClass="RowStyle"  />
        <EditRowStyle CssClass="EditRowStyle"  />
        <HeaderStyle CssClass="HeaderStyle"  />
        <AlternatingRowStyle CssClass="AlternatingRowStyle"  />
        
    </asp:GridView>
    
</asp:Content>

