<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequirementQuestion.aspx.cs" Inherits="SocialRequirements.Requirements.RequirementQuestion" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    Requirement Question
</asp:Content>
<asp:Content ID="ToolbarContent" ContentPlaceHolderID="ToolbarContent" runat="server">
    <ul class="demo-btns">
        <li>
            <asp:LinkButton runat="server" CssClass="btn btn-default" ID="AnswersButton" OnClick="AnswersButton_OnClick" ToolTip="Answers">
                <i class="fa fa-fw fa-comment">
                    <asp:Label runat="server" ID="AnswerCounter" /></i>
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
    <div class="bigcard">
        <div class="bigcard_title">
            <h4><asp:Label runat="server" ID="RequirementTitle" /></h4>
        </div>
        <div class="bigcard_subtitle">
            <asp:Label runat="server" ID="ProjectName" />
        </div>
        <div class="bigcard_body">
            <asp:Label runat="server" ID="QuestionText" />
        </div>
        <div class="bigcard_footer">
            <div class="bigcard_footer left">
                Status:
                    <asp:Label runat="server" ID="QuestionStatus" />
                <asp:HiddenField runat="server" ID="QuestionStatusId" />
            </div>
            <div class="bigcard_footer right">
                Created by:
                    <asp:Label runat="server" ID="CreatedByName" />
                &nbsp;On:
                    <asp:Label runat="server" ID="CreatedOn" />
            </div>
        </div>
    </div>
    <asp:Panel runat="server" ID="AnswersPanel" Visible="True">
        <asp:Repeater runat="server" ID="AnswersList" ItemType="SocialRequirements.Domain.DTO.Requirement.RequirementQuestionAnswerDto">
            <HeaderTemplate>
                <div>
            </HeaderTemplate>
            <ItemTemplate>
                <div>
                    <span><%# Eval("CreatedByName") %>:</span>
                    <span><%# Eval("Answer") %></span>
                    <span><%# Eval("Createdon") %></span>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>

        <asp:Panel runat="server" ID="NoAnswersPanel" Visible="False">
            <div class="alert alert-dismissable alert-warning">
                <i class="fa fa-fw fa-warning"></i>&nbsp; <strong>Warning!</strong> This question has no answers yet.
            </div>
        </asp:Panel>

        <div>
            <asp:TextBox runat="server" TextMode="MultiLine" ID="NewAnswerInput" Rows="6" Columns="100" placeholder="Type your answer here" /><br />
            <asp:Button runat="server" ID="AddNewAnswerButton" Text="Add answer" OnClick="AddNewAnswerButton_Click" />
        </div>
    </asp:Panel>
</asp:Content>
