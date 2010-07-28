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
using ZNode.Libraries.DataAccess.Entities;

public partial class Admin_Secure_design_Page_Revert : System.Web.UI.Page
{
    #region Protected Member Variables
    protected int ItemId;
    protected string ListLink = "~/admin/secure/design/pages/default.aspx";
    protected string AddLink = "~/admin/secure/design/pages/add.aspx";
    protected string RevertLink = "~/admin/secure/design/pages/revert.aspx";
    protected string EditLink = "~/admin/secure/design/pages/add.aspx";
    protected string DeleteLink = "~/admin/secure/design/pages/delete.aspx";
    protected string PageName = string.Empty;
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
            BindGridData();
        }
    }
    #endregion

    #region Bind Methods
    /// <summary>
    /// Bind data to grid
    /// </summary>
    private void BindGridData()
    {
        ContentPageAdmin pageAdmin = new ContentPageAdmin();
        TList<ContentPageRevision> revisionList = pageAdmin.GetPageRevisions(ItemId);
        revisionList.Sort("UpdateDate Desc");

        ContentPage page = pageAdmin.GetPageByID(ItemId);
        PageName = page.Name;

        uxGrid.DataSource = revisionList;
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


           if (e.CommandName == "Revert")
            {
                ContentPageAdmin pageAdmin = new ContentPageAdmin();
                bool retval = pageAdmin.RevertToRevision(int.Parse(Id), HttpContext.Current.User.Identity.Name);

                if (retval)
                {
                    BindGridData();
                    lblMsg.Text = "Successfully reverted page version.";
                }
                else
                {
                    lblError.Text = "Unable to revert to the requested version.";
                }
                //Response.Redirect(ListLink);
            }
            
        }
    }

    #endregion

    #region Other Events
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddLink);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ListLink);
    }
    #endregion
    
}
