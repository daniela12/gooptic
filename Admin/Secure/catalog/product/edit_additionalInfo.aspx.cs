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

public partial class Admin_Secure_catalog_product_edit_additionalInfo : System.Web.UI.Page
{
    # region Protected Member Variables
    protected int ItemID = 0;
    protected string ManagePageLink = "~/admin/secure/catalog/product/view.aspx?mode=additional&itemid=";
    protected string CancelPageLink = "~/admin/secure/catalog/product/view.aspx?mode=additional&itemid=";
    # endregion

    # region Page Load Event
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["itemid"] != null)
        {
            ItemID = int.Parse(Request.Params["itemid"].ToString());
        }

        if (!Page.IsPostBack)
        {

            if (ItemID > 0)
            {
                lblTitle.Text = "Edit Additional Info for ";

                //Bind Sku Details
                this.Bind();

            }
        }
    }
    #endregion

    # region Bind Methods
    private void Bind()
    {
        //Create Instance for Product Admin and Product entity
        ZNode.Libraries.Admin.ProductAdmin ProdAdmin = new ProductAdmin();
        Product _Product = ProdAdmin.GetByProductId(ItemID);

        if (_Product != null)
        {
            ctrlHtmlPrdFeatures.Html = _Product.FeaturesDesc;
            ctrlHtmlPrdSpec.Html = _Product.Specifications;
            CtrlHtmlProdInfo.Html = _Product.AdditionalInformation;
            lblTitle.Text += "\"" + _Product.Name + "\"";
        }

    }
    #endregion

    # region General Events
    /// <summary>
    /// Submit Button Click Event - Fires when Submit button is triggered
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ProductAdmin _ProductAdmin = new ProductAdmin();
        Product _product = new Product();
        //if edit mode then get all the values first
        if (ItemID > 0)
        {
            _product = _ProductAdmin.GetByProductId(ItemID);

        }

        _product.FeaturesDesc = ctrlHtmlPrdFeatures.Html.Trim();
        _product.Specifications = ctrlHtmlPrdSpec.Html.Trim();
        _product.AdditionalInformation = CtrlHtmlProdInfo.Html.Trim();

        bool status = false;

        try
        {
            if (ItemID > 0) //PRODUCT UPDATE
            {
                status = _ProductAdmin.Update(_product);
            }

            if (status)
            {
                Response.Redirect(ManagePageLink + ItemID);
            }
            else
            {
                lblError.Text = "Unable to update product additional information settings. Please try again.";
            }

        }
        catch (Exception)
        {
            lblError.Text = "Unable to update product additional information settings. Please try again.";
            return;
        }

    }

    /// <summary>
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(CancelPageLink + ItemID);
    }
    # endregion
}
