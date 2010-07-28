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

public partial class Admin_Secure_settings_Profile_add : System.Web.UI.Page
{
    # region Protected Member Variables
    protected int ItemID = 0;
    protected string ListPageLink = "~/admin/secure/settings/profile/default.aspx";    
    # endregion

    # region Page Load Event
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["ItemID"] != null)
        {
            ItemID = int.Parse(Request.Params["ItemID"].ToString());
        }        

        if (!Page.IsPostBack)
        {
            if (ItemID > 0)
            {                
                lblTitle.Text = "Edit Profile – ";
                BindData();
            }
            else
            {
                lblTitle.Text = "Add a New Profile";
                chkShowPrice.Checked = true;                
                
            }
        }

    }
    #endregion

    # region Bind Methods
    /// <summary>
    /// Bind fields for this Profile
    /// </summary>
    protected void BindData()
    {
        ProfileAdmin profileAdmin = new ProfileAdmin();
        ZNode.Libraries.DataAccess.Entities.Profile profile = profileAdmin.GetByProfileID(ItemID);

        if (profile != null)
        {
            ProfileName.Text = profile.Name;
            chkDefaultInd.Checked = profile.IsDefault;
            chkIsAnonymous.Checked = profile.IsAnonymous;
            chkShowPrice.Checked = profile.ShowPricing;
            chkShowOnPartner.Checked = profile.ShowOnPartnerSignup;
            if (profile.UseWholesalePricing.HasValue)
                chkUseWholesalePrice.Checked = profile.UseWholesalePricing.Value;

            chkTaxExempt.Checked = profile.TaxExempt;    
            ExternalAccountNum.Text = profile.DefaultExternalAccountNo;
            
            // Set Page Title
            lblTitle.Text += profile.Name;
        }
        else
        {
            //throw error 
        }       
        
    }
    
    

    #endregion

    #region Events
    /// <summary>
    /// Submit Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ProfileAdmin profileAdmin = new ProfileAdmin();
        ZNode.Libraries.DataAccess.Entities.Profile profile = new Profile();

        if (ItemID > 0) //Edit Mode
        {
            //get profile by Id
            profile = profileAdmin.GetByProfileID(ItemID);
        }

        if (ExternalAccountNum.Text.Trim().Length > 0)
        {
            profile.DefaultExternalAccountNo = ExternalAccountNum.Text.Trim();
        }
        else if (ItemID > 0)
        {
            profile.DefaultExternalAccountNo = ExternalAccountNum.Text.Trim();
        }

        profile.Name = ProfileName.Text.Trim();
        profile.IsDefault = chkDefaultInd.Checked;
        profile.IsAnonymous = chkIsAnonymous.Checked;
        profile.ShowPricing = chkShowPrice.Checked;
        profile.ShowOnPartnerSignup = chkShowOnPartner.Checked;
        profile.UseWholesalePricing = chkUseWholesalePrice.Checked;
        profile.TaxExempt = chkTaxExempt.Checked;
        profile.PortalID = ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.PortalID;
        
        bool Status = false;

        if (ItemID > 0)
        {
            Status = profileAdmin.Update(profile);
        }
        else 
        { 
            Status = profileAdmin.Add(profile); 
        }

        if (Status)
        {
            Response.Redirect(ListPageLink);
        }
        else
        {
            lblErrorMsg.Text = "Unable to update profile. Please try again.";
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
}
