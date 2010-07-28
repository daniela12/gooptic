using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Custom;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.Framework.Business;

public partial class Admin_Secure_sales_Affiliates_View : System.Web.UI.Page
{

    # region Procted Member Variables
    protected static bool CheckSearch = false;
    protected string EditLink = "~/admin/secure/sales/Affiliates/Edit.aspx?itemid=";
    #endregion

    # region Page Load

    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.BindGrid();          
            CheckSearch = false;
        }
    }
    #endregion

    # region Bind Grid Data

    /// <summary>
    /// Bind Grid
    /// </summary>
    protected void BindGrid()
    {

        CustomerHelper HelperAccess = new CustomerHelper();
        uxGrid.DataSource = HelperAccess.GetAffiliate();
        uxGrid.DataBind();
    }
    #endregion

    # region General Events
    /// <summary>
    /// Search Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet MyDataSet = this.BindSearchAffiliate();
        CheckSearch = true;

        //Bind DataGrid with Filtered Output
        uxGrid.DataSource = MyDataSet;
        uxGrid.DataBind();

    }

    /// <summary>
    /// Clear Search Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        //Clear Search and Redirect to same page
        string link = "~/admin/secure/sales/Affiliates/View.aspx";
        Response.Redirect(link);
    }
    #endregion

    # region Grid Events

    protected void uxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Page")
        {
            //Do nothing
        }
        else
        {
            // Get the Account id  stored in the CommandArgument
            string Id = e.CommandArgument.ToString();

            if (e.CommandName == "Edit")
            {
                EditLink = EditLink + Id;
                Response.Redirect(EditLink);
            }
        }
    }

    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        if ((txtAccountID.Text == "") && (txtComapanyName.Text == "") && (txtFirstName.Text == "") && (txtLastName.Text == "") && (ListAffilaiteStatus.SelectedValue == Convert.ToString(0)))
        {
            BindGrid();
        }
        else
        {
            BindSearchAffiliate();
        }
    }
    #endregion

    # region Helper Methods

    /// <summary>
    /// Return DataSet for a given Input
    /// </summary>
    /// <returns></returns>
    private DataSet BindSearchAffiliate()
    {        
        CustomerHelper HelperAccess = new CustomerHelper();

        string Status = "";
         if (ListAffilaiteStatus.SelectedValue == "I")
        {
           Status = "I";
        }
        else if (ListAffilaiteStatus.SelectedValue == "A")
        {
             Status = "A";
        }
        else if (ListAffilaiteStatus.SelectedValue == "N")
        {
             Status = "N";
        }
        else
        {
            Status = "";
        }
        DataSet Dataset = HelperAccess.SearchAffiliate(txtFirstName.Text, txtLastName.Text, txtComapanyName.Text, txtAccountID.Text, Status);
        return Dataset;
    }
    #endregion
}
