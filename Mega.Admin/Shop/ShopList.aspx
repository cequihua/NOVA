<%@ Page Title="Administración de Tiendas (CNV)" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ShopList.aspx.cs" Inherits="Mega.Admin.Shop.ShopList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Administración de Tiendas (CNV)</h2>
    <p>
    </p>
    <div id="shop-list">
        <fieldset>
            <legend>Filtros</legend>
            <div class="doble_column_content">
                <p>
                    <label>
                        Id/Nombre:
                    </label>
                    <asp:TextBox ID="IDOrNameTextBox" runat="server" Text="" />
                </p>
            </div>
            <div class="doble_column_content">
                <p>
                    <asp:Button ID="FilterButton" runat="server" Text="Filtrar" OnClick="FilterButton_Click"
                        CausesValidation="false" />
                </p>
            </div>
        </fieldset>
    </div>
    <p>
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Shop/ShopAdd.aspx">Agregar Tienda</asp:HyperLink>
    </p>
    <div class="table_container">
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" DataSourceID="LinqDataSource1" PagerStyle-CssClass="grid-pager">
            <Columns>
                <asp:HyperLinkField HeaderText="Opciones" Text="Editar" DataNavigateUrlFields="Id"
                    DataNavigateUrlFormatString="~/Shop/ShopEdit.aspx?id={0}" />
                <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="KeyCodeName" HeaderText="Código Clave" ReadOnly="True"
                    SortExpression="KeyCodeName" />
                <asp:BoundField DataField="Name" HeaderText="Nombre" ReadOnly="True" SortExpression="Name" />
                <asp:BoundField DataField="Phone1" HeaderText="Teléfono 1" ReadOnly="True" SortExpression="Phone1" />
                <asp:BoundField DataField="CompanyName" HeaderText="Compañía" ReadOnly="True" SortExpression="CompanyName" />
                <asp:BoundField DataField="CountryName" HeaderText="País" ReadOnly="True" SortExpression="CountryName" />
                <asp:BoundField DataField="CurrencyCode" HeaderText="Moneda" ReadOnly="True" SortExpression="" />
                <asp:CheckBoxField DataField="Disabled" HeaderText="Desabilitado" ReadOnly="True"
                    SortExpression="Disabled" />
            </Columns>
        </asp:GridView>
    </div>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="Mega.Common.AdminDataContext"
        EntityTypeName="" OrderBy="Name" OnContextCreating="LinqDataSource1_OnContextCreating"
        TableName="Shops" Where="Name.Contains(@Filter) || Id.Contains(@Filter)">
        <WhereParameters>
            <asp:ControlParameter ControlID="IDOrNameTextBox" PropertyName="Text" Name="Filter" ConvertEmptyStringToNull="false" />
        </WhereParameters>
    </asp:LinqDataSource>
</asp:Content>
