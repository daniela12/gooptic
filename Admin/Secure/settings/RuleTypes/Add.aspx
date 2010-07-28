<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Admin_Secure_settings_RuleTypes_Add" MasterPageFile="~/Admin/Themes/Standard/edit.master" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <div class="Form">
        <table width="100%" cellspacing="0" cellpadding="0" >
	        <tr>
		        <td><h1><asp:Label ID="lblTitle" Runat="server"></asp:Label>
                    <ZNode:DemoMode ID="DemoMode1" runat="server" />
                </h1></td>
		        <td align="right">
			        <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>&nbsp;
			        <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
		        </td>
	        </tr>
        </table>            
        <div><ZNode:spacer id="Spacer2" SpacerHeight="15" SpacerWidth="3" runat="server"></ZNode:spacer></div>        
        
        
        <div><ZNode:spacer id="Spacer8" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>
        <div><asp:Label ID="lblErrorMsg" EnableViewState="false" runat="server" CssClass="Error"></asp:Label></div>
        <div><ZNode:spacer id="Spacer1" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>
        
        <asp:Panel ID="pnlRuleType" runat="server" Visible="false">
        <div class="FieldStyle">Rule Type</div>
        <div class="ValueStyle">
            <asp:DropDownList ID="ddlRuleTypes" runat="server">
                <asp:ListItem Selected="True" Value="0" Text="Promotion"></asp:ListItem>
                <asp:ListItem Text="Shipping" Value="1"></asp:ListItem>
                <asp:ListItem Text="Tax" Value="2"></asp:ListItem>
                <asp:ListItem Text="Supplier" Value="3"></asp:ListItem>
            </asp:DropDownList>
        </div>
        </asp:Panel>
        	
        <div class="FieldStyle">Class Name<span class="Asterix">*</span></div>
       
        <div class="ValueStyle"><asp:TextBox ID="txtRuleClassName" runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRuleClassName" ErrorMessage=" Enter  rule class name" CssClass="Error" display="dynamic"></asp:RequiredFieldValidator></div>
        
        <div class="FieldStyle">Rule Name<span class="Asterix">*</span></div>
         <div class="ValueStyle"><asp:TextBox ID="txtRuleName" runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRuleName" ErrorMessage=" Enter rule name" CssClass="Error" display="dynamic"></asp:RequiredFieldValidator></div>
        
        <div class="FieldStyle">Rule Description</div>
      
        <div class="ValueStyle"><asp:TextBox ID="txtRuleDesc" runat="server" Width="152px"></asp:TextBox></div>
        
        <div class="FieldStyle"><asp:CheckBox ID="chkEnable" runat="server" Text="Enable Rule" /></div>
        <div><ZNode:spacer id="Spacer4" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>
        
        <div>
                <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>&nbsp;
                <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
        </div>
        <div><ZNode:spacer id="Spacer3" SpacerHeight="15" SpacerWidth="3" runat="server"></ZNode:spacer></div>
    </div>
</asp:Content>