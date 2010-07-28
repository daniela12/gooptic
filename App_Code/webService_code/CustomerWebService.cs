using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CustomerWebService
/// </summary>
[WebService(Namespace = "http://www.znode.com/webservices/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class CustomerWebService : ZNodeWebserviceBase
{

    public CustomerWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "Returns the storefront customers list.")]
    public System.Xml.XmlDocument GetCustomers(string UserId, string Password)
    {
        try
        {
            //Authorize Users
            bool IsAuthorized = Authorize(UserId, Password);

            if (IsAuthorized)
            {
                //Create Instance for Connection and Adapter Objects
                SqlConnection MyConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ZNodeECommerceDB"].ConnectionString);
                SqlCommand myCommand = new SqlCommand("ZNODE_WS_GetCustomers", MyConnection);

                //Mark the Select command as Stored procedure
                myCommand.CommandType = CommandType.StoredProcedure;

                //Open Connection
                MyConnection.Open();

                //Execute Query
                SqlDataReader MyXMLReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                while (MyXMLReader.Read())
                {
                    sb.Append(MyXMLReader[0].ToString());
                }

                //Return SOAP response
                return CreateSOAPResponse(sb.ToString(), "0", String.Empty);
            }
            else
            {
                return CreateSOAPResponse("", "1", ResponseMSG_UNAUTHORIZED);
            }
        }
        catch (SqlException ex)
        {
            return CreateSOAPResponse("", "3", "Error has occured. " + ex.Message);
        }
        catch (Exception e)
        {
            return CreateSOAPResponse("", "2", ResponseMSG_ERROR + "<br>Reference:" + e.Message);
        }
    }

}

