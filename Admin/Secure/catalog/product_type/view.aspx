<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master"  AutoEventWireup="true" CodeFile="view.aspx.cs" Inherits="Admin_Secure_catalog_product_type_view" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">

<div class="Form">
     <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <h1>
                     Attributes for product type "<asp:Label ID="lblProductType" runat="server"></asp:Label>"
                </h1>
                
            </td>
            <td align="right">
                <asp:Button ID="ProductTypeList" runat="server" CssClass="Button" Text="< Back" OnClick="ProductTypeList_Click" />&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" CssClass="Button" Text="Add Attribute" width="150px" OnClick="AddAttributeType_Click" />                                
            </td>
        </tr>
    </table>

    
    
    <uc1:Spacer ID="Spacer" runat="server" spacerheight="10" />
    
    <asp:GridView ID="uxGrid" runat="server" AllowPaging="True"
        AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="Grid"
        EmptyDataText="No Attributes exist in the database." ForeColor="#333333" GridLines="None" Width="100%" OnPageIndexChanging="uxGrid_PageIndexChanging" OnRowCommand="uxGrid_RowCommand" OnRowDeleting="uxGrid_RowDeleting">
        
        <Columns>
            <asp:BoundField DataField="ProductAttributeTypeID" HeaderText="ID" />
            <asp:BoundField DataField="Name" HeaderText="Attribute" />
            <asp:BoundField DataField="DisplayOrder" HeaderText="Display Order" />
            <asp:TemplateField>
                <ItemTemplate>
                        <asp:Button ID="DeleteAttribute" CommandName="Delete" CommandArgument ='<%# Eval("ProductAttributeTypeID") %>' runat="server" CssClass="Button" Text="Delete" width="100px"  />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        
        <RowStyle CssClass="RowStyle" />
        <EditRowStyle CssClass="EditRowStyle" />
        <HeaderStyle CssClass="HeaderStyle" />
        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
    </asp:GridView>

    <uc1:spacer id="Spacer5" SpacerHeight="100" SpacerWidth="3" runat="server"></uc1:spacer>
    
</div>
</asp:Content>


