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


public partial class Admin_Secure_settings_tax_AddRule : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    protected int taxId = 0;
    #endregion

    #region Page Load
    /// <summary>
    /// Page Load Event
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
            BindTaxRuleTypes();

            // Bind Country
            BindCountry();

            //if edit func then bind the data fields
            if (ItemId > 0)
            {
                lblTitle.Text = "Edit Tax Rule";
                BindEditData();
            }
            else
            {
                lblTitle.Text = "Add a Tax Rule";
            }        
        }
    }
    #endregion

    #region Bind Data
    /// <summary>
    /// Bind data to the fields in edit mode
    /// </summary>
    protected void BindEditData()
    {
        TaxRuleAdmin taxAdmin = new TaxRuleAdmin();
        TaxRule taxRule = new TaxRule();            

        if (ItemId > 0)
        {
            taxRule = taxAdmin.GetTaxRule(ItemId);           
            
            // Destination CountryCode
            if (taxRule.DestinationCountryCode != null)
            {
                lstCountries.SelectedValue = taxRule.DestinationCountryCode;

                if (lstCountries.SelectedValue != null)
                {
                    BindStates();
                } 
            }
            
            // Destination StateCode
            if (taxRule.DestinationStateCode != null)
            {
                lstStateOption.SelectedValue = taxRule.DestinationStateCode;

                if (lstStateOption.SelectedValue != null)
                {
                    BindCounty();
                }                                             
            }
          
            // CountyFIPS
            if (taxRule.CountyFIPS != null)
            {
                lstCounty.SelectedValue = taxRule.CountyFIPS;       
            }
            
            // Tax Rates
            txtSalesTax.Text = taxRule.SalesTax.ToString();
            txtVAT.Text = taxRule.VAT.ToString();
            txtGST.Text = taxRule.GST.ToString();
            txtPST.Text = taxRule.PST.ToString();
            txtHST.Text = taxRule.HST.ToString();

            // Tax Preferences
            txtPrecedence.Text = taxRule.Precedence.ToString();
            
            chkTaxInclusiveInd.Checked = taxRule.InclusiveInd;

            ddlRuleTypes.SelectedValue = taxRule.TaxRuleTypeID.GetValueOrDefault().ToString();
        }
        else
        {
           //nothing to do here
        }

    }

    /// <summary>
    /// Bind the Countries
    /// </summary>
    private void BindTaxRuleTypes()
    {
        TaxRuleAdmin taxAdmin = new TaxRuleAdmin();
        
        ddlRuleTypes.DataSource = taxAdmin.GetTaxRuleTypes();
        ddlRuleTypes.DataTextField = "Name";
        ddlRuleTypes.DataValueField = "TaxRuleTypeId";
        ddlRuleTypes.DataBind();
    }

    /// <summary>
    /// Bind the Countries
    /// </summary>
    private void BindCountry()
    {
        ShippingAdmin shipAdmin = new ShippingAdmin();
        TList<ZNode.Libraries.DataAccess.Entities.Country> countries = shipAdmin.GetDestinationCountries();
        
        // Country
        lstCountries.DataSource = countries;
        lstCountries.DataTextField = "Name";
        lstCountries.DataValueField = "Code";
        lstCountries.DataBind();    
    }

    /// <summary>
    /// Bind the States
    /// </summary>
    private void BindStates()
    {
        StateService stateService = new StateService();
        StateQuery filters = new StateQuery();

        // Parameters
        filters.Append(StateColumn.CountryCode, lstCountries.SelectedItem.Value);

        // Get States list
        TList<State> StatesList = stateService.Find(filters.GetParameters());

        lstStateOption.DataSource = StatesList;
        lstStateOption.DataTextField = "Name";
        lstStateOption.DataValueField = "Code";
        lstStateOption.DataBind();
        ListItem li = new ListItem("Apply to ALL States", "0");
        lstStateOption.Items.Insert(0, li);           
    }

    /// <summary>
    /// Bind the County from ZnodeZipcode table
    /// </summary>
    private void BindCounty()
    {
        TaxRuleAdmin taxrule = new TaxRuleAdmin();
        DataSet _countylist = taxrule.GetCountyCodeByStateAbbr(lstStateOption.SelectedValue);

        lstCounty.DataSource = _countylist;
        lstCounty.DataTextField = "CountyName";
        lstCounty.DataValueField = "CountyFIPS";
        lstCounty.DataBind();
        ListItem li = new ListItem("Apply to ALL Counties", "0");
        lstCounty.Items.Insert(0, li);         
    }

    #endregion

    #region General Events
    /// <summary>
    /// Submit button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        TaxRuleAdmin taxAdmin = new TaxRuleAdmin();
        TaxRule taxRule = new TaxRule();

        taxRule.PortalID = ZNodeConfigManager.SiteConfig.PortalID;

        // If edit mode then retrieve data first
        if (ItemId > 0)
        {
            taxRule = taxAdmin.GetTaxRule(ItemId);
        }

        // TaxClassID
        taxRule.TaxClassID = taxId;
        // Destination Country
        if (lstCountries.SelectedValue.Equals("*"))        
            taxRule.DestinationCountryCode = null;
        else
            taxRule.DestinationCountryCode = lstCountries.SelectedValue;

        // Destination State
        if (lstStateOption.SelectedValue != "0")
            taxRule.DestinationStateCode = lstStateOption.SelectedValue;
        else
            taxRule.DestinationStateCode = null;

        // CountyFIPS
        if (lstCounty.SelectedValue != "0")
            taxRule.CountyFIPS = lstCounty.SelectedValue;
        else
            taxRule.CountyFIPS = null;
        
        // Tax Rates
        taxRule.SalesTax = Convert.ToDecimal(txtSalesTax.Text);
        taxRule.VAT = Convert.ToDecimal(txtVAT.Text);
        taxRule.GST = Convert.ToDecimal(txtGST.Text);
        taxRule.PST = Convert.ToDecimal(txtPST.Text);
        taxRule.HST = Convert.ToDecimal(txtHST.Text);
        
        // Tax Preferences
        
        taxRule.InclusiveInd = chkTaxInclusiveInd.Checked;
        taxRule.Precedence = int.Parse(txtPrecedence.Text);

        if (ddlRuleTypes.SelectedIndex != -1)
        {
            taxRule.TaxRuleTypeID = int.Parse(ddlRuleTypes.SelectedValue);
        }

        bool retval = false;

        if (ItemId > 0)
        {
            retval = taxAdmin.UpdateTaxRule(taxRule);
        }
        else
        {
            retval = taxAdmin.AddTaxRule(taxRule);
        }

        if (retval)
        {
            System.Web.HttpContext.Current.Cache.Remove("InclusiveTaxRules");

            //redirect to view page
            Response.Redirect("~/admin/secure/settings/taxes/view.aspx?taxId=" + taxId);
        }
        else
        {
            //display error message
            lblMsg.Text = "An error occurred while updating. Please try again.";
        }
    }

    /// <summary>
    /// Cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/settings/taxes/view.aspx?taxId=" + taxId);
    }      

    /// <summary>
    /// Country option changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstCountries_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindStates();
    }

    /// <summary>
    /// state option changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstStateOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCounty();
    }    

    #endregion        
}
