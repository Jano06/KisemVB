<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="inventarios_movimientos.aspx.vb" Inherits="sw_inventarios_movimientos" title="Página sin título" %>

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
            <td class="titulobienvenida" colspan="3">
                Movimiento de Productos entre Bodegas<asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Width="200px"></asp:Label>
            </td>
            <td class="style8">
                &nbsp;</td>
            <td>
                <asp:Label ID="Label4" runat="server" Width="350px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="textonuevo">
                Cantidad a Mover:</td>
            <td class="style8">
                <asp:TextBox ID="cantidad" runat="server" MaxLength="50" TabIndex="1" 
                    Width="350px"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="cantidad_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Numbers" TargetControlID="cantidad">
                </cc1:FilteredTextBoxExtender>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="cantidad" EnableClientScript="true" 
                    ErrorMessage="Cantidad Inicial es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="textonuevo">
                Producto:</td>
            <td>
                <asp:DropDownList ID="productos" runat="server" Width="350px" 
                TabIndex="2">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="textonuevo">
                Bodega proveedora:</td>
            <td>
                <asp:DropDownList ID="bodegasprov" runat="server" Width="350px" 
                TabIndex="3" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="textonuevo">
                Bodega receptora:</td>
            <td>
                <asp:DropDownList ID="bodegasrecep" runat="server" Width="350px" 
                TabIndex="4">
                </asp:DropDownList>
            </td>
            <td>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="bodegasrecep" ControlToValidate="bodegasprov" 
                    ErrorMessage="Deben ser bodegas diferentes" Operator="NotEqual"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="textonuevo">
                Fecha:</td>
            <td>
                <asp:TextBox ID="fecha" runat="server" CssClass="texto" 
                    Width="100px" ValidationGroup="comprasarray" TabIndex="5"></asp:TextBox>
                            <cc1:CalendarExtender ID="fecha_CalendarExtender" runat="server" 
                                Enabled="True" Format="dd/M/yyyy" TargetControlID="fecha">
                            </cc1:CalendarExtender>
                                    </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="fecha" EnableClientScript="true" 
                    ErrorMessage="Fecha es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" TabIndex="6" Text="Guardar" />
                &nbsp;&nbsp;&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="subtitulo" colspan="2">
                &nbsp;</td>
            <td class="subtitulo">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="proveedor" HeaderText="Proveedor" />
                        <asp:BoundField DataField="receptor" HeaderText="Receptor" />
                        <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MMMM/yyyy}" HeaderText="Fecha" />
                        <asp:BoundField DataField="producto" HeaderText="Producto" />
                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
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
                &nbsp;</td>
            <td class="style8">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

