<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetCompany.aspx.cs" Inherits="SocialRequirements.Account.SetCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Set Company
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ToolbarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function DisplaySetCompanyButton() {
            debugger;
            document.getElementById("SetCompanyButton").style.display = 'block';
        }
    </script>
    <asp:ScriptManager runat="server" ID="MainScriptManager" />
    <asp:UpdateProgress ID="updProgress" DisplayAfter="10" AssociatedUpdatePanelID="SetCompanyUpdatePanel" runat="server">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="ImageLoading" runat="server" ImageUrl="~/assets/img/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="SetCompanyUpdatePanel">
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
                <asp:Panel runat="server" ID="ChooseCompanyPanel" Visible="True">
                    Please choose your company
                    <br />
                    <asp:DropDownList runat="server" ID="CompaniesDropDownList" />
                    <br />
                    <asp:LinkButton runat="server" ID="CompanyNotFoundButton" Text="Can't find your company? Click here" OnClick="CompanyNotFoundButton_Click" />
                    <br />
                    <asp:Button runat="server" ID="SetCompanyButton" ClientIDMode="Static" OnClick="SetCompanyButton_Click" Style="visibility: hidden" Text="Set company" />
                </asp:Panel>
                <asp:Panel runat="server" ID="CreateCompanyPanel" Visible="False">
                    <asp:ValidationSummary runat="server" CssClass="text-danger" />
                    <div class="form-group">
                        <asp:Label runat="server" ID="NameTitle" AssociatedControlID="Name" Text="Name" CssClass="col-sm-2 control-label" />
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" ID="Name" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                                CssClass="text-danger" ErrorMessage="The name field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="TypeTitle" AssociatedControlID="Name" Text="Type" CssClass="col-sm-2 control-label" />
                        <div class="col-md-10">
                            <asp:CheckBoxList runat="server" ID="TypeList" />
                        </div>
                    </div>
                    <asp:Button runat="server" ID="CreateCompanyButton" Text="Create company" OnClick="CreateCompanyButton_Click" />
                    <asp:Button runat="server" ID="CancelCreateCompanyButton" Text="Cancel" OnClick="CancelCreateCompanyButton_Click" />
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
