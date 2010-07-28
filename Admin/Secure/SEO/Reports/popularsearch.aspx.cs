using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
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

public partial class Admin_Secure_SEO_Reports_SEOReport : System.Web.UI.Page
{
    #region Protected Member Variables
    DataSet dsOrders = new DataSet();
    protected string Mode = "";
    protected static string PageType = "";
    protected string Year = "";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.Params["filter"] != null)
            {
                Mode = Request.Params["filter"];
            }
            if (Request.Params["pagetype"] != null)
            {
                PageType = Request.Params["pagetype"];
            }
            else
            {
                PageType = "";
            }

            // Get Filetered Orders in DataSet
            OrderAdmin _OrderAdmin = new OrderAdmin();
            dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            DataView dv = new DataView(dsOrders.Tables[0]);

            // Set the Report Path
            this.objReportViewer.LocalReport.ReportPath = "admin/Secure/SEO/Reports/popularsearch.rdlc";           


            if (dv.ToTable().Rows.Count == 0)
            {
                lblErrorMsg.Text = "No records found";
                objReportViewer.Visible = false;
                return;
            }

            objReportViewer.LocalReport.DataSources.Clear();
            ReportParameter param1 = new ReportParameter("CurrentLanguage", System.Globalization.CultureInfo.CurrentCulture.Name);
            objReportViewer.LocalReport.SetParameters(new ReportParameter[] { param1 });
            objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZnodeSEOMost_Search_ZnodeSEOSearch", dsOrders.Tables[0]));
            objReportViewer.LocalReport.Refresh();
        }
    }

     /// <summary>
    /// Back Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!PageType.Equals(""))
        { 
            Response.Redirect("~/admin/secure/default.aspx"); 
        }
        else
        {
            Response.Redirect("~/admin/Secure/SEO/SEOManager.aspx");
        }
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
        dsOrders = _OrderAdmin.ReportList(Mode, Year, "");        
        DataView dv = new DataView();        

        string Status = ListOrderStatus.SelectedItem.Text;
        if (Mode == "23")
        {
            this.objReportViewer.LocalReport.ReportPath = "admin/Secure/SEO/Reports/popularsearch.rdlc";

            if (Status == "Day")
            {
                Mode = "231";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Week")
            {
                Mode = "232";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Month")
            {
                Mode = "233";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Quarter")
            {
                Mode = "234";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }
            else if (Status == "Year")
            {
                Mode = "235";
                dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            }

            dv = new DataView(dsOrders.Tables[0]);
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
        objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZnodeSEOMost_Search_ZnodeSEOSearch", dv));
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

        Response.Redirect("~/admin/Secure/SEO/Reports/popularsearch.aspx?filter=" + Mode);
    }
}
