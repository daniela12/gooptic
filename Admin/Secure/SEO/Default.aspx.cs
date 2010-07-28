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

public partial class Admin_Secure_settings_SEO_Default : System.Web.UI.Page
{
    
   #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
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

        // SEO Settings
        portal.SeoDefaultProductTitle = txtSEOProductTitle.Text.Trim();
        portal.SeoDefaultProductDescription = txtSEOProductDescription.Text.Trim();
        portal.SeoDefaultProductKeyword = txtSEOProductKeyword.Text.Trim();

        portal.SeoDefaultCategoryTitle = txtSEOCategoryTitle.Text.Trim();
        portal.SeoDefaultCategoryDescription = txtSEOCategoryDescription.Text.Trim();
        portal.SeoDefaultCategoryKeyword = txtSEOCategoryKeyword.Text.Trim();

        portal.SeoDefaultContentTitle = txtSEOContentTitle.Text.Trim();
        portal.SeoDefaultContentDescription = txtSEOContentDescription.Text.Trim();
        portal.SeoDefaultContentKeyword = txtSEOContentKeyword.Text.Trim();

       
        bool ret = storeAdmin.Update(portal);

        //remove the siteconfig from session
        ZNodeConfigManager.SiteConfig = null;

        if (!ret)
        {
            lblMsg.Text = "An error ocurred while updating the SEO settings. Please try again.";

            //Log Activity
            ZNode.Libraries.Framework.Business.ZNodeLogging.LogActivity(9002, HttpContext.Current.User.Identity.Name);

        }
        else
        {
            Response.Redirect("~/admin/secure/SEO/SEOManager.aspx");

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
        Response.Redirect("~/admin/secure/SEO/SEOManager.aspx");
    }

   
    #endregion


    #region Bind Method
    /// <summary>
    /// Bind data to form fields
    /// </summary>
    private void BindData()
    {
        StoreSettingsAdmin storeAdmin = new StoreSettingsAdmin();
        Portal portal = storeAdmin.GetByPortalId(ZNodeConfigManager.SiteConfig.PortalID);

        // Set SEO Details
        if (portal.SeoDefaultProductTitle != null)
            txtSEOProductTitle.Text = portal.SeoDefaultProductTitle.ToString();
        if (portal.SeoDefaultProductDescription != null)
            txtSEOProductDescription.Text = portal.SeoDefaultProductDescription.ToString();
        if (portal.SeoDefaultProductKeyword != null)
            txtSEOProductKeyword.Text = portal.SeoDefaultProductKeyword.ToString();
        if (portal.SeoDefaultCategoryTitle != null)
            txtSEOCategoryTitle.Text = portal.SeoDefaultCategoryTitle.ToString();
        if (portal.SeoDefaultCategoryDescription != null)
            txtSEOCategoryDescription.Text = portal.SeoDefaultCategoryDescription.ToString();
        if (portal.SeoDefaultCategoryKeyword != null)
            txtSEOCategoryKeyword.Text = portal.SeoDefaultCategoryKeyword.ToString();
        if (portal.SeoDefaultContentTitle != null)
            txtSEOContentTitle.Text = portal.SeoDefaultContentTitle.ToString();
        if (portal.SeoDefaultContentDescription != null)
            txtSEOContentDescription.Text = portal.SeoDefaultContentDescription.ToString();
        if (portal.SeoDefaultContentKeyword != null)
            txtSEOContentKeyword.Text = portal.SeoDefaultContentKeyword.ToString();



    }
    #endregion
}
