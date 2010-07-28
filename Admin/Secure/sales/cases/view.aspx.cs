using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.Admin;


public partial class Admin_Secure_sales_cases_view : System.Web.UI.Page
{

    # region Protected Member Variables
        protected int ItemID = 0;
    protected string listLink = "~/admin/secure/sales/cases/list.aspx";
    protected string EditLink = "~/admin/secure/sales/cases/add.aspx?itemid=";
    protected string EmailLink = "~/admin/secure/sales/cases/case_email.aspx?itemid=";
    protected string AddNoteLink = "~/admin/secure/sales/cases/note_add.aspx?itemid=";
    # endregion

    # region General Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.Params["itemid"] != null)
        {
            ItemID = int.Parse(Request.Params["itemid"].ToString());
        }

        if(!Page.IsPostBack)
        {
            if(ItemID > 0)
            {
                    this.BindNotes();
                    this.BindValues();
            }
        }
    }

    /// <summary>
    /// Add New Note Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddNewNote_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddNoteLink  + ItemID.ToString());
    }

    /// <summary>
    /// Case List Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CaseList_Click(object sender, EventArgs e)
    {
        Response.Redirect(listLink);
    }

    /// <summary>
    /// Edit Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CaseEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect(EditLink + ItemID.ToString());
    }

    /// <summary>
    /// Reply to Customer Button  Click Event 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ReplyToCase_Click(object sender, EventArgs e)
    {
        Response.Redirect(EmailLink + ItemID);
    }

    # endregion

    # region Bind Data

    /// <summary>
    /// //Bind Repeater
    /// </summary>
    protected void BindNotes()
    {
        NoteAdmin _NoteAdmin = new NoteAdmin();

        CustomerNotes.DataSource = _NoteAdmin.GetByCaseID(ItemID);
        CustomerNotes.DataBind();
    }


    private void BindValues()
    {
        CaseAdmin _CaseAdmin = new CaseAdmin();
        CaseRequest _CaseList = _CaseAdmin.GetByCaseID(ItemID);

        if (_CaseList != null)
        {
            //Set General Case Information
            lblCreatedDate.Text = _CaseList.CreateDte.ToShortDateString();
            lblTitle.Text = _CaseList.Title;
            lblCaseTitle.Text = _CaseList.Title;
            lblAccountName.Text = this.GetAccountTypeByAccountID(_CaseList.AccountID);
            lblCaseStatus.Text = this.GetCaseStatusByCaseID(_CaseList.CaseStatusID);
            lblCasePriority.Text = this.GetCasePriorityByCaseID(_CaseList.CasePriorityID);
            //txtCaseDescription.Text = _CaseList.Description;
            lblCaseDescription.Text = _CaseList.Description;
            //Set Customer Information

            lblCustomerName.Text = _CaseList.FirstName + " " + _CaseList.LastName;
            lblCompanyName.Text = _CaseList.CompanyName;
            lblEmailID.Text = _CaseList.EmailID;
            lblPhoneNumber.Text = _CaseList.PhoneNumber;


        }
    }
    # endregion

    # region Helper Methods

    private string GetAccountTypeByAccountID(Object FieldValue)
    {
        if (FieldValue == null)
        {
            return string.Empty;
        }
        else
        {
            CaseAdmin _CaseAdmin = new CaseAdmin();
            AccountType _AccountTypeList = _CaseAdmin.GetByAccountTypeID(int.Parse(FieldValue.ToString()));

            if (_AccountTypeList != null)
            {
                return _AccountTypeList.AccountTypeNme;
            }
            else
            {
                return String.Empty;
            }

        }
    }
    

    private string GetCaseStatusByCaseID(Object FieldValue)
    {
        if (FieldValue == null)
        {
            return String.Empty;
        }
        else
        {
            CaseAdmin _CaseStatusAdmin = new CaseAdmin();
            CaseStatus _caseStatusList = _CaseStatusAdmin.GetByCaseStatusID(int.Parse(FieldValue.ToString()));
            if (_caseStatusList == null)
            {
                return string.Empty;
            }
            else
            {
                return _caseStatusList.CaseStatusNme;
            }

        }
    }

    private string GetCasePriorityByCaseID(Object FieldValue)
    {
        if (FieldValue == null)
        {
            return String.Empty;
        }
        else
        {
            CaseAdmin _CasePriorityAdmin = new CaseAdmin();
            CasePriority _CasePriority = _CasePriorityAdmin.GetByCasePriorityID(int.Parse(FieldValue.ToString()));
            if (_CasePriority == null)
            {
                return string.Empty;
            }
            else
            {
                return _CasePriority.CasePriorityNme;
            }

        }
    }

    /// <summary>
    /// Format Note Description 
    /// </summary>
    /// <param name="Field1"></param>
    /// <param name="Field2"></param>
    /// <param name="Field3"></param>
    /// <returns></returns>
    protected string FormatCustomerNote(Object Field1, Object Field2, Object Field3)
    {
        StringBuilder Build = new StringBuilder();
        Build.Append(Field1);
        Build.Append(" - ");
        Build.Append(Field2);
        Build.Append(" on ");
        Build.Append(Field3);
        return Build.ToString();

    }

    # endregion
    
}
