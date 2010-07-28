<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="Admin_Secure_settings_Profile_add" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div class="Form">        
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <h1>
                        <asp:Label ID="lblTitle" runat="server"></asp:Label>
                        <uc2:demomode id="DemoMode1" runat="server"></uc2:demomode>
                    </h1>
                </td>
                <td align="right">
                    <asp:Button ID="btnSubmitTop" runat="server" CausesValidation="True" class="Button"
                        OnClick="btnSubmit_Click" Text="SUBMIT" />
                    <asp:Button ID="btnCancelTop" runat="server" CausesValidation="False" class="Button"
                        OnClick="btnCancel_Click" Text="CANCEL" />
                </td>
            </tr>
        </table>
        
        <div><asp:Label ID="lblErrorMsg" runat="server" CssClass="Error"></asp:Label></div>
        
        <div><uc1:spacer id="Spacer8" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
        	    
	    <div class="FieldStyle">Profile Name<span class="Asterix">*</span></div>	    
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ProfileName" Display="Dynamic" ErrorMessage="* Enter a profile name" SetFocusOnError="True"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:TextBox ID="ProfileName" runat="server" Columns="25" ></asp:TextBox></div>
        
        <div class="FieldStyle">Default Account Number</div>
        <small>Enter a default account number for this profile. This is usually your internal ERP account number.</small>
        <div class="ValueStyle"><asp:TextBox ID="ExternalAccountNum" runat="server" Columns="25" ></asp:TextBox></div>        
        
        <div class="FieldStyle"><asp:CheckBox Text='' ID="chkDefaultInd" runat="server" />Is Default</div>
        <small>If checked, a customer registering on this site will be assinged to this profile</small>
        
        <div><uc1:spacer id="Spacer6" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
        
        <div class="FieldStyle"><asp:CheckBox Text='' ID="chkIsAnonymous" runat="server" />Is Anonymous</div>
        <small>If checked, an anonymous visitor to your website will use this profile</small>        
        
        <div><uc1:spacer id="Spacer4" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
        
        <div class="FieldStyle"><asp:CheckBox Text='' ID="chkShowPrice" runat="server" />Display Pricing for Catalog Items</div>
        <small>Check this box to show catalog pricing for this profile. Orders can't be placed if unchecked. If you wish to hide pricing for this profile, you must also disable pricing for the Anonymous Profile for obvious reasons.</small>
        
        <div><uc1:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
        
        <div class="FieldStyle"><asp:CheckBox ID="chkUseWholesalePrice" runat="server" />Use Wholesale Price</div>
        <small>Check this box to use wholesale pricing for this profile.</small>    
        
        <div><uc1:spacer id="Spacer3" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
        
        <div class="FieldStyle"><asp:CheckBox ID="chkShowOnPartner" runat="server" />Enable Self Sign-Up</div>
        <small>Check this box to display this profile as an option on the Partner Sign-Up page.</small>       
        
        <div><uc1:spacer id="Spacer5" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
        
        <div class="FieldStyle"><asp:CheckBox ID="chkTaxExempt" runat="server" />Is Tax Exempt</div>        
          
        <div><uc1:spacer id="Spacer1" SpacerHeight="20" SpacerWidth="3" runat="server"></uc1:spacer></div>
        
        <asp:button class="Button" id="SubmitButton" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click" ></asp:button>
	    <asp:button class="Button" id="CancelButton" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
	    
    </div>

</asp:Content>

