<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="comisiones_eliminar.aspx.vb" Inherits="sw_comisiones_eliminar" title="Página sin título" %>

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
            Eliminar Comisiones<br />
            <asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="500px">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:BoundField DataField="corte" HeaderText="Corte" />
                    <asp:BoundField DataField="de" HeaderText="De" DataFormatString="{0:dd/MMMM/yyyy}" />
                    <asp:BoundField DataField="a" HeaderText="A" DataFormatString="{0:dd/MMMM/yyyy}" />
                    <asp:CommandField SelectText="Eliminar" ShowSelectButton="True" />
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
            <asp:Button ID="eliminar" runat="server" Text="Eliminar Todo" />
            <cc1:ConfirmButtonExtender ID="eliminar_ConfirmButtonExtender" runat="server" 
                ConfirmText="¿Está seguro de querer eliminar todos los períodos de comisiones?" 
                Enabled="True" TargetControlID="eliminar">
            </cc1:ConfirmButtonExtender>
        </td>
        <td>
            &nbsp;</td>
    </tr>
</table>
</asp:Content>

