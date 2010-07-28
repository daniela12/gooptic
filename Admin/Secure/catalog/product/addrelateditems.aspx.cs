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
using ZNode.Libraries.DataAccess.Custom;
using ZNode.Libraries.Framework.Business;

public partial class Admin_Secure_catalog_product_addrelateditems : System.Web.UI.Page
{
    # region Protected Member Variables
    protected int ItemID = 0;
    protected string ViewPage = "view.aspx?itemid=";
    # endregion

    # region Events

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["itemid"] != null)
        {
            ItemID = int.Parse(Request.Params["itemid"].ToString());
        }

        if (!Page.IsPostBack)
        {
            this.BindData();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Update_Click(object sender, EventArgs e)
    {
        ProductCrossSellAdmin _ProdCrossAdmin = new ProductCrossSellAdmin();
        bool status = true;

        // Loop through the grid values
        foreach (GridViewRow row in uxGrid.Rows)
        {
            CheckBox prodSelected = (CheckBox)row.Cells[0].FindControl("chkProduct") as CheckBox;

            if (prodSelected.Checked)
            {
                // Get ProductId
                int productId = int.Parse(row.Cells[1].Text);

                status &= _ProdCrossAdmin.Insert(ZNodeConfigManager.SiteConfig.PortalID, ItemID, productId);
            }
        }

        if (status)
        {
            Response.Redirect(ViewPage + ItemID + "&mode=crosssell");
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = "You are trying to add same product as Related Item";
        }
    }

    /// <summary>
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(ViewPage + ItemID + "&mode=crosssell");
    }

    /// <summary>
    /// Search Button Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindSearchProduct();
    }

    /// <summary>
    /// Clear Button Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("addrelateditems.aspx?itemid=" + ItemID);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        BindSearchProduct();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ViewPage + ItemID + "&mode=crosssell");
    }
    # endregion

    # region Bind Methods

    /// <summary>
    /// 
    /// </summary>
    private void BindData()
    {
        ListItem defaultItem = new ListItem("ALL", "0");

        // Add the manufacturers to the drop-down list
        ManufacturerAdmin manuadmin = new ManufacturerAdmin();
        dmanufacturer.DataSource = manuadmin.GetAllByPortalID(ZNodeConfigManager.SiteConfig.PortalID);
        dmanufacturer.DataTextField = "Name";
        dmanufacturer.DataValueField = "ManufacturerID";
        dmanufacturer.DataBind();

        dmanufacturer.Items.Insert(0, defaultItem);
        // making ALL as the default value of the drop-down list box
        dmanufacturer.SelectedIndex = 0;

        // Add Producttypes to the drop-down list
        ProductTypeAdmin producttypeadmin = new ProductTypeAdmin();
        dproducttype.DataSource = producttypeadmin.GetAllProductType(ZNodeConfigManager.SiteConfig.PortalID);
        dproducttype.DataTextField = "Name";
        dproducttype.DataValueField = "Producttypeid";
        dproducttype.DataBind();

        dproducttype.Items.Insert(0, defaultItem);
        dproducttype.SelectedIndex = 0;

        // Add Product categories to the drop-down list
        CategoryAdmin categoryadmin = new CategoryAdmin();
        dproductcategory.DataSource = categoryadmin.GetAllCategories(ZNodeConfigManager.SiteConfig.PortalID);
        dproductcategory.DataTextField = "Name";
        dproductcategory.DataValueField = "Categoryid";
        dproductcategory.DataBind();


        dproductcategory.Items.Insert(0, defaultItem);
        dproductcategory.SelectedIndex = 0;
    }

    /// <summary>
    /// Search a Product by name,productnumber,sku,manufacturer,category,producttype
    /// </summary>
    private void BindSearchProduct()
    {
        ProductAdmin prodadmin = new ProductAdmin();
        DataSet ds = prodadmin.SearchProduct(txtproductname.Text.Trim(), txtproductnumber.Text.Trim(), txtsku.Text.Trim(), dmanufacturer.SelectedValue, dproducttype.SelectedValue, dproductcategory.SelectedValue);

        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlProductList.Visible = true;
            uxGrid.DataSource = ds;
            uxGrid.DataBind();
        }
        else { pnlProductList.Visible = false; }
    }
    # endregion    
}