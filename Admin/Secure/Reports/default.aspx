<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Admin_Secure_Reports_default" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <h1>Znode Storefront Reports</h1>
    <p>Use the links on this page to view various storefront reports. You can also write your own custom reports using the templates provided in your web project.</p>
    
    <div class="LandingPage">
        <hr />  
        
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                    <div style="margin-right:100px;">
                    <div class="Shortcut"><a id="A3" href="~/admin/secure/reports/ReportList.aspx?filter=12" runat="server">Orders</a></div>
                    <div class="Shortcut"><a id="A12" href="~/admin/secure/reports/ReportList.aspx?filter=21" runat="server">Accounts</a></div> 
                    <div class="Shortcut"><a id="A21" href="~/admin/secure/reports/InventoryReports.aspx?filter=20" runat="server">Best Sellers</a></div>  
                    <div class="Shortcut"><a id="A20" href="~/admin/secure/reports/InventoryReports.aspx?filter=19" runat="server">Service Requests</a></div>  
                    <div class="Shortcut"><a id="A15" href="~/admin/secure/reports/InventoryReports.aspx?filter=14" runat="server">Email Opt-In Customers</a></div>
                    <div class="Shortcut"><a id="A19" href="~/admin/secure/reports/InventoryReports.aspx?filter=18" runat="server">Inventory Re-Order</a></div> 
                    <div class="Shortcut"><a id="A16" href="~/admin/secure/reports/InventoryReports.aspx?filter=15" runat="server">Most Frequent Customers</a></div>
                    <div class="Shortcut"><a id="A17" href="~/admin/secure/reports/InventoryReports.aspx?filter=16" runat="server">Top Spending Customers</a></div> 
                    </div>
                </td>
                <td valign="top">
                    <div>
                    <div class="Shortcut"><a id="A18" href="~/admin/secure/reports/InventoryReports.aspx?filter=17" runat="server">Top Earning Products</a></div> 
                    <div class="Shortcut"><a id="A14" href="~/admin/secure/reports/InventoryReports.aspx?filter=13" runat="server">Order Pick List</a></div>   
                    <div class="Shortcut"><a id="A1" href="~/admin/secure/reports/ActivityLogReport.aspx?filter=22" runat="server">Activity Log</a></div> 
                    <div class="Shortcut"><a id="A2" href="~/admin/secure/reports/InventoryReports.aspx?filter=24" runat="server">Coupon Usage</a></div> 
                    <div class="Shortcut"><a id="A4" href="~/admin/secure/reports/TaxReport.aspx?filter=25" runat="server">Sales Tax</a></div> 
                    <div class="Shortcut"><a id="A5" href="~/admin/secure/reports/InventoryReports.aspx?filter=26" runat="server">Affiliate Orders</a></div> 
                    <div class="Shortcut"><a id="A6" href="~/admin/secure/reports/SupplierReport.aspx?filter=27" runat="server">Supplier List</a></div> 
                    </div>
                </td>
            </tr>        
        </table>
         
        
    </div>    
</asp:Content>
