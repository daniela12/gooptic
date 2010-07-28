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

public partial class Admin_Secure_Catalog_DeleteCatalog : System.Web.UI.Page
{

    # region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion

    # region General Events
    /// <summary>
    /// Confirm Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {        
        ProductAdmin _productAdmin = new ProductAdmin();

        bool Check = _productAdmin.EmptyCatalog(HttpContext.Current.User.Identity.Name);

        if (Check)
        {
            Response.Redirect("~/admin/secure/settings/default.aspx?mode=setup");
        }
        else
        {
            //Display Error Message
            lblErrorMessage.Text = " Delete action could not be completed. Please try again.";
        }
        
    }

    /// <summary>
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Redirect to Dashboard
        Response.Redirect("~/admin/secure/settings/default.aspx?mode=setup");
    }

    # endregion

}
