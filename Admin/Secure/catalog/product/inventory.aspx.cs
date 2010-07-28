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


public partial class Admin_Secure_products_inventory : System.Web.UI.Page
{

    #region Protected Member Variables
    protected int ItemId;
    protected static int productTypeID = 0;
    protected string AddLink = "add.aspx?itemid=";
    protected string AddRelatedItemLink = "addrelateditems.aspx?itemid=";
    protected string ListLink = "list.aspx";
    protected string PreviewLink = "/product.aspx?zpid=";
    protected string AddSKULink = "~/admin/secure/catalog/product/add_sku.aspx?itemid=";
    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
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
            butAddNewSKU.Enabled = false;

            if (ItemId > 0)
            {
                this.BindViewData();
                ZNodeUrl _Url = new ZNodeUrl();
                PreviewLink = ZNodeConfigManager.EnvironmentConfig.ApplicationPath + "/product.aspx?zpid=" + ItemId;
            }

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


    }
    #endregion

    #region Bind Datas

    /// <summary>
    /// Binding Product Values into label Boxes
    /// </summary>
    public void BindViewData()
    {
        //Create Instance for Product Admin and Product entity
        ZNode.Libraries.Admin.ProductAdmin ProdAdmin = new ProductAdmin();
        Product _Product = ProdAdmin.GetByProductId(ItemId);

        DataSet ds = ProdAdmin.GetProductDetails(ItemId);

        //Check for Number of Rows
        if (ds.Tables[0].Rows.Count != 0)
        {
            //Check For Product Type
            productTypeID = int.Parse(ds.Tables[0].Rows[0]["ProductTypeId"].ToString());
            int Count = ProdAdmin.GetAttributeCount(int.Parse(ds.Tables[0].Rows[0]["ProductTypeId"].ToString()), ZNodeConfigManager.SiteConfig.PortalID);
            if (Count > 0)
            {
                //DivAddSKU.Visible = true;           
                butAddNewSKU.Enabled = true;
            }
            else
            {
                lblmessage.Text = "Note: You can add multiple SKUs or Part Numbers to a product only if the product has attributes.";
                butAddNewSKU.Enabled = false;
            }

        }

        if (_Product != null)
        {
            //General Informations
            lblProdName.Text = _Product.Name;

            //Binding product Category 
            DataSet dsCategory = ProdAdmin.Get_CategoryByProductID(ItemId);
            StringBuilder Builder = new StringBuilder();
            foreach (System.Data.DataRow dr in dsCategory.Tables[0].Rows)
            {
                Builder.Append(ProdAdmin.GetCategorypath(dr["Name"].ToString(), dr["Parent1CategoryName"].ToString(), dr["Parent2CategoryName"].ToString()));
                Builder.Append("<br>");
            }

            //Bind Grid - SKU
            this.BindSKU();

        }
        else
        {
            throw (new ApplicationException("Product Requested could not be found."));
        }
    }

    private void BindSKU()
    {
        SKUAdmin _SkuAdmin = new SKUAdmin();
        DataSet MyDatas = _SkuAdmin.GetByProductID(ItemId);
        uxGridInventoryDisplay.DataSource = MyDatas;
        uxGridInventoryDisplay.DataBind();

        if (MyDatas.Tables[0].Rows.Count == 0)
        {
            butAddNewSKU.Enabled = true; //override previous logic
        }
    }

    #endregion

    #region Grid Events


    /// <summary>
    /// Event triggered when the grid(Inventory) page is changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGridInventoryDisplay_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGridInventoryDisplay.PageIndex = e.NewPageIndex;
        this.BindSKU();
    }

    /// <summary>
    /// Event triggered when the Grid Row is Deleted
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGridInventoryDisplay_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        this.BindSKU();
    }

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
                if (check)
                {
                    _AdminAccess.DeleteBySKUId(int.Parse(e.CommandArgument.ToString()));
                }
            }
        }
    }

    #endregion

    # region Events



    protected void AddSKU_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddSKULink + ItemId + "&typeid=" + productTypeID);
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

    # endregion

    #region Helper Functions

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
                return FieldValue.ToString() + " lbs";
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
            if (Convert.ToInt32(FieldValue) == 0)
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

    # endregion

}
