<%@ Page Language="C#"  MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="Admin_Secure_catalog_Promotions_add" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/spacer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <script type="text/javascript">    
        var hiddenTextValue; //alias to the hidden field: hideValue

        function AutoCompleteSelected(source, eventArgs) 
        {   
            hiddenTextValue = $get("<%=txtReqProductId.ClientID %>");
            hiddenTextValue.value = eventArgs.get_value();           
        }

        function AutoCompleteShowing(source, eventArgs) 
        {
            hiddenTextValue = $get("<%=txtReqProductId.ClientID %>");
            hiddenTextValue.value = 0;
        }

        function AutoComplete_PromoProductsSelected(source, eventArgs) 
        {   
            hiddenTextValue = $get("<%=txtPromProductId.ClientID %>");
            hiddenTextValue.value = eventArgs.get_value();           
        }        

        function AutoComplete_PromoProductsShowing(source, eventArgs) 
        { 
            hiddenTextValue = $get("<%=txtPromProductId.ClientID %>");
            hiddenTextValue.value = 0;
        }
    </script>
    <div class="Form">
        <asp:ScriptManager Id="ScriptManager" runat="server"></asp:ScriptManager>
	    <table width="100%" cellspacing="0" cellpadding="0" >
		    <tr>
			    <td><h1><asp:Label ID="lblTitle" Runat="server"></asp:Label>
                    <uc2:DemoMode ID="DemoMode1" runat="server" />
                </h1></td>
			    <td align="right">
				    <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"  ></asp:button>
				    <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	    </table>
	   
	    <div><uc1:spacer id="Spacer8" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
	    <h4>General Information</h4>
	    <div class="FieldStyle">Promotion Name<span class="Asterix">*</span></div>	    
        <div><asp:RequiredFieldValidator CssClass="Error" ID="RequiredFieldValidator7" runat="server" ControlToValidate="PromotionName" Display="Dynamic" ErrorMessage="Enter a Promotion Name." SetFocusOnError="True"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:TextBox ID="PromotionName" runat="server" Columns="25" ></asp:TextBox></div>
        
        <div class="FieldStyle">Promotion Description</div>	            
        <div class="ValueStyle"><asp:TextBox ID="Description" runat="server" Columns="25" ></asp:TextBox></div>
        
        <div class="FieldStyle">Begin date (MM/DD/YYYY)<span class="Asterix">*</span></div>
        <div class="ValueStyle">
        <asp:TextBox id="StartDate" Text="01/01/2007" runat="server" />
        <asp:ImageButton CausesValidation="false" ID="imgbtnStartDt" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/icons/SmallCalendar.gif" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="StartDate"
            CssClass="Error" Display="Dynamic" ErrorMessage="* Enter Valid Start Date"
            ValidationExpression="((^(10|12|0?[13578])([/])(3[01]|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(11|0?[469])([/])(30|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(2[0-8]|1[0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(29)([/])([2468][048]00)$)|(^(0?2)([/])(29)([/])([3579][26]00)$)|(^(0?2)([/])(29)([/])([1][89][0][48])$)|(^(0?2)([/])(29)([/])([2-9][0-9][0][48])$)|(^(0?2)([/])(29)([/])([1][89][2468][048])$)|(^(0?2)([/])(29)([/])([2-9][0-9][2468][048])$)|(^(0?2)([/])(29)([/])([1][89][13579][26])$)|(^(0?2)([/])(29)([/])([2-9][0-9][13579][26])$))"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator CssClass="Error" ID="RequiredFieldValidator4" runat="server" ControlToValidate="StartDate" Display="Dynamic" ErrorMessage="* Enter a valid start Date" SetFocusOnError="True"></asp:RequiredFieldValidator>
        </div>
        
        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" Enabled="true" PopupButtonID="imgbtnStartDt" runat="server" TargetControlID="StartDate"></ajaxToolKit:CalendarExtender>        
        
        <div class="FieldStyle">End date (MM/DD/YYYY)<span class="Asterix">*</span></div>        
        <div class="ValueStyle">
            <asp:TextBox id="EndDate" Text="01/01/2007" runat="server" />
            <asp:ImageButton CausesValidation="false" ID="ImgbtnEndDt" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/icons/SmallCalendar.gif" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="EndDate"
                CssClass="Error" Display="Dynamic" ErrorMessage="* Enter Valid Date in MM/DD/YYYY format"
                ValidationExpression="((^(10|12|0?[13578])([/])(3[01]|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(11|0?[469])([/])(30|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(2[0-8]|1[0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(29)([/])([2468][048]00)$)|(^(0?2)([/])(29)([/])([3579][26]00)$)|(^(0?2)([/])(29)([/])([1][89][0][48])$)|(^(0?2)([/])(29)([/])([2-9][0-9][0][48])$)|(^(0?2)([/])(29)([/])([1][89][2468][048])$)|(^(0?2)([/])(29)([/])([2-9][0-9][2468][048])$)|(^(0?2)([/])(29)([/])([1][89][13579][26])$)|(^(0?2)([/])(29)([/])([2-9][0-9][13579][26])$))"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator CssClass="Error" ID="RequiredFieldValidator5" runat="server" ControlToValidate="EndDate" Display="Dynamic" ErrorMessage="* Enter a valid Exp Date" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareEndDate" runat="server" ControlToCompare="StartDate" ControlToValidate="EndDate"  Display="Dynamic" CssClass="Error" ErrorMessage="The Expiration date must be later than the Start Date"  Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
        </div>        
        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" Enabled="true" PopupButtonID="ImgbtnEndDt" runat="server" TargetControlID="EndDate"></ajaxToolKit:CalendarExtender>
      
        <div class="FieldStyle">Display Order<span class="Asterix">*</span></div> 	    
        <div class="HintStyle">Enter a number. Items with a lower number are displayed first on the page.</div>                
        <div class="ValueStyle">
            <asp:TextBox ID="DisplayOrder" runat="server" MaxLength="9" Columns="9"></asp:TextBox>
            <asp:requiredfieldvalidator id="Requiredfieldvalidator11" runat="server" Display="Dynamic"  ErrorMessage="*Enter a Display Order" ControlToValidate="DisplayOrder"></asp:requiredfieldvalidator>
            <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="DisplayOrder" Display="Dynamic" 
            ErrorMessage="Enter a whole number." MaximumValue="999999999" MinimumValue="1" Type="Integer"></asp:RangeValidator>
        </div>       
        
        <h4>Discount</h4>    
        <div class="FieldStyle">Select Discount Type</div>
        <div class="ValueStyle"><asp:DropDownList id="DiscountType" runat="server" OnSelectedIndexChanged="DiscountType_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></div>        
                           
        <asp:UpdatePanel ID="updPnlProducts" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:TextBox CssClass="HiddenFieldStyle" Text="0" ID="txtPromProductId" runat="server"></asp:TextBox>
                <asp:TextBox CssClass="HiddenFieldStyle" Text="0" ID="txtReqProductId" runat="server"></asp:TextBox>                
                    <asp:Panel id="pnlProducts" runat="server" Visible="false">
                        <table class="ValueStyle" cellpadding="0" cellspacing="0" width="380px">
                            <tr>
                                <td>
                                    <div class="FieldStyle">Quantity</div>
                                    <div class="TextValueStyle"><asp:DropDownList width="50" id="ddlMinimumQty" runat="server"></asp:DropDownList></div>
                                </td>                           
                                <td style="width:250px;">
                                    <div class="FieldStyle">Required Product</div>                                
                                    <div class="TextValueStyle"><asp:TextBox ID="txtRequiredProduct" AutoPostBack="true" AutoCompleteType="None" autocomplete="off" runat="server" ></asp:TextBox></div>
                                    <ajaxToolkit:AutoCompleteExtender 
                                        ID="AutoCompleteExtender3" 
                                        runat="server" 
                                        TargetControlID="txtRequiredProduct"
                                        ServicePath="ZNodeCatalogServices.asmx"
                                        ServiceMethod="GetCompletionListWithContextAndValues"
                                        UseContextKey="true"
                                        MinimumPrefixLength="2" 
                                        EnableCaching="false"
                                        CompletionSetCount="100" 
                                        CompletionInterval="1" 
                                        DelimiterCharacters=";, :"
                                        BehaviorID="autoCompleteBehavior3" OnClientPopulating="AutoCompleteShowing"  OnClientItemSelected="AutoCompleteSelected"/>                    
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator8" runat="server" ErrorMessage="Select Product" Display="Dynamic" ControlToValidate="txtRequiredProduct" CssClass="Error"></asp:RequiredFieldValidator></div>
                                </td>
                            </tr>                   
                            <tr><td colspan="2"><div class="HintStyle">Enter the product that must be purchased for this promotion to apply.</div></td></tr>
                        </table>                    
                    </asp:Panel>
                               
                    <asp:Panel id="pnlPromotionalProduct" runat="server" Visible="false">
                        <table class="ValueStyle" cellpadding="0" cellspacing="0" width="380px">
                            <tr>
                                <td>
                                    <div class="FieldStyle">Quantity</div>
                                    <div class="TextValueStyle"><asp:DropDownList width="50" id="ddlQuantity" runat="server"></asp:DropDownList></div>
                                </td>                        
                                <td style="width:250px;">
                                    <div class="FieldStyle">Promotional Product</div>                                
                                    <div class="TextValueStyle"><asp:TextBox ID="txtPromoProduct" AutoPostBack="true" AutoCompleteType="None" autocomplete="off" runat="server" ></asp:TextBox></div>
                                    <ajaxToolkit:AutoCompleteExtender 
                                        ID="AutoCompleteExtender1" 
                                        runat="server" 
                                        TargetControlID="txtPromoProduct"
                                        ServicePath= "ZNodeCatalogServices.asmx"                                
                                        ServiceMethod="GetCompletionListWithContextAndValues"
                                        UseContextKey="true"
                                        MinimumPrefixLength="2" 
                                        EnableCaching="false"
                                        CompletionSetCount="100" 
                                        CompletionInterval="1" 
                                        DelimiterCharacters=";, :"
                                        BehaviorID="autoCompleteBehavior4" OnClientShowing="AutoComplete_PromoProductsShowing"  OnClientItemSelected="AutoComplete_PromoProductsSelected"/>                    
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator9" runat="server" ErrorMessage="Select promotional product." Display="Dynamic" ControlToValidate="txtPromoProduct" CssClass="Error"></asp:RequiredFieldValidator></div>
                                </td>
                            </tr>                       
                            <tr><td colspan="2"><div class="HintStyle">Enter the product that will be discounted in this promotion.</div></td></tr>
                        </table>                  
                    </asp:Panel>              
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="ValueStyle"></div>
        <div class="FieldStyle">Discount Amount<span class="Asterix">*</span></div>
        <small><asp:Label ID="lblDiscAmtMessage" runat="server" Visible="false">Enter the amount discounted from the Promotional Product.</asp:Label></small>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Discount" Display="Dynamic" ErrorMessage="* Enter Discount" SetFocusOnError="True" CssClass="Error"></asp:RequiredFieldValidator></div>        
        <div class="ValueStyle">
            <asp:TextBox ID="Discount" runat="server" MaxLength="7" Columns="25"></asp:TextBox>
            <asp:RangeValidator ID="discAmountValidator" runat="server" ControlToValidate="Discount"
                CssClass="Error" Enabled="false" Display="Dynamic" MaximumValue="9999999" MinimumValue="0.01" CultureInvariantValues="true"
                Type="Currency"></asp:RangeValidator>
            <asp:RangeValidator ID="discPercentageValidator" Enabled="true" runat="server" ControlToValidate="Discount"
                CssClass="Error" Display="Dynamic" MaximumValue="100" CultureInvariantValues="true" MinimumValue="0.01" SetFocusOnError="True" Type="Double"></asp:RangeValidator>
        </div>        
        
        <div class="FieldStyle">Apply to Profile</div>
        <div class="ValueStyle"><asp:DropDownList ID="ddlProfileTypes" runat="server"></asp:DropDownList></div>
        
        <asp:Panel ID="pnlOrderMin" runat="server">
            <div class="FieldStyle">Order Minimum Amount<span class="Asterix">*</span></div>
            <small>Enter the minimum amount the customer needs to order before this promotion takes effect.</small>
            <div><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="OrderMinimum" Display="Dynamic" ErrorMessage="* Order Minimum is required" SetFocusOnError="True" CssClass="Error"></asp:RequiredFieldValidator></div>
            <div><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="OrderMinimum" Display="Dynamic" ErrorMessage="* Enter a valid Order Minimum Amount" SetFocusOnError="true" ValidationExpression="(^N/A$)|(^[-]?(\d+)(\.\d{0,3})?$)|(^[-]?(\d{1,3},(\d{3},)*\d{3}(\.\d{1,3})?|\d{1,3}(\.\d{1,3})?)$)" CssClass="Error"></asp:RegularExpressionValidator></div>
            <div class="ValueStyle">$&nbsp;<asp:TextBox ID="OrderMinimum" runat="server" MaxLength="25" Columns="25" Width="170px">0</asp:TextBox></div>
        </asp:Panel>
        
        <h4>Coupon Information</h4>
        <div class="ValueStyle"><asp:CheckBox AutoPostBack="true" ID="chkCouponInd" runat="server" Text="Requires Coupon" OnCheckedChanged="CouponInd_CheckedChanged" /></div>
        
        <asp:Panel ID="pnlCouponInfo" runat="server" Visible="false">
	        <div class="FieldStyle">Enter Coupon Code<span class="Asterix">*</span></div>
            <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CouponCode" Display="Dynamic" ErrorMessage="* Enter a Coupon Code" SetFocusOnError="True" CssClass="Error"></asp:RequiredFieldValidator></div>
            <div class="ValueStyle"><asp:TextBox ID="CouponCode" runat="server" Columns="25" ></asp:TextBox></div>        
            
            <div class="FieldStyle">Promotion Message</div>
	        <small>Enter a message to display when a coupon is applied in the shopping cart.</small>            
            <div class="ValueStyle"><asp:TextBox ID="txtPromotionMessage" runat="server" Columns="25" ></asp:TextBox></div>        
            
            <div class="FieldStyle">Enter Available Quantity<span class="Asterix">*</span></div>
            <div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Quantity" Display="Dynamic" ErrorMessage="* Enter Quantity" SetFocusOnError="True" CssClass="Error"></asp:RequiredFieldValidator></div> 
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="Quantity"
                Display="Dynamic" ErrorMessage="* Enter a valid quantity. Enter a number between 0-9999" MaximumValue="9999"
                MinimumValue="0" Type="Integer" CssClass="Error"></asp:RangeValidator>
            <div class="ValueStyle"><asp:TextBox ID="Quantity" runat="server" MaxLength="4" Columns="25" Width="173px">99</asp:TextBox></div>
        </asp:Panel>       
        
        <div><uc1:spacer id="Spacer2" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>
        <div class="Error"><asp:Label ID="lblError" runat="server" Visible="true"></asp:Label></div>    
        <div><uc1:spacer id="Spacer1" SpacerHeight="15" SpacerWidth="3" runat="server"></uc1:spacer></div>
                
        <asp:button class="Button" id="SubmitButton" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click" ></asp:button>
	    <asp:button class="Button" id="CancelButton" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
    </div>
</asp:Content>