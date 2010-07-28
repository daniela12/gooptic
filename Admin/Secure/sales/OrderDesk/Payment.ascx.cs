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
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.ECommerce.ShoppingCart;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.DataAccess;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.ECommerce.Fulfillment;

/// <summary>
/// Checkout Payment
/// </summary>
public partial class Admin_Secure_Enterprise_OrderDesk_Payment : System.Web.UI.UserControl
{
    #region Member Variables
    private ZNodePayment _payment;    
    private bool _isShippingOptionValid = false;
    #endregion

    # region Public Events
    public System.EventHandler ShippingSelectedIndexChanged;    
    # endregion

    #region Page Load
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (lstPaymentType.Items.Count == 0)
        {
            BindPaymentTypeData();
        }

        if (lstYear.Items.Count < 1)
        {
            BindYearList();
        }
    }
    #endregion   

    #region Public Properties
    /// <summary>
    /// Retrieves or sets the Account Object
    /// </summary>
    public Account UserAccount
    {
        get
        {
            if (ViewState["AccountObject"] != null)
            {
                //the account info with 'AccountObject' is retrieved from the session and
                //It is converted to a Entity Account object
                return (Account)ViewState["AccountObject"];
            }

            return new Account();
        }
        set
        {
            //Customer account object is placed in the session and assigned a key, AccountObject.            
            ViewState["AccountObject"] = value;
        }
    }

    /// <summary>
    /// Hides or shows the Payment section on the final checkout page
    /// </summary>
    public bool ShowPaymentSection
    {
        set { pnlPayment.Visible = value; }
    }

    /// <summary>
    /// Returns false, if there is no payment options exists for this profile    
    /// </summary>
    public bool IsPaymentOptionExists
    {
        get
        {            
            if (lstPaymentType.Items.Count == 0)
                return false;

            return true;
        }
    }

    /// <summary>
    /// Retrieves or sets the shipping option valid property
    /// </summary>
    public bool IsShippingOptionValid
    {
        get { return _isShippingOptionValid; }
        set { _isShippingOptionValid = value; }
    }

    public ZNodePayment Payment
    {
        get
        {           

            //check payment type            
            if (PaymentType == ZNode.Libraries.Payment.PaymentType.CREDIT_CARD)
            {
                ZNodePayment _creditCardPayment = new ZNodePayment();
                _creditCardPayment.CreditCard.CardNumber = txtCreditCardNumber.Text.Trim();
                _creditCardPayment.CreditCard.CreditCardExp = lstMonth.SelectedValue + "/" + lstYear.SelectedValue;
                _creditCardPayment.CreditCard.CardSecurityCode = txtCVV.Text.Trim();
                _payment = _creditCardPayment;                
                
            }
            else if (PaymentType == ZNode.Libraries.Payment.PaymentType.PURCHASE_ORDER)
            {
                ZNodePayment _purchaseOrderPayment = new ZNodePayment();
                _payment = _purchaseOrderPayment;
            }
            else if (PaymentType == ZNode.Libraries.Payment.PaymentType.COD)
            {
                ZNodePayment _chargeOnDeliveryPayment = new ZNodePayment();
                _payment = _chargeOnDeliveryPayment;
            }


            if(lstPaymentType.Items.Count > 0)
                //Set payment display name
                _payment.PaymentName = lstPaymentType.SelectedItem.Text; 

            return _payment;
        }
        set
        {
            _payment = value;
        }
    }

    /// <summary>
    /// Returns the payment type selected
    /// </summary>
    public ZNode.Libraries.Payment.PaymentType PaymentType
    {
        get
        {
            int paymentTypeID = 0;

            if (int.TryParse(lstPaymentType.SelectedValue, out paymentTypeID))
            {
                PaymentSettingService _pmtServ = new PaymentSettingService();
                ZNode.Libraries.DataAccess.Entities.PaymentSetting pmtSetting = _pmtServ.DeepLoadByPaymentSettingID(int.Parse(lstPaymentType.SelectedValue), true, DeepLoadType.IncludeChildren, typeof(ZNode.Libraries.DataAccess.Entities.PaymentType));
                int _paymentTypeID = pmtSetting.PaymentTypeID;

                if (_paymentTypeID == (int)ZNode.Libraries.Payment.PaymentType.CREDIT_CARD)
                {
                    return ZNode.Libraries.Payment.PaymentType.CREDIT_CARD;
                }
                else if (_paymentTypeID == (int)ZNode.Libraries.Payment.PaymentType.PURCHASE_ORDER)
                {
                    return ZNode.Libraries.Payment.PaymentType.PURCHASE_ORDER;
                }
                else if (_paymentTypeID == (int)ZNode.Libraries.Payment.PaymentType.COD)
                {
                    return ZNode.Libraries.Payment.PaymentType.COD;
                }
            }

            return ZNode.Libraries.Payment.PaymentType.CREDIT_CARD;
        }
    }

    /// <summary>
    /// Returns the payment settingId associated with the payment type selected
    /// </summary>
    public int PaymentSettingID
    {
        get
        {
            return int.Parse(lstPaymentType.SelectedValue);
        }
        set
        {
            lstPaymentType.SelectedValue = value.ToString();
        }
    }

    /// <summary>
    /// Returns the PO number if purchase Order payment Method selected
    /// </summary>
    public string PurchaseOrderNumber
    {
        get
        {
            return txtPONumber.Text.Trim();
        }
    }

    /// <summary>
    /// Returns the additional instruction
    /// </summary>
    public string AdditionalInstructions 
    {
        get
        {
            return txtAdditionalInstructions.Text.Trim();
        }      
    }

    # region Shipping Option Related Properties
    /// <summary>
    /// Get Shipping Id
    /// </summary>
    public int ShippingID
    {
        get 
        {
            if (lstShipping.SelectedIndex != -1)
            {
                int.Parse(lstShipping.SelectedValue);
            }

            return 0;
        }
    }
    # endregion

    #endregion

    #region Private Methods
    /// <summary>
    /// Binds the expiration year list based on current year
    /// </summary>
    private void BindYearList()
    {
        ListItem defaultItem = new ListItem("-- Year --","");
        lstYear.Items.Add(defaultItem);
        defaultItem.Selected = true;
        
        int currentYear = System.DateTime.Now.Year;
        int counter = 25;
           
        do
        {
          string itemtext = currentYear.ToString();

          lstYear.Items.Add(new ListItem(itemtext));

          currentYear = currentYear + 1;

          counter = counter - 1;

        } while (counter > 0);           
    }

    /// <summary>
    /// Binds the payment types
    /// </summary>
    public void BindPaymentTypeData()
    {
        Account userAccount = UserAccount;

        if (lstPaymentType.Items.Count == 0)
        {
            PaymentSettingService _pmtServ = new PaymentSettingService();
            ZNode.Libraries.DataAccess.Entities.TList<PaymentSetting> _pmtSetting = _pmtServ.GetAll();
            int profileID = 0;

            //Check user Session
            if (userAccount.ProfileID.HasValue)
            {                
                profileID = userAccount.ProfileID.Value;
            }

            if (profileID > 0)
            {
                _pmtSetting.ApplyFilter(delegate(ZNode.Libraries.DataAccess.Entities.PaymentSetting pmt) { return (pmt.ProfileID == null || pmt.ProfileID == profileID) && (pmt.PaymentTypeID != 2 && pmt.PaymentTypeID != 3) && pmt.ActiveInd == true; });
            }
            else
            {
                _pmtSetting.ApplyFilter(delegate(ZNode.Libraries.DataAccess.Entities.PaymentSetting pmt) { return (pmt.ProfileID == null) && (pmt.PaymentTypeID != 2 && pmt.PaymentTypeID != 3)  && pmt.ActiveInd == true; });
            }
            _pmtServ.DeepLoad(_pmtSetting, true, DeepLoadType.IncludeChildren, typeof(ZNode.Libraries.DataAccess.Entities.PaymentType));
            _pmtSetting.Sort("DisplayOrder");

            foreach (PaymentSetting _pmt in _pmtSetting)
            {
                ListItem li = new ListItem();
                li.Text = _pmt.PaymentTypeIDSource.Name;
                li.Value = _pmt.PaymentSettingID.ToString();

                if (_pmt.PaymentTypeID != (int)ZNode.Libraries.Payment.PaymentType.GOOGLE_CHECKOUT || _pmt.PaymentTypeID != (int)ZNode.Libraries.Payment.PaymentType.PAYPAL)
                {
                    lstPaymentType.Items.Add(li);    
                }

                if (_pmt.PaymentTypeID == (int)ZNode.Libraries.Payment.PaymentType.CREDIT_CARD)
                {
                    imgAmex.Visible = (bool)_pmt.EnableAmex;
                    imgMastercard.Visible = (bool)_pmt.EnableMasterCard;
                    imgVisa.Visible = (bool)_pmt.EnableVisa;
                    imgDiscover.Visible = (bool)_pmt.EnableDiscover;
                }
            }

            //select first item
            if (lstPaymentType.Items.Count > 0) { lstPaymentType.Items[0].Selected = true; }

            //show appropriate payment control
            SetPaymentControl();
        }

    }

    /// <summary>
    /// Shows the appropriate payment control based on the option selected
    /// </summary>
    public void SetPaymentControl()
    {
         int paymentTypeID = 0;
         //show credit card panel
         pnlCreditCard.Visible = false;

         if (int.TryParse(lstPaymentType.SelectedValue, out paymentTypeID))
         {
             PaymentSettingService _pmtServ = new PaymentSettingService();
             ZNode.Libraries.DataAccess.Entities.PaymentSetting pmtSetting = _pmtServ.DeepLoadByPaymentSettingID(int.Parse(lstPaymentType.SelectedValue), true, DeepLoadType.IncludeChildren, typeof(ZNode.Libraries.DataAccess.Entities.PaymentType));

             int paymentTypeId = pmtSetting.PaymentTypeID;

             if (paymentTypeId == (int)ZNode.Libraries.Payment.PaymentType.CREDIT_CARD)
             {
                 //Hide purchase order panel
                 pnlPurchaseOrder.Visible = false;
                 //show credit card panel
                 pnlCreditCard.Visible = true;
             }
             else if (paymentTypeId == (int)ZNode.Libraries.Payment.PaymentType.PURCHASE_ORDER)
             {
                 //hide credit card panel
                 pnlCreditCard.Visible = false;
                 //show purchase order panel
                 pnlPurchaseOrder.Visible = true;
             }
             else if (paymentTypeId == (int)ZNode.Libraries.Payment.PaymentType.COD)
             {
                 //Hide credit card panel
                 pnlCreditCard.Visible = false;
                 //hide purchase order panel
                 pnlPurchaseOrder.Visible = false;
             }
         }
    }
    #endregion

    # region Bind Methods
    /// <summary>
    /// 
    /// </summary>
    public void BindShipping()
    {
        if (UserAccount.AccountID > 0)
        {
            if (lstShipping.Items.Count == 0)
            {

                Account userAccount = UserAccount;

                int profileID = userAccount.ProfileID.Value;

                ShippingService shipServ = new ShippingService();
                TList<Shipping> shippingList = shipServ.GetAll();
                shippingList.Sort("DisplayOrder Asc");
                shippingList.ApplyFilter(delegate(ZNode.Libraries.DataAccess.Entities.Shipping shipping) { return (shipping.ActiveInd == true && (shipping.ProfileID == null || shipping.ProfileID == profileID)); });

                DataSet ds = shippingList.ToDataSet(false);
                DataView dv = new DataView(ds.Tables[0]);

                if (userAccount.BillingCountryCode == userAccount.ShipCountryCode)
                {
                    dv.RowFilter = "DestinationCountryCode = '" + userAccount.BillingCountryCode + "' or DestinationCountryCode is null";
                }
                else
                {
                    dv.RowFilter = "DestinationCountryCode = '" + userAccount.ShipCountryCode + "' or DestinationCountryCode is null";
                }

                if (dv.ToTable().Rows.Count > 0)
                {
                    foreach (DataRow dr in dv.ToTable().Rows)
                    {
                        string description = dr["Description"].ToString();

                        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("&reg;");
                        description = regex.Replace(description, "®");
                        ListItem li = new ListItem(description, dr["ShippingID"].ToString());
                        lstShipping.Items.Add(li);
                    }

                    lstShipping.SelectedIndex = 0;
                }
            }
        }
    }

    /// <summary>
    /// Clears the collection of items in the list control
    /// </summary>
    public void ClearUI()
    {
        lstShipping.Items.Clear();
        lstPaymentType.Items.Clear();
    }
    # endregion

    #region Events
    /// <summary>
    /// Payment type selected index changed event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetPaymentControl();
    }

    /// <summary>
    /// Shipping option selected index changed event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstShipping_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalculateShipping();

        if (ShippingSelectedIndexChanged != null)
        {
            this.ShippingSelectedIndexChanged(sender, e);
        }
    }
    #endregion     

    # region Helper Methods
    /// <summary>
    /// Re-calculate shipping cost
    /// </summary>
    public void CalculateShipping()
    {
        ZNodeShoppingCart _shoppingCart = ZNodeShoppingCart.CurrentShoppingCart();

        if (_shoppingCart != null)
        {
            if (lstShipping.Items.Count > 0)
            {
                int shippingID = int.Parse(lstShipping.SelectedValue);

                // set shipping name in shopping cart object
                _shoppingCart.Shipping.ShippingName = lstShipping.SelectedItem.Text;
                _shoppingCart.Shipping.ShippingID = shippingID;

                // calculate shipping cost
                _shoppingCart.Calculate();

                uxErrorMsg.Text = _shoppingCart.ErrorMessage;
            }
        }
    }
    # endregion
}
