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
using ZNode.Libraries.Framework;
using ZNode.Libraries.DataAccess.Entities;
using SCommImaging.Imaging;
using System.Drawing.Imaging;
using System.Drawing;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.ECommerce.Catalog;

public partial class Admin_Secure_catalog_product_Highlights_add : System.Web.UI.Page
{
    # region Protected Member Variables
    protected int ItemID;    
    protected int ProductID;
    protected string ListPageLink = "~/admin/secure/catalog/product_highlights/default.aspx";
    protected string ProductListPageLink = "~/admin/secure/catalog/product/view.aspx?";
    # endregion

    # region Page Load Event
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //Get highlight id 
        if (Request.Params["itemid"] != null)
        {
            ItemID = int.Parse(Request.Params["itemid"].ToString());
        }

        if (Request.Params["productid"] != null)
        {
            ProductID = int.Parse(Request.Params["productid"].ToString());
        }

        if (!Page.IsPostBack)
        {
            BindHighLightType();           

            if (ItemID > 0)
            {
                lblHeading.Text = "Edit Product Highlight";
                Bind();
                EnablePanel();
                textadd.Visible = false;
                textedit.Visible = true; 
                tblShowImage.Visible = true;
            }
            else
            {
                textadd.Visible = true;
                textedit.Visible = false; 
                tblHighlight.Visible = true;    
            }       
        }

    }
    # endregion

    # region Bind Methods
    /// <summary>
    /// Bind Edit data
    /// </summary>
    protected void Bind()
    {
        HighlightAdmin AdminAccess = new HighlightAdmin();
        Highlight entity = AdminAccess.GetByHighlightID(ItemID);

        if (entity != null)
        {
            lblHeading.Text += " - " + entity.Name;

            if (entity.ImageFile != null && entity.ImageFile != "")
            {
                HighlightImage.ImageUrl = ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.MediumImagePath + entity.ImageFile;
            }
            else
            {
                RadioHighlightNoImage.Checked = true;
                RadioHighlightCurrentImage.Visible = false;
            }
            chkEnableHyperlink.Checked = entity.DisplayPopup;
            HighlightName.Text = entity.Name;
            Description.Html = entity.Description;
            Hyperlink.Text = entity.Hyperlink;
            HighlightType.SelectedValue = entity.HighlightTypeID.ToString();
            
            DisplayOrder.Text = Convert.ToString(entity.DisplayOrder);
            chkHyperlinkExternal.Checked = Convert.ToBoolean(entity.HyperlinkNewWinInd);
            VisibleInd.Checked = Convert.ToBoolean(entity.ActiveInd);
            txtImageAltTag.Text = entity.ImageAltTag;
        }
        else
        {
        }
    }

    /// <summary>
    /// Binds Highlight Type drop-down list
    /// </summary>
    private void BindHighLightType()
    {
        ZNode.Libraries.Admin.HighlightAdmin highlight = new HighlightAdmin();        
        HighlightType.DataSource = highlight.GetAllHighLightType();
        HighlightType.DataTextField = "Name";
        HighlightType.DataValueField = "HighlightTypeId";
        HighlightType.DataBind(); 
    }
    #endregion

    # region Events

    /// <summary>
    /// Submit Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        # region Local Variables
        System.IO.FileInfo _FileInfo = null;
        HighlightAdmin AdminAccess = new HighlightAdmin();
        Highlight entity = new Highlight();
        #endregion

        if (ItemID > 0)
        {
            entity = AdminAccess.GetByHighlightID(ItemID);
        }

        // set properties
        entity.Name = HighlightName.Text;
        
        // Using Regex remove carriage return and New line feed from the description text
        // to avoid javascript "Unterminated string" error         
        entity.Description = System.Text.RegularExpressions.Regex.Replace(Description.Html.Trim(),">\r\n<", "><");
        entity.Hyperlink = Hyperlink.Text.Trim();
        entity.PortalID = ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.PortalID;
        entity.DisplayPopup = chkEnableHyperlink.Checked;
        entity.HighlightTypeID = Convert.ToInt32(HighlightType.SelectedValue);
        entity.DisplayOrder = Convert.ToInt32(DisplayOrder.Text);
        entity.HyperlinkNewWinInd = chkHyperlinkExternal.Checked;
        entity.ActiveInd = VisibleInd.Checked;
        entity.ImageAltTag = txtImageAltTag.Text.Trim();

        #region Image Validation
        if (entity.HighlightTypeID == 1 || entity.HighlightTypeID == 2 || entity.HighlightTypeID == 4)
        {
            //Validate image
            if (RadioHighlightNoImage.Checked == false)
            {
                if ((ItemID == 0) || (RadioHighlightNewImage.Checked == true))
                {
                    if (UploadHighlightImage.PostedFile.FileName != "")
                    {
                        //Check for Product Image
                        _FileInfo = new System.IO.FileInfo(UploadHighlightImage.PostedFile.FileName);

                        if (_FileInfo != null)
                        {
                            entity.ImageFile = _FileInfo.Name;
                        }
                    }
                }
                else
                {
                    entity.ImageFile = entity.ImageFile;
                }
            }
            else
            {
                entity.ImageFile = "";
            }
        }
        else
        {
            entity.ImageFile = "";
        }

        #endregion

        // Upload File if this is a new Highlight or the New Image option was selected for an existing Highlight
        if (RadioHighlightNewImage.Checked || ItemID == 0)
        {
            if (_FileInfo != null)
            {
                UploadHighlightImage.SaveAs(Server.MapPath(ZNodeConfigManager.EnvironmentConfig.OriginalImagePath + _FileInfo.Name));
            }
            else
            {
                entity.ImageFile = "";
            }
        }        

        bool status = false;

        //
        if (ItemID > 0)
        {
            status = AdminAccess.Update(entity);
        }
        else 
        {
            status = AdminAccess.Add(entity);
        }

        if (status)
        {
            if (ProductID > 0)
            {
                Response.Redirect(ProductListPageLink + "mode=highlight&" + "itemid=" + ProductID);
            }
            else
            {
                Response.Redirect(ListPageLink);
            }
        }
        else
        {
            lblError.Text = "";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (ProductID > 0)
        {
            Response.Redirect(ProductListPageLink + "mode=highlight&" + "itemid=" +ProductID);
        }
        else
        {
            //Redirect to View Page
            Response.Redirect(ListPageLink);
        }
    }    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioHighlightCurrentImage_CheckedChanged(object sender, EventArgs e)
    {
        tblHighlight.Visible = false;
        HighlightImage.Visible = true;
    }

    /// <summary>
    /// Radio Button Check Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioHighlightNoImage_CheckedChanged(object sender, EventArgs e)
    {
        tblHighlight.Visible = false;
        HighlightImage.Visible = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioHighlightNewImage_CheckedChanged(object sender, EventArgs e)
    {
        tblHighlight.Visible = true;
        HighlightImage.Visible = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DisplayPopup_CheckedChanged(object sender, EventArgs e)
    {
        EnablePanel();
    }
    #endregion  

    # region Helper Methods
    /// <summary>
    /// 
    /// </summary>
    public void EnablePanel()
    {
        chkHyperlinkExternal.Visible = chkEnableHyperlink.Checked;
        pnlHyperLinkExternal.Visible = chkHyperlinkExternal.Checked && chkEnableHyperlink.Checked;
        pnlHyperLinkInternal.Visible = !chkHyperlinkExternal.Checked && chkEnableHyperlink.Checked;
    }
    #endregion    
    protected void HyperlinkChk_CheckedChanged(object sender, EventArgs e)
    {
        pnlHyperLinkExternal.Visible = chkHyperlinkExternal.Checked;
        pnlHyperLinkInternal.Visible = !chkHyperlinkExternal.Checked;
    }
}


