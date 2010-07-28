<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DownloadSku.ascx.cs" Inherits="PlugIns_DataManager_DownloadSku" %>
<%@ Register TagPrefix="ZNode" TagName="Spacer" Src="~/Controls/Spacer.ascx" %>

<div class="Form">    
    <h1>Download SKUs</h1>
    <div><ZNode:spacer id="Spacer1" SpacerHeight="5" SpacerWidth="3" runat="server"></ZNode:spacer></div>
    <div>
    <table border="0" width="70%" cellpadding="0" cellspacing="0">
    <tr>        
        <td>
            <div class="FieldStyle">Product Name</div>
            <div class="ValueStyle"><asp:DropDownList ID = "lstProduct" AutoPostBack="true" OnSelectedIndexChanged="lstProduct_SelectedIndexChanged" runat="server"></asp:DropDownList></div>
        </td>           
    </tr>
    <tr>                                                                    
      <td>
        <asp:Panel ID= "pnlAttribteslist" Visible = "false" runat="server">
          <div class="FieldStyle">Search SKUs</div>
          <div class="ValueStyle"><asp:PlaceHolder id="ControlPlaceHolder" runat="server"></asp:PlaceHolder></div>        
        </asp:Panel>
      </td>
    </tr>
    <tr>
      <td>
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
      </td>
    </tr>
    <tr>
    <td colspan="2">
        <div class="ValueStyle">
            <asp:Button ID="btnDownloadInventory" runat="server" Text="Download SKU Data" OnClick="btnDownloadProdInventory_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
        </div>
    </td>
    </tr>
    </table>            
    </div>
</div>