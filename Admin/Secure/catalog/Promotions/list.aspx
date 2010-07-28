<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master"  AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="Admin_Secure_catalog_Promotions_list" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
<div class="Form">
    <asp:ScriptManager id="ScriptManager" runat="server"></asp:ScriptManager>
    <table cellspacing="0" cellpadding="0" width="100%">
		    <tr>
			    <td><h1>Promotions</h1></td>
			    <td align="right" style="width: 371px"><asp:button CssClass="Button" id="btnAddCoupon" CausesValidation="False" Text="Add a New Promotion" Runat="server" onclick="btnAddCoupon_Click"></asp:button></td>
		    </tr>
    </table>

    <h4>Search Promotions</h4>    
    <asp:Panel ID="Test" DefaultButton="btnSearch" runat="server" >  
        <table border="0" width="60%">
            <tr>
                <td>
                     <div class="FieldStyle">Name</div>
                     <div class="ValueStyle"><asp:TextBox ID="txtName" runat="server" ValidationGroup="grpSearch"></asp:TextBox></div>
                </td>
                <td>
                     <div class="FieldStyle">Amount</div>
                     <div class="ValueStyle"><asp:TextBox ID="txtAmount" runat="server" ValidationGroup="grpSearch"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="FieldStyle">Coupon Code</div>
                    <div class="ValueStyle"><asp:TextBox ID="CouponCode" runat="server" ValidationGroup="grpSearch"></asp:TextBox></div>
                </td>
                <td>
                    <div class="FieldStyle">Discount Type</div>
                    <div class="ValueStyle"><asp:DropDownList ID="ddlDiscountTypes" runat="server" /></div>
                </td>
            </tr>
            <tr>                
                <td>
                    <div class="FieldStyle">Begin Date</div>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtStartDate"
                        CssClass="Error" Display="Dynamic" ErrorMessage="* Enter Valid Start Date in MM/DD/YYYY format"
                        ValidationExpression="((^(10|12|0?[13578])([/])(3[01]|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(11|0?[469])([/])(30|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(2[0-8]|1[0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(29)([/])([2468][048]00)$)|(^(0?2)([/])(29)([/])([3579][26]00)$)|(^(0?2)([/])(29)([/])([1][89][0][48])$)|(^(0?2)([/])(29)([/])([2-9][0-9][0][48])$)|(^(0?2)([/])(29)([/])([1][89][2468][048])$)|(^(0?2)([/])(29)([/])([2-9][0-9][2468][048])$)|(^(0?2)([/])(29)([/])([1][89][13579][26])$)|(^(0?2)([/])(29)([/])([2-9][0-9][13579][26])$))" ValidationGroup="grpSearch"></asp:RegularExpressionValidator>
                    <div class="ValueStyle">
                        <asp:TextBox ID="txtStartDate" runat="server" ValidationGroup="grpSearch"></asp:TextBox>
                        <asp:ImageButton ID="imgbtnStartDt" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/icons/SmallCalendar.gif" />
                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" Enabled="true" PopupButtonID="imgbtnStartDt" runat="server" TargetControlID="txtStartDate"></ajaxToolKit:CalendarExtender>                        
                    </div>
                </td>                     
                <td>
                     <div class="FieldStyle">End Date</div>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEndDate"
                                CssClass="Error" Display="Dynamic" ErrorMessage="* Enter Valid End Date in MM/DD/YYYY format"
                                ValidationExpression="((^(10|12|0?[13578])([/])(3[01]|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(11|0?[469])([/])(30|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(2[0-8]|1[0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(29)([/])([2468][048]00)$)|(^(0?2)([/])(29)([/])([3579][26]00)$)|(^(0?2)([/])(29)([/])([1][89][0][48])$)|(^(0?2)([/])(29)([/])([2-9][0-9][0][48])$)|(^(0?2)([/])(29)([/])([1][89][2468][048])$)|(^(0?2)([/])(29)([/])([2-9][0-9][2468][048])$)|(^(0?2)([/])(29)([/])([1][89][13579][26])$)|(^(0?2)([/])(29)([/])([2-9][0-9][13579][26])$))" ValidationGroup="grpSearch"></asp:RegularExpressionValidator>
                     <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" CssClass="Error" ErrorMessage="End date should be greater than Begin date" ValidationGroup="grpSearch"  SetFocusOnError="True" Display="Dynamic" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                     <div class="ValueStyle">                     
                            <asp:TextBox ID="txtEndDate" runat="server" ValidationGroup="grpSearch"></asp:TextBox>
                            <asp:ImageButton ID="ImgbtnEndDt" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/icons/SmallCalendar.gif" />                         
                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" Enabled="true" PopupButtonID="ImgbtnEndDt" runat="server" TargetControlID="txtEndDate"></ajaxToolKit:CalendarExtender>                        
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
    
    <h4>Promotions List </h4>
    <asp:GridView ID="uxGrid" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="uxGrid_PageIndexChanging" CaptionAlign="Left" OnRowCommand="uxGrid_RowCommand" Width="100%" PageSize="25" AllowSorting="True" EmptyDataText="No promotions available to display.">
        <Columns>
            <asp:BoundField DataField="PromotionID" HeaderText="ID" />
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <a href='add.aspx?ItemID=<%# DataBinder.Eval(Container.DataItem, "PromotionID").ToString()%>'><%# DataBinder.Eval(Container.DataItem, "Name").ToString()%></a>
                </ItemTemplate> 
            </asp:TemplateField>    
              <asp:TemplateField HeaderText="Amount">
                <ItemTemplate>
                    <div align="right"><%# (DataBinder.Eval(Container.DataItem, "Discount"))%></div>
                </ItemTemplate> 
            </asp:TemplateField>              
            <asp:TemplateField HeaderText="Discount Type Name">
                <ItemTemplate>
                    <%# DiscountTypeName(DataBinder.Eval(Container.DataItem, "DiscountTypeID"))%>
                </ItemTemplate> 
            </asp:TemplateField>  
            <asp:TemplateField HeaderText="Display Order">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "DisplayOrder")%>
                </ItemTemplate> 
            </asp:TemplateField>                    
            <asp:BoundField DataField="StartDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Start date" HtmlEncode="False"/>
            <asp:BoundField DataField="EndDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="End Date" HtmlEncode="False"/>
            <asp:BoundField DataField="CouponCode" HeaderText="Coupon Code" />            
            <asp:ButtonField CommandName="Edit" Text="Edit" ButtonType="Button">
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