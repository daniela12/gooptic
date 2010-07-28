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
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.ECommerce.ShoppingCart;
using ZNode.Libraries.ECommerce.Fulfillment;
using ZNode.Libraries.ECommerce.UserAccount;

public partial class Admin_Secure_sales_OrderDesk_Default : System.Web.UI.Page
{
    #region Private Variables
    ZNodeShoppingCart _shoppingCart;
    ZNodeCheckout _checkout = null;
    #endregion

    # region Public Properties
    /// <summary>
    /// Retrieves or Sets the Account Object from/to Cache
    /// </summary>
    public Account UserAccount
    {
        get
        {
            if (ViewState["AccountObject"] != null)
            {
                //the account info with 'AccountObject' is retrieved from the cache using Get Method and
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
    /// Retrieves the Customer Billing address
    /// </summary>
    public ZNodeAddress BillingAddress
    {
        get
        {
            ZNodeAddress _billingAddress = new ZNodeAddress();
            Account _account = UserAccount;

            if (_account.AccountID > 0)
            {
                _billingAddress.FirstName = _account.BillingFirstName;
                _billingAddress.LastName = _account.BillingLastName;
                _billingAddress.PhoneNumber = _account.BillingPhoneNumber;
                _billingAddress.Street1 = _account.BillingStreet;
                _billingAddress.Street2 = _account.BillingStreet1;
                _billingAddress.StateCode = _account.BillingStateCode;
                _billingAddress.PostalCode = _account.BillingPostalCode;
                _billingAddress.EmailId = _account.BillingEmailID;
                _billingAddress.CompanyName = _account.BillingCompanyName;
                _billingAddress.City = _account.BillingCity;
                _billingAddress.CountryCode = _account.BillingCountryCode;
            }

            return _billingAddress;
        }

    }

    /// <summary>
    ///  Retrieves the Customer shipping address
    /// </summary>
    public ZNodeAddress ShippingAddress
    {
        get
        {
            Account _account = UserAccount;
            ZNodeAddress _shippingAddress = new ZNodeAddress();

            if (_account.AccountID > 0)
            {
                _shippingAddress.FirstName = _account.ShipFirstName;
                _shippingAddress.LastName = _account.ShipLastName;
                _shippingAddress.CompanyName = _account.ShipCompanyName;
                _shippingAddress.PhoneNumber = _account.ShipPhoneNumber;
                _shippingAddress.Street1 = _account.ShipStreet;
                _shippingAddress.Street2 = _account.ShipStreet1;
                _shippingAddress.City = _account.ShipCity;
                _shippingAddress.PostalCode = _account.ShipPostalCode;
                _shippingAddress.StateCode = _account.ShipStateCode;
                _shippingAddress.EmailId = _account.ShipEmailID;
                _shippingAddress.CountryCode = _account.ShipCountryCode;
            }

            return _shippingAddress;
        }
    }

    # endregion

    # region Protected Properties
    /// <summary>
    /// 
    /// </summary>
    private string ProfileName
    {
        get
        {
            string _profileName = string.Empty;
            Account _account = UserAccount;

            if (_account.ProfileID.HasValue)
            {
                ProfileService profileService = new ProfileService();
                Profile profileEntity = profileService.GetByProfileID(_account.ProfileID.Value);

                _profileName = profileEntity.Name;
            }

            return _profileName;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    protected string LoginName
    {
        get
        {
            string _loginName = string.Empty;
            Account _account = UserAccount;

            if (_account.UserID.HasValue)
            {
                MembershipUser user = Membership.GetUser(_account.UserID.Value);

                if (user != null)
                {
                    _loginName = user.UserName;
                }
            }

            return _loginName;
        }
    }

    # endregion

    # region Page Events

    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Browser.Browser.Equals("IE"))
        {
            UpdateProgressIE.Visible = true;
        }
        else if (Request.Browser.Browser.Equals("Firefox"))
        {
            UpdateProgressMozilla.Visible = true;
        }
        else
        {
            UpdateProgressMozilla.Visible = true;
        }


        //registers the event for the customer search (child) control
        this.uxCustomerSearch.SelectedIndexChanged += new EventHandler(FoundUsers_SelectedIndexChanged);

        //registers the event for the create user (child) control
        this.uxCreateUser.ButtonClick += new EventHandler(btnCancel_Click);

        //registers the event for the create user (child) control
        this.uxCustomerSearch.SearchButtonClick += new EventHandler(btnSearchClose_Click);

        //registers the event for the quick Order (child) control - Addto cart button
        this.uxQuickOrder.SubmitButtonClicked += new EventHandler(AddToCart_Click);

        //registers the event for the quick Order (child) control - Addto cart button
        this.uxQuickOrder.CancelButtonClicked += new EventHandler(AddToCart_Click);

        //registers the event for the quick Order (child) control - Add another product button
        this.uxQuickOrder.AddProductButtonClicked += new EventHandler(AddAnotherProduct_Click);

        //registers the event for the payment (child) control - Shipping Type selected change list
        this.uxPayment.ShippingSelectedIndexChanged += new EventHandler(Shipping_SelectedIndexChanged);

        if (!Page.IsPostBack)
        {
            _shoppingCart = (ZNodeShoppingCart)ZNodeShoppingCart.CreateFromSession(ZNodeSessionKeyType.ShoppingCart);

            if (_shoppingCart != null)
            {
                _shoppingCart.EmptyCart();
            }

            // Bind fields
            Bind();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreRender(object sender, EventArgs e)
    {
        _shoppingCart = (ZNodeShoppingCart)ZNodeShoppingCart.CreateFromSession(ZNodeSessionKeyType.ShoppingCart);


        if (_shoppingCart != null)
        {
            _shoppingCart.Payment = uxPayment.Payment;

            if (UserAccount.AccountID > 0)
            {
                uxPayment.UserAccount = UserAccount;
                _shoppingCart.Payment.BillingAddress = BillingAddress;
                _shoppingCart.Payment.ShippingAddress = ShippingAddress;
            }

            if (_shoppingCart.ShoppingCartItems.Count > 0 && UserAccount.AccountID > 0)
            {
                pnlPaymentShipping.Visible = true;
                CalculateTaxes(_shoppingCart); //Calculate tax if Customer selected
            }

            if (_shoppingCart.ShoppingCartItems.Count > 0)
            {
                uxPayment.CalculateShipping(); //Calculate shipping                
                uxShoppingCart.Visible = true;

                uxShoppingCart.ShoppingCartObject = _shoppingCart;
                uxShoppingCart.Bind();//Bind Shopping Cart items
            }
            else
            {
                uxShoppingCart.Visible = false;
            }

            if (_shoppingCart.Total == 0)
            {
                uxPayment.ShowPaymentSection = false;
            }
            else
            {
                uxPayment.ShowPaymentSection = true;
            }
        }
        else
        {
            uxShoppingCart.Visible = false;
        }
    }

    # endregion

    # region Bind Methods
    /// <summary>
    /// Bind Customer section and Shopping cart 
    /// </summary>
    private void Bind()
    {

        if (UserAccount.AccountID > 0)
        {
            ui_lblProfile.Text = ProfileName;
            ui_lblUserID.Text = LoginName;
            ui_BillingAddress.Text = "";
            ui_ShippingAddress.Text = "";

            try
            {
                uxPayment.UserAccount = UserAccount;
                uxPayment.BindShipping();
                uxPayment.BindPaymentTypeData();
                ui_pnlAcctInfo.Visible = true;
                ui_BillingAddress.Text = BillingAddress.ToString();
                ui_ShippingAddress.Text = ShippingAddress.ToString();

                //Hide the edit button if the seleted Customer is Admin user
                Account _account = UserAccount;
                MembershipUser _user = Membership.GetUser(_account.UserID.Value);

                string roleList = "";
                //Get roles for this User account
                string[] roles = Roles.GetRolesForUser(_user.UserName);

                foreach (string Role in roles)
                {
                    roleList += Role + "<br>";
                }
                string rolename = roleList;
                //Hide the Delete button if a NonAdmin user has entered this page
                if (!Roles.IsUserInRole(HttpContext.Current.User.Identity.Name, "ADMIN"))
                {
                    if (Roles.IsUserInRole(_user.UserName, "ADMIN"))
                    {
                        ui_Edit.Visible = false;
                    }
                    else if (Roles.IsUserInRole(HttpContext.Current.User.Identity.Name, "CUSTOMER SERVICE REP"))
                    {
                        if (rolename == Convert.ToString("USER<br>") || rolename == Convert.ToString(""))
                        {
                            ui_Edit.Visible = true;
                        }
                        else
                        {
                            ui_Edit.Visible = false;
                        }
                    }
                }
            }
            catch { }
        }
        else
        {
            ui_pnlAcctInfo.Visible = false;
            ui_lblProfile.Text = "";
            ui_lblUserID.Text = "";
            ui_BillingAddress.Text = "";
            ui_ShippingAddress.Text = "";
        }
    }
    # endregion

    # region Events
    /// <summary>
    /// Fires when Cancel Button Click triggered by child control (Create User)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        UserAccount = uxCreateUser.UserAccount;
        Bind();

        UpdatePanelOrderDesk.Update();
        mdlCreateUserPopup.Hide();
    }

    /// <summary>
    /// Search Close button Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearchClose_Click(object sender, EventArgs e)
    {
        Bind();

        mdlPopup.Hide();
        UpdatePanelOrderDesk.Update();

    }

    /// <summary>
    /// Cancel Order Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelOrder_Click(object sender, EventArgs e)
    {
        _shoppingCart = ZNodeShoppingCart.CurrentShoppingCart();

        if (_shoppingCart != null)
        {
            _shoppingCart.EmptyCart();
            _shoppingCart = null;

            Session.Remove(ZNodeSessionKeyType.ShoppingCart.ToString());
        }

        ResetProfileCache();

        Response.Redirect("~/admin/Secure/default.aspx");

    }

    /// <summary>
    /// Add To cart Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddToCart_Click(object sender, EventArgs e)
    {
        Bind();
        UpdatePanelOrderDesk.Update();
    }

    /// <summary>
    /// Search Customer Link Button click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linkSearchCustomer_Click(object sender, EventArgs e)
    {
        uxCustomerSearch.ClearUI();
        mdlPopup.Show();
    }


    /// <summary>
    /// Create new User link button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linkNewCustomer_Click(object sender, EventArgs e)
    {
        //Reset Account object
        UserAccount = new Account();
        uxCreateUser.UserAccount = new Account();
        uxPayment.ClearUI();
        //Clear previous values bounded with the text box fields
        uxCreateUser.Bind();
        mdlCreateUserPopup.Show();
    }

    /// <summary>
    /// Quick Order Popup Link button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linkbtnQuickOrder_Click(object sender, EventArgs e)
    {
        uxQuickOrder.Product = new ZNodeProduct();
        uxQuickOrder.Bind();
        UpdatePanelQuickOrder.Update();
        mdlQuickOrderPopup.Show();
    }

    /// <summary>
    /// Quick Order Popup Link button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddAnotherProduct_Click(object sender, EventArgs e)
    {
        uxQuickOrder.Product = new ZNodeProduct();
        uxQuickOrder.Bind();
        UpdatePanelQuickOrder.Update();
        mdlQuickOrderPopup.Show();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void FoundUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        uxPayment.ClearUI();
        UserAccount = uxCustomerSearch.UserAccount;
        Bind();
        mdlPopup.Hide();
        UpdatePanelOrderDesk.Update();
    }

    /// <summary>
    /// Shipping Type list selected index changed event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Shipping_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind();
        UpdatePanelOrderDesk.Update();
    }

    /// <summary>
    /// Edit Customer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ui_Edit_Click(object sender, EventArgs e)
    {
        if (UserAccount.AccountID > 0)
        {
            uxCreateUser.UserAccount = UserAccount;
            uxCreateUser.Bind();
            UpdatePanelOrderDesk.Update();
            mdlCreateUserPopup.Show();

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        _shoppingCart = ZNodeShoppingCart.CurrentShoppingCart();

        if (_shoppingCart != null)
        {
            _shoppingCart.EmptyCart();
            _shoppingCart = null;

            Session.Remove(ZNodeSessionKeyType.ShoppingCart.ToString());
        }

        ResetProfileCache();

        if (Page.Session["AccountObject"] != null)
        {
            //Remove account object
            Page.Session.Remove("AccountObject");
        }

        Response.Redirect("~/admin/Secure/default.aspx");
    }

    /// <summary>
    /// Fires when New Customer link is triggered
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linkNewOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/sales/OrderDesk/OrderDesk.aspx");
    }

    /// <summary>
    /// Submit Order 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Submit_Click(object sender, EventArgs e)
    {
        # region Local Variables
        //Get ShoppingCart object from current session
        _shoppingCart = ZNodeShoppingCart.CurrentShoppingCart();
        ZNodeOrderFullfillment order = new ZNodeOrderFullfillment();
        ZNodeUserAccount _userAccount = new ZNodeUserAccount();
        # endregion

        if (_shoppingCart != null)
        {
            //Check no.of items in the shopping cart object
            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                lblError.Text = "There are no items in the shopping cart. Please add items to your shopping cart before checking out.";
                return;
            }

            if (!uxPayment.IsPaymentOptionExists)
            {
                lblError.Text = "No payment options have been setup for this profile in the database. Please use the admin to setup valid payment options first.";
                return;
            }

            try
            {
                // Retrieve Account object from Cache object
                Account _account = UserAccount;

                if (_account.AccountID == 0)
                {
                    btnSubmit.Enabled = true;
                    lblError.Text = "Customer information is incomplete.";
                    return;
                }

                // User Settings
                _userAccount.AccountID = _account.AccountID;
                if (_account.ProfileID.HasValue)
                {
                    _userAccount.ProfileID = _account.ProfileID.Value;
                }
                if (_account.PortalID.HasValue)
                {
                    _userAccount.PortalID = _account.PortalID.Value;
                }
                if (_account.ParentAccountID.HasValue)
                {
                    _userAccount.ParentAccountID = _account.ParentAccountID.Value;
                }
                _userAccount.UpdateUser = _account.UpdateUser;
                _userAccount.UserID = _account.UserID;
                _userAccount.CreateUser = _account.CreateUser;

                // set address
                _userAccount.BillingAddress = BillingAddress;
                _userAccount.ShippingAddress = ShippingAddress;


                if ((BillingAddress.Street1.Length == 0) || (BillingAddress.City.Length == 0) || (BillingAddress.PostalCode.Length == 0) || (BillingAddress.StateCode.Length == 0))
                {
                    lblError.Text = "Customer address fields are incomplete.";
                    return;
                }

                // get data from user controls
                GetControlData(_userAccount);


                _checkout.UserAccount = _userAccount;

                if (_checkout.ShoppingCart.PreSubmitOrderProcess())
                {
                    order = _checkout.SubmitOrder();
                }
                else
                {
                    lblError.Text = _checkout.ShoppingCart.ErrorMessage;
                    return;
                }

            }
            catch (ZNodePaymentException ex)
            {
                // display payment error message
                lblError.Text = ex.Message;
                return;
            }
            catch
            {
                // display error page
                lblError.Text = "Could not submit your order. Please contact customer support.";
                return;
            }

            if (_checkout.IsSuccess)
            {
                // Receipt 
                uxConfirm.Order = order;
                uxConfirm.ReceiptText = ZNodeConfigManager.MessageConfig.GetMessage("OrderReceiptConfirmationIntroText");
                uxConfirm.SiteName = ZNodeConfigManager.SiteConfig.CompanyName;
                uxConfirm.CustomerServiceEmail = ZNodeConfigManager.SiteConfig.CustomerServiceEmail;
                uxConfirm.CustomerServicePhoneNumber = ZNodeConfigManager.SiteConfig.CustomerServicePhoneNumber;

                uxConfirm.GenerateReceipt();

                // Update product inventory and coupon
                OnSubmitOrder(order, _shoppingCart);

                // empty cart & remove selected user
                Session.Remove(ZNodeSessionKeyType.ShoppingCart.ToString());

                ResetProfileCache();

                //Hide Other sections
                pnlOrderDesk.Visible = false;
                btnCancelOrder.Visible = false;
                btnSubmitOrder.Visible = false;
                pnlReceipt.Visible = true;
                pnlCreateOrderLink.Visible = true;
                UpdatePanelOrderDesk.Update();
            }
            else
            {
                //display error page
                lblError.Text = _checkout.PaymentResponseText;
                return;
            }
        }
        else
        {
            lblError.Text = "There are no items in the shopping cart. Please add items to your shopping cart before checking out.";
            return;
        }
    }
    # endregion

    # region Helper Methods
    /// <summary>
    /// Calculate taxes
    /// </summary>
    private void CalculateTaxes(ZNodeShoppingCart _shoppingCart)
    {
        Account userAccount = UserAccount;

        if (userAccount.AccountID > 0)
        {
            _shoppingCart.Payment.BillingAddress = BillingAddress;
            _shoppingCart.Calculate();
        }
    }

    /// <summary>
    /// This method get data from user controls(payment and shippping info)
    /// </summary>
    /// <param name="_userAccount"></param>
    protected void GetControlData(ZNodeUserAccount _userAccount)
    {
        // Get objects from session
        _checkout = new ZNodeCheckout();

        // Set payment data in checkout object
        _checkout.ShoppingCart.Payment = uxPayment.Payment;
        _checkout.ShoppingCart.Payment.BillingAddress = BillingAddress;
        _checkout.ShoppingCart.Payment.ShippingAddress = ShippingAddress;
        _checkout.UserAccount = new ZNodeUserAccount();
        _checkout.UserAccount.BillingAddress = BillingAddress;
        _checkout.UserAccount.ShippingAddress = ShippingAddress;
        _checkout.PaymentSettingID = uxPayment.PaymentSettingID;
        _checkout.UpdateUserInfo = false;
        _checkout.PurchaseOrderNumber = uxPayment.PurchaseOrderNumber;
        _checkout.AdditionalInstructions = uxPayment.AdditionalInstructions;
        uxPayment.CalculateShipping();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="Order"></param>
    /// <param name="ShoppingCart"></param>
    private void OnSubmitOrder(ZNodeOrderFullfillment Order, ZNodeShoppingCart ShoppingCart)
    {
        ZNode.Libraries.ECommerce.Suppliers.ZNodeSupplierOption supplierOption = new ZNode.Libraries.ECommerce.Suppliers.ZNodeSupplierOption();

        supplierOption.SubmitOrder(Order, ShoppingCart);

        # region Set Digital Asset
        int Counter = 0;
        DigitalAssetService digitalAssetService = new DigitalAssetService();

        //Loop through the Order Line Items
        foreach (OrderLineItem orderLineItem in Order.OrderLineItems)
        {
            ZNodeShoppingCartItem shoppingCartItem = ShoppingCart.ShoppingCartItems[Counter++];

            // set quantity ordered
            int qty = shoppingCartItem.Quantity;

            // set product id
            int productId = shoppingCartItem.Product.ProductID;

            ZNodeGenericCollection<ZNodeDigitalAsset> AssignedDigitalAssets = new ZNodeGenericCollection<ZNodeDigitalAsset>();
            // get Digital assets for productid and quantity
            AssignedDigitalAssets = (ZNodeDigitalAssetList.CreateByProductIdAndQuantity(productId, qty)).DigitalAssetCollection;

            // Loop through the digital asset retrieved for this product
            foreach (ZNodeDigitalAsset digitalAsset in AssignedDigitalAssets)
            {
                DigitalAsset entity = digitalAssetService.GetByDigitalAssetID(digitalAsset.DigitalAssetID);
                entity.OrderLineItemID = orderLineItem.OrderLineItemID; //Set OrderLineitemId property
                digitalAssetService.Update(entity);//Update digital asset to the database
            }

            // Set retrieved digital asset collection to shopping product object
            // if product has digital assets, it will display it on the receipt page along with the product name
            shoppingCartItem.Product.ZNodeDigitalAssetCollection = AssignedDigitalAssets;
        }
        #endregion

        ShoppingCart.PostSubmitOrderProcess();
    }

    /// <summary>
    /// 
    /// </summary>
    private void ResetProfileCache()
    {
        // Reset current cached profile cache object with logged in user profile.
        if (System.Web.HttpContext.Current.Session["ProfileCache"] != null)
        {
            ZNode.Libraries.DataAccess.Entities.Profile _profile = (ZNode.Libraries.DataAccess.Entities.Profile)System.Web.HttpContext.Current.Session["ProfileCache"];

            ZNodeUserAccount _userAccount = ZNodeUserAccount.CurrentAccount();

            if (_profile.ProfileID != _userAccount.ProfileID)
            {
                ZNode.Libraries.DataAccess.Service.ProfileService profileService = new ZNode.Libraries.DataAccess.Service.ProfileService();
                ZNode.Libraries.DataAccess.Entities.Profile profile = profileService.GetByProfileID(_userAccount.ProfileID);

                System.Web.HttpContext.Current.Session["ProfileCache"] = profile;
            }
        }
    }
    # endregion
}
