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

public partial class Admin_Secure_catalog_product_suppliers_add : System.Web.UI.Page
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
            Bind();

            if (ItemId > 0)
            {
                this.BindEditDatas();
                lblTitle.Text = "Edit Supplier";
            }
            else
            {
                lblTitle.Text = "Add New Supplier";
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
        SupplierAdmin supplierAdmin = new SupplierAdmin();
        ZNode.Libraries.DataAccess.Entities.Supplier supplier = supplierAdmin.GetBySupplierID(ItemId);

        if (supplier != null)
        {
            Name.Text = supplier.Name;
            Description.Text = supplier.Description;
            ContactFirstName.Text = supplier.ContactFirstName;
            ContactLastName.Text = supplier.ContactLastName;
            ContactPhone.Text = supplier.ContactPhone;
            EmailId.Text = supplier.ContactEmail;
            NotifyEmail.Text = supplier.NotificationEmailID;
            NotificationTemplate.Text = supplier.EmailNotificationTemplate;
            ChkEmailNotify.Checked = supplier.EnableEmailNotification;
            DisplayOrder.Text = supplier.DisplayOrder.ToString();
            CheckActiveInd.Checked = supplier.ActiveInd;
            txtCustom1.Text = supplier.Custom1;
            txtCustom2.Text = supplier.Custom2;
            txtCustom3.Text = supplier.Custom3;
            txtCustom4.Text = supplier.Custom4;
            txtCustom5.Text = supplier.Custom5;
            ddlSupplierTypes.SelectedValue = supplier.SupplierTypeID.GetValueOrDefault().ToString();
        }
    }

    /// <summary>
    /// Bind supplier types
    /// </summary>
    public void Bind()
    {
        SupplierAdmin supplierAdmin = new SupplierAdmin();

        ddlSupplierTypes.DataSource = supplierAdmin.GetSupplierTypes();
        ddlSupplierTypes.DataTextField = "Name";
        ddlSupplierTypes.DataValueField = "SupplierTypeID";
        ddlSupplierTypes.DataBind();
    }
    #endregion

    #region General Events

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SupplierAdmin supplierAdmin = new SupplierAdmin();
        Supplier supplier = new Supplier();
        
        if (ItemId > 0)
        {
            supplier = supplierAdmin.GetBySupplierID(ItemId);
        }

        supplier.Name = Name.Text;
        supplier.Description = Description.Text;
        supplier.ContactFirstName = ContactFirstName.Text;
        supplier.ContactLastName = ContactLastName.Text;
        supplier.ContactPhone = ContactPhone.Text;
        supplier.ContactEmail = EmailId.Text;
        supplier.NotificationEmailID = NotifyEmail.Text;
        supplier.EmailNotificationTemplate = NotificationTemplate.Text;
        supplier.EnableEmailNotification = ChkEmailNotify.Checked;
        supplier.DisplayOrder = int.Parse(DisplayOrder.Text.Trim());
        supplier.ActiveInd = CheckActiveInd.Checked;
        supplier.Custom1 = txtCustom1.Text;
        supplier.Custom2 = txtCustom2.Text;
        supplier.Custom3 = txtCustom3.Text;
        supplier.Custom4 = txtCustom4.Text;
        supplier.Custom5 = txtCustom5.Text;

        if (ddlSupplierTypes.SelectedIndex != -1)
        {
            supplier.SupplierTypeID = int.Parse(ddlSupplierTypes.SelectedValue);
        }

        bool check = false;

        if (ItemId > 0)
        {
            check = supplierAdmin.Update(supplier);
        }
        else
        {
            check = supplierAdmin.Insert(supplier);
        }

        if (check)
        {
            // redirect to main page
            Response.Redirect("list.aspx");
        }
        else
        {
            // display error message
            lblError.Text = "An error occurred while updating. Please try again.";
        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("list.aspx");
    }
    #endregion    
}
