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
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.ECommerce.UserAccount;

public partial class Admin_Secure_Enterprise_OrderDesk_CreateUser : System.Web.UI.UserControl
{
    #region Public Variables
    //Public Event Handler
    public event System.EventHandler ButtonClick;
    # endregion

    # region Protected Member Variables
    private ZNodeAddress _billingAddress = new ZNodeAddress();
    private ZNodeAddress _shippingAddress = new ZNodeAddress();
    # endregion

    # region Public Properties
    public Account UserAccount
    {
        get
        {
            if (ViewState["AccountObject"] != null)
            {
                //the account info with 'AccountObject' is retrieved from the cache and
                //It is converted to a Entity Account object
                return (Account)ViewState["AccountObject"];
            }

            return new Account();
        }
        set
        {
            //Customer account object is placed in the cache and assigned a key, AccountObject.            
            ViewState["AccountObject"] = value;
        }
    }

    /// <summary>
    /// Sets or gets accountobject with addresses
    /// </summary>
    public ZNodeAddress BillingAddress
    {
        get
        {
            //get fields
            _billingAddress.FirstName = txtBillingFirstName.Text;
            _billingAddress.LastName = txtBillingLastName.Text;
            _billingAddress.CompanyName = txtBillingCompanyName.Text;
            _billingAddress.Street1 = txtBillingStreet1.Text;
            _billingAddress.Street2 = txtBillingStreet2.Text;
            _billingAddress.City = txtBillingCity.Text;
            _billingAddress.StateCode = txtBillingState.Text.ToUpper();
            _billingAddress.PostalCode = txtBillingPostalCode.Text;
            _billingAddress.CountryCode = lstBillingCountryCode.SelectedValue;
            _billingAddress.PhoneNumber = txtBillingPhoneNumber.Text;            
            _billingAddress.EmailId = txtBillingEmail.Text;

            return _billingAddress;
        }
        set
        {
            _billingAddress = value;

            //set field values
            txtBillingFirstName.Text = _billingAddress.FirstName;
            txtBillingLastName.Text = _billingAddress.LastName;
            txtBillingCompanyName.Text = _billingAddress.CompanyName;
            txtBillingStreet1.Text = _billingAddress.Street1;
            txtBillingStreet2.Text = _billingAddress.Street2;
            txtBillingCity.Text = _billingAddress.City;
            txtBillingState.Text = _billingAddress.StateCode;
            txtBillingPostalCode.Text = _billingAddress.PostalCode;

            if (_billingAddress.CountryCode.Length > 0)
            {
                lstBillingCountryCode.SelectedValue = _billingAddress.CountryCode;
            }

            txtBillingPhoneNumber.Text = _billingAddress.PhoneNumber;
            txtBillingEmail.Text = _billingAddress.EmailId;
        }
    }

    /// <summary>
    /// Sets or gets accountobject with addresses
    /// </summary>
    public ZNodeAddress ShippingAddress
    {
        get
        {
            if (chkSameAsBilling.Checked)
            {
                _shippingAddress.FirstName = txtBillingFirstName.Text;
                _shippingAddress.LastName = txtBillingLastName.Text;
                _shippingAddress.CompanyName = txtBillingCompanyName.Text;
                _shippingAddress.Street1 = txtBillingStreet1.Text;
                _shippingAddress.Street2 = txtBillingStreet2.Text;
                _shippingAddress.City = txtBillingCity.Text;
                _shippingAddress.StateCode = txtBillingState.Text.ToUpper();
                _shippingAddress.PostalCode = txtBillingPostalCode.Text;
                _shippingAddress.CountryCode = lstBillingCountryCode.SelectedValue;
                _shippingAddress.PhoneNumber = txtBillingPhoneNumber.Text;
                _shippingAddress.EmailId = txtBillingEmail.Text;
            }
            else
            {
                _shippingAddress.FirstName = txtShippingFirstName.Text;
                _shippingAddress.LastName = txtShippingLastName.Text;
                _shippingAddress.CompanyName = txtShippingCompanyName.Text;
                _shippingAddress.Street1 = txtShippingStreet1.Text;
                _shippingAddress.Street2 = txtShippingStreet2.Text;
                _shippingAddress.City = txtShippingCity.Text;
                _shippingAddress.PostalCode = txtShippingPostalCode.Text;
                _shippingAddress.CountryCode = lstShippingCountryCode.SelectedValue;
                _shippingAddress.StateCode = txtShippingState.Text.ToUpper();
                _shippingAddress.PhoneNumber = txtShippingPhoneNumber.Text;
                _shippingAddress.EmailId = txtShippingEmail.Text;
            }

            return _shippingAddress;
        }
        set
        {
            _shippingAddress = value;

            //set field values
            txtShippingFirstName.Text = _shippingAddress.FirstName;
            txtShippingLastName.Text = _shippingAddress.LastName;
            txtShippingCompanyName.Text = _shippingAddress.CompanyName;
            txtShippingStreet1.Text = _shippingAddress.Street1;
            txtShippingStreet2.Text = _shippingAddress.Street2;
            txtShippingCity.Text = _shippingAddress.City;
            txtShippingState.Text = _shippingAddress.StateCode;
            txtShippingPostalCode.Text = _shippingAddress.PostalCode;

            if (_shippingAddress.CountryCode.Length > 0)
            {
                lstShippingCountryCode.SelectedValue = _shippingAddress.CountryCode;
            }

            txtShippingPhoneNumber.Text = _shippingAddress.PhoneNumber;
            txtShippingEmail.Text = _shippingAddress.EmailId;
        }
    }
    # endregion

    # region Bind Methods
    /// <summary>
    /// Binds country drop-down list
    /// </summary>
    private void BindCountry()
    {
        CountryService countryServ = new CountryService();
        TList<ZNode.Libraries.DataAccess.Entities.Country> countries = countryServ.GetByPortalIDActiveInd(ZNodeConfigManager.SiteConfig.PortalID, true);
        countries.Sort("DisplayOrder,Name");

        lstBillingCountryCode.DataSource = countries;
        lstBillingCountryCode.DataTextField = "Name";
        lstBillingCountryCode.DataValueField = "Code";
        lstBillingCountryCode.DataBind();
        lstBillingCountryCode.SelectedValue = "US";

        lstShippingCountryCode.DataSource = countries;
        lstShippingCountryCode.DataTextField = "Name";
        lstShippingCountryCode.DataValueField = "Code";
        lstShippingCountryCode.DataBind();
        lstShippingCountryCode.SelectedValue = "US";
    }

    public void Bind()
    {
        if (UserAccount.AccountID > 0)
        {
            AccountService service = new AccountService();
            Account _account = service.GetByAccountID(UserAccount.AccountID);

            if (_account != null)
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

            }            
            btnUpdate.Text = "Update";
        }
        else
        {
            _billingAddress = new ZNodeAddress();
            _shippingAddress = new ZNodeAddress();

            btnUpdate.Text = "Create an account";
        }

        //get user's address
        BillingAddress = _billingAddress;
        ShippingAddress = _shippingAddress;

    }
    # endregion

    # region Events
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //Gets the browser name
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

        if (!Page.IsPostBack)
        {
            //Bind Countries list
            BindCountry();
        }
    }

    /// <summary>
    /// Event is raised when the "Update" Button is clicked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        AccountService accountService = new AccountService();
        Account _account = UserAccount;
        ZNodeUserAccount _userAccount = new ZNodeUserAccount();

        if (_account.AccountID  == 0)
        {
            _userAccount.PortalID = ZNodeConfigManager.SiteConfig.PortalID;
            _userAccount.BillingAddress = BillingAddress;
            _userAccount.ShippingAddress = ShippingAddress;
            _userAccount.AddUserAccount();

            int accountID = _userAccount.AccountID;            
            _account = accountService.GetByAccountID(accountID);

            UserAccount = _account;

            //
            ZNode.Libraries.Admin.ProfileAdmin profileAdmin = new ZNode.Libraries.Admin.ProfileAdmin();

            HttpContext.Current.Session["ProfileCache"] = profileAdmin.GetByProfileID(_account.ProfileID.Value);
        }
        else 
        {
            
            //Billing Address
            _account.BillingFirstName = BillingAddress.FirstName;
            _account.BillingLastName = BillingAddress.LastName;
            _account.BillingCompanyName = BillingAddress.CompanyName;
            _account.BillingStreet = BillingAddress.Street1;
            _account.BillingStreet1 = BillingAddress.Street2;
            _account.BillingCity = BillingAddress.City;
            _account.BillingPostalCode = BillingAddress.PostalCode;
            _account.BillingStateCode = BillingAddress.StateCode;
            _account.BillingCountryCode = BillingAddress.CountryCode;
            _account.BillingEmailID = BillingAddress.EmailId;
            _account.BillingPhoneNumber = BillingAddress.PhoneNumber;

            //Shipping Address
            _account.ShipFirstName = ShippingAddress.FirstName;
            _account.ShipLastName = ShippingAddress.LastName;
            _account.ShipCompanyName = ShippingAddress.CompanyName;
            _account.ShipStreet = ShippingAddress.Street1;
            _account.ShipStreet1 = ShippingAddress.Street2;
            _account.ShipCity = ShippingAddress.City;
            _account.ShipPostalCode = ShippingAddress.PostalCode;
            _account.ShipStateCode = ShippingAddress.StateCode;
            _account.ShipCountryCode = ShippingAddress.CountryCode;
            _account.ShipEmailID = ShippingAddress.EmailId;
            _account.ShipPhoneNumber = ShippingAddress.PhoneNumber;

            //Update account
            accountService.Update(_account);

            //Set account object to cache object
            UserAccount = _account;

            if (_account.UserID.HasValue)
            {
                MembershipUser user = Membership.GetUser(_account.UserID.Value);

                if (user != null)
                {
                    user.Email = _account.BillingEmailID;
                    Membership.UpdateUser(user);
                }
            }
        }

        //Triggers parent control event
        if (ButtonClick != null)
        {
            this.ButtonClick(sender, e);
        }
    }

    /// <summary>
    /// Event is raised when the "Cancel" Button is clicked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (ButtonClick != null)
        {
            //Triggers parent control event
            this.ButtonClick(sender, e);
        }
    }

    /// <summary>
    /// CheckBox checked change event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkSameAsBilling_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSameAsBilling.Checked)
        {
            ShippingAddress = BillingAddress;
            pnlShipping.Visible = false;
        }
        else
        {
            pnlShipping.Visible = true;
            Account _account = UserAccount;
            
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

                ShippingAddress = _shippingAddress;
            }
        }

        //Update panel
        pnlCustomerDetail.Update();

    }
    # endregion
}
