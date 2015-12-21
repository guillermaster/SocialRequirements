<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Requirement.aspx.cs" Inherits="SocialRequirements.Requirements.Requirement" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    Requirement
</asp:Content>
<asp:Content ID="ToolbarContent" ContentPlaceHolderID="ToolbarContent" runat="server">
    <ul class="demo-btns">
        <li>
            <asp:LinkButton runat="server" CssClass="btn btn-default" ID="SaveButton" OnClick="SaveButton_Click" ToolTip="Save">
                <i class="fa fa-fw fa-save"></i>
            </asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton runat="server" CssClass="btn btn-default" ID="ApproveButton" OnClick="ApproveButton_Click" ToolTip="Approve">
                <i class="fa fa-fw fa-check"></i>
            </asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton runat="server" CssClass="btn btn-default" ID="RejectButton" OnClick="RejectButton_OnClick" ToolTip="Reject">
                <i class="fa fa-fw fa-times"></i>
            </asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton runat="server" CssClass="btn btn-default" ID="EditButton" OnClick="EditButton_OnClick" ToolTip="Modify requirement">
                <i class="fa fa-fw fa-pencil"></i>
            </asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton runat="server" CssClass="btn btn-default" ID="CommentsButton" OnClick="CommentsButton_OnClick" ToolTip="View comments">
                <i class="fa fa-fw fa-comments-o"></i>
            </asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton runat="server" CssClass="btn btn-default" ID="HistoryButton" OnClick="HistoryButton_OnClick" ToolTip="View version history">
                <i class="fa fa-fw fa-history"></i>
            </asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton runat="server" CssClass="btn btn-default" ID="UploadButton" OnClick="UploadButton_OnClick" ToolTip="Upload file">
                <i class="fa fa-fw fa-upload"></i>
            </asp:LinkButton>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="PostSuccessPanel" runat="server" Visible="False" CssClass="alert alert-success" ClientIDMode="Static">
        <p>
            <asp:Label runat="server" ID="PostSuccessMessage" />
        </p>
    </asp:Panel>
    <asp:Panel ID="PostErrorPanel" runat="server" Visible="False" CssClass="alert alert-danger" ClientIDMode="Static">
        <p class="text-danger">
            <asp:Label runat="server" ID="PostErrorMessage" />
        </p>
    </asp:Panel>
    <div>
        <div>
            <asp:Label runat="server" ID="RequirementTitle" />
            <asp:TextBox runat="server" ID="RequirementTitleInput" Visible="False"/>
        </div>
        <div>
            <div>
                <div>
                    Status:
                            <asp:Label runat="server" ID="RequirementStatus" />
                    <asp:HiddenField runat="server" ID="RequirementStatusId"/>
                </div>
                <div>
                    Version:
                            <asp:Label runat="server" ID="RequirementVersion" />
                </div>
            </div>
            <div>
                <div>
                    Created by:
                            <asp:Label runat="server" ID="CreatedByName" />
                    &nbsp;On:
                            <asp:Label runat="server" ID="CreatedOn" />
                </div>
                <div>
                    Last modified by:
                            <asp:Label runat="server" ID="ModifiedByName" />
                    &nbsp;On:
                            <asp:Label runat="server" ID="ModifiedOn" />
                </div>
            </div>

        </div>
        <div>
            <asp:Label runat="server" ID="RequirementDescription" />
            <asp:TextBox runat="server" ID="RequirementDescriptionInput" Visible="False" TextMode="MultiLine" Width="100%" Rows="20"/>
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
</asp:Content>
