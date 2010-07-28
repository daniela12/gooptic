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
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Entities;

public partial class Admin_Secure_settings_general_Key : System.Web.UI.Page
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
        Response.Redirect("~/admin/secure/Settings/default.aspx");
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        ZNodeEncryption enc = new ZNodeEncryption();

        //Rotate Key
        enc.RotateKey();

        ZNodeLogging.LogActivity(9000);

        lblMsg.Text = "Key has been successfully rotated.";
        btnCancel.Visible = false;
        btnGenerate.Visible = false;
    }
}
