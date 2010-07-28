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
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Entities;
using System.Data.SqlClient;

public partial class Admin_Secure_ProductTypes_add : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
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

        if (Page.IsPostBack == false)
        {
            //if edit func then bind the data fields
            if (ItemId > 0)
            {
                lblTitle.Text = "Edit Product Type";
                BindEditData();
            }
            else
            {
                lblTitle.Text = "Add Product Type";
              
            }
        }
    }
    #endregion

    #region Bind Edit Data
    /// <summary>
    /// Bind data to the fields on the edit screen
    /// </summary>
    protected void BindEditData()
    {
        ProductTypeAdmin ProdTypeAdmin = new ProductTypeAdmin();
        ProductType ProductTypes = ProdTypeAdmin.GetByProdTypeId(ItemId);


        if (ProductTypes != null)
        {
            Name.Text = ProductTypes.Name;
            Description.Text = ProductTypes.Description;
            DisplayOrder.Text = ProductTypes.DisplayOrder.ToString();
        }
        else
        {
            throw (new ApplicationException("Product Type Requested could not be found."));
        }
    }

   
    #endregion

    #region General Events
    /// <summary>
    /// Submit button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ProductTypeAdmin prodTypeAdmin = new ProductTypeAdmin();
        ProductType prodType = new ProductType();
        prodType.ProductTypeId = ItemId;
        prodType.PortalId  = ZNodeConfigManager.SiteConfig.PortalID ;
        prodType.Name = Name.Text;
        prodType.Description = Description.Text;
        prodType.DisplayOrder = Convert.ToInt32(DisplayOrder.Text);

        bool check = false;
       
        if (ItemId > 0)
        {
            
            check = prodTypeAdmin.Update(prodType);
        }
        else
        {
            check = prodTypeAdmin.Add(prodType);
        }

        if (check)
        {
            //redirect to main page
            Response.Redirect("list.aspx");
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
        Response.Redirect("list.aspx");
    }

    #endregion 
}
