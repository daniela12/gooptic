<%@ Page Language="C#"  MasterPageFile="~/Admin/Themes/Standard/edit.master"AutoEventWireup="true" CodeFile="view.aspx.cs" Inherits="Admin_Secure_catalog_product_Attribute_Types_view" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">

<div class="Form">
     <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <h1>
                     Attribute Values For: <asp:Label ID="lblAttributeType" runat="server"></asp:Label>
                </h1>
                
            </td>
            <td align="right">
                <asp:Button ID="AttributeTypeList" runat="server" CssClass="Button" Text="< Back To Attributes" width="200px" OnClick="AttributeTypeList_Click" />
            </td>
        </tr>
    </table>
       
    <h4>
         Value List
    </h4>
    
    <div align="right">
    <span style="float:left">
        <asp:Label ID="FailureText" runat="Server" EnableViewState="false" CssClass="Error" />
    </span>
    <asp:Button ID="AddAttribute" runat="server" CssClass="Button" Text="Add Value" width="150px" OnClick="AddAttribute_Click" />
    </div>
    
    <uc1:Spacer ID="Spacer" runat="server" spacerheight="10" />
    
    <asp:GridView ID="uxGrid" runat="server" AllowPaging="True"
        AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="Grid"
        EmptyDataText="No Product Attributes exist in the database." ForeColor="#333333" GridLines="None" Width="100%" OnPageIndexChanging="uxGrid_PageIndexChanging" OnRowCommand="uxGrid_RowCommand" OnRowDeleting="uxGrid_RowDeleting">
        
        <Columns>            
            <asp:BoundField DataField="Name" HeaderText="Attribute Value" />
            <asp:BoundField DataField="DisplayOrder" HeaderText="Display Order" />
            <asp:TemplateField>
                <ItemTemplate>
                        <asp:Button ID="EditAttribute" CommandName="Edit" CommandArgument ='<%# Eval("AttributeId") %>' runat="server" CssClass="Button" Text="Edit"  />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                        <asp:Button ID="DeleteAttribute" CommandName="Delete" CommandArgument ='<%# Eval("AttributeId") %>' runat="server" CssClass="Button" Text="Delete"  />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        
        <RowStyle CssClass="RowStyle" />
        <EditRowStyle CssClass="EditRowStyle" />
        <HeaderStyle CssClass="HeaderStyle" />
        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
    </asp:GridView>

    <uc1:spacer id="Spacer5" SpacerHeight="1" SpacerWidth="3" runat="server"></uc1:spacer>
    
</div> 
</asp:Content>



