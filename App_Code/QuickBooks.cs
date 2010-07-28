using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.Framework.Business;

# region Enum RequestType
/// <summary>
/// Represents request type
/// </summary>
public enum RequestType
{
    ItemDownload = 1,
    OrderDownload = 2,
    AdjustItemInventory = 3,
    AccountDownload = 4,
}
#endregion

/// <summary>
/// Summary description for QuickBooks
/// </summary>
public class QuickBooks : ZNodeBusinessBase
{
    # region Protected member Variables
    static ArrayList requestList = new ArrayList();
    static ArrayList ItemRequestList = new ArrayList();
    static ArrayList ItemInventoryDownloadRequestList = new ArrayList();
    static ArrayList accountDownloadRequestList = new ArrayList();
    #endregion

    # region Constructor
    public QuickBooks()
    {
    }
    #endregion

    # region Protected Properties
    /// <summary>
    /// Gets the message configuration
    /// </summary>
    protected ZNodeMessageConfig MessageConfig
    {
        get
        {
            if (HttpContext.Current.Session["MessageConfig"] != null)
            {
                return (ZNodeMessageConfig)HttpContext.Current.Session["MessageConfig"];
            }
            else
            {
                string messageConfigPath = HttpContext.Current.Server.MapPath(ZNodeConfigManager.EnvironmentConfig.ConfigPath + "QuickbooksConfig.xml");

                ZNodeMessageConfig messageConfig = new ZNodeMessageConfig(messageConfigPath);

                //Add to session
                HttpContext.Current.Session.Add("MessageConfig", messageConfig);

                return messageConfig;
            }
        }
        set
        {
            HttpContext.Current.Session["MessageConfig"] = value;
        }
    }

    #endregion

    # region Public  properties
    /// <summary>
    /// Removes all the request xml items from the list
    /// </summary>
    public void ClearRequestList(RequestType requestType)
    {
        switch (requestType)
        {
            case RequestType.OrderDownload:
                requestList.Clear();
                break;
            case RequestType.ItemDownload:
                ItemRequestList.Clear();
                break;
            case RequestType.AdjustItemInventory:
                ItemInventoryDownloadRequestList.Clear();
                break;
            case RequestType.AccountDownload:
                accountDownloadRequestList.Clear();
                break;
            default:
                requestList.Clear();
                ItemRequestList.Clear();
                ItemInventoryDownloadRequestList.Clear();
                accountDownloadRequestList.Clear();
                break;
        }
    }
    # endregion

    #region  Request Methods
    /// <summary>
    /// Returns the request xml list based on the type of the request
    /// </summary>
    public ArrayList GetRequestXmlList(RequestType requestType)
    {
        ZNodeMessageConfig messageConfig = MessageConfig;

        // Order download 
        if (requestType == RequestType.OrderDownload)
        {
            if (requestList.Count == 0)
            {
                requestList = new ArrayList();

                OrderService service = new OrderService();
                OrderQuery query = new OrderQuery();
                query.AppendIsNull(OrderColumn.WebServiceDownloadDate);
                TList<Order> orderList = service.Find(query.GetParameters());

                if (orderList != null)
                {
                    service.DeepLoad(orderList, true, DeepLoadType.IncludeChildren, typeof(Account), typeof(TList<OrderLineItem>), typeof(Shipping));
                    BuildOrderRequestXML(orderList, requestList, messageConfig);
                }
            }

            return requestList;
        }
        else if (requestType == RequestType.ItemDownload) //Item list download request type
        {
            if (ItemRequestList.Count == 0)
            {
                ItemRequestList = new ArrayList();

                ZNode.Libraries.DataAccess.Custom.ProductHelper helperAccess = new ZNode.Libraries.DataAccess.Custom.ProductHelper();
                DataSet ItemList = helperAccess.GetItemInventoryList(ZNodeConfigManager.SiteConfig.PortalID);

                foreach (DataRow dr in ItemList.Tables[0].Rows)
                {
                    if (dr["WebServiceDownloadDte"].ToString().Length > 0)
                    {
                        string returnXML = BuildItemQueryRequest("Product" + dr["ProductID"].ToString(), "Contains", dr["SKU"].ToString());

                        //add to request list
                        ItemRequestList.Add(returnXML);
                    }
                    else
                    {
                        BuildProductAddRequestXML(ItemRequestList, dr, messageConfig);
                    }
                }

                foreach (DataRow dr in ItemList.Tables[1].Rows)
                {
                    if (dr["WebServiceDownloadDte"].ToString().Length > 0)
                    {
                        string returnXML = BuildItemQueryRequest("Sku" + dr["SKUID"].ToString(), "Contains", dr["SKU"].ToString());

                        //add to request list
                        ItemRequestList.Add(returnXML);
                    }
                    else
                    {
                        BuildSKUItemAddRequestXML(ItemRequestList, dr, messageConfig);
                    }
                }

                //Loop through each addon value Item in the dataset
                foreach (DataRow dr in ItemList.Tables[2].Rows)
                {
                    if (dr["WebServiceDownloadDte"].ToString().Length > 0)
                    {
                        string returnXML = BuildItemQueryRequest("AddOnValue" + dr["AddOnValueID"].ToString(), "Contains", dr["SKU"].ToString());

                        //add to request list
                        ItemRequestList.Add(returnXML);
                    }
                    else
                    {
                        BuildAddOnValueItemAddRequestXML(ItemRequestList, dr, messageConfig);
                    }
                }
            }

            return ItemRequestList;
        }
        else if (requestType == RequestType.AdjustItemInventory)
        {
            if (ItemInventoryDownloadRequestList.Count == 0)
            {
                ItemInventoryDownloadRequestList = new ArrayList();

                ProductService productService = new ProductService();
                TList<Product> productList = productService.DeepLoadByPortalIDActiveInd
                            (ZNodeConfigManager.SiteConfig.PortalID, true, true, ZNode.Libraries.DataAccess.Data.DeepLoadType.IncludeChildren, typeof(Manufacturer), typeof(TList<SKU>));
                AddOnValueService addOnValueService = new AddOnValueService();
                TList<AddOnValue> addOnValueList = addOnValueService.GetAll();

                //Build bulk adjustment Item Inventory request
                BuildAdjustItemInventoryRequestXML(ItemInventoryDownloadRequestList, productList, addOnValueList, messageConfig);
            }

            return ItemInventoryDownloadRequestList;
        }
        else if (requestType == RequestType.AccountDownload)
        {
            if (accountDownloadRequestList.Count == 0)
            {
                accountDownloadRequestList = new ArrayList();

                AccountService accountService = new AccountService();
                int totalCount = 0;
                int rowsCount = accountService.GetTotalItems("WebServiceDownloadDte is null OR UpdateDte > WebServiceDownloadDte", out totalCount);

                if (rowsCount > 0)
                {
                    TList<Account> accountList = accountService.GetPaged
                                                    ("WebServiceDownloadDte is null OR UpdateDte > WebServiceDownloadDte", "AccountID Asc", 0, rowsCount, out totalCount);

                    foreach (Account accountEntity in accountList)
                    {
                        if (!accountEntity.WebServiceDownloadDte.HasValue)
                        {
                            BuildCustomerAddRequestXML(accountDownloadRequestList, accountEntity, messageConfig);
                        }
                        else if (accountEntity.UpdateDte.GetValueOrDefault(System.DateTime.Today) > accountEntity.WebServiceDownloadDte.Value)
                        {
                            //Build Request Query
                            BuildCustomerQueryRequestXML(accountDownloadRequestList, accountEntity);
                        }
                    }
                }
            }

            return accountDownloadRequestList;
        }


        return new ArrayList();
    }

    #endregion

    # region Receive & Parse Response XML

    /// <summary>
    /// This method parse the QB response xml.
    /// </summary>
    /// <param name="Message"></param>
    public void ReceiveResponse(string Message, out int Count, RequestType ReqType)
    {
        int value = 0;

        if (Regex.IsMatch(Message, "<SalesReceiptAddRs\\s*[^><]*>"))
        {
            UpdateQuickBooksStatus(Message, "SalesReceiptAddRs");
        }
        else if (Regex.IsMatch(Message, "<SalesOrderAddRs\\s*[^><]*>"))
        {
            UpdateQuickBooksStatus(Message, "SalesOrderAddRs");
        }
        else if (Regex.IsMatch(Message, "<CustomerAddRs\\s*[^><]*>"))
        {
            UpdateQuickBooksStatus(Message, "CustomerAddRs");
        }
        else if (Regex.IsMatch(Message, "<CustomerModRs\\s*[^><]*>"))
        {
            UpdateQuickBooksStatus(Message, "CustomerModRs");
        }
        else if (Regex.IsMatch(Message, "<CustomerQueryRs\\s*[^><]*>"))
        {
            value = DownloadCustomerInfo(Message, "CustomerQueryRs", ReqType);
        }
        else if (Regex.IsMatch(Message, "<ItemInventoryAddRs\\s*[^><]*>"))
        {
            UpdateQuickBooksStatus(Message, "ItemInventoryAddRs");
        }
        else if (Regex.IsMatch(Message, "<ItemInventoryModRs\\s*[^><]*>"))
        {
            UpdateQuickBooksStatus(Message, "ItemInventoryModRs");
        }
        else if (Regex.IsMatch(Message, "<ItemInventoryQueryRs\\s*[^><]*>"))
        {
            value = DownloadProduct(Message, "ItemInventoryQueryRs");
        }

        Count = value;
    }

    /// <summary>
    /// Receive QB Customer Query and retireve ListId and EidtSequence tag value to update customer Info
    /// </summary>
    /// <param name="Message"></param>
    /// <param name="rootTagName"></param>
    private int DownloadCustomerInfo(string Message, string RootTagName, RequestType ReqType)
    {
        ZNodeMessageConfig messageConfig = MessageConfig;
        string editSequence = "";
        string listID = "";
        string statusCode = "";

        Match m = Regex.Match(Message, "(?<=<EditSequence[^>]*>).*?(?=</EditSequence>)", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        editSequence = m.Value;

        m = Regex.Match(Message, "(?<=<ListID[^>]*>).*?(?=</ListID>)", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        listID = m.Value;

        string accountID = ParseRequestID(Message, RootTagName, out statusCode);
        if (accountID.Length == 0) { accountID = "0"; }

        if (statusCode == "0")
        {
            AccountService service = new AccountService();
            Account account = service.GetByAccountID(int.Parse(accountID));

            if (account == null)
            {
                return 0;
            }

            if (ReqType == RequestType.AccountDownload)
            {
                //build update query
                BuildCustomerModifyRequestXML(accountDownloadRequestList, account, listID, editSequence, messageConfig);
            }
            else
            {
                //build update query
                BuildCustomerModifyRequestXML(requestList, account, listID, editSequence, messageConfig);
            }
            return 1;
        }

        return 0;
    }

    /// <summary>
    /// Update generic flag "DownloadDate" in the order table with time created in QB
    /// </summary>
    /// <param name="Message"></param>
    private void UpdateQuickBooksStatus(string Message, string rootTagName)
    {
        //find matching time created tag in the response message
        Match m = Regex.Match(Message, "(?<=<TimeModified[^>]*>).*?(?=</TimeModified>)", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        string TimeModified = m.Value;
        string statusCode;

        string entityID = ParseRequestID(Message, rootTagName, out statusCode);
        if (entityID.Length == 0) { entityID = "0"; }

        if (statusCode == "0" && (rootTagName == "SalesReceiptAddRs" || rootTagName == "SalesOrderAddRs"))
        {
            OrderService service = new OrderService();
            Order _order = service.GetByOrderID(int.Parse(entityID));

            if (_order != null)
            {
                _order.WebServiceDownloadDate = DateTime.Parse(TimeModified);
                service.Update(_order);
            }
        }
        else if (statusCode == "0" && (rootTagName == "CustomerAddRs" || rootTagName == "CustomerModRs"))
        {
            AccountService accountService = new AccountService();
            Account entity = accountService.GetByAccountID(int.Parse(entityID));

            if (entity != null)
            {
                entity.WebServiceDownloadDte = DateTime.Parse(TimeModified);
                accountService.Update(entity);
            }
        }
        else if (statusCode == "0" && (rootTagName == "ItemInventoryAddRs" || rootTagName == "ItemInventoryModRs"))
        {
            if (entityID.Contains("Product"))
            {
                entityID = entityID.Replace("Product", "");

                ProductService service = new ProductService();
                Product _product = service.GetByProductID(int.Parse(entityID));

                if (statusCode == "0" && _product != null)
                {
                    _product.WebServiceDownloadDte = DateTime.Parse(TimeModified);
                    service.Update(_product);
                }
            }
            else if (entityID.Contains("Sku"))
            {
                entityID = entityID.Replace("Sku", "");
                SKUService service = new SKUService();
                SKU _sku = service.GetBySKUID(int.Parse(entityID));

                if (statusCode == "0" && _sku != null)
                {
                    _sku.WebServiceDownloadDte = DateTime.Parse(TimeModified);
                    service.Update(_sku);
                }
            }
            else if (entityID.Contains("AddOnValue"))
            {
                entityID = entityID.Replace("AddOnValue", "");
                AddOnValueService service = new AddOnValueService();
                AddOnValue _addOnValue = service.GetByAddOnValueID(int.Parse(entityID));

                if (statusCode == "0" && _addOnValue != null)
                {
                    _addOnValue.WebServiceDownloadDte = DateTime.Parse(TimeModified);
                    service.Update(_addOnValue);
                }
            }
        }
    }

    /// <summary>
    /// Build modify request xml for this item using ListId and Edit Sequence
    /// </summary>
    /// <param name="Message"></param>
    /// <param name="rootTagName"></param>
    /// <returns></returns>
    public int DownloadProduct(string Message, string rootTagName)
    {
        # region Local variables
        ZNodeMessageConfig messageConfig = MessageConfig;

        string editSequence = "";
        string listID = "";
        string statusCode = "";

        string requestID = ParseRequestID(Message, rootTagName, out statusCode);
        if (requestID.Length == 0) { requestID = "0"; }


        Match m = Regex.Match(Message, "(?<=<EditSequence[^>]*>).*?(?=</EditSequence>)", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        editSequence = m.Value;

        m = Regex.Match(Message, "(?<=<ListID[^>]*>).*?(?=</ListID>)", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        listID = m.Value;
        #endregion

        if (requestID.Contains("Product"))
        {
            requestID = requestID.Replace("Product", "");

            ProductService service = new ProductService();
            Product _product = service.GetByProductID(int.Parse(requestID));

            if (_product == null)
            {
                return 0;
            }

            if (statusCode == "0")
            {
                //build modify request xml
                BuildProductModifyRequestXML(ItemRequestList, _product, listID, editSequence, messageConfig);
                //build item inventory adjustment request xml
                BuildAdjustItemInventoryRequestXML(ItemRequestList, _product.SKU, _product.QuantityOnHand.Value, messageConfig);
            }
        }
        else if (requestID.Contains("Sku"))
        {
            requestID = requestID.Replace("Sku", "");
            SKUService service = new SKUService();
            SKU _sku = service.DeepLoadBySKUID(int.Parse(requestID), true,
                                        DeepLoadType.IncludeChildren, typeof(Product));

            if (statusCode == "0")
            {
                //build modify request xml
                BuildItemModifyRequestXML(ItemRequestList, _sku, listID, editSequence, messageConfig);
                //build item inventory adjustment request xml
                BuildAdjustItemInventoryRequestXML(ItemRequestList, _sku.SKU, _sku.QuantityOnHand, messageConfig);
            }
        }
        else if (requestID.Contains("AddOnValue"))
        {
            requestID = requestID.Replace("AddOnValue", "");
            AddOnValueService service = new AddOnValueService();
            AddOnValue _addOnValue = service.GetByAddOnValueID(int.Parse(requestID));

            if (statusCode == "0")
            {
                //build modify request xml
                BuildItemModifyRequestXML(ItemRequestList, _addOnValue, listID, editSequence, messageConfig);
                //build item inventory adjustment request xml
                BuildAdjustItemInventoryRequestXML(ItemRequestList, _addOnValue.SKU, _addOnValue.QuantityOnHand, messageConfig);
            }
        }

        return 1;
    }
    #endregion

    # region Download Orders request methods
    /// <summary>
    /// Build xml request to send it to quick books through web connector
    /// </summary>
    /// <param name="orderList"></param>
    /// <returns></returns>
    private void BuildOrderRequestXML(TList<Order> orderList, ArrayList req, ZNodeMessageConfig messageConfig)
    {
        string strRequestXML = "";
        XmlDocument requestXMLDoc = null;
        ProductService productService = new ProductService();

        foreach (Order order in orderList)
        {
            Account accountEntity = order.AccountIDSource;
            requestXMLDoc = new XmlDocument();
            string customerName = accountEntity.AccountID.ToString();

            if (!accountEntity.WebServiceDownloadDte.HasValue)
            {
                BuildCustomerAddRequestXML(req, accountEntity, messageConfig);
            }
            else if (accountEntity.UpdateDte.GetValueOrDefault(System.DateTime.Today) > accountEntity.WebServiceDownloadDte.Value)
            {
                BuildCustomerQueryRequestXML(req, accountEntity);
            }

            if (messageConfig.GetMessage("OrdersDownloadType") == "SalesOrder")
            {
                BuildSalesOrderRequestXML(requestXMLDoc, customerName, order, messageConfig);
            }
            else
            {
                //Sales Receipt Add Query        
                requestXMLDoc.AppendChild(requestXMLDoc.CreateXmlDeclaration("1.0", null, null));
                requestXMLDoc.AppendChild(requestXMLDoc.CreateProcessingInstruction("qbxml", "version=\"7.0\""));

                XmlElement qbXML = requestXMLDoc.CreateElement("QBXML");
                requestXMLDoc.AppendChild(qbXML);

                XmlElement qbXMLMsgsRq = requestXMLDoc.CreateElement("QBXMLMsgsRq");
                qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
                qbXML.AppendChild(qbXMLMsgsRq);

                XmlElement SalesReceiptAddRq = requestXMLDoc.CreateElement("SalesReceiptAddRq");
                SalesReceiptAddRq.SetAttribute("requestID", order.OrderID.ToString());
                qbXMLMsgsRq.AppendChild(SalesReceiptAddRq);

                XmlElement SalesReceiptAdd = requestXMLDoc.CreateElement("SalesReceiptAdd");
                SalesReceiptAddRq.AppendChild(SalesReceiptAdd);

                //associate customer with this order using User AccountID
                XmlElement CustomerRef = requestXMLDoc.CreateElement("CustomerRef");
                CustomerRef.AppendChild(MakeElement(requestXMLDoc, "FullName", customerName));
                SalesReceiptAdd.AppendChild(CustomerRef);

                string msg = messageConfig.GetMessage("SalesReceiptMessage");
                if (msg != null)
                {
                    //Custom message reference
                    XmlElement CustomerMsgRef = requestXMLDoc.CreateElement("CustomerMsgRef");
                    CustomerMsgRef.AppendChild(MakeElement(requestXMLDoc, "FullName", msg));
                    SalesReceiptAdd.AppendChild(CustomerMsgRef);
                }

                //loop throught each order line item
                foreach (OrderLineItem lineItem in order.OrderLineItemCollection)
                {
                    string ItemRef = lineItem.SKU;
                    string description = lineItem.Name;
                    if (lineItem.ParentOrderLineItemID.HasValue)
                        description = lineItem.Name + " - " + lineItem.Description;

                    SalesReceiptAdd.AppendChild(BuildSaleReceiptLineItem
                               (requestXMLDoc, ItemRef, description, lineItem.Quantity.Value.ToString(), lineItem.Price.Value.ToString("N2")));
                }

                string couponCode = "Discount";
                if (order.CouponCode.Length > 0) { couponCode = " - " + order.CouponCode; }

                //set Tax
                SalesReceiptAdd.AppendChild
                    (BuildSaleReceiptLineItem(requestXMLDoc, messageConfig.GetMessage("SalesTaxItem"), "Tax", " ", order.TaxCost.Value.ToString("N2")));
                //set Discount
                SalesReceiptAdd.AppendChild
                    (BuildSaleReceiptLineItem(requestXMLDoc, messageConfig.GetMessage("DiscountItemName"), couponCode, "", order.DiscountAmount.Value.ToString("N2")));
                //set shipping Info
                string shippingDescription = order.ShippingIDSource.Description.Replace("®", string.Empty);
                SalesReceiptAdd.AppendChild
                    (BuildSaleReceiptLineItem(requestXMLDoc, messageConfig.GetMessage("ShippingItemName"), shippingDescription, "", order.ShippingCost.Value.ToString("N2")));

            }

            //Request xml
            strRequestXML = requestXMLDoc.OuterXml;

            req.Add(strRequestXML);//add it to request list

            strRequestXML = "";
        }
    }

    /// <summary>
    /// Returns xml element for salereceiptLine item
    /// </summary>
    /// <param name="requestXMLDoc"></param>
    /// <param name="ItemRef"></param>
    /// <param name="Description"></param>
    /// <param name="Quantity"></param>
    /// <param name="rate"></param>
    /// <returns></returns>
    private void BuildSalesOrderRequestXML(XmlDocument requestXMLDoc, string customerName, Order entity, ZNodeMessageConfig messageConfig)
    {
        //Sales Receipt Add Query        
        requestXMLDoc.AppendChild(requestXMLDoc.CreateXmlDeclaration("1.0", null, null));
        requestXMLDoc.AppendChild(requestXMLDoc.CreateProcessingInstruction("qbxml", "version=\"7.0\""));

        XmlElement qbXML = requestXMLDoc.CreateElement("QBXML");
        requestXMLDoc.AppendChild(qbXML);

        XmlElement qbXMLMsgsRq = requestXMLDoc.CreateElement("QBXMLMsgsRq");
        qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
        qbXML.AppendChild(qbXMLMsgsRq);

        XmlElement SalesOrderAddRq = requestXMLDoc.CreateElement("SalesOrderAddRq");
        SalesOrderAddRq.SetAttribute("requestID", entity.OrderID.ToString());
        qbXMLMsgsRq.AppendChild(SalesOrderAddRq);

        XmlElement SalesOrderAdd = requestXMLDoc.CreateElement("SalesOrderAdd");
        SalesOrderAddRq.AppendChild(SalesOrderAdd);

        //associate customer with this order using User AccountID
        XmlElement CustomerRef = requestXMLDoc.CreateElement("CustomerRef");
        CustomerRef.AppendChild(MakeElement(requestXMLDoc, "FullName", customerName));
        SalesOrderAdd.AppendChild(CustomerRef);

        string msg = messageConfig.GetMessage("SalesReceiptMessage");
        if (msg != null)
        {
            //Custom message reference
            XmlElement CustomerMsgRef = requestXMLDoc.CreateElement("CustomerMsgRef");
            CustomerMsgRef.AppendChild(MakeElement(requestXMLDoc, "FullName", msg));
            SalesOrderAdd.AppendChild(CustomerMsgRef);
        }

        //loop throught each order line item
        foreach (OrderLineItem lineItem in entity.OrderLineItemCollection)
        {
            string ItemRef = lineItem.SKU;
            string description = lineItem.Name;
            if (lineItem.ParentOrderLineItemID.HasValue)
                description = lineItem.Name + " - " + lineItem.Description;

            SalesOrderAdd.AppendChild(BuildSaleOrderLineItem
                       (requestXMLDoc, ItemRef, description, lineItem.Quantity.Value.ToString(), lineItem.Price.Value.ToString("N2")));
        }

        string couponCode = "Discount";
        if (entity.CouponCode.Length > 0) { couponCode = " - " + entity.CouponCode; }

        //set Tax
        SalesOrderAdd.AppendChild
            (BuildSaleOrderLineItem(requestXMLDoc, messageConfig.GetMessage("SalesTaxItem"), "Tax", "", entity.TaxCost.Value.ToString("N2")));
        //set Discount
        SalesOrderAdd.AppendChild
            (BuildSaleOrderLineItem(requestXMLDoc, messageConfig.GetMessage("DiscountItemName"), couponCode, "", entity.DiscountAmount.Value.ToString("N2")));

        //set shipping Info
        string shippingDescription = entity.ShippingIDSource.Description.Replace("®", string.Empty);
        SalesOrderAdd.AppendChild
            (BuildSaleOrderLineItem(requestXMLDoc, messageConfig.GetMessage("ShippingItemName"), shippingDescription, "", entity.ShippingCost.Value.ToString("N2")));
    }

    /// <summary>
    /// Returns xml element for salereceiptLine item
    /// </summary>
    /// <param name="requestXMLDoc"></param>
    /// <param name="ItemRef"></param>
    /// <param name="Description"></param>
    /// <param name="Quantity"></param>
    /// <param name="rate"></param>
    /// <returns></returns>
    private XmlElement BuildSaleReceiptLineItem(XmlDocument requestXMLDoc, string ItemRef, string Description, string Quantity, string rate)
    {
        XmlElement SalesReceiptLineAdd = requestXMLDoc.CreateElement("SalesReceiptLineAdd");

        XmlElement ItemRefXML = requestXMLDoc.CreateElement("ItemRef");
        ItemRefXML.AppendChild(MakeElement(requestXMLDoc, "FullName", ItemRef));
        SalesReceiptLineAdd.AppendChild(ItemRefXML);

        SalesReceiptLineAdd.AppendChild(MakeElement(requestXMLDoc, "Desc", Description.Replace("<br />", "")));
        SalesReceiptLineAdd.AppendChild(MakeElement(requestXMLDoc, "Quantity", Quantity.ToString()));
        SalesReceiptLineAdd.AppendChild(MakeElement(requestXMLDoc, "Amount", rate));

        return SalesReceiptLineAdd;
    }

    /// <summary>
    /// Returns xml element for salereceiptLine item
    /// </summary>
    /// <param name="requestXMLDoc"></param>
    /// <param name="ItemRef"></param>
    /// <param name="Description"></param>
    /// <param name="Quantity"></param>
    /// <param name="rate"></param>
    /// <returns></returns>
    private XmlElement BuildSaleOrderLineItem(XmlDocument requestXMLDoc, string ItemRef, string Description, string Quantity, string rate)
    {
        XmlElement SalesOrderLineAdd = requestXMLDoc.CreateElement("SalesOrderLineAdd");

        XmlElement ItemRefXML = requestXMLDoc.CreateElement("ItemRef");
        ItemRefXML.AppendChild(MakeElement(requestXMLDoc, "FullName", ItemRef));
        SalesOrderLineAdd.AppendChild(ItemRefXML);

        SalesOrderLineAdd.AppendChild(MakeElement(requestXMLDoc, "Desc", Description.Replace("<br />", "")));
        SalesOrderLineAdd.AppendChild(MakeElement(requestXMLDoc, "Quantity", Quantity.ToString()));
        SalesOrderLineAdd.AppendChild(MakeElement(requestXMLDoc, "Rate", rate));

        return SalesOrderLineAdd;
    }
    #endregion

    # region Download Customers request methods
    /// <summary>
    /// Build Customer add request xml
    /// </summary>
    /// <param name="req"></param>
    /// <param name="accountList"></param>
    protected void BuildCustomerAddRequestXML(ArrayList req, Account account, ZNodeMessageConfig messageConfig)
    {
        XmlDocument requestXMLDoc = new XmlDocument();

        //customer add Request Query        
        requestXMLDoc.AppendChild(requestXMLDoc.CreateXmlDeclaration("1.0", null, null));
        requestXMLDoc.AppendChild(requestXMLDoc.CreateProcessingInstruction("qbxml", "version=\"7.0\""));

        XmlElement qbXML = requestXMLDoc.CreateElement("QBXML");
        requestXMLDoc.AppendChild(qbXML);

        XmlElement qbXMLMsgsRq = requestXMLDoc.CreateElement("QBXMLMsgsRq");
        qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
        qbXML.AppendChild(qbXMLMsgsRq);

        XmlElement CustomerAddRq = requestXMLDoc.CreateElement("CustomerAddRq");
        CustomerAddRq.SetAttribute("requestID", account.AccountID.ToString());
        qbXMLMsgsRq.AppendChild(CustomerAddRq);

        XmlElement CustomerAdd = requestXMLDoc.CreateElement("CustomerAdd");
        CustomerAddRq.AppendChild(CustomerAdd);
        CustomerAdd.AppendChild(MakeElement(requestXMLDoc, "Name", account.AccountID.ToString()));

        //optional - Customer information
        CustomerAdd.AppendChild(MakeElement(requestXMLDoc, "CompanyName", account.CompanyName));
        CustomerAdd.AppendChild(MakeElement(requestXMLDoc, "FirstName", account.BillingFirstName));
        CustomerAdd.AppendChild(MakeElement(requestXMLDoc, "MiddleName", ""));
        CustomerAdd.AppendChild(MakeElement(requestXMLDoc, "LastName", account.BillingLastName));

        //Billing address - Optional
        XmlElement BillAddress = requestXMLDoc.CreateElement("BillAddress");
        CustomerAdd.AppendChild(BillAddress);

        BillAddress.AppendChild(MakeElement(requestXMLDoc, "Addr1", account.BillingFirstName + " " + account.BillingLastName));
        BillAddress.AppendChild(MakeElement(requestXMLDoc, "Addr2", account.BillingStreet));
        BillAddress.AppendChild(MakeElement(requestXMLDoc, "Addr3", account.BillingStreet1));
        BillAddress.AppendChild(MakeElement(requestXMLDoc, "Addr4", ""));
        BillAddress.AppendChild(MakeElement(requestXMLDoc, "City", account.BillingCity));
        BillAddress.AppendChild(MakeElement(requestXMLDoc, "State", account.BillingStateCode));
        BillAddress.AppendChild(MakeElement(requestXMLDoc, "PostalCode", account.BillingPostalCode));

        //Shipping address  - Optional
        XmlElement ShipAddress = requestXMLDoc.CreateElement("ShipAddress");
        CustomerAdd.AppendChild(ShipAddress);

        ShipAddress.AppendChild(MakeElement(requestXMLDoc, "Addr1", account.ShipFirstName + " " + account.ShipLastName));
        ShipAddress.AppendChild(MakeElement(requestXMLDoc, "Addr2", account.ShipStreet));
        ShipAddress.AppendChild(MakeElement(requestXMLDoc, "Addr3", account.ShipStreet1));
        ShipAddress.AppendChild(MakeElement(requestXMLDoc, "Addr4", ""));
        ShipAddress.AppendChild(MakeElement(requestXMLDoc, "City", account.ShipCity));
        ShipAddress.AppendChild(MakeElement(requestXMLDoc, "State", account.ShipStateCode));
        ShipAddress.AppendChild(MakeElement(requestXMLDoc, "PostalCode", account.ShipPostalCode));

        CustomerAdd.AppendChild(MakeElement(requestXMLDoc, "Phone", account.BillingPhoneNumber));
        CustomerAdd.AppendChild(MakeElement(requestXMLDoc, "Fax", ""));
        CustomerAdd.AppendChild(MakeElement(requestXMLDoc, "Email", account.BillingEmailID));
        CustomerAdd.AppendChild(MakeElement(requestXMLDoc, "Contact", account.BillingFirstName));

        //Customer type (Retail Customer or Dealer) - Optional        
        string customerType = messageConfig.GetMessage("ProfileID" + account.ProfileID.GetValueOrDefault(0).ToString());
        if (customerType.Length > 0)
        {
            XmlElement CustomerTypeRef = requestXMLDoc.CreateElement("CustomerTypeRef");
            CustomerAdd.AppendChild(CustomerTypeRef);
            CustomerTypeRef.AppendChild(MakeElement(requestXMLDoc, "FullName", customerType));
        }

        //add customer add request to web service request Queue
        req.Add(requestXMLDoc.OuterXml);
    }

    /// <summary>
    /// Method to build customer mod request xml
    /// </summary>
    /// <param name="req"></param>
    /// <param name="accountList"></param>
    protected void BuildCustomerQueryRequestXML(ArrayList req, Account account)
    {
        XmlDocument requestXMLDoc = new XmlDocument();

        //Customer Query to find customer based on the Name match criterion
        requestXMLDoc.AppendChild(requestXMLDoc.CreateXmlDeclaration("1.0", null, null));
        requestXMLDoc.AppendChild(requestXMLDoc.CreateProcessingInstruction("qbxml", "version=\"7.0\""));

        XmlElement qbXML = requestXMLDoc.CreateElement("QBXML");
        requestXMLDoc.AppendChild(qbXML);

        XmlElement qbXMLMsgsRq = requestXMLDoc.CreateElement("QBXMLMsgsRq");
        qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
        qbXML.AppendChild(qbXMLMsgsRq);

        XmlElement CustomerQueryRq = requestXMLDoc.CreateElement("CustomerQueryRq");
        CustomerQueryRq.SetAttribute("requestID", account.AccountID.ToString());
        qbXMLMsgsRq.AppendChild(CustomerQueryRq);

        XmlElement NameFilter = requestXMLDoc.CreateElement("NameFilter");
        NameFilter.AppendChild(MakeElement(requestXMLDoc, "MatchCriterion", "Contains"));
        NameFilter.AppendChild(MakeElement(requestXMLDoc, "Name", account.AccountID.ToString()));
        CustomerQueryRq.AppendChild(NameFilter);

        req.Add(requestXMLDoc.OuterXml);
    }

    /// <summary>
    /// Method to find customer in the QB list
    /// </summary>
    /// <param name="req"></param>
    /// <param name="accountList"></param>
    protected void BuildCustomerModifyRequestXML(ArrayList req, Account account, string ListID, string EditSequence, ZNodeMessageConfig messageConfig)
    {
        XmlDocument requestXMLDoc = new XmlDocument();

        //Customer Mod(edit) Request Query        
        requestXMLDoc.AppendChild(requestXMLDoc.CreateXmlDeclaration("1.0", null, null));
        requestXMLDoc.AppendChild(requestXMLDoc.CreateProcessingInstruction("qbxml", "version=\"7.0\""));

        XmlElement qbXML = requestXMLDoc.CreateElement("QBXML");
        requestXMLDoc.AppendChild(qbXML);

        XmlElement qbXMLMsgsRq = requestXMLDoc.CreateElement("QBXMLMsgsRq");
        qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
        qbXML.AppendChild(qbXMLMsgsRq);

        XmlElement CustomerModRq = requestXMLDoc.CreateElement("CustomerModRq");
        CustomerModRq.SetAttribute("requestID", account.AccountID.ToString());
        qbXMLMsgsRq.AppendChild(CustomerModRq);

        XmlElement CustomerMod = requestXMLDoc.CreateElement("CustomerMod");
        CustomerModRq.AppendChild(CustomerMod);

        CustomerMod.AppendChild(MakeElement(requestXMLDoc, "ListID", ListID));
        CustomerMod.AppendChild(MakeElement(requestXMLDoc, "EditSequence", EditSequence));

        //set company & customer name
        CustomerMod.AppendChild(MakeElement(requestXMLDoc, "CompanyName", account.CompanyName));//Optional
        CustomerMod.AppendChild(MakeElement(requestXMLDoc, "FirstName", account.BillingFirstName));//Optional
        CustomerMod.AppendChild(MakeElement(requestXMLDoc, "LastName", account.BillingLastName));//optional

        //Billing address
        XmlElement BillAddress = requestXMLDoc.CreateElement("BillAddress");
        CustomerMod.AppendChild(BillAddress);

        BillAddress.AppendChild(MakeElement(requestXMLDoc, "Addr1", account.BillingFirstName + " " + account.BillingLastName));
        BillAddress.AppendChild(MakeElement(requestXMLDoc, "Addr2", account.BillingStreet));
        BillAddress.AppendChild(MakeElement(requestXMLDoc, "Addr3", account.BillingStreet1));
        BillAddress.AppendChild(MakeElement(requestXMLDoc, "Addr4", ""));
        BillAddress.AppendChild(MakeElement(requestXMLDoc, "Addr5", ""));
        BillAddress.AppendChild(MakeElement(requestXMLDoc, "City", account.BillingCity));
        BillAddress.AppendChild(MakeElement(requestXMLDoc, "State", account.BillingStateCode));
        BillAddress.AppendChild(MakeElement(requestXMLDoc, "PostalCode", account.BillingPostalCode));

        //Shipping address
        XmlElement ShipAddress = requestXMLDoc.CreateElement("ShipAddress");
        CustomerMod.AppendChild(ShipAddress);

        ShipAddress.AppendChild(MakeElement(requestXMLDoc, "Addr1", account.ShipFirstName + " " + account.ShipLastName));
        ShipAddress.AppendChild(MakeElement(requestXMLDoc, "Addr2", account.ShipStreet));
        ShipAddress.AppendChild(MakeElement(requestXMLDoc, "Addr3", account.ShipStreet1));
        ShipAddress.AppendChild(MakeElement(requestXMLDoc, "Addr4", ""));
        ShipAddress.AppendChild(MakeElement(requestXMLDoc, "Addr5", ""));
        ShipAddress.AppendChild(MakeElement(requestXMLDoc, "City", account.ShipCity));
        ShipAddress.AppendChild(MakeElement(requestXMLDoc, "State", account.ShipStateCode));
        ShipAddress.AppendChild(MakeElement(requestXMLDoc, "PostalCode", account.ShipPostalCode));

        CustomerMod.AppendChild(MakeElement(requestXMLDoc, "Phone", account.BillingPhoneNumber));
        CustomerMod.AppendChild(MakeElement(requestXMLDoc, "Fax", ""));
        CustomerMod.AppendChild(MakeElement(requestXMLDoc, "Email", account.BillingEmailID));
        CustomerMod.AppendChild(MakeElement(requestXMLDoc, "Contact", account.BillingFirstName));

        //Customer type (Retail Customer or Dealer) - Optional
        string customerType = messageConfig.GetMessage("ProfileID" + account.ProfileID.GetValueOrDefault(0).ToString());
        if (customerType.Length > 0)
        {
            XmlElement CustomerTypeRef = requestXMLDoc.CreateElement("CustomerTypeRef");
            CustomerMod.AppendChild(CustomerTypeRef);
            CustomerTypeRef.AppendChild(MakeElement(requestXMLDoc, "FullName", customerType));
        }

        //add customer add request to web service request Queue
        req.Add(requestXMLDoc.OuterXml);

    }
    #endregion

    # region Build (Product)ItemInventoy RequestXML Methods

    # region Product
    /// <summary>
    /// Builds the product add query as a inventory Item
    /// </summary>
    /// <param name="req"></param>
    /// <param name="productList"></param>
    protected void BuildProductAddRequestXML(ArrayList req, DataRow productRow, ZNodeMessageConfig messageConfig)
    {
        XmlDocument requestXMLDoc = new XmlDocument();

        //ItemInventoryadd request Query        
        requestXMLDoc.AppendChild(requestXMLDoc.CreateXmlDeclaration("1.0", null, null));
        requestXMLDoc.AppendChild(requestXMLDoc.CreateProcessingInstruction("qbxml", "version=\"7.0\""));

        XmlElement qbXML = requestXMLDoc.CreateElement("QBXML");
        requestXMLDoc.AppendChild(qbXML);

        XmlElement qbXMLMsgsRq = requestXMLDoc.CreateElement("QBXMLMsgsRq");
        qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
        qbXML.AppendChild(qbXMLMsgsRq);

        XmlElement ItemInventoryAddRq = requestXMLDoc.CreateElement("ItemInventoryAddRq");
        ItemInventoryAddRq.SetAttribute("requestID", "Product" + productRow["ProductID"].ToString());
        qbXMLMsgsRq.AppendChild(ItemInventoryAddRq);

        XmlElement ItemInventoryAdd = requestXMLDoc.CreateElement("ItemInventoryAdd");
        ItemInventoryAddRq.AppendChild(ItemInventoryAdd);

        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "Name", productRow["SKU"].ToString()));
        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "IsActive", Convert.ToInt32(productRow["ActiveInd"]).ToString()));
        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "ManufacturerPartNumber", productRow["ProductNum"].ToString()));
        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "SalesDesc", productRow["Name"].ToString()));
        decimal retailPrice = 0;
        if (productRow["RetailPrice"].ToString().Length > 0)
            retailPrice = decimal.Parse(productRow["RetailPrice"].ToString());

        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "SalesPrice", retailPrice.ToString("N2")));
        XmlElement IncomeAccountRef = requestXMLDoc.CreateElement("IncomeAccountRef");
        IncomeAccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", messageConfig.GetMessage("SalesIncomeAccount")));
        ItemInventoryAdd.AppendChild(IncomeAccountRef);

        XmlElement COGSAccountRef = requestXMLDoc.CreateElement("COGSAccountRef");
        COGSAccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", messageConfig.GetMessage("CostofGoodsSoldAccount")));
        ItemInventoryAdd.AppendChild(COGSAccountRef);

        XmlElement AssetAccountRef = requestXMLDoc.CreateElement("AssetAccountRef");
        AssetAccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", messageConfig.GetMessage("InventoryAssetAccount")));
        ItemInventoryAdd.AppendChild(AssetAccountRef);
        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "QuantityOnHand", productRow["QuantityOnHand"].ToString()));
        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "TotalValue", retailPrice.ToString("N2")));

        //add to request list
        req.Add(requestXMLDoc.OuterXml);
    }


    /// <summary>
    /// Search product Inventory Item in the QB list
    /// </summary>
    /// <param name="req"></param>
    /// <param name="accountList"></param>
    protected void BuildProductSKUQueryRequestXML(ArrayList req, Product product)
    {
        int skuCount = product.SKUCollection.Count;
        string returnOuterXML = "";

        if (skuCount == 0)
        {
            returnOuterXML = BuildItemQueryRequest("Product" + product.ProductID.ToString(), "Contains", product.SKU);

            //add to request list
            req.Add(returnOuterXML);
        }
        else
        {
            //Add
            foreach (SKU sku in product.SKUCollection)
            {
                returnOuterXML = BuildItemQueryRequest("Sku" + sku.SKUID.ToString(), "Contains", sku.SKU);

                //add to request list
                req.Add(returnOuterXML);
            }
        }
    }

    /// <summary>
    /// Modify Product Inventory Item using ListID and Edit sequence objects
    /// </summary>
    /// <param name="req"></param>
    /// <param name="product"></param>
    /// <param name="ListID"></param>
    /// <param name="EditSequence"></param>
    protected void BuildProductModifyRequestXML(ArrayList req, Product product, string ListID, string EditSequence, ZNodeMessageConfig messageConfig)
    {
        XmlDocument requestXMLDoc = new XmlDocument();

        //Sales Receipt Query
        requestXMLDoc.AppendChild(requestXMLDoc.CreateXmlDeclaration("1.0", null, null));
        requestXMLDoc.AppendChild(requestXMLDoc.CreateProcessingInstruction("qbxml", "version=\"7.0\""));

        XmlElement qbXML = requestXMLDoc.CreateElement("QBXML");
        requestXMLDoc.AppendChild(qbXML);

        XmlElement qbXMLMsgsRq = requestXMLDoc.CreateElement("QBXMLMsgsRq");
        qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
        qbXML.AppendChild(qbXMLMsgsRq);

        XmlElement ItemInventoryModRq = requestXMLDoc.CreateElement("ItemInventoryModRq");
        ItemInventoryModRq.SetAttribute("requestID", product.ProductID.ToString());
        qbXMLMsgsRq.AppendChild(ItemInventoryModRq);

        XmlElement ItemInventoryMod = requestXMLDoc.CreateElement("ItemInventoryMod");
        ItemInventoryModRq.AppendChild(ItemInventoryMod);

        ItemInventoryMod.AppendChild(MakeElement(requestXMLDoc, "ListID", ListID));
        ItemInventoryMod.AppendChild(MakeElement(requestXMLDoc, "EditSequence", EditSequence));

        ItemInventoryMod.AppendChild(MakeElement(requestXMLDoc, "IsActive", Convert.ToInt32(product.ActiveInd).ToString()));
        ItemInventoryMod.AppendChild(MakeElement(requestXMLDoc, "ManufacturerPartNumber", product.ProductNum));
        ItemInventoryMod.AppendChild(MakeElement(requestXMLDoc, "SalesDesc", product.Name));
        ItemInventoryMod.AppendChild(MakeElement(requestXMLDoc, "SalesPrice", product.RetailPrice.GetValueOrDefault(0).ToString("N2")));

        XmlElement IncomeAccountRef = requestXMLDoc.CreateElement("IncomeAccountRef");
        IncomeAccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", messageConfig.GetMessage("SalesIncomeAccount")));
        ItemInventoryMod.AppendChild(IncomeAccountRef);

        XmlElement COGSAccountRef = requestXMLDoc.CreateElement("COGSAccountRef");
        COGSAccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", messageConfig.GetMessage("CostofGoodsSoldAccount")));
        ItemInventoryMod.AppendChild(COGSAccountRef);

        XmlElement AssetAccountRef = requestXMLDoc.CreateElement("AssetAccountRef");
        AssetAccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", messageConfig.GetMessage("InventoryAssetAccount")));
        ItemInventoryMod.AppendChild(AssetAccountRef);

        //add to request list
        req.Add(requestXMLDoc.OuterXml);

    }
    #endregion

    # region SKU

    /// <summary>
    /// Builds the product add query as a inventory Item
    /// </summary>
    /// <param name="req"></param>
    /// <param name="productList"></param>
    protected void BuildSKUItemAddRequestXML(ArrayList Request, DataRow SkuDataRow, ZNodeMessageConfig ConfigSettings)
    {
        XmlDocument requestXMLDoc = new XmlDocument();

        //ItemInventoryadd request Query        
        requestXMLDoc.AppendChild(requestXMLDoc.CreateXmlDeclaration("1.0", null, null));
        requestXMLDoc.AppendChild(requestXMLDoc.CreateProcessingInstruction("qbxml", "version=\"7.0\""));

        XmlElement qbXML = requestXMLDoc.CreateElement("QBXML");
        requestXMLDoc.AppendChild(qbXML);

        XmlElement qbXMLMsgsRq = requestXMLDoc.CreateElement("QBXMLMsgsRq");
        qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
        qbXML.AppendChild(qbXMLMsgsRq);

        XmlElement ItemInventoryAddRq = requestXMLDoc.CreateElement("ItemInventoryAddRq");
        ItemInventoryAddRq.SetAttribute("requestID", "Sku" + SkuDataRow["SKUID"].ToString());
        qbXMLMsgsRq.AppendChild(ItemInventoryAddRq);

        XmlElement ItemInventoryAdd = requestXMLDoc.CreateElement("ItemInventoryAdd");
        ItemInventoryAddRq.AppendChild(ItemInventoryAdd);

        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "Name", SkuDataRow["SKU"].ToString()));
        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "IsActive", Convert.ToInt32(SkuDataRow["ActiveInd"]).ToString()));
        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "ManufacturerPartNumber", SkuDataRow["ProductNum"].ToString()));
        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "SalesDesc", SkuDataRow["ProductName"].ToString()));

        decimal retailPriceOverride = 0;
        if (SkuDataRow["RetailPriceOverride"].ToString().Length > 0)
            retailPriceOverride = decimal.Parse(SkuDataRow["RetailPriceOverride"].ToString());

        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "SalesPrice", retailPriceOverride.ToString("N2")));
        XmlElement IncomeAccountRef = requestXMLDoc.CreateElement("IncomeAccountRef");
        IncomeAccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", ConfigSettings.GetMessage("SalesIncomeAccount")));
        ItemInventoryAdd.AppendChild(IncomeAccountRef);

        XmlElement COGSAccountRef = requestXMLDoc.CreateElement("COGSAccountRef");
        COGSAccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", ConfigSettings.GetMessage("CostofGoodsSoldAccount")));
        ItemInventoryAdd.AppendChild(COGSAccountRef);

        XmlElement AssetAccountRef = requestXMLDoc.CreateElement("AssetAccountRef");
        AssetAccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", ConfigSettings.GetMessage("InventoryAssetAccount")));
        ItemInventoryAdd.AppendChild(AssetAccountRef);
        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "QuantityOnHand", SkuDataRow["QuantityOnHand"].ToString()));
        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "TotalValue", retailPriceOverride.ToString("N2")));

        //add to request list
        Request.Add(requestXMLDoc.OuterXml);
    }

    /// <summary>
    /// Modify Product Inventory Item using ListID and Edit sequence objects
    /// </summary>
    /// <param name="req"></param>
    /// <param name="product"></param>
    /// <param name="ListID"></param>
    /// <param name="EditSequence"></param>
    protected void BuildItemModifyRequestXML(ArrayList req, SKU sku, string ListID, string EditSequence, ZNodeMessageConfig messageConfig)
    {
        XmlDocument requestXMLDoc = new XmlDocument();

        //Sales Receipt Query
        requestXMLDoc.AppendChild(requestXMLDoc.CreateXmlDeclaration("1.0", null, null));
        requestXMLDoc.AppendChild(requestXMLDoc.CreateProcessingInstruction("qbxml", "version=\"7.0\""));

        XmlElement qbXML = requestXMLDoc.CreateElement("QBXML");
        requestXMLDoc.AppendChild(qbXML);

        XmlElement qbXMLMsgsRq = requestXMLDoc.CreateElement("QBXMLMsgsRq");
        qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
        qbXML.AppendChild(qbXMLMsgsRq);

        XmlElement ItemInventoryModRq = requestXMLDoc.CreateElement("ItemInventoryModRq");
        ItemInventoryModRq.SetAttribute("requestID", "SKU" + sku.SKUID.ToString());
        qbXMLMsgsRq.AppendChild(ItemInventoryModRq);

        XmlElement ItemInventoryMod = requestXMLDoc.CreateElement("ItemInventoryMod");
        ItemInventoryModRq.AppendChild(ItemInventoryMod);

        ItemInventoryMod.AppendChild(MakeElement(requestXMLDoc, "ListID", ListID));
        ItemInventoryMod.AppendChild(MakeElement(requestXMLDoc, "EditSequence", EditSequence));

        ItemInventoryMod.AppendChild(MakeElement(requestXMLDoc, "IsActive", Convert.ToInt32(sku.ActiveInd).ToString()));
        ItemInventoryMod.AppendChild(MakeElement(requestXMLDoc, "ManufacturerPartNumber", sku.ProductIDSource.ProductNum));
        ItemInventoryMod.AppendChild(MakeElement(requestXMLDoc, "SalesDesc", sku.ProductIDSource.Name));
        ItemInventoryMod.AppendChild(MakeElement(requestXMLDoc, "SalesPrice", sku.RetailPriceOverride.GetValueOrDefault(0).ToString("N2")));

        XmlElement IncomeAccountRef = requestXMLDoc.CreateElement("IncomeAccountRef");
        IncomeAccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", messageConfig.GetMessage("SalesIncomeAccount")));
        ItemInventoryMod.AppendChild(IncomeAccountRef);

        XmlElement COGSAccountRef = requestXMLDoc.CreateElement("COGSAccountRef");
        COGSAccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", messageConfig.GetMessage("CostofGoodsSoldAccount")));
        ItemInventoryMod.AppendChild(COGSAccountRef);

        XmlElement AssetAccountRef = requestXMLDoc.CreateElement("AssetAccountRef");
        AssetAccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", messageConfig.GetMessage("InventoryAssetAccount")));
        ItemInventoryMod.AppendChild(AssetAccountRef);

        //add to request list
        req.Add(requestXMLDoc.OuterXml);
    }
    #endregion

    # region AddOnValue

    /// <summary>
    /// Builds the product add query as a inventory Item
    /// </summary>
    /// <param name="req"></param>
    /// <param name="productList"></param>
    protected void BuildAddOnValueItemAddRequestXML(ArrayList Request, DataRow AddOnValueRow, ZNodeMessageConfig ConfigSettings)
    {
        XmlDocument requestXMLDoc = new XmlDocument();

        //ItemInventoryadd request Query        
        requestXMLDoc.AppendChild(requestXMLDoc.CreateXmlDeclaration("1.0", null, null));
        requestXMLDoc.AppendChild(requestXMLDoc.CreateProcessingInstruction("qbxml", "version=\"7.0\""));

        XmlElement qbXML = requestXMLDoc.CreateElement("QBXML");
        requestXMLDoc.AppendChild(qbXML);

        XmlElement qbXMLMsgsRq = requestXMLDoc.CreateElement("QBXMLMsgsRq");
        qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
        qbXML.AppendChild(qbXMLMsgsRq);

        XmlElement ItemInventoryAddRq = requestXMLDoc.CreateElement("ItemInventoryAddRq");
        ItemInventoryAddRq.SetAttribute("requestID", "AddOnValue" + AddOnValueRow["AddOnValueID"].ToString());
        qbXMLMsgsRq.AppendChild(ItemInventoryAddRq);

        XmlElement ItemInventoryAdd = requestXMLDoc.CreateElement("ItemInventoryAdd");
        ItemInventoryAddRq.AppendChild(ItemInventoryAdd);

        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "Name", AddOnValueRow["SKU"].ToString()));
        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "IsActive", "1"));
        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "ManufacturerPartNumber", ""));
        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "SalesDesc", AddOnValueRow["Name"].ToString()));

        decimal retailPrice = 0;
        if (AddOnValueRow["RetailPrice"].ToString().Length > 0)
            retailPrice = decimal.Parse(AddOnValueRow["RetailPrice"].ToString());

        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "SalesPrice", retailPrice.ToString("N2")));
        XmlElement IncomeAccountRef = requestXMLDoc.CreateElement("IncomeAccountRef");
        IncomeAccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", ConfigSettings.GetMessage("SalesIncomeAccount")));
        ItemInventoryAdd.AppendChild(IncomeAccountRef);

        XmlElement COGSAccountRef = requestXMLDoc.CreateElement("COGSAccountRef");
        COGSAccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", ConfigSettings.GetMessage("CostofGoodsSoldAccount")));
        ItemInventoryAdd.AppendChild(COGSAccountRef);

        XmlElement AssetAccountRef = requestXMLDoc.CreateElement("AssetAccountRef");
        AssetAccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", ConfigSettings.GetMessage("InventoryAssetAccount")));
        ItemInventoryAdd.AppendChild(AssetAccountRef);
        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "QuantityOnHand", AddOnValueRow["QuantityOnHand"].ToString()));
        ItemInventoryAdd.AppendChild(MakeElement(requestXMLDoc, "TotalValue", retailPrice.ToString("N2")));

        //add to request list
        Request.Add(requestXMLDoc.OuterXml);
    }

    /// <summary>
    /// Modify Product Inventory Item using ListID and Edit sequence objects
    /// </summary>
    /// <param name="req"></param>
    /// <param name="product"></param>
    /// <param name="ListID"></param>
    /// <param name="EditSequence"></param>
    protected void BuildItemModifyRequestXML(ArrayList req, AddOnValue addOnValue, string ListID, string EditSequence, ZNodeMessageConfig messageConfig)
    {
        XmlDocument requestXMLDoc = new XmlDocument();

        //Sales Receipt Query
        requestXMLDoc.AppendChild(requestXMLDoc.CreateXmlDeclaration("1.0", null, null));
        requestXMLDoc.AppendChild(requestXMLDoc.CreateProcessingInstruction("qbxml", "version=\"7.0\""));

        XmlElement qbXML = requestXMLDoc.CreateElement("QBXML");
        requestXMLDoc.AppendChild(qbXML);

        XmlElement qbXMLMsgsRq = requestXMLDoc.CreateElement("QBXMLMsgsRq");
        qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
        qbXML.AppendChild(qbXMLMsgsRq);

        XmlElement ItemInventoryModRq = requestXMLDoc.CreateElement("ItemInventoryModRq");
        ItemInventoryModRq.SetAttribute("requestID", "AddOnValue" + addOnValue.AddOnValueID.ToString());
        qbXMLMsgsRq.AppendChild(ItemInventoryModRq);

        XmlElement ItemInventoryMod = requestXMLDoc.CreateElement("ItemInventoryMod");
        ItemInventoryModRq.AppendChild(ItemInventoryMod);

        ItemInventoryMod.AppendChild(MakeElement(requestXMLDoc, "ListID", ListID));
        ItemInventoryMod.AppendChild(MakeElement(requestXMLDoc, "EditSequence", EditSequence));

        ItemInventoryMod.AppendChild(MakeElement(requestXMLDoc, "IsActive", "1"));
        ItemInventoryMod.AppendChild(MakeElement(requestXMLDoc, "ManufacturerPartNumber", ""));
        ItemInventoryMod.AppendChild(MakeElement(requestXMLDoc, "SalesDesc", addOnValue.Name));
        ItemInventoryMod.AppendChild(MakeElement(requestXMLDoc, "SalesPrice", addOnValue.RetailPrice.ToString("N2")));

        XmlElement IncomeAccountRef = requestXMLDoc.CreateElement("IncomeAccountRef");
        IncomeAccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", messageConfig.GetMessage("SalesIncomeAccount")));
        ItemInventoryMod.AppendChild(IncomeAccountRef);

        XmlElement COGSAccountRef = requestXMLDoc.CreateElement("COGSAccountRef");
        COGSAccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", messageConfig.GetMessage("CostofGoodsSoldAccount")));
        ItemInventoryMod.AppendChild(COGSAccountRef);

        XmlElement AssetAccountRef = requestXMLDoc.CreateElement("AssetAccountRef");
        AssetAccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", messageConfig.GetMessage("InventoryAssetAccount")));
        ItemInventoryMod.AppendChild(AssetAccountRef);

        //add to request list
        req.Add(requestXMLDoc.OuterXml);
    }
    #endregion

    # region ItemQuery request XML
    /// <summary>
    /// Search product Inventory Item in the QB list
    /// </summary>
    /// <param name="requestID"></param>
    /// <param name="matchCriterion"></param>
    /// <param name="valueToSearch"></param>
    /// <returns></returns>
    private string BuildItemQueryRequest(string requestID, string matchCriterion, string valueToSearch)
    {
        XmlDocument requestXMLDoc = new XmlDocument();

        //Sales Receipt Query        
        requestXMLDoc.AppendChild(requestXMLDoc.CreateXmlDeclaration("1.0", null, null));
        requestXMLDoc.AppendChild(requestXMLDoc.CreateProcessingInstruction("qbxml", "version=\"7.0\""));

        XmlElement qbXML = requestXMLDoc.CreateElement("QBXML");
        requestXMLDoc.AppendChild(qbXML);

        XmlElement qbXMLMsgsRq = requestXMLDoc.CreateElement("QBXMLMsgsRq");
        qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
        qbXML.AppendChild(qbXMLMsgsRq);

        XmlElement ItemInventoryQueryRq = requestXMLDoc.CreateElement("ItemInventoryQueryRq");
        ItemInventoryQueryRq.SetAttribute("requestID", requestID);
        qbXMLMsgsRq.AppendChild(ItemInventoryQueryRq);

        XmlElement NameFilter = requestXMLDoc.CreateElement("NameFilter");
        NameFilter.AppendChild(MakeElement(requestXMLDoc, "MatchCriterion", matchCriterion));
        NameFilter.AppendChild(MakeElement(requestXMLDoc, "Name", valueToSearch));
        ItemInventoryQueryRq.AppendChild(NameFilter);

        //add to request list
        return requestXMLDoc.OuterXml;
    }
    #endregion

    #endregion

    #region Adjust Item Inventory level request XML

    /// <summary>
    /// Build request xml for the item to adjust quantity level
    /// </summary>
    /// <param name="req"></param>
    /// <param name="product"></param>
    /// <param name="ListID"></param>
    /// <param name="EditSequence"></param>
    protected void BuildAdjustItemInventoryRequestXML(ArrayList Request, string Sku, int QuantityOnHand, ZNodeMessageConfig messageConfig)
    {
        XmlDocument requestXMLDoc = new XmlDocument();

        //Sales Receipt Query
        requestXMLDoc.AppendChild(requestXMLDoc.CreateXmlDeclaration("1.0", null, null));
        requestXMLDoc.AppendChild(requestXMLDoc.CreateProcessingInstruction("qbxml", "version=\"7.0\""));

        XmlElement qbXML = requestXMLDoc.CreateElement("QBXML");
        requestXMLDoc.AppendChild(qbXML);

        XmlElement qbXMLMsgsRq = requestXMLDoc.CreateElement("QBXMLMsgsRq");
        qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
        qbXML.AppendChild(qbXMLMsgsRq);

        XmlElement InventoryAdjustmentAddRq = requestXMLDoc.CreateElement("InventoryAdjustmentAddRq");
        InventoryAdjustmentAddRq.SetAttribute("requestID", "InventoryAdjustent");
        qbXMLMsgsRq.AppendChild(InventoryAdjustmentAddRq);

        XmlElement InventoryAdjustmentAdd = requestXMLDoc.CreateElement("InventoryAdjustmentAdd");
        InventoryAdjustmentAddRq.AppendChild(InventoryAdjustmentAdd);

        XmlElement AccountRef = requestXMLDoc.CreateElement("AccountRef");
        InventoryAdjustmentAdd.AppendChild(AccountRef);
        AccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", messageConfig.GetMessage("SalesIncomeAccount")));

        InventoryAdjustmentAdd.AppendChild(BuildInventoryAdjustmentLineAdd
                    (requestXMLDoc, InventoryAdjustmentAdd, Sku, QuantityOnHand));

        Request.Add(requestXMLDoc.OuterXml);
    }

    /// <summary>
    /// Build bulk update request for all the items to adjust quantity level
    /// </summary>
    /// <param name="req"></param>
    /// <param name="product"></param>
    /// <param name="ListID"></param>
    /// <param name="EditSequence"></param>
    protected void BuildAdjustItemInventoryRequestXML(ArrayList Request, TList<Product> ProductList, TList<AddOnValue> AddOnValueList, ZNodeMessageConfig messageConfig)
    {
        XmlDocument requestXMLDoc = new XmlDocument();

        //Sales Receipt Query
        requestXMLDoc.AppendChild(requestXMLDoc.CreateXmlDeclaration("1.0", null, null));
        requestXMLDoc.AppendChild(requestXMLDoc.CreateProcessingInstruction("qbxml", "version=\"7.0\""));

        XmlElement qbXML = requestXMLDoc.CreateElement("QBXML");
        requestXMLDoc.AppendChild(qbXML);

        XmlElement qbXMLMsgsRq = requestXMLDoc.CreateElement("QBXMLMsgsRq");
        qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
        qbXML.AppendChild(qbXMLMsgsRq);

        XmlElement InventoryAdjustmentAddRq = requestXMLDoc.CreateElement("InventoryAdjustmentAddRq");
        InventoryAdjustmentAddRq.SetAttribute("requestID", "InventoryAdjustent");
        qbXMLMsgsRq.AppendChild(InventoryAdjustmentAddRq);

        XmlElement InventoryAdjustmentAdd = requestXMLDoc.CreateElement("InventoryAdjustmentAdd");
        InventoryAdjustmentAddRq.AppendChild(InventoryAdjustmentAdd);

        XmlElement AccountRef = requestXMLDoc.CreateElement("AccountRef");
        InventoryAdjustmentAdd.AppendChild(AccountRef);
        AccountRef.AppendChild(MakeElement(requestXMLDoc, "FullName", messageConfig.GetMessage("SalesIncomeAccount")));

        foreach (Product product in ProductList)
        {
            if (product.SKUCollection.Count == 0)
            {
                InventoryAdjustmentAdd.AppendChild(BuildInventoryAdjustmentLineAdd
                    (requestXMLDoc, InventoryAdjustmentAdd, product.SKU, product.QuantityOnHand.GetValueOrDefault(0)));
            }
            else
            {
                foreach (SKU sku in product.SKUCollection)
                {
                    InventoryAdjustmentAdd.AppendChild(BuildInventoryAdjustmentLineAdd
                        (requestXMLDoc, InventoryAdjustmentAdd, sku.SKU, sku.QuantityOnHand));
                }
            }
        }

        foreach (AddOnValue addOnValue in AddOnValueList)
        {
            InventoryAdjustmentAdd.AppendChild(BuildInventoryAdjustmentLineAdd
                        (requestXMLDoc, InventoryAdjustmentAdd, addOnValue.SKU, addOnValue.QuantityOnHand));
        }

        Request.Add(requestXMLDoc.OuterXml);
    }


    /// <summary>
    /// Build adjustment line item
    /// </summary>
    /// <param name="RequestXMLDoc"></param>
    /// <param name="RootElement"></param>
    /// <param name="ItemRef"></param>
    /// <param name="NewQuantity"></param>
    /// <returns></returns>
    private XmlElement BuildInventoryAdjustmentLineAdd(XmlDocument RequestXMLDoc, XmlElement RootElement, string ItemRef, int NewQuantity)
    {
        XmlElement InventoryAdjustmentLineAdd = RequestXMLDoc.CreateElement("InventoryAdjustmentLineAdd");
        RootElement.AppendChild(InventoryAdjustmentLineAdd);

        XmlElement ItemReference = RequestXMLDoc.CreateElement("ItemRef");
        ItemReference.AppendChild(MakeElement(RequestXMLDoc, "FullName", ItemRef));
        InventoryAdjustmentLineAdd.AppendChild(ItemReference);

        XmlElement QuantityAdjustment = RequestXMLDoc.CreateElement("QuantityAdjustment");
        QuantityAdjustment.AppendChild(MakeElement(RequestXMLDoc, "NewQuantity", NewQuantity.ToString()));
        InventoryAdjustmentLineAdd.AppendChild(QuantityAdjustment);

        return InventoryAdjustmentLineAdd;
    }
    #endregion

    # region Helper Methods
    /// <summary>
    /// Creates and Returns an Element with the specified tagName with the value 
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="tagName"></param>
    /// <param name="tagValue"></param>
    /// <returns></returns>
    private XmlElement MakeElement(XmlDocument doc, string tagName, string tagValue)
    {
        XmlElement elem = doc.CreateElement(tagName);
        elem.InnerText = tagValue;
        return elem;
    }

    /// <summary>
    /// Returns the RequestID by parsing the response xml using Xml reader
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="tagName"></param>
    /// <param name="tagValue"></param>
    /// <returns></returns>
    private string ParseRequestID(string Message, string responsetagName, out string statusCode)
    {
        System.IO.TextReader txtReader = new System.IO.StringReader(Message);
        XmlTextReader xmlreader = new XmlTextReader(txtReader);
        string requestID = "0";
        string _statusCode = "-1";

        //Read from XML File          
        while (xmlreader.Read())
        {
            switch (xmlreader.NodeType)
            {
                case XmlNodeType.Element:
                    if (xmlreader.HasAttributes && xmlreader.Name == responsetagName)
                    {
                        for (int i = 0; i < xmlreader.AttributeCount; i++)
                        {
                            xmlreader.MoveToAttribute(i);

                            if (xmlreader.Name == "statusCode")
                            {
                                _statusCode = xmlreader.Value;
                            }
                            else if (xmlreader.Name == "requestID")
                            {
                                requestID = xmlreader.Value;
                            }
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        statusCode = _statusCode;
        return requestID;
    }

    /// <summary>
    /// Returns the profile Name for this profileID
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="tagName"></param>
    /// <param name="tagValue"></param>
    /// <returns></returns>
    private string ProfileName(int profileID)
    {
        ProfileService profileService = new ProfileService();
        Profile profile = profileService.GetByProfileID(profileID);

        if (profile != null)
            return profile.Name;

        return "";
    }
    #endregion
}