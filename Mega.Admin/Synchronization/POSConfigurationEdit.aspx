<%@ Page Title="Modificar Configuración de Sincronización de Tienda" Language="C#"
    MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="POSConfigurationEdit.aspx.cs"
    Inherits="Mega.Admin.Synchronization.POSConfigurationEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.timepicker.js" type="text/javascript"></script>
    <link href="../Styles/css/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="DateFormat" runat="server" />
    <h2>
        Modificar Configuración de Sincronización de Tienda
    </h2>
    <p>
        <asp:HyperLink runat="server" ID="ItemList" NavigateUrl="~/Synchronization/POSConfiguration.aspx"
            Text="Regresar a la Lista de Configuraciones" />
    </p>
    <div>
        <fieldset>
            <legend>Datos de la Tienda</legend>
            <div class="doble_column_content">
                <label>
                    Id:
                </label>
                <asp:Label ID="IdLabel" runat="server" Text=""></asp:Label>
            </div>
            <div class="doble_column_content">
                <label>
                    Nombre:
                </label>
                <asp:Label ID="NameLabel" runat="server" Text=""></asp:Label>
            </div>
            <p></p>
        </fieldset>
    </div>
    <p>
    </p>
    <div>
        <fieldset>
            <legend>Modificar Configuración</legend>
            <div class="doble_column_content">
                <p>
                    <b>Días y Horas en que la tienda tiene planificado Importar Información</b></p>
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
                <p>
                    <label>
                        Notas de la última Importación:
                    </label>
                    <asp:TextBox ID="LastNotesInTextBox" runat="server" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                </p>
                <p>
                </p>
                <p>
                    La próxima salva del WEB hacia esta tienda, tendrá fecha INICIAL del <b>
                        <asp:Literal ID="InitalDateInLiteral" runat="server" /></b>
                </p>
                <p>
                    Si desea enviar una actualización hacia la tienda con una fecha inicial anterior
                    usted puede indicarla a continuación y Guardar sus cambios. La proxima actualización
                    de la Web hacia la tienda enviará todo lo modificado desde la fecha que usted indique
                    a continuación.</p>
                <p>
                    <label>
                        <asp:Label ID="LastInitialDateInLabel" runat="server" Text=""></asp:Label>
                    </label>
                    <asp:TextBox ID="LastInitialDateInTextBox" runat="server"></asp:TextBox>
                </p>
            </div>
            <div class="doble_column_content">
                <p>
                    <b>Días y Horas en que la tienda tiene planificado Exportar Información</b></p>
                <p style="display: inline;">
                    <label>
                        Días de Exportación:
                    </label>
                    <asp:CheckBoxList ID="DaysOutCheckBoxList" runat="server" RepeatDirection="Horizontal"
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
                        Horas de Exportación:
                    </label>
                    <asp:TextBox ID="HoursPlanOutTextBox" runat="server" MaxLength="500"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="HoursPlanOutRequiredFieldValidator" runat="server"
                        ControlToValidate="HoursPlanOutTextBox" ErrorMessage="El campo Horas de Exportación es Requerido"
                        Display="None" />
                </p>
                <p>
                    <label>
                        Notas de la última Exportación:
                    </label>
                    <asp:TextBox ID="LastNotesOutTextBox" runat="server" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                </p>
                <p>
                </p>
                <p>
                    La última Expotación de esta tienda, hacia la Web, abarcó el periodo desde <b>
                        <asp:Literal ID="InitalDateOutLiteral" runat="server" /></b> &nbsp;hasta <b>
                            <asp:Literal ID="FinalDateOutLiteral" runat="server" /></b>
                </p>
                <p>
                    Si desea que la tienda envíe una Exportación de información con una fecha anterior
                    a la última usted puede indicarla a continuación y Guardar sus cambios. La proxima
                    exportación de la Tienda hacia la Web enviará todo lo modificado desde la fecha
                    que usted indique a continuación (Primero deberá tener lugar una Importación desde
                    la tienda para que reciba esta fecha).</p>
                <p>
                    <label>
                        <asp:Label ID="LastFinalDateOutLabel" runat="server" Text=""></asp:Label>
                    </label>
                    <asp:TextBox ID="LastFinalDateOutTextBox" runat="server" MaxLength="500"></asp:TextBox>
                </p>
                <p>
                </p>
            </div>
            <p>
                Es válido aclarar que el sistema está preparado para enviar la información cuantas
                veces se necesite (en ambos sentidos) sin temor a inconsistencias de datos.
            </p>
            <p>
                <asp:Button ID="SaveButton" runat="server" Text="Guardar" OnClick="SaveButton_Click"
                    CausesValidation="true" />
            </p>
        </fieldset>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:CustomValidator ID="GeneralCustomValidator" runat="server" ErrorMessage="" Display="None"
        OnServerValidate="GeneralCustomValidator_OnServerValidate"></asp:CustomValidator>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#MainContent_LastInitialDateInTextBox").datepicker({ duration: '', showTime: true, constrainInput: false, changeMonth: true, changeYear: true, dateFormat: $('#MainContent_DateFormat').val() });
            $("#MainContent_LastFinalDateOutTextBox").datepicker({ duration: '', showTime: true, constrainInput: false, changeMonth: true, changeYear: true, dateFormat: $('#MainContent_DateFormat').val() });
        });
    </script>
</asp:Content>
