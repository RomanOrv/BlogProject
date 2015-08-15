<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Blog.WebUI.Admin.Login" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Login ID="lgAuth" runat="server" BorderStyle="Solid" BorderWidth="2px" Height="150px" CssClass="login" TitleTextStyle-Font-Bold="true"
        TextBoxStyle-Font-Bold="false" LabelStyle-Font-Bold="true" CheckBoxStyle-Font-Bold="false" BackColor="Bisque"
         OnAuthenticate="lgAuth_OnAuthenticate" OnLoggingIn="lgAuth_OnLoggingIn">
        <ValidatorTextStyle ForeColor="Red" />
    </asp:Login>
</asp:Content>
