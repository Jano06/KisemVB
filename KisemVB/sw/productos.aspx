﻿<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="productos.aspx.vb" Inherits="sw_productos" title="Página sin título" %>

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
            Productos<asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
        <td class="titulo">
                &nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Width="200px"></asp:Label>
        </td>
        <td class="style8">
                &nbsp;</td>
        <td>
            <asp:Label ID="Label4" runat="server" Width="350px"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="textonuevo">
                Nombre:</td>
        <td class="style8">
            <asp:TextBox ID="nombre" runat="server" MaxLength="50" TabIndex="1" 
                    Width="350px"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="nombre" EnableClientScript="true" 
                    ErrorMessage="Nombre es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="textonuevo">
            Categoría:</td>
        <td>
                <asp:DropDownList ID="categorias" runat="server" Width="350px" 
                TabIndex="20">
                </asp:DropDownList>
            </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="Button1" runat="server" TabIndex="30" Text="Guardar" />
&nbsp;&nbsp;<asp:Button ID="Button3" runat="server" Text="Actualizar" Visible="False" />
&nbsp;<asp:Button ID="Button2" runat="server" Text="Cancelar" UseSubmitBehavior="False" 
                    Visible="False" />
        </td>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td class="subtitulo" colspan="2">
                &nbsp;</td>
        <td class="subtitulo">
                &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:BoundField DataField="producto" HeaderText="Nombre" />
                    <asp:BoundField DataField="categoria" HeaderText="Categoría" />
                    <asp:CommandField SelectText="Editar" ShowSelectButton="True" />
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
                &nbsp;</td>
        <td class="style8">
                &nbsp;</td>
        <td>
                &nbsp;</td>
    </tr>
</table>
</asp:Content>

