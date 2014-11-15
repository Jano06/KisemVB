<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="pagosrangos.aspx.vb" Inherits="sw_pagosrangos" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">





        .style8
        {
            width: 147px;
        }
        .style9
        {
            width: 64px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
        <tr>
            <td class="titulobienvenida" colspan="2">
                Pagos por Rango<asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
            <td class="titulo">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style9">
                &nbsp;</td>
            <td class="style8">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" CssClass="textogrid" ForeColor="#333333" GridLines="None" 
                    Width="450px">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="rango" HeaderText="Rango"></asp:BoundField>
                        <asp:BoundField DataField="paquete" HeaderText="Paquete" />
                        <asp:BoundField DataField="pago" HeaderText="Pago" />
                        <asp:TemplateField HeaderText="Pago">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="TextBox1" Text="0" Width="70" />
                                <cc1:FilteredTextBoxExtender ID="TextBox1_FilteredTextBoxExtender" 
                                runat="server" Enabled="True"  FilterType="Custom, Numbers" ValidChars=".%" TargetControlID="TextBox1">
                                </cc1:FilteredTextBoxExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            </td>
            <td valign="top">
                <asp:Button ID="Button1" runat="server" TabIndex="30" Text="Guardar" />
                <cc1:ConfirmButtonExtender ID="Button1_ConfirmButtonExtender" runat="server" 
                    ConfirmText="¿Desea actualizar los valores de la lista?" Enabled="True" 
                    TargetControlID="Button1">
                </cc1:ConfirmButtonExtender>
            </td>
        </tr>
        <tr>
            <td class="style9">
                &nbsp;</td>
            <td valign="top">
&nbsp;&nbsp;&nbsp;</td>
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
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style9">
                &nbsp;</td>
            <td class="style8">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

