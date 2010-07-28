<%@ WebService Language="C#" Class="ZNodeCatalogServices" %>

using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Data;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class ZNodeCatalogServices : System.Web.Services.WebService
{

    public DataTable ProductListTable
    {
        get
        {   
            if (System.Web.HttpContext.Current.Session["ProductList"] == null)
            {
                ZNode.Libraries.DataAccess.Custom.ProductHelper productHelper = new ZNode.Libraries.DataAccess.Custom.ProductHelper();

                // Get Products       
                System.Data.DataSet ds = productHelper.GetProductListByPortalId(ZNode.Libraries.Framework.Business.ZNodeConfigManager.SiteConfig.PortalID);

                System.Web.HttpContext.Current.Session["ProductList"] = ds.Tables[0];
                
                return ds.Tables[0];
            }
            else
            {
                return (DataTable)System.Web.HttpContext.Current.Session["ProductList"] as DataTable;
            }            
        }
    }
    
    [System.Web.Services.WebMethod(true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCompletionListWithContextAndValues(string prefixText, int count, string contextKey)
    {
        ArrayList items = new ArrayList(count);
        
        System.Data.DataView dv = new System.Data.DataView(ProductListTable);
        dv.RowFilter = "Name like '" + prefixText + "*'";
        int counter = 1;

        foreach (System.Data.DataRowView dr in dv)
        {
            if (counter++ >= count)
                break;

            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["Name"].ToString(), dr["ProductId"].ToString()));
        }

        return (string[])items.ToArray(typeof(string));
    }


    [System.Web.Services.WebMethod(true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetProductStyles(string prefixText, int count, string contextKey)
    {
        ArrayList items = new ArrayList(count);

        System.Data.DataView dv = new System.Data.DataView(ProductListTable);
        
        dv.RowFilter = "ProductNum like '" + prefixText + "*'";
        int counter = 1;

        foreach (System.Data.DataRowView dr in dv)
        {
            if (counter++ >= count)
                break;

            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["ProductNum"].ToString(), dr["ProductId"].ToString()));
        }

        return (string[])items.ToArray(typeof(string));
    }
}
