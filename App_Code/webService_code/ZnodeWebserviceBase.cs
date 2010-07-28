using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Services.Protocols;

/// <summary>
/// This is the Znode framework Base class for all Web Services.
/// </summary>
public class ZNodeWebserviceBase : System.Web.Services.WebService
{
    # region Public Member Variables
    public string ResponseMSG_UNAUTHORIZED = "Invalid Credentials. Access is denied.";
    public string ResponseMSG_ERROR = "Your request could not be completed because of an unknown error. Please contact your administrator.";
    #endregion

    # region Public Constructors
    public ZNodeWebserviceBase()
    {
    }
    #endregion

    /// <summary>
    /// Check if the caller is authorized user to use this service
    /// </summary>
    /// <param name="UserName"></param>
    /// <param name="Password"></param>
    /// <returns>returns boolean value</returns>
    public bool Authorize(string UserId, string Password)
    {
        //Check User
        bool IsAuthorizedUser = Membership.ValidateUser(UserId, Password);

        if (IsAuthorizedUser)
        {
            //check if user is an administrator
            if (Roles.IsUserInRole(UserId, "Web Service"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Return value
        return IsAuthorizedUser;
    }
    public System.Xml.XmlDocument CreateSOAPResponse(string XMLData,string ResponseErrorCode,string ResponseErrorDescription)
    {
        System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<Response>");
        sb.Append("<ErrorCode>");
        sb.Append(ResponseErrorCode);
        sb.Append("</ErrorCode>");
        sb.Append("<ErrorDescription>");
        sb.Append(ResponseErrorDescription);
        sb.Append("</ErrorDescription>");
        sb.Append("<Data>");
        sb.Append(XMLData);
        sb.Append("</Data>");
        sb.Append("</Response>");
        xmlDoc.LoadXml(sb.ToString());

        return xmlDoc;
    }
}
