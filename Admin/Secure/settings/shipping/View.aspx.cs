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

public partial class Admin_Secure_settings_shipping_View : System.Web.UI.Page
{
    #region Private Variables
    protected int ItemId;
    protected string ListLink = "~/admin/secure/settings/shipping/default.aspx";
    protected string AddRuleLink = "~/admin/secure/settings/shipping/addrule.aspx";
    protected string EditOptionLink = "~/admin/secure/settings/shipping/add.aspx";
    #endregion

    #region Bind Data
    /// <summary>
    /// Bind data to the fields 
    /// </summary>
    protected void BindData()
    {
        ShippingAdmin shipAdmin = new ShippingAdmin();
        StoreSettingsAdmin settingsAdmin = new StoreSettingsAdmin();

        if (ItemId > 0)
        {
            Shipping shippingOption = shipAdmin.GetShippingOptionById(ItemId);

            lblShippingType.Text = shipAdmin.GetShippingTypeName(shippingOption.ShippingTypeID);
            if (shippingOption.ProfileID.HasValue)
                lblProfileName.Text = shipAdmin.GetProfileNamee((int)shippingOption.ProfileID);
            else
                lblProfileName.Text = "All Profiles";

            lblDescription.Text  = shippingOption.Description;
            lblShippingCode.Text = shippingOption.ShippingCode;

            if (shippingOption.HandlingCharge > 0)
            {
                lblHandlingCharge.Text = shippingOption.HandlingCharge.ToString("N2");
            }
            else
            {
                lblHandlingCharge.Text = "0.00";
            }

            if (shippingOption.DestinationCountryCode != null)
            {
                if (shippingOption.DestinationCountryCode.Length > 0)
                {
                    lblDestinationCountry.Text = shippingOption.DestinationCountryCode;
                }
            }
            else
            {
                lblDestinationCountry.Text = "All Countries";
            }

            imgActive.Src = ZNodeHelper.GetCheckMark((bool)shippingOption.ActiveInd);
            lblDisplayOrder.Text = shippingOption.DisplayOrder.ToString();

            if (shippingOption.ShippingTypeID == 1)
            {
                pnlShippingRuletypes.Visible = true;
            }
        }
        else
        {
            //nothing to do here
        }
    }

    /// <summary>
    /// Bind data to grid
    /// </summary>
    private void BindGridData()
    {
        ShippingAdmin shipAdmin = new ShippingAdmin();

        uxGrid.DataSource = shipAdmin.GetShippingRules(ItemId);
        uxGrid.DataBind();
    }


    #endregion

    #region Events
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
            //Bind data to the fields on the page
            BindData();
            BindGridData();
        }
    }
   
    protected void Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(ListLink);
    }
    
    protected void EditShipping_Click(object sender, EventArgs e)
    {
        EditOptionLink = EditOptionLink + "?itemid=" + ItemId.ToString();
        Response.Redirect(EditOptionLink);
    }

    protected void btnAddRule_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddRuleLink + "?sid=" + ItemId.ToString());
    }

    #endregion

    #region Grid Events
    /// <summary>
    /// Event triggered when the grid page is changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        BindGridData();
    }

    /// <summary>
    /// Event triggered when an item is deleted from the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        BindGridData();
    }

    /// <summary>
    /// Event triggered when a command button is clicked on the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "page")
        {
        }
        else
        {
            // Convert the row index stored in the CommandArgument
            // property to an Integer.
            int index = Convert.ToInt32(e.CommandArgument);

            // Get the values from the appropriate
            // cell in the GridView control.
            GridViewRow selectedRow = uxGrid.Rows[index];

            TableCell Idcell = selectedRow.Cells[0];
            string Id = Idcell.Text;


            if (e.CommandName == "Edit")
            {
                AddRuleLink = AddRuleLink + "?itemid=" + Id + "&sid=" + ItemId.ToString();
                Response.Redirect(AddRuleLink);
            }
            else if (e.CommandName == "Delete")
            {

                ShippingAdmin shipAdmin = new ShippingAdmin();
                ShippingRule shipRule = new ShippingRule();
                shipRule.ShippingRuleID = int.Parse(Id);

                shipAdmin.DeleteShippingRule(shipRule);
                
            }
        }
    }

    #endregion
   
}
