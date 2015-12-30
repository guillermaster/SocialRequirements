<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetCompany.aspx.cs" Inherits="SocialRequirements.Account.SetCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Set Company&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
    <script type="text/javascript">
        function ValidateCompanyTypeList(sender, args) {
            debugger;
            var checkBoxList = document.getElementById("<%=CompanyType.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }
    </script>
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
            <div class="bigcard">
                <div class="bigcard_title">Please choose your company</div>
                <div class="bigcard_body" style="padding: 20px;">
                    <div style="margin: 10px auto;">
                        Company:
                        <asp:DropDownList runat="server" ID="CompaniesDropDownList" AutoPostBack="True" OnSelectedIndexChanged="CompaniesDropDownList_OnSelectedIndexChanged" />
                    </div>
                    <br />
                    <asp:Button runat="server" ID="SetCompanyButton" ClientIDMode="Static" OnClick="SetCompanyButton_Click" Visible="False" Text="Set company" />
                </div>
                <div class="bigcard_footer">
                    <asp:LinkButton runat="server" ID="CompanyNotFoundButton" Text="Can't find your company? Click here" OnClick="CompanyNotFoundButton_Click" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="CreateCompanyPanel" Visible="False">
            <div class="bigcard">
                <div class="bigcard_title">Create new company</div>
                <div class="bigcard_body" style="padding: 20px;">
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
                        <asp:Label runat="server" ID="TypeTitle" AssociatedControlID="CompanyType" Text="Type" CssClass="col-sm-2 control-label" />
                        <div class="col-md-10">
                            <asp:RadioButtonList runat="server" ID="CompanyType" />
                            <asp:CustomValidator ID="CompanyTypeValidator" ErrorMessage="Please select at least one company type."
                                CssClass="text-danger" ClientValidationFunction="ValidateCompanyTypeList" runat="server" />
                        </div>
                    </div>
                    <asp:Button runat="server" ID="CreateCompanyButton" Text="Create company" OnClick="CreateCompanyButton_Click" />
                    <asp:Button runat="server" ID="CancelCreateCompanyButton" Text="Cancel" OnClick="CancelCreateCompanyButton_Click" CausesValidation="False" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
