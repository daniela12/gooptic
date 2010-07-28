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
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.DataAccess.Service;
using System.Data.SqlClient;
using ZNode.Libraries.ECommerce.Catalog;

public partial class Admin_Secure_catalog_Promotions_add : System.Web.UI.Page
{
    # region Protected Member Variables
    protected int ItemID = 0;
    # endregion

    # region Page Load Event
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //Get promotionid from query string
        if (Request.Params["ItemID"] != null)
        {
            ItemID = int.Parse(Request.Params["ItemID"].ToString());
        }
        if (!Page.IsPostBack)
        {
            BindQuantityList();
            this.BindData();

            discAmountValidator.Text = "Enter discount amount between " + (0.01).ToString("N") + "- 9999999";
            discPercentageValidator.Text = "Enter percentage discount between " + (0.01).ToString("N") + "- 100";

            if (ItemID > 0) //Edit Mode
            {
                lblTitle.Text = "Edit Promotion – ";
                this.BindEditData();
            }
            else
            {
                StartDate.Text = System.DateTime.Now.ToShortDateString();
                EndDate.Text = System.DateTime.Now.AddDays(30).ToShortDateString();
                lblTitle.Text = "Add a New Promotion";
            }
        }

    }
    #endregion

    #region Bind Methods
    /// <summary>
    /// Bind Edit mode fields
    /// </summary>
    public void BindEditData()
    {
        ZNode.Libraries.Admin.PromotionAdmin couponAdmin = new ZNode.Libraries.Admin.PromotionAdmin();
        ZNode.Libraries.DataAccess.Entities.Promotion promotion = couponAdmin.DeepLoadByPromotionId(ItemID);

        if (promotion != null)
        {
            // General Section
            PromotionName.Text = promotion.Name;
            Description.Text = promotion.Description;
            StartDate.Text = promotion.StartDate.ToShortDateString();
            EndDate.Text = promotion.EndDate.ToShortDateString();
            DisplayOrder.Text = promotion.DisplayOrder.ToString();

            // Discount             
            Discount.Text = promotion.Discount.ToString();
            DiscountType.SelectedValue = promotion.DiscountTypeIDSource.ClassName.ToString();
            ToggleDiscountValidator();

            if (!string.IsNullOrEmpty(promotion.DiscountTypeIDSource.ClassName))
            {
                if (promotion.ProfileID.HasValue)
                {
                    ddlProfileTypes.SelectedValue = promotion.ProfileID.Value.ToString();
                }

                if (promotion.ProductID.HasValue)
                {
                    txtReqProductId.Text = promotion.ProductID.Value.ToString();
                    txtRequiredProduct.Text = promotion.ProductIDSource.Name;
                }

                if (promotion.PromotionProductID.HasValue)
                {
                    txtPromProductId.Text = promotion.PromotionProductID.Value.ToString();

                    ProductAdmin prodAdmin = new ProductAdmin();
                    txtPromoProduct.Text = prodAdmin.GetProductName(promotion.PromotionProductID.Value);

                    ddlQuantity.SelectedValue = promotion.PromotionProductQty.GetValueOrDefault(1).ToString();

                }

                txtPromProductId.Text = promotion.PromotionProductID.GetValueOrDefault(0).ToString();
                ddlQuantity.SelectedValue = promotion.PromotionProductQty.GetValueOrDefault(0).ToString();
                ddlMinimumQty.SelectedValue = promotion.QuantityMinimum.GetValueOrDefault(1).ToString();

                // Coupon Info
                chkCouponInd.Checked = promotion.CouponInd;
                if (chkCouponInd.Checked)
                    pnlCouponInfo.Visible = true;

                CouponCode.Text = promotion.CouponCode;
                txtPromotionMessage.Text = promotion.PromotionMessage;

                if (promotion.CouponQuantityAvailable.HasValue)
                    Quantity.Text = promotion.CouponQuantityAvailable.Value.ToString();
                if (promotion.OrderMinimum.HasValue)
                    OrderMinimum.Text = promotion.OrderMinimum.Value.ToString("N2");


                // Set page Title
                lblTitle.Text += promotion.Name;
            }
            else
            {

            }
        }
    }

    /// <summary>
    /// Bind List controls 
    /// </summary>
    public void BindData()
    {
        // Bind DiscountTypes
        ZNode.Libraries.Admin.PromotionAdmin Discountname = new ZNode.Libraries.Admin.PromotionAdmin();
        DiscountType.DataSource = Discountname.GetAllDiscountTypes();
        DiscountType.DataTextField = "Name";
        DiscountType.DataValueField = "ClassName";
        DiscountType.DataBind();

        ProfileAdmin profileAdmin = new ProfileAdmin();
        ddlProfileTypes.DataSource = profileAdmin.GetAll();
        ddlProfileTypes.DataTextField = "Name";
        ddlProfileTypes.DataValueField = "ProfileID";
        ddlProfileTypes.DataBind();

        ListItem li = new ListItem("All Profiles", "0");
        ddlProfileTypes.Items.Insert(0, li);
        ddlProfileTypes.SelectedValue = "0";
    }

    /// <summary>
    /// Bind quantity drop down list
    /// </summary>
    public void BindQuantityList()
    {
        for (int i = 1; i < 15; i++)
        {
            ddlQuantity.Items.Add(i.ToString());
            ddlMinimumQty.Items.Add(i.ToString());
        }
    }
    #endregion

    #region General Events
    /// <summary>
    /// Submit Button Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ProductAdmin prodAdmin = new ProductAdmin();
        PromotionAdmin promotionAdmin = new ZNode.Libraries.Admin.PromotionAdmin();
        ZNode.Libraries.DataAccess.Entities.Promotion promotionEnity = null;

        if (ItemID > 0)
        {
            promotionEnity = promotionAdmin.GetByPromotionId(ItemID);         
        }
        else
        {
            promotionEnity = new ZNode.Libraries.DataAccess.Entities.Promotion();
        }

        // set properties
        promotionEnity.ProductID = null;
        promotionEnity.SKUID = null;
        promotionEnity.AddOnValueID = null;
        promotionEnity.AccountID = null;

        // General Info section properties
        promotionEnity.Name = PromotionName.Text.Trim();
        promotionEnity.Description = Description.Text.Trim();
        promotionEnity.StartDate = Convert.ToDateTime(StartDate.Text.Trim());
        promotionEnity.EndDate = Convert.ToDateTime(EndDate.Text.Trim());
        promotionEnity.DisplayOrder = int.Parse(DisplayOrder.Text.Trim());
        
        // DiscountType
        if (DiscountType.SelectedIndex != -1)
        {
            promotionEnity.DiscountTypeID = promotionAdmin.GetDiscountTypeId(DiscountType.SelectedItem.Text, DiscountType.SelectedValue);
        }

        decimal decDiscountAmt = Convert.ToDecimal(Discount.Text);
        promotionEnity.OrderMinimum = Convert.ToDecimal(OrderMinimum.Text);

        string className = DiscountType.SelectedValue;

        // Product Level Promotions
        if (className.Equals("ZNodePromotionPercentOffProduct", StringComparison.OrdinalIgnoreCase)
                    || className.Equals("ZNodePromotionAmountOffProduct", StringComparison.OrdinalIgnoreCase))
        {
            promotionEnity.QuantityMinimum = int.Parse(ddlMinimumQty.SelectedValue);

            if (txtReqProductId.Text != "0")
                promotionEnity.ProductID = int.Parse(txtReqProductId.Text.Trim());
            else
            {
                promotionEnity.ProductID = prodAdmin.GetProductIdByName(txtRequiredProduct.Text.Trim());
            }
        }
        else if (className.Equals("ZNodePromotionPercentXifYPurchased", StringComparison.OrdinalIgnoreCase)
                    || className.Equals("ZNodePromotionAmountOffXifYPurchased", StringComparison.OrdinalIgnoreCase))
        {
            promotionEnity.QuantityMinimum = int.Parse(ddlMinimumQty.SelectedValue);
            promotionEnity.PromotionProductQty = int.Parse(ddlQuantity.SelectedValue);

            if (txtReqProductId.Text != "0")
                promotionEnity.ProductID = int.Parse(txtReqProductId.Text.Trim());
            else
                promotionEnity.ProductID = prodAdmin.GetProductIdByName(txtRequiredProduct.Text.Trim()); ;

            if (txtPromProductId.Text != "0")
                promotionEnity.PromotionProductID = int.Parse(txtPromProductId.Text.Trim());
            else
                promotionEnity.PromotionProductID = prodAdmin.GetProductIdByName(txtPromoProduct.Text.Trim()); ;
        }

        // Set Discount field
        promotionEnity.Discount = Convert.ToDecimal(Discount.Text);

        if (ddlProfileTypes.SelectedValue == "0")
        {
            promotionEnity.ProfileID = null;
        }
        else
        {
            promotionEnity.ProfileID = int.Parse(ddlProfileTypes.SelectedValue);
        }

        // Coupon Info section
        promotionEnity.CouponInd = chkCouponInd.Checked;
        if (chkCouponInd.Checked)
        {
            promotionEnity.CouponCode = CouponCode.Text;
            promotionEnity.CouponQuantityAvailable = int.Parse(Quantity.Text);
            promotionEnity.PromotionMessage = txtPromotionMessage.Text;
        }
        else
        {
            promotionEnity.CouponCode = "";
            promotionEnity.PromotionMessage = "";
        }

        bool check = false;

        if (ItemID > 0)
        {
            check = promotionAdmin.UpdatePromotion(promotionEnity);
        }
        else
        {
            check = promotionAdmin.AddPromotion(promotionEnity);
        }

        if (check)
        {
            // Replace the Promtion Cache application object with new active promotions
            HttpContext.Current.Application["PromotionCache"] = promotionAdmin.GetActivePromotions();

            Response.Redirect("list.aspx");
        }
        else
        {
            lblError.Text = "An error occurred while updating. Please try again.";
        }
    }

    /// <summary>
    /// Cancel Button Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("list.aspx");
    }

    /// <summary>
    /// Coupon checkbox checked changed event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CouponInd_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCouponInd.Checked)
        {
            pnlCouponInfo.Visible = true;
        }
        else
        {
            pnlCouponInfo.Visible = false;
        }
    }

    /// <summary>
    /// Discount Type select index change event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DiscountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ToggleDiscountValidator();
    }
    #endregion

    # region Helper Methods
    /// <summary>
    /// Enable/Disable Percentage/Amount validator on Discount field
    /// </summary>
    private void ToggleDiscountValidator()
    {
        string className = DiscountType.SelectedValue;

        pnlProducts.Visible = false;
        discAmountValidator.Enabled = false;
        discPercentageValidator.Enabled = false;
        pnlPromotionalProduct.Visible = false;
        lblDiscAmtMessage.Visible = false;
        pnlOrderMin.Visible = true;

        if (className.Equals("ZNodePromotionPercentOffOrder", StringComparison.OrdinalIgnoreCase)) // Percentage Discount on Order
        {
            discPercentageValidator.Enabled = true;
            discAmountValidator.Enabled = false;
        }
        else if (className.Equals("ZNodePromotionPercentOffProduct", StringComparison.OrdinalIgnoreCase))//Product - Percentage Discount 
        {
            pnlProducts.Visible = true;
            discAmountValidator.Enabled = false;
            discPercentageValidator.Enabled = true;
        }
        else if (className.Equals("ZNodePromotionPercentOffShipping", StringComparison.OrdinalIgnoreCase)) // Percentage Discount on Shipping
        {
            discPercentageValidator.Enabled = true;
            discAmountValidator.Enabled = false;
        }
        else if (className.Equals("ZNodePromotionPercentXifYPurchased", StringComparison.OrdinalIgnoreCase))// Percentage off x if y purchased
        {
            lblDiscAmtMessage.Visible = true;
            pnlProducts.Visible = true;
            pnlPromotionalProduct.Visible = true;
            pnlOrderMin.Visible = false;

            discAmountValidator.Enabled = false;
            discPercentageValidator.Enabled = true;
        }
        else if (className.Equals("ZNodePromotionAmountOffOrder", StringComparison.OrdinalIgnoreCase)) // Amount off Order
        {
            discPercentageValidator.Enabled = false;
            discAmountValidator.Enabled = true;
        }
        else if (className.Equals("ZNodePromotionAmountOffProduct", StringComparison.OrdinalIgnoreCase)) // Product - Amount Discount
        {
            pnlProducts.Visible = true;
            discPercentageValidator.Enabled = false;
            discAmountValidator.Enabled = true;
        }
        else if (className.Equals("ZNodePromotionAmountOffShipping", StringComparison.OrdinalIgnoreCase)) // Amount on Shipping
        {
            discPercentageValidator.Enabled = false;
            discAmountValidator.Enabled = true;
        }
        else if (className.Equals("ZNodePromotionAmountOffXifYPurchased", StringComparison.OrdinalIgnoreCase)) // Amount off (buy 1 Get 1 free)
        {
            lblDiscAmtMessage.Visible = true;
            pnlProducts.Visible = true;
            pnlPromotionalProduct.Visible = true;
            pnlOrderMin.Visible = false;

            discAmountValidator.Enabled = true;
            discPercentageValidator.Enabled = false;
        }
        else
        {
            pnlOrderMin.Visible = true;
            lblDiscAmtMessage.Visible = true;
            pnlProducts.Visible = true;
            discAmountValidator.Enabled = false;
            discPercentageValidator.Enabled = false;
            pnlPromotionalProduct.Visible = true;
        }
    }
    #endregion
}