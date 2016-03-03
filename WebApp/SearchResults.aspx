<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchResults.aspx.cs" Inherits="SocialRequirements.SearchResults" %>
<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    Search Results
</asp:Content>
<asp:Content ID="ToolbarContent" ContentPlaceHolderID="ToolbarContent" runat="server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView ID="SearchResultsListView" ItemType="SocialRequirements.Domain.DTO.General.SearchResultDto" runat="server">
        <ItemTemplate>
            <h3><asp:HyperLink runat="server" NavigateUrl='<%# Eval("Url") %>' ID="SearchLink"><%# Eval("Title") %></asp:HyperLink></h3>
            <p><%# Eval("Description") %></p>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
<asp:Content ID="FileUploadContent" ContentPlaceHolderID="ContentPlaceHolderFileUpload" runat="server">
</asp:Content>
