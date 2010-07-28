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
using ZNode.Libraries.ECommerce.Catalog;


public partial class Admin_Secure_catalog_product_add_sku : System.Web.UI.Page
{

    # region Protected Member Variables
    protected int ItemID = 0;
    protected int skuID = 0;
    protected int productTypeID = 0;
    protected static int SKUAttributeID = 0;
    protected int ProductImageID = 0;
    protected string viewLink = "~/admin/secure/catalog/product/view.aspx?itemid=";
    # endregion

    # region Page Load

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

        if (Request.Params["skuid"] != null)
        {
            skuID = int.Parse(Request.Params["skuid"].ToString());
        }

        if (Request.Params["typeid"] != null)
        {
            productTypeID = int.Parse(Request.Params["typeid"].ToString());
        }

        this.Bind();

        if (!Page.IsPostBack)
        {
            this.BindBillingFrequency();

            //Bind Supplier 
            this.BindSuppliersTypes();

            ShowSubscriptionPanel();

            if ((ItemID > 0) && (skuID > 0))
            {
                lblHeading.Text = "Edit Product SKU";

                //Bind Sku Details
                this.BindData();
               
                //Bind Attributes
                this.BindAttributes();
                tblShowImage.Visible = true;
            }
            else
            {
                tblSKUDescription.Visible = true;
                lblHeading.Text = "Add Product SKU";
            }

        }
    }
    # endregion

    # region Bind Methods
    /// <summary>
    /// 
    /// </summary>
    private void ShowSubscriptionPanel()
    {
        ProductAdmin prodAdmin = new ProductAdmin();
        Product entity = prodAdmin.GetByProductId(ItemID);

        if (entity != null)
            pnlRecurringBilling.Visible = entity.RecurringBillingInd;
    }

    /// <summary>
    /// Bind control display based on properties set
    /// </summary>
    public void Bind()
    {

        ProductAdmin _adminAccess = new ProductAdmin();

        DataSet MyDataSet = _adminAccess.GetAttributeTypeByProductTypeID(productTypeID);

        //Repeats until Number of AttributeType for this Product
        foreach (DataRow dr in MyDataSet.Tables[0].Rows)
        {
            //Bind Attributes
            DataSet _AttributeDataSet = _adminAccess.GetByAttributeTypeID(int.Parse(dr["attributetypeid"].ToString()));

            System.Web.UI.WebControls.DropDownList lstControl = new DropDownList();
            lstControl.ID = "lstAttribute" + dr["AttributeTypeId"].ToString();

            ListItem li = new ListItem(dr["Name"].ToString(), "0");
            li.Selected = true;

            lstControl.DataSource = _AttributeDataSet;
            lstControl.DataTextField = "Name";
            lstControl.DataValueField = "AttributeId";
            lstControl.DataBind();
            lstControl.Items.Insert(0, li);

            if (!Convert.ToBoolean(dr["IsPrivate"]))
            {
                //Add Dynamic Attribute DropDownlist in the Placeholder
                ControlPlaceHolder.Controls.Add(lstControl);

                //Required Field validator to check SKU Attribute
                RequiredFieldValidator FieldValidator = new RequiredFieldValidator();
                FieldValidator.ID = "Validator" + dr["AttributeTypeId"].ToString();
                FieldValidator.ControlToValidate = "lstAttribute" + dr["AttributeTypeId"].ToString();
                FieldValidator.ErrorMessage = "Select " + dr["Name"].ToString();
                FieldValidator.Display = ValidatorDisplay.Dynamic;
                FieldValidator.CssClass = "Error";
                FieldValidator.InitialValue = "0";

                ControlPlaceHolder.Controls.Add(FieldValidator);
                Literal lit1 = new Literal();
                lit1.Text = "&nbsp;&nbsp;";
                ControlPlaceHolder.Controls.Add(lit1);
            }


        }

        //Hide the Product Attribute
        if (MyDataSet.Tables[0].Rows.Count == 0)
        {
            DivAttributes.Visible = false;
        }


    }


    /// <summary>
    /// Bind Edit Attributes
    /// </summary>
    private void BindAttributes()
    {
        SKUAdmin _adminSKU = new SKUAdmin();
        ProductAdmin _adminAccess = new ProductAdmin();

        DataSet SkuDataSet = _adminSKU.GetBySKUId(skuID);
        DataSet MyDataSet = _adminAccess.GetAttributeTypeByProductTypeID(productTypeID);


        foreach (DataRow dr in MyDataSet.Tables[0].Rows)
        {
            foreach (DataRow Dr in SkuDataSet.Tables[0].Rows)
            {
                System.Web.UI.WebControls.DropDownList lstControl = (System.Web.UI.WebControls.DropDownList)ControlPlaceHolder.FindControl("lstAttribute" + dr["AttributeTypeId"].ToString());

                if (lstControl != null)
                {
                    lstControl.SelectedValue = Dr["Attributeid"].ToString();
                }
            }
        }
    }

    /// <summary>
    /// Binds Edit SKU Datas
    /// </summary>
    private void BindData()
    {
        //Create Instance for SKU Admin and SKU entity
        SKUAdmin skuAdmin = new SKUAdmin();
        SKU skuEntity = skuAdmin.DeepLoadBySKUID(skuID);

        if (skuEntity != null)
        {
            SKU.Text = skuEntity.SKU;
            Quantity.Text = skuEntity.QuantityOnHand.ToString();
            ReOrder.Text = skuEntity.ReorderLevel.ToString();
            WeightAdditional.Text = skuEntity.WeightAdditional.ToString();
            if( skuEntity.RetailPriceOverride.HasValue)
                RetailPrice.Text = skuEntity.RetailPriceOverride.Value.ToString("N");
            if (skuEntity.SalePriceOverride.HasValue)
                SalePrice.Text = skuEntity.SalePriceOverride.Value.ToString("N");
            if (skuEntity.WholesalePriceOverride.HasValue)
                WholeSalePrice.Text = skuEntity.WholesalePriceOverride.Value.ToString("N");
            VisibleInd.Checked = skuEntity.ActiveInd;
            if (ddlSupplier.SelectedIndex != -1)
            {
                ddlSupplier.SelectedValue = skuEntity.SupplierID.ToString();
            }
            if (skuEntity.SKUPicturePath != null)
            {
                SKUImage.ImageUrl = ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.MediumImagePath + skuEntity.SKUPicturePath;
            }
            else
            {
                RadioSKUNoImage.Checked = true;
                RadioSKUCurrentImage.Visible = false;
            }

            txtImageAltTag.Text = skuEntity.ImageAltTag;


            pnlRecurringBilling.Visible = skuEntity.ProductIDSource.RecurringBillingInd;

            if (skuEntity.ProductIDSource.RecurringBillingInd)
            {
                ddlBillingPeriods.SelectedValue = skuEntity.RecurringBillingPeriod;
                ddlBillingFrequency.SelectedValue = skuEntity.RecurringBillingFrequency;
                txtSubscrptionCycles.Text = skuEntity.RecurringBillingTotalCycles.GetValueOrDefault(0).ToString();
            }
        }

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

    # endregion

    # region General Events

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        # region Declarations
        System.IO.FileInfo fileInfo = null;
        SKUAdmin adminAccess = new SKUAdmin();
        SKU skuEntity = new SKU();
        SKUAttribute skuAttribute = new SKUAttribute();
        ProductAdmin prodAdminAccess = new ProductAdmin();
        # endregion


        if (skuID > 0)
        {
            skuEntity = adminAccess.GetBySKUID(skuID);

        }

        // Set Values to SKU
        skuEntity.SKU = SKU.Text.Trim();
        skuEntity.QuantityOnHand = int.Parse(Quantity.Text.Trim());

        if (ReOrder.Text.Trim().Length > 0)
        {
            skuEntity.ReorderLevel = int.Parse(ReOrder.Text.Trim());
        }
        if (WeightAdditional.Text.Trim().Length > 0)
        {
            skuEntity.WeightAdditional = Convert.ToDecimal(WeightAdditional.Text);
        }
        if (RetailPrice.Text.Trim().Length > 0)
        {
            skuEntity.RetailPriceOverride = Convert.ToDecimal(RetailPrice.Text.Trim());
        }
        else { skuEntity.RetailPriceOverride = null; }

        // Set Sale price override property
        if (SalePrice.Text.Trim().Length > 0)
        {
            skuEntity.SalePriceOverride = Convert.ToDecimal(SalePrice.Text.Trim());
        }
        else { skuEntity.SalePriceOverride = null; }

        // Set wholesale price override property
        if (WholeSalePrice.Text.Trim().Length > 0)
        {
            skuEntity.WholesalePriceOverride = decimal.Parse(WholeSalePrice.Text.Trim());
        }
        else { skuEntity.WholesalePriceOverride = null; }

        skuEntity.ProductID = ItemID;
        skuEntity.ActiveInd = VisibleInd.Checked;
        skuEntity.ImageAltTag = txtImageAltTag.Text.Trim();

        #region Image Validation

        if (RadioSKUNoImage.Checked == false)
        {
            //Validate image    
            if ((skuID == 0) || (RadioSKUNewImage.Checked == true))
            {
                if (UploadSKUImage.PostedFile.FileName != "")
                {
                    //Check for Store Image
                    fileInfo = new System.IO.FileInfo(UploadSKUImage.PostedFile.FileName);

                    if (fileInfo != null)
                    {
                        skuEntity.SKUPicturePath = fileInfo.Name;
                    }
                }
            }
            else
            {
                skuEntity.SKUPicturePath = skuEntity.SKUPicturePath;
            }
        }
        else
        {
            skuEntity.SKUPicturePath = null;
        }

        #endregion

        // Upload File if this is a new sku or the New Image option was selected for an existing sku
        if (RadioSKUNewImage.Checked || skuID == 0)
        {
            if (fileInfo != null)
            {
                UploadSKUImage.SaveAs(Server.MapPath(ZNodeConfigManager.EnvironmentConfig.OriginalImagePath + fileInfo.Name));

                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemLargeWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.LargeImagePath));
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemThumbnailWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.ThumbnailImagePath));
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemMediumWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.MediumImagePath));
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemSmallWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.SmallImagePath));
            }
        }

        // Supplier
        if (ddlSupplier.SelectedIndex != -1)
        {
            if (ddlSupplier.SelectedItem.Text.Equals("None"))
            {
               skuEntity.SupplierID = null;
            }
            else
            {
                skuEntity.SupplierID = Convert.ToInt32(ddlSupplier.SelectedValue);
            }
        }

        // Recurring Billing
        skuEntity.RecurringBillingPeriod = ddlBillingPeriods.SelectedItem.Value;
        skuEntity.RecurringBillingFrequency = ddlBillingFrequency.SelectedItem.Text;
        skuEntity.RecurringBillingTotalCycles = int.Parse(txtSubscrptionCycles.Text.Trim());        

        // For Attribute value.
        string Attributes = String.Empty;

        DataSet MyAttributeTypeDataSet = prodAdminAccess.GetAttributeTypeByProductTypeID(productTypeID);

        foreach (DataRow MyDataRow in MyAttributeTypeDataSet.Tables[0].Rows)
        {
            System.Web.UI.WebControls.DropDownList lstControl = (System.Web.UI.WebControls.DropDownList)ControlPlaceHolder.FindControl("lstAttribute" + MyDataRow["AttributeTypeId"].ToString());

            int selValue = int.Parse(lstControl.SelectedValue);

            Attributes += selValue.ToString() + ",";
        }

        // Split the string
        string Attribute = Attributes.Substring(0,Attributes.Length - 1);


        // To check SKU combination is already exists.
        bool RetValue = adminAccess.CheckSKUAttributes(ItemID,skuID,Attribute);


        if (!RetValue)
        {
            bool Check = false;

            //Check For Edit SKu
            if (skuID > 0)
            {
                //set update date for web service download
                skuEntity.UpdateDte = System.DateTime.Now;

                //Update Product SKU
                Check = adminAccess.Update(skuEntity);

                //Delete SKUAttributes
                adminAccess.DeleteBySKUId(skuID);

            }
            else
            {
                //Add New Product SKU and SKUAttribute
                Check = adminAccess.Add(skuEntity, out  skuID);
            }

            foreach (DataRow MyDataRow in MyAttributeTypeDataSet.Tables[0].Rows)
            {
                System.Web.UI.WebControls.DropDownList lstControl = (System.Web.UI.WebControls.DropDownList)ControlPlaceHolder.FindControl("lstAttribute" + MyDataRow["AttributeTypeId"].ToString());

                int selValue = int.Parse(lstControl.SelectedValue);

                if (selValue > 0)
                {
                    skuAttribute.AttributeId = selValue;
                }

                skuAttribute.SKUID = skuID;

                adminAccess.AddSKUAttribute(skuAttribute);

            }

            if (Check)
            {
                //Redirect to View Page
                Response.Redirect(viewLink + ItemID + "&mode=inventory");
            }
            else
            {
                //Do nothing
            }
        }
        else
        {
            lblError.Text = "* This Attribute combination already exists for this product. Please select different combination.";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Redirect to View Page
        Response.Redirect(viewLink + ItemID + "&mode=inventory");

    }   

    /// <summary>
    /// Radio Button Check Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioSKUCurrentImage_CheckedChanged(object sender, EventArgs e)
    {
        tblSKUDescription.Visible = false;
        SKUImage.Visible = true;
    }

    /// <summary>
    /// Radio Button Check Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioSKUNoImage_CheckedChanged(object sender, EventArgs e)
    {
        tblSKUDescription.Visible = false;
        SKUImage.Visible = false;
    }

    /// <summary>
    /// Radio Button Check Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioSKUNewImage_CheckedChanged(object sender, EventArgs e)
    {
        tblSKUDescription.Visible = true;
        SKUImage.Visible = true;
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
