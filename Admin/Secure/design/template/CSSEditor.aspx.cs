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
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Xml;

public partial class Admin_Secure_design_template_CSSEditor : System.Web.UI.Page
{
    # region protected Variables
        string CancelLink = "~/admin/secure/contentmanager.aspx";
        string HomeLink = "~/admin/secure/contentmanager.aspx";
    # endregion
       
    #region Page Load
        /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.BindData();
        }

    }
    # endregion

    # region Bind Methods

    /// <summary>
    /// Bind Style Sheet Content
    /// </summary>
    private void BindData()
    {
        //Instance for ContentPageAdmin 
        TemplateAdmin TemplateAdmin = new TemplateAdmin();
        
        //Get Values
        StyleSheetContent.Text = TemplateAdmin.GetTemplateStyle();
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
        TemplateAdmin TemplatePageAdmin = new TemplateAdmin();
        bool Status = TemplatePageAdmin.UpdateTemplateStyleFile(StyleSheetContent.Text.Trim());

        if (Status)
        {           
            //redirect to next page
            Response.Redirect(HomeLink);
        }
        else
        {
            //display error message
            lblMsg.Text = "An error occurred while updating. Please try again.";
            return;
        }
        
    }
    /// <summary>
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //redirect to Content Manager page
        Response.Redirect(CancelLink);
    }
    # endregion
}
