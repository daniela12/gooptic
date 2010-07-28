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
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.DataAccess.Custom;
using System.Data.SqlClient;

public partial class Admin_Secure_design_Page_Delete : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    protected string PageName = string.Empty;
    #endregion

    #region Bind Data
    /// <summary>
    /// Bind data to the fields on the screen
    /// </summary>
    protected void BindData()
    {
        ContentPageAdmin pageAdmin = new ContentPageAdmin();
        ContentPage contentPage = pageAdmin.GetPageByID(ItemId);
        PageName = contentPage.Name;

        if (!contentPage.AllowDelete)
        {
            btnDelete.Enabled = false;
            lblMsg.Text = "This page is a reserved page and cannot be deleted.";
        }

    }
    #endregion

    #region Events
    /// <summary>
    /// Page Load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get ItemId from querystring        
        if (Request.Params["itemid"] != null)
        {
            ItemId = int.Parse(Request.Params["itemid"]);
        }
        else
        {
            ItemId = 0;
        }

        BindData();
    }

    /// <summary>
    /// Cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/design/pages/default.aspx");
    }

    /// <summary>
    /// Delete button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ContentPageAdmin pageAdmin = new ContentPageAdmin();
        ContentPage contentPage = pageAdmin.GetPageByID(ItemId);

        bool retval = pageAdmin.DeletePage(contentPage);

        if (!retval)
        {
            lblMsg.Text = "Error: Delete action could not be completed.";
        }
        else
        {
            Response.Redirect("~/admin/secure/design/pages/default.aspx");
        }
    }
    #endregion
}
