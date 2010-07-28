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

public partial class admin_themes_standard_content : ZNodeAdminTemplate 
{

    /// <summary>
    /// Raised after the user clicks the logout link and the logout process is complete.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        //Clearing a per-user cache of objects here.
        Session.Remove(ZNode.Libraries.Framework.Business.ZNodeSessionKeyType.UserAccount.ToString());
        Session["ProfileCache"] = null; // Reset profile cache
    }

    /// <summary>
    /// Raised after the user clicks the logout link and the logout process is complete.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AdminUserLoginStatus_LoggedOut(object sender, EventArgs e)
    {
         Response.Redirect("~/admin/default.aspx");
    }
}
