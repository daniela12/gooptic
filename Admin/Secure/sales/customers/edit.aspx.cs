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
using ZNode.Libraries.ECommerce.Fulfillment;
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.ECommerce.UserAccount;
using ZNode.Libraries.DataAccess.Custom;
public partial class Admin_Secure_sales_customers_edit : System.Web.UI.Page
{
    # region Protected Member Variables
    protected int AccountID;
    private ZNodeAddress _billingAddress = new ZNodeAddress();
    private ZNodeAddress _shippingAddress = new ZNodeAddress();
    protected decimal discountAmount;
    # endregion

    # region Page Load

    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (HttpContext.Current.Request.Params["itemid"] == null)
        {
            AccountID = 0;
        }
        else
        {
            AccountID = int.Parse(Request.Params["itemid"].ToString());
        }

        if (!Page.IsPostBack)
        {
            this.BindCountry();
            this.BindProfile();            
            this.BindRoles();
            this.BindTrackingStatus();
            this.BindReferral();
            this.BindData();
            discAmountValidator.ErrorMessage = "Enter commission amount between " + (0).ToString("N") + "- 9999999";
            discPercentageValidator.ErrorMessage = "Enter percentage amount between " + (0).ToString("N") + "- 100";


            if (AccountID > 0)
            {
                //txtExternalAccNumber.Enabled = false;
                lblTitle.Text = "Edit Account Information";
                amountOwed.Visible = true;
                lblAmountOwed.Visible = true;
            }
            else
            {
                lblTitle.Text = "Add Account Information";
                amountOwed.Visible = false;
                lblAmountOwed.Visible = false;
            }

           
        }       
    }

    # endregion

    # region Bind Data

    /// <summary>
    /// Bind Account Details
    /// </summary>
    private void BindData()
    {

        ZNode.Libraries.Admin.AccountAdmin _UserAccountAdmin = new ZNode.Libraries.Admin.AccountAdmin();
        ZNode.Libraries.DataAccess.Entities.Account _UserAccount = _UserAccountAdmin.GetByAccountID(AccountID);//new Account();

        if (_UserAccount != null)
        {
            //General Information
            txtExternalAccNumber.Text = _UserAccount.ExternalAccountNo;
            txtCompanyName.Text = _UserAccount.CompanyName;
            if (ListProfileType.SelectedIndex != -1)
            {
                ListProfileType.SelectedValue = _UserAccount.ProfileID.ToString();
            }

            txtWebSite.Text = _UserAccount.Website;
            txtSource.Text = _UserAccount.Source;

            //Description
            txtDescription.Text = _UserAccount.Description;

            //Login Info
            if (_UserAccount.UserID.HasValue)
            {
                UserID.Enabled = false;

                MembershipUser _user = Membership.GetUser(_UserAccount.UserID.Value);
                UserID.Text = _user.UserName;
                ddlSecretQuestions.Text = _user.PasswordQuestion;

                string roleList = "";
                //Get roles for this User account
                string[] roles = Roles.GetRolesForUser(_user.UserName);

                foreach (string Role in roles)
                {
                    roleList += Role + "<br>";
                    //Loop through the roles list
                    foreach (ListItem li in RolesCheckboxList.Items)
                    {
                        if (li.Text == Role)
                        {
                            li.Selected = true;
                        }

                        //Admin User should not remove the Admin role from their own account.
                        if (HttpContext.Current.User.Identity.Name.Equals(_user.UserName.ToLower()) && li.Text == "ADMIN")
                        {
                            li.Enabled = false;
                        }
                    }
                }

                string rolename = roleList;

                //Hide the Submit button if a NonAdmin user has entered this page
                if (!Roles.IsUserInRole(HttpContext.Current.User.Identity.Name, "ADMIN"))
                {
                    if (Roles.IsUserInRole(_user.UserName, "ADMIN"))
                    {
                        btnSubmitTop.Visible = false;
                        Submit.Visible = false;
                    }
                    else if (Roles.IsUserInRole(HttpContext.Current.User.Identity.Name, "CUSTOMER SERVICE REP"))
                    {
                        if (rolename == Convert.ToString("USER<br>") || rolename == Convert.ToString(""))
                        {
                            btnSubmitTop.Visible = true;
                            Submit.Visible = true;
                        }
                        else
                        {
                            btnSubmitTop.Visible = false;
                            Submit.Visible = false;
                        }
                    }
                }
            }

            //Billing Address

            //set field values
            txtBillingFirstName.Text = _UserAccount.BillingFirstName;
            txtBillingLastName.Text = _UserAccount.BillingLastName;
            txtBillingCompanyName.Text = _UserAccount.BillingCompanyName;
            txtBillingStreet1.Text = _UserAccount.BillingStreet;
            txtBillingStreet2.Text = _UserAccount.BillingStreet1;
            txtBillingCity.Text = _UserAccount.BillingCity;
            txtBillingState.Text = _UserAccount.BillingStateCode;
            txtBillingPostalCode.Text = _UserAccount.BillingPostalCode;

            if (_UserAccount.BillingCountryCode.Length > 0)
            {
                lstBillingCountryCode.SelectedValue = _UserAccount.BillingCountryCode;
            }

            txtBillingPhoneNumber.Text = _UserAccount.BillingPhoneNumber;
            txtBillingEmail.Text = _UserAccount.BillingEmailID;
            chkOptIn.Checked = _UserAccount.EmailOptIn;

            //Shipping Address

            //set field values
            txtShippingFirstName.Text = _UserAccount.ShipFirstName;
            txtShippingLastName.Text = _UserAccount.ShipLastName;
            txtShippingCompanyName.Text = _UserAccount.ShipCompanyName;
            txtShippingStreet1.Text = _UserAccount.ShipStreet;
            txtShippingStreet2.Text = _UserAccount.ShipStreet1;
            txtShippingCity.Text = _UserAccount.ShipCity;
            txtShippingState.Text = _UserAccount.ShipStateCode;
            txtShippingPostalCode.Text = _UserAccount.ShipPostalCode;

            //Referral Details
            if (_UserAccount.ReferralCommissionTypeID != null)
            {
                lstReferral.SelectedValue = _UserAccount.ReferralCommissionTypeID.ToString();
            }
            if (_UserAccount.ReferralCommission != null)
            {
                Discount.Text = _UserAccount.ReferralCommission.Value.ToString("N");
                discountAmount = Convert.ToDecimal(_UserAccount.ReferralCommission);
            }
            if (_UserAccount.ReferralStatus != null)
                lstReferralStatus.SelectedValue = _UserAccount.ReferralStatus;
            else
                lstReferralStatus.SelectedValue = _UserAccount.ReferralStatus;
         
            if (_UserAccount.ReferralStatus == "A")
            {
                string affiliateLink = "http://" + ZNodeConfigManager.SiteConfig.DomainName + "/default.aspx?affiliate_id=" + _UserAccount.AccountID;
                hlAffiliateLink.Text = affiliateLink;
                hlAffiliateLink.NavigateUrl = affiliateLink;
            }
            else
            {
                hlAffiliateLink.Text = "NA";                
            }
            ToggleDiscountValidator();
            txtTaxId.Text = _UserAccount.TaxID;
           
            AccountHelper helperAccess = new AccountHelper();
            DataSet MyDataSet = helperAccess.GetCommisionAmount(ZNodeConfigManager.SiteConfig.PortalID, AccountID.ToString());

            lblAmountOwed.Text ="$" +  MyDataSet.Tables[0].Rows[0]["CommissionOwed"].ToString();

            if (_UserAccount.ShipCountryCode.Length > 0)
            {
                lstShippingCountryCode.SelectedValue = _UserAccount.ShipCountryCode;
            }

            txtShippingPhoneNumber.Text = _UserAccount.ShipPhoneNumber;
            txtShippingEmail.Text = _UserAccount.ShipEmailID;

            //Custom properties
            txtCustom1.Text = _UserAccount.Custom1;
            txtCustom2.Text = _UserAccount.Custom2;
            txtCustom3.Text = _UserAccount.Custom3;

        }
    }

    /// <summary>
    /// Binds country drop-down list
    /// </summary>
    private void BindCountry()
    {
        CountryService countryServ = new CountryService();
        TList<ZNode.Libraries.DataAccess.Entities.Country> countries = countryServ.GetByPortalIDActiveInd(ZNodeConfigManager.SiteConfig.PortalID, true);
        countries.Sort("DisplayOrder,Name");

        //Billing Drop Down List
        lstBillingCountryCode.DataSource = countries;
        lstBillingCountryCode.DataTextField = "Name";
        lstBillingCountryCode.DataValueField = "Code";
        lstBillingCountryCode.DataBind();
        lstBillingCountryCode.SelectedValue = "US";

        //Shipping Drop Down List
        lstShippingCountryCode.DataSource = countries;
        lstShippingCountryCode.DataTextField = "Name";
        lstShippingCountryCode.DataValueField = "Code";
        lstShippingCountryCode.DataBind();
        lstShippingCountryCode.SelectedValue = "US";
    }

    /// <summary>
    /// Binds Account Type drop-down list
    /// </summary>
    private void BindProfile()
    {
        ZNode.Libraries.Admin.ProfileAdmin _Profileadmin = new ProfileAdmin();
        ListProfileType.DataSource = _Profileadmin.GetAll();
        ListProfileType.DataTextField = "name";
        ListProfileType.DataValueField = "profileID";
        ListProfileType.DataBind();

        if (AccountID == 0)
        {
            ProfileAdmin AdminAccess = new ProfileAdmin();
            Profile entity = AdminAccess.GetByProfileID(int.Parse(ListProfileType.SelectedValue));

            if (entity != null)
            { txtExternalAccNumber.Text = entity.DefaultExternalAccountNo; }
        }
    }

    /// <summary>
    /// Binds user roles checkbox list
    /// </summary>
    private void BindRoles()
    {
        //Get all roles
        string[] roles = Roles.GetAllRoles();
        //Bind Checkbox list
        RolesCheckboxList.DataSource = roles;
        RolesCheckboxList.DataBind();

        if (Roles.IsUserInRole("ADMIN"))
        {
            RolesCheckboxList.Enabled = true;
        }

        //When the admin user sets the admin role to the end user then pop up a message box 
        ListItem item = RolesCheckboxList.Items.FindByText("ADMIN");
        item.Attributes.Add("OnClick", "if(this.checked == true)alert('Warning! Setting this option will give full administrative access of your catalog to this user.')");
    }

    /// <summary>
    /// Binds Referral type drop-down list
    /// </summary>
    private void BindReferral()
    {
        ZNode.Libraries.Admin.ReferralCommissionAdmin ReferralType = new ReferralCommissionAdmin();
        lstReferral.DataSource = ReferralType.GetAll();
        lstReferral.DataTextField = "Name";
        lstReferral.DataValueField = "ReferralCommissiontypeID";
        lstReferral.DataBind();
    }

    /// <summary>
    /// Binds Tracking Status drop-down list
    /// </summary>
    private void BindTrackingStatus()
    {
        Array names = Enum.GetNames(typeof(ZNodeApprovalStatus.ApprovalStatus));
        Array values = Enum.GetValues(typeof(ZNodeApprovalStatus.ApprovalStatus));

        // clear list items
        lstReferralStatus.Items.Clear();

        // add default value to the item
        lstReferralStatus.Items.Add(new ListItem("Inactive",""));

        for (int i = 0; i < names.Length; i++)
        {
            ListItem listitem = new ListItem(ZNodeApprovalStatus.GetEnumValue(values.GetValue(i)),names.GetValue(i).ToString());
            lstReferralStatus.Items.Add(listitem);
        }
    }

    /// <summary>
    /// Discount Type select index change event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DiscountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ToggleDiscountValidator();
    }

    # endregion

    # region General Events

    protected void lstReferralStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstReferralStatus.SelectedValue == "A" && AccountID>0)
        {
            string affiliateLink = "http://" + ZNodeConfigManager.SiteConfig.DomainName + "/default.aspx?affiliate_id=" + AccountID;
            hlAffiliateLink.Text = affiliateLink;
            hlAffiliateLink.NavigateUrl = affiliateLink;
        }
        else
        {
            hlAffiliateLink.Text = "NA";
            hlAffiliateLink.NavigateUrl = "";
        }
    }

    /// <summary>
    /// Submit Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ZNode.Libraries.Admin.AccountAdmin _UserAccountAdmin = new ZNode.Libraries.Admin.AccountAdmin();
        ZNode.Libraries.DataAccess.Entities.Account _UserAccount = new Account();

        if (AccountID > 0)
        {
           
              _UserAccount = _UserAccountAdmin.GetByAccountID(AccountID);
        }

        if (AccountID == 0)
        {
            _UserAccount.CreateDte = System.DateTime.Now;
        }

        //Set user's address

        _UserAccount.BillingFirstName = txtBillingFirstName.Text;
        _UserAccount.BillingLastName = txtBillingLastName.Text;
        _UserAccount.BillingCompanyName = txtBillingCompanyName.Text;
        _UserAccount.BillingStreet = txtBillingStreet1.Text;
        _UserAccount.BillingStreet1 = txtBillingStreet2.Text;
        _UserAccount.BillingCity = txtBillingCity.Text;
        _UserAccount.BillingStateCode = txtBillingState.Text;
        _UserAccount.BillingPostalCode = txtBillingPostalCode.Text;
        _UserAccount.BillingPhoneNumber = txtBillingPhoneNumber.Text;
        _UserAccount.BillingEmailID = txtBillingEmail.Text;
        _UserAccount.EmailOptIn = chkOptIn.Checked;

        if (lstBillingCountryCode.SelectedIndex != -1)
        {
            _UserAccount.BillingCountryCode = lstBillingCountryCode.SelectedValue;
        }

        // Get Shipping field values
        _UserAccount.ShipFirstName = txtShippingFirstName.Text;
        _UserAccount.ShipLastName = txtShippingLastName.Text;
        _UserAccount.ShipCompanyName = txtShippingCompanyName.Text;
        _UserAccount.ShipStreet = txtShippingStreet1.Text;
        _UserAccount.ShipStreet1 = txtShippingStreet2.Text;
        _UserAccount.ShipCity = txtShippingCity.Text;
        _UserAccount.ShipStateCode = txtShippingState.Text;
        _UserAccount.ShipPostalCode = txtShippingPostalCode.Text;

        if (lstShippingCountryCode.SelectedIndex != -1)
        {
            _UserAccount.ShipCountryCode = lstShippingCountryCode.SelectedValue;
        }

        _UserAccount.ShipPhoneNumber = txtShippingPhoneNumber.Text;
        _UserAccount.ShipEmailID = txtShippingEmail.Text;


        //Set Account Details
        _UserAccount.ExternalAccountNo = txtExternalAccNumber.Text.Trim();
        _UserAccount.Description = txtDescription.Text;
        _UserAccount.CompanyName = txtCompanyName.Text;
        _UserAccount.Website = txtWebSite.Text.Trim();
        _UserAccount.Source = txtSource.Text.Trim();

        // Referral Detail        
        if (lstReferral.SelectedIndex != -1)
        {
            _UserAccount.ReferralCommissionTypeID = Convert.ToInt32(lstReferral.SelectedValue);          
        }
        if (Discount.Text != "" && Convert.ToDecimal(Discount.Text)>0)
        {
            _UserAccount.ReferralCommission = Convert.ToDecimal(Discount.Text);
            discountAmount = Convert.ToDecimal(Discount.Text);
        }
        ToggleDiscountValidator();
        _UserAccount.TaxID = txtTaxId.Text;

        if (lstReferralStatus.SelectedValue == "I")
        {
            _UserAccount.ReferralStatus = "I";
        }
        else if (lstReferralStatus.SelectedValue == "A")
        {
            _UserAccount.ReferralStatus = "A";
        }
        else if (lstReferralStatus.SelectedValue == "N")
        {
            _UserAccount.ReferralStatus = "N";
        }
        else
        {
            _UserAccount.ReferralStatus = "";
        }

        if (ListProfileType.SelectedIndex != -1)
        {
            _UserAccount.ProfileID = int.Parse(ListProfileType.SelectedValue);
        }
        
        _UserAccount.AccountProfileCode = DBNull.Value.ToString();
        _UserAccount.UpdateUser = System.Web.HttpContext.Current.User.Identity.Name;
        _UserAccount.UpdateDte = System.DateTime.Now;

        _UserAccount.UserID = _UserAccount.UserID;
        _UserAccount.AccountID = AccountID;
        _UserAccount.PortalID = int.Parse(ZNodeConfigManager.SiteConfig.PortalID.ToString());
        
        //Custom Information Section
        _UserAccount.Custom1 = txtCustom1.Text.Trim();
        _UserAccount.Custom2 = txtCustom2.Text.Trim();
        _UserAccount.Custom3 = txtCustom3.Text.Trim();     

        try
        {
            if (_UserAccount.UserID.HasValue)
            {
                
                //get the User associated with the specified Unique identifier
                MembershipUser user = Membership.Providers["ZNodeAdminMembershipProvider"].GetUser(_UserAccount.UserID.Value, false);

                user.Email = txtBillingEmail.Text.Trim();
                //Update the database with the Email Id for the specified user
                Membership.UpdateUser(user);                

                if (Password.Text.Length > 0)
                {
                    string passwordAnswer = Answer.Text.Trim();

                    if (passwordAnswer.Length == 0)
                    {
                        //Display error message
                        lblAnswerErrorMsg.Text = " * Enter security answer";
                        lblErrorMsg.Text = " * Enter security answer";
                        return;
                    }

                    //Update new password
                    user.ChangePassword(user.ResetPassword(), Password.Text.Trim());
                    
                    //Log password for further debugging
                    ZNodeUserAccount.LogPassword((Guid)user.ProviderUserKey, Password.Text.Trim());

                    //change password question and answer
                    user.ChangePasswordQuestionAndAnswer(Password.Text.Trim(), ddlSecretQuestions.SelectedItem.Text, Answer.Text.Trim());                   
                }

                //retrieve roles associated with this user
                string[] roles = Roles.GetRolesForUser(user.UserName);

                //Loop through the roles
                foreach (string Role in roles)
                {
                    //Unassign this user from that role
                    Roles.RemoveUserFromRole(user.UserName, Role);
                }

                //Loop thorugh the roles checkbox list
                foreach (ListItem li in RolesCheckboxList.Items)
                {
                    if (li.Selected)
                    {
                        //Associate the User with the selected role
                        Roles.AddUserToRole(user.UserName, li.Text);
                    }
                }             
            }
            else
            {
                if (UserID.Text.Trim().Length > 0)
                {
                    string errorMessage = "";

                    //Check password field
                    if (Password.Text.Trim().Length == 0)
                    {
                        errorMessage = " * Enter password <br />";
                        lblPwdErrorMsg.Text = " * Enter password";
                    }

                    //check security answer field
                    if (Answer.Text.Trim().Length == 0)
                    {
                        errorMessage += " * Enter security answer";
                        lblAnswerErrorMsg.Text = " * Enter security answer";
                    }

                    //Show error message
                    if (errorMessage.Length > 0)
                    {
                        lblErrorMsg.Text = errorMessage;
                        return;
                    }

                    MembershipUser user = Membership.GetUser(UserID.Text.Trim());

                    if (user == null) //online account DOES NOT exist 
                    {
                        ZNodeLogging log = new ZNodeLogging();
                        MembershipCreateStatus status = MembershipCreateStatus.ProviderError;
                        log.LogActivityTimerStart();
                        MembershipUser newUser = Membership.CreateUser(UserID.Text.Trim(), Password.Text.Trim(), txtBillingEmail.Text.Trim(), ddlSecretQuestions.SelectedItem.Text, Answer.Text.Trim(), true, out status);

                        if (status == MembershipCreateStatus.Success)
                        {
                            log.LogActivityTimerEnd((int)ZNodeLogging.ErrorNum.LoginCreateSuccess, UserID.Text.Trim());

                            //Log password for further debugging
                            ZNodeUserAccount.LogPassword((Guid)newUser.ProviderUserKey, Password.Text.Trim());

                            //Update provider user key
                            _UserAccount.UserID = new Guid(newUser.ProviderUserKey.ToString());

                            //Associate this user with the selected role
                            foreach (ListItem li in RolesCheckboxList.Items)
                            {
                                if (li.Selected)
                                {
                                    //Associate the User with the selected role
                                    Roles.AddUserToRole(newUser.UserName, li.Text);
                                }
                            }
                        }
                        else
                        {
                            log.LogActivityTimerEnd((int)ZNodeLogging.ErrorNum.LoginCreateFailed, UserID.Text.Trim());
                            lblErrorMsg.Text = "Unable to create online account. Please try again.";
                            return;
                        }
                    }
                    else
                    {
                        lblErrorMsg.Text = "User Id already exists. Please use differnet user id.";
                        return;
                    }
                }
            }
        }
        catch (Exception exception)
        {
            lblErrorMsg.Text = exception.Message;
            return;
        }
  
        bool Check = false;

        if(AccountID >0)
        {
            //set update date
            _UserAccount.UpdateDte = System.DateTime.Now;

            Check = _UserAccountAdmin.Update(_UserAccount);
        }      
        else
        {            
            Check = _UserAccountAdmin.Insert(_UserAccount);

        }
        //Check Boolean Value
        if (Check)
        {
            Response.Redirect("~/admin/secure/sales/customers/list.aspx");
        }
        else
        {
            lblErrorMsg.Text = "";
        }
    }
    
    /// <summary>
    ///  Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/sales/customers/list.aspx");
    }
   
    /// <summary>
    /// Event is raised when the selection from the profile type list control changes between posts to the server
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ListProfileType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (AccountID == 0)
        {
            ProfileAdmin AdminAccess = new ProfileAdmin();
            Profile entity = AdminAccess.GetByProfileID(int.Parse(ListProfileType.SelectedValue));

            if (entity != null)
                txtExternalAccNumber.Text = entity.DefaultExternalAccountNo;
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
            //Set user's shipping address
            txtShippingFirstName.Text = txtBillingFirstName.Text.Trim();
            txtShippingLastName.Text = txtBillingLastName.Text.Trim();
            txtShippingCompanyName.Text = txtBillingCompanyName.Text.Trim();
            txtShippingStreet1.Text = txtBillingStreet1.Text.Trim();
            txtShippingStreet2.Text = txtBillingStreet2.Text.Trim();
            txtShippingCity.Text = txtBillingCity.Text.Trim();
            txtShippingState.Text = txtBillingState.Text.Trim();
            txtShippingPostalCode.Text = txtBillingPostalCode.Text.Trim();
            txtShippingPhoneNumber.Text = txtBillingPhoneNumber.Text.Trim();
            txtShippingEmail.Text = txtBillingEmail.Text.Trim();
            lstShippingCountryCode.SelectedValue = lstBillingCountryCode.SelectedValue;
            
            pnlShipping.Visible = false;

            tblBillingAddr.Width = "100%";
        }
        else
        {
            tblBillingAddr.Width = "50%";
            pnlShipping.Visible = true;
        }

        
    }
    # endregion    

    # region Helper Methods
    /// <summary>
    /// Enable/Disable Percentage/Amount validator on Discount field
    /// </summary>
    private void ToggleDiscountValidator()
    {
        int Id = int.Parse(lstReferral.SelectedValue);

        discAmountValidator.Enabled = false;
        discPercentageValidator.Enabled = false;

        if (Id == 1) // Percentage Discount 
        {
            if ((discountAmount <= 1) || (discountAmount >= 100))
            {
                discPercentageValidator.Enabled = true;
                discPercentageValidator.SetFocusOnError = true;
                discAmountValidator.Enabled = false;
            }
        }
        else if (Id == 2) //Amount Discount
        {
            if ((discountAmount <= 1) || (discountAmount >= 9999999))
            {
                discAmountValidator.Enabled = true;
                discAmountValidator.SetFocusOnError = true;
                discPercentageValidator.Enabled = false;
            }
        }
    }
     #endregion

   
}
