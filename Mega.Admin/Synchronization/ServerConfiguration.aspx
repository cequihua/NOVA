<%@ Page Title="Modificar Configuracion de Sincronización Web" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ServerConfiguration.aspx.cs" Inherits="Mega.Admin.Synchronization.ServerConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Modificar Configuracion de Sincronización Web
    </h2>
    <p>
    </p>
    <fieldset>
        <legend>Valores de Configuración de la Sincronización de la Web</legend>
        <div class="doble_column_content">
            <p style="display: inline;">
                <label>
                    Días de Importación:
                </label>
                <asp:CheckBoxList ID="DaysInCheckBoxList" runat="server" RepeatDirection="Horizontal"
                    RepeatColumns="3">
                    <asp:ListItem Value="0">Domingo</asp:ListItem>
                    <asp:ListItem Value="1">Lunes</asp:ListItem>
                    <asp:ListItem Value="2">Martes</asp:ListItem>
                    <asp:ListItem Value="3">Miércoles</asp:ListItem>
                    <asp:ListItem Value="4">Jueves</asp:ListItem>
                    <asp:ListItem Value="5">Viernes</asp:ListItem>
                    <asp:ListItem Value="6">Sábado</asp:ListItem>
                </asp:CheckBoxList>
            </p>
            <p>
                <label>
                    Horas de Importación:
                </label>
                <asp:TextBox ID="HoursPlanInTextBox" runat="server" MaxLength="500"></asp:TextBox>
                <asp:RequiredFieldValidator ID="HoursPlanInRequiredFieldValidator" runat="server"
                    ControlToValidate="HoursPlanInTextBox" ErrorMessage="El campo Horas de Importación es Requerido"
                    Display="None" />
            </p>
        </div>
        <div class="doble_column_content">
            <p style="display: inline;">
                <label>
                    Días de Exportación:
                </label>
                <asp:CheckBoxList ID="DaysOutCheckBoxList" runat="server" RepeatDirection="Horizontal"
                    RepeatColumns="3">
                    <asp:ListItem Value="0">Lunes</asp:ListItem>
                    <asp:ListItem Value="1">Martes</asp:ListItem>
                    <asp:ListItem Value="2">Miércoles</asp:ListItem>
                    <asp:ListItem Value="3">Jueves</asp:ListItem>
                    <asp:ListItem Value="4">Viernes</asp:ListItem>
                    <asp:ListItem Value="5">Sábado</asp:ListItem>
                    <asp:ListItem Value="6">Domingo</asp:ListItem>
                </asp:CheckBoxList>
            </p>
            <p>
                <label>
                    Horas de Exportación:
                </label>
                <asp:TextBox ID="HoursPlanOutTextBox" runat="server" MaxLength="500"></asp:TextBox>
                <asp:RequiredFieldValidator ID="HoursPlanOutRequiredFieldValidator" runat="server"
                    ControlToValidate="HoursPlanOutTextBox" ErrorMessage="El campo Horas de Exportación es Requerido"
                    Display="None" />
                <p>
                    <label>
                        &nbsp;
                    </label>
                </p>
                <p>
                    <label>
                        &nbsp;
                    </label>
                    <asp:Button ID="SaveButton" runat="server" Text="Guardar" OnClick="SaveButton_Click"
                        CausesValidation="true" />
                </p>
            </p>
        </div>
    </fieldset>
    <asp:CustomValidator ID="GeneralCustomValidator" runat="server" ErrorMessage="" Display="None"
        OnServerValidate="GeneralCustomValidator_OnServerValidate"></asp:CustomValidator>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" class="error_msg" ShowMessageBox="true"
        ShowSummary="false" />
</asp:Content>
