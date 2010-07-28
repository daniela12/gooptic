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
using System.IO;

public partial class Admin_Activate : System.Web.UI.Page
{
    # region Private Member Variables
    protected string DomainName = "";
    private string customerIPaddress = HttpContext.Current.Request.UserHostAddress;
    # endregion

    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {        
        //Check the EnableDiagnostics value in the config to allow user to run the diagnostics tool
        if (System.Configuration.ConfigurationManager.AppSettings["EnableActivationPage"].ToString() == "0")
        {
            throw (new ApplicationException("The Activation page is disabled for your storefront."));
        }
        else
        {
            // Set Domain name
            DomainName = Request.Url.Host + Request.ApplicationPath;

            if (!Page.IsPostBack)
            {
                // Remove the license from the cache so that we will be sure to re-check it.
                if (HttpContext.Current.Application["ZNODELICENSE"] != null)
                {
                    HttpContext.Current.Application.Remove("ZNODELICENSE");
                }

                CheckLicense();

                chkFreeTrial.Visible = true;
                chkFreeTrial.Checked = true;
                pnlSerial.Visible = false;

                txtEULA.Text = ZNodeEULA.GetEULA();
                lblDomainName.Text = DomainName;
                chkIntro.Text = "Yes I wish to activate my license for the domain name: <strong>" + DomainName + "</strong>. I also understand that once activated, my key cannot be transferred to a different domain.";

                // Log the Details
                ZNodeLogging.LogActivity(103, customerIPaddress);
            }
        }
    }

    /// <summary>
    /// Initialize display
    /// </summary>
    private void InitDisplay()
    {
    }

    /// <summary>
    /// Activate button clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnActivateLicense_Click(object sender, EventArgs e)
    {
        //check if user agreed to EULA
        if (!chkEULA.Checked)
        {
            lblError.Text = "Storefront Activation requires that you accept the software license agreement (EULA)";
            return;
        }

        //get the target license requested
        ZNodeLicenseType lt = ZNodeLicenseType.Trial; //default
        if (chkFreeTrial.Checked)
        {
            lt = ZNodeLicenseType.Trial;
        }
        else if (chkEntLicense.Checked)
        {
            lt = ZNodeLicenseType.Enterprise;
        }
        else if (chkSerLicense.Checked)
        {
            lt = ZNodeLicenseType.Server;
        }

        //install license
        ZNodeLicenseManager lm = new ZNodeLicenseManager();
        bool retval = false;
        string ErrorMessage = "";
        retval = lm.InstallLicense(lt,txtSerialNumber.Text.Trim(),txtName.Text, txtEmail.Text,out ErrorMessage);
        
        //if success
        if (retval)
        {
            lblConfirm.Text = "Your Znode Storefront license has been successfully activated.";
            pnlConfirm.Visible = true;
            pnlLicenseActivate.Visible = false;
            ZNodeLogging.LogActivity(104,customerIPaddress,DomainName,txtName.Text, txtEmail.Text,lblConfirm.Text);
        }
        //install failed
        else
        {
            lblError.Text = "Failed to activate license. ";
            lblError.Text = lblError.Text + ErrorMessage;
            ZNodeLogging.LogActivity(105, customerIPaddress, DomainName, txtName.Text, txtEmail.Text, lblError.Text);
        }

    }

    /// <summary>
    /// License option changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkFreeTrial_CheckedChanged(object sender, EventArgs e)
    {
        
        if (chkFreeTrial.Visible)
        {
            if (chkFreeTrial.Checked)
            {
                pnlSerial.Visible = false;
            }
            else
            {
                pnlSerial.Visible = true;
            }
        }
        else
        {
            pnlSerial.Visible = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnProceedToActivation_Click(object sender, EventArgs e)
    {
        // Check if user agreed to proceed with activation process
        if (!chkIntro.Checked)
        {
            lblErrorMsg.Text = "You would need to accept the above terms before proceeding with activation.";
            return;
        }
        
        pnlLicenseActivate.Visible = true;
        pnlIntro.Visible = false;
    }

    # region Helper Methods
    /// <summary>
    /// 
    /// </summary>
    protected void CheckLicense()
    {
        // Get the target license requested
        ZNodeLicenseType lt = ZNodeLicenseType.Invalid;

        //Create Instance for License Manager
        ZNodeLicenseManager LicenseManager = new ZNodeLicenseManager();
        lt = LicenseManager.Validate();

    }
    #endregion
    protected void chkIntro_CheckedChanged(object sender, EventArgs e)
    {
        btnActivateLicense.Enabled = true;
        btnProceedToActivation.Enabled = true;
               
        if (!chkIntro.Checked)
        {
            btnProceedToActivation.Enabled = false;
        }
    }
}
