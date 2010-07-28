<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="add_addons.aspx.cs" Inherits="Admin_Secure_catalog_product_add_addons" %>
<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="ZNode" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
<asp:ScriptManager id="ScriptManager" runat="server"></asp:ScriptManager>
    <div class="Form">
        <h1><asp:Label ID="lblTitle" runat="server" Text="Add a Product Add-Ons"></asp:Label></h1>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <div>Use this page to associate product add-ons to this product.<br /> The associated add-ons will show up on the Product Add-Ons Tab.</div>
                    <ZNode:DemoMode id="DemoMode1" runat="server" />                    
                </td>
                <td align="right">
                    <asp:Button CssClass="Button" ID="btntopAddSelectedAddons" ValidationGroup="grpAssociateAddOn" OnClick="btnAddSelectedAddons_Click" Text="Add Selected Items" runat="server" />                   
                    <asp:Button ID="btnCancel" runat="server" CssClass="Button" Text="Cancel" OnClick="btnCancel_Click"  />
                </td>
            </tr>
        </table>
        <p></p>
        <div><asp:Label CssClass="Error" ID="lblAddOnErrorMessage" runat="server" EnableViewState="false" Visible="false"></asp:Label></div>
        <h4>Add-Ons Search</h4>
                    <small>Search for Add-Ons to associate with your product</small>
                    <asp:Panel ID="pnlAddOnSearch" DefaultButton="btnAddOnSearch" runat="server" >  
                        <table border="0" width="70%">
                            <tr>                
                                <td>
                                    <div class="FieldStyle">Name</div>
                                    <div class="ValueStyle"><asp:TextBox ID="txtAddonName" runat="server"></asp:TextBox></div>
                                </td> 
                                <td>
                                    <div class="FieldStyle">Title</div>
                                    <div class="ValueStyle"><asp:TextBox ID="txtAddOnTitle" runat="server"></asp:TextBox></div>
                                </td>                             
                                <td>
                                    <div class="FieldStyle">SKU or Product#</div>
                                    <div class="ValueStyle"><asp:TextBox ID="txtAddOnsku" runat="server"></asp:TextBox></div>
                                </td>                                
                            </tr>
                            <tr>            
                                <td colspan="3">
                                    <div class="ValueStyle">
                                        <asp:Button ID="btnAddOnSearch" runat="server" CssClass="Button" OnClick="btnAddOnSearch_Click" Text="Search" />
                                        <asp:Button ID="btnAddOnClear" CausesValidation="false" runat="server" OnClick="btnAddOnClear_Click" Text="Clear Search" CssClass="Button" />            
                                    </div> 
                                </td>        
                            </tr>
                        </table>        
                    </asp:Panel>
                    <h4>Add-On List</h4>
                    <div class="HintStyle">Select One or more Add-Ons to associate with this product.<br /><br /></div>
                    <!-- Update Panel for grid paging that are used to avoid the postbacks -->
                    <asp:UpdatePanel ID="updPnlAddOnGrid" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="uxAddOnGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="uxAddOnGrid_PageIndexChanging" CaptionAlign="Left" Width="100%" EnableSortingAndPagingCallbacks="False" PageSize="15" AllowSorting="True" PagerSettings-Visible="true" OnRowCommand="uxAddOnGrid_RowCommand"  EmptyDataText="No product Add-Ons exist in the database.">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkProductAddon" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AddOnId" HeaderText="ID" />
                                    <asp:BoundField DataField="Title" HeaderText="Title" />
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate><%# Eval("Name") %></ItemTemplate>                          
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DisplayOrder" HeaderText="DisplayOrder" />                                     
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