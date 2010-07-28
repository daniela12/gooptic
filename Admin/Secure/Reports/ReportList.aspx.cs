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

public partial class Admin_Secure_orders_ReportList : System.Web.UI.Page
{
    # region Protected Member Variables
    protected int AccountID;
    protected DataSet MyDataSet = null;
    DataSet dsOrders = new DataSet();
    DataSet dsOrdersLineItems = new DataSet();
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
        if (Request.Params["filter"] != null)
        {
            Mode = Request.Params["filter"];
        }

        if (!Page.IsPostBack)
        {
            // Load Order State Item 
            OrderAdmin _OrderAdminAccess = new OrderAdmin();
            ddlOrderStatus.DataSource = _OrderAdminAccess.GetAllOrderStates();
            ddlOrderStatus.DataTextField = "orderstatename";
            ddlOrderStatus.DataValueField = "Orderstateid";
            ddlOrderStatus.DataBind();
            ListItem item1 = new ListItem("ALL", "0");
            ddlOrderStatus.Items.Insert(0, item1);
            ddlOrderStatus.SelectedIndex = 0;

            // Load Profile Types
            ProfileAdmin profileAdmin = new ProfileAdmin();
            ddlProfilename.DataSource = profileAdmin.GetAll();
            ddlProfilename.DataTextField = "Name";
            ddlProfilename.DataValueField = "ProfileID";
            ddlProfilename.DataBind();
            ListItem item2 = new ListItem("ALL", "0");
            ddlProfilename.Items.Insert(0, item2);
            ddlProfilename.SelectedIndex = 0;
            

            // Get Filetered Orders in DataSet
            OrderAdmin _OrderAdmin = new OrderAdmin();
            dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            dsOrdersLineItems = _OrderAdmin.GetAllOrderLineItems().ToDataSet(false);
            DataView dv = new DataView(dsOrders.Tables[0]);

            if (Request.Params["filter"] != null)
            {               
                if (Mode.Equals("12"))
                {                    
                    this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/Orders.rdlc";
                    pnlOrderStatus.Visible = true;  
                    lblOrderStatus.Visible = true;
                    lblOrderStatus.Text = "Order Status";
                    btnOrderFilter.Text = "Get Orders";
                }           
                if (Mode.Equals("21"))
                {
                    this.objReportViewer.LocalReport.ReportPath = "Admin/Secure/Reports/Accounts.rdlc";
                    pnlprofile.Visible = true;
                    lblProfileName.Visible = true;
                    lblProfileName.Text = "Profiles";
                    btnOrderFilter.Text = "Get Details";
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
            if (Mode.Equals("12"))            
                objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeOrderDataSet_ZNodeOrder", dv));
            else if(Mode.Equals("21"))
                objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeAccountDataSet_ZNodeAccount", dv));
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

    protected void btnOrderFilter_Click(object sender, EventArgs e)
    {
        objReportViewer.Visible = true;

        //Get Filetered Orders in DataSet
        OrderAdmin _OrderAdmin = new OrderAdmin();
        DataView dv = new DataView();
        if (Mode.Equals("12"))
        {
            DataSet tempDs = FormatReportDataSet(_OrderAdmin.GetAllOrders(ZNodeConfigManager.SiteConfig.PortalID).ToDataSet(false));
            dsOrdersLineItems = _OrderAdmin.GetAllOrderLineItems().ToDataSet(false);
            dv = new DataView(tempDs.Tables[0]);
        }
        else if (Mode.Equals("21"))
        {
            dsOrders = _OrderAdmin.ReportList(Mode, Year, "");
            dv = new DataView(dsOrders.Tables[0]);
        }

        DateTime StartDate = DateTime.Parse(txtStartDate.Text.Trim());
        DateTime EndDate = DateTime.Parse(txtEndDate.Text.Trim());

        if (Mode.Equals("12"))
        {
            string Status = ddlOrderStatus.SelectedItem.Text;
            if (Status == "ALL")
            {
                dv.RowFilter = "orderdate >= '" + StartDate + "' and orderdate <= '" + EndDate.AddDays(1) + "'";
            }
            else
            {
                dv.RowFilter = "orderdate >= '" + StartDate + "' and orderdate <= '" + EndDate.AddDays(1) + "' and orderstatus = '" + Status + "'";
            }
        }

        if (Mode.Equals("21"))
        {
            string Status = ddlProfilename.SelectedItem.Text;
            if (Status == "ALL")
            {
                dv.RowFilter = "CreateDte >= '" + StartDate + "' and CreateDte <= '" + EndDate.AddDays(1) + "'";
            }
            else
            {
                dv.RowFilter = "CreateDte >= '" + StartDate + "' and CreateDte <= '" + EndDate.AddDays(1) + "' and ProfileName = '" + Status + "'";
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
        if (Mode.Equals("12"))
            objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeOrderDataSet_ZNodeOrder", dv));
        else if (Mode.Equals("21"))
            objReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ZNodeAccountDataSet_ZNodeAccount", dv));
        objReportViewer.LocalReport.Refresh();
    }

    /// <summary>
    /// Clear Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        if (Mode.Equals("12"))
        {
            Response.Redirect("~/Admin/Secure/Reports/ReportList.aspx?filter=12");
            pnlOrderStatus.Visible = true;
        }
        if (Mode.Equals("21"))
        {
            Response.Redirect("~/Admin/Secure/Reports/ReportList.aspx?filter=21");
            pnlprofile.Visible = true;
        }        
    }
   # endregion

    # region Helper Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ds"></param>
    /// <returns></returns>
    private DataSet FormatReportDataSet(DataSet ds)
    {
        //This will copied the structure and data from the Original dataset
        DataSet tempDataSet = ds.Copy();

        # region Local Variables
        OrderAdmin orderAdmin = new OrderAdmin();
        ShippingAdmin shippingAdmin = new ShippingAdmin();
        StoreSettingsAdmin storeAdmin = new StoreSettingsAdmin();
        #endregion

        //Initialize new columns for OrderStatus,ShippingType,Payment Type
        //Add column to this temporary dataset
        DataColumn columnOrderStatus = new DataColumn("OrderStatus");
        tempDataSet.Tables[0].Columns.Add(columnOrderStatus);

        //Add ShippingType column
        DataColumn columnShippingType = new DataColumn("ShippingTypeName");
        tempDataSet.Tables[0].Columns.Add(columnShippingType);

        //Add Paymenttype column
        DataColumn columnPaymentType = new DataColumn("PaymentTypeName");
        tempDataSet.Tables[0].Columns.Add(columnPaymentType);
        
        //Loop through the Orders in the dataset
        foreach (DataRow dr in tempDataSet.Tables[0].Rows)
        {
            //Get Order Status
            int OrderStateId = int.Parse(dr["OrderStateId"].ToString());
            OrderState entity = orderAdmin.GetByOrderStateID(OrderStateId);

            int shippingId = int.Parse(dr["ShippingId"].ToString());
            Shipping shippingEntity = shippingAdmin.GetShippingOptionById(shippingId);

            int paymentTypeId = 0;
            if(dr["PaymentTypeId"].ToString().Length > 0)
            	//If PaymentTypeId value length is greater than Zero
                paymentTypeId = int.Parse(dr["PaymentTypeId"].ToString());
            PaymentType paymentType = storeAdmin.GetPaymentTypeById(paymentTypeId);

            if (entity != null)
                dr["OrderStatus"] = entity.OrderStateName;

            if (shippingEntity != null)
                dr["ShippingTypeName"] = (shippingAdmin.GetShippingTypeName(shippingEntity.ShippingTypeID));

            if (paymentType != null)
                dr["PaymentTypeName"] = paymentType.Name;
        }

        //Return dataset
        return tempDataSet;
    }
    #endregion    
    
}
