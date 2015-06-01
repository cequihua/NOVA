<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="Mega.Admin.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Acerca de MegaPos
    </h2>
    <p>
        MegaPOS en el Sistema para Puntos de Ventas de MegaHealth. 
    </p>
     <p>
        <asp:Label ID="VersionStatusLabel" runat="server" Text=""></asp:Label>
    </p>
    <p></p>
</asp:Content>
