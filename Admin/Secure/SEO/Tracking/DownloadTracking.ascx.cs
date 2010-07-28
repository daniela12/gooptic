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
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.DataAccess.Service;

public partial class PlugIns_DataManager_DownloadTracking : System.Web.UI.UserControl
{
    # region Private member Variables
    string defaultPageLink = "~/admin/Secure/DataManager/default.aspx";
    DataDownloadAdmin adminAccess = new DataDownloadAdmin();
    DataManagerAdmin manager = new DataManagerAdmin();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    # region General Events

    /// <summary>
    /// Submit Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        try
        {
            DataSet ds = BindTrackingData();

            if (ds.Tables[0].Rows.Count != 0)
            {
                // Save as Excel Sheet
                if (ddlFileSaveType.SelectedValue == ".xls")
                {
                    // Temp Grid control
                    GridView gView = new GridView();
                    gView.DataSource = ds;
                    gView.DataBind();
                    ExportDataToExcel("Attributes.xls", gView);
                }
                else // Save as CSV Format
                {
                    // Set Formatted Data from dataset object
                    string strData = adminAccess.Export(ds, true);

                    byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(strData);

                    ExportDataToCSV("DownloadedTrackingData.csv", data);
                }
            }
            else
            {
                ltrlError.Text = "No Tracking Data Found";
                return;
            }
        }
        catch
        {
            ltrlError.Text = "Failed to process your request. Please try again.";
        }
    }

    /// <summary>
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(defaultPageLink);
    }
    #endregion

    # region Helper Methods
    /// <summary>
    ///  Returns string for a Given Dataset Values
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="exportColumnHeadings"></param>
    public void ExportDataToExcel(string strFileName, GridView gridViewControl)
    {
        Response.Clear();
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=" + strFileName);
        // Set as Excel as the primary format
        Response.AddHeader("Content-Type", "application/Excel");
        Response.ContentType = "application/Excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gridViewControl.RenderControl(htw);
        Response.Write(sw.ToString());

        //
        gridViewControl.Dispose();

        Response.End();
    }

    /// <summary>
    ///  Returns string for a Given Dataset Values
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="exportColumnHeadings"></param>
    public void ExportDataToCSV(string FileName, byte[] Data)
    {
        Response.Clear();
        Response.ClearHeaders();
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
        Response.AddHeader("Content-Type", "application/Excel");
        // Set as text as the primary format
        Response.ContentType = "text/csv";
        Response.ContentType = "application/vnd.xls";
        Response.AddHeader("Pragma", "public");

        Response.BinaryWrite(Data);
        Response.End();
    }

    /// <summary>
    /// Bind the Tracking Data
    /// </summary>
    /// <returns></returns>
    protected DataSet BindTrackingData()
    {        
        string startdate = txtStartDate.Text.Trim();
        string enddate = txtEndDate.Text.Trim();

        DataSet ds = manager.GetDownloadTrackingData(startdate, enddate); 
        
        return ds;
    }

    #endregion
}
