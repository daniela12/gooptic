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
using ZNode.Libraries.ECommerce.Catalog;

/// <summary>
/// Displays HTML content using the content management system
/// </summary>
public partial class ContentBlocks_TextEditor_TextEditor_Display : System.Web.UI.UserControl
{
    protected string _pageName = string.Empty;

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //get product id from querystring  
        if (Request.Params["page"] != null)
        {
            _pageName = Request.Params["page"];
        }
        else
        {
            _pageName = "home";
        }   

        //HTML 
        lblHtml.Text = ZNodeContentManager.GetPageHTMLByName(_pageName);

        //Title
        if (HttpContext.Current.Items["Title"] != null)
        {
            lblTitle.Text = HttpContext.Current.Items["Title"].ToString();
        }
    }
    #endregion
}
