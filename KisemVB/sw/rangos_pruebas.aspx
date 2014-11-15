<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="rangos_pruebas.aspx.vb" Inherits="sw_rangos_pruebas" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 507px;
            height: 73px;
        }
        .style2
        {
            height: 73px;
        }
        .style3
        {
            text-align: justify;
            font-family: Arial;
            font-size: large;
            font-weight: bold;
            color: #2b5076;
            vertical-align: top;
            height: 73px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
        <tr>
            <td colspan="2">
                <span  class="titulobienvenida">Prueba de Rangos</span>&nbsp;<br />&nbsp;
                                    
                                            <asp:Label ID="mensajes" runat="server" CssClass="titulo"></asp:Label>
                                            <p class="texto">
                                                <span  class="texto">Correr prueba de Rangos del período de
                                                <asp:TextBox ID="de" runat="server" MaxLength="50" TabIndex="4" 
                    Width="150px" 
    ValidationGroup="pago"></asp:TextBox>
                                                <cc1:CalendarExtender ID="TextBox26_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="de" Format="yyyy/M/dd">
                                                </cc1:CalendarExtender>
                                            &nbsp;a&nbsp;
                                                <asp:TextBox ID="a" runat="server" MaxLength="50" TabIndex="4" 
                    Width="150px" 
    ValidationGroup="pago"></asp:TextBox>
                                                <cc1:CalendarExtender ID="TextBox26_CalendarExtender0" runat="server" 
                    Enabled="True" TargetControlID="a" Format="yyyy/M/dd">
                                                </cc1:CalendarExtender>
                                                <asp:Button ID="Button1" runat="server" TabIndex="30" 
    Text="Iniciar" ValidationGroup="pago" OnClientClick="this.disabled=true;this.value = 'Generando Rangos...'" UseSubmitBehavior="false" />
                                                &nbsp;</span></p>
            
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <br />
                                     
               
                </td>
                <td class="titulo">
                                        &nbsp;</td>
            </tr>
        <tr>
            <td class="style1" valign="top">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" CssClass="textogrid" ForeColor="#333333" GridLines="None" 
                    Width="240px">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:CommandField SelectText="detalle" ShowSelectButton="True" />
                                <asp:BoundField DataField="rango" HeaderText="Rango"></asp:BoundField>
                                <asp:BoundField DataField="titulo" HeaderText="Título" />
                                <asp:BoundField DataField="pago" HeaderText="Pago" />
                            </Columns>
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                                     
               
                </td>
            <td class="style2" valign="top">
                    
                <asp:GridView ID="GridDetalle" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="800px">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="Asociado" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="inicioactivacion" HeaderText="Inicio Activación" 
                            DataFormatString="{0:dd/MMMM/yyyy}" />
                        <asp:BoundField DataField="finactivacion" HeaderText="Fin Activación" 
                            DataFormatString="{0:dd/MMMM/yyyy}" />
                        <asp:BoundField DataField="ptsmes" HeaderText="Puntos del Mes" />
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            
               
                </td>
                <td class="style3">
                                        </td>
            </tr>
            <tr>
                <td colspan="2">
                    
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="1200px">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="asociado" HeaderText="Asociado" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="rangoanterior" HeaderText="Rango Anterior" />
                        <asp:BoundField DataField="rangonuevo" HeaderText="Rango Nuevo" />
                        <asp:BoundField DataField="izq" HeaderText="Puntos Izquierdos" />
                        <asp:BoundField DataField="der" HeaderText="Puntos Derechos" />
                        <asp:BoundField DataField="directosizq" HeaderText="Patrocinados Izq&gt;350" />
                        <asp:BoundField DataField="directosder" HeaderText="Patrocinados Der&gt;350" />
                        <asp:BoundField DataField="inactivosizq" HeaderText="Inactivos Izq" />
                        <asp:BoundField DataField="inactivosder" HeaderText="Inactivos der" />
                        <asp:CommandField HeaderText="Patrocinados Izq&gt;350" 
                            ShowSelectButton="True" />
                        <asp:CommandField HeaderText="Patrocinados der&gt;350" 
                            ShowSelectButton="True" />
                        <asp:CommandField HeaderText="Inactivos Izq" ShowSelectButton="True" />
                        <asp:CommandField HeaderText="Inactivos Der" ShowSelectButton="True" />
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            
                </td>
                <td class="titulo">
                                        &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <br />
                </td>
            </tr>
        </table>
</asp:Content>

