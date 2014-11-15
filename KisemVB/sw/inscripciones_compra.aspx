<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="inscripciones_compra.aspx.vb" Inherits="sw_inscripciones_compra" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style8
        {
            width: 147px;
        }
        .style9
        {
            width: 275px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
        <tr>
            <td class="titulobienvenida" colspan="2">
                Compra de Inscripción<asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
            <td class="titulo">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" CssClass="textogrid" ForeColor="#333333" GridLines="None" 
                    Width="950px">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ApPaterno" HeaderText="Apellido Paterno" />
                        <asp:BoundField DataField="ApMaterno" HeaderText="Apellido Materno" />
                        <asp:BoundField DataField="TelMovil" HeaderText="Celular" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:CommandField SelectText="Registrar Compra" ShowSelectButton="True" />
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
            <td>
                &nbsp;</td>
            <td class="style8">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <table class="style1">
                    <tr>
                        <td class="style9">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" CssClass="textogrid" ForeColor="#333333" GridLines="None" 
                    Width="450px">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="Paquete"></asp:BoundField>
                        <asp:BoundField DataField="nombre" />
                        <asp:BoundField DataField="puntos" HeaderText="Puntos" />
                        <asp:BoundField DataField="costo" DataFormatString="{0:c}" HeaderText="Costo" />
                        <asp:TemplateField>
                             <ItemTemplate>
                            <asp:TextBox runat="server" ID="TextBox1" Text="0" Width="30"/>
                             <cc1:FilteredTextBoxExtender ID="TextBox1_FilteredTextBoxExtender" 
                                runat="server" Enabled="True" FilterType="Numbers" TargetControlID="TextBox1">
                            </cc1:FilteredTextBoxExtender>
                        </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                        </td>
                        <td valign="top">
                <asp:Button ID="Button1" runat="server" TabIndex="30" Text="Guardar" Visible="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
</asp:Content>

