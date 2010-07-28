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

public partial class Admin_Secure_settings_FedEx_add : System.Web.UI.Page
{
    # region Page Load Event
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCountries();
        }
    }
    #endregion

    # region General Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ZNode.Libraries.Shipping.FedEx fdx = new ZNode.Libraries.Shipping.FedEx();
        ZNodeEncryption decrypt = new ZNodeEncryption (); 
        
        fdx.ClientProductId = ZNodeConfigManager.SiteConfig.FedExClientProductId;
        fdx.ClientProductVersion = ZNodeConfigManager.SiteConfig.FedExClientProductVersion;
        fdx.CSPAccessKey = ZNodeConfigManager.SiteConfig.FedExCSPKey;
        fdx.CSPPassword = ZNodeConfigManager.SiteConfig.FedExCSPPassword;
        fdx.CurrencyCode = ZNode.Libraries.ECommerce.Catalog.ZNodeCurrencyManager.CurrencyCode();        
        fdx.FedExAccountNumber = decrypt.DecryptData(ZNodeConfigManager.SiteConfig.FedExAccountNumber);

        ZNode.Libraries.Shipping.FedExRegisterCspUserService.Address BillingAddress = new ZNode.Libraries.Shipping.FedExRegisterCspUserService.Address();
        
        BillingAddress.StreetLines =  new string[]{txtBillingStreet1.Text.Trim(),txtBillingStreet2.Text.Trim()};
        BillingAddress.City = txtBillingCity.Text.Trim();
        BillingAddress.StateOrProvinceCode = txtBillingState.Text.Trim();
        BillingAddress.PostalCode = txtBillingPostalCode.Text.Trim();
        BillingAddress.CountryCode = lstBillingCountryCode.SelectedValue;

        ZNode.Libraries.Shipping.FedExRegisterCspUserService.WebAuthenticationCredential userCredential = fdx.RegisterCPCUser(FirstName.Text.Trim(), LastName.Text.Trim(), PhoneNumber.Text.Trim(), "", "", EmailId.Text.Trim(), BillingAddress);

        if (fdx.ErrorCode == "0")
        {
            //User Credential
            string userKey = userCredential.Key;
            string password = userCredential.Password;

            PortalService portalService = new PortalService();
            Portal _portal = portalService.GetByPortalID(ZNodeConfigManager.SiteConfig.PortalID);

            _portal.FedExProductionKey = decrypt.EncryptData(userKey);
            _portal.FedExSecurityCode = decrypt.EncryptData(password);

            portalService.Update(_portal);

            //remove the siteconfig from session
            ZNodeConfigManager.SiteConfig = null;

            Response.Redirect("~/admin/secure/settings/default.aspx?mode=fedex");
        }
        else
        {
            lblErrorMsg.Text = fdx.ErrorDescription;
        }
    }

    /// <summary>
    /// Cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/settings/default.aspx?mode=fedex");
    }

    #endregion

    # region Bind Methods
    /// <summary>
    /// 
    /// </summary>
    protected void BindCountries()
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
    }

     
    #endregion
}
