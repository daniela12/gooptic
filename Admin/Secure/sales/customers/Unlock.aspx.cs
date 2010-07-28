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

public partial class Admin_Secure_sales_customers_Unlock : System.Web.UI.Page
{
    # region Procted Member Variables
    protected string ListPageLink = "~/admin/secure/sales/customers/List.aspx";
    private int ItemId = 0;
    AccountAdmin accountAdmin = new AccountAdmin();
    #endregion

    #region Events
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["itemid"] != null)
        {
            ItemId = int.Parse(Request.Params["itemid"]);
        }

        if (!Page.IsPostBack)
        {
            Account _account = accountAdmin.GetByAccountID(ItemId);

            MembershipUser _user = Membership.GetUser(_account.UserID.Value);

            string roleList = "";
            //Get roles for this User account
            string[] roles = Roles.GetRolesForUser(_user.UserName);

            foreach (string Role in roles)
            {
                roleList += Role + "<br>";
            }
            string rolename = roleList;

            //Hide the Delete button if a NonAdmin user has entered this page
            if (!Roles.IsUserInRole(HttpContext.Current.User.Identity.Name, "ADMIN"))
            {
                if (Roles.IsUserInRole(_user.UserName, "ADMIN"))
                {
                    btnUnlock.Visible = false;
                }
                else if (Roles.IsUserInRole(HttpContext.Current.User.Identity.Name, "CUSTOMER SERVICE REP"))
                {
                    if (rolename == Convert.ToString("USER<br>") || rolename == Convert.ToString(""))
                    {
                        btnUnlock.Visible = true;
                    }
                    else
                    {
                        btnUnlock.Visible = false;
                    }
                }
            }
        }  
    }

    /// <summary>
    /// Unlock User button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUnlockUSer_Click(object sender, EventArgs e)
    {       
        Account _account = accountAdmin.GetByAccountID(ItemId);        

        bool status = false;
        lblMsg.Text = "User account not found. Please go back to the previous page and reselect the account.";

        if (_account != null)
        {
            if (_account.UserID.HasValue)
            {
                //Get user by Unique GUID - User Id
                MembershipUser user = Membership.GetUser(_account.UserID.Value);
                if (user != null)
                {
                    // If the user is locked out then unlock them so they will be able to log in.
                    status = user.UnlockUser();

                    // Update user
                    user.IsApproved = true;
                    Membership.UpdateUser(user);
                    ZNode.Libraries.Framework.Business.ZNodeLogging.LogActivity(1104, user.UserName);
                }
            }
        }
       
        if (status)
        {
            Response.Redirect(ListPageLink);
        }
    }

    /// <summary>
    /// Cancel Button Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
		Response.Redirect(ListPageLink);
    }
    #endregion
}
