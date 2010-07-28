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
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.Admin;

public partial class Admin_Secure_catalog_product_Highlights_delete : System.Web.UI.Page
{
    #region Protected Member Variables
    protected int ItemID;
    protected string HighlightName = string.Empty;
    protected string CancelLink = "~/admin/secure/catalog/product_highlights/default.aspx";
    #endregion

    # region Page Load
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get ItemId from querystring   
        if (Request.Params["Itemid"] != null)
        {
            ItemID = int.Parse(Request.Params["Itemid"].ToString());         
        }
        else
        {
            ItemID = 0;
        }

        if (!Page.IsPostBack)
        {
            if (ItemID > 0)
            {
                HighlightAdmin AdminAccess = new HighlightAdmin();
                Highlight entity = AdminAccess.GetByHighlightID(ItemID);

                HighlightName = entity.Name;
            }
        }
    }
    # endregion

    # region General Events
    /// <summary>
    /// Delete Button Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        HighlightAdmin AdminAccess = new HighlightAdmin();

        bool check = AdminAccess.Delete(ItemID);
        if (check)
        {
            Response.Redirect(CancelLink);
        }
        else
        {
            lblErrorMessage.Text = "The highlight can not be deleted until all associated items are removed. Please ensure that this highlight does not associate with the catalog product. If it does, then delete these Items first.";
        }
    }

    /// <summary>
    /// Cancel Button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(CancelLink);
    }
    # endregion
}
