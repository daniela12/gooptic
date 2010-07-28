<%@ Page Language="C#"  MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="Admin_Secure_catalog_product_manufacturer_Default" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/spacer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <div class="Form">
	    <table width="100%" cellspacing="0" cellpadding="0" >
		    <tr>
			    <td><h1><asp:Label ID="lblTitle" Runat="server"></asp:Label>
                    <uc2:DemoMode id="DemoMode1" runat="server">
                    </uc2:DemoMode></h1></td>
			    <td align="right">
				    <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>
	      <div><uc1:spacer id="Spacer8" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
    	        
        <div class="FieldStyle">Brand Name<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Name" Display="Dynamic" ErrorMessage="* Enter a Manufacturer Name" SetFocusOnError="True"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:TextBox ID='Name' runat='server' MaxLength="50" Columns="50"></asp:TextBox></div>
        <div class="FieldStyle">Enter a Description <br />
            <asp:TextBox ID="Description" runat="server" rows="10" MaxLength="4000" TextMode="MultiLine" columns="50"></asp:TextBox>
        </div>                
        
        <uc1:spacer id="Spacer1" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer>
         
        <div class="FieldStyle">Email Address</div>
        <div class="ValueStyle"><asp:TextBox ID='EmailId' runat='server' MaxLength="50" Columns="50"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="EmailId"
                CssClass="Error" Display="Dynamic" ErrorMessage="Enter Valid EmailId" SetFocusOnError="True"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></div>
        
        
        
        <asp:Panel id="TempPanel" runat="server" style="display:none">
            <div class="FieldStyle">Drop Shipper</div>

            <div class="FieldStyle"><asp:CheckBox ID="DropShipInd" runat="server" Text="" /></div>
            <uc1:spacer id="Spacer3" SpacerHeight="5" SpacerWidth="10" runat="server"></uc1:spacer>
            
            <div class="FieldStyle">Email Notification Template</div>
            <div class="ValueStyle"><asp:TextBox ID="NotificationTemplate" runat="server" rows="15" columns="50" TextMode="MultiLine"></asp:TextBox></div>
        
            <div class="FieldStyle">Custom1</div>
            <div class="ValueStyle"><asp:TextBox ID="Custom1" runat="server" rows="10" MaxLength="4000" columns="50" TextMode="MultiLine"></asp:TextBox></div>
        
            <div class="FieldStyle">Custom2</div>
            <div class="ValueStyle"><asp:TextBox ID="Custom2" runat="server" rows="10" MaxLength="4000" columns="50" TextMode="MultiLine"></asp:TextBox></div>
        
            <div class="FieldStyle">Custom3</div>
            <div class="ValueStyle"><asp:TextBox ID="Custom3" runat="server" rows="10" MaxLength="4000" Columns="50"  TextMode="MultiLine"></asp:TextBox></div>
        </asp:Panel>      
        
	    <div>
	        <uc1:spacer id="Spacer2" SpacerHeight="5" SpacerWidth="10" runat="server"></uc1:spacer>
	        <div class="FieldStyle">Is Active?</div>        
            <asp:CheckBox ID="CheckActiveInd" runat="server" Text="" CssClass="FieldStyle" Checked="true" />
	        <asp:Label ID="lblError" runat="server"></asp:Label>	   
        </div>
    <p></p>
    <asp:button class="Button" id="SubmitButton" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"></asp:button>
	<asp:button class="Button" id="CancelButton" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	
</div>
                
</asp:Content>
