<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Requirement.aspx.cs" Inherits="SocialRequirements.Requirement" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    Requirement
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
            <div>
                <div>
                    <asp:Label runat="server" ID="RequirementTitle" />
                </div>
                <div>
                    <div>
                        Status:
                        <asp:Label runat="server" ID="RequirementStatus"/>
                    </div>
                    <div>
                        <div>
                            Created by:
                            <asp:Label runat="server" ID="CreatedByName"/>
                            &nbsp;On:
                            <asp:Label runat="server" ID="CreatedOn"/>
                        </div>
                        <div>
                            Last modified by:
                            <asp:Label runat="server" ID="ModifiedByName"/>
                            &nbsp;On:
                            <asp:Label runat="server" ID="ModifiedOn"/>
                        </div>
                    </div>
                    
                </div>
                <div>
                    <asp:Label runat="server" ID="RequirementDescription" />
                </div>
                <div>
                    <!-- likes and dislikes -->
                    <asp:Panel runat="server" ID="RequirementActionsPanel" CssClass="actions_wrapper">
                        <ul class="activity actions">
                            <li>
                                <asp:LinkButton runat="server" ID="LikeButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Like %>">
                                    <asp:Label runat="server" ID="LikeQty" />
                                    <img src="assets/img/like.png" alt="Like" />
                                </asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton runat="server" ID="DislikeButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Dislike %>">
                                    <asp:Label runat="server" ID="DislikeQty" />
                                    <img src="assets/img/dislike.png" alt="Dislike" />
                                </asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton runat="server" ID="CommentButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Comment %>">
                                    <asp:Label runat="server" ID="CommentsQty" />
                                    <img src="assets/img/comment.png" alt="Comment" />
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
