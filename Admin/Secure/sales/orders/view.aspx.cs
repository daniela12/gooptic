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
using ZNode.Libraries.DataAccess.Entities;
using System.IO;
using ZNode.Libraries.Framework.Business;

public partial class Admin_Secure_sales_orders_view : System.Web.UI.Page
{
    # region Protected variables
    protected int OrderID = 0;
    bool AdvancedShipping = false;
    protected string StatusPage = "orderstatus.aspx?itemid=";
    protected string RefundPage = "refund.aspx?itemid=";
    protected string ListPage = "list.aspx";
    protected string OrderTrackingNumber = string.Empty;
    protected string filepath = ZNodeConfigManager.EnvironmentConfig.DataPath + "/ShippingLabels//FedEx//";
    # endregion

    # region Page Load

    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if ((Request.Params["itemid"] != null) || (Request.Params["itemid"].Length != 0))
        {
            OrderID = int.Parse(Request.Params["itemid"].ToString());
            lblOrderHeader.Text = OrderID.ToString();
        }

        


        if (!Page.IsPostBack)
        {
            this.BindData();
            this.BindGrid();
        }


        //build the javascript block
        StringBuilder sb = new StringBuilder();
        sb.Append("<script language=JavaScript>");
        sb.Append("    function Back() {");
        sb.Append("        javascript:history.go(-1);");
        sb.Append("    }");
        sb.Append("<" + "/script>");


        if (!ClientScript.IsStartupScriptRegistered("GoBack"))
        {
            ClientScript.RegisterStartupScript(GetType(), "GoBack", sb.ToString());
        }

    }
    # endregion

    # region Bind Data

    public void BindData()
    {
        # region Declarations
        OrderAdmin _OrderAdmin = new OrderAdmin();
        Order _orderList = _OrderAdmin.DeepLoadByOrderID(OrderID);
        # endregion

        if (_orderList != null)
        {

            //FedEx shipping is the only advanced shipping currently supported.
            if (_orderList.ShippingIDSource.ShippingTypeID == 3)
            {

                
                if(_orderList.ShippingIDSource.ShippingCode.ToLower().Contains("international"))
                {
                AdvancedShipping = false;
                }
                else
                {
             //Leaving Advanced Shipping Options disabled by Default.
                      AdvancedShipping = false;
                    //AdvancedShipping = true;
                }
            }
            else
            {
                AdvancedShipping = false;
            }
            StringBuilder Build = new StringBuilder();
            Build.Append(_orderList.BillingFirstName.ToString() + " ");
            Build.Append(_orderList.BillingLastName.ToString() + "<br>");
            Build.Append(_orderList.BillingCompanyName.ToString() + "<br>");
            Build.Append(_orderList.BillingStreet.ToString() + " ");
            Build.Append(_orderList.BillingStreet1.ToString() + "<br>");
            Build.Append(_orderList.BillingCity.ToString() + ", ");
            Build.Append(_orderList.BillingStateCode.ToString() + " ");
            Build.Append(_orderList.BillingPostalCode.ToString() + "<br>");
            Build.Append(_orderList.BillingCountry.ToString() + "<br>");
            Build.Append("Tel: " + _orderList.BillingPhoneNumber.ToString() + "<br>");
            Build.Append("Email: " + _orderList.BillingEmailId.ToString());

            lblBillingAddress.Text = Build.ToString();

            Build.Remove(0, Build.Length);
            Build.Append(_orderList.ShipFirstName.ToString() + " ");
            Build.Append(_orderList.ShipLastName.ToString() + "<br>");
            Build.Append(_orderList.ShipCompanyName.ToString() + "<br>");
            Build.Append(_orderList.ShipStreet.ToString() + " ");
            Build.Append(_orderList.ShipStreet1.ToString() + "<br>");
            Build.Append(_orderList.ShipCity.ToString() + ", ");
            Build.Append(_orderList.ShipStateCode.ToString() + " ");
            Build.Append(_orderList.ShipPostalCode.ToString() + "<br>");
            Build.Append(_orderList.ShipCountry.ToString() + "<br>");
            Build.Append("Tel: " + _orderList.ShipPhoneNumber.ToString() + "<br>");
            Build.Append("Email: " + _orderList.ShipEmailID.ToString());

            lblShippingAddress.Text = Build.ToString();

            lblOrderDate.Text = _orderList.OrderDate.Value.ToShortDateString();
            lblOrderStatus.Text = _orderList.OrderStateIDSource.Description;
            lblShipAmount.Text = this.Formatprice(_orderList.ShippingCost);
            lblOrderAmount.Text = this.Formatprice(_orderList.Total);
            lblTaxAmount.Text = this.Formatprice(_orderList.TaxCost);
            if (_orderList.DiscountAmount.HasValue)
                lblDiscountAmt.Text = _orderList.DiscountAmount.Value.ToString("c");

            lblCouponCode.Text = _orderList.CouponCode;

            if (_orderList.PaymentTypeId.HasValue)
            {
                string paymentTypeName = GetPaymentTypeName(_orderList.PaymentTypeId.Value);

                if (_orderList.PaymentTypeId.Value == 1)//If purchase order payment
                    lblPurchaseOrder.Text = _orderList.PurchaseOrderNumber;

                lblPaymentType.Text = paymentTypeName;
            }
            lblTransactionId.Text = _orderList.CardTransactionID;
            lblShippingMethod.Text = _orderList.ShippingIDSource.Description;
            lblTrackingNumber.Text = _orderList.TrackingNumber;
            lblPaymentStatus.Text = _orderList.PaymentStatusIDSource.Description;

            //Bind custom additional instructions to label field
            lblAdditionalInstructions.Text = _orderList.AdditionalInstructions;

            if (_orderList.PaymentTypeId != 0)
            {
                Refund.Enabled = false;
            }


            OrderTrackingNumber = _orderList.TrackingNumber;
            if (OrderTrackingNumber == null)
            {
                OrderTrackingNumber = string.Empty;
            }

            if (AdvancedShipping)
            {
                if (OrderTrackingNumber == string.Empty)
                {
                    Shipping.Text = "Create Shipment";
                    LabelButton.Enabled = false;
                    LabelButton.Visible = false;
                    DemensionPanel.Visible = true;
                    LineItemDemensions.Visible = true;
                }
                else
                {
                    Shipping.Text = "Cancel Shipment";

                    //Shipment exists, disable the estimated demension boxes.
                    DemensionPanel.Visible = false;


                    if (File.Exists(Server.MapPath(filepath) + OrderTrackingNumber + ".pdf"))
                    {
                        LabelButton.Enabled = true;
                        LabelButton.Visible = true;
                    }
                    else
                    {
                        LabelButton.Enabled = false;
                        LabelButton.Visible = false;
                    }
                }
            }
            else
            {
                //LabelButton.Enabled = false;
                //LabelButton.Visible = false;
                //Shipping.Enabled = false;
                //Shipping.Visible = false;
                ShippingPanel.Visible = false;
                LineItemDemensions.Visible = false;
                ShippingErrorPanel.Visible = false;
            }
        }
        else
        {
            throw (new ApplicationException("Order Requested could not be found."));
        }
    }

    /// <summary>
    /// Bind grid with Order line items
    /// </summary>
    private void BindGrid()
    {
        ZNode.Libraries.Admin.OrderAdmin _OrderLineItemAdmin = new ZNode.Libraries.Admin.OrderAdmin();
        ShippingAdmin _ShippingAdmin = new ShippingAdmin();

        TList<OrderLineItem> fullorderlist = _OrderLineItemAdmin.GetOrderLineItemByOrderID(OrderID);

        TList<OrderLineItem> shiptogetherlist = new TList<OrderLineItem>();
        TList<OrderLineItem> shipseperatelist = new TList<OrderLineItem>();

        shipseperatelist = _ShippingAdmin.GetLineItemsbyShippingPreference(fullorderlist, true);
        shiptogetherlist = _ShippingAdmin.GetLineItemsbyShippingPreference(fullorderlist, false);
        uxGrid2.DataSource = shipseperatelist;
        uxGrid.DataSource = shiptogetherlist;
        uxGrid.DataBind();
        uxGrid2.DataBind();
        if (AdvancedShipping)
        {
            decimal weight;
            decimal height;
            decimal length;
            decimal width;
            _ShippingAdmin.EstimatePackageSize(shiptogetherlist, out height, out width, out length, out weight);
            EstimatedHeight.Text = height.ToString();
            EstimatedLength.Text = length.ToString();
            EstimatedWeight.Text = weight.ToString();
            EstimatedWidth.Text = width.ToString();
     
        }
    }
    # endregion

    # region General Events

    /// <summary>
    /// Change Status Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ChangeStatus_Click(object sender, EventArgs e)
    {
        Response.Redirect(StatusPage + OrderID);
    }

    protected void Refund_Click(object sender, EventArgs e)
    {
        Response.Redirect(RefundPage + OrderID);
    }

    protected void List_Click(object sender, EventArgs e)
    {
        Response.Redirect(ListPage);
    }

    /// <summary>
    /// Order Shipping Button click event Creates or Cancels a shipment
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Shipping_Click(object sender, EventArgs e)
    {

        ShippingErrors.Text = string.Empty;
        OrderAdmin _OrderLineItemAdmin = new OrderAdmin();
        ZNode.Libraries.Admin.ShippingAdmin _ShippingAdmin = new ShippingAdmin();
        TList<OrderLineItem> fullorderlist = _OrderLineItemAdmin.GetOrderLineItemByOrderID(OrderID);
        TList<OrderLineItem> shiptogetherlist = new TList<OrderLineItem>();
        shiptogetherlist = _ShippingAdmin.GetLineItemsbyShippingPreference(fullorderlist, false);
        OrderTrackingNumber = lblTrackingNumber.Text;
        //Tracking Number is null or empty, no shipment exists. Create one.
        if (OrderTrackingNumber == string.Empty)
        {


            if (shiptogetherlist.Count > 0)
            {
                RequiredEstimatedHeight.Validate();
                RequiredEstimatedLength.Validate();
                RequiredEstimatedWeight.Validate();
                RequiredEstimatedWidth.Validate();
                EstimatedHeightValidator.Validate();
                EstimatedWeightValidator.Validate();
                EstimatedLengthValidator.Validate();
                EstimatedWidthValidator.Validate();
                if(IsValid)
                {
                _ShippingAdmin.Ship(shiptogetherlist, EstimatedHeight.Text, EstimatedWeight.Text, EstimatedLength.Text, EstimatedWidth.Text, false, Server.MapPath(filepath));
                }
            }
        }
        else
        {
            if (shiptogetherlist.Count > 0)
            {
                _ShippingAdmin.CancelShipment(shiptogetherlist, false);


            }
        }

        if (_ShippingAdmin.ErrorCode != "0")
        {
            ShippingErrors.Text = string.Format("Error Code: {0}. Error Message:{1}", _ShippingAdmin.ErrorCode, _ShippingAdmin.ErrorDescription);
        }

        this.BindData();
        this.BindGrid();
    }

    /// <summary>
    /// Label for Order shipment event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Label_Click(object sender, EventArgs e)
    {


        OrderTrackingNumber = lblTrackingNumber.Text;
        string filename = OrderTrackingNumber + ".pdf";
        if (OrderTrackingNumber == string.Empty)
        {
            //no tracking number
        }
        else
        {
            DownloadLabel(filename);

        }

    }

    # endregion

    # region Helper Funtions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="FieldValue"></param>
    /// <returns></returns>
    public string Formatprice(Object FieldValue)
    {
        string Price = String.Empty;

        if (FieldValue != null)
        {
            Price = String.Format("{0:c}", FieldValue);
        }
        return Price;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="FieldValue"></param>
    /// <returns></returns>
    public string GetOrderState(Object FieldValue)
    {
        string OrderStatus = " ";
        if (FieldValue != null)
        {
            ZNode.Libraries.Admin.OrderAdmin _OrderStateAdmin = new ZNode.Libraries.Admin.OrderAdmin();
            OrderState _orderStateList = _OrderStateAdmin.GetByOrderStateID(int.Parse(FieldValue.ToString()));

            OrderStatus = _orderStateList.OrderStateName.ToString();
        }
        return OrderStatus;
    }

    /// <summary>
    /// Returns shipping option name for this shipping Id
    /// </summary>
    /// <param name="FieldValue"></param>
    /// <returns></returns>
    public string GetShippingOptionName(int ShippingId)
    {
        string Name = "";

        ZNode.Libraries.Admin.ShippingAdmin shippingAdmin = new ZNode.Libraries.Admin.ShippingAdmin();
        Shipping entity = shippingAdmin.GetShippingOptionById(ShippingId);

        if (entity != null)
        {
            Name = entity.Description;
        }

        return Name;
    }

    /// <summary>
    /// Returns promotion code name
    /// </summary>
    /// <param name="FieldValue"></param>
    /// <returns></returns>
    //public string GetCouponCode(int couponId)
    //{
    //    //string Name = "";
    //    //ZNode.Libraries.Admin.CouponAdmin couponAdmin = new ZNode.Libraries.Admin.CouponAdmin();
    //    //Coupon entity = couponAdmin.GetByCouponID(couponId);

    //    //if (entity != null)
    //    //{
    //    //    Name = entity.CouponCode;
    //    //}

    //    //return Name;
    //}

    /// <summary>
    /// Returns payment type name for this payment type id
    /// </summary>
    /// <param name="FieldValue"></param>
    /// <returns></returns>
    public string GetPaymentTypeName(int PaymentTypeId)
    {
        string Name = "";

        ZNode.Libraries.Admin.StoreSettingsAdmin settingsAdmin = new ZNode.Libraries.Admin.StoreSettingsAdmin();
        PaymentType entity = settingsAdmin.GetPaymentTypeById(PaymentTypeId);

        if (entity != null)
        {
            Name = entity.Name;
        }

        return Name;
    }

    public bool isAdvancedShipping()
    {
        return AdvancedShipping;
    }

    /// <summary>
    /// Writes a label PDF to the browser.
    /// </summary>
    protected void DownloadLabel(string filename)
    {
        if (File.Exists(Server.MapPath(filepath) + filename))
        {
            FileStream MyFileStream = new FileStream(Server.MapPath(filepath) + filename, FileMode.Open);
            long FileSize;
            FileSize = MyFileStream.Length;
            byte[] Buffer = new byte[(int)FileSize];
            MyFileStream.Read(Buffer, 0, (int)MyFileStream.Length);
            MyFileStream.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename=" + filename);
            Response.BinaryWrite(Buffer);
            Response.End();
        }
    }

#endregion

    # region Grid Events

    /// <summary>
    /// Order Items Grid Page Index Changing Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        this.BindGrid();
    }



    /// <summary>
    /// Grid Button Command for Estimating Package Size, Creating Shipments, Cancelling Shipments, and Printing Labels
    /// </summary>
    protected void uxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Label")
        {
            DownloadLabel(e.CommandArgument.ToString() + ".pdf");
        }
        else
        {
            ShippingErrors.Text = string.Empty;
            ZNode.Libraries.Admin.ShippingAdmin _ShippingAdmin = new ShippingAdmin();
            OrderAdmin _OrderAdmin = new OrderAdmin();
            TList<OrderLineItem> items = _OrderAdmin.GetOrderLineItemByOrderID(OrderID);
            TList<OrderLineItem> singleshipitemlist = new TList<OrderLineItem>();

            foreach (OrderLineItem i in items)
            {
                if (i.OrderLineItemID.ToString() == e.CommandArgument.ToString())
                {
                    singleshipitemlist.Add(i);
                }
            }

            if (singleshipitemlist.Count > 0)
            {
                if (e.CommandName == "CreateShipment")
                {
                    
                   LineItemHeightValidator.Validate();
                    LineItemLengthValidator.Validate();
                    LineItemWeightValidator.Validate();
                    LineItemWidthValidator.Validate();
                    LineItemLengthRequired.Validate();
                    LineItemWeightRequired.Validate();
                    LineItemHeightRequired.Validate();
                    LineItemWidthRequired.Validate();
                    if (IsValid)
                    {
                        _ShippingAdmin.Ship(singleshipitemlist, LineItemHeight.Text, LineItemWeight.Text, LineItemLength.Text, LineItemWidth.Text, true, Server.MapPath(filepath));
                    }
                        //Clear out the hidden value for estimation so that the correct button shows next time
                    this.EstimatedLineItemID.Value = string.Empty;
                    this.EstimatedHeight.Text = string.Empty;
                    this.EstimatedLength.Text = string.Empty;
                    this.EstimatedWeight.Text = string.Empty;
                    this.EstimatedWidth.Text = string.Empty;

                    this.BindData();
                    this.BindGrid();
                }
                else if (e.CommandName == "CancelShipment")
                {

                    _ShippingAdmin.CancelShipment(singleshipitemlist, true);
                    this.BindData();
                    this.BindGrid();
                }
                else if (e.CommandName == "EstimateDimensions")
                {
                    decimal weight;
                    decimal height;
                    decimal length;
                    decimal width;
                    _ShippingAdmin.EstimatePackageSize(singleshipitemlist, out height, out width, out length, out weight);
                    LineItemHeight.Text = height.ToString();
                    LineItemLength.Text = length.ToString();
                    LineItemWeight.Text = weight.ToString();
                    LineItemWidth.Text = width.ToString();
                    
                    
                    //This gets the row where the button was pushed
                    GridViewRow selectedRow = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    //Store the line item ID so when we reload the page we know what button to update to "Create Shipment"
                    EstimatedLineItemID.Value = selectedRow.Cells[0].Text;


                    
                    this.BindData();
                    this.BindGrid();
                    
                }
            }

            if (_ShippingAdmin.ErrorCode != "0")
            {
                ShippingErrors.Text = string.Format("Error Code: {0}. Error Message:{1}", _ShippingAdmin.ErrorCode, _ShippingAdmin.ErrorDescription);
            }
        }
    }

    /// <summary>
    /// Sets grid button Text
    /// </summary>
    public string ButtonText(object trackingnumber, object orderlineitemid)
    {
        if (orderlineitemid != null)
        {
            //If the value for this row matches the just estimated line item we set the command to create

            if (orderlineitemid.ToString() == EstimatedLineItemID.Value)
            {
                return "Create Shipment";
            }
            if (trackingnumber != null)
            {
                if (trackingnumber.ToString().Length > 0)
                {
                    return "Cancel Shipment";
                }
            }

            return "Estimate Dimensions";
        }
        return string.Empty;
    }

    /// <summary>
    /// Sets the Command for the Ship seperately grid
    /// </summary>
    public string ShippingCommand(object trackingnumber,object orderlineitemid)
    {
      

        if (orderlineitemid != null)
        {
            //If the value for this row matches the just estimated line item we set the command to create

            if (orderlineitemid.ToString() == EstimatedLineItemID.Value)
            {
                return "CreateShipment";
            }

            if (trackingnumber != null)
            {
                if (trackingnumber.ToString().Length > 0)
                {
                    return "CancelShipment";
                }
            }

            return "EstimateDimensions";
        }
        return string.Empty;
    }

    /// <summary>
    /// Checks to see if a label exists before displaying the label button
    /// </summary>
    public bool ShowLabelButton(object value)
    {
        if (value != null)
        {
            if (value.ToString().Length > 0)
            {
                if (File.Exists(Server.MapPath(filepath) + value.ToString() + ".pdf") && AdvancedShipping)
                {
                    return true;
                }

            }
        }


        return false;
    }

   


    # endregion

}


