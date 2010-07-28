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


public partial class Admin_Secure_settings_msg_Add : System.Web.UI.Page
{

    #region Protected Variables
    protected int ItemId;
    protected string EditLink = "Editmessage.aspx?itemid=";
    #endregion

    #region Page Load
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Bind();
        }
    }
    #endregion

    # region Bind methods
    /// <summary>
    /// 
    /// </summary>
    private void Bind()
    {        
        DataSet ds = new DataSet();
        //Get the path for the XML file
        string path = Server.MapPath("../../../../Data/Default/Config/MessageConfig.xml");
        ds.ReadXml(path);
        DataView dv = new DataView(ds.Tables[0]);
        dv.Sort = "MsgDescription ASC";
        uxGrid.DataSource = dv;
        uxGrid.DataBind();
    }
    #endregion

    #region Grid Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Page")
        {
        }
        else
        {            
            string key = e.CommandArgument.ToString();
            if (e.CommandName == "Edit")
            {
                Response.Redirect(EditLink + key);
            }
        }
    }
    #endregion 
}
