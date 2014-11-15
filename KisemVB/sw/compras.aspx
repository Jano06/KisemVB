<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="compras.aspx.vb" Inherits="sw_compras" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


        .style8
        {
            width: 147px;
        }
        .style9
        {
            width: 275px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
    <tr>
        <td class="titulobienvenida" colspan="2">
            Compras<asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
        <td class="titulo">
                &nbsp;</td>
        <td class="titulo">
                &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Panel ID="Panel1" runat="server" Visible="False">
                Índice para Iniciar:<asp:TextBox ID="indice" runat="server">0</asp:TextBox>
                <asp:Button ID="Button2" runat="server" Text="Compras Anexas" />
                &nbsp;Índice final:<asp:Label ID="indicefinal" runat="server"></asp:Label>
                <br />
            </asp:Panel>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
                &nbsp;<asp:TextBox ID="txt_usuario" runat="server" Visible="False"></asp:TextBox>
            &nbsp; 
                <asp:TextBox ID="fechade" runat="server" CssClass="texto" 
                    Width="100px" ValidationGroup="comprasarray" Visible="False"></asp:TextBox>
                            <cc1:CalendarExtender ID="fechade_CalendarExtender" runat="server" 
                                Enabled="True" Format="yyyy/M/d" TargetControlID="fechade">
                            </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                    runat="server" ControlToValidate="fechade" ErrorMessage="Fecha requerida" 
                    ValidationGroup="comprasarray"></asp:RequiredFieldValidator>
                                    </td>
        <td>
                &nbsp;</td>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
                <asp:TextBox ID="delasociado" runat="server" ValidationGroup="comprasarray" 
                    Width="52px" Visible="False"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="delasociado_FilteredTextBoxExtender" 
                    runat="server" Enabled="True" FilterType="Numbers" 
                    TargetControlID="delasociado">
                </cc1:FilteredTextBoxExtender>
                &nbsp;<asp:TextBox ID="alasociado" runat="server" ValidationGroup="comprasarray" 
                    Width="52px" Visible="False"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="alasociado_FilteredTextBoxExtender" 
                    runat="server" Enabled="True" FilterType="Numbers" TargetControlID="alasociado">
                </cc1:FilteredTextBoxExtender>
&nbsp;<br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="delasociado" 
                    ErrorMessage="Del Asociado es un campo requerido" 
                    ValidationGroup="comprasarray"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="alasociado" ControlToValidate="delasociado" 
                    ErrorMessage="Del asociado debe ser menor o igual" Operator="LessThanEqual" 
                    ValidationGroup="comprasarray"></asp:CompareValidator>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="alasociado" ErrorMessage="Al Asociado es un campo requerido" 
                    ValidationGroup="comprasarray"></asp:RequiredFieldValidator>
                <br />
                        <asp:Button ID="Button12" runat="server" TabIndex="30" 
                    Text="Agregar Compras a Asociados" ValidationGroup="comprasarray" 
                    Visible="False" />
                                    </td>
        <td>
                &nbsp;</td>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
            <table class="style1">
                <tr>
                    <td class="style9">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" CssClass="textogrid" ForeColor="#333333" GridLines="None" 
                    Width="450px" Visible="False">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="Paquete"></asp:BoundField>
                                <asp:BoundField DataField="nombre" />
                                <asp:BoundField DataField="puntos" HeaderText="Puntos" />
                                <asp:BoundField DataField="costo" DataFormatString="{0:c}" HeaderText="Costo" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="TextBox1" Text="0" Width="30"/>
                                        <cc1:FilteredTextBoxExtender ID="TextBox1_FilteredTextBoxExtender" 
                                runat="server" Enabled="True" FilterType="Numbers" TargetControlID="TextBox1">
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
                        <asp:Button ID="Button1" runat="server" TabIndex="30" Text="Comprar" 
                            Visible="False" />
                    </td>
                </tr>
            </table>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
                <asp:TextBox ID="pruebas" runat="server" Visible="False"></asp:TextBox>
        </td>
        <td class="style8">
                <asp:Button ID="Button29" runat="server" Text="04-10 mayo" Visible="False" />
                </td>
        <td>
                &nbsp;</td>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td>
                &nbsp;</td>
        <td class="style8">
                &nbsp;</td>
        <td>
                &nbsp;</td>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td>
                Compra de inicio
                <asp:TextBox ID="inicio" runat="server" Width="50px">1</asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="inicio_FilteredTextBoxExtender" runat="server" 
                    Enabled="True" FilterType="Numbers" TargetControlID="inicio">
                </cc1:FilteredTextBoxExtender>
        </td>
        <td class="style8">
                &nbsp;</td>
        <td>
                &nbsp;</td>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td>
                <asp:Button ID="Button3" runat="server" Text="16-22 marzo" />
                <asp:Label ID="fecha1" runat="server"></asp:Label>
        </td>
        <td class="style8">
                <asp:Button ID="Button13" runat="server" Text="11-17 mayo" />
        </td>
        <td>
                <asp:Button ID="Button21" runat="server" Text="6-12 Jul" />
        </td>
        <td>
                <asp:Button ID="Button30" runat="server" Text="31 ago - 6 sep" />
        </td>
    </tr>
    <tr>
        <td>
                <asp:Button ID="Button4" runat="server" Text="23-29 marzo" 
                    style="width: 96px" />
                <asp:Label ID="fecha2" runat="server"></asp:Label>
        </td>
        <td class="style8">
                <asp:Button ID="Button14" runat="server" Text="18-24 mayo" />
        </td>
        <td>
                <asp:Button ID="Button22" runat="server" Text="13-19 Jul" />
        </td>
        <td>
                <asp:Button ID="Button31" runat="server" Text="7 -  13 sep" />
        </td>
    </tr>
    <tr>
        <td>
                <asp:Button ID="Button5" runat="server" Text="30 marzo - 05 abril" />
                <asp:Label ID="fecha3" runat="server"></asp:Label>
        </td>
        <td class="style8">
                <asp:Button ID="Button15" runat="server" Text="25-31 mayo" />
        </td>
        <td>
                <asp:Button ID="Button23" runat="server" Text="20-26 Jul" />
        </td>
        <td>
                <asp:Button ID="Button32" runat="server" Text="14 -  20 sep" />
        </td>
    </tr>
    <tr>
        <td>
                <asp:Button ID="Button6" runat="server" Text="06-12 abril" />
                <asp:Label ID="fecha4" runat="server"></asp:Label>
        </td>
        <td class="style8">
                <asp:Button ID="Button16" runat="server" Text="01-07 junio" />
        </td>
        <td>
                <asp:Button ID="Button24" runat="server" Text="27 Jul - 2 Ago" />
        </td>
        <td>
                <asp:Button ID="Button33" runat="server" Text="21 - 27 sep" />
        </td>
    </tr>
    <tr>
        <td>
                <asp:Button ID="Button7" runat="server" Text="13-19 abril" />
                <asp:Label ID="fecha5" runat="server"></asp:Label>
        </td>
        <td class="style8">
                <asp:Button ID="Button17" runat="server" Text="08-14 junio" />
        </td>
        <td>
                <asp:Button ID="Button25" runat="server" Text="3-9 Ago" />
        </td>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td>
                <asp:Button ID="Button8" runat="server" Text="20-26 abril" />
                <asp:Label ID="fecha6" runat="server"></asp:Label>
        </td>
        <td class="style8">
                <asp:Button ID="Button18" runat="server" Text="15-21 junio" />
        </td>
        <td>
                <asp:Button ID="Button26" runat="server" Text="10-16 Ago" />
        </td>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td>
                <asp:Button ID="Button9" runat="server" Text="27 abril - 03 mayo" />
                <asp:Label ID="fecha7" runat="server"></asp:Label>
        </td>
        <td class="style8">
                <asp:Button ID="Button19" runat="server" Text="22-28 junio" />
        </td>
        <td>
                <asp:Button ID="Button27" runat="server" Text="17-23 Ago" />
        </td>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td>
                <asp:Button ID="Button10" runat="server" Text="04-10 mayo" />
                <asp:Label ID="fecha8" runat="server"></asp:Label>
        </td>
        <td class="style8">
                <asp:Button ID="Button20" runat="server" Text="29 junio -05 julio" />
        </td>
        <td>
                <asp:Button ID="Button28" runat="server" Text="24-30 Ago" />
        </td>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td>
                &nbsp;</td>
        <td class="style8">
                &nbsp;</td>
        <td>
                &nbsp;</td>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td>
                <asp:Button ID="Button11" runat="server" 
                    Text="Iniciar Ejercicio" />
                <cc1:ConfirmButtonExtender ID="Button11_ConfirmButtonExtender" runat="server" 
                    ConfirmText="¿Está seguro? Esto borrará todos los pagos y compras de la base de datos" 
                    Enabled="True" TargetControlID="Button11">
                </cc1:ConfirmButtonExtender>
        </td>
        <td class="style8">
                &nbsp;</td>
        <td>
                &nbsp;</td>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td>
                
                
        </td>
        <td class="style8">
                &nbsp;</td>
        <td>
                &nbsp;</td>
        <td>
                &nbsp;</td>
    </tr>
</table>
</asp:Content>

