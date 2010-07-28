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

public partial class Admin_Secure_settings_Profile_Default : System.Web.UI.Page
{
    #region Protected Member Variables
    protected string AddLink = "~/admin/secure/settings/profile/add.aspx";
    protected string DeleteLink = "~/admin/secure/settings/profile/delete.aspx?ItemID=";
    #endregion  

    # region Events
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddLink);
    }
    #endregion

    #region Bind Methods
    /// <summary>
    /// Bind Grid
    /// </summary>
    protected void Bind()
    {
        ProfileAdmin profileAdmin = new ProfileAdmin();
        uxGrid.DataSource = profileAdmin.GetAll();
        uxGrid.DataBind();
    }
    #endregion

    # region Grid Events
    /// <summary>
    /// Grid page Index Change Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        Bind();
    }

    /// <summary>
    /// Grid Row Command Event
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
            int index = Convert.ToInt32(e.CommandArgument);

            // Get the values from the appropriate
            // cell in the GridView control.
            GridViewRow selectedRow = uxGrid.Rows[index];

            TableCell Idcell = selectedRow.Cells[0];
            string Id = Idcell.Text;

            if (e.CommandName == "Edit")
            {
                AddLink = AddLink + "?ItemID=" + Id;
                Response.Redirect(AddLink);
            }
            else if (e.CommandName == "Delete")
            {
                Response.Redirect(DeleteLink + Id);
            }

        }
    }
    #endregion
}
