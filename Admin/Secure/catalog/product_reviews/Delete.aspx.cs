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

public partial class Admin_Secure_catalog_product_reviews_Delete : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    protected string ProductReviewTitle = string.Empty;
    #endregion

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
  
        Bind();
    }

    private void Bind()
    {
        ReviewAdmin reviewAdmin = new ReviewAdmin();
        Review Entity = reviewAdmin.GetByReviewID(ItemId);

        if (Entity != null)
        {
            ProductReviewTitle = Entity.Subject;
        }
        else
        {
            throw (new ApplicationException("Review Requested could not be found."));
        }
    }

     #region Events
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/catalog/product_reviews/default.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ReviewAdmin reviewAdmin = new ReviewAdmin();        

        bool retval = reviewAdmin.Delete(ItemId);

        if (retval)
        {
            Response.Redirect("~/admin/secure/catalog/product_reviews/default.aspx");
        }
        else
        {
            lblMsg.Text = "An error occurred and the product review could not be deleted.";// Please ensure that this Add-On does not contain Add-On Values or products. If it does, then delete the Add-On values and products first.";
        }
    }
    #endregion

}
