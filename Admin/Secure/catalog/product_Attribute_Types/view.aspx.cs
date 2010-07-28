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
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.Framework.Business;


public partial class Admin_Secure_catalog_product_Attribute_Types_view : System.Web.UI.Page
{

    # region Private Member Variables
    protected int ItemID = 0;
    protected string AddAttributeLink = "~/admin/secure/catalog/product_Attribute_Types/add_attribute.aspx?itemid=";
    protected string ListLink = "~/admin/secure/catalog/product_Attribute_Types/list.aspx";
    protected string EditLink = "~/admin/secure/catalog/product_Attribute_Types/add.aspx?itemid=";
    # endregion

    # region Page Load

    /// <summary>
    /// page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        // Get ItemId from querystring        
        if (Request.Params["itemid"] != null)
        {
            ItemID = int.Parse(Request.Params["itemid"]);
        }

        if (!Page.IsPostBack)
        {
            this.BindGrid();
            this.BindData();
        }
    }

    # endregion

    # region Bind Datas

    private void BindData()
    {
        AttributeTypeAdmin _AdminAccess = new AttributeTypeAdmin();
        AttributeType _AttributeTypeList = _AdminAccess.GetByAttributeTypeId(ItemID);

        if (_AttributeTypeList != null)
        {
            //lblAttributeName.Text = _AttributeTypeList.Description;
            //lblAttributeName.Text = _AttributeTypeList.Name;
            //lblDisplayOrder.Text = _AttributeTypeList.DisplayOrder.ToString();
            lblAttributeType.Text = _AttributeTypeList.Name;
        }
    }

    # endregion

    # region Bind Grid

    private void BindGrid()
    {
        AttributeTypeAdmin _AdminAccess = new AttributeTypeAdmin();

        uxGrid.DataSource = _AdminAccess.GetByAttributeTypeID(ItemID);

        uxGrid.DataBind();
    }

    # endregion

    # region General Events

    /// <summary>
    /// List Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AttributeTypeList_Click(object sender, EventArgs e)
    {
        Response.Redirect(ListLink);
    }

    /// <summary>
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddAttribute_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddAttributeLink + ItemID);
    }

    protected void EditAttributeType_Click(object sender, EventArgs e)
    {
        Response.Redirect(EditLink + ItemID);
    }

    # endregion

    # region Grid Events

    /// <summary>
    /// Grid Page Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        this.BindGrid();

    }

    /// <summary>
    /// Grid Row Command Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "page")
        {
        }
        else
        {
            //Get the Value from the command argument
            string Id = e.CommandArgument.ToString();
            if (e.CommandName == "Edit")
            {              
                //Redirect to Attribute Edit page
                Response.Redirect(AddAttributeLink + ItemID + "&AttributeID=" + Id);
            }

            if (e.CommandName == "Delete")
            {
				
                AttributeTypeAdmin _Access = new AttributeTypeAdmin();
                ProductAttribute _Attribute = _Access.GetByAttributeID(int.Parse(Id));
                if (_Access.DeleteProductAttribute(_Attribute))
                {
                    //Nothing todo here
                }
                else
                {
                    FailureText.Text = "* Delete action could not be completed because the Product Attribute Value is in use.";
                }

            }
        }

    }

    protected void uxGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        this.BindGrid();
    }

    # endregion         
        
}
