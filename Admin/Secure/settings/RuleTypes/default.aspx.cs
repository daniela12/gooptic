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

public partial class Admin_Secure_settings_RuleTypes_Default : System.Web.UI.Page
{
    # region Private Member Variables
    RuleTypeAdmin _ruleTypeAdmin = new RuleTypeAdmin();
    protected string AddPageLink = "~/admin/secure/settings/ruleTypes/add.aspx";
    #endregion

    # region Page Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindShippingTypes();
        }
    }

    #endregion

    # region Bind Methods
    /// <summary>
    /// 
    /// </summary>
    private void BindShippingTypes()
    {
        uxGrid.DataSource = _ruleTypeAdmin.GetShippingTypes();
        uxGrid.DataBind();
    }
    #endregion

    # region Grid Events


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        BindShippingTypes();
    }

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
            int index = Convert.ToInt32(e.CommandArgument);

            // Get the values from the appropriate
            // cell in the GridView control.
            GridViewRow selectedRow = uxGrid.Rows[index];

            TableCell Idcell = selectedRow.Cells[0];
            string Id = Idcell.Text;

            if (e.CommandName == "Edit")
            {
                Response.Redirect(AddPageLink + "?ItemID=" + Id + "&ruletype=1");
            }
            else if (e.CommandName == "Delete")
            {
                bool status = _ruleTypeAdmin.DeleteShippingType(int.Parse(Id));

                if (!status)
                {
                    lblErrorMsg.Text = "The shipping type '" + selectedRow.Cells[2].Text + "' can not be deleted until all associated items are removed. Please ensure that this shipping type does not contain shipping options. If it does, then delete shipping options first.";
                }
                else
                {
                    BindShippingTypes();
                }
            }

        }
    }
    protected void uxGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    #endregion

    # region Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddRuleType_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddPageLink + "?ruletype=1");
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/AdvanceToolsManager.aspx");
    }
    #endregion
}
