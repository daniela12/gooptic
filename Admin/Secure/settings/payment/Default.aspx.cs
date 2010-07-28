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

public partial class Admin_Secure_settings_payment_Default : System.Web.UI.Page
{
    #region Protected Member Variables
    protected string CatalogImagePath = "";
    protected string AddLink = "~/admin/secure/settings/payment/add.aspx";
    protected string ViewLink = "NA";
    protected string EditLink = "~/admin/secure/settings/payment/add.aspx";
    protected string DeleteLink = "~/admin/secure/settings/payment/delete.aspx";
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
        StoreSettingsAdmin settingsAdmin = new StoreSettingsAdmin();
        uxGrid.DataSource = settingsAdmin.GetPaymentSettings(ZNodeConfigManager.SiteConfig.PortalID);
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
}
