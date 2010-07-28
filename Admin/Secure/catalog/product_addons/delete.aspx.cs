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

public partial class Admin_Secure_catalog_product_addons_delete : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    protected string ProductAddOnName = string.Empty;
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
        ProductAddOnAdmin AddOnAdmin = new ProductAddOnAdmin();
        AddOn Entity = AddOnAdmin.GetByAddOnId(ItemId);

        if (Entity != null)
        {
            ProductAddOnName = Entity.Name;
        }
        else
        {
            throw (new ApplicationException("Product Add-On Requested could not be found."));
        }
    }
    #endregion

    #region Events
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/catalog/product_addons/list.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ProductAddOnAdmin AddOnAdmin = new ProductAddOnAdmin();
        AddOn AddOn = new AddOn();
        AddOn.AddOnID = ItemId;        

        bool retval = AddOnAdmin.DeleteAddOn(AddOn);

        if (retval)
        {
            Response.Redirect("~/admin/secure/catalog/product_addons/list.aspx");
        }
        else
        {
            lblMsg.Text = "An error occurred and the product Add-On could not be deleted. Please ensure that this Add-On does not contain Add-On Values or products. If it does, then delete the Add-On values and products first.";
        }
    }
    #endregion
}
