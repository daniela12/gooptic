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


public partial class Admin_Secure_catalog_product_Attribute_Types_add : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    protected string CancelLink = "~/admin/secure/catalog/product_Attribute_Types/list.aspx";
    #endregion

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
            ItemId = int.Parse(Request.Params["itemid"]);
        }
        else
        {
            ItemId = 0;
        }

        if (!Page.IsPostBack)
        {
            //if edit func then bind the data fields
            if (ItemId > 0)
            {
                this.BindEditData();
                lblTitle.Text = "Edit Attribute Type";
            }
            else
            {
                lblTitle.Text = "Add Attribute Type";
            }
        }
    }
    # endregion

    # region Bind Data

    /// <summary>
    ///  Bind data to the fields 
    /// </summary>
    private void BindEditData()
    {
         AttributeTypeAdmin _AttributeTypeAccess = new AttributeTypeAdmin();
         AttributeType _AttributeTypeList = _AttributeTypeAccess.GetByAttributeTypeId(ItemId);

        //Get Attribute Type Values
         if (_AttributeTypeList != null)
         {
             Name.Text  = _AttributeTypeList.Name;
             DisplayOrder.Text = _AttributeTypeList.DisplayOrder.ToString();
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
        AttributeTypeAdmin _AttributeTypeAccess = new AttributeTypeAdmin();
        AttributeType _NewAttributetype = new AttributeType();

        //If edit mode then retrieve data first
        if (ItemId > 0)
        {
            _NewAttributetype = _AttributeTypeAccess.GetByAttributeTypeId(ItemId);
        }

        //set values
        _NewAttributetype.Name = Name.Text.Trim();
        _NewAttributetype.DisplayOrder = int.Parse(DisplayOrder.Text.Trim());
        _NewAttributetype.PortalId = ZNodeConfigManager.SiteConfig.PortalID;
        _NewAttributetype.IsPrivate = false;

        //Update or Add
        bool Checkbool = false;

        if (ItemId > 0)
        {
           Checkbool =  _AttributeTypeAccess.Update(_NewAttributetype);
        }
        else
        {
            Checkbool = _AttributeTypeAccess.Add(_NewAttributetype);
        }

        if (Checkbool)
        {
            //redirect to main page
            Response.Redirect(CancelLink);
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
        Response.Redirect(CancelLink);
    }
    # endregion

}
