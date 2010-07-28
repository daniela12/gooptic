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

public partial class Admin_Secure_products_list : System.Web.UI.Page
{
    #region Protected Variables
    protected int productid;   
    protected string AddLink = "add.aspx";
    protected string PreviewLink = string.Empty; 
    protected string EditLink = "add.aspx";
    protected string DeleteLink = "delete.aspx?itemid=";
    protected string DetailsLink = "~/admin/secure/catalog/product/view.aspx?itemid=";
    protected string Edit = "add.aspx?itemid=";
    protected string Image = "Views.aspx?itemid=";
    protected string Inventory = "inventory.aspx?itemid=";    
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        PreviewLink = "http://" + ZNodeConfigManager.SiteConfig.DomainName + "/product.aspx"; // ZNodeConfigManager.EnvironmentConfig.ApplicationPath + "/product.aspx";

        if (!Page.IsPostBack)
        {
           this.BindDropDownData();
           this.BindGridData();
        }

    }
    #endregion
  
    #region Bind Grid

    /// <summary>
    /// Bind data to grid
    /// </summary>
    public void BindGridData()
    {
        ZNode.Libraries.Admin.ProductAdmin ProdAdmin = new ZNode.Libraries.Admin.ProductAdmin();
        TList<Product> prodList = ProdAdmin.GetAllProducts(ZNodeConfigManager.SiteConfig.PortalID);
        if(prodList.Count > 0) // if items exists on this list, then sort the list items with display order
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
        if (e.CommandName == "Page")
        {
        }
        else
        {
            if (e.CommandName == "Manage")
            {
                Response.Redirect(DetailsLink + e.CommandArgument);
            }                      
            else if (e.CommandName == "Delete")
            {
                Response.Redirect(DeleteLink + e.CommandArgument);
            }
        }
    }

    #endregion

    #region General Events
    /// <summary>
    /// AddNew Product button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddProduct_Click(object sender, EventArgs e)
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

    #region Helper Functions
    
    /// <summary>
    /// Format the Price of a product and return in string format
    /// </summary>
    /// <param name="fieldvalue"></param>
    /// <returns></returns>
    protected string DisplayPrice(Object fieldvalue)
    {
        if (fieldvalue == DBNull.Value)
        {
            return "$0.00";
        }
        else
        {
            if (Convert.ToInt32(fieldvalue) == 0)
            {
                return "$0.00";
            }
            else
            {
                return String.Format("${0:#.00}", fieldvalue);
            }
        }
    }


    /// <summary>
    /// Search a Product for a Keyword
    /// </summary>
    protected void BindSearchData()
    {
       ZNode.Libraries.Admin.ProductAdmin _ProdAdmin =new ProductAdmin();
        
       uxGrid.DataSource = _ProdAdmin.FindProducts(ZNodeConfigManager.SiteConfig.PortalID, txtproductname.Text.Trim());
       uxGrid.DataBind();
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
            if(productPrice.ToString().Length > 0)
            return decimal.Parse(productPrice.ToString()).ToString("c");
        }

        return "";
    }
    #endregion
}
