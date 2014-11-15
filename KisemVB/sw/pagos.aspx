<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="pagos.aspx.vb" Inherits="sw_pagos" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">




        .style8
        {
            width: 147px;
        }
        .style9
    {
        height: 22px;
    }
        .style10
        {
            width: 141px;
        }
        .style11
        {
            width: 535%;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
        <tr>
            <td class="titulobienvenida" colspan="2">
                Pagos<asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
        </tr>
        <tr>
            <td class="textonuevo">
                Corte</td>
            <td>
                <asp:DropDownList ID="cortes" runat="server">
                </asp:DropDownList>
                    &nbsp;<asp:Button ID="Button2" runat="server" Text="Ver Corte" />
            &nbsp;<asp:Button ID="Button3" runat="server" Text="Reporte de Cheques" />
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
                                        <td>
                                            Compras Producto</td>
                                        <td class="style10">
                                            $<asp:Label ID="totalcompras" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td rowspan="21">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Kit de Inscripción</td>
                                        <td class="style10">
                                            $<asp:Label ID="totalinscripciones" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="textonuevo">
                                            Total Compras</td>
                                        <td class="textonuevo2">
                                            $<asp:Label ID="total" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="textonuevo">
                                            &nbsp;</td>
                                        <td class="textonuevo2">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Comisiones Producto</td>
                                        <td class="style10">
                                            $<asp:Label ID="comisionesproducto" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcproducto" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Comisiones Inscripción</td>
                                        <td class="style10">
                                            $<asp:Label ID="comisionesinscripcion" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcinscripcion" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textonuevo">
                                            Total de Comisiones</td>
                                        <td class="textonuevo2">
                                            $<asp:Label ID="totalcomisiones" runat="server"></asp:Label>
                                        </td>
                                        <td class="textonuevo2">
                                            <asp:Label ID="porctotal" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textonuevo">
                                            &nbsp;</td>
                                        <td class="textonuevo2">
                                            &nbsp;</td>
                                        <td class="textonuevo2">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Bono 1</td>
                                        <td class="style10">
                                            $<asp:Label ID="bono1" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono1" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Bono 2</td>
                                        <td class="style10">
                                            $<asp:Label ID="bono2" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono2" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Bono 3</td>
                                        <td class="style10">
                                            $<asp:Label ID="bono3" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono3" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Bono 4</td>
                                        <td class="style10">
                                            $<asp:Label ID="bono4" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono4" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Bono 5</td>
                                        <td class="style10">
                                            $<asp:Label ID="bono5" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono5" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Bono 6</td>
                                        <td class="style10">
                                            $<asp:Label ID="bono6" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono6" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Bono 7</td>
                                        <td class="style10">
                                            $<asp:Label ID="bono7" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono7" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Bono 8</td>
                                        <td class="style10">
                                            $<asp:Label ID="bono8" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono8" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Bono 9</td>
                                        <td class="style10">
                                            <asp:Label ID="bono9" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono9" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Bono 3 Niveles</td>
                                        <td class="style10">
                                            $<asp:Label ID="bono11" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono11" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td class="style10">
                                            <asp:Label ID="bono10" runat="server" Visible="False"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono10" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="Button1" runat="server" TabIndex="30" Text="Ver Detalle" 
                                                ValidationGroup="pago" />
                                            &nbsp;<asp:Button ID="btnliberar" runat="server" TabIndex="30" 
                                                Text="Liberar Corte" ValidationGroup="pago" Visible="False" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td class="style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Button ID="Button4" runat="server" Text="Exportar a Excel" />
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px">
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:BoundField DataField="asociado" HeaderText="Asociado" />
                                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="bono" HeaderText="Bono" />
                                                    <asp:BoundField DataField="monto" DataFormatString="{0:c}" HeaderText="Monto" />
                                                    <asp:BoundField DataField="de" DataFormatString="{0:dd/MMMM/yyyy}" 
                                                        HeaderText="De" />
                                                    <asp:BoundField DataField="a" DataFormatString="{0:dd/MMMM/yyyy}" 
                                                        HeaderText="A" />
                                                    <asp:BoundField DataField="corte" HeaderText="Corte" />
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
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
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
                <asp:Panel ID="Panelbono1" runat="server" Visible="False">
                    <table class="style11">
                        <tr>
                            <td class="titulobienvenida">
                                Detalle del Bono 1 del Asociado
                                <asp:Label ID="labelidasociado1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridBono1" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="compra" HeaderText="Compra" />
                                        <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MMMM/yyyy}" HeaderText="Fecha" />
                                        <asp:BoundField DataField="monto" DataFormatString="{0:c}" HeaderText="Monto" />
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
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style8" colspan="2">
                <asp:Panel ID="Panelbono2" runat="server" Visible="False">
                    <table class="style11">
                        <tr>
                            <td class="titulobienvenida">
                                Detalle del Bono 2 del Asociado
                                <asp:Label ID="labelidasociado2" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridBono2" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="Asociado" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MMMM/yyyy}" 
                                            HeaderText="Fecha Inscripción" />
                                        <asp:BoundField DataField="orden" HeaderText="Orden" />
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
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style8" colspan="2">
                <asp:Panel ID="Panelbono3" runat="server" Visible="False">
                    <table class="style11">
                        <tr>
                            <td class="titulobienvenida">
                                Detalle del Bono 3 del Asociado
                                <asp:Label ID="labelidasociado3" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridBono3" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="asociado" HeaderText="Asociado" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MMMM/yyyy}" 
                                            HeaderText="Fecha Inscripción" />
                                        <asp:BoundField DataField="orden" HeaderText="Orden" />
                                        <asp:BoundField DataField="patrocinador" HeaderText="Patrocinador" />
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
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style8" colspan="2">
                <asp:Panel ID="Panelbono4" runat="server" Visible="False">
                    <table class="style11">
                        <tr>
                            <td class="titulobienvenida">
                                Detalle del Bono 4 del Asociado
                                <asp:Label ID="labelidasociado4" runat="server"></asp:Label>
                                &nbsp;<br />
                                Pagado al
                                <asp:Label ID="porcentajelbl" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridBono4" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="descripcion" />
                                        <asp:BoundField DataField="izquierdos" HeaderText="Izquierdos" />
                                        <asp:BoundField DataField="derechos" HeaderText="Derechos" />
                                        <asp:CommandField HeaderText="Izquierdos" ShowSelectButton="True" />
                                        <asp:CommandField HeaderText="Derechos" ShowSelectButton="True" />
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                </asp:GridView>
                                <br />
                                <asp:Label ID="labelladodetalle" runat="server" CssClass="titulobienvenida"></asp:Label>
                                <asp:GridView ID="GridDetalleBono4" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="asociado" HeaderText="Asociado" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="compra" HeaderText="Compra" />
                                        <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MMMM/yyyy}" HeaderText="Fecha" />
                                        <asp:BoundField DataField="volumen" HeaderText="Volumen" />
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
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style8" colspan="2">
                <asp:Panel ID="Panelbono5" runat="server" Visible="False">
                    <table class="style11">
                        <tr>
                            <td class="titulobienvenida">
                                Detalle del Bono 5 del Asociado
                                <asp:Label ID="labelidasociado5" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridBono5" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="asociado" HeaderText="Asociado" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="monto" DataFormatString="{0:c}" 
                                            HeaderText="Monto Bono 4" />
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
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style8" colspan="2">
                <asp:Panel ID="Panelbono6" runat="server" Visible="False">
                    <table class="style11">
                        <tr>
                            <td class="titulobienvenida">
                                Detalle del Bono 6 del Asociado
                                <asp:Label ID="labelidasociado6" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridBono6" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="asociado" HeaderText="Asociado" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="compra" HeaderText="Compra" />
                                        <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MMMM/yyyy}" HeaderText="Fecha" />
                                        <asp:BoundField DataField="monto" DataFormatString="{0:c}" HeaderText="Monto" />
                                        <asp:BoundField DataFormatString="{0:c}" HeaderText="Pago" DataField="puntos" />
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
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style8" colspan="2">
                <asp:Panel ID="Panelbono7" runat="server" Visible="False">
                    <table class="style11">
                        <tr>
                            <td class="titulobienvenida">
                                Detalle del Bono 7 del Asociado
                                <asp:Label ID="labelidasociado7" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridBono7" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="asociado" HeaderText="Asociado" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="compra" HeaderText="Compra" />
                                        <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MMMM/yyyy}" HeaderText="Fecha" />
                                        <asp:BoundField DataField="monto" DataFormatString="{0:c}" HeaderText="Monto" />
                                        <asp:BoundField HeaderText="Pago" DataFormatString="{0:c}" />
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
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style8" colspan="2">
                <asp:Panel ID="Panelbono8" runat="server" Visible="False">
                    <table class="style11">
                        <tr>
                            <td class="titulobienvenida">
                                Detalle del Bono 8 del Asociado
                                <asp:Label ID="labelidasociado8" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridBono8" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="asociado" HeaderText="Asociado" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="compra" HeaderText="Compra" />
                                        <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MMMM/yyyy}" HeaderText="Fecha" />
                                        <asp:BoundField DataField="monto" DataFormatString="{0:c}" HeaderText="Monto" />
                                        <asp:BoundField DataField="puntos" DataFormatString="{0:c}" HeaderText="Pago" />
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
                    </table>
                </asp:Panel>
                <br />
                <asp:Panel ID="Panelbono11" runat="server" Visible="False">
                    <table class="style11">
                        <tr>
                            <td class="titulobienvenida">
                                Detalle del Bono de Niveles del Asociado
                                <asp:Label ID="labelidasociado11" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridBono11" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="compra" HeaderText="Compra" />
                                        <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MMMM/yyyy}" HeaderText="Fecha" />
                                        <asp:BoundField DataField="puntos" HeaderText="Puntos" />
                                        <asp:BoundField DataField="monto" DataFormatString="{0:c}" HeaderText="Monto" />
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
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>

