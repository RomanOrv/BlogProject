<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PersonInfo.aspx.cs" Inherits="Blog.WebUI.Admin.PersonInfo" %>

<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="tablePersonal" CssClass="tablePersonal" runat="server" BorderColor="Black" BackColor="LemonChiffon" HorizontalAlign="Center" BorderWidth="2" ForeColor="Black" GridLines="Both" BorderStyle="Solid">
        <asp:TableHeaderRow ID="TableHeaderRow" runat="server" ForeColor="Black" BackColor="Red">
            <asp:TableHeaderCell>Title</asp:TableHeaderCell>
            <asp:TableHeaderCell>Content</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    <div style="width:100%; height:100px;">
        <asp:Button ID="btnBack" runat="server" Text="Go to Users" OnClick="btnBack_Click" style="position:relative;  left:85%; bottom:-80%;"/>
    </div>
</asp:Content>
