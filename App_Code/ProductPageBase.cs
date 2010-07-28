using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Custom;
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.ECommerce.SEO;


/// <summary>
/// Summary description for ProductPageBase
/// </summary>
public class ProductPageBase : ZNodePageBase
{
    #region Private Variables
    private ZNodeProduct _product;
    protected int _productId;
    protected int _productimageid;    
    #endregion

    /// <summary>
    /// Event occurs after all postback data and view-state data is loaded into the page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (HttpContext.Current.Items["SeoUrlFound"] != null)
        {
            System.Web.HttpContext.Current.RewritePath(Request.RawUrl + "?");
        }
    }

    /// <summary>
    /// Page Pre-Initialization Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Default master page file path
        string masterPageFilePath = "~/themes/" + ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.Theme + "/product/Product.master";

        // Get product id from querystring  
        if (Request.Params["zpid"] != null)
        {
            _productId = int.Parse(Request.Params["zpid"]);
        }
        else
        {
            throw (new ApplicationException("Invalid Product Id"));
        }

        // Retrieve product data
        _product = ZNodeProduct.Create(_productId, ZNodeConfigManager.SiteConfig.PortalID);

        if (_product != null)
        {
            if (_product.MasterPage.Length > 0 && _product.MasterPage != "Default")
            {
                string masterFilePath = ZNodeConfigManager.EnvironmentConfig.DataPath + "MasterPages/product/" + _product.MasterPage + ".master";

                if (System.IO.File.Exists(Server.MapPath(masterFilePath)))
                {
                    // If template master page exists, then override default master page
                    masterPageFilePath = masterFilePath;
                }
            }
        }
        else
        {
            Response.Redirect("~/default.aspx");
        }

        // Set master page
        this.MasterPageFile = masterPageFilePath;

        // Add to http context so it can be shared with user controls
        HttpContext.Current.Items.Add("Product", _product);


        // Set SEO Properties
        ZNodeSEO seo = new ZNodeSEO();       
        
        // Set SEO default values to the product 
        ZNodeMetaTags tags = new ZNodeMetaTags();
        seo.SEOTitle = tags.ProductTitle(_product);
        seo.SEODescription = tags.ProductDescription(_product);
        seo.SEOKeywords = tags.ProductKeywords(_product);

    }
}
