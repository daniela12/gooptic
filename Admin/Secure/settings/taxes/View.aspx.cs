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
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.DataAccess.Entities;

public partial class Admin_Secure_settings_tax_View : System.Web.UI.Page
{
    #region Protected Member Variables
    protected int ItemId;
    protected int taxId;
    protected string CatalogImagePath = "";
    protected string EditLink = "~/admin/secure/settings/taxes/add.aspx";
    protected string AddRuleLink = "~/admin/secure/settings/taxes/addRule.aspx";
    protected string EditRuleLink = "~/admin/secure/settings/taxes/addRule.aspx";
    #endregion

    #region Page Load
    /// <summary>
    /// Page Load Event
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

        //Get TaxId from querystring
        if (Request.Params["taxId"] != null)
        {
            taxId = int.Parse(Request.Params["taxId"]);
        }
        else
        {
            taxId = 0;
        }

        if (!Page.IsPostBack)
        {
            BindData();
            lblTitle.Text = lblProfileName.Text;
        }
    }
    #endregion

    #region Bind Methods
    /// <summary>
    /// Bind data to grid
    /// </summary>
    private void BindData()
    {
        if (taxId > 0)
        {
            TaxRuleAdmin taxAdmin = new TaxRuleAdmin();
            TaxClass taxClass = taxAdmin.DeepLoadByTaxClassId(taxId);
            taxClass.TaxRuleCollection.Sort("Precedence");
            lblProfileName.Text = taxClass.Name;
            if (taxClass.DisplayOrder.HasValue)
                lblDisplayOrder.Text = taxClass.DisplayOrder.ToString();
            imgActive.Src = ZNodeHelper.GetCheckMark((bool)taxClass.ActiveInd);
            uxGrid.DataSource = taxClass.TaxRuleCollection;
            uxGrid.DataBind();
        }
    }
    #endregion

    #region Grid Events
    /// <summary>
    /// Event triggered when the grid page is changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        BindData();
    }

    /// <summary>
    /// Event triggered when an item is deleted from the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        BindData();
    }

    /// <summary>
    /// Event triggered when a command button is clicked on the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "page")
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
                EditRuleLink = EditRuleLink + "?itemid=" + Id + "&taxId=" + taxId;
                Response.Redirect(EditRuleLink);
            }
            else if (e.CommandName == "Delete")
            {
                TaxRuleAdmin taxAdmin = new TaxRuleAdmin();
                TaxRule taxRule = new TaxRule();

                taxRule.TaxRuleID = int.Parse(Id);
                bool status = taxAdmin.DeleteTaxRule(taxRule);

                if(status)
                    System.Web.HttpContext.Current.Cache.Remove("InclusiveTaxRules");

            }
        }
    }

    #endregion

    #region Other Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddRuleLink + "?taxid=" + taxId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect(EditLink + "?taxid=" + taxId);
    }
    #endregion

    # region Helper Methods
    /// <summary>
    /// Get a formatted region code for grid display
    /// </summary>
    /// <param name="regionCode"></param>
    /// <returns></returns>
    protected string GetDefaultRegionCode(object regionCode)
    {
        
        if (regionCode == null)
        {
            return "ALL";
        }
        else if (regionCode.ToString().Length == 0)
        {
            return "ALL";
        }
        else
        {
            return regionCode.ToString();
        }
    }
    /// <summary>
    /// Get a County Name for grid display
    /// </summary>
    /// <param name="regionCode"></param>
    /// <returns></returns>
    protected string GetCountyName(object regionCode)
    {
        string countyName = "";

        if (regionCode == null)
        {
            countyName = "ALL";
        }
        else if (regionCode.ToString().Length == 0)
        {
            countyName = "ALL";
        }
        else
        {
            //return regionCode.ToString();
            TaxRuleAdmin taxruleAdmin = new TaxRuleAdmin();
            countyName = taxruleAdmin.GetNameByCountyFIPS(regionCode.ToString());            
        }

        return countyName;
    }
    #endregion
}