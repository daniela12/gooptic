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
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.ECommerce.SEO;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Data;

public partial class Admin_Secure_SEO_UrlRedirect_edit : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    protected string ListPageLink = "~/admin/secure/SEO/UrlRedirect/default.aspx";
    #endregion    

    #region Bind Data
    /// <summary>
    /// Bind data to the fields 
    /// </summary>
    protected void BindData()
    {
        UrlRedirectAdmin urlRedirectAdmin = new UrlRedirectAdmin();
        UrlRedirect urlRedirectEntity = null;

        if (ItemId > 0)
        {
            urlRedirectEntity = urlRedirectAdmin.GetById(ItemId);

            if (urlRedirectEntity != null)
            {
                txtNewUrl.Text = FormattedUrl(urlRedirectEntity.NewUrl);
                txtOldUrl.Text = FormattedUrl(urlRedirectEntity.OldUrl);
                chkIsActive.Checked = urlRedirectEntity.IsActive;
            }
        }
        else
        {
            // nothing to do here
        }
    }
    #endregion

    # region Events

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
            // if edit func then bind the data fields
            if (ItemId > 0)
            {
                lblTitle.Text = "Edit URL 301 Redirect";
                // Bind data to the fields on the page
                BindData();
            }
            else
            {
                lblTitle.Text = "Add URL 301 Redirect";
            }

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {   
        UrlRedirectAdmin urlRedirectAdmin = new UrlRedirectAdmin();
        UrlRedirect urlRedirectEntity = new UrlRedirect();
        bool status = false;
        string oldSeoURL = "";
        bool activeInd = false;

        if (ItemId > 0)
        {
            urlRedirectEntity = urlRedirectAdmin.GetById(ItemId);
            oldSeoURL = urlRedirectEntity.OldUrl;
            activeInd = urlRedirectEntity.IsActive;
        }

        urlRedirectEntity.IsActive = chkIsActive.Checked;
        urlRedirectEntity.OldUrl = MakeUrl(txtOldUrl.Text.Trim());
        urlRedirectEntity.NewUrl = MakeUrl(txtNewUrl.Text.Trim());

        // create transaction
        TransactionManager tranManager = ConnectionScope.CreateTransaction();

        if (ItemId > 0)
        {
            status = urlRedirectAdmin.Update(urlRedirectEntity);            
        }
        else
        {
            status = urlRedirectAdmin.Add(urlRedirectEntity);
        }

        if (status)
        {
            try
            {
                ZNodeSEOUrl seoUrl = new ZNodeSEOUrl();
                bool urlRedirectExists = urlRedirectAdmin.Exists(urlRedirectEntity.OldUrl, urlRedirectEntity.NewUrl, urlRedirectEntity.UrlRedirectID);

                if (ItemId > 0 && activeInd && !chkIsActive.Checked)
                {
                    status &= seoUrl.RemoveRedirectURL(oldSeoURL);
                }
                else if(chkIsActive.Checked && !urlRedirectExists)
                {
                    status &= seoUrl.AddRedirectUrl(oldSeoURL, urlRedirectEntity.OldUrl, urlRedirectEntity.NewUrl, chkIsActive.Checked);                
                }
                else if(urlRedirectExists)
                {
                    status &= false;

                    // error occurred so rollback transaction
                    tranManager.Rollback();

                    lblMsg.Text = "Unable to update SEO Friendly URL. Please try again.";

                    return;
                }
            }
            catch
            {
                status = false;

                // error occurred so rollback transaction        
                tranManager.Rollback();

                lblMsg.Text = "Unable to update SEO Friendly URL. Please try again.";

                return;
            }
        }

        if (status)
        {
            // Commit transaction
            tranManager.Commit();

            Response.Redirect(ListPageLink);
        }
        else
        {
            // error occurred so rollback transaction        
            tranManager.Rollback();

            lblMsg.Text = "Unable to process your request.";
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
    #endregion

    # region Helper Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Url"></param>
    /// <returns></returns>
    private string FormattedUrl(string Url)
    {
        if (Url.Length > 0)
        {
            Url = Url.Replace("~/", string.Empty);          
        }

        return Url;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="SeoUrl"></param>
    /// <returns></returns>
    private string MakeUrl(string SeoUrl)
    {
        SeoUrl = "~/" + SeoUrl;

        return SeoUrl;
    }
    # endregion
}
