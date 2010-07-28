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
using ZNode.Libraries.Admin;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.DataAccess.Entities;
using System.Globalization;

public partial class Admin_Secure_settings_storesettings : System.Web.UI.Page
{
    #region Bind CurrencyType List
    /// <summary>
    /// Bind Currency Types to dropdown list
    /// </summary>
    private void BindList()
    {
        StoreSettingsAdmin storeAdmin = new StoreSettingsAdmin();
        ddlCurrencyTypes.DataSource = storeAdmin.GetAll();
        ddlCurrencyTypes.DataTextField = "Description";
        ddlCurrencyTypes.DataValueField = "CurrencyTypeId";
        ddlCurrencyTypes.DataBind();

        ddlCurrencyTypes.SelectedValue = "150"; //By Default US-English
    }
    #endregion

    # region Bind Methods

    protected void BindOrderStatus()
    {
        OrderAdmin orderAdmin = new OrderAdmin();
        ddlOrderStateList.DataSource = orderAdmin.GetAllOrderStates();
        ddlOrderStateList.DataTextField = "OrderStateName";
        ddlOrderStateList.DataValueField = "OrderStateID";
        ddlOrderStateList.DataBind();
    }
    /// <summary>
    /// Bind data to form fields
    /// </summary>
    private void BindData()
    {
        StoreSettingsAdmin storeAdmin = new StoreSettingsAdmin();
        Portal portal = storeAdmin.GetByPortalId(ZNodeConfigManager.SiteConfig.PortalID);

        txtDomainName.Text = portal.DomainName;
        txtCompanyName.Text = portal.CompanyName;
        txtStoreName.Text = portal.StoreName;
        chkEnableSSL.Checked = portal.UseSSL;
        txtAdminEmail.Text = portal.AdminEmail;
        txtSalesEmail.Text = portal.SalesEmail;
        txtCustomerServiceEmail.Text = portal.CustomerServiceEmail;
        txtSalesPhoneNumber.Text = portal.SalesPhoneNumber;
        txtCustomerServicePhoneNumber.Text = portal.CustomerServicePhoneNumber;
        txtMaxCatalogDisplayColumns.Text = portal.MaxCatalogDisplayColumns.ToString();
        txtMaxCatalogDisplayItems.Text = portal.MaxCatalogDisplayItems.ToString();
        txtMaxSmallThumbnailsDisplay.Text = portal.MaxCatalogCategoryDisplayThumbnails.ToString();        
        chkInclusiveTax.Checked = portal.InclusiveTax;

        txtMaxCatalogItemLargeWidth.Text = portal.MaxCatalogItemLargeWidth.ToString();
        txtMaxCatalogItemMediumWidth.Text = portal.MaxCatalogItemMediumWidth.ToString();        
        txtMaxCatalogItemSmallWidth.Text = portal.MaxCatalogItemSmallWidth.ToString();
        txtMaxCatalogCrossSellWidth.Text = portal.MaxCatalogItemCrossSellWidth.ToString();
        txtMaxCatalogItemSwatchesWidth.Text = portal.MaxCatalogItemSwatchWidth.ToString();
        txtMaxCatalogItemThumbnailWidth.Text = portal.MaxCatalogItemThumbnailWidth.ToString();
        

        txtShopByPriceMax.Text = portal.ShopByPriceMax.ToString();
        txtShopByPriceMin.Text = portal.ShopByPriceMin.ToString();
        txtShopByPriceIncrement.Text = portal.ShopByPriceIncrement.ToString();
        ListReviewStatus.SelectedValue = portal.DefaultReviewStatus;

        //Currency Type settings
        if (portal.CurrencyTypeID.HasValue)
        {
            ddlCurrencyTypes.SelectedValue = portal.CurrencyTypeID.Value.ToString();
        }

        //Show Example
        ShowPriceFormat();

        if(portal.SiteWideBottomJavascript!=null)
        {
            txtSiteWideBottomJavaScript.Text = portal.SiteWideBottomJavascript.ToString();
        }
        if (portal.SiteWideTopJavascript != null)
        {
            txtSiteWideTopJavaScript.Text = portal.SiteWideTopJavascript.ToString();
        }
        if (portal.SiteWideAnalyticsJavascript != null)
        {
            txtSiteWideAnalyticsJavascript.Text = portal.SiteWideAnalyticsJavascript.ToString();
        }
        if (portal.OrderReceiptAffiliateJavascript != null)
        {
            txtOrderReceiptJavaScript.Text = portal.OrderReceiptAffiliateJavascript.ToString();
        }

        
        try
        {
            ZNodeEncryption encrypt = new ZNodeEncryption();

            if (portal.SMTPServer != null)
            {
                txtSMTPServer.Text = portal.SMTPServer;
            }
            if (portal.SMTPUserName != null)
            {
                txtSMTPUserName.Text = encrypt.DecryptData(portal.SMTPUserName);
            }
            if (portal.SMTPPassword != null)
            {
                txtSMTPPassword.Text = encrypt.DecryptData(portal.SMTPPassword);
            }

            //Set UPS Account details
            if (portal.UPSUserName != null)
            {
                txtUPSUserName.Text = encrypt.DecryptData(portal.UPSUserName);
            }
            if (portal.UPSPassword != null)
            {
                txtUPSPassword.Text = encrypt.DecryptData(portal.UPSPassword);
            }
            if (portal.UPSKey != null)
            {
                txtUPSKey.Text = encrypt.DecryptData(portal.UPSKey);
            }

            //Bind FedEx Account details
            if (portal.FedExAccountNumber != null)
                txtAccountNum.Text = encrypt.DecryptData(portal.FedExAccountNumber);
            if (portal.FedExMeterNumber != null)
                txtMeterNum.Text = encrypt.DecryptData(portal.FedExMeterNumber);
            if (portal.FedExProductionKey != null)
                txtProductionAccessKey.Text = encrypt.DecryptData(portal.FedExProductionKey);
            if (portal.FedExSecurityCode != null)
                txtSecurityCode.Text = encrypt.DecryptData(portal.FedExSecurityCode);
            if (portal.FedExUseDiscountRate.HasValue)
                chkFedExDiscountRate.Checked = portal.FedExUseDiscountRate.Value;
            if (portal.FedExAddInsurance.HasValue)
                chkAddInsurance.Checked = portal.FedExAddInsurance.Value;

        }
        catch
        {
            //ignore decryption errors
        }

        txtShippingAddress1.Text = portal.ShippingOriginAddress1;
        txtShippingAddress2.Text = portal.ShippingOriginAddress2;
        txtShippingCity.Text = portal.ShippingOriginCity;
        txtShippingPhone.Text = portal.ShippingOriginPhone;
        txtShippingZipCode.Text = portal.ShippingOriginZipCode;
        txtShippingStateCode.Text = portal.ShippingOriginStateCode;
        txtShippingCountryCode.Text = portal.ShippingOriginCountryCode;        

        //Units - tab
        ddlWeightUnits.SelectedValue = portal.WeightUnit;
        ddlPackageTypeCodes.SelectedValue = portal.FedExPackagingType;
        ddldropOffTypes.SelectedValue = portal.FedExDropoffType;
        ddlDimensions.SelectedValue = portal.DimensionUnit;
        
        //set logo image
        imgLogo.ImageUrl = portal.LogoPath;

        //display tab
        ddlOrderStateList.SelectedValue = portal.DefaultOrderStateID.GetValueOrDefault(10).ToString();
    }
    #endregion
    
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindOrderStatus();
            BindList();
            BindData();            
        }
    }

    /// <summary>
    /// Submit button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StoreSettingsAdmin storeAdmin = new StoreSettingsAdmin();
        Portal portal = storeAdmin.GetByPortalId(ZNodeConfigManager.SiteConfig.PortalID);

        portal.AdminEmail = txtAdminEmail.Text;
        portal.CompanyName = txtCompanyName.Text;
        portal.CustomerServiceEmail = txtCustomerServiceEmail.Text;
        portal.CustomerServicePhoneNumber = txtCustomerServicePhoneNumber.Text;
        portal.DomainName = txtDomainName.Text;
        portal.MaxCatalogDisplayColumns = byte.Parse(txtMaxCatalogDisplayColumns.Text);
        portal.MaxCatalogDisplayItems = int.Parse(txtMaxCatalogDisplayItems.Text);
        portal.InclusiveTax = chkInclusiveTax.Checked;

        portal.MaxCatalogItemThumbnailWidth = int.Parse(txtMaxCatalogItemThumbnailWidth.Text);
        portal.MaxCatalogCategoryDisplayThumbnails = int.Parse(txtMaxSmallThumbnailsDisplay.Text);
        
        portal.SiteWideBottomJavascript = txtSiteWideBottomJavaScript.Text;
        portal.SiteWideTopJavascript = txtSiteWideTopJavaScript.Text;
        portal.SiteWideAnalyticsJavascript = txtSiteWideAnalyticsJavascript.Text;
        portal.OrderReceiptAffiliateJavascript = txtOrderReceiptJavaScript.Text;

        portal.MaxCatalogItemLargeWidth = int.Parse(txtMaxCatalogItemLargeWidth.Text);
        portal.MaxCatalogItemMediumWidth = int.Parse(txtMaxCatalogItemMediumWidth.Text);
        portal.MaxCatalogItemSmallWidth = int.Parse(txtMaxCatalogItemSmallWidth.Text);
        portal.MaxCatalogItemCrossSellWidth = int.Parse(txtMaxCatalogCrossSellWidth.Text);
        portal.MaxCatalogItemThumbnailWidth = int.Parse(txtMaxCatalogItemThumbnailWidth.Text);
        portal.MaxCatalogItemSwatchWidth = int.Parse(txtMaxCatalogItemSwatchesWidth.Text);
        

        portal.ShopByPriceMin = int.Parse(txtShopByPriceMin.Text);
        portal.ShopByPriceMax = int.Parse(txtShopByPriceMax.Text);
        portal.ShopByPriceIncrement = int.Parse(txtShopByPriceIncrement.Text);

        portal.SalesEmail = txtSalesEmail.Text;
        portal.SalesPhoneNumber = txtSalesPhoneNumber.Text;
        portal.StoreName = txtStoreName.Text;
        portal.UseSSL = chkEnableSSL.Checked;
        portal.DefaultReviewStatus = ListReviewStatus.SelectedValue;

        //Currency Settings
        portal.CurrencyTypeID = int.Parse(ddlCurrencyTypes.SelectedValue);

        ZNodeEncryption encrypt = new ZNodeEncryption();

        //SMTP Server Settings
        portal.SMTPServer = txtSMTPServer.Text;
        portal.SMTPUserName = encrypt.EncryptData(txtSMTPUserName.Text);
        portal.SMTPPassword = encrypt.EncryptData(txtSMTPPassword.Text);
        
        //UPS Shipping Settings
        portal.UPSUserName = encrypt.EncryptData(txtUPSUserName.Text.Trim());
        portal.UPSPassword = encrypt.EncryptData(txtUPSPassword.Text.Trim());
        portal.UPSKey = encrypt.EncryptData(txtUPSKey.Text.Trim());
        
        //FedEx Shipping Settings
        portal.FedExAccountNumber = encrypt.EncryptData(txtAccountNum.Text.Trim());
        portal.FedExMeterNumber = encrypt.EncryptData(txtMeterNum.Text.Trim());
        portal.FedExProductionKey = encrypt.EncryptData(txtProductionAccessKey.Text.Trim());
        portal.FedExSecurityCode = encrypt.EncryptData(txtSecurityCode.Text.Trim());

        //Units 
        portal.WeightUnit = ddlWeightUnits.SelectedItem.Text;
        portal.DimensionUnit = ddlDimensions.SelectedItem.Text;

        //default order status setting
        portal.DefaultOrderStateID = int.Parse(ddlOrderStateList.SelectedValue);

        //Shipping Settings        
        portal.ShippingOriginAddress1 = txtShippingAddress1.Text.Trim();
        portal.ShippingOriginAddress2 = txtShippingAddress2.Text.Trim();
        portal.ShippingOriginCity = txtShippingCity.Text.Trim();
        portal.ShippingOriginPhone = txtShippingPhone.Text.Trim();
        
        portal.ShippingOriginZipCode = txtShippingZipCode.Text.Trim();
        portal.ShippingOriginStateCode = txtShippingStateCode.Text.Trim();
        portal.ShippingOriginCountryCode = txtShippingCountryCode.Text.Trim();

        portal.FedExDropoffType = ddldropOffTypes.SelectedItem.Value;
        portal.FedExPackagingType = ddlPackageTypeCodes.SelectedItem.Value;
        portal.FedExUseDiscountRate = chkFedExDiscountRate.Checked;
        portal.FedExAddInsurance = chkAddInsurance.Checked;


     
        
        // set logo path
        System.IO.FileInfo _FileInfo = null;
        
        if (radNewImage.Checked == true)
        {
            //Check for Product Image
            _FileInfo = new System.IO.FileInfo(UploadImage.PostedFile.FileName);

            if (_FileInfo != null)
            {
                if ((_FileInfo.Extension == ".jpeg") || (_FileInfo.Extension.Equals(".jpg")) || (_FileInfo.Extension.Equals(".png")) || (_FileInfo.Extension.Equals(".gif")))
                {
                    portal.LogoPath = ZNodeConfigManager.EnvironmentConfig.ContentPath + _FileInfo.Name;
                    UploadImage.SaveAs(Server.MapPath(ZNodeConfigManager.EnvironmentConfig.ContentPath + _FileInfo.Name));
                }
                else
                {
                    lblImageError.Text = "Select a valid jpg, gif or png image.";
                    return;
                }
            }
        }

        bool ret = storeAdmin.Update(portal);

        //Set currency
        CurrencyType currencyType  = storeAdmin.GetByCurrencyTypeID(int.Parse(ddlCurrencyTypes.SelectedValue));
        if(currencyType !=null)
        {
            currencyType.CurrencySuffix = txtCurrencySuffix.Text.Trim();
            storeAdmin.UpdateCurrencyType(currencyType);
        }

        //remove the siteconfig from session
        ZNodeConfigManager.SiteConfig = null;

        if (!ret)
        {
            lblMsg.Text = "An error ocurred while updating the store settings. Please try again.";

            //Log Activity
            ZNode.Libraries.Framework.Business.ZNodeLogging.LogActivity(9002, HttpContext.Current.User.Identity.Name);

        }
        else
        {
            HttpContext.Current.Application["CurrencyTypeCache"] = currencyType;
            Response.Redirect("~/admin/secure/default.aspx");

            //Log Activity
            ZNode.Libraries.Framework.Business.ZNodeLogging.LogActivity(9001, HttpContext.Current.User.Identity.Name);

        }
    }

    /// <summary>
    /// Cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/default.aspx");
    }

    protected void radCurrentImage_CheckedChanged(object sender, EventArgs e)
    {
        tblLogoUpload.Visible = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void radNewImage_CheckedChanged(object sender, EventArgs e)
    {
        tblLogoUpload.Visible = true;
    }

    /// <summary>
    /// Currency Type dropdown list selected index changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCurrencyTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowPriceFormat();
    }

    /// <summary>
    /// Currency Suffix TextBox Change Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtCurrencySuffix_TextChanged(object sender, EventArgs e)
    {
        string val = lblPrice.Text.Trim();

        if (lblPrice.Text.Contains("("))
            val = lblPrice.Text.Remove(lblPrice.Text.IndexOf('('));

        if (txtCurrencySuffix.Text.Trim().Length > 0)
        {
            lblPrice.Text = val + " (" + txtCurrencySuffix.Text.Trim() + ")";
            return;
        }
        lblPrice.Text = val;
    }
    #endregion   

    # region Helper Method
    /// <summary>
    /// Display Price in specified culture format
    /// </summary>
    private void ShowPriceFormat()
    {
        StoreSettingsAdmin storeAdmin = new StoreSettingsAdmin();
        CurrencyType _currencyType = storeAdmin.GetByCurrencyTypeID(int.Parse(ddlCurrencyTypes.SelectedValue));
        txtCurrencySuffix.Text = _currencyType.CurrencySuffix;
        string currencySymbol = _currencyType.Name;

        CultureInfo info = new CultureInfo(currencySymbol);
        
        decimal price = 100.12M;       

        lblPrice.Text = price.ToString("c", info.NumberFormat);

        if(txtCurrencySuffix.Text.Trim().Length > 0)
          lblPrice.Text += " (" + txtCurrencySuffix.Text.Trim() + ")";        
        
    }
    #endregion
    
}
