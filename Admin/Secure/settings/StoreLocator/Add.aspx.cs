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
using ZNode.Libraries.DataAccess.Custom;
using System.Data.SqlClient;
using SCommImaging.Imaging;
using System.Drawing.Imaging;
using System.Drawing;
using ZNode.Libraries.ECommerce.Catalog;

public partial class Admin_Secure_settings_StoreLocator_Add : System.Web.UI.Page
{

    # region Protected Member Variables
    protected int ItemID = 0;
    protected int ProductImageID = 0;   
    # endregion

    # region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["ItemID"] != null)
        {
            ItemID = int.Parse(Request.Params["ItemID"].ToString());
        }
        if (!Page.IsPostBack)
        {
            if (ItemID > 0)
            {
                this.BindAccount();       
                this.BindEditDatas();                
                tblShowImage.Visible = true;
                lblTitle.Text = "Edit Store Detail";                
            }
            else
            {
                this.BindAccount();
                tblStoreDescription.Visible = true;           
                lblTitle.Text = "Add Store Detail";
            }
        }
    }

    #endregion

    #region Bind Method

    public void BindEditDatas()
    {
        ZNode.Libraries.Admin.StoreLocatorAdmin storeAdmin = new ZNode.Libraries.Admin.StoreLocatorAdmin();
        ZNode.Libraries.DataAccess.Entities.Store store = storeAdmin.GetByStoreID(ItemID);        
        ListAccounts.SelectedValue = store.AccountID.ToString();       
        txtstorename.Text = store.Name;
        txtaddress1.Text = store.Address1;
        txtaddress2.Text = store.Address2;
        txtaddress3.Text = store.Address3;
        txtcity.Text = store.City;
        txtstate.Text = store.State;
        txtzip.Text = store.Zip;
        txtphone.Text = store.Phone;
        txtfax.Text = store.Fax;
        txtcname.Text = store.ContactName;
        txtdisplayorder.Text = store.DisplayOrder.ToString();        
        chkActiveInd.Checked = (bool)store.ActiveInd;
        if (store.ImageFile != null)
        {
            StoreImage.ImageUrl = ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.MediumImagePath + store.ImageFile;
        }
        else
        {
            RadioStoreNoImage.Checked = true;
            RadioStoreCurrentImage.Visible = false;
        }
    }

    public void BindAccount()
    {
        ZNode.Libraries.Admin.AccountAdmin Account = new AccountAdmin();       
        //Load AccountID
        ListAccounts.DataSource = Account.GetByPortalID(ZNodeConfigManager.SiteConfig.PortalID);
        ListAccounts.DataTextField = "AccountID";
        ListAccounts.DataValueField = "AccountID";
        ListAccounts.DataBind();           
        ListItem li = new ListItem("", "0");
        ListAccounts.Items.Insert(0, li);
    }

    #endregion    

    #region General Events  

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.IO.FileInfo _FileInfo = null;
        ZNode.Libraries.Admin.StoreLocatorAdmin StoreLocatorAdmin = new StoreLocatorAdmin();
        ZNode.Libraries.DataAccess.Entities.Store store = new Store();
        if (ItemID > 0)
        {
            store = StoreLocatorAdmin.GetByStoreID(ItemID);
        }       
        store.Name = txtstorename.Text;
        store.Address1 = txtaddress1.Text;
        store.Address2 = txtaddress2.Text;
        store.Address3 = txtaddress3.Text;
        store.City = txtcity.Text;
        store.State = txtstate.Text;
        store.Zip = txtzip.Text;
        store.Phone = txtphone.Text;
        store.Fax = txtfax.Text;
        store.ContactName = txtcname.Text;
        if (ListAccounts.SelectedItem.Text.Equals(""))
        {
            store.AccountID = null;
        }
        else
        {
            store.AccountID = Convert.ToInt32(ListAccounts.SelectedValue);
        }
        if (txtdisplayorder.Text != "")
        {
            store.DisplayOrder = Convert.ToInt32(txtdisplayorder.Text);
        }
        else
        {
            store.DisplayOrder = null;
        }

        store.ActiveInd = chkActiveInd.Checked;

        #region Image Validation

        if (RadioStoreNoImage.Checked == false)
        {
            //Validate image    
            if ((ItemID == 0) || (RadioStoreNewImage.Checked == true))
            {
                if (UploadStoreImage.PostedFile.FileName != "")
                {
                    //Check for Store Image
                    _FileInfo = new System.IO.FileInfo(UploadStoreImage.PostedFile.FileName);

                    if (_FileInfo != null)
                    {
                        store.ImageFile = _FileInfo.Name;
                    }
                }
            }
            else
            {
                store.ImageFile = store.ImageFile;
            }
        }
        else
        {
            store.ImageFile = null;
        }

        #endregion

        //Upload File if this is a new Store or the New Image option was selected for an existing Store
        if (RadioStoreNewImage.Checked || ItemID == 0)
        {
            if (_FileInfo != null)
            {
                UploadStoreImage.SaveAs(Server.MapPath(ZNodeConfigManager.EnvironmentConfig.OriginalImagePath + _FileInfo.Name));
                
                ZNodeImage.ResizeImage(_FileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemLargeWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.LargeImagePath));
                ZNodeImage.ResizeImage(_FileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemThumbnailWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.ThumbnailImagePath));
                ZNodeImage.ResizeImage(_FileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemMediumWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.MediumImagePath));
                ZNodeImage.ResizeImage(_FileInfo, ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.MaxCatalogItemSmallWidth, Server.MapPath(ZNodeConfigManager.EnvironmentConfig.SmallImagePath));
            }
        }   

        bool check = false;

        if (ItemID > 0)
        {
            check = StoreLocatorAdmin.UpdateStore(store);
        }
        else
        {
            check = StoreLocatorAdmin.InsertStore(store);
        }
        if (check)
        {
            Response.Redirect("List.aspx");
        }       
        else
        {
            //display error message
            lblError.Text = "An error occurred while updating. Please try again.";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)    
    {
        Response.Redirect("List.aspx");
    }

    /// <summary>
    /// Radio Button Check Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioStoreCurrentImage_CheckedChanged(object sender, EventArgs e)
    {
        tblStoreDescription.Visible = false;
        StoreImage.Visible = true;
    }

    /// <summary>
    /// Radio Button Check Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioStoreNoImage_CheckedChanged(object sender, EventArgs e)
    {
        tblStoreDescription.Visible = false;
        StoreImage.Visible = false;
    }

    /// <summary>
    /// Radio Button Check Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioStoreNewImage_CheckedChanged(object sender, EventArgs e)
    {
        tblStoreDescription.Visible = true;
        StoreImage.Visible = true;
    }

    #endregion     
}
