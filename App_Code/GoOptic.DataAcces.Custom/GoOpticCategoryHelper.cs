using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ZNode.Libraries.DataAccess.Custom;
using System.Data.SqlClient;

/// <summary>
/// Summary description for GoOpticCategoryHelper
/// </summary>
public class GoOpticCategoryHelper : CategoryHelper
{
    public GoOpticCategoryHelper()
    {}
        //
        // TODO: Add constructor logic here
        //
        public DataSet GetBrands(int portalId)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ZNodeECommerceDB"].ConnectionString);

            SqlDataAdapter myCommand = new SqlDataAdapter("ZNODE_GetBrands", myConnection);

            // Mark the Command as a SPROC
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterportalId = new SqlParameter("@portalId", SqlDbType.Int, 4);
            parameterportalId.Value = portalId;
            myCommand.SelectCommand.Parameters.Add(parameterportalId);

            // Create and Fill the DataSet
            DataSet myDataSet = new DataSet();
            myCommand.Fill(myDataSet);

            //close connection
            myConnection.Close();

            //Return DataSet
            return myDataSet;
        }

        public DataSet GetBrandNavigationItems(int portalId)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ZNodeECommerceDB"].ConnectionString);

            SqlDataAdapter myCommand = new SqlDataAdapter("ZNODE_GetBrandNavigationItems", myConnection);

            // Mark the Command as a SPROC
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterportalId = new SqlParameter("@portalId", SqlDbType.Int, 4);
            parameterportalId.Value = portalId;
            myCommand.SelectCommand.Parameters.Add(parameterportalId);

            // Create and Fill the DataSet
            DataSet myDataSet = new DataSet();
            myCommand.Fill(myDataSet);

            //close connection
            myConnection.Close();

            //Return DataSet
            return myDataSet;
        }
}
