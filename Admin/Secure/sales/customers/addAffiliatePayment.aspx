<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="addAffiliatePayment.aspx.cs" Inherits="Admin_Secure_sales_customers_addAffiliatePayment" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
<asp:ScriptManager id="ScriptManager" runat="server"></asp:ScriptManager>
<ZNode:DemoMode ID="DemoMode1" runat="server" />

<div class="Form">
    <table width="100%" cellSpacing="0" cellPadding="0">
        <tr>
            <td><h1><asp:Label ID="lblTitle" Text="Add Payment" runat="server"></asp:Label></h1></td>
			    <td align="right">
				    <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>
	    
	    <div><ZNode:spacer id="Spacer1" SpacerHeight="15" SpacerWidth="3" runat="server" /></div>
    <div class="FieldStyle" >Amount Owed</div>  
    <div class="ValueStyle"><asp:Label ID="lblAmountOwed" runat="server" Text="" /></div>
	    
	    <div class="FieldStyle">Payment Description</div>
	    <div class="HintStyle">Enter a short meaningful description for this payment.</div>
        <div class="ValueStyle">
                <asp:textbox id="txtDescription" runat="server" columns="30" Rows="5" TextMode="MultiLine" />		        
		</div>
		
		<div class="FieldStyle">Payment Date<span class="Asterix">*</span></div>
        <div class="ValueStyle">
            <table border=0 cellpadding=0 cellspacing=0><tr>
            <td><asp:TextBox id="txtDate" Text='' runat="server" /></td>
            <td>&nbsp;<asp:ImageButton ID="imgbtnStartDt" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/icons/SmallCalendar.gif" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter received date" ControlToValidate="txtDate" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDate"
                CssClass="Error" Display="Dynamic" ErrorMessage="Enter Valid Date in MM/DD/YYYY format"
                ValidationExpression="((^(10|12|0?[13578])([/])(3[01]|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(11|0?[469])([/])(30|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(2[0-8]|1[0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(29)([/])([2468][048]00)$)|(^(0?2)([/])(29)([/])([3579][26]00)$)|(^(0?2)([/])(29)([/])([1][89][0][48])$)|(^(0?2)([/])(29)([/])([2-9][0-9][0][48])$)|(^(0?2)([/])(29)([/])([1][89][2468][048])$)|(^(0?2)([/])(29)([/])([2-9][0-9][2468][048])$)|(^(0?2)([/])(29)([/])([1][89][13579][26])$)|(^(0?2)([/])(29)([/])([2-9][0-9][13579][26])$))"></asp:RegularExpressionValidator>    
            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" Enabled="true" PopupButtonID="imgbtnStartDt" runat="server" TargetControlID="txtDate"></ajaxToolKit:CalendarExtender>        
            </td>
            </tr></table></div>
        
        <div class="FieldStyle">Amount<span class="Asterix">*</span></div>
        <div class="ValueStyle">
            <asp:textbox id="txtAmount" runat="server" width="130" columns="30" MaxLength="100"></asp:textbox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter a amount." ControlToValidate="txtAmount" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtAmount" Type="Currency" Operator="DataTypeCheck" ErrorMessage="You must enter a valid amount." CssClass="Error" Display="Dynamic" />
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAmount" ErrorMessage="You must enter a amount." MaximumValue="99999999" Type="Currency" MinimumValue="0" Display="Dynamic" CssClass="Error"></asp:RangeValidator>
		</div>	    
	    
	    <div><asp:Label ID="ErrorMessage" runat="server" CssClass="Error"></asp:Label></div>
        <ZNode:spacer id="Spacer3" SpacerHeight="10" SpacerWidth="3" runat="server" />
        
        <div>
            <asp:button class="Button" id="Submit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
		    <asp:button class="Button" id="Cancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
        </div>
</div>
</asp:Content>