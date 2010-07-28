<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DownloadAttributes.ascx.cs" Inherits="PlugIns_DataManager_DownloadAttributes" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<div class="Form">    
    <h1>Download Attributes</h1>
    <div><ZNode:spacer id="Spacer1" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>    
    <div><ZNode:spacer id="Spacer2" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>  
    <div class="FieldStyle">Select Attribute Type</div><br />        
    <div class="ValueStyle"><asp:DropDownList  ID="lstAttributeTypeList" runat="server" /></div>    
    <div><ZNode:spacer id="Spacer5" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>    
    <div><ZNode:spacer id="Spacer6" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>  
    <div class="FieldStyle">File Type</div>
        <div class="ValueStyle">
        <asp:DropDownList ID="ddlFileSaveType" runat="server">
            <asp:ListItem Text="Microsoft Excel (.xls)" Value=".xls"></asp:ListItem>
            <asp:ListItem Text="Comma Separated Values (.csv)" Value=".csv" Selected="True"></asp:ListItem>
        </asp:DropDownList>
        </div>        
        <div><ZNode:spacer id="Spacer3" SpacerHeight="1" SpacerWidth="3" runat="server"></ZNode:spacer></div>
        <div class="Error"><asp:Literal ID="ltrlError" runat="server"></asp:Literal></div>       
        <div><ZNode:spacer id="Spacer4" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>

    <div>
        <asp:Button ID="btnSubmit" runat="server" CausesValidation="True" class="Button" OnClick="btnSubmit_Click" Text="Download Attributes" />
        <asp:Button ID="btnCancel" runat="server" CausesValidation="False" class="Button"  OnClick="btnCancel_Click" Text="Cancel" />
    </div>
</div>