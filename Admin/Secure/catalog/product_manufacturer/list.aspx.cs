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
using System.Data.SqlClient;

public partial class Admin_Secure_catalog_product_manufacturer_list : System.Web.UI.Page
{        
    #region Protected Member Variables
    protected static bool IsSearchEnabled = false;
    protected string CatalogImagePath = "";
    protected string AddLink = "~/admin/secure/catalog/product_manufacturer/add.aspx";
    protected string EditLink = "~/admin/secure/catalog/product_manufacturer/edit.aspx";
    protected int ItemId = 0;
    #endregion    
    
    # region Protected properties
    private string GridViewSortDirection
    {

        get { return ViewState["SortDirection"] as string ?? "ASC"; }

        set { ViewState["SortDirection"] = value; }

    }
    # endregion

    #region Page Load
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["ItemID"] != null)
        {
            ItemId = int.Parse(Request.Params["ItemID"].ToString());
        }
        if (!Page.IsPostBack)
        {
            BindGridData();
        }
        lblError.Visible = false; 
    }
    #endregion    
    
    #region Bind Methods
    /// <summary>
    /// Bind data to grid
    /// </summary>
    private void BindGridData()
    {        
        ZNode.Libraries.Admin.ManufacturerAdmin ManuAdmin = new ZNode.Libraries.Admin.ManufacturerAdmin();
        DataSet ds = ManuAdmin.GetAllByPortalID(ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.PortalID).ToDataSet(true);
        DataView dv = new DataView(ds.Tables[0]);
        dv.Sort = "Name";
        uxGrid.DataSource = dv;                
        uxGrid.DataBind();
    }

    protected void BindSearchData()
    {
        ManufacturerAdmin manufaturer = new ManufacturerAdmin();
        DataSet ds = manufaturer.GetManufacturerBySearchData(txtManufacturerName.Text.Trim());        
        DataView dv = new DataView(ds.Tables[0]);
        dv.Sort = "Name";
        uxGrid.DataSource = dv; 
        uxGrid.DataBind();
    }
    #endregion

    #region General Events

    protected void btnAddManufacturer_Click(object sender, EventArgs e)
    {   
        //Redirect to Add Manufacturer Page
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
    protected void uxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Page")
        {
        }
        else
        {
            string Id = e.CommandArgument.ToString();

            if (e.CommandName == "Edit")
            {
                AddLink = AddLink + "?itemid=" + Id;
                Response.Redirect(AddLink);
            }
            else if (e.CommandName == "Delete")
            {
                   
                    ZNode.Libraries.Admin.ManufacturerAdmin adminaccess = new ZNode.Libraries.Admin.ManufacturerAdmin();
                    bool Check = false;
                    Check = adminaccess.Delete(int.Parse(Id));
                    if (Check)
                    {
                        this.BindGridData();
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Delete action could not be completed because the manufacturer is in use.";
                    }
                
            }

        }   
    }
    
    protected void uxGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (IsSearchEnabled)
        {
            ManufacturerAdmin manufaturer = new ManufacturerAdmin();
            DataSet ds = manufaturer.GetManufacturerBySearchData(txtManufacturerName.Text.Trim());
            uxGrid.DataSource = SortDataTable(ds, e.SortExpression, true);
            uxGrid.DataBind();
        }
        else
        {
            ManufacturerAdmin ManuAdmin = new ManufacturerAdmin();
            DataSet ds = ManuAdmin.GetAllByPortalID(ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.PortalID).ToDataSet(true);
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
    protected void uxGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        this.BindGridData();        
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
