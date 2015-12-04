<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SocialRequirements.Default" %>

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

    <asp:Panel runat="server" ID="PostContent" Visible="False">
            <div>
                <div class="row">
                    <div>
                        <div class="well well-sm well-social-post">
                            <form>
                                <ul class="list-inline" id='list_PostActions'>
                                    <li class='active'><a href='#'>Add requirement</a></li>
                                    <li><a href='#'>Ask question</a></li>
                                </ul>
                                <asp:TextBox runat="server" ID="ContentPost" TextMode="MultiLine" CssClass="form-control vresize" 
                                    Columns="140" Rows="5" placeholder="What's in your mind?" />
                                <ul class='list-inline post-actions'>
                                    <li><a href="#"><span class="glyphicon glyphicon-camera"></span></a></li>
                                    <li><a href="#" class='glyphicon glyphicon-user'></a></li>
                                    <li><a href="#" class='glyphicon glyphicon-map-marker'></a></li>
                                    <li class='pull-right'><a href="#" class='btn btn-primary btn-m'>Post</a></li>
                                </ul>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
    </asp:Panel>
</asp:Content>
