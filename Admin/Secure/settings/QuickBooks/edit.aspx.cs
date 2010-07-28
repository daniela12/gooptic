using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Xml;
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

public partial class Admin_Secure_settings_QuickBooks_edit : System.Web.UI.Page
{
    # region Page Load Method
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (QBXmlDataSource.DataFile.Length == 0)
        {
            QBXmlDataSource.DataFile = Server.MapPath(ZNodeConfigManager.EnvironmentConfig.ConfigPath + "/QuickbooksConfig.xml");
        }

        if (!Page.IsPostBack)
        {            
            Bind();         
        }

        BindProfiles();
    }
    #endregion

    #region Bind Methods
    /// <summary>
    /// 
    /// </summary>
    public void Bind()
    {
        txtTaxItem.Text = QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='SalesTaxItem']").Attributes["MsgValue"].Value;
        txtDiscountItem.Text = QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='DiscountItemName']").Attributes["MsgValue"].Value;
        txtShippingItem.Text = QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='ShippingItemName']").Attributes["MsgValue"].Value;
        txtIncomeAccount.Text = QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='SalesIncomeAccount']").Attributes["MsgValue"].Value;
        txtCOGSAccount.Text = QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='CostofGoodsSoldAccount']").Attributes["MsgValue"].Value;
        txtAssetAccount.Text = QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='InventoryAssetAccount']").Attributes["MsgValue"].Value;
        txtSalesCustomerMessage.Text = QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='SalesReceiptMessage']").Attributes["MsgValue"].Value;
        txtCompanyFile.Text = QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='CompanyFilePath']").Attributes["MsgValue"].Value;
        lstorderDownloadType.SelectedValue = QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='OrdersDownloadType']").Attributes["MsgValue"].Value;
    }

    /// <summary>
    /// 
    /// </summary>
    protected void BindProfiles()
    {
        ProfileAdmin profileAdmin = new ProfileAdmin();
        TList<Profile> profileList = profileAdmin.GetAll();

        foreach (Profile entity in profileList)
        {
            Literal ltrl = new Literal();
            ltrl.Text = "<div class=\"FieldStyle\">" + entity.Name + "</div><div class=\"ValueStyle\">";

            TextBox textBox = new TextBox();
            textBox.ID = "txtProfile" + entity.ProfileID.ToString();            
            XmlNode node = QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='ProfileID" + entity.ProfileID.ToString() + "']");
            if (node != null)
            {
                textBox.Text = node.Attributes["MsgValue"].Value;
            }

            Literal newLine = new Literal();
            newLine.Text = "</div>";

            ControlPlaceHolder.Controls.Add(ltrl);
            ControlPlaceHolder.Controls.Add(textBox);
            ControlPlaceHolder.Controls.Add(newLine);
        }
    }    
    #endregion

    # region General Events
    /// <summary>
    /// Submit Button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {      
        QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='SalesTaxItem']").Attributes["MsgValue"].Value = txtTaxItem.Text;
        QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='DiscountItemName']").Attributes["MsgValue"].Value = txtDiscountItem.Text;
        QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='ShippingItemName']").Attributes["MsgValue"].Value = txtShippingItem.Text;
        QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='SalesIncomeAccount']").Attributes["MsgValue"].Value = txtIncomeAccount.Text;
        QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='CostofGoodsSoldAccount']").Attributes["MsgValue"].Value = txtCOGSAccount.Text;
        QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='InventoryAssetAccount']").Attributes["MsgValue"].Value = txtAssetAccount.Text;
        QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='SalesReceiptMessage']").Attributes["MsgValue"].Value = txtSalesCustomerMessage.Text;
        QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='CompanyFilePath']").Attributes["MsgValue"].Value = txtCompanyFile.Text.Trim();
        QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='OrdersDownloadType']").Attributes["MsgValue"].Value = lstorderDownloadType.SelectedValue;


        ProfileAdmin profileAdmin = new ProfileAdmin();
        TList<Profile> profileList = profileAdmin.GetAll();

        foreach (Profile entity in profileList)
        {
            TextBox txt = (TextBox)ControlPlaceHolder.FindControl("txtProfile" + entity.ProfileID.ToString());
            
             XmlNode node = QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages/Message[@MsgKey='ProfileID" + entity.ProfileID.ToString() + "']");
             if (node != null)
             {
                 node.Attributes["MsgValue"].Value = txt.Text.Trim();
             }
             else
             {

                 XmlElement element = QBXmlDataSource.GetXmlDocument().CreateElement("Message");
                 element.SetAttribute("MsgKey", "ProfileID" + entity.ProfileID.ToString());
                 element.SetAttribute("MsgDescription", "");
                 element.SetAttribute("MsgValue", txt.Text.Trim());

                 QBXmlDataSource.GetXmlDocument().SelectSingleNode("//Messages").AppendChild(element);
             }
        }

        try
        {
            QBXmlDataSource.Save();
        }
        catch
        {
            //display error message
            lblMsg.Text = "An error occurred while updating. Please try again.";
        }

        Response.Redirect("~/admin/secure/settings/default.aspx?mode=quickbooks");   
    }

    /// <summary>
    /// Cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/Secure/Settings/Default.aspx?mode=quickbooks");
    }
    #endregion
}
