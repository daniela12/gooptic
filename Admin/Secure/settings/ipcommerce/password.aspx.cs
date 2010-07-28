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
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Entities;

public partial class admin_secure_settings_ipcommerce_password : System.Web.UI.Page
{
    # region Page Load Event
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    #endregion

    # region General Events
    /// <summary>
    /// Submit button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ZNode.Libraries.Payment.GatewayIPCommerce ipc = new ZNode.Libraries.Payment.GatewayIPCommerce();
        bool ret = ipc.ChangePassword(UserID.Text, PasswordOld.Text, Password.Text);

        if (!ret)
        {
            lblErrorMsg.Text = "Unable to change IP Commerce Password. " + ipc.ResponseText  ;
        }
        else
        {
            Response.Redirect("~/admin/secure/settings/default.aspx?mode=ipcommerce");
        }
    }

    /// <summary>
    /// Cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/settings/default.aspx?mode=ipcommerce");
    }

    #endregion

}
