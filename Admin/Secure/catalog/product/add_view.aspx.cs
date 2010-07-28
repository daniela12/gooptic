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
using ZNode.Libraries.ECommerce.Catalog;
using SCommImaging.Imaging;
using System.Drawing.Imaging;
using System.Drawing;

public partial class Admin_Secure_catalog_product_add_view : System.Web.UI.Page
{

    #region Protected member variable
    protected int ItemID = 0;
    protected int ProductImageID = 0;
    protected int productTypeID = 0;
    protected string EditLink = "~/admin/secure/catalog/product/view.aspx?itemid=";
    protected string CancelLink = "~/admin/secure/catalog/product/view.aspx?itemid=";
    ZNode.Libraries.Admin.ProductViewAdmin imageAdmin = new ProductViewAdmin();
    ZNode.Libraries.DataAccess.Entities.ProductImage productImage = new ProductImage();
    #endregion

    #region page load

    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["itemid"] != null)
        {
            ItemID = int.Parse(Request.Params["itemid"].ToString());
        }
        if (Request.Params["productimageid"] != null)
        {
            ProductImageID = int.Parse(Request.Params["productimageid"].ToString());
        }
        if (Request.Params["typeid"] != null)
        {
            productTypeID = int.Parse(Request.Params["typeid"].ToString());
        }

        if (!Page.IsPostBack)
        {
            if ((ItemID > 0) && (ProductImageID > 0))
            {
                lblHeading.Text = "Edit Product Image";
                this.BindDatas(); 
                BindImageType();
                               
                tblShowImage.Visible = true;
                tblShowProductSwatchImage.Visible = true;
                txtimagename.Visible = true;
                ImagefileName.Visible = true;                
            }
            else
            {
                tblProductDescription.Visible = true;
                lblHeading.Text = "Add Product Image";
                BindImageType();                  
            }
        }
    }
    #endregion

    #region Bind event
    /// <summary>
    /// Bind Datas
    /// </summary>
    private void BindDatas()
    {
        productImage = imageAdmin.GetByProductImageID(ProductImageID);

        if (productImage != null)
        {
            txtimagename.Text = productImage.ImageFile;
            txttitle.Text = productImage.Name;
            VisibleInd.Checked = productImage.ActiveInd;
            VisibleCategoryInd.Checked = productImage.ShowOnCategoryPage;
            Image1.ImageUrl = ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.SmallImagePath + productImage.ImageFile;
            ImageType.SelectedValue = productImage.ProductImageTypeID.ToString();
            DisplayOrder.Text = productImage.DisplayOrder.GetValueOrDefault().ToString();
            txtImageAltTag.Text = productImage.ImageAltTag;
            txtAlternateThumbnail.Text = productImage.AlternateThumbnailImageFile;

            if (txtAlternateThumbnail.Text != "")
            {
                ProductSwatchImage.ImageUrl = ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.SmallImagePath + productImage.AlternateThumbnailImageFile;
                tblShowProductSwatchImage.Visible = true;                
            }
            else
            {
                RbtnProductSwatchNoImage.Checked = true;
                RbtnProductSwatchCurrentImage.Visible = false;
            }
        }
    }

    #endregion

    #region General events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.IO.FileInfo fileInfo = null;
        System.IO.FileInfo swatchfileInfo = null;   

        if (ProductImageID > 0)
        {
            productImage = imageAdmin.GetByProductImageID(ProductImageID);
        }

        productImage.Name = txttitle.Text;
        productImage.ActiveInd = VisibleInd.Checked;
        productImage.ShowOnCategoryPage = VisibleCategoryInd.Checked;
        productImage.ProductID = ItemID;
        productImage.ProductImageTypeID = Convert.ToInt32(ImageType.SelectedValue);
        productImage.DisplayOrder = int.Parse(DisplayOrder.Text.Trim());
        productImage.ImageAltTag = txtImageAltTag.Text.Trim();
        productImage.AlternateThumbnailImageFile = txtAlternateThumbnail.Text.Trim();
        
        // Validate image
        if ((ProductImageID == 0) || (RadioProductNewImage.Checked == true))
        {
            // Check for Product View Image
            fileInfo = new System.IO.FileInfo(UploadProductImage.PostedFile.FileName);           

            // 
            if (fileInfo != null)
            {
                productImage.ImageFile = fileInfo.Name;
            }          
        }
        else
        {
            productImage.ImageFile = productImage.ImageFile;
        }

        if (ImageType.SelectedItem.Value == "2")
        {
            if (!RbtnProductSwatchNoImage.Checked)
            {
                // Validate Product Swatch image
                if ((ProductImageID == 0) || productImage.AlternateThumbnailImageFile.Length == 0)
                {
                    if (UploadProductSwatchImage.PostedFile.FileName.Trim().Length != 0)
                    {
                        swatchfileInfo = new System.IO.FileInfo(UploadProductSwatchImage.PostedFile.FileName);

                        if (swatchfileInfo != null)
                        {
                            productImage.AlternateThumbnailImageFile = swatchfileInfo.Name;
                        }

                        RbtnProductSwatchNewImage.Checked = true;
                    }
                }
                else
                {
                    productImage.AlternateThumbnailImageFile = productImage.AlternateThumbnailImageFile;
                }
            }
        }

        // Upload File if this is a new product or the New Image option was selected for an existing product
        if (RadioProductNewImage.Checked || ProductImageID == 0)
        {
            if (fileInfo != null)
            {
                UploadProductImage.SaveAs(Server.MapPath(ZNodeConfigManager.EnvironmentConfig.OriginalImagePath + fileInfo.Name));

                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemLargeWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.LargeImagePath));
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemThumbnailWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.ThumbnailImagePath));
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemMediumWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.MediumImagePath));
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemSmallWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.SmallImagePath));
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemSwatchWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.SwatchImagePath));
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemCrossSellWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.CrossSellImagePath));

                if (productImage.ProductImageTypeID.GetValueOrDefault() == 2)
                {
					// Create fileInfo for Original product View Image
					fileInfo = new System.IO.FileInfo(Server.MapPath(ZNodeConfigManager.EnvironmentConfig.OriginalImagePath + fileInfo.Name));

                    if (fileInfo.Exists)
                    {
                        //int maxWidthHeight = ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemThumbnailWidth;

                        //ZNodeImage.CropImage(fileInfo, maxWidthHeight, maxWidthHeight, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.ThumbnailImagePath));

                        int maxWidthHeight = ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemSwatchWidth;

                        ZNodeImage.CropImage(fileInfo, maxWidthHeight, maxWidthHeight, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.SwatchImagePath));
                    }
                }

                // Release all resources used
                UploadProductImage.Dispose();
            }
        }
       
        // Upload File if this is a new product or the New Image option was selected for an existing product
        if (RbtnProductSwatchNewImage.Checked || ProductImageID == 0)
        {
            if (swatchfileInfo != null)
            {
                UploadProductSwatchImage.SaveAs(Server.MapPath(ZNodeConfigManager.EnvironmentConfig.OriginalImagePath + swatchfileInfo.Name));

                ZNodeImage.ResizeImage(swatchfileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemLargeWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.LargeImagePath));
                ZNodeImage.ResizeImage(swatchfileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemThumbnailWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.ThumbnailImagePath));
                ZNodeImage.ResizeImage(swatchfileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemMediumWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.MediumImagePath));
                ZNodeImage.ResizeImage(swatchfileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemSmallWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.SmallImagePath));
                ZNodeImage.ResizeImage(swatchfileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemSwatchWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.SwatchImagePath));
                ZNodeImage.ResizeImage(swatchfileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemCrossSellWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.CrossSellImagePath));

                // Release all resources used
                UploadProductSwatchImage.Dispose();
            }
        }

        bool check = false;

        if (ProductImageID > 0)
        {
            // update the Imageview
            check = imageAdmin.Update(productImage);
        }
        else
        {
            check = imageAdmin.Insert(productImage);
        }

        if (check)
        {
            Response.Redirect(EditLink + ItemID + "&mode=views");
        }
        else
        {
            // display error message
            lblError.Text = "An error occurred while updating. Please try again.";
        }
    }


    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(CancelLink + ItemID + "&mode=views");
    }

    /// <summary>
    /// Radio Button Check Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioProductCurrentImage_CheckedChanged(object sender, EventArgs e)
    {
        tblProductDescription.Visible = false;
    }

    /// <summary>
    /// Radio Button Check Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioProductNewImage_CheckedChanged(object sender, EventArgs e)
    {
        tblProductDescription.Visible = true;
    }

    /// <summary>
    /// Radio Button Check Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RbtnProductSwatchCurrentImage_CheckedChanged(object sender, EventArgs e)
    {
        tblProductSwatchDescription.Visible = false;        
    }

    /// <summary>
    /// Radio Button Check Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RbtnProductSwatchNewImage_CheckedChanged(object sender, EventArgs e)
    {
        tblProductSwatchDescription.Visible = true;
    }

    /// <summary>
    /// Radio Button Check Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RbtnProductSwatchNoImage_CheckedChanged(object sender, EventArgs e)
    {
        tblProductSwatchDescription.Visible = false;
        txtAlternateThumbnail.Text = "";
    }
    

    /// <summary>
    /// Dropdown list Seclected Index changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetVisibleData();
    }

    #endregion

    /// <summary>
    /// Binds ImageType Type drop-down list
    /// </summary>
    private void BindImageType()
    {
        ZNode.Libraries.Admin.ProductViewAdmin imageadmin = new ProductViewAdmin();
        ImageType.DataSource = imageadmin.GetImageType();
        ImageType.DataTextField = "Name";
        ImageType.DataValueField = "ProductImageTypeID";
        ImageType.DataBind();

        SetVisibleData();
    }

    protected void SetVisibleData()
    {
        AlternateThumbnail.Visible = false;
        SwatchHint.Visible = false;
        ProductHint.Visible = false;
        ProductSwatchPanel.Visible = false;         

        if (ImageType.SelectedItem.Value == "1")
        {
            AlternateThumbnail.Visible = false;
            lblImage.Text = "Product Image";
            lblImageName.Text = "Product Image File Name";
            ProductHint.Visible = true;
        }
        else if (ImageType.SelectedItem.Value == "2")
        {
            AlternateThumbnail.Visible = true;
            lblImage.Text = "Swatch Image";
            lblImageName.Text = "Swatch Image File Name";
            SwatchHint.Visible = true;            
            ProductSwatchPanel.Visible = true;
            tblProductSwatchDescription.Visible = true;
            if (ProductImageID > 0)
            {
                tblProductSwatchDescription.Visible = false;
            }
        }
    }
}
