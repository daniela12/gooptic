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
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.ECommerce.Catalog;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.DataAccess;
using ZNode.Libraries.Paypal;

/// <summary>
/// Summary description for NotificationPageBase
/// </summary>
public class NotificationPageBase :ZNodePageBase
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.MasterPageFile = "~/themes/" + ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.Theme + "/Checkout/Notification.master";
    }
}
