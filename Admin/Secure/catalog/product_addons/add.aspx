<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="Admin_Secure_catalog_product_addons_add" Title="Untitled Page"  ValidateRequest="false" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Src="~/Controls/HtmlTextBox.ascx" TagName="HtmlTextBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <div class="Form">
	    <table width="100%" cellSpacing="0" cellPadding="0" >
		    <tr>
			    <td><h1><asp:Label ID="lblTitle" Runat="server"></asp:Label></h1>
                    <uc2:DemoMode ID="DemoMode1" runat="server" />
                    <div><asp:Label ID="lblMsg" CssClass="Error" EnableViewState="false" runat="server"></asp:Label></div>
               </td>
			    <td align="right">
				   <div align="right">
    	               <asp:button class="Button" id="btnSubmitTop" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click" ValidationGroup="grpAddOn"></asp:button>
				       <asp:button class="Button" id="btnCancelTop" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
    	           </div>
			    </td>
		    </tr>
	     </table>    
	    
         <div><uc1:spacer id="Spacer1" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>

        
         <h4>General Settings</h4>
         
         <div class="FieldStyle">Name<span class="Asterix">*</span></div>
         <small>Enter an internal name for this Add-On (Ex. Model 1234 Color). This is not displayed to the customer</small>
         <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="Enter Product Add-On Name" ValidationGroup="grpAddOn" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator> </div>
         <div class="ValueStyle"><asp:TextBox ID="txtName" runat="server"></asp:TextBox></div>
         
         <div class="FieldStyle">Title<span class="Asterix">*</span></div>
         <small>Enter a title for this product Add-On (Ex. "Color"). This is the label displayed for your customer.</small>
         <div><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddOnTitle" ErrorMessage="Enter Product Add-On Title" ValidationGroup="grpAddOn" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator> </div>
         <div class="ValueStyle"><asp:TextBox ID="txtAddOnTitle" runat="server"></asp:TextBox></div>
         
        <div class="FieldStyle">Enter a Description</div>
        <div class="HintStyle">You can enter rich text description for this Add-On.</div>
        <div class="ValueStyle"><uc1:HtmlTextBox id="ctrlHtmlText" runat="server"></uc1:HtmlTextBox></div>
        
        <div class="FieldStyle">Display Order<span class="Asterix">*</span></div> 	    
        <div class="HintStyle">Enter a number. Items with a lower number are displayed first on the page.</div>                
        <div class="ValueStyle">
            <asp:TextBox ID="txtDisplayOrder" runat="server" MaxLength="9" Columns="5"></asp:TextBox>
            <asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" Display="Dynamic" ValidationGroup="grpAddOn" ErrorMessage="* Enter a Display Order" ControlToValidate="txtDisplayOrder"></asp:requiredfieldvalidator>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtDisplayOrder" Display="Dynamic" ValidationGroup="grpAddOn"
            ErrorMessage="Enter a whole number." MaximumValue="999999999" MinimumValue="1" Type="Integer"></asp:RangeValidator>
        </div>
         
         <div class="FieldStyle">Display Type</div>
         <div class="ValueStyle">
            <asp:DropDownList ID = "ddlDisplayType" runat="server">
                <asp:ListItem Value="DropDownList">Drop Down List</asp:ListItem>
                <asp:ListItem Value="RadioButton">Radio Buttons</asp:ListItem>
                <asp:ListItem Value="CheckBox">Check Boxes</asp:ListItem>                
            </asp:DropDownList>
         </div>
                             
         <asp:CheckBox ID="chkOptionalInd" CssClass="HintStyle" runat="server" text="Check this box if this Add-On is optional. " />
        
         <h4>Inventory Settings</h4>
        
         <div class="FieldStyle">Out of Stock Options</div>
         <div class="ValueStyle">
            <asp:RadioButtonList ID="InvSettingRadiobtnList" runat="server">
                <asp:ListItem Selected="True" Value="1">Only Sell if Inventory Available (User can only add to cart if inventory is above 0)</asp:ListItem>
                <asp:ListItem Value="2">Allow Back Order (Items can always be added to the cart. Inventory is reduced)</asp:ListItem>
                <asp:ListItem Value="3">Don't Track Inventory (Items can always be added to the cart and inventory is not reduced)</asp:ListItem>
            </asp:RadioButtonList>
         </div>
        
         <div class="FieldStyle">In Stock Message</div>
         <small>Displayed on the catalog when items are in stock</small>
         <div class="ValueStyle"><asp:TextBox ID="txtInStockMsg" runat="server"></asp:TextBox></div>
        
         <div class="FieldStyle">Out of Stock Message</div>
         <small>Displayed if this product is out of stock</small>
         <div class="ValueStyle"><asp:TextBox ID="txtOutofStock" runat="server">Out of Stock</asp:TextBox></div>
        
         <div class="FieldStyle">Back Order Message</div>
         <small>Displayed if an item is on back order</small>
         <div class="ValueStyle"><asp:TextBox ID="txtBackOrderMsg" runat="server"></asp:TextBox></div>
         <div align="right">
            <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" ValidationGroup="grpAddOn" onclick="btnSubmit_Click"></asp:button>
            <asp:button class="Button" id="btnCancel" CausesValidation="False" Text="CANCEL" Runat="server" onclick="btnCancel_Click"></asp:button>
         </div>
	</div>
</asp:Content>

