<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="Admin_Secure_settings_tax_View" Title="Untitled Page" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
	<table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td><h1>Tax Class - <asp:Label ID="lblTitle" runat="server"></asp:Label></h1></td>
			<td align="right"><asp:button CssClass="Button" id="btnEdit" CausesValidation="False" Text="Edit Tax Class" runat="server" onclick="btnEdit_Click"></asp:button></td>
		</tr>
	</table>
	<div>
	    <h4>General Information</h4>
	    <table cellspacing="0" cellpadding="0" class="ViewForm">
	        <tr class="RowStyle" valign="middle">
	            <td class="FieldStyle">Name</td>
	            <td class="ValueStyle"><asp:Label ID="lblProfileName" runat="server"></asp:Label></td>
	        </tr>	       
            <tr class="AlternatingRowStyle">
	            <td class="FieldStyle">Display Order</td>
	            <td class="ValueStyle"><asp:Label ID="lblDisplayOrder" runat="server"></asp:Label></td>
	        </tr>
	        <tr class="RowStyle">
	            <td class="FieldStyle">Enabled</td>
	            <td class="ValueStyle"><img ID="imgActive" runat="server" alt="" /></td>
	        </tr>
	    </table>
	</div>	
	<br /><br />
		
	<h4>Tax Rule List</h4>
	<p>
    Tax Rules are applied in the order of precedence. <b>For Example</b>, to implement a tax rule <br />to apply 5% tax 
    to residents of Alaska and 6.5% for all other US States do the following:<br /><br />
    <img id="Img3" src="~/images/icons/Right_arrow.gif" runat="server"/>&nbsp;&nbsp;Add a rule with Country=US, State=AK, Tax=5%, Precedence=1
    <br />
    <img id="Img4" src="~/images/icons/Right_arrow.gif" runat="server"/>&nbsp;&nbsp;Add a second rule with Country=US, State=ALL States, Tax=6.5%, Precedence=2
    </p>
     <table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td align="right"><asp:button CssClass="Button" id="Button1" CausesValidation="False" Text="Add Tax Rule" Runat="server" onclick="btnAdd_Click"></asp:button></td>
		</tr>
	</table>
	<br />
	
    <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" PageSize="25" OnRowDeleting="uxGrid_RowDeleting" AllowSorting="True" EmptyDataText="No tax rules exist in the database.">
        <Columns>
            <asp:BoundField DataField="TaxRuleID" HeaderText="ID" />         
            <asp:TemplateField HeaderText="Sales Tax">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "SalesTax")%>%
                </ItemTemplate>                          
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Country Code">
                <ItemTemplate>
                    <%# GetDefaultRegionCode(DataBinder.Eval(Container.DataItem, "DestinationCountryCode"))%>
                </ItemTemplate>                          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="State Code">
                <ItemTemplate>
                    <%# GetDefaultRegionCode(DataBinder.Eval(Container.DataItem, "DestinationStateCode"))%>
                </ItemTemplate>                          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="County">
                <ItemTemplate>
                    <%# GetCountyName(DataBinder.Eval(Container.DataItem, "CountyFIPS"))%>
                </ItemTemplate>                          
            </asp:TemplateField>
            
            <asp:BoundField DataField="Precedence" HeaderText="Precedence" />
                        
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


