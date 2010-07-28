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

public partial class Admin_Secure_categories_delete : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    protected string ProductCategoryName = string.Empty;
    #endregion
    

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

        BindData();
    }

    #region Bind Data
    /// <summary>
    /// Bind data to the fields on the screen
    /// </summary>
    protected void BindData()
    {
        CategoryAdmin categoryAdmin = new CategoryAdmin();
        Category category = categoryAdmin.GetByCategoryId(ItemId);

        if (category != null)
        {
            ProductCategoryName = category.Name;
        }
        else
        {
            throw (new ApplicationException("Category Requested could not be found."));
        }
    }
    #endregion

    #region Events
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/catalog/product_category/list.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Category category = new Category();
        category.CategoryID = ItemId;
        category.PortalID = ZNodeConfigManager.SiteConfig.PortalID;

        CategoryAdmin categoryAdmin = new CategoryAdmin();
        bool retval = categoryAdmin.Delete(category);

        if (retval)
        {
            Response.Redirect("~/admin/secure/catalog/product_category/list.aspx");
        }
        else
        {
            lblMsg.Text = "An error occurred and the category could not be deleted. Please ensure that this category does not contain sub-categories or products. If it does, then delete the sub-categories and products first.";
        }
    }
    #endregion
}
