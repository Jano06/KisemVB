<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="miscompras.aspx.vb" Inherits="sw_miscompras" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style11
        {
            width: 807px;
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
        .style12
        {
            width: 128px;
        }
        .style13
        {
            width: 130px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
    <tr>
        <td align="center">
            <img alt="" src="img/hilda/titulos/miscompras.jpg" 
                style="width: 252px; height: 33px" /><asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td class="celdamarco">
    <table align="left" class="texto">
    <tr>
        <td>
            <asp:Panel ID="PanelResumen" runat="server">
                <table class="style11">
                    <tr>
                        <td class="style12">
                            &nbsp;&nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td class="style13">
                            Fecha Inicial
                        </td>
                        <td>
                            Fecha Final</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style12">
                            &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="primera" runat="server" 
                                ImageUrl="~/sw/img/hilda/botones/primera.jpg" />
                        </td>
                        <td>
                            <asp:ImageButton ID="anterior" runat="server" 
                            ImageUrl="~/sw/img/hilda/botones/anterior.jpg" />
                        </td>
                        <td class="style13">
                            <asp:TextBox ID="fechade" runat="server" CssClass="texto" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="fechade_CalendarExtender" runat="server" 
                                Enabled="True" Format="d/M/yyyy" TargetControlID="fechade">
                            </cc1:CalendarExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="fechaa" runat="server" CssClass="texto" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="fechaa_CalendarExtender" runat="server" 
                                Enabled="True" Format="d/M/yyyy" TargetControlID="fechaa">
                            </cc1:CalendarExtender>
                        </td>
                        <td>
                            <asp:ImageButton ID="siguiente" runat="server" 
                            ImageUrl="~/sw/img/hilda/botones/siguiente.jpg" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ultima" runat="server" 
                            ImageUrl="~/sw/img/hilda/botones/ultima.jpg" />
                        </td>
                        <td>
                            <asp:ImageButton ID="actualizar" runat="server" 
                                ImageUrl="~/sw/img/hilda/botones/actualizar.jpg" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style12">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td class="style13">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" ForeColor="#333333" GridLines="None" 
                            Width="900px" DataKeyNames="id">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="No de Orden" />
                                    <asp:BoundField DataField="fechaorden" HeaderText="Fecha Orden" 
                                                    DataFormatString="{0:dd/MMMM/yyyy}" />
                                    <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MMMM/yyyy}" 
                                                    HeaderText="Fecha Pago" />
                                    <asp:BoundField DataField="puntos" 
                                                        HeaderText="Puntaje" />
                                    <asp:BoundField DataField="total" DataFormatString="{0:c}" HeaderText="Total" />
                                    <asp:BoundField DataField="statuspago" HeaderText="Pago" />
                                    <asp:BoundField DataField="statusentrega" HeaderText="Entrega" />
                                    <asp:BoundField DataField="referencia" HeaderText="Referencia" />
                                    <asp:BoundField HeaderText="Período" />
                                    <asp:CommandField HeaderText="Referencia" ShowSelectButton="True" />
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
        <td align="center">
            <asp:Panel ID="PanelDetalle" runat="server" Visible="False">
            <table width="800" border="0" cellspacing="0" cellpadding="5">
    <tr class="subtitulo">
      <td>INFORMACIÓN PERSONAL</td>
      <td>Orden:
          <asp:Label ID="orden" runat="server"></asp:Label>
        </td>
      <td>ARTÍCULOS</td>
    </tr>
    <tr>
      <td align="left" colspan="2">Núm. Distribuidor:
          <asp:Label ID="numDistribuidor" runat="server"></asp:Label>
        </td>
      <td align="left">Código:
          <asp:Label ID="codigoArticulo" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
      <td bgcolor="#CEE1FF" align="left" colspan="2">Nombre:
          <asp:Label ID="nombreDistribuidor" runat="server"></asp:Label>
        </td>
      <td bgcolor="#CEE1FF" align=left>Descripción:
          <asp:Label ID="descripcion" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
      <td align="left" colspan="2">Dirección de envío:
          <asp:Label ID="direccion1" runat="server"></asp:Label>
        </td>
      <td align="left">Cantidad:
          <asp:Label ID="cantidad" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
      <td bgcolor="#CEE1FF" align="left" colspan="2">
          <asp:Label ID="direccion2" runat="server"></asp:Label>
        </td>
      <td bgcolor="#CEE1FF" align="left">Puntaje:
          <asp:Label ID="puntaje" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
      <td align="left" colspan="2">Fecha de la Orden:
          <asp:Label ID="fechaorden" runat="server"></asp:Label>
        </td>
      <td align="left">Precio Unitario:
          <asp:Label ID="precioUnitario" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
      <td bgcolor="#CEE1FF" align="left" colspan="2">Fecha de Pago:
          <asp:Label ID="fechapago" runat="server"></asp:Label>
        </td>
      <td bgcolor="#CEE1FF" align="left">Total:
          <asp:Label ID="total" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
      <td align="left" colspan="2">Origen de la Orden:
          <asp:Label ID="origen" runat="server"></asp:Label>
        </td>
      <td align="left">&nbsp;</td>
    </tr>
                <tr>
                    <td align="left" colspan="2" bgcolor="#CEE1FF">
                        Período Incentivable:
                        <asp:Label ID="periodo" runat="server"></asp:Label>
                    </td>
                    <td bgcolor="#CEE1FF">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        Estatus de la Orden:
                        <asp:Label ID="status" runat="server"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" align="left" bgcolor="#CEE1FF">
                        Operador:
                        <asp:Label ID="operador" runat="server"></asp:Label>
                    </td>
                    <td align="right" bgcolor="#CEE1FF">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                    <td align="right">
                        <asp:ImageButton ID="ImageButton1" runat="server" 
                            ImageUrl="~/sw/img/hilda/botones/anterior.jpg" />
                    </td>
                </tr>
  </table>
            </asp:Panel>
        </td>
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

