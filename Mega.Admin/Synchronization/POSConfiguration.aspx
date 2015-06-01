<%@ Page Title="Configuración de Sincronización Tiendas" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="POSConfiguration.aspx.cs" Inherits="Mega.Admin.Synchronization.POSConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Configuración de Sincronización Tiendas
    </h2>
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
    </p>
    <div class="table_container">
        <asp:Panel ID="AddConfigurationsPanel" runat="server" Visible="false" CssClass="AddConfigurationsPanel">
            <asp:Button ID="AddConfigurationsButton" runat="server" Text="Insertar Tiendas que faltan en la sincronización"
                OnClick="AddConfigurationsButton_Click" />
        </asp:Panel>
    </div>
    <div class="table_container">
        <asp:GridView ID="POSConfigurationsGridView" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            DataSourceID="SincronizationsLinqDataSource" DataKeyNames="IdShop" EmptyDataText="No existen configuraciones registradas"
            EmptyDataRowStyle-CssClass="grid-empty-data" AllowPaging="true" PagerStyle-CssClass="grid-pager">
            <Columns>
                <asp:HyperLinkField HeaderText="Opciones" Text="Editar" DataNavigateUrlFields="IdShop"
                    DataNavigateUrlFormatString="~/Synchronization/POSConfigurationEdit.aspx?id={0}" />
                <asp:BoundField DataField="IdShop" HeaderText="Id Tienda" SortExpression="IdShop"></asp:BoundField>
                <asp:BoundField DataField="ShopName" HeaderText="Tienda" SortExpression="">
                    <ControlStyle Width="200px"></ControlStyle>
                    <ItemStyle Width="200px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="DaysPlanInFromated" HeaderText="Días de Importación" SortExpression="">
                    <ControlStyle Width="200px"></ControlStyle>
                    <ItemStyle Width="200px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="HoursPlanIn" HeaderText="Horas de Importación" SortExpression="">
                    <ControlStyle Width="100px"></ControlStyle>
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="DaysPlanOutFromated" HeaderText="Días de Exportación"
                    SortExpression="">
                    <ControlStyle Width="200px"></ControlStyle>
                    <ItemStyle Width="200px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="HoursPlanOut" HeaderText="Horas de Exportación" SortExpression="">
                    <ControlStyle Width="100px"></ControlStyle>
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    <p>
    </p>
    <asp:LinqDataSource ID="SincronizationsLinqDataSource" runat="server" ContextTypeName="Mega.Common.AdminDataContext"
        EnableInsert="False" EnableUpdate="True" TableName="Synchronizations" OnContextCreating="LinqDataSource1_OnContextCreating"
        Where="Shop.Name.Contains(@Filter) || IdShop.Contains(@Filter)" OrderBy="Shop.Name">
        <WhereParameters>
            <asp:ControlParameter ControlID="IDOrNameTextBox" PropertyName="Text" Name="Filter"
                ConvertEmptyStringToNull="false" />
        </WhereParameters>
    </asp:LinqDataSource>
</asp:Content>
