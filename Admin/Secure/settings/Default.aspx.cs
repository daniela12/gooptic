using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Xml;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.DataAccess.Custom;
using SCommImaging.Imaging;
using System.Drawing.Imaging;
using System.Drawing;

public partial class Admin_Secure_settings_Default : System.Web.UI.Page
{
    # region Protected Member variables
    protected string _mode = "";
    #endregion

    # region Page Load Event
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/maintmanager.aspx");

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["mode"] != null)
            {
                _mode = Request.QueryString["mode"];
            }            

            PreSelectTab();

            Bind();            
        }
    }
    #endregion

    # region Bind Methods
    /// <summary>
    /// 
    /// </summary>
    protected void Bind()
    {
        MembershipUser user = Membership.GetUser(HttpContext.Current.User.Identity.Name);
        lblLogInDate.Text = user.LastLoginDate.ToString();
        lblLastPwdUpdated.Text = user.LastPasswordChangedDate.ToString();

        TimeSpan span = System.DateTime.Now.Subtract(user.LastPasswordChangedDate);
        lbldaysCount.Text = (60 - span.Days).ToString();
    }    
    #endregion     

    # region Helper methods
    /// <summary>
    /// 
    /// </summary>
    private void PreSelectTab()
    {
        if (_mode == "quickbooks")
        {
            tabSettings.ActiveTabIndex = 2;
        }
        else if (_mode == "fedex")
        {
            tabSettings.ActiveTabIndex = 1;
        }
        //else if (_mode == "ipcommerce")
        //{
        //    tabSettings.ActiveTabIndex = 3;
        //}
        else if (_mode == "setup")
        {
            tabSettings.ActiveTabIndex = 4;
        }
        else if (_mode == "custom")
        {
            tabSettings.ActiveTabIndex = 5;
        }
    }
    #endregion

}
