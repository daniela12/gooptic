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
using ZNode.Libraries.ECommerce.SEO;
using ZNode.Libraries.DataAccess.Entities;

public partial class Admin_Secure_catalog_SEO_pages_list : System.Web.UI.Page
{
    #region Protected Member Variables   
    protected string EditLink = "~/admin/secure/SEO/pages/pages_edit.aspx?itemid=";        
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            BindGridData();
    }
    #endregion

    #region Bind Methods
    /// <summary>
    /// Bind data to grid
    /// </summary>
    private void BindGridData()
    {
        ContentPageAdmin pageAdmin = new ContentPageAdmin();
        TList<ContentPage> pages = pageAdmin.GetPages(ZNodeConfigManager.SiteConfig.PortalID);
        pages.Sort("Name");

        uxGrid.DataSource = pages;
        uxGrid.DataBind();
    }
    #endregion

    #region Grid Events
    /// <summary>
    /// Event triggered when the grid page is changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        BindGridData();
    }

    /// <summary>
    /// Event triggered when a command button is clicked on the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        // Convert the row index stored in the CommandArgument
        // property to an Integer.
        int index = Convert.ToInt32(e.CommandArgument);

        // Get the values from the appropriate
        // cell in the GridView control.
        GridViewRow selectedRow = uxGrid.Rows[index];

        TableCell Idcell = selectedRow.Cells[0];
        string Id = Idcell.Text;

        if (e.CommandName == "Edit")
        {            
            Response.Redirect(EditLink + Id);
        }

    }

    #endregion
 
    # region Helper Methods
    /// <summary>
    /// Returns the path to open the html content pages
    /// </summary>
    /// <param name="pageName"></param>
    /// <returns></returns>
    public string GetPageURL(string pageName, object seoURL)
    {
        //if page name is Home, then it must be open with default page.
        if (pageName.Equals("Home"))
        {
            return "~/";
        }

        string seoUrl = "";

        if (seoURL != null)
        {
            if (seoURL.ToString().Length > 0)
                seoUrl = seoURL.ToString();
        }

        //otherwise the content pages should be open with content.aspx page
        //more Specific to Content pages
        return ZNodeSEOUrl.MakeURL(pageName, SEOUrlType.ContentPage, seoUrl);
    }

    #endregion
}
