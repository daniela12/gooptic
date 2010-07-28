<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master"  AutoEventWireup="true" CodeFile="edit_advancedsettings.aspx.cs" Inherits="Admin_Secure_catalog_product_edit_advancedsettings" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode" %>
<%@ Register Src="~/Controls/HtmlTextBox.ascx" TagName="HtmlTextBox" TagPrefix="ZNode" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div class="Form">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <h1><asp:Label ID="lblTitle" runat="server" Text="Edit Advanced Settings for"></asp:Label></h1>
                    <ZNode:demomode id="DemoMode1" runat="server"></ZNode:demomode>
                </td>
                <td align="right">
                    <asp:Button ID="btnSubmitTop" runat="server" CausesValidation="True" class="Button"
                        OnClick="btnSubmit_Click" Text="SUBMIT" />
                    <asp:Button ID="btnCancelTop" runat="server" CausesValidation="False" class="Button"
                        OnClick="btnCancel_Click" Text="CANCEL" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        <div>   
            <h4>Display Settings</h4>
            <div class="FieldStyle">Display Product?</div>
            <div class="ValueStyle"><asp:CheckBox Checked="true" ID="CheckEnabled" Text="Check this box to display this product in the storefront"  runat="server" /></div>
            
            <div class="FieldStyle">Home Page Special?</div>
            <div class="ValueStyle"><asp:CheckBox ID="CheckHomeSpecial" Text="Check this box if this product should be displayed on the Home Page"  runat="server" /></div>
            
            <div class="FieldStyle">New Item?</div>
            <div class="ValueStyle"><asp:CheckBox ID="CheckNewItem" Text="Check this box to display the new icon with this product." runat="server" /></div>            
                          
            <div class="FieldStyle">Featured Item Icon</div>
            <div class="ValueStyle"><asp:CheckBox ID="ChkFeaturedProduct" Text="Check this box to display the Featured Item icon with this product."  runat="server" /></div>
                   
            <div class="FieldStyle">Call For Pricing?</div>
            <div class="ValueStyle"><asp:CheckBox ID="CheckCallPricing" Text="Check this box if you want the customer to call you for pricing. This will disable the [Add to Cart button] "  runat="server" /></div>
            
            <div class="FieldStyle" style="display:none;">Display Inventory?</div>
            <div class="ValueStyle" style="display:none;"><asp:CheckBox ID="CheckDisplayInventory" Text="Check this box if you want the available product inventory to be displayed to the user."  runat="server" /></div>
            
            <h4>Inventory Settings</h4>
            
            <div class="FieldStyle">Out of Stock Options</div>
            <small>Select how being out of stock affects if items can be added to the cart</small>
            <div class="ValueStyle">
                <asp:RadioButtonList ID="InvSettingRadiobtnList" runat="server">
                    <asp:ListItem Selected="True" Value="1">Only Sell if Inventory Available (User can only add to cart if inventory is above 0)</asp:ListItem>
                    <asp:ListItem Value="2">Allow Back Order (Items can always be added to the cart. Inventory is reduced)</asp:ListItem>
                    <asp:ListItem Value="3">Don't Track Inventory (Items can always be added to the cart and inventory is not reduced)</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            
            <div class="FieldStyle">In Stock Message</div>
            <small>Enter a custom message that is displayed when items are in stock. If blank then no message will be displayed.</small>
            <div class="ValueStyle"><asp:TextBox ID="txtInStockMsg" runat="server"></asp:TextBox></div>
            
            <div class="FieldStyle">Out of Stock Message</div>
            <small>Enter a custom message to be displayed if this product is out of stock. If no message is entered then "Out of Stock" will be displayed.</small>
            <div class="ValueStyle"><asp:TextBox ID="txtOutofStock" runat="server">Out of Stock</asp:TextBox></div>
            
            <div class="FieldStyle">Back Order Message</div>
            <small>Enter a message to be displayed if an item is on back order. Leave blank to display nothing.</small>
            <div class="ValueStyle"><asp:TextBox ID="txtBackOrderMsg" runat="server"></asp:TextBox></div>
            
            <div style="display:none;"><small>Check this box to indicate that this product is a drop ship item.</small></div>
            <div class="ValueStyle"><asp:CheckBox ID="chkDropShip" runat="server" Visible="false" Text="Drop Ship" /></div>
            
            
            <h4>Recurring Billing Settings</h4>
            
            <div><small>Check this box to enable recurring billing subscription for this product.</small></div>
            <div class="ValueStyle"><asp:CheckBox ID="chkRecurringBillingInd" AutoPostBack="true" OnCheckedChanged="chkRecurringBillingInd_CheckedChanged" runat="server" Text="Recurring Billing" /></div>
            
            <asp:Panel ID="pnlRecurringBilling" runat="server" Visible="false">                
                <div class="FieldStyle">Billing Period<span class="Asterix">*</span></div>
                <small>Select a value for billing during this subscription period.</small>
                <div class="ValueStyle">
                     <asp:DropDownList ID="ddlBillingPeriods" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlBillingPeriods_SelectedIndexChanged">
                        <asp:ListItem Text="Day(s)" Value="DAY"></asp:ListItem>
                        <asp:ListItem Text="Weekly" Value="WEEK"></asp:ListItem>                        
                        <asp:ListItem Text="Monthly" Value="MONTH"></asp:ListItem>
                        <asp:ListItem Text="Yearly" Value="YEAR"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                
                <div class="FieldStyle">Billing Frequency<span class="Asterix">*</span></div>
                <small>Select number of billing periods that make up one billing cycle.</small>
                <div class="ValueStyle"><asp:DropDownList ID="ddlBillingFrequency" runat="server"></asp:DropDownList></div>
                
                <div class="FieldStyle">Billing Cycles<span class="Asterix">*</span></div>
                <small>Enter a number of billing cycles for payment period.</small>
                <div class="ValueStyle"><asp:TextBox ID="txtSubscrptionCycles" runat="server">1</asp:TextBox></div>
            </asp:Panel>
        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" CausesValidation="True" class="Button"
                OnClick="btnSubmit_Click" Text="SUBMIT" />
            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" class="Button"
                OnClick="btnCancel_Click" Text="CANCEL" />
        </div>
    </div>
</asp:Content>

