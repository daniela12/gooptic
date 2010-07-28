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
using ZNode.Libraries.DataAccess.Custom;
using System.Data.SqlClient;

public partial class Admin_Secure_catalog_product_addons_add : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    protected int ProductId = 0;
    protected bool mode = false;
    protected int AddOnValueId = 0;    
    protected string ListLink = "~/admin/secure/catalog/product_addons/list.aspx";
    protected string ViewLink = "~/admin/secure/catalog/product_addons/view.aspx?itemid=";
    protected string CancelLink = "~/admin/secure/catalog/product_addons/view.aspx?itemid=";
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

        // Get productid from querystring        
        if (Request.Params["zpid"] != null)
        {
            ProductId = int.Parse(Request.Params["zpid"]);
        }

        //Reset the edit fields
        if (Request.Params["mode"] != null)
        {
            mode = bool.Parse(Request.Params["mode"]);
        }

        // Redirect directly to product details page
        if (mode)
        {
            //Reset the URL with Product details page
            ListLink = "~/admin/secure/catalog/product/view.aspx?itemid=" + ProductId + "&mode=addons";
            ViewLink = ListLink;
            CancelLink = ListLink;
        }

        if (Page.IsPostBack == false)
        {   
            //if edit func then bind the data fields
            if (ItemId > 0)
            {
                lblTitle.Text = "Edit Add-On : ";                
                BindEditData();                
            }
            else
            {
                lblTitle.Text = "Add Product Add-On";                
            }
        }
    }
    #endregion

    #region Bind Data

    /// <summary>
    /// Bind data to the fields on the edit screen
    /// </summary>
    protected void BindEditData()
    {
        ProductAddOnAdmin AddOnAdmin = new ProductAddOnAdmin();
        AddOn addOnEntity = AddOnAdmin.GetByAddOnId(ItemId);
        
        if (addOnEntity != null)
        {
            lblTitle.Text += addOnEntity.Name;
            txtName.Text = addOnEntity.Name;
            txtAddOnTitle.Text = addOnEntity.Title;
            txtDisplayOrder.Text = addOnEntity.DisplayOrder.ToString();
            chkOptionalInd.Checked = addOnEntity.OptionalInd;
            ctrlHtmlText.Html = addOnEntity.Description;
            //if (addOnEntity.DisplayType != null)
                ddlDisplayType.SelectedValue = addOnEntity.DisplayType;
            //else
            //    ddlDisplayType.SelectedIndex = 0;

            //Inventory Setting - Out of Stock Options
            
            if ((addOnEntity.TrackInventoryInd) && (addOnEntity.AllowBackOrder == false))
            {
                InvSettingRadiobtnList.Items[0].Selected = true;
            }
            else if (addOnEntity.TrackInventoryInd && addOnEntity.AllowBackOrder)
            {
                InvSettingRadiobtnList.Items[1].Selected = true;
            }
            else if ((addOnEntity.TrackInventoryInd == false) && (addOnEntity.AllowBackOrder == false))
            {
                InvSettingRadiobtnList.Items[2].Selected = true;
            }
            

            //Inventory Setting - Stock Messages
            txtInStockMsg.Text = addOnEntity.InStockMsg;
            txtOutofStock.Text = addOnEntity.OutOfStockMsg;
            txtBackOrderMsg.Text = addOnEntity.BackOrderMsg;
        }
        else
        {
            throw (new ApplicationException("Add-On Requested could not be found."));
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
        ProductAddOnAdmin AddonAdmin = new ProductAddOnAdmin();
        AddOn AddOnEntityObject = new AddOn();

        if (ItemId > 0)
        {
            AddOnEntityObject = AddonAdmin.GetByAddOnId(ItemId);
        }
        //Set properties - General settings
        AddOnEntityObject.Name = txtName.Text.Trim();
        AddOnEntityObject.Title = txtAddOnTitle.Text.Trim();
        AddOnEntityObject.DisplayOrder = int.Parse(txtDisplayOrder.Text.Trim());
        AddOnEntityObject.OptionalInd = chkOptionalInd.Checked;
        AddOnEntityObject.Description = ctrlHtmlText.Html;
        AddOnEntityObject.DisplayType = ddlDisplayType.SelectedItem.Value;

        //Set properties - Inventory settings
        //Out of Stock Options
        if (InvSettingRadiobtnList.SelectedValue.Equals("1"))
        {
            //Only Sell if Inventory Available - Set values
            AddOnEntityObject.TrackInventoryInd = true;
            AddOnEntityObject.AllowBackOrder = false;
        }
        else if (InvSettingRadiobtnList.SelectedValue.Equals("2"))
        {
            //Allow Back Order - Set values
            AddOnEntityObject.TrackInventoryInd = true;
            AddOnEntityObject.AllowBackOrder = true;
        }
        else if (InvSettingRadiobtnList.SelectedValue.Equals("3"))
        {
            //Don't Track Inventory - Set property values
            AddOnEntityObject.TrackInventoryInd = false;
            AddOnEntityObject.AllowBackOrder = false;
        }

        //Inventory Setting - Stock Messages
        if (txtOutofStock.Text.Trim().Length == 0)
        {
            AddOnEntityObject.OutOfStockMsg = "Out of Stock";
        }
        else
        {
            AddOnEntityObject.OutOfStockMsg = txtOutofStock.Text.Trim();
        }
        AddOnEntityObject.InStockMsg = txtInStockMsg.Text.Trim();
        AddOnEntityObject.BackOrderMsg = txtBackOrderMsg.Text.Trim();
        

        bool retval = false;

        if (ItemId > 0)
        {
            retval = AddonAdmin.UpdateNewProductAddOn(AddOnEntityObject);            
        }
        else
        {            
            retval = AddonAdmin.CreateNewProductAddOn(AddOnEntityObject,out ItemId);           
        }


        if (retval)
        {
            if (mode)
            {
                Response.Redirect(ViewLink);
            }
           
            Response.Redirect(ViewLink + ItemId);
        }
        else
        {
            lblMsg.Text = "Could not update the product Add-On. Please try again.";
            lblMsg.CssClass = "Error";
            return;
        }
    }

    /// <summary>
    /// Cancel Button Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (ItemId > 0)
        {
            Response.Redirect(CancelLink + ItemId);
        }
        else
        {
            Response.Redirect(ListLink);
        }
    }
    # endregion
}
