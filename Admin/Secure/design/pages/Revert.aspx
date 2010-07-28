<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="Revert.aspx.cs" Inherits="Admin_Secure_design_Page_Revert" Title="Untitled Page" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
	<table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td><h1>Revisions for Page: <%=PageName %><uc2:DemoMode ID="DemoMode1" runat="server" />
            </h1></td>
			<td align="right"><asp:button CssClass="Button" id="btnBack" CausesValidation="False" Text="< Go Back" Runat="server" OnClick="btnBack_Click" /></td>
		</tr>
	</table>
    <p>
    If you need to revert back to a previous version of this page, click on the "Revert" button next to the target revision. 
    </p>
   
    <div><asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="#00C000"></asp:Label></div>
    <div><asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label></div>
    
	<h4>Revisions</h4>    
    <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" PageSize="25" OnRowDeleting="uxGrid_RowDeleting" AllowSorting="True" EmptyDataText="No content pages exist in the database.">
        <Columns>
            <asp:BoundField DataField="RevisionID" HeaderText="ID" />
            <asp:BoundField DataField="UpdateUser" HeaderText="Updated By" />
            <asp:BoundField DataField="UpdateDate" HeaderText="Updated On" />
            <asp:BoundField DataField="Description" HeaderText="Description" />

            <asp:ButtonField CommandName="Revert" Text="Revert to this Version" ButtonType="Button">
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


