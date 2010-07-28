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
using ZNode.Libraries.Payment;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.DataAccess.Service;


public partial class Admin_Secure_sales_orders_capture : System.Web.UI.Page
{
    #region Protected Member Variables
    protected int OrderID = 0;
    #endregion

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        }
    }

    /// <summary>
    /// Refund button clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Capture_Click(object sender, EventArgs e)
    {
        //retrieve order
        ZNode.Libraries.Admin.OrderAdmin orderAdmin = new ZNode.Libraries.Admin.OrderAdmin();
        Order order = orderAdmin.GetOrderByOrderID(OrderID);
        ZNode.Libraries.Framework.Business.ZNodeEncryption enc = new ZNode.Libraries.Framework.Business.ZNodeEncryption();

        //get payment settings
        int paymentSettingID = (int)order.PaymentSettingID;
        PaymentSettingService pss = new PaymentSettingService();
        PaymentSetting ps = pss.GetByPaymentSettingID(paymentSettingID);

        //set gateway info
        GatewayInfo gi = new GatewayInfo();
        gi.GatewayLoginID = enc.DecryptData(ps.GatewayUsername);
        gi.GatewayPassword = enc.DecryptData(ps.GatewayPassword);
        gi.TransactionKey = enc.DecryptData(ps.TransactionKey);
        gi.Vendor = ps.Vendor;
        gi.Partner = ps.Partner;         
        gi.TestMode = ps.TestMode;
        gi.gateway = (GatewayType)ps.GatewayTypeID ;

        string creditCardExp = Convert.ToString(order.CardExp);
        if (creditCardExp == null)
        {
            creditCardExp = "";
        }

        //set credit card
        CreditCard cc = new CreditCard();
        cc.CreditCardExp = creditCardExp;
        cc.OrderID = order.OrderID;
        cc.TransactionID = order.CardTransactionID;
        
        GatewayResponse resp = new GatewayResponse();

        if ((GatewayType)ps.GatewayTypeID == GatewayType.AUTHORIZE)
        {
            GatewayAuthorize auth = new GatewayAuthorize();
            resp = auth.CapturePayment(gi, cc);
        }
        else if ((GatewayType)ps.GatewayTypeID == GatewayType.VERISIGN)
        {
            GatewayPayFlowPro pp = new GatewayPayFlowPro();
            resp = pp.CapturePayment(gi, cc);
        }
        else if ((GatewayType)ps.GatewayTypeID == GatewayType.PAYMENTECH)
        {
            GatewayOrbital pmt = new GatewayOrbital();
            resp = pmt.CapturePayment(gi, cc);
        }
        else
        {
            lblError.Text = "Error: Credit card payment capture is not supported for your gateway.";

        }

        if (resp.IsSuccess)
        {
            //update order status
            order.PaymentStatusID = 1; //refund status

            OrderService os = new OrderService();
            os.Update(order);

            pnlEdit.Visible = false;
            pnlConfirm.Visible = true;
        }
        else
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Could not complete request. The following response was returned by the gateway: ");
            sb.Append(resp.ResponseText);
            lblError.Text = sb.ToString();
        }        
    }

    

    /// <summary>
    /// Cancel button clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("view.aspx?itemid=" + OrderID.ToString());
    }

    /// <summary>
    /// Bind fields
    /// </summary>
    private void BindData()
    {
        ZNode.Libraries.Admin.OrderAdmin _OrderAdminAccess = new ZNode.Libraries.Admin.OrderAdmin();
        ZNode.Libraries.DataAccess.Entities.Order order = _OrderAdminAccess.GetOrderByOrderID(OrderID);

        lblCustomerName.Text = order.BillingFirstName + " " + order.BillingLastName;
        lblTotal.Text = order.Total.Value.ToString("c");
        lblTransactionID.Text = order.CardTransactionID;
    }
}
