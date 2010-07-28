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

public partial class Admin_Secure_catalog_product_add_addons : System.Web.UI.Page
{
    # region Private Variables
    protected static bool IsSearchEnabled = false;    
    protected string DetailsLink = "~/admin/secure/catalog/product/view.aspx?mode=addons";
    protected int ItemId = 0;
    #endregion

    # region page Load Event
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
            BindAddons();
            BindProductName();
        }
    }
    #endregion

    # region Bind Events
    /// <summary>
    /// Bind AddOn grid with filtered data
    /// </summary>
    protected void SearchAddons()
    {
        ProductAddOnAdmin AdminAccess = new ProductAddOnAdmin();
        uxAddOnGrid.DataSource = AdminAccess.SearchAddOns(txtAddonName.Text.Trim(),txtAddOnTitle.Text.Trim(), txtAddOnsku.Text.Trim());
        uxAddOnGrid.DataBind();
    }

    private void BindProductName()
    {
        ProductAdmin ProductAdminAccess = new ProductAdmin();
        Product entity = ProductAdminAccess.GetByProductId(ItemId);
        if (entity != null)
        {
            lblTitle.Text = lblTitle.Text + " for \"" + entity.Name + "\"";
        }
    }
    /// <summary>
    /// Bind Addon Grid - all Addons
    /// </summary>
    private void BindAddons()
    {
        ProductAddOnAdmin ProdAddonAdminAccess = new ProductAddOnAdmin();

        //List of Addons
        uxAddOnGrid.DataSource = ProdAddonAdminAccess.GetAllAddOns();
        uxAddOnGrid.DataBind();
    }
    #endregion

    # region Grid Events

    # region Related Addon Grid Events
    /// <summary>
    /// AddOn Grid items page event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxAddOnGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxAddOnGrid.PageIndex = e.NewPageIndex;

        if (IsSearchEnabled)
        {
            SearchAddons();
        }
        else
        {
            BindAddons();
        }
    }
    

    /// <summary>
    /// ProductAdd-Ons Grid Row command event - occurs when Manage button is fired.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxAddOnGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Page")
        {
        }
        else
        {
            // Convert the row index stored in the CommandArgument
            // property to an Integer.
            int index = Convert.ToInt32(e.CommandArgument);

            // Get the values from the appropriate
            // cell in the GridView control.
            GridViewRow selectedRow = uxAddOnGrid.Rows[index];
            
        }
    }


     #endregion

    # endregion

    # region Events

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddSelectedAddons_Click(object sender, EventArgs e)
    {
        ProductAddOnAdmin AdminAccess = new ProductAddOnAdmin();
        StringBuilder sb = new StringBuilder();

        //Loop through the grid values
        foreach (GridViewRow row in uxAddOnGrid.Rows)
        {
            CheckBox check = (CheckBox)row.Cells[0].FindControl("chkProductAddon") as CheckBox;
            //Get AddOnId
            int AddOnID = int.Parse(row.Cells[1].Text);

            if (check.Checked)
            {
                ProductAddOn ProdAddOnEntity = new ProductAddOn();

                //Set Properties
                ProdAddOnEntity.ProductID = ItemId;
                ProdAddOnEntity.AddOnID = AddOnID;

                if (AdminAccess.IsAddOnExists(ProdAddOnEntity))
                {
                    AdminAccess.AddNewProductAddOn(ProdAddOnEntity);
                    check.Checked = false;
                }
                else
                {
                    sb.Append(GetAddOnName(AddOnID) + ",");
                    lblAddOnErrorMessage.Visible = true;
                }
            }
        }

        if (sb.ToString().Length > 0)
        {
            sb.Remove(sb.ToString().Length - 1, 1);

            //Display Error message
            lblAddOnErrorMessage.Text = "The following Add-On(s) are already associated with this product.<br/>" + sb.ToString();

        }
        else
        {
            Response.Redirect(DetailsLink + "&itemid=" + ItemId);
        }               

    }
    /// <summary>
    /// Addon Search Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddOnSearch_Click(object sender, EventArgs e)
    {
        SearchAddons();
        IsSearchEnabled = true;
    }

    /// <summary>
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(DetailsLink + "&itemid=" + ItemId);
    }

    /// <summary>
    /// Clear Search Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddOnClear_Click(object sender, EventArgs e)
    {
        BindAddons(); // bind Grid
        // Reset Text fields & Bool variable
        txtAddonName.Text = "";
        txtAddOnTitle.Text = "";
        txtAddOnsku.Text = "";
        IsSearchEnabled = false;
    }
    #endregion

    # region Helper Methods
    /// <summary>
    /// Gets the name of the Addon for this AddonId
    /// </summary>
    /// <param name="addOnId"></param>
    /// <returns></returns>
    public string GetAddOnName(object addOnId)
    {
        ProductAddOnAdmin AdminAccess = new ProductAddOnAdmin();
        AddOn _addOn = AdminAccess.GetByAddOnId(int.Parse(addOnId.ToString()));

        if (_addOn != null)
        {
            return _addOn.Name;
        }

        return "";
    }
    #endregion
    
}
