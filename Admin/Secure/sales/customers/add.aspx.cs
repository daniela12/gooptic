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

public partial class Admin_Secure_sales_customers_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            //Call Bind Profile Method
            BindProfile();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ZNode.Libraries.Admin.AccountAdmin _UserAccountAdmin = new ZNode.Libraries.Admin.AccountAdmin();
        ZNode.Libraries.DataAccess.Entities.Account _UserAccount = new Account();

        //Set values                
        _UserAccount.BillingFirstName = txtBillingFirstName.Text;
        _UserAccount.BillingLastName = txtBillingLastName.Text;
        _UserAccount.BillingPhoneNumber = txtBillingPhoneNumber.Text;
        _UserAccount.BillingEmailID = txtBillingEmail.Text;

        //Set to empty values to other billing address properties
        _UserAccount.BillingCompanyName = "";
        _UserAccount.BillingStreet = "";
        _UserAccount.BillingStreet1 = "";
        _UserAccount.BillingCity = "";
        _UserAccount.BillingStateCode = "";
        _UserAccount.BillingPostalCode = "";
        _UserAccount.BillingCountryCode = "";

        //Set to empty values to other shipping address properties
        _UserAccount.ShipFirstName = "";
        _UserAccount.ShipLastName = "";
        _UserAccount.ShipCompanyName = "";
        _UserAccount.ShipStreet = "";
        _UserAccount.ShipStreet1 = "";
        _UserAccount.ShipPhoneNumber = "";
        _UserAccount.ShipPostalCode = "";
        _UserAccount.ShipStateCode = "";
        _UserAccount.ShipEmailID = "";
        _UserAccount.ShipCity = "";
        _UserAccount.ShipCountryCode = "";


        if (ListProfileType.SelectedIndex != -1)
        {
            _UserAccount.ProfileID = int.Parse(ListProfileType.SelectedValue);
        }

        //pre-set properties
        _UserAccount.UserID = null;
        _UserAccount.PortalID = int.Parse(ZNodeConfigManager.SiteConfig.PortalID.ToString());        
        _UserAccount.ActiveInd = true;
        _UserAccount.ParentAccountID = null;
        _UserAccount.AccountTypeID = 0;
        _UserAccount.CreateDte = System.DateTime.Now;
        _UserAccount.UpdateDte = System.DateTime.Now;
        _UserAccount.CreateUser = HttpContext.Current.User.Identity.Name;

        //Add New Contact
        bool Check = _UserAccountAdmin.Add(_UserAccount);

        //Check Boolean Value
        if (Check)
        {
            Response.Redirect("~/admin/secure/sales/customers/list.aspx");
        }
        else
        {
            ErrorMessage.Text = "Could not create new contact. Please contact customer support.";
            return;
        }

    }

    /// <summary>
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/sales/customers/list.aspx");
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

    }
}
