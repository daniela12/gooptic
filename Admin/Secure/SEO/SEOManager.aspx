<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Themes/Standard/content.master"  CodeFile="SEOManager.aspx.cs" Inherits="Admin_Secure_catalog_SEO_SEOManager" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxMainContent" Runat="Server">
    <h1>Search Engine Optimization (SEO)</h1>
    <p>Use the links on this page to manage the Search Engine Optimization (SEO) settings for your storefront</p>
    
    <div class="LandingPage">
      <hr />      
      <div class="Shortcut"><a id="A3" href="~/admin/secure/SEO/Default.aspx" runat="server">Default SEO Setings</a></div>
      <p>Set default meta tags for each page in your storefront. These settings can also be overridden in the SEO section of the Product, Category and Content admin pages.</p>
              
      <div class="Shortcut"><a id="A7" href="~/admin/secure/SEO/product/product_list.aspx" runat="server">Product SEO Setings</a></div>
      <p>Update product specific attributes that are used by search engines such as Description, Title, Meta Tags and URL.</p>
  
      <div class="Shortcut"><a id="A5" href="~/admin/secure/SEO/category/product_category_list.aspx" runat="server">Category SEO Settings</a></div>
      <p>Update category specific attributes that are used by search engines such as Description, Title, Meta Tags and URL.</p>
                  
      <div class="Shortcut"><a id="A2" href="~/admin/secure/SEO/pages/pages_list.aspx" runat="server">Content Page SEO Settings</a></div>    
      <p>Update various elements of content pages that are used by search engines such as Title, Content, Meta Tags and URL.</p>
      
      <div class="Shortcut"><a id="A4" href="~/admin/secure/SEO/UrlRedirect/default.aspx" runat="server">Manage URL Redirection</a></div>          
      <p>Create automatic 301 re-directs from old urls to new urls to indicate to search engines that the page has been re-located.</p>
      
      <div class="Shortcut"><a id="A1" href="~/admin/secure/SEO/reports/popularsearch.aspx?filter=23" runat="server">Most Popular Searches</a></div>      
      <p>Run a report of the search keywords that visitors use to locate products on your site. 
      You can then use this report to help refine your Pay per Click key words and SEO strategy.</p>
      
      <div class="Shortcut"><a id="A6" href="~/admin/secure/SEO/Tracking/DownloadTracking.aspx" runat="server">Download Tracking Data</a></div>          
      <p>Download affiliate tracking data that has been collected on the site. You can use this data in your favorite reporting tool to analyze site traffic.</p>      
      
    </div>
</asp:Content>
