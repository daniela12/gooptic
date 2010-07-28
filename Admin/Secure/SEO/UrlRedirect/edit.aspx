<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Themes/Standard/edit.master" CodeFile="edit.aspx.cs" Inherits="Admin_Secure_SEO_UrlRedirect_edit" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Src="~/Controls/HtmlTextBox.ascx" TagName="HtmlTextBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <div class="Form">
	    <table width="100%" cellspacing="0" cellpadding="0" >
		    <tr>
			    <td>
			        <h1><asp:Label ID="lblTitle" Runat="server"></asp:Label>
                    <uc2:DemoMode ID="DemoMode1" runat="server" />
                    </h1>
                </td>
			    <td align="right">
				    <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>
	    
	    
	    <div><uc1:spacer id="Spacer8" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>
    	<div><asp:Label ID="lblMsg" runat="server" CssClass="Error"></asp:Label></div>
    	<div><uc1:spacer id="Spacer1" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>
    	
    	
        <div class="FieldStyle">Enter URL to Redirect From</div>
        <div class="HintStyle">Be sure to include the .aspx file extension(ex. apple.aspx or product.aspx?zpid=302).</div>
        <div class="ValueStyle"><asp:TextBox ID="txtOldUrl" runat="server" MaxLength="500" Columns="50"></asp:TextBox>
        <asp:RequiredFieldValidator ID="OldSeourlFieldValidator" CssClass="Error" ControlToValidate="txtOldUrl" runat="server" ErrorMessage="Enter From Seo URL"></asp:RequiredFieldValidator>        
        </div>
  
        <div class="FieldStyle">Enter URL to Redirect To</div>
        <div class="HintStyle">Be sure to include the .aspx file extension(ex. apple.aspx or category.aspx?zcid=81).</div> 
        <div class="ValueStyle"><asp:TextBox ID="txtNewUrl" runat="server" MaxLength="500" Columns="50"></asp:TextBox>
        <asp:RequiredFieldValidator ID="NewSeourlFieldValidator"  CssClass="Error" ControlToValidate="txtNewUrl" runat="server" ErrorMessage="Enter To Seo URL"></asp:RequiredFieldValidator>            
        </div>
  

        <div class="FieldStyle"><asp:CheckBox ID='chkIsActive' Checked="true" runat="server" Text="Enable this Redirection" /></div>
	    
	    <div style="margin-top:20px;">
	        <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
	        <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	    </div>	
	</div>
</asp:Content>