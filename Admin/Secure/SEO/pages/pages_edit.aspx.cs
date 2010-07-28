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
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.DataAccess.Entities;

public partial class Admin_Secure_catalog_SEO_pages_edit : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    protected string ManageLink = "~/admin/secure/SEO/pages/pages_list.aspx";    
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


        if (!Page.IsPostBack)
        {
            //if edit func then bind the data fields
            if (ItemId > 0)
            {
                lblTitle.Text = "Edit Page - ";
                //Bind data to the fields on the page
                BindData();
            }    
        }
    }
    #endregion

    #region Bind Data
    /// <summary>
    /// Bind data to the fields 
    /// </summary>
    protected void BindData()
    {
        ContentPageAdmin pageAdmin = new ContentPageAdmin();
        ContentPage contentPage = new ContentPage();

        if (ItemId > 0)
        {
            contentPage = pageAdmin.GetPageByID(ItemId);

            //set fields 
            lblTitle.Text += contentPage.Title;
            txtTitle.Text = contentPage.Title;
            txtSEOMetaDescription.Text = contentPage.SEOMetaDescription;
            txtSEOMetaKeywords.Text = contentPage.SEOMetaKeywords;
            txtSEOTitle.Text = contentPage.SEOTitle;
            txtSEOUrl.Text = contentPage.SEOURL;
            
            //get content
            ctrlHtmlText.Html = pageAdmin.GetPageHTMLByName(contentPage.Name);
        }
        else
        {
            //nothing to do here
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
        ContentPageAdmin pageAdmin = new ContentPageAdmin();
        ContentPage contentPage = new ContentPage();
        string mappedSEOUrl = "";

        bool allowDelete = true;

        // If edit mode then retrieve data first
        if (ItemId > 0)
        {
            contentPage = pageAdmin.GetPageByID(ItemId);
            allowDelete = contentPage.AllowDelete; //override this setting
            if (contentPage.SEOURL != null)
                mappedSEOUrl = contentPage.SEOURL;
        }       

        // set values
        contentPage.ActiveInd = true;
        contentPage.Title = txtTitle.Text;
        contentPage.PortalID = ZNodeConfigManager.SiteConfig.PortalID;
        contentPage.SEOMetaDescription = txtSEOMetaDescription.Text;
        contentPage.SEOMetaKeywords = txtSEOMetaKeywords.Text;
        contentPage.SEOTitle = txtSEOTitle.Text;
        contentPage.SEOURL = null;
        if (txtSEOUrl.Text.Trim().Length > 0)
            contentPage.SEOURL = txtSEOUrl.Text.Trim().Replace(" ", "-");     

        bool retval = false;

        if (ItemId > 0)
        {
            // update code here
            retval = pageAdmin.UpdatePage(contentPage, ctrlHtmlText.Html, HttpContext.Current.User.Identity.Name, mappedSEOUrl, chkAddURLRedirect.Checked);
        }
   
        if (retval)
        {
            // redirect to main page
            Response.Redirect(ManageLink);
        }
        else
        {
            if (contentPage.SEOURL != null)
            {
                // display error message
                lblMsg.Text = "Failed to update page. Please check with SEO Url settings and try again.";
            }
            else
            {
                // display error message
                lblMsg.Text = "Failed to update page. Please try again.";
            }
        }
    }

    /// <summary>
    /// Cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(ManageLink);
    }


    #endregion   
}
