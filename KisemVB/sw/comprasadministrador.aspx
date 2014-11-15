<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="comprasadministrador.aspx.vb" Inherits="sw_comprasadministrador" title="Página sin título" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
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
    <script type="text/javascript">
 function ValidNum(e) {
    var tecla=document.all ? tecla = e.keyCode : tecla = e.which;
    return ((tecla > 47 && tecla < 58) || tecla == 46);
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
    <tr>
        <td class="titulobienvenida" colspan="3">
            Compras<asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="titulobienvenida" colspan="3">
            <asp:Panel ID="PanelComprar" runat="server">
                <table class="style1">
                    <tr>
                        <td class="textonuevo">
                            Asociado:
                        </td>
                        <td>
                            <asp:TextBox ID="txtasociado" runat="server" CssClass="texto" Width="350px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="txtasociado_AutoCompleteExtender" runat="server" 
                                completioninterval="200" completionsetcount="10" enablecaching="true" 
                                enabled="True" minimumprefixlength="2" servicemethod="ObtListaAsociados" 
                                servicepath="~/BuscarAsociados.asmx" targetcontrolid="txtasociado" 
                                usecontextkey="True">
                            </cc1:AutoCompleteExtender>
                            <asp:Label ID="faltaasociado" runat="server" CssClass="error" 
                                Text="Error en el asociado o fecha" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            Fecha:</td>
                        <td>
                            <span class="texto">
                            <asp:TextBox ID="fecha" runat="server" MaxLength="50" TabIndex="4" 
                                ValidationGroup="pago" Width="150px"></asp:TextBox>
                            <cc1:CalendarExtender ID="TextBox26_CalendarExtender" runat="server" 
                                Enabled="True" Format="dd/M/yyyy" TargetControlID="fecha">
                            </cc1:CalendarExtender>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Table ID="Table1" runat="server" Width="900px">
                            </asp:Table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td  colspan="3">
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
                <asp:Panel ID="PanelGracias" runat="server" Visible="False">
                    <asp:Literal ID="gracias" runat="server"></asp:Literal>
                    <br />
                    <br />
                    <table cellpadding="5px" class="style1">
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
                                <img alt="Skitiabank" src="img/Scotiabank.png" 
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
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" CssClass="textogrid" ForeColor="#333333" GridLines="None" 
                    Width="450px">
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
                </asp:Panel>
                
        </td>
    </tr>
    <tr>
        <td  colspan="3">
            <asp:Panel ID="PanelDatos" runat="server" Visible="False">
                <table class="style1">
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
        </td>
    </tr>
    <tr>
        <td  colspan="3">
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
        </td>
    </tr>
    <tr>
        <td>
                <asp:Panel ID="PanelPregunta1" runat="server" Visible="False">
                <div>
                <table style="width: 542px;">
                <tr>
                <td class="textonuevo">
                    ¿En qué período deseas activarte?
                </td>
                </tr>
                 <tr>
                <td class="textonuevo">
                    El que está finalizando
               <asp:RadioButton ID="actual" runat="server" Checked="True" GroupName="periodo" />
                </td>
                </tr>
                 <tr>
                <td class="textonuevo">
                    El que está por comenzar
                <asp:RadioButton ID="siguiente" runat="server" GroupName="periodo" />
                </td>
                </tr>
                    <tr>
                        <td class="textonuevo">
                            <asp:Button ID="cancelar2" runat="server" Text="Cancelar" />
                            &nbsp;<asp:Button ID="confirmar2" runat="server" Text="Activarme" />
                        </td>
                    </tr>
                </table>
                
                
            </div>
                  
                </asp:Panel>
                
        </td>
        <td class="style8">
                &nbsp;</td>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td>
                <asp:Panel ID="PanelPregunta2" runat="server" Visible="False">
                <div>
                <table style="width: 542px;">
                <tr>
                <td class="textonuevo">
                    ¿Cómo deseas que administremos tu compra?
                </td>
                </tr>
                 <tr>
                <td class="textonuevo">
               <asp:RadioButton ID="activacionsiguienteperiodo" runat="server" Checked="True" 
                        GroupName="pregunta2" Text="Como activación del siguiente período " />
                </td>
                </tr>
                 <tr>
                <td class="textonuevo">
                <asp:RadioButton ID="compraexcedente" runat="server" GroupName="pregunta2" 
                        Text="Como compra excedente" />
                </td>
                </tr>
                    <tr>
                        <td class="textonuevo">
                            <asp:Button ID="cancelar3" runat="server" Text="Cancelar" />
                            &nbsp;<asp:Button ID="confirmar3" runat="server" Text="Aceptar" />
                        </td>
                    </tr>
                </table>
                
                
            </div>
                  
                </asp:Panel>
                
        </td>
        <td class="style8">
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
    </tr>
</table>
</asp:Content>

