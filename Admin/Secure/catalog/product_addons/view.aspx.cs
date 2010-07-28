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
using ZNode.Libraries.DataAccess.Custom;

public partial class Admin_Secure_catalog_product_addons_view : System.Web.UI.Page
{
    
    #region Protected Variables
    protected int ItemId;
    protected int ProductId = 0;
    protected string CatalogImagePath = "";
    protected string AddLink = "~/admin/secure/catalog/product_addons/add.aspx";
    protected string AddOnValuepageLink = "~/admin/secure/catalog/product_addons/add_Addonvalues.aspx?itemid=";
    protected string EditLink = "~/admin/secure/catalog/product_addons/add.aspx?itemid=";
    protected string ListLink = "~/admin/secure/catalog/product_addons/list.aspx";
    # endregion

    # region Page Load Event
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        // Get ItemId from querystring        
        if (Request.Params["itemid"] != null)
        {
            ItemId = int.Parse(Request.Params["itemid"]);
        }       
        else
        {
            ItemId = 0;
        }

        if (!Page.IsPostBack)
        {
            this.BindData();
            this.BindGrid();
        }
    }
    #endregion

    # region Bind Methods
    /// <summary>
    ///  Bind data to grid
    /// </summary>
    private void BindGrid()
    {
        ProductAddOnAdmin AddOnValueAdmin = new ProductAddOnAdmin();
        TList<AddOnValue> ValueList = AddOnValueAdmin.GetAddOnValuesByAddOnId(ItemId);
        if(ValueList != null)
            ValueList.Sort("DisplayOrder");
        uxGrid.DataSource = ValueList;
        uxGrid.DataBind();
    }
    /// <summary>
    /// Bind data to the fields on the edit screen
    /// </summary>
    protected void BindData()
    {
        ProductAddOnAdmin AddOnAdmin = new ProductAddOnAdmin();
        AddOn addOnEntity = AddOnAdmin.GetByAddOnId(ItemId);

        if (addOnEntity != null)
        {
            lblTitle.Text = addOnEntity.Name;
            lblName.Text = addOnEntity.Name;
            lblAddOnTitle.Text = addOnEntity.Title;
            lblDisplayOrder.Text = addOnEntity.DisplayOrder.ToString();
            lblDisplayType.Text = addOnEntity.DisplayType.ToString();

            //Display Settings
            chkOptionalInd.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse(addOnEntity.OptionalInd.ToString()));

            //Inventory Setting - Out of Stock Options
            if ((addOnEntity.TrackInventoryInd) && (addOnEntity.AllowBackOrder == false))
            {
                chkCartInventoryEnabled.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse("true"));
                chkIsBackOrderEnabled.Src = SetImage();                    
                chkIstrackInvEnabled.Src = SetImage();
                    
            }
            else if (addOnEntity.TrackInventoryInd && addOnEntity.AllowBackOrder)
            {
                chkCartInventoryEnabled.Src = SetImage();
                chkIsBackOrderEnabled.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse("true"));
                chkIstrackInvEnabled.Src = SetImage();
            }
            else if ((addOnEntity.TrackInventoryInd == false) && (addOnEntity.AllowBackOrder == false))
            {
                chkCartInventoryEnabled.Src = SetImage();
                chkIsBackOrderEnabled.Src = SetImage();
                chkIstrackInvEnabled.Src = ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse("true"));
            }


            //Inventory Setting - Stock Messages
            lblInStockMsg.Text = addOnEntity.InStockMsg;
            lblOutofStock.Text = addOnEntity.OutOfStockMsg;
            lblBackOrderMsg.Text = addOnEntity.BackOrderMsg;
        }
        else
        {
            throw (new ApplicationException("Add-On Requested could not be found."));
        }
    }
    #endregion

    # region General Events

    /// <summary>
    /// Create New AddOnValue Button Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddNewAddOnValues_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddOnValuepageLink + ItemId);
    }

    /// <summary>
    /// Back button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect(ListLink);
    }

    /// <summary>
    /// Edit Add-On Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void EditAddOn_Click(object sender, EventArgs e)
    {
        Response.Redirect(EditLink + ItemId);
    }

    # endregion

    #region Grid Events
    /// <summary>
    /// Add Client side event to the Delete Button in the Grid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Retrieve the Button control from the Seventh column.
            Button DeleteButton = (Button)e.Row.Cells[7].FindControl("btnDelete");

            //Set the Button's CommandArgument property with the row's index.
            DeleteButton.CommandArgument = e.Row.RowIndex.ToString();

            //Add Client Side confirmation
            DeleteButton.OnClientClick = "return confirm('Are you sure you want to delete this Add-On Value?');";
        }
    }
    /// <summary>
    /// Event triggered when the grid page is changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    
    /// <summary>
    /// Even triggered when an item is deleted from the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    /// <summary>
    /// Event triggered when a command button is clicked on the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowCommand(Object sender, GridViewCommandEventArgs e)
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
                EditLink = AddOnValuepageLink + ItemId + "&AddOnValueId=" + Id;
                Response.Redirect(EditLink);
            }
            if (e.CommandName == "Delete")
            {
                ProductAddOnAdmin AdminAccess = new ProductAddOnAdmin();
                bool Status = AdminAccess.DeleteAddOnValue(int.Parse(Id));

                if (Status)
                {
                    BindGrid();
                }
            }

        }
    }
    #endregion

    # region Helper Methods
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string SetImage()
    {
        return ZNode.Libraries.Framework.Business.ZNodeHelper.GetCheckMark(bool.Parse("false"));
    }
    #endregion    
    
}
