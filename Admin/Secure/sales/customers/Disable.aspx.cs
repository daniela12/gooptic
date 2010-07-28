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

public partial class Admin_Secure_sales_customers_Disable : System.Web.UI.Page
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
                    btnDelete.Visible = false;
                }
                else if (Roles.IsUserInRole(HttpContext.Current.User.Identity.Name, "CUSTOMER SERVICE REP"))
                {
                    if (rolename == Convert.ToString("USER<br>") || rolename == Convert.ToString(""))
                    {
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        btnDelete.Visible = false;
                    }
                }
            }
        }     
    }

    /// <summary>
    /// Disable button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDisable_Click(object sender, EventArgs e)
    {       
        Account _account = accountAdmin.GetByAccountID(ItemId);        

        if (_account != null)
        {
            if (_account.UserID.HasValue)
            {   
                //Get user by Unique GUID using UserId
                MembershipUser user = Membership.GetUser(_account.UserID.Value);
                if (user != null)
                {                    
                    //disable online account
                    user.IsApproved = false;
                    Membership.UpdateUser(user);
                    ZNode.Libraries.Framework.Business.ZNodeLogging.LogActivity(1005, user.UserName);
                }
            }
        }
        else
        {
            //throw exception here
        }

        Response.Redirect(ListPageLink);
    }

    /// <summary>
    /// Cancel Button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(ListPageLink);
    }
    #endregion
}
