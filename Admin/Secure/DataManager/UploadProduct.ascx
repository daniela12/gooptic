<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadProduct.ascx.cs" Inherits="PlugIns_DataManager_UploadProduct" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<div class="Form">    
    <h1>Upload Products</h1>
    <div><ZNode:spacer id="Spacer8" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>        
    <div class="FieldStyle">Select CSV File</div>
    <div class="ValueStyle">
        <asp:FileUpload ID="FileUpload" runat="server" Width="350px" />
        <asp:RequiredFieldValidator Display="dynamic"  ControlToValidate="FileUpload" CssClass="Error" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select a CSV file to upload."></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="FileUpload" CssClass="Error" 
                    Display="dynamic" ValidationExpression=".*(\.[cC][sS][vV])" 
                    ErrorMessage="Please select a valid CSV file."></asp:RegularExpressionValidator>
    </div>        
    <div class="Error"><asp:Literal ID="ltrlError" runat="server"></asp:Literal></div>
    <div><ZNode:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>
    
    <div><asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="Submit" Runat="server" onclick="btnSubmit_Click"></asp:button>
    <asp:button class="Button" id="Button1" CausesValidation="false" Text="Cancel" Runat="server" onclick="btnCancel_Click"></asp:button></div>
    
    <div><ZNode:spacer id="Spacer3" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>
</div>