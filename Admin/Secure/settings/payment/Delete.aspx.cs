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

public partial class Admin_Secure_settings_payment_Delete : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    #endregion

    #region Bind Data
    /// <summary>
    /// Bind data to the fields on the screen
    /// </summary>
    protected void BindData()
    {  
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
        Response.Redirect("~/admin/secure/settings/payment/");
    }

    /// <summary>
    /// Delete button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        StoreSettingsAdmin storeAdmin = new StoreSettingsAdmin();
        PaymentSetting _pmtSettings = storeAdmin.GetPaymentSettingByID(ItemId);

        //Disabled because this does not permit removing CC profiles which is annoying
        //Validate atleast one payment must exist for each profile
        //if (!storeAdmin.CheckPaymentSettingProfile(_pmtSettings.ProfileID))
        //{
        //    lblMsg.Text = "At least one Payment Option must exist for each Profile Name.";
        //    lblMsg.CssClass = "Error";
        //    return;
        //}

        bool retval = storeAdmin.DeletePaymentSetting(ItemId);

        if (!retval)
        {
            lblMsg.Text = "Delete action could not be completed.";
            lblMsg.CssClass = "Error";
        }
        else
        {
            Response.Redirect("~/admin/secure/settings/payment/");
        }
    }
    #endregion
}
