<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master"  AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="Admin_Secure_sales_Affiliates_View" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>


<asp:Content id="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
 <div class="Form">
        <h1>Affiliates</h1>
        <p>Use this page to manage the affiliate details.</p>
        
        <h4>Search Affiliates</h4>        
    
    <asp:Panel ID="SearchCustomers" CssClass="Form" DefaultButton="btnSearch" runat="Server">
        <table border="0" width="70%">
            <tr>
                <td>
                    <div class="FieldStyle">First Name</div>        
                    <div class="ValueStyle"><asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></div>            
                </td>
                
                <td>
                    <div class="FieldStyle">Last Name</div>        
                    <div class="ValueStyle"><asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></div>
                </td>
                
                <td>
                    <div class="FieldStyle">Company Name</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtComapanyName" runat="server"></asp:TextBox></div>    
                </td>
                         
            </tr>
            
            <tr>
                <td>
                    <div class="FieldStyle">Affilaite ID</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtAccountID" runat="server"></asp:TextBox></div>                
                </td>  
                 <td>
                    <div class="FieldStyle">Status</div>
                    <div class="ValueStyle">
                        <asp:DropDownlist ID="ListAffilaiteStatus" runat="server">
                            <asp:ListItem Selected="True" Text="All" Value="0"></asp:ListItem>
                            <asp:ListItem Text="New" Value="N"></asp:ListItem>
                            <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="I"></asp:ListItem>                            
                        </asp:DropDownlist>
                    </div>
                 </td>    
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
          
        <h4>Affiliates List </h4>
        <asp:GridView id="uxGrid" runat="server" CssClass="Grid" Width="100%" ForeColor="#333333" CellPadding="4" DataKeyNames="accountid" EmptyDataText="No affiliate exist in the database." OnRowCommand="uxGrid_RowCommand" OnPageIndexChanging="uxGrid_PageIndexChanging" PageSize="25" AllowPaging="True" GridLines="None" AutoGenerateColumns="False" CaptionAlign="Left">    
        <Columns>   
            <asp:BoundField DataField="AccountId" HeaderText="Affiliate ID" />                       
            <asp:BoundField DataField="companyname" HeaderText="Company Name" />    
            <asp:BoundField DataField="billingfirstname" HeaderText="First Name" /> 
            <asp:BoundField DataField="billinglastname" HeaderText="Last Name" />                                                                              
            <asp:TemplateField> 
                <ItemTemplate><asp:Button ID="EditCustomer" Text="Edit" CommandName="Edit"  CssClass="Button" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"AccountId") %>' runat="server" /></ItemTemplate> 
            </asp:TemplateField>          
        </Columns>
        
        <RowStyle CssClass="RowStyle"  />
        <EditRowStyle CssClass="EditRowStyle"  />
        <HeaderStyle CssClass="HeaderStyle"  />
        <AlternatingRowStyle CssClass="AlternatingRowStyle"  />
        
    </asp:GridView>
        </div>
</asp:Content>