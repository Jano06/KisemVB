<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="genealogia.aspx.vb" Inherits="sw_genealogia" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style11
        {
            width: 600px;
        }
        
        .style9
    {
        height: 22px;
    }
        .style14
    {
        width: 100%;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
    <tr>
        <td class="titulobienvenida">
            Genealogía<br />
            <asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
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
                    <td class="textonuevo">
                Asociado</td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" Width="350px"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" 
                    enabled="True" servicepath="~/BuscarAsociados.asmx" minimumprefixlength="2" 
                    servicemethod="ObtListaAsociados" enablecaching="true" targetcontrolid="TextBox1" 
                    usecontextkey="True" completionsetcount="10"
                    completioninterval="200">
                        </cc1:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="Button2" runat="server" Text="Ver Genealogía" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
&nbsp;&nbsp;<table class="style14">
                <tr>
                    <td>
                            Izquierdo</td>
                    <td>
                        Derecho</td>
                </tr>
                <tr>
                    <td valign="top">
                            <asp:GridView ID="GridIzq" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" ForeColor="#333333" GridLines="None" 
                                Width="600px">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="Asociado" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
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
                            <asp:GridView ID="GridDer" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" ForeColor="#333333" GridLines="None" 
                            Width="600px">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="Asociado" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
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
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="Panel1" runat="server" Visible="False">
                <table style="width: 823px">
                    <tr>
                        <td class="style9">
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
</asp:Content>

