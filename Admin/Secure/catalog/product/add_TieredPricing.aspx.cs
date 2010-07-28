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

public partial class Admin_Secure_catalog_product_add_TieredPricing : System.Web.UI.Page
{
    # region Protected Member Variables
    protected int ItemID = 0;
    protected int productTierID = 0;
    protected string viewLink = "~/admin/secure/catalog/product/view.aspx?itemid=";
    # endregion

    # region Page Load
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //Retrieve Product Id from Query string
        if (Request.Params["itemid"] != null)
        {
            ItemID = int.Parse(Request.Params["itemid"].ToString());
        }

        
        if (Request.Params["tierid"] != null)
        {
            productTierID = int.Parse(Request.Params["tierid"].ToString());
        }

        //
        if (!IsPostBack)
        {
            Bind();

            if (productTierID > 0)
            {
                //Edit Mode
                lblHeading.Text = "Edit Pricing Tier - " + GetProductName;

                BindData();
            }
            else
            {
                lblHeading.Text = "Add Pricing Tier - " + GetProductName;
            }
            
        }
    }

    #endregion

    # region Protected Properties
    /// <summary>
    /// Retrieves the product name
    /// </summary>
    protected string GetProductName
    {
        get
        {
            ProductAdmin productAdmin = new ProductAdmin();
            Product product = productAdmin.GetByProductId(ItemID);

            if (product != null)
            {
                return product.Name;
            }

            return "";
        }
        
    }
    #endregion

    # region Bind Methods

    /// <summary>
    /// Binds Profile dropdown list
    /// </summary>
    private void Bind()
    {
        ProfileAdmin profileAdmin = new ProfileAdmin();
        ddlProfiles.DataSource = profileAdmin.GetAll();
        ddlProfiles.DataTextField = "Name";
        ddlProfiles.DataValueField = "ProfileId";
        ddlProfiles.DataBind();

        ListItem li = new ListItem("Apply to All Profiles", "0");
        ddlProfiles.Items.Insert(0,li);
    }

    /// <summary>
    /// Bind edit data fields
    /// </summary>
    private void BindData()
    {
        ProductAdmin productTierAdmin = new ProductAdmin();
        ProductTier productTier = productTierAdmin.GetByProductTierId(productTierID);

        if (productTier != null)
        {
            txtPrice.Text = productTier.Price.ToString("N");
            txtTierStart.Text = productTier.TierStart.ToString();
            txtTierEnd.Text = productTier.TierEnd.ToString();
            if (productTier.ProfileID.HasValue)
                ddlProfiles.SelectedValue = productTier.ProfileID.Value.ToString();
        }
    }
    #endregion

    # region General Events

    /// <summary>
    /// Submit Button Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ProductAdmin productTierAdmin = new ProductAdmin();
        ProductTier _productTier = new ProductTier();

        if (productTierID > 0)
        {
            _productTier = productTierAdmin.GetByProductTierId(productTierID);
        }


        if (ddlProfiles.SelectedValue == "0")
        {
            _productTier.ProfileID = null;
        }
        else { _productTier.ProfileID = int.Parse(ddlProfiles.SelectedValue); }

        _productTier.TierStart = int.Parse(txtTierStart.Text.Trim());
        _productTier.TierEnd = int.Parse(txtTierEnd.Text.Trim());
        _productTier.Price = decimal.Parse(txtPrice.Text.Trim());
        _productTier.ProductID = ItemID; //Set ProductId field

        bool status = false;

        //if Edit mode, then update fields
        if (productTierID > 0)
        {
           status = productTierAdmin.UpdateProductTier(_productTier);
        }
        else
        {
            status = productTierAdmin.AddProductTier(_productTier);
        }

        if (status)
        {
            Response.Redirect(viewLink + ItemID + "&mode=tieredPricing");
        }
        else
        {
            lblError.Text = "Could not update the product Tiered pricing. Please try again.";
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(viewLink + ItemID + "&mode=tieredPricing");
    }
    #endregion
}
