using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

/// <summary>
/// Summary description for ZNodeEULA
/// </summary>
public class ZNodeEULA
{
    public ZNodeEULA()
    {
    }

    /// <summary>
    /// Get the Software License Agreement
    /// </summary>
    /// <returns></returns>
    public static string GetEULA()
    {
        string path = HttpContext.Current.Server.MapPath("~/eula.txt");
        return File.ReadAllText(path);
    }
}
