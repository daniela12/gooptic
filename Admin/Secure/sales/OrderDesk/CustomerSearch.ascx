<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerSearch.ascx.cs" Inherits="Themes_Default_OrderDesk_CustomerSearch" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<div class="Form">
<h1>Customer Search</h1>   
    <asp:UpdatePanel ID="UpdatePnlSearch" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
             <asp:Panel CssClass="CustomerSearch" ID="ui_pnlSearchParams" runat="server" DefaultButton="ui_Search">
                <div class="HintStyle">Enter Zip, Last Name, Company Name, UserID or Order ID below and click on Search to find a Customer.</div>
                <table width="70%" cellpadding="0" cellspacing="0" class="SearchBox">
                    <tr class="Row">
                        <td>
                            <div class="FieldStyle">First Name</div>
                            <div class="ValueStyle"><asp:TextBox ID="FirstName" runat="server" ValidationGroup="groupSearch"></asp:TextBox></div>
                        </td>
                        <td class="Row">
                            <div class="FieldStyle">Last Name</div>
                            <div class="ValueStyle"><asp:TextBox ID="LastName" runat="server" ValidationGroup="groupSearch"></asp:TextBox></div>
                        </td>
                        <td class="Row">
                            <div class="FieldStyle">Company Name</div>
                            <div class="ValueStyle"><asp:TextBox ID="CompanyName" runat="server" ValidationGroup="groupSearch"></asp:TextBox></div>
                        </td>                        
                    </tr>
                    <tr class="Row">
                        <td>
                            <div class="FieldStyle">Zip</div>
                            <div class="ValueStyle"><asp:TextBox ID="ZipCode" runat="server" ValidationGroup="groupSearch"></asp:TextBox></div>
                        </td>
                        <td class="Row">
                            <div class="FieldStyle">User ID</div>
                            <div class="ValueStyle"><asp:TextBox ID="UserID" runat="server" ValidationGroup="groupSearch"></asp:TextBox></div>
                        </td>
                        <td class="Row">
                            <div class="FieldStyle">Order ID</div>
                            <div class="ValueStyle"><asp:TextBox ID="OrderID" runat="server" ValidationGroup="groupSearch"></asp:TextBox>
                            <asp:RegularExpressionValidator ValidationGroup="groupSearch" ValidationExpression="^\d+$" ID="val1" ControlToValidate="OrderID" ErrorMessage="*invalid" runat="server" Display="Dynamic" CssClass="Error" SetFocusOnError="true"></asp:RegularExpressionValidator></div>
                        </td>                        
                    </tr>
                    <tr>
                        <td colspan="2"><asp:Label CssClass="Error" ID="lblSearhError" runat="server" EnableViewState="false" /> </td>
                        <td class="ValueStyle">
                            <div><asp:Button ID="ui_Search" ValidationGroup="groupSearch" CssClass="Button" runat="server" Text="Search" OnClick="ui_Search_Click"  /></div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>    
    
    <asp:Panel ID="pnlSelectUsers" runat="server" Width="100%">
        <asp:UpdatePanel ID="updatePnlFoundUsers" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <h4>Select Users</h4>
                    <div class="HintStyle">Click on the "Select" link in the first column to select a customer.</div>
                    <asp:GridView ID="ui_FoundUsers" CellPadding="0" CssClass="Grid" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="ui_FoundUsers_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="ui_FoundUsers_PageIndexChanging" Width="100%">
                        <Columns>                         
                            <asp:CommandField ShowSelectButton="true" ValidationGroup="groupSearch" ButtonType="Link" SelectText="Select" />
                            <asp:TemplateField HeaderText="Login">
                                <ItemTemplate>
                                    <asp:Label ID="ui_CustomerIdSelect" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AccountID") %>'></asp:Label></a>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:BoundField HeaderText="Last Name" DataField="BillingLastName" />
                            <asp:BoundField HeaderText="First Name" DataField="BillingFirstName" />
                            <asp:BoundField HeaderText="Company" DataField="BillingCompanyName"  />
                            <asp:TemplateField HeaderText="Address">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "BillingStreet") + " " + DataBinder.Eval(Container.DataItem, "BillingStreet1")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="City" DataField="BillingCity"  />
                            <asp:BoundField HeaderText="St" DataField="BillingStateCode"  />
                            <asp:BoundField HeaderText="Zip" DataField="BillingPostalCode"  />
                            <asp:BoundField HeaderText="Phone" DataField="BillingPhoneNumber"  />
                             <asp:TemplateField HeaderText="User ID">
                                <ItemTemplate>
                                    <%# GetLoginName(DataBinder.Eval(Container.DataItem, "UserID"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle CssClass="EditRowStyle" />
                        <FooterStyle CssClass="FooterStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                    </asp:GridView>                    
                </div>
            </ContentTemplate>             
        </asp:UpdatePanel>
        <div>&nbsp;</div>
        <div align="right"><asp:Button ID="btnClose" runat="server" CausesValidation="false" CssClass="Button" Text="Close" Width="50px" OnClick="btnClose_Click" /></div>
    </asp:Panel>
    <!--During Update Process -->
    <asp:UpdateProgress ID="UpdateProgressMozilla" runat="server" DisplayAfter="0" DynamicLayout="true" Visible="false">
    <ProgressTemplate>                                    
         <asp:Panel ID="pnlOverlay" CssClass="overlay" runat="server">
            <asp:Panel ID="pnlLoader" CssClass="loader" runat="server">
                Loading...<img id="Img1" align="absmiddle" src="~/Images/buttons/loading.gif" runat="server" />
            </asp:Panel>
        </asp:Panel>
    </ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:UpdateProgress ID="UpdateProgressIE" runat="server" DisplayAfter="0" DynamicLayout="true" Visible="false">                                
        <ProgressTemplate>
            <div id="updateProgress">
                <iframe frameborder="0" src="about:blank" style="border:0px;position:absolute;z-index:9;left:0px;top:0px;width:expression(this.offsetParent.scrollWidth);height:expression(this.offsetParent.scrollHeight);filter:progid:DXImageTransform.Microsoft.Alpha(Opacity=75, FinishOpacity=0, Style=0, StartX=0, FinishX=100, StartY=0, FinishY=100);"></iframe>
                <div style="position:absolute;z-index:10;left:expression((this.offsetParent.clientWidth/2)-(this.clientWidth/2)+this.offsetParent.scrollLeft);top:expression((this.offsetParent.clientHeight/2)-(this.clientHeight/2)+this.offsetParent.scrollTop);"><div>Loading...<img id="Img1" align="absmiddle" src="~/Images/buttons/loading.gif" runat="server" /></div></div>
            </div>
        </ProgressTemplate>                                                             
    </asp:UpdateProgress>
</div>
