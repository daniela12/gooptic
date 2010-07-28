<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Secure_settings_payment_Default" Title="Untitled Page" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
	<table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td><h1>Payment Options</h1></td>
			<td align="right"><asp:button CssClass="Button" id="btnAdd" CausesValidation="False" Text="Add New Payment Option"
					Runat="server" onclick="btnAdd_Click"></asp:button></td>
		</tr>
	</table>
    <p>
    Using this page you can manage the payment options available for your customers. You can add payment options including Credit Cards, Purchase Orders, PayPal, etc
    </p>
	<h4>Payment Options List</h4>    
    <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" PageSize="25" OnRowDeleting="uxGrid_RowDeleting" AllowSorting="True" EmptyDataText="No records exist in the database.">
        <Columns>
            <asp:BoundField DataField="PaymentSettingID" HeaderText="ID" />
            <asp:TemplateField HeaderText="Payment Option">
                <ItemTemplate>
                    <a href='add.aspx?itemid=<%# DataBinder.Eval(Container.DataItem, "PaymentSettingID").ToString()%>'><%# DataBinder.Eval(Container.DataItem, "PaymentTypeName").ToString()%></a>
                </ItemTemplate>                          
            </asp:TemplateField>
            <asp:BoundField DataField="ProfileName" HeaderText="Profile Name" />
            <asp:TemplateField HeaderText="Enabled">
                <ItemTemplate>                                
                    <img id="Img1" alt="" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "ActiveInd").ToString()))%>' runat="server" />
                </ItemTemplate>                          
            </asp:TemplateField> 
            <asp:BoundField DataField="DisplayOrder" HeaderText="Display Order" />   
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


