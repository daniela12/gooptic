<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="Admin_Secure_settings_shipping_View" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td><h1>Shipping Option - <asp:Label ID="lblDescription" runat="server"></asp:Label></h1></td>
			<td></td>
		</tr>
		<tr>
		    <td></td>
		    <td align="right">
                <asp:Button ID="EditShipping" runat="server" CssClass="Button" OnClick="EditShipping_Click" Text="Edit Shipping Option" />
            </td>
		</tr>
	</table>
	<h4>General Information</h4>
	    <table cellspacing="0" cellpadding="0" class="ViewForm">
	        <tr class="RowStyle" valign="middle">
	            <td class="FieldStyle">Profile Name</td>
	            <td class="ValueStyle"><asp:Label ID="lblProfileName" runat="server"></asp:Label></td>
	        </tr>
	         <tr class="AlternatingRowStyle">
	            <td class="FieldStyle">Shipping Code</td>
	            <td class="ValueStyle"><asp:Label ID="lblShippingCode" runat="server"></asp:Label></td>
	        </tr>
	        <tr class="RowStyle">
	            <td class="FieldStyle">Shipping Type</td>
	            <td class="ValueStyle"><asp:Label ID="lblShippingType" runat="server"></asp:Label></td>
	        </tr>
	        <tr class="AlternatingRowStyle">
	            <td class="FieldStyle">Destination Country</td>
	            <td class="ValueStyle"><asp:Label ID="lblDestinationCountry" runat="server"></asp:Label></td>
	        </tr>
	        <tr class="RowStyle">
	            <td class="FieldStyle">Handling Charge</td>
	            <td class="ValueStyle">$ <asp:Label ID="lblHandlingCharge" runat="server"></asp:Label></td>
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
	    
	<asp:Panel ID="pnlShippingRuletypes" runat="server" Visible="false">
	<h4>Shipping Rules</h4>
	<table width="100%" border="0" cellpadding="0" cellspacing="0">
	    <tr>
	        <td>Shipping rules determine shipping costs based on quantity, weight and other parameters. <b>You will need to add at least one rule</b> in order to use a shipping option in your storefront.
	        </td>
	        <td align="right">&nbsp;&nbsp;<asp:Button ID="btnAddRule" runat="server" CssClass="Button" Text="Add New Rule" OnClick="btnAddRule_Click" /></td>
	    </tr>
	</table>
	<br /><br />
	<asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" PageSize="25" OnRowDeleting="uxGrid_RowDeleting" AllowSorting="True" EmptyDataText="No rules have been added.">
        <Columns>
            <asp:BoundField DataField="ShippingRuleID" HeaderText="ID" />
            <asp:BoundField DataField="ShippingRuleTypeName" HeaderText="Rule Type" />
            <asp:TemplateField HeaderText="Base Cost">
                <ItemTemplate>                                
                      <%# DataBinder.Eval(Container.DataItem, "BaseCost","{0:c}").ToString()%>
                </ItemTemplate>                          
            </asp:TemplateField> 
             <asp:TemplateField HeaderText="Per Unit Cost">
                <ItemTemplate>                                
                      <%# DataBinder.Eval(Container.DataItem, "PerItemCost","{0:c}").ToString()%>
                </ItemTemplate>                          
            </asp:TemplateField> 
            
            <asp:BoundField DataField="LowerLimit" HeaderText="Lower Limit" />  
            <asp:BoundField DataField="UpperLimit" HeaderText="Upper Limit" />  
                     
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
    </asp:Panel>
</asp:Content>

