using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Custom;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.Framework.Business;
using Microsoft.Reporting.WebForms;

public partial class Admin_Secure_Reports_InventoryReports : System.Web.UI.Page
{

    # region Protected Member Variables
    protected int AccountID;
    protected DataSet MyDataSet = null;
    DataSet dsOrders = new DataSet();
    DataSet dsOrdersLineItems = new DataSet();
    DataSet dsCaseRequest = new DataSet();
    protected string Mode = "";
    protected string Year = "";
    #endregion  

    # region Page Load Event
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>    
    protected void Page_Load(object sender, EventArgs e)
    {      

        if (!Page.IsPostBack)
        {
            if (Request.Params["filter"] != null)
            {
                Mode = Request.Params["filter"];
            }      

            //Get Filetered Orders in DataSet
            OrderAdmin _OrderAdmin = new OrderAdmin();
            DataView dv = new DataView();

            // Mode 17 - For Top Earning Products
            // Mode 18 - For Inventory Re-Orderlevel
            // Mode 20 - For Best Sellers
            if (Mode.Equals("17") || Mode.Equals("18") || Mode.Equals("20") || Mode.Equals("26"))
            {
                dsOrdersLineItems = _OrderAdmin.ReportList(Mode, Year, "");
                dv = new DataView(dsOrdersLineItems.Tables[0]);
            }
            else if (Mode.Equals("19")) // Mode 19 - Customer Feedback
            {
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
                dsCaseRequest = _OrderAdmin.ReportList(Mode, Year, "");
                dv = new DataView(dsOrders.Tables[0]);
            }
            else
            {
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
                dsOrdersLineItems = _OrderAdmin.GetAllOrderLineItems().ToDataSet(false);
                dv = new DataView(dsOrders.Tables[0]);
            }

            if (Request.Params["filter"] != null)
            {
                Mode = Request.Params["filter"];

                // Order Pick List
                if (Mode.Equals("13"))
                {
                    this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/Inventory.rdlc";                    
                }
                // Email Opt-In Customers
                if (Mode.Equals("14"))
                {
                    this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/EmailOptIn.rdlc";                    
                }
                // Most Frequent Customers
                if (Mode.Equals("15"))
                {
                    pnlCustom.Visible = true;
                    this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/FrequentCustomer.rdlc";                    
                }
                // Highest Dollar Customers
                if (Mode.Equals("16"))
                {
                    pnlCustom.Visible = true;
                    this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/VolumeCustomer.rdlc";
                }
                // Most Popular Product
                if (Mode.Equals("17"))
                {
                    pnlCustom.Visible = true;
                    this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/PopularProduct.rdlc";                    
                }
                // Inventory Re-Order
                if (Mode.Equals("18"))
                {                    
                    this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/ReOrder.rdlc";                    
                }
                // Customer Feedback
                if (Mode.Equals("19"))
                {
                    this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/Feedback.rdlc";                    
                }
                // Best Sellers
                if (Mode.Equals("20"))
                {
                    pnlCustom.Visible = true;
                    this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/BestSellers.rdlc";                   
                }
                // Coupon Usage
                if (Mode.Equals("24"))
                {
                    pnlCustom.Visible = true;
                    this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/CouponUsage.rdlc";
                }
                // Affiliate Orders
                if (Mode.Equals("26"))
                {
                    pnlCustom.Visible = true;
                    this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/Affiliate.rdlc";
                }
            }
          
            if (dv.ToTable().Rows.Count == 0)
            {
                lblErrorMsg.Text = "No records found";
                objReportViewer.Visible = false;
                pnlCustom.Visible = false;
                return;
            }

            objReportViewer.LocalReport.DataSources.Clear();
            ReportParameter param1 = new ReportParameter("CurrentLanguage", System.Globalization.CultureInfo.CurrentCulture.Name);
            objReportViewer.LocalReport.SetParameters(new ReportParameter[] { param1 });
            this.objReportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

            if (Mode.Equals("24"))
            {
                objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeCouponDataSet_ZNodeCoupon", dv));                
            }
            else if (!Mode.Equals("19"))
            {   
                objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeOrderDataSet_ZNodeOrder", dv));
                objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeAccountDataSet_ZNodeAccount", dv));
                objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeOrderLineItems_ZNodeOrderLineItem", dsOrdersLineItems.Tables[0]));
                objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeAffiliate_ZNodeAffiliate", dv));
            }
            else
            {
                objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeTitleDataSet_ZNodeTitle", dv));
                objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeCaseRequestDataSet_ZNodeCaseRequest", dsCaseRequest.Tables[0]));
            }
            objReportViewer.LocalReport.Refresh();
        }
    }
    #endregion   

    # region Events
    /// <summary>
    /// Report Viewer - Sub Report processing event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
    {
        if (!Mode.Equals("19"))
        {
            e.DataSources.Add(new ReportDataSource("ZNodeOrderLineItems_ZNodeOrderLineItem", dsOrdersLineItems.Tables[0]));
        }      
        else
        {           
            e.DataSources.Add(new ReportDataSource("ZNodeCaseRequestDataSet_ZNodeCaseRequest", dsCaseRequest.Tables[0]));
        }
    }

    /// <summary>
    /// Back Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/Secure/Reports/default.aspx");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOrderFilter_Click(object sender, EventArgs e)
    {
        objReportViewer.Visible = true;
        if (Request.Params["filter"] != null)
        {
            Mode = Request.Params["filter"];
        }      
        //Get Filetered Orders in DataSet
        OrderAdmin _OrderAdmin = new OrderAdmin();
        dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
        dsOrdersLineItems = _OrderAdmin.GetAllOrderLineItems().ToDataSet(false);
        DataView dv = new DataView(dsOrders.Tables[0]);
        DataView dv1 = new DataView();
        pnlCustom.Visible = true;

        string Status = ListOrderStatus.SelectedItem.Text;
        if (Mode == "15")
        {
            this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/FrequentCustomer.rdlc";

            if (Status == "Day")
            {
                Mode = "151";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Week")
            {
                Mode = "152";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Month")
            {
                Mode = "153";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Quarter")
            {
                Mode = "154";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Year")
            {
                Mode = "155";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            dsOrdersLineItems = _OrderAdmin.GetAllOrderLineItems().ToDataSet(false);
            dv1 = new DataView(dsOrders.Tables[0]);

        }
        else if (Mode == "16")
        {
            this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/FrequentCustomer.rdlc";

            if (Status == "Day")
            {
                Mode = "161";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Week")
            {
                Mode = "162";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Month")
            {
                Mode = "163";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Quarter")
            {
                Mode = "164";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Year")
            {
                Mode = "165";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            dsOrdersLineItems = _OrderAdmin.GetAllOrderLineItems().ToDataSet(false);
            dv1 = new DataView(dsOrders.Tables[0]);

        }
        else if (Mode == "17")
        {
            this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/PopularProduct.rdlc";

            if (Status == "Day")
            {
                Mode = "171";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Week")
            {
                Mode = "172";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Month")
            {
                Mode = "173";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Quarter")
            {
                Mode = "174";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Year")
            {
                Mode = "175";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            dsOrdersLineItems = _OrderAdmin.ReportList(Mode, Year, "");
            dv1 = new DataView(dsOrdersLineItems.Tables[0]);
        }

        else if (Mode == "20")
        {
            this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/BestSellers.rdlc";

            if (Status == "Day")
            {
                Mode = "201";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Week")
            {
                Mode = "202";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Month")
            {
                Mode = "203";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Quarter")
            {
                Mode = "204";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Year")
            {
                Mode = "205";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            dsOrdersLineItems = _OrderAdmin.ReportList(Mode, Year, "");
            dv1 = new DataView(dsOrdersLineItems.Tables[0]);
        }
        else if (Mode == "24")
        {
            this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/CouponUsage.rdlc";

            if (Status == "Day")
            {
                Mode = "241";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Week")
            {
                Mode = "242";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Month")
            {
                Mode = "243";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Quarter")
            {
                Mode = "244";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Year")
            {
                Mode = "245";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            dsOrdersLineItems = _OrderAdmin.GetAllOrderLineItems().ToDataSet(false);
            dv1 = new DataView(dsOrders.Tables[0]);
        }
        else if (Mode == "26")
        {
            this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/Affiliate.rdlc";

            if (Status == "Day")
            {
                Mode = "261";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Week")
            {
                Mode = "262";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Month")
            {
                Mode = "263";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Quarter")
            {
                Mode = "264";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Year")
            {
                Mode = "265";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            dsOrdersLineItems = _OrderAdmin.GetAllOrderLineItems().ToDataSet(false);
            dv1 = new DataView(dsOrders.Tables[0]);
        }


        if (dv1.ToTable().Rows.Count == 0)
        {
            lblErrorMsg.Text = "No records found";
            objReportViewer.Visible = false;            
            return;
        }

        objReportViewer.LocalReport.DataSources.Clear();
        ReportParameter param1 = new ReportParameter("CurrentLanguage", System.Globalization.CultureInfo.CurrentCulture.Name);
        objReportViewer.LocalReport.SetParameters(new ReportParameter[] { param1 });
        this.objReportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
        objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeAffiliate_ZNodeAffiliate", dv1));
        objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeOrderDataSet_ZNodeOrder", dv1));
        objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeCouponDataSet_ZNodeCoupon", dv1));
        objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeOrderLineItems_ZNodeOrderLineItem", dsOrdersLineItems.Tables[0]));
        objReportViewer.LocalReport.Refresh();
    }

    /// <summary>
    /// Clear Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        if (Request.Params["filter"] != null)
        {
            Mode = Request.Params["filter"];
        } 

        Response.Redirect("~/Admin/Secure/Reports/InventoryReports.aspx?filter=" + Mode);
    }
    # endregion    
}
