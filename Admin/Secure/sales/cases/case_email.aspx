<%@ Page Language="C#"  MasterPageFile="~/Admin/Themes/Standard/edit.master"  AutoEventWireup="true" CodeFile="case_email.aspx.cs" Inherits="Admin_Secure_sales_cases_case_email" validateRequest="false"%>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Src="~/Controls/HtmlTextBox.ascx" TagName="HtmlTextBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">

<div class="Form">
	    <table width="100%" cellSpacing="0" cellPadding="0" >
		    <tr>
			    <td style="height: 60px"><h1>Reply to Customer<uc2:DemoMode ID="DemoMode1" runat="server" />
                </h1></td>
			    <td align="right" style="height: 60px">
				    <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>

        <uc1:spacer id="Spacer8" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer>
        
        <h4>
        General Information
        </h4>
        
         <table border="0" cellpadding="0" cellspacing="0" class="ViewForm">
        <tr class="RowStyle">
            <td class="FieldStyle">Account Name</td>
            <td class="ValueStyle">
                    <asp:Label ID="lblAccountName" runat="server" Text="Label"></asp:Label>
            </td>
        </tr> 
        
        <tr class="AlternatingRowStyle">
            <td>
                            <div class="FieldStyle">Case Status</div>
                                                    
            </td>
            
            <td class="ValueStyle">                                                   
                            <asp:Label ID="lblCaseStatus" runat="server" Text="Label"></asp:Label>                          
            </td>
        </tr>    
        
        <tr class="RowStyle">
            <td class="FieldStyle">Case Priority</td>
            <td class="ValueStyle">
                    <asp:Label ID="lblCasePriority" runat="server" Text="Label"></asp:Label>
            </td>
        </tr> 
        
        <tr class="AlternatingRowStyle">
            <td>
                            <div class="FieldStyle">Case Title</div>
                                                    
            </td>
            
            <td class="ValueStyle">                                                   
                            <asp:Label ID="lblCaseTitle" runat="server" Text="Label"></asp:Label>                          
            </td>
        </tr> 
        
        <tr class="RowStyle">
            <td class="FieldStyle" valign="top" style="height: 67px">Case Description</td>
            <td class="ValueStyle" style="height: 67px">                            
              <asp:TextBox runat="server" ReadOnly="true" ID="txtCaseDescription" Height="59px" TextMode="MultiLine" Width="688px" />
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
            <td class="ValueStyle"><asp:Label ID="lblEmailTo" runat="server" Text="Label"></asp:Label></td>
        </tr> 
        
        <tr class="AlternatingRowStyle">
            <td class="FieldStyle">Phone Number</td>
            <td class="ValueStyle"><asp:Label ID="lblPhoneNumber" runat="server" Text="Label"></asp:Label></td>
        </tr>
        
      </table> 
               
         <h4>
          Email Reply
        </h4>
        <table border="0">
                <tr>
                    <td class="FieldStyle">
                         Email Subject                
                    </td>
                    <td class="ValueStyle" style="width: 726px">
                        <asp:TextBox ID="txtEmailSubj" runat="server" Width="279px" />
                    </td>
               </tr> 
               <tr>
                    <td colspan="2">
                        <uc1:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer>
                    </td>
               </tr>
               <tr>
                    <td valign="top" class="FieldStyle">Email Message</td>
                    <td class="ValueStyle" style="width: 726px"><uc1:HtmlTextBox id="ctrlHtmlText" Mode="1" runat="server"></uc1:HtmlTextBox></td>
               </tr>
              <tr>
                    <td valign="top" class="FieldStyle">Attachments</td>
                    <td class="ValueStyle" style="width: 726px"><INPUT id="FileBrowse" type="file" size="47" name="File1" runat="server" tabIndex="8"></td>
               </tr>                           
        </table>
        
        <uc1:spacer id="Spacer1" SpacerHeight="15" SpacerWidth="3" runat="server"></uc1:spacer>    	   	  
        <asp:Label id="lblEmailid" runat="server"  Visible="false"/>
        <div>
	        <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
	        <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	    </div>	
</div> 
</asp:Content>


