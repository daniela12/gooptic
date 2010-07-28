
using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Xml;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.DataAccess.Service;

/// <summary>
/// Summary description for CatalogWebService
/// </summary>
[WebService(Namespace = "http://www.znode.com/webservices/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class CatalogWebService : ZNodeWebserviceBase
{
    # region Public Constructors    
    public CatalogWebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #endregion

    # region Web Methods
    [WebMethod(Description = "Uploads the product data with the default sku to the database.")]
    public System.Xml.XmlDocument UploadProuctDefaultSKU(string UserId, string Password, ZNode.Libraries.Framework.Business.ZNodeGenericCollection<ProductEntity> ProductList, TList<SKUEntity> SKUList)
    {
        try
        {

            //Authorize Users
            bool IsAuthorized = Authorize(UserId, Password);

            if (IsAuthorized)
            {
                //Call Product insert/update method
                System.Xml.XmlDocument doc = InsertProduct(ProductList);

                string ResponseErrorCode = doc.SelectSingleNode("Response/ErrorCode").InnerText;
                string ResponseErrorDescription = doc.SelectSingleNode("Response/ErrorDescription").InnerText;

                //Check if any error encountered on the web service while processing our request
                if (ResponseErrorCode.Equals("0"))
                {
                    //Default Insert method
                    InsertDefaultSKU(SKUList);

                    XmlNode Node = doc.SelectSingleNode("Response/Data");

                    return CreateSOAPResponse("Data Uploaded Successfully!", "0", Node.Value + " of rows are affected in the product table");
                }

                return CreateSOAPResponse("","-1",ResponseErrorDescription);

            }
            else
            {
                return CreateSOAPResponse("", "1", ResponseMSG_UNAUTHORIZED);
            }
        }
        catch (Exception ex)
        {
            return CreateSOAPResponse("", "2", ResponseMSG_ERROR + "Reference: " + ex.Message);
        }
    }   

    [WebMethod(Description = "Uploads the Inventory for Product,SKUs and Add-On Values.")]
    public System.Xml.XmlDocument UploadInventory(string UserId, string Password, ZNode.Libraries.Framework.Business.ZNodeGenericCollection<ProductSKUEntity> ProductSKUList)
    {
        try
        {

            //Authorize Users
            bool IsAuthorized = Authorize(UserId, Password);

            if (IsAuthorized)
            {
                string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ZNodeECommerceDB"].ConnectionString;
                
                //Call 
                UploadQuantityAvailble(ProductSKUList, ConnectionString);

                return CreateSOAPResponse("Data Upload Success", "0", String.Empty);
            }
            else
            {
                return CreateSOAPResponse("", "1", ResponseMSG_UNAUTHORIZED);
            }
        }
        catch (Exception ex)
        {
            return CreateSOAPResponse("", "2", ResponseMSG_ERROR + "Reference: " + ex.Message);
        }
    }

    [WebMethod(Description = "Uploads the retail price for Product,SKUs and Add-On Values.")]
    public System.Xml.XmlDocument UploadRetailPrice(string UserId, string Password, ZNode.Libraries.Framework.Business.ZNodeGenericCollection<ProductSKUEntity> ProductSKUList)
    {
        try
        {

            //Authorize Users
            bool IsAuthorized = Authorize(UserId, Password);

            if (IsAuthorized)
            {
                string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ZNodeECommerceDB"].ConnectionString;

                //Call 
                UploadProductRetailPrice(ProductSKUList, ConnectionString);

                return CreateSOAPResponse("Data Upload Success", "0", String.Empty);
            }
            else
            {
                return CreateSOAPResponse("", "1", ResponseMSG_UNAUTHORIZED);
            }
        }
        catch (Exception ex)
        {
            return CreateSOAPResponse("", "2", ResponseMSG_ERROR + "Reference: " + ex.Message);
        }
    }


    [WebMethod(Description = "Uploads the Product attributes.")]
    public System.Xml.XmlDocument UploadProductAttributes(string UserId, string Password, XmlNode node, DataSet InputDataSet)
    {
        return CreateSOAPResponse("", "2", ResponseMSG_ERROR);
    }

    # endregion

    # region NetTiers Layer Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ProductList"></param>
    private void InsertDefaultSKU(TList<SKUEntity> SKUList)
    {        
        string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ZNodeECommerceDB"].ConnectionString;
        ZNode.Libraries.DataAccess.Service.SKUService ProductSKUServ = new SKUService();

        foreach (SKUEntity entity in SKUList)
        {
            SKU _sku = new SKU();

            int Productid = GetProductID(entity.ProductNum, ConnectionString);
            int SkuId = GetSkuID(entity.SKU, ConnectionString);

            if (Productid == 0)
            {
                break;
            }

            //Set Properties
            _sku.QuantityOnHand = entity.QuantityOnHand;
            _sku.ProductID = Productid;

            if (entity.RetailPriceOverride.HasValue)
            {
                _sku.RetailPriceOverride = entity.RetailPriceOverride.Value;
            }
            _sku.SKU = entity.SKU;

            if (SkuId > 0)
            {
                _sku.UpdateDte = System.DateTime.Now;
                ProductSKUServ.Update(_sku);
            }
            else
            {
                ProductSKUServ.Insert(_sku);
            }

        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ProductList"></param>
    private System.Xml.XmlDocument InsertProduct(ZNode.Libraries.Framework.Business.ZNodeGenericCollection<ProductEntity> ProductList)
    {
        ZNode.Libraries.DataAccess.Service.ProductService ProductServ = new ProductService();
        string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ZNodeECommerceDB"].ConnectionString;
        int RowsAffected = 0;

        try
        {
            //Loop through the no.of items in the Product List object
            foreach (ProductEntity Entity in ProductList)
            {
                Product _product = new Product();

                int ProductId = GetProductID(Entity.ProductNum, ConnectionString);

                if (ProductId > 0)
                {
                    _product = ProductServ.GetByProductID(ProductId);
                }
                //Set Properties
                _product.Name = Entity.Name;
                _product.Description = Entity.Description;
                _product.FeaturesDesc = Entity.FeaturesDesc;
                _product.DisplayOrder = Entity.DisplayOrder;

                //General settings 
                _product.PortalID = Entity.PortalID;
                _product.ProductNum = Entity.ProductNum;
                _product.ActiveInd = Entity.IsActive;
                _product.ImageFile = Entity.ImageFile;
                _product.RetailPrice = Entity.RetailPrice;
                _product.SalePrice = Entity.SalePrice;
                _product.WholesalePrice = Entity.WholesalePrice;

                //Display Settings
                _product.CallForPricing = Entity.CallForPricing;
                _product.HomepageSpecial = Entity.HomepageSpecial;
                _product.CategorySpecial = Entity.CategorySpecial;
                _product.Keywords = Entity.Keywords;

                //inventory Settings            
                _product.BackOrderMsg = Entity.BackOrderMsg;
                _product.OutOfStockMsg = Entity.OutOfStockMsg;
                _product.InStockMsg = Entity.InStockMsg;

                // Product Type Section
                int ProductTypeId = ZnodeProductTypeInsert(Entity.PortalID, Entity.ProductTypeName, ConnectionString);

                if (ProductTypeId == 0)
                {
                    ProductTypeId = ZnodeProductTypeInsert(Entity.PortalID, Entity.ProductTypeName, ConnectionString);
                }

                //If product type is "Default" ,then SKU and Quantity on Hand values are updated into Product Table
                // Otherwise it will update the values into SKU table
                if (Entity.ProductTypeName.Equals("Default"))
                {
                    _product.QuantityOnHand = Entity.QuantityOnHand;
                    _product.SKU = Entity.SKU;

                    //remove Existing Skus for this product
                    ZNode.Libraries.Admin.SKUAdmin _skuAdmin = new ZNode.Libraries.Admin.SKUAdmin();
                    _skuAdmin.DeleteByProductId(ProductId);
                }
                else
                {
                    //Reset Default product Inventory settings
                    _product.QuantityOnHand = 0;
                    _product.SKU = Entity.SKU;
                }

                _product.ProductTypeID = ProductTypeId; // Set product type id

                int ManufacturerId = GetManufacturerID(Entity.ManufacturerName, ConnectionString);

                if (ManufacturerId == 0)
                {
                    _product.ManufacturerID = null; //If not exists ,then insert null to Manufacturerid
                }

                bool status = false;

                if (ProductId > 0)
                {
                    _product.UpdateDte = System.DateTime.Now;
                    status = ProductServ.Update(_product);
                }
                else
                {
                    status = ProductServ.Insert(_product);
                }

                if (status)
                {
                    RowsAffected++;
                }
            }
        }
        catch (Exception ex)
        { return CreateSOAPResponse("", "-2", ex.Message); }


        return CreateSOAPResponse(RowsAffected.ToString(),"0",""); ;
    }
    #endregion   

    #region Protected Commom Methods - Returns Values for Product,Default SKU,manufacturer
    /// <summary>
    /// 
    /// </summary>
    /// <param name="portalid"></param>
    /// <param name="prodtype"></param>
    /// <param name="connString"></param>
    /// <returns></returns>
    private static int ZnodeProductTypeInsert(int portalid, string prodtypeName, string connString)
    {
        int prodtypeid = 0;
        try
        {
            SqlConnection conn = new SqlConnection(connString);

            SqlCommand SKUCMD = new SqlCommand("ZNODE_NT_ZNodeProductType_Find", conn);
            SKUCMD.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@portalid", SqlDbType.Int);
            param.Value = portalid;
            SKUCMD.Parameters.Add(param);

            param = new SqlParameter("@Name", SqlDbType.VarChar);
            param.Value = prodtypeName;
            SKUCMD.Parameters.Add(param);

            conn.Open();

            SqlDataReader dr = SKUCMD.ExecuteReader();

            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    prodtypeid = Convert.ToInt32(dr["ProducttypeId"]);
                }
            }
            else
            {
                ProductTypeService prodTypeServ = new ProductTypeService();
                ProductType prodType = new ProductType();
                prodType.PortalId = portalid;
                prodType.Name = prodtypeName;
                prodTypeServ.Insert(prodType);

                prodtypeid = prodType.ProductTypeId;
            }
            conn.Close();

            return prodtypeid;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="SKU"></param>
    /// <param name="connString"></param>
    /// <returns></returns>
    public static int GetSkuID(string SKU, string connString)
    {
        SqlConnection conn = new SqlConnection(connString);
        // To Insert the values to ZnodeProduct
        try
        {
            SqlCommand cmd = new SqlCommand("ZNODE_NT_ZNodeSKU_Find", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Param;
            Param = new SqlParameter("@SKU", SqlDbType.NVarChar);
            Param.Value = SKU;
            cmd.Parameters.Add(Param);

            conn.Open();

            object ob = cmd.ExecuteScalar();
            int skuid = Convert.ToInt32(ob);

            conn.Close();

            return skuid;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="SKU"></param>
    /// <param name="connString"></param>
    /// <returns></returns>
    public static int GetSkuIDByAddOnValueSKU(string SKU, string connString)
    {
        SqlConnection conn = new SqlConnection(connString);
        // To Insert the values to ZnodeProduct
        try
        {
            SqlCommand cmd = new SqlCommand("ZNODE_NT_ZNodeAddOnValue_Find", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Param;
            Param = new SqlParameter("@SKU", SqlDbType.NVarChar);
            Param.Value = SKU;
            cmd.Parameters.Add(Param);

            conn.Open();

            object ob = cmd.ExecuteScalar();
            int AddOnValueId = Convert.ToInt32(ob);

            conn.Close();

            return AddOnValueId;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    /// <summary>
    /// Returns product id for the given product code
    /// </summary>
    /// <param name="ProductNum"></param>
    /// <param name="connString"></param>
    /// <returns></returns>
    public static int GetManufacturerID(string ManufacturerName, string connString)
    {
        SqlConnection conn = new SqlConnection(connString);
        // To Insert the values to ZnodeProduct
        try
        {
            SqlCommand cmd = new SqlCommand("ZNODE_NT_ZNodeManufacturer_Find", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Param;
            Param = new SqlParameter("@Name", SqlDbType.VarChar);
            Param.Value = ManufacturerName;
            cmd.Parameters.Add(Param);

            conn.Open();

            object ob = cmd.ExecuteScalar();
            int manufacturerId = Convert.ToInt32(ob);

            conn.Close();

            return manufacturerId;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    
    /// <summary>
    /// Returns product id for the given product code
    /// </summary>
    /// <param name="ProductNum"></param>
    /// <param name="connString"></param>
    /// <returns></returns>
    public static int GetProductID(string ProductNum, string connString)
    {
        SqlConnection conn = new SqlConnection(connString);
        // To Insert the values to ZnodeProduct
        try
        {
            SqlCommand cmd = new SqlCommand("ZNODE_NT_ZNodeProduct_Find", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Param;
            Param = new SqlParameter("@ProductNum", SqlDbType.VarChar);
            Param.Value = ProductNum;
            cmd.Parameters.Add(Param);

            conn.Open();

            object ob = cmd.ExecuteScalar();
            int prodid = Convert.ToInt32(ob);

            conn.Close();

            return prodid;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    /// <summary>
    /// Returns product id for the given product code
    /// </summary>
    /// <param name="ProductNum"></param>
    /// <param name="connString"></param>
    /// <returns></returns>
    public static int GetProductBySKU(string SKU, string connString)
    {
        SqlConnection conn = new SqlConnection(connString);
        // To Insert the values to ZnodeProduct
        try
        {
            SqlCommand cmd = new SqlCommand("ZNODE_NT_ZNodeProduct_Find", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Param;
            Param = new SqlParameter("@SKU", SqlDbType.NVarChar);
            Param.Value = SKU;
            cmd.Parameters.Add(Param);

            conn.Open();

            object ob = cmd.ExecuteScalar();
            int prodid = Convert.ToInt32(ob);

            conn.Close();

            return prodid;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    #endregion

    # region Inventory Update related methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="appSettings"></param>
    /// <param name="connString"></param>
    private void UploadQuantityAvailble(ZNode.Libraries.Framework.Business.ZNodeGenericCollection<ProductSKUEntity> ProdSKUList, string connString)
    {
        # region Local Variables Declaration
        SqlConnection conn = new SqlConnection(connString);
        ZNode.Libraries.DataAccess.Service.ProductService ProductServ = new ProductService();
        ZNode.Libraries.DataAccess.Service.SKUService ProductSKUServ = new SKUService();
        ZNode.Libraries.DataAccess.Service.AddOnValueService ProductAddOnValueServ = new AddOnValueService();
        # endregion


        foreach (ProductSKUEntity entity in ProdSKUList)
        {
            SKU _sku = new SKU();

            //Update Prodcut table
            int ProductID = GetProductBySKU(entity.SKU, connString);

            if (ProductID > 0)
            {
                Product _productObject = ProductServ.GetByProductID(ProductID);
                _productObject.QuantityOnHand = entity.QuantityOnHand;
                _productObject.UpdateDte = System.DateTime.Now;

                ProductServ.Update(_productObject);
            }

            //Update SKU table
            int SkuId = GetSkuID(entity.SKU, connString);

            if (SkuId > 0)
            {
                _sku = ProductSKUServ.GetBySKUID(SkuId);
                //Set Quantity available value
                _sku.QuantityOnHand = entity.QuantityOnHand;
                _sku.UpdateDte = System.DateTime.Now;

                //Upate SKU
                ProductSKUServ.Update(_sku);
            }

            //Update Add-On table
            AddOnValue _addOnvalue = new AddOnValue();

            int AddOnValueId = GetSkuIDByAddOnValueSKU(entity.SKU, connString);

            if (AddOnValueId > 0)
            {
                _addOnvalue = ProductAddOnValueServ.GetByAddOnValueID(AddOnValueId);

                _addOnvalue.QuantityOnHand = entity.QuantityOnHand;
                _addOnvalue.UpdateDte = System.DateTime.Now;

                ProductAddOnValueServ.Update(_addOnvalue);
            }
        }
    }
    #endregion

    # region Retail Price Update related methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="appSettings"></param>
    /// <param name="connString"></param>
    private void UploadProductRetailPrice(ZNode.Libraries.Framework.Business.ZNodeGenericCollection<ProductSKUEntity> ProdSKUList, string connString)
    {
        #region Local Variable Declaration
        SqlConnection conn = new SqlConnection(connString);
        ZNode.Libraries.DataAccess.Service.ProductService ProductServ = new ProductService();
        ZNode.Libraries.DataAccess.Service.SKUService ProductSKUServ = new SKUService();
        ZNode.Libraries.DataAccess.Service.AddOnValueService ProductAddOnValueServ = new AddOnValueService();
        #endregion

        foreach (ProductSKUEntity entity in ProdSKUList)
        {
            SKU _sku = new SKU();

            //Update Prodcut table
            int ProductID = GetProductBySKU(entity.SKU, connString);

            if (ProductID > 0)
            {
                Product _productObject = ProductServ.GetByProductID(ProductID);
                _productObject.RetailPrice = entity.RetailPrice;
                _productObject.SalePrice = entity.SalePrice;
                _productObject.WholesalePrice = entity.WholesalePrice;
                _productObject.UpdateDte = System.DateTime.Now;

                ProductServ.Update(_productObject);
            }

            //Update SKU table
            int SkuId = GetSkuID(entity.SKU, connString);

            if (SkuId > 0)
            {
                _sku = ProductSKUServ.GetBySKUID(SkuId);
                //Set Quantity available value
                _sku.RetailPriceOverride = entity.RetailPrice;
                _sku.SalePriceOverride = entity.SalePrice;
                _sku.WholesalePriceOverride = entity.WholesalePrice;

                _sku.UpdateDte = System.DateTime.Now;

                //Upate SKU
                ProductSKUServ.Update(_sku);
            }

            //Update Add-On table
            AddOnValue _addOnvalue = new AddOnValue();

            int AddOnValueId = GetSkuIDByAddOnValueSKU(entity.SKU, connString);

            if (AddOnValueId > 0)
            {
                _addOnvalue = ProductAddOnValueServ.GetByAddOnValueID(AddOnValueId);

                _addOnvalue.RetailPrice = entity.RetailPrice;
                _addOnvalue.SalePrice = entity.SalePrice;
                _addOnvalue.WholesalePrice = entity.WholesalePrice;

                _addOnvalue.UpdateDte = System.DateTime.Now;

                ProductAddOnValueServ.Update(_addOnvalue);
            }
        }
    }
    #endregion

    # region Helper Methods
    /// <summary>
    /// The ArrayListObject below is the array that contains the arraylist
    /// Method converts the array list objects into NameValueCollection
    /// </summary>
    /// <param name="nvp"></param>
    /// <returns></returns>
    private NameValueCollection ConvertArrayListObjectsIntoNameValueCollection(ArrayList nvp)
    {
        System.Collections.Specialized.NameValueCollection collection = new System.Collections.Specialized.NameValueCollection();

        for (int i = 0; i < nvp.Count; i++)
        {
            System.Collections.DictionaryEntry strNameValuePair = (System.Collections.DictionaryEntry)nvp[i];
            string key = strNameValuePair.Key.ToString();
            string Value = strNameValuePair.Value.ToString();
            collection.Add(key, Value);
        }

        return collection;
    }

    public System.Collections.Specialized.NameValueCollection GetNameValueCollection(System.Xml.XmlNode section)
    {
        string temp;
        System.Collections.Specialized.NameValueCollection collection = new System.Collections.Specialized.NameValueCollection();
        Hashtable ConfigChilds = new Hashtable();
        ArrayList AllElements = new ArrayList();
        ArrayList Sections = new ArrayList();
        ArrayList innerSectionElements = new ArrayList();
        //We look for the Config nodes
        XmlNodeList xmlnlist1;
        xmlnlist1 = section.ChildNodes;
        foreach (XmlNode ConfigNode in xmlnlist1)
        {
            if ((ConfigNode.NodeType == XmlNodeType.Whitespace) || (ConfigNode.NodeType == XmlNodeType.Comment))
                continue;
            else
            {
                temp = ConfigNode.Attributes["key"].Value;
                string valuefield = ConfigNode.Attributes["value"].Value.ToString(); //We read the attibute 'value'

                collection.Add(temp, valuefield);

            }
        }
        return collection;
    }
    #endregion
}

