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
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.ECommerce.ShoppingCart;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.DataAccess.Service;

public partial class Admin_Secure_Enterprise_OrderDesk_QuickOrder : System.Web.UI.UserControl
{
    # region Public Events
    public System.EventHandler SubmitButtonClicked;
    public System.EventHandler AddProductButtonClicked;
    public System.EventHandler CancelButtonClicked;
    # endregion

    #region Private Variables
    protected bool _showInventoryLevels = false;
    private ZNodeProduct _product;
    ZNodeShoppingCart shoppingCart = null;
    #endregion

    #region Public Properties
    /// <summary>
    /// Sets the catalog item object. The control will bind to the data from this object
    /// </summary>
    public ZNodeProduct Product
    {
        get
        {
            if (ViewState["CatalogItem"] != null)
            {
                return ViewState["CatalogItem"] as ZNodeProduct;
            }

            return new ZNodeProduct();
        }
        set
        {
            ViewState["CatalogItem"] = value;
        }
    }

    /// <summary>
    /// Sets the catalog item object. The control will bind to the data from this object
    /// </summary>
    public int Quantity
    {
        get
        {
            if (ViewState["ShoppingCartQuantity"] != null)
            {
                return (int)ViewState["ShoppingCartQuantity"];
            }

            return 1;
        }
        set
        {
            ViewState["ShoppingCartQuantity"] = value;
        }
    }

    /// <summary>
    /// Sets the Grid
    /// </summary>
    public bool ShowInventoryLevels
    {
        set
        {
            _showInventoryLevels = value;
        }
        get
        {
            return _showInventoryLevels;
        }
    }
    #endregion

    # region Page Events
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //Gets the browser name
        if (Request.Browser.Browser.Equals("IE"))
        {
            UpdateProgressIE.Visible = true;
        }
        else if (Request.Browser.Browser.Equals("Firefox"))
        {
            UpdateProgressMozilla.Visible = true;
        }
        else
        {
            UpdateProgressMozilla.Visible = true;
        }

        if (!Page.IsPostBack)
        {          

            //Hide product attribute grid
            uxProductAttributeGrid.Visible = false;                               
            
        }
        else
        {
            BindAddOnsAttributes();
        }
    }   

    # endregion

    # region Bind Methods
    /// <summary>
    /// Bind edit data
    /// </summary>
    public void Bind()
    {
        _product = Product;

        if (_product.ProductID > 0)
        {
            BindQuantityList();

            DisplayPrice();

            tablerow.Visible = true;
        }
        else
        {
            ResetUI();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void BindQuantityList()
    {
        // Bind the MaxQuantity value to the Dropdown list
        int max = 0;

        // If Max quantity is not set in admin , set it to 10
        if (_product.MaxQty == 0)
        {
            max = 10;
        }
        else
        {
            max = _product.MaxQty;
        }

        ArrayList quantityList = new ArrayList();

        for (int i = 1; i <= max; i++)
        {
            quantityList.Add(i);
        }

        Qty.DataSource = quantityList;
        Qty.DataBind();
        

        Qty.SelectedValue = Quantity.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    protected void DisplayProductGrid(ZNodeProduct product)
    {
        if (product.ProductID > 0)
        {
            string[] attributeValues = product.SelectedSKU.AttributesValue.Split(new char[] { ',' });           

            if (attributeValues.Length > 1)
            {
                uxProductAttributeGrid.ShowInventoryLevels = true;
                uxProductAttributeGrid.ShowFooter = false;

                int attributeId = int.Parse(attributeValues[0]);
                uxProductAttributeGrid.AttributeId = attributeId;
                uxProductAttributeGrid.ProductTypeID = product.ProductTypeID;
                uxProductAttributeGrid.ProductID = product.ProductID;
                uxProductAttributeGrid.Bind();
                uxProductAttributeGrid.Visible = true;

                string _imagePath = ZNodeConfigManager.EnvironmentConfig.MediumImagePath + uxProductAttributeGrid.Path;

                if (ShowCatalogImage(_imagePath))
                {
                    CatalogItemImage.ImageUrl = _imagePath;
                }
                else
                {
                    CatalogItemImage.ImageUrl = _product.MediumImageFilePath;
                }

                ProductDescription.Text = _product.Description;
            }
            else
            {
                uxProductAttributeGrid.Visible = false;
            }
        }
    }

    /// <summary>
    /// Reset the field values
    /// </summary>
    public void  ResetUI()
    {
        tablerow.Visible = false;

        pnlProductDetails.Visible = false;
        pnlProductList.Visible = false;
        txtProductName.Text = "";
        txtProductNum.Text = "";
        txtProductSku.Text = "";

        CatalogItemImage.ImageUrl = "";
        ProductDescription.Text = "";
        lblUnitPrice.Text = (0).ToString("c");
        lblTotalPrice.Text = lblUnitPrice.Text;
        Qty.Text = "1";
        ControlPlaceHolder.Controls.Clear();

        CatalogItemImage.Visible = ShowCatalogImage(_product.MediumImageFilePath);
    }

    /// <summary>
    /// 
    /// </summary>
    private void BindProducts()
    {
        int currentPageIndex = int.Parse(PageIndex.Value);
        int totalPages = 0;

        if (!string.IsNullOrEmpty(txtProductName.Text.Trim()) || !string.IsNullOrEmpty(txtProductSku.Text.Trim()) || !string.IsNullOrEmpty(txtProductNum.Text.Trim()) || !string.IsNullOrEmpty(txtBrand.Text.Trim()) || !string.IsNullOrEmpty(txtCategory.Text.Trim()))
        {
            // Get Products            
            DataTable productList = ZNodeProductList.GetPagedProductListByPortalID(ZNodeConfigManager.SiteConfig.PortalID, txtProductName.Text.Trim(), txtProductNum.Text.Trim(), txtProductSku.Text.Trim(), txtBrand.Text.Trim(), txtCategory.Text.Trim(), currentPageIndex, uxGrid.PageSize, out totalPages);

            TotalPages.Value = totalPages.ToString();

            if (productList.Rows.Count > 0)
            {
                pnlProductList.Visible = true;

                uxGrid.DataSource = productList;
                uxGrid.DataBind();

                if (totalPages > 1)
                {
                    NextPageLink.Enabled = true;
                    LastPageLink.Enabled = true;
                }
            }
            else
            {
                lblSearhError.Text = "No products found.";
            }
        }
        else
        {
            lblSearhError.Text = "Enter Product Name or Item# or SKU or Brand or Category and click on Search to find a Product.";
        }
    }
    # endregion

    # region General Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmitAnother_Click(object sender, EventArgs e)
    {
        AddToCart();

        if (this.AddProductButtonClicked != null)
        {
            this.AddProductButtonClicked(sender, e);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Search_Click(object sender, EventArgs e)
    {
        pnlProductDetails.Visible = false;
        pnlProductList.Visible = false;

        NextPageLink.Enabled = false;
        PreviousPageLink.Enabled = false;
        FirstPageLink.Enabled = false;
        LastPageLink.Enabled = false;

        // 
        uxGrid.DataSource = null;
        uxGrid.DataBind();

        TotalPages.Value = "0";
        PageIndex.Value = "1";

        BindProducts();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (this.CancelButtonClicked != null)
        {
            this.CancelButtonClicked(sender, e);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve Product Id
        Label lblId = uxGrid.Rows[uxGrid.SelectedIndex].FindControl("ui_CustomerIdSelect") as Label;

        if (lblId.Text != "0")
        {
            int productId = 0;

            int.TryParse(lblId.Text, out productId);

            if (productId > 0)
            {
                pnlProductDetails.Visible = true;
                pnlProductList.Visible = false;
                uxGrid.DataSource = null;
                uxGrid.DataBind();
                uxGrid.Dispose(); // Clear resources

                int quantity = 1;
                int.TryParse(Qty.SelectedValue, out quantity);


                if (Product.ProductID != productId)
                {
                    // Get a product object using create static method
                    _product = ZNodeProduct.Create(productId, ZNodeConfigManager.SiteConfig.PortalID);
                    quantity = 1;
                    Product = _product;
                }
                else
                {
                    _product = Product;
                }

                Quantity = quantity;

                CatalogItemImage.ImageUrl = _product.MediumImageFilePath;
                CatalogItemImage.Visible = ShowCatalogImage(_product.MediumImageFilePath);
                ProductDescription.Text = _product.Description;
                BindAddOnsAttributes();

                if (_product.ZNodeAddOnCollection.Count > 0)
                {
                    ValidateAddOns(sender, e);
                }
                else
                {
                    Bind();
                }

                if (_product.ZNodeAttributeTypeCollection.Count == 0)
                {
                    //Enable stock validator
                    EnableStockValidator(_product);
                }
                else
                {
                    ValidateAttributes(sender, e);
                }
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Qty_SelectedIndexChanged(object sender, EventArgs e)
    {
        Quantity = int.Parse(Qty.SelectedValue);
        DisplayPrice();
    }    
    #endregion

    # region Events
    /// <summary>
    /// Submit Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        AddToCart();

        if (this.SubmitButtonClicked != null)
        {
            this.SubmitButtonClicked(sender, e);
        }
    }

    /// <summary>
    /// Event raised when the "Cancel" button is clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (this.CancelButtonClicked != null)
        {
            this.CancelButtonClicked(sender, e);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void SelectorList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList list = (sender) as DropDownList;

        if (list.SelectedValue != "0")
        {
            int productId = int.Parse(list.SelectedValue);
            int quantity = 0;

            if (!int.TryParse(Qty.Text.Trim(), out quantity))
            {
                quantity = 1;
            }

            if (Product.ProductID != productId)
            {
                //Get a product object using create static method
                _product = ZNodeProduct.Create(productId, ZNodeConfigManager.SiteConfig.PortalID);                
                Product = _product;
            }
            else
            {
                _product = Product;
            }

            CatalogItemImage.ImageUrl = _product.MediumImageFilePath;
            CatalogItemImage.Visible = ShowCatalogImage(_product.MediumImageFilePath);
            ProductDescription.Text = _product.Description;
            
            BindAddOnsAttributes();

            if (_product.ZNodeAddOnCollection.Count > 0)
            {
                ValidateAddOns(sender, e);
            }
            else
            {
                Bind();
            }

            if (_product.ZNodeAttributeTypeCollection.Count == 0)
            {
                //Enable stock validator
                EnableStockValidator(_product);
            }
            else
            {
                ValidateAttributes(sender, e);
            }            
        }
        else
        {
            Product = new ZNodeProduct();
            Bind();
        }
    }

    /// <summary>
    /// Validate selected addons
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ValidateAddOns(object sender, EventArgs e)
    {
        # region Local Variables
        System.Text.StringBuilder _addonValues = new System.Text.StringBuilder();
        _product = Product;
        # endregion

        # region Validate AddOns

        foreach (ZNodeAddOn AddOn in _product.ZNodeAddOnCollection)
        {
            System.Web.UI.WebControls.DropDownList lstCtrl = new DropDownList();

            lstCtrl = (System.Web.UI.WebControls.DropDownList)ControlPlaceHolder.FindControl("lstAddOn" + AddOn.AddOnID.ToString());

            if (_addonValues.Length > 0)
            {
                _addonValues.Append(",");
            }

            if (lstCtrl.SelectedValue == "0" || lstCtrl.SelectedValue == "-1")
            {
                _addonValues.Append(lstCtrl.SelectedValue);
            }
            else
            {
                //Loop through the Add-on values for each Add-on
                foreach (ZNodeAddOnValue AddOnValue in AddOn.ZNodeAddOnValueCollection)
                {
                    //Optional Addons are not selected,then leave those addons 
                    //If optinal Addons are selected, it should add with the Selected item 
                    if (AddOnValue.AddOnValueID.ToString() == lstCtrl.SelectedValue) //Check for Selected Addon value 
                    {
                        //Add to Selected Addon list for this product
                        _addonValues.Append(AddOnValue.AddOnValueID.ToString());
                    }
                }
            }
        }

        ZNodeAddOnList SelectedAddOn = new ZNodeAddOnList();

        if (!_addonValues.ToString().Contains("0"))
        {
            //get a sku based on Add-ons selected
            SelectedAddOn = ZNodeAddOnList.CreateByProductAndAddOns(_product.ProductID, _addonValues.ToString());

            SelectedAddOn.SelectedAddOnValues = _addonValues.ToString();
        }
        else { SelectedAddOn.SelectedAddOnValues = _addonValues.ToString(); }

        //Set Selected Add-on 
        _product.SelectedAddOnItems = SelectedAddOn;
        # endregion

        # region Update Product
        Product = _product;
        Bind();
        # endregion
    }

    /// <summary>
    /// Validate selected attributes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ValidateAttributes(object sender, EventArgs e)
    {
        # region Local Variables
        System.Text.StringBuilder _attributes = new System.Text.StringBuilder();
        System.Text.StringBuilder _description = new System.Text.StringBuilder();
        System.Text.StringBuilder _addonValues = new System.Text.StringBuilder();

        _product = Product;
        # endregion

        # region Validate attributes

        //loop through types to locate the controls
        foreach (ZNodeAttributeType AttributeType in _product.ZNodeAttributeTypeCollection)
        {
            if (_attributes.Length > 0)
            {
                _attributes.Append(",");
            }

            if (!AttributeType.IsPrivate)
            {
                System.Web.UI.WebControls.DropDownList lstControl = (System.Web.UI.WebControls.DropDownList)ControlPlaceHolder.FindControl("lstAttribute" + AttributeType.AttributeTypeId.ToString());

                int selValue = 0;
                if (lstControl.SelectedIndex != -1)
                {
                    selValue = int.Parse(lstControl.SelectedValue);
                }
                if (selValue > 0)
                {
                    AttributeType.SelectedAttributeId = selValue;

                    _attributes.Append(selValue.ToString());

                    _description.Append(AttributeType.Name);
                    _description.Append(": ");
                    _description.Append(lstControl.SelectedItem.Text);
                    _description.Append("<br />");
                }
                else
                {
                    _attributes.Append(selValue.ToString());
                }
            }
        }

        ZNodeSKU SKU = new ZNodeSKU();
            
        if (_attributes.Length > 0)
        {
            SKU = ZNodeSKU.CreateByProductAndAttributes(_product.ProductID, _attributes.ToString());

            SKU.AttributesDescription = _description.ToString();
            SKU.AttributesValue = _attributes.ToString();
        }

        _product.SelectedSKU = SKU;

        # endregion

        # region Product & Bind Fields
        Product = _product;
        Bind();
        //Enable Stock validator
        EnableStockValidator(_product);
        //Display Grid
        DisplayProductGrid(_product);
        # endregion
    }
    # endregion

    # region Paging Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void NextRecord(object sender, EventArgs e)
    {
        if (!PageIndex.Value.Equals(TotalPages.Value))
        {
            PageIndex.Value = (int.Parse(PageIndex.Value) + 1).ToString();

            BindProducts();

            FirstPageLink.Enabled = true;
            PreviousPageLink.Enabled = true;
        }

        if (PageIndex.Value.Equals(TotalPages.Value))
        {
            LastPageLink.Enabled = false;
            NextPageLink.Enabled = false;
        }

        lblSearhError.Text = "";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void PrevRecord(object sender, EventArgs e)
    {
        if (!PageIndex.Value.Equals("1"))
        {
            PageIndex.Value = (int.Parse(PageIndex.Value) - 1).ToString();

            BindProducts();

            LastPageLink.Enabled = true;
            NextPageLink.Enabled = true;
        }

        if (PageIndex.Value.Equals("1"))
        {
            FirstPageLink.Enabled = false;
            PreviousPageLink.Enabled = false;
        }

        lblSearhError.Text = "";
    }


    /// <summary>
    /// 
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void MoveToLastPage(object sender, EventArgs e)
    {
        if (!PageIndex.Value.Equals(TotalPages.Value))
        {
            int pageLength = int.Parse(TotalPages.Value);

            PageIndex.Value = pageLength.ToString();
            BindProducts();

            FirstPageLink.Enabled = true;
            PreviousPageLink.Enabled = true;
        }

        if (PageIndex.Value.Equals(TotalPages.Value))
        {
            LastPageLink.Enabled = false;
            NextPageLink.Enabled = false;
        }

        lblSearhError.Text = "";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void MoveToFirstPage(object sender, EventArgs e)
    {
        if (!PageIndex.Value.Equals("1"))
        {
            PageIndex.Value = "1";
            BindProducts();

            NextPageLink.Enabled = true;
            LastPageLink.Enabled = true;
        }

        if (PageIndex.Value.Equals("1"))
        {
            FirstPageLink.Enabled = false;
            PreviousPageLink.Enabled = false;
        }

        lblSearhError.Text = "";
    }
    #endregion

    #region Bind Data
    /// <summary>
    /// Creates attribute drop downlist controls dynamically for a product
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="rowNum"></param>
    /// <param name="UpdateInd"></param>
    protected void BindAddOnsAttributes()
    {
        _product = Product;

        if (_product.ProductID > 0)
        {
            //Remove existing dynamic controls from the control place holder object
            ControlPlaceHolder.Controls.Clear();

            //Find the placeholder control and Add the literal control to the Controls collection
            // of the PlaceHolder control.
            Literal literal = new Literal();
            literal.Text = "<div class='Attributes'>";
            ControlPlaceHolder.Controls.Add(literal);
            int counter = 0;

            # region Create list control for each product Attribute
            //
            if (_product.ZNodeAttributeTypeCollection.Count > 0)
            {
                foreach (ZNodeAttributeType AttributeType in _product.ZNodeAttributeTypeCollection)
                {
                    System.Web.UI.WebControls.DropDownList lstControl = new DropDownList();
                    lstControl.ID = "lstAttribute" + AttributeType.AttributeTypeId.ToString();
                    lstControl.Attributes.Add("class", "ValueStyle");
                    lstControl.AutoPostBack = true;
                    lstControl.SelectedIndexChanged += new System.EventHandler(ValidateAttributes);

                    foreach (ZNodeAttribute Attribute in AttributeType.ZNodeAttributeCollection)
                    {
                        ListItem li1 = new ListItem(Attribute.Name, Attribute.AttributeId.ToString());
                        lstControl.Items.Add(li1);
                    }

                    string attributeValues = _product.SelectedSKU.AttributesValue;
                    string[] attributes = new string[_product.ZNodeAttributeTypeCollection.Count];


                    if (attributeValues.Length > 0)
                    {
                        attributes = attributeValues.Split(new Char[] { ',' }, StringSplitOptions.None);
                    }

                    if (lstControl.Items.Count > 0)
                    {
                        if (attributes != null)
                            lstControl.SelectedValue = attributes[counter++];
                    }

                    // Add the Attribute dropdownlist control to the Controls collection
                    // of the PlaceHolder control.                
                    ControlPlaceHolder.Controls.Add(lstControl);

                    Literal ltrlSpacer = new Literal();
                    ltrlSpacer.Text = "<br />";
                    ControlPlaceHolder.Controls.Add(ltrlSpacer);

                }
            }
            # endregion

            # region Create list control for each product AddOns
            string addonValues = _product.SelectedAddOnItems.SelectedAddOnValues;

            string[] addonValueId = new string[_product.ZNodeAddOnCollection.Count];
            if (addonValues.Length > 0)
            {
                addonValueId = addonValues.Split(new string[] { "," }, StringSplitOptions.None);
            }

            if (_product.ZNodeAddOnCollection.Count > 0)
            {

                foreach (ZNodeAddOn AddOn in _product.ZNodeAddOnCollection)
                {
                    System.Web.UI.WebControls.DropDownList lstControl = new DropDownList();
                    lstControl.ID = "lstAddOn" + AddOn.AddOnID.ToString();
                    lstControl.AutoPostBack = true;
                    lstControl.SelectedIndexChanged += new System.EventHandler(ValidateAddOns);
                    lstControl.Attributes.Add("class", "ValueStyle");

                    //Don't display list box if there is no add-on values for AddOns
                    if (AddOn.ZNodeAddOnValueCollection.Count > 0)
                    {
                        foreach (ZNodeAddOnValue AddOnValue in AddOn.ZNodeAddOnValueCollection)
                        {
                            string AddOnValueName = AddOnValue.Name;
                            decimal decRetailPrice = AddOnValue.FinalPrice;

                            if (decRetailPrice < 0)
                            {
                                //Price format
                                string priceformat = "-" + ZNodeCurrencyManager.GetCurrencyPrefix() + "{0:0.00}" + ZNodeCurrencyManager.GetCurrencySuffix();
                                AddOnValueName += " : " + String.Format(priceformat, System.Math.Abs(decRetailPrice)) + ZNode.Libraries.ECommerce.Catalog.ZNodeCurrencyManager.GetCurrencySuffix();
                            }
                            else if (decRetailPrice > 0)
                            {
                                AddOnValueName += " : " + decRetailPrice.ToString("c") + ZNode.Libraries.ECommerce.Catalog.ZNodeCurrencyManager.GetCurrencySuffix();

                            }

                            //Added Inventory Message with the Addon Value Name in the dropdownlist
                            AddOnValueName += " " + BindStatusMsg(AddOn, AddOnValue);

                            ListItem li1 = new ListItem(AddOnValueName, AddOnValue.AddOnValueID.ToString());
                            lstControl.Items.Add(li1);

                            if (AddOnValue.IsDefault)
                            {
                                lstControl.SelectedValue = AddOnValue.AddOnValueID.ToString();
                            }
                        }

                        //Check for Optional 
                        if (AddOn.OptionalInd)
                        {
                            ListItem OptionalItem = new ListItem("Optional", "-1");
                            lstControl.Items.Insert(0, OptionalItem);
                            lstControl.SelectedValue = "-1";
                        }

                        //Pre-select previous selected addon values in the list
                        for (int i = 0; i < addonValueId.Length; i++)
                        {
                            if (addonValueId[i] != null)
                            {
                                lstControl.SelectedValue = addonValueId[i];
                                if (lstControl.SelectedValue == addonValueId[i])
                                {
                                    break;
                                }
                            }
                        }

                        ControlPlaceHolder.Controls.Add(lstControl); // Dropdown list control

                        Literal literalSpacer = new Literal();
                        literalSpacer.Text = "<br />";
                        //Add controls to the place holder
                        ControlPlaceHolder.Controls.Add(literalSpacer);
                    }
                }
            }
            # endregion

            //Add the literal control to the Controls collection
            // of the PlaceHolder control.
            literal = new Literal();
            literal.Text = "</div>";
            ControlPlaceHolder.Controls.Add(literal);
        }

    }

    # endregion

    # region Helper Methods
    /// <summary>
    /// check whether catalog image is exists or not
    /// If exists, then return true otherwise false
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    protected bool ShowCatalogImage(string filePath)
    {
        //FileInfo object
        System.IO.FileInfo Info = new System.IO.FileInfo(Server.MapPath(filePath));

        if (Info.Exists) //If file exists
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns boolean value to enable Validator to check against the Quantity textbox
    /// It checks for AllowBackOrder and TrackInventory settings for this product,and returns the value
    /// </summary>
    /// <param name="Value"></param>
    /// <returns></returns>
    protected void EnableStockValidator(ZNodeProduct product)
    {
        _product = product;

        if (_product.ProductID > 0)
        {
            //Allow Back Order
            if (_product.AllowBackOrder && _product.TrackInventoryInd)
            {
                QuantityRangeValidator.Enabled = false;
                return;
            }
            // Don't track inventory
            else if ((!_product.AllowBackOrder) && (!_product.TrackInventoryInd))
            {
                QuantityRangeValidator.Enabled = false;
                return;
            }

            // Enable validator
            CheckInventory();
            QuantityRangeValidator.Enabled = true;
        }
        else
        {
            QuantityRangeValidator.Enabled = false;
        }
    }

    /// <summary>
    /// Returns Quantity on Hand (inventory stock value)
    /// </summary>
    /// <param name="Value"></param>
    /// <returns></returns>
    protected void CheckInventory()
    {
        _product = Product;

        if (_product.ProductID > 0)
        {
            QuantityRangeValidator.MaximumValue = _product.QuantityOnHand.ToString();             
            int CurrentQuantity = _product.QuantityOnHand;

            //Retreive shopping cart object from current session
            shoppingCart = ZNodeShoppingCart.CurrentShoppingCart();            

            if (shoppingCart != null)
            {
                ZNodeShoppingCartItem Item = new ZNodeShoppingCartItem();
                Item.Product = _product;

                //Get Current Qunatity by Subtracting QunatityOrdered from Quantity On Hand(Inventory)
                CurrentQuantity -= shoppingCart.GetQuantityOrdered(Item);
            }

            if (CurrentQuantity <= 0)
            {
                CurrentQuantity = 0;
            }

            QuantityRangeValidator.MaximumValue = CurrentQuantity.ToString();
        }
        else
        {
            QuantityRangeValidator.MaximumValue = "1";
        }
    }    

    /// <summary>
    /// Validate the selected product
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    private bool ValidateProduct(ZNodeProduct product)
    {
        if (product != null)
        {
            if (product.ZNodeAttributeTypeCollection.Count == 0)
            {
                if (product.QuantityOnHand == 0 && product.ProductID > 0 && (!product.AllowBackOrder) && product.TrackInventoryInd)
                {
                    uxStatus.Text = product.OutOfStockMsg;
                    return false;
                }
            }
            else
            {
                string attributeIds = "";

                for (int i = 0; i < product.ZNodeAttributeTypeCollection.Count; i++)
                {
                    if (attributeIds.Length > 0)
                    {
                        attributeIds += ",";
                    }
                    attributeIds += "0";
                }

                if (product.SelectedSKU.SKUID == 0 || product.SelectedSKU.QuantityOnHand == 0)
                {
                    if (product.SelectedSKU.AttributesValue == attributeIds)
                    {
                        uxStatus.Text = "Please select another color,size or width.";
                    }
                    else if (product.SelectedSKU.AttributesValue.Contains("0"))
                    {
                        uxStatus.Text = "Please select another color,size or width.";
                    }
                    else
                    {
                        uxStatus.Text = product.OutOfStockMsg;
                    }
                    return false;
                }
            }

            if (product.ZNodeAddOnCollection.Count > 0)
            {
                # region Check Addons
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.Text.StringBuilder seletecAddOns = new System.Text.StringBuilder();
                System.Text.StringBuilder optionalAddOns = new System.Text.StringBuilder();

                foreach (ZNodeAddOn AddOn in product.ZNodeAddOnCollection)
                {
                    if (sb.Length > 0)
                    {
                        if (!AddOn.OptionalInd)
                        {
                            sb.Append(",");
                        }
                    }

                    if (optionalAddOns.Length > 0)
                    {
                        optionalAddOns.Append(",");
                    }

                    if (AddOn.OptionalInd)
                    {
                        optionalAddOns.Append(AddOn.AddOnID);
                    }
                    else { sb.Append(AddOn.AddOnID); optionalAddOns.Append(AddOn.AddOnID); }
                }

                foreach (ZNodeAddOn AddOn in product.SelectedAddOnItems.ZNodeAddOnCollection)
                {
                    if (seletecAddOns.Length > 0)
                    {
                        seletecAddOns.Append(",");
                    }

                    seletecAddOns.Append(AddOn.AddOnID);
                }

                if (sb.ToString() == seletecAddOns.ToString() || seletecAddOns.ToString() == optionalAddOns.ToString())
                { }
                else
                {
                    uxStatus.Text = "Select required Add-On" + "<br />";
                    return false;
                }
                # endregion

                foreach (ZNodeAddOn AddOn in product.SelectedAddOnItems.ZNodeAddOnCollection)
                {
                    //Loop through the Add-on values for each Add-on
                    foreach (ZNodeAddOnValue AddOnValue in AddOn.ZNodeAddOnValueCollection)
                    {
                        //Check for quantity on hand and back-order,track inventory settings
                        if (AddOnValue.QuantityOnHand <= 0 && (!AddOn.AllowBackOrder) && AddOn.TrackInventoryInd)
                        {
                            uxStatus.Text = "A Required Add-On \'" + AddOn.Name + "\' is out of stock" + "<br />";
                            return false;
                        }
                    }
                }
            }

            if (product.ProductID > 0)
                return true;
        }

        return false;
    }

    /// <summary>
    /// validate selected addons and returns the Inventory related messages
    /// </summary>
    protected string BindStatusMsg(ZNodeAddOn AddOn, ZNodeAddOnValue AddOnValue)
    {
        //If quantity available is less and track inventory is enabled
        if (AddOnValue.QuantityOnHand <= 0 && AddOn.AllowBackOrder == false && AddOn.TrackInventoryInd)
        {
            return AddOn.OutOfStockMsg;
        }
        else if (AddOnValue.QuantityOnHand <= 0 && AddOn.AllowBackOrder == true && AddOn.TrackInventoryInd)
        {
            return AddOn.BackOrderMsg;
        }
        else if (AddOn.TrackInventoryInd && AddOnValue.QuantityOnHand > 0)
        {
            return AddOn.InStockMsg;
        }
        //Don't track Inventory
        //Items can always be added to the cart,TrackInventory is disabled
        else if (AddOn.AllowBackOrder == false && AddOn.TrackInventoryInd == false)
        {
            return AddOn.InStockMsg;
        }

        return string.Empty;
    }

    /// <summary>
    /// Show product price
    /// </summary>
    protected void DisplayPrice()
    {
        decimal unitPrice = 0;
        decimal extendedPrice = 0;
        int shoppingCartQuantity = int.Parse(Qty.Text);

        if (_product.ProductID > 0)
        {
            unitPrice = _product.FinalPrice + _product.AddOnPrice;
            extendedPrice = unitPrice * shoppingCartQuantity;
        }

        lblUnitPrice.Text = unitPrice.ToString("c");
        lblTotalPrice.Text = extendedPrice.ToString("c");
    }


    /// <summary>
    /// 
    /// </summary>
    private void AddToCart()
    {
        // Get product object from Cache object
        _product = Product;
        int Quantity = int.Parse(Qty.Text.Trim());

        if (!ValidateProduct(_product) && Quantity > 0 && _product.ProductID > 0)
        {
            DisplayProductGrid(_product);
            return;
        }

        // Retreive shopping cart object from current session
        shoppingCart = ZNodeShoppingCart.CurrentShoppingCart();

        if (shoppingCart == null)
        {
            shoppingCart = new ZNodeShoppingCart();
            shoppingCart.AddToSession(ZNodeSessionKeyType.ShoppingCart);
        }

        // Add these item if Quantity is greater than zero
        if (Quantity > 0 && _product.ProductID > 0)
        {
            ZNodeShoppingCartItem Item = new ZNodeShoppingCartItem();
            Item.Quantity = Quantity;
            Item.Product = new ZNodeProductBase(_product);

            // add item to cart
            shoppingCart.AddToCart(Item);
        }
    }
    # endregion            
}
