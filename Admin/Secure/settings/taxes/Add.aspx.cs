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
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Data;
using System.Data.SqlClient;

public partial class Admin_Secure_settings_taxes_Add : System.Web.UI.Page
{
    #region Protected Variable
    protected int taxId;
    #endregion

    #region Page Load
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get tax classid from querystring        
        if (Request.Params["taxId"] != null)
        {
            taxId = int.Parse(Request.Params["taxId"]);
        }
        else
        {
            taxId = 0;
        }

        if (Page.IsPostBack == false)
        {
            //if edit func then bind the data fields
            if (taxId > 0)
            {

                lblTitle.Text = "Edit Tax Class";
                BindEditData();
            }
            else
            {
                lblTitle.Text = "Add a Tax Class";
            }       
        }
    }
    #endregion

    #region Bind Data
    /// <summary>
    /// Bind data in the edit mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BindEditData()
    {
        TaxRuleAdmin taxRuleAdmin = new TaxRuleAdmin();
        TaxClass taxClass = new TaxClass();
        
        if (taxId > 0)
        {            
            taxClass = taxRuleAdmin.GetByTaxClassID(taxId);
            lblTitle.Text = lblTitle.Text + " - " + taxClass.Name;
            txtTaxClassName.Text = taxClass.Name;
            txtDisplayOrder.Text = taxClass.DisplayOrder.ToString();
            chkActiveInd.Checked = taxClass.ActiveInd;
        }
    }

    #endregion

    #region General Events
    /// <summary>
    /// Submit button click event to store in Tax Class
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        TaxRuleAdmin taxRuleAdmin = new TaxRuleAdmin();
        TaxClass taxClass = new TaxClass();
        
        if (taxId > 0)
        {
            taxClass = taxRuleAdmin.GetByTaxClassID(taxId);
        }

        taxClass.Name = txtTaxClassName.Text;
        taxClass.DisplayOrder = int.Parse(txtDisplayOrder.Text);
        taxClass.ActiveInd = chkActiveInd.Checked;

        bool retval = false;

        if (taxId > 0)
        {
            retval = taxRuleAdmin.UpdateTaxClass(taxClass);
        }
        else
        {
            retval = taxRuleAdmin.InsertTaxClass(taxClass);
        }

        if (retval)
        {
            // redirect to view page
            Response.Redirect("~/admin/secure/settings/taxes/view.aspx?taxId=" + taxClass.TaxClassID);
        }
        else
        {
            // display error message
            lblMsg.Text = "An error occurred while updating. Please try again.";
        }
    }


    /// <summary>
    /// Cancel Button click event to cancel the action
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (taxId > 0)
        {
            Response.Redirect("~/admin/secure/settings/taxes/view.aspx?taxId=" + taxId);
        }
        else
        {
            Response.Redirect("~/admin/secure/settings/taxes/default.aspx");
        }
    }

    #endregion
}
