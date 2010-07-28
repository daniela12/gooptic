<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Secure_catalog_product_Highlights_Default" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <div class="Form">
	    <table cellspacing="0" cellpadding="0" width="100%">
		    <tr>
			    <td><h1>Product Highlights</h1></td>
			    <td align="right"><asp:button CssClass="Button" id="btnAddHighlight" CausesValidation="False" Text="Add New Highlight"
					    Runat="server" onclick="btnAddHighlight_Click"></asp:button></td>
		    </tr>
	    </table>
        <p>Use this page to manage the list of Product highlights which are ways to associate common features such as awards or certifications to your product.</p>
        
        <h4>Search Product Highlights</h4>   
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
        <asp:GridView ID="uxGrid" Width="100%" CssClass="Grid" CellPadding="4" CaptionAlign="Left" GridLines="None" runat="server" AutoGenerateColumns="False" AllowPaging="True" ForeColor="Black" OnPageIndexChanging="uxGrid_PageIndexChanging" OnRowCommand="uxGrid_RowCommand" PageSize="25" EmptyDataText="No highlights found for this Product.">
           <FooterStyle CssClass="FooterStyle" />
           <Columns>    
                 <asp:BoundField DataField="HighlightID" HeaderText="ID" Visible="true" />         
                <asp:TemplateField HeaderText="Image">
                     <ItemTemplate>
                        <a  href='<%# "add.aspx?itemid=" + DataBinder.Eval(Container.DataItem,"Highlightid")%>' id="LinkView">
                            <img id="Img1" alt="" src='<%# GetImagePath(Eval("ImageFile").ToString()) %>' runat="server" style="border:none"  />
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="Name" />            
                <asp:TemplateField>
                    <HeaderTemplate>Enable Hyperlink</HeaderTemplate>
                    <ItemTemplate><img alt="" id="Img2" src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark((bool)DataBinder.Eval(Container.DataItem, "DisplayPopup"))%>' runat="server" /></ItemTemplate>
                </asp:TemplateField>  
                <asp:BoundField HeaderText="Type"  DataField="HighlightName"/>
                <asp:BoundField HeaderText="Display Order"  DataField="DisplayOrder"/>    
                <asp:TemplateField HeaderText="Is Active?">
                    <ItemTemplate>                                
                        <img id="Img3" alt='' src='<%# ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark((bool)DataBinder.Eval(Container.DataItem, "ActiveInd"))%>' runat="server" />
                    </ItemTemplate>                          
                </asp:TemplateField>      
                <asp:TemplateField HeaderText="" ItemStyle-Wrap="false" >
                    <ItemTemplate>                
                    <asp:Button ID="Button1" CommandName="Edit"  CommandArgument ='<%#DataBinder.Eval(Container.DataItem,"HighlightID")%>' Text="Edit" runat="server" CssClass="Button"  />                
                    &nbsp;<asp:Button ID="Button5" CommandName="Delete"  CommandArgument ='<%#DataBinder.Eval(Container.DataItem,"HighlightID")%>' Text="Delete" runat="server" CssClass="Button" />
                    </ItemTemplate>                
                </asp:TemplateField> 
            </Columns>
            <RowStyle CssClass="RowStyle" />
            <EditRowStyle CssClass="EditRowStyle" />
            <PagerStyle CssClass="PagerStyle" />
            <HeaderStyle CssClass="HeaderStyle" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
        </asp:GridView>    
	    <div><uc1:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="10" runat="server"></uc1:spacer></div>
    </div>
</asp:Content>