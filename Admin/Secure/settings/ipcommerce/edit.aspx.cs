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
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Entities;
using System.IO;

public partial class admin_secure_settings_ipcommerce_edit : System.Web.UI.Page
{
    # region Page Load Event
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindData();
        }
    }
    #endregion

    # region General Events
    /// <summary>
    /// Submit button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        IPCommerce ipc = new IPCommerce();
        IPCommerceService ipcserv = new IPCommerceService();
        ZNodeEncryption encrypt = new ZNodeEncryption();

        ipc = ipcserv.GetByPortalID(ZNodeConfigManager.SiteConfig.PortalID);

        bool recordExists = false;

        if (ipc == null)
        {
            recordExists = false;
            ipc = new IPCommerce();

            //delete config files
            string uc = Server.MapPath("~/data/default/config/ipcommerce_users.config");
            string zc = Server.MapPath("~/data/default/config/ipcommerce_znode.config");

            File.Delete(uc);
            File.Delete(zc);
        }
        else
        {
            recordExists = true;
        }

        ipc.PortalID = ZNodeConfigManager.SiteConfig.PortalID;
        ipc.IPPFSocketId = encrypt.EncryptData(SocketID.Text);
        ipc.MerchantLogin = encrypt.EncryptData(UserID.Text);
        ipc.MerchantPassword = encrypt.EncryptData(Password.Text);
        ipc.MerchantCity = txtBillingCity.Text;
        ipc.MerchantCountryCode = lstBillingCountryCode.SelectedItem.Text;
        ipc.MerchantCustServicePhone = PhoneNumber.Text;
        ipc.MerchantName = MerchantName.Text;
        ipc.MerchantPostalCode = txtBillingPostalCode.Text;
        ipc.MerchantSocketNum = SocketNumber.Text;
        ipc.MerchantStateProv = txtBillingState.Text;
        ipc.MerchantStoreId = StoreID.Text;
        ipc.MerchantStreet1 = txtBillingStreet1.Text;
        ipc.MerchantStreet2 = txtBillingStreet2.Text;
        ipc.Custom1 = SIC.Text.Trim(); // SIC Code
        ipc.Custom5 = SerialNumber.Text.Trim(); //Serial number        
        ipc.Custom2 = chkCVVData.Checked.ToString();
        ipc.Custom3 = chkAVSData.Checked.ToString();

        //validate these settings with IP Commerce 
        ZNode.Libraries.Payment.GatewayIPCommerce gwipc = new ZNode.Libraries.Payment.GatewayIPCommerce(ipc);

        //save to database
        if (gwipc.PaymentSolutionStatus == SecurePayments.Framework.PaymentSolutionStatus.ReadyToTransact)
        {
            Hashtable elementMapping = new Hashtable();
            // Add elements to the hash table. There are no 
            // duplicate keys, which contains xpath to retrieve value from provisioned data
            elementMapping.Add("/ppbcp:MerchId", "");            
            elementMapping.Add("/ppbcp:SIC", "");
            elementMapping.Add("/ppbcp:SocketNum", "");            
            elementMapping.Add("/ppbcp:StoreId", "");

            //retrieve data
            gwipc.GetMerchantInfo("CREDIT", elementMapping);

            ipc.MerchantSocketNum = elementMapping["/ppbcp:SocketNum"].ToString();
            ipc.MerchantStoreId = elementMapping["/ppbcp:StoreId"].ToString();
            ipc.Custom4 = elementMapping["/ppbcp:MerchId"].ToString(); // Merchant ID

            if (recordExists)
            {
                ipcserv.Save(ipc);
            }
            else
            {
                ipcserv.Insert(ipc);
            }

            Response.Redirect("~/admin/secure/settings/default.aspx?mode=ipcommerce");
        }
        else
        {
            lblErrorMsg.Text = "Unable to configure IP Commerce Settings. Please verify the settings and try again. " + gwipc.ResponseText;
        }                  
        
    }

    /// <summary>
    /// Cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/settings/default.aspx?mode=ipcommerce");
    }

    #endregion

    # region Bind Methods
    /// <summary>
    /// 
    /// </summary>
    protected void BindCountries()
    {
        CountryService countryServ = new CountryService();
        TList<ZNode.Libraries.DataAccess.Entities.Country> countries = countryServ.GetByPortalIDActiveInd(ZNodeConfigManager.SiteConfig.PortalID, true);
        countries.Sort("DisplayOrder,Name");

        //Billing Drop Down List
        lstBillingCountryCode.DataSource = countries;
        lstBillingCountryCode.DataTextField = "Name";
        lstBillingCountryCode.DataValueField = "Code";
        lstBillingCountryCode.DataBind();
        lstBillingCountryCode.SelectedValue = "US";
    }

    /// <summary>
    /// Bind fields
    /// </summary>
    protected void BindData()
    {
        
        BindCountries();

        ZNodeEncryption encrypt = new ZNodeEncryption();

        IPCommerceService ips = new IPCommerceService();

        IPCommerce ipc = ips.GetByPortalID(ZNodeConfigManager.SiteConfig.PortalID);

        if (ipc != null)
        {

            try
            {
                UserID.Text = encrypt.DecryptData(ipc.MerchantLogin);
                Password.Text = encrypt.DecryptData(ipc.MerchantPassword);
                SocketID.Text = encrypt.DecryptData(ipc.IPPFSocketId);
            }
            catch
            {
                //ignore
            }

            txtBillingCity.Text = ipc.MerchantCity;
            lstBillingCountryCode.SelectedItem.Text = ipc.MerchantCountryCode;
            PhoneNumber.Text = ipc.MerchantCustServicePhone;
            MerchantName.Text = ipc.MerchantName;
            txtBillingPostalCode.Text = ipc.MerchantPostalCode;
            SocketNumber.Text = ipc.MerchantSocketNum;
            txtBillingState.Text = ipc.MerchantStateProv;
            StoreID.Text = ipc.MerchantStoreId;
            SIC.Text = ipc.Custom1;//SIC Code
            MerchantID.Text = ipc.Custom4;// Merchant ID
            SerialNumber.Text = ipc.Custom5; // Serial number
            txtBillingStreet1.Text = ipc.MerchantStreet1;
            txtBillingStreet2.Text = ipc.MerchantStreet2;
            if (ipc.Custom2 != null) //CV Data
                chkCVVData.Checked = bool.Parse(ipc.Custom2);
            if (ipc.Custom3 != null) // AVS Data
                chkAVSData.Checked = bool.Parse(ipc.Custom3);

        }
    }
     
    #endregion
}
