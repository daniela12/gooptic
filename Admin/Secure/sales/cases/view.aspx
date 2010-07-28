<%@ Page Language="C#"  MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="view.aspx.cs" Inherits="Admin_Secure_sales_cases_view" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
<div class="Form">
     <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <h1>
                     Case# <asp:Label ID="lblTitle" runat="server"></asp:Label>
                </h1>                
            </td>
            <td align="right">
                <asp:Button ID="CaseList" runat="server" CssClass="Button" Text="< Back To Case List" Width="150px" OnClick="CaseList_Click" />
                <asp:Button ID="EditCase" runat="server" CssClass="Button" Text="Edit Case" Width="100px" OnClick="CaseEdit_Click" />
                <asp:Button ID="ReplyToCase" runat="server" CssClass="Button" Text="Reply to Customer" Width="150px" OnClick="ReplyToCase_Click" />
            </td>
        </tr>
    </table>
    
    <h4>
        General Information
    </h4>
    
    <table border="0" cellpadding="0" cellspacing="0" class="ViewForm">
        <tr class="RowStyle">
            <td class="FieldStyle">Created Date</td>
            <td class="ValueStyle">
              <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
            </td>
        </tr> 
        <tr class="AlternatingRowStyle">
            <td class="FieldStyle">Account Name</td>
            <td class="ValueStyle">
                <asp:Label ID="lblAccountName" runat="server" Text="Label"></asp:Label>
            </td>
        </tr> 
        
        <tr class="RowStyle">
            <td>
                <div class="FieldStyle">Case Status</div>                                                    
            </td>            
            <td class="ValueStyle">                                                   
                <asp:Label ID="lblCaseStatus" runat="server" Text="Label"></asp:Label>                          
            </td>
        </tr>       
        <tr class="AlternatingRowStyle">
            <td class="FieldStyle">Case Priority</td>
            <td class="ValueStyle">
                <asp:Label ID="lblCasePriority" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>         
        <tr class="RowStyle">
            <td>
                <div class="FieldStyle">Case Title</div>                                                    
            </td>            
            <td class="ValueStyle">                                                   
                <asp:Label ID="lblCaseTitle" runat="server" Text="Label"></asp:Label>                          
            </td>
        </tr>         
        <tr class="AlternatingRowStyle">
            <td class="FieldStyle" valign="top">Case Description</td>
            <td class="ValueStyle">                            
                <asp:Label runat="server" ID="lblCaseDescription" Width="519px"></asp:Label>                       
            </td>
        </tr> 
    </table>
    
    <h4>
        Customer Information
    </h4>
    
    <table border="0" cellpadding="0" cellspacing="0" class="ViewForm">
        <tr class="RowStyle">
            <td class="FieldStyle">Full Name</td>
            <td class="ValueStyle">
                <asp:Label ID="lblCustomerName" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>         
        <tr class="AlternatingRowStyle">
            <td class="FieldStyle">
                Company Name
            </td>
            <td class="ValueStyle">                                                   
                <asp:Label ID="lblCompanyName" runat="server" Text="Label"></asp:Label>                          
            </td>
        </tr>        
        <tr class="RowStyle">
            <td class="FieldStyle">Email ID</td>
            <td class="ValueStyle">
                <asp:Label ID="lblEmailID" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>        
        <tr class="AlternatingRowStyle">
            <td class="FieldStyle">
                 Phone Number
            </td>
            <td class="ValueStyle">                                                   
                 <asp:Label ID="lblPhoneNumber" runat="server" Text="Label"></asp:Label>                          
            </td>
        </tr>        
      </table> 
    
    <h4>
         Notes
    </h4>
        
    <div style="text-align:right">
     <asp:Button ID="AddNewNote" runat="server" CssClass="Button" Text="Add Note" Width="80px" OnClick="AddNewNote_Click"  />
    </div>
    
    <asp:Repeater ID="CustomerNotes" runat="server">                    
        <ItemTemplate>
            <asp:Label ID="Label1" CssClass="FieldStyle" runat="Server" Text='<%# FormatCustomerNote(Eval("NoteTitle"),Eval("CreateUser"),Eval("CreateDte")) %>' />
            <br />
            <asp:Label ID="Label2" CssClass="ValueStyle" runat="Server" Text='<%# Eval("NoteBody") %>' />
            <br /><br />                           
        </ItemTemplate>                              
    </asp:Repeater>
    
    <uc1:spacer id="Spacer1" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer>
    
</div> 
</asp:Content>


