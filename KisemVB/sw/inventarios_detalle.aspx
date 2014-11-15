<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="inventarios_detalle.aspx.vb" Inherits="sw_inventarios_detalle" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">






        .style8
        {
            width: 774px;
        }
        .style9
        {
            text-align: justify;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: Black;
            vertical-align: top;
            height: 14px;
        }
    .style10
    {
        width: 100%;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
        <tr>
            <td class="titulobienvenida" colspan="2">
                Detalle de
            Inventarios<asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
            &nbsp;</td>
            <td class="titulo">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
            &nbsp;</td>
            <td class="style8">
            &nbsp;&nbsp;&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px" 
                ShowFooter="True" DataKeyNames="id">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333"  />
                <Columns>
                    <asp:BoundField DataField="producto" HeaderText="Código" />
                    <asp:BoundField DataField="detalles" HeaderText="Descripción" />
                    <asp:BoundField DataField="bodega" HeaderText="Bodega" />
                    <asp:BoundField DataField="cantidad" HeaderText="Existencia" />
                    <asp:CommandField SelectText="Ver Detalle" ShowSelectButton="True" />
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
            <td class="style9" colspan="2">
                </td>
            <td class="style9">
                </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Panel ID="PanelFiltros" runat="server" Visible="False">
                    <table class="style10">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Width="95px"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Width="350px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textonuevo">
                                Movimiento:</td>
                            <td class="style8">
                                <asp:DropDownList ID="movimiento" runat="server" TabIndex="20" Width="230px">
                                    <asp:ListItem Value="0">Todos</asp:ListItem>
                                    <asp:ListItem Value="1">Inicial</asp:ListItem>
                                    <asp:ListItem Value="2">Entrada</asp:ListItem>
                                    <asp:ListItem Value="3">Salida</asp:ListItem>
                                    <asp:ListItem Value="4">Traspaso</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="textonuevo">
                                Fecha:</td>
                            <td class="style8">
                                Del
                                <asp:TextBox ID="fechade" runat="server" CssClass="texto" Width="100px"></asp:TextBox>
                                <cc1:CalendarExtender ID="fechade_CalendarExtender" runat="server" 
                                    Enabled="True" Format="dd/M/yyyy" TargetControlID="fechade">
                                </cc1:CalendarExtender>
                                &nbsp;Al &nbsp;&nbsp;<asp:TextBox ID="fechaa" runat="server" CssClass="texto" Width="100px"></asp:TextBox>
                                <cc1:CalendarExtender ID="fechaa_CalendarExtender" runat="server" 
                                    Enabled="True" Format="dd/M/yyyy" TargetControlID="fechaa">
                                </cc1:CalendarExtender>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="textonuevo">
                                Operador:</td>
                            <td class="style8">
                                <asp:DropDownList ID="Operador" runat="server" TabIndex="20" Width="230px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td class="style8">
                                <asp:Button ID="Button1" runat="server" TabIndex="30" Text="Filtrar" />
                                &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" 
                                    ImageUrl="~/sw/img/hilda/botones/exportaraexcel.jpg" ValidationGroup="filtro" 
                                    Visible="False" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
                </td>
        </tr>
        <tr>
            <td class="style9" colspan="2">
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="GridViewDetalle" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px" 
                ShowFooter="False">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="producto" HeaderText="Código" />
                        <asp:BoundField DataField="detalles" HeaderText="Descripción" />
                        <asp:BoundField DataField="operador" HeaderText="Operador" />
                        <asp:BoundField DataField="referencia" HeaderText="Referencia" />
                        <asp:BoundField DataField="tipo" HeaderText="Tipo de Movimiento" />
                        <asp:BoundField DataField="bodega" HeaderText="Bodega" />
                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
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

