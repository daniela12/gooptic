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


public partial class Admin_Secure_catalog_SEO_product_list : System.Web.UI.Page
{
    #region Protected Variables
    protected int productid;
    protected string EditLink = "product_edit.aspx?itemid=";    
    #endregion

    #region Page_Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.BindDropDownData();
            this.BindGridData();
        }
    }

    #endregion

    #region Bind Methods

    /// <summary>
    /// Bind data to grid
    /// </summary>
    public void BindGridData()
    {
        ZNode.Libraries.Admin.ProductAdmin ProdAdmin = new ZNode.Libraries.Admin.ProductAdmin();
        TList<Product> prodList = ProdAdmin.GetAllProducts(ZNodeConfigManager.SiteConfig.PortalID);
        if (prodList.Count > 0) // if items exists on this list, then sort the list items with display order
            prodList.Sort("DisplayOrder");

        uxGrid.DataSource = prodList;
        uxGrid.DataBind();
    }

    /// <summary>
    /// Bind data to the drop down list
    /// </summary>
    public void BindDropDownData()
    {
        //Add the manufacturers to the drop-down list
        ManufacturerAdmin manuadmin = new ManufacturerAdmin();
        dmanufacturer.DataSource = manuadmin.GetAllByPortalID(ZNodeConfigManager.SiteConfig.PortalID);
        dmanufacturer.DataTextField = "Name";
        dmanufacturer.DataValueField = "ManufacturerID";
        dmanufacturer.DataBind();
        ListItem item = new ListItem("ALL", "0");
        dmanufacturer.Items.Insert(0, item);
        //making ALL as the default value of the drop-down list box
        dmanufacturer.SelectedIndex = 0;

        //Add Producttypes to the drop-down list
        ProductTypeAdmin producttypeadmin = new ProductTypeAdmin();
        dproducttype.DataSource = producttypeadmin.GetAllProductType(ZNodeConfigManager.SiteConfig.PortalID);
        dproducttype.DataTextField = "Name";
        dproducttype.DataValueField = "Producttypeid";
        dproducttype.DataBind();
        ListItem item1 = new ListItem("ALL", "0");
        dproducttype.Items.Insert(0, item1);
        dproducttype.SelectedIndex = 0;

        //Add Product categories to the drop-down list
        CategoryAdmin categoryadmin = new CategoryAdmin();
        dproductcategory.DataSource = categoryadmin.GetAllCategories(ZNodeConfigManager.SiteConfig.PortalID);
        dproductcategory.DataTextField = "Name";
        dproductcategory.DataValueField = "Categoryid";
        dproductcategory.DataBind();
        ListItem item2 = new ListItem("ALL", "0");
        dproductcategory.Items.Insert(0, item2);
        dproductcategory.SelectedIndex = 0;
    }

    /// <summary>
    /// Search a Product by name,productnumber,sku,manufacturer,category,producttype
    /// </summary>
    private void BindSearchProduct()
    {
        ProductAdmin prodadmin = new ProductAdmin();
        uxGrid.DataSource = prodadmin.SearchProduct(txtproductname.Text.Trim(), txtproductnumber.Text.Trim(), txtsku.Text.Trim(), dmanufacturer.SelectedValue, dproducttype.SelectedValue, dproductcategory.SelectedValue);
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
        if ((txtproductname.Text == "") && (txtproductnumber.Text == "") && (txtsku.Text == "") && (dmanufacturer.SelectedValue == Convert.ToString(0)) && (dproductcategory.SelectedValue == Convert.ToString(0)) && (dproducttype.SelectedValue == Convert.ToString(0)))
        {
            BindGridData();
        }
        else
        {
            this.BindSearchProduct();
        }
    }

    /// <summary>
    /// Event triggered when a command button is clicked on the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Manage")
        {
            Response.Redirect(EditLink + e.CommandArgument);
        }
    }

    #endregion

    #region General Events

    /// <summary>
    /// Search button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.BindSearchProduct();
    }

    /// <summary>
    /// Clear button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtproductname.Text = "";
        txtproductnumber.Text = "";
        txtsku.Text = "";
        this.BindDropDownData();
        this.BindGridData();
    }   

    #endregion

    # region FormatPrice Helper method
    /// <summary>
    /// Returns formatted price to the grid
    /// </summary>
    /// <param name="productPrice"></param>
    /// <returns></returns>
    protected string FormatPrice(object productPrice)
    {
        if (productPrice != null)
        {
            if (productPrice.ToString().Length > 0)
                return decimal.Parse(productPrice.ToString()).ToString("c");
        }

        return "";
    }
    #endregion
}
