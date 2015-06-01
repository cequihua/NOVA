<%@ Page Title="Histórico de Sincronización" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Logs.aspx.cs" Inherits="Mega.Admin.Synchronization.Logs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Histórico de Sincronización
    </h2>
    <p>
    </p>
    <div id="product-list">
        <fieldset>
            <legend>Filtros</legend>
            <div class="doble_column_content">
                <p>
                    <label>
                        Solo para la Tienda:
                    </label>
                    <asp:DropDownList ID="ShopsDropDownList" runat="server" DataTextField="Name" DataValueField="Id">
                    </asp:DropDownList>
                </p>
            </div>
            <div class="doble_column_content">
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
    </p>
    <div class="table_container">
        <asp:GridView ID="LogsGridView" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            DataKeyNames="Id" AllowPaging="true" PagerSettings-Visible="true" OnSorting="LogsGridView_Sorting"
            OnPageIndexChanging="LogsGridView_PageIndexChanging" PagerStyle-CssClass="grid-pager"  EmptyDataRowStyle-CssClass="grid-empty-data" EmptyDataText="No existe Histórico de Sincronización.">
            <Columns>
                <asp:BoundField DataField="ShopName" HeaderText="Tienda" SortExpression="ShopName">
                    <ControlStyle Width="200px"></ControlStyle>
                    <ItemStyle Width="200px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="InitialDate" HeaderText="Fecha Inicial" SortExpression="InitialDate">
                    <ControlStyle Width="145px"></ControlStyle>
                    <ItemStyle Width="145px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="FinalDate" HeaderText="Fecha Final" SortExpression="FinalDate">
                    <ControlStyle Width="145px"></ControlStyle>
                    <ItemStyle Width="145px"></ItemStyle>
                </asp:BoundField>
                <asp:CheckBoxField DataField="IsExportation" HeaderText="Es Exportación" ReadOnly="True"
                    SortExpression="IsExportation" />
                <asp:CheckBoxField DataField="IsOk" HeaderText="Fue Satisfactorio" ReadOnly="True"
                    SortExpression="IsOk" />
                <asp:BoundField DataField="Notes" HeaderText="Notas" SortExpression="">
                    <ControlStyle Width="250px"></ControlStyle>
                    <ItemStyle Width="250px"></ItemStyle>
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
