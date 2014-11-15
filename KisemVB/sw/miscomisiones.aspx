<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="miscomisiones.aspx.vb" Inherits="sw_miscomisiones" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
        {
        }
        .style3
        {
        }
            
        .style1
        {
            width: 100%;
            padding:0;
            
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
                <asp:Label ID="mensajes" runat="server" 
                CssClass="titulobienvenida"></asp:Label>
                                            </td>
    </tr>
    <tr>
        <td class="celdamarco">
    <table align="left" class="texto">
    <tr>
        <td class="style2" colspan="2" valign="top">
                                                <span  class="texto" __designer:mapid="96">Correr comisiones del período&nbsp;Actual
                                            &nbsp;&nbsp;
                                                </span>
        </td>
    </tr>
    <tr>
        <td class="textonuevo">
            <span  class="textonuevo" __designer:mapid="96">Del día&nbsp; </span><br />
        </td>
        <td>
                                                <span  class="textonuevo2" __designer:mapid="96">
            <asp:Label ID="de" runat="server"></asp:Label>
                                                </span>
        </td>
    </tr>
    <tr>
        <td class="textonuevo">
            Al día </td>
        <td>
                                                <span  class="textonuevo2" __designer:mapid="96">
            <asp:Label ID="a" runat="server"></asp:Label>
                                                </span>
        </td>
    </tr>
    <tr>
        <td class="style3">
            &nbsp;</td>
        <td>
                                                &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            &nbsp;</td>
        <td>
                                                <span  class="texto" __designer:mapid="96">
                                                <asp:Button ID="Button1" runat="server" TabIndex="30" 
    Text="Iniciar Cálculo" ValidationGroup="pago" OnClientClick="this.disabled=true;this.value = 'Generando Reporte...'" UseSubmitBehavior="false" />
                                                </span>
        </td>
    </tr>
    <tr>
        <td class="style3" colspan="2">
            <asp:Label ID="recordatorio" runat="server" CssClass="subtitulo"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="style3">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px" 
                    ShowFooter="True">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="asociado" HeaderText="Asociado" />
                        <asp:BoundField DataField="bono" HeaderText="Bono" />
                        <asp:BoundField DataField="monto" DataFormatString="{0:c}" HeaderText="Monto" />
                        <asp:BoundField DataField="de" DataFormatString="{0:dd/MMMM/yyyy}" HeaderText="De" />
                        <asp:BoundField DataField="a" DataFormatString="{0:dd/MMMM/yyyy}" HeaderText="A" />
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

