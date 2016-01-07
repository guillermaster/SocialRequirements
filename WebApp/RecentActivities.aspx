<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecentActivities.aspx.cs" Inherits="SocialRequirements.RecentActivities" EnableEventValidation="false" %>

<asp:Content runat="server" ID="TitleContent" ContentPlaceHolderID="TitleContent">
    Recent Activities
</asp:Content>

<asp:Content runat="server" ID="ToolbarContent" ContentPlaceHolderID="ToolbarContent">
    <a href="#" class="btn btn-default"><i class="fa fa-fw fa-wrench"></i></a>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        $(function () {
            var postActions = $('#list_PostActions');
            var currentAction = $('#list_PostActions li.active');
            if (currentAction.length === 0) {
                postActions.find('li:first').addClass('active');
            }
            postActions.find('li').on('click', function (e) {
                e.preventDefault();
                var self = $(this);
                if (self === currentAction) { return; }
                // else
                currentAction.removeClass('active');
                self.addClass('active');
                currentAction = self;
            });
        });

    </script>

    <asp:Panel runat="server" ID="RequiredActionPanel" Visible="False">
        <div class="jumbotron">
            <p>
                <asp:Literal runat="server" ID="RequiredActionMessage"></asp:Literal>
            </p>
            <p>
                <asp:LinkButton runat="server" ID="RequiredActionExecute" Text="Take action" OnClick="RequiredActionExecute_Click" />
            </p>
        </div>
    </asp:Panel>

    <!-- ACTIVITY FEED -->
    <asp:Repeater ID="ActivityFeedRepeater" OnItemDataBound="ActivityFeedRepeater_ItemDataBound"
        ItemType="SocialRequirements.Domain.DTO.General.ActivityFeedDto" runat="server" OnItemCommand="ActivityFeedRepeater_ItemCommand">
        <HeaderTemplate>
            <div id="activityFeed">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="activity">
                <div class="activityTitle">
                    <table>
                        <tr>
                            <td rowspan="2">
                                <asp:Image runat="server" CssClass="avatar" ID="UserAvatarImage" ImageUrl="~/assets/img/user_defaultAvatar.png" />
                            </td>
                            <td>
                                <a href="#"><%# Eval("CreatedByName") %> <%# Eval("CreatedByLastname") %></a>
                                <strong><%# Eval("EntityActionPastTense") %></strong>&nbsp;
                                <asp:HyperLink runat="server" ID="EntityInstanceLink"><%# Eval("EntitySingular") %> </asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%# Eval("Createdon") %>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="activity description">
                    <asp:Label runat="server" ID="DescriptionLabel" Text='<%# Eval("ShortDescription") %>' />
                    <asp:LinkButton runat="server" ID="ReadMoreButton" Text="Read more" Visible='<%# Eval("ShortDescription").ToString().Length < Eval("Description").ToString().Length %>' CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.ReadMore %>"></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="ReadEvenMoreButton" Text="Read even more" Visible="False" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.ReadEvenMore %>"></asp:LinkButton>
                </div>
                <asp:UpdatePanel runat="server" ID="InnerUpdatePanel">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="ActivityActionsPanel" CssClass="actions_wrapper">
                            <ul class="activity actions">
                                <li>
                                    <asp:LinkButton runat="server" ID="LikeButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Like %>">
                                        <asp:Label runat="server" ID="LikeQty" Text='<%# Eval("Likes") %>' />
                                        <img src="assets/img/like.png" alt="Like" />
                                    </asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="DislikeButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Dislike %>">
                                        <asp:Label runat="server" ID="DislikeQty" Text='<%# Eval("Dislikes") %>' />
                                        <img src="assets/img/dislike.png" alt="Dislike" />
                                    </asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="CommentButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Comment %>">
                                        <asp:Label runat="server" ID="CommentsQty" Text='<%# Eval("Comments") %>' />
                                        <img src="assets/img/comment.png" alt="Comment" />
                                    </asp:LinkButton>
                                </li>
                            </ul>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="CommentsPanel" Visible="False" CssClass="activity_comments">
                            <asp:Repeater runat="server" ID="CommentsRepeater">
                                <HeaderTemplate>
                                    <div class="activity_comments_list">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="activity_comments_comment">
                                        <strong><%# Eval("CreatedByName") %></strong>:<br />
                                        <%# Eval("Comment") %><br />
                                        <span><%# Eval("Createdon") %></span>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>
                            <asp:HiddenField runat="server" ID="CompanyIdComment" Value='<%# Eval("CompanyId") %>' />
                            <asp:HiddenField runat="server" ID="ProjectIdComment" Value='<%# Eval("ProjectId") %>' />
                            <asp:HiddenField runat="server" ID="ParentIdComment" Value='<%# Eval("ParentId") %>' />
                            <asp:HiddenField runat="server" ID="RecordIdComment" Value='<%# Eval("RecordId") %>' />
                            <asp:HiddenField runat="server" ID="EntityIdComment" Value='<%# Eval("EntityId") %>' />
                            <asp:TextBox runat="server" TextMode="MultiLine" ID="NewCommentInput" Rows="6" Columns="100" Style="width: 100%" placeholder="Type your comment here" /><br />
                            <asp:Button runat="server" ID="AddNewCommentButton" Text="Add comment" OnClick="AddNewCommentButton_Click" />
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="CommentButton" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="DislikeButton" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="LikeButton" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    <!-- END OF ACTIVITY FEED -->
</asp:Content>
