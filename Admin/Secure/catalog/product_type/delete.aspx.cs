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

public partial class Admin_Secure_categories_type_delete : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    protected string ProductCategoryName = string.Empty;
    #endregion

    # region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {       
        // Get ItemId from querystring        
        if (Request.Params["itemid"] != null)
        {
            ItemId = int.Parse(Request.Params["itemid"]);
            BindData();
        }
        else
        {
            ItemId = 0;
        }

    }
    # endregion

    #region Bind Data
    /// <summary>
    /// Bind data to the fields on the screen
    /// </summary>
    protected void BindData()
    {
        ProductTypeAdmin ProductTypeAdmin = new ProductTypeAdmin();
        ProductType ProdType = ProductTypeAdmin.GetByProdTypeId(ItemId);


        if (ProdType != null)
        {
            ProductCategoryName = ProdType.Name;
        }
        else
        {
            throw (new ApplicationException("Product Type Requested could not be found."));
        }
    }
    #endregion

    #region Events

    /// <summary>
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("list.aspx");
    }

    /// <summary>
    /// Delete Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ProductTypeAdmin ProdTypeAdmin = new ProductTypeAdmin();

        ProductType ProdType = new ProductType();
        ProdType.ProductTypeId = ItemId;
        ProdType.PortalId = ZNodeConfigManager.SiteConfig.PortalID;
        bool Check = false;
        Check = ProdTypeAdmin.Delete(ProdType);
        if (Check)
        {
            Response.Redirect("list.aspx");
        }
        else
        {
            lblError.Text = "* Delete action could not be completed because the Product Type is in use.";            
        }
              
    }
    #endregion
}
