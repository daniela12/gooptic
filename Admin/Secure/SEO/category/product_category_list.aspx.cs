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
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.DataAccess.Custom;

public partial class Admin_Secure_catalog_SEO_product_category_list : System.Web.UI.Page
{
    #region Protected Variables
    protected string CatalogImagePath = "";
    protected string EditLink = "~/admin/secure/SEO/category/product_category_edit.aspx";
    protected static bool IsSearchEnabled = false;
    #endregion

    # region Protected properties
    private string GridViewSortDirection
    {

        get { return ViewState["SortDirection"] as string ?? "ASC"; }

        set { ViewState["SortDirection"] = value; }

    }
    # endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
            BindGridData();
    }
    #endregion

    #region Bind Methods
    /// <summary>
    /// Bind data to grid
    /// </summary>
    private void BindGridData()
    {
        CategoryAdmin categoryAdmin = new CategoryAdmin();
        DataSet ds = categoryAdmin.GetAllCategories(ZNodeConfigManager.SiteConfig.PortalID).ToDataSet(true);
        DataView dv = new DataView(ds.Tables[0]);
        dv.Sort = "Name";
        uxGrid.DataSource = dv;
        uxGrid.DataBind();
    }

    /// <summary>
    /// Bind Search data
    /// </summary>
    protected void BindSearchData()
    {
        CategoryAdmin categoryAdmin = new CategoryAdmin();
        DataSet ds = categoryAdmin.GetCategoriesBySerachData(txtCategoryName.Text.Trim());
        DataView dv = new DataView(ds.Tables[0]);
        dv.Sort = "Name";
        uxGrid.DataSource = dv;
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
        if (IsSearchEnabled)
        {
            uxGrid.PageIndex = e.NewPageIndex;
            this.BindSearchData();
        }
        else
        {
            uxGrid.PageIndex = e.NewPageIndex;
            this.BindGridData();
        }
    }

    protected void uxGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (IsSearchEnabled)
        {
            CategoryAdmin categoryAdmin = new CategoryAdmin();
            DataSet ds = categoryAdmin.GetCategoriesBySerachData(txtCategoryName.Text.Trim());
            uxGrid.DataSource = SortDataTable(ds, e.SortExpression, true);
            uxGrid.DataBind();
        }
        else
        {
            CategoryAdmin categoryAdmin = new CategoryAdmin();
            DataSet categoryList = categoryAdmin.GetAllCategories(ZNodeConfigManager.SiteConfig.PortalID).ToDataSet(true);
            uxGrid.DataSource = SortDataTable(categoryList, e.SortExpression, true);
            uxGrid.DataBind();
        }

        if (GetSortDirection() == "DESC")
        {
            e.SortDirection = SortDirection.Descending;
        }
        else
        {
            e.SortDirection = SortDirection.Ascending;
        }
    }

    /// <summary>
    /// Event triggered when a command button is clicked on the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        string Id = e.CommandArgument.ToString();

        if (e.CommandName == "Edit")
        {
            EditLink = EditLink + "?itemid=" + Id;
            Response.Redirect(EditLink);
        }
    }

    #endregion

    #region General Events

    /// <summary>
    /// Search Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindSearchData();
        IsSearchEnabled = true;
    }

    /// <summary>
    /// Clear Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("product_category_list.aspx");
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
        DataSet MyDataset = _Categoryhelper.GetCategoryHierarchy(ZNodeConfigManager.SiteConfig.PortalID);
        foreach (DataRow dr in MyDataset.Tables[0].Rows)
        {
            if (dr["categoryid"].ToString() == CategoryID.ToString())
                path += ProductHelper.GetCategoryPath(dr["Name"].ToString(), dr["Parent1CategoryName"].ToString(), dr["Parent2CategoryName"].ToString());
        }
        return path;

    }

    /// <summary>
    /// Sorting Data
    /// </summary>
    /// <param name="dataSet"></param>
    /// <param name="GridViewSortExpression"></param>
    /// <param name="isPageIndexChanging"></param>
    /// <returns></returns>
    protected DataView SortDataTable(DataSet dataSet, string GridViewSortExpression, bool isPageIndexChanging)
    {
        if (dataSet != null)
        {
            DataView dataView = new DataView(dataSet.Tables[0]);

            if (GridViewSortExpression.Length > 0)
            {
                if (isPageIndexChanging)
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GridViewSortDirection);
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GetSortDirection());
                }
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }
    }
    
    private string GetSortDirection()
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;

            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }
        return GridViewSortDirection;
    }
  
    #endregion
}
