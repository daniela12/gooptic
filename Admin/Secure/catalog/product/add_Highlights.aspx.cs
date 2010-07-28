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
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.Framework.Business;

public partial class Admin_Secure_catalog_product_add_Highlights : System.Web.UI.Page
{
    # region Private Variables
    protected string ViewPageLink = "~/admin/secure/catalog/product/view.aspx?mode=highlight";
    protected int ItemId = 0;
    protected DataSet MyDataSet = null;
    #endregion

    # region Page Load Event
    /// <summary>
    /// Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
            Bind();
            BindProductName();
            BindHighLightType();
        }
    }
    # endregion

    # region Bind Methods
    /// <summary>
    /// Bind highlight Grid - all Addons
    /// </summary>
    private void Bind()
    {
        HighlightAdmin AdminAccess = new HighlightAdmin();

        //List of product Highlights
        uxGrid.DataSource = AdminAccess.GetAllHighlight(ZNodeConfigManager.SiteConfig.PortalID);
        uxGrid.DataBind();
    }

    private void BindProductName()
    {
        ProductAdmin ProductAdminAccess = new ProductAdmin();
        Product entity = ProductAdminAccess.GetByProductId(ItemId);
        if (entity != null)
        {
            lblTitle.Text = lblTitle.Text + " for \"" + entity.Name + "\"";
        }
    }

    /// <summary>
    /// Binds Highlight Type drop-down list
    /// </summary>
    private void BindHighLightType()
    {
        ZNode.Libraries.Admin.HighlightAdmin highlight = new HighlightAdmin();
        ddlHighlightType.DataSource = highlight.GetAllHighLightType();
        ddlHighlightType.DataTextField = "Name";
        ddlHighlightType.DataValueField = "HighlightTypeId";
        ddlHighlightType.DataBind();
        ListItem item2 = new ListItem("ALL", "0");
        ddlHighlightType.Items.Insert(0, item2);
        ddlHighlightType.SelectedIndex = 0;          
    }

    /// <summary>
    /// Binds Search Data
    /// </summary>
    public DataSet BindSearchData()
    {
        HighlightAdmin AdminAccess = new HighlightAdmin();
        DataSet ds = AdminAccess.SearchProductHighlight(txtName.Text, int.Parse(ddlHighlightType.SelectedValue), ZNodeConfigManager.SiteConfig.PortalID);
        return ds;
    }

    # endregion

    # region Events

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddSelectedAddons_Click(object sender, EventArgs e)
    {
        ProductAdmin AdminAccess = new ProductAdmin();
        StringBuilder sb = new StringBuilder();

        //Loop through the grid values
        foreach (GridViewRow row in uxGrid.Rows)
        {
            CheckBox check = (CheckBox)row.Cells[0].FindControl("chkProductHighlight") as CheckBox;
            //Get AddOnId
            int HighlighID = int.Parse(row.Cells[1].Text);
            int DisplayOrder = int.Parse(row.Cells[4].Text);
            string name = row.Cells[2].Text;

            if (check.Checked)
            {
                ProductHighlight entity = new ProductHighlight();

                //Set Properties
                entity.ProductID = ItemId;
                entity.HighlightID = HighlighID;
                entity.DisplayOrder = DisplayOrder;
                
                if (!AdminAccess.IsHighlightExists(ItemId,HighlighID))
                {
                    AdminAccess.AddProductHighlight(entity);
                    check.Checked = false;
                }
                else
                {
                    sb.Append(name + ",");
                    lblErrorMessage.Visible = true;
                }
            }
        }

        if (sb.ToString().Length > 0)
        {
            sb.Remove(sb.ToString().Length - 1, 1);

            //Display Error message
            lblErrorMessage.Text = "The following highlight(s) are already associated with this product.<br/>" + sb.ToString();

        }
        else
        {
            Response.Redirect(ViewPageLink + "&itemid=" + ItemId);
        }               

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(ViewPageLink + "&itemid=" + ItemId);
    }

    /// <summary>
    /// Search Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MyDataSet = this.BindSearchData();
        DataView dv = new DataView(MyDataSet.Tables[0]);
        dv.Sort = "DisplayOrder Asc";
        uxGrid.DataSource = dv;
        uxGrid.DataBind();
    }

    /// <summary>
    /// Clear Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        txtName.Text = string.Empty;
        BindHighLightType();
        Bind();
        BindProductName();
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
        Bind();
    }
    #endregion
}
