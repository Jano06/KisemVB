<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="sw_Default" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style3
        {
            width: 100%;
        }
        .globo
        {
            width: 682px;
            height: 340px;
            background-image: url('img/globo.png');
            background-repeat:no-repeat;
            padding-top: 35px;
            padding-left: 35px;
            }
        .style4
        {
            width: 69px;
        }
        .style5
        {
            width: 620px;
            border: 1px;
        }
        </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style3">
        <tr>
            <td class="style4" rowspan="2">
                                                        <asp:Image ID="rango" runat="server" />
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                <asp:Label ID="nombreasociado" runat="server" CssClass="titulobienvenida"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblnombrerango" runat="server" CssClass="rangobienvenida"></asp:Label>
&nbsp;<asp:Label ID="estado" runat="server" Width="120px" CssClass="rangobienvenida"></asp:Label>
                <asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style4">
                                                        &nbsp;</td>
                                                    <td class="globo" align="left" valign="top">
                                                        <table class="style5">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Width="310px"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Width="310px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td  class="textonuevo">
                            Fecha de próxima activación</td>
                                                                <td class="textonuevo2">
                            del
                            <asp:Label ID="iniciocompra" runat="server"></asp:Label>
&nbsp;al
                            <asp:Label ID="fincompra" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textonuevo">
                                                                    Mi Organización</td>
                                                                <td class="textonuevo2">
                            <asp:Label ID="activos" runat="server"></asp:Label>
&nbsp;activos -&nbsp;
                            <asp:Label ID="inactivos" runat="server"></asp:Label>
&nbsp;inactivos</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textonuevo">
                                                                    &nbsp;</td>
                                                                <td class="textonuevo2">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="2">
                                                                    <span class="textonuevo">Mi Volumen</span><br />
                                                                    <img alt="" src="img/arbolito.png"  /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textonuevo">
                            <asp:Label ID="izquierdo" runat="server"></asp:Label>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                                                <td>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="derecho" runat="server" CssClass="textonuevo"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td align="left" valign="top">
                            <asp:Label ID="titulo" runat="server" CssClass="subtitulo"></asp:Label>
                                                        <br />
                            <asp:Label ID="texto" runat="server" CssClass="texto"></asp:Label>
                                                                </td>
                                                </tr>
                                            </table>
    </asp:Content>

