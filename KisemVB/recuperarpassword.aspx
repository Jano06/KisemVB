<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="recuperarpassword.aspx.vb" Inherits="recuperarpassword" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


        .style3
        {
            width: 100%;
        }
        .style4
        {
            width: 410px;
        }
        .globo
        {
            width: 682px;
            height: 340px;
            background-image: url('sw/img/globo.png');
            background-repeat:no-repeat;
            padding-top: 35px;
            padding-left: 35px;
            }
        .style10
        {
            width: 418px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style3">
    <tr>
        <td class="style4">
                                                        &nbsp;</td>
        <td align="left" valign="top" class="style10">
            <br />
            <br />
        </td>
        <td align="left" valign="top">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style4">
                                                        &nbsp;</td>
        <td align="left" valign="top" class="style10" style="background-image:url('sw/img/fondousuarioypassword.jpg'); background-repeat:no-repeat; background-position:center;">
            <table>
                <tr>
                    <td class="titulobienvenida" colspan="2">
                        Recuperar contraseña</td>
                    <td rowspan="6" valign="top">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  class="ingreso">
                                                                    Número de Asociado:</td>
                    <td class="textonuevo2">
                        <asp:TextBox ID="numasociado" runat="server" CssClass="texto" 
                ValidationGroup="colonos" Width="150px"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="numasociado_FilteredTextBoxExtender" runat="server" 
                                                                        Enabled="True" FilterType="Numbers" TargetControlID="numasociado">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td class="ingreso">
                        Email:</td>
                    <td class="textonuevo2">
                        <asp:TextBox ID="email" runat="server" CssClass="texto" 
                ValidationGroup="colonos" Width="150px"></asp:TextBox>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="textonuevo">
                                                                    &nbsp;</td>
                    <td class="textonuevo2">
                        <asp:Button ID="Button1" runat="server" Text="Recuperar" 
                ValidationGroup="colonos" />
                                                                    &nbsp;<asp:Button ID="Button2" 
                            runat="server" Text="volver" />
                                                                    &nbsp;<br />
                    </td>
                </tr>
                <tr>
                    <td class="textonuevo" colspan="2">
                        <asp:Label ID="mensajes" runat="server" CssClass="error"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="textonuevo" colspan="2">
                                                                    &nbsp;&nbsp;&nbsp;&nbsp; 
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="numasociado" Display="None" 
                ErrorMessage="Número de Asociado es un campo obligatorio" ValidationGroup="colonos"></asp:RequiredFieldValidator>
                        <br />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="email" CssClass="error" Display="None" 
                            ErrorMessage="Email no válido" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                            ValidationGroup="colonos"></asp:RegularExpressionValidator>
                        <asp:ValidationSummary ID="ValSummary" Runat="server" CssClass="error" 
                DisplayMode="List" HeaderText="Se encontraron los siguientes errores:" 
                ShowSummary="True" ValidationGroup="colonos" Width="303px" />
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="email" Display="None" 
                ErrorMessage="Email es un campo obligatorio" ValidationGroup="colonos"></asp:RequiredFieldValidator>
                        <br />
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    </td>
                </tr>
            </table>
        </td>
        <td align="left" valign="top">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style4">
                                                        &nbsp;</td>
        <td class="style10" align="left" valign="top">
            &nbsp;</td>
        <td align="left" valign="top">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style4">
                                                        &nbsp;</td>
        <td align="left" valign="top" class="style10">
            <br />
            <br />
        </td>
        <td align="left" valign="top">
            &nbsp;</td>
    </tr>
</table>
</asp:Content>

