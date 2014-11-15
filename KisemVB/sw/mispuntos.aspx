<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="mispuntos.aspx.vb" Inherits="sw_mispuntos" title="Página sin título" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

                
        .style1
        {
            width: 100%;
            padding:0;
            
        }
        .style11
        {
            width: 100%;
        }
                



        .style8
        {
            width: 147px;
        }
                
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
        <tr>
            <td align="center">
                <img alt="" src="img/hilda/titulos/mispuntos.jpg"
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
                                            &nbsp;</td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" CssClass="subtitulo" Width="450px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:GridView ID="GridViewresumen" runat="server" AutoGenerateColumns="False" 
                                CellPadding="4" ForeColor="#333333" GridLines="None" Width="450px">
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:BoundField DataField="corte" HeaderText="Período" />
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
                                        <td valign="top">
                                            <asp:GridView ID="GridBono4" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" ForeColor="#333333" GridLines="None" Width="500px">
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:BoundField DataField="descripcion" />
                                                    <asp:BoundField DataField="izquierdos" HeaderText="Izquierdos" />
                                                    <asp:BoundField DataField="derechos" HeaderText="Derechos" />
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
                                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                                ImageUrl="~/sw/img/hilda/botones/primera.jpg" />
                                            &nbsp;<asp:ImageButton ID="PagAnterior" runat="server" 
                                                ImageUrl="~/sw/img/hilda/botones/anterior.jpg" />
                                            &nbsp;<asp:ImageButton ID="PagSiguiente" runat="server" 
                                ImageUrl="~/sw/img/hilda/botones/siguiente.jpg" />
                                            &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" 
                                                ImageUrl="~/sw/img/hilda/botones/ultima.jpg" />
                                        </td>
                                        <td align="center">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <br />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="Panelbono4" runat="server" Visible="False">
                                <table class="style11">
                                    <tr>
                                        <td class="titulobienvenida">
                                            Detalle de Puntos</td>
                                    </tr>
                                    <tr>
                                        <td>
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
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style8">
                            &nbsp;</td>
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

