<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="compradeinscripcion.aspx.vb" Inherits="sw_compradeinscripcion" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


        .style8
        {
            width: 147px;
        }
        .style1
        {
            width: 100%;
        }
        .style2
        {
            text-align: right;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: Gray;
            vertical-align: top;
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
        <tr>
            <td class="titulobienvenida" colspan="2">
                Compra de Inscripción<br />
                <asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;<asp:Label ID="idpadre" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="ladopadre" runat="server" Visible="False"></asp:Label>
            </td>
            <td class="titulo">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" CssClass="textogrid" ForeColor="#333333" GridLines="None" 
                    Width="950px">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ApPaterno" HeaderText="Apellido Paterno" />
                        <asp:BoundField DataField="ApMaterno" HeaderText="Apellido Materno" />
                        <asp:BoundField DataField="TelMovil" HeaderText="Celular" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:CommandField SelectText="Registrar Compra" ShowSelectButton="True" />
                    </Columns>
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
        <tr>
            <td colspan="3">
            <asp:Panel ID="PanelComprar" runat="server" Visible="False">
                <asp:Table ID="Table1" runat="server" Width="900px">
                </asp:Table>
            </asp:Panel>
            <asp:Panel ID="PanelCarrito" runat="server" Visible="False">
                <asp:GridView ID="GridCarrito" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="863px">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                        <asp:BoundField DataField="paquete" HeaderText="Paquete" />
                        <asp:BoundField DataField="precio" HeaderText="Precio Unitario" 
                            DataFormatString="{0:c}" />
                        <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:c}" />
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                <br />
                <asp:Button ID="continuar" runat="server" Text="Continuar Comprando" />
                &nbsp;<asp:Button ID="cancelar" runat="server" Text="Cancelar" />
                &nbsp;<asp:Button ID="confirmar" runat="server" Text="Confirmar" />
            </asp:Panel>
            <asp:Panel ID="PanelDatos" runat="server" Visible="False">
                <table class="style1">
                    <tr>
                        <td>
                            <span class="titulo">Datos del Prospecto</span></td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            Nombre</td>
                        <td class="textonuevo2">
                            <asp:Label ID="nombre" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Calle y Número</td>
                        <td class="textonuevo2">
                            <asp:Label ID="calle" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            Colonia</td>
                        <td class="textonuevo2">
                            <asp:Label ID="colonia" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            CP</td>
                        <td class="textonuevo2">
                            <asp:Label ID="cp" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            Municipio</td>
                        <td class="textonuevo2">
                            <asp:Label ID="municipio" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            Estado</td>
                        <td class="textonuevo2">
                            <asp:Label ID="estado" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            Forma de Pago</td>
                        <td>
                            <asp:DropDownList ID="formadepago" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            Entrega</td>
                        <td>
                            <asp:DropDownList ID="entrega" runat="server">
                                <asp:ListItem>Mostrador</asp:ListItem>
                                <asp:ListItem>Domicilio</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="cancelar0" runat="server" Text="Cancelar" />
                            &nbsp;<asp:Button ID="confirmar0" runat="server" Text="Cerrar Pedido" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="PanelResumen" runat="server" Visible="False">
                <table class="style1">
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridCarritoFinal" runat="server" AutoGenerateColumns="False" 
                                CellPadding="4" ForeColor="#333333" GridLines="None" Width="863px">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                    <asp:BoundField DataField="paquete" HeaderText="Paquete" />
                                    <asp:BoundField DataField="precio" DataFormatString="{0:c}" 
                                        HeaderText="Precio Unitario" />
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
                    <tr>
                        <td>
                            <span class="titulo">Mis Datos</span></td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            Nombre</td>
                        <td class="textonuevo2">
                            <asp:Label ID="nombreasociado" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Calle y Número</td>
                        <td class="textonuevo2">
                            <asp:Label ID="calle0" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            Colonia</td>
                        <td class="textonuevo2">
                            <asp:Label ID="colonia0" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            CP</td>
                        <td class="textonuevo2">
                            <asp:Label ID="cp0" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            Municipio</td>
                        <td class="textonuevo2">
                            <asp:Label ID="municipio0" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            Estado</td>
                        <td class="textonuevo2">
                            <asp:Label ID="estado0" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            Forma de Pago</td>
                        <td class="textonuevo2">
                            <asp:Label ID="formadepago0" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            Entrega</td>
                        <td class="textonuevo2">
                            <asp:Label ID="entrega0" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="cancelar1" runat="server" 
    Text="Cancelar" />
                &nbsp;<asp:Button ID="confirmar1" runat="server" 
    Text="Generar Orden" OnClientClick="this.disabled=true;this.value = 'Generando Orden...'" UseSubmitBehavior="false" />
            </asp:Panel>
                <asp:Panel ID="PanelGracias" runat="server" Visible="False">
                    <asp:Literal ID="gracias" runat="server"></asp:Literal>
                    <br />
                    <table class="style1" cellpadding="5px">
                        <tr>
                            <td class="textonuevo2">
                                Formas de Pago</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Banco</td>
                            <td>
                                Cuenta</td>
                            <td>
                                No. de Referencia</td>
                        </tr>
                        <tr>
                            <td>
                                <img alt="Skotiabank" src="img/Scotiabank.png" 
                                    style="width: 300px; height: 53px" /></td>
                            <td>
                                03504208749</td>
                            <td>
                                <asp:Label ID="oc" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img alt="Banorte" src="img/banorte.jpg" style="width: 300px; height: 53px" /></td>
                            <td>
                                0870626045</td>
                            <td>
                                <asp:Label ID="oc1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td class="textonuevo">
                                Importe</td>
                            <td>
                                <asp:Label ID="importe" runat="server" CssClass="textonuevo2"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                Favor de enviar su ficha de pago al correo:
                                <a href="mailto:atencionaclientes@kisem.com.mx">atencionaclientes@kisem.com.mx</a>
                                o al fax 442 222-8507 después de realizar su depósito.</td>
                        </tr>
                    </table>
                    <br />
                    &nbsp;
                </asp:Panel>
                
            </td>
        </tr>
    </table>
</asp:Content>

