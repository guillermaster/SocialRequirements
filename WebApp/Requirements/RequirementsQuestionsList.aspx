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
            <div>
        </HeaderTemplate>
        <ItemTemplate>
            <div class="card">
                <div class="card_tag"><%# Eval("RequirementTitle") %></div>
                <div class="card_tag2"><%# Eval("ProjectName") %></div>
                <div class="card_icon">
                    <i class="fa fa-question-circle"></i>
                </div>
                <div class="card_title"style="cursor: pointer"
                    onclick="javascript:location.href='RequirementQuestion.aspx?<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.Id %>=<%# Eval("Id") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.CompanyId %>=<%# Eval("CompanyId") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.ProjectId %>=<%# Eval("ProjectId") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.RequirementId %>=<%# Eval("RequirementId") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.RequirementVersionId %>=<%# Eval("RequirementVersionId") %>'">
                    <%# Eval("Question") %>
                </div>
                <div class="card_footer">
                    <div class="car_footer left">
                        <span><%# Eval("AnswersQuantity") %> answers</span>
                    </div>
                    <div class="car_footer right">
                        <span>Asked by: </span>
                        <span><%# Eval("CreatedByName") %></span><br/>
                        <span>Asked on: </span>
                        <span><%# Eval("Createdon") %></span><br/>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
