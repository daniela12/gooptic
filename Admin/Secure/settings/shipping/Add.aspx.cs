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
using System.Data.SqlClient;

public partial class Admin_Secure_settings_ship_Add : System.Web.UI.Page
{
    #region Protected Variables
    protected int ItemId;
    #endregion

    #region Page Load
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

        if (Page.IsPostBack == false)
        {
            //if edit func then bind the data fields
            if (ItemId > 0)
            {
                lblTitle.Text = "Edit Shipping Option";                
            }
            else
            {
                lblTitle.Text = "Add a Shipping Option";
            }
            txtHandlingCharge.Text = (0.0).ToString("N");
            CompareValidator1.Text = "You must enter a valid handling charge (ex: " +(123.45).ToString("N") +" )" ;
            //Bind data to the fields on the page
            BindData();
        }
    }
    #endregion

    #region Bind Data
    /// <summary>
    /// Bind data to the fields 
    /// </summary>
    protected void BindData()
    {
        ShippingAdmin shipAdmin = new ShippingAdmin();
        StoreSettingsAdmin settingsAdmin = new StoreSettingsAdmin();

        //GET LIST DATA
        
        //Get shipping types
        lstShippingType.DataSource = shipAdmin.GetShippingTypes();
        lstShippingType.DataTextField = "Name";
        lstShippingType.DataValueField = "ShippingTypeID";
        lstShippingType.DataBind();
        lstShippingType.SelectedIndex = 0;

        //Bind countries
        BindCountry();

        //get profiles
        lstProfile.DataSource = settingsAdmin.GetProfiles();
        lstProfile.DataTextField = "Name";
        lstProfile.DataValueField = "ProfileID";
        lstProfile.DataBind();
        lstProfile.Items.Insert(0, new ListItem("All Profiles", "-1"));
        lstProfile.SelectedItem.Value = "-1";

        if (ItemId > 0)
        {
            Shipping shippingOption = shipAdmin.GetShippingOptionById(ItemId);

            if(shippingOption.ProfileID.HasValue)
                lstProfile.SelectedValue = shippingOption.ProfileID.Value.ToString();
            lstShippingType.SelectedValue = shippingOption.ShippingTypeID.ToString();

            lstShippingType.Enabled = false;

            //If UPS Shipping Option is Selected
            if (lstShippingType.SelectedValue == "2")
            {
                //Bind UPS Service codes
                BindShippingServiceCodes();
                lstShippingServiceCodes.SelectedValue = shippingOption.ShippingCode;

                pnlShippingServiceCodes.Visible = true;
                pnlShippingOptions.Visible = false;
                pnlDestinationCountry.Visible = false;
            }
            //If FedEx Shipping Option is Selected
            else if (lstShippingType.SelectedValue == "3")
            {
                //Bind FedEx Service codes
                BindShippingServiceCodes();
                lstShippingServiceCodes.SelectedValue = shippingOption.ShippingCode;

                pnlShippingServiceCodes.Visible = true;
                pnlShippingOptions.Visible = false;
                pnlDestinationCountry.Visible = false;
            }
            else
            {
                txtDescription.Text = shippingOption.Description;
                txtShippingCode.Text = shippingOption.ShippingCode;
            }
            if (shippingOption.HandlingCharge > 0)
            {
                txtHandlingCharge.Text = shippingOption.HandlingCharge.ToString("N2");
            }
            else
            {
                txtHandlingCharge.Text = "0.00";
            }
            
            if (shippingOption.DestinationCountryCode != null)
            {
                if (shippingOption.DestinationCountryCode.Length > 0)
                {
                    lstCountries.SelectedValue = shippingOption.DestinationCountryCode;
                }
            }
            chkActiveInd.Checked = (bool)shippingOption.ActiveInd;
            txtDisplayOrder.Text = shippingOption.DisplayOrder.ToString();
        }
        else
        {
           //nothing to do here
        }

    }

    /// <summary>
    /// Binds country drop-down list
    /// </summary>
    private void BindCountry()
    {
        ShippingAdmin shipAdmin = new ShippingAdmin();
        TList<ZNode.Libraries.DataAccess.Entities.Country> countries = shipAdmin.GetDestinationCountries();       

        lstCountries.DataSource = countries;
        lstCountries.DataTextField = "Name";
        lstCountries.DataValueField = "Code";
        lstCountries.DataBind();
        lstCountries.SelectedValue = "*";
    }

    /// <summary>
    /// Binds Shipping Servic code list
    /// </summary>
    private void BindShippingServiceCodes()
    {
        ShippingAdmin shipAdmin = new ShippingAdmin();
        DataSet ds = shipAdmin.GetShippingServiceCodes(int.Parse(lstShippingType.SelectedValue));

        //If FedEx shipping type is selected
        if (lstShippingType.SelectedValue == "3")
        {
            lstShippingServiceCodes.Items.Clear();//Reset dropdownlist items

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string description = dr["Description"].ToString();

                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("&reg;");
                description = regex.Replace(description,"®");
                ListItem li = new ListItem(description, dr["Code"].ToString());
                lstShippingServiceCodes.Items.Add(li);
            }
        }
        else
        {
            lstShippingServiceCodes.DataSource = ds;
            lstShippingServiceCodes.DataTextField = "Description";
            lstShippingServiceCodes.DataValueField = "code";
            lstShippingServiceCodes.DataBind();
        }
    }

    #endregion

    #region General Events
    /// <summary>
    /// Submit button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StoreSettingsAdmin settingsAdmin = new StoreSettingsAdmin();
        ShippingAdmin shipAdmin = new ShippingAdmin();
        Shipping shipOption = new Shipping();

        //If edit mode then retrieve data first
        if (ItemId > 0)
        {
            shipOption = shipAdmin.GetShippingOptionById(ItemId);
        }

        //set values
        shipOption.ActiveInd = chkActiveInd.Checked;
        
        //If UPS Shipping type is selected
        if (lstShippingType.SelectedValue == "2")
        {
            shipOption.ShippingCode = lstShippingServiceCodes.SelectedItem.Value;
            shipOption.Description = lstShippingServiceCodes.SelectedItem.Text;
        }
        //If FedEx Shipping type is selected
        else if (lstShippingType.SelectedValue == "3")
        {
            shipOption.ShippingCode = lstShippingServiceCodes.SelectedItem.Value;
            shipOption.Description = lstShippingServiceCodes.SelectedItem.Text;
        }
        else 
        {
            shipOption.ShippingCode = txtShippingCode.Text;
            shipOption.Description = txtDescription.Text;
        }

        if (lstCountries.SelectedValue.Equals("*"))
        {
            shipOption.DestinationCountryCode = null;
        }
        else
        {
            shipOption.DestinationCountryCode = lstCountries.SelectedValue;
        }

        shipOption.DisplayOrder = int.Parse(txtDisplayOrder.Text);
        //Profile settings
        if (lstProfile.SelectedValue != "-1")
        {
            shipOption.ProfileID = int.Parse(lstProfile.SelectedValue);
        }
        else
        {
            shipOption.ProfileID = null;
        }

        shipOption.ShippingTypeID = int.Parse(lstShippingType.SelectedValue);

        if (txtHandlingCharge.Text.Length > 0)
        {
            shipOption.HandlingCharge = decimal.Parse(txtHandlingCharge.Text);
        }

        bool retval = false;

        if (ItemId > 0)
        {
            retval = shipAdmin.UpdateShippingOption(shipOption);
        }
        else
        {
            retval = shipAdmin.AddShippingOption(shipOption);
        }

        if (retval)
        {
            //redirect to main page
            Response.Redirect("~/admin/secure/settings/shipping/default.aspx");
        }
        else
        {
            //display error message
            lblMsg.Text = "An error occurred while updating. Please try again.";
        }
    }

    /// <summary>
    /// Cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/secure/settings/shipping/default.aspx");
    }

    protected void lstShippingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstShippingType.SelectedValue == "2")
        {
            BindShippingServiceCodes();

            pnlShippingServiceCodes.Visible = true;
            pnlShippingOptions.Visible = false;
            pnlDestinationCountry.Visible = false;
        }
        else if (lstShippingType.SelectedValue == "3")
        {
            BindShippingServiceCodes();

            pnlShippingServiceCodes.Visible = true;
            pnlShippingOptions.Visible = false;
            pnlDestinationCountry.Visible = false;
        }
        else
        {
            pnlDestinationCountry.Visible = true;
            pnlShippingServiceCodes.Visible = false;
            pnlShippingOptions.Visible = true;
        }
    }
    #endregion
}
