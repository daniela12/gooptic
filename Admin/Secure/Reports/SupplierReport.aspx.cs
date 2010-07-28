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

public partial class Admin_Secure_Reports_SupplierReport : System.Web.UI.Page
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

            //Bind Supplier
            SupplierAdmin supplier = new SupplierAdmin();
            ddlSupplier.DataSource = supplier.GetAll();
            ddlSupplier.DataTextField = "name";
            ddlSupplier.DataValueField = "supplierid";
            ddlSupplier.DataBind();
            ListItem li1 = new ListItem("None", "0");
            ddlSupplier.Items.Insert(0, li1);    

            //Get Filetered Orders in DataSet
            OrderAdmin _OrderAdmin = new OrderAdmin();
            DataView dv = new DataView();
           
            dsOrders = _OrderAdmin.ReportList(Mode, Year,"");
            dsOrdersLineItems = _OrderAdmin.GetAllOrderLineItems().ToDataSet(false);
            dv = new DataView(dsOrders.Tables[0]);         

            if (Request.Params["filter"] != null)
            {  
                this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/SupplierReport.rdlc";                
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
            objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeOrderLineItems_ZNodeOrderLineItem", dsOrdersLineItems.Tables[0]));
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
        e.DataSources.Add(new ReportDataSource("ZNodeOrderDataSet_ZNodeOrder", dsOrdersLineItems.Tables[0]));
        e.DataSources.Add(new ReportDataSource("ZNodeOrderLineItems_ZNodeOrderLineItem", dsOrdersLineItems.Tables[0]));
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

        Response.Redirect("~/Admin/Secure/Reports/SupplierReport.aspx?filter=" + Mode);
    }

    /// <summary>
    /// Get Order details click event
    /// </summary>
    protected void btnOrderFilter_Click(object sender, EventArgs e)
    {
        objReportViewer.Visible = true;

        Mode = "271";
        
        //Get Filetered Orders in DataSet
        OrderAdmin _OrderAdmin = new OrderAdmin();
        dsOrders = _OrderAdmin.ReportList(Mode, Year, ddlSupplier.SelectedValue);
        dsOrdersLineItems = _OrderAdmin.GetAllOrderLineItems().ToDataSet(false);
        DataView dv = new DataView(dsOrders.Tables[0]);
        DataView dv1 = new DataView();
        
        dsOrders = _OrderAdmin.ReportList(Mode, Year,ddlSupplier.SelectedValue);
        dv = new DataView(dsOrders.Tables[0]);
        
        DateTime StartDate = DateTime.Parse(txtStartDate.Text.Trim());
        DateTime EndDate = DateTime.Parse(txtEndDate.Text.Trim());
             
        dv.RowFilter = "orderdate >= '" + StartDate + "' and orderdate <= '" + EndDate.AddDays(1) + "'";
       
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
        objReportViewer.LocalReport.Refresh(); 
    }
    # endregion

}

