<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="reporte_datospersonales.aspx.vb" Inherits="sw_reporte_datospersonales" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


        .style1
        {
            width: 100%;
            padding:0;
            
        }
        .style5
        {
            text-align: justify;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: Black;
            vertical-align: top;
            width: 41px;
        }
        .style3
        {
            text-align: justify;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: Black;
            vertical-align: top;
            width: 857px;
        }
        .style7
        {
            width: 41px;
        }
        .style4
        {
            width: 857px;
        }
        .style6
        {
            width: 100%;
        }
        .style8
        {
            height: 23px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
    <tr>
        <td align="center">
            <img alt="" src="img/hilda/titulos/informaciondeasociados.jpg" /></td>
    </tr>
    <tr>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td class="celdamarco">
            <table class="subtitulo" __designer:mapid="5b6">
                <tr>
                    <td class="style5">
                                &nbsp;</td>
                    <td class="style3">
                                &nbsp;</td>
                </tr>
                <tr>
                    <td class="style5">
                                &nbsp;</td>
                    <td class="style3">
                                INFORMACIÓN DEL ASOCIADO
                            <asp:TextBox ID="asociado" runat="server" Width="350px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="asociado_AutoCompleteExtender" runat="server" 
                    enabled="True" servicepath="~/BuscarAsociados.asmx" minimumprefixlength="2" 
                    servicemethod="ObtListaAsociados" enablecaching="true" targetcontrolid="asociado" 
                    usecontextkey="True" completionsetcount="10"
                    completioninterval="200">
                            </cc1:AutoCompleteExtender>
                            <asp:Button ID="Button2" runat="server" Text="Mostrar" />
                        <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="asociado" 
                            ErrorMessage="Asociado es un campo obligatorio"></asp:RequiredFieldValidator>
                        <asp:Label ID="error" runat="server" CssClass="error"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                                &nbsp;</td>
                    <td class="style4">
                        <table class="style6">
                            <tr>
                                <td>
                                            Nombre:
                                            <asp:Label ID="nombre" runat="server"></asp:Label>
                                </td>
                                <td>
                                            Teléfono Local:
                                            <asp:Label ID="local" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                            Fecha de Nacimiento:
                                            <asp:Label ID="nacimiento" runat="server"></asp:Label>
                                </td>
                                <td>
                                            Teléfono Móvil:
                                            <asp:Label ID="movil" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                            Fecha de Ingreso:
                                            <asp:Label ID="ingreso" runat="server"></asp:Label>
                                </td>
                                <td>
                                            Teléfono Alternativo:
                                            <asp:Label ID="nextel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                            RFC:
                                            <asp:Label ID="rfc" runat="server"></asp:Label>
                                </td>
                                <td>
                                            E-mail:
                                            <asp:Label ID="email" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                            CURP:
                                            <asp:Label ID="curp" runat="server"></asp:Label>
                                </td>
                                <td>
                                            Alias:
                                            <asp:Label ID="alias" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                            Usuario:
                                            <asp:Label ID="usuario" runat="server"></asp:Label>
                                </td>
                                <td>
                                            Tipo de Asociado:
                                            <asp:Label ID="tipo" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                            Contraseña:
                                            <asp:Label ID="password" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="acceso" runat="server" Visible="False"></asp:Label>
                                                               </td>
                            </tr>
                            <tr>
                                <td>
                                            Patrocinador:
                                            <asp:Label ID="patrocinador" runat="server"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <table class="style6">
                            <tr>
                                <td>
                                            &nbsp;</td>
                                <td>
                                            &nbsp;</td>
                                <td>
                                            &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                            &nbsp;</td>
                                <td>
                                            &nbsp;</td>
                                <td>
                                            &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="background-image:url('img/hilda/division.jpg'); background-repeat:repeat-x;" 
                                            colspan="3">
                                &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table class="style6">
                                        <tr>
                                            <td>
                                            Dirección</td>
                                            <td>
                                            Dirección para Envío</td>
                                        </tr>
                                        <tr>
                                            <td>
                                            &nbsp;</td>
                                            <td>
                                            &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style8">
                                            País:
                                                <asp:Label ID="pais" runat="server"></asp:Label>
                                            </td>
                                            <td class="style8">
                                                País:
                                                <asp:Label ID="pais0" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            Calle:
                                                <asp:Label ID="calle" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                            Calle:
                                                <asp:Label ID="calle0" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            Núm. Exterior:
                                                <asp:Label ID="numext" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                            Núm. Exterior:
                                                <asp:Label ID="numext0" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            Núm. Interior:
                                                <asp:Label ID="numint" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                            Núm. Interior:
                                                <asp:Label ID="numint0" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            Colonia:
                                                <asp:Label ID="colonia" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                            Colonia:
                                                <asp:Label ID="colonia0" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            Municipio:
                                                <asp:Label ID="municipio" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                            Municipio:
                                                <asp:Label ID="municipio0" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            Estado:
                                                <asp:Label ID="estado" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                            Estado:
                                                <asp:Label ID="estado0" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            CP:
                                                <asp:Label ID="cp" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                            CP:
                                                <asp:Label ID="cp0" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            &nbsp;</td>
                                            <td>
                                            &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                            &nbsp;</td>
                                            <td>
                                            &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                            Bodega:
                                            <asp:Label ID="bodega" runat="server"></asp:Label>
                                </td>
                                <td>
                                            &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left">
            <img alt="" src="img/hilda/sombra.png" style="width: 323px; height: 80px" /></td>
    </tr>
</table>
</asp:Content>

