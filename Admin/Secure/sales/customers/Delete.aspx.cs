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

public partial class Admin_Secure_sales_customers_Delete : System.Web.UI.Page
{
    # region Procted Member Variables
    protected string ListPageLink = "~/admin/secure/sales/customers/List.aspx";
    private int ItemId = 0;
    AccountAdmin accountAdmin = new AccountAdmin();
    #endregion

    # region Page Load Event
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.Params["itemid"]!=null)
        {
            ItemId = int.Parse(Request.Params["itemid"]);
        }

        if (!Page.IsPostBack)
        {
            Account _account = accountAdmin.GetByAccountID(ItemId);

            if (_account.UserID != null)
            {
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
    }

    #endregion

    #region Events
    /// <summary>
    /// Delete button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {        
        CaseAdmin caseAdmin = new CaseAdmin();
        NoteAdmin noteAdmin = new NoteAdmin();

        Account _account = accountAdmin.GetByAccountID(ItemId);
        Guid UserKey = new Guid();

        if(_account !=null)
        {
            if(_account.UserID.HasValue) 
                //Get UserId for this account
            UserKey = _account.UserID.Value;
        }       

        //Delete the note for this AccountId.
        noteAdmin.DeleteByAccountId(ItemId);

        //Delete the case for this AccountId.
        caseAdmin.DeleteByAccountId(ItemId);

        bool status = accountAdmin.Delete(_account);

        if (status) //Check 
        {
            //Get user by Unique GUID - User Id
            MembershipUser user = Membership.GetUser(UserKey);
            if (user != null)
            {
                //Delete online account
                Membership.DeleteUser(user.UserName);
            }
            //Redirect to list page
            Response.Redirect(ListPageLink);
        }
        else
        {
            lblMsg.Text = "Unable to delete this account. Please try again.";
        }
    }


    /// <summary>
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(ListPageLink);
    }
    #endregion
}
