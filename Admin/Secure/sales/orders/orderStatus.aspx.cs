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
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.IO;
using System.Text.RegularExpressions;
using ZNode.Libraries.Framework.Business;

public partial class Admin_Secure_sales_orders_orderStatus : System.Web.UI.Page
{
    # region Protected Member Variables
    protected int OrderID = 0;
    protected string ListPage = "list.aspx";
    # endregion

    # region General Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Request.Params["itemid"] != null) || (Request.Params["itemid"].Length != 0))
        {
            OrderID = int.Parse(Request.Params["itemid"].ToString());
            lblOrderID.Text = OrderID.ToString();
        }

        if (!Page.IsPostBack)
        {
            this.BindData();
            if (ListOrderStatus.SelectedValue.Equals("20"))
            {
                EmailStatus.Visible = true;
            }
            else
            {
                EmailStatus.Visible = false;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void UpdateOrderStatus_Click(object sender, EventArgs e)
    {
        ZNode.Libraries.Admin.OrderAdmin orderAdmin = new ZNode.Libraries.Admin.OrderAdmin();
        ZNode.Libraries.DataAccess.Entities.Order order = orderAdmin.GetOrderByOrderID(OrderID);

        if (order != null)
        {
            order.OrderStateID = int.Parse(ListOrderStatus.SelectedValue);
            order.OrderID = OrderID;
            order.TrackingNumber = TrackingNumber.Text.Trim();
            if (order.OrderStateID == 20)
            {
                order.ShipDate = DateTime.Now.Date + DateTime.Now.TimeOfDay;
            }
            if (order.OrderStateID == 30)
            {
                order.ReturnDate = DateTime.Now.Date + DateTime.Now.TimeOfDay;
            }

            bool Check = orderAdmin.Update(order);

            if (Check)
            {
                Response.Redirect(ListPage);
            }
            else
            {
                //Do Nothing
            }
            
        }
    }

    /// <summary>
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CancelStatus_Click(object sender, EventArgs e)
    {
        Response.Redirect(ListPage);
    }

    /// <summary>
    /// Dropdown list selected index change
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void OrderStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ListOrderStatus.SelectedValue.Equals("20"))
        {
            BindData();
            ListOrderStatus.SelectedValue = "20";
            EmailStatus.Visible = true;
        }
        else
        {
            EmailStatus.Visible = false;
        }
    }

    /// <summary>
    /// Email Status Button Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void EmailStatus_Click(object sender, EventArgs e)
    {

        ZNode.Libraries.Admin.OrderAdmin _OrderAdmin = new ZNode.Libraries.Admin.OrderAdmin();
        ZNode.Libraries.DataAccess.Entities.Order _OrderList = _OrderAdmin.DeepLoadByOrderID(OrderID);

        SendEmailReceipt(_OrderList);

        EmailStatus.Enabled = false;
    }

    private void SendEmailReceipt(ZNode.Libraries.DataAccess.Entities.Order order)
    {
        try
        {
            string senderEmail = ZNodeConfigManager.SiteConfig.CustomerServiceEmail;
            string recepientEmail = order.BillingEmailId;
            string subject = "Track your package";

            //get message text.
            StreamReader rw = new StreamReader(Server.MapPath(ZNodeConfigManager.EnvironmentConfig.ConfigPath + "TrackingNumber.htm"));
            string messageText = rw.ReadToEnd();
            Regex rx1 = new Regex("#BillingFirstName#", RegexOptions.IgnoreCase);
            messageText = rx1.Replace(messageText, order.BillingFirstName);


            Regex rx2 = new Regex("#BillingLastName#", RegexOptions.IgnoreCase);
            messageText = rx2.Replace(messageText, order.BillingLastName);

            Regex rx3 = new Regex("#Custom1#", RegexOptions.IgnoreCase);
            messageText = rx3.Replace(messageText, TrackingNumber.Text);

            Regex rx4 = new Regex("#TrackingMessage#", RegexOptions.IgnoreCase);
            if (order.ShippingIDSource.ShippingTypeID == 3)
            { messageText = rx4.Replace(messageText, "Your FedEx Tracking Number :  "); }
            else if (order.ShippingIDSource.ShippingTypeID == 2)
            { messageText = rx4.Replace(messageText, "Your UPS Tracking Number :  "); }
            else
            { messageText = rx4.Replace(messageText, ""); }

            Regex rx5 = new Regex("#Message#",RegexOptions.IgnoreCase);
             if (order.ShippingIDSource.ShippingTypeID == 3)
            {messageText = rx5.Replace(messageText, "We would like to inform you that your order has shipped. You can track your package using the tracking number given below:"); }
             else if (order.ShippingIDSource.ShippingTypeID == 2)
             { messageText = rx5.Replace(messageText, "We would like to inform you that your order has shipped. You can track your package using the tracking number given below:"); }
             else
             { messageText = rx5.Replace(messageText, "We would like to inform you that your order has shipped."); }

            ZNode.Libraries.Framework.Business.ZNodeEmail.SendEmail(recepientEmail, senderEmail, senderEmail, subject, messageText, true);


            trackmessage.Text = "Tracking Number Email Sent to " + "<a href=\"mailto:" + senderEmail.ToString() + "\">" + senderEmail + "</a>";

        }
        catch (Exception ex)
        {
            string senderEmail = ZNodeConfigManager.SiteConfig.CustomerServiceEmail;
            trackmessage.Text = "There was a problem sending the email to " + "<a href=\"mailto:" + senderEmail.ToString() + "\">" + senderEmail + "</a>";

            //log exception
            ExceptionPolicy.HandleException(ex, "ZNODE_GLOBAL_EXCEPTION_POLICY");
            //do not rethrow as this is non-critical
        }
    }

    # endregion

    # region Bind Data

    private void BindData()
    {
        ZNode.Libraries.Admin.OrderAdmin _OrderAdminAccess = new ZNode.Libraries.Admin.OrderAdmin();

        //Load Order State Item 
        ListOrderStatus.DataSource = _OrderAdminAccess.GetAllOrderStates();
        ListOrderStatus.DataTextField = "orderstatename";
        ListOrderStatus.DataValueField = "Orderstateid";
        ListOrderStatus.DataBind();

        ZNode.Libraries.DataAccess.Entities.Order order = _OrderAdminAccess.GetOrderByOrderID(OrderID);

        int ShippingId = Convert.ToInt32(order.ShippingID);

        ZNode.Libraries.Admin.ShippingAdmin _shipping = new ZNode.Libraries.Admin.ShippingAdmin();

        int ShipId = _shipping.GetShippingtypeid(ShippingId);

        //Set the Tracking number title based on the ShippingTypeId
        if (ShipId == 1)
        {
            pnlTrack.Visible = true;
            TrackTitle.Visible = false;
            TrackingNumber.Visible = false;
        }
        else if (ShipId == 2)
        {
            pnlTrack.Visible = true;
            TrackTitle.Text = "UPS Tracking Number";
        }
        else
        {
            pnlTrack.Visible = true;
            TrackTitle.Text = "FedEx Tracking Number";
        }       

        if (order != null)
        {         
            ListOrderStatus.SelectedValue = order.OrderStateID.ToString(); 
            TrackingNumber.Text = order.TrackingNumber;          
        }

        lblCustomerName.Text = order.BillingFirstName + " " + order.BillingLastName;
        lblTotal.Text = order.Total.Value.ToString("c");
    }

    # endregion
    
}
