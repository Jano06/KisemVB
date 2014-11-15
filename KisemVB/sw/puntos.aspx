<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="puntos.aspx.vb" Inherits="sw_puntos" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">





        .style8
        {
            width: 147px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
    <tr>
        <td class="titulobienvenida" colspan="2">
                Puntos<asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
&nbsp;<br />
        </td>
    </tr>
    <tr>
        <td class="textonuevo">
                Asociado</td>
        <td>
                <asp:TextBox ID="txtasociado" runat="server" Width="350px"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="txtasociado_AutoCompleteExtender" runat="server" 
                    enabled="True" servicepath="~/BuscarAsociados.asmx" minimumprefixlength="2" 
                    servicemethod="ObtListaAsociados" enablecaching="true" targetcontrolid="txtasociado" 
                    usecontextkey="True" completionsetcount="10"
                    completioninterval="200">
                </cc1:AutoCompleteExtender>
&nbsp;*Opcional<asp:Button ID="Button2" runat="server" Text="Ver Puntos" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
                &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style8" colspan="2">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="Num" />
                    <asp:BoundField DataField="asociado" HeaderText="Asociado" />
                    <asp:BoundField DataField="izquierdo" HeaderText="Izquierdo" />
                    <asp:BoundField DataField="derecho" HeaderText="Derecho" />
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
        <td class="style8" colspan="2">
                &nbsp;</td>
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
                &nbsp;</td>
    </tr>
</table>
</asp:Content>

