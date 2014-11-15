<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="miorganizacion.aspx.vb" Inherits="sw_miorganizacion" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


    .style11
        {
            width: 719px;
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
            <img alt="" src="img/hilda/titulos/miorganizacion.jpg" 
                style="width: 252px; height: 33px" /></td>
    </tr>
    <tr>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td class="celdamarco">
    <table align="left" class="texto">
        <tr>
            <td class="subtitulo" colspan="3">
                Ver Mi Organización hasta el día
                                                <span  class="texto" __designer:mapid="1f9">
                                            <asp:TextBox ID="fecha" runat="server" 
                    MaxLength="50" TabIndex="4" 
                    Width="150px" 
    ValidationGroup="pago" AutoPostBack="True"></asp:TextBox>
                                            <cc1:CalendarExtender ID="TextBox26_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="fecha" Format="yyyy/M/dd">
                    </cc1:CalendarExtender>
                                            &nbsp;</span><asp:ImageButton ID="ImageButton1" 
                    runat="server" ImageUrl="~/sw/img/hilda/botones/exportaraexcel.jpg" />
                                                   </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
                </td>
        </tr>
        <tr>
            <td colspan="3">
                <table class="style11">
                </table>
            </td>
        </tr>
        <tr>
            <td class="textonuevo2" valign="top" colspan="3">
                <asp:GridView ID="GridUnico" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" CssClass="textogrid" ForeColor="#333333" GridLines="None" 
                    Width="984px">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="asociado" />
                        <asp:BoundField DataField="nombreizq" HeaderText="Izquierdo">
                            <ItemStyle Width="250px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombreder" HeaderText="Derecho" />
                        <asp:BoundField DataField="status" HeaderText="Status" />
                        <asp:BoundField DataField="rango" HeaderText="Rango"></asp:BoundField>
                        <asp:BoundField DataField="fecha" HeaderText="Última Compra" 
                            DataFormatString="{0:dd/MMMM/yyyy}" />
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
            <td class="textonuevo2" valign="top">
                &nbsp;</td>
            <td class="textonuevo2" valign="top">
                &nbsp;</td>
            <td class="textonuevo2" valign="top">
                &nbsp;</td>
        </tr>
        <tr>
            <td valign="top">
                &nbsp;</td>
            <td valign="top">
                &nbsp;</td>
            <td valign="top">
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

