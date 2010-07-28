using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Custom;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.ECommerce.Catalog;
using SCommImaging.Imaging;
using System.Drawing.Imaging;
using System.Drawing;
//using Dart.PowerWEB.TextBox;


public partial class Admin_Secure_products_add : System.Web.UI.Page
{
    #region protected Member Variables
    protected string CancelLink = "view.aspx?itemid=";
    protected string ListLink = "list.aspx";
    protected string ProductImageName = "";
    protected int ItemID;    
    protected int SKUId = 0;
    protected int _ProductID = 0;
    protected StringBuilder SelectedNodes=null;
    protected StringBuilder DeleteNodes = null;
    protected int ProductTypeId= 0;
    #endregion

    # region Page load

    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        
        SelectedNodes = new StringBuilder();
        DeleteNodes = new StringBuilder();
        
        //Get product id value from query string
        if (Request.Params["itemid"] != null)
        {
           
            ItemID = int.Parse(Request.Params["itemid"].ToString());
        }
        else
        {
            ItemID = 0;
        }
        
        if (!Page.IsPostBack)
        {
            //Dynamic error message for weight field compare validator
            WeightFieldCompareValidator.ErrorMessage = "You must enter a valid product Weight (Ex:" + (2.5).ToString() + ")";

            // Bind master templates
            BindMasterPageTemplates();

            //Bind shipping options
            BindShippingTypes();

            //if edit func then bind the data fields
            if (ItemID > 0)
            {
                lblTitle.Text = "Edit Product - ";
                //txtimagename.Enabled = false;
                this.BindData();
                this.BindEditData();
                tblShowImage.Visible = true;
                pnlImageName.Visible = true;
                
            }
            else
            {
                lblTitle.Text = "Add New Product";
                this.BindData();
                tblProductDescription.Visible = true;
                
            }
        }        

        //Bind SKU Attributes Dynamically
        if (ProductTypeList.SelectedIndex != -1)
        {            
            this.Bind(int.Parse(ProductTypeList.SelectedValue));

            if (ItemID > 0)
            {
                //Bind Edit SKU Attributes
                this.BindAttributes(int.Parse(ProductTypeList.SelectedValue));
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(),"Error", "<script>answer = confirm('Please add a product type before you add a new product'); if(answer != '0'){ location='list.aspx' } </script>");
            
        }
    }
    # endregion
    
    # region Bind Data
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

    public void BindData()
    { 
        //Bind product Type
        ProductTypeAdmin productTypeAdmin = new ProductTypeAdmin();
                     
        ProductTypeList.DataSource = productTypeAdmin.GetAllProductTypes(ZNodeConfigManager.SiteConfig.PortalID);
        ProductTypeList.DataTextField = "name";
        ProductTypeList.DataValueField = "productTypeid";
        ProductTypeList.DataBind();
             
        //Bind Manufacturer
        ManufacturerAdmin ManufacturerAdmin = new ManufacturerAdmin();
        ManufacturerList.DataSource=ManufacturerAdmin.GetAllByPortalID(ZNodeConfigManager.SiteConfig.PortalID);
        ManufacturerList.DataTextField = "name";
        ManufacturerList.DataValueField = "manufacturerid";
        ManufacturerList.DataBind();
        ListItem li = new ListItem("No Manufacturer Selected", "0");
        ManufacturerList.Items.Insert(0,li);

        //Bind Supplier
        SupplierService serv = new SupplierService();
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
        
        //Bind Categories
        this.BindTreeViewCategory();

        // Bind Tax Class
        TaxRuleAdmin TaxRuleAdmin = new TaxRuleAdmin();
        ddlTaxClass.DataSource = TaxRuleAdmin.GetAllTaxClass();
        ddlTaxClass.DataTextField = "name";
        ddlTaxClass.DataValueField = "TaxClassID";
        ddlTaxClass.DataBind();
        
    }

    /// <summary>
    /// Bind TreeView with Categories
    /// </summary>
    public void BindTreeViewCategory()
    {
      this.PopulateAdminTreeView(string.Empty);
    }

   
    /// <summary>
    /// Bind control display based on properties set-Dynamically adds DropDownList for Each AttributeType
    /// </summary>
    public void Bind(int productTypeID)
    {
        ProductTypeId = productTypeID;

        //Bind Attributes
        ProductAdmin adminAccess = new ProductAdmin();

        DataSet MyDataSet = adminAccess.GetAttributeTypeByProductTypeID(productTypeID);

        //Repeat until Number of Attributetypes for this Product
        foreach (DataRow dr in MyDataSet.Tables[0].Rows)
        {
            //Get all the Attribute for this Attribute
            DataSet _AttributeDataSet = new DataSet();
            _AttributeDataSet = adminAccess.GetByAttributeTypeID(int.Parse(dr["attributetypeid"].ToString()));
            DataView dv = new DataView(_AttributeDataSet.Tables[0]);
            //Create Instance for the DropDownlist
            System.Web.UI.WebControls.DropDownList lstControl = new DropDownList();
            lstControl.ID = "lstAttribute" + dr["AttributeTypeId"].ToString();

            ListItem li = new ListItem(dr["Name"].ToString(), "0");
            li.Selected = true;
            dv.Sort = "DisplayOrder ASC";
            lstControl.DataSource = dv;//_AttributeDataSet;
            lstControl.DataTextField = "Name";
            lstControl.DataValueField = "AttributeId";
            lstControl.DataBind();
            lstControl.Items.Insert(0, li);

            if (!Convert.ToBoolean(dr["IsPrivate"]))
            {
                //Add the Control to Place Holder
                ControlPlaceHolder.Controls.Add(lstControl);

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

        if (MyDataSet.Tables[0].Rows.Count == 0)
        {
            DivAttributes.Visible = false;
            pnlquantity.Visible = true;
            pnlSupplier.Visible = true;
        }
        else
        {
            DivAttributes.Visible = true;
            pnlquantity.Visible = false;
            pnlSupplier.Visible = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void BindMasterPageTemplates()
    {
        // Create instance for direcoryInfo and specify the directory 'MasterPages/Product'
        DirectoryInfo directoryInfo = new DirectoryInfo(Server.MapPath(ZNodeConfigManager.EnvironmentConfig.DataPath + "MasterPages/Product/"));

        // Determine whether the directory 'MasterPages/Product' exists.
		if(directoryInfo.Exists)
		{
            // Returns a master file list from the current directory.
	        FileInfo[] masterFiles = directoryInfo.GetFiles("*.master");

	        foreach (FileInfo masterPage in masterFiles)
	        {
	            string fileName = masterPage.Name;
	            fileName = fileName.Replace(".master", string.Empty); // Name only

	            ddlPageTemplateList.Items.Add(fileName);
	        }
		}
		
        // Default master template
        ddlPageTemplateList.Items.Insert(0, "Default");
    }

    
    

    # endregion       

    # region Bind Edit Data
    /// <summary>
    /// Bind value for Particular Product
    /// </summary>
    public void BindEditData()
    {
        ProductAdmin productAdmin = new ProductAdmin();
        Product product = new Product();
        SKUAdmin skuAdmin = new SKUAdmin();

        if (ItemID > 0)
        {
            product = productAdmin.GetByProductId(ItemID);

            // General Information - Section1
			lblTitle.Text += product.Name;
            txtProductName.Text = product.Name;           
            txtProductNum.Text = product.ProductNum;
            if (ProductTypeList.SelectedIndex != -1)
            {
                ProductTypeList.SelectedValue = product.ProductTypeID.ToString();
            }
            if (ManufacturerList.SelectedIndex != -1)
            {
                ManufacturerList.SelectedValue = product.ManufacturerID.ToString();
            }

            if (ddlSupplier.SelectedIndex != -1)
            {
                ddlSupplier.SelectedValue = product.SupplierID.ToString();
            }
                    

            // Product Description and Image - Section2
            txtshortdescription.Text = product.ShortDescription;
            ctrlHtmlText.Html = product.Description;
            Image1.ImageUrl = ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.MediumImagePath + product.ImageFile; 
            // Displaying the Image file name in a textbox           
            txtimagename.Text = product.ImageFile;
            txtImageAltTag.Text = product.ImageAltTag;

            // Product properties
            if(product.RetailPrice.HasValue)
                txtproductRetailPrice.Text = product.RetailPrice.Value.ToString("N");
            if (product.SalePrice.HasValue)
                txtproductSalePrice.Text = product.SalePrice.Value.ToString("N");
            if (product.WholesalePrice.HasValue)
                txtProductWholeSalePrice.Text = product.WholesalePrice.Value.ToString("N");
            txtDownloadLink.Text = product.DownloadLink;
            
            // Tax Classes
            if(product.TaxClassID.HasValue)
                ddlTaxClass.SelectedValue = product.TaxClassID.Value.ToString();
            
            // Category List
            this.BindEditCategoryList();

            //Display Settings
            txtDisplayOrder.Text = product.DisplayOrder.ToString();
            ddlPageTemplateList.SelectedValue = product.MasterPage;

            // Inventory
            DataSet MySKUDataSet = skuAdmin.GetByProductID(ItemID);
            if (MySKUDataSet.Tables[0].Rows.Count != 0)
            {
                ViewState["productSkuId"] = MySKUDataSet.Tables[0].Rows[0]["skuid"].ToString();
                txtProductSKU.Text = MySKUDataSet.Tables[0].Rows[0]["sku"].ToString();
                txtProductQuantity.Text = MySKUDataSet.Tables[0].Rows[0]["quantityonhand"].ToString();

                this.BindAttributes(product.ProductTypeID);
            }
            else
            {
                if (product.QuantityOnHand.HasValue)
                {
                    txtProductQuantity.Text = product.QuantityOnHand.Value.ToString();
                }
                if (product.ReorderLevel.HasValue)
                {
                    txtReOrder.Text = product.ReorderLevel.Value.ToString();
                }               
                txtProductSKU.Text = product.SKU;
            }

            if (product.MaxQty.HasValue)
            {
                txtMaxQuantity.Text = product.MaxQty.Value.ToString();
            }

            // Shipping settings
            ShippingTypeList.SelectedValue = product.ShippingRuleTypeID.ToString();
            EnableValidators(); //This method will enable or disable validators based on the shippingtype
            if(product.FreeShippingInd.HasValue)
                chkFreeShippingInd.Checked = product.FreeShippingInd.Value;
            
            chkShipSeparately.Checked = product.ShipSeparately;

            if (product.Weight.HasValue)
                txtProductWeight.Text = product.Weight.Value.ToString("N2");

            if (product.Height.HasValue)
                txtProductHeight.Text = product.Height.Value.ToString("N2");

            if (product.Width.HasValue)
                txtProductWidth.Text = product.Width.Value.ToString("N2");

            if (product.Length.HasValue)
                txtProductLength.Text = product.Length.Value.ToString("N2");

            // Release the Resources
            MySKUDataSet.Dispose();
        }

    }

    /// <summary>
    /// Binds the Sku Attributes for this Product
    /// </summary>
    /// <param name="productTypeID"></param>
    private void BindAttributes(int productTypeID)
    {
        SKUAdmin adminSKU = new SKUAdmin();
        ProductAdmin adminAccess = new ProductAdmin();

        if (ViewState["productSkuId"] != null)
        {
            //Get SKUID from the ViewState
            DataSet SkuDataSet = adminSKU.GetBySKUId(int.Parse(ViewState["productSkuId"].ToString()));
            DataSet MyDataSet = adminAccess.GetAttributeTypeByProductTypeID(productTypeID);


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

    }


    /// <summary>
    /// Check the Category to the particular Product
    /// </summary>
    public void BindEditCategoryList()
    {        
          ProductCategoryAdmin productCategoryAdmin = new ProductCategoryAdmin();

          if (productCategoryAdmin != null)
          {
              DataSet MyDataset = productCategoryAdmin.GetByProductID(ItemID);
              foreach (DataRow dr in MyDataset.Tables[0].Rows)
              {
                  this.PopulateEditTreeView(dr["categoryid"].ToString());
              }
          }
       
    }

    # endregion

    # region General Events

    /// <summary>
    /// Submit Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {   
        #region Declarations
        SKUAdmin skuAdminAccess = new SKUAdmin();
        ProductAdmin productAdmin = new ProductAdmin();
        SKUAttribute skuAttribute = new SKUAttribute();
        SKU sku = new SKU();
        ProductCategory productCategory = new ProductCategory();
        ProductCategoryAdmin  productCategoryAdmin=new ProductCategoryAdmin();
        Product product = new Product(); 
        System.IO.FileInfo fileInfo=null;
        bool retVal = false;        
             

        //check if category was selected
        if (CategoryTreeView.CheckedNodes.Count > 0)
        {
            lblCategoryError.Visible = false;
        }
        else
        {
            lblCategoryError.Visible = true;
            return;
        }

        #endregion

        #region Set Product Properties

        //passing Values 
        product.ProductID = ItemID;
        product.PortalID = ZNodeConfigManager.SiteConfig.PortalID;

        //if edit mode then get all the values first
        if (ItemID > 0)
        {
            product = productAdmin.GetByProductId(ItemID);
            
            if (ViewState["productSkuId"] != null)
            {
                sku.SKUID = int.Parse(ViewState["productSkuId"].ToString());
            }
        }

        //General Info
        product.Name = txtProductName.Text;
        product.ImageFile = txtimagename.Text;
        product.ProductNum = txtProductNum.Text;
        product.PortalID = ZNodeConfigManager.SiteConfig.PortalID;
        
        if (ProductTypeList.SelectedIndex != -1)
        {
            product.ProductTypeID = Convert.ToInt32(ProductTypeList.SelectedValue);
        }
        else
        {
            //"Please add a product type before you add a new product";
        }
        //MANUFACTURER
        if (ManufacturerList.SelectedIndex != -1)
        {
            if (ManufacturerList.SelectedItem.Text.Equals("No Manufacturer Selected"))
            {
                product.ManufacturerID = null;
            }
            else
            {
                product.ManufacturerID = Convert.ToInt32(ManufacturerList.SelectedValue);
            }
        }

        //Supplier
        if (ddlSupplier.SelectedIndex != -1)
        {
            if (ddlSupplier.SelectedItem.Text.Equals("None"))
            {
                product.SupplierID = null;
            }
            else
            {
                product.SupplierID = Convert.ToInt32(ddlSupplier.SelectedValue);
            }
        }

        product.DownloadLink = txtDownloadLink.Text.Trim();
        product.ShortDescription = txtshortdescription.Text;
        product.Description = ctrlHtmlText.Html;                           
        product.RetailPrice = Convert.ToDecimal(txtproductRetailPrice.Text);

        if (txtproductSalePrice.Text.Trim().Length > 0)
        {
            product.SalePrice = Convert.ToDecimal(txtproductSalePrice.Text.Trim());
        }
        else { product.SalePrice = null; }

        if (txtProductWholeSalePrice.Text.Trim().Length > 0)
        {
            product.WholesalePrice = Convert.ToDecimal(txtProductWholeSalePrice.Text.Trim());
        }
        else { product.WholesalePrice = null; }

        //Quantity Available
        product.QuantityOnHand = Convert.ToInt32(txtProductQuantity.Text);
        if (txtReOrder.Text.Trim().Length > 0)
        {
            product.ReorderLevel = Convert.ToInt32(txtReOrder.Text);
        }
        else
        {
            product.ReorderLevel = null;
        }
        if(txtMaxQuantity.Text.Equals(""))
        {
            product.MaxQty = 10;
        }
        else
        {
            product.MaxQty = Convert.ToInt32(txtMaxQuantity.Text);
        }

        // Display Settings
        product.MasterPage = ddlPageTemplateList.SelectedItem.Text;
        product.DisplayOrder = int.Parse(txtDisplayOrder.Text.Trim());        

        // Tax Settings        
        if(ddlTaxClass.SelectedIndex != -1)
            product.TaxClassID = int.Parse(ddlTaxClass.SelectedValue);

        //Shipping Option setting
        product.ShippingRuleTypeID = Convert.ToInt32(ShippingTypeList.SelectedValue);
        product.FreeShippingInd = chkFreeShippingInd.Checked;
        product.ShipSeparately = chkShipSeparately.Checked;

        if (txtProductWeight.Text.Trim().Length > 0)
        {
            product.Weight = Convert.ToDecimal(txtProductWeight.Text.Trim());
        }
        else { product.Weight = null; }

        //Product Height - Which will be used to determine the shipping cost
        if (txtProductHeight.Text.Trim().Length > 0)
        {
            product.Height = decimal.Parse(txtProductHeight.Text.Trim());
        }
        else { product.Height = null; }

        if (txtProductWidth.Text.Trim().Length > 0)
        {
            product.Width = decimal.Parse(txtProductWidth.Text.Trim());
        }
        else { product.Width = null; }

        if (txtProductLength.Text.Trim().Length > 0)
        {
            product.Length = decimal.Parse(txtProductLength.Text.Trim());
        }
        else { product.Length = null; }

        //Stock
        DataSet MyAttributeTypeDataSet = productAdmin.GetAttributeTypeByProductTypeID(int.Parse(ProductTypeList.SelectedValue));
        if (MyAttributeTypeDataSet.Tables[0].Rows.Count == 0)
        {
            product.SKU = txtProductSKU.Text.Trim();
            product.QuantityOnHand = Convert.ToInt32(txtProductQuantity.Text);
        }
        else
        {
            //SKU         
            sku.ProductID = ItemID;
            sku.QuantityOnHand = Convert.ToInt32(txtProductQuantity.Text);
            sku.SKU = txtProductSKU.Text.Trim();
            sku.ActiveInd = true;
            product.SKU = txtProductSKU.Text.Trim();
            product.QuantityOnHand = 0; //Reset quantity available in the Product table,If SKU is selected
        }
        
        product.ImageAltTag = txtImageAltTag.Text.Trim();
        #endregion

        #region Image Validation

        // Validate image
        if ((ItemID == 0 || RadioProductNewImage.Checked == true) && UploadProductImage.PostedFile.FileName.Length > 0)
        {
            //Check for Product Image
            fileInfo = new System.IO.FileInfo(UploadProductImage.PostedFile.FileName);

            if (fileInfo != null)
            {              
              product.ImageFile = fileInfo.Name;
              sku.SKUPicturePath = fileInfo.Name; 
            }  
        }
        #endregion

        #region Database & Image Updates

        //set update date
        product.UpdateDte = System.DateTime.Now;

        //create transaction
        TransactionManager tranManager = ConnectionScope.CreateTransaction();

        try
        {
            if (ItemID > 0) //PRODUCT UPDATE
            {
                //Update product Sku and Product values
                if (MyAttributeTypeDataSet.Tables[0].Rows.Count > 0) //If ProductType has SKU's
                {
                    if (sku.SKUID > 0) //For this product already SKU if on exists
                    {
                        sku.UpdateDte = System.DateTime.Now;

                        // Check whether Duplicate attributes is created
                        string Attributes = String.Empty;

                        DataSet MyAttributeTypeDataSet1 = productAdmin.GetAttributeTypeByProductTypeID(ProductTypeId);

                        foreach (DataRow MyDataRow in MyAttributeTypeDataSet1.Tables[0].Rows)
                        {
                            System.Web.UI.WebControls.DropDownList lstControl = (System.Web.UI.WebControls.DropDownList)ControlPlaceHolder.FindControl("lstAttribute" + MyDataRow["AttributeTypeId"].ToString());

                            int selValue = int.Parse(lstControl.SelectedValue);

                            Attributes += selValue.ToString() + ",";
                        }

                        // Split the string
                        string Attribute = Attributes.Substring(0, Attributes.Length - 1);


                        // To check SKU combination is already exists.
                        bool RetValue = skuAdminAccess.CheckSKUAttributes(ItemID, sku.SKUID, Attribute);

                        if (!RetValue)
                        {
                            //then Update the database with new property values
                            retVal = productAdmin.Update(product, sku);
                        }
                        else
                        {
                            //Throw error if duplicate attribute
                            lblMsg.Text = "This Attribute combination already exists for this product. Please select different combination.";
                            return;
                        }
                    }
                    else
                    {
                        retVal = productAdmin.Update(product);
                        //If Product doesn't have any SKUs yet,then create new SKU in the database
                        skuAdminAccess.Add(sku);
                    }
                }
                else
                {
                    retVal = productAdmin.Update(product);
                    // If User selectes Default product type for this product,
                    // then Remove all the existing SKUs for this product
                    skuAdminAccess.DeleteByProductId(ItemID);
                }

                if (!retVal) { throw (new ApplicationException()); }

                // Delete existing categories
                productAdmin.DeleteProductCategories(ItemID);

                // Add Product Categories
                foreach (TreeNode Node in CategoryTreeView.CheckedNodes)
                {
                    ProductCategory prodCategory = new ProductCategory();
                    ProductAdmin prodAdmin = new ProductAdmin();

                    prodCategory.CategoryID = int.Parse(Node.Value);
                    prodCategory.ProductID = ItemID;
                    prodAdmin.AddProductCategory(prodCategory);
                }

                // Delete existing SKUAttributes
                skuAdminAccess.DeleteBySKUId(sku.SKUID);

                // Add SKU Attributes                
                foreach (DataRow MyDataRow in MyAttributeTypeDataSet.Tables[0].Rows)
                {
                    System.Web.UI.WebControls.DropDownList lstControl = (System.Web.UI.WebControls.DropDownList)ControlPlaceHolder.FindControl("lstAttribute" + MyDataRow["AttributeTypeId"].ToString());

                    int selValue = int.Parse(lstControl.SelectedValue);

                    if (selValue > 0)
                    {
                        skuAttribute.AttributeId = selValue;
                    }

                    skuAttribute.SKUID = sku.SKUID;

                    skuAdminAccess.AddSKUAttribute(skuAttribute);


                }


            }
            else // PRODUCT ADD
            {
                product.ActiveInd = true;

                // Add Product/SKU
                if (MyAttributeTypeDataSet.Tables[0].Rows.Count > 0)
                {
                    //if ProductType has SKUs, then insert sku with Product
                    retVal = productAdmin.Add(product, sku, out _ProductID, out SKUId);
                }
                else
                {
                    retVal = productAdmin.Add(product, out _ProductID); //if ProductType is Default
                }

                if (!retVal) { throw (new ApplicationException()); }

                // Add Category List for the Product
                foreach (TreeNode Node in CategoryTreeView.CheckedNodes)
                {
                    ProductCategory prodCategory = new ProductCategory();
                    ProductAdmin prodAdmin = new ProductAdmin();

                    prodCategory.CategoryID = int.Parse(Node.Value);
                    prodCategory.ProductID = _ProductID;
                    prodAdmin.AddProductCategory(prodCategory);
                }


                // Add SKU Attributes
                foreach (DataRow MyDataRow in MyAttributeTypeDataSet.Tables[0].Rows)
                {
                    System.Web.UI.WebControls.DropDownList lstControl = (System.Web.UI.WebControls.DropDownList)ControlPlaceHolder.FindControl("lstAttribute" + MyDataRow["AttributeTypeId"].ToString());

                    int selValue = int.Parse(lstControl.SelectedValue);

                    if (selValue > 0)
                    {
                        skuAttribute.AttributeId = selValue;
                    }

                    skuAttribute.SKUID = SKUId;

                    skuAdminAccess.AddSKUAttribute(skuAttribute);
                }


                ZNode.Libraries.Admin.ProductViewAdmin imageAdmin = new ProductViewAdmin();
                ZNode.Libraries.DataAccess.Entities.ProductImage productImage = new ProductImage();

                productImage.Name = txtimagename.Text;
                productImage.ActiveInd = false;
                productImage.ShowOnCategoryPage = false;
                productImage.ProductID = _ProductID;
                productImage.ProductImageTypeID = 1;
                productImage.DisplayOrder = 500;
                productImage.ImageAltTag = txtImageAltTag.Text.Trim();
                productImage.AlternateThumbnailImageFile = txtImageAltTag.Text.Trim();

                if (fileInfo != null)
                {
                    productImage.ImageFile = fileInfo.Name;
                }

                imageAdmin.Insert(productImage);
            }
       
            // Commit transaction
            tranManager.Commit();
        }
        catch // error occurred so rollback transaction
        {
            if (tranManager.IsOpen)
                tranManager.Rollback();

            lblMsg.Text = "Unable to update product. Please try again.";
            return;
        }

        // Upload File if this is a new product or the New Image option was selected for an existing product
        if (RadioProductNewImage.Checked || ItemID == 0)
        {
            if (fileInfo != null)
            {
                UploadProductImage.SaveAs(Server.MapPath(ZNodeConfigManager.EnvironmentConfig.OriginalImagePath + fileInfo.Name));

                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemLargeWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.LargeImagePath));
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemThumbnailWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.ThumbnailImagePath));
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemMediumWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.MediumImagePath));
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemSmallWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.SmallImagePath));
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemSwatchWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.SwatchImagePath));
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemCrossSellWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.CrossSellImagePath));
            }
        }  

        #endregion     

        #region Redirect to next page
        //Redirect to next page
        if (ItemID > 0)
        {
            string ViewLink = "~/admin/secure/catalog/product/view.aspx?itemid=" + ItemID.ToString();
            Response.Redirect(ViewLink);
        }
        else
        {
            string NextLink = "~/admin/secure/catalog/product/view.aspx?itemid=" + _ProductID.ToString();
            Response.Redirect(NextLink);
        }
        #endregion
    }

    /// <summary> 
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (ItemID > 0)
        {
            Response.Redirect(CancelLink + ItemID);
        }
        else
        {
            Response.Redirect(ListLink);
        }
    }

    /// <summary>
    /// Radio Button Check Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioProductCurrentImage_CheckedChanged(object sender, EventArgs e)
    {
        tblProductDescription.Visible = false;
    }

    /// <summary>
    /// Radio Button Check Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioProductNewImage_CheckedChanged(object sender, EventArgs e)
    {
        tblProductDescription.Visible = true;
    }

    /// <summary>
    /// Shipping rule type selected index change event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ShippingTypeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        EnableValidators();
    }
    # endregion

    # region Helper Methods
    
    /// <summary>
    /// Returns a Format Weight string
    /// </summary>
    /// <param name="FieldValue"></param>
    /// <returns></returns>
    public string FormatNull(Object FieldValue)
    {
        if (FieldValue == null)
        {
            return String.Empty;

        }
        else
        {
            if (Convert.ToDecimal(FieldValue.ToString()) == 0)
            {
                return string.Empty;
            }
            else
            {
                return FieldValue.ToString();
            }
        }
    }

    /// <summary>
    /// Format Retail and Wholesale price
    /// </summary>
    /// <param name="FieldValue"></param>
    /// <returns></returns>
    public string FormatPrice(Object FieldValue)
    {
        if (FieldValue == null)
        {
            return String.Empty;

        }
        else
        {
            if (FieldValue.ToString().Equals("0"))
            {
                return String.Empty;
            }
            else
            {
                return ((String.Format("{0:c}", FieldValue)).Substring(1).ToString());
            }
            
        }
    }

    /// <summary>
    /// Populates a treeview with store categories for a Product
    /// </summary>
    /// <param name="selectedCategoryId"></param>
    private void PopulateEditTreeView(string selectedCategoryId)
    {
        foreach (TreeNode Tn in CategoryTreeView.Nodes)
        {
            if (Tn.Value.Equals(selectedCategoryId))
            {
                Tn.Checked  = true;
            }
            RecursivelyEditPopulateTreeView(Tn, selectedCategoryId);
        }
    }

    /// <summary>
    /// Recursively populate a particular node with it's children for a product
    /// </summary>
    /// <param name="ChildNodes"></param>
    /// <param name="selectedCategoryid"></param>
    private void RecursivelyEditPopulateTreeView(TreeNode ChildNodes, string selectedCategoryid)
    {
        foreach (TreeNode CNodes in ChildNodes.ChildNodes)
        {
            if (CNodes.Value.Equals(selectedCategoryid))
            {
                CNodes.Checked  = true;
                RecursivelyExpandTreeView(CNodes);
            }

            RecursivelyEditPopulateTreeView(CNodes, selectedCategoryid);
        }
    }

    /// <summary>
    /// Populates a treeview with store categories
    /// </summary>
    /// <param name="ctrlTreeView"></param>
    public void PopulateAdminTreeView(string selectedCategoryId)
    {
        CategoryHelper categoryHelper = new CategoryHelper();
        System.Data.DataSet ds = categoryHelper.GetNavigationItems(ZNodeConfigManager.SiteConfig.PortalID);

        //add the hierarchical relationship to the dataset
        ds.Relations.Add("NodeRelation", ds.Tables[0].Columns["CategoryId"], ds.Tables[0].Columns["ParentCategoryId"]);

        foreach (DataRow dbRow in ds.Tables[0].Rows)
        {
            if (dbRow.IsNull("ParentCategoryID"))
            {
                if ((bool)dbRow["VisibleInd"])
                {
                    //create new tree node
                    TreeNode tn = new TreeNode();
                    tn.Text = dbRow["Name"].ToString();
                    tn.Value = dbRow["categoryid"].ToString();

                    string categoryId = dbRow["CategoryId"].ToString();

                    //Add Root Node to Category Tree view
                    CategoryTreeView.Nodes.Add(tn);

                    if (selectedCategoryId.Equals(categoryId))
                    {
                        tn.Selected = true;
                        RecursivelyExpandTreeView(tn);
                    }

                    RecursivelyPopulateTreeView(dbRow, tn, selectedCategoryId);
                }
            }

        }
    }

    
    /// <summary>
    /// Recursively populate a particular node with it's children
    /// </summary>
    /// <param name="dbRow"></param>
    /// <param name="parentNode"></param>
    /// <param name="selectedCategoryId"></param>
    private void RecursivelyPopulateTreeView(DataRow dbRow, TreeNode parentNode, string selectedCategoryId)
    {
        foreach (DataRow childRow in dbRow.GetChildRows("NodeRelation"))
        {
            if ((bool)childRow["VisibleInd"])
            {
                //create new tree node
                TreeNode tn = new TreeNode();
                tn.Text = childRow["Name"].ToString();
                tn.Value = childRow["categoryid"].ToString();

                string categoryId = childRow["CategoryId"].ToString();

                parentNode.ChildNodes.Add(tn);

                if (selectedCategoryId.Equals(categoryId))
                {
                    tn.Selected = true;
                    RecursivelyExpandTreeView(tn);
                }

                RecursivelyPopulateTreeView(childRow, tn, selectedCategoryId);
            }
        }

    }

    /// <summary>
    /// Recursively expands parent nodes of a selected tree node
    /// </summary>
    /// <param name="nodeItem"></param>
    private void RecursivelyExpandTreeView(TreeNode nodeItem)
    {
        if (nodeItem.Parent != null)
        {
            nodeItem.Parent.ExpandAll();
            RecursivelyExpandTreeView(nodeItem.Parent);
        }
        else
        {
            nodeItem.Expand();
            return;
        }
    }

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
   # endregion    
    
}
