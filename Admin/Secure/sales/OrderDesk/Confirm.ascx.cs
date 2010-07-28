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
using ZNode.Libraries.ECommerce.Catalog;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.ECommerce.Fulfillment;
using ZNode.Libraries.ECommerce.ShoppingCart;
using ZNode.Libraries.ECommerce.Analytics;

/// <summary>
/// Checkout confirmation
/// </summary>
public partial class Confirm : System.Web.UI.UserControl
{
    #region Private Variables
    private ZNodeOrderFullfillment _Order = new ZNodeOrderFullfillment();
    private string _siteName = string.Empty;
    private string _receiptText = string.Empty;
    private string _customerServiceEmail = string.Empty;
    private string _customerServicePhoneNumber = string.Empty;    
    protected string _receiptTemplate = string.Empty;
    private ZNodeShoppingCart _shoppingCart = ZNodeShoppingCart.CurrentShoppingCart();
    #endregion

    #region Public Properties
    /// <summary>
    /// 
    /// </summary>
    public ZNodeOrderFullfillment Order
    {
        get
        {
            return _Order;
        }
        set
        {
            _Order = value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public string SiteName
    {
        get
        {
            return _siteName;
        }
        set
        {
            _siteName = value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public string ReceiptText
    {
        get
        {
            return _receiptText;
        }
        set
        {
            _receiptText = value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public string CustomerServiceEmail
    {
        get
        {
            return _customerServiceEmail;
        }
        set
        {
            _customerServiceEmail = value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public string CustomerServicePhoneNumber
    {
        get
        {
            return _customerServicePhoneNumber;
        }
        set
        {
            _customerServicePhoneNumber = value;
        }
    }
   
    /// <summary>
    /// Returns customer feedback url
    /// </summary>
    public string FeedBackUrl
    {
        get
        {
            return Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath  + "/CustomerFeedback.aspx";
        }        
    }
    #endregion

    #region Page Load
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {  
        // Override the default page analytics so we can include the Receipt Page specific info.
        ZNodeAnalytics analytics = new ZNodeAnalytics();
        analytics.AnalyticsData.IsOrderReceiptPage = true;
        analytics.Bind(_Order);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Generate the order receipt using XSL Transformation
    /// </summary>
    public void GenerateReceipt()
    {
        string templatePath = Server.MapPath(ZNodeConfigManager.EnvironmentConfig.ConfigPath + "Receipt.xsl");

        XmlDocument xmlDoc = new XmlDocument();
        XmlElement rootElement = GetElement(xmlDoc, "Order", "");

        rootElement.AppendChild(GetElement(xmlDoc, "SiteName", ZNodeConfigManager.SiteConfig.CompanyName));
        rootElement.AppendChild(GetElement(xmlDoc, "AccountId", _Order.AccountID.ToString()));
        rootElement.AppendChild(GetElement(xmlDoc, "ReceiptText", ZNodeConfigManager.MessageConfig.GetMessage("OrderReceiptConfirmationIntroText")));
        rootElement.AppendChild(GetElement(xmlDoc, "CustomerServiceEmail", ZNodeConfigManager.SiteConfig.CustomerServiceEmail));
        rootElement.AppendChild(GetElement(xmlDoc, "CustomerServicePhoneNumber", ZNodeConfigManager.SiteConfig.CustomerServicePhoneNumber));
        rootElement.AppendChild(GetElement(xmlDoc, "FeedBack", FeedBackUrl));

        if (_Order.CardTransactionID != null)
        {
            rootElement.AppendChild(GetElement(xmlDoc, "TransactionID", _Order.CardTransactionID));
        }
        rootElement.AppendChild(GetElement(xmlDoc, "PromotionCode", _Order.CouponCode));
        rootElement.AppendChild(GetElement(xmlDoc, "PONumber", _Order.PurchaseOrderNumber));
        rootElement.AppendChild(GetElement(xmlDoc, "OrderId", _Order.OrderID.ToString()));
        rootElement.AppendChild(GetElement(xmlDoc, "OrderDate", _Order.OrderDate.ToString()));

        rootElement.AppendChild(GetElement(xmlDoc, "ShippingName", _shoppingCart.Shipping.ShippingName));
        rootElement.AppendChild(GetElement(xmlDoc, "PaymentName", _shoppingCart.Payment.PaymentName));

        rootElement.AppendChild(GetElement(xmlDoc, "ShippingAddressCompanyName", _Order.ShippingAddress.CompanyName));
        rootElement.AppendChild(GetElement(xmlDoc, "ShippingAddressFirstName", _Order.ShippingAddress.FirstName));
        rootElement.AppendChild(GetElement(xmlDoc, "ShippingAddressLastName", _Order.ShippingAddress.LastName));
        rootElement.AppendChild(GetElement(xmlDoc, "ShippingAddressStreet1", _Order.ShippingAddress.Street1));
        rootElement.AppendChild(GetElement(xmlDoc, "ShippingAddressStreet2", _Order.ShippingAddress.Street2));
        rootElement.AppendChild(GetElement(xmlDoc, "ShippingAddressCity", _Order.ShippingAddress.City));
        rootElement.AppendChild(GetElement(xmlDoc, "ShippingAddressStateCode", _Order.ShippingAddress.StateCode));
        rootElement.AppendChild(GetElement(xmlDoc, "ShippingAddressPostalCode", _Order.ShippingAddress.PostalCode));
        rootElement.AppendChild(GetElement(xmlDoc, "ShippingAddressCountryCode", _Order.ShippingAddress.CountryCode));
        rootElement.AppendChild(GetElement(xmlDoc, "ShippingAddressPhoneNumber", _Order.ShippingAddress.PhoneNumber));

        rootElement.AppendChild(GetElement(xmlDoc, "BillingAddressCompanyName", _Order.BillingAddress.CompanyName));
        rootElement.AppendChild(GetElement(xmlDoc, "BillingAddressFirstName", _Order.BillingAddress.FirstName));
        rootElement.AppendChild(GetElement(xmlDoc, "BillingAddressLastName", _Order.BillingAddress.LastName));
        rootElement.AppendChild(GetElement(xmlDoc, "BillingAddressStreet1", _Order.BillingAddress.Street1));
        rootElement.AppendChild(GetElement(xmlDoc, "BillingAddressStreet2", _Order.BillingAddress.Street2));
        rootElement.AppendChild(GetElement(xmlDoc, "BillingAddressCity", _Order.BillingAddress.City));
        rootElement.AppendChild(GetElement(xmlDoc, "BillingAddressStateCode", _Order.BillingAddress.StateCode));
        rootElement.AppendChild(GetElement(xmlDoc, "BillingAddressPostalCode", _Order.BillingAddress.PostalCode));
        rootElement.AppendChild(GetElement(xmlDoc, "BillingAddressCountryCode", _Order.BillingAddress.CountryCode));
        rootElement.AppendChild(GetElement(xmlDoc, "BillingAddressPhoneNumber", _Order.BillingAddress.PhoneNumber));


        XmlElement items = xmlDoc.CreateElement("Items");


        foreach (ZNodeShoppingCartItem shoppingCartItem in _shoppingCart.ShoppingCartItems)
        {
            XmlElement item = xmlDoc.CreateElement("Item");
            item.AppendChild(GetElement(xmlDoc, "Quantity", shoppingCartItem.Quantity.ToString()));

            ZNodeGenericCollection<ZNodeDigitalAsset> assetCollection = shoppingCartItem.Product.ZNodeDigitalAssetCollection;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(shoppingCartItem.Product.Name + "<br />");

            if (assetCollection.Count > 0)
            {
                sb.Append("DownloadLink : <a href='" + shoppingCartItem.Product.DownloadLink + "' target='_blank'>" + shoppingCartItem.Product.DownloadLink + "</a><br />");

                foreach (ZNodeDigitalAsset digitalAsset in assetCollection)
                {
                    sb.Append(digitalAsset.DigitalAsset + "<br />");
                }
            }

            item.AppendChild(GetElement(xmlDoc, "Name", sb.ToString()));
            item.AppendChild(GetElement(xmlDoc, "SKU", shoppingCartItem.Product.SKU));
            item.AppendChild(GetElement(xmlDoc, "Description", shoppingCartItem.Product.ShoppingCartDescription));
            item.AppendChild(GetElement(xmlDoc, "Note", ""));
            item.AppendChild(GetElement(xmlDoc, "Price", shoppingCartItem.UnitPrice.ToString("c") + ZNodeCurrencyManager.GetCurrencySuffix()));
            item.AppendChild(GetElement(xmlDoc, "Extended", shoppingCartItem.ExtendedPrice.ToString("c") + ZNodeCurrencyManager.GetCurrencySuffix()));

            items.AppendChild(item);
        }

        rootElement.AppendChild(GetElement(xmlDoc, "TaxCostValue", _Order.TaxCost.ToString()));
        rootElement.AppendChild(GetElement(xmlDoc, "SalesTaxValue", _Order.SalesTax.ToString()));
        rootElement.AppendChild(GetElement(xmlDoc, "VATCostValue", _Order.VAT.ToString()));
        rootElement.AppendChild(GetElement(xmlDoc, "HSTCostValue", _Order.HST.ToString()));
        rootElement.AppendChild(GetElement(xmlDoc, "PSTCostValue", _Order.PST.ToString()));
        rootElement.AppendChild(GetElement(xmlDoc, "GSTCostValue", _Order.GST.ToString()));


        rootElement.AppendChild(GetElement(xmlDoc, "SubTotal", _Order.SubTotal.ToString("c") + ZNodeCurrencyManager.GetCurrencySuffix()));
        rootElement.AppendChild(GetElement(xmlDoc, "TaxCost", _Order.TaxCost.ToString("c") + ZNodeCurrencyManager.GetCurrencySuffix()));
        rootElement.AppendChild(GetElement(xmlDoc, "SalesTax", _Order.SalesTax.ToString("c") + ZNodeCurrencyManager.GetCurrencySuffix()));
        rootElement.AppendChild(GetElement(xmlDoc, "VAT", _Order.VAT.ToString("c") + ZNodeCurrencyManager.GetCurrencySuffix()));
        rootElement.AppendChild(GetElement(xmlDoc, "HST", _Order.HST.ToString("c") + ZNodeCurrencyManager.GetCurrencySuffix()));
        rootElement.AppendChild(GetElement(xmlDoc, "PST", _Order.PST.ToString("c") + ZNodeCurrencyManager.GetCurrencySuffix()));
        rootElement.AppendChild(GetElement(xmlDoc, "GST", _Order.GST.ToString("c") + ZNodeCurrencyManager.GetCurrencySuffix()));

        //
        rootElement.AppendChild(GetElement(xmlDoc, "DiscountAmount", "-" + _Order.DiscountAmount.ToString("c") + ZNodeCurrencyManager.GetCurrencySuffix()));
        rootElement.AppendChild(GetElement(xmlDoc, "ShippingName", _shoppingCart.Shipping.ShippingName));
        rootElement.AppendChild(GetElement(xmlDoc, "ShippingCost", _Order.ShippingCost.ToString("c") + ZNodeCurrencyManager.GetCurrencySuffix()));
        rootElement.AppendChild(GetElement(xmlDoc, "TotalCost", _Order.Total.ToString("c") + ZNodeCurrencyManager.GetCurrencySuffix()));        
        
        rootElement.AppendChild(GetElement(xmlDoc, "AdditionalInstructions", _Order.AdditionalInstructions));

        rootElement.AppendChild(items);
        xmlDoc.AppendChild(rootElement);

        // Use a memorystream to store the result into the memory
        MemoryStream ms = new MemoryStream();

        XslCompiledTransform xsl = new XslCompiledTransform();
       
        xsl.Load(templatePath);       

        xsl.Transform(xmlDoc, null, ms);

        // Move to the begining 
        ms.Seek(0, SeekOrigin.Begin);

        // Pass the memorystream to a streamreader
        StreamReader sr = new StreamReader(ms);

        _receiptTemplate = sr.ReadToEnd();
    }

    /// <summary>
    /// 
    /// </summary>
    public void ResetReceiptTemplate()
    {
        _receiptTemplate = "";
    }
    #endregion

    # region Helper Methods
    /// <summary>
    /// Creates an XML Element
    /// </summary>
    /// <param name="xmlDoc"></param>
    /// <param name="elementName"></param>
    /// <param name="elementValue"></param>
    /// <returns></returns>
    private XmlElement GetElement(XmlDocument xmlDoc, string elementName, string elementValue)
    {
        XmlElement elmt = xmlDoc.CreateElement(elementName);
        if (elementValue.Length > 0)
        {
            elmt.InnerText = elementValue;
        }
        return elmt;
    }   
    #endregion
}
