<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Secure_settings_taxes_Default" Title="Untitled Page" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
	<table cellspacing="0" cellpadding="0" width="100%">
		<tr>
		    <td><h1>Manage Tax Classes</h1></td>
			<td align="right"><asp:button CssClass="Button" id="btnAddTaxClass" CausesValidation="False" Text="Add Tax Class" Runat="server" onclick="btnAddTaxClass_Click"></asp:button></td>
		</tr>
	</table>
	<p>
        Tax classes are used to group product types with specific tax rules. For example you can create a tax class for product type "Food" that applies to a specific region.<br />
        <br />
        <b>Steps to define tax rules for your products:</b><br /><br />
        Step 1: Create a new Tax Class<br />
        Step 2: Associate rules to this Tax Class<br />
        Step 3: Associate products with this Tax Class<br />
    </p>
    
    <div><uc1:Spacer id="Spacer8" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:Spacer></div>
    <div><asp:Label ID="lblErrorMsg" EnableViewState="false" runat="server" CssClass="Error"></asp:Label></div>
    <div><uc1:Spacer id="Spacer1" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:Spacer></div>
    
	<h4>Tax Class List</h4>
    <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" PageSize="25" OnRowDeleting="uxGrid_RowDeleting" AllowSorting="True" EmptyDataText="No tax class exist in the database.">
        <Columns>
            <asp:BoundField DataField="TaxClassID" HeaderText="ID" />         
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="DisplayOrder" HeaderText="Display Order" />
            <asp:TemplateField HeaderText="Enabled">
                <ItemTemplate>                                
                    <img alt="" id="Img1" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "ActiveInd").ToString()))%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>   
            <asp:TemplateField>
                <ItemTemplate>
                <asp:Button ID="btnAddTaxRules" Width="120px" CommandName="TaxRules" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TaxClassID")%>' Text="Add Tax Rule" runat="server" CssClass="Button"  OnClick="btnAddTaxRules_Click" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField ButtonType="Button" Text="View" CommandName="View">
                 <ControlStyle CssClass="Button" />        
            </asp:ButtonField>
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
	<div><uc1:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></div>
</asp:Content>


