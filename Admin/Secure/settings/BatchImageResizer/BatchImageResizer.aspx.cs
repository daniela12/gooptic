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
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.DataAccess.Custom;
using SCommImaging.Imaging;
using System.Drawing.Imaging;
using System.Drawing;

public partial class Admin_Secure_settings_BatchImageResizer_BatchImageResizer : System.Web.UI.Page
{  
   
    #region pageload
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion

    #region events
    /// <summary>
    /// Submit Button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsubmit_click(object sender, EventArgs e)
    {
        System.IO.DirectoryInfo path = new System.IO.DirectoryInfo(Server.MapPath(ZNodeConfigManager.EnvironmentConfig.OriginalImagePath));
        string[] patterns = { "*.jpg", "*.png" , "*.gif" , "*.jpeg" };        
        foreach (string pattern in patterns)
        {
            System.IO.FileInfo[] file = path.GetFiles(pattern);

            foreach (System.IO.FileInfo _FileInfo in file)
            {                
                ZNodeImage.ResizeImage(_FileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemLargeWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.LargeImagePath));
                ZNodeImage.ResizeImage(_FileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemThumbnailWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.ThumbnailImagePath));
                ZNodeImage.ResizeImage(_FileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemMediumWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.MediumImagePath));
                ZNodeImage.ResizeImage(_FileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemSmallWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.SmallImagePath));
                ZNodeImage.ResizeImage(_FileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemSwatchWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.SwatchImagePath));
                ZNodeImage.ResizeImage(_FileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemCrossSellWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.CrossSellImagePath));
                
                // Crop Images for Swatches 
                int maxWidthHeight = ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemSwatchWidth;
                ZNodeImage.CropImage(_FileInfo, maxWidthHeight, maxWidthHeight, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.SwatchImagePath));

                lblMessage.Text = "The images have been successfully resized.";
                btnsubmit.Visible = false;
                btnCancel.Visible = false;
                btnback.Visible = true;
            }
        }           
    }

    /// <summary>
    /// Cancel Button Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btncancel_click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/settings/default.aspx?mode=setup");

    }

    /// <summary>
    /// Back Button Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnback_click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/settings/default.aspx?mode=setup");

    }
    #endregion
}
