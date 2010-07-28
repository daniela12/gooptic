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
using ZNode.Libraries.Admin;
using ZNode.Libraries.DataAccess.Entities;
using System.Data.SqlClient;

public partial class Admin_Secure_catalog_product_suppliers_list : System.Web.UI.Page
{
    #region Protected Member Variables
    protected string CatalogImagePath = "";
    protected string AddLink = "~/admin/secure/catalog/product_suppliers/add.aspx";
    protected int ItemId = 0;  
    protected DataSet MyDataSet = null;   
    #endregion

    #region Page Load
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["ItemID"] != null)
        {
            ItemId = int.Parse(Request.Params["ItemID"].ToString());
        }
        if (!Page.IsPostBack)
        {
            BindGridData();
        }
        lblError.Visible = false;
    }
    #endregion

    #region Bind Methods
    /// <summary>
    /// Bind data to grid
    /// </summary>
    private void BindGridData()
    {
        ZNode.Libraries.Admin.SupplierAdmin supplierAdmin = new SupplierAdmin();
        TList<ZNode.Libraries.DataAccess.Entities.Supplier> supplier = supplierAdmin.GetAll();

        if (supplier != null)
        {
            supplier.Sort("Name Asc");
        }

        uxGrid.DataSource = supplier;
        uxGrid.DataBind();
    }

    /// <summary>
    /// Binds Search Data
    /// </summary>
    public DataSet BindSearchData()
    {
        SupplierAdmin AdminAccess = new SupplierAdmin();
        DataSet ds = AdminAccess.SearchSupplier(txtName.Text, ddlSupplierStatus.SelectedValue);
        return ds;
    }

    #endregion

    #region General Events

    protected void btnAddSupplier_Click(object sender, EventArgs e)
    {
        //Redirect to Add Supplier Page
        Response.Redirect(AddLink);
    }

    /// <summary>
    /// Search Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MyDataSet = this.BindSearchData();
        DataView dv = new DataView(MyDataSet.Tables[0]);
        dv.Sort = "DisplayOrder Asc";
        uxGrid.DataSource = dv;
        uxGrid.DataBind();
    }

    /// <summary>
    /// Clear Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        txtName.Text = string.Empty;
        ddlSupplierStatus.SelectedValue = "";
        BindGridData();        
    }
    # endregion

    # region Grid Events

    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        BindGridData();

    }
    protected void uxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Page")
        {
        }
        else
        {
            // Convert the row index stored in the CommandArgument
            // property to an Integer.
            int index = Convert.ToInt32(e.CommandArgument);

            // Get the values from the appropriate
            // cell in the GridView control.
            GridViewRow selectedRow = uxGrid.Rows[index];

            TableCell Idcell = selectedRow.Cells[0];
            string Id = Idcell.Text;


            if (e.CommandName == "Edit")
            {
                AddLink = AddLink + "?itemid=" + Id;
                Response.Redirect(AddLink);
            }
            else if (e.CommandName == "Delete")
            {

                ZNode.Libraries.Admin.SupplierAdmin adminaccess = new ZNode.Libraries.Admin.SupplierAdmin();
                bool Check = false;
                Check = adminaccess.Delete(int.Parse(Id));
                if (Check)
                {
                    this.BindGridData();
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Delete action could not be completed because the supplier is in use.";
                }

            }

        }
    }
    protected void uxGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        this.BindGridData();
    }

    #endregion
}
