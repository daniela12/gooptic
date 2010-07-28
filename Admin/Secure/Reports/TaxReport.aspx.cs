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

public partial class Admin_Secure_Reports_TaxReport : System.Web.UI.Page
{

    # region Protected Member Variables
    protected int AccountID;
    protected DataSet MyDataSet = null;
    static DataSet dsOrders = new DataSet();
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
            OrderAdmin _OrderAdmin = new OrderAdmin();
            dsOrders = _OrderAdmin.ReportList(Mode, Year,""); 
            DataView dv = new DataView(dsOrders.Tables[0]);           

            if (Request.Params["filter"] != null)
            {
                Mode = Request.Params["filter"];
                if (Mode.Equals("25"))
                {
                    this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/TaxReport.rdlc";
                }                
            }


                       
            if (dv.ToTable().Rows.Count == 0)
            {
                lblErrorMsg.Text = "No records found";
                objReportViewer.Visible = false;               
                return;
            }

            objReportViewer.LocalReport.DataSources.Clear();
            ReportParameter param1 = new ReportParameter("CurrentLanguage", System.Globalization.CultureInfo.CurrentCulture.Name);
            objReportViewer.LocalReport.SetParameters(new ReportParameter[] { param1 });
            this.objReportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);           
            objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeOrderDataSet_ZNodeOrder", dv));
            objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeTitleDataSet_ZNodeTitle", dv));
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
        e.DataSources.Add(new ReportDataSource("ZNodeOrderDataSet_ZNodeOrder", dsOrders.Tables[0]));
        e.DataSources.Add(new ReportDataSource("ZNodeTitleDataSet_ZNodeTitle", dsOrders.Tables[0]));
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

    protected void btnOrderFilter_Click(object sender, EventArgs e)
    {
        objReportViewer.Visible = true;
        if (Request.Params["filter"] != null)
        {
            Mode = Request.Params["filter"];
        }
        
        //Get Filetered Orders in DataSet
        OrderAdmin _OrderAdmin = new OrderAdmin();       
        DataView dv = new DataView();
       

        if (dsOrders.Tables.Count == 0)
        {
            Mode = "25";
            dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
        }
        this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/TaxReport.rdlc";
        dv = new DataView(dsOrders.Tables[0]);

        DateTime StartDate = DateTime.Parse(txtStartDate.Text.Trim());
        DateTime EndDate = DateTime.Parse(txtEndDate.Text.Trim());

        dv.RowFilter = "orderdate >= '" + StartDate + "' and orderdate <= '" + EndDate.AddDays(1) + "'";

        if (dv.Count == 0)
        {
            lblErrorMsg.Text = "No records found";
            objReportViewer.Visible = false;
            return;
        }
        

     

        objReportViewer.LocalReport.DataSources.Clear();
        ReportParameter param1 = new ReportParameter("CurrentLanguage", System.Globalization.CultureInfo.CurrentCulture.Name);
        objReportViewer.LocalReport.SetParameters(new ReportParameter[] { param1 });
        this.objReportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
        objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeOrderDataSet_ZNodeOrder", dv));
        objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeTitleDataSet_ZNodeTitle", dv));
        this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/TaxReportMonth.rdlc";
        string asas = this.objReportViewer.LocalReport.ReportPath;
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

        Response.Redirect("~/Admin/Secure/Reports/TaxReport.aspx?filter=" + Mode);
    }
    # endregion


}
