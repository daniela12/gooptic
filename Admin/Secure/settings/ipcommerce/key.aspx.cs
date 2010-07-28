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

public partial class admin_Secure_settings_ipcommerce_key : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        ZNode.Libraries.Payment.GatewayIPCommerce ipc = new ZNode.Libraries.Payment.GatewayIPCommerce();
        ipc.RotateKey();

        lblMsg.Text = "Key has been successfully rotated.";
        btnCancel.Visible = false;
        btnGenerate.Visible = false;
    }
}
