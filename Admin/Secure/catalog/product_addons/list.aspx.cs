using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.DataAccess.Custom;

public partial class Admin_Secure_catalog_product_addons_list : System.Web.UI.Page
{
    #region Protected Variables
    protected static bool IsSearchEnabled = false;
    protected string CatalogImagePath = "";
    protected string AddLink = "~/admin/secure/catalog/product_addons/add.aspx";
    protected string ViewLink = "~/admin/secure/catalog/product_addons/view.aspx";
    protected string EditLink = "~/admin/secure/catalog/product_addons/add.aspx";
    protected string DeleteLink = "~/admin/secure/catalog/product_addons/delete.aspx";
    #endregion

    #region Page Load
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
 
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
        ProductAddOnAdmin ProdAddonAdminAccess = new ProductAddOnAdmin();

        uxGrid.DataSource = ProdAddonAdminAccess.GetAllAddOns();
        uxGrid.DataBind();
    }

    private void SearchAddOns()
    {
        ProductAddOnAdmin AdminAccess = new ProductAddOnAdmin();
        uxGrid.DataSource = AdminAccess.SearchAddOns(txtAddonName.Text.Trim(),txtAddOnTitle.Text.Trim(), txtsku.Text.Trim());
        uxGrid.DataBind();
    }
    #endregion

    #region General Events
    /// <summary>
    /// Add New Product AddOn Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddCategory_Click(object sender, System.EventArgs e)
    {
        Response.Redirect(AddLink);
    }
    /// <summary>
    /// Search Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchAddOns();
        IsSearchEnabled = true;
    }

    /// <summary>
    /// Clear Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("list.aspx");
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
            SearchAddOns(); //bind grid
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

    #region Helper methods

    /// <summary>
    /// Display the Category for a Category id
    /// </summary>
    /// <param name="CategoryID"></param>
    /// <returns></returns>
    public string GetRootCategory(Object CategoryID)
    {
        string path = "";
       
       CategoryHelper _Categoryhelper = new CategoryHelper();
       DataSet MyDataset=  _Categoryhelper.GetCategoryHierarchy(ZNodeConfigManager.SiteConfig.PortalID);
       foreach (DataRow dr in MyDataset.Tables[0].Rows)
       {
           if(dr["categoryid"].ToString() == CategoryID.ToString())
           path += ProductHelper.GetCategoryPath(dr["Name"].ToString(), dr["Parent1CategoryName"].ToString(), dr["Parent2CategoryName"].ToString());
       }
        return path;

    }

    #endregion
   
}
