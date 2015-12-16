<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Requirements.aspx.cs" Inherits="SocialRequirements.Requirements" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    Requirements
</asp:Content>


<asp:Content ID="ToolbarContent" ContentPlaceHolderID="ToolbarContent" runat="server">
    <a href="#" class="btn btn-default"><i class="fa fa-fw fa-wrench"></i></a>
</asp:Content>


<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server" ID="MainScriptManager" />

    <asp:UpdateProgress ID="updProgress" DisplayAfter="10" AssociatedUpdatePanelID="PostContentUpdatePanel" runat="server">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="ImageLoading" runat="server" ImageUrl="~/assets/img/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel runat="server" ID="PostContentUpdatePanel" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Repeater ID="RequirementsListRepeater" 
                ItemType="SocialRequirements.Domain.DTO.Requirement.RequirementDto" runat="server" OnItemCommand="RequirementsListRepeater_ItemCommand">
                <HeaderTemplate>
                    <div id="activityFeed">
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="requirement_list">
                        <div class="requirement_list title">
                            <%# Eval("Title") %>
                        </div>
                        <div class="requirement_list description">
                            <%# Eval("ShortDescription") %>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
