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

public partial class Admin_Secure_catalog_product_reviews_ReviewStatus : System.Web.UI.Page
{
    # region Protected Member Variables
    private int ItemId;
    protected string ListPageLink = "~/admin/secure/catalog/product_reviews/default.aspx";
    # endregion

    # region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get ItemId from querystring        
        if (Request.Params["itemid"] != null)
        {
            ItemId = int.Parse(Request.Params["itemid"]);
        }
        else
        {
            ItemId = 0;
        }

        if (!Page.IsPostBack)
        {
            Bind();
        }
    }

    # endregion

    # region Bind Methods
    /// <summary>
    /// 
    /// </summary>
    private void Bind()
    { 
        ReviewAdmin reviewAdmin = new ReviewAdmin();
        Review review = reviewAdmin.GetByReviewID(ItemId);

        if (review != null)
        {
            lblReviewHeader.Text = review.Subject;
            ListReviewStatus.SelectedValue = review.Status;
        }
    }
    # endregion

    # region General Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void UpdateReviewStatus_Click(object sender, EventArgs e)
    {
        ReviewAdmin reviewAdmin = new ReviewAdmin();
        Review review = reviewAdmin.GetByReviewID(ItemId);

        if (review != null)
        {
            if (ListReviewStatus.SelectedValue == "I")
            {
                review.Status  = "I";
            }
            else if (ListReviewStatus.SelectedValue == "A")
            {
                review.Status = "A";
            }
            else
            {
                review.Status = "N";
            }
        }

        reviewAdmin.Update(review);

        Response.Redirect(ListPageLink);

    }
    protected void CancelStatus_Click(object sender, EventArgs e)
    {
        Response.Redirect(ListPageLink);
    }
    # endregion
}
