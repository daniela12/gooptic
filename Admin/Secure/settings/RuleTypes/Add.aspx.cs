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
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.Admin;
using ZNode.Libraries.ECommerce.Promotions;
using ZNode.Libraries.ECommerce.Shipping;
using ZNode.Libraries.ECommerce.Taxes;
using ZNode.Libraries.ECommerce.Suppliers;

public partial class Admin_Secure_settings_RuleTypes_Add : System.Web.UI.Page
{
    # region Private Member Variables
    private int _ItemId = 0;
    private string _ruleType = "0";
    private RuleTypeAdmin _RuleTypeAdmin = new RuleTypeAdmin();
    #endregion

    # region Protected Properties
    /// <summary>
    /// 
    /// </summary>
    protected string RedirectUrl
    {
        get
        {
            string redirectUrl = "~/admin/secure/settings/ruleTypes/";

            if (ddlRuleTypes.SelectedValue == "0")
            {
                redirectUrl += "list.aspx";
            }
            else if (ddlRuleTypes.SelectedValue == "1")
            {
                redirectUrl += "default.aspx";
            }
            else if (ddlRuleTypes.SelectedValue == "2")
            {
                redirectUrl += "taxRuleList.aspx";
            }
            else if (ddlRuleTypes.SelectedValue == "3")
            {
                redirectUrl += "SupplierTypeList.aspx";
            }

            return redirectUrl;
        }
    }
    #endregion

    # region Page Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["ItemID"] != null)
        {
            _ItemId = int.Parse(Request.Params["ItemID"].ToString());
        }

        if (Request.Params["ruletype"] != null)
        {
            _ruleType = Request.Params["ruletype"].ToString();

            ddlRuleTypes.SelectedValue = _ruleType;

            ddlRuleTypes.Enabled = false;
        }

        if (!Page.IsPostBack)
        {
            if (_ItemId > 0)
            {
                if (_ruleType == "0")
                    lblTitle.Text = "Configure .NET Promotion Class ";
                if (_ruleType == "1")
                    lblTitle.Text = "Configure .NET Shipping Class ";
                if (_ruleType == "2")
                    lblTitle.Text = "Configure .NET Tax Class ";
                if (_ruleType == "3")
                    lblTitle.Text = "Configure .NET Supplier Class ";

                Bind();
            }
            else
            {
                if (_ruleType == "0")
                    lblTitle.Text = "Configure .NET Promotion Class ";
                if (_ruleType == "1")
                    lblTitle.Text = "Configure .NET Shipping Class ";
                if (_ruleType == "2")
                    lblTitle.Text = "Configure .NET Tax Class ";
                if (_ruleType == "3")
                    lblTitle.Text = "Configure .NET Supplier Class ";
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
        if (_ruleType == "0") // Promotions
        {
            DiscountType promoType = _RuleTypeAdmin.GetDiscountTypeById(_ItemId);

            txtRuleClassName.Text = promoType.ClassName;
            txtRuleName.Text = promoType.Name;
            txtRuleDesc.Text = promoType.Description;
            chkEnable.Checked = promoType.ActiveInd;

        }
        else if (_ruleType == "1") // Shipping
        {
            ShippingType shippingType = _RuleTypeAdmin.GetShippingTypeById(_ItemId);

            txtRuleClassName.Text = shippingType.ClassName;
            txtRuleName.Text = shippingType.Name;
            txtRuleDesc.Text = shippingType.Description;
            chkEnable.Checked = shippingType.IsActive;
        }
        else if (_ruleType == "2") // Tax
        {

            TaxRuleType taxRuleType = _RuleTypeAdmin.GetTaxRuleTypeById(_ItemId);

            txtRuleClassName.Text = taxRuleType.ClassName;
            txtRuleName.Text = taxRuleType.Name;
            txtRuleDesc.Text = taxRuleType.Description;
            chkEnable.Checked = taxRuleType.ActiveInd;
        }
        else if (_ruleType == "3") // Supplier
        {

            SupplierType supplierType = _RuleTypeAdmin.GetSupplierTypeById(_ItemId);

            txtRuleClassName.Text = supplierType.ClassName;
            txtRuleName.Text = supplierType.Name;
            txtRuleDesc.Text = supplierType.Description;
            chkEnable.Checked = supplierType.ActiveInd;
        }

        lblTitle.Text += " - "+txtRuleName.Text.Trim();
    }
    #endregion

    # region General Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {   
        bool status = false;

        if (ddlRuleTypes.SelectedValue == "0") // Promotions
        {
            if (!CheckAssemblyClass(typeof(ZNodePromotionOption)))
                return;

            DiscountType entity = new DiscountType();
            
            if(_ItemId > 0)
               entity = _RuleTypeAdmin.GetDiscountTypeById(_ItemId);

            entity.Name = txtRuleName.Text.Trim();
            entity.ClassName = txtRuleClassName.Text.Trim();
            entity.Description = txtRuleDesc.Text.Trim();
            entity.ActiveInd = chkEnable.Checked;

            if (_ItemId > 0)
                status = _RuleTypeAdmin.UpdateDicountType(entity);
            else
                status = _RuleTypeAdmin.AddDicountType(entity);
        }
        else if (ddlRuleTypes.SelectedValue == "1") // Shipping
        {
            if (!CheckAssemblyClass(typeof(ZNodeShippingOption)))
                return;

            ShippingType shipType = new ShippingType();

            if (_ItemId > 0)
                shipType = _RuleTypeAdmin.GetShippingTypeById(_ItemId);

            shipType.Name = txtRuleName.Text.Trim();
            shipType.Description = txtRuleDesc.Text.Trim();
            shipType.ClassName = txtRuleClassName.Text.Trim();
            shipType.IsActive = chkEnable.Checked;

            if (_ItemId > 0)
                status = _RuleTypeAdmin.UpdateShippingType(shipType);
            else
                status = _RuleTypeAdmin.AddShippingType(shipType);
        }
        else if (ddlRuleTypes.SelectedValue == "2") // Taxes
        {
            if (!CheckAssemblyClass(typeof(ZNodeTaxOption)))
                return;

            TaxRuleType taxRuleType = new TaxRuleType();

            if (_ItemId > 0)
                taxRuleType = _RuleTypeAdmin.GetTaxRuleTypeById(_ItemId);

            taxRuleType.Name = txtRuleName.Text.Trim();
            taxRuleType.Description = txtRuleDesc.Text.Trim();
            taxRuleType.ClassName = txtRuleClassName.Text.Trim();
            taxRuleType.ActiveInd = chkEnable.Checked;

            if (_ItemId > 0)
                status = _RuleTypeAdmin.UpdateTaxRuleType(taxRuleType);
            else
                status = _RuleTypeAdmin.AddTaxRuleType(taxRuleType);
        }
        else if (ddlRuleTypes.SelectedValue == "3") // supplier
        {
            if (!CheckAssemblyClass(typeof(ZNodeSupplierOption)))
                return;

            SupplierType supplierType = new SupplierType();

            if (_ItemId > 0)
                supplierType = _RuleTypeAdmin.GetSupplierTypeById(_ItemId);

            supplierType.Name = txtRuleName.Text.Trim();
            supplierType.Description = txtRuleDesc.Text.Trim();
            supplierType.ClassName = txtRuleClassName.Text.Trim();
            supplierType.ActiveInd = chkEnable.Checked;

            if (_ItemId > 0)
                status = _RuleTypeAdmin.UpdateSupplierType(supplierType);
            else
                status = _RuleTypeAdmin.AddSupplierType(supplierType);
        }

        if (status)
        {
            Response.Redirect(RedirectUrl);
        }
        else
        {
            lblErrorMsg.Text = "Unable to process your request. Please try again.";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(RedirectUrl);
    }

    #endregion

    # region Helper Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="AssemblyName"></param>
    /// <returns></returns>
    protected bool CheckAssemblyClass(Type AssemblyType)
    {
        string className = txtRuleClassName.Text.Trim();

        lblErrorMsg.Text = "Could not instantiate the class " + className + ". Check that the Class Name you typed exactly matches your class.";

        bool status = false;

        try
        {
            string assemblyName = AssemblyType.Assembly.GetName().Name;

            System.Reflection.Assembly promoRuleAssebmly = System.Reflection.Assembly.Load(assemblyName);

            object obj = promoRuleAssebmly.CreateInstance(assemblyName + "." + className);

            if (obj != null)
            {
                status = true;
                lblErrorMsg.Text = "";
            }
        }
        catch { }

        return status;
    }
    #endregion    
}
