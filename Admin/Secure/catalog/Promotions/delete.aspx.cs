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

public partial class Admin_Secure_catalog_Promotions_delete : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemID;
    protected string RedirectLink = "~/admin/secure/catalog/promotions/list.aspx";
    protected string PromotionName = string.Empty;
    #endregion

    # region Page Load
    
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get ItemID from querystring        
        if (Request.Params["ItemID"] != null)
        {
            ItemID = int.Parse(Request.Params["ItemID"]);
        }
        else
        {
            ItemID = 0;
        }

        if (!Page.IsPostBack)
        {
            this.BindData();
        }
    }

    # endregion

    # region Bind Data
    /// <summary>
    /// Bind Promotion name
    /// </summary>
    private void BindData()
    {
        ZNode.Libraries.Admin.PromotionAdmin PromotionAdmin = new ZNode.Libraries.Admin.PromotionAdmin();
        ZNode.Libraries.DataAccess.Entities.Promotion _promotion = PromotionAdmin.GetByPromotionId(ItemID);

        if (_promotion != null)
        {
            PromotionName = _promotion.Name;
        }
    }

    # endregion

    # region General Events

    /// <summary>
    /// Delete Button click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ZNode.Libraries.Admin.PromotionAdmin PromotionAdmin = new ZNode.Libraries.Admin.PromotionAdmin();       
       
        bool check = false;

        check = PromotionAdmin.DeletePromotion(ItemID);
        
        if (check)
        {
            //Replace the Promtion Cache application object with new active promotions
            HttpContext.Current.Application["PromotionCache"] = PromotionAdmin.GetActivePromotions();

            Response.Redirect(RedirectLink);
        }
        else
        {
            lblErrorMsg.Text = "Delete action could not be completed. Please try again.";
            lblErrorMsg.Visible = true;
        }
    }
    
    /// <summary>
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(RedirectLink);
    }

    # endregion
    
}
