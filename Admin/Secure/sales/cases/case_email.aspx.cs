using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.Framework.Business;

public partial class Admin_Secure_sales_cases_case_email : System.Web.UI.Page
{

    # region Protected Member Variables
        protected int ItemId = 0;
    protected string RedirectLink = "~/admin/secure/sales/cases/list.aspx";
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
            this.BindData();
            if (ItemId > 0)
            {                
                this.BindValues();
            }
        }
    }

    # endregion

    # region Bind Methods

    private void BindData()
    {
        CaseAdmin _CaseAdmin = new CaseAdmin();
        CaseRequest _CaseList = _CaseAdmin.GetByCaseID(ItemId);

        if (_CaseList != null)
        {
            txtEmailSubj.Text = _CaseList.Title;
            lblEmailid.Text = _CaseList.EmailID;
        }
    }
    
    private void BindValues()
    {
        CaseAdmin _CaseAdmin = new CaseAdmin();
        CaseRequest _CaseList = _CaseAdmin.GetByCaseID(ItemId);

        if (_CaseList != null)
        {
            //Set General Case Information            
            lblCaseTitle.Text = _CaseList.Title;
            lblAccountName.Text = this.GetAccountTypeByAccountID(_CaseList.AccountID);
            lblCaseStatus.Text = this.GetCaseStatusByCaseID(_CaseList.CaseStatusID);
            lblCasePriority.Text = this.GetCasePriorityByCaseID(_CaseList.CasePriorityID);
            txtCaseDescription.Text = _CaseList.Description.Replace("<br>", "\r\n");

            //Set Customer Information
            lblCustomerName.Text = _CaseList.FirstName + " " + _CaseList.LastName;
            lblCompanyName.Text = _CaseList.CompanyName;
            lblEmailTo.Text = _CaseList.EmailID;
            lblPhoneNumber.Text = _CaseList.PhoneNumber;
        }
    }


    #endregion

    # region General Events

    /// <summary>
    /// Submit Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string strFileName = "";        

        MailMessage EmailContent = new MailMessage(ZNodeConfigManager.SiteConfig.AdminEmail, lblEmailid.Text);         
        EmailContent.Subject = txtEmailSubj.Text;
        EmailContent.Body = ctrlHtmlText.Html.ToString();
        EmailContent.IsBodyHtml = true;

        #region Attachment Steps
        
        // Get the file name 
        strFileName = Path.GetFileName(FileBrowse.PostedFile.FileName);
       
        if (!strFileName.Equals(""))
        {
            // Save the file on the server 
            FileBrowse.PostedFile.SaveAs(Server.MapPath(strFileName));

            //Email Attachment
            Attachment attach = new Attachment(Server.MapPath(strFileName));

            //Attach the created email attachment 
            EmailContent.Attachments.Add(attach);            
        }

        #endregion

        ZNodeEncryption encrypt = new ZNodeEncryption();

        string SMTPServer = string.Empty;
        string SMTPUsername = string.Empty;
        string SMTPPassword = string.Empty;

        if (ZNodeConfigManager.SiteConfig.SMTPServer != null)
        {
            SMTPServer = ZNodeConfigManager.SiteConfig.SMTPServer;
        }
        if (ZNodeConfigManager.SiteConfig.SMTPUserName != null)
        {
            SMTPUsername = encrypt.DecryptData(ZNodeConfigManager.SiteConfig.SMTPUserName);
        }
        if (ZNodeConfigManager.SiteConfig.SMTPPassword != null)
        {
            SMTPPassword = encrypt.DecryptData(ZNodeConfigManager.SiteConfig.SMTPPassword);
        }

        //create mail client and send email
        System.Net.Mail.SmtpClient emailClient = new System.Net.Mail.SmtpClient();
        emailClient.Host = SMTPServer;
        emailClient.Credentials = new NetworkCredential(SMTPUsername, SMTPPassword);
        
        //Send MailContent
        emailClient.Send(EmailContent);
                   
        EmailContent.Attachments.Dispose();          
               
        /* Delete the attachements if any */
        if (strFileName != null && strFileName != "")
        {
            File.Delete(Server.MapPath(strFileName));
        }

        Response.Redirect(RedirectLink);
    }

    /// <summary>
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(RedirectLink);
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
    # endregion
}
