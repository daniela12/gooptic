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

public partial class Admin_Secure_Reports_ActivityLogReport : System.Web.UI.Page
{
    # region Protected Member Variables    
    protected DataSet MyDataSet = null;
    DataSet dsOrders = new DataSet();    
    protected string Mode = "";
    protected string Year = "";
    #endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Params["filter"] != null)
        {
            Mode = Request.Params["filter"];
        }

        // Get Fietered Orders in DataSet
        OrderAdmin _OrderAdmin = new OrderAdmin();
        dsOrders = _OrderAdmin.ReportList(Mode, Year, "");        
        DataView dv = new DataView(dsOrders.Tables[0]);

        if (!Page.IsPostBack)
        {
            // Load Order State Item 
            if (Request.Params["filter"] != null)
            {
                if (Mode.Equals("22"))
                {
                    this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/ActivityLog.rdlc";
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
            objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZnodeActivityLog_ZnodeActivityLog", dv));
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

    protected void btnOrderFilter_Click(object sender, EventArgs e)
    {
        objReportViewer.Visible = true;
        DateTime StartDate = DateTime.MinValue;        

        //Get Filetered Orders in DataSet
        OrderAdmin _OrderAdmin = new OrderAdmin();
        dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
        DataView dv = new DataView(dsOrders.Tables[0]);
        
        if (txtStartDate.Text.Length > 0)
            StartDate = DateTime.Parse(txtStartDate.Text.Trim());     

        string Status = ddlLogCategory.SelectedItem.Text;

        if (Status == "ALL" && txtStartDate.Text.Length > 0)
        {
            dv.RowFilter = "createDte >= '" + StartDate + "'";
        }
        else if(Status != "ALL" && txtStartDate.Text.Length > 0)
        {
            dv.RowFilter = "createDte >= '" + StartDate + "' and category = '" + Status + "'";
        }
        else if(Status != "ALL")
        {
            dv.RowFilter = "category = '" + Status + "'";
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
        objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZnodeActivityLog_ZnodeActivityLog", dv));
    }

    /// <summary>
    /// Clear Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/Secure/Reports/ActivityLogReport.aspx?filter=22");
    }
}
