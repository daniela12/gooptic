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
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Entities;


public partial class Admin_Secure_sales_Affiliates_Edit : System.Web.UI.Page
{
    # region Protected Member Variables
    protected int AccountID;
    protected decimal discountAmount;
    #endregion

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
            lblTitle.Text = "Edit Affiliate Information";
            this.BindReferral();
            this.BindData();            
            discAmountValidator.Text = "Enter commission amount between " + (0.01).ToString("N") + "- 9999999";
            discPercentageValidator.Text = "Enter percentage amount between " + (0.01).ToString("N") + "- 100";

            //Setting the following fields as Readonly
            txtAccountID.ReadOnly = true;
            txtFisrtName.ReadOnly = true;
            txtLastName.ReadOnly = true;
            txtCompanyName.ReadOnly = true;
            txtEmailID.ReadOnly = true;
            txtPhoneNumber.ReadOnly = true;
        }
    }
    #endregion

    # region Bind Data

    /// <summary>
    /// Bind Account Details
    /// </summary>
    private void BindData()
    {

        ZNode.Libraries.Admin.AccountAdmin _UserAccountAdmin = new ZNode.Libraries.Admin.AccountAdmin();
        ZNode.Libraries.DataAccess.Entities.Account _UserAccount = _UserAccountAdmin.GetByAccountID(AccountID);

        if (_UserAccount != null)
        {
            if (_UserAccount.ReferralCommissionTypeID != null)
            {
                lstReferral.SelectedValue = Convert.ToString(_UserAccount.ReferralCommissionTypeID);
            }           
            if(_UserAccount.ReferralCommission != null)
            {
                Discount.Text = _UserAccount.ReferralCommission.Value.ToString("N");
                discountAmount = Convert.ToDecimal(_UserAccount.ReferralCommission);
            }
            ToggleDiscountValidator();
            txtTaxId.Text = _UserAccount.TaxID;
            txtAccountID.Text = _UserAccount.AccountID.ToString();
            txtFisrtName.Text = _UserAccount.BillingFirstName;
            txtLastName.Text = _UserAccount.BillingLastName;
            txtCompanyName.Text = _UserAccount.CompanyName;
            txtEmailID.Text = _UserAccount.BillingEmailID;
            txtPhoneNumber.Text = _UserAccount.BillingPhoneNumber;            
        }
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
    
        if (lstReferral.SelectedIndex != -1)
        {
            _UserAccount.ReferralCommissionTypeID = Convert.ToInt32(lstReferral.SelectedValue);
        }
        if (Discount.Text != "")
        {
            _UserAccount.ReferralCommission = Convert.ToDecimal(Discount.Text);
            discountAmount = Convert.ToDecimal(Discount.Text);
        }       
        _UserAccount.TaxID = txtTaxId.Text;
      
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
            Response.Redirect("~/admin/secure/sales/Affiliates/View.aspx");
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
        Response.Redirect("~/admin/secure/sales/Affiliates/View.aspx");
    }
    #endregion

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
            if ((discountAmount <= 1) && (discountAmount >= 9999999))
            {
                discAmountValidator.Enabled = true;
                discPercentageValidator.Enabled = false;
            }
        }
    }
    #endregion
}
