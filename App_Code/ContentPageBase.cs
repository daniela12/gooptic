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
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.ECommerce.SEO;

/// <summary>
/// This class builds the Content Managed Page
/// </summary>
public partial class ContentPageBase : ZNodePageBase
{
    protected void Page_PreInit(object sender, EventArgs e)
    {   
        // Default master page file path
        string masterPageFilePath = "~/themes/" + ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.Theme + "/content/content.master";

        //get page id from querystring  
        string _pageName = "";
        if (Request.Params["page"] != null)
        {
            _pageName = Request.Params["page"];
        }
        else
        {
            _pageName = "home";
        }

        //get content page data
        ContentPage contentPage = ZNodeContentManager.GetPageByName(_pageName);

        if (contentPage != null)
        {
            if (contentPage.MasterPage != null && contentPage.MasterPage != "Default")
            {
                string masterFilePath = ZNodeConfigManager.EnvironmentConfig.DataPath + "MasterPages/Content/" + contentPage.MasterPage + ".master";

                // Only do the template override operation if the master page file exists                
                if (System.IO.File.Exists(Server.MapPath(masterFilePath)))
                {
                    // If template master page exists, then override default master page
                    masterPageFilePath = masterFilePath;
                }
            }
        }

        // Set master page
        this.MasterPageFile = masterPageFilePath;

        //add to context for control access
        HttpContext.Current.Items.Add("PageTitle", contentPage.Title);

        //SEO stuff
        ZNodeSEO seo = new ZNodeSEO();
        
        // Set Default SEO Values
        ZNodeMetaTags tags = new ZNodeMetaTags();
        seo.SEOTitle = tags.ContentTitle(contentPage);
        seo.SEODescription = tags.ContentDescription(contentPage);
        seo.SEOKeywords = tags.ContentKeywords(contentPage);

    }
}
