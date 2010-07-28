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
using ZNode.Libraries.Admin;
using System.IO;

public partial class PlugIns_DataManager_UpdatePricing : System.Web.UI.UserControl
{
    # region Private Member Variables
    private string defaultPageLink = "~/admin/Secure/DataManager/";
    # endregion

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
    /// Event fired when Submit button is triggered.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        # region Local Member Variables
        string DataPath = ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.DataPath;
        string _strFilePath = Server.MapPath(DataPath + UploadFile.FileName);
        DataDownloadAdmin dataAdmin = new DataDownloadAdmin();
        DataManagerAdmin manager = new DataManagerAdmin();
        string dataFilePath = Server.MapPath(ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.DataPath);
        #endregion

        try
        {
            // Save the file on the server 
            UploadFile.PostedFile.SaveAs(_strFilePath);

             FileInfo fileinfo = new FileInfo(_strFilePath);

                using (FileStream filestr = new FileStream(dataFilePath + "schema.ini",
                FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(filestr))
                    {
                        writer.WriteLine("[" + fileinfo.Name + "]");
                        writer.WriteLine("ColNameHeader=True");
                        writer.WriteLine("MaxScanRows=0");
                        writer.WriteLine("Format=CSVDelimited");
                        writer.Close();
                        writer.Dispose();
                    }
                    filestr.Close();
                    filestr.Dispose();
                }

            // Import data from excel/csv file
            DataTable dataTable = dataAdmin.GetDataTable(_strFilePath);

            string responseText = manager.UpdateProductPricing(dataTable);

            if (responseText.Length > 0)
            {
                ltrlError.Text = responseText;
                return;
            }

        }
        catch // Generic exception handler
        {
            ltrlError.Text = "Failed to process your request. Please try again.";
            return;
        }
        finally
        {
            // Delete the temporary file
            System.IO.File.Delete(_strFilePath);
        }

        Response.Redirect(defaultPageLink);
    }

    /// <summary>
    /// Event fired when Cancel button is triggered.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(defaultPageLink);
    }
    #endregion
}
