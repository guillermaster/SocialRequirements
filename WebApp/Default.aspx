<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="Default.aspx.cs" Inherits="SocialRequirements.Default" EnableEventValidation="false" %>

<asp:Content runat="server" ID="TitleContent" ContentPlaceHolderID="TitleContent">
    Updates Feed
</asp:Content>

<asp:Content runat="server" ID="ToolbarContent" ContentPlaceHolderID="ToolbarContent">
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

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler_Page);

        function EndRequestHandler_Page(sender, args) {
            tinyMCE.get('<%= TxtContentPost.ClientID %>').setContent('');
            tinyMCE.execCommand('mceRemoveControl', false, "<%= TxtContentPost.ClientID %>");
            initTinyMCE();
        }

        function BeforePostback() {
            var text = tinyMCE.get('<%= TxtContentPost.ClientID %>').getContent();
            var hashtags = document.getElementById('<%= Hashtags.ClientID %>').innerHTML;
            document.getElementById('<%= HdnContentPost.ClientID %>').value = text;
            document.getElementById('<%= HdnHashtags.ClientID %>').value = hashtags;
        }

        function setHashtagInput() {
            var hashtagTextbox = document.getElementById('<%= HashtagInput.ClientID %>');
            hashtagTextbox.style.display = '';
            hashtagTextbox.focus();

            var hashtagLink = document.getElementById('AddHashtagLink');
            hashtagLink.style.display = '';

            var cancelHashtagLink = document.getElementById('CancelHashtagLink');
            cancelHashtagLink.style.display = '';

            var hashtagsVal = document.getElementById('<%= Hashtags.ClientID %>');
            hashtagsVal.style.display = 'none';

            var setHashtagsLink = document.getElementById('SetHashtagInputLink');
            setHashtagsLink.style.display = 'none';
        }

        function addHashtag() {
            var hashtagTextbox = document.getElementById('<%= HashtagInput.ClientID %>');
            var hashtag = hashtagTextbox.value; // the hashtag
            hashtagTextbox.value = '';
            hashtagTextbox.style.display = 'none';
            
            var hashtagLink = document.getElementById('AddHashtagLink');
            hashtagLink.style.display = 'none';

            var cancelHashtagLink = document.getElementById('CancelHashtagLink');
            cancelHashtagLink.style.display = 'none';

            var hashtagsVal = document.getElementById('<%= Hashtags.ClientID %>');
            if (hashtag.length > 0) {
                hashtagsVal.innerHTML = hashtagsVal.innerHTML + ' ' + hashtag;
            }
            hashtagsVal.style.display = '';

            var setHashtagsLink = document.getElementById('SetHashtagInputLink');
            setHashtagsLink.innerText = 'Add another hashtag';
            setHashtagsLink.style.display = '';
            setHashtagsLink.style.fontWeight = 'normal';
            setHashtagsLink.style.border = 'none';

            // increase counter
            var numHashtags = +document.getElementById('<%= HdnHashtagsCounter.ClientID %>').value;
            if (hashtag.length > 0) {
                numHashtags++;
            }
            document.getElementById('<%= HdnHashtagsCounter.ClientID %>').value = numHashtags;

            if (numHashtags === 5) {
                setHashtagsLink.style.display = 'none';
                var hashtagLimit = document.getElementById('<%= MaxHashtagsReached.ClientID %>');
                hashtagLimit.display = '';
            }

            HighlightHashtagLink();
        }

        function cancelHashtag() {
            var hashtagTextbox = document.getElementById('<%= HashtagInput.ClientID %>');
            hashtagTextbox.value = '';
            hashtagTextbox.style.display = 'none';

            var hashtagLink = document.getElementById('AddHashtagLink');
            hashtagLink.style.display = 'none';

            var cancelHashtagLink = document.getElementById('CancelHashtagLink');
            cancelHashtagLink.style.display = 'none';

            var hashtagsVal = document.getElementById('<%= Hashtags.ClientID %>');
            hashtagsVal.style.display = '';

            var setHashtagsLink = document.getElementById('SetHashtagInputLink');
            if (hashtagsVal.innerText === '') {
                HighlightHashtagLink();
            } else {
                setHashtagsLink.innerText = 'Add another hashtag';
            }
            setHashtagsLink.style.display = '';
        }

        function spacecheck(e) {
            var hashtag = document.getElementById('<%= HashtagInput.ClientID %>').value;

            var evt = e || window.event;
            
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;

                if (keyCode === 13) { //if is ENTER
                    addHashtag();
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    } else {
                        evt.returnValue = false;
                    }
                } else {
                    if (!(keyCode >= 48 && keyCode <= 57) && !(keyCode >= 65 && keyCode <= 90) && keyCode !== 8) {
                        if (evt.preventDefault) {
                            evt.preventDefault();
                        } else {
                            evt.returnValue = false;
                        }
                    } else {
                        if (hashtag === '' && keyCode !== 51) { //keycode 51 is #
                            document.getElementById('<%= HashtagInput.ClientID %>').value = '#';
                        } else if (hashtag !== '' && keyCode === 51) {
                            if (evt.preventDefault) {
                                evt.preventDefault();
                            } else {
                                evt.returnValue = false;
                            }
                        }
                    }
                }
            }
        }

        function HighlightHashtagLink() {
            var setHashtagLink = document.getElementById('SetHashtagInputLink');
            var numHashtags = +document.getElementById('<%= HdnHashtagsCounter.ClientID %>').value;
            if (numHashtags === 0) {
                setHashtagLink.style.fontWeight = 'bold';
                setHashtagLink.style.border = 'thin dotted #FF0000';
                setHashtagLink.innerText = "You haven't set any hashtag to your new requirement, click here to set one.";
            }
        }

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

    <!-- POST CONTENT -->

            <asp:Panel runat="server" ID="PostContent" Visible="False">

                <div>
                    <div class="row">
                        <div>
                            <div class="well well-sm well-social-post">
                                <form>
                                    <ul class="list-inline" id="list_PostActions">
                                        <li class="active"><a href="#">Add requirement</a></li>
                                    </ul>
                                    <asp:TextBox runat="server" ID="TxtContentPost" TextMode="MultiLine" CssClass="form-control vresize mceEditor"
                                        Columns="140" Rows="5" placeholder="What's in your mind?" ReadOnly="True" />
                                    <asp:HiddenField runat="server" ID="HdnContentPost"/>
                                    <ul class="list-inline post-actions">
                                        <li>
                                            Company: <asp:DropDownList runat="server" ID="DdlCompanyPost"  OnSelectedIndexChanged="DdlCompanyPost_SelectedIndexChanged" AutoPostBack="True" /></li>
                                        <li>
                                            Project: <asp:DropDownList runat="server" ID="DdlProjectPost" Visible="True" /></li>
                                        <li>
                                            Requirement Title: <asp:TextBox runat="server" ID="TxtContentPostTitle" ClientIDMode="Static" placehoder="Requirement title" Visible="True" Width="300px" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtContentPostTitle"></asp:RequiredFieldValidator>
                                            <asp:Label runat="server" ID="NoTitleLabel" Text="Don't forget to set the title!" Visible="False" ForeColor="#FF0000"/>
                                            <asp:Label runat="server" ID="NoCompanyLabel" Text="Don't forget to select the company!" Visible="False" ForeColor="#FF0000"/>
                                            <asp:Label runat="server" ID="NoProjectLabel" Text="Don't forget to select the project!" Visible="False" ForeColor="#FF0000"/>
                                        </li>
                                        <li><a href="#"><span class="glyphicon glyphicon-camera"></span></a></li>
                                        <li><a href="#" class="glyphicon glyphicon-user"></a></li>
                                        <li><a href="#" class="glyphicon glyphicon-map-marker"></a></li>
                                        <li class="pull-right">
                                            <asp:LinkButton runat="server" Text="Post" ID="BtnPost" onmouseover="Javascript: HighlightHashtagLink()" CssClass="btn btn-primary btn-m" OnClick="BtnPost_Click" />
                                        </li>
                                    </ul>
                                    <div>
                                        Hashtags: <asp:Label runat="server" ID="Hashtags" Text="" style="display: none"/>
                                        <asp:HiddenField runat="server" ID="HdnHashtags"/>
                                        <asp:HiddenField runat="server" ID="HdnHashtagsCounter" Value="0"/>
                                        <asp:TextBox runat="server" ID="HashtagInput" placeholder="#AddHereYourHashtag" MaxLength="144" style="display: none" onkeydown="spacecheck()"/>
                                        <a href="Javascript: setHashtagInput();" id="SetHashtagInputLink">You haven't set any hashtag to your new requirement, click here to set one.</a>
                                        <a href="Javascript: addHashtag();" style="display: none;" id="AddHashtagLink">Add</a>&nbsp;
                                        <a href="Javascript: cancelHashtag();" style="display: none;" id="CancelHashtagLink">Cancel</a>
                                        <asp:Label runat="server" ID="MaxHashtagsReached" Text="You've reached the limit of hashtags" style="display: none"/>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
    <!-- END OF POST CONTENT -->

    <!-- ACTIVITY FEED -->
    <asp:Repeater ID="ActivityFeedRepeater" OnItemDataBound="ActivityFeedRepeater_ItemDataBound"
        ItemType="SocialRequirements.Domain.DTO.General.ActivityFeedDto" runat="server" OnItemCommand="ActivityFeedRepeater_ItemCommand">
        <HeaderTemplate>
            <div id="activityFeed">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="activity">
                <div class="activityTitle">
                    <table>
                        <tr>
                            <td rowspan="3">
                                <asp:Image runat="server" CssClass="avatar" ID="UserAvatarImage" ImageUrl="~/assets/img/user_defaultAvatar.png" />
                            </td>
                            <td>
                                <a href="#"><%# Eval("CreatedByName") %> <%# Eval("CreatedByLastname") %></a>
                                <strong><%# Eval("EntityActionPastTense") %></strong>&nbsp;
                                <asp:HyperLink runat="server" ID="EntityInstanceLink"><%# Eval("EntitySingular") %> </asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%# Eval("Createdon") %>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <!-- hashtags -->
                                <asp:Repeater ID="RequirementsHashtagsRepeater" runat="server">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" Text="<%# Container.DataItem %>"
                                            NavigateUrl="<%# GetRequirementsListByHashtagUrl(Container.DataItem.ToString()) %>"/>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:UpdatePanel runat="server" ID="InnerUpdatePanel">
                    <ContentTemplate>
                        <div class="activity description">
                            <strong><asp:Label runat="server" ID="TitleLabel" Text='<%# Eval("Title") %>' /></strong>
                        </div>
                        <div class="activity description">
                            <asp:Label runat="server" ID="DescriptionLabel" Text='<%# Eval("ShortDescription") %>' />
                            <asp:LinkButton runat="server" ID="ReadMoreButton" Text="Read more" Visible='<%# Eval("ShortDescription").ToString().Length < Eval("Description").ToString().Length %>' CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.ReadMore %>"></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="ReadEvenMoreButton" Text="Read even more" Visible="False" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.ReadEvenMore %>"></asp:LinkButton>
                        </div>
                        <asp:Panel runat="server" ID="ActivityActionsPanel" Visible="False" CssClass="actions_wrapper">
                            <ul class="activity actions">
                                <li>
                                    <asp:LinkButton runat="server" ID="LikeButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Like %>">
                                        <asp:Label runat="server" ID="LikeQty" Text='<%# Eval("Likes") %>' />
                                        <img src="assets/img/like.png" alt="Like" />
                                    </asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="DislikeButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Dislike %>">
                                        <asp:Label runat="server" ID="DislikeQty" Text='<%# Eval("Dislikes") %>' />
                                        <img src="assets/img/dislike.png" alt="Dislike" />
                                    </asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="CommentButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Comment %>">
                                        <asp:Label runat="server" ID="CommentsQty" Text='<%# Eval("Comments") %>' />
                                        <img src="assets/img/comment.png" alt="Comment" />
                                    </asp:LinkButton>
                                </li>
                            </ul>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="CommentsPanel" Visible="False" CssClass="activity_comments">
                            <asp:Repeater runat="server" ID="CommentsRepeater">
                                <HeaderTemplate>
                                    <div class="activity_comments_list">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="activity_comments_comment">
                                        <strong><%# Eval("CreatedByName") %></strong>:<br />
                                        <%# Eval("Comment") %><br />
                                        <span><%# Eval("Createdon") %></span>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>
                            <asp:HiddenField runat="server" ID="CompanyIdComment" Value='<%# Eval("CompanyId") %>' />
                            <asp:HiddenField runat="server" ID="ProjectIdComment" Value='<%# Eval("ProjectId") %>' />
                            <asp:HiddenField runat="server" ID="ParentIdComment" Value='<%# Eval("ParentId") %>' />
                            <asp:HiddenField runat="server" ID="RecordIdComment" Value='<%# Eval("RecordId") %>' />
                            <asp:HiddenField runat="server" ID="EntityIdComment" Value='<%# Eval("EntityId") %>' />
                            <asp:TextBox runat="server" TextMode="MultiLine" ID="NewCommentInput" Rows="6" Columns="100" Style="width: 100%" placeholder="Type your comment here" /><br />
                            <asp:Button runat="server" ID="AddNewCommentButton" Text="Add comment" OnClick="AddNewCommentButton_Click" />
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="CommentButton" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="DislikeButton" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="LikeButton" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ReadMoreButton" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="AddNewCommentButton" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    <!-- END OF ACTIVITY FEED -->
</asp:Content>
