<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="note_add.aspx.cs" Inherits="Admin_Secure_sales_Note_Add" Title="Untitled Page" ValidateRequest="false" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Src="~/Controls/HtmlTextBox.ascx" TagName="HtmlTextBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">

     <div class="Form">
	    <table width="100%" cellSpacing="0" cellPadding="0" >
		    <tr>
			    <td><h1>Add Note<uc2:DemoMode ID="DemoMode1" runat="server" />
                </h1></td>
			    <td align="right">
				    <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>
	    
	    <uc1:spacer id="Spacer8" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer>    	   	  
        
        <div class="FieldStyle">Note Title<span class="Asterix">*</span></div>
        <div class="ValueStyle"><asp:TextBox ID='txtNoteTitle' runat='server' MaxLength="50" Columns="20"></asp:TextBox>
            <asp:RequiredFieldValidator  ControlToValidate="txtNoteTitle" ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Enter Note Title" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>

        <div class="FieldStyle">Note Body<span class="Asterix">*</span></div>
        <div class="ValueStyle"><uc1:HtmlTextBox id="ctrlHtmlText" runat="server"></uc1:HtmlTextBox></div> 
        
    	<div class="ValueStyle">
    	    <asp:Label ID="lblError"  CssClass="Error" ForeColor="DarkRed" runat="server" Text="Label" Visible="False"></asp:Label>
    	</div>
    	
    	<div>
	        <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
	        <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	    </div>	
	    
	 </div> 
</asp:Content>

