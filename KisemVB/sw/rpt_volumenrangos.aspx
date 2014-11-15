<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="rpt_volumenrangos.aspx.vb" Inherits="sw_rpt_volumenrangos" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        



        .style8
        {
            width: 147px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
    <tr>
        <td class="titulobienvenida" colspan="2">
            Reporte de Volumen para Rangos<br />
            <asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
                &nbsp;&nbsp;&nbsp;
                </td>
    </tr>
    <tr>
        <td colspan="2">
                                                <span  class="texto" __designer:mapid="5">De
                                            <asp:TextBox ID="de" runat="server" 
                MaxLength="50" TabIndex="4" 
                    Width="150px" 
    ValidationGroup="pago"></asp:TextBox>
                                            <cc1:CalendarExtender ID="TextBox26_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="de" Format="yyyy/M/dd">
                    </cc1:CalendarExtender>
                                            &nbsp;a&nbsp;
                                            <asp:TextBox ID="a" runat="server" 
                MaxLength="50" TabIndex="4" 
                    Width="150px" 
    ValidationGroup="pago"></asp:TextBox> 
                                                <cc1:CalendarExtender ID="TextBox26_CalendarExtender0" runat="server" 
                    Enabled="True" TargetControlID="a" Format="yyyy/M/dd">
                    </cc1:CalendarExtender></span>
        </td>
    </tr>
    <tr>
        <td colspan="2">
&nbsp;&nbsp;Asociado
                            <asp:TextBox ID="asociado" runat="server" Width="350px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="asociado_AutoCompleteExtender" runat="server" 
                    enabled="True" servicepath="~/BuscarAsociados.asmx" minimumprefixlength="2" 
                    servicemethod="ObtListaAsociados" enablecaching="true" targetcontrolid="asociado" 
                    usecontextkey="True" completionsetcount="10"
                    completioninterval="200">
                            </cc1:AutoCompleteExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                runat="server" ControlToValidate="asociado" ErrorMessage="Dato Necesario" 
                ValidationGroup="asociado"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2">
                                                <span  class="texto" __designer:mapid="5">
            <asp:Button ID="Button2" runat="server" TabIndex="30" 
    Text="Generar Reporte" ValidationGroup="asociado" />
                                            </span>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style8" valign="top">
            Volumen Izquierdo
            <br />
            <asp:Label ID="volizquierdo" runat="server"></asp:Label>
        </td>
        <td class="style8" valign="top">
                                            Volumen Derecho
                                            <br />
                                            <asp:Label ID="volderecho" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style8" colspan="2">
                &nbsp;</td>
    </tr>
    <tr>
        <td class="style8" colspan="2">
                &nbsp;</td>
    </tr>
    <tr>
        <td class="style8" colspan="2">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style8" colspan="2">
                &nbsp;</td>
    </tr>
</table>
</asp:Content>

