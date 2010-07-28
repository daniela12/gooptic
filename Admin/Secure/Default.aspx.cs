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
using System.Diagnostics;
using ZNode.Libraries.DataAccess.Custom;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.ECommerce.UserAccount;
using System.Xml;

public partial class Admin_Secure_Default : System.Web.UI.Page
{
    protected DashboardAdmin dashAdmin = new DashboardAdmin();
    protected string daysToExpire = "0";
    protected string storefrontUrl = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        BindData();        
    }

    /// <summary>
    /// Bind data to the dashboard
    /// </summary>
    protected void BindData()
    {
        PasswordLink.HRef = "~/admin/secure/ChangePassword.aspx";
        PasswordLink1.HRef = PasswordLink.HRef;

        ZNodeUserAccount userAcct = ZNodeUserAccount.CurrentAccount();
        
        MembershipUser _user = Membership.GetUser(userAcct.UserID);
        
        string roleList = "";
        
        //Get roles for this User account
        string[] roles = Roles.GetRolesForUser(_user.UserName);
        
        foreach (string Role in roles)
        {
            roleList += Role + ",";
        }
        
        if (roleList.Contains("ADMIN") || roleList.Contains("EXECUTIVE"))
        {
            Metrics.Visible = true;
            MetricsMessage.Visible = false;
            pnlAlerts.Visible = true;
        }
        else
        {
            MetricsMessage.Visible = true;
            Metrics.Visible = false;
            lblAlertsMsg.Visible = true;

            PasswordLink.HRef = "~/admin/Secure/ChangePassword.aspx?mode=1";
        }
        
        try
        {
            dashAdmin.GetDashboardItems();

            if (dashAdmin.PopularSearchKeywords.Rows.Count == 0)
            {
                lblMessage.Visible = true;
            }
            else
            {
                // Bind Reapter control
                rptPopularSearchKeywords.DataSource = dashAdmin.PopularSearchKeywords;
                rptPopularSearchKeywords.DataBind();
            }            
        }
        catch  
        { 
            //non critical - ignore        
        }

        // Set the IFrame to https if we are on a secure connection.
        string prefix = "http://"; 
        if (Request.IsSecureConnection)
        {
            prefix = "https://";
        }
               
        //get storefront path
        storefrontUrl = prefix + HttpContext.Current.Request.ServerVariables.Get("HTTP_HOST") + HttpContext.Current.Request.ApplicationPath; ;

    }


    # region Helper Methods

    /// <summary>
    /// Concate Firstname, Lastname and UserRole
    /// </summary>   
    protected string ConcatName()
    {
        ZNodeUserAccount userAcct = ZNodeUserAccount.CurrentAccount();        
        MembershipUser _user = Membership.GetUser(userAcct.UserID);
        string[] roles = Roles.GetRolesForUser(_user.UserName);
        string roleList = "";
       
        foreach (string Role in roles)
        {
            roleList += " " + Role + ",";
        }
        roleList = " " + roleList.TrimEnd(',');
        
        string rolename = roleList;

        MembershipUser user = Membership.GetUser(HttpContext.Current.User.Identity.Name);        
        TimeSpan span = System.DateTime.Now.Subtract(user.LastPasswordChangedDate);
        daysToExpire = (60 - span.Days).ToString();

        return "Welcome " + "<span> <b>" + userAcct.FirstName + " " + userAcct.LastName + "</b>.</span>" + " You are logged in as " + "<span> <b>" + rolename + "</b>.</span>";      
    }
    
    /// <summary>
    /// Get the product Version
    /// </summary>
    /// <returns></returns>
    public string GetProductVersion()
    {
        try
        {
            string path = ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.ApplicationPath + "/Bin/ZNode.Libraries.Framework.Business.dll";

            //Create Instance for FileVersionInfo object
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(Server.MapPath(path));
            if (info != null)
            {
                return "v" + info.FileVersion;
            }
            else
            {
                return String.Empty;
            }
        }
        catch (Exception)
        {
            return String.Empty;
        }
    }
    # endregion
}
