using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.ECommerce.Catalog;

/// <summary>
/// Summary description for EmailFriendPageBase.cs
/// </summary>
public class EmailFriendPageBase : ZNodePageBase
{
    #region Private Variables
    private ZNodeProduct _product;
    protected int _productId;        
    #endregion

    /// <summary>
    /// Page Pre-Initialization Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.MasterPageFile = "~/themes/" + ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.Theme + "/emailfriend/emailfriend.master";

        //get product id from querystring  
        if (Request.Params["zpid"] != null)
        {
            _productId = int.Parse(Request.Params["zpid"]);
        }
        else
        {
            throw (new ApplicationException("Invalid Product Id"));
        }

        //retrieve product data
        _product = ZNodeProduct.Create(_productId, ZNodeConfigManager.SiteConfig.PortalID);

        //add to http context so it can be shared with user controls
        HttpContext.Current.Items.Add("Product", _product);
    }
}
