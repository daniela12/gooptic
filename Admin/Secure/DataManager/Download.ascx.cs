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
using System.Reflection;
using System.Text;
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.DataAccess.Service;

public partial class PlugIns_DataManager_Download : System.Web.UI.UserControl
{
    # region Private member Variables
    string filter = "";
    string defaultPageLink = "~/admin/Secure/DataManager/default.aspx";
    #endregion

    # region Events
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get filter value from request parameter
        if (Request.Params["filter"] != null)
        {
            filter = Request.Params["filter"];
        }

        // 
        if (filter.Length == 0)
        {
            Response.Redirect(defaultPageLink);
        }

        // 
        if (!Page.IsPostBack)
        {
            if (filter.ToLower() == "pricing")
            {
                ltrlTitle.Text = "Download Pricing";
                pnlDownloadPricing.Visible = true;
                pnlProductTypes.Visible = true;
            }
            else if (filter.ToLower() == "inventory")
            {
                ltrlTitle.Text = "Download Inventory";
                pnlDownloadInventory.Visible = true;
                pnlProductTypes.Visible = true;
            }
            else if (filter.ToLower() == "product")
            {
                ltrlTitle.Text = "Download Products ";
                pnlDownloadProduct.Visible = true;
                pnlProductTypes.Visible = false;
            }
            else
            {
                Response.Redirect(defaultPageLink);
            }
        }
    }

    /// <summary>
    /// Event fired when Download Inventory Button is triggered.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDownloadProdInventory_Click(object sender, EventArgs e)
    {
        try
        {
            DataDownloadAdmin adminAccess = new DataDownloadAdmin();
            DataManagerAdmin dataManager = new DataManagerAdmin();
            DataSet ds = dataManager.GetProductQuantities(int.Parse(ddlProductType.SelectedValue));

            // 
            if (ddlFileSaveType.SelectedValue == ".xls")
            {
                // Temp Grid control
                GridView gView = new GridView();
                gView.DataSource = ds;
                gView.DataBind();
                ExportDataToExcel("Inventory.xls", gView);
            }
            else
            {
                // Set Formatted Data from dataset object
                string strData = adminAccess.Export(ds, true);

                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(strData);

                ExportDataToCSV("Inventory.csv", data);
            }
        }
        catch
        {
            ltrlError.Text = "Failed to process your request. Please try again.";
        }
    }


    /// <summary>
    /// Event fired when Download Pricing button is triggered.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDownloadProductPricing_Click(object sender, EventArgs e)
    {
        try
        {
            DataDownloadAdmin adminAccess = new DataDownloadAdmin();
            DataManagerAdmin dataManager = new DataManagerAdmin();
            DataSet ds = dataManager.GetProductPrices(int.Parse(ddlProductType.SelectedValue));

            // 
            if (ddlFileSaveType.SelectedValue == ".xls")
            {
                // Temp Grid control
                GridView gView = new GridView();
                gView.DataSource = ds;
                gView.DataBind();
                ExportDataToExcel("Pricing.xls", gView);
            }
            else
            {
                // Set Formatted Data from dataset
                string strData = adminAccess.Export(ds, true);
                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(strData);
                ExportDataToCSV("Pricing.csv", data);
            }
        }
        catch
        {
            ltrlError.Text = "Failed to process your request. Please try again.";
        }
    }

    /// <summary>
    /// Event fired when Download Product Button is triggered.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDownloadProduct_Click(object sender, EventArgs e)
    {
        try
        {
            DataDownloadAdmin adminAccess = new DataDownloadAdmin();
            DataManagerAdmin dataManager = new DataManagerAdmin();

            DataSet ds = dataManager.GetProductListByPortalId(ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.PortalID);

            // 
            if (ddlFileSaveType.SelectedValue == ".xls")
            {
                // Temp Grid control
                GridView gView = new GridView();
                gView.DataSource = ds;
                gView.DataBind();
                ExportDataToExcel("Product.xls", gView);
            }
            else
            {
                // Set Formatted Data from dataset object
                string strData = adminAccess.Export(ds, true);

                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(strData);

                ExportDataToCSV("Product.csv", data);
            }
        }
        catch
        {
            ltrlError.Text = "Failed to process your request. Please try again.";
        }
    }

    /// <summary>
    /// Event fired when Cancel Button is triggered
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
    #endregion
}
