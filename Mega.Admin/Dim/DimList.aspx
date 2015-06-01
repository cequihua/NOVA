<%@ Page Title="Lista de Clientes (DIM)" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="DimList.aspx.cs" Inherits="Mega.Admin.Dim.DimList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Administración de Clientes (Dim)
    </h2>
    <div id="dim-list">
        <fieldset>
            <legend>Filtros</legend>
            <div class="doble_column_content">
                <p>
                    <label>
                        Buscar en No. Dim:
                    </label>
                    <asp:TextBox ID="FindByDimTextBox" runat="server" Text="" />
                </p>
                <p>
                    <label>
                        y en Nombre:
                    </label>
                    <asp:TextBox ID="FindByNameTextBox" runat="server" Text="" />
                </p>
            </div>
            <div class="doble_column_content">
                <p>
                    <label>
                        y en Apellido Paterno:
                    </label>
                    <asp:TextBox ID="FindByLastNameTextBox" runat="server" Text="" />
                </p>
                <p>
                    <label>
                        y Apellido Materno:
                    </label>
                    <asp:TextBox ID="FindByMotherMaidenNameTextBox" runat="server" Text="" />
                </p>
                <p>
                </p>
                <p>
                    <label>
                        &nbsp;
                    </label>
                    <asp:Button ID="FilterButton" runat="server" Text="Filtrar" OnClick="FilterButton_Click"
                        CausesValidation="false" />
                </p>
            </div>
        </fieldset>
    </div>
    <p>
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Dim/Dim.aspx">Agregar Cliente</asp:HyperLink>
    </p>
    <div class="table_container">
        <asp:GridView ID="DimsGridView" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            DataKeyNames="Id" AllowPaging="true" PagerSettings-Visible="true"
            OnPageIndexChanging="DimsGridView_PageIndexChanging" OnSorting="DimsGridView_Sorting"
            PagerStyle-CssClass="grid-pager">
            <Columns>
                <asp:HyperLinkField HeaderText="Opciones" Text="Editar" DataNavigateUrlFields="Id"
                    DataNavigateUrlFormatString="~/Dim/Dim.aspx?id={0}" />
                <asp:BoundField DataField="Id" HeaderText="Dim/id" SortExpression="Id">
                    <ControlStyle Width="50px"></ControlStyle>
                    <ItemStyle Width="50px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="AddedDate" HeaderText="Fecha Alta" SortExpression="AddedDate">
                    <ControlStyle Width="100px"></ControlStyle>
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="TypeName" HeaderText="Tipo" SortExpression="TypeName">
                    <ControlStyle Width="70px"></ControlStyle>
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="FullName" HeaderText="Nombre" SortExpression="FullName">
                    <ControlStyle Width="150px"></ControlStyle>
                    <ItemStyle Width="150px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="SexName" HeaderText="Sexo" SortExpression="SexName">
                    <ControlStyle Width="70px"></ControlStyle>
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="CoHolder" HeaderText="Co-titular" SortExpression="CoHolder">
                    <ControlStyle Width="200px"></ControlStyle>
                    <ItemStyle Width="200px"></ItemStyle>
                </asp:BoundField>
                <asp:CheckBoxField DataField="SaleRetention" HeaderText="Retención" ReadOnly="True"
                    SortExpression="SaleRetention" />
                <asp:CheckBoxField DataField="Disabled" HeaderText="Desabilitado" ReadOnly="True"
                    SortExpression="Disabled" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
