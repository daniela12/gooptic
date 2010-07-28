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
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Entities;

public partial class Admin_Secure_settings_FedEx_SubScribeUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCountries();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ZNode.Libraries.Shipping.FedEx fdx = new ZNode.Libraries.Shipping.FedEx();
        ZNodeEncryption decrypt = new ZNodeEncryption();

        if (ZNodeConfigManager.SiteConfig.FedExProductionKey.Length == 0 || ZNodeConfigManager.SiteConfig.FedExSecurityCode.Length == 0)
        {
            lblErrorMsg.Text = "Please obtain user account first using RegisterCSPUSer.";
            return;
        }

        fdx.ClientProductId = ZNodeConfigManager.SiteConfig.FedExClientProductId;
        fdx.ClientProductVersion = ZNodeConfigManager.SiteConfig.FedExClientProductVersion;
        fdx.CSPAccessKey = ZNodeConfigManager.SiteConfig.FedExCSPKey;
        fdx.CSPPassword = ZNodeConfigManager.SiteConfig.FedExCSPPassword;
        fdx.FedExAccountNumber = decrypt.DecryptData(ZNodeConfigManager.SiteConfig.FedExAccountNumber);
        fdx.CurrencyCode = ZNode.Libraries.ECommerce.Catalog.ZNodeCurrencyManager.CurrencyCode();        
        fdx.FedExAccessKey = decrypt.DecryptData(ZNodeConfigManager.SiteConfig.FedExProductionKey);
        fdx.FedExSecurityCode = decrypt.DecryptData(ZNodeConfigManager.SiteConfig.FedExSecurityCode);
        

        ZNode.Libraries.Shipping.FedExSubscribeService.Address BillingAddress = new ZNode.Libraries.Shipping.FedExSubscribeService.Address();        
        BillingAddress.StreetLines = new string[] { txtBillingStreet1.Text.Trim(), txtBillingStreet2.Text.Trim() };
        BillingAddress.PostalCode = txtBillingPostalCode.Text.Trim();
        BillingAddress.City = txtBillingCity.Text.Trim();
        BillingAddress.StateOrProvinceCode = txtBillingState.Text.Trim();
        BillingAddress.CountryCode = lstBillingCountryCode.SelectedValue;

        string meterNumber = fdx.SubscribeUser(FirstName.Text.Trim() + " " + LastName.Text.Trim(), PhoneNumber.Text.Trim(), "", "", EmailID.Text, BillingAddress);

        if (fdx.ErrorCode == "0")//Check for success
        {
            PortalService portalService = new PortalService();
            Portal _portal = portalService.GetByPortalID(ZNodeConfigManager.SiteConfig.PortalID);

            _portal.FedExMeterNumber = decrypt.EncryptData(meterNumber);

            portalService.Update(_portal);

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
