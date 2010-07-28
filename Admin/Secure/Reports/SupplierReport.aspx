<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/Report.master" AutoEventWireup="true" CodeFile="SupplierReport.aspx.cs" Inherits="Admin_Secure_Reports_SupplierReport" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <asp:ScriptManager id="ScriptManager" runat="server"></asp:ScriptManager>
<div class="Form">
    <table width="100%">
    <tr>
        <td>
            <h1>Storefront Report</h1>
            <div><uc1:spacer id="Spacer2" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>                     
            <table width="99%">
               <tr>
                <td><div class="FieldStyle">Begin Date</div></td>
                <td><div class="FieldStyle">End Date</div></td>
                <td><div class="FieldStyle">Supplier Name</div></td>               
            </tr>            
            <tr>
                <td>
                    <div class="ValueStyle">
                    <asp:TextBox id="txtStartDate" Text='' runat="server" />&nbsp;<asp:ImageButton ID="imgbtnStartDt" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/icons/SmallCalendar.gif" /><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Begin date" ControlToValidate="txtStartDate" ValidationGroup="grpReports" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator></div>                                                           
                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" Enabled="true" PopupButtonID="imgbtnStartDt" runat="server" TargetControlID="txtStartDate"></ajaxToolKit:CalendarExtender></td>                 
                <td>
                    <div class="ValueStyle">
                    <asp:TextBox id="txtEndDate" Text='' runat="server" />&nbsp;<asp:ImageButton ID="ImgbtnEndDt" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/icons/SmallCalendar.gif" /><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate" ErrorMessage="Enter End date" ValidationGroup="grpReports" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator></div>                          
                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" Enabled="true" PopupButtonID="ImgbtnEndDt" runat="server" TargetControlID="txtEndDate"></ajaxToolKit:CalendarExtender>
                </td>             
                <td>
                    <div class="ReportStyle"><asp:DropDownList ID="ddlSupplier" runat="server" /></div>
                </td>
            </tr> 
            <tr>              
              <td colspan ="3">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtStartDate"
                CssClass="Error" Display="Dynamic" ErrorMessage="* Enter Valid Start Date in MM/DD/YYYY format"
                ValidationExpression="((^(10|12|0?[13578])([/])(3[01]|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(11|0?[469])([/])(30|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(2[0-8]|1[0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(29)([/])([2468][048]00)$)|(^(0?2)([/])(29)([/])([3579][26]00)$)|(^(0?2)([/])(29)([/])([1][89][0][48])$)|(^(0?2)([/])(29)([/])([2-9][0-9][0][48])$)|(^(0?2)([/])(29)([/])([1][89][2468][048])$)|(^(0?2)([/])(29)([/])([2-9][0-9][2468][048])$)|(^(0?2)([/])(29)([/])([1][89][13579][26])$)|(^(0?2)([/])(29)([/])([2-9][0-9][13579][26])$))" ValidationGroup="grpReports"></asp:RegularExpressionValidator><br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEndDate"
                CssClass="Error" Display="Dynamic" ErrorMessage="* Enter Valid End Date in MM/DD/YYYY format"
                ValidationExpression="((^(10|12|0?[13578])([/])(3[01]|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(11|0?[469])([/])(30|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(2[0-8]|1[0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(29)([/])([2468][048]00)$)|(^(0?2)([/])(29)([/])([3579][26]00)$)|(^(0?2)([/])(29)([/])([1][89][0][48])$)|(^(0?2)([/])(29)([/])([2-9][0-9][0][48])$)|(^(0?2)([/])(29)([/])([1][89][2468][048])$)|(^(0?2)([/])(29)([/])([2-9][0-9][2468][048])$)|(^(0?2)([/])(29)([/])([1][89][13579][26])$)|(^(0?2)([/])(29)([/])([2-9][0-9][13579][26])$))" ValidationGroup="grpReports"></asp:RegularExpressionValidator><br />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate" ValidationGroup="grpReports"
                 ControlToValidate="txtEndDate" CssClass="Error" Display="Dynamic" ErrorMessage="* End Date must be greater than Begin date"
                 Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
              </td>
            </tr>            
            <tr>
                <td></td>  
                <td></td>             
                <td><asp:Button ID="btnOrderFilter" runat="server" CssClass="Button" OnClick="btnOrderFilter_Click" Text="Get Details" ValidationGroup="grpReports" />
                    <asp:Button ID="btnClear" runat="server" CausesValidation="False" CssClass="Button" OnClick="btnClear_Click" Text="Clear" />
                </td>
            </tr>                      
            </table>
            </td>
            <td>&nbsp;</td>
                <td valign="top">
                    <div align="right"><asp:Button CausesValidation="false" ID="btnBack" CssClass="Button" runat="server" Text="<< See More Reports" OnClick="btnBack_Click" /></div>            
                </td>
            </tr>
                  
    </table>


    <div><uc1:spacer id="Spacer3" SpacerHeight="15" SpacerWidth="3" runat="server"></uc1:spacer></div>
    <div><asp:Label ID="lblErrorMsg" CssClass="Error" runat="server" EnableViewState="false"></asp:Label></div> 
    <rsweb:ReportViewer ID="objReportViewer" runat="server" Width="100%" Font-Names="Verdana" Font-Size="8pt">
      <LocalReport DisplayName="Report" ReportPath="SupplierReport.rdlc">               
        </LocalReport>
    </rsweb:ReportViewer>
    <div><uc1:spacer id="Spacer1" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>
  </div>
</asp:Content>