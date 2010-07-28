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
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Entities;
using System.Data.SqlClient;

public partial class Admin_Secure_settings_payment_Add : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get ItemId from querystring        
        if (Request.Params["itemid"] != null)
        {
            ItemId = int.Parse(Request.Params["itemid"]);
        }
        else
        {
            ItemId = 0;
        }

        if (Page.IsPostBack == false)
        {
            //if edit func then bind the data fields
            if (ItemId > 0)
            {
                lblTitle.Text = "Edit Payment Option";                
            }
            else
            {
                lblTitle.Text = "Add a Payment Option";
            }

            //Bind data to the fields on the page
            BindData();
        }
    }
    #endregion

    #region Bind Data
    /// <summary>
    /// Bind data to the fields 
    /// </summary>
    protected void BindData()
    {
        StoreSettingsAdmin settingsAdmin = new StoreSettingsAdmin();

        txtDisplayOrder.Text = "1";

        //GET LIST DATA
        
        //Get payment types
        lstPaymentType.DataSource = settingsAdmin.GetPaymentTypes();
        lstPaymentType.DataTextField = "Name";
        lstPaymentType.DataValueField = "PaymentTypeID";
        lstPaymentType.DataBind();
        lstPaymentType.SelectedIndex = 0;

        //get gateways
        lstGateway.DataSource = settingsAdmin.GetGateways();
        lstGateway.DataTextField = "GatewayName";
        lstGateway.DataValueField = "GatewayTypeID";
        lstGateway.DataBind();
        lstGateway.SelectedIndex = 0;

        //get profiles
        lstProfile.DataSource = settingsAdmin.GetProfiles();
        lstProfile.DataTextField = "Name";
        lstProfile.DataValueField = "ProfileID";
        lstProfile.DataBind();
        lstProfile.Items.Insert(0, new ListItem("All Profiles", "-1"));
        lstProfile.SelectedValue = "-1";

        try
        {
            if (ItemId > 0)
            {
                //get payment setting
                PaymentSetting paymentSetting = settingsAdmin.GetPaymentSettingByID(ItemId);

                txtDisplayOrder.Text = paymentSetting.DisplayOrder.ToString();

                if (paymentSetting.ProfileID.HasValue)
                    lstProfile.SelectedValue = paymentSetting.ProfileID.Value.ToString();
                lstPaymentType.SelectedValue = paymentSetting.PaymentTypeID.ToString();
                chkActiveInd.Checked = paymentSetting.ActiveInd;

                if (paymentSetting.PaymentTypeID == 0)
                {
                    pnlCreditCard.Visible = true;

                    ZNodeEncryption encrypt = new ZNodeEncryption();

                    txtGatewayUserName.Text = encrypt.DecryptData(paymentSetting.GatewayUsername);

                    //if authorize.net
                    if (paymentSetting.GatewayTypeID == 1)
                    {
                        txtTransactionKey.Text = encrypt.DecryptData(paymentSetting.TransactionKey);
                        lblTransactionKey.Text = "Transaction key (Authorize.Net only)";
                        pnlAuthorizeNet.Visible = true;
                        pnlPassword.Visible = false;
                        chkTestMode.Checked = paymentSetting.TestMode;
                        chkPreAuthorize.Checked = paymentSetting.PreAuthorize;
                        pnlGatewayOptions.Visible = true;
                    }
                    //if verisign payflow pro
                    else if (paymentSetting.GatewayTypeID == 2)
                    {
                        txtVendor.Text = paymentSetting.Vendor; // Vendor
                        txtGatewayPassword.Text = encrypt.DecryptData(paymentSetting.GatewayPassword);
                        txtPartner.Text = paymentSetting.Partner;
                        pnlPassword.Visible = true;
                        pnlVerisignGateway.Visible = true;
                        chkTestMode.Checked = paymentSetting.TestMode;
                        pnlGatewayOptions.Visible = true;
                    }
                    //if Nova Gateway
                    else if (paymentSetting.GatewayTypeID == 5)
                    {
                        txtTransactionKey.Text = encrypt.DecryptData(paymentSetting.TransactionKey);
                        lblTransactionKey.Text = "Pin Number (Nova gateway Only)";
                        pnlAuthorizeNet.Visible = true;
                        pnlPassword.Visible = true;
                        txtGatewayPassword.Text = encrypt.DecryptData(paymentSetting.GatewayPassword);
                        chkTestMode.Checked = paymentSetting.TestMode;
                    }
                    //if NSoftware Gateway
                    else if (paymentSetting.GatewayTypeID == 6)
                    {
                        txtTransactionKey.Text = paymentSetting.TransactionKey;
                        txtGatewayPassword.Text = encrypt.DecryptData(paymentSetting.GatewayPassword);
                        lblTransactionKey.Text = "Transaction key";
                        pnlAuthorizeNet.Visible = true;
                        pnlPassword.Visible = true;
                        chkTestMode.Checked = paymentSetting.TestMode;
                    }
                    else if (paymentSetting.GatewayTypeID == 9)
                    {
                        pnlLogin.Visible = false;
                        pnlPassword.Visible = false;
                        chkTestMode.Checked = paymentSetting.TestMode;
                    }
                    //if World Pay Gateway
                    else if (paymentSetting.GatewayTypeID == 10)
                    {
                        txtGatewayPassword.Text = encrypt.DecryptData(paymentSetting.GatewayPassword);
                        txtTransactionKey.Text = encrypt.DecryptData(paymentSetting.TransactionKey);
                        lblGatewayUserName.Text = "Merchant Code";
                        lblGatewaypassword.Text = "Authorization Password";
                        lblTransactionKey.Text = "World Pay Installation Id";                       
                        pnlAuthorizeNet.Visible = true;
                        pnlPassword.Visible = true;                        
                        chkTestMode.Checked = paymentSetting.TestMode;                
                    }
                    else
                    {
                        pnlAuthorizeNet.Visible = false;
                        pnlPassword.Visible = true;
                        txtGatewayPassword.Text = encrypt.DecryptData(paymentSetting.GatewayPassword);
                        txtTransactionKey.Text = "";
                        chkTestMode.Checked = paymentSetting.TestMode;
                    }

                    lstGateway.SelectedValue = paymentSetting.GatewayTypeID.ToString();
                    chkEnableAmex.Checked = (bool)paymentSetting.EnableAmex;
                    chkEnableDiscover.Checked = (bool)paymentSetting.EnableDiscover;
                    chkEnableMasterCard.Checked = (bool)paymentSetting.EnableMasterCard;
                    chkEnableVisa.Checked = (bool)paymentSetting.EnableVisa;
                    //ChkEnableOfflinemode.Checked = (bool)paymentSetting.OfflineMode;
                    //ChkEnableCreditCard.Checked = (bool)paymentSetting.SaveCreditCartInfo;
                }
                else if (paymentSetting.PaymentTypeID == 2)
                {
                    pnlCreditCard.Visible = true;

                    ZNodeEncryption encrypt = new ZNodeEncryption();

                    txtGatewayUserName.Text = encrypt.DecryptData(paymentSetting.GatewayUsername);
                    txtTransactionKey.Text = paymentSetting.TransactionKey;
                    txtGatewayPassword.Text = encrypt.DecryptData(paymentSetting.GatewayPassword);
                    lblTransactionKey.Text = "API Signature (Paypal only)";
                    pnlAuthorizeNet.Visible = true;
                    pnlPassword.Visible = true;
                    pnlGatewayList.Visible = false;
                    pnlCreditCardOptions.Visible = false;
                    chkTestMode.Checked = paymentSetting.TestMode;
                }
                else if (paymentSetting.PaymentTypeID == 3) //If Google Checkout is Selected
                {
                    ZNodeEncryption encrypt = new ZNodeEncryption();

                    txtGatewayUserName.Text = encrypt.DecryptData(paymentSetting.GatewayUsername);
                    txtGatewayPassword.Text = encrypt.DecryptData(paymentSetting.GatewayPassword);
                    chkTestMode.Checked = paymentSetting.TestMode;

                    pnlCreditCardOptions.Visible = false; //Hide CreditCard Options
                    pnlGatewayList.Visible = false; // Hide Gateway dropdownlist
                    pnlCreditCard.Visible = true; //Enable settings
                    pnlAuthorizeNet.Visible = false; //Disable TransactionKey field
                    pnlPassword.Visible = true; // Enable PassWord field
                }
                else
                {
                    pnlCreditCard.Visible = false;
                }
            }
            else
            {
                // enable credit card option by default
                pnlCreditCard.Visible = true;

                lstGateway.SelectedValue = "1";                
                BindPanels();
            }
        }
        catch
        {
            //ignore
        }

    }


    #endregion

    #region General Events
    /// <summary>
    /// Submit button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StoreSettingsAdmin settingsAdmin = new StoreSettingsAdmin();      
        PaymentSetting paymentSetting = new PaymentSetting();

        //If edit mode then retrieve data first
        if (ItemId > 0)
        {
            paymentSetting = settingsAdmin.GetPaymentSettingByID(ItemId);

            if (paymentSetting.ProfileID.HasValue)
            {                
                if ((paymentSetting.ProfileID != int.Parse(lstProfile.SelectedValue)) || (paymentSetting.PaymentTypeID != int.Parse(lstPaymentType.SelectedValue)))
                {
                    //check if this setting already exists for this profile
                    bool settingExists = PaymentSettingExists();

                    if (settingExists)
                    {
                        lblMsg.Text = "This Payment Option already exists for this Profile. Please select a different Payment Option.";
                        return;
                    }
                }
            }
            else
            {
                if (lstProfile.SelectedValue != "-1")
                {
                    //check if this setting already exists for this profile
                    bool settingExists = PaymentSettingExists();

                    if (settingExists)
                    {
                        lblMsg.Text = "This Payment Option already exists for this Profile. Please select a different Payment Option.";
                        return;
                    }
                }
            }
        }

        //set values based on user input
        paymentSetting.ActiveInd = chkActiveInd.Checked;
        paymentSetting.PaymentTypeID = int.Parse(lstPaymentType.SelectedValue);
        if (lstProfile.SelectedValue == "-1")
        {
            paymentSetting.ProfileID = null;//If All profiles is selected
        }
        else
        {
            paymentSetting.ProfileID = int.Parse(lstProfile.SelectedValue);
        }
        paymentSetting.DisplayOrder = Convert.ToInt32(txtDisplayOrder.Text);
        //paymentSetting.OfflineMode = false;        

        //if credit card
        if (paymentSetting.PaymentTypeID == 0)
        {
            paymentSetting.GatewayTypeID = int.Parse(lstGateway.SelectedValue);

            paymentSetting.EnableAmex = chkEnableAmex.Checked;
            paymentSetting.EnableDiscover = chkEnableDiscover.Checked;
            paymentSetting.EnableMasterCard = chkEnableMasterCard.Checked;
            paymentSetting.EnableVisa = chkEnableVisa.Checked;            
            paymentSetting.TestMode = chkTestMode.Checked;
            paymentSetting.PreAuthorize = chkPreAuthorize.Checked;
            paymentSetting.GatewayPassword = string.Empty;
            paymentSetting.TransactionKey = string.Empty;

            ZNodeEncryption encrypt = new ZNodeEncryption();

            paymentSetting.GatewayUsername = encrypt.EncryptData(txtGatewayUserName.Text);
            
            //if authorize.net
            if (paymentSetting.GatewayTypeID == 1)
            {                
                paymentSetting.TransactionKey = encrypt.EncryptData(txtTransactionKey.Text);                
            }
            //If Verisign PayFlow pro gateway is selected
            else if (paymentSetting.GatewayTypeID == 2)
            {
                paymentSetting.GatewayPassword = encrypt.EncryptData(txtGatewayPassword.Text);
                paymentSetting.Partner = txtPartner.Text.Trim();
                paymentSetting.Vendor = txtVendor.Text.Trim();
            }
            //If Nova gateway is selected
            else if (paymentSetting.GatewayTypeID == 5)
            {               
                paymentSetting.GatewayPassword = encrypt.EncryptData(txtGatewayPassword.Text);
                paymentSetting.TransactionKey = encrypt.EncryptData(txtTransactionKey.Text);                
            }
            //If Paypal direct payment gateway is selected
            else if (paymentSetting.GatewayTypeID == 6)
            {
                paymentSetting.GatewayPassword = encrypt.EncryptData(txtGatewayPassword.Text);
                paymentSetting.TransactionKey = txtTransactionKey.Text;
            }
            // If World Pay gateway is selected
            else if (paymentSetting.GatewayTypeID == 10)
            {
                // Authorization password
                paymentSetting.GatewayPassword = encrypt.EncryptData(txtGatewayPassword.Text);
                // Installation Id
                paymentSetting.TransactionKey = encrypt.EncryptData(txtTransactionKey.Text);                
            }
            else
            {
                paymentSetting.TransactionKey = "";
                paymentSetting.GatewayPassword = encrypt.EncryptData(txtGatewayPassword.Text);
            }
        }
        // if Paypal
        else if (paymentSetting.PaymentTypeID == 2)
        {
            paymentSetting.GatewayTypeID = null;
            ZNodeEncryption encrypt = new ZNodeEncryption();

            paymentSetting.GatewayUsername = encrypt.EncryptData(txtGatewayUserName.Text);            
            paymentSetting.TransactionKey = txtTransactionKey.Text;
            paymentSetting.GatewayPassword = encrypt.EncryptData(txtGatewayPassword.Text);
            paymentSetting.TestMode = chkTestMode.Checked;

        }
            //if Google Checkout
        else if (paymentSetting.PaymentTypeID == 3)
        {
            paymentSetting.GatewayTypeID = null; //Set null value to Google
            ZNodeEncryption encrypt = new ZNodeEncryption();

            paymentSetting.GatewayUsername = encrypt.EncryptData(txtGatewayUserName.Text);            
            paymentSetting.GatewayPassword = encrypt.EncryptData(txtGatewayPassword.Text);
            paymentSetting.TestMode = chkTestMode.Checked;
        }
        else // Purchase Order
        {
            paymentSetting.GatewayTypeID = null;            
        }

        bool retval = false;             
       
        //Update Payment setting into database
        if (ItemId > 0)
        {
            retval = settingsAdmin.UpdatePaymentSetting(paymentSetting);
        }
        else
        {
            bool settingExists = settingsAdmin.PaymentSettingExists(int.Parse(lstProfile.SelectedValue), int.Parse(lstPaymentType.SelectedValue));

            if (settingExists)
            {
                lblMsg.Text = "This Payment Option already exists for this Profile. Please select a different Payment Option.";
                return;
            }

            retval = settingsAdmin.AddPaymentSetting(paymentSetting);
        }

        if (retval)
        {
            //redirect to main page
            Response.Redirect("~/admin/secure/settings/payment/");
        }
        else
        {
            //display error message
            lblMsg.Text = "An error occurred while updating. Please try again.";
        }
    }

    /// <summary>
    /// Cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/settings/payment/");
    }

    /// <summary>
    /// Payment type change event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        googleNote.Visible = false;

        if (lstPaymentType.SelectedValue.Equals("0"))
        {
            pnlCreditCard.Visible = true;
            pnlCreditCardOptions.Visible = true;
            pnlGatewayList.Visible = true;

            BindPanels();
        }
        else if (lstPaymentType.SelectedValue.Equals("2")) // if Paypal gateway
        {
            lblTransactionKey.Text = "API Signature (Paypal only)";
            pnlAuthorizeNet.Visible = true;
            pnlPassword.Visible = true;
            pnlCreditCard.Visible = true;
            pnlCreditCardOptions.Visible = false;
            pnlGatewayList.Visible = false;
        }
        else if (lstPaymentType.SelectedValue.Equals("3")) // if Google EC gateway
        {
            googleNote.Visible = true;
            pnlCreditCardOptions.Visible = false; //Hide CreditCard Options
            pnlGatewayList.Visible = false; // Hide Gateway dropdownlist
            pnlCreditCard.Visible = true; //Enable settings
            pnlAuthorizeNet.Visible = false; //Disable TransactionKey field
            pnlPassword.Visible = true; // Enable PassWord field
        }       
        else
        {
            pnlCreditCard.Visible = false;
        }
    }

    /// <summary>
    /// Gateway changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstGateway_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPanels();
    }
    #endregion 
    
    # region Helper Methods
    /// <summary>
    /// Check if this setting already exists for this profile
    /// </summary>
    private bool PaymentSettingExists()
    {
        StoreSettingsAdmin settingsAdmin = new StoreSettingsAdmin();
        return settingsAdmin.PaymentSettingExists(int.Parse(lstProfile.SelectedValue), int.Parse(lstPaymentType.SelectedValue));      
    }

    /// <summary>
    /// 
    /// </summary>
    protected void BindPanels()
    {
        // initialize panels
        pnlLogin.Visible = true;
        pnlPassword.Visible = true;
        pnlVerisignGateway.Visible = false;
        pnlAuthorizeNet.Visible = false;
        pnlGatewayOptions.Visible = false;

        if (lstGateway.SelectedValue.Equals("1")) // if authorize.net
        {
            lblTransactionKey.Text = "Transaction key (Authorize.Net only)";
            pnlAuthorizeNet.Visible = true;
            pnlLogin.Visible = true;
            pnlPassword.Visible = false;
            pnlGatewayOptions.Visible = true;
        }
        else if (lstGateway.SelectedValue.Equals("2")) // if Verisign payflow pro
        {
            pnlVerisignGateway.Visible = true;
            pnlAuthorizeNet.Visible = false;
            pnlLogin.Visible = true;
            pnlPassword.Visible = true;
            pnlGatewayOptions.Visible = true;
        }
        else if (lstGateway.SelectedValue.Equals("5")) // if Nova Payment  Gateway
        {
            lblTransactionKey.Text = "NOVA PIN Number";
            pnlAuthorizeNet.Visible = true;
            pnlLogin.Visible = true;
            pnlPassword.Visible = true;

        }
        else if (lstGateway.SelectedValue.Equals("6")) // if paypal direct Payment
        {
            lblTransactionKey.Text = "PayPal Transaction key";
            pnlAuthorizeNet.Visible = true;
            pnlLogin.Visible = true;
            pnlPassword.Visible = true;

        }
        else if (lstGateway.SelectedValue.Equals("9")) // if IP Commerce
        {
            pnlLogin.Visible = false;
            pnlAuthorizeNet.Visible = false;
            pnlPassword.Visible = false;
            pnlVerisignGateway.Visible = false;
        }
        else if (lstGateway.SelectedValue.Equals("10"))
        {
            lblGatewayUserName.Text = "Merchant Code";
            lblGatewaypassword.Text = "Authorization Password";
            lblTransactionKey.Text = "World Pay Installation Id";
            pnlAuthorizeNet.Visible = true;
            pnlPassword.Visible = true;
        }
    }
    # endregion

}
