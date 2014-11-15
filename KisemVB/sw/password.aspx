<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="password.aspx.vb" Inherits="sw_password" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">





        .style9
        {
            width: 244px;
        }
        
        .style1
        {
            width: 100%;
            padding:0;
            
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
    <tr>
        <td align="center">
            <img alt="" src="img/hilda/titulos/password.jpg" 
                style="width: 252px; height: 33px" /><asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td class="celdamarco">
                <table class="subtitulo">
                    <tr>
                        <td class="subtitulo">
                            Contraseña Anterior </td>
                        <td>
                            <asp:TextBox ID="passwordanterior" runat="server" CssClass="texto" 
                                TextMode="Password" Width="250px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="subtitulo">
                            Contraseña Nueva (Max 10 caracteres)</td>
                        <td>
                            <asp:TextBox ID="password" runat="server" CssClass="texto" TextMode="Password" 
                                Width="250px" MaxLength="10"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="subtitulo">
                            Repetir Contraseña Nueva</td>
                        <td>
                            <asp:TextBox ID="password0" runat="server" CssClass="texto" TextMode="Password" 
                                Width="250px" MaxLength="10"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style9">
                            <asp:Label ID="Label1" runat="server" Width="310px"></asp:Label>
                        </td>
                        <td colspan="2">
            <div style="float:left; width: 348px;"><asp:ImageButton ID="ImageButton1" runat="server" 
                ImageUrl="~/sw/img/hilda/botones/guardar.jpg" />
            &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" 
                ImageUrl="~/sw/img/hilda/botones/cancelar.jpg" style="height: 20px" /></div>
                            <asp:ValidationSummary ID="ValSummary" Runat="server" CssClass="error" 
                                DisplayMode="List" HeaderText="Se encontraron los siguientes errores:" 
                                ShowSummary="True" Width="367px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                ControlToValidate="password0" Display="None" 
                                ErrorMessage="Repetir Contraseña es un campo obligatorio"></asp:RequiredFieldValidator>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="password" Display="None" 
                                ErrorMessage="Contraseña es un campo obligatorio"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                ControlToCompare="password" ControlToValidate="password0" Display="None" 
                                ErrorMessage="Las contraseñas deben coincidir"></asp:CompareValidator>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="passwordanterior" Display="None" EnableClientScript="true" 
                                ErrorMessage="Contraseña es un campo obligatorio"></asp:RequiredFieldValidator>
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

