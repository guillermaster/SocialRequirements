<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SocialRequirements.Default" EnableEventValidation="false" %>

<asp:Content runat="server" ID="TitleContent" ContentPlaceHolderID="TitleContent">
    Updates Feed
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
            <script>
                function fadeOutControl(controlName) {
                    $(controlName).fadeOut(3800);
                }
            </script>
            <!-- POST CONTENT -->
            <asp:Panel runat="server" ID="PostContent" Visible="False">
                <div>
                    <div class="row">
                        <div>
                            <div class="well well-sm well-social-post">
                                <form>
                                    <ul class="list-inline" id="list_PostActions">
                                        <li class="active"><a href="#">Add requirement</a></li>
                                        <li><a href="#">Ask question</a></li>
                                    </ul>
                                    <asp:TextBox runat="server" ID="TxtContentPost" TextMode="MultiLine" CssClass="form-control vresize"
                                        Columns="140" Rows="5" placeholder="What's in your mind?" />
                                    <ul class="list-inline post-actions">
                                        <li>
                                            <asp:DropDownList runat="server" ID="DdlCompanyPost" OnSelectedIndexChanged="DdlCompanyPost_SelectedIndexChanged" AutoPostBack="True" /></li>
                                        <li>
                                            <asp:DropDownList runat="server" ID="DdlProjectPost" Visible="False" /></li>
                                        <li>
                                            <asp:TextBox runat="server" ID="TxtContentPostTitle" placehoder="Title your requirement here" /></li>
                                        <li><a href="#"><span class="glyphicon glyphicon-camera"></span></a></li>
                                        <li><a href="#" class="glyphicon glyphicon-user"></a></li>
                                        <li><a href="#" class="glyphicon glyphicon-map-marker"></a></li>
                                        <li class="pull-right">
                                            <asp:LinkButton runat="server" Text="Post" ID="BtnPost" CssClass="btn btn-primary btn-m" OnClick="BtnPost_Click" />
                                        </li>
                                    </ul>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <!-- END OF POST CONTENT -->
            
            
            <asp:Panel ID="PostSuccessPanel" runat="server" Visible="False" CssClass="alert alert-success" ClientIDMode="Static">
                <p>
                    <asp:Label runat="server" ID="PostSuccessMessage" />
                </p>
            </asp:Panel>
            <asp:Panel ID="PostErrorPanel" runat="server" Visible="False" CssClass="alert alert-danger" ClientIDMode="Static">
                <p class="text-danger">
                    <asp:Literal runat="server" ID="PostErrorMessage" />
                </p>
            </asp:Panel>
            
            <!-- ACTIVITY FEED -->
            <asp:Repeater ID="ActivityFeedRepeater" OnItemDataBound="ActivityFeedRepeater_ItemDataBound" 
                ItemType="SocialRequirements.Domain.DTO.General.ActivityFeedDto" runat="server">
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
                                        <strong>created a new </strong>&nbsp;
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
                            Description goes here
                        </div>
                        <asp:Panel runat="server" ID="ActivityActionsPanel" CssClass="actions_wrapper">
                            <ul class="activity actions">
                                <li>
                                    <asp:ImageButton runat="server" ID="LikeButton" ImageUrl="~/assets/img/like.png" CssClass="button"/>
                                </li>
                                <li>
                                    <asp:ImageButton runat="server" ID="DislikeButton" ImageUrl="~/assets/img/dislike.png" CssClass="button"/>
                                </li>
                                <li>
                                    <asp:ImageButton runat="server" ID="CommentButton" ImageUrl="~/assets/img/comment.png" CssClass="button"/>
                                </li>
                            </ul>
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
