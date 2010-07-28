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
using System.IO;
using ZNode.Libraries.Admin;

public partial class PlugIns_DataManager_UploadZipcode : System.Web.UI.UserControl
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
        # region Local Member Variables
        string dataPath = ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.DataPath;
        string filePath = "";
        string statusMsg = "";
        DataDownloadAdmin dataAdmin = new DataDownloadAdmin();
        DataManagerAdmin manager = new DataManagerAdmin();
        #endregion

        try
        {
            string dataFilePath = Server.MapPath(ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.DataPath);

            // set the file path
            filePath = txtInputFile.Text;

            FileInfo filefound = new FileInfo(filePath);         

            using (FileStream filestr = new FileStream(dataFilePath + "schema.ini",
            FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(filestr))
                {
                    writer.WriteLine("[" + filefound.Name + "]");
                    writer.WriteLine("ColNameHeader=True");
                    writer.WriteLine("MaxScanRows=0");
                    writer.WriteLine("Format=CSVDelimited");
                    writer.Close();
                    writer.Dispose();
                }
                filestr.Close();
                filestr.Dispose();
            }


             /* To Check Whether File is Exists Or Not*/
            if (filefound.Exists)
            {
                // Import data from excel/csv file
                DataTable dataTable = dataAdmin.GetDataTable(filePath);

                // Set the tableName to upload.
                string tableName = "ZnodeZipCode";

                statusMsg = manager.UploadZipCode(dataTable, tableName);

                if (statusMsg.Length != 0)
                {
                    ltrlError.Text = "Failed to process your request. Please try again." + statusMsg;
                    return;
                }
            }
            else
            {
                ltrlError.Text = "Input file not found. Please check the file path and try again.";
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
            // System.IO.File.Delete(filePath);
        }

        ltrlmsg.Text = "File successfully uploaded.";
        ltrlmsg.Visible = true;
        btnGoback.Visible = true;
        uploadPanel.Visible = false;        
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
