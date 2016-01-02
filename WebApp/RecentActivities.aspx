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

    <asp:UpdateProgress ID="updProgress" DisplayAfter="10" AssociatedUpdatePanelID="PostContentUpdatePanel" runat="server">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="ImageLoading" runat="server" ImageUrl="~/assets/img/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="PostContentUpdatePanel" UpdateMode="Conditional">
        <ContentTemplate>
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
                                        <asp:Image runat="server" CssClass="avatar" ID="UserAvatarImage" ImageUrl="~/assets/img/user_defaultAvatar.png"/>
                                    </td>
                                    <td>
                                        <a href="#"><%# Eval("CreatedByName") %> <%# Eval("CreatedByLastname") %></a>
                                        <strong><%# Eval("EntityAction") %></strong>&nbsp;
                                        <a href="#"><%# Eval("EntityName") %> </a>
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
                            <asp:Label runat="server" ID="DescriptionLabel" Text='<%# Eval("ShortDescription") %>'/>
                            <asp:LinkButton runat="server" ID="ReadMoreButton" Text="Read more" Visible='<%# Eval("ShortDescription").ToString().Length < Eval("Description").ToString().Length %>' CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.ReadMore %>"></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="ReadEvenMoreButton" Text="Read even more" Visible="False" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.ReadEvenMore %>"></asp:LinkButton>
                        </div>
                        <asp:Panel runat="server" ID="ActivityActionsPanel" CssClass="actions_wrapper">
                            <ul class="activity actions">
                                <li>
                                    <asp:LinkButton runat="server" ID="LikeButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Like %>">
                                        <asp:Label runat="server" ID="LikeQty" Text='<%# Eval("Likes") %>'/>
                                        <img src="assets/img/like.png" alt="Like"/>
                                    </asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="DislikeButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Dislike %>">
                                        <asp:Label runat="server" ID="DislikeQty" Text='<%# Eval("Dislikes") %>'/>
                                        <img src="assets/img/dislike.png" alt="Dislike"/>
                                    </asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="CommentButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Comment %>">
                                        <asp:Label runat="server" ID="CommentsQty" Text='<%# Eval("Comments") %>'/>
                                        <img src="assets/img/comment.png" alt="Comment"/>
                                    </asp:LinkButton>
                                </li>
                            </ul>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="AddCommentPanel">
                            <asp:TextBox runat="server" ID="CommentText" TextMode="MultiLine" CssClass="activity comment_input" Rows="5" />
                        </asp:Panel>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
            <!-- END OF ACTIVITY FEED -->
        </ContentTemplate>
        
    </asp:UpdatePanel>
</asp:Content>
