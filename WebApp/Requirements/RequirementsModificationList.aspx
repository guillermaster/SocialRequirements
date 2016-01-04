<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequirementsModificationList.aspx.cs" Inherits="SocialRequirements.Requirements.RequirementsModificationList" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    Requirements
</asp:Content>


<asp:Content ID="ToolbarContent" ContentPlaceHolderID="ToolbarContent" runat="server">
    <a href="#" class="btn btn-default"><i class="fa fa-fw fa-wrench"></i></a>
</asp:Content>


<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Repeater ID="RequirementsListRepeater"
        ItemType="SocialRequirements.Domain.DTO.Requirement.RequirementDto" runat="server" OnItemCommand="RequirementsListRepeater_ItemCommand">
        <HeaderTemplate>
            <div>
        </HeaderTemplate>
        <ItemTemplate>
            <div class="smallcard">
                <div class="smallcard_title" style="cursor: pointer"
                    onclick="javascript:location.href='Requirement.aspx?<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.Id %>=<%# Eval("Id") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.CompanyId %>=<%# Eval("CompanyId") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.ProjectId %>=<%# Eval("ProjectId") %>'">
                    <h5><%# Eval("Title") %></h5>
                </div>
                <div class="smallcard_subtitle" style="cursor: pointer">
                    <%# Eval("Project") %>
                </div>
                <div class="smallcard_body" style="cursor: pointer"
                    onclick="javascript:location.href='Requirement.aspx?<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.Id %>=<%# Eval("Id") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.CompanyId %>=<%# Eval("CompanyId") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.ProjectId %>=<%# Eval("ProjectId") %>'">
                    <%# Eval("ShortDescription") %>
                </div>
                <div class="smallcard_footer">
                    <div class="smallcard_footer left">
                        Status: <%# Eval("Status") %><br />
                        Version:
                    </div>
                    <div class="smallcard_footer right">
                        Last modified
                        <br />
                        by: <%# Eval("ModifiedByName") %><br />
                        on: <%# Eval("Modifiedon") %>
                    </div>
                </div>
                <div>
                    <!-- likes and dislikes -->
                    <asp:Panel runat="server" ID="RequirementActionsPanel" CssClass="smallcard_socialactions_wrapper">
                        <ul class="smallcard_socialactions">
                            <li>
                                <asp:LinkButton runat="server" ID="LikeButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Like %>">
                                    <asp:Label runat="server" ID="LikeQty" Text='<%# Eval("Agreed") %>' />
                                    <asp:Image runat="server" ID="LikeImage" ImageUrl="~/assets/img/like.png" ToolTip="Like" />
                                </asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton runat="server" ID="DislikeButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Dislike %>">
                                    <asp:Label runat="server" ID="DislikeQty" Text='<%# Eval("Disagreed") %>' />
                                    <asp:Image runat="server" ID="DislikeImage" ImageUrl="~/assets/img/dislike.png" ToolTip="Dislike" />
                                </asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton runat="server" ID="CommentButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Comment %>">
                                    <asp:Label runat="server" ID="CommentsQty" Text='<%# Eval("CommentsQuantity") %>' />
                                    <asp:Image runat="server" ID="CommentImage" ImageUrl="~/assets/img/comment.png" ToolTip="Comment" />
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </asp:Panel>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
