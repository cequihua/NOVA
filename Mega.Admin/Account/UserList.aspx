<%@ Page Title="Administración de Usuarios" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Mega.Admin.Account.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Administración de Usuarios</h2>
    <p>
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Account/UserAdd.aspx">Agregar Usuario</asp:HyperLink>
    </p>
    <div class="table_container">
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" DataSourceID="LinqDataSource1" PagerStyle-CssClass="grid-pager">
            <Columns>
                <asp:HyperLinkField HeaderText="Opciones" Text="Editar" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/Account/UserEdit.aspx?id={0}" />
                <asp:BoundField DataField="Id" HeaderText="Login" ReadOnly="True" 
                    SortExpression="Id" />
                <asp:BoundField DataField="Name" HeaderText="Nombre" ReadOnly="True" 
                    SortExpression="Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="True" 
                    SortExpression="Email" />
                <asp:BoundField DataField="ValidDate" DataFormatString="{0:d}" 
                    HeaderText="Valido hasta" ReadOnly="True" SortExpression="ValidDate" />
                <asp:CheckBoxField DataField="Disabled" HeaderText="Inactivo" ReadOnly="True" 
                    SortExpression="Disabled" />
            </Columns>
        </asp:GridView>
    </div>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="Mega.Common.AdminDataContext"
        EntityTypeName="" OrderBy="Name" Select="new (Id, Name, Email, ValidDate, Disabled)"
        TableName="Users" OnContextCreating="LinqDataSource1_OnContextCreating">
    </asp:LinqDataSource>
</asp:Content>
