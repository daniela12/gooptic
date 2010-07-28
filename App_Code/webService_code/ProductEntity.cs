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
/// Summary description for ProductEntity
/// </summary>
public class ProductEntity
{
    public ProductEntity()
    {
    }

    #region Private Variables
    private int _ProductID = 0;
    private int _PortalID = 0;
    private string _Name = string.Empty;
    private string _Description = string.Empty;
    private string _FeaturesDesc = string.Empty;
    private string _ProductNum = string.Empty;
    private string _ProductTypeName;
    private decimal _RetailPrice;
    private decimal? _WholesalePrice;
    private string _ImageFile;
    private decimal _Weight;
    private bool _IsActive;
    private int _DisplayOrder;
    private string _Custom1 = string.Empty;
    private string _Custom2 = string.Empty;
    private string _ShortDesc = string.Empty;
    private bool _CallForPricing;
    private string _CallMessage = string.Empty;
    private bool _HomepageSpecial;
    private bool _CategorySpecial;
    private bool _Hide;
    private int _InventoryDisplay;
    private string _Keywords = string.Empty;
    private string _ManufacturerName;
    private string _ManufacturerPartNum = string.Empty;
    private string _AdditionalInfoLink = string.Empty;
    private string _AdditionalInfoLinkLabel = string.Empty;
    private int? _ShippingRuleTypeID;
    private string _Specifications = string.Empty;
    private string _AdditionalInformation = string.Empty;
    private string _BackOrderMsg = string.Empty;
    private string _InStockMsg = string.Empty;
    private string _OutOfStockMsg = string.Empty;
    private decimal? _SalePrice;
    private string _SEOTitle = string.Empty;
    private string _SEOKeywords = string.Empty;
    private string _SEODescription = string.Empty;
    private string _addOnDescription = string.Empty;
    private bool _TrackInventoryInd;
    private bool _AllowBackOrder;
    private int _QuantityOnHand;
    private string _Sku;
    #endregion

    # region Public Properties

    /// <summary>
    /// Retrieves or sets the ProductID
    /// </summary>
    
    public int ProductID
    {
        get
        {
            return _ProductID;
        }
        set
        {
            _ProductID = value;
        }
    }    

    /// <summary>
    /// Retrieves or sets the site portal id
    /// </summary>
    
    public int PortalID
    {
        get
        {
            return _PortalID;
        }
        set
        {
            _PortalID = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the product Name
    /// </summary>    
    public string Name
    {
        get
        {
            return _Name;
        }
        set
        {
            _Name = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the product description
    /// </summary>
    
    public string Description
    {
        get
        {
            return _Description;
        }
        set
        {
            _Description = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the product features
    /// </summary>
    
    public string FeaturesDesc
    {
        get
        {
            return _FeaturesDesc;
        }
        set
        {
            _FeaturesDesc = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the product code
    /// </summary>
    
    public string ProductNum
    {
        get
        {
            return _ProductNum;
        }
        set
        {
            _ProductNum = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the product type id
    /// </summary>
    
    public string ProductTypeName
    {
        get
        {
            return _ProductTypeName;
        }
        set
        {
            _ProductTypeName = value;
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
    /// Returns the wholesale price.
    /// </summary>
    
    public decimal? WholesalePrice
    {
        get
        {            
               return _WholesalePrice;            
        }
        set
        {
            _WholesalePrice = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the product sale price as a decimal value
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
    /// Returns the image file name for this product. Will return the SKU picture override if one exists.
    /// </summary>
    
    public string ImageFile
    {
        get
        {           
            return _ImageFile;         
        }
        set
        {
            _ImageFile = value;
        }
    }   
   

    /// <summary>
    /// Retrieves or sets the weight of this product 
    /// </summary>
    
    public decimal Weight
    {
        get
        {            
            return _Weight;            
        }
        set
        {
            _Weight = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the Active property as a boolean value
    /// </summary>
    
    public bool IsActive
    {
        get
        {
            return _IsActive;
        }
        set
        {
            _IsActive = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the display order for this product
    /// </summary>
    
    public int DisplayOrder
    {
        get
        {
            return _DisplayOrder;
        }
        set
        {
            _DisplayOrder = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the custom comments for this product
    /// </summary>
    
    public string Custom1
    {
        get
        {
            return _Custom1;
        }
        set
        {
            _Custom1 = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the custom comments for this product
    /// </summary>
    
    public string Custom2
    {
        get
        {
            return _Custom2;
        }
        set
        {
            _Custom2 = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the custom comments for this product
    /// </summary>
    
    public string ShortDesc
    {
        get
        {
            return _ShortDesc;
        }
        set
        {
            _ShortDesc = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the CallForPricing property for this product
    /// </summary>
    
    public bool CallForPricing
    {
        get
        {
            return _CallForPricing;
        }
        set
        {
            _CallForPricing = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the call for pricing text message
    /// </summary>
    public string CallMessage
    {
        get
        {
            return _CallMessage;
        }
        set
        {
            _CallMessage = value;
        }
    }

    /// <summary>
    /// Retrieves or sets to HomepageSpecial property
    /// </summary>
    
    public bool HomepageSpecial
    {
        get
        {
            return _HomepageSpecial;
        }
        set
        {
            _HomepageSpecial = value;
        }
    }

    /// <summary>
    /// Sets or retrieves the CategorySpecial property
    /// </summary>
    
    public bool CategorySpecial
    {
        get
        {
            return _CategorySpecial;
        }
        set
        {
            _CategorySpecial = value;
        }
    }

    /// <summary>
    /// Sets or retrieves the Hide option
    /// </summary>
    
    public bool Hide
    {
        get
        {
            return _Hide;
        }
        set
        {
            _Hide = value;
        }
    }

    /// <summary>
    /// Sets or retrieves the inventory display value for this product
    /// </summary>
    
    public int InventoryDisplay
    {
        get
        {
            return _InventoryDisplay;
        }
        set
        {
            _InventoryDisplay = value;
        }
    }

    /// <summary>        
    /// Sets or retrieves the manufacturer name for this product        
    /// </summary>
    
    public string ManufacturerName
    {
        get
        {
            return _ManufacturerName;
        }
        set
        {
            _ManufacturerName = value;
        }
    }

    /// <summary>
    /// Sets or retrieves the manufacturer part number for this product
    /// </summary>
    
    public string ManufacturerPartNum
    {
        get
        {
            return _ManufacturerPartNum;
        }
        set
        {
            _ManufacturerPartNum = value;
        }
    }

    /// <summary>
    /// Sets or retrieves the additional information link for this product
    /// </summary>
    
    public string AdditionalInfoLink
    {
        get
        {
            return _AdditionalInfoLink;
        }
        set
        {
            _AdditionalInfoLink = value;
        }
    }

    /// <summary>
    /// Sets or retrieves the additional link labale text for this product
    /// </summary>
    
    public string AdditionalInfoLinkLabel
    {
        get
        {
            return _AdditionalInfoLinkLabel;
        }
        set
        {
            _AdditionalInfoLinkLabel = value;
        }
    }

    /// <summary>
    /// Sets or retrieves the ShippingRuleTypeID for this product
    /// </summary>
    
    public int? ShippingRuleTypeID
    {
        get
        {
            return _ShippingRuleTypeID;
        }
        set
        {
            _ShippingRuleTypeID = value;
        }
    }
    /// <summary>
    /// Sets or retrieves the SEO Keywords
    /// </summary>
    
    public string SEOKeywords
    {
        get
        {
            return _SEOKeywords;
        }
        set
        {
            _SEOKeywords = value;
        }
    }

    /// <summary>
    /// Sets or retrieves the SEO Title
    /// </summary>
    
    public string SEOTitle
    {
        get
        {
            return _SEOTitle;
        }
        set
        {
            _SEOTitle = value;
        }
    }

    /// <summary>
    /// Sets or retrieves the SEO Description
    /// </summary>
    
    public string SEODescription
    {
        get
        {
            return _SEODescription;
        }
        set
        {
            _SEODescription = value;
        }
    }

    /// <summary>
    /// Sets or retrieves the Specification
    /// </summary>
    
    public string Specifications
    {
        get
        {
            return _Specifications;
        }
        set
        {
            _Specifications = value;
        }
    }


    /// <summary>
    /// Sets or retrieves the Additional Information
    /// </summary>
    
    public string AdditionalInformation
    {
        get
        {
            return _AdditionalInformation;
        }
        set
        {
            _AdditionalInformation = value;
        }
    }


    /// <summary>
    /// Retrieves or sets the BackOrder Message
    /// </summary>
    
    public string BackOrderMsg
    {
        get
        {
            return _BackOrderMsg;
        }
        set
        {
            _BackOrderMsg = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the InStock Message
    /// </summary>
    
    public string InStockMsg
    {
        get
        {
            return _InStockMsg;
        }
        set
        {
            _InStockMsg = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the OutOfStock Message
    /// </summary>
    
    public string OutOfStockMsg
    {
        get
        {
            return _OutOfStockMsg;
        }
        set
        {
            _OutOfStockMsg = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the keywords
    /// </summary>

    public string Keywords
    {
        get
        {
            return _Keywords;
        }
        set
        {
            _Keywords = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the TrackInventoryInd property for this product
    /// </summary>    
    public bool TrackInventoryInd
    {
        get
        {
            return _TrackInventoryInd;
        }
        set
        {
            _TrackInventoryInd = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the AllowBackOrder property for this product
    /// </summary>    
    public bool AllowBackOrder
    {
        get
        {
            return _AllowBackOrder;
        }
        set
        {
            _AllowBackOrder = value;
        }
    }

    /// <summary>
    /// Retrieves or sets the QuantityOnHand for this product
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
    # endregion
}
