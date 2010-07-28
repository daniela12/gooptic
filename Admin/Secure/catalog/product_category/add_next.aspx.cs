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

public partial class Admin_Secure_catalog_product_category_add_next : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAddCategory_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/catalog/product_category/add.aspx");
    }
    protected void btnCategoryList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/catalog/product_category/list.aspx");
    }
    protected void btnAddProduct_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/catalog/product/add.aspx");
    }
}
