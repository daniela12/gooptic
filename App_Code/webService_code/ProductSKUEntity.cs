using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ProductSKUEntity
/// Use this class to set properties to upload Inventory Level and Retail price of the products
/// </summary>
public class ProductSKUEntity
{
    private string _productNum;
    private string _Sku;
    private decimal _RetailPrice;
    private decimal? _SalePrice;
    private decimal? _WholeSalePrice;
    private int _QuantityOnHand;

    public ProductSKUEntity()
    {
    }

    # region Public Properties

    /// <summary>
    /// Retrieves or sets the product code
    /// </summary>

    public string ProductNum
    {
        get
        {
            return _productNum;
        }
        set
        {
            _productNum = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the product SKU
    /// </summary>

    public string SKU
    {
        get
        {
            return _Sku;
        }
        set
        {
            _Sku = value;
        }
    }

    /// <summary>
    /// Returns the retail price.
    /// </summary>    
    public decimal RetailPrice
    {
        get
        {
            return _RetailPrice;
        }
        set
        {
            _RetailPrice = value;
        }
    }

    /// <summary>
    /// Returns the SalePrice.
    /// </summary>
    public decimal? SalePrice
    {
        get
        {
            return _SalePrice;
        }
        set
        {
            _SalePrice = value;
        }
    }

    /// <summary>
    /// Returns the Wholesaleprice
    /// </summary>
    public decimal? WholesalePrice
    {
        get
        {
            return _WholeSalePrice;
        }
        set
        {
            _WholeSalePrice = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the QuantityOnHand for this product
    /// </summary>    
    public int QuantityOnHand
    {
        get
        {
            return _QuantityOnHand;
        }
        set
        {
            _QuantityOnHand = value;
        }
    }
    #endregion
}
