<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetProject.aspx.cs" Inherits="SocialRequirements.Account.SetProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Set Project
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ToolbarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server" ID="MainScriptManager" />
    <asp:UpdateProgress ID="updProgress" DisplayAfter="10" AssociatedUpdatePanelID="SetProjectUpdatePanel" runat="server">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="ImageLoading" runat="server" ImageUrl="~/assets/img/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="SetProjectUpdatePanel">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:Panel ID="SuccessPanel" runat="server" Visible="False" CssClass="alert alert-success">
                    <p>
                        <asp:Label runat="server" ID="SuccessMessage" />
                    </p>
                    <asp:LinkButton runat="server" ID="ContinueLinkButton" Text="Continue" OnClick="ContinueLinkButton_Click" />
                </asp:Panel>
                <asp:Panel ID="ErrorPanel" runat="server" Visible="False" CssClass="alert alert-danger">
                    <a name="ErrorPanel"></a>
                    <p class="text-danger">
                        <asp:Literal runat="server" ID="ErrorMessage" />
                    </p>
                </asp:Panel>
                <asp:Panel runat="server" ID="CreateProjectPanel" Visible="True">
                    <asp:ValidationSummary runat="server" CssClass="text-danger" />
                    <!-- COMPANY -->
                    <div class="form-group">
                        <asp:Label runat="server" ID="LblCompany" AssociatedControlID="DdlCompany" Text="Company" CssClass="col-sm-2 control-label" />
                        <div class="col-sm-10">
                            <asp:DropDownList runat="server" ID="DdlCompany" />
                        </div>
                    </div>
                    <!-- TITLE -->
                    <div class="form-group">
                        <asp:Label runat="server" ID="LblTitle" AssociatedControlID="TxtTitle" Text="Title" CssClass="col-sm-2 control-label" />
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" ID="TxtTitle" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtTitle"
                                CssClass="text-danger" ErrorMessage="The title field is required." />
                        </div>
                    </div>
                    <!-- DESCRIPTION -->
                    <div class="form-group">
                        <asp:Label runat="server" ID="LblDescription" AssociatedControlID="TxtDescription" Text="Description" CssClass="col-sm-2 control-label" />
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="TxtDescription" CssClass="form-control" TextMode="MultiLine" Columns="20" Rows="10" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtDescription"
                                CssClass="text-danger" ErrorMessage="The description field is required." />
                        </div>
                    </div>
                    <!-- COMMAND BUTTONS -->
                    <asp:Button runat="server" ID="CreateProjectButton" Text="Create project" OnClick="CreateProjectButton_Click" />
                    <asp:Button runat="server" ID="CancelCreateProjectButton" Text="Cancel" OnClick="CancelCreateProjectButton_Click" />
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
