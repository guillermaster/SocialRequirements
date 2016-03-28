<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequirementVersionHistory.aspx.cs" Inherits="SocialRequirements.Requirements.RequirementVersionHistory" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Requirement Version History
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ToolbarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView runat="server" ID="RequirementVersionsGrid" ItemType="SocialRequirements.Domain.DTO.Requirement.RequirementDto"
        CssClass="Grid" AutoGenerateColumns="False" OnSelectedIndexChanged="RequirementVersionsGrid_OnSelectedIndexChanged"
        OnRowDataBound="RequirementVersionsGrid_OnRowDataBound">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    Title
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" ID="Title" Text='<%# Eval("Title") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Project
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" ID="Project" Text='<%# Eval("Project") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Description
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" ID="ShortDescription" Text='<%# Eval("ShortDescription") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Priority
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" ID="Priority" Text='<%# Eval("Priority") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle Width="60px" />
                <HeaderTemplate>
                    Status
                </HeaderTemplate>
                <ItemTemplate>
                    <div style="text-align: center;">
                        <asp:Label runat="server" ID="Status" Text='<%# Eval("Status") %>' />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle Width="40px" />
                <HeaderTemplate>
                    Version
                </HeaderTemplate>
                <ItemTemplate>
                    <div style="text-align: center;">
                        <asp:Label runat="server" ID="Version" Text='<%# Eval("VersionNumber") %>' />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle Width="30px" />
                <HeaderTemplate>
                    <asp:Image runat="server" ID="LikeImage" ImageUrl="~/assets/img/like.png" ToolTip="Like" />
                </HeaderTemplate>
                <ItemTemplate>
                    <div style="text-align: center;">
                        <asp:Label runat="server" ID="Likes" Text='<%# Eval("Agreed") %>' />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle Width="30px" />
                <HeaderTemplate>
                    <asp:Image runat="server" ID="DislikeImage" ImageUrl="~/assets/img/dislike.png" ToolTip="Dislike" />
                </HeaderTemplate>
                <ItemTemplate>
                    <div style="text-align: center;">
                        <asp:Label runat="server" ID="Dislikes" Text='<%# Eval("Disagreed") %>' />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle Width="30px" />
                <HeaderTemplate>
                    <asp:Image runat="server" ID="CommentImage" ImageUrl="~/assets/img/comment.png" ToolTip="Comment" />
                </HeaderTemplate>
                <ItemTemplate>
                    <div style="text-align: center;">
                        <asp:Label runat="server" ID="CommentsQty" Text='<%# Eval("CommentsQuantity") %>' />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle Width="80px" />
                <HeaderTemplate>
                    Last modified by
                </HeaderTemplate>
                <ItemTemplate>
                    <div style="text-align: center;">
                        <asp:Label runat="server" ID="ModifiedBy" Text='<%# Eval("ModifiedByName") %>' />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Panel runat="server" ID="RequirementPanel" Visible="False">
        <div class="bigcard">
            <div class="bigcard_title">
                <h4>
                    <asp:Label runat="server" ID="RequirementTitle" />
                </h4>
                <asp:TextBox runat="server" ID="RequirementTitleInput" Visible="False" />
            </div>
            <div class="bigcard_subtitle">
                <ul class="list-inline" style="margin-bottom: 0;">
                    <li>
                        <asp:HyperLink runat="server" ID="PriorityButton">&nbsp;</asp:HyperLink>
                        <asp:Label runat="server" ID="ProjectName" />
                        <asp:DropDownList runat="server" ID="ProjectInput" Visible="False" />
                        <asp:HiddenField runat="server" ID="PriorityId" />
                    </li>
                    <li class="pull-right">
                        <!-- hashtags -->
                        <asp:Repeater ID="RequirementsHashtagsRepeater" runat="server">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" Text="<%# Container.DataItem %>"
                                    NavigateUrl="<%# GetRequirementsListByHashtagUrl(Container.DataItem.ToString()) %>" />
                            </ItemTemplate>
                        </asp:Repeater>
                    </li>
                </ul>
            </div>
            <div class="bigcard_body">
                <asp:Label runat="server" ID="RequirementDescription" />
                <asp:TextBox runat="server" ID="RequirementDescriptionInput" CssClass="mceEditor" Visible="False" TextMode="MultiLine" Width="100%" Rows="20" />
                <asp:HiddenField runat="server" ID="HdnRequirementDescriptionInput" />
            </div>
            <div class="bigcard_footer">
                <div class="bigcard_footer left">
                    Status:
                    <asp:Label runat="server" ID="RequirementStatus" />
                    <asp:HiddenField runat="server" ID="RequirementStatusId" />
                    <br />
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
    </asp:Panel>
</asp:Content>
