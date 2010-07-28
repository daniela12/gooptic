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
using ZNode.Libraries.Framework.Business;

public partial class Admin_Secure_catalog_product_Highlights_Default : System.Web.UI.Page
{
    # region Protected Member Variables
    protected string AddHighlightPageLink = "~/admin/secure/catalog/product_Highlights/add.aspx";
    protected string DeletePageLink = "~/admin/secure/catalog/product_Highlights/delete.aspx";
    protected DataSet MyDataSet = null;
    # endregion

    # region Page Load Event
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Bind();
            BindHighLightType();
        }
    }
    #endregion

    # region Bind Methods
    /// <summary>
    /// Bind grid
    /// </summary>
    protected void Bind()
    {
        HighlightAdmin AdminAccess = new HighlightAdmin();
        uxGrid.DataSource = AdminAccess.GetAllHighlight(ZNodeConfigManager.SiteConfig.PortalID);
        uxGrid.DataBind();   
    }

    /// <summary>
    /// Binds Highlight Type drop-down list
    /// </summary>
    private void BindHighLightType()
    {
        ZNode.Libraries.Admin.HighlightAdmin highlight = new HighlightAdmin();
        ddlHighlightType.DataSource = highlight.GetAllHighLightType();
        ddlHighlightType.DataTextField = "Name";
        ddlHighlightType.DataValueField = "HighlightTypeId";
        ddlHighlightType.DataBind();
        ListItem item2 = new ListItem("ALL", "0");
        ddlHighlightType.Items.Insert(0, item2);
        ddlHighlightType.SelectedIndex = 0;   
    }

    /// <summary>
    /// Binds Search Data
    /// </summary>
    public DataSet BindSearchData()
    {
        HighlightAdmin AdminAccess = new HighlightAdmin();
        DataSet ds = AdminAccess.SearchProductHighlight(txtName.Text, int.Parse(ddlHighlightType.SelectedValue), ZNodeConfigManager.SiteConfig.PortalID);
        return ds;
    }

    #endregion

    # region Grid Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        uxGrid.PageIndex = e.NewPageIndex;
        Bind();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Page")
        {
        }
        else
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect(AddHighlightPageLink + "?itemid=" + e.CommandArgument);
            }
            else if (e.CommandName == "Delete")
            {
                Response.Redirect(DeletePageLink + "?itemid=" + e.CommandArgument);
            }
        }
    }
    #endregion

    # region Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddHighlight_Click(object sender, EventArgs e)
    {
        Response.Redirect(AddHighlightPageLink);
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
        BindHighLightType();
        Bind();
    }

    # endregion

    # region Helper Method
    protected string GetImagePath(string filename)
    {
        string parmImgPath = "";
        string path = ZNodeConfigManager.EnvironmentConfig.ThumbnailImagePath + filename;
        //get input params     
        string temp = Server.UrlDecode(path);
        parmImgPath = Server.MapPath(temp);

        //Create instance for Fileinfo object
        System.IO.FileInfo imgFile = new System.IO.FileInfo(parmImgPath);
        if (imgFile.Exists)
        {
            return path;
        }
        else
        {

            return ("../../../../Themes/Default/Images/noimage1.gif");
        }
    }
    #endregion
}
