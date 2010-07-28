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
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Custom;

public partial class Admin_Secure_sales_customers_view : System.Web.UI.Page
{
    # region Protected Member Variables
    protected int AccountID;
    # endregion
        
    # region Protected Bind Methods
    /// <summary>
    /// 
    /// </summary>
    protected void BindData()
    {
        AccountAdmin accountAdmin = new AccountAdmin();
        AccountTypeAdmin accountTypeAdmin = new AccountTypeAdmin();
        ProfileAdmin profileAdmin = new ProfileAdmin();        
        CustomerAdmin customerAdmin = new CustomerAdmin();
        ReferralCommissionAdmin referralCommissionAdmin = new ReferralCommissionAdmin();

        ZNode.Libraries.DataAccess.Entities.Account account = accountAdmin.GetByAccountID(AccountID);

        if (account != null)
        {           
            // General Information            
            lblAccountID.Text = account.AccountID.ToString();
            lblCompanyName.Text = account.BillingCompanyName;
            lblExternalAccNumber.Text = account.ExternalAccountNo;
            lblDescription.Text = account.Description;
            lblLoginName.Text = customerAdmin.GetByUserID(int.Parse(AccountID.ToString()));
            lblCustomerDetails.Text = account.AccountID.ToString() + " - " + account.BillingFirstName + " " + account.BillingLastName;
            lblWebSite.Text = account.Website;
            lblSource.Text = account.Source;
            lblCreatedDate.Text = account.CreateDte.ToShortDateString();
            lblCreatedUser.Text = account.CreateUser;

            // Referral Detail

            // Get Referral Type Name for a Account
            if (account.ReferralCommissionTypeID != null)
            {
                ReferralCommissionType referralType = referralCommissionAdmin.GetByReferralID(int.Parse(account.ReferralCommissionTypeID.ToString()));
                lblReferralType.Text = referralType.Name;
            }
            else
            {
                lblReferralType.Text = "";
            }

            if (account.ReferralStatus == "A")
            {
                string affiliateLink = "http://" + ZNodeConfigManager.SiteConfig.DomainName + "/default.aspx?affiliate_id=" + account.AccountID;
                hlAffiliateLink.Text = affiliateLink;
                hlAffiliateLink.NavigateUrl = affiliateLink;
            }
            else
            {
                hlAffiliateLink.Text = "NA";
            }

            if (account.ReferralCommission != null)
            {
                if (account.ReferralCommissionTypeID == 1)
                {
                    lblReferralCommission.Text = account.ReferralCommission.Value.ToString("N");
                }
                else
                {
                    lblReferralCommission.Text = account.ReferralCommission.Value.ToString("c");
                }
            }
            else
            {
                lblReferralCommission.Text = "";
            }


            lblTaxId.Text = account.TaxID;

            if (account.ReferralStatus != null)
            {
                //Getting the Status Description 
                Array values = Enum.GetValues(typeof(ZNodeApprovalStatus.ApprovalStatus));
                Array names=Enum.GetNames(typeof(ZNodeApprovalStatus.ApprovalStatus));
                for (int i = 0; i < names.Length; i++)
                {
                    if (names.GetValue(i).ToString() == account.ReferralStatus)
                    {
                        lblReferralStatus.Text = ZNodeApprovalStatus.GetEnumValue(values.GetValue(i));
                        break;
                    }
                }

                
                BindPayments(accountAdmin);
            }
            else
            {
                pnlAffiliatePayment.Visible = false;
                lblReferralStatus.Text = "";
            }
            
            if (account.UpdateDte != null)
            {
                lblUpdatedDate.Text = account.UpdateDte.Value.ToShortDateString();
            }

            // Email Opt-In
            if (account.EmailOptIn)
            {
                EmailOptin.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(true);
            }
            else
            {
                EmailOptin.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(false);
            }

            lblUpdatedUser.Text = account.UpdateUser;
            lblCustom1.Text = account.Custom1;
            lblCustom2.Text = account.Custom2;
            lblCustom3.Text = account.Custom3;            

            // Get Profile Type Name for a Account
            Profile _profileList = profileAdmin.GetByProfileID(int.Parse(account.ProfileID.ToString()));
            lblProfileTypeName.Text = _profileList.Name;

            // Address Information

            ZNodeAddress AddressFormat = new ZNodeAddress();

            // Format Billing Address
            AddressFormat.FirstName = string.IsNullOrEmpty(account.BillingFirstName) ? string.Empty : account.BillingFirstName;
            AddressFormat.LastName = string.IsNullOrEmpty(account.BillingLastName) ? string.Empty : account.BillingLastName;
            AddressFormat.CompanyName = string.IsNullOrEmpty(account.BillingCompanyName) ? string.Empty : account.BillingCompanyName;
            AddressFormat.Street1 = string.IsNullOrEmpty(account.BillingStreet) ? string.Empty : account.BillingStreet;
            AddressFormat.Street2 = string.IsNullOrEmpty(account.BillingStreet1) ? string.Empty : account.BillingStreet1;
            AddressFormat.City = string.IsNullOrEmpty(account.BillingCity) ? string.Empty : account.BillingCity;
            AddressFormat.StateCode = string.IsNullOrEmpty(account.BillingStateCode) ? string.Empty : account.BillingStateCode;
            AddressFormat.PostalCode = string.IsNullOrEmpty(account.BillingPostalCode) ? string.Empty : account.BillingPostalCode;
            AddressFormat.CountryCode = string.IsNullOrEmpty(account.BillingCountryCode) ? string.Empty : account.BillingCountryCode;
            lblBillingAddress.Text = AddressFormat.ToString() + "Tel: " + account.BillingPhoneNumber + "<br>Email: " + account.BillingEmailID;

            // Format Shipping Address
            AddressFormat.FirstName = string.IsNullOrEmpty(account.ShipFirstName) ? string.Empty : account.BillingFirstName;
            AddressFormat.LastName = string.IsNullOrEmpty(account.ShipLastName) ? string.Empty : account.ShipLastName;
            AddressFormat.CompanyName = string.IsNullOrEmpty(account.ShipCompanyName) ? string.Empty : account.ShipCompanyName;
            AddressFormat.Street1 = string.IsNullOrEmpty(account.ShipStreet) ? string.Empty : account.ShipStreet;
            AddressFormat.Street2 = string.IsNullOrEmpty(account.ShipStreet1) ? string.Empty : account.ShipStreet1;
            AddressFormat.City = string.IsNullOrEmpty(account.ShipCity) ? string.Empty : account.ShipCity;
            AddressFormat.StateCode = string.IsNullOrEmpty(account.ShipStateCode) ? string.Empty : account.ShipStateCode;
            AddressFormat.PostalCode = string.IsNullOrEmpty(account.ShipPostalCode) ? string.Empty : account.ShipPostalCode;
            AddressFormat.CountryCode = string.IsNullOrEmpty(account.ShipCountryCode) ? string.Empty : account.ShipCountryCode;
            lblShippingAddress.Text = AddressFormat.ToString() + "Tel: " + account.ShipPhoneNumber + "<br>Email: " + account.ShipEmailID;

            //To get Amount owed 
            AccountHelper helperAccess = new AccountHelper();
            DataSet myDataSet = helperAccess.GetCommisionAmount(ZNodeConfigManager.SiteConfig.PortalID, account.AccountID.ToString());

            lblAmountOwed.Text = "$" + myDataSet.Tables[0].Rows[0]["CommissionOwed"].ToString();

            // Orders Grid
            this.BindGrid();       
           
            // Retrieves the Role for User using Userid
            if (account.UserID != null)
            {
                 Guid UserKey = new Guid();
                 UserKey = account.UserID.Value;
                 MembershipUser _user = Membership.GetUser(UserKey);
                 string roleList = "";

                //Get roles for this User account
                string[] roles = Roles.GetRolesForUser(_user.UserName);

                foreach (string Role in roles)
                {
                    roleList += Role + "<br>";
                }
                lblRoles.Text = roleList;

                string rolename = roleList;

                //Hide the Edit button if a NonAdmin user has entered this page
                if(!Roles.IsUserInRole("ADMIN"))
                {
                    if (Roles.IsUserInRole(_user.UserName, "ADMIN"))
                    {
                        EditCustomer.Visible = false;
                    }
                    else if (Roles.IsUserInRole(HttpContext.Current.User.Identity.Name, "CUSTOMER SERVICE REP"))
                    {
                        if (rolename == Convert.ToString("USER<br>") || rolename == Convert.ToString(""))
                        {
                            EditCustomer.Visible = true;
                        }
                        else
                        {
                            EditCustomer.Visible = false;
                        }
                    }
                }
            }
        }
    }

    # endregion

    # region Bind Grid

    /// <summary>
    /// Bind Order Grid
    /// </summary>
    protected void BindGrid()
    {
        OrderAdmin _OrderAdmin = new OrderAdmin();
        //TList<Order> _Orders = _OrderAdmin.GetByAccountID(AccountID);
        DataSet DataSetOrderList = _OrderAdmin.GetByAccountID(AccountID, ZNodeConfigManager.SiteConfig.PortalID);

        uxGrid.DataSource = DataSetOrderList;
        uxGrid.DataBind();
        
    }

    /// <summary>
    /// Bind payments Grid
    /// </summary>
    protected void BindPayments()
    {
        AccountAdmin accountAdmin = new AccountAdmin();
        BindPayments(accountAdmin);
    }

    /// <summary>
    /// Bind payments Grid
    /// <param name="AccountAdmin"></param>
    /// </summary>    
    protected void BindPayments(AccountAdmin AccountAdmin)
    {
        uxPaymentList.DataSource = AccountAdmin.GetPaymentsByAccountId(AccountID);
        uxPaymentList.DataBind();

        pnlAffiliatePayment.Visible = true;
    }

    /// <summary>
    /// //Bind Repeater
    /// </summary>
    protected void BindNotes()
    {
        
        NoteAdmin _NoteAdmin = new NoteAdmin();
        TList<Note> noteList =  _NoteAdmin.GetByAccountID(AccountID);
        noteList.Sort("NoteID Desc");
        CustomerNotes.DataSource = noteList;
        CustomerNotes.DataBind();
    }

    # endregion

    # region General Events

    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["itemid"] != null)
        {
            AccountID = int.Parse(Request.Params["itemid"].ToString());
        }
        else
        {
            AccountID = 0;
        }

        if (!IsPostBack)
        {
            this.BindData();
            this.BindNotes();           
        }        
    }


    /// <summary>
    /// CustomerList Button  Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CustomerList_Click(object sender, EventArgs e)
    {
        Response.Redirect("list.aspx");
    }

    /// <summary>
    /// Edit Customer Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CustomerEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("edit.aspx?itemid=" + AccountID);
    }

    /// <summary>
    /// Add Note Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddNewNote_Click(object sender, EventArgs e)
    {
        Response.Redirect("note_add.aspx?itemid=" + AccountID);
    }

    /// <summary>
    /// Add Payment button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddAffiliatePayment_Click(object sender, EventArgs e)
    {
        Response.Redirect("addAffiliatePayment.aspx?accId=" + AccountID);
    }

    # endregion

    # region Helper Methods

    /// <summary>
    /// Format the Price with two decimal
    /// </summary>
    /// <param name="Fieldvalue"></param>
    /// <returns></returns>
    protected string FormatPrice(Object Fieldvalue)
    {
        if (Fieldvalue == DBNull.Value)
        {
            return string.Empty;
        }
        else
        {
            if (Fieldvalue.ToString().Length == 0)
            {
                return string.Empty;
            }
            else
            {
                return "$" + Fieldvalue.ToString().Substring(0, Fieldvalue.ToString().Length - 2);
            }
        }
    }

    /// <summary>
    /// Display the Order State name for a Order state
    /// </summary>
    /// <param name="FieldValue"></param>
    /// <returns></returns>
    protected string DisplayOrderStatus(object FieldValue)
    {
        ZNode.Libraries.Admin.OrderAdmin _OrderStateAdmin = new OrderAdmin();
        OrderState _OrderState = _OrderStateAdmin.GetByOrderStateID(int.Parse(FieldValue.ToString()));
        return _OrderState.OrderStateName.ToString();
    }

    /// <summary>
    ///  Format Customer Note
    /// </summary>
    /// <param name="Field1"></param>
    /// <param name="Field2"></param>
    /// <param name="Field3"></param>
    /// <returns></returns>
    protected string FormatCustomerNote(Object Field1, Object Field2, Object Field3)
    {
        return "<b>" + Field1.ToString() + "</b>  [created by " + Field2.ToString() + " on " + Convert.ToDateTime(Field3).ToShortDateString() + "]";
    }
   
    # endregion

    # region Grid Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        this.BindGrid();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxPaymentList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxPaymentList.PageIndex = e.NewPageIndex;
        this.BindPayments();
    }
    # endregion    
}
