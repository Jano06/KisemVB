<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="comprasvalidacion.aspx.vb" Inherits="sw_comprasvalidacion" title="Página sin título" %>

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
                Validación de Compras
                <br />
                <asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
            <td class="titulo">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3" valign="top">
               
                Archivo de Banco:<asp:FileUpload ID="FileUpload1" runat="server" />
&nbsp;<asp:Button ID="Button1" runat="server" Text="Cargar" />
               
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </td>
            <td class="style8">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

