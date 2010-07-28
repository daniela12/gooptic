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

public partial class Admin_Secure_catalog_product_Views : System.Web.UI.Page
{
    #region Protected Member Variables
    protected int ItemID;
    protected static int productTypeID = 0;
    protected string ListLink = "list.aspx";
    protected string AddViewlink = "~/admin/secure/catalog/product/add_view.aspx?itemid=";
    protected string PreviewLink = "/product.aspx?zpid=";    
    #endregion

    #region page load

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get ItemId from querystring        
        if (Request.Params["itemid"] != null)
        {
            ItemID = int.Parse(Request.Params["itemid"]);
        }
        else
        {
            ItemID = 0;
        }

        if (!Page.IsPostBack)
        {            
            if (ItemID > 0)
            {
                this.BindViewData();
                ZNodeUrl _Url = new ZNodeUrl();
                PreviewLink = ZNodeConfigManager.EnvironmentConfig.ApplicationPath + "/product.aspx?zpid=" + ItemID;
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
        ZNode.Libraries.Admin.ProductAdmin ProdAdmin = new ProductAdmin();
        Product _Product = ProdAdmin.GetByProductId(ItemID);
    
        if (_Product != null)
        {
            //General Informations
            lblProdName.Text = _Product.Name;
            DataSet dsCategory = ProdAdmin.Get_CategoryByProductID(ItemID);
            StringBuilder Builder = new StringBuilder();
            foreach (System.Data.DataRow dr in dsCategory.Tables[0].Rows)
            {
                Builder.Append(ProdAdmin.GetCategorypath(dr["Name"].ToString(), dr["Parent1CategoryName"].ToString(), dr["Parent2CategoryName"].ToString()));
                Builder.Append("<br>");
            }            
            this.BindProductView();     
        }
        else
        {
            throw (new ApplicationException("Product Requested could not be found."));
        }
    }

    private void BindProductView()
    {            
        ZNode.Libraries.Admin.ProductViewAdmin image = new ProductViewAdmin();
        uxGridProductViews.DataSource = image.GetByProductID(ItemID);  
        uxGridProductViews.DataBind();       
    }
    
    #endregion

    #region Grid Events

    protected void uxGridProductViews_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGridProductViews.PageIndex = e.NewPageIndex;
        this.BindProductView();        
    }

    protected void uxGridProductViews_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        this.BindProductView();
    }

    protected void uxGridProductViews_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() == "page")
        { }
        else
        {
            if (e.CommandName == "Edit")
            {                
                Response.Redirect(AddViewlink + ItemID + "&productimageid=" + e.CommandArgument.ToString() + "&typeid=" + productTypeID);
            }
            else if (e.CommandName == "Delete")
            {             
                ProductViewAdmin _admin = new ProductViewAdmin();


                bool check = _admin.Delete(int.Parse(e.CommandArgument.ToString()));
                if (check)
                {
                    _admin.DeleteByProductID(int.Parse(e.CommandArgument.ToString()));
                }
                
            }
        }
    }

    #endregion

    #region General Events

    protected void AddProduct_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddViewlink + ItemID);
    }   
    protected void ProductList_Click(object sender, EventArgs e)
    {
        Response.Redirect(ListLink);
    }

    #endregion

}

