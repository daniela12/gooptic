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

public partial class PlugIns_DataManager_UploadProduct : System.Web.UI.UserControl
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
    /// Event fired when Submit button is triggered.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string dataFilePath = Server.MapPath(ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.DataPath);
        string filePath = dataFilePath + FileUpload.FileName;

        try
        {
            if (FileUpload.HasFile)
            {
                // Save the file on the server 
                FileUpload.PostedFile.SaveAs(filePath);

                FileInfo fileinfo = new FileInfo(filePath);

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

                DataManagerAdmin managerAdmin = new DataManagerAdmin();
                DataDownloadAdmin dataAdmin = new DataDownloadAdmin();

                // Get data table from csv file
                DataTable dataTable = dataAdmin.GetDataTable(filePath);

                managerAdmin.UploadProducts(dataTable);

                // Release all resources
                dataTable.Dispose();
            }
            else
            {
                ltrlError.Text = "Please select a valid CSV file.";
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
            System.IO.File.Delete(filePath);

            // Delete the 
            System.IO.File.Delete(dataFilePath + "schema.ini");
        }

        Response.Redirect("~/admin/Secure/DataManager/Default.aspx");
    }

    /// <summary>
    /// Event fired when Cancel button is triggered.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/Secure/DataManager/Default.aspx");
    }
    #endregion
}
