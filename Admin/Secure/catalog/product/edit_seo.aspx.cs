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
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.DataAccess.Service;

public partial class Admin_Secure_catalog_product_edit_seo : System.Web.UI.Page
{
    # region Protected Member Variables
    protected int ItemID = 0;    
    protected string ManagePageLink = "~/admin/secure/catalog/product/view.aspx?mode=seo&itemid=";
    protected string CancelPageLink = "~/admin/secure/catalog/product/view.aspx?mode=seo&itemid=";
    # endregion

    # region Page Load Event
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["itemid"] != null)
        {
            ItemID = int.Parse(Request.Params["itemid"].ToString());
        }

        if (!Page.IsPostBack)
        {
            if (ItemID > 0)
            {
                lblTitle.Text = "Edit SEO for ";

                //Bind Sku Details
                this.Bind();

            }
        }
    }
    #endregion

    # region Bind Methods
    private void Bind()
    {
        //Create Instance for Product Admin and Product entity
        ZNode.Libraries.Admin.ProductAdmin ProdAdmin = new ProductAdmin();
        Product _Product = ProdAdmin.GetByProductId(ItemID);

        if (_Product != null)
        {
            txtSEOTitle.Text = _Product.SEOTitle;
            txtSEOMetaKeywords.Text = _Product.SEOKeywords;
            txtSEOMetaDescription.Text = _Product.SEODescription;
            txtSEOUrl.Text = _Product.SEOURL;
            lblTitle.Text += "\"" + _Product.Name + "\"";
        }

    }
    #endregion

    # region General Events
    /// <summary>
    /// Submit Button Click Event - Fires when Submit button is triggered
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

        // Set properties
        product.SEOTitle = txtSEOTitle.Text.Trim();
        product.SEOKeywords = txtSEOMetaKeywords.Text.Trim();
        product.SEODescription = txtSEOMetaDescription.Text.Trim();
        product.SEOURL = null;        
        
        if (txtSEOUrl.Text.Trim().Length > 0)
        { 
            product.SEOURL = txtSEOUrl.Text.Trim().Replace(" ", "-"); 
        }
        

        bool status = false;
        
        // create transaction
        TransactionManager tranManager = ConnectionScope.CreateTransaction();

        try
        {
            if (ItemID > 0) //PRODUCT UPDATE
            {
                status = productAdmin.Update(product);
            }       
            
        }
        catch (Exception)
        {
            //error occurred so rollback transaction        
            tranManager.Rollback();
            lblError.Text = "Unable to update product SEO settings. Please try again.";
            return;
        }


        if (status)
        {
            
            UrlRedirectAdmin urlRedirectAdmin = new UrlRedirectAdmin();
            bool retval = false;

            try
            {
                retval = urlRedirectAdmin.UpdateUrlRedirectTable(SEOUrlType.Product, mappedSEOUrl, product.SEOURL, ItemID.ToString(), chkAddURLRedirect.Checked);
            }
            catch
            {
                // error occurred so rollback transaction
                tranManager.Rollback();

                lblError.Text = "The SEO Friendly URL you entered is already in use on another page. Please select another name for your URL";

                return;
            }
            
            if (retval)
            {
                //Commit transaction
                tranManager.Commit();

                Response.Redirect(ManagePageLink + ItemID);
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
        Response.Redirect(CancelPageLink + ItemID);
    }
    # endregion
}
