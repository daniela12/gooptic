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
using ZNode.Libraries.DataAccess.Custom;
using ZNode.Libraries.Framework.Business;

public partial class Admin_Secure_catalog_product_addons_add_Addonvalues : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    protected int AddOnValueId = 0;    
    protected string ViewLink = "~/admin/secure/catalog/product_addons/view.aspx?itemid=";
    protected string CancelLink = "~/admin/secure/catalog/product_addons/view.aspx?itemid=";    
    #endregion

    # region Page Load Event
    /// <summary>
    /// Page Load Event - fires while page is loading
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

        if (Request.Params["AddOnValueId"] != null)
        {
            AddOnValueId = int.Parse(Request.Params["AddOnValueId"]);
        }

        if (!Page.IsPostBack)
        {
            BindTaxClasses();
            BindBillingFrequency();
            BindShippingTypes();
            BindSuppliersTypes();

            //if edit func then bind the data fields
            if (AddOnValueId > 0)
            {
                lblTitle.Text = "Edit Add-On Value : ";
                BindEditData();
            }
            else
            {
                lblTitle.Text = "Add Add-On Value";                
            }
        }
    }
    # endregion

    # region Bind Methods
    /// <summary>
    /// Bind Shipping option list
    /// </summary>
    private void BindShippingTypes()
    {
        // Bind ShippingRuleTypes
        ShippingAdmin shippingAdmin = new ShippingAdmin();
        ShippingTypeList.DataSource = shippingAdmin.GetShippingRuleTypes();
        ShippingTypeList.DataTextField = "description";
        ShippingTypeList.DataValueField = "shippingruletypeid";
        ShippingTypeList.DataBind();
    }

    /// <summary>
    /// Bind tax classes list
    /// </summary>
    private void BindTaxClasses()
    {
        // Bind Tax Class
        TaxRuleAdmin TaxRuleAdmin = new TaxRuleAdmin();
        ddlTaxClass.DataSource = TaxRuleAdmin.GetAllTaxClass();
        ddlTaxClass.DataTextField = "name";
        ddlTaxClass.DataValueField = "TaxClassID";
        ddlTaxClass.DataBind();
    }

    /// <summary>
    /// Bind supplier option list
    /// </summary>
    private void BindSuppliersTypes()
    {
        //Bind Supplier
        ZNode.Libraries.DataAccess.Service.SupplierService serv = new ZNode.Libraries.DataAccess.Service.SupplierService();
        TList<ZNode.Libraries.DataAccess.Entities.Supplier> list = serv.GetAll();
        list.Sort("DisplayOrder Asc");
        list.ApplyFilter(delegate(ZNode.Libraries.DataAccess.Entities.Supplier supplier)
        { return (supplier.ActiveInd == true); });

        DataSet ds = list.ToDataSet(false);
        DataView dv = new DataView(ds.Tables[0]);
        ddlSupplier.DataSource = dv;
        ddlSupplier.DataTextField = "name";
        ddlSupplier.DataValueField = "supplierid";
        ddlSupplier.DataBind();
        ListItem li1 = new ListItem("None", "0");
        ddlSupplier.Items.Insert(0, li1);
    }

    /// <summary>
    /// Bind data to the fields on the edit screen
    /// </summary>
    protected void BindEditData()
    {
        ProductAddOnAdmin AddOnAdmin = new ProductAddOnAdmin();
        AddOnValue AddOnValueEntity = AddOnAdmin.GetByAddOnValueID(AddOnValueId);

        if (AddOnValueEntity != null)
        {
            //General Settings
            lblTitle.Text += AddOnValueEntity.Name;
            txtAddOnValueName.Text = AddOnValueEntity.Name;
            txtAddOnValueRetailPrice.Text = AddOnValueEntity.RetailPrice.ToString("N");
            if (AddOnValueEntity.SalePrice.HasValue)
                txtSalePrice.Text = AddOnValueEntity.SalePrice.Value.ToString("N");

            if (AddOnValueEntity.WholesalePrice.HasValue)
                txtWholeSalePrice.Text = AddOnValueEntity.WholesalePrice.Value.ToString("N");
            if (ddlSupplier.SelectedIndex != -1)
            {
                ddlSupplier.SelectedValue = AddOnValueEntity.SupplierID.ToString();
            }

            if (ddlTaxClass.SelectedIndex != -1)
                ddlTaxClass.SelectedValue = AddOnValueEntity.TaxClassID.GetValueOrDefault(0).ToString();

            // Display Settings
            txtAddonValueDispOrder.Text = AddOnValueEntity.DisplayOrder.ToString();
            chkIsDefault.Checked = AddOnValueEntity.DefaultInd;

            // Inventory Settings
            txtAddOnValueSKU.Text = AddOnValueEntity.SKU;
            txtAddOnValueQuantity.Text = AddOnValueEntity.QuantityOnHand.ToString();
            if(AddOnValueEntity.ReorderLevel.HasValue)
            txtReOrder.Text = AddOnValueEntity.ReorderLevel.ToString();
            txtAddOnValueWeight.Text = AddOnValueEntity.Weight.ToString();
            if (AddOnValueEntity.Height.HasValue)
                Height.Text = AddOnValueEntity.Height.Value.ToString("N2");
            if (AddOnValueEntity.Width.HasValue)
                Width.Text = AddOnValueEntity.Width.Value.ToString("N2");
            if (AddOnValueEntity.Length.HasValue)
                Length.Text = AddOnValueEntity.Length.Value.ToString("N2");

            // Shipping Settings
            if (AddOnValueEntity.ShippingRuleTypeID.HasValue)            
                ShippingTypeList.SelectedValue = AddOnValueEntity.ShippingRuleTypeID.Value.ToString();                            
            EnableValidators();

            if (AddOnValueEntity.FreeShippingInd.HasValue)
                chkFreeShippingInd.Checked = AddOnValueEntity.FreeShippingInd.Value;

            // Recurring Billing
            pnlRecurringBilling.Visible = AddOnValueEntity.RecurringBillingInd;
            chkRecurringBillingInd.Checked = AddOnValueEntity.RecurringBillingInd;            
            txtSubscrptionCycles.Text = AddOnValueEntity.RecurringBillingTotalCycles.GetValueOrDefault(0).ToString();
            ddlBillingPeriods.SelectedValue = AddOnValueEntity.RecurringBillingPeriod;
            ddlBillingFrequency.SelectedValue = AddOnValueEntity.RecurringBillingFrequency;
        }
        else
        {
            throw (new ApplicationException("Add-On Value Requested could not be found."));
        }
    }
    #endregion

    # region General Events
    /// <summary>
    /// Submit Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddOnValueSubmit_Click(object sender, EventArgs e)
    {
        ProductAddOnAdmin AddOnValueAdmin = new ProductAddOnAdmin();
        AddOnValue addOnValue = new AddOnValue();

        if (AddOnValueId > 0)
        {
            addOnValue = AddOnValueAdmin.GetByAddOnValueID(AddOnValueId);
        }

        // Set Properties
        // General Settings
        addOnValue.AddOnID = ItemId; // AddOnId from querystring 
        addOnValue.Name = txtAddOnValueName.Text.Trim();
        addOnValue.RetailPrice = decimal.Parse(txtAddOnValueRetailPrice.Text.Trim());
        addOnValue.SalePrice = null;
        addOnValue.WholesalePrice = null;

        if(txtSalePrice.Text.Trim().Length > 0)
        addOnValue.SalePrice = decimal.Parse(txtSalePrice.Text.Trim());
        if(txtWholeSalePrice.Text.Trim().Length > 0)
        addOnValue.WholesalePrice = decimal.Parse(txtWholeSalePrice.Text.Trim());

        // Display Settings
        addOnValue.DisplayOrder = int.Parse(txtAddonValueDispOrder.Text.Trim());
        addOnValue.DefaultInd = chkIsDefault.Checked;

        // Inventory Settings
        addOnValue.SKU = txtAddOnValueSKU.Text.Trim();        
        addOnValue.QuantityOnHand = int.Parse(txtAddOnValueQuantity.Text.Trim());
        if (txtReOrder.Text.Trim().Length > 0)
        {
            addOnValue.ReorderLevel = int.Parse(txtReOrder.Text.Trim());
        }
        else
        { addOnValue.ReorderLevel = null; }
        if (txtAddOnValueWeight.Text.Trim().Length > 0)
        {
            addOnValue.Weight = decimal.Parse(txtAddOnValueWeight.Text.Trim());
        }        

        # region Add-On Package Dimensions
        if (Height.Text.Trim().Length > 0)
        {
            addOnValue.Height = decimal.Parse(Height.Text.Trim());
        }
        else { addOnValue.Height = null; }

        if (Width.Text.Trim().Length > 0)
        {
            addOnValue.Width = decimal.Parse(Width.Text.Trim());
        }
        else { addOnValue.Width = null; }

        if (Length.Text.Trim().Length > 0)
        {
            addOnValue.Length = decimal.Parse(Length.Text.Trim());
        }
        else 
        { addOnValue.Length = null; }
        #endregion

        // set shipping type for this add-on item
        addOnValue.ShippingRuleTypeID = int.Parse(ShippingTypeList.SelectedValue);
        addOnValue.FreeShippingInd = chkFreeShippingInd.Checked;

        if (chkRecurringBillingInd.Checked)
        {
            // Recurring Billing
            addOnValue.RecurringBillingInd = chkRecurringBillingInd.Checked;            
            addOnValue.RecurringBillingPeriod = ddlBillingPeriods.SelectedItem.Value;
            addOnValue.RecurringBillingFrequency = ddlBillingFrequency.SelectedItem.Text;
            addOnValue.RecurringBillingTotalCycles = int.Parse(txtSubscrptionCycles.Text.Trim());
        }
        else
        {
            addOnValue.RecurringBillingInd = false;
            addOnValue.RecurringBillingInstallmentInd = false;
        }

        // Supplier
        if (ddlSupplier.SelectedIndex != -1)
        {
            if (ddlSupplier.SelectedItem.Text.Equals("None"))
            {
                addOnValue.SupplierID = null;
            }
            else
            {
                addOnValue.SupplierID = Convert.ToInt32(ddlSupplier.SelectedValue);
            }
        }

        // Tax Class
        if (ddlTaxClass.SelectedIndex != -1)
        {
            addOnValue.TaxClassID = int.Parse(ddlTaxClass.SelectedValue);
        }

        bool status = false;

        if (AddOnValueId > 0)
        {
            // set update date
            addOnValue.UpdateDte = System.DateTime.Now;

            // Update option values
            status = AddOnValueAdmin.UpdateAddOnValue(addOnValue);
        }
        else
        {
            // Add new option values
            status = AddOnValueAdmin.AddNewAddOnValue(addOnValue);
        }

        if (status)
        {
            Response.Redirect(ViewLink + ItemId);
        }
        else
        {
            lblAddonValueMsg.Text = "Could not update the Add-On Value. Please try again.";            
            return;
        }
    }

    /// <summary>
    /// Cancel Button Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(CancelLink + ItemId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ShippingTypeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        EnableValidators();        
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
    #endregion    

    # region Helper Method
    
    /// <summary>
    /// 
    /// </summary>
    private void EnableValidators()
    {
        if (ShippingTypeList.SelectedValue == "2")
        {
            weightBasedRangeValidator.Enabled = true;
            RequiredForWeightBasedoption.Enabled = true;
        }
        else
        {
            weightBasedRangeValidator.Enabled = false;
            RequiredForWeightBasedoption.Enabled = false;
        }
    }
    
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
