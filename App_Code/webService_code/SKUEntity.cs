using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Represents the ZNodeSKU table
/// SKUEntity class inherits all the properties and methods of the SKU entity
/// </summary>
public class SKUEntity : ZNode.Libraries.DataAccess.Entities.SKU
{
    # region Protected Member Variables
    private string _productNum;
    #endregion

    # region Constructors
    public SKUEntity()
    {
    }    
    # endregion

    # region Public Properties
    /// <summary>
    /// Retrieves or sets the Product Number ot Product Code
    /// </summary>
    public string ProductNum
    {
        get { return _productNum; }
        set {_productNum = value; }
    }
    #endregion
}
