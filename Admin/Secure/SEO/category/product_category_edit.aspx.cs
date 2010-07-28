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
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.DataAccess.Custom;

public partial class Admin_Secure_catalog_SEO_product_category_edit : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    protected string ManagePageLink = "~/admin/secure/SEO/category/product_category_list.aspx";
    #endregion
    
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
                lblTitle.Text = "Edit Category SEO Settings - ";
                BindEditData();
            }
        }
    }

    #region Bind Data
    /// <summary>
    /// Bind data to the fields on the edit screen
    /// </summary>
    protected void BindEditData()
    {
        CategoryAdmin categoryAdmin = new CategoryAdmin();
        Category category = categoryAdmin.GetByCategoryId(ItemId);

        if (category != null)
        {
            lblTitle.Text += category.Title;
            txtTitle.Text = category.Title;            
            txtshortdescription.Text = category.ShortDescription;
            ctrlHtmlText.Html = category.Description;            
            txtSEOMetaDescription.Text = category.SEODescription;
            txtSEOMetaKeywords.Text = category.SEOKeywords;
            txtSEOTitle.Text = category.SEOTitle;
            txtSEOURL.Text = category.SEOURL;
        }
        else
        {
            throw (new ApplicationException("Category Requested could not be found."));
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
        CategoryAdmin categoryAdmin = new CategoryAdmin();
        Category category = new Category();
        string mappedSEOUrl = "";

        if (ItemId > 0)
        {
            category = categoryAdmin.GetByCategoryId(ItemId);

            if (category.SEOURL != null)
                mappedSEOUrl = category.SEOURL;
        }

        category.CategoryID = ItemId;
        category.PortalID = ZNodeConfigManager.SiteConfig.PortalID;        
        category.ShortDescription = txtshortdescription.Text;
        category.Description = ctrlHtmlText.Html;
        category.Title = txtTitle.Text;        
        category.SEOTitle = txtSEOTitle.Text;
        category.SEOKeywords = txtSEOMetaKeywords.Text;
        category.SEODescription = txtSEOMetaDescription.Text;
        category.SEOURL = null;
        if (txtSEOURL.Text.Trim().Length > 0)
            category.SEOURL = txtSEOURL.Text.Trim().Replace(" ", "-");

        bool retval = false;
        //create transaction
        TransactionManager tranManager = ConnectionScope.CreateTransaction();

        if (ItemId > 0)
        {
            retval = categoryAdmin.Update(category);
        }

        if (retval)
        {   
            UrlRedirectAdmin urlRedirectAdmin = new UrlRedirectAdmin();
            bool status = false;

            try
            {
                status = urlRedirectAdmin.UpdateUrlRedirectTable(SEOUrlType.Category, mappedSEOUrl, category.SEOURL, ItemId.ToString(), chkAddURLRedirect.Checked);
            }
            catch
            {
                //error occurred so rollback transaction        
                tranManager.Rollback();
                lblError.Text = "The SEO Friendly URL you entered is already in use on another page. Please select another name for your URL.";

                return;
            }

            if (status) // check status whether urlmapping table updated successfully
            {
                // Commit transaction
                tranManager.Commit();

                Response.Redirect(ManagePageLink);
                                
            }
            else
            {
                //error occurred so rollback transaction        
                tranManager.Rollback();

                lblError.Text = "Could not update the product category SEO Url. Please try again.";
            }
        }
        else
        {
            if (ItemId > 0)
                lblError.Text = "Could not update the product category. Please try again.";           

            return;
        }
    }
    
    /// <summary>
    /// Cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(ManagePageLink);
    }

    #endregion
}
