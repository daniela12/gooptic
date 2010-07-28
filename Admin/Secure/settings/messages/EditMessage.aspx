<%@ Page Language="C#" MasterPageFile="~/Admin/Themes/Standard/edit.master" AutoEventWireup="true" CodeFile="EditMessage.aspx.cs" Inherits="Admin_Secure_settings_messages_EditMessage" ValidateRequest ="false" %>

<%@ Register Src="~/Controls/HtmlTextBox.ascx" TagName="HtmlTextBox" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="../../../../Data/Default/Config/MessageConfig.xml" ></asp:XmlDataSource><br />
    <h1><asp:Label ID="lbltitle" runat="server"></asp:Label></h1><br />
        <div class="ValueStyle">
            <uc1:HtmlTextBox id="ctrlHtmlText" runat="server"></uc1:HtmlTextBox>
        </div><br />
	    <div>
	        <asp:button class="Button" id="btnSubmit" CausesValidation="True" Text="SUBMIT" Runat="server" onclick="btnSubmit_Click"/> &nbsp;
	        <asp:Button class="Button" ID="btnCancel" CausesValidation="true" Text="CANCEL" runat="server" OnClick="btnCancel_Click" />	
	    </div>	
	    <br />		   
	    <div><asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="LimeGreen"></asp:Label></div>    
	     
</asp:Content>
