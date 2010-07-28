<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DownloadTracking.ascx.cs" Inherits="PlugIns_DataManager_DownloadTracking" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<div class="Form">    
    <h1>Download Tracking data</h1>
    <asp:ScriptManager id="ScriptManager" runat="server"></asp:ScriptManager>
    <div><ZNode:spacer id="Spacer1" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>    
    <div><ZNode:spacer id="Spacer2" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>  
    <div>
    <table width="99%">            
        <tr>
        <td><div class="FieldStyle">Begin Date</div>
        <div class="ValueStyle">
             <asp:TextBox id="txtStartDate" Text='' runat="server" />&nbsp;<asp:ImageButton ID="imgbtnStartDt" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/icons/SmallCalendar.gif" /><br />
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Begin date" ControlToValidate="txtStartDate" ValidationGroup="GroupTracking" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator></div>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtStartDate"
             CssClass="Error" Display="Dynamic" ErrorMessage="* Enter Valid Date in MM/DD/YYYY format"
             ValidationExpression="((^(10|12|0?[13578])([/])(3[01]|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(11|0?[469])([/])(30|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(2[0-8]|1[0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(29)([/])([2468][048]00)$)|(^(0?2)([/])(29)([/])([3579][26]00)$)|(^(0?2)([/])(29)([/])([1][89][0][48])$)|(^(0?2)([/])(29)([/])([2-9][0-9][0][48])$)|(^(0?2)([/])(29)([/])([1][89][2468][048])$)|(^(0?2)([/])(29)([/])([2-9][0-9][2468][048])$)|(^(0?2)([/])(29)([/])([1][89][13579][26])$)|(^(0?2)([/])(29)([/])([2-9][0-9][13579][26])$))" ValidationGroup="GroupTracking"></asp:RegularExpressionValidator>                                        
             <ajaxToolKit:CalendarExtender ID="CalendarExtender1" Enabled="true" PopupButtonID="imgbtnStartDt" runat="server" TargetControlID="txtStartDate"></ajaxToolKit:CalendarExtender>
        </td>        
        <td>
            <div class="FieldStyle">End Date</div>
            <div class="ValueStyle">
                <asp:TextBox id="txtEndDate" Text='' runat="server" />&nbsp;<asp:ImageButton ID="ImgbtnEndDt" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/icons/SmallCalendar.gif" /><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate" ErrorMessage="Enter End date" ValidationGroup="GroupTracking" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
            </div> 
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEndDate"
            CssClass="Error" Display="Dynamic" ErrorMessage="* Enter Valid Date in MM/DD/YYYY format"
            ValidationExpression="((^(10|12|0?[13578])([/])(3[01]|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(11|0?[469])([/])(30|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(2[0-8]|1[0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(29)([/])([2468][048]00)$)|(^(0?2)([/])(29)([/])([3579][26]00)$)|(^(0?2)([/])(29)([/])([1][89][0][48])$)|(^(0?2)([/])(29)([/])([2-9][0-9][0][48])$)|(^(0?2)([/])(29)([/])([1][89][2468][048])$)|(^(0?2)([/])(29)([/])([2-9][0-9][2468][048])$)|(^(0?2)([/])(29)([/])([1][89][13579][26])$)|(^(0?2)([/])(29)([/])([2-9][0-9][13579][26])$))" ValidationGroup="GroupTracking"></asp:RegularExpressionValidator>                    
            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" Enabled="true" PopupButtonID="ImgbtnEndDt" runat="server" TargetControlID="txtEndDate"></ajaxToolKit:CalendarExtender>
            <div class="ValueStyle">
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                ControlToValidate="txtEndDate" CssClass="Error" Display="Dynamic" ErrorMessage=" End Date must be greater than Begin date"
                Operator="GreaterThanEqual" Type="Date" ValidationGroup="GroupTracking">
                </asp:CompareValidator>
            </div>
        </td>
        </tr>  
    </table>
    </div>
 
    <div class="FieldStyle">File Type</div>
        <div class="ValueStyle">
        <asp:DropDownList ID="ddlFileSaveType" runat="server">
            <asp:ListItem Text="Microsoft Excel (.xls)" Value=".xls"></asp:ListItem>
            <asp:ListItem Text="Comma Separated Values (.csv)" Value=".csv" Selected="True"></asp:ListItem>
        </asp:DropDownList>
        </div>        
        <div><ZNode:spacer id="Spacer3" SpacerHeight="1" SpacerWidth="3" runat="server"></ZNode:spacer></div>
        <div class="Error"><asp:Literal ID="ltrlError" runat="server"></asp:Literal></div>       
        <div><ZNode:spacer id="Spacer4" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>

    <div>
        <asp:Button ID="btnSubmit" runat="server" CausesValidation="True" class="Button"  ValidationGroup="GroupTracking" OnClick="btnSubmit_Click" Text="Download Tracking Data" />
        <asp:Button ID="btnCancel" runat="server" CausesValidation="False" class="Button"  OnClick="btnCancel_Click" Text="Cancel" />
    </div>
</div>