<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequirementsList.aspx.cs" Inherits="SocialRequirements.RequirementsList" %>

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
                        <div class="requirement_list title" 
                            onclick="javascript:location.href='Requirement.aspx?<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.Id %>=<%# Eval("Id") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.CompanyId %>=<%# Eval("CompanyId") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.ProjectId %>=<%# Eval("ProjectId") %>'">
                            <%# Eval("Title") %>
                        </div>
                        <span class="requirement_list project">
                            <a href="#"><%# Eval("Project") %></a>
                        </span>
                        <span class="requirement_list date">
                            <%# Eval("Modifiedon") %>
                        </span>
                        <span class="requirement_list status">
                            <%# Eval("Status") %>
                        </span>
                        <div class="requirement_list description" 
                            onclick="javascript:location.href='Requirement.aspx?<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.Id %>=<%# Eval("Id") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.CompanyId %>=<%# Eval("CompanyId") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.ProjectId %>=<%# Eval("ProjectId") %>'">
                            <%# Eval("ShortDescription") %>
                        </div>
                        <div>
                            <!-- likes and dislikes -->
                            <asp:Panel runat="server" ID="RequirementActionsPanel" CssClass="actions_wrapper">
                            <ul class="activity actions">
                                <li>
                                    <asp:LinkButton runat="server" ID="LikeButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Like %>">
                                        <asp:Label runat="server" ID="LikeQty" Text='<%# Eval("Agreed") %>'/>
                                        <img src="assets/img/like.png" alt="Like"/>
                                    </asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="DislikeButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Dislike %>">
                                        <asp:Label runat="server" ID="DislikeQty" Text='<%# Eval("Disagreed") %>'/>
                                        <img src="assets/img/dislike.png" alt="Dislike"/>
                                    </asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="CommentButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Comment %>">
                                        <asp:Label runat="server" ID="CommentsQty" Text='<%# Eval("CommentsQuantity") %>'/>
                                        <img src="assets/img/comment.png" alt="Comment"/>
                                    </asp:LinkButton>
                                </li>
                            </ul>
                        </asp:Panel>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
