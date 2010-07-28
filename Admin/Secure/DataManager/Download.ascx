<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Download.ascx.cs" Inherits="PlugIns_DataManager_Download" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<div class="Form">
    <asp:ScriptManager id="ScriptManager" runat="server"></asp:ScriptManager>
    <h1><asp:Literal ID="ltrlTitle" runat="server"></asp:Literal></h1>
    <div><ZNode:spacer id="Spacer1" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>
    
     <asp:Panel ID="pnlProductTypes" runat="server" Visible="false">
    <div class="FieldStyle">Select Product Type</div>
    <div class="ValueStyle">
        <asp:DropDownList ID="ddlProductType" runat="server">
            <asp:ListItem Text="All" Selected="True" Value= "0"></asp:ListItem>
            <asp:ListItem Text="Products Only" Value = "1"></asp:ListItem>
            <asp:ListItem Text="Attributes Only" Value = "2"></asp:ListItem>
            <asp:ListItem Text="AddOnValues Only" Value = "3"></asp:ListItem>
        </asp:DropDownList>
    </div>
    </asp:Panel>
    <div class="FieldStyle">File Type</div>
    <div class="ValueStyle">
        <asp:DropDownList ID="ddlFileSaveType" runat="server">
            <asp:ListItem Text="Microsoft Excel (.xls)" Value=".xls"></asp:ListItem>
            <asp:ListItem Text="Comma Separated Values (.csv)" Value=".csv" Selected="True"></asp:ListItem>
        </asp:DropDownList>
    </div>
    
    <div><ZNode:spacer id="Spacer2" SpacerHeight="1" SpacerWidth="3" runat="server"></ZNode:spacer></div>
    <div class="Error"><asp:Literal ID="ltrlError" runat="server"></asp:Literal></div>       
    <div><ZNode:spacer id="Spacer4" SpacerHeight="10" SpacerWidth="3" runat="server"></ZNode:spacer></div>
    
    <div>
        <!-- Download product inventory Panel -->
        <asp:Panel ID="pnlDownloadInventory" runat="server" Visible="false">
           <div>
                <asp:Button ID="btnDownloadInventory" runat="server" Text="Download Inventory" OnClick="btnDownloadProdInventory_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </asp:Panel>
        
        <!-- Download Product prices Panel -->
        <asp:Panel ID="pnlDownloadPricing" runat="server" Visible="false">
           <div>
                <asp:Button ID="btnDownloadProductPricing" runat="server" Text="Download Pricing" OnClick="btnDownloadProductPricing_Click" />
                <asp:Button ID="btnDownloadCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </asp:Panel>
        
         <!-- Download product Panel -->
        <asp:Panel ID="pnlDownloadProduct" runat="server" Visible="false">
           <div>
                <asp:Button ID="btnDownloadProduct" runat="server" Text="Download Product" OnClick="btnDownloadProduct_Click" />
                <asp:Button ID="btnCancelProduct" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </asp:Panel>
    </div>  
</div>
