<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadZipcode.ascx.cs" Inherits="PlugIns_DataManager_UploadZipcode" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<div class="Form">    
    <h1>Upload Zip Code Data</h1>
    
    <p>Use this function to load Zip Code reference data into your storefront. This data is used for calculating county based taxes and the Store Locater.</p>        
    <p>You can learn more about obtaining Zip Code data in our documentation <a href="http://help.znode.com/zip_code_data.htm" target="_blank">here</a>.</p>
    <p><strong>WARNING: </strong>This action can not be undone!</p>
        
    <div><ZNode:spacer id="Spacer8" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>  
    <asp:Panel ID = "uploadPanel" runat="server">       
    <div class="FieldStyle">Enter the Path of CSV File</div>
    <div class="HintStyle">Upload your Zip Code data file to your server using FTP. Enter the full path to your file here.</div>
    <div class="ValueStyle">        
        <asp:TextBox ID = "txtInputFile" runat="server" Width="300px"></asp:TextBox>
        <asp:RequiredFieldValidator Display="dynamic"  ControlToValidate="txtInputFile" CssClass="Error" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select a CSV file to upload."></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtInputFile" CssClass="Error" 
                    Display="dynamic" ValidationExpression=".*(\.[cC][sS][vV])" 
                    ErrorMessage="Please enter a valid CSV file."></asp:RegularExpressionValidator>
    </div>        
    <div class="Error"><asp:Literal ID="ltrlError" runat="server"></asp:Literal></div>
    <div><ZNode:spacer id="Spacer2" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>
    
    <div><asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="Submit" Runat="server" onclick="btnSubmit_Click"></asp:button>
    <asp:button class="Button" id="Button1" CausesValidation="false" Text="Cancel" Runat="server" onclick="btnCancel_Click"></asp:button></div>
    
    <div><ZNode:spacer id="Spacer3" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>
    </asp:Panel>
    <div>
        <div class="SuccessMark"><asp:Literal ID="ltrlmsg" runat="server" Visible="false"></asp:Literal></div>
        <div><ZNode:spacer id="Spacer1" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>    
        <div><ZNode:spacer id="Spacer4" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>    
        <div><asp:button class="Button" id="btnGoback" CausesValidation="True" Text="< Back" Runat="server" onclick="btnCancel_Click" Visible="false"></asp:button></div>
    </div>