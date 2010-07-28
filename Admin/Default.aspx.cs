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
using ZNode.Libraries.ECommerce.UserAccount;

public partial class Admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //instantiated just to trigger licensing > Do not remove!
        ZNodeHelper hlp = new ZNodeHelper();

        //Set input focus on the page load
        UserName.Focus();

        string inURL = Request.Url.ToString();

        //check if SSL is required
        if (ZNodeConfigManager.SiteConfig.UseSSL)
        {
            if (!Request.IsSecureConnection)
            {                
                string outURL = inURL.ToLower().Replace("http://", "https://");

                Response.Redirect(outURL);
            }
        }

        //set ssl link
        sslLink.HRef = inURL.ToLower().Replace("http://", "https://");

        //Forgot password link
        string link = Request.Url.GetLeftPart(UriPartial.Authority) + Response.ApplyAppPathModifier("ForgotPassword.aspx");
        link = link.ToLower().Replace("admin/", "");
        forgotPasswordLink.HRef = link.ToLower().Replace("https://", "http://");

        if (Page.User.Identity.IsAuthenticated)
        {
            Response.Redirect("~/admin/secure/default.aspx");
        }
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        ZNodeUserAccount userAcct = new ZNodeUserAccount();
        bool loginSuccess = userAcct.Login(ZNodeConfigManager.SiteConfig.PortalID,UserName.Text,Password.Text);

        if (loginSuccess)
        {
            string retValue = "";

            bool status = ZNodeUserAccount.CheckLastPasswordChangeDate((Guid)userAcct.UserID, out retValue);

            if (!status)
            {
                ZNode.Libraries.DataAccess.Service.AccountService acctService = new ZNode.Libraries.DataAccess.Service.AccountService();
                ZNode.Libraries.DataAccess.Entities.Account account = acctService.GetByAccountID(userAcct.AccountID);

                // Set Error Code to session Object
                Session.Add("ErrorCode", retValue);

                // Get account and set to session
                Session.Add("AccountObject", account);

                Response.Redirect("~/ResetPassword.aspx");
            }

            //get account and set to session
            Session.Add(ZNodeSessionKeyType.UserAccount.ToString(), userAcct);

            FormsAuthentication.SetAuthCookie(UserName.Text, false);

            Response.Redirect("~/admin/secure/default.aspx");
        }
        else
        {
            FailureText.Text = "Login unsuccessful. Please try again.";
        }
    }
}
