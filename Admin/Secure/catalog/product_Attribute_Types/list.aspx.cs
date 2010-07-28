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
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.Admin;
using ZNode.Libraries.Framework.Business;

public partial class Admin_Secure_catalog_product_Attribute_Types_list : System.Web.UI.Page
{

    #region Protected Member Variables
    protected string AddLink = "~/admin/secure/catalog/product_Attribute_Types/add.aspx";
    protected string DeleteLink = "~/admin/secure/catalog/product_Attribute_Types/delete.aspx?itemid=";
    protected string ViewLink = "~/admin/secure/catalog/product_Attribute_Types/view.aspx?itemid=";
    protected string EditLink = "~/admin/secure/catalog/product_Attribute_Types/add.aspx?itemid=";
    protected static bool IsSearchEnabled = false;
    #endregion

    # region Protected properties
    private string GridViewSortDirection
    {

        get { return ViewState["SortDirection"] as string ?? "ASC"; }

        set { ViewState["SortDirection"] = value; }

    }
    # endregion

    # region General Events

    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.BindGrid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
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
    # endregion

    # region Grid Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() == "page")
        {  }
        else
        {
            string Id = e.CommandArgument.ToString();

            if (e.CommandName == "Edit")
            {
                Response.Redirect(EditLink + Id);
            }
            else if (e.CommandName == "View")
            {
                Response.Redirect(ViewLink + Id);
            }
            else if (e.CommandName == "Delete")
            {
                Response.Redirect(DeleteLink +  Id);
            }
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (IsSearchEnabled)
        {
            AttributeTypeAdmin AttributeAccess = new AttributeTypeAdmin();
            DataSet ds = AttributeAccess.GetAttributeBySearchData(txtAttributeName.Text.Trim());
            uxGrid.DataSource = SortDataTable(ds, e.SortExpression, true);
            uxGrid.DataBind();
        }
        else
        {
            AttributeTypeAdmin _AdminAccess = new AttributeTypeAdmin();
            DataSet ds = _AdminAccess.GetAll().ToDataSet(true);
            uxGrid.DataSource = SortDataTable(ds, e.SortExpression, true);
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
    /// 
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
            this.BindGrid();
        }
    }

    # endregion
    
    # region Bind Methods
    /// <summary>
    /// Bind Grid
    /// </summary>
    private void BindGrid()
    {
        AttributeTypeAdmin _AdminAccess = new AttributeTypeAdmin();
        DataSet ds = _AdminAccess.GetAll().ToDataSet(true);
        DataView dv = new DataView(ds.Tables[0]);
        dv.Sort = "Name";
        uxGrid.DataSource = dv;
        uxGrid.DataBind();
    }
    protected void BindSearchData()
    {
        AttributeTypeAdmin AttributeAccess = new AttributeTypeAdmin();
        DataSet ds = AttributeAccess.GetAttributeBySearchData(txtAttributeName.Text.Trim());
        DataView dv = new DataView(ds.Tables[0]);
        dv.Sort = "Name";
        uxGrid.DataSource = dv;
        uxGrid.DataBind();
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
