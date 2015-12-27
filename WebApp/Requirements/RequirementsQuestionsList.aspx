<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequirementsQuestionsList.aspx.cs" Inherits="SocialRequirements.Requirements.RequirementsQuestionsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Requirement Questions
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ToolbarContent" runat="server">
    <a href="#" class="btn btn-default"><i class="fa fa-fw fa-wrench"></i></a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater ID="RequirementsQuestionsListRepeater"
        ItemType="SocialRequirements.Domain.DTO.Requirement.RequirementQuestionDto" runat="server" OnItemCommand="RequirementsQuestionsListRepeater_ItemCommand">
        <HeaderTemplate>
            <div id="activityFeed">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="requirement_list">
                <div class="requirement_list title"
                    onclick="javascript:location.href='RequirementQuestion.aspx?<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.Id %>=<%# Eval("Id") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.CompanyId %>=<%# Eval("CompanyId") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.ProjectId %>=<%# Eval("ProjectId") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.RequirementId %>=<%# Eval("RequirementId") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.RequirementVersionId %>=<%# Eval("RequirementVersionId") %>'">
                    <%# Eval("RequirementTitle") %>
                </div>
                <span class="requirement_list project">
                    <a href="#"><%# Eval("ProjectName") %></a>
                </span>
                <span class="requirement_list date">
                    <%# Eval("Modifiedon") %>
                </span>
                <span class="requirement_list status">
                    <%# Eval("Status") %>
                </span>
                <div class="requirement_list description"
                    onclick="javascript:location.href='RequirementQuestion.aspx?<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.Id %>=<%# Eval("Id") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.CompanyId %>=<%# Eval("CompanyId") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.ProjectId %>=<%# Eval("ProjectId") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.RequirementId %>=<%# Eval("RequirementId") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.RequirementVersionId %>=<%# Eval("RequirementVersionId") %>'">
                    <%# Eval("Question") %>
                </div>
                <div>
                    <asp:Panel runat="server" ID="RequirementActionsPanel" CssClass="actions_wrapper">
                        <ul class="activity actions">
                            <li>
                                <a onclick="javascript:location.href='RequirementQuestion.aspx?<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.Id %>=<%# Eval("Id") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.CompanyId %>=<%# Eval("CompanyId") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.ProjectId %>=<%# Eval("ProjectId") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.RequirementId %>=<%# Eval("RequirementId") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.RequirementVersionId %>=<%# Eval("RequirementVersionId") %>'">
                                    <asp:Label runat="server" ID="AnswersQty" Text='<%# Eval("AnswersQuantity") %>' />
                                </a>
                            </li>
                        </ul>
                    </asp:Panel>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
