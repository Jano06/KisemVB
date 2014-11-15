<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="reporteavancerangosciclo.aspx.vb" Inherits="sw_reporteavancerangosciclo" title="Página sin título" %>

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
        <td class="titulobienvenida" colspan="2">
            Avances de Rangos por Ciclos de Calificación<asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
                &nbsp;&nbsp;&nbsp;
                </td>
    </tr>
    <tr>
        <td colspan="2">
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
            </table>
        </td>
    </tr>
    <tr>
        <td class="textonuevo2" valign="top">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" CssClass="textogrid" ForeColor="#333333" GridLines="None" 
                    Width="266px">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:CommandField SelectText="detalle" ShowSelectButton="True" />
                                <asp:BoundField DataField="rango" HeaderText="Rango"></asp:BoundField>
                                <asp:BoundField DataField="cuenta" HeaderText="Cuenta" />
                            </Columns>
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                    </td>
        <td class="textonuevo2" valign="top">
                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" CssClass="textogrid" ForeColor="#333333" GridLines="None" 
                    Width="638px">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="Id">
                                    <ItemStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" >
                                    <ItemStyle Width="300px" />
                                </asp:BoundField>
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
        <td colspan="2">
&nbsp;&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
                &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
                &nbsp;</td>
    </tr>
</table>
</asp:Content>

