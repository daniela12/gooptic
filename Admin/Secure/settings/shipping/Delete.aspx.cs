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
using ZNode.Libraries.DataAccess.Custom;
using System.Data.SqlClient;

public partial class Admin_Secure_settings_ship_Delete : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    protected string ShippingOptionName = string.Empty;
    #endregion

    #region Bind Data
    /// <summary>
    /// Bind data to the fields on the screen
    /// </summary>
    protected void BindData()
    {
        ShippingAdmin shipAdmin = new ShippingAdmin();
        ShippingOptionName = shipAdmin.GetShippingOptionById(ItemId).Description;
    }
    #endregion

    #region Events
    /// <summary>
    /// Page Load event
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

        BindData();
    }

    /// <summary>
    /// Cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/settings/shipping/default.aspx");
    }

    /// <summary>
    /// Delete button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ShippingAdmin shipAdmin = new ShippingAdmin();
        Shipping shipping = new Shipping();
        shipping.ShippingID = ItemId;

        bool retval = shipAdmin.DeleteShippingOption(shipping);

        if (!retval)
        {
            lblMsg.Text = "Error: Delete action could not be completed. You must delete all shipping rules on this option first. ";
            lblMsg.Text = lblMsg.Text  + "You should also ensure that this shipping option is not currently referenced by an order or a product. ";
        }
        else
        {
            Response.Redirect("~/admin/secure/settings/shipping/default.aspx");
        }
    }
    #endregion
}
