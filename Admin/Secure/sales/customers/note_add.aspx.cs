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

public partial class Admin_Secure_sales_customers_Note_Add : System.Web.UI.Page
{
    # region Protected Member Variables

    protected int AccountID;
    protected string CancelLink = "view.aspx?itemid=";

    # endregion

    # region General Events

    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["itemid"] != null)
        {
            AccountID = int.Parse(Request.Params["itemid"].ToString());
        }
              
    }

    /// <summary>
    /// Submit Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        NoteAdmin _Noteadmin = new NoteAdmin();
        Note _NoteAccess = new Note();
                
        _NoteAccess.CaseID = null;
        _NoteAccess.AccountID = AccountID;
        _NoteAccess.CreateDte = System.DateTime.Now;
        _NoteAccess.CreateUser = HttpContext.Current.User.Identity.Name;
        _NoteAccess.NoteTitle = txtNoteTitle.Text.Trim();
        _NoteAccess.NoteBody = ctrlHtmlText.Html ;

        bool Check = _Noteadmin.Insert(_NoteAccess);

        if (Check)
        {
            //redirect to main page
            Response.Redirect(CancelLink + AccountID);
        }
        else
        {
            //display error message
            lblError.Text  = "An error occurred while updating. Please try again.";
            lblError.Visible = true;
        }
        
    }

    /// <summary>
    /// Cancel Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(CancelLink + AccountID);
    }
    # endregion
}
