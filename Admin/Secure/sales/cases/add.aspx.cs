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
using ZNode.Libraries.DataAccess.Custom;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.Admin;
//using Dart.PowerWEB.TextBox;


public partial class Admin_Secure_sales_cases_Default : System.Web.UI.Page
{
    # region Protected Member Variables
    protected string CancelLink = "~/admin/secure/sales/cases/list.aspx";
    protected string AddLink = "~/admin/secure/sales/cases/list.aspx";
    protected int itemID = 0;
    # endregion

    # region General Events

    /// <summary>
    /// page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["itemid"] != null)
        {
            itemID = int.Parse(Request.Params["itemid"].ToString());
        }

        if (!Page.IsPostBack)
        {
            //Bind data to the fields on the page
            this.BindData();

            if (itemID > 0)
            {
                lblTitle.Text = "Edit Service Request";
                lblCreateDate.Text = "Created Date :";
                txtCaseDescription.ReadOnly = true;
                txtCaseTitle.ReadOnly = true;
                this.BindEditData();
            }
            else
            {
                lblTitle.Text = "Create a New Service Request";
                lblCreateDate.Text = "Create Date :";
                lblCaseDate.Text = System.DateTime.Now.ToShortDateString();
            }
        }
    }

    /// <summary>
    /// Submit Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        CaseAdmin _CaseAdmin = new CaseAdmin();
        CaseRequest _CaseList = new CaseRequest();

        //If edit mode then retrieve data first
        if (itemID > 0 )
        {
            _CaseList = _CaseAdmin.GetByCaseID(itemID);
            
        }

        //Set Null Values
        _CaseList.OwnerAccountID = null;
        _CaseList.CaseOrigin = "admin";
        _CaseList.CreateUser = System.Web.HttpContext.Current.User.Identity.Name;
        _CaseList.CreateDte = System.DateTime.Now;
        _CaseList.PortalID = ZNodeConfigManager.SiteConfig.PortalID;

        //Set Values 
        _CaseList.Title = txtCaseTitle.Text.Trim();
        _CaseList.Description = txtCaseDescription.Text.Trim();
        _CaseList.EmailID = txtEmailID.Text.Trim();
        _CaseList.FirstName = txtFirstName.Text.Trim();
        _CaseList.LastName = txtLastName.Text.Trim();
        _CaseList.PhoneNumber = txtPhoneNo.Text.Trim();
        _CaseList.CompanyName = txtCompanyName.Text.Trim();

        if (lstAccountList.SelectedValue == "-1")
        {
            _CaseList.AccountID = null;
        }
        else
        {
            _CaseList.AccountID = int.Parse(lstAccountList.SelectedValue);
        }
                        
        if (lstCasePriority.SelectedIndex != -1)
        {
            _CaseList.CasePriorityID = int.Parse(lstCasePriority.SelectedValue);
        }
        if (lstCaseStatus.SelectedValue != null)
        {
            _CaseList.CaseStatusID = int.Parse(lstCaseStatus.SelectedValue);
        }
               

        bool retval = false;

        if (itemID  > 0)
        {
           retval = _CaseAdmin.Update(_CaseList);
        }
        else
        {
            retval = _CaseAdmin.Add(_CaseList);
        }

        if (retval)
        {
            //redirect to list page
            Response.Redirect("~/admin/secure/sales/cases/list.aspx");
        }
        else
        {
            //display error message
            lblMsg.Text = "An error occurred while updating. Please try again.";
        }
        

    }

    /// <summary>
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(CancelLink);
    }

    # endregion

    # region Bind Event

    private void BindData()
    {
        //Bind Account Type Dropdown List
        CaseAdmin _AdminAccess = new CaseAdmin();
        lstAccountList.DataSource = _AdminAccess.GetByPortalID(ZNodeConfigManager.SiteConfig.PortalID);
        lstAccountList.DataTextField = "CustomerName";
        lstAccountList.DataValueField = "AccountID";
        lstAccountList.DataBind();

        ListItem LI = new ListItem();
        LI.Text = "None";
        LI.Value = "-1";
        lstAccountList.Items.Insert(0, LI);
        lstAccountList.Items[0].Selected = true;

        //Bind Case status DropdownList
        lstCaseStatus.DataSource = _AdminAccess.GetAllCaseStatus();
        lstCaseStatus.DataTextField = "CaseStatusNme";
        lstCaseStatus.DataValueField = "CaseStatusID";
        lstCaseStatus.DataBind();

        //Bind case priority DropdownList
        lstCasePriority.DataSource = _AdminAccess.GetAllCasePriority();
        lstCasePriority.DataTextField = "CasePriorityNme";
        lstCasePriority.DataValueField = "CasePriorityID";
        lstCasePriority.DataBind();
    }

    private void BindEditData()
    {
        CaseAdmin _CaseAdmin = new CaseAdmin();
        CaseRequest _CaseList = _CaseAdmin.GetByCaseID(itemID);
                
            
        if (_CaseList != null)
        {
                if (_CaseList.AccountID == null)
                {}
                else
                {
                    lstAccountList.SelectedValue = _CaseList.AccountID.ToString();
                }
            
            if (lstCasePriority.Items.Count > 0)
            {
                lstCaseStatus.SelectedValue = _CaseList.CaseStatusID.ToString();
            }

            if (lstCaseStatus.Items.Count > 0)
            {
                lstCasePriority.SelectedValue = _CaseList.CasePriorityID.ToString();
            }

            //set Values 
            txtCaseTitle.Text = _CaseList.Title;
            txtCaseDescription.Text = _CaseList.Description.Replace("<br>", "\r\n");
            txtFirstName.Text = _CaseList.FirstName;
            txtLastName.Text = _CaseList.LastName;
            txtCompanyName.Text = _CaseList.CompanyName;
            txtEmailID.Text = _CaseList.EmailID;
            txtPhoneNo.Text = _CaseList.PhoneNumber;
            lblCaseDate.Text = _CaseList.CreateDte.ToShortDateString();
        }

    }
    # endregion

    # region Helper Methods
        
    //private string 
    # endregion
}
