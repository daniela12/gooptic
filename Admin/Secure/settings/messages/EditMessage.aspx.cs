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
using System.Data.SqlClient;

public partial class Admin_Secure_settings_messages_EditMessage : System.Web.UI.Page
{
    #region Protected Variables
    protected string ItemId = "";
    #endregion

    # region page load
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get ItemId from querystring        
        if (Request.Params["itemid"] != null)
        {
            ItemId = Request.Params["itemid"];            
        }
        if (!Page.IsPostBack)
        {
            Bind();
        }        
    }
    #endregion

    #region bind
    public void Bind()
    {
        ctrlHtmlText.Html = XmlDataSource1.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='" + ItemId + "']").Attributes["MsgValue"].Value;
        String TitleMsg = XmlDataSource1.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='" + ItemId + "']").Attributes["MsgDescription"].Value;
        lbltitle.Text = "Edit Message - " + TitleMsg.Replace("-", ""); ;
    }
    #endregion

    #region General Events
    /// <summary>
    /// Submit button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        XmlDataSource1.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='" + ItemId + "']").Attributes["MsgValue"].Value = ctrlHtmlText.Html;

        try
        {
            XmlDataSource1.Save();

            //invalidate message config
            ZNodeConfigManager.MessageConfig = null;  
        }
        catch
        {            
            //display error message
            lblmsg.Text = "An error occurred while updating. Please try again.";
        }
        Response.Redirect("~/admin/secure/settings/messages/default.aspx");                     

    }

    /// <summary>
    /// Cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/settings/messages/default.aspx");
    }

    #endregion
}
