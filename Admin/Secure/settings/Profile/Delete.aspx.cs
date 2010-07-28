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
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.Admin;

public partial class Admin_Secure_settings_Profile_Delete : System.Web.UI.Page
{
    #region Protected Member Variables
    protected int ItemID;
    protected string ProfileName = string.Empty;
    protected string CancelLink = "~/admin/secure/settings/profile/default.aspx";
    #endregion

    #region Page Load
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get ItemId from querystring   
        if (Request.Params["Itemid"] != null)
        {
            ItemID = int.Parse(Request.Params["Itemid"].ToString());
            this.BindData();
        }
        else
        {
            ItemID = 0;
        }

    }
    #endregion

    # region Bind Data
    /// <summary>
    /// Bind the data for a Product id
    /// </summary>
    public void BindData()
    {
        ProfileAdmin profileAdmin = new ProfileAdmin();
        Profile profileEntity = profileAdmin.GetByProfileID(ItemID);

        if (profileEntity != null)
        {
            ProfileName = profileEntity.Name;
        }
    }
    # endregion

    # region Events
    /// <summary>
    /// Delete Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ProfileAdmin profileAdmin = new ProfileAdmin();
        Profile profileEntity = profileAdmin.GetByProfileID(ItemID);
        bool status;
        string message;
        if (profileEntity.IsDefault == true)
        {
           status = false;
           message = "The profile can not be deleted because it is set to default";
        }
        else
        {
           status = profileAdmin.Delete(profileEntity);
           message ="The profile can not be deleted until all associated items are removed. Please ensure that this profile does not associated with accounts or promotions. If it does, then delete these Items first.";
        }
        if(status)
        {
            Response.Redirect(CancelLink);
        }
        else
        {
            lblErrorMessage.Text = message;
        }
    }

    /// <summary>
    /// Cancel Button click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(CancelLink);
    }
    #endregion
}
