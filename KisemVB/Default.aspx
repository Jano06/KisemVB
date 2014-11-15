<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div style="text-align:center;">
    <table  width="900px">
                                                <tr>
                                                    <td class="style4">
                                                        <asp:Label ID="Label2" runat="server" Width="200px"></asp:Label>
                                                    </td>
                                                    <td class="style4">
                                                        &nbsp;</td>
                                                    <td align="left" valign="top">
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style4">
                                                        &nbsp;</td>
                                                    <td class="style4">
                                                        <img alt="Oficina Virtual" src="sw/img/logooficinavirtual.jpg" 
                                                            style="width: 197px; height: 84px" /></td>
                                                    <td align="left" valign="top">
                                                        <table class="style3">
                                                            <tr>
                                                                <td>
                                                                    <img alt="Acceso a Oficina Virtual" src="sw/img/acceso-a-oficina-virtual.jpg" 
                                                                        style="width: 237px; height: 38px" /></td>
                                                                <td>
                                                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                                                    </asp:ScriptManager>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="background-image:url('sw/img/fondousuarioypassword.jpg'); background-repeat:no-repeat; background-position:center;">
                                                        <table style="width:400px;">
                                                            <tr>
                                                                <td class="ingreso" >
                                                                    &nbsp;</td>
                                                                <td class="style7">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ingreso" >
                                                                    Número de Asociado:</td>
                                                                <td class="style7">
                                                                    <asp:TextBox ID="numasociado" runat="server" Width="130px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ingreso">
                                                                    Contraseña:</td>
                                                                <td class="style9">
                                                                    <asp:TextBox ID="password" runat="server" TextMode="Password" Width="130px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style8">
                                                                    &nbsp;</td>
                                                                <td class="style9">
            <asp:Button ID="Button1" runat="server" Text="Ingresar" 
                ValidationGroup="colonos" />
                                                                    &nbsp;<br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style8">
                                                                    &nbsp;</td>
                                                                <td class="style9">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            </table>
                                                                </td>
                                                                <td>
            <asp:ValidationSummary ID="ValSummary" Runat="server" CssClass="error" 
                DisplayMode="List" HeaderText="Se encontraron los siguientes errores:" 
                ShowSummary="True" ValidationGroup="colonos" Width="303px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="numasociado" Display="None" 
                ErrorMessage="Número de Asociado es un campo obligatorio" ValidationGroup="colonos"></asp:RequiredFieldValidator>
            <asp:Label ID="mensajes" runat="server" CssClass="error"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="password" Display="None" 
                ErrorMessage="Contraseña es un campo obligatorio" ValidationGroup="colonos"></asp:RequiredFieldValidator>
                                                                    <br />
                                                                    <asp:Label ID="Label1" runat="server" Width="400px"></asp:Label>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="olvidaste">
                                                                    <a href="recuperarpassword.aspx" class="olvidaste">Olvidaste tu contraseña</a></td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style4">
                                                        &nbsp;</td>
                                                    <td class="style4">
                                                        &nbsp;</td>
                                                    <td align="left" valign="top">
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                                </table>
                                                </div>
    </asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <style type="text/css">

        .style3
        {
            width: 100%;
        }
        .style4
        {
            width: 223px;
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
        .style7
        {
            text-align: left;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: #666666;
            vertical-align: top;
            height: 26px;
            width: 199px;
        }
        .style8
        {
            text-align: right;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: Gray;
            vertical-align: top;
            width: 216px;
        }
        .style9
        {
            text-align: left;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: #666666;
            vertical-align: top;
            width: 199px;
        }
        </style>

</asp:Content>


