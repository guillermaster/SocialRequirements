<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetCompany.aspx.cs" Inherits="SocialRequirements.Account.SetCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Set Company
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ToolbarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" ID="ChooseCompanyPanel" Visible="True">
        Please choose your company
        <br />
        <asp:DropDownList runat="server" ID="CompaniesDropDownList" />
        <br />
        <asp:LinkButton runat="server" ID="CompanyNotFoundButton" Text="Can't find your company? Click here" />
        <br />
        <asp:Button runat="server" ID="SetCompanyButton" />
    </asp:Panel>
    <asp:Panel runat="server" ID="CreateCompanyPanel" Visible="False">
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Name" CssClass="col-md-2 control-label">Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Name" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                    CssClass="text-danger" ErrorMessage="The name field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Type" CssClass="col-md-2 control-label">Type</asp:Label>
            <div class="col-md-10">
                <asp:CheckBoxList runat="server" ID="TypeList"/>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
