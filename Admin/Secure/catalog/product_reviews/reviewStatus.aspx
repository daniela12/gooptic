<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="reviewStatus.aspx.cs" Inherits="Admin_Secure_catalog_product_reviews_ReviewStatus" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">    
    <div class="Form">
          <h1>Review Title - &nbsp;<asp:Label ID="lblReviewHeader" runat="server" /></h1>
          <ZNode:DemoMode ID="DemoMode1" runat="server" />        
          <div class="HintStyle">Select "Active" to post the review on the site. "New" or "Inactive" reviews will not be shown on the public site.</div><br />
          <div class="FieldStyle">
            <asp:DropDownList ID="ListReviewStatus" runat="server">            
                <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inactive" Value="I"></asp:ListItem>
                <asp:ListItem Selected="True" Text="New" Value="N"></asp:ListItem>
            </asp:DropDownList>
          </div>
          <div><ZNode:spacer id="Spacer1" SpacerHeight="20" SpacerWidth="3" runat="server"></ZNode:spacer></div>
          <div class="ValueField">
              <asp:Button ID="UpdateReviewStatus" runat="Server" CssClass="Button" Text="Submit" OnClick="UpdateReviewStatus_Click" />
              <asp:Button ID="Cancel" runat="Server" CssClass="Button" Text="Cancel" OnClick="CancelStatus_Click" />
          </div>
         <div><ZNode:spacer id="LongSpace" SpacerHeight="200" SpacerWidth="3" runat="server"></ZNode:spacer></div>
    </div>
</asp:Content>

