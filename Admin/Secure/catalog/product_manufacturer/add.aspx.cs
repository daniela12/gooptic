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

public partial class Admin_Secure_catalog_product_manufacturer_Default : System.Web.UI.Page
{
    # region Protected Member Variables
    protected int ItemId = 0;
    # endregion

    # region Page Load
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["itemid"] != null)
        {
            ItemId = int.Parse(Request.Params["itemid"].ToString());
        }
        if (!Page.IsPostBack)
        {
            if (ItemId > 0)
            {
                this.BindEditDatas();
                lblTitle.Text = "Edit Brand - " + Name.Text.Trim();
            }
            else
            {
                lblTitle.Text = "Add New Brand";
            }
        }
    }
    #endregion

    #region Bind Data

    /// <summary>
    /// Bind Edit Datas
    /// </summary>
    public void BindEditDatas()
    {
        ManufacturerAdmin ManuAdmin = new ManufacturerAdmin();
        Manufacturer _manufacturer = ManuAdmin.GetByManufactureId(ItemId);
        if (_manufacturer != null)
        {
            Name.Text = _manufacturer.Name;
            Description.Text = _manufacturer.Description;
            CheckActiveInd.Checked = _manufacturer.ActiveInd;
            if(_manufacturer.IsDropShipper.HasValue)
            DropShipInd.Checked = _manufacturer.IsDropShipper.Value;
            EmailId.Text = _manufacturer.EmailID;
            NotificationTemplate.Text = _manufacturer.EmailNotificationTemplate;
            Custom1.Text = _manufacturer.Custom1;
            Custom2.Text = _manufacturer.Custom2;
            Custom3.Text = _manufacturer.Custom3;
        }
    }
    #endregion

    #region General Events

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ManufacturerAdmin ManuAdmin = new ManufacturerAdmin();
        Manufacturer _manufacturer = new Manufacturer();
        if (ItemId > 0)
        {
            _manufacturer = ManuAdmin.GetByManufactureId(ItemId);            
        }

        _manufacturer.PortalID = ZNodeConfigManager.SiteConfig.PortalID;
        _manufacturer.Name = Name.Text;
        _manufacturer.Description = Description.Text;
        _manufacturer.ActiveInd = CheckActiveInd.Checked;
        _manufacturer.IsDropShipper = DropShipInd.Checked;
        _manufacturer.EmailID = EmailId.Text.Trim();
        _manufacturer.EmailNotificationTemplate = NotificationTemplate.Text.Trim();
        _manufacturer.Custom1 = Custom1.Text.Trim();
        _manufacturer.Custom2 = Custom2.Text.Trim();
        _manufacturer.Custom3 = Custom3.Text.Trim();

        bool check = false;

        if (ItemId > 0)
        {

            check = ManuAdmin.Update(_manufacturer);
        }
        else
        {
            check = ManuAdmin.Insert(_manufacturer);
        }

        if (check)
        {
            //redirect to main page
            Response.Redirect("list.aspx");
        }
        else
        {
            //display error message
            lblError.Text = "An error occurred while updating. Please try again.";
        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("list.aspx");
    }
    #endregion    

}       
        


   
