<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="Admin_Secure_settings_StoreLocator_List" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
<div class="Form">
   <table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td><h1>Store Locator</h1></td>
			<td align="right"><asp:button CssClass="Button" id="btnAddStore" CausesValidation="False" Text="Add New Store"
					Runat="server" onclick="btnAddStore_Click"></asp:button></td>
		</tr>
	</table>
	<p>Use this page to manage the stores displayed in the Store Locator.</p>
	<p>To enable the Store Locator search function you would need to purchase and upload ZIP code data to your storefront database. See our documentation for instructions. <a  href="http://help.znode.com/zip_code_data.htm?zoom_highlightsub=locator" target="_blank" runat="server">Click for Help</a></p>    
    
    <h4>Search Store</h4>
    <asp:Panel ID="Test" DefaultButton="btnSearch" runat="server" >  
        <table border="0" width="70%">
            <tr>                
                <td>
                    <div class="FieldStyle"> Store Name</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtstorename" runat="server"></asp:TextBox></div>
                </td>
                <td>
                    <div class="FieldStyle">Zip Code</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtzipcode" runat="server"></asp:TextBox></div>
                </td>  
            </tr>
            <tr>                            
                <td>
                    <div class="FieldStyle">City</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtcity" runat="server"></asp:TextBox></div>
                </td>
                <td>
                    <div class="FieldStyle">State</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtstate" runat="server"></asp:TextBox></div>
                </td>                
            </tr>
            <tr>            
                <td colspan="3">
                    <div class="ValueStyle">
                        <asp:Button ID="btnSearch" runat="server" CssClass="Button" OnClick="btnSearch_Click" Text="Search" />
                        <asp:Button ID="btnClear" CausesValidation="false" runat="server" OnClick="btnClear_Click" Text="Clear Search" CssClass="Button" />            
                    </div> 
                </td>        
            </tr>
        </table>        
    </asp:Panel>
    
     <h4>Store List</h4> 
      <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" EnableSortingAndPagingCallbacks="False" PageSize="25" AllowSorting="True" EmptyDataText="No store exist in the database.">
        <Columns>
            <asp:BoundField DataField="StoreID" HeaderText="ID" />
            <asp:TemplateField HeaderText="StoreName">
                <ItemTemplate>
                    <a href='Add.aspx?itemid=<%# DataBinder.Eval(Container.DataItem, "StoreId").ToString()%>'><%# Eval("Name") %></a>
                </ItemTemplate>                          
            </asp:TemplateField>            
            <asp:BoundField DataField="City" HeaderText="City" />
            <asp:BoundField DataField="State" HeaderText="State" />
            <asp:BoundField DataField="Zip" HeaderText="Zip Code" />
           <asp:TemplateField HeaderText="Is Active?">
                <ItemTemplate>                                
                    <img id="Img1" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "ActiveInd").ToString()))%>' runat="server" />
                </ItemTemplate>                          
            </asp:TemplateField>           
            <asp:ButtonField CommandName="Manage" Text="Manage" ButtonType="Button">
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
</div>
</asp:Content>


