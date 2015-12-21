<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequirementModify.aspx.cs" Inherits="SocialRequirements.Requirements.RequirementModify" %>

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
            <asp:Label runat="server" ID="RequirementTitleLabel" Visible="False" />
            <asp:TextBox runat="server" ID="RequirementTitleInput" Visible="True"/>
        </div>
        <div>
            <div>
                <div>
                    Modification Status:
                            <asp:Label runat="server" ID="RequirementStatus" />
                </div>
                <div>
                    Modification Version:
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
            <asp:Label runat="server" ID="RequirementDescriptionLabel" Visible="False" />
            <asp:TextBox runat="server" ID="RequirementDescriptionInput" Visible="True" TextMode="MultiLine" Width="100%" Rows="20"/>
        </div>
    </div>
</asp:Content>
