<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Requirement.aspx.cs" Inherits="SocialRequirements.Requirements.Requirement" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    Requirement
</asp:Content>
<asp:Content ID="ToolbarContent" ContentPlaceHolderID="ToolbarContent" runat="server">
    <ul class="demo-btns">
        <li>
            <asp:LinkButton runat="server" CssClass="btn btn-default" ID="SubmitButton" OnClick="SubmitButton_Click" ToolTip="Submit for approval">
                <i class="fa fa-fw fa-legal"></i>
            </asp:LinkButton>
        </li>
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
            <asp:LinkButton runat="server" CssClass="btn btn-default" ID="UndoEditButton" OnClick="UndoEditButton_OnClick" ToolTip="Undo modify">
                <i class="fa fa-fw fa-undo"></i>
            </asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton runat="server" CssClass="btn btn-default" ID="LikeButton" OnClick="LikeButton_OnClick" ToolTip="Like">
                <i class="fa fa-fw fa-thumbs-o-up">
                    <asp:Label runat="server" ID="LikeCounter" /></i>
            </asp:LinkButton></li>
        <li>
            <asp:LinkButton runat="server" CssClass="btn btn-default" ID="DislikeButton" OnClick="DislikeButton_OnClick" ToolTip="Like">
                <i class="fa fa-fw fa-thumbs-o-down">
                    <asp:Label runat="server" ID="DislikeCounter" /></i>
            </asp:LinkButton></li>
        <li>
            <asp:LinkButton runat="server" CssClass="btn btn-default" ID="CommentsButton" OnClick="CommentsButton_OnClick" ToolTip="Comments">
                <i class="fa fa-fw fa-comments-o">
                    <asp:Label runat="server" ID="CommentCounter" /></i>
            </asp:LinkButton></li>
        <li>
            <asp:LinkButton runat="server" CssClass="btn btn-default" ID="HistoryButton" OnClick="HistoryButton_OnClick" ToolTip="View version history">
                <i class="fa fa-fw fa-history"></i>
            </asp:LinkButton></li>
        <li>
            <asp:LinkButton runat="server" CssClass="btn btn-default" ID="UploadButton" OnClick="UploadButton_OnClick" ToolTip="Upload file">
                <i class="fa fa-fw fa-upload"></i>
            </asp:LinkButton></li>
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
    <div class="bigcard">
        <div class="bigcard_title">
            <asp:Label runat="server" ID="RequirementTitle" />
            <asp:TextBox runat="server" ID="RequirementTitleInput" Visible="False" />
        </div>
        <div class="bigcard_subtitle">
            <asp:Label runat="server" ID="ProjectName"/>
        </div>
        <div class="bigcard_body">
            <asp:Label runat="server" ID="RequirementDescription" />
            <asp:TextBox runat="server" ID="RequirementDescriptionInput" Visible="False" TextMode="MultiLine" Width="100%" Rows="20" />
        </div>
        <div class="bigcard_footer">
            <div class="bigcard_footer left">
                Status:
                    <asp:Label runat="server" ID="RequirementStatus" />
                <asp:HiddenField runat="server" ID="RequirementStatusId"/><br/>
                Version:
                    <asp:Label runat="server" ID="RequirementVersion" />
            </div>
            <div class="bigcard_footer right">
                Created by:
                    <asp:Label runat="server" ID="CreatedByName" />
                    &nbsp;On:
                    <asp:Label runat="server" ID="CreatedOn" /><br />
                Last modified by:
                    <asp:Label runat="server" ID="ModifiedByName" />
                    &nbsp;On:
                    <asp:Label runat="server" ID="ModifiedOn" />
            </div>
        </div>
    </div>
    <div>
        <asp:LinkButton runat="server" ID="ViewHideCommentsButton" Text="View comments" OnClick="ViewHideCommentsButton_Click"></asp:LinkButton>
    </div>
    <asp:Panel runat="server" ID="CommentsPanel" Visible="False">
        <asp:Repeater runat="server" ID="CommentsList">
            <HeaderTemplate>
                <div>
                    <h3>Comments</h3>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="card">
                    <div class="card_tag"><%# Eval("CreatedByName") %>:</div>
                    <div class="card_body">
                        <%# Eval("Comment") %>
                    </div>
                    <div class="card_footer">
                        <div class="card_footer right"><%# Eval("Createdon") %></div>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>
        <asp:TextBox runat="server" TextMode="MultiLine" ID="NewCommentInput" Rows="6" Columns="100" placeholder="Type your comment here" /><br />
        <asp:Button runat="server" ID="AddNewCommentButton" Text="Add comment" OnClick="AddNewCommentButton_Click" />
    </asp:Panel>
</asp:Content>
