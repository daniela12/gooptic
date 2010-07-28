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
using ZNode.Libraries.Framework.Business;

public partial class Admin_Secure_catalog_product_edit_advancedsettings : System.Web.UI.Page
{
    # region Protected Member Variables
    protected int ItemID = 0;
    protected string ManagePageLink = "~/admin/secure/catalog/product/view.aspx?mode=advanced&itemid=";
    protected string CancelPageLink = "~/admin/secure/catalog/product/view.aspx?mode=advanced&itemid=";
    # endregion

    # region Page Load Event
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["itemid"] != null)
        {
            ItemID = int.Parse(Request.Params["itemid"].ToString());
        }

        if (!Page.IsPostBack)
        {
            BindBillingFrequency();

            if (ItemID > 0)
            {
                lblTitle.Text = "Edit Advanced Settings for ";


                //Bind Details
                this.Bind();
            }
        }
    }
    #endregion

    # region Bind Methods
    /// <summary>
    /// 
    /// </summary>
    private void Bind()
    {
        //Create Instance for Product Admin and Product entity
        ZNode.Libraries.Admin.ProductAdmin ProdAdmin = new ProductAdmin();
        Product _product = ProdAdmin.GetByProductId(ItemID);

        if (_product != null)
        {
            //Display Settings                                             
            CheckEnabled.Checked = _product.ActiveInd;
            CheckHomeSpecial.Checked = _product.HomepageSpecial;
            CheckCallPricing.Checked = _product.CallForPricing;
            ChkFeaturedProduct.Checked = _product.FeaturedInd;

            if (_product.NewProductInd.HasValue)
            {
                CheckNewItem.Checked = _product.NewProductInd.Value;
            }


            //Inventory Setting - Out of Stock Options
            if (_product.AllowBackOrder.HasValue && _product.TrackInventoryInd.HasValue)
            {
                if ((_product.TrackInventoryInd.Value) && (_product.AllowBackOrder.Value == false))
                {
                    InvSettingRadiobtnList.Items[0].Selected = true;
                }
                else if (_product.TrackInventoryInd.Value && _product.AllowBackOrder.Value)
                {
                    InvSettingRadiobtnList.Items[1].Selected = true;
                }
                else if ((_product.TrackInventoryInd.Value == false) && (_product.AllowBackOrder.Value == false))
                {
                    InvSettingRadiobtnList.Items[2].Selected = true;
                }
            }

            //Inventory Setting - Stock Messages
            txtInStockMsg.Text = _product.InStockMsg;
            txtOutofStock.Text = _product.OutOfStockMsg;
            txtBackOrderMsg.Text = _product.BackOrderMsg;

            if (_product.DropShipInd.HasValue)
                chkDropShip.Checked = _product.DropShipInd.Value;

            lblTitle.Text += "\"" + _product.Name + "\"";

            // Recurring Billing
            chkRecurringBillingInd.Checked = _product.RecurringBillingInd;
            pnlRecurringBilling.Visible = _product.RecurringBillingInd;
            ddlBillingPeriods.SelectedValue = _product.RecurringBillingPeriod;
            ddlBillingFrequency.SelectedValue = _product.RecurringBillingFrequency;
            txtSubscrptionCycles.Text = _product.RecurringBillingTotalCycles.GetValueOrDefault(0).ToString();            
        }

    }
    #endregion

    # region General Events
    /// <summary>
    /// Submit Button Click Event - Fires when Submit button is triggered
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ProductAdmin _ProductAdmin = new ProductAdmin();
        Product _product = new Product();

        //if edit mode then get all the values first
        if (ItemID > 0)
        {
            _product = _ProductAdmin.GetByProductId(ItemID);

        }

        //Set properties
        //Display Settings
        _product.ActiveInd = CheckEnabled.Checked;
        _product.HomepageSpecial = CheckHomeSpecial.Checked;
        _product.InventoryDisplay = Convert.ToByte(false);
        _product.CallForPricing = CheckCallPricing.Checked;
        _product.NewProductInd = CheckNewItem.Checked;
        _product.FeaturedInd = ChkFeaturedProduct.Checked;

        //Inventory Setting - Out of Stock Options
        if (InvSettingRadiobtnList.SelectedValue.Equals("1"))
        {
            //Only Sell if Inventory Available - Set values
            _product.TrackInventoryInd = true;
            _product.AllowBackOrder = false;
        }
        else if (InvSettingRadiobtnList.SelectedValue.Equals("2"))
        {
            //Allow Back Order - Set values
            _product.TrackInventoryInd = true;
            _product.AllowBackOrder = true;
        }
        else if (InvSettingRadiobtnList.SelectedValue.Equals("3"))
        {
            //Don't Track Inventory - Set property values
            _product.TrackInventoryInd = false;
            _product.AllowBackOrder = false;
        }

        //Inventory Setting - Stock Messages
        if (txtOutofStock.Text.Trim().Length == 0)
        {
            _product.OutOfStockMsg = "Out of Stock";
        }
        else
        {
            _product.OutOfStockMsg = txtOutofStock.Text.Trim();
        }
        _product.InStockMsg = txtInStockMsg.Text.Trim();
        _product.BackOrderMsg = txtBackOrderMsg.Text.Trim();
        _product.DropShipInd = chkDropShip.Checked;


        // Recurring Billing settings
        if (chkRecurringBillingInd.Checked)
        {
            _product.RecurringBillingInd = chkRecurringBillingInd.Checked;            
            _product.RecurringBillingPeriod = ddlBillingPeriods.SelectedItem.Value;
            _product.RecurringBillingFrequency = ddlBillingFrequency.SelectedItem.Text;
            _product.RecurringBillingTotalCycles = int.Parse(txtSubscrptionCycles.Text.Trim());            
        }
        else
        {
            _product.RecurringBillingInd = false;
            _product.RecurringBillingInstallmentInd = false;
        }

        bool status = false;

        try
        {
            if (ItemID > 0) //PRODUCT UPDATE
            {
                status = _ProductAdmin.Update(_product);
            }

            if (status)
            {
                Response.Redirect(ManagePageLink + ItemID);
            }
            else
            {
                lblError.Text = "Unable to update product advanced settings. Please try again.";
            }

        }
        catch (Exception)
        {
            lblError.Text = "Unable to update product advanced settings. Please try again.";
            return;
        }

    }

    /// <summary>
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(CancelPageLink + ItemID);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkRecurringBillingInd_CheckedChanged(object sender, EventArgs e)
    {
        if (chkRecurringBillingInd.Checked)
        {
            pnlRecurringBilling.Visible = true;
        }
        else
        {
            pnlRecurringBilling.Visible = false;
        }
    }   

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlBillingPeriods_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBillingFrequency();
    }
    # endregion

    # region Helper Methods
    /// <summary>
    /// 
    /// </summary>
    private void BindBillingFrequency()
    {
        ddlBillingFrequency.Items.Clear();

        int upperBoundValue = 30;

        if (ddlBillingPeriods.SelectedValue == "WEEK") // Week
        {
            upperBoundValue = 4;
        }
        else if (ddlBillingPeriods.SelectedValue == "MONTH") // month
        {
            upperBoundValue = 12;
        }
        else if (ddlBillingPeriods.SelectedValue == "YEAR") // Year
        {
            upperBoundValue = 1;
        }

        for (int i = 1; i <= upperBoundValue; i++)
        {
            ddlBillingFrequency.Items.Add(i.ToString());
        }
    }
    #endregion
}
