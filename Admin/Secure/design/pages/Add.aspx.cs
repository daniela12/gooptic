using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Entities;
using System.Data.SqlClient;

public partial class Admin_Secure_design_Page_Add : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    protected string AddLink = "~/admin/secure/design/pages/add.aspx";
    protected string CancelLink = "~/admin/secure/design/pages/default.aspx";
    protected string ListLink = "~/admin/secure/design/pages/default.aspx";
    protected string NextLink = "~/admin/secure/design/pages/default.aspx";
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

        if (!Page.IsPostBack)
        {
            BindMasterPageTemplates();

            //if edit func then bind the data fields
            if (ItemId > 0)
            {
                lblTitle.Text = "Edit Content Page";
                txtName.Enabled = false;
            }
            else
            {
                lblTitle.Text = "Add a Content Page";
            }
             
            //Bind data to the fields on the page
            BindData();
        }
    }
    #endregion

    #region Bind Data
    /// <summary>
    /// Bind data to the fields 
    /// </summary>
    protected void BindData()
    {
        ContentPageAdmin pageAdmin = new ContentPageAdmin();
        ContentPage contentPage = new ContentPage(); 

        if (ItemId > 0)
        {
            contentPage = pageAdmin.GetPageByID(ItemId); 

            //set fields          
            txtName.Text = contentPage.Name.Trim();           
            txtSEOMetaDescription.Text = contentPage.SEOMetaDescription;
            txtSEOMetaKeywords.Text = contentPage.SEOMetaKeywords;
            txtSEOTitle.Text = contentPage.SEOTitle;
            txtSEOUrl.Text = contentPage.SEOURL;
            txtTitle.Text = contentPage.Title;
            ddlPageTemplateList.SelectedValue = contentPage.MasterPage;

            //get content
            ctrlHtmlText.Html = pageAdmin.GetPageHTMLByName(contentPage.Name);
            if (contentPage.Name.Contains("Home"))
            {
                pnlSEOURL.Visible = false;
            }
        }
        else
        {
           //nothing to do here
        }

    }

    /// <summary>
    /// 
    /// </summary>
    protected void BindMasterPageTemplates()
    {
        // Create instance for direcoryInfo and specify the directory 'MasterPages/Content'
        DirectoryInfo directoryInfo = new DirectoryInfo(Server.MapPath(ZNodeConfigManager.EnvironmentConfig.DataPath + "MasterPages/Content/"));

        // Determine whether the directory 'MasterPages/Content' exists.
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


    #endregion

    #region General Events
    /// <summary>
    /// Submit button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ContentPageAdmin pageAdmin = new ContentPageAdmin();
        ContentPage contentPage = new ContentPage();
        string mappedSEOUrl = "";

        bool allowDelete = true ;

        //If edit mode then retrieve data first
        if (ItemId > 0)
        {
            contentPage = pageAdmin.GetPageByID(ItemId);
            allowDelete = contentPage.AllowDelete; //override this setting
            if(contentPage.SEOURL != null)
                mappedSEOUrl = contentPage.SEOURL;
        }
        else
        {
            //add mode - check if this page name already exists
            if (!pageAdmin.IsNameAvailable(txtName.Text.Trim()))
            {
                lblMsg.Text = "A page with this name already exists in the database. Please enter a different name.";
                return;
            }
        }

        //set values
        contentPage.ActiveInd = true;
        contentPage.Name = txtName.Text.Trim();
        contentPage.PortalID = ZNodeConfigManager.SiteConfig.PortalID;
        contentPage.SEOMetaDescription = txtSEOMetaDescription.Text;
        contentPage.SEOMetaKeywords = txtSEOMetaKeywords.Text;
        contentPage.SEOTitle = txtSEOTitle.Text;
        contentPage.SEOURL = null;
        if(txtSEOUrl.Text.Trim().Length > 0)
            contentPage.SEOURL = txtSEOUrl.Text.Trim().Replace(" ", "-");
        contentPage.Title = txtTitle.Text;
        contentPage.AllowDelete = allowDelete;
        contentPage.MasterPage = ddlPageTemplateList.SelectedItem.Text;

        bool retval = false;

        if (ItemId > 0)
        {
            // update code here
            retval = pageAdmin.UpdatePage(contentPage, ctrlHtmlText.Html, HttpContext.Current.User.Identity.Name, mappedSEOUrl, chkAddURLRedirect.Checked);
        }
        else
        {
            // add code here
            retval = pageAdmin.AddPage(contentPage, ctrlHtmlText.Html, HttpContext.Current.User.Identity.Name, mappedSEOUrl, chkAddURLRedirect.Checked);
        }

        if (retval)
        {
            //redirect to main page
            Response.Redirect(NextLink);
        }
        else
        {
            if (contentPage.SEOURL != null)
            {
                //display error message
                lblMsg.Text = "Failed to update page. Please check with SEO Url settings and try again.";
            }
            else
            {
                //display error message
                lblMsg.Text = "Failed to update page. Please try again.";
            }
        }
    }

    /// <summary>
    /// Cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(CancelLink);
    }

  
    #endregion      
}
