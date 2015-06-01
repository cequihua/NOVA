<%@ Page Title="Informe de Error del Sistema" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Mega.Admin.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Informe de Error del Sistema
    </h2>

    <div style="width: 90%;padding-top:6px; padding-left: 8px;">
            <span>El sistema ha detectado un funcionamiento anormal en sus procesos. Lamentamos
                las molestias por este error.
                <br />
                <br />
                Las causas posibles son:
                <br />
                <br />
                - Su sesión ha expirado por estar inactivo más tiempo del permitido: por lo general
                30 minutos.
                <br />
                - Parámetros incorrectos: generalmente debido a intentos de violar la transición
                normal entre páginas.
                <br />
                - Valores no permitidos en los controles de edición: debido a intentos de violar
                la seguridad del sistema.
                <br />
                - Servicio temporalmente innacesible: puede estar dado por congestion temporal en la red,
                en los servicios de acceso a datos, etc.
                <br />
                <br />
                En cualquier caso le sugerimos intentar nuevamente. Si persiste el error y no cree
                que las causas posibles sean las enumeradas arriba le solicitamos que se ponga en
                contacto con nosotros. </span>
            <br />
            <br />
            <asp:UpdatePanel ID="errorDetailUpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <b>
                        <asp:Label ID="Label1" runat="server">Detalle del error:</asp:Label>
                    </b>&nbsp;
                    <div class="separatorBig_div">
                    </div>
                    <asp:Label ID="lblMessage" runat="server" Text="Mensaje: "></asp:Label>
                    <div class="separatorBig_div">
                    </div>
                    <asp:Label ID="lblSource" runat="server" Text="Fuente: "></asp:Label>
                    <div class="separatorBig_div">
                    </div>
                    <asp:Label ID="lblException" runat="server" Text="Excepción: "></asp:Label>
                    <div class="separatorBig_div">
                    </div>
                    <asp:LinkButton ID="LinkDetail" runat="server" Font-Bold="True" OnClick="LinkButton1_Click">Ver más detalle &gt;&gt;</asp:LinkButton>
                    <div class="separatorBig_div">
                    </div>
                    <asp:Label ID="lblMoreDetail" runat="server" Text="Excepción: " Visible="False"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
</asp:Content>
