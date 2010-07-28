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

public partial class Admin_Secure_settings_StoreLocator_Delete : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemID;
    protected string RedirectLink = "~/admin/secure/settings/StoreLocator/List.aspx";
    protected string Name = string.Empty;
    #endregion

    #region Page Load
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get ItemID from querystring        
        if (Request.Params["ItemID"] != null)
        {
            ItemID = int.Parse(Request.Params["ItemID"]);
        }
        else
        {
            ItemID = 0;
        }

        if (!Page.IsPostBack)
        {
            this.BindData();
        }
    }
    #endregion

    # region Bind Data

    private void BindData()
    {
        ZNode.Libraries.Admin.StoreLocatorAdmin StoreBind = new StoreLocatorAdmin();

        ZNode.Libraries.DataAccess.Entities.Store StoreList = StoreBind.GetByStoreID(ItemID);
        
        if (StoreList != null)
        {
            Name = StoreList.Name;
        }       
    }

    # endregion

    # region General Events

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ZNode.Libraries.Admin.StoreLocatorAdmin StoreAdmin = new StoreLocatorAdmin();

        bool check = false;

        check = StoreAdmin.DeleteStore(ItemID);

        if (check)
        {
            Response.Redirect(RedirectLink);
        }
        else
        {
            lblErrorMsg.Text = "Delete action could not be completed.";
            lblErrorMsg.Visible = true;
        }       
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(RedirectLink);
    }
    #endregion

}
