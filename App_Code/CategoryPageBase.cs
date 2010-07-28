using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.DataAccess.Custom;
using ZNode.Libraries.ECommerce.SEO;

/// <summary>
/// Summary description for CategoryPageBase
/// </summary>
public class CategoryPageBase : ZNodePageBase
{
    #region Member Variables
    private int _categoryId;
    #endregion

    #region Events

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
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Default master page file path
        string masterPageFilePath = "~/themes/" + ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.Theme + "/category/category.master";

        // Get Category id from querystring  
        if (Request.Params["zcid"] != null)
        {
            _categoryId = int.Parse(Request.Params["zcid"]);
        }
        else
        {         
           throw(new ApplicationException("Invalid Category Request"));
        }

        // Retrieve category data
        ZNodeCategory category = ZNodeCategory.Create(_categoryId, ZNodeConfigManager.SiteConfig.PortalID);

        if (category != null)
        {
            if (category.MasterPage.Length > 0 && category.MasterPage != "Default")
            {
                string masterFilePath = ZNodeConfigManager.EnvironmentConfig.DataPath + "MasterPages/category/" + category.MasterPage + ".master";

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

        // Add to httpContext
        HttpContext.Current.Items.Add("Category", category);

        // Set SEO Properties 
        ZNodeSEO seo = new ZNodeSEO();

        // Set Default SEO values
        ZNodeMetaTags tags = new ZNodeMetaTags();
        seo.SEOTitle = tags.CategoryTitle(category);
        seo.SEODescription = tags.CategoryDescription(category);
        seo.SEOKeywords = tags.CategoryKeywords(category);
    }
    #endregion

}
