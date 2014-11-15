<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="funciones.aspx.vb" Inherits="sw_funciones" title="Página sin título" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="3" height="200" width="750">
        <tr>
            <td class="titulobienvenida" colspan="3" style="height: 41px; text-align: left">
                Funciones<br />
                <asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
            &nbsp;</td>
        </tr>
        <tr>
            <td class="textonuevo">Administrador</td>
            <td style="height: 14px; text-align: left" width="250">
                <asp:DropDownList ID="drp_nombre" runat="server" AutoPostBack="True" 
                    CssClass="Subtitulo" Width="240px">
                </asp:DropDownList>
            </td>
            <td style="width: 232px; height: 14px; text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 252px; height: 17px">
            </td>
            <td style="height: 17px">
            </td>
            <td style="width: 232px; height: 17px; text-align: left">
                <asp:CheckBox ID="chk_todos" runat="server" AutoPostBack="True" 
                    CssClass="textonuevo2" Text="Selecciona todos" Visible="False" 
                    Width="200px" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                <asp:GridView ID="grid_funciones" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="386px" 
                    CssClass="textogrid">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="funcion" HeaderText="Función" />
                        <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
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
            <td style="width: 252px; height: 31px">
            </td>
            <td style="text-align: right">
                <asp:Button ID="btn_eliminar" runat="server" Text="Actualizar" 
                    Visible="False" />
            </td>
            <td style="width: 232px">
            </td>
        </tr>
        <tr>
            <td style="width: 252px; height: 10px">
            </td>
            <td align="right" style="height: 10px">
            </td>
            <td style="width: 232px; height: 10px">
            </td>
        </tr>
    </table>
</asp:Content>

