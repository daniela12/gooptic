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
using ZNode.Libraries.Framework.Business;

public partial class Admin_Secure_catalog_product_reviews_Default : System.Web.UI.Page
{
    # region Protected Member Variables
    protected string ChangeStatusPageLink = "~/admin/secure/catalog/product_reviews/reviewStatus.aspx?ItemId=";
    protected string EditReviewPageLink = "~/admin/secure/catalog/product_reviews/edit_review.aspx?ItemId=";
    protected string DeleteReviewPageLink = "~/admin/secure/catalog/product_reviews/delete.aspx?ItemId=";
    protected string ListReviewPageLink = "~/admin/secure/catalog/product_reviews/default.aspx";
    protected static bool IsSearchEnabled = false;
    # endregion

    # region Bind Methods
    /// <summary>
    /// Bind grid
    /// </summary>
    private void Bind()
    {
        ReviewAdmin reviewAdmin = new ReviewAdmin();
        TList<Review> reviewList = reviewAdmin.GetAll();

        //Apply sort
        if (reviewList != null)
        {
            reviewList.Sort("ReviewID desc");
        }

        uxGrid.DataSource = reviewList;
        uxGrid.DataBind();
    }
    /// <summary>
    /// Bind product to the drop down list
    /// </summary>
    public void BindProductNames()
    {
        //Bind Drop down list
        ProductAdmin productAdmin = new ProductAdmin();
        TList<Product> productList = productAdmin.GetAllProducts(ZNodeConfigManager.SiteConfig.PortalID);
        productList.Sort("Name");
        ddlProductNames.DataSource = productList;
        ddlProductNames.DataTextField = "Name";
        ddlProductNames.DataValueField = "ProductId";
        ddlProductNames.DataBind();

        ListItem li = new ListItem("All","0");
        li.Selected = true;
        ddlProductNames.Items.Insert(0, li);

    }

    /// <summary>
    /// Bind filtered collection list to grid control
    /// </summary>
    private void SearchReviews()
    {
        if (ViewState["ReviewList"] != null)
        {
            //read from ViewState 
            TList<Review> reviewList = (TList<Review>)ViewState["ReviewList"];

            uxGrid.DataSource = reviewList;
            uxGrid.DataBind();
        }
    }
    # endregion

    # region Events
    /// <summary>
    /// Occurs when the server control is loaded into the Page object. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //This is to indicate whether the page is being loaded in response to a client postback, 
        //or if it is being loaded and accessed for the first time.
        if (!Page.IsPostBack)
        {
            BindProductNames();
            Bind();
        }
    }

    /// <summary>
    /// The Click event is raised when the "Search" Button control is clicked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IsSearchEnabled = true;

        ReviewAdmin reviewAdmin = new ReviewAdmin();
        TList<Review> reviewList = reviewAdmin.GetAll();

        string sqlQuery = "subject like '*" + ReviewTitle.Text.Trim() + "*'";
        sqlQuery += " and CreateUser like '*" + Name.Text.Trim() + "*'";

        //Review status
        if (ListReviewStatus.SelectedValue != "0")
        {
            sqlQuery += " and Status = '" + ListReviewStatus.SelectedValue + "'";
        }        

        if (ddlProductNames.SelectedValue != "0")
        {
            sqlQuery += " and ProductId = " + ddlProductNames.SelectedValue;
        }

        if (reviewList != null)
        {
            //Apply filter - it will force the filtering of the review collection,based on the filter expression
            reviewList.Filter = sqlQuery;

            //Apply sort            
            reviewList.Sort("ReviewID desc");

            //save in ViewState
            ViewState["ReviewList"] = reviewList;
            
            uxGrid.DataSource = reviewList;
            uxGrid.DataBind();
        }
    }

    /// <summary>
    /// The Click event is raised when the "Clear" Button control is clicked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        IsSearchEnabled = false;
        Response.Redirect(ListReviewPageLink);
    }
    #endregion

    # region Grid Events
    /// <summary>
    /// Occurs when a button is clicked in a GridView control.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        // Multiple buttons are used in a GridView control, use the
        // CommandName property to determine which button was clicked
        if (e.CommandName == "Edit")
        {
            //
            int reviewID = int.Parse(e.CommandArgument.ToString());

            //Redirect to change review status page
            Response.Redirect(EditReviewPageLink + reviewID);
        }
        else if (e.CommandName == "Delete")
        {
            //
            int reviewID = int.Parse(e.CommandArgument.ToString());

            //Redirect to change review delete confirmation page
            Response.Redirect(DeleteReviewPageLink + reviewID);
        }
        else if (e.CommandName == "Status")
        {
            //
            int reviewID = int.Parse(e.CommandArgument.ToString());

            //Redirect to change review status page
            Response.Redirect(ChangeStatusPageLink + reviewID);
        }
    }

    /// <summary>
    /// Occurs when one of the pager buttons is clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;

        //If Search is enabled, then it will bind the filtered collection to the data grid
        if (IsSearchEnabled)
        {
            SearchReviews();
        }
        else
        {
            Bind();
        }
    }
    #endregion

    # region Helper Methods
    protected string ReviewStatus(string reviewStatus)
    {

        if (reviewStatus == "A")
        {
            return "Active";
        }
        else if(reviewStatus == "N")            
        {
            return "New";
        }

        return "In Active"; 
        
    }
    #endregion
}
