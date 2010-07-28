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
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.DataAccess.Entities;

public partial class Admin_Secure_design_template_SiteTheme : System.Web.UI.Page
{   

    # region page load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
           this.BindList();           
        }      
    }
    # endregion

    # region Bind Data
    /// <summary>
    /// Binds the themes  to the DropDownList
    /// </summary>
    private void BindList()
    {          
        System.IO.DirectoryInfo path = new System.IO.DirectoryInfo(Server.MapPath(ZNodeConfigManager.EnvironmentConfig.ApplicationPath + "/Themes"));
        themeslist.DataSource = path.GetDirectories();
        themeslist.DataBind();
        StoreSettingsAdmin storeAdmin = new StoreSettingsAdmin();
        Portal portal = storeAdmin.GetByPortalId(ZNodeConfigManager.SiteConfig.PortalID);
        if (themeslist.SelectedIndex != -1)
        {
            themeslist.SelectedValue = portal.Theme;
        }
        ListItem li = new ListItem("Select a Site Theme", "0");
        themeslist.Items.Insert(0, li);
        lblError.Visible = false;
    }
     # endregion

    # region General Events
    
    /// <summary>
    /// Submit Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if(themeslist.SelectedIndex > 0)
        {
            StoreSettingsAdmin storeAdmin = new StoreSettingsAdmin();
            Portal portal = storeAdmin.GetByPortalId(ZNodeConfigManager.SiteConfig.PortalID);
            portal.Theme = themeslist.SelectedValue;
            ZNodeConfigManager.RefreshConfiguration();
            storeAdmin.Update(portal);
            lblmessage.Text = "The theme of your site has been successfully changed.";
            btnSubmit.Visible = false;
            btncancel.Visible = false;
            lblError.Visible = false;
            btnback.Visible = true;
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = "* Select a theme";
        }     
    }

    protected void btnback_click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/default.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/default.aspx");
    }

    # endregion

}
