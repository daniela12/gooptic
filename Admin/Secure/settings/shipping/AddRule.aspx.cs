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
using System.Data.SqlClient;

public partial class Admin_Secure_settings_shipping_AddRule : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    protected int ShippingId;
    protected string TierText;
    #endregion

    #region Page Load
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

        if (Request.Params["sid"] != null)
        {
            ShippingId = int.Parse(Request.Params["sid"]);
        }
        else
        {
            ShippingId = 0;
        }

        if (Page.IsPostBack == false)
        {
            //if edit func then bind the data fields
            if (ItemId > 0)
            {
                lblTitle.Text = "Edit Shipping Rule for: ";
            }
            else
            {
                lblTitle.Text = "Add a Shipping Rule for: ";
            }
            txtBaseCost.Text = (0.00).ToString("N");
            txtPerItemCost.Text = (0.00).ToString("N");
            CompareValidator2.Text = "You must enter a valid base cost (ex: " + (123.45).ToString("N") + ")";
            CompareValidator3.Text = "You must enter a valid per-item cost (ex: " + (123.45).ToString("N") + ")";
            //Bind data to the fields on the page
            BindData();
        }
    }
    #endregion

    #region Bind Data
    /// <summary>
    /// Bind data to the fields 
    /// </summary>
    protected void BindData()
    {
        ShippingAdmin shipAdmin = new ShippingAdmin();
        Shipping shippingOption = shipAdmin.GetShippingOptionById(ShippingId);

        //Set Title
        lblTitle.Text = lblTitle.Text + shippingOption.Description;

        //Get shipping rule types
        foreach (ShippingRuleType ruleType in shipAdmin.GetShippingRuleTypes())
        {
            if (ruleType.ShippingRuleTypeID == 3 || ruleType.ShippingRuleTypeID == 4)
            { // Nothing to do here 
            }
            else
            {
                ListItem li = new ListItem(ruleType.Description, ruleType.ShippingRuleTypeID.ToString());
                lstShippingRuleType.Items.Add(li);
            }
        }
        lstShippingRuleType.SelectedIndex = 0;


        if (ItemId > 0)
        {
            ShippingRule shippingRule = shipAdmin.GetShippingRule(ItemId);

            lstShippingRuleType.SelectedValue = shippingRule.ShippingRuleTypeID.ToString();

            SetShippingTypeOptions(shippingRule.ShippingRuleTypeID);

            if (shippingRule.BaseCost != 0)
            {
                txtBaseCost.Text = shippingRule.BaseCost.ToString("N2");
            }
            else
            {
                txtBaseCost.Text = "0";
            }

            if (shippingRule.PerItemCost != 0)
            {
                txtPerItemCost.Text = shippingRule.PerItemCost.ToString("N2");
            }
            else
            {
                txtPerItemCost.Text = "0";
            }

            if (shippingRule.LowerLimit != null)
            {
                txtLowerLimit.Text = shippingRule.LowerLimit.ToString();
            }
            else
            {
                txtLowerLimit.Text = "0";
            }

            if (shippingRule.UpperLimit != null)
            {
                txtUpperLimit.Text = shippingRule.UpperLimit.ToString();
            }
            else
            {
                txtUpperLimit.Text = "0";
            }
           
        }
        else
        {
            pnlNonFlat.Visible = false;
        }

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
        ShippingAdmin shipAdmin = new ShippingAdmin();
        ShippingRule shipRule = new ShippingRule();

        //set shipping id for this rule
        shipRule.ShippingID = ShippingId;

        //If edit mode then retrieve data first
        if (ItemId > 0)
        {
            shipRule = shipAdmin.GetShippingRule(ItemId);
        }

        //set values
        shipRule.ShippingRuleTypeID = int.Parse(lstShippingRuleType.SelectedValue);
        shipRule.BaseCost = decimal.Parse(txtBaseCost.Text);
       
        // "PerItemCost" field is not saving for Quantity and Weight based Rules 
        //So i have commented this out.
        //if (int.Parse(lstShippingRuleType.SelectedValue) == 0)
        //{
            shipRule.PerItemCost = decimal.Parse(txtPerItemCost.Text);
        //}

        if (int.Parse(lstShippingRuleType.SelectedValue) > 0)
        {
            shipRule.LowerLimit = decimal.Parse(txtLowerLimit.Text);
            shipRule.UpperLimit = decimal.Parse(txtUpperLimit.Text);
        }
        else
        {
            shipRule.LowerLimit = null;
            shipRule.UpperLimit = null;
        }
            
        //Update or Add
        bool retval = false;

        if (ItemId > 0)
        {
            retval = shipAdmin.UpdateShippingRule(shipRule);
        }
        else
        {
            retval = shipAdmin.AddShippingRule(shipRule);
        }

        if (retval)
        {
            //redirect to main page
            Response.Redirect("~/admin/secure/settings/shipping/view.aspx?itemid=" + ShippingId.ToString() );
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
        //redirect to main page
        Response.Redirect("~/admin/secure/settings/shipping/view.aspx?itemid=" + ShippingId.ToString());
    }

    /// <summary>
    /// Rule type changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstShippingRuleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetShippingTypeOptions(int.Parse(lstShippingRuleType.SelectedItem.Value));
    }

    #endregion   

    #region Helper Functions
    private void SetShippingTypeOptions(int ShippingTypeID)
    {
        if (ShippingTypeID == 0) 
        {
            pnlNonFlat.Visible = false;
        }
        else if (ShippingTypeID == 1) 
        {
            pnlNonFlat.Visible = true;
            TierText = "# Items";
        }
        else if (ShippingTypeID == 2)
        {
            pnlNonFlat.Visible = true;
            TierText = "lbs";
        }
    }

    #endregion

}
