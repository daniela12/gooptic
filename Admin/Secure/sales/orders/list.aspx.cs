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
using ZNode.Libraries.Framework.Business ;

public partial class Admin_Secure_sales_orders_list : System.Web.UI.Page
{

    # region Protected Member Variables
    protected string ViewOrderLink = "~/admin/secure/sales/orders/view.aspx?itemid=";
    protected string ChangeStatusLink = "~/admin/secure/sales/orders/orderStatus.aspx?itemid=";
    protected string RefundOrderLink = "~/admin/secure/sales/orders/refund.aspx?itemid=";
    protected string CaptureLink = "~/admin/secure/sales/orders/capture.aspx?itemid=";
    protected static bool SearchEnabled = false;
    protected DataSet MyDataSet = null;
    protected static string ss = "Desc";
    protected string tabDelimeter = ",";
    # endregion

    #region Public Properties
    // SortField property is tracked in ViewState
    public string SortField
    {

        get
        {
            object o = ViewState["SortField"];
            if (o == null)
            {
                return String.Empty;
            }
            return (string)o;
        }

        set
        {
            if (value == SortField)
            {
                // same as current sort file, toggle sort direction
                SortAscending = !SortAscending;
            }
            ViewState["SortField"] = value;
        }
    }

    // SortAscending property is tracked in ViewState
    public bool SortAscending
    {

        get
        {
            object o = ViewState["SortAscending"];
            if (o == null)
            {
                return true;
            }
            return (bool)o;
        }

        set
        {
            ViewState["SortAscending"] = value;
        }
    }
    #endregion

    # region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        ltrlError.Text = "";

        if (!Page.IsPostBack)
        {
            SearchEnabled = false;
            this.BindGrid();
            this.BindData();
            this.GetHighestOrderId();
        }       
    }

    # endregion

    # region Bind Data

    /// <summary>
    /// Bind Datas
    /// </summary>
    public void BindGrid()
    {
        ZNode.Libraries.Admin.OrderAdmin OrderAdmin = new ZNode.Libraries.Admin.OrderAdmin();

        //Bind Grid
        TList<Order> orderList = OrderAdmin.GetAllOrders(ZNodeConfigManager.SiteConfig.PortalID);

        if (orderList != null)
        {
            orderList.Sort("OrderID Desc");
        }

        uxGrid.DataSource = orderList;
        uxGrid.DataBind();
    }

    public void GetHighestOrderId()
    {
        OrderAdmin _OrderAdmin = new OrderAdmin();
        int OrderId = _OrderAdmin.GetHighestOrderId();

        OrderNumber.Text = OrderId.ToString();
    }

    public DataSet BindSearchData(string startdate,string EndDate)
    {
        OrderAdmin _OrderAdmin = new OrderAdmin();
        MyDataSet = _OrderAdmin.FindOrders(txtorderid.Text.Trim(), txtfirstname.Text.Trim(), txtlastname.Text.Trim(), txtcompanyname.Text.Trim(), txtaccountnumber.Text.Trim(), startdate.Trim(), EndDate.Trim(), int.Parse(ListOrderStatus.SelectedValue.ToString()), ZNodeConfigManager.SiteConfig.PortalID);        
        return MyDataSet;
    }
    public void BindData()
    {
        ZNode.Libraries.Admin.OrderAdmin OrderAdmin = new ZNode.Libraries.Admin.OrderAdmin();

        //Add New Item
        ListItem Li = new ListItem();
        Li.Text = "All";
        Li.Value = "-1";

        //Load Order State Item 
        ListOrderStatus.DataSource = OrderAdmin.GetAllOrderStates();
        ListOrderStatus.DataTextField = "orderstatename";
        ListOrderStatus.DataValueField = "Orderstateid";
        ListOrderStatus.DataBind();
        ListOrderStatus.Items.Insert(0, Li);
        ListOrderStatus.Items[0].Selected = true;
    }
    # endregion

    #region General Events

    /// <summary>
    /// Search Button Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string stdate = txtStartDate.Text.Trim();
        string enddate = txtEndDate.Text.Trim();
        MyDataSet  = this.BindSearchData(stdate,enddate);
        DataView dv = new DataView(MyDataSet.Tables[0]);
        dv.Sort = "OrderID Desc";
        uxGrid.DataSource = dv;
        uxGrid.DataBind();        
        SearchEnabled = true;        
    }

    /// <summary>
    /// Clear Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        SearchEnabled = false;       
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        txtorderid.Text = "";
        txtfirstname.Text = "";
        txtlastname.Text = "";
        txtcompanyname.Text = "";
        txtaccountnumber.Text = "";
        ltrlError.Text = "";
        this.BindData();
        this.BindGrid();
    }

    /// <summary>
    /// Download Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButDownload_Click(object sender, EventArgs e)
    {
        DataDownloadAdmin csv = new DataDownloadAdmin();     
        OrderAdmin _OrderAdmin = new OrderAdmin();

        string Orderid = Convert.ToString(OrderNumber.Text);
        DataSet Orders = _OrderAdmin.GetOrdersByOrderId(Orderid);

        if (Orders.Tables[0].Rows.Count > 0)
        { 
            //Set Formatted Data from DataSet           
            string strData = csv.Export(Orders, true, tabDelimeter);

            byte[] data = ASCIIEncoding.ASCII.GetBytes(strData);

            Response.Clear();
            // Set as Excel as the primary format
            Response.AddHeader("Content-Type", "application/Excel");

            Response.AddHeader("Content-Disposition", "attachment;filename=Order.csv");
            Response.ContentType = "application/vnd.xls";
            Response.BinaryWrite(data);

            Response.End();
        }
        else
        {
            ltrlError.Text = "* No Orders to download";
            return;
        }
    }

    /// <summary>
    /// Order ListItem Buttton Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButOrderLineItemsDownload_Click(object sender, EventArgs e)
    {
        DataDownloadAdmin csv = new DataDownloadAdmin();
        OrderAdmin _OrderAdmin = new OrderAdmin();
        
        string Orderid = Convert.ToString(OrderNumber.Text);
        DataSet OrderLineItems = _OrderAdmin.GetOrderLineItemsByOrderId(Orderid);

        if (OrderLineItems.Tables[0].Rows.Count > 0)
        {
            string strData = csv.Export(OrderLineItems, true, tabDelimeter);

            byte[] data = ASCIIEncoding.ASCII.GetBytes(strData);

            Response.Clear();
            // Set as Excel as the primary format
            Response.AddHeader("Content-Type", "application/Excel");

            Response.AddHeader("Content-Disposition", "attachment;filename=OrderLineItems.csv");
            Response.ContentType = "application/vnd.xls";
            Response.BinaryWrite(data);

            Response.End();
        }
        else
        {
            ltrlError.Text = "* No Orders to download";
            return;
        }
    }

    # endregion

    # region Helper methods

    /// <summary>
    /// Contact first name and last name
    /// </summary>
    /// <param name="Firstname"></param>
    /// <param name="LastName"></param>
    /// <returns></returns>
    public string ReturnName(Object Firstname, Object LastName)
    {
        return Firstname.ToString() + " " + LastName.ToString();
    }

    /// <summary>
    /// Format the Price with two decimal
    /// </summary>
    /// <param name="Fieldvalue"></param>
    /// <returns></returns>
    public string FormatPrice(Object Fieldvalue)
    {
        if (Fieldvalue == DBNull.Value)
        {
            return string.Empty;
        }
        else
        {
            return "$" + Fieldvalue.ToString().Substring(0, Fieldvalue.ToString().Length - 2);
        }
    }

    /// <summary>
    /// Display the Order State name for a Order state
    /// </summary>
    /// <param name="FieldValue"></param>
    /// <returns></returns>
    public string DisplayOrderStatus(object FieldValue)
    {
        ZNode.Libraries.Admin.OrderAdmin _OrderAdmin = new ZNode.Libraries.Admin.OrderAdmin();
        OrderState _OrderState = _OrderAdmin.GetByOrderStateID(int.Parse(FieldValue.ToString()));
        return _OrderState.OrderStateName.ToString();
    }

    /// <summary>
    /// Display the Payment type for the Order
    /// </summary>
    /// <param name="Value"></param>
    /// <returns></returns>
    public string DisplayPaymentType(object Value)
    {
        if (Value != null)
        {
            if (Value.ToString().Length > 0)
            {
                ZNode.Libraries.Admin.OrderAdmin _OrderAdmin = new ZNode.Libraries.Admin.OrderAdmin();
                PaymentType _type = _OrderAdmin.GetByPaymentTypeId(int.Parse(Value.ToString()));
                return _type.Name.ToString();
            }
        }

        return "";
    }

    /// <summary>
    /// Retrieves description of payment status
    /// </summary>
    /// <param name="Value"></param>
    /// <returns></returns>
    public string DisplayPaymentStatus(object Value)
    {
        if (Value != null)
        {
            if (Value.ToString().Length > 0)
            {
                PaymentStatusService serv = new PaymentStatusService();
                PaymentStatus ps = serv.GetByPaymentStatusID(int.Parse(Value.ToString()));

                return ps.PaymentStatusName.ToString();
            }
        }

        return "";
    }

    /// <summary>
    /// Enable or disable refund button
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool EnableRefund(object value)
    {
        if (value != null)
        {
            if (value.ToString().Length > 0)
            {
                if (int.Parse(value.ToString()) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        return false;
    }

    public bool EnableCapture(object value)
    {
        if (value != null)
        {
            if (value.ToString().Length > 0)
            {
                if (int.Parse(value.ToString()) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        return false;
    }

    private DataSet GetOrderLineItems()
    {
        OrderAdmin _OrderAdmin = new OrderAdmin();

        string stdate = String.Empty;
        string enddate = String.Empty;
        
        //Check for Search is enabled or not
        if (SearchEnabled)
        {
            stdate = txtStartDate.Text.Trim();
            enddate = txtEndDate.Text.Trim();
        }       
        
        DataSet MyDataSet = _OrderAdmin.GetOrderLineItems(txtorderid.Text.Trim(),txtfirstname.Text.Trim(),txtlastname.Text.Trim(),txtcompanyname.Text.Trim(),txtaccountnumber.Text.Trim(),stdate ,enddate, int.Parse(ListOrderStatus.SelectedValue.ToString()), ZNodeConfigManager.SiteConfig.PortalID);

        return MyDataSet;
    }

    # endregion

    #region Grid Events

    /// <summary>
    /// Method to sort the grid in Ascending and Descending Order
    /// </summary>   
    protected void SortGrid() 
    {
        OrderAdmin _OrderAdmin = new OrderAdmin();
        DataSet ds = _OrderAdmin.FindOrders(txtorderid.Text.Trim(), txtfirstname.Text.Trim(), txtlastname.Text.Trim(), txtcompanyname.Text.Trim(), txtaccountnumber.Text.Trim(), txtStartDate.Text.Trim(), txtEndDate.Text.Trim(), int.Parse(ListOrderStatus.SelectedValue.ToString()), ZNodeConfigManager.SiteConfig.PortalID);
        uxGrid.DataSource = ds;

        DataSet dataSet = uxGrid.DataSource as DataSet;      

        DataView dataView = new DataView(dataSet.Tables[0]);

        // Apply sort filter and direction
        dataView.Sort = SortField;

        //If sortDirection is not Ascending
        if (!SortAscending) 
        {
             dataView.Sort += " DESC";
        }
            uxGrid.DataSource = null;
            uxGrid.DataSource = dataView;
            uxGrid.DataBind();            
        }

    /// <summary>
    /// Grid Sorting Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_Sorting(object sender, GridViewSortEventArgs e)
    {

        uxGrid.PageIndex = 0;
        SortField = e.SortExpression;
        SortGrid();
    }

    /// <summary>
    /// Grid Paging Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        if (SearchEnabled)
        {
            MyDataSet = this.BindSearchData(txtStartDate.Text.Trim(), txtEndDate.Text.Trim());
            uxGrid.DataSource = MyDataSet;
            uxGrid.DataBind();
        }
        else
        {
            this.BindGrid();
        }
    }

    /// <summary>
    ///  Event triggered when the grid page is changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Page")
        {  }
        else
        {
            if (e.CommandName == "ViewOrder")
            {
                Response.Redirect(ViewOrderLink + e.CommandArgument.ToString());
            }
            if (e.CommandName == "RefundOrder")
            {
                Response.Redirect(RefundOrderLink + e.CommandArgument.ToString());
            }
            else if (e.CommandName == "Status")
            {
                Response.Redirect(ChangeStatusLink  + e.CommandArgument.ToString());
            }
            else if (e.CommandName == "Capture")
            {
                Response.Redirect(CaptureLink + e.CommandArgument.ToString());
            }
        }
    }

    #endregion

    # region Client Side Script

    /// <summary>
    /// Includes Javascript file and css file into this page
    /// </summary>
    public void RegisterClientScript()
    {
        //Include the Client Side Script from the resource file
        //The Resource File is named “Calender.js”
        //Located inside the Calendar directory
        HtmlGenericControl Include = new HtmlGenericControl("script");
        Include.Attributes.Add("type", "text/javascript");
        Include.Attributes.Add("src", "Calendar/Calendar.js");


        //The Resource File is named “Calender.css”
        //Located inside the Calendar directory
        HtmlGenericControl Include1 = new HtmlGenericControl("link");
        Include1.Attributes.Add("type", "text/css");
        Include1.Attributes.Add("rel", "stylesheet");
        Include1.Attributes.Add("href", "Calendar/Calendar.css");

        //add a script reference for Javascript to the head section
        this.Page.Header.Controls.Add(Include);
        this.Page.Header.Controls.Add(Include1);

    }

    # endregion

}
