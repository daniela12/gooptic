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
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.ECommerce.SEO;

public partial class Admin_Secure_SEO_UrlRedirect_Default : System.Web.UI.Page
{

    # region Protected Member Variables
    protected string EditPageLink = "~/Admin/Secure/Seo/UrlRedirect/edit.aspx?ItemId=";
    #endregion

    # region Events
    /// <summary>
    /// Page Load Event
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddUrlRedirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("edit.aspx");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bind();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        txtNewUrl.Text = "";
        txtOldUrl.Text = "";
        ddlURLStatus.SelectedValue = "0";

        Bind();
    }
    #endregion

    # region Bind Methods
    /// <summary>
    /// Bind grid
    /// </summary>
    protected void Bind()
    {
        UrlRedirectAdmin adminAccess = new UrlRedirectAdmin();
        
        uxGrid.DataSource = adminAccess.Search(txtOldUrl.Text.Trim(), txtNewUrl.Text.Trim(),ddlURLStatus.SelectedValue);
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
        Bind();
    }

    /// <summary>
    /// Event triggered when a command button is clicked on the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        // Convert the row index stored in the CommandArgument
        // property to an Integer.
        int index = Convert.ToInt32(e.CommandArgument);

        // Get the values from the appropriate
        // cell in the GridView control.
        GridViewRow selectedRow = uxGrid.Rows[index];

        TableCell Idcell = selectedRow.Cells[0];
        string Id = Idcell.Text;

        if (e.CommandName == "Edit")
        {
            Response.Redirect(EditPageLink + Id);
        }
        else if (e.CommandName == "Delete")
        {
            UrlRedirectAdmin urlRedirectAdmin = new UrlRedirectAdmin();
            UrlRedirect urlRedirect = urlRedirectAdmin.GetById(int.Parse(Id));

            bool status = urlRedirectAdmin.Delete(int.Parse(Id));

            if (status)
            {
                if (urlRedirect != null)
                {
                    ZNodeSEOUrl seoUrl = new ZNodeSEOUrl();
                    seoUrl.RemoveRedirectURL(urlRedirect.OldUrl);
                }

                Bind();                
            }
        }
    }

    /// <summary>
    /// Add Client side event to the Delete Button in the Grid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Retrieve the Button control from the Seventh column.
            Button DeleteButton = (Button)e.Row.Cells[5].FindControl("btnDelete");

            //Set the Button's CommandArgument property with the row's index.
            DeleteButton.CommandArgument = e.Row.RowIndex.ToString();

            //Add Client Side confirmation
            DeleteButton.OnClientClick = "return confirm('Are you sure you want to delete this item?');";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    #endregion
}
