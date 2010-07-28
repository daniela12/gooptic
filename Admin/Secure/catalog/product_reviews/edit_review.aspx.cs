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

public partial class Admin_Secure_catalog_product_reviews_edit_review : System.Web.UI.Page
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
            BindEditData();
        }
    }

    # endregion

    # region Bind Method
    /// <summary>
    /// Bind fields with data
    /// </summary>
    private void BindEditData()
    {
        ReviewAdmin reviewAdmin = new ReviewAdmin();
        Review review = reviewAdmin.GetByReviewID(ItemId);

        if (review != null)
        {
            lblReviewHeader.Text = review.Subject;

            Headline.Text = review.Subject;
            Pros.Text = review.Pros;
            Cons.Text = review.Cons;
            Comments.Text = Server.HtmlDecode(review.Comments);
            ListReviewStatus.SelectedValue = review.Status;
        }
        else
        {
            //throw exception
        }
    }
    # endregion

    # region General Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ReviewAdmin reviewAdmin = new ReviewAdmin();
        Review review = reviewAdmin.GetByReviewID(ItemId);

        if (review != null)
        {

            review.Subject = HttpUtility.HtmlEncode(Headline.Text.Trim());
            review.Pros = HttpUtility.HtmlEncode(Pros.Text.Trim());
            review.Cons = HttpUtility.HtmlEncode(Cons.Text.Trim());
            review.Comments = HttpUtility.HtmlEncode(Comments.Text.Trim());

            if(ListReviewStatus.SelectedValue == "I")
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

            bool Status = reviewAdmin.Update(review);

            if (Status)
            {
                Response.Redirect(ListPageLink);
            }
            else
            {
                //throw error message
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(ListPageLink);
    }
    # endregion
}
