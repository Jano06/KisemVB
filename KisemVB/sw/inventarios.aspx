<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="inventarios.aspx.vb" Inherits="sw_inventarios" title="Página sin título" %>

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
            Inventarios<asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
            &nbsp;</td>
        <td class="titulo">
                &nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Width="95px"></asp:Label>
        </td>
        <td class="style8">
                &nbsp;</td>
        <td>
            <asp:Label ID="Label4" runat="server" Width="350px"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="textonuevo">
            Bodega:</td>
        <td class="style8">
            <asp:DropDownList ID="bodegas" runat="server" Width="230px" 
                TabIndex="20">
            </asp:DropDownList>
        </td>
        <td>
            Seleccione las columnas a mostrar:</td>
    </tr>
    <tr>
        <td class="textonuevo">
            Categoría:</td>
        <td>
            <asp:DropDownList ID="categorias" runat="server" Width="230px" 
                TabIndex="20">
            </asp:DropDownList>
        </td>
        <td>
            <asp:CheckBox ID="Codigo" runat="server" Checked="True" Text="Código" />
&nbsp;<asp:CheckBox ID="Descripcion" runat="server" Checked="True" Text="Descripción" />
&nbsp;
            <asp:CheckBox ID="Bodega" runat="server" Checked="True" Text="Bodega" />
&nbsp;<asp:CheckBox ID="Existencia" runat="server" Checked="True" Text="Existencia" />
        </td>
    </tr>
    <tr>
        <td class="textonuevo">
            Producto:</td>
        <td>
            <asp:TextBox ID="Producto" runat="server" Width="230px"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="Button1" runat="server" TabIndex="30" Text="Filtrar" />
        &nbsp;<asp:ImageButton ID="ImageButton1" 
                    runat="server" ImageUrl="~/sw/img/hilda/botones/exportaraexcel.jpg" 
                                ValidationGroup="filtro" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
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
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px" 
                ShowFooter="True">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:BoundField DataField="producto" HeaderText="Código" />
                    <asp:BoundField DataField="detalles" HeaderText="Descripción" />
                    <asp:BoundField DataField="bodega" HeaderText="Bodega" />
                    <asp:BoundField DataField="cantidad" HeaderText="Existencia" />
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

