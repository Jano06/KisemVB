<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="reporteavancesrangos.aspx.vb" Inherits="sw_reporteavancesrangos" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .style11
        {
            width: 719px;
        }
        .style12
    {
        text-align: right;
        font-family: Arial;
        font-size: medium;
        font-weight: bold;
        color: Gray;
        vertical-align: top;
        width: 174px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
    <tr>
        <td class="titulobienvenida">
            Avances por Ciclos de Calificación<asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
    </tr>
    <tr>
        <td>
                &nbsp;&nbsp;&nbsp;
                </td>
    </tr>
    <tr>
        <td>
            <table class="style11">
                <tr>
                    <td class="style12">
                        Ciclo de Calificación:&nbsp;&nbsp;</td>
                    <td>
                        <asp:DropDownList ID="ciclos" runat="server">
                        </asp:DropDownList>
&nbsp;
                        <asp:Button ID="Button2" runat="server" Text="Generar" OnClientClick="this.disabled=true;this.value = 'Enviando...'" UseSubmitBehavior="false" />
                    </td>
                </tr>
                <tr>
                    <td class="style12">
                        Asociado:&nbsp;&nbsp;</td>
                    <td>
                <asp:TextBox ID="txtasociado" runat="server" Width="350px"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="txtasociado_AutoCompleteExtender" runat="server" 
                    enabled="True" servicepath="~/BuscarAsociados.asmx" minimumprefixlength="2" 
                    servicemethod="ObtListaAsociados" enablecaching="true" targetcontrolid="txtasociado" 
                    usecontextkey="True" completionsetcount="10"
                    completioninterval="200">
                </cc1:AutoCompleteExtender>
                        *Opcional</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="textonuevo2">
            <asp:Literal ID="detalle" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td>
&nbsp;&nbsp;<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" ForeColor="#333333" 
                GridLines="None" Width="948px" CssClass="textogrid">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:BoundField DataField="asociado" HeaderText="Asociado" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" >
                        <ItemStyle Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="directosi" HeaderText="1 Directos I" />
                    <asp:BoundField DataField="directosd" HeaderText="1 Directos D" />
                    <asp:BoundField DataField="puntosi" HeaderText="1 Puntos I" />
                    <asp:BoundField DataField="puntosd" HeaderText="1 Puntos D" />
                    <asp:BoundField DataField="mispuntos" HeaderText="1 Mis Puntos" />
                     <asp:BoundField DataField="directosi2" HeaderText="2 Directos I" />
                    <asp:BoundField DataField="directosd2" HeaderText="2 Directos D" />
                    <asp:BoundField DataField="puntosi2" HeaderText="2 Puntos I" />
                    <asp:BoundField DataField="puntosd2" HeaderText="2 Puntos D" />
                    <asp:BoundField DataField="mispuntos2" HeaderText="2 Mis Puntos" />
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
    <tr>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td>
                &nbsp;</td>
    </tr>
</table>
</asp:Content>

