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


public partial class Admin_Secure_catalog_product_Attribute_Types_add_attribute : System.Web.UI.Page
{
    # region Protected Variables
    protected int ItemID = 0;
    protected int AttributeID = 0;
    protected string viewLink = "~/admin/secure/catalog/product_Attribute_Types/view.aspx?itemid=";
    # endregion

    #region Page Load
    /// <summary>
    /// Page Load Event
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

        //Get AttributeId from QueryString
        if (Request.Params["AttributeID"] != null)
        {
            AttributeID = int.Parse(Request.Params["AttributeID"]);
        }

        
        if (!Page.IsPostBack)
        {
            //Check for Edit Mode
            if (AttributeID > 0)
            {
                //Bind Data into fields
                this.BindData();
                lblTitle.Text = "Edit Attribute";
            }
            else
            {
                lblTitle.Text = "Add Attribute";
            }
        }
    }
    # endregion

    # region Bind Methods
    /// <summary>
    /// Bind Edit Attribute Datas
    /// </summary>
    private void BindData()
    {
        //Declarations
        AttributeTypeAdmin _AdminAccess = new AttributeTypeAdmin();
        ProductAttribute _ProductAttribute = _AdminAccess.GetByAttributeID(AttributeID);

        //Check Product Attribute for null
        if (_ProductAttribute != null)
        {
            Name.Text = _ProductAttribute.Name;
            DisplayOrder.Text = _ProductAttribute.DisplayOrder.ToString();
        }                
    }
    # endregion

    # region Events
    /// <summary>
    /// Submit button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //Declarations
        AttributeTypeAdmin _AdminAccess = new AttributeTypeAdmin();
        ProductAttribute  _ProductAttribute = new  ProductAttribute();

        //Check for Edit Mode
        if (AttributeID > 0)
        {
            _ProductAttribute = _AdminAccess.GetByAttributeID(AttributeID);
        }

        //Set Values
        _ProductAttribute.Name = Name.Text.Trim();
        _ProductAttribute.DisplayOrder = int.Parse(DisplayOrder.Text.Trim());
        _ProductAttribute.AttributeTypeId = ItemID;
        _ProductAttribute.ExternalId = null;
        _ProductAttribute.OldAttributeId = null;
        _ProductAttribute.IsActive = true;

        bool status = false;

        if (AttributeID > 0)
        {
            //Update Product Attribute
            status = _AdminAccess.UpdateProductAttribute(_ProductAttribute);
        }
        else
        {            
            status = _AdminAccess.AddProductAttribute(_ProductAttribute);
        }

        if (status)
        {
            //redirect to main page
            Response.Redirect(viewLink + ItemID);
        }
        else
        {
            //display error message
            lblError.Text = "An error occurred while updating. Please try again.";
        }
    }

    /// <summary>
    /// Cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //redirect to main page
        Response.Redirect(viewLink + ItemID);
    }
    # endregion
}
