using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ZNode.Libraries.Framework.Business;
using ZNode.Libraries.ECommerce.Catalog;
using ZNode.Libraries.DataAccess.Custom;
using ZNode.Libraries.DataAccess.Service;
using ZNode.Libraries.DataAccess.Entities;
using ZNode.Libraries.ECommerce.SEO;
using OboutInc.EasyMenu_Pro;
/// <summary>
/// Summary description for GoOpticNavigation
/// </summary>
public class GoOpticNavigation : ZNodeNavigation
{
    public GoOpticNavigation()
    { }
    //
    // TODO: Add constructor logic here
    //

    public void PopulateStoreMenu2(EasyMenu ctrlMenu, EasyMenu[] ids)
    {
        //check if the product already exists in the cache
        System.Data.DataSet ds = (System.Data.DataSet)HttpContext.Current.Cache["MenuNavigation"];

        if (ds == null)
        {
            CategoryHelper categoryHelper = new CategoryHelper();
            ds = categoryHelper.GetNavigationItems(ZNodeConfigManager.SiteConfig.PortalID);

            //add the hierarchical relationship to the dataset
            ds.Relations.Add("NodeRelation", ds.Tables[0].Columns["CategoryId"], ds.Tables[0].Columns["ParentCategoryId"]);

            HttpContext.Current.Cache["MenuNavigation"] = ds;
        }
        System.Data.DataSet dsBrands = (System.Data.DataSet)HttpContext.Current.Cache["SubMenuBrands"];

        if (dsBrands == null)
        {
            GoOpticCategoryHelper gocategoryHelper = new GoOpticCategoryHelper();
            dsBrands = gocategoryHelper.GetBrands(ZNodeConfigManager.SiteConfig.PortalID);

            //add the hierarchical relationship to the dataset
            //dsBrands.Relations.Add("NodeRelation", ds.Tables[0].Columns["CategoryId"], ds.Tables[0].Columns["ParentCategoryId"]);

            HttpContext.Current.Cache["SubMenuBrands"] = dsBrands;
        }

        int CategoryNo = 0;
        foreach (DataRow dbRow in ds.Tables[0].Rows)
        {
            if (dbRow.IsNull("ParentCategoryID"))
            {
                if ((bool)dbRow["VisibleInd"])
                {
                    //create new menu item
                    // string itemText = dbRow["Name"].ToString();
                    int rows;
                    try
                    {
                        rows = Int32.Parse(dbRow["Rows"].ToString());
                    }
                    catch (Exception e)
                    { rows = 0; }
                    int columns;
                    try
                    {
                        columns = Int32.Parse(dbRow["Columns"].ToString());
                    }
                    catch (Exception e)
                    { columns = 0; }

                    string categoryId = dbRow["CategoryId"].ToString();
                    string seoURL = dbRow["SEOURL"].ToString();
                    string imageName=dbRow["ImageFile"].ToString();
                    string imageNameHOver = imageName.Replace(".","HOver.");
                    string imagePath = ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.OriginalImagePath +imageName ;
                    imagePath = imagePath.Replace("~","");
                    string imagePathHOver = ZNode.Libraries.Framework.Business.ZNodeConfigManager.EnvironmentConfig.OriginalImagePath+imageNameHOver;
                    imagePathHOver = imagePathHOver.Replace("~", "");
                 
                    string categoryText = dbRow["Name"].ToString();
                    if (categoryText.ToLower() == "home")
                        seoURL = "~/";
                    else             
                        seoURL = ZNodeSEOUrl.MakeURL(categoryId, SEOUrlType.Category, seoURL);
                    imageName = imageName.Remove(imageName.IndexOf("."), imageName.Length - imageName.IndexOf("."));
                    string itemText = "<a href=\"" + seoURL.Replace("~","") + "\" onmouseover=\"roll_over('" + imageName + "', '" + imagePathHOver + "')\"  onmouseout=\"roll_over('" + imageName + "', '" + imagePath + "')\" ><img border=\"0\"  name =\"" + imageName + "\" src=\"" + imagePath + "\"/>";
                    //  itemText = itemText.Replace("default", imagePath);

                    categoryText = categoryText.Replace(" ", "");
                    string sepID = "sep" + categoryText;
                    string sepPath = "<img  src=\"~/Images/home/bete.png\"/>";
                    sepPath = sepPath.Replace("~", "");

                    if(CategoryNo!=0)
                        ctrlMenu.AddSeparator(sepID, sepPath);
                    OboutInc.EasyMenu_Pro.MenuItem mi = new OboutInc.EasyMenu_Pro.MenuItem(categoryText, itemText, "","", "", "");
                    ctrlMenu.AddItem(mi);

                    ids[CategoryNo].AttachTo = categoryText;
                    //recursively populate node
                    if (categoryText.ToLower() == "brands")
                    {
                        RecursivelyPopulateMenuBrands(ids[CategoryNo], rows, columns);
                    }
                    else
                    {
                        RecursivelyPopulateMenuSubCategory(dbRow, ids[CategoryNo], dsBrands, rows, columns);
                        // RecursivelyPopulateMenuBrands(ids[CategoryNo], categoryId, dsBrands);
                   }
                    CategoryNo++;
                }
            }
        }


    }

    private void RecursivelyPopulateMenuSubCategory(DataRow dbRow, EasyMenu parentMenuItem, DataSet ds,int rows,int columns)
    {
        string ParentId = dbRow["CategoryId"].ToString();

           //if( dbRow["CategoryId"].ToString().ToLower()=="")
            if (dbRow.GetChildRows("NodeRelation").Length > 0 && columns > 0)
            {
                // parentMenuItem.AttachTo = category;
                // parentMenuItem.Visible = true;
                parentMenuItem.Align = MenuAlign.Under;
                parentMenuItem.Position = MenuPosition.Horizontal;
                parentMenuItem.ShowEvent = MenuShowEvent.MouseOver;
                parentMenuItem.UseIcons = true;
                parentMenuItem.OffsetHorizontal = 10;
                parentMenuItem.ZIndex = 400;
                parentMenuItem.ExpandStyle = ExpandStyle.Slide;
                //parentMenuItem.EventList = "OnAfterMenuClose";
                //parentMenuItem.ID = "mynewid";
                if (columns >= 2)
                    parentMenuItem.RepeatColumns = columns;
                parentMenuItem.Width = (120 * columns).ToString();
                string iconPath = "~/Images/home/";
                iconPath = iconPath.Replace("~", "");
                parentMenuItem.IconsFolder = iconPath;
                OboutInc.EasyMenu_Pro.MenuItem miDepart = new OboutInc.EasyMenu_Pro.MenuItem("Departments", "&nbsp&nbsp&nbsp&nbsp", "departments.jpg", "", "", "");//<span id=\"departments\">departments</span>
                miDepart.Disabled = true;
                // miDepart.Visible = false;
                OboutInc.EasyMenu_Pro.MenuItem miSplit = new OboutInc.EasyMenu_Pro.MenuItem("Split", "&nbsp&nbsp&nbsp&nbsp", "split.png", "", "", "");
                miSplit.Disabled = true;
                parentMenuItem.AddItemAt(0, miDepart);
                parentMenuItem.AddItemAt(1, miSplit);
                int DepartNo = 0;
                foreach (DataRow childRow in dbRow.GetChildRows("NodeRelation"))
                {
                    DepartNo++;
                    if (DepartNo <= rows)
                    {
                        string itemText = childRow["Name"].ToString();
                        string categoryId = childRow["CategoryId"].ToString();
                        string seoURL = childRow["SEOURL"].ToString();

                        seoURL = ZNodeSEOUrl.MakeURL(categoryId, SEOUrlType.Category, seoURL);
                        string categoryText = itemText;
                        categoryText = categoryText.Replace(" ", "");
                        parentMenuItem.AddMenuItem(categoryText, "<span id=\"menu_item\">" + itemText + "</span>", "arrow.png", seoURL, "", "");
                    }
                }
          
                if (DepartNo > rows)
                {
                    string ceva = "Departments";
                    string callback = "for (i = 0; i < ob_em_mynewid.items.length; i++){alert(ob_em_mynewid.items[i].id)}";
                    parentMenuItem.AddMenuItem("ViewAll1", "<span id=\"view_all\">View All...</span>", "", "", "", "displaymessage(ob_em_subMenu4," + ceva + ")");
                }
                if (columns > 1)
                {
                    OboutInc.EasyMenu_Pro.MenuItem miBrand = new OboutInc.EasyMenu_Pro.MenuItem("Brand", "&nbsp&nbsp&nbsp&nbsp", "brands.jpg", "", "", "");//<span id=\"brand\">brands</span>
                    miBrand.Disabled = true;
                    OboutInc.EasyMenu_Pro.MenuItem miSplit2 = new OboutInc.EasyMenu_Pro.MenuItem("Split2", "&nbsp&nbsp&nbsp&nbsp", "split.png", "", "", "");
                    miSplit2.Disabled = true;
                    parentMenuItem.AddItemAt(1, miBrand);
                    parentMenuItem.AddItemAt(1 + columns, miSplit2);
                    int BrandNo = 0;
                    string newID = "";
                    foreach (DataRow childRow in ds.Tables[0].Rows)
                    {

                        string itemText = childRow["ManufacturerName"].ToString();
                        string categoryId = childRow["CategoryId"].ToString();
                        string seoURL = " ";// = childRow["SEOURL"].ToString();
                        // seoURL = ZNodeSEOUrl.MakeURL(categoryId, SEOUrlType.Category, seoURL);
                        string categoryText = itemText;
                        categoryText = categoryText.Replace(" ", "");
                        newID = categoryText+BrandNo.ToString();
                        //newID = newID.Replace(" ","");
                        if (categoryId == ParentId && BrandNo + 1 <= rows)
                        {
                            //parentMenuItem.AddMenuItem(categoryText, itemText, "arrow.png", seoURL, "", "");
                            parentMenuItem.AddMenuItemAt((BrandNo + 1) * 2 - 1, newID, "<span id=\"menu_item\">" + itemText + "</span>", "arrow.png", seoURL, "", "");
                            BrandNo++;
                        }

                    }
                    if (BrandNo > rows)
                        parentMenuItem.AddMenuItem("ViewAll2", "<b>View All...</b>", "", "", "", "");
                }
                if (columns > 2)
                {
                   string  categoryName=dbRow["Name"].ToString().ToLower();
                    if (categoryName == "eyeglasses" || categoryName == "sunglasses")
                    {
                           string imagePath = "~/Images/home/";
                           imagePath = imagePath.Replace("~", "");
                           string itemText = "<a href=\"ceva\" onmouseover=\"roll_over('" + categoryName + "clip_on" + "', '" + imagePath + "clip_on_over.jpg" + "')\"  onmouseout=\"roll_over('" + categoryName + "clip_on" + "', '" + imagePath + "clip_on.jpg" + "')\" ><img border=\"0\"  name =\"" + categoryName + "clip_on" + "\" src=\"" + imagePath +"clip_on.jpg"+ "\"/>";
                           parentMenuItem.AddMenuItemAt(2, categoryName + "clip_on", itemText, "", "", "", "");
                           itemText = "<a href=\"ceva\" onmouseover=\"roll_over('" + categoryName + "goggles" + "', '" + imagePath + "goggles_over.jpg" + "')\"  onmouseout=\"roll_over('" + categoryName + "goggles" + "', '" + imagePath + "goggles.jpg" + "')\" ><img border=\"0\"  name =\"" + categoryName + "goggles" + "\" src=\"" + imagePath + "goggles.jpg" + "\"/>";
                           parentMenuItem.AddMenuItemAt(5, categoryName + "goggles", itemText, "", "", "", "");
                           itemText = "<a href=\"ceva\" onmouseover=\"roll_over('" + categoryName + "readers" + "', '" + imagePath + "readers_over.jpg" + "')\"  onmouseout=\"roll_over('" + categoryName + "readers" + "', '" + imagePath + "readers.jpg" + "')\" ><img border=\"0\"  name =\"" + categoryName + "readers" + "\" src=\"" + imagePath + "readers.jpg" + "\"/>";
                           parentMenuItem.AddMenuItemAt(8, categoryName + "readers", itemText, "", "", "", "");
                           /*itemText = "<a href=\"ceva\" ><img border=\"0\"  name =\"" + categoryName + "build" + "\" src=\"" + imagePath + "build_custom.jpg" + "\"/>";
                           parentMenuItem.AddMenuItemAt(11, categoryName + "build", itemText, "", "", "", "");*/
                          itemText = "<a href=\"ceva\" ><img border=\"0\"  name =\"" + categoryName + "build1" + "\" src=\"" + imagePath + "build_custom1.jpg" + "\"/>";
                           parentMenuItem.AddMenuItemAt(11, categoryName + "build1", itemText, "", "", "", "");
                           itemText = "<a href=\"ceva\" ><img border=\"0\"  name =\"" + categoryName + "build2" + "\" src=\"" + imagePath + "build_custom2.jpg" + "\"/>";
                           parentMenuItem.AddMenuItemAt(14, categoryName + "build2", itemText, "", "", "", "");
                           itemText = "<a href=\"ceva\" ><img border=\"0\"  name =\"" + categoryName + "build3" + "\" src=\"" + imagePath + "build_custom3.jpg" + "\"/>";
                           parentMenuItem.AddMenuItemAt(17, categoryName + "build3", itemText, "", "", "", "");

                    }
                }
          
        }
}

    private void RecursivelyPopulateMenuBrands(EasyMenu parentMenuItem, int rows, int columns)
    {
  
        //check if the product already exists in the cache
        System.Data.DataSet ds = (System.Data.DataSet)HttpContext.Current.Cache["MenuBrands"];

        if (ds == null)
        {
            GoOpticCategoryHelper categoryHelper = new GoOpticCategoryHelper();
            ds = categoryHelper.GetBrandNavigationItems(ZNodeConfigManager.SiteConfig.PortalID);

            HttpContext.Current.Cache["MenuBrands"] = ds;
        }
        
        parentMenuItem.Align = MenuAlign.Under;
        parentMenuItem.Position = MenuPosition.Horizontal;
        parentMenuItem.ShowEvent = MenuShowEvent.MouseOver;
        if (columns >= 2)
            parentMenuItem.RepeatColumns = 2*columns-1;
        parentMenuItem.Width = (100 * columns).ToString();
        parentMenuItem.UseIcons = true;
        parentMenuItem.ZIndex = 400;
        parentMenuItem.ExpandStyle = ExpandStyle.Slide;
        string iconPath = "~/Images/home/";
        iconPath = iconPath.Replace("~", "");
        parentMenuItem.IconsFolder = iconPath;
        int alphabetLetters = 0;
        string alphabet =String.Empty;
        foreach (DataRow dbRow in ds.Tables[0].Rows)
        {
            string brandName = dbRow["Name"].ToString();
            string firstLetter = brandName.Substring(0, 1);
            if (alphabet != firstLetter)
            {
                alphabet = firstLetter;
                alphabetLetters++;
            }
        }
        int BrandsPerCol=(int)Math.Ceiling((double)(ds.Tables[0].Rows.Count+alphabetLetters)/columns);
        int  brandNo = 0;
        int currentCol = 0;
        alphabet = String.Empty;
        OboutInc.EasyMenu_Pro.MenuItem miBrand;
        foreach (DataRow dbRow in ds.Tables[0].Rows)
        {
            string brandName = dbRow["Name"].ToString();
            string firstLetter= brandName.Substring(0, 1);
            string itemBrandName = brandName.Replace(" ","");
            
            if (alphabet != firstLetter)
            {
                alphabet = firstLetter;
                miBrand = new OboutInc.EasyMenu_Pro.MenuItem(alphabet, "<span id=\"alphabet\">" + alphabet.ToUpper() + "</span>", "", "", "", "");
                miBrand.Disabled = true;
                if (currentCol == 0)
                {
                    parentMenuItem.AddItemAt(brandNo,miBrand);
                }else{
                    parentMenuItem.AddItemAt((brandNo+1) *(currentCol+1)-1, miBrand);
                }
                brandNo++;
                if (brandNo+1 == BrandsPerCol)
                {
                    currentCol++;
                    brandNo = 0;
                }
            }
            string seoURL = " ";// = childRow["SEOURL"].ToString();
            // seoURL = ZNodeSEOUrl.MakeURL(categoryId, SEOUrlType.Category, seoURL);
            miBrand = new OboutInc.EasyMenu_Pro.MenuItem(itemBrandName, "<span >" + brandName + "</span>", "",seoURL, "", "");//id=\"menu_item\"
                if (currentCol == 0)
                {
                    parentMenuItem.AddItemAt(brandNo, miBrand);
                }
                else
                {
                    parentMenuItem.AddItemAt((brandNo + 1) * (currentCol + 1) - 1, miBrand);
                }
                brandNo++;
                if (brandNo+1 == BrandsPerCol)
                {
                    currentCol++;
                    brandNo = 0;
                }
           
        }
        int sepCount=1;
        int colSepCount = 1;
        string sepPath = "<img  src=\"~/Images/home/split_brand.jpg\"/>";
        sepPath = sepPath.Replace("~", "");
        for (int i = 0; i < (columns-1) * BrandsPerCol;i++ )
        {
            parentMenuItem.AddSeparatorAt(sepCount, "sep" + sepCount.ToString(), sepPath);
            sepCount+=2*columns-1;
            if ((i + 1)%BrandsPerCol==0)
            {
                colSepCount += 2; sepCount = colSepCount;
            }
        }
    }

   


    public void PopulateStoreMenu(Menu ctrlMenu)
    {

        //check if the product already exists in the cache
        System.Data.DataSet ds = (System.Data.DataSet)HttpContext.Current.Cache["MenuNavigation"];

        if (ds == null)
        {
            CategoryHelper categoryHelper = new CategoryHelper();
            ds = categoryHelper.GetNavigationItems(ZNodeConfigManager.SiteConfig.PortalID);

            //add the hierarchical relationship to the dataset
            ds.Relations.Add("NodeRelation", ds.Tables[0].Columns["CategoryId"], ds.Tables[0].Columns["ParentCategoryId"]);

            HttpContext.Current.Cache["MenuNavigation"] = ds;
        }


        foreach (DataRow dbRow in ds.Tables[0].Rows)
        {
            if (dbRow.IsNull("ParentCategoryID"))
            {
                if ((bool)dbRow["VisibleInd"])
                {
                    //create new menu item
                    System.Web.UI.WebControls.MenuItem mi = new System.Web.UI.WebControls.MenuItem();
                    mi.Text = dbRow["Name"].ToString();

                    string categoryId = dbRow["CategoryId"].ToString();
                    string seoURL = dbRow["SEOURL"].ToString();

                    seoURL = ZNodeSEOUrl.MakeURL(categoryId, SEOUrlType.Category, seoURL);

                    mi.NavigateUrl = seoURL;

                    //add to tree control
                    ctrlMenu.Items.Add(mi);

                    //recursively populate node
                    RecursivelyPopulateMenuRollOver(dbRow, mi);

                }
            }
        }

    }
    //Recursively populate a particular node with it's children
    private void RecursivelyPopulateMenuRollOver(DataRow dbRow, System.Web.UI.WebControls.MenuItem parentMenuItem)
    {
        string ParentId = dbRow["CategoryId"].ToString();
        foreach (DataRow childRow in dbRow.GetChildRows("NodeRelation"))
        {

            System.Web.UI.WebControls.MenuItem mi = new System.Web.UI.WebControls.MenuItem();
            mi.Text = childRow["Name"].ToString();

            string categoryId = childRow["CategoryId"].ToString();
            string seoURL = childRow["SEOURL"].ToString();

            seoURL = ZNodeSEOUrl.MakeURL(categoryId, SEOUrlType.Category, seoURL);

            mi.NavigateUrl = seoURL;

            parentMenuItem.ChildItems.Add(mi);

            RecursivelyPopulateMenuRollOver(childRow, mi);

        }
    }
}
