<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="edit_review.aspx.cs" Inherits="Admin_Secure_catalog_product_reviews_edit_review" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">    
    <div class="Form">        
        <ZNode:DemoMode ID="DemoMode1" runat="server" />
        <table width="100%" cellspacing="0" cellpadding="0" >
		    <tr>
			    <td><h1>Edit Review - <asp:Label ID="lblReviewHeader" runat="server" /></h1></td>
			    <td align="right">
				    <asp:Button CssClass="Button" id="btnSubmitTop" CausesValidation="True" Text="Update" Runat="server" onclick="btnUpdate_Click"  ></asp:button>
				    <asp:Button CssClass="Button" id="btnCancelTop" CausesValidation="False" Text="Cancel" Runat="server" onclick="btnCancel_Click"></asp:button>
			    </td>
		    </tr>
	   </table>

        <p>&nbsp;</p>
        <div class="FieldStyle">Headline<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Headline" Display="Dynamic" ErrorMessage="* Enter a Headline" SetFocusOnError="True"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:TextBox ID='Headline' runat='server' MaxLength="100" Columns="50"></asp:TextBox></div>
        
        <div id='divPros' runat="server" visible="false">
            <div class="FieldStyle">Pros</div>
            <div class="ValueStyle"><asp:TextBox ID='Pros' runat='server' MaxLength="100" Columns="50"></asp:TextBox></div>
        </div>
        
        <div id='divCons' runat="server" visible="false">
            <div class="FieldStyle">Cons</div>
            <div class="ValueStyle"><asp:TextBox ID='Cons' runat='server' MaxLength="100" Columns="50"></asp:TextBox></div>
        </div>
        
        <div class="FieldStyle">Comments<span class="Asterix">*</span></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Comments" Display="Dynamic" ErrorMessage="* Enter a Comment" SetFocusOnError="True"></asp:RequiredFieldValidator></div>
        <div class="ValueStyle"><asp:TextBox ID='Comments' runat='server' TextMode="MultiLine" Rows="20" Columns="50"></asp:TextBox></div>
        
        <div class="FieldStyle">
            <asp:DropDownList ID="ListReviewStatus" runat="server">            
                <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inactive" Value="I"></asp:ListItem>
                <asp:ListItem Selected="True" Text="New" Value="N"></asp:ListItem>
            </asp:DropDownList>
        </div>        
        <div><ZNode:spacer id="Spacer1" SpacerHeight="20" SpacerWidth="3" runat="server"></ZNode:spacer></div>
        
        <div>
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="Button" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="Button" OnClick="btnCancel_Click" CausesValidation="False" />
        </div>
        
        <div><ZNode:spacer id="LongSpace" SpacerHeight="20" SpacerWidth="3" runat="server"></ZNode:spacer></div>
    </div>
</asp:Content>


