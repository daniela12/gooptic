<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master"  AutoEventWireup="true" CodeFile="password.aspx.cs" Inherits="admin_secure_settings_ipcommerce_password" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div class="Form">        
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <h1>Reset IP Commerce Password</h1>
                </td>
                <td align="right">

                </td>
            </tr>
        </table>
        
        <div><asp:Label ID="lblErrorMsg" runat="server" CssClass="Error"></asp:Label></div>
        
        <div><ZNode:spacer id="Spacer8" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>
           
        <h4>Login Information</h4>
        <div class="FieldStyle">User Id</div>                        
        <div class="ValueStyle"><asp:TextBox ID="UserID" runat="server" columns="40" /></div>
        
        <div class="FieldStyle">Old Password</div>                        
        <div class="ValueStyle"><asp:TextBox ID="PasswordOld" runat="server" columns="40" /></div>
        
        <div class="FieldStyle">New Password</div>
        <div class="ValueStyle"><asp:TextBox ID="Password" ValidationGroup="PasswordField" runat="server" />
        <div><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Password"
                Display="Dynamic" ErrorMessage="New Password length should be minimum 8" SetFocusOnError="True"
                ToolTip="New Password length should be minimum 8 and should contain atleast 1 number." ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,})"
                ValidationGroup="PasswordField" CssClass="Error">Password must be 8 or more characters and should contain only alphanumeric values with at least 1 numeric character.</asp:RegularExpressionValidator></div>
        <asp:Label ID="lblPwdErrorMsg" runat="server" CssClass="Error" EnableViewState="false"></asp:Label></div>       
                          
        <div><ZNode:spacer id="Spacer1" SpacerHeight="20" SpacerWidth="3" runat="server" /></div>
        
        <asp:button class="Button" id="SubmitButton" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click" ></asp:button>
	    <asp:button class="Button" id="CancelButton" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>	    
    </div>
</asp:Content>