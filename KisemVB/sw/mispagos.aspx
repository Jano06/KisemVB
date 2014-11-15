<%@ Page  Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="mispagos.aspx.vb" Inherits="sw_mispagos" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

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
                
        .style1
        {
            width: 100%;
            padding:0;
            
        }
        .style11
        {
            width: 100%;
        }
        .style12
        {
            width: 230px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
    <tr>
        <td align="center">
            <img alt="" src="img/hilda/titulos/misganancias.jpg" 
                style="width: 252px; height: 33px" /></td>
    </tr>
    <tr>
        <td>
                <asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="celdamarco">
    <table align="left" class="texto">
    <tr>
        <td>
            <asp:Panel ID="PanelResumen" runat="server">
                <table class="style11">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" CssClass="subtitulo"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridViewresumen" runat="server" AutoGenerateColumns="False" 
                                CellPadding="4" ForeColor="#333333" GridLines="None" Width="950px">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:BoundField DataField="corte" HeaderText="Período" />
                                    <asp:BoundField DataField="monto" DataFormatString="{0:c}" 
                                        HeaderText="Ganancias" />
                                    <asp:BoundField HeaderText="Retención ISR" DataField="factura" />
                                    <asp:BoundField HeaderText="IVA" />
                                    <asp:BoundField HeaderText="Retención IVA" />
                                    <asp:BoundField HeaderText="Ganancia Neta" />
                                    <asp:BoundField DataField="periodo" HeaderText="Período Incentivable" />
                                    <asp:CommandField HeaderText="Período Incentivable" SelectText="Detalle" 
                                        ShowSelectButton="True" />
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
                        <td align="center">
                            &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" 
                                ImageUrl="~/sw/img/hilda/botones/primera.jpg" />
&nbsp;<asp:ImageButton ID="PagAnterior" runat="server" ImageUrl="~/sw/img/hilda/botones/anterior.jpg" />
                            &nbsp;<asp:ImageButton ID="PagSiguiente" runat="server" 
                                ImageUrl="~/sw/img/hilda/botones/siguiente.jpg" />
                            &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" 
                                ImageUrl="~/sw/img/hilda/botones/ultima.jpg" />
                        </td>
                    </tr>
                </table>
                <br />
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
                <asp:Panel ID="PanelDetalle" runat="server" Visible="False">
                    <table class="style11">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="periodotext" runat="server" CssClass="subtitulo"></asp:Label>
                                <asp:Label ID="periodo" runat="server" CssClass="subtitulo"></asp:Label>
                                <asp:Label ID="dea" runat="server" CssClass="subtitulo"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="GridViewGanancias" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="800px">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="bono" HeaderText="Bono">
                                            <ItemStyle Width="500px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ganancias" DataFormatString="{0:c}" 
                                            HeaderText="Ganancias">
                                            <ItemStyle Width="300px" />
                                        </asp:BoundField>
                                        <asp:CommandField HeaderText="Ganancias" ShowSelectButton="True">
                                            <ItemStyle Width="300px" />
                                        </asp:CommandField>
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
                            <td colspan="2">
                                <asp:GridView ID="GridViewimpuestos" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="800px">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="impuesto" HeaderText="Impuestos">
                                            <ItemStyle Width="500px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto" DataFormatString="{0:c}">
                                            <ItemStyle Width="300px" />
                                        </asp:BoundField>
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
                                &nbsp;</td>
                            <td>
                                <asp:ImageButton ID="anterior" runat="server" 
                                    ImageUrl="~/sw/img/hilda/botones/anterior.jpg" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
                <asp:Panel ID="Panelbono1" runat="server" Visible="False">
                    <table class="style11">
                        <tr>
                            <td class="titulobienvenida">
                                Detalle del Bono Excedente
                                <asp:Label ID="labelidasociado1" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridBono1" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="compra" HeaderText="Compra" />
                                        <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MMMM/yyyy}" 
                                            HeaderText="Fecha" />
                                        <asp:BoundField DataField="monto" DataFormatString="{0:c}" HeaderText="Monto" />
                                        <asp:BoundField DataField="ganancia" DataFormatString="{0:c}" 
                                            HeaderText="Ganancia" />
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
                <asp:Panel ID="Panelbono2" runat="server" Visible="False">
                    <table class="style11">
                        <tr>
                            <td class="titulobienvenida">
                                Detalle del Bono por Inscripción
                                <asp:Label ID="labelidasociado2" runat="server" Visible="false"></asp:Label>
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
                                        <asp:BoundField DataField="ganancia" DataFormatString="{0:c}" 
                                            HeaderText="Ganancia" />
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
                <asp:Panel ID="Panelbono3" runat="server" Visible="False">
                    <table class="style11">
                        <tr>
                            <td class="titulobienvenida">
                                Detalle del Bono por Inscripción Infinito
                                <asp:Label ID="labelidasociado3" runat="server" Visible="false"></asp:Label>
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
                                        <asp:BoundField DataField="ganancia" DataFormatString="{0:c}" 
                                            HeaderText="Ganancia" />
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
                <asp:Panel ID="Panelbono4" runat="server" Visible="False">
                    <table class="style11">
                        <tr>
                            <td class="titulobienvenida">
                                Detalle del Bono de Igualación de Volumen
                                <asp:Label ID="labelidasociado4" runat="server" Visible="false"></asp:Label>
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
                                        <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MMMM/yyyy}" 
                                            HeaderText="Fecha" />
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
                <asp:Panel ID="Panelbono5" runat="server" Visible="False">
                    <table class="style11">
                        <tr>
                            <td class="titulobienvenida">
                                Detalle del Bono de Seguimiento
                                <asp:Label ID="labelidasociado5" runat="server" Visible="false"></asp:Label>
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
                                        <asp:BoundField DataField="ganancia" DataFormatString="{0:c}" 
                                            HeaderText="Ganancia" />
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
                <asp:Panel ID="Panelbono6" runat="server" Visible="False">
                    <table class="style11">
                        <tr>
                            <td class="titulobienvenida">
                                Detalle del Bono de Bienestar
                                <asp:Label ID="labelidasociado6" runat="server" Visible="false"></asp:Label>
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
                                        <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MMMM/yyyy}" 
                                            HeaderText="Fecha" />
                                        <asp:BoundField DataField="monto" DataFormatString="{0:c}" HeaderText="Monto" />
                                        <asp:BoundField DataFormatString="{0:c}" HeaderText="Ganancia" 
                                            DataField="ganancia" />
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
                <asp:Panel ID="Panelbono7" runat="server" Visible="False">
                    <table class="style11">
                        <tr>
                            <td class="titulobienvenida">
                                Detalle del Bono Guía
                                <asp:Label ID="labelidasociado7" runat="server" Visible="false"></asp:Label>
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
                                        <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MMMM/yyyy}" 
                                            HeaderText="Fecha" />
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
                <asp:Panel ID="Panelbono8" runat="server" Visible="False">
                    <table class="style11">
                        <tr>
                            <td class="titulobienvenida">
                                Detalle del Bono de Desarrollo de Red
                                <asp:Label ID="labelidasociado8" runat="server" Visible="false"></asp:Label>
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
                                        <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MMMM/yyyy}" 
                                            HeaderText="Fecha" />
                                        <asp:BoundField DataField="monto" DataFormatString="{0:c}" HeaderText="Monto" />
                                        <asp:BoundField HeaderText="Pago" DataFormatString="{0:c}" DataField="puntos" />
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
        <td>
            <asp:Panel ID="Panel1" runat="server" Visible="False">
                <table style="width: 823px">
                    <tr>
                        <td class="style9">
                            <table class="style1">
                                <tr>
                                    <td class="textonuevo">
                                            <asp:DropDownList ID="DropDownList1" runat="server">
                                            </asp:DropDownList>
                                    </td>
                                    <td class="textonuevo2">
                                            <asp:Button ID="Button2" runat="server" Text="Ver Corte" />
                                    </td>
                                    <td>
                                            &nbsp;</td>
                                    <td rowspan="13">
                                            &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="textonuevo">
                                            Total de Compras</td>
                                    <td class="textonuevo2">
                                            $<asp:Label ID="totalcompras" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                            &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="textonuevo">
                                            Total de Comisiones</td>
                                    <td class="textonuevo2">
                                            $<asp:Label ID="totalcomisiones" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="textonuevo">
                                            Bono 1</td>
                                    <td class="textonuevo2">
                                            $<asp:Label ID="bono1" runat="server"></asp:Label>
                                    </td>
                                    <td class="textonuevo2">
                                        <asp:Label ID="porcbono1" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="textonuevo">
                                            Bono 2</td>
                                    <td class="textonuevo2">
                                            $<asp:Label ID="bono2" runat="server"></asp:Label>
                                    </td>
                                    <td class="textonuevo2">
                                        <asp:Label ID="porcbono2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="textonuevo">
                                            Bono 3</td>
                                    <td class="textonuevo2">
                                            $<asp:Label ID="bono3" runat="server"></asp:Label>
                                    </td>
                                    <td class="textonuevo2">
                                        <asp:Label ID="porcbono3" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="textonuevo">
                                            Bono 4</td>
                                    <td class="textonuevo2">
                                            $<asp:Label ID="bono4" runat="server"></asp:Label>
                                    </td>
                                    <td class="textonuevo2">
                                        <asp:Label ID="porcbono4" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="textonuevo">
                                            Bono 5</td>
                                    <td class="textonuevo2">
                                            $<asp:Label ID="bono5" runat="server"></asp:Label>
                                    </td>
                                    <td class="textonuevo2">
                                        <asp:Label ID="porcbono5" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="textonuevo">
                                            Bono 6</td>
                                    <td class="textonuevo2">
                                            $<asp:Label ID="bono6" runat="server"></asp:Label>
                                    </td>
                                    <td class="textonuevo2">
                                        <asp:Label ID="porcbono6" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="textonuevo">
                                            Bono 7</td>
                                    <td class="textonuevo2">
                                            $<asp:Label ID="bono7" runat="server"></asp:Label>
                                    </td>
                                    <td class="textonuevo2">
                                        <asp:Label ID="porcbono7" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="textonuevo">
                                        Bono 8</td>
                                    <td class="textonuevo2">
                                            $<asp:Label ID="bono8" runat="server"></asp:Label>
                                    </td>
                                    <td class="textonuevo2">
                                            <asp:Label ID="porcbono8" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                            <asp:Button ID="Button1" runat="server" TabIndex="30" Text="Ver Detalle" 
                                                ValidationGroup="pago" />
                                    </td>
                                    <td class="style10">
                                            &nbsp;</td>
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
        <td class="style8">
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
    <tr>
        <td align="left">
            <img alt="" src="img/hilda/sombra.png" style="width: 323px; height: 80px" /></td>
    </tr>
</table>
</asp:Content>

