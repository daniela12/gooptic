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

public partial class Admin_Secure_ProductTypes_search : System.Web.UI.Page
{
    #region Protected Member Variables
    protected string CatalogImagePath = "";
    protected string AddLink = "~/admin/secure/catalog/product_type/add.aspx";
    protected string ViewLink = "~/admin/secure/catalog/product_type/view.aspx";
    protected string EditLink = "~/admin/secure/catalog/product_type/add.aspx";
    protected string DeleteLink = "~/admin/secure/catalog/product_type/delete.aspx";
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
        ZNode.Libraries.Admin.ProductTypeAdmin prodTypeAdmin = new ZNode.Libraries.Admin.ProductTypeAdmin();
        DataSet ds = prodTypeAdmin.GetAllProductTypes(ZNodeConfigManager.SiteConfig.PortalID).ToDataSet(true);
        DataView dv = new DataView(ds.Tables[0]);
        dv.Sort = "Name";
        uxGrid.DataSource = dv;        
        uxGrid.DataBind();
    }

    /// <summary>
    /// Bind Search Data to Grid
    /// </summary>
    protected void BindSearchData()
    {
        ZNode.Libraries.Admin.ProductTypeAdmin prodTypeAdmin = new ZNode.Libraries.Admin.ProductTypeAdmin();
        DataSet ds = prodTypeAdmin.GetProductTypeBySearchData(txtproductType.Text.Trim(), txtDescription.Text.Trim());
        DataView dv = new DataView(ds.Tables[0]);
        dv.Sort = "Name";
        uxGrid.DataSource = dv;  
        uxGrid.DataBind();
    }
    

    #endregion

    #region General Events

    protected void btnAddProductType_Click(object sender, System.EventArgs e)
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
        if (IsSearchEnabled)
        {
            uxGrid.PageIndex = e.NewPageIndex;
            BindSearchData();
        }
        else
        {
            uxGrid.PageIndex = e.NewPageIndex;
            BindGridData();
        }
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

    protected void uxGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (IsSearchEnabled)
        {
            ZNode.Libraries.Admin.ProductTypeAdmin prodTypeAdmin = new ZNode.Libraries.Admin.ProductTypeAdmin();
            DataSet ds = prodTypeAdmin.GetProductTypeBySearchData(txtproductType.Text.Trim(), txtDescription.Text.Trim());
            uxGrid.DataSource = SortDataTable(ds, e.SortExpression, true);
            uxGrid.DataBind();
        }
        else
        {
            ZNode.Libraries.Admin.ProductTypeAdmin prodTypeAdmin = new ZNode.Libraries.Admin.ProductTypeAdmin();
            uxGrid.DataSource = SortDataTable(prodTypeAdmin.GetAllProductTypes(ZNodeConfigManager.SiteConfig.PortalID).ToDataSet(true), e.SortExpression, true);
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
        if (e.CommandName == "page")
        {
        }
        else
        {            
            string Id = e.CommandArgument.ToString();
            
            if (e.CommandName == "Edit")
            {
                EditLink = EditLink + "?itemid=" + Id;
                Response.Redirect(EditLink);
            }
            else if (e.CommandName == "View")
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

    #region Helper Methods
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
    # endregion

}
