<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="OrderDesk.aspx.cs" Inherits="Admin_Secure_sales_OrderDesk_Default" %>
<%@ Register Src="Confirm.ascx" TagName="Confirm" TagPrefix="ZNode" %>
<%@ Register Src="Payment.ascx" TagName="Payment" TagPrefix="ZNode" %>
<%@ Register Src="QuickOrder.ascx" TagName="QuickOrder" TagPrefix="ZNode" %>
<%@ Register Src="~/Themes/Default/ShoppingCart/ShoppingCart.ascx" TagName="ShoppingCart" TagPrefix="ZNode" %>
<%@ Register Src="CreateUser.ascx" TagName="CreateUser" TagPrefix="ZNode" %>
<%@ Register Src="CustomerSearch.ascx" TagName="CustomerSearch" TagPrefix="ZNode" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
<div class="OrderDesk">
    <asp:ScriptManager id="ScriptManager" runat="server" EnablePartialRendering="true"></asp:ScriptManager>    
    <asp:UpdatePanel ID="UpdatePanelOrderDesk" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" RenderMode="Inline">
        <ContentTemplate>    
            <table width="100%">
                <tr>
                    <td><h1>Create an Order</h1></td>
                    <td align="right">
                        <asp:Button ID="btnSubmitOrder" CssClass="Button" runat="server" Text="Submit Order" OnClick="Submit_Click" ValidationGroup="groupPayment" />
                        <asp:Button ID="btnCancelOrder" CausesValidation="false" CssClass="Button" runat="server" Text="Cancel Order" OnClick="btnCancelOrder_Click" />
                        <asp:Panel ID="pnlCreateOrderLink" runat="server" Visible="false">
                            <div class="LinkButton"><asp:LinkButton ID="linkTopNewOrder" runat="server" OnClick="linkNewOrder_Click">Create a new order</asp:LinkButton></div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            
            <!-- Ajax Modal Popup Box  -->
            <asp:Panel ID="pnlOrderDesk" runat="server">
            <asp:Button id="btnShowPopup" runat="server" style="display:none" />
            <asp:Button id="btnShowPopup1" runat="server" style="display:none" />
            <asp:Button id="btnShowQuickOrder" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlSelectUsers" BackgroundCssClass="modalBackground" />
            <ajaxToolkit:ModalPopupExtender ID="mdlCreateUserPopup" runat="server" TargetControlID="btnShowPopup1" PopupControlID="pnlCreateUser" BackgroundCssClass="modalBackground"  />
            <ajaxToolkit:ModalPopupExtender ID="mdlQuickOrderPopup" runat="server" TargetControlID="btnShowQuickOrder" PopupControlID="pnlQuickOrder" BackgroundCssClass="modalBackground"  />
                
            <!-- Customer Section -->
            <h4>Customer</h4>
            <asp:Panel ID="pnlSelectCustomer" runat="server">
                <asp:UpdatePanel ID="UpdatePnlSelecCustomer" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="LinkButton"><asp:LinkButton CausesValidation="false" ID="linkSearchCustomer" runat="server" Text="Search for a customer" OnClick="linkSearchCustomer_Click" /></div>                        
                        <div class="LinkButton"><asp:LinkButton CausesValidation="false" ID="linkNewCustomer" runat="server" Text="Create a new customer" OnClick="linkNewCustomer_Click" /></div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:Panel ID="pnlSelectUsers" runat="server" style="display:none;" cssClass="PopupStyle">                
                <div><ZNode:CustomerSearch id="uxCustomerSearch" runat="server" /></div>                    
            </asp:Panel>        
    
            <asp:Panel ID="ui_pnlAcctInfo" runat="server" Visible="false">                
                        <table cellpadding="0" cellspacing="0" width="100%">        
                            <tr><td colspan="3"><uc1:spacer id="Spacer3" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></td></tr>
                            <tr><td colspan="3" align="right">&nbsp;</td></tr>
                            <tr>
                                <td><strong>Billing Address:</strong></td>
                                <td>&nbsp;&nbsp;</td>
                                <td><strong>Shipping Address:</strong></td>
                            </tr>
                            <tr>
                                <td><asp:Label ID="ui_BillingAddress" runat="server" Text=''></asp:Label></td>
                                <td></td>
                                <td><asp:Label ID="ui_ShippingAddress" runat="server" Text=''></asp:Label></td>
                            </tr>
                            <tr><td colspan="3"><uc1:spacer id="Spacer1" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></td></tr>
                            <tr><td colspan="3"><uc1:spacer id="Spacer7" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></td></tr>
                            <tr>
                                <td><asp:Button ID="ui_Edit" runat="server" CausesValidation="false" Text="Edit Billing and Shipping Address" CssClass="Button" width="200px" OnClick="ui_Edit_Click" /></td>
                                <td><uc1:spacer id="Spacer6" SpacerHeight="1" SpacerWidth="10" runat="server"></uc1:spacer></td>
                                <td><strong>Profile</strong>&nbsp;&nbsp;&nbsp;<asp:Label ID="ui_lblProfile" runat="server" Text=''></asp:Label></td>
                            </tr>
                            <tr><td colspan="3"><uc1:spacer id="Spacer4" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></td></tr>            
                            <tr>                
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td><strong>User ID</strong>&nbsp;<asp:Label ID="ui_lblUserID" runat="server" Text=''></asp:Label></td>
                            </tr>
                            <tr><td colspan="3"><uc1:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></td></tr>
                        </table>                    
            </asp:Panel>   
            <!-- Customer Section -->
        
            <!-- Create Customer Popup -->
            <asp:Panel ID="pnlCreateUser" style="display:none;" runat="server" cssClass="PopupStyle">
                <div><ZNode:CreateUser ID="uxCreateUser" runat="server" /></div>            
            </asp:Panel>
    
            <!-- Quick Order Section-->            
            <asp:Panel ID="pnlQuickOrder" runat="server" cssClass="PopupStyle" style="display:none;">
                <asp:UpdatePanel ID="UpdatePanelQuickOrder" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>                
                        <div class="QuickOrder"><ZNode:QuickOrder ID="uxQuickOrder" runat="server" /></div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            
            <!-- Shopping Cart section -->
            <h4>Shopping Cart</h4>
            <asp:Panel ID="pnlCart" runat="server" CssClass="ShoppingCart">
                    <div><asp:LinkButton ID="linkbtnQuickOrder" runat="server" Text="Select products" OnClick="linkbtnQuickOrder_Click"></asp:LinkButton></div>
                    <div><uc1:Spacer ID="Spacer" SpacerWidth="1" SpacerHeight="10" runat="server" /></div>
                    <div><ZNode:ShoppingCart ID="uxShoppingCart" ShowTaxShipping="true" runat="server" /></div>                
            </asp:Panel>        
           
           <!-- Payment and Shipping Section -->
            <asp:Panel ID="pnlPaymentShipping" runat="server" CssClass="Payment" Visible="false">                
                <div><ZNode:Payment id="uxPayment" runat="server" /></div>                
            </asp:Panel>
            <div><asp:Label ID="lblError" runat="server" EnableViewState="false" CssClass="Error"></asp:Label></div>
            <div><uc1:Spacer ID="Spacer8" SpacerWidth="1" SpacerHeight="15" runat="server" /></div>
            <div align="right"><asp:Button ID="btnSubmit" runat="server" Text="Submit Order" ValidationGroup="groupPayment" CssClass="Button" OnClick="Submit_Click" />
            <div><uc1:Spacer ID="Spacer9" SpacerWidth="1" SpacerHeight="10" runat="server" /></div></div>
        </asp:Panel>
        <!-- Order Receipt -->      
        <asp:Panel ID="pnlReceipt" runat="server" Visible="false">
            <div><ZNode:Confirm ID="uxConfirm" runat="server" /></div>
            <div><uc1:spacer id="Spacer5" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></div>
            <div class="LinkButton"><asp:LinkButton ID="linkBottom" runat="server" OnClick="linkNewOrder_Click">Create a new order</asp:LinkButton></div>
        </asp:Panel>    
    </ContentTemplate> 
</asp:UpdatePanel>
<!--During Update Process -->
<asp:UpdateProgress ID="UpdateProgressMozilla" runat="server" DisplayAfter="0" DynamicLayout="true" Visible="false">
    <ProgressTemplate>                                    
         <asp:Panel ID="Panel1" CssClass="overlay" runat="server">
            <asp:Panel ID="Panel2" CssClass="loader" runat="server">
                Loading...<img id="Img1" align="absmiddle" src="~/Images/buttons/loading.gif" runat="server" />
            </asp:Panel>
        </asp:Panel>
    </ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:UpdateProgress ID="UpdateProgressIE" runat="server" DisplayAfter="0" DynamicLayout="true" Visible="false">                                
    <ProgressTemplate>
        <div id="updateProgress">
        <iframe frameborder="0" src="about:blank" style="border:0px;position:absolute;z-index:9;left:0px;top:0px;width:expression(this.offsetParent.scrollWidth);height:expression(this.offsetParent.scrollHeight);filter:progid:DXImageTransform.Microsoft.Alpha(Opacity=75, FinishOpacity=0, Style=0, StartX=0, FinishX=100, StartY=0, FinishY=100);"></iframe>
        <div style="position:absolute;z-index:10;left:expression((this.offsetParent.clientWidth/2)-(this.clientWidth/2)+this.offsetParent.scrollLeft);top:expression((this.offsetParent.clientHeight/2)-(this.clientHeight/2)+this.offsetParent.scrollTop);"><div>Loading...<img id="Img1" align="absmiddle" src="~/Images/buttons/loading.gif" runat="server" /></div></div>
        </div>
    </ProgressTemplate>                                                             
</asp:UpdateProgress>
</div>
</asp:Content>
