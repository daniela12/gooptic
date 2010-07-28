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
using System.Text;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.ECommerce.UserAccount;

public partial class Admin_Secure_ChangePassword : System.Web.UI.Page
{
    # region Private Member Variables
    protected string redirectUrl = "~/admin/secure/settings/default.aspx";
    # endregion

    # region General Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Mode"] != null)
        {
            redirectUrl = "~/admin/secure/";
        }

        if (!Page.IsPostBack)
        {
            //Retrieve the information from the database
            MembershipUser user = Membership.GetUser(HttpContext.Current.User.Identity.Name);          
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ContinuePushButton_Click(object sender, EventArgs e)
    {
        Response.Redirect(redirectUrl);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CancelPushButton_Click(object sender, EventArgs e)
    {
        Response.Redirect(redirectUrl);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ChangePassword1_ChangePasswordError(object sender, EventArgs e)
    {
        (AdminChangePassword.ChangePasswordTemplateContainer.FindControl("PasswordFailureText") as Literal).Text = " Current Password incorrect";
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AdminChangePassword_ChangingPassword(object sender, LoginCancelEventArgs e)
    {
        e.Cancel = true;
        ChangePassword();
    }

    # endregion   

    # region Helper Methods
    /// <summary>
    /// Custom method to updates the password
    /// </summary>
    protected void ChangePassword()
    {
        MembershipUser user = Membership.GetUser(HttpContext.Current.User.Identity.Name);

        if (user != null)
        {
            //verify if the new password specified by the user is in the list of the last 4 passwords used.
            bool ret = ZNodeUserAccount.VerifyNewPassword((Guid)user.ProviderUserKey, AdminChangePassword.NewPassword);

            if (!ret)
            {
                (AdminChangePassword.ChangePasswordTemplateContainer.FindControl("PasswordFailureText") as Literal).Text = "Please select a different password. You should select a password that is different than the previous 4 passwords.";
                return;
            }

            //Updates the password for this user
            if (user.ChangePassword(AdminChangePassword.CurrentPassword, AdminChangePassword.NewPassword))
            {
                ZNode.Libraries.Framework.Business.ZNodeLogging.LogActivity(1106, HttpContext.Current.User.Identity.Name, Request.UserHostAddress.ToString(), null, "Current Password Incorrect", null);

                //Log password for further debugging
                ZNodeUserAccount.LogPassword((Guid)user.ProviderUserKey, AdminChangePassword.NewPassword);
  
                //Redirect to account page
                Response.Redirect("~/admin/secure/settings/default.aspx");
            }
            else
            {
                //Display Error message
                (AdminChangePassword.ChangePasswordTemplateContainer.FindControl("PasswordFailureText") as Literal).Text = "Current Password Incorrect";
                ZNode.Libraries.Framework.Business.ZNodeLogging.LogActivity(1107, HttpContext.Current.User.Identity.Name, Request.UserHostAddress.ToString(), null, "Current Password Incorrect", null);
            }            
        }
    }
    # endregion    
}
