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
using ZNode.Libraries.DataAccess.Custom;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.ECommerce.SEO;

public partial class Admin_Secure_catalog_SEO_product_edit : System.Web.UI.Page
{   
    #region protected Member Variables
    protected string CancelLink = "view.aspx?itemid=";
    protected string ManagePageLink = "~/admin/secure/SEO/product/product_list.aspx";
    protected string ListLink = "list.aspx";
    protected string ProductImageName = "";
    protected int ItemID;    
    protected int SKUId = 0;
    protected int _ProductID = 0;  
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //Get product id value from query string
        if (Request.Params["itemid"] != null)
        {

            ItemID = int.Parse(Request.Params["itemid"].ToString());
        }
        else
        {
            ItemID = 0;
        }

        if (!Page.IsPostBack)
        {
            lblTitle.Text = "Edit Product SEO Settings - ";
            this.BindEditData();
        }
    }
    #endregion

    # region Bind Edit Data
    /// <summary>
    /// Bind value for Particular Product
    /// </summary>
    public void BindEditData()
    {
        ProductAdmin _ProductAdmin = new ProductAdmin();
        Product _Products = new Product();
        
        //if edit mode then get all the values first
        if (ItemID > 0)
        {
            _Products = _ProductAdmin.GetByProductId(ItemID);
        }
    
        lblTitle.Text += _Products.Name;
        txtProductName.Text = _Products.Name;
        txtSEOTitle.Text = _Products.SEOTitle;
        txtSEOMetaKeywords.Text = _Products.SEOKeywords;
        txtSEOMetaDescription.Text = _Products.SEODescription;
        txtSEOUrl.Text = _Products.SEOURL;
        ctrlHtmlDescription.Html = _Products.Description;
        txtshortdescription.Text = _Products.ShortDescription;
        ctrlHtmlPrdFeatures.Html = _Products.FeaturesDesc;
        ctrlHtmlProdInfo.Html = _Products.AdditionalInformation;
        ctrlHtmlPrdSpec.Html = _Products.Specifications;
    }
    #endregion

    # region General Events

    /// <summary>
    /// Submit Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ProductAdmin productAdmin = new ProductAdmin();
        Product product = new Product();
        string mappedSEOUrl = "";

        // If edit mode then get all the values first
        if (ItemID > 0)
        {
            product = productAdmin.GetByProductId(ItemID);

            if (product.SEOURL != null)
                mappedSEOUrl = product.SEOURL;
        }

        // Passing Values 
        product.ProductID = ItemID;
        product.PortalID = ZNodeConfigManager.SiteConfig.PortalID;
        product.Name = txtProductName.Text;        
        product.Description = ctrlHtmlDescription.Html.Trim();
        product.ShortDescription = txtshortdescription.Text;
        product.AdditionalInformation = ctrlHtmlProdInfo.Html.Trim();
        product.Specifications = ctrlHtmlPrdSpec.Html.Trim();
        product.FeaturesDesc = ctrlHtmlPrdFeatures.Html.Trim();
        
        product.SEOTitle = txtSEOTitle.Text.Trim();
        product.SEOKeywords = txtSEOMetaKeywords.Text.Trim();
        product.SEODescription = txtSEOMetaDescription.Text.Trim();
        product.SEOURL = null;
        
        if (txtSEOUrl.Text.Trim().Length > 0)
        {
            product.SEOURL = txtSEOUrl.Text.Trim().Replace(" ", "-"); 
        }


        bool status = false;
        
        // Create transaction
        TransactionManager tranManager = ConnectionScope.CreateTransaction();

        try
        {
            if (ItemID > 0) // PRODUCT UPDATE
            {
                status = productAdmin.Update(product);
            }

        }
        catch (Exception)
        {
            // Error occurred so rollback transaction        
            tranManager.Rollback();

            lblError.Text = "Unable to update product SEO settings. Please try again.";
            return;
        }


        if (status)
        {
            ZNodeSEOUrl seoUrl = new ZNodeSEOUrl();
            UrlRedirectAdmin urlRedirectAdmin = new UrlRedirectAdmin();            
            bool retval = false;

            try
            {
                retval = urlRedirectAdmin.UpdateUrlRedirectTable(SEOUrlType.Product, mappedSEOUrl, product.SEOURL, ItemID.ToString(), chkAddURLRedirect.Checked);
            }
            catch
            {
                // Error occurred so rollback transaction
                tranManager.Rollback();

                lblError.Text = "The SEO Friendly URL you entered is already in use on another page. Please select another name for your URL";

                return;
            }

            if (retval)
            {
                // Commit transaction
                tranManager.Commit();

                Response.Redirect(ManagePageLink);
            }
            else
            {
                lblError.Text = "Could not update the product SEO Url. Please try again.";
            }
        }
        else
        {
            lblError.Text = "Unable to update product SEO settings. Please try again.";
        }

    }

    /// <summary> 
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
       Response.Redirect(ManagePageLink);     
    }

     #endregion
}
