<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="reporte_compras.aspx.vb" Inherits="sw_reporte_compras" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register assembly="ExportToExcel" namespace="KrishLabs.Web.Controls" tagprefix="RK" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style9
    {
        height: 22px;
    }
        .style10
        {
            width: 141px;
        }
        



        .style8
        {
            width: 147px;
        }
        .style11
        {
            width: 600px;
        }
        .style12
        {
            width: 327px;
        }
        .style13
        {
            text-align: right;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: Gray;
            vertical-align: top;
            width: 327px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
        <tr>
            <td class="titulobienvenida" colspan="2">
                Reporte de Ventas<asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;&nbsp;&nbsp;
                </td>
        </tr>
        <tr>
            <td colspan="2">
                <table class="style11">
                    <tr>
                        <td class="textonuevo">
                            Fecha Inicio &nbsp;&nbsp;</td>
                        <td>
                            <asp:TextBox ID="fechade" runat="server" CssClass="texto" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="fechade_CalendarExtender" runat="server" 
                                Enabled="True" Format="dd/M/yyyy" TargetControlID="fechade">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            Fecha Final &nbsp;&nbsp;</td>
                        <td>
                            <asp:TextBox ID="fechaa" runat="server" CssClass="texto" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="fechaa_CalendarExtender" runat="server" 
                                Enabled="True" Format="dd/M/yyyy" TargetControlID="fechaa">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                Asociado</td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" Width="350px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" 
                    enabled="True" servicepath="~/BuscarAsociados.asmx" minimumprefixlength="2" 
                    servicemethod="ObtListaAsociados" enablecaching="true" targetcontrolid="TextBox1" 
                    usecontextkey="True" completionsetcount="10"
                    completioninterval="200">
                            </cc1:AutoCompleteExtender>
                            *Opcional</td>
                    </tr>
                    <tr>
                        <td>
                <asp:Button ID="Button2" runat="server" Text="Ver Reporte" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
&nbsp;&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel1" runat="server" Visible="False">
                    <table style="width: 823px">
                        <tr>
                            <td class="style9">
                                <table class="style1">
                                    <tr>
                                        <td class="style13">
                                            Total de Compras</td>
                                        <td class="textonuevo2">
                                            $<asp:Label ID="totalcompras" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td rowspan="5">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style13">
                                            Número de Compras</td>
                                        <td class="textonuevo2">
                                            <asp:Label ID="numerodecompras" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" ForeColor="#333333" GridLines="None" Width="600px">
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:BoundField DataField="paquete" HeaderText="Paquete" />
                                                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                                    <asp:BoundField DataField="monto" DataFormatString="{0:c}" HeaderText="Monto" />
                                                    <asp:BoundField DataField="porcentaje" DataFormatString="{0:P1}" 
                                                        HeaderText="% Ventas" />
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
                                        <td class="style12">
                                            <asp:Button ID="Button1" runat="server" TabIndex="30" Text="Ver Detalle" 
                                                ValidationGroup="pago" />
                                        </td>
                                        <td class="style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style12">
                                            &nbsp;</td>
                                        <td class="style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style8" valign="top">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="900px" 
                    CssClass="textochico">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="referencia" HeaderText="Referencia" />
                        <asp:BoundField DataField="id" HeaderText="Num" />
                        <asp:BoundField DataField="asociado" HeaderText="Asociado" />
                        <asp:BoundField DataField="monto" DataFormatString="{0:c}" HeaderText="Monto" />
                        <asp:BoundField DataField="Fecha" DataFormatString="{0:dd/MMMM/yyyy}" 
                            HeaderText="Fecha Pago" />
                        <asp:BoundField DataField="fechaorden" DataFormatString="{0:dd/MMMM/yyyy}" 
                            HeaderText="Fecha Orden" />
                        <asp:BoundField DataField="puntos" HeaderText="Puntos" />
                        <asp:BoundField DataField="tipopago" HeaderText="Tipo de Pago" />
                        <asp:BoundField DataField="comisionable" DataFormatString="{0:c}" 
                            HeaderText="Comisionable" />
                        <asp:BoundField DataField="statusentrega" HeaderText="Status" />
                        <asp:BoundField DataField="autor" HeaderText="Autor" />
                        <asp:CommandField SelectText="Detalle" ShowSelectButton="True" />
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            </td>
            <td class="style8" valign="top">
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
                                            <asp:GridView ID="GridPaquetes" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" ForeColor="#333333" 
                    GridLines="None" Width="339px">
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:BoundField DataField="paquete" HeaderText="Paquete" />
                                                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                                    <asp:BoundField DataField="costo" DataFormatString="{0:c}" HeaderText="Monto" />
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
            <td class="style8" colspan="2">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

