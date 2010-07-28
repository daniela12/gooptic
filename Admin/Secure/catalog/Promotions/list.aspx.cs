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

public partial class Admin_Secure_catalog_Promotions_list : System.Web.UI.Page
{
    #region Protected Member Variables
    protected string CatalogImagePath = "";
    protected string ListPage = "~/admin/secure/catalog/promotions/list.aspx";
    protected string AddLink = "~/admin/secure/catalog/promotions/add.aspx";
    protected string DeleteLink = "~/admin/secure/catalog/promotions/delete.aspx?ItemID=";
    protected static bool IsSearchEnabled = false;
    #endregion  

    #region page load
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            IsSearchEnabled = false;
            BindDiscountTypeList();
            BindGridData();            
        }
    }
    #endregion

    #region Bind Methods
   
    // Bind data to grid   
    private void BindGridData()
    {
        ZNode.Libraries.Admin.PromotionAdmin couponbind = new ZNode.Libraries.Admin.PromotionAdmin();
        DataSet ds = couponbind.GetAllPromotions().ToDataSet(false);
        DataView dv = new DataView(ds.Tables[0]);
        dv.Sort = "displayorder asc";

        uxGrid.DataSource = dv;
        uxGrid.DataBind();
    }

    /// <summary>
    /// Bind Discount type dropdownlist
    /// </summary>
    private void BindDiscountTypeList()
    {
        PromotionAdmin AdminAccess = new PromotionAdmin();
        ddlDiscountTypes.DataSource = AdminAccess.GetAllDiscountTypes();
        ddlDiscountTypes.DataTextField = "Name";
        ddlDiscountTypes.DataValueField = "DiscountTypeId";
        ddlDiscountTypes.DataBind();

        ListItem li = new ListItem("All", "0");
        ddlDiscountTypes.Items.Insert(0, li);
    }

    /// <summary>
    /// Search Promotion
    /// </summary>
    private void SearchPromotions()
    {
        DataView dv = new DataView();

        if (ViewState["PromotionsList"]!=null)
        {
            dv = new DataView(ViewState["PromotionsList"] as DataTable);
        }
        else
        {
            ZNode.Libraries.Admin.PromotionAdmin couponbind = new ZNode.Libraries.Admin.PromotionAdmin();
            DataSet ds = couponbind.GetAllPromotions().ToDataSet(false);
            string filterQuery = "";            

            dv = new DataView(ds.Tables[0]);
            dv.Sort = "displayorder asc";

            # region Create row filter query 
            if (ddlDiscountTypes.SelectedValue != "0")
            {
                filterQuery = "DiscountTypeId = " +ddlDiscountTypes.SelectedValue + " and  ";
            }
            if (txtStartDate.Text.Trim().Length > 0 && txtEndDate.Text.Trim().Length == 0)
            {
                filterQuery = "StartDate >= '" + txtStartDate.Text.Trim() + "' and  ";
            }
            else if (txtEndDate.Text.Trim().Length > 0 && txtStartDate.Text.Trim().Length == 0)
            {
                filterQuery = "EndDate <='" + txtEndDate.Text.Trim() + "' and ";
            }
            else if(txtStartDate.Text.Trim().Length > 0 && txtEndDate.Text.Trim().Length > 0)
            {
                filterQuery = "StartDate >= '" + txtStartDate.Text.Trim() + "' and  EndDate <='" + txtEndDate.Text.Trim() + "' and ";
            }

            if (txtName.Text.Trim().Length > 0)
                filterQuery += "Name like '%" + txtName.Text.Trim() + "%' and ";

            if (txtAmount.Text.Trim().Length > 0)
                filterQuery += "Discount >= " + txtAmount.Text.Trim() + " and ";

            if (CouponCode.Text.Trim().Length > 0)
                filterQuery += "CouponCode like '%" + CouponCode.Text.Trim() + "%' and ";

            #endregion

            //if filter query has conditition, if any
            if (filterQuery.Length > 0)
            {
                //Apply filter
                dv.RowFilter = filterQuery + "description like '%'";                    
            }            

            ViewState.Add("PromotionsList", dv.ToTable());
        }

        uxGrid.DataSource = dv;
        uxGrid.DataBind();
    }
    #endregion

    #region General Events

    /// <summary>
    /// Add Promtion Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddCoupon_Click(object sender, EventArgs e)
    {
        //Redirect to Add promotion Page    
        Response.Redirect(AddLink);
    }

    /// <summary>
    /// Search Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IsSearchEnabled = true;
        ViewState.Remove("PromotionsList");
        SearchPromotions();
    }

    /// <summary>
    /// Clear Search Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect(ListPage);
    }
    # endregion

    # region Grid Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        if (IsSearchEnabled)        
            SearchPromotions();
        else
            BindGridData();

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Page")
        {
        }
        else
        {
            int index = Convert.ToInt32(e.CommandArgument);

            // Get the values from the appropriate
            // cell in the GridView control.
            GridViewRow selectedRow = uxGrid.Rows[index];

            TableCell Idcell = selectedRow.Cells[0];
            string Id = Idcell.Text;

            if (e.CommandName == "Edit")
            {
                AddLink = AddLink + "?ItemID=" + Id;
                Response.Redirect(AddLink);
            }
            else if (e.CommandName == "Delete")
            {
                Response.Redirect(DeleteLink + Id);
            }

        }
    }
    
     #endregion

    # region Helper Methods
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected string DiscountTypeName(object DiscountTypeID)
    {
        if (DiscountTypeID != null)
        {
            int discTypeID = int.Parse(DiscountTypeID.ToString());

            PromotionAdmin promotionAdmin = new PromotionAdmin();
            DiscountType entity = promotionAdmin.GetByDiscountTypeID(discTypeID);

            if (entity != null)
                return entity.Name;
        }
        return "";
    }
    #endregion
}
