<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="Admin_Secure_settings_RuleTypes_List" MasterPageFile="~/Admin/Themes/Standard/content.master" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div class="Form">
        <table cellspacing="0" cellpadding="0" width="100%">
		        <tr>
			        <td><h1>Custom .NET Promotion Classes</h1></td>
			        <td align="right">
			            <asp:button CssClass="Button" id="btnAddRuleType" CausesValidation="False" Text="Add a New Promotion Rule Type" Runat="server" onclick="btnAddRuleType_Click"></asp:button>
                        
                    </td>
		        </tr>
        </table>
        
        <div><ZNode:Spacer ID="Spacer14" SpacerHeight="10" SpacerWidth="5" runat="server" /></div>
  
        <div><ZNode:spacer id="Spacer8" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>
        <div><asp:Label ID="lblErrorMsg" EnableViewState="false" runat="server" CssClass="Error"></asp:Label></div>
        <div><ZNode:spacer id="Spacer1" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>
    
        <h4>.NET Promotion Class List</h4>
        
        <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" AllowSorting="True" EmptyDataText="No profiles available to display." OnRowDeleting="uxGrid_RowDeleting">
            <Columns>
                <asp:BoundField DataField="DiscountTypeId" HeaderText="ID" />
                <asp:BoundField DataField="ClassName" HeaderText="Class Name" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:TemplateField HeaderText="Enabled">
                <ItemTemplate>                                
                    <img alt="" id="Img1" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "ActiveInd").ToString()))%>' runat="server" />
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
            <RowStyle CssClass="RowStyle" />
            <PagerStyle CssClass="PagerStyle" Font-Underline="True" />
            <HeaderStyle CssClass="HeaderStyle"/>
            <AlternatingRowStyle CssClass="AlternatingRowStyle"/>
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
        </asp:GridView>
    </div>
</asp:Content>