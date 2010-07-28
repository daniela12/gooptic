<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master"  AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Secure_catalog_product_reviews_Default" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">    
    <div class="Form">
        <h1>Product Reviews</h1>
        <p>Use this page to manage the product reviews submitted by customers.</p>
        <h4>Search Product Reviews</h4>    
        <asp:Panel ID="pnlReviewSearch" DefaultButton="btnSearch" runat="server" >  
            <table border="0" width="60%">
                <tr>
                    <td>
                        <div class="FieldStyle">Review Title</div>
                        <div class="ValueStyle"><asp:TextBox ID="ReviewTitle" runat="server" ValidationGroup="grpSearch"></asp:TextBox></div>                            
                    </td>
                    <td>
                        <div class="FieldStyle">Nick Name</div>
                        <div class="ValueStyle"><asp:TextBox ID="Name" runat="server" ValidationGroup="grpSearch"></asp:TextBox></div>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:ScriptManager id="ScriptManager" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="updPnlProducts" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel id="pnlProducts" runat="server">
                                    <ajaxToolkit:ListSearchExtender id="LSE" runat="server" TargetControlID="ddlProductNames" PromptText="Type to search" PromptPosition="Bottom" /> 
                                    <div class="FieldStyle">Product Name</div>
                                    <div class="ValueStyle"><asp:DropDownList id="ddlProductNames" runat="server"></asp:DropDownList></div>
                                </asp:Panel>
                            </ContentTemplate>
                         </asp:UpdatePanel>
                    </td>
                    <td>
                        <div class="FieldStyle">Status</div>
                        <div class="ValueStyle">
                                <asp:DropDownlist ID="ListReviewStatus" runat="server">
                                    <asp:ListItem Selected="True" Text="All" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                    <asp:ListItem Text="Inactive" Value="I"></asp:ListItem>
                                    <asp:ListItem Text="New" Value="N"></asp:ListItem>
                                </asp:DropDownlist>
                        </div>
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
        <h4>Product Reviews List </h4>
        <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" PageSize="15" AllowSorting="True" EmptyDataText="No reviews available to display.">
            <Columns>
                <asp:BoundField DataField="ReviewId" HeaderText="ID" />             
                <asp:TemplateField HeaderText="Comments">
                    <ItemTemplate> 
                        <span class="FieldStyle">Title:</span><span class="ValueStyle"><%# DataBinder.Eval(Container.DataItem,"Subject")%></span>
                        <div><uc1:spacer id="Spacer1" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>
                        <div id='divPros' runat="server" visible="false"><span class="FieldStyle">Pros:</span><span class="ValueStyle"><%# DataBinder.Eval(Container.DataItem,"Pros")%></span></div>
                        <div><uc1:spacer id="Spacer2" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>
                        <div id='divCons' runat="server" visible="false"><span class="FieldStyle">Cons:</span><span class="ValueStyle"><%# DataBinder.Eval(Container.DataItem,"Cons")  %></span></div>
                        <div><uc1:spacer id="Spacer3" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>                        
                        <%# DataBinder.Eval(Container.DataItem, "Comments").ToString()%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "CreateDate","{0:d}")%></ItemTemplate>
                </asp:TemplateField>                
                <asp:BoundField DataField="CreateUser" HeaderText="User" />
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>                                
                        <%# ReviewStatus(DataBinder.Eval(Container.DataItem, "Status").ToString())%>
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnEditReview" CommandName="Edit"  CommandArgument ='<%#DataBinder.Eval(Container.DataItem,"ReviewId")%>' Text="Edit" runat="server" CssClass="Button"  />                        
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnChangeStatus" CommandName="Status"  CommandArgument ='<%#DataBinder.Eval(Container.DataItem,"ReviewId")%>' Text="Change Status" runat="server" CssClass="Button" Width="110px"  />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" CommandName="Delete"  CommandArgument ='<%#DataBinder.Eval(Container.DataItem,"ReviewId")%>' Text="Delete" runat="server" CssClass="Button" Width="50px"  />
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
    </div>
</asp:Content>