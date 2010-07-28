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

public partial class Admin_Secure_catalog_product_Attribute_Types_delete : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    protected string RedirectLink = "~/admin/secure/catalog/product_Attribute_Types/list.aspx";
    protected string ProductAttributeTypeName = string.Empty;
    #endregion

    # region Page Load

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
            this.BindData();
        }
    }

    # endregion

    # region Bind Data

    private void BindData()
    {
        AttributeTypeAdmin _AttributeTypeAccess = new AttributeTypeAdmin();
        AttributeType _AttributeTypeList = _AttributeTypeAccess.GetByAttributeTypeId(ItemId);

        if (_AttributeTypeList != null)
        {
            ProductAttributeTypeName = _AttributeTypeList.Name;
        }

    }

    # endregion

    # region General Events

    protected void btnDelete_Click(object sender, EventArgs e)
    {
       AttributeTypeAdmin  AdminAccess = new  AttributeTypeAdmin();
             
       int ReturnValue = AdminAccess.GetCountByAttributeTypeID(ItemId);

       if (ReturnValue == 0)
       {
           AttributeType _AttributeTypeList = AdminAccess.GetByAttributeTypeId(ItemId);
           
           bool Check = false;

           if (_AttributeTypeList != null)
           {
               Check = AdminAccess.DeleteAttributeType(_AttributeTypeList);
           }

           if (Check)
           {
               Response.Redirect(RedirectLink);
           }
           else
           {
               lblErrorMsg.Text = "* Delete action could not be completed because the Product Attribute Type is in use.";
               lblErrorMsg.Visible = true;
           }
       }
       else
       {
           lblErrorMsg.Text = "* Delete action could not be completed because the Product Attribute Type is in use.";
           lblErrorMsg.Visible = true;
       }
        
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(RedirectLink);
    }

    # endregion
}
