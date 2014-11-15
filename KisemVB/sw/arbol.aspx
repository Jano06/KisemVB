<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="arbol.aspx.vb" Inherits="sw_arbol" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    .style2
    {
        width: 329px;
    }
    .style3
    {
        width: 534px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="center" class="tablaarbol">
        <tr>
            <td align="center" colspan="32">
                <table class="style1">
                    <tr>
                        <td class="style3">
                                    <img alt="" src="img/hilda/arbol/rangostitulo.png" 
                                        style="width: 321px; height: 34px" /></td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style3">
                                    <img alt="" src="img/hilda/arbol/inactivo.png" />&nbsp;
                                    <img alt="" src="img/hilda/arbol/asociado.png" />&nbsp;
                                    <img alt="" src="img/hilda/arbol/colaborador.png" />&nbsp;
                                    <img alt="" src="img/hilda/arbol/ce.png" />&nbsp;
                            <br />
                                    <img alt="" src="img/hilda/arbol/bronce.png" />&nbsp;
                                    <img alt="" src="img/hilda/arbol/plata.png" />&nbsp;
                                    <img alt="" src="img/hilda/arbol/oro.png" />&nbsp;
                                    <img alt="" src="img/hilda/arbol/diamante.png" />&nbsp;
                                    <img alt="" src="img/hilda/arbol/de.png" />&nbsp;
                                    <img alt="" src="img/hilda/arbol/di.png" style="width: 43px; height: 46px" /></td>
                        <td>
                                    <asp:ImageButton ID="btn_inicio" runat="server" 
                                        ImageUrl="~/sw/img/hilda/arbol/inicio.png" />
                                    <asp:ImageButton ID="Subir1" runat="server" ImageUrl="~/sw/img/hilda/arbol/subir1.png" 
                    ToolTip="Subir un Nivel" />
&nbsp;<asp:ImageButton ID="Subir2" runat="server" ImageUrl="~/sw/img/hilda/arbol/subir2.png" 
                    ToolTip="Subir Dos Niveles" />
&nbsp;<asp:ImageButton ID="Subir3" runat="server" ImageUrl="~/sw/img/hilda/arbol/subir3.png" 
                    ToolTip="Subir Tres Niveles" />
&nbsp;<asp:ImageButton ID="Subir4" runat="server" ImageUrl="~/sw/img/hilda/arbol/subir4.png" 
                    ToolTip="Subir Cuatro Niveles" />
&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                <span class="textonuevo">Número de Asociado</span><asp:TextBox ID="asociado" 
                    runat="server" ValidationGroup="buscar"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="asociado_FilteredTextBoxExtender" 
                    runat="server" Enabled="True" FilterType="Numbers" TargetControlID="asociado">
                </cc1:FilteredTextBoxExtender>
                <asp:Button ID="Button1" runat="server" Text="Buscar" 
                    ValidationGroup="buscar" />
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="asociado" ErrorMessage="Dato Necesario" 
                    ValidationGroup="buscar"></asp:RequiredFieldValidator>
                <asp:Label ID="error" runat="server" CssClass="error"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="16">
                &nbsp;</td>
            <td align="left" colspan="16">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="32" 
                style="background-image:url('img/arbol/r1.png'); background-position:center; background-position:bottom; background-repeat:no-repeat;">
                <table class="style1">
                    <tr>
                        <td align="left" valign="bottom">
                                    <asp:ImageButton ID="ImageButton33" runat="server" 
                                ImageUrl="~/sw/img/hilda/arbol/fondoizq.png" 
                                        ToolTip="Ir al Fondo de la Izquierda" />
                        </td>
                        <td align="center">
                <asp:ImageButton ID="ImageButton1" runat="server" 
                    ImageUrl="~/sw/img/rangos/1.png" />
                        </td>
                        <td align="right" valign="bottom">
                                    <asp:ImageButton ID="ImageButton32" runat="server" 
                                ImageUrl="~/sw/img/hilda/arbol/fondoder.png" ToolTip="Ir al Fondo de la Derecha" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="16" 
                style="background-image:url('img/arbol/r2.png'); background-position:center; background-position:bottom; background-repeat:no-repeat;">
                <asp:ImageButton ID="ImageButton2" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td align="center" colspan="16" 
                style="background-image:url('img/arbol/r2.png'); background-position:center; background-position:bottom; background-repeat:no-repeat;">
                <asp:ImageButton ID="ImageButton3" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
        </tr>
        <tr align="center">
            <td align="center" colspan="8" 
                style="background-image:url('img/arbol/r3.png'); background-position:center; background-position:bottom; background-repeat:no-repeat;">
                <asp:ImageButton ID="ImageButton4" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="8" 
                style="background-image:url('img/arbol/r3.png'); background-position:center; background-position:bottom; background-repeat:no-repeat;">
                <asp:ImageButton ID="ImageButton5" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="8" 
                style="background-image:url('img/arbol/r3.png'); background-position:center; background-position:bottom; background-repeat:no-repeat;">
                <asp:ImageButton ID="ImageButton6" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="8" 
                style="background-image:url('img/arbol/r3.png'); background-position:center; background-position:bottom; background-repeat:no-repeat;">
                <asp:ImageButton ID="ImageButton7" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
        </tr>
        <tr align="center">
            <td colspan="4">
                <asp:ImageButton ID="ImageButton8" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="4">
                <asp:ImageButton ID="ImageButton9" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="4">
                <asp:ImageButton ID="ImageButton10" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="4">
                <asp:ImageButton ID="ImageButton11" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="4">
                <asp:ImageButton ID="ImageButton12" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="4">
                <asp:ImageButton ID="ImageButton13" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="4">
                <asp:ImageButton ID="ImageButton14" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="4">
                <asp:ImageButton ID="ImageButton15" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
        </tr>
        <tr align="center">
            <td colspan="2">
                <asp:ImageButton ID="ImageButton16" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton17" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton18" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton19" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton20" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton21" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton22" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton23" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton24" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton25" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton26" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton27" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton28" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton29" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton30" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton31" runat="server" 
                    ImageUrl="~/sw/img/rangos/0.png" />
            </td>
        </tr>
        <tr align="center">
            <td>
                <asp:ImageButton ID="I1" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
            </td>
            <td>
                <asp:ImageButton ID="D1" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
            </td>
            <td>
                <asp:ImageButton ID="I2" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
            </td>
            <td>
                <asp:ImageButton ID="D2" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
            </td>
            <td>
                <asp:ImageButton ID="I3" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
            </td>
            <td>
                <asp:ImageButton ID="D3" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
            </td>
            <td>
                <asp:ImageButton ID="I4" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
            </td>
            <td>
                <asp:ImageButton ID="D4" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
            </td>
            <td>
                <asp:ImageButton ID="I5" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
            </td>
            <td>
                <asp:ImageButton ID="D5" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
            </td>
            <td>
                <asp:ImageButton ID="I6" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
            </td>
            <td>
                <asp:ImageButton ID="D6" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
            </td>
            <td>
                <asp:ImageButton ID="I7" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
            </td>
            <td>
                <asp:ImageButton ID="D7" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
            </td>
            <td>
                <asp:ImageButton ID="I8" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
            </td>
            <td class="style2">
                <asp:ImageButton ID="D8" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
            </td>
            <td>
                <asp:ImageButton ID="I9" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
            </td>
            <td>
                <asp:ImageButton ID="D9" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
            </td>
            <td>
                <asp:ImageButton ID="I10" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
            </td>
            <td>
                <asp:ImageButton ID="D10" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
            </td>
            <td>
                <asp:ImageButton ID="I11" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
            </td>
            <td>
                <asp:ImageButton ID="D11" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
            </td>
            <td>
                <asp:ImageButton ID="I12" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
            </td>
            <td>
                <asp:ImageButton ID="D12" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
            </td>
            <td>
                <asp:ImageButton ID="I13" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
            </td>
            <td>
                <asp:ImageButton ID="D13" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
            </td>
            <td>
                <asp:ImageButton ID="I14" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
            </td>
            <td>
                <asp:ImageButton ID="D14" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
            </td>
            <td>
                <asp:ImageButton ID="I15" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
            </td>
            <td>
                <asp:ImageButton ID="D15" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
            </td>
            <td>
                <asp:ImageButton ID="I16" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
            </td>
            <td>
                <asp:ImageButton ID="D16" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
            </td>
        </tr>
    </table>
    <br />
    <br />
</asp:Content>

