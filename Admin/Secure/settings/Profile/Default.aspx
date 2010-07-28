<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Secure_settings_Profile_Default" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
<div class="Form">
<table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td><h1>Profiles</h1></td>
			<td align="right"><asp:button CssClass="Button" id="btnAddProfile" CausesValidation="False" Text="Add a New Profile" Runat="server" onclick="btnAddProfile_Click"></asp:button></td>
		</tr>
</table>
<p>This page allows you to manage the list of profiles in the storefront. Profiles can be associated with an Account, Pricing, Shipping, and Payment Options. This allows you to customize the storefront experience for each type of user that logs in.</p>

<h4>Profile List</h4> 
<asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" AllowSorting="True" EmptyDataText="No profiles available to display.">
    <Columns>
        <asp:BoundField DataField="ProfileID" HeaderText="ID" />
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <a href='add.aspx?ItemID=<%# DataBinder.Eval(Container.DataItem, "ProfileID").ToString()%>'><%# DataBinder.Eval(Container.DataItem, "Name").ToString()%></a>
            </ItemTemplate> 
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Anonymous">
            <ItemTemplate>                                
                <img alt="" id="Img1" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "IsAnonymous").ToString()))%>' runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Default">
            <ItemTemplate>                                
                <img alt="" id="Img1" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "IsDefault").ToString()))%>' runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Show Price">
        <ItemTemplate>                                
                <img alt="" id="Img1" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "ShowPricing").ToString()))%>' runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Wholesale">
            <ItemTemplate>                                
                <img alt="" id="Img1" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "UseWholesalePricing").ToString()))%>' runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Exempt">
            <ItemTemplate>                                
                <img alt="" id="Img1" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "TaxExempt").ToString()))%>' runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Self Sign-up">
            <ItemTemplate>
                <img id="Img1" runat="server" alt="" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "ShowOnPartnerSignup").ToString()))%>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:ButtonField CommandName="Edit" Text="Edit" ButtonType="Button">
            <ControlStyle CssClass="Button" />
        </asp:ButtonField>
        <asp:ButtonField CommandName="Delete" Text="Delete" ButtonType="Button">
            <ControlStyle CssClass="Button" />
        </asp:ButtonField>
    </Columns>
    <FooterStyle CssClass="FooterStyle"/>
    <RowStyle CssClass="RowStyle"/>                    
    <PagerStyle CssClass="PagerStyle" Font-Underline="True" />
    <HeaderStyle CssClass="HeaderStyle"/>
    <AlternatingRowStyle CssClass="AlternatingRowStyle"/>
    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
</asp:GridView>

</div>
</asp:Content>

