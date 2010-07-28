using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.ECommerce.ShoppingCart;

/// <summary>
/// Summary description for BuyPageBase.cs
/// </summary>
public class BuyPageBase : ZNodePageBase
{
    # region Protected Member Variables
    string SKU = "";
    string _productNum = "";
    int ProductID = 0;
    int _quantity = 1;
    ZNodeProduct _product = null;
    # endregion

    # region General Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //this.MasterPageFile = "~/themes/" + ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.Theme + "/shoppingcart/OneClickAddToCart.master"; 
    }

   
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // get product sku from querystring 
        if (Request.Params["sku"] != null)
        {
            SKU = Request.Params["sku"];
        }

        // get product num # from querystring 
        if (Request.QueryString["product_num"] != null)
        {
            _productNum = Request.QueryString["product_num"];
        }

        // get quantity from querystring 
        if (Request.Params["quantity"] != null)
        {
            if (!int.TryParse(Request.Params["quantity"],out _quantity))
            {
                _quantity = 1;
            }
        }

        if (SKU.Length == 0 && _productNum.Length == 0)
        {
            Response.Redirect("~/error.aspx");
            return;
        }

        if (!Page.IsPostBack)
        {
            ZNodeSKU productSKU = new ZNodeSKU();

            if (SKU.Length > 0)
            {   
                productSKU = ZNodeSKU.CreateBySKU(SKU);

                if (productSKU.SKUID == 0)
                {
                    ProductService productService = new ProductService();
                    ProductQuery filters = new ProductQuery();
                    filters.AppendEquals(ProductColumn.SKU, SKU);
                    TList<Product> productList = productService.Find(filters.GetParameters());

                    if (productList != null)
                    {
                        if (productList.Count == 0)
                        {
                            // If SKUID or Invalid SKU is Zero then Redirected to default page                            
                            Response.Redirect("~/error.aspx");
                        }
                    }
                    else
                    {
                        // If SKUID or Invalid SKU is Zero then Redirected to default page                        
                        Response.Redirect("~/error.aspx");
                    }

                    ProductID = productList[0].ProductID;

                    _product = ZNodeProduct.Create(ProductID, ZNodeConfigManager.SiteConfig.PortalID);
                }
                else
                {
                    // If SKU parameter is supplied & it has value                    
                    ProductID = productSKU.ProductID;

                    _product = ZNodeProduct.Create(ProductID, ZNodeConfigManager.SiteConfig.PortalID);

                    // Set product SKU
                    _product.SelectedSKU = productSKU;
                }
            }
            else if (_productNum.Length > 0)
            {
                ProductService productService = new ProductService();
                ProductQuery filters = new ProductQuery();
                filters.AppendEquals(ProductColumn.ProductNum, _productNum);
                TList<Product> productList = productService.Find(filters.GetParameters());

                if (productList != null)
                {
                    if (productList.Count == 0)
                    {
                        // If SKUID or Invalid SKU is Zero then Redirected to default page                        
                        Response.Redirect("~/error.aspx");
                    }
                }
                else
                {
                    // If SKUID or Invalid SKU is Zero then Redirected to default page                    
                    Response.Redirect("~/error.aspx");
                }

                ProductID = productList[0].ProductID;

                _product = ZNodeProduct.Create(ProductID, ZNodeConfigManager.SiteConfig.PortalID);
            }
            else
            {
                // If SKUID or Invalid SKU is Zero then Redirected to default page                
                Response.Redirect("~/error.aspx");
            }

            // Check product attributes count
            if (_product.ZNodeAttributeTypeCollection.Count > 0 && productSKU.SKUID == 0)
            {
                Response.Redirect(_product.ViewProductLink);
            }

            // Loop through the product addons
            foreach (ZNodeAddOn _addOn in _product.ZNodeAddOnCollection)
            {
                if (!_addOn.OptionalInd)
                {
                    Response.Redirect(_product.ViewProductLink);
                }
            }

            // Create shopping cart item
            ZNodeShoppingCartItem item = new ZNodeShoppingCartItem();
            item.Product = new ZNodeProductBase(_product);
            item.Quantity = _quantity;

            // Add product to cart
            ZNodeShoppingCart shoppingCart = ZNodeShoppingCart.CurrentShoppingCart();
            
            // If shopping cart is null, create in session
            if (shoppingCart == null)
            {
                shoppingCart = new ZNodeShoppingCart();
                shoppingCart.AddToSession(ZNodeSessionKeyType.ShoppingCart);
            }

            // add item to cart
            if (shoppingCart.AddToCart(item))
            {
                string link = "~/shoppingcart.aspx";
                Response.Redirect(link);
            }
            else
            {
                // If Product is out of Stock - Redirected to Product Details  page                
                Response.Redirect("~/error.aspx");
            }           
        }
    }
    # endregion
}
