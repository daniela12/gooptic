using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Custom;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.Framework.Business;


public partial class Admin_Secure_sales_customers_list : System.Web.UI.Page
{
  
    # region Procted Member Variables
    protected static bool CheckSearch = false;
    protected string EditLink = "~/admin/secure/sales/customers/edit.aspx?itemid=";
    protected string ViewLink = "~/admin/secure/sales/customers/view.aspx?itemid=";
    protected string disablePageLink = "~/admin/secure/sales/customers/disable.aspx?itemid=";
    protected string unlockPageLink = "~/admin/secure/sales/customers/Unlock.aspx?itemid=";
    protected string DeletePageLink = "~/admin/secure/sales/customers/delete.aspx?itemid=";
    # endregion
    
    # region Page Load

    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.BindGrid();
            this.BindData();
            this.BindTrackingStatus();
            
            if (Session["ContactList"] != null)
            {
                Session.Remove("ContactList");
            }

            CheckSearch = false;
        }        
    }

    # endregion   

    # region Bind Grid Data

    /// <summary>
    /// Bind Grid
    /// </summary>
    protected void BindGrid()
    {
        AccountAdmin _AccountAdmin = new AccountAdmin();
        TList<Account> accountList = _AccountAdmin.GetAllCustomer();
        accountList.Sort("AccountID Desc");
        uxGrid.DataSource = accountList;
        uxGrid.DataBind();
    }

    /// <summary>
    /// Bind Profile drop down list
    /// </summary>
    private void BindData()
    {
        StoreSettingsAdmin settingsAdmin = new StoreSettingsAdmin();

        //get profiles
        lstProfile.DataSource = settingsAdmin.GetProfiles();
        lstProfile.DataTextField = "Name";
        lstProfile.DataValueField = "ProfileID";
        lstProfile.DataBind();

        //Insert New Item 
        ListItem item = new ListItem("All", "0");
        lstProfile.Items.Insert(0, item);

        //Set Default as "ALL"
        lstProfile.SelectedIndex = 0;

    }
    /// <summary>
    /// Binds Tracking Status drop-down list
    /// </summary>
    private void BindTrackingStatus()
    {
        Array names = Enum.GetNames(typeof(ZNodeApprovalStatus.ApprovalStatus));
        Array values = Enum.GetValues(typeof(ZNodeApprovalStatus.ApprovalStatus));

        // clear list items
        lstReferralStatus.Items.Clear();

        // add default value to the item
        lstReferralStatus.Items.Add(new ListItem("ALL",""));

        for (int i = 0; i < names.Length; i++)
        {
            ListItem listitem = new ListItem(ZNodeApprovalStatus.GetEnumValue(values.GetValue(i)), names.GetValue(i).ToString());
            lstReferralStatus.Items.Add(listitem);
        }
    }

   
    # endregion

    # region General Events

    /// <summary>
    /// Search Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet MyDataSet = this.BindSearchCustomer();
        CheckSearch = true;

        //Bind DataGrid with Filtered Output
        uxGrid.DataSource = MyDataSet;
        uxGrid.DataBind();

    }

    /// <summary>
    /// Clear Search Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        //Clear Search and Redirect to same page
        string link = "~/admin/secure/sales/customers/list.aspx";
        Response.Redirect(link);
    }

    protected void download_Click(object sender, EventArgs e)
    {
        DataDownloadAdmin _DataDownloadAdmin = new DataDownloadAdmin();
        CustomerHelper HelperAccess = new CustomerHelper();
        DataSet _dataset = new DataSet();

        //Check for Search enabled
        if(CheckSearch)
        {
            if(Session["ContactList"]!=null)
                _dataset = Session["ContactList"] as DataSet;
        }
        else
        {
            _dataset = HelperAccess.SearchCustomer(String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "0",String.Empty);
        }
                    
        string StrData = _DataDownloadAdmin.Export(_dataset, true);

        byte[] data = ASCIIEncoding.ASCII.GetBytes(StrData);

        //Release Resources
        _dataset.Dispose();

        Response.Clear();

        // Set as Excel as the primary format
        Response.AddHeader("Content-Type", "application/Excel");

        Response.AddHeader("Content-Disposition", "attachment;filename=Customer.csv");
        Response.ContentType = "application/vnd.xls";

        Response.BinaryWrite(data);

        Response.End();
    }

    /// <summary>
    /// Add Contact Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddContact_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/sales/customers/edit.aspx");       
    }

    # endregion

    # region Grid Events

    protected void uxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Page")
        {
            //Do nothing
        }
        else
        {
            // Get the Account id  stored in the CommandArgument
            string Id = e.CommandArgument.ToString();

            if (e.CommandName == "Edit")
            {
                EditLink = EditLink +  Id;
                Response.Redirect(EditLink);
            }
            if (e.CommandName == "Delete")
            {
                DeletePageLink = DeletePageLink + Id;
                Response.Redirect(DeletePageLink);
            }
            else if (e.CommandName == "View")
            {
                ViewLink = ViewLink + Id;
                Response.Redirect(ViewLink);
            }
            else if (e.CommandName == "Disable")
            {
                disablePageLink = disablePageLink + Id;
                Response.Redirect(disablePageLink);
            }
            else if (e.CommandName == "Unlock")
            {
                unlockPageLink = unlockPageLink + Id;
                Response.Redirect(unlockPageLink);
            }
            
        }
    }
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        
        if (CheckSearch)
        {
            if (Session["ContactList"] != null)
            {
                DataSet _dataset = Session["ContactList"] as DataSet;

                uxGrid.DataSource = _dataset;
                uxGrid.DataBind();

                //Release resources
                _dataset.Dispose();
            }
        }
        else
        {
            this.BindGrid();
        }

    }

    # endregion

    # region Helper Methods

    /// <summary>
    /// Return DataSet for a given Input
    /// </summary>
    /// <returns></returns>
    private DataSet BindSearchCustomer()
    {
        // Create Instance for Customer HElper class
        CustomerHelper HelperAccess = new CustomerHelper();
        DataSet Dataset = HelperAccess.SearchCustomer(txtFName.Text.Trim(), txtLName.Text.Trim(), txtComapnyNM.Text.Trim(), txtLoginName.Text.Trim(), txtExternalaccountno.Text.Trim(), txtContactID.Text.Trim(), txtStartDate.Text.Trim(), txtEndDate.Text.Trim(), txtPhoneNumber.Text.Trim(), txtEmailID.Text.Trim(), lstProfile.SelectedValue, lstReferralStatus.SelectedValue);

        Session["ContactList"] = Dataset;

        //Return DataSet
        return Dataset;
        
    }
    /// <summary>
    /// Concate firstname and lastname 
    /// </summary>
    /// <param name="FirstName"></param>
    /// <param name="LastName"></param>
    /// <returns></returns>
    protected string ConcatName(Object FirstName, Object LastName)
    {
        return (FirstName.ToString() + " " + LastName.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="UserId"></param>
    /// <returns></returns>
    protected bool HideDeleteButton(object UserId)
    {
        if (UserId != null)
        {
            if (UserId.ToString().Length > 0)
            {
                MembershipUser _user = Membership.GetUser(UserId);

                if (_user == null)
                    return true;

                if (_user.UserName.ToLower().Equals(HttpContext.Current.User.Identity.Name))
                {
                    return false;
                }

                string roleList = "";
                //Get roles for this User account
                string[] roles = Roles.GetRolesForUser(_user.UserName);

                foreach (string Role in roles)
                {
                    roleList += Role + ",";
                }
                string rolename = roleList;

                if (Roles.IsUserInRole("CUSTOMER SERVICE REP"))
                {
                    if (rolename == Convert.ToString("USER,") || rolename == Convert.ToString(""))
                    { return true; }
                    else
                    { return false; }
                }
                else if (roleList.Contains("ADMIN"))
                {
                if (_user.UserName.Equals(HttpContext.Current.User.Identity.Name))                   
                 return false;                    
                else if (Roles.IsUserInRole(HttpContext.Current.User.Identity.Name, "ADMIN"))
                 return true;
                else
                return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// Hides the disable or enable button
    /// </summary>
    /// <param name="UserId"></param>
    /// <returns></returns>
    protected bool HideLockUnlockButton(object UserId, bool isEnableButton)
    {
        bool status = false;

        if (UserId != null)
        {
            if (UserId.ToString().Length > 0)
            {
                bool isAdmin = Roles.IsUserInRole(HttpContext.Current.User.Identity.Name, "ADMIN");

                if (isAdmin
                    || Roles.IsUserInRole(HttpContext.Current.User.Identity.Name, "CUSTOMER SERVICE REP"))
                {
                    MembershipUser _user = Membership.GetUser(UserId);
                    
                    // Hide the button if the account is not found.
                    if (_user != null)
                    {
                        // Hide the button if I am editing my own account so I can't lock myself out.
                        if (!_user.UserName.ToLower().Equals(HttpContext.Current.User.Identity.Name))
                        {
                            if (isEnableButton)
                            {
                                // We are looking at the enable button, Enable it if the user is locked out.
                                status = (_user.IsLockedOut) || !_user.IsApproved;
                            }
                            else
                            {
                                // The Unlock button must always be the oposite of the Enable button.
                                status = ! ((_user.IsLockedOut) || !_user.IsApproved);
                            }
                        }
                        string roleList = "";
                        //Get roles for this User account
                        string[] roles = Roles.GetRolesForUser(_user.UserName);

                        foreach (string Role in roles)
                        {
                            roleList += Role + "<br>";
                        }
                        string rolename = roleList;

                        if (Roles.IsUserInRole("CUSTOMER SERVICE REP"))
                        {
                            if (rolename == Convert.ToString("USER<br>") || rolename == Convert.ToString(""))
                            { status = true; }
                            else
                            { status = false; }
                        }     
                        
                        // Don't let non-Admins disable Admin Accounts.
                        if (!isAdmin && Roles.IsUserInRole(_user.UserName, "ADMIN"))
                        {
                            status = false;
                        }
                    }
                }
            }
        }

        return status;
    }

    /// <summary>
    /// Hide the Edit button for admin accounts if the user is not an Admin
    /// </summary>
    /// <param name="UserId"></param>
    /// <returns></returns>
    protected bool HideEditButton(object UserId)
    {
        if (UserId != null)
        {
            if (UserId.ToString().Length > 0)
            {
                MembershipUser _user = Membership.GetUser(UserId);

                if (_user == null)
                    return true;

                string roleList = "";
                //Get roles for this User account
                string[] roles = Roles.GetRolesForUser(_user.UserName);

                foreach (string Role in roles)
                {
                    roleList += Role + "<br>";
                }
                string rolename = roleList;

                if (!Roles.IsUserInRole("ADMIN"))
                {
                    if (Roles.IsUserInRole("CUSTOMER SERVICE REP"))
                    {
                        if (rolename == Convert.ToString("USER<br>") || rolename == Convert.ToString(""))
                        { return true; }
                        else
                        { return false; }
                    }
                    else if (rolename == Convert.ToString("ADMIN<br>"))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }    
  

    /// <summary>
    /// Gets the Name of the Role for this ProfileID
    /// </summary>
    /// <param name="ProfileID"></param>
    /// <returns></returns>
    public string GetRoleName(object UserId)
    {       
        if(UserId != null)
        {
            string roleList = "";

            if (UserId.ToString().Length > 0)
            {
                MembershipUser _user = Membership.GetUser(UserId);

                if (_user == null)
                {
                    return "";
                }

                //Get roles for this User account
                string[] roles = Roles.GetRolesForUser(_user.UserName);

                foreach (string Role in roles)
                {
                    roleList += Role + "<br>";
                }
            }

            return roleList;
        }
        else
        {
            return "";
        }
    }

    /// <summary>
    /// Gets the Name of the Profile for this ProfileID
    /// </summary>
    /// <param name="ProfileID"></param>
    /// <returns></returns>
    public string GetProfileName(object ProfileID)
    {
        ProfileAdmin AdminAccess = new ProfileAdmin();
        if (ProfileID == null)
        {
            return "All Profile";
        }
        else
        {
            Profile profile = AdminAccess.GetByProfileID(int.Parse(ProfileID.ToString()));

            if (profile != null)
            {
                return profile.Name;
            }
        }
        return "";
    }
    # endregion    
}
