<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/content.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Secure_Default" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">   
    
    <h1>Znode Storefront Enterprise Edition&#0153; - <%=GetProductVersion()%></h1>
    <div align="left" style="font-size:9pt;font-family: verdana; vertical-align:baseline; margin-bottom:20px;" valign="middle">
    <% =ConcatName()%>
    </div>
    
    
    <div class="Dashboard">
        <div><img src="~/images/clear.gif" runat="server" width="10" height="20" alt="" /></div>
        <table class="DashBoardItems">
            <tr>                
                <td><a href="~/admin/secure/catalogmanager.aspx" runat="server" style="text-decoration:none;"><img src="~/images/icons/large/catalog.gif" runat="server" border="0" alt=""/></a></td>
                <td><a id="A11" href="~/admin/secure/ordermanager.aspx" runat="server" style="text-decoration:none;"><img id="Img11" src="~/images/icons/large/orders.gif" runat="server" border="0" alt=""/></a></td>                              
                <td><a id="A13" href="~/admin/secure/DataManager/default.aspx" runat="server" style="text-decoration:none;"><img id="Img14" src="~/images/icons/large/settings.gif" runat="server" border="0" alt=""/></a></td>
                <td><a id="A12" href="~/admin/secure/Reports/default.aspx" runat="server" style="text-decoration:none;"><img id="Img13" src="~/images/icons/large/content.gif" runat="server" border="0" alt=""/></a></td> 
                <td><a id="A14" href="http://help.znode.com" target="_blank" style="text-decoration:none;"><img id="Img15" src="~/images/icons/large/help.gif" runat="server" border="0" alt="" /></a></td>
            </tr>
            <tr>                
                <td valign="top"><a id="A21" href="~/admin/secure/catalogmanager.aspx" runat="server" style="text-decoration:none;"><span style="text-decoration:underline;">Product Catalog</span></a></td>
                <td valign="top"><a id="A22" href="~/admin/secure/ordermanager.aspx" runat="server" style="text-decoration:none;"><span style="text-decoration:underline;">Manage Sales</span></a></td>                              
                <td valign="top"><a id="A23" href="~/admin/secure/DataManager/default.aspx" runat="server" style="text-decoration:none;"><span style="text-decoration:underline;">Upload/ Download Data</span></a></td>
                <td valign="top"><a id="A24" href="~/admin/secure/Reports/default.aspx" runat="server" style="text-decoration:none;"><span style="text-decoration:underline;">View Reports</span></a></td> 
                <td valign="top"><a id="A25" href="http://help.znode.com" target="_blank" style="text-decoration:none;"><span style="text-decoration:underline;">Help & Support</span></a></td>
            </tr>
        </table>
        <div><img id="Img16" src="~/images/clear.gif" runat="server" width="10" height="20" alt="" /></div>
        <table cellpadding="0" cellspacing="0">
            <tr>
         
                
                <td valign="top">
                   <div class="Box">                        
                        <div class="Title"><span class="Caption">STOREFRONT METRICS</span></div>
                        <asp:Panel runat="server" ID="Metrics">
                            <div class="Inner">       
                                <div class="MetricItem"><span class="MetricLabel">Net Sales YTD:</span><span class="MetricValue"><% =dashAdmin.YTDRevenue.ToString("c")%></span></div>
                                <div class="MetricItem"><span class="MetricLabel">Net Sales MTD:</span><span class="MetricValue"><% =dashAdmin.MTDRevenue.ToString("c")%></span></div>
                                <div class="MetricItem"><span class="MetricLabel">Net Sales Today:</span><span class="MetricValue"><% =dashAdmin.TodayRevenue.ToString("c")%></span></div>                            
                                <div class="MetricItem"><span class="MetricLabel">#Total Orders: </span><span class="MetricValue"><% =dashAdmin.TotalOrders.ToString()%></span></div> 
                                <div class="MetricItem"><span class="MetricLabel">#Total Orders MTD: </span><span class="MetricValue"><% =dashAdmin.TotalOrdersMTD.ToString()%></span></div> 
                         
                                <div class="MetricItem"><span class="MetricLabel">#Total Accounts:</span><span class="MetricValue"><% =dashAdmin.TotalAccounts.ToString()%></span></div>                                  
                                <div class="MetricItem"><span class="MetricLabel">#Total Accounts MTD:</span><span class="MetricValue"><% =dashAdmin.TotalAccountsMTD.ToString()%></span></div>  
                                <div style=" margin-top:20px;"><a href="~/admin/secure/Reports/default.aspx" runat="server">See more reports >></a></div>
                            </div>
                        </asp:Panel>
                        <!-- metrics message -->
                        <asp:Panel runat="server" ID="MetricsMessage">
                            <div class="Inner"><asp:Label runat="server" ID="Message" Text="Please log in as an Admin or Executive to see Storefront Metrics."></asp:Label></div>
                        </asp:Panel>
                    </div>                
                </td>
                
                <td><div><img src="~/images/clear.gif" runat="server" width="25" height="20" alt="" /></div></td>
                
                <td valign="top">
                     <div class="Box">                        
                        <div class="Title"><span class="Caption">REPORTS</span></div>
                        <div class="Inner">                                
                            <div class="ReportItem"><a id="A1555" href="~/admin/secure/reports/ReportList.aspx?filter=12" runat="server">Storefront Orders</a></div>
                                        
                            <div class="ReportItem"><a id="A4555" href="~/admin/secure/reports/ReportList.aspx?filter=21" runat="server">Customer Accounts</a></div> 
                                              
                            <div class="ReportItem"><a id="A2155" href="~/admin/secure/reports/InventoryReports.aspx?filter=20" runat="server">Products - Best Sellers</a></div>  
                            
                            <div class="ReportItem"><a id="A2055" href="~/admin/secure/reports/InventoryReports.aspx?filter=19" runat="server">Service Requests</a></div>  
                             
                            <div class="ReportItem"><a id="A155s5" href="~/admin/secure/reports/InventoryReports.aspx?filter=14" runat="server">Email Opt-In Customers</a></div>
                            
                            <div class="ReportItem"><a id="A1955" href="~/admin/secure/reports/InventoryReports.aspx?filter=18" runat="server">Inventory Re-Order</a></div>    
                            
                            
                            <div class="ReportItem"><a id="A1dd6" href="~/admin/secure/reports/InventoryReports.aspx?filter=15" runat="server">Most Frequent Customers</a></div>                            
                             
                            <div style=" margin-top:20px; margin-left:10px;"><a id="Ass1" href="~/admin/secure/Reports/default.aspx" runat="server">See more reports >></a></div>
                        </div>         
                    </div>    
                </td>    
                
                <td><div><img src="~/images/clear.gif" runat="server" width="25" height="20" alt="" /></div></td>
                
                <td valign="top">                   
                   <div class="Box">
                        <div class="Title"><span class="Caption">ALERTS</span></div>                        
                        <div class="Inner">
                            <asp:Panel runat="server" ID="pnlAlerts" Visible="false">
                                <div class="AlertItem"><span class="MetricLabel"><a id="A1" href="~/admin/secure/sales/cases/list.aspx" runat="server">Pending Service Requests:</a></span><span class="MetricValue"><a id="A9" href="~/admin/secure/sales/cases/list.aspx" runat="server"><% =dashAdmin.TotalPendingServiceRequests.ToString()%></a></span></div>
                                <div class="AlertItem"><span class="MetricLabel"><a id="A4" href="~/admin/secure/catalog/product_reviews/default.aspx" runat="server">Reviews Pending Approval:</a></span><span class="MetricValue"><a id="A10" href="~/admin/secure/catalog/product_reviews/default.aspx" runat="server"><% =dashAdmin.TotalReviewsToApprove.ToString()%></a></span></div>
                                <div class="AlertItem"><span class="MetricLabel"><a id="A8" runat="server" href="~/admin/secure/sales/customers/list.aspx">Tracking Pending Approval:</a></span><span class="MetricValue"><a id="A20" runat="server" href="~/admin/secure/sales/customers/list.aspx"><% =dashAdmin.TotalAffiliatesToApprove.ToString()%></a></span></div>
                                <div class="AlertItem"><span class="MetricLabel"><a id="A5" href="~/admin/secure/Reports/InventoryReports.aspx?filter=18" runat="server">Low Inventory:</a></span><span class="MetricValue"><a id="A15" href="~/admin/secure/Reports/InventoryReports.aspx?filter=18" runat="server"><% =dashAdmin.TotalLowInventoryItems.ToString()%></a></span></div>
                                <div class="AlertItem"><span class="MetricLabel"><a id="A16" href="~/admin/secure/reports/ActivityLogReport.aspx?filter=22" runat="server">Failed Logins Today:</a></span><span class="MetricValue"><a id="A18" href="~/admin/secure/reports/ActivityLogReport.aspx?filter=22" runat="server"><% =dashAdmin.TotalFailedLoginsToday.ToString()%></a></span></div> 
                                <div class="AlertItem"><span class="MetricLabel"><a id="A17" href="~/admin/secure/reports/ActivityLogReport.aspx?filter=22" runat="server">Declined Transactions:</a></span><span class="MetricValue"><a id="A19" href="~/admin/secure/reports/ActivityLogReport.aspx?filter=22" runat="server"><% =dashAdmin.TotalDeclinedTransactions.ToString()%></a></span></div>
                            </asp:Panel>
                            <div class="AlertItem"><span class="MetricLabel"><a id="PasswordLink" href='<%= changePasswordPageLink %>' runat="server">Reset password in </a></span> <span class="MetricValue"><a id="PasswordLink1" href='<%= changePasswordPageLink %>' runat="server"><% =daysToExpire + " days"%></a></span></div>
                            <div class="Metric"><asp:Label runat="server" Visible="false" ID="lblAlertsMsg" Text="Please log in as an Admin or Executive to see other Storefront Alerts."></asp:Label></div>
                            <div><a id="A6" href="~/admin/secure/reports/ActivityLogReport.aspx?filter=22" runat="server">See activity log >></a></div>
                        </div>
                    </div>                    
                </td>                  
            </tr>    
        </table>
        
        <table cellpadding="0" cellspacing="0">
            <tr>
                
                <td>
                     <div class="Box">
                        <div class="Title"><span class="Caption">POPULAR SEARCH TERMS</span></div>                          
                            <div class="Inner" style="z-index:-1;">  
                                <asp:Repeater ID="rptPopularSearchKeywords" runat="server">                 
                                    <HeaderTemplate><table border="0" cellpadding="0" cellspacing="0"></HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="SearchItem"><a id="A1733" href='<%# "~/search.aspx?keyword=" + DataBinder.Eval(Container.DataItem,"Data1") %>' target="_blank" runat="server"><%# DataBinder.Eval(Container.DataItem,"Data1") %></a></div>                                    
                                    </ItemTemplate> 
                                    <FooterTemplate></table></FooterTemplate>                                               
                                </asp:Repeater>
                                <asp:Label ID="lblMessage" CssClass="Metric" runat="server" Visible="false" Text="No keyword data collected yet."></asp:Label>
                                <div style=" margin-top:10px;"><a id="A3" href="~/admin/secure/SEO/reports/popularsearch.aspx?filter=23&pagetype=dashboard" runat="server">See all searches >></a></div>
                            </div>
                    </div>                                         
                </td>
                <td><div><img id="Img2dd" src="~/images/clear.gif" runat="server" width="25" height="20" alt="" /></div></td>
                <td>
                    <div class="Box">                        
                        <div class="Title"><span class="Caption">HELP TOPICS</span></div>
                        <div class="Inner">       
                            <div class="HelpItem"><a href="http://help.znode.com/release_notes_51.htm" target="_blank">View v5.1 Release Notes</a></div>
                            <div class="HelpItem"><a href="http://help.znode.com/deploying_to_production.htm" target="_blank">Deploying to Production</a></div>
                            <div class="HelpItem"><a href="http://help.znode.com/znode_storefront_architecture.htm" target="_blank">Architectural Overview</a></div>
                            <div class="HelpItem"><a href="http://help.znode.com/quick_start_guide.htm" target="_blank">Customizing the Storefront</a></div>
                            <div class="HelpItem"><a href="http://help.znode.com/activity_logging.htm" target="_blank">Logging Activity and Events</a></div>               
                            <div class="HelpItem"><a href="http://help.znode.com/data_manager.htm" target="_blank">Bulk Data Upload/ Download</a></div>                                                       
                            <div class="HelpItem"><a href="http://help.znode.com/pci_overview.htm" target="_blank">Managing PCI Compliance</a></div>
                                        
                            <div style=" margin-top:20px; margin-left:10px;"><a id="A2" href="http://help.znode.com" target="_blank">See more Topics >></a></div>
                        </div>         
                    </div>  
                </td>  
                <td><div><img src="~/images/clear.gif" runat="server" width="25" height="20" alt="" /></div></td>
                <td>
                    <div class="Box">                        
                        <div class="Title"><span class="Caption">YOUR STORE</span></div>
                        <div class="Inner" style=" padding:0;">       
                            <iframe id="ifStore" src="<% =storefrontUrl %>" width="240px" height="200px" frameborder=0 scrolling=auto></iframe> 
                            
                            <div style=" margin-top:10px; margin-left:10px;"><a id="A7" href="~/" target="_blank" runat="server">Open in new window >></a></div>
                        </div>         
                    </div>  
                </td>                            
            </tr>        
        </table>
        
        <div style="margin-top:30px;">
            <div class="NewsItem">Get updated product information and documentation at <a href="http://www.znode.com" target="_blank">www.znode.com</a></div>
            <div class="NewsItem">Discuss issues and collaborate with other Znode Community members <a href="http://kb.znode.com" target="_blank">kb.znode.com</a></div>
        </div>
     </div>
</asp:Content>

