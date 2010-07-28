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

public partial class Admin_Secure_settings_ship_Default : System.Web.UI.Page
{
    #region Protected Member Variables
    protected string CatalogImagePath = "";
    protected string AddLink = "~/admin/secure/settings/shipping/add.aspx";
    protected string AddRuleLink = "~/admin/secure/settings/shipping/addRule.aspx";
    protected string ViewLink = "~/admin/secure/settings/shipping/view.aspx";
    protected string EditLink = "~/admin/secure/settings/shipping/add.aspx";
    protected string DeleteLink = "~/admin/secure/settings/shipping/delete.aspx";
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
        ShippingAdmin shipAdmin = new ShippingAdmin();
        uxGrid.DataSource = shipAdmin.GetAllShippingOptions(ZNodeConfigManager.SiteConfig.PortalID);
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
            if (e.CommandName == "View")
            {
                ViewLink = ViewLink + "?itemid=" + Id;
                Response.Redirect(ViewLink);
            }
            else if (e.CommandName == "Delete")
            {
                Response.Redirect(DeleteLink + "?itemid=" + Id);
            }
        }
    }

    #endregion

    #region Other Events
    /// <summary>
    /// Add Shipping Option Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddLink);
    }

    /// <summary>
    /// Add Shipping Rule Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShippingRules_Click(object sender, EventArgs e)
    {
        string shippingId = ((Button)sender).CommandArgument;
        Response.Redirect(AddRuleLink + "?sid=" + shippingId);
    }
    #endregion

    # region Helper Methods
    /// <summary>
    /// Method to show "Add Shipping Rule" button for Custom Shipping options
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool CheckForCustomShipping(string value)
    {
        if (int.Parse(value) == 1)
        {
            return true;
        }

        return false;
    }
    #endregion
    
}
