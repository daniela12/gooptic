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

/// <summary>
/// Summary description for ForgotPasswordPageBase.cs
/// </summary>
public class ForgotPasswordPageBase : ZNodePageBase
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.MasterPageFile = "~/themes/" + ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.Theme + "/account/forgotpassword.master"; 
    }
}