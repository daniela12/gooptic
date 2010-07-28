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

public partial class Admin_Secure_settings_taxes_Default : System.Web.UI.Page
{
    #region Protected Variables
    protected string AddRuleLink = "~/admin/secure/settings/taxes/AddRule.aspx";
    protected string ViewLink = "~/admin/secure/settings/taxes/View.aspx";
    protected string EditLink = "~/admin/secure/settings/taxes/Add.aspx";
    #endregion

    #region Page Load
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            BindGridData();
    }

    #endregion

    #region Bind Methods
    /// <summary>
    /// Bind data to the grid
    /// </summary>
    private void BindGridData()
    {
        TaxRuleAdmin taxRuleAdmin = new TaxRuleAdmin();
        uxGrid.DataSource = taxRuleAdmin.GetAllTaxClass();        
        uxGrid.DataBind();
    }
    
    #endregion

    #region Grid Events
    /// <summary>
    /// Event triggered when the grid page is changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        BindGridData();

    }
    /// <summary>
    /// Event triggered when a command button is clicked on the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "page")
        {
        }
        else
        {
            // Convert the row index stored in the CommandArgument
            // property to an Integer.
            int index = Convert.ToInt32(e.CommandArgument);

            // Get the values from the appropriate
            // cell in the GridView control.
            GridViewRow selectedRow = uxGrid.Rows[index];

            TableCell Idcell = selectedRow.Cells[0];
            string Id = Idcell.Text;


            if (e.CommandName == "View")
            {
                ViewLink = ViewLink + "?taxid=" + Id;
                Response.Redirect(ViewLink);
            }
            else if (e.CommandName == "Edit")
            {
                EditLink = EditLink + "?taxid=" + Id;
                Response.Redirect(EditLink);
            }
            else if (e.CommandName == "Delete")
            {
                TaxRuleAdmin taxRuleAdmin = new TaxRuleAdmin();
                bool status = taxRuleAdmin.DeleteTaxClass(int.Parse(Id));
                if (!status)
                {
                    lblErrorMsg.Text = "Delete action could not be completed because the Tax class is in use.";
                }
            }
        }
    }
    /// <summary>
    /// Event triggered when an item is deleted from the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        BindGridData();
    }
    #endregion

    #region Other Events
    /// <summary>
    /// Add Tax Class button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddTaxClass_Click(object sender, EventArgs e)
    {
        Response.Redirect(EditLink);
    }
    /// <summary>
    /// Add Tax Rules button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddTaxRules_Click(object sender, EventArgs e)
    {
        string taxId = ((Button)sender).CommandArgument;
        Response.Redirect(AddRuleLink + "?taxid=" + taxId);
    }
    #endregion

}
