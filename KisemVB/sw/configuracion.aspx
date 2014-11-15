<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="configuracion.aspx.vb" Inherits="sw_configuracion" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">




        .style8
        {
            width: 208px;
        }
        .style9
        {
            width: 414px;
        }
        .style10
        {
            text-align: justify;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: Black;
            vertical-align: top;
            width: 414px;
        }
        .style11
        {
            width: 455px;
        }
        .style12
        {
            text-align: right;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: Gray;
            vertical-align: top;
            width: 455px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
    <tr>
        <td class="titulobienvenida" colspan="3">
            Configuración del Sistema<br />
            <asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
    </tr>
    <tr>
        <td class="style11">
            <asp:Label ID="Label1" runat="server" Width="200px"></asp:Label>
        </td>
        <td class="style8">
                <asp:Label ID="Label6" runat="server" Width="200px"></asp:Label>
        </td>
        <td class="style9">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style12">
            Costo Envío:</td>
        <td class="style8">
            <asp:TextBox ID="envio" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="envio_FilteredTextBoxExtender" runat="server" 
                Enabled="True" FilterType="Custom, Numbers" TargetControlID="envio" 
                ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="envio" EnableClientScript="true" 
                    ErrorMessage="Costo Envío es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Porcentaje de Balance:</td>
        <td class="style8">
            <asp:TextBox ID="balance" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="balance_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="balance" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="balance" EnableClientScript="true" 
                    ErrorMessage="Porcentaje de Balance es un campo obligatorio" 
                Width="336px"></asp:RequiredFieldValidator>
        </td>
    </tr>
        <tr>
        <td class="style12">
            Porcentaje de Balance Colaborador:</td>
        <td class="style8">
            <asp:TextBox ID="balancecolaborador" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="balancecolaborador_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="balancecolaborador" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                    ControlToValidate="balancecolaborador" EnableClientScript="true" 
                    ErrorMessage="Porcentaje de Balance es un campo obligatorio" 
                Width="336px"></asp:RequiredFieldValidator>
        </td>
                                   </tr>
    <tr>
        <td class="style12">
            Puntos para Colaborador</td>
        <td class="style8">
            <asp:TextBox ID="puntosrango2" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="puntosrango2_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="puntosrango2" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                    ControlToValidate="puntosrango2" EnableClientScript="true" 
                    ErrorMessage="Puntos es un campo obligatorio" 
                Width="349px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Puntos para Colaborador Ejecutivo</td>
        <td class="style8">
            <asp:TextBox ID="puntosrango3" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="puntosrango3_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="puntosrango3" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                    ControlToValidate="puntosrango3" EnableClientScript="true" 
                    ErrorMessage="Puntos es un campo obligatorio" 
                Width="349px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Puntos para Bronce</td>
        <td class="style8">
            <asp:TextBox ID="puntosrango4" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="puntosrango4_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="puntosrango4" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                    ControlToValidate="puntosrango4" EnableClientScript="true" 
                    ErrorMessage="Puntos es un campo obligatorio" 
                Width="349px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Puntos para Plata</td>
        <td class="style8">
            <asp:TextBox ID="puntosrango5" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="puntosrango5_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="puntosrango5" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                    ControlToValidate="puntosrango5" EnableClientScript="true" 
                    ErrorMessage="Puntos es un campo obligatorio" 
                Width="349px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Puntos para Oro</td>
        <td class="style8">
            <asp:TextBox ID="puntosrango6" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="puntosrango6_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="puntosrango6" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                    ControlToValidate="puntosrango6" EnableClientScript="true" 
                    ErrorMessage="Puntos es un campo obligatorio" 
                Width="349px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Puntos para Diamante</td>
        <td class="style8">
            <asp:TextBox ID="puntosrango7" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="puntosrango7_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="puntosrango7" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                    ControlToValidate="puntosrango7" EnableClientScript="true" 
                    ErrorMessage="Puntos es un campo obligatorio" 
                Width="349px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Puntos para Diamante Ejecutivo</td>
        <td class="style8">
            <asp:TextBox ID="puntosrango8" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="puntosrango8_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="puntosrango8" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                    ControlToValidate="puntosrango8" EnableClientScript="true" 
                    ErrorMessage="Puntos es un campo obligatorio" 
                Width="349px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Puntos para Diamante Internacional</td>
        <td class="style8">
            <asp:TextBox ID="puntosrango9" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="puntosrango9_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="puntosrango9" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                    ControlToValidate="puntosrango9" EnableClientScript="true" 
                    ErrorMessage="Puntos es un campo obligatorio" 
                Width="349px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Porcentaje Bono 5</td>
        <td class="style8">
            <asp:TextBox ID="porcentajebono5" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="porcentajebono5_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="porcentajebono5" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
            %</td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                    ControlToValidate="porcentajebono5" EnableClientScript="true" 
                    ErrorMessage="Porcentaje es un campo obligatorio" 
                Width="349px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Pago Mínimo Bono 6, 7 y 8</td>
        <td class="style8">
            <asp:TextBox ID="pagominimo" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="pagominimo_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="pagominimo" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                    ControlToValidate="pagominimo" EnableClientScript="true" 
                    ErrorMessage="Pago mínimo es un campo obligatorio" 
                Width="349px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Pago 1 Bono 6</td>
        <td class="style8">
            <asp:TextBox ID="pago1bono6" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="pago1bono6_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="pago1bono6" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="pago1bono6" EnableClientScript="true" 
                    ErrorMessage="Pago 1 Bono 6 es un campo obligatorio" Width="336px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Pago 2 Bono 6</td>
        <td class="style8">
            <asp:TextBox ID="pago2bono6" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="pago2bono6_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="pago2bono6" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="pago2bono6" EnableClientScript="true" 
                    ErrorMessage="Pago 2 Bono 6 es un campo obligatorio" Width="336px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Pago 1 Bono 7</td>
        <td class="style8">
            <asp:TextBox ID="pago1bono7" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="pago1bono7_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="pago1bono7" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="pago1bono7" EnableClientScript="true" 
                    ErrorMessage="Pago 1 Bono 7 es un campo obligatorio" Width="336px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Pago 2 Bono 7</td>
        <td class="style8">
            <asp:TextBox ID="pago2bono7" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="pago2bono7_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="pago2bono7" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="pago2bono7" EnableClientScript="true" 
                    ErrorMessage="Pago 2 Bono 7 es un campo obligatorio" Width="336px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Pago 1 Bono 8</td>
        <td class="style8">
            <asp:TextBox ID="pago1bono8" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="pago1bono8_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="pago1bono8" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="pago1bono8" EnableClientScript="true" 
                    ErrorMessage="Pago 1 Bono 8 es un campo obligatorio" Width="336px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Pago 2 Bono 8</td>
        <td class="style8">
            <asp:TextBox ID="pago2bono8" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="pago2bono8_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="pago2bono8" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                    ControlToValidate="pago2bono8" EnableClientScript="true" 
                    ErrorMessage="Pago 2 Bono 8 es un campo obligatorio" Width="336px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Bono10 Rango Plata</td>
        <td class="style8">
            <asp:TextBox ID="bono10rango5" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="bono10rango5_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="bono10rango5" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
                    ControlToValidate="bono10rango5" EnableClientScript="true" 
                    ErrorMessage="Bono 10 Plata es un campo obligatorio" Width="336px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Bono10 Rango Oro</td>
        <td class="style8">
            <asp:TextBox ID="bono10rango6" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="bono10rango6_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="bono10rango6" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" 
                    ControlToValidate="bono10rango6" EnableClientScript="true" 
                    ErrorMessage="Bono 10 Oro es un campo obligatorio" Width="336px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Bono10 Rango Diamante</td>
        <td class="style8">
            <asp:TextBox ID="bono10rango7" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="bono10rango7_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="bono10rango7" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" 
                    ControlToValidate="bono10rango7" EnableClientScript="true" 
                    ErrorMessage="Bono 10 Diamante es un campo obligatorio" Width="336px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Bono10 Rango Diamante Ejecutivo</td>
        <td class="style8">
            <asp:TextBox ID="bono10rango8" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="bono10rango8_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="bono10rango8" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" 
                    ControlToValidate="bono10rango8" EnableClientScript="true" 
                    ErrorMessage="Bono 10 Diamante Ejecutivo es un campo obligatorio" 
                Width="336px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Bono10 Rango Diamante Internacional</td>
        <td class="style8">
            <asp:TextBox ID="bono10rango9" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="bono10rango9_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                TargetControlID="bono10rango9" ValidChars=".">
            </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" 
                    ControlToValidate="bono10rango9" EnableClientScript="true" 
                    ErrorMessage="Bono 10 Diamante Internacional es un campo obligatorio" 
                Width="336px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Mensaje de compra</td>
        <td colspan="2">
            <asp:TextBox ID="mensajedecompra" runat="server" Height="101px" 
                TextMode="MultiLine" Width="487px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style12">
            Mensaje de Bienvenida</td>
        <td colspan="2">
            <asp:TextBox ID="mensajedebienvenida" runat="server" Height="101px" 
                TextMode="MultiLine" Width="487px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style11">
            <asp:Label ID="Label5" runat="server" Width="300px"></asp:Label>
        </td>
        <td colspan="2">
            <asp:Button ID="Button1" runat="server" TabIndex="30" Text="Guardar" />
&nbsp;&nbsp;&nbsp;<asp:Button ID="Button2" runat="server" Text="Cancelar" 
                UseSubmitBehavior="False" CausesValidation="False" />
        </td>
    </tr>
    <tr>
        <td class="subtitulo" colspan="2">
                &nbsp;</td>
        <td class="style10">
                &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style11">
                &nbsp;</td>
        <td class="style8">
                &nbsp;</td>
        <td class="style9">
                &nbsp;</td>
    </tr>
</table>
</asp:Content>

