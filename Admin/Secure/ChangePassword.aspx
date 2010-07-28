<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master"  AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Admin_Secure_ChangePassword" Title="Change Password" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/spacer.ascx" TagName="spacer" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
<div class="Form Search">
    <h1>Change Password<uc2:DemoMode ID="DemoMode1" runat="server" />
    </h1>
    <div><uc1:spacer id="Spacer1" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></div>
    <asp:ChangePassword ID="AdminChangePassword" runat="server" CancelDestinationPageUrl="~/account.aspx"
        ContinueDestinationPageUrl="~/account.aspx" DisplayUserName="False" ChangePasswordTitleText="" OnChangePasswordError="ChangePassword1_ChangePasswordError" OnChangingPassword="AdminChangePassword_ChangingPassword" >
        <SuccessTemplate>
            <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td>
                        <table border="0" cellpadding="0">
                            <tr class="Row">
                                <td align="left" colspan="2">
                                    Confirmation</td>
                            </tr>
                            <tr class="Row">
                                <td class="Success">
                                    Your password has been changed!</td>
                            </tr>
                            <tr class="Row">
                                <td align="right" colspan="2">
                                    <asp:Button ID="ContinuePushButton" OnClick="ContinuePushButton_Click" runat="server" CausesValidation="False" CommandName="Continue" CssClass="Button"  Text="Continue" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </SuccessTemplate>
        <ChangePasswordTemplate>        
            <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td>
                        <table border="0" cellpadding="0">       
                            <tr class="Row">
                                <td align="left" colspan="2" style="color: red" class="Error">                                    
                                    <asp:Literal ID="PasswordFailureText" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>                    
                            <tr class="Row">
                                <td align="right" class="FieldStyle">
                                    <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Current Password</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ChangePassword1" CssClass="Error" Display="Dynamic">Password is required.</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="Row">
                                <td align="right" class="FieldStyle">
                                    <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="NewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                                        ErrorMessage="New Password is required." ToolTip="New Password is required."
                                        ValidationGroup="ChangePassword1" CssClass="Error" Display="Dynamic">New Password is required.</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="NewPassword"
                                        Display="Dynamic" ErrorMessage="New Password length should be minimum 8" SetFocusOnError="True"
                                        ToolTip="New Password length should be minimum 8" ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,})"
                                        ValidationGroup="ChangePassword1" CssClass="Error">Password must be at least 8 alphanumeric characters and contain at least one number.</asp:RegularExpressionValidator> 
                                    </td>
                            </tr>
                            <tr class="Row">
                                <td align="right" class="FieldStyle">
                                    <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                                        ErrorMessage="Confirm New Password is required." ToolTip="Confirm New Password is required."
                                        ValidationGroup="ChangePassword1" CssClass="Error" Display="Dynamic">Confirmation password is required.</asp:RequiredFieldValidator>
                                    </td>
                            </tr>
                            <tr class="Row">
                                <td align="center" colspan="2">
                                    <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                                        ControlToValidate="ConfirmNewPassword" CssClass="Error" Display="Dynamic" ErrorMessage="Confirmation password does not match your new password."
                                        ValidationGroup="ChangePassword1"></asp:CompareValidator>
                                </td>
                            </tr>
                           
                            <tr class="Row">
                                <td align="right">
                                    
                                </td>
                                <td>
                                <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword"
                                        Text="Change Password" ValidationGroup="ChangePassword1" CssClass="Button" />
                                    <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                        Text="Cancel" CssClass="Button" OnClick="CancelPushButton_Click"  />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ChangePasswordTemplate>                            
     </asp:ChangePassword>
</div> 
</asp:Content>

