<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master"  AutoEventWireup="true" CodeFile="add_TieredPricing.aspx.cs" Inherits="Admin_Secure_catalog_product_add_TieredPricing" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div class="Form">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <h1><asp:Label ID="lblHeading" runat="server" /></h1>
                    <ZNode:DemoMode id="DemoMode1" runat="server"></ZNode:DemoMode>
                    <small>Use this page to add/edit a new pricing tier to this product.</small>
                </td>
                <td align="right">
                    <asp:Button ID="btnSubmitTop" runat="server" CausesValidation="True" class="Button" OnClick="btnSubmit_Click" Text="SUBMIT" />
                    <asp:Button ID="btnCancelTop" runat="server" CausesValidation="False" class="Button" OnClick="btnCancel_Click" Text="CANCEL" />
                </td>
            </tr>
        </table>
        <div><uc1:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></div>
        <asp:Label ID="lblError" CssClass= "Error" runat="server"></asp:Label>
        
        <div class="FieldStyle">Select Profile</div>
        <div class="ValueStyle"><asp:DropDownList ID="ddlProfiles" runat=server ></asp:DropDownList></div>
        
        <div class="FieldStyle">Tier Start<span class="Asterix">*</span></div>
        <div class="HintStyle">Enter the minimum quantity for this tier.</div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtTierStart" ErrorMessage="* Enter minimum quantity" CssClass="Error" Display="dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator10" runat="server" ControlToValidate="txtTierStart" CssClass="Error" Display="dynamic" ErrorMessage="Enter a whole number.333" MaximumValue="10000" MinimumValue="1" SetFocusOnError="true" Type="Integer"></asp:RangeValidator></div>
        <div class="ValueStyle"><asp:TextBox ID="txtTierStart" runat="server" Width="80px"></asp:TextBox></div>
        
        <div class="FieldStyle">Tier End<span class="Asterix">*</span></div>
        <div class="HintStyle">Enter the maximum quantity for this tier.</div>
        <div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTierEnd" ErrorMessage="* Enter maximum quantity" CssClass="Error" Display="dynamic"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtTierEnd" CssClass="Error" Display="dynamic" ErrorMessage="Enter a whole number." MaximumValue="10000" MinimumValue="1" SetFocusOnError="true" Type="Integer"></asp:RangeValidator>
            <asp:CompareValidator ID="cmpTierLimitsValidator" runat="server" ControlToValidate="txtTierEnd" ControlToCompare="txtTierStart" Display="Dynamic" Type="Integer" ErrorMessage="Tier maximum quantity must be greater than tier minimum quantity." CssClass="Error" Operator="GreaterThan"></asp:CompareValidator>
        </div>
        <div class="ValueStyle"><asp:TextBox ID="txtTierEnd" runat="server" Width="80px"></asp:TextBox></div>
        
        <div class="FieldStyle">Price<span class="Asterix">*</span></div>        
        <div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter a discounted price for this tier." ControlToValidate="txtPrice" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>            
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtPrice" ErrorMessage="You must enter a discounted price value between $0 and $999,999.99" MaximumValue="99999999" Type="Currency" MinimumValue="0" Display="Dynamic" CssClass="Error"></asp:RangeValidator>
        </div>
        <div class="ValueStyle"><%= ZNode.Libraries.ECommerce.Catalog.ZNodeCurrencyManager.GetCurrencyPrefix() %>&nbsp;<asp:TextBox ID="txtPrice" runat="server" MaxLength="10"></asp:TextBox></div>
        
        <div>
            <asp:Button ID="btnSubmit" runat="server" CausesValidation="True" class="Button" OnClick="btnSubmit_Click" Text="SUBMIT" />
            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" class="Button" OnClick="btnCancel_Click" Text="CANCEL" />
        </div>
    </div>
</asp:Content>




