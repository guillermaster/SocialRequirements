<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequirementsList.aspx.cs" Inherits="SocialRequirements.Requirements.RequirementsList" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    <asp:Label runat="server" ID="FormTitle"/>
</asp:Content>


<asp:Content ID="ToolbarContent" ContentPlaceHolderID="ToolbarContent" runat="server">
    <script>
        function showGridView() {
            var repeater = document.getElementById('<%= RequirementsRepeaterPanel.ClientID %>');
            if (repeater != null)
                repeater.style.display = 'none';

            var viewGridButton = document.getElementById('GridViewButton');
            if (viewGridButton != null)
                viewGridButton.style.display = 'none';

            var gridView = document.getElementById('<%= RequirementsListGrid.ClientID %>');
            if (gridView != null)
                gridView.style.display = '';

            var viewListButton = document.getElementById('ListViewButton');
            if (viewListButton != null)
                viewListButton.style.display = '';
        }

        function showListView() {
            var repeater = document.getElementById('<%= RequirementsRepeaterPanel.ClientID %>');
            if (repeater != null)
                repeater.style.display = '';

            var viewGridButton = document.getElementById('GridViewButton');
            if (viewGridButton != null)
                viewGridButton.style.display = '';

            var gridView = document.getElementById('<%= RequirementsListGrid.ClientID %>');
            if (gridView != null)
                gridView.style.display = 'none';

            var viewListButton = document.getElementById('ListViewButton');
            if (viewListButton != null)
                viewListButton.style.display = 'none';
        }
    </script>

    <ul class="demo-btns">
        <li>
            <a id="GridViewButton" title="View as a grid" class="btn btn-default" href="javascript: showGridView();">
                <i class="fa fa-fw fa-table"></i>
            </a>
        </li>
        <li>
            <a id="ListViewButton" title="View as a list" class="btn btn-default" href="javascript: showListView();" style="display: none">
                <i class="fa fa-fw fa-navicon"></i>
            </a>
        </li>
    </ul>
</asp:Content>


<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:GridView runat="server" ID="RequirementsListGrid" ItemType="SocialRequirements.Domain.DTO.Requirement.RequirementDto" 
        CssClass="Grid" AutoGenerateColumns="False" style="display: none" OnRowDataBound="RequirementsListGrid_OnRowDataBound">
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
                            <asp:Label runat="server" ID="Status" Text='<%# Eval("VersionNumber") %>' />
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
                <asp:TemplateField>
                    <ItemStyle Width="80px" />
                    <HeaderTemplate>
                        Last modified on
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align: center;">
                            <asp:Label runat="server" ID="ModifiedOn" Text='<%# Eval("Modifiedon") %>' />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    <asp:Panel runat="server" ID="RequirementsRepeaterPanel">
        <asp:Repeater ID="RequirementsListRepeater"
            ItemType="SocialRequirements.Domain.DTO.Requirement.RequirementDto" runat="server" 
            OnItemCommand="RequirementsListRepeater_ItemCommand" OnItemDataBound="RequirementsListRepeater_OnItemDataBound">
            <HeaderTemplate>
                <div>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="smallcard">
                    <div class="smallcard_title" style="cursor: pointer"
                        onclick="javascript:location.href='<%# SocialRequirements.Domain.General.CommonConstants.FormsFileName.Requirement %>?<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.Id %>=<%# Eval("Id") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.CompanyId %>=<%# Eval("CompanyId") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.ProjectId %>=<%# Eval("ProjectId") %>'">
                        <h5><%# Eval("Title") %></h5>
                    </div>
                    <div class="smallcard_subtitle">
                        <asp:HyperLink runat="server" ID="PriorityButton">&nbsp;</asp:HyperLink>
                        <%# Eval("Project") %>
                    </div>
                    <div class="smallcard_body" style="cursor: pointer"
                        onclick="javascript:location.href='<%# SocialRequirements.Domain.General.CommonConstants.FormsFileName.Requirement %>?<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.Id %>=<%# Eval("Id") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.CompanyId %>=<%# Eval("CompanyId") %>&amp;<%# SocialRequirements.Domain.General.CommonConstants.QueryStringParams.ProjectId %>=<%# Eval("ProjectId") %>'">
                        <%# Eval("ShortDescription") %>
                    </div>
                    <div class="smallcard_footer">
                        <div class="smallcard_footer left">
                            Status: <%# Eval("Status") %><br />
                            Version:
                        </div>
                        <div class="smallcard_footer right">
                            Last modified
                        <br />
                            by: <%# Eval("ModifiedByName") %><br />
                            on: <%# Eval("Modifiedon") %>
                        </div>
                    </div>
                    <div>
                        <!-- likes and dislikes -->
                        <asp:Panel runat="server" ID="RequirementActionsPanel" CssClass="smallcard_socialactions_wrapper">
                            <ul class="smallcard_socialactions">
                                <li>
                                    <asp:LinkButton runat="server" ID="LikeButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Like %>">
                                        <asp:Label runat="server" ID="LikeQty" Text='<%# Eval("Agreed") %>' />
                                        <asp:Image runat="server" ID="LikeImage" ImageUrl="~/assets/img/like.png" ToolTip="Like" />
                                    </asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="DislikeButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Dislike %>">
                                        <asp:Label runat="server" ID="DislikeQty" Text='<%# Eval("Disagreed") %>' />
                                        <asp:Image runat="server" ID="DislikeImage" ImageUrl="~/assets/img/dislike.png" ToolTip="Dislike" />
                                    </asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="CommentButton" CssClass="activity_actions_button" CommandName="<%# SocialRequirements.Domain.General.CommonConstants.SocialActionsCommands.Comment %>">
                                        <asp:Label runat="server" ID="CommentsQty" Text='<%# Eval("CommentsQuantity") %>' />
                                        <asp:Image runat="server" ID="CommentImage" ImageUrl="~/assets/img/comment.png" ToolTip="Comment" />
                                    </asp:LinkButton>
                                </li>
                            </ul>
                        </asp:Panel>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
        
</asp:Content>
