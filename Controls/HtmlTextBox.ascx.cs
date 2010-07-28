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
//using Dart.PowerWEB.TextBox;


public partial class Controls_HtmlTextBox : System.Web.UI.UserControl
{
    private int _Mode;

    /// <summary>
    /// Gets or sets the html text
    /// </summary>
    public string Html
    {
        get
        {
            // Remove the <p> tag
            string htmlContent = ZnodeEditBox.Text;

            // Check for first occurence of opening paragraph tag
            if (htmlContent.IndexOf("<p>") == 0)
            {
                htmlContent = htmlContent.Remove(0, 3); // If exists, then remove the first occurence
            }

            // Check for last occurence of closing paragraph tag
            if (htmlContent.LastIndexOf("</p>") == (htmlContent.Length - 4))
            {
                htmlContent = htmlContent.Remove(htmlContent.Length - 4, 4); // If exists, then remove it
            }

            return htmlContent; 
        }
        set
        {
            ZnodeEditBox.Text = value;
        }
    }

    /// <summary>
    /// Sets the mode for this control
    /// </summary>
    public int Mode
    {   
        set
        {
            _Mode = value;
        }
        get
        {
            return _Mode;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ////initialize properties
        //string[] imageUploadPaths = {ZNodeConfigManager.EnvironmentConfig.ImagePath};

        //ctrlHTML.AllowScripts = true;
        //ctrlHTML.ConvertToXhtml = false;
        //ctrlHTML.EnableHtmlIndentation = true;

        //ctrlHTML.ImagesPaths = imageUploadPaths;
        //ctrlHTML.UploadImagesPaths = imageUploadPaths;
        //ctrlHTML.MediaPaths = imageUploadPaths;
        //ctrlHTML.UploadMediaPaths = imageUploadPaths;
        //ctrlHTML.FlashPaths = imageUploadPaths;
        //ctrlHTML.UploadFlashPaths = imageUploadPaths;
        
        //ctrlHTML.DeleteImagesPaths = imageUploadPaths;
        //ctrlHTML.DeleteFlashPaths = imageUploadPaths;
        //ctrlHTML.DeleteMediaPaths = imageUploadPaths;

        //string[] documentUploadPaths = {ZNodeConfigManager.EnvironmentConfig.ContentPath };
        //ctrlHTML.DocumentsPaths = documentUploadPaths;
        //ctrlHTML.UploadDocumentsPaths = documentUploadPaths;
        //ctrlHTML.DeleteDocumentsPaths = documentUploadPaths;       
        
        //ctrlHTML.Width = Unit.Percentage(100);

        //string[] cssPaths = { "~/themes/" + ZNodeConfigManager.SiteConfig.Theme + "/common/style.css" };
        //ctrlHTML.CssFiles = cssPaths;

        ////set mode
        //if (Mode == 0) //Full Mode
        //{
        //    ctrlHTML.ToolsFile = "~/controls/toolsfilefull.xml";           
        //}
        //else if (Mode == 1) //Limited editor Mode
        //{
        //    ctrlHTML.ToolsFile = "~/controls/toolsfilelimited.xml";   
        //}
    }
}
