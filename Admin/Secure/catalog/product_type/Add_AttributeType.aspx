<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="Add_AttributeType.aspx.cs" Inherits="Admin_Secure_catalog_product_type_Add_AttributeType" %>

<%@ Register Src="~/Controls/DemoMode.ascx" TagName="DemoMode" TagPrefix="uc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="uxMainContent">
    <div class="Form">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <h1>
                        Add ProductType Attribute<uc1:DemoMode id="DemoMode1" runat="server"></uc1:DemoMode></h1>
                </td>
                <td align="right">
                    <asp:Button ID="btnSubmitTop" runat="server" CausesValidation="True" class="Button"
                        OnClick="btnSubmit_Click" Text="SUBMIT" />
                    <asp:Button ID="btnCancelTop" runat="server" CausesValidation="False" class="Button"
                        OnClick="btnCancel_Click" Text="CANCEL" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblError" runat="server" CssClass="Error" Visible="False"></asp:Label>
        <div class="FieldStyle">
            Select Attribute Type<span class="Asterix">*</span>
        </div>
        <div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="lstAttributeTypeList" CssClass="Error"
                Display="Dynamic" ErrorMessage="* Select Attribute Type for this producttype" SetFocusOnError="True"></asp:RequiredFieldValidator>
        </div>
        <div class="ValueStyle">
            <asp:DropDownList  ID="lstAttributeTypeList" runat="server" />
        </div>
        
        <div>
            <asp:Button ID="btnSubmit" runat="server" CausesValidation="True" class="Button"
                OnClick="btnSubmit_Click" Text="SUBMIT" />
            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" class="Button"
                OnClick="btnCancel_Click" Text="CANCEL" />
        </div>
    </div>
</asp:Content>


