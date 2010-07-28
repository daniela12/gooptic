<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master"   AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="Admin_Secure_sales_cases_list" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">

<div class="Form">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <h1>
                    Service Requests
                </h1>
                Use this page to manage customer service requests submitted using the Contact-Us form.              
            </td>
            
            <td align="right">
                    <asp:button CssClass="Button" id="btnAdd" CausesValidation="False" Text="Create Service Request"	Runat="server" onclick="btnAdd_Click"></asp:button>
            </td>
        </tr>
    </table>
    
    <p></p>
    
    <h4>
        Search Service Requests
    </h4>

     <asp:Panel ID="Test" DefaultButton="btnSearch" runat="server" >  
        <table border="0" width="70%">        
        <tr>                
        <td>
        <div class="FieldStyle">Case ID</div>
        <div class="ValueStyle"><asp:TextBox ID="txtcaseid" runat="server"></asp:TextBox></div>
        </td>
        
        <td>
        <div class="FieldStyle">Title</div>
        <div class="ValueStyle"><asp:TextBox ID="txttitle" runat="server"></asp:TextBox></div>
        </td>  
        
        <td>
        <div class="FieldStyle">Case Status</div>    
        <div class="ValueStyle"><asp:DropDownList  runat="server" ID="ListCaseStatus" /></div>   
        </td>  
        </tr>
       
        <tr>
         <td>
        <div class="FieldStyle">Last Name</div>
        <div class="ValueStyle"><asp:TextBox ID="txtlastname" runat="server"></asp:TextBox></div>
        </td>        
        
        <td>
        <div class="FieldStyle">Company Name</div>
        <div class="ValueStyle"><asp:TextBox ID="txtcompanyname" runat="server"></asp:TextBox></div>
        </td>
        
        <td>
        <div class="FieldStyle">First Name</div>
        <div class="ValueStyle"><asp:TextBox ID="txtfirstname" runat="server"></asp:TextBox></div>
        </td>
        </tr>
        
        <tr>
        <td colspan="2">
        <div class="ValueStyle">
            <asp:Button ID="btnSearch" runat="server" CssClass="Button" OnClick="btnSearch_Click" Text="Search" />
            <asp:Button ID="btnClearSearch" runat="server" CssClass="Button" OnClick="btnClearSearch_Click" Text="Clear Search" />
        </div>
        </td>
        </tr>
        </table>
    </asp:Panel> 
    <p></p>
    <h4>
        Service Request List
    </h4>  
   
    <asp:GridView id="uxGrid" runat="server" CssClass="Grid" Width="100%" AllowSorting="True" CellPadding="4" DataKeyNames="accountid" EmptyDataText="No Service Requests were found." OnRowCommand="uxGrid_RowCommand" OnPageIndexChanging="uxGrid_PageIndexChanging" PageSize="50" AllowPaging="True" GridLines="None" AutoGenerateColumns="False" CaptionAlign="Left" OnSorting="uxGrid_Sorting">
        <Columns>
            <asp:HyperLinkField SortExpression="CaseID" HeaderText="ID" DataTextField="CaseID" DataNavigateUrlFormatString="~/admin/secure/sales/cases/view.aspx?itemid={0}" DataNavigateUrlFields="caseid" />
            <asp:HyperLinkField HeaderText="Title" DataTextField="Title" DataNavigateUrlFormatString="~/admin/secure/sales/cases/view.aspx?itemid={0}" DataNavigateUrlFields="CaseID" />
            <asp:TemplateField HeaderText="Case Status" SortExpression="CaseStatusID">
                <ItemTemplate>
                        <%# GetCaseStatusByCaseID(Eval("CaseStatusID"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Case Priority">
                 <ItemTemplate>
                        <%# GetCasePriorityByCaseID(Eval("CasePriorityID"))%>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText ="Created Date">
                <ItemTemplate>
                    <%# (DataBinder.Eval(Container.DataItem, "CreateDte", "{0:d}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="butView" CssClass="Button"  runat="server" CommandArgument='<%# Eval("caseid") %>' CommandName="View" Text="View" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="butEdit" CssClass="Button"  runat="server" CommandArgument='<%# Eval("caseid") %>' CommandName="Edit" Text="Edit" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="ButAddNote" CssClass="Button"  runat="server" CommandArgument='<%# Eval("caseid") %>' CommandName="AddNote" Text="Add Note" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="ButtonReply" CssClass="Button"  runat="server" CommandArgument='<%# Eval("caseid") %>' CommandName="Reply" Text="Email Reply" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle CssClass="RowStyle" />
        <EditRowStyle CssClass="EditRowStyle" />
        <PagerStyle CssClass="PageStyle" />
        <HeaderStyle CssClass="HeaderStyle" />
        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
    </asp:GridView>
   
</div>
</asp:Content>
