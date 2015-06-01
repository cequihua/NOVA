<%@ Page Title="Información del Producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Product.aspx.cs" Inherits="Mega.Admin.Product.Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Detalles del Producto
    </h2>
    <p>
        <asp:HyperLink runat="server" ID="ItemList" NavigateUrl="~/Product/ProductList.aspx"
            Text="Regresar a la Lista de Productos" />
    </p>
    <div id="product-details">
        <fieldset>
            <legend>Datos del Producto</legend>
            <div class="doble_column_content">
                <p>
                    <label>
                        No. Producto:
                    </label>
                    <asp:Label ID="IDLabel" runat="server"></asp:Label>
                </p>
                <p>
                    <label>
                        Nombre:
                    </label>
                    <asp:Label ID="NameLabel" runat="server"></asp:Label>
                </p>
                <p>
                    <label>
                        UM:
                    </label>
                    <asp:Label ID="UMLabel" runat="server"></asp:Label>
                </p>
                <p>
                    <label>
                        Precio de Costo:
                    </label>
                    <asp:Label ID="CostPriceLabel" runat="server"></asp:Label>
                </p>
                <p>
                    <label>
                        Categoría 1:
                    </label>
                    <asp:Label ID="Category1Label" runat="server"></asp:Label>
                </p>
                <p>
                    <label>
                        Categoría 2:
                    </label>
                    <asp:Label ID="Category2Label" runat="server"></asp:Label>
                </p>
                <p>
                    <label>
                        Categoría 3:
                    </label>
                    <asp:Label ID="Category3Label" runat="server"></asp:Label>
                </p>
            </div>
            <div class="doble_column_content">
                <p>
                    <label>
                        Tipo:
                    </label>
                    <asp:Label ID="ProductTypeLabel" runat="server"></asp:Label>
                </p>
                <p>
                    <label>
                        Descripción:
                    </label>
                    <asp:Label ID="DescriptionLabel" runat="server"></asp:Label>
                </p>
                <p>
                    <label>
                        Cod. Barra:
                    </label>
                    <asp:Label ID="BarCodeLabel" runat="server"></asp:Label>
                </p>
                <p>
                    <label>
                        Tipo IVA:
                    </label>
                    <asp:Label ID="IVATypeLabel" runat="server"></asp:Label>
                </p>
                <p>
                    <label>
                        Aplica Descuento:
                    </label>
                    <asp:CheckBox ID="ApplyDiscountCheckBox" runat="server" Enabled="false"></asp:CheckBox>
                </p>
                <p>
                    <label>
                        Desabilitado:
                    </label>
                    <asp:CheckBox ID="DisabledCheckBox" runat="server" Enabled="false"></asp:CheckBox>
                </p>
            </div>
        </fieldset>
    </div>
    <asp:Panel ID="CompositionPanel" runat="server">
        <div class="separator">
            <p>
            </p>
        </div>
        <div>
            <fieldset>
                <legend>Productos que lo Componen</legend>
                <div class="table_container">
                    <asp:GridView ID="ProductsGridView" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        DataKeyNames="Id">
                        <Columns>
                            <asp:BoundField DataField="IdProductSimple" HeaderText="No. Producto" SortExpression="IdProductSimple">
                            </asp:BoundField>
                            <asp:BoundField DataField="UMCode" HeaderText="UM" SortExpression="UMCode"></asp:BoundField>
                            <asp:BoundField DataField="ProductName" HeaderText="Nombre" SortExpression="ProductName">
                            </asp:BoundField>
                            <asp:BoundField DataField="Price" HeaderText="Precio" SortExpression="Price">
                            </asp:BoundField>
                            <asp:BoundField DataField="Count" HeaderText="Cantidad" SortExpression="Count"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
    </asp:Panel>
    <div class="separator">
        <p>
        </p>
    </div>
    <div>
        <fieldset>
            <legend>Listado de Precios</legend>
            <div class="table_container">
                <asp:GridView ID="PricesGridView" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    DataKeyNames="Id">
                    <Columns>
                        <asp:BoundField DataField="ClientTypeName" HeaderText="Tipo de Cliente" SortExpression="ClientTypeName">
                            <ControlStyle Width="70px"></ControlStyle>
                            <ItemStyle Width="70px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CurrencyCode" HeaderText="Moneda" SortExpression="CurrencyCode">
                        </asp:BoundField>
                        <asp:BoundField DataField="CompanyName" HeaderText="Compañía" SortExpression="CompanyName">
                        </asp:BoundField>
                        <asp:BoundField DataField="ShopName" HeaderText="Tienda" SortExpression="ShopName">
                        </asp:BoundField>
                        <asp:BoundField DataField="DisabledAsText" HeaderText="Desact." SortExpression="DisabledAsText">
                        </asp:BoundField>
                        <asp:BoundField DataField="PriceTypeName" HeaderText="Tipo Precio" SortExpression="PriceTypeName">
                        </asp:BoundField>
                        <asp:BoundField DataField="InitialDate" HeaderText="Fecha Inicial" SortExpression="InitialDate">
                        </asp:BoundField>
                        <asp:BoundField DataField="FinalDate" HeaderText="Fecha Final" SortExpression="FinalDate">
                        </asp:BoundField>
                        <asp:BoundField DataField="Price" HeaderText="Precio" SortExpression="Price"></asp:BoundField>
                        <asp:BoundField DataField="Points" HeaderText="Puntos" SortExpression="Points"></asp:BoundField>
                        <asp:BoundField DataField="Management" HeaderText="Manejo" SortExpression="Management">
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>
    </div>
    <div class="separator">
    </div>
</asp:Content>
