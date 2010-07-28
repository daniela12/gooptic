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
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.Admin;

public partial class Admin_Secure_products_delete : System.Web.UI.Page
{


    #region Protected Member Variables
    protected int ItemID;
    protected string ProductName = string.Empty;
    protected string CancelLink = "list.aspx";
    #endregion

    #region Page Load
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get ItemId from querystring   
        if (Request.Params["Itemid"] != null)
        {
            ItemID = int.Parse(Request.Params["Itemid"].ToString());
            this.BindData();
        }
        else
        {
            ItemID = 0;
        }

    }
    #endregion

    # region Bind Data

    /// <summary>
    /// Bind the data for a Product id
    /// </summary>
    public void BindData()
    {
        ProductAdmin _ProductAdmin = new ProductAdmin();
        Product ProductList = _ProductAdmin.GetByProductId(ItemID);
        if (ProductList != null)
        {
            ProductName = ProductList.Name;
        }
    }
    # endregion

    #region General Events

    /// <summary>
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(CancelLink);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ProductAdmin _ProductAdmin = new ProductAdmin();
        bool Retvalue = _ProductAdmin.DeleteByProductID(ItemID, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.PortalID);
        
        
        if (Retvalue)
        {
            Response.Redirect(CancelLink);
        }
        else
        {
            lblErrorMessage.Text = "The product can not be deleted until all associated items are removed. Please ensure that this product does not contain cross-sell items, product images, or skus. If it does, then delete these Items first.";
        }

    }
    #endregion
}
