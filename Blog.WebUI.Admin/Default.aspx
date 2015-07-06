<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Blog.WebUI.Admin.Default" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ObjectDataSource ID="odsUser" runat="server"
        OnObjectCreating="odsUser_ObjectCreating"
        TypeName="Blog.Repository.EFUserRepository"
        SelectMethod="GetAllUsers"
        UpdateMethod="UpdateUser"
        OnUpdating="odsUser_Updating">
        <UpdateParameters>
            <asp:Parameter Name="IsEnable" Type="Boolean" />
        </UpdateParameters>
    </asp:ObjectDataSource>

    <asp:GridView ID="grvUser" runat="server" CssClass="grvUser"
        DataSourceID="odsUser"
        AutoGenerateColumns="false"
        DataKeyNames="Id"  
        ShowHeader="true"
        AutoGenerateEditButton="true"
        OnRowCommand ="grvUser_RowCommand"
        OnRowEditing="grvUser_RowEditing" >
        <Columns>
            <asp:TemplateField HeaderText="isEnable">
                <ItemTemplate>
                    <asp:CheckBox ID="chbxEnable" runat="server" Enabled="false" Checked='<%# Eval("IsEnable") %>'
                        Text='<%# Convert.ToBoolean(Eval("IsEnable")) == true ? "Enable" : "Disable" %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="chbxEnable" runat="server" Enabled="true" Checked='<%# Eval("IsEnable") %>'
                        Text='<%# Convert.ToBoolean(Eval("IsEnable")) == true ? "Enable" : "Disable" %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Id" HeaderText="UserId" ReadOnly="true" />
            <asp:BoundField DataField="Surname" HeaderText="Lastname" ReadOnly="true" />
            <asp:BoundField DataField="Firstname" HeaderText="Firstname" ReadOnly="true" />
              <asp:BoundField DataField="Username" HeaderText="Username" ReadOnly="true" />
            <asp:BoundField DataField="DateRegister" HeaderText="Registration" ReadOnly="true" />
            <asp:BoundField DataField="isAdmin" HeaderText="is Admin" ReadOnly="true" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnInfo" runat="server" Text="View Personal Info" OnClick="btnInfo_Click"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
