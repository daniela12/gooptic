using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://www.znode.com/webservices/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class OrderWebService  : ZNodeWebserviceBase
{

    public OrderWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "Returns the storefront orders and order Line items")]
    public System.Xml.XmlDocument GetOrders(string UserId,string Password, int StartOrderNumber)
    {
        try
        {
            //Authorize Users
            bool IsAuthorized = Authorize(UserId, Password);

            if (IsAuthorized)
            {
                //Create Instance for Connection and Adapter Objects
                SqlConnection MyConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ZNodeECommerceDB"].ConnectionString);
                SqlCommand myCommand = new SqlCommand("ZNODE_WS_SEARCHORDER", MyConnection);

                //Mark the Select command as Stored procedure
                myCommand.CommandType = CommandType.StoredProcedure;
                //Declare Parameters
                SqlParameter Myparam = new SqlParameter("@INITIALORDERID", SqlDbType.Int);
                Myparam.Value = StartOrderNumber;

                //Add Paramters into Command
                myCommand.Parameters.Add(Myparam);

                //Execute Query
                MyConnection.Open(); //Open Connection

                SqlDataReader MyXMLReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                
                while (MyXMLReader.Read())
                {
                    sb.Append(MyXMLReader[0].ToString());
                }
                
                DecryptCreditCardInfo(sb, "CreditCardNumber");
                DecryptCreditCardInfo(sb, "CreditCardExp");
                DecryptCreditCardInfo(sb, "CreditCardCVV");

                //Return Dataset
                return CreateSOAPResponse(sb.ToString(), "0", String.Empty);
            }
            else
            {
                return CreateSOAPResponse("", "1", ResponseMSG_UNAUTHORIZED);
            }
        }
        catch (SqlException SqlEx)
        {
            return CreateSOAPResponse("", "3", "Error has occured. " + SqlEx.Message);
        }
        catch (Exception ex)
        {
            return CreateSOAPResponse("", "2", ResponseMSG_ERROR + "Reference: " + ex.Message);
        }

    }

    # region Helper Methods
    /// <summary>
    /// Decrypts the credit card informations
    /// Set the data to the string builder
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="tag"></param>
    private void DecryptCreditCardInfo(System.Text.StringBuilder sb,string tag)
    {
        ZNode.Libraries.Framework.Business.ZNodeEncryption encryptData = new ZNode.Libraries.Framework.Business.ZNodeEncryption();
                
        MatchCollection mCollection = Regex.Matches(sb.ToString(), "<" + tag + ">[^>]*>(.*?)", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

        //Loop through the matching collection
        foreach (Match m in mCollection)
        {
            string temp = StripXMLtag(m.Value);

            if (temp.Trim().Length > 0)
            {                
                sb.Replace(m.Value, "<" + tag + ">" + encryptData.DecryptData(temp) + "</" + tag + ">");
            }
        }
    }

    /// <summary>
    /// Returns the value between the tags
    /// Also, removes the tag
    /// </summary>
    /// <param name="InputString"></param>
    /// <returns></returns>
    private string StripXMLtag(string InputString)
    {
        string strOutput;
        System.Text.RegularExpressions.Regex objRegExp =  new System.Text.RegularExpressions.Regex("<(.|\n)+?>");
        
        //Replace all HTML tag matches with the empty string
        strOutput = objRegExp.Replace(InputString, "");
  
        //Replace all < and > with &lt; and &gt;
        strOutput = strOutput.Replace("<", "&lt;");
        strOutput = strOutput.Replace(">", "&gt;");

        return strOutput;    //Return the value of strOutput        
    }
    #endregion
}

