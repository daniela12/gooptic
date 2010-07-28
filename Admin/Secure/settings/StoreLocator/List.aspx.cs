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
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.DataAccess.Custom;

public partial class Admin_Secure_settings_StoreLocator_List : System.Web.UI.Page
{

    #region Protected Variables  
    protected string AddLink = "~/admin/secure/settings/StoreLocator/Add.aspx";
    protected string ViewLink = "~/admin/secure/settings/StoreLocator/View.aspx";   
    protected string DeleteLink = "~/admin/secure/settings/StoreLocator/Delete.aspx";
    protected static bool IsSearchEnabled = false;
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            this.BindGridData();
        }
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
        //Check if search is Enabled
        if (IsSearchEnabled)
        {
            uxGrid.PageIndex = e.NewPageIndex;
            BindSearchStore(); //bind grid
        }
        else
        {
            uxGrid.PageIndex = e.NewPageIndex;
            BindGridData();
        }
    }

    /// <summary>
    /// Event triggered when a command button is clicked on the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Page")
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


            if (e.CommandName == "Manage")
            {
                AddLink = AddLink + "?ItemID=" + Id;
                Response.Redirect(AddLink);                
            }
            else if (e.CommandName == "Delete")
            {
                Response.Redirect(DeleteLink + "?ItemID=" + Id);
            }
        }
    }

    #endregion

    #region Bind

    /// <summary>
    /// Search a Store by Store Name, ZipCode , State and City
    /// </summary>
    private void BindSearchStore()
    {
        StoreLocatorAdmin AdminAccess = new StoreLocatorAdmin();
        uxGrid.DataSource = AdminAccess.SearchStore(txtstorename.Text.Trim(), txtzipcode.Text.Trim(), txtcity.Text.Trim(), txtstate.Text.Trim());      
        uxGrid.DataBind();
    }
     
    /// <summary>
    /// Bind data to grid
    /// </summary>
    private void BindGridData()
    {
        StoreLocatorAdmin store = new StoreLocatorAdmin();
        uxGrid.DataSource = store.GetAllStore();
        uxGrid.DataBind();       
    }

    #endregion

    #region General Events

    /// <summary>
    /// Add New Store Detail
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddStore_Click(object sender, System.EventArgs e)
    {
        Response.Redirect(AddLink);
    }

    /// <summary>
    /// Search button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.BindSearchStore();
        IsSearchEnabled = true;
    }

    /// <summary>
    /// Clear button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtcity.Text = "";
        txtstate.Text = "";
        txtstorename.Text = "";
        txtzipcode.Text = "";
        this.BindGridData();
    }   

    #endregion
}
