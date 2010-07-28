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
using ZNode.Libraries.DataAccess.Custom;
using ZNode.Libraries.Framework.Business;
public partial class Admin_Secure_sales_customers_addAffiliatePayment : System.Web.UI.Page
{
    # region Protected Member Variables
    private int ItemId = 0;
    private int AccountId = 0;
    private string _ViewPageLink = "view.aspx?itemid=";
    #endregion

    # region Page Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["itemid"] != null)
        {
            ItemId = int.Parse(Request.Params["itemid"].ToString());
        }
        else
        {
            ItemId = 0;
        }


        if (Request.Params["accId"] != null)
        {
            AccountId = int.Parse(Request.Params["accId"].ToString());
        }
        else
        {
            // Throw error here
        }

        if (ItemId > 0)
        {
            lblTitle.Text = "Edit Payment";
        }
        AccountHelper helperAccess = new AccountHelper();
        DataSet MyDataSet = helperAccess.GetCommisionAmount(ZNodeConfigManager.SiteConfig.PortalID, AccountId.ToString());

        lblAmountOwed.Text = "$" + MyDataSet.Tables[0].Rows[0]["CommissionOwed"].ToString();
    }

    #endregion

    # region General Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DateTime dt = DateTime.Parse(txtDate.Text);

        if(dt.CompareTo(System.DateTime.Today) > 0)
        {
            ErrorMessage.Text = "Received date should not be greater than today.";
            return;
        }

        AccountAdmin accountAdmin = new AccountAdmin();
        AccountPayment entity = new AccountPayment();

        if(ItemId > 0)
            entity = accountAdmin.GetByAccountPaymentId(ItemId);

        entity.AccountID = AccountId;
        entity.Amount = decimal.Parse(txtAmount.Text.Trim());
        entity.Description = txtDescription.Text.Trim();
        entity.ReceivedDate = dt;

        bool status = accountAdmin.AddAffiliatePayment(entity);


        if (status)
        {
            Response.Redirect(_ViewPageLink + AccountId);
        }
        else
        {
            ErrorMessage.Text = "";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(_ViewPageLink + AccountId);
    }
    #endregion
}
