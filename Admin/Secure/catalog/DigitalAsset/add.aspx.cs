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

public partial class Admin_Secure_catalog_DigitalAsset_add : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;    
    #endregion

    #region Page Load
    /// <summary>
    /// Page Load Event
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

        if (Page.IsPostBack == false)
        {
            //if edit func then bind the data fields
            if (ItemId > 0)
            {
                lblTitle.Text += SetProductName;
            }
            else
            {
                throw (new ApplicationException("Product Requested could not be found."));
            }
        }
    }
    #endregion

    # region Events
    /// <summary>
    /// Event fierd when submit button is triggered.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ProductAdmin AdminAccess = new ProductAdmin();

        DigitalAsset entity = new DigitalAsset();
        entity.DigitalAsset = txtDigitalAsset.Text.Trim();
        entity.ProductID = ItemId;
        entity.OrderLineItemID = null;


        bool Check = AdminAccess.AddDigitalAsset(entity);

        if (Check)
        {
            Response.Redirect("~/admin/secure/catalog/product/view.aspx?mode=digitalAsset&itemid=" + ItemId);
        }
        else
        {
            lblMsg.Text = "Could not add the product category. Please try again.";
            return;
        }
    }
        
    /// <summary>
    /// Cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/catalog/product/view.aspx?mode=digitalAsset&itemid=" + ItemId);
    }
    #endregion

    # region Bind Property
    /// <summary>
    /// Returns the product name
    /// </summary>
    /// <returns></returns>
    protected string SetProductName
    {
        get
        {
            ProductAdmin AdminAccess = new ProductAdmin();
            Product entity = AdminAccess.GetByProductId(ItemId);

            if (entity != null)
            {
                return entity.Name;
            }

            return "";
        }
    }
    #endregion
}
