<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/activate.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Admin_Activate" Title="Activate your Znode Storefront" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <div class="License">
        <!-- License Activation Intro panel -->
        <asp:Panel ID="pnlIntro" runat="server" Visible="true">
            <h1>Activate your Storefront</h1>
            <div><img src="~/images/clear.gif" runat="server" width="1" height="10" alt=""/></div>
            You are about to activate your storefront to the following URL:
            <strong><%= DomainName %></strong>
            <div style="margin-bottom:20px; margin-top:20px;">
                If you are activating a Free Trial then proceed with the activation. You will have
                the option of creating an activation without using a license key.
            </div>
            <div style="margin-bottom:20px;">
                If you are using a purchased license, then you should be aware that once you have activated your key on this domain, 
                it cannot be moved to a different domain. It is highly recommended that you do not activate to an IP address. 
            For more information see <a href="http://help.znode.com/activating_znode_storefront.htm" target="_blank">help.znode.com/activating_znode_storefront</a>.</p>
            </div>
            <div><img src="~/images/clear.gif" runat="server" width="1" height="10" alt=""/>&nbsp;</div>
            <div><asp:CheckBox ID="chkIntro" runat="server" Text='' OnCheckedChanged="chkIntro_CheckedChanged" /></div>            
            
            <div><img src="~/images/clear.gif" runat="server" width="1" height="10" alt=""/></div>
            <div><asp:Label ID="lblErrorMsg" runat="server" CssClass="Error"></asp:Label></div>
            <div><img src="~/images/clear.gif" runat="server"  width="1" height="10" alt=""/></div>
            
            <div><asp:Button ID="btnProceedToActivation" runat="server" Text="Proceed With Activation" CausesValidation="true" OnClick="btnProceedToActivation_Click" /></div>
            <div><img src="~/images/clear.gif" runat="server" width="1" height="10" alt=""/></div>
        </asp:Panel>
            
        <!-- ACTIVATE LICENSE -->
        <asp:Panel ID="pnlLicenseActivate" runat="server" Visible="false">
            <h1>Activate your Storefront</h1>
            <div><img src="~/images/clear.gif" width="1" height="10" alt=""/></div>
            <b>Please Note:</b>
            <ul>
                <li>You are activating your license to the domain <strong><asp:Label id="lblDomainName" Text='' runat="server"></asp:Label></strong>. Be sure this is what you want to do!</li>
                <li>Once the key is registered, it cannot be moved to a different domain.</li>
                <li>You should have Read+Write+Modify permissions for the ASPNET account (Win XP) or the Network Service account (Win 2003) on the <b>Data</b> folder before activating.</li>
            </ul>
            <div><img src="~/images/clear.gif" width="1" height="10" alt=""/></div>    
            <div><asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label></div>
            <div><img src="~/images/clear.gif" width="1" height="10" alt=""/></div> 

            <table>
                <tr class="Row">
                    <td class="FieldLabel" valign="top">Select License</td>
                    <td valign="top">
                        <div><asp:RadioButton ID="chkFreeTrial" GroupName="lic" Checked="false" Text="30 Day Free Trial" runat="server" OnCheckedChanged="chkFreeTrial_CheckedChanged" AutoPostBack="true" /></div>
                        <div><asp:RadioButton ID="chkEntLicense" GroupName="lic" Checked="false" Text="Enterprise Edition - 1 Domain License" runat="server" OnCheckedChanged="chkFreeTrial_CheckedChanged" AutoPostBack="true"  /></div> 
                        <div><asp:RadioButton ID="chkSerLicense" GroupName="lic" Checked="false" Text="Enterprise Edition - 1 CPU License" runat="server" OnCheckedChanged="chkFreeTrial_CheckedChanged" AutoPostBack="true"  /></div> 
                    </td>
                </tr>
                <tr>
                    <td><img src="~/images/clear.gif" width="1" height="10" alt=""/></td>
                </tr>
                <asp:Panel ID="pnlSerial" runat="server" Visible="false">
                    <tr class="Row">
                        <td class="FieldLabel">Serial Number</td>
                        <td><asp:TextBox ID="txtSerialNumber" runat="server" Width="300" CausesValidation="true"></asp:TextBox>
                        <div><asp:RequiredFieldValidator ID="SerialReqd" runat="server" ControlToValidate="txtSerialNumber" ErrorMessage="Serial number is required." ToolTip="Serial number is required." Display="Dynamic" CssClass="Error">Serial number is required.</asp:RequiredFieldValidator></div>                        
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td><img src="~/images/clear.gif" width="1" height="10" alt=""/></td>
                </tr>
                <tr class="Row">
                    <td class="FieldLabel">Full Name</td>
                    <td><asp:TextBox ID="txtName" runat="server" Width="200" CausesValidation="true"></asp:TextBox>
                    <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="Full name is required." Display="Dynamic" CssClass="Error">Full name is required.</asp:RequiredFieldValidator></div>  
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldLabel">Email Address</td>
                    <td><asp:TextBox ID="txtEmail" runat="server" Width="200" CausesValidation="true"></asp:TextBox>
                    <div><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required." Display="Dynamic" CssClass="Error">Email is required.</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail"
                        CssClass="Error" ErrorMessage="Enter Valid Email Address" ToolTip="Enter Valid Email Address."
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>   
                    </div>  
                    </td>
                </tr>
                <tr class="Row">
                    <td><img src="~/images/clear.gif" width="1" height="20" alt=""/></td>
                    <td></td>
                </tr>
                <tr class="Row">
                    <td></td>
                    <td><asp:TextBox ID="txtEULA" runat="server" Columns="70" Rows="10" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><img src="~/images/clear.gif" width="1" height="10" alt=""/></td>
                </tr>
                <tr class="Row">
                    <td></td>
                    <td><asp:CheckBox ID="chkEULA" runat="server" Text="I have read and agreed to the software license agreement (EULA) above" /></td>
                </tr>
                <tr>
                    <td><img src="~/images/clear.gif" width="1" height="20" alt=""/></td>
                </tr>
                <tr class="Row">
                    <td></td>
                    <td><asp:Button ID="btnActivateLicense" runat="server" Text="Click to Activate Storefront" CausesValidation="true" OnClick="btnActivateLicense_Click" /></td>
                </tr>
                 <tr>
                    <td><img src="~/images/clear.gif" width="1" height="10" alt=""/></td>
                </tr>
            </table> 
        </asp:Panel>
           
        <!-- CONFIRM -->
        <asp:Panel ID="pnlConfirm" runat="server" Visible="false">
            <h1>Confirmation</h1>  
            <p><asp:Label ID="lblConfirm" runat="server"></asp:Label></p>
            <div><img src="~/images/clear.gif" width="1" height="20" alt=""/></div>      
            <a id="A1" href="~/" runat="server">Go to Storefront >></a>  
            <div><img src="~/images/clear.gif" width="1" height="20" alt=""/></div>
        </asp:Panel>
    </div>
</asp:Content>

