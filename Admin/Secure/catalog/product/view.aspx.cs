using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.Framework.Business;


public partial class Admin_Secure_products_view : System.Web.UI.Page
{

    #region Protected Member Variables
    protected int ItemId;    
    protected int PortalID;
    protected string Mode = "";
    protected static int productTypeID = 0;
    protected int ProductID;    
    protected string EditPageLink = "add.aspx?itemid=";
    protected string EditSEOLink = "edit_seo.aspx?itemid=";
    protected string EditAdvancedPageLink = "edit_advancedsettings.aspx?itemid=";    
    protected string EditAdditionalInfoLink = "edit_additionalinfo.aspx?itemid=";
    protected string InventoryLink = "inventory.aspx?itemid=";
    protected string AddRelatedItemLink = "addrelateditems.aspx?itemid=";
    protected string AddTieredPricingPageLink = "add_TieredPricing.aspx?itemid=";
    protected string AddDigitalAssetPageLink = "~/admin/secure/catalog/DigitalAsset/add.aspx?itemid=";
    protected string AddProductAddOnLink = "~/admin/secure/catalog/product/add_addons.aspx?";
    protected string ListLink = "~/admin/secure/catalog/product/list.aspx";
    protected string PreviewLink = "/product.aspx?zpid=";
    protected string AddSKULink = "~/admin/secure/catalog/product/add_sku.aspx?itemid=";
    protected string AddViewlink = "~/admin/secure/catalog/product/add_view.aspx?itemid=";
    protected string DetailsLink = "~/admin/secure/catalog/product_addons/view.aspx?mode=true";
    protected string AddHighlightPageLink = "~/admin/secure/catalog/product/add_highlights.aspx?itemid=";
    protected string EditHighlightPageLink = "~/admin/secure/catalog/product_Highlights/add.aspx?itemid=";

    #endregion

    #region Page Load
    
    protected void Page_Load(object sender, EventArgs e)
     {       

        // Get mode value from querystring        
        if (Request.Params["mode"] != null)
        {
            Mode = Request.Params["mode"];
        }

        // Get ItemId from querystring        
        if (Request.Params["itemid"] != null)
        {
            ItemId = int.Parse(Request.Params["itemid"]);
        }
        else
        {
            ItemId = 0;
        }

        if (!Page.IsPostBack)
        {
            if (ItemId > 0)
            {
                this.BindViewData();
                ZNodeUrl _Url = new ZNodeUrl();
                PreviewLink = ZNodeConfigManager.EnvironmentConfig.ApplicationPath + "/product.aspx?zpid=" + ItemId;
            }
            else 
            {
                throw (new ApplicationException("Product Requested could not be found."));                
            }


            ResetTab();
        }
        //Add Client Side Script
        StringBuilder StringBuild = new StringBuilder();
        StringBuild.Append("<script language=JavaScript>");
        StringBuild.Append("    function  PreviewProduct() {");
        StringBuild.Append("  window.open('" + PreviewLink + "');");
        StringBuild.Append("    }");
        StringBuild.Append("<" + "/script>");


        if (!ClientScript.IsStartupScriptRegistered("Preview"))
        {
            ClientScript.RegisterStartupScript(GetType(),"Preview", StringBuild.ToString());
        }
       

        //Bind the Attribute List
        BindSkuAttributes();
    }
    #endregion

    #region Bind Methods
    /// <summary>
    /// Bind Attributes List.
    /// </summary>
    protected void BindSkuAttributes()
    {
        ProductAdmin adminAccess = new ProductAdmin();

        DataSet MyDataSet = adminAccess.GetAttributeTypeByProductTypeID(productTypeID);

        //Repeats until Number of AttributeType for this Product
        foreach (DataRow dr in MyDataSet.Tables[0].Rows)
        {
            //Bind Attributes
            DataSet AttributeDataSet = adminAccess.GetAttributesByAttributeTypeIdandProductID(int.Parse(dr["attributetypeid"].ToString()),ItemId);

            System.Web.UI.WebControls.DropDownList lstControl = new DropDownList();
            lstControl.ID = "lstAttribute" + dr["AttributeTypeId"].ToString();

            ListItem li = new ListItem(dr["Name"].ToString(), "0");
            li.Selected = true;

            lstControl.DataSource = AttributeDataSet;
            lstControl.DataTextField = "Name";
            lstControl.DataValueField = "AttributeId";
            lstControl.DataBind();
            lstControl.Items.Insert(0, li);            

            //Add Dynamic Attribute DropDownlist in the Placeholder
            ControlPlaceHolder.Controls.Add(lstControl);

            Literal lit1 = new Literal();
            lit1.Text = "&nbsp;&nbsp;";
            ControlPlaceHolder.Controls.Add(lit1);
        }       
    }

    /// <summary>
    /// Bind Inventory Grid
    /// </summary>
    private void BindSKU()
    {
        SKUAdmin SkuAdmin = new SKUAdmin();
        DataSet MyDatas = SkuAdmin.GetByProductID(ItemId);

        DataView Sku = new DataView(MyDatas.Tables[0]);
        Sku.Sort = "SKU";
        uxGridInventoryDisplay.DataSource = Sku;
        uxGridInventoryDisplay.DataBind();        
    }

    /// <summary>
    /// Binds the Search data
    /// </summary>
    private void BindSearchData()
    {
        ProductAdmin adminAccess = new ProductAdmin();
        DataSet ds = adminAccess.GetProductDetails(ItemId);

        //Check for Number of Rows
        if (ds.Tables[0].Rows.Count != 0)
        {
            //Check For Product Type
            productTypeID = int.Parse(ds.Tables[0].Rows[0]["ProductTypeId"].ToString());
        }

        // For Attribute value.
        string Attributes = String.Empty;

        //GetAttribute for this ProductType
        DataSet MyAttributeTypeDataSet = adminAccess.GetAttributeTypeByProductTypeID(productTypeID);

        foreach (DataRow MyDataRow in MyAttributeTypeDataSet.Tables[0].Rows)
        {
            System.Web.UI.WebControls.DropDownList lstControl = (System.Web.UI.WebControls.DropDownList)ControlPlaceHolder.FindControl("lstAttribute" + MyDataRow["AttributeTypeId"].ToString());

            if (lstControl != null)
            {
                int selValue = int.Parse(lstControl.SelectedValue);

                if (selValue > 0)
                {
                    Attributes += selValue.ToString() + ",";
                }
            }
        }

        //If Attributes length is more than zero.
        if (Attributes.Length > 0)
        {
            // Split the string
            string Attribute = Attributes.Substring(0, Attributes.Length - 1);

            if (Attribute.Length > 0)
            {
                SKUAdmin _SkuAdmin = new SKUAdmin();
                DataSet MyDatas = _SkuAdmin.GetBySKUAttributes(ItemId, Attribute);

                DataView Sku = new DataView(MyDatas.Tables[0]);
                Sku.Sort = "SKU";
                uxGridInventoryDisplay.DataSource = Sku;
                uxGridInventoryDisplay.DataBind();
            }
        }
        else
        {          
            this.BindSKU();
        }               
    }

    /// <summary>
    /// Binding Product tiered pricing for this product
    /// </summary>
    public void BindTieredPricing()
    {
        //Create Instance for Product Admin and Product entity
        ZNode.Libraries.Admin.ProductAdmin ProdAdmin = new ProductAdmin();
        TList<ProductTier> productTierList = ProdAdmin.GetTieredPricingByProductId(ItemId);

        uxGridTieredPricing.DataSource = productTierList;
        uxGridTieredPricing.DataBind();          }

    /// <summary>
    /// Binding Product Values into label Boxes
    /// </summary>
    public void BindViewData()
    {
        //Create Instance for Product Admin and Product entity
        ZNode.Libraries.Admin.ProductAdmin prodAdmin = new ProductAdmin();
        Product product=prodAdmin.GetByProductId(ItemId);

        DataSet ds = prodAdmin.GetProductDetails(ItemId);
        int Count = 0;

        //Check for Number of Rows
        if (ds.Tables[0].Rows.Count != 0)
        {
            //Bind ProductType,Manufacturer,Supplier
            lblProdType.Text = ds.Tables[0].Rows[0]["producttype name"].ToString();
            lblManufacturerName.Text = ds.Tables[0].Rows[0]["MANUFACTURER NAME"].ToString();

            //Check For Product Type
            productTypeID = int.Parse(ds.Tables[0].Rows[0]["ProductTypeId"].ToString());
            Count = prodAdmin.GetAttributeCount(int.Parse(ds.Tables[0].Rows[0]["ProductTypeId"].ToString()), ZNodeConfigManager.SiteConfig.PortalID);
            //Check product atributes count
            if (Count > 0)
            {
                tabProductDetails.Tabs[2].Enabled = true; // Enable Manage inventory tab 
            }
            else
            {
                tabProductDetails.Tabs[2].Enabled = false;// Disable Manage inventory tab
            }
        }

        if (product != null)
        {
            //General Information
            lblProdName.Text = product.Name;                    
            lblProdNum.Text  =  product.ProductNum.ToString();
            if (Count > 0)
            {
                lblProductSKU.Text = "See Manage SKUs tab";
                lblQuantity.Text = "See Manage SKUs tab";
                lblReOrder.Text = "See Manage SKUs tab";
                lblSupplierName.Text = "See Manage SKUs tab";
            }
            else
            {
                lblProductSKU.Text = product.SKU;

                if(product.QuantityOnHand.HasValue)
                lblQuantity.Text = product.QuantityOnHand.Value.ToString();
                if(product.ReorderLevel.HasValue)
                lblReOrder.Text = product.ReorderLevel.Value.ToString();
                lblSupplierName.Text = ds.Tables[0].Rows[0]["Supplier Name"].ToString();
            }

            if (product.MaxQty.HasValue)
                lblMaxQuantity.Text = product.MaxQty.Value.ToString();

            //image
            if (product.ImageFile.Trim().Length > 0)
            {
                string ImageFilePath = ZNodeConfigManager.EnvironmentConfig.MediumImagePath + product.ImageFile;
                ItemImage.ImageUrl = ImageFilePath;                
            }               
            else
            {
                ItemImage.ImageUrl = ZNodeConfigManager.SiteConfig.ImageNotAvailablePath;
            }      
             

            //Product Description and Features
            lblShortDescription.Text = product.ShortDescription;
            lblProductDescription.Text = product.Description;
            lblProductFeatures.Text = product.FeaturesDesc;
            lblproductspecification.Text = product.Specifications;
            lbladditionalinfo.Text = product.AdditionalInformation;
            lblDownloadLink.Text = product.DownloadLink;

            //Product Attributes
            if (product.RetailPrice.HasValue)
                lblRetailPrice.Text = product.RetailPrice.Value.ToString("c");
            if (product.WholesalePrice.HasValue)
                lblWholeSalePrice.Text = product.WholesalePrice.Value.ToString("c");
            lblSalePrice.Text = FormatPrice(product.SalePrice);               
            lblWeight.Text = this.FormatProductWeight(product.Weight);
            if (product.Height.HasValue)
                lblHeight.Text = product.Height.Value.ToString("N2") + " " + ZNodeConfigManager.SiteConfig.DimensionUnit;
            if (product.Width.HasValue)
                lblWidth.Text = product.Width.Value.ToString("N2") + " " + ZNodeConfigManager.SiteConfig.DimensionUnit;
            if (product.Length.HasValue)
                lblLength.Text = product.Length.Value.ToString("N2") + " " + ZNodeConfigManager.SiteConfig.DimensionUnit;


            //Display Settings
            chkProductEnabled.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(product.ActiveInd.ToString()));
            lblProdDisplayOrder.Text = product.DisplayOrder.ToString();
            chkIsSpecialProduct.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(this.DisplayVisible(product.HomepageSpecial));
            chkIsNewItem.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(this.DisplayVisible(product.NewProductInd));
            chkProductPricing.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(this.DisplayVisible(product.CallForPricing));
            chkproductInventory.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(this.DisplayVisible(product.InventoryDisplay));            
            
            // Display Tax Class Name
            if (product.TaxClassID.HasValue)
            {
                 
                TaxRuleAdmin taxRuleAdmin = new TaxRuleAdmin();
                TaxClass taxClass = taxRuleAdmin.GetByTaxClassID(product.TaxClassID.Value);

                if(taxClass != null)
                    lblTaxClass.Text = taxClass.Name;
            }

            // Recurring Billing
            imgRecurringBillingInd.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(this.DisplayVisible(product.RecurringBillingInd));
            lblBillingPeriod.Text = product.RecurringBillingPeriod;
            lblFrequency.Text = product.RecurringBillingFrequency;
            lblTotalCycles.Text = product.RecurringBillingTotalCycles.GetValueOrDefault(0).ToString();

            shipSeparatelyInd.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(product.ShipSeparately);
            lblMastePageName.Text = product.MasterPage;

            //seo 
            lblSEODescription.Text = product.SEODescription;
            lblSEOKeywords.Text = product.SEOKeywords;
            lblSEOTitle.Text = product.SEOTitle;
            lblSEOURL.Text = product.SEOURL;

            //checking whether the image is active or not
            //ZNode.Libraries.Admin.ProductViewAdmin pp = new ProductViewAdmin();

            //Inventory Setting - Out of Stock Options
            if (product.TrackInventoryInd.HasValue && product.AllowBackOrder.HasValue)
            {
                if ((product.TrackInventoryInd.Value) && (product.AllowBackOrder.Value == false))
                {
                    chkCartInventoryEnabled.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse("true"));
                    chkIsBackOrderEnabled.Src = SetCheckMarkImage();
                    chkIstrackInvEnabled.Src = SetCheckMarkImage();

                }
                else if (product.TrackInventoryInd.Value && product.AllowBackOrder.Value)
                {
                    chkCartInventoryEnabled.Src = SetCheckMarkImage();
                    chkIsBackOrderEnabled.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse("true"));
                    chkIstrackInvEnabled.Src = SetCheckMarkImage();
                }
                else if ((product.TrackInventoryInd.Value == false) && (product.AllowBackOrder.Value == false))
                {
                    chkCartInventoryEnabled.Src = SetCheckMarkImage();
                    chkIsBackOrderEnabled.Src = SetCheckMarkImage();
                    chkIstrackInvEnabled.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse("true"));
                }
            }
            else
            {
                chkCartInventoryEnabled.Src = SetCheckMarkImage();
                chkIsBackOrderEnabled.Src = SetCheckMarkImage();
                chkIstrackInvEnabled.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse("true"));
            }

            //Inventory Setting - Stock Messages
            lblInStockMsg.Text = product.InStockMsg;
            lblOutofStock.Text = product.OutOfStockMsg;
            lblBackOrderMsg.Text = product.BackOrderMsg;

            if (product.DropShipInd.HasValue)
            {
                IsDropShipEnabled.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(product.DropShipInd.Value);
            }
            else
            {
                IsDropShipEnabled.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse("false"));
            }
            //Binding product Category 
            DataSet dsCategory = prodAdmin.Get_CategoryByProductID(ItemId);
            StringBuilder Builder = new StringBuilder();
            foreach (System.Data.DataRow dr in dsCategory.Tables[0].Rows)
            {
                Builder.Append(prodAdmin.GetCategorypath(dr["Name"].ToString(),dr["Parent1CategoryName"].ToString(), dr["Parent2CategoryName"].ToString()));
                Builder.Append("<br>");
            }

            if (product.FeaturedInd)
            {
                FeaturedProduct.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(true);
            }
            else
            {
                FeaturedProduct.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(false);
            }
            lblProductCategories.Text = Builder.ToString();
            
            //Bind ShippingRule type
            if(product.ShippingRuleTypeID.HasValue)
            lblShippingRuleTypeName.Text = GetShippingRuleTypeName(product.ShippingRuleTypeID.Value);
            if (product.FreeShippingInd.HasValue)
                freeShippingInd.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(product.FreeShippingInd.Value);
            else
                freeShippingInd.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(false);
            
            //Bind Grid - Product Related Items
            this.BindRelatedItems();
            //this.BindImage();
            this.BindImageDatas();
            //Bind Grid - Product Addons
            BindProductAddons();            
            //Bind Grid - Inventory
            BindSKU();
            //Tiered Pricing
            BindTieredPricing();
            //Digital Asset
            BindDigitalAssets();
            //Bind product highlights
            BindProductHighlights();
        }
        else
        {
            throw (new ApplicationException("Product Requested could not be found."));
        }
        
    }

    /// <summary>
    /// Bind data to grid
    /// </summary>
    private void BindProductHighlights()
    {
        ProductViewAdmin imageadmin = new ProductViewAdmin();
        uxGridHighlights.DataSource = imageadmin.GetAllHighlightImage(ItemId);
        uxGridHighlights.DataBind();
    }  

    /// <summary>
    /// Bind data to grid
    /// </summary>
    private void BindProductAddons()
    {
        ProductAddOnAdmin ProdAddonAdminAccess = new ProductAddOnAdmin();

        //Bind Associated Addons for this product
        uxGridProductAddOns.DataSource = ProdAddonAdminAccess.GetByProductId(ItemId);
        uxGridProductAddOns.DataBind();
    }    

    /// <summary>
    /// Bind Related Products 
    /// </summary>
    public void BindRelatedItems()
    {
        ProductCrossSellAdmin ProdCrossSellAccess = new ProductCrossSellAdmin();
        DataSet MyDataSet = ProdCrossSellAccess.GetByProductID(ItemId);
        uxGrid.DataSource = MyDataSet;
        uxGrid.DataBind();
    }

    public void BindImage()
    {
        ZNode.Libraries.Admin.StoreSettingsAdmin imageadmin = new StoreSettingsAdmin();
        GridThumb.DataSource = imageadmin.Getall();
        GridThumb.DataBind();
    }

    /// <summary>
    /// Bind Related ImageViews 
    /// </summary>
    private void BindImageDatas()
    {
        ProductViewAdmin imageadmin = new ProductViewAdmin();
        GridThumb.DataSource = imageadmin.GetAllAlternateImage(ItemId);
        GridThumb.DataBind();                     
    }

    /// <summary>
    /// Bind Digital assets for this product
    /// </summary>
    private void BindDigitalAssets()
    {
        ZNode.Libraries.Admin.ProductAdmin productAdmin = new ProductAdmin();
        uxGridDigitalAsset.DataSource = productAdmin.GetDigitAssetByProductId(ItemId);
        uxGridDigitalAsset.DataBind();

    }
    #endregion

    #region Grid Events
    
    # region Events for ProductAddon Grid

    /// <summary>
    /// Add Client side event to the Delete Button in the Grid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGridProductAddOns_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Retrieve the Button control from the Seventh column.
            Button DeleteButton = (Button)e.Row.Cells[2].FindControl("btnDelete");

            //Set the Button's CommandArgument property with the row's index.
            DeleteButton.CommandArgument = e.Row.RowIndex.ToString();

            //Add Client Side confirmation
            DeleteButton.OnClientClick = "return confirm('Are you sure you want to delete this item?');";           
        }
    }
    /// <summary>
    /// Product Add On Row command event - occurs when delete button is fired.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGridProductAddOns_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Page")
        {
        }
        else
        {
            // Convert the row index stored in the CommandArgument
            // property to an Integer.
            int index = Convert.ToInt32(e.CommandArgument);

            // Get the values from the appropriate
            // cell in the GridView control.
            GridViewRow selectedRow = uxGridProductAddOns.Rows[index];

            TableCell Idcell = selectedRow.Cells[0];
            string Id = Idcell.Text;

            if (e.CommandName == "Remove")
            {
                ProductAddOnAdmin AdminAccess = new ProductAddOnAdmin();
                if(AdminAccess.DeleteProductAddOn(int.Parse(Id)))
                BindProductAddons();
            }            
        }
    }

    /// <summary>
    /// Product AddOn grid Items Page Index Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGridProductAddOns_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGridProductAddOns.PageIndex = e.NewPageIndex;
        this.BindProductAddons();
    } 

    # endregion   

    # region Related to CrossSell Grid Events
    /// <summary>
    /// Event triggered when a command button is clicked on the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Page")
        { }
        else
        {
            if (e.CommandName == "RemoveItem")
            {
                ProductCrossSellAdmin _prodCrossSellAdmin = new ProductCrossSellAdmin();
                bool Check = _prodCrossSellAdmin.Delete(int.Parse(e.CommandArgument.ToString()),ItemId,ZNodeConfigManager.SiteConfig.PortalID);
                if (Check)
                {
                    this.BindRelatedItems();
                }
            }          
        }
    }

    /// <summary>
    /// Related Items Page Index Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        this.BindRelatedItems();
    }
    #endregion

    # region InventoryDisplay Grid related Methods
    /// <summary>
    /// Event triggered when a command button is clicked on the grid (InventoryDisplay Grid)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGridInventoryDisplay_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() == "page")
        { }
        else
        {
            if (e.CommandName == "Edit")
            {
                //Redirect Edit SKUAttrbute Page
                Response.Redirect(AddSKULink + ItemId + "&skuid=" + e.CommandArgument.ToString() + "&typeid=" + productTypeID);
            }
            else if (e.CommandName == "Delete")
            {
                // Delete SKU and SKU Attribute
                SKUAdmin _AdminAccess = new SKUAdmin();
                
                bool check = _AdminAccess.Delete(int.Parse(e.CommandArgument.ToString()));
                if(check)
                {
                    _AdminAccess.DeleteBySKUId(int.Parse(e.CommandArgument.ToString()));
                }
            }
        }
    }

    /// <summary>
    /// Event triggered when the Grid Row is Deleted
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGridInventoryDisplay_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {        
        this.BindSearchData();
    }
    /// <summary>
    /// Event triggered when the grid(Inventory) page is changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGridInventoryDisplay_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGridInventoryDisplay.PageIndex = e.NewPageIndex;     
        this.BindSearchData();
    }

    #endregion

    # region Related to Product Views Grid Events

    /// <summary>
    /// Related Items Page Index Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    protected void GridThumb_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridThumb.PageIndex = e.NewPageIndex;               
        this.BindImageDatas();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridThumb_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        this.BindImageDatas();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridThumb_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Page")
        {
        }
        else
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect(AddViewlink + ItemId + "&productimageid=" + e.CommandArgument.ToString() + "&typeid=" + productTypeID);
            }
            if (e.CommandName == "RemoveItem")
            {
                ZNode.Libraries.Admin.ProductViewAdmin prodadmin = new ProductViewAdmin();
                bool Status = prodadmin.Delete(int.Parse(e.CommandArgument.ToString()));
                if (Status)
                {
                    this.BindImageDatas();
                }
            }
        }
    }
    #endregion

    # region Related to Product - Tiered Pricing
    /// <summary>
    /// Add Client side event to the Delete Button in the Grid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGridTieredPricing_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Retrieve the Button control from the Seventh column.
            Button DeleteButton = (Button)e.Row.Cells[5].FindControl("btnRemoveTieredPricing");

            //Add Client Side confirmation
            DeleteButton.OnClientClick = "return confirm('Are you sure you want to delete this item?');";
        }
    }
    /// <summary>
    /// Related Items Page Index Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    protected void uxGridTieredPricing_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridThumb.PageIndex = e.NewPageIndex;
        BindTieredPricing();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGridTieredPricing_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {  
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGridTieredPricing_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Page")
        {
        }
        else
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect(AddTieredPricingPageLink + ItemId + "&tierid=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "Delete")
            {
                ZNode.Libraries.Admin.ProductAdmin productAdmin = new ProductAdmin();
                bool Status = productAdmin.DeleteProductTierById(int.Parse(e.CommandArgument.ToString()));
                if (Status)
                {                    
                    BindTieredPricing();
                }
            }
        }
    }
    #endregion

    # region Related to product Highlights
    /// <summary>
    /// Add Client side event to the Delete Button in the Grid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGridHighlights_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Retrieve the Button control from the Seventh column.
            Button DeleteButton = (Button)e.Row.Cells[1].FindControl("DeleteHighlight");

            //Add Client Side confirmation
            DeleteButton.OnClientClick = "return confirm('Are you sure you want to delete this item?');";
        }
    }
    /// <summary>
    /// product highlight Items Page Index Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    protected void uxGridHighlights_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGridHighlights.PageIndex = e.NewPageIndex;
        BindProductHighlights();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGridHighlights_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    /// <summary>
    /// Product highlights row command event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGridHighlights_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Page")
        {
        }
        else
        {
            if (e.CommandName == "Delete") //Delete button is triggered
            {
                ZNode.Libraries.Admin.ProductAdmin productAdmin = new ProductAdmin();
                bool Status = productAdmin.DeleteProductHighlight(int.Parse(e.CommandArgument.ToString()));                
                BindProductHighlights();                
            }
            else if (e.CommandName == "Edit")
            {
                Response.Redirect(EditHighlightPageLink + e.CommandArgument + "&productid=" + ItemId);
            }            
        }
    }
    #endregion

    # region Related to Digital Asset
    /// <summary>
    /// Add Client side event to the Delete Button in the Grid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGridDigitalAsset_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Retrieve the Button control from the Seventh column.
            Button DeleteButton = (Button)e.Row.Cells[5].FindControl("btnRemoveDigitalAsset");

            //Add Client Side confirmation
            DeleteButton.OnClientClick = "return confirm('Are you sure you want to delete this item?');";
        }
    }
    /// <summary>
    /// Digital asset grid Page Index Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>    
    protected void uxGridDigitalAsset_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGridDigitalAsset.PageIndex = e.NewPageIndex;
        BindDigitalAssets();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGridDigitalAsset_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGridDigitalAsset_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Page")
        {
        }
        else
        {            
            if (e.CommandName == "Delete")
            {
                ZNode.Libraries.Admin.ProductAdmin productAdmin = new ProductAdmin();
                bool Status = productAdmin.DeleteDigitalAsset(int.Parse(e.CommandArgument.ToString()));
                if (Status)
                {
                    BindDigitalAssets();
                }
            }
        }
    }
    #endregion

    #endregion

    # region Events

    # region Events for ProductAddon

    /// <summary>
    /// Add AddOn Button Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddNewAddOn_Click(object sender, EventArgs e)
    {        
        Response.Redirect(AddProductAddOnLink + "&itemid=" + ItemId);
    }  
    
    #endregion

    /// <summary>
    /// Add New Highlight button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddHighlight_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddHighlightPageLink + ItemId);
    }

    /// <summary>
    /// Add Digital asset button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddDigitalAsset_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddDigitalAssetPageLink + ItemId);
    }
    
    /// <summary>
    /// Add Tiered pricing button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddTieredPricing_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddTieredPricingPageLink + ItemId);
    }

    /// <summary>
    /// Add New product sku button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddSKU_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddSKULink + ItemId + "&typeid=" + productTypeID);
    }
    /// <summary>
    /// Redirecting to add new Related Item for a Product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddRelatedItems_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddRelatedItemLink + ItemId);
    }

    /// <summary>
    /// Redirecting to Product Edit Page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void EditProduct_Click(object sender, EventArgs e)
    {
        Response.Redirect(EditPageLink + ItemId);
    }

    /// <summary>
    /// Redirecting to product advance setting page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void EditAdvancedSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect(EditAdvancedPageLink + ItemId);
    }

    /// <summary>
    /// Redirecting to product SEO setting page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void EditSEOSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect(EditSEOLink + ItemId);
    }

    /// <summary>
    /// Redirecting to product additional Info page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void EditAdditionalInfo_Click(object sender, EventArgs e)
    {
        Response.Redirect(EditAdditionalInfoLink + ItemId);
    }
    
    /// <summary>
    /// Redirecting to Product List Page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ProductList_Click(object sender, EventArgs e)
    {
        Response.Redirect(ListLink);
    }

    /// <summary>
    /// Redirecting to product views page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddProductView_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddViewlink + ItemId);
    }

    /// <summary>
    /// Back Button Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ListLink);
    }

    /// <summary>
    /// Search button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindSearchData();
    }

    /// <summary>
    /// Clear button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ProductAdmin _adminAccess = new ProductAdmin();
        DataSet ds = _adminAccess.GetProductDetails(ItemId);

        //Check for Number of Rows
        if (ds.Tables[0].Rows.Count != 0)
        {
            //Check For Product Type
            productTypeID = int.Parse(ds.Tables[0].Rows[0]["ProductTypeId"].ToString());
        }

        // For Attribute value.
        string Attributes = String.Empty;

        //GetAttribute for this ProductType
        DataSet MyAttributeTypeDataSet = _adminAccess.GetAttributeTypeByProductTypeID(productTypeID);

        foreach (DataRow MyDataRow in MyAttributeTypeDataSet.Tables[0].Rows)
        {
            System.Web.UI.WebControls.DropDownList lstControl = (System.Web.UI.WebControls.DropDownList)ControlPlaceHolder.FindControl("lstAttribute" + MyDataRow["AttributeTypeId"].ToString());
            lstControl.SelectedIndex = 0;
        }

        this.BindSKU();        
    }   
   
    # endregion

    #region Helper Functions

    /// <summary>
    /// This will automatically pre-select the tab according the query string value(mode)
    /// </summary>
    private void ResetTab()
    {
        if (Mode.Equals("advanced"))
        {
            tabProductDetails.ActiveTabIndex = 1; //Set Advanced Settings as active tab 
        }
        else if (Mode.Equals("crosssell"))
        {
            tabProductDetails.ActiveTabIndex = 4; //For Related Items
        }
        else if (Mode.Equals("views"))
        {
            tabProductDetails.ActiveTabIndex = 5; //For Related Images
        }
        else if (Mode.Equals("addons"))
        {
            tabProductDetails.ActiveTabIndex = 6; //For Product Options
        }  
        else if (Mode.Equals("inventory"))
        {
            tabProductDetails.ActiveTabIndex = 2; //For Manage Inventory
        }
        else if (Mode.Equals("tieredPricing"))
        {
            tabProductDetails.ActiveTabIndex = 7; //For Product Tiered pricing tab
        }
        else if (Mode.Equals("highlight"))
        {
            tabProductDetails.ActiveTabIndex = 8; //For Product Highlights tab
        }
        else if (Mode.Equals("digitalAsset"))
        {
            tabProductDetails.ActiveTabIndex = 9; //For product digital assets tab
        }
        else if (Mode.Equals("additional"))
        {
            tabProductDetails.ActiveTabIndex = 10; //For Additional Information tab
        }
        else if (Mode.Equals("seo"))
        {
            tabProductDetails.ActiveTabIndex = 3; //For SEO settings
        } 

    }
    /// <summary>
    /// Returns a Format Weight string
    /// </summary>
    /// <param name="FieldValue"></param>
    /// <returns></returns>
    public string FormatProductWeight(Object FieldValue)
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
                return FieldValue.ToString() + " " + ZNodeConfigManager.SiteConfig.WeightUnit;
            }
        }
    }


    /// <summary>
    /// Format the Price of a Product
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
            if (Convert.ToDecimal(FieldValue) == 0)
            {
                return String.Empty;
            }
            else
            {
                return String.Format("{0:c}", FieldValue);
            }
            
        }
    }

    /// <summary>
    /// Validate for Null Values and return a Boolean Value
    /// </summary>
    /// <param name="Fieldvalue"></param>
    /// <returns></returns>
    public bool DisplayVisible(Object Fieldvalue)
    {
        if (Fieldvalue == DBNull.Value) 
        {
            return false;
        }
        else
        {
            if (Convert.ToInt32(Fieldvalue) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    /// <summary>
    /// Gets the name of the Addon for this AddonId
    /// </summary>
    /// <param name="addOnId"></param>
    /// <returns></returns>
    public string GetAddOnName(object addOnId)
    {
        ProductAddOnAdmin AdminAccess = new ProductAddOnAdmin();
        AddOn _addOn = AdminAccess.GetByAddOnId(int.Parse(addOnId.ToString()));

        if (_addOn != null)
        {
            return _addOn.Name;
        }

        return "";
    }

    /// <summary>
    /// Gets the title of the Addon for this AddonId
    /// </summary>
    /// <param name="addOnId"></param>
    /// <returns></returns>
    public string GetAddOnTitle(object addOnId)
    {
        ProductAddOnAdmin AdminAccess = new ProductAddOnAdmin();
        AddOn _addOn = AdminAccess.GetByAddOnId(int.Parse(addOnId.ToString()));

        if (_addOn != null)
        {
            return _addOn.Title;
        }

        return "";
    }


    /// <summary>
    /// Gets the Name of the Profile for this ProfileID
    /// </summary>
    /// <param name="ProfileID"></param>
    /// <returns></returns>
    public string GetProfileName(object ProfileID)
    {
        ProfileAdmin AdminAccess = new ProfileAdmin();
        if (ProfileID == null)
        {
            return "All Profile";
        }
        else
        {
            Profile profile = AdminAccess.GetByProfileID(int.Parse(ProfileID.ToString()));

            if (profile != null)
            {
                return profile.Name;
            }
        }
        return "";
    }

    /// <summary>
    /// Gets the name of the Addon for this AddonId
    /// </summary>
    /// <param name="addOnId"></param>
    /// <returns></returns>
    public string GetShippingRuleTypeName(int shippingRuleTypeID)
    {
        ShippingRuleTypeAdmin _ShippingRuleTypeAdmin = new ShippingRuleTypeAdmin();
       
        ShippingRuleType _shippingRuleType = _ShippingRuleTypeAdmin.GetByShippingRuleTypeID(shippingRuleTypeID);

        if (_shippingRuleType != null)
        {
            return _shippingRuleType.Name;
        }

        return "";
    }

    /// <summary>
    /// Swap image with the new image file path
    /// </summary>
    /// <param name="imagepath"></param>
    public void changeimage(string imagepath)
    {
        ItemImage.ImageUrl = ZNodeConfigManager.EnvironmentConfig.SmallImagePath + imagepath;
    }

    /// <summary>
    /// Return cross-mark image path
    /// </summary>
    /// <returns></returns>
    public string SetCheckMarkImage()
    {
        return ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse("false"));
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="OrderLineItemId"></param>
    /// <returns></returns>
    public bool IsDigitalAssetAssigned(object OrderLineItemId)
    {
        if (OrderLineItemId == null)
            return false;

        return true;
    }
    # endregion        
}
