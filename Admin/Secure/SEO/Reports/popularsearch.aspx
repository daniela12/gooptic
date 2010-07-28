<%@ Page Language="C#"  MasterPageFile="~/Admin/Themes/Standard/Report.master" AutoEventWireup="true" CodeFile="popularsearch.aspx.cs" Inherits="Admin_Secure_SEO_Reports_SEOReport" %>
<%@ Register TagPrefix="uc1" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
<asp:ScriptManager id="ScriptManager" runat="server"></asp:ScriptManager>
<div class="Form">
<table width="100%">
    <tr>
        <td>
            <h1>Storefront Report</h1>
            <div><uc1:spacer id="Spacer2" SpacerHeight="5" SpacerWidth="3" runat="server"></uc1:spacer></div>
            <asp:Panel ID="pnlCustom" runat="server">
            <table>            
            <tr>             
                <td><div class="ReportStyle">Get searches for the last</div></td>                         
                <td> 
                <div class="ReportStyle">
                <asp:DropDownList ID="ListOrderStatus" runat="server">
                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                <asp:ListItem Value="1" Text="Day"></asp:ListItem>
                <asp:ListItem Value="2" Text="Week"></asp:ListItem>
                <asp:ListItem Value="3" Text="Month"></asp:ListItem>
                <asp:ListItem Value="4" Text="Quarter"></asp:ListItem>
                <asp:ListItem Value="5" Text="Year"></asp:ListItem>
                </asp:DropDownList>                
                </div>
                </td>
            </tr>          
            <tr>  
                <td>&nbsp;</td>             
                <td><asp:Button ID="btnOrderFilter" runat="server" CssClass="Button" OnClick="btnOrderFilter_Click" Text="Get Details" ValidationGroup="grpReports" />
                    <asp:Button ID="btnClear" runat="server" CausesValidation="False" CssClass="Button" OnClick="btnClear_Click" Text="Clear" />
                </td>
            </tr>
            </table>
            </asp:Panel>
        </td>
        <td valign="top">
            <div align="right"><asp:Button CausesValidation="false" ID="btnBack" CssClass="Button" runat="server" Text="<< Previous Page" OnClick="btnBack_Click" /></div>            
        </td>
       </tr>
    </table> 
    <div><uc1:spacer id="Spacer3" SpacerHeight="15" SpacerWidth="3" runat="server"></uc1:spacer></div>
    <div><asp:Label ID="lblErrorMsg" CssClass="Error" runat="server" EnableViewState="false"></asp:Label> </div> 
    <rsweb:ReportViewer ID="objReportViewer" runat="server" Width="100%" Font-Names="Verdana" Font-Size="8pt">   
        <LocalReport DisplayName="Report" ReportPath="popularsearch.rdlc">               
        </LocalReport>
    </rsweb:ReportViewer>
    <div><uc1:spacer id="Spacer1" SpacerHeight="10" SpacerWidth="3" runat="server"></uc1:spacer></div>   
</div>
</asp:Content>