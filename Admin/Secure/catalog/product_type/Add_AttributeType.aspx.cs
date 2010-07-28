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

public partial class Admin_Secure_catalog_product_type_Add_AttributeType : System.Web.UI.Page
{
    # region Private Member Variables
    protected int ItemID = 0;
    protected string RedirectLink = "~/admin/secure/catalog/product_Type/view.aspx?itemid=";
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
            this.BindList();
        }
    }
    # endregion

    # region Bind Data
    /// <summary>
    /// Binds the Attribute type DropDownList
    /// </summary>
    private void BindList()
    {
        AttributeTypeAdmin _AttributeAdmin = new AttributeTypeAdmin();

        lstAttributeTypeList.DataSource = _AttributeAdmin.GetAll();
        lstAttributeTypeList.DataTextField = "Name";
        lstAttributeTypeList.DataValueField = "AttributeTypeId";
        lstAttributeTypeList.DataBind();
    }
    # endregion

    # region General Events

    /// <summary>
    /// Submit Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        AttributeTypeAdmin _AttributeAdmin = new AttributeTypeAdmin();
        ProductTypeAttribute _typeList = new ProductTypeAttribute();

        //Set Values
        _typeList.ProductTypeId = ItemID;
        
        if (lstAttributeTypeList.SelectedIndex != -1)
        {
           _typeList.AttributeTypeId = int.Parse(lstAttributeTypeList.SelectedValue);
        }

        bool Check = false;
        
        Check =  _AttributeAdmin.AddProductTypeAttribute(_typeList);

        if (Check)
        {
            //Redirect to List Page
            Response.Redirect(RedirectLink + ItemID);
        }
        else
        {
            //Display Error Message
            lblError.Text = "An error occurred while updating. Please try again.";
            lblError.Visible = true;
        }

    }

    /// <summary>
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Redirect to Main Page
        Response.Redirect(RedirectLink  + ItemID);
    }

    # endregion
}
