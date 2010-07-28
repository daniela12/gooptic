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
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.ECommerce.UserAccount;


/// <summary>
/// Summary description for DefaultPageBase.cs
/// </summary>
public class DefaultPageBase : ZNodePageBase
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig == null)
        {
            throw (new ApplicationException("Could not retrieve site settings from the database. Please check your database connection by browsing to Diagnostics.aspx"));
        }
        
        this.MasterPageFile = "~/themes/" + ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.Theme + "/home/home.master";

        //get Home page data
        ContentPage contentPage = ZNodeContentManager.GetPageByName("home");

        //add to context for control access
        HttpContext.Current.Items.Add("PageTitle", contentPage.Title);

        //SEO stuff
        ZNodeSEO seo = new ZNodeSEO();
        seo.SEODescription = contentPage.SEOMetaDescription;
        seo.SEOKeywords = contentPage.SEOMetaKeywords;
        seo.SEOTitle = contentPage.SEOTitle;

        //get the user account from session
        ZNodeUserAccount _userAccount = (ZNodeUserAccount) ZNodeUserAccount.CurrentAccount();
    }
}
