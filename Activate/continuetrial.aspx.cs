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
using System.Text;

public partial class ContinueTrial : System.Web.UI.Page
{
    public string DaysRemaining = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            ZNodeLicenseManager lm = new ZNodeLicenseManager();
            ZNodeLicenseType lt;
            lt = lm.Validate();

            DaysRemaining = lm.DemoDaysRemaining().ToString() + " Days Remaining";

        }
    }

    
}
