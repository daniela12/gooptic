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
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.Admin;
using ZNode.Libraries.Framework.Business;

public partial class Admin_Secure_catalog_product_type_view : System.Web.UI.Page
{

    # region Private Member Variables
    protected int ItemID = 0;
    protected string AddAttributetypeLink = "~/admin/secure/catalog/product_Type/Add_AttributeType.aspx?itemid=";
    protected string ListLink = "~/admin/secure/catalog/product_Type/list.aspx";
    protected string EditLink = "~/admin/secure/catalog/product_Type/add.aspx?itemid=";
    # endregion

    # region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
         // Get ItemId from querystring        
        if (Request.Params["itemid"] != null)
        {
            ItemID = int.Parse(Request.Params["itemid"]);
        }

        if (!Page.IsPostBack)
        {
            this.BindData();
            this.BindGrid();
        }
    }
    # endregion

    # region Bind Datas

    private void BindData()
    {
        ProductTypeAdmin _AdminAccess = new ProductTypeAdmin();

        ProductType _ProductTypeList  = _AdminAccess.GetByProdTypeId(ItemID);
        

        if(_ProductTypeList != null)
        {
            lblProductType.Text = _ProductTypeList.Name;
            //lblProductTypeName.Text = _ProductTypeList.Name;
            //lblDescription.Text = _ProductTypeList.Description;
            //lblDisplayOrder.Text = _ProductTypeList.DisplayOrder.ToString();
        }

    }
    /// <summary>
    /// Binds the Grid
    /// </summary>
    private void BindGrid()
    {
        ProductTypeAdmin _ProductTypeAdmin = new ProductTypeAdmin();
        DataSet ds = new DataSet();
        ds = _ProductTypeAdmin.GetAttributeNamesByProductTypeid(ItemID, ZNodeConfigManager.SiteConfig.PortalID);
        DataView dv = new DataView(ds.Tables[0]);
        dv.Sort = "AttributeTypeId DESC";
        uxGrid.DataSource = dv;
        uxGrid.DataBind();
    }

    # endregion

    # region General Events

    protected void ProductTypeList_Click(object sender, EventArgs e)
    {
        Response.Redirect(ListLink);
    }
    protected void EditProductType_Click(object sender, EventArgs e)
    {
        Response.Redirect(EditLink + ItemID);
    }
    protected void AddAttributeType_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddAttributetypeLink + ItemID);
    }
    # endregion

    # region Grid Events

    protected void uxGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        this.BindGrid();
    }
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        this.BindGrid();
    }
    protected void uxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "page")
        {
        }
        else
        {          

            //Get the Value from the command argument
            string Id = e.CommandArgument.ToString();

            if (e.CommandName == "Delete")
            {

                AttributeTypeAdmin _Access = new AttributeTypeAdmin();
                ProductTypeAttribute _ProdTypeAttrib = new ProductTypeAttribute();
                _ProdTypeAttrib.ProductAttributeTypeID = int.Parse(Id);

                bool Check = false;
                Check = _Access.DeleteProductTypeAttribute(_ProdTypeAttrib);
                
            }
        }
    }

    # endregion

}
