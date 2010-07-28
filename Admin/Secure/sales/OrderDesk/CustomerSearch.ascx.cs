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
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.Framework.Business;

public partial class Themes_Default_OrderDesk_CustomerSearch : System.Web.UI.UserControl
{
    # region Protected Member Variables
    private ZNodeAddress _billingAddress = new ZNodeAddress();
    private ZNodeAddress _shippingAddress = new ZNodeAddress();
    private string _profileName = string.Empty;
    private string _loginName = string.Empty;
    # endregion
    
    #region Public Variables
    //Public Event Handler
    public event System.EventHandler SelectedIndexChanged;
    //Public Event Handler
    public event System.EventHandler SearchButtonClick;
    #endregion

    # region Public Properties

    /// <summary>
    /// Sets or Retrieves Account Object from/to page viewstate
    /// </summary>
    public Account UserAccount
    {
        get
        {
            if (ViewState["AccountObject"] != null)
            {
                // the account info with 'AccountObject' is retrieved from the viewstate and
                // It is converted to a Entity Account object
                return (Account)ViewState["AccountObject"];
            }

            return new Account();
        }
        set
        {
            // Customer account object is placed in the viewstate and assigned a key, AccountObject.            
            ViewState["AccountObject"] = value;
        }    
    }
    # endregion

    # region Page Load Event
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //Gets the browser name
        if (Request.Browser.Browser.Equals("IE"))
        {
            UpdateProgressIE.Visible = true;
        }
        else if (Request.Browser.Browser.Equals("Firefox"))
        {
            UpdateProgressMozilla.Visible = true;
        }
        else
        {
            UpdateProgressMozilla.Visible = true;
        }
    }
    # endregion

    # region General Events
    /// <summary>
    /// Event is raised when the "Search" Button is clicked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ui_Search_Click(object sender, EventArgs e)
    {        
        Bind();
        updatePnlFoundUsers.Update();
    }


    /// <summary>
    /// Occurs when one of the pager buttons is clicked,
    /// but before the "FoundUsers" GridView control handles the paging operation.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ui_FoundUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Retrieve Account Id
        Label userLogin = ui_FoundUsers.Rows[ui_FoundUsers.SelectedIndex].FindControl("ui_CustomerIdSelect") as Label;


        AccountService accountService = new AccountService();
        Account _account = accountService.GetByAccountID(int.Parse(userLogin.Text));


        if (_account != null)
        {
            UserAccount = _account;//Set Page session object
            //
            ZNode.Libraries.Admin.ProfileAdmin profileAdmin = new ZNode.Libraries.Admin.ProfileAdmin();

            HttpContext.Current.Session["ProfileCache"] = profileAdmin.GetByProfileID(_account.ProfileID.Value);
        }

        if (SelectedIndexChanged != null)
        {
            //Triggers parent control event
            this.SelectedIndexChanged(sender, e);
        }
    }
    
    /// <summary>
    /// Occurs when one of the pager buttons is clicked, 
    /// but before the GridView control handles the paging operation.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ui_FoundUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ui_FoundUsers.PageIndex = e.NewPageIndex;
        Bind();
    }

    /// <summary>
    /// Event is raised when the "Close" Button is clicked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (SearchButtonClick != null)
        {
            this.SearchButtonClick(sender, e);
        }
    }
    # endregion

    # region Bind Methods
    /// <summary>
    /// Bind grid
    /// </summary>
    public void Bind()
    {
        AccountService accountService = new AccountService();
        AccountQuery filters = new AccountQuery();        

        //check Login id
        if (UserID.Text.Trim().Length > 0)
        {
            MembershipUser user = Membership.GetUser(UserID.Text.Trim());

            if (user != null)
            {
                filters.AppendEquals(AccountColumn.UserID, user.ProviderUserKey.ToString());
            }
            else
            {
                filters.AppendEquals(AccountColumn.UserID, UserID.Text.Trim());
            }
        }

        //check OrderID
        if (OrderID.Text.Trim().Length > 0)
        {
            int orderID = int.Parse(OrderID.Text.Trim());

            OrderService _orderService = new OrderService();
            Order entity = _orderService.GetByOrderID(orderID);

            if (entity != null)
            {   
                filters.AppendEquals(AccountColumn.AccountID, entity.AccountID.Value.ToString());
            }
            else
            {
                filters.AppendEquals(AccountColumn.AccountID, "0");
            }
        }

        //Filter query
        filters.Append(AccountColumn.BillingFirstName,"%" + FirstName.Text.Trim() + "%");
        filters.Append(AccountColumn.BillingLastName, "%" + LastName.Text.Trim() + "%");
        filters.Append(AccountColumn.BillingPostalCode, "%" + ZipCode.Text.Trim() + "%");
        filters.Append(AccountColumn.BillingCompanyName, "%" + CompanyName.Text.Trim() + "%");

        //get account list
        TList<Account> accountList = accountService.Find(filters.GetParameters());

        if (accountList != null)
        {
            if (accountList.Count == 0)
            {
                lblSearhError.Text = "No users found.";
            }
        }
        else
        {
            lblSearhError.Text = "No users found.";
        }

        //Bind account list grid
        ui_FoundUsers.DataSource = accountList;
        ui_FoundUsers.DataBind();        
    }

    /// <summary>
    /// 
    /// </summary>
    public void ClearUI()
    {
        ui_FoundUsers.DataBind();

        FirstName.Text = "";
        LastName.Text = "";
        ZipCode.Text = "";
        CompanyName.Text = "";
        OrderID.Text = "";
        UserID.Text = "";
        UpdatePnlSearch.Update();
        updatePnlFoundUsers.Update();
    }

    # endregion    

    # region Helper Methods
    /// <summary>
    /// Returns login name for a Provider access key(UserID)
    /// </summary>
    /// <param name="UserID"></param>
    /// <returns></returns>
    protected string GetLoginName(object UserID)
    {
        if (UserID != null)
        {
            MembershipUser user = Membership.GetUser(UserID);

            if (user != null)
            {
                return user.UserName;
            }
        }

        return "";
    }
    # endregion    
}
