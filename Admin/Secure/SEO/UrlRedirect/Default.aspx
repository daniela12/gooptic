<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Themes/Standard/content.master" CodeFile="Default.aspx.cs" Inherits="Admin_Secure_SEO_UrlRedirect_Default" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <div class="SEO">
	    <table cellspacing="0" cellpadding="0" width="100%">
		    <tr>
			    <td><h1>URL 301 Redirects</h1></td>
			    <td align="right"><asp:button CssClass="Button" id="btnAddUrlRedirect" CausesValidation="False" Text="Add URL 301 Redirect" runat="server" OnClick="btnAddUrlRedirect_Click"></asp:button></td>
		    </tr>
	    </table>

        <h4>Search URL Redirects</h4> 
        <asp:Panel ID="pnlSearch" DefaultButton="btnSearch" runat="server" CssClass="Form">
        <table border="0" width="70%">
            <tr>                
                <td>
                    <div class="FieldStyle">From URL</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtOldUrl" runat="server"></asp:TextBox></div>
                </td>                        
                <td>
                    <div class="FieldStyle">To URL</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtNewUrl" runat="server"></asp:TextBox></div>
                </td>
                <td>
                    <div class="FieldStyle">URL Status</div>                           
                    <div class="ValueStyle">
                        <asp:DropDownList ID="ddlURLStatus" runat="server">
                            <asp:ListItem  Text="All" Value="0"></asp:ListItem>
                            <asp:ListItem  Text="Enabled" Value="true"></asp:ListItem>
                            <asp:ListItem  Text="Disabled" Value="false"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>            
                <td colspan="3">       
                    <div class="ValueStyle">
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" CssClass="Button" />
                        <asp:Button ID="btnClearSearch" runat="server" OnClick="btnClearSearch_Click" Text="Clear Search" CssClass="Button" CausesValidation="False" />
                    </div>
                    </td> 
                    </tr>
        </table>
        </asp:Panel>
	    <h4>URL Redirect List</h4>    
        <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" PageSize="25" AllowSorting="True" EmptyDataText="No URL redirects exist in the database." OnRowDeleting="uxGrid_RowDeleting" OnRowDataBound="uxGrid_RowDataBound">
            <Columns>
                <asp:BoundField DataField="UrlRedirectID" HeaderText="ID" />                
                <asp:TemplateField HeaderText="From URL">
                    <ItemTemplate>                                
                        <%# DataBinder.Eval(Container.DataItem, "OldUrl").ToString().Replace("~/",string.Empty) %>
                    </ItemTemplate>
                </asp:TemplateField>               
                <asp:TemplateField HeaderText="To URL">
                    <ItemTemplate>                                
                        <%# DataBinder.Eval(Container.DataItem, "NewUrl").ToString().Replace("~/",string.Empty) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Enabled">
                    <ItemTemplate>                                
                        <img alt="" id="Img1" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(DataBinder.Eval(Container.DataItem, "IsActive").ToString()))%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField CommandName="Edit" Text="Edit" ButtonType="Button">
                    <ControlStyle CssClass="Button" />
                </asp:ButtonField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" CssClass="Button" runat="server" Text="Delete" CommandName="Delete" CommandArgument="Delete" />
                    </ItemTemplate>
                </asp:TemplateField>
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