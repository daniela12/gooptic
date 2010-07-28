using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.DataAccess.Custom;
using System.Data.SqlClient;
using SCommImaging.Imaging;
using System.Drawing.Imaging;
using System.Drawing;

public partial class Admin_Secure_categories_add : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    protected int ProductImageID = 0;
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
            BindTemplates();

            //if edit func then bind the data fields
            if (ItemId > 0)
            {
                lblTitle.Text = "Edit Category";
                tblShowImage.Visible = true;
                BindListData();
                BindEditData();
            }
            else
            {
                lblTitle.Text = "Add Category";
                tblCategoryDescription.Visible = true;
                BindListData();
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
        CategoryAdmin categoryAdmin = new CategoryAdmin();
        Category category = categoryAdmin.GetByCategoryId(ItemId);

        if (category != null)
        {            
            txtName.Text = category.Name;
            txtshortdescription.Text = category.ShortDescription; 
            ctrlHtmlText.Html = category.Description;
            ParentCategoryID.SelectedValue = category.ParentCategoryID.ToString();
            DisplayOrder.Text = category.DisplayOrder.ToString();
            VisibleInd.Checked = category.VisibleInd;

            txtTitle.Text = category.Title;  
            chkSubCategoryGridVisibleInd.Checked = category.SubCategoryGridVisibleInd;
            txtSEOMetaDescription.Text = category.SEODescription;
            txtSEOMetaKeywords.Text = category.SEOKeywords;
            txtSEOTitle.Text = category.SEOTitle;
            txtSEOURL.Text = category.SEOURL;
            Image1.ImageUrl = ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.MediumImagePath + category.ImageFile;
            txtImageAltTag.Text = category.ImageAltTag;
            try
            {
                RowDropDown.SelectedIndex = Int32.Parse(category.Custom1);
            }
            catch (Exception e)
            { RowDropDown.SelectedIndex = 0; }
            try
            {
                ColumnDropDown.SelectedIndex = Int32.Parse(category.Custom2);

            }
            catch (Exception e)
            {
                ColumnDropDown.SelectedIndex = 0;

            }
            ddlPageTemplateList.SelectedValue = category.MasterPage;
        }
        else
        {
            throw (new ApplicationException("Category Requested could not be found."));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void BindTemplates()
    {
        // Create instance for direcoryInfo and specify the directory 'MasterPages/Category'
        DirectoryInfo directoryInfo = new DirectoryInfo(Server.MapPath(ZNodeConfigManager.EnvironmentConfig.DataPath + "MasterPages/Category/"));

        // Determine whether the directory 'MasterPages/Category' exists.
        if (directoryInfo.Exists)
        {
            // Returns a master file list from the current directory.
            FileInfo[] masterFiles = directoryInfo.GetFiles("*.master");

            foreach (FileInfo masterPage in masterFiles)
            {
                string fileName = masterPage.Name;
                fileName = fileName.Replace(".master", string.Empty); // Name only

                ddlPageTemplateList.Items.Add(fileName);
            }
        }

        // Default master template
        ddlPageTemplateList.Items.Insert(0, "Default");
    }

    /// <summary>
    /// Bind category list box
    /// </summary>
    protected void BindListData()
    {
        CategoryAdmin categoryAdmin = new CategoryAdmin();
        CategoryHelper categoryHelper = new CategoryHelper();
        DataSet dsCategory=categoryHelper.GetCategoryHierarchy(ZNodeConfigManager.SiteConfig.PortalID);

        ListItem defaultitem = new ListItem("NONE - Add Category to Root Level", "0");
        defaultitem.Selected = true;
        ParentCategoryID.Items.Add(defaultitem);

        foreach (System.Data.DataRow dr in dsCategory.Tables[0].Rows)
        {
            ListItem item = new ListItem();
            item.Text = CategoryAdmin.GetCategoryPath(dr["Name"].ToString(), dr["Parent1CategoryName"].ToString(), dr["Parent2CategoryName"].ToString());
            item.Value = dr["categoryid"].ToString();
            ParentCategoryID.Items.Add(item);
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
        System.IO.FileInfo fileInfo = null;
        CategoryAdmin categoryAdmin = new CategoryAdmin();   
        Category category = new Category();
        string mappedSEOUrl = "";

        if (ItemId > 0)
        {
            category = categoryAdmin.GetByCategoryId(ItemId);

            if (category.SEOURL != null)
                mappedSEOUrl = category.SEOURL;
        }
        category.CategoryID = ItemId;
        category.PortalID = ZNodeConfigManager.SiteConfig.PortalID ;
        category.Name = txtName.Text;
        category.ShortDescription = txtshortdescription.Text;
        category.Description = ctrlHtmlText.Html;
        category.Title = txtTitle.Text;
        category.SubCategoryGridVisibleInd = chkSubCategoryGridVisibleInd.Checked;
        category.SEOTitle = txtSEOTitle.Text;
        category.SEOKeywords = txtSEOMetaKeywords.Text;
        category.SEODescription = txtSEOMetaDescription.Text;
        category.SEOURL = null;
        if(txtSEOURL.Text.Trim().Length > 0)
            category.SEOURL = txtSEOURL.Text.Trim().Replace(" ","-");

        if (int.Parse(ParentCategoryID.SelectedValue) > 0)
        {
            category.ParentCategoryID = int.Parse(ParentCategoryID.SelectedValue);
        }
        else
        {
            category.ParentCategoryID = null;
        }

        category.ImageAltTag = txtImageAltTag.Text.Trim();
        category.MasterPage = ddlPageTemplateList.SelectedItem.Text;
        category.DisplayOrder = int.Parse(DisplayOrder.Text);
        category.VisibleInd = VisibleInd.Checked;
        category.Custom1 = RowDropDown.SelectedItem.ToString();
        category.Custom2 = ColumnDropDown.SelectedIndex.ToString();

        #region Image Validation

        //Validate image
        if ((ItemId == 0) || (RadioCategoryNewImage.Checked == true))
        {
            if(UploadCategoryImage.PostedFile.FileName != "")
            {
                //Check for Product Image
                fileInfo = new System.IO.FileInfo(UploadCategoryImage.PostedFile.FileName);

                if (fileInfo != null)
                {                  
                  category.ImageFile = fileInfo.Name;                   
                }
            }
        }
        else
        {
            category.ImageFile = category.ImageFile; 
        }
        #endregion

        //Upload File if this is a new product or the New Image option was selected for an existing product
        if (RadioCategoryNewImage.Checked || ItemId == 0)
        {
            if (fileInfo != null)
            {
                UploadCategoryImage.SaveAs(Server.MapPath(ZNodeConfigManager.EnvironmentConfig.OriginalImagePath + fileInfo.Name));
                ZNodeImage image = new ZNodeImage();
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemLargeWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.LargeImagePath));
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemThumbnailWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.ThumbnailImagePath));
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemMediumWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.MediumImagePath));
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemSmallWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.SmallImagePath));
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemSwatchWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.SwatchImagePath));
                ZNodeImage.ResizeImage(fileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemCrossSellWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.CrossSellImagePath));
            }
        }   
        bool retval = false;
        
        // create transaction
        TransactionManager tranManager = ConnectionScope.CreateTransaction();

        if (ItemId > 0)
        {
            retval = categoryAdmin.Update(category);
        }
        else
        {
            retval = categoryAdmin.Add(category);
        }

        if (retval)
        {
            UrlRedirectAdmin urlRedirectAdmin = new UrlRedirectAdmin();
            bool status = false;

            try
            {
                status = urlRedirectAdmin.UpdateUrlRedirectTable(SEOUrlType.Category, mappedSEOUrl, category.SEOURL, category.CategoryID.ToString(), chkAddURLRedirect.Checked);
            }
            catch
            {
                //error occurred so rollback transaction        
                tranManager.Rollback();
                lblMsg.Text = "The SEO Friendly URL you entered is already in use on another page. Please select another name for your URL.";

                return;
            }          

            if (status) //check status whether urlmapping table updated successfully
            {
                //Commit transaction
                tranManager.Commit();

                if (ItemId > 0)
                    Response.Redirect("~/admin/secure/catalog/product_category/list.aspx");
                else
                    Response.Redirect("~/admin/secure/catalog/product_category/add_next.aspx");
            }
            else 
            {
                //error occurred so rollback transaction        
                tranManager.Rollback();

                lblMsg.Text = "Could not update the product category SEO Url. Please try again.";
            }
        }
        else
        {
            if (ItemId > 0)
                lblMsg.Text = "Could not update the product category. Please try again.";
            else
                lblMsg.Text = "Could not add the product category. Please try again.";

            return;
        }

    }

    /// <summary>
    /// Cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/catalog/product_category/list.aspx");
    }

    /// <summary>
    /// Radio Button Check Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioCategoryCurrentImage_CheckedChanged(object sender, EventArgs e)
    {
        tblCategoryDescription.Visible = false;
    }

    /// <summary>
    /// Radio Button Check Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioCategoryNewImage_CheckedChanged(object sender, EventArgs e)
    {
        tblCategoryDescription.Visible = true;
    }

    #endregion 
        
}
