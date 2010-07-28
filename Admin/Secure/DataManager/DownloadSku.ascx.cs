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
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.DataAccess.Data;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.Admin;

public partial class PlugIns_DataManager_DownloadSku : System.Web.UI.UserControl
{
    # region Private member Variables 
    int productId = 0;
    int productTypeID = 0;
    string defaultPageLink = "~/admin/Secure/DataManager/default.aspx";
    #endregion

    #region Page_Load 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // Bind all Arributes and Product
            BindDropDowns();
        }

        // BindSkuAttributes
        BindSkuAttributes();
    }

    #endregion

    #region Events 
    /// <summary>
    /// Event fired when Download Inventory Button is triggered.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDownloadProdInventory_Click(object sender, EventArgs e)
    {        
        DataDownloadAdmin adminAccess = new DataDownloadAdmin();

        try
        {
            DataSet ds = BindSearchData();

            if (ds.Tables[0].Rows.Count != 0)
            {
                // Save as Excel Sheet
                if (ddlFileSaveType.SelectedValue == ".xls")
                {
                    // Temp Grid control
                    GridView gView = new GridView();
                    gView.DataSource = ds;
                    gView.DataBind();
                    ExportDataToExcel("Inventory.xls", gView);
                }
                else // Save as CSV Format
                {
                    // Set Formatted Data from dataset object
                    string strData = adminAccess.Export(ds, true);

                    byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(strData);

                    ExportDataToCSV("Inventory.csv", data);
                }
            }
            else
            {
                ltrlError.Text = "No Sku Found";
                return;
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

    /// <summary>
    /// Product option changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Clear the controls
        ControlPlaceHolder.Controls.Clear();

        BindSkuAttributes();
    }

    #endregion

    #region Bind Methods 
    /// <summary>
    /// Bind Attributes List.
    /// </summary>
    protected void BindDropDowns()
    {
        //Get Products        
        ListItem li = new ListItem("All", "0");
        DataSet productList = ZNodeProductList.GetProductListByPortalID(ZNodeConfigManager.SiteConfig.PortalID);
        DataView dataView = new DataView(productList.Tables[0]);
        dataView.Sort = "Name";

        //Bind dropdownlist
        lstProduct.DataSource = dataView;
        lstProduct.DataTextField = "Name";
        lstProduct.DataValueField = "ProductID";
        lstProduct.DataBind();
        lstProduct.Items.Insert(0, li);
    }

    /// <summary>
    /// Bind Attributes List.
    /// </summary>
    protected void BindSkuAttributes()
    {        
        // Set ProductId
        productId = Convert.ToInt16(lstProduct.SelectedValue);

        if (productId != 0)
        {
            ProductAdmin _adminAccess = new ProductAdmin();

            DataSet ds = _adminAccess.GetProductDetails(productId);

            //Check for Number of Rows
            if (ds.Tables[0].Rows.Count != 0)
            {
                //Check For Product Type
                productTypeID = int.Parse(ds.Tables[0].Rows[0]["ProductTypeId"].ToString());
            }

            DataSet MyDataSet = _adminAccess.GetAttributeTypeByProductTypeID(productTypeID);

            if (MyDataSet.Tables[0].Rows.Count > 0)
            {
                //Repeats until Number of AttributeType for this Product
                foreach (DataRow dr in MyDataSet.Tables[0].Rows)
                {
                    //Bind Attributes
                    DataSet _AttributeDataSet = _adminAccess.GetAttributesByAttributeTypeIdandProductID(int.Parse(dr["attributetypeid"].ToString()), productId);

                    System.Web.UI.WebControls.DropDownList lstControl = new DropDownList();
                    lstControl.ID = "lstAttribute" + dr["AttributeTypeId"].ToString();

                    ListItem li = new ListItem(dr["Name"].ToString(), "0");
                    li.Selected = true;

                    lstControl.DataSource = _AttributeDataSet;
                    lstControl.DataTextField = "Name";
                    lstControl.DataValueField = "AttributeId";
                    lstControl.DataBind();
                    lstControl.Items.Insert(0, li);

                    //Add Dynamic Attribute DropDownlist in the Placeholder
                    ControlPlaceHolder.Controls.Add(lstControl);

                    Literal lit1 = new Literal();
                    lit1.Text = "&nbsp;&nbsp;";
                    ControlPlaceHolder.Controls.Add(lit1);
                }

                pnlAttribteslist.Visible = true;
            }
            else
            {
                pnlAttribteslist.Visible = false;
            }
        }
        else
        {
            pnlAttribteslist.Visible = false;
        }
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

    protected DataSet BindSearchData()
    {
        #region Local Variables 
        string Attributes = String.Empty;
        string AttributeList = string.Empty;
        DataSet MyDatas = new DataSet();
        SKUAdmin _SkuAdmin = new SKUAdmin();
        ProductAdmin _adminAccess = new ProductAdmin();
        #endregion

        productId = Convert.ToInt16(lstProduct.SelectedValue);

        if (productId != 0)
        {
            DataSet ds = _adminAccess.GetProductDetails(productId);

            //Check for Number of Rows
            if (ds.Tables[0].Rows.Count != 0)
            {
                //Check For Product Type
                productTypeID = int.Parse(ds.Tables[0].Rows[0]["ProductTypeId"].ToString());
            }

            //GetAttribute for this ProductType
            DataSet MyAttributeTypeDataSet = _adminAccess.GetAttributeTypeByProductTypeID(productTypeID);

            foreach (DataRow MyDataRow in MyAttributeTypeDataSet.Tables[0].Rows)
            {
                System.Web.UI.WebControls.DropDownList lstControl = (System.Web.UI.WebControls.DropDownList)ControlPlaceHolder.FindControl("lstAttribute" + MyDataRow["AttributeTypeId"].ToString());

                if (lstControl != null)
                {
                    int selValue = int.Parse(lstControl.SelectedValue);

                    if (selValue > 0)
                    {
                        Attributes += selValue.ToString() + ",";
                    }
                }
            }

            if (Attributes != "")
            {
                // Split the string
                AttributeList = Attributes.Substring(0, Attributes.Length - 1);
            }
        }

        if (Attributes.Length == 0 && productId == 0)
        {
            MyDatas = _SkuAdmin.GetallSkuData();
        }
        else
        {
            MyDatas = _SkuAdmin.GetBySKUAttributes(productId, AttributeList);              
        }              

        return MyDatas;     
    }
    #endregion
}
