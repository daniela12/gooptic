<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master"  AutoEventWireup="true" CodeFile="add_Highlights.aspx.cs" Inherits="Admin_Secure_catalog_product_add_Highlights" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
<asp:ScriptManager id="ScriptManager" runat="server"></asp:ScriptManager>
<div class="Form">
    <h1><asp:Label ID="lblTitle" runat="server" Text="Add a Product Highlights"></asp:Label></h1>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                             
            </td>
            <td align="right">
                <asp:Button CssClass="Button" ID="btntopAddSelectedAddons" ValidationGroup="grpAssociateAddOn" OnClick="btnAddSelectedAddons_Click" Text="Add Selected Items" runat="server" />                   
                <asp:Button ID="btnCancel" runat="server" CssClass="Button" Text="Cancel" OnClick="btnCancel_Click"  />
            </td>
        </tr>
    </table>
    <p></p>
    <div><asp:Label CssClass="Error" ID="lblErrorMessage" runat="server" EnableViewState="false" Visible="false"></asp:Label></div>    
    <h4>Highlight Search</h4>
    <small>Search for Highlights to associate with your product</small>
    <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
       <table border="0" width="60%">
            <tr>
                <td>
                    <div class="FieldStyle">Name</div>
                    <div class="ValueStyle"><asp:TextBox ID="txtName" runat="server" ValidationGroup="grpSearch"></asp:TextBox></div>                            
                </td>
                <td>
                    <div class="FieldStyle">Type</div>
                    <div class="ValueStyle"><asp:DropDownList ID="ddlHighlightType" runat="server"></asp:DropDownList></div>
                </td>
           </tr>
           <tr>
                <td align="left" colspan="2">
                    <div class="ValueStyle">
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" CssClass="Button" ValidationGroup="grpSearch" />           
                    <asp:Button ID="btnClearSearch" runat="server" OnClick="btnClearSearch_Click" Text="Clear Search" CssClass="Button" CausesValidation="False" ValidationGroup="grpSearch" />
                    </div>        
                </td>        
           </tr>
        </table>
     </asp:Panel>
    
    <h4>Product Highlight List</h4>
            <div class="HintStyle">Select One or more highlights to associate with this product.<br /><br /></div>
            <!-- Update Panel for grid paging that are used to avoid the postbacks -->
            <asp:UpdatePanel ID="updPnlHightlight" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
<asp:GridView id="uxGrid" runat="server" CssClass="Grid" EmptyDataText="No product Highlights exist in the database." PagerSettings-Visible="true" AllowSorting="True" PageSize="15" EnableSortingAndPagingCallbacks="False" Width="100%" CaptionAlign="Left" GridLines="None" ForeColor="#333333" CellPadding="4" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="uxGrid_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkProductHighlight" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="HighlightId" HeaderText="ID" />
                            <asp:BoundField DataField="Name" HeaderText="Name" /> 
                            <asp:BoundField HeaderText="Type"  DataField="HighlightName"/> 
                            <asp:BoundField HeaderText="Display Order"  DataField="DisplayOrder"/>                            
                        </Columns>
                        <FooterStyle CssClass="FooterStyle"/>
                        <RowStyle CssClass="RowStyle"/>                    
                        <PagerStyle CssClass="PagerStyle" Font-Underline="True" />
                        <HeaderStyle CssClass="HeaderStyle"/>
                        <AlternatingRowStyle CssClass="AlternatingRowStyle"/>
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
                    </asp:GridView> 
</ContentTemplate>
            </asp:UpdatePanel>
            <div><uc1:spacer id="Spacer3" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
            <div>
                <asp:Button CssClass="Button" ID="btnAddSelectedAddons" ValidationGroup="grpAssociateAddOn" OnClick="btnAddSelectedAddons_Click" Text="Add Selected Items" runat="server" />
                <asp:Button ID="btnBottomCancel" runat="server" CssClass="Button" Text="Cancel" OnClick="btnCancel_Click"  />
            </div>
 </div>
</asp:Content>