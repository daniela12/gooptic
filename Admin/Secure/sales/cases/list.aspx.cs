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

public partial class Admin_Secure_sales_cases_list : System.Web.UI.Page
{
    # region Protected Member Variables    
    protected string AddLink = "~/admin/secure/sales/cases/add.aspx";
    protected string NotesLink = "~/admin/secure/sales/cases/note_add.aspx?itemid=";
    protected string ViewLink = "~/admin/secure/sales/cases/view.aspx?itemid=";
    protected string EditLink = "~/admin/secure/sales/cases/add.aspx?itemid=";
    protected string EmailLink = "~/admin/secure/sales/cases/case_email.aspx?itemid=";

    # endregion

    # region Protected properties
    private string GridViewSortDirection
    {

        get { return ViewState["SortDirection"] as string ?? "ASC"; }

        set { ViewState["SortDirection"] = value; }

    }   
    # endregion

    # region General Events

    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.BindList();
            this.BindSearchData();
            this.BindGrid();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.BindSearchData();        
    }

    protected void btnClearSearch_Click(object sender, EventArgs e)
    {        
        txtcaseid.Text = string.Empty;
        txtfirstname.Text = string.Empty;
        txtlastname.Text = string.Empty;
        txtcompanyname.Text = string.Empty;        
        txttitle.Text = string.Empty;
        this.BindList();
        this.BindSearchData();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddLink);
    }
    # endregion

    # region Bind Data

    /// <summary>
    /// Bind Searched Data
    /// </summary>
    private void BindSearchData()
    {
        CaseAdmin _CaseAdminAccess = new CaseAdmin();
        DataSet MyDataSet = _CaseAdminAccess.SearchCase(int.Parse(ListCaseStatus.SelectedValue),txtcaseid.Text.Trim(),txtfirstname.Text.Trim(),txtlastname.Text.Trim(),txtcompanyname.Text.Trim(),txttitle.Text.Trim());
        DataView dv = new DataView(MyDataSet.Tables[0]);
        dv.Sort = "CaseID Desc";
        uxGrid.DataSource = dv;
        uxGrid.DataBind();
    }

    protected DataView SortDataTable(DataSet dataSet,string GridViewSortExpression, bool isPageIndexChanging)
    {
        if (dataSet != null)
        {
            DataView dataView = new DataView(dataSet.Tables[0]);

            if (GridViewSortExpression.Length > 0)
            {
                if (isPageIndexChanging)
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GridViewSortDirection);
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GetSortDirection());
                }
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }

    }
    private string GetSortDirection()
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;

            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }
        return GridViewSortDirection;
    }

    /// <summary>
    /// Binds a Grid
    /// </summary>
    private void BindGrid()
    {
        CaseAdmin _CaseAdmin = new CaseAdmin();
        TList<CaseRequest> caseList = _CaseAdmin.GetAll();
        caseList.Sort("CaseID Desc");
        SortDataTable(caseList.ToDataSet(false),"",false);
        uxGrid.DataSource = caseList;
        uxGrid.DataBind();
    }
    private void BindList()
    {
        CaseAdmin _AdminAccess = new CaseAdmin();
        ListCaseStatus.DataSource = _AdminAccess.GetAllCaseStatus();
        ListCaseStatus.DataTextField = "CaseStatusNme";
        ListCaseStatus.DataValueField = "CaseStatusID";
        ListCaseStatus.DataBind();
        ListItem newItem = new ListItem();
        newItem.Text = "All";
        newItem.Value = "-1";        
        ListCaseStatus.Items.Insert(0, newItem);
        ListCaseStatus.SelectedIndex = 1;
    }

    # endregion

    # region Grid Events

    protected void uxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "page")
        {

        }
        else
        {
            // Convert the row index stored in the CommandArgument
            // Get the values from the appropriate 
            // cell in the GridView control.
             string Id = e.CommandArgument.ToString();

            if (e.CommandName == "Edit")
            {
                EditLink = EditLink + Id;
                Response.Redirect(EditLink);
            }
            else if(e.CommandName == "AddNote")
            {
                Response.Redirect(NotesLink + Id);
            }
            else if (e.CommandName == "Reply")
            {
                Response.Redirect(EmailLink + Id);
            }
            else if (e.CommandName == "View")
            {
                Response.Redirect(ViewLink + Id);
            }

        }
    }
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {        
       uxGrid.PageIndex = e.NewPageIndex;
       this.BindSearchData();      
    }


    /// <summary>
    /// Occurs when the hyperlink to sort a column is clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_Sorting(object sender, GridViewSortEventArgs e)
    {        
        CaseAdmin _CaseAdminAccess = new CaseAdmin();
        DataSet MyDataSet = _CaseAdminAccess.SearchCase(int.Parse(ListCaseStatus.SelectedValue), txtcaseid.Text.Trim(), txtfirstname.Text.Trim(), txtlastname.Text.Trim(), txtcompanyname.Text.Trim(), txttitle.Text.Trim());
        uxGrid.DataSource = SortDataTable(MyDataSet, e.SortExpression,true);
        uxGrid.DataBind();

        if (GetSortDirection() == "DESC")
        {
            e.SortDirection = (SortDirection.Descending);
        }
        else
        {
            e.SortDirection = SortDirection.Ascending;
        }
    }

    # endregion
    
    # region Helper Methods

    public string GetCaseStatusByCaseID(Object FieldValue)
    {
        if (FieldValue == null)
        {
            return String.Empty;
        }
        else
        {
            CaseAdmin _CaseStatusAdminAccess = new CaseAdmin();
            CaseStatus _caseStatusList = _CaseStatusAdminAccess.GetByCaseStatusID(int.Parse(FieldValue.ToString()));
            if (_caseStatusList == null)
            {
                return string.Empty;
            }
            else
            {
                return _caseStatusList.CaseStatusNme;
            }
            
        }
    }
    public string GetCasePriorityByCaseID(Object FieldValue)
    {
        if (FieldValue == null)
        {
            return String.Empty;
        }
        else
        {
            CaseAdmin _CasePriorityAdmin = new CaseAdmin();
            CasePriority _CasePriority = _CasePriorityAdmin.GetByCasePriorityID(int.Parse(FieldValue.ToString()));
            if(_CasePriority == null)
            {
              return string.Empty;      
            }
            else
            {
            return _CasePriority.CasePriorityNme;
            }

        }
    }

    # endregion    
}
