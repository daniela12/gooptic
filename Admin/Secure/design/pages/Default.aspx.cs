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

public partial class Admin_Secure_design_page_Default : System.Web.UI.Page
{
    #region Protected Member Variables
    protected string ListLink = "~/admin/secure/design/pages/default.aspx";
    protected string AddLink = "~/admin/secure/design/pages/add.aspx";
    protected string RevertLink = "~/admin/secure/design/pages/revert.aspx";
    protected string EditLink = "~/admin/secure/design/pages/add.aspx";
    protected string DeleteLink = "~/admin/secure/design/pages/delete.aspx";
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
    /// Event triggered when an item is deleted from the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        BindGridData();
    }

    /// <summary>
    /// Event triggered when a command button is clicked on the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "page")
        {
        }
        else
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
                EditLink = EditLink + "?itemid=" + Id;
                Response.Redirect(EditLink);
            }
            else if (e.CommandName == "Publish")
            {
                ContentPageAdmin pageAdmin = new ContentPageAdmin();
                ContentPage contentPage = pageAdmin.GetPageByID(int.Parse(Id));
                pageAdmin.PublishPage(contentPage);
                Response.Redirect(ListLink);
                
            }
            else if (e.CommandName == "Revert")
            {
                RevertLink = RevertLink + "?itemid=" + Id;
                Response.Redirect(RevertLink);
            }
            else if (e.CommandName == "Delete")
            {
                Response.Redirect(DeleteLink + "?itemid=" + Id);
            }
        }
    }

    #endregion

    #region Other Events
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddLink);
    }
    #endregion

    # region Helper Methods
    /// <summary>
    /// Returns the path to open the html content pages
    /// </summary>
    /// <param name="pageName"></param>
    /// <returns></returns>
    public string GetPageURL(string pageName,object seoURL)
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
