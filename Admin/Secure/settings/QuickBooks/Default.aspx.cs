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
using System.Xml;
using System.Text;
using System.IO;

public partial class Admin_Secure_settings_QuickBooks_Default : System.Web.UI.Page
{
    # region Events
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Download 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DownloadQWC_Click(object sender, EventArgs e)
    {
        XmlDocument XMLDoc = null;
        string appUrl = Request.Url.GetLeftPart(UriPartial.Authority) + ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.ApplicationPath;
        appUrl = appUrl.Replace("http:", "https:");
        appUrl += "/WebServices/QuickBooks/";

        if (ddlServiceType.SelectedValue == "Order")
        {
            appUrl += "QBOrderDownloadWebService.asmx";
        }
        else if (ddlServiceType.SelectedValue == "Inventory")
        {
            appUrl += "QBItemInventoryDownloadWebService.asmx";
        }
        else if (ddlServiceType.SelectedValue == "Account")
        {
            appUrl += "QBAccountDownloadWebService.asmx";
        }
        else
        {
            appUrl += "QBItemDownloadWebService.asmx";
        }

        //build qwc file
        XMLDoc = new XmlDocument();
        XMLDoc.AppendChild(XMLDoc.CreateXmlDeclaration("1.0", null, null));

        //Root element
        XmlElement qbwcXML = XMLDoc.CreateElement("QBWCXML");
        XMLDoc.AppendChild(qbwcXML);

        qbwcXML.AppendChild(MakeElement(XMLDoc, "AppName", AppName.Text.Trim()));
        qbwcXML.AppendChild(MakeElement(XMLDoc, "AppID", ""));
        qbwcXML.AppendChild(MakeElement(XMLDoc, "AppURL", appUrl));
        qbwcXML.AppendChild(MakeElement(XMLDoc, "AppDescription", "A short description for webservice"));
        qbwcXML.AppendChild(MakeElement(XMLDoc, "AppSupport", appUrl + "?wsdl"));
        qbwcXML.AppendChild(MakeElement(XMLDoc, "OwnerID", "{" + System.Guid.NewGuid().ToString() + "}"));
        qbwcXML.AppendChild(MakeElement(XMLDoc, "FileID", "{" + System.Guid.NewGuid().ToString() + "}"));
        qbwcXML.AppendChild(MakeElement(XMLDoc, "UserName", UserName.Text.Trim()));
        qbwcXML.AppendChild(MakeElement(XMLDoc, "QBType", "QBFS"));
        qbwcXML.AppendChild(MakeElement(XMLDoc, "Style", "Document"));
        qbwcXML.AppendChild(MakeElement(XMLDoc, "AuthFlags", "0xF"));
        XmlElement schedulerXML = XMLDoc.CreateElement("Scheduler");
        schedulerXML.AppendChild(MakeElement(XMLDoc, "RunEveryNMinutes", TimeInterval.Text.Trim()));
        qbwcXML.AppendChild(schedulerXML);

        //convert it to byte array data
        byte[] data = ASCIIEncoding.ASCII.GetBytes(XMLDoc.OuterXml);

        Response.Clear();
        // Set as notepad as the primary format
        Response.AddHeader("Content-Type", "application/txt");

        Response.AddHeader("Content-Disposition", "attachment;filename=ZnodeStorefrontQuickbooksWebConnector.qwc");
        Response.ContentType = "application/Znode Storefront Quickbooks Web Connector.qwc";
        Response.BinaryWrite(data); //generate qwc file

        Response.End();

        Response.Redirect("~/admin/Secure/Settings/Default.aspx?mode=quickbooks");
    }


    /// <summary>
    /// Cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/Secure/Settings/default.aspx?mode=quickbooks");
    }
    # endregion

    # region Helper Methods
    /// <summary>
    /// Creates and Returns the xml element
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="tagName"></param>
    /// <param name="tagValue"></param>
    /// <returns></returns>
    private XmlElement MakeElement(XmlDocument doc, string tagName, string tagValue)
    {
        XmlElement elem = doc.CreateElement(tagName);
        elem.InnerText = tagValue;
        return elem;
    }
    #endregion    
}
