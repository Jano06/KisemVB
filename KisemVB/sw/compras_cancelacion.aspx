<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="compras_cancelacion.aspx.vb" Inherits="sw_compras_cancelacion" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        
    .style1
    {
        width: 100%;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
        <tr>
            <td class="titulobienvenida" colspan="4">
                Cancelación de compras.
                <asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="textonuevo2">
                Orden de Compra:<asp:TextBox ID="compra" runat="server"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="compra_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Numbers" TargetControlID="compra">
                </cc1:FilteredTextBoxExtender>
&nbsp;<asp:Button ID="Button2" runat="server" Text="Buscar" />
&nbsp;<asp:Button ID="Button3" runat="server" Text="Cancelar" />
            </td>
            <td class="textonuevo">
            &nbsp;</td>
            <td class="textonuevo2">
            &nbsp;</td>
            <td>
            &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <br />
                <asp:Panel ID="Panel1" runat="server" BackColor="White" 
                    CssClass="overtextonuevo">
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="textonuevo">
                <asp:GridView ID="GridCarrito" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="1000px" 
                CssClass="texto">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="compra" HeaderText="OC" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/sw/img/lupa.png" />
                                <cc1:PopupControlExtender 
                                ID="PopupControlExtender1"
                                   runat="server"
                                   DynamicServiceMethod="GetDynamicContent"
                                   DynamicContextKey='<%# Eval("compra") %>'
                                   DynamicControlID="Panel1"
                                   TargetControlID="Image1"
                                   PopupControlID="Panel1"
                                   Position="right">
                                </cc1:PopupControlExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="asociado" HeaderText="Asociado" HtmlEncode="False" 
                            HtmlEncodeFormatString="False" />
                        <asp:BoundField DataField="fechaorden" HeaderText="Fecha de Orden" 
                            DataFormatString="{0:dd/MMMM/yyyy}" />
                        <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:c}" />
                        <asp:BoundField DataField="statuspago" HeaderText="Status de Pago" />
                        <asp:BoundField DataField="statusentrega" HeaderText="Status de Entrega" />
                        <asp:CommandField SelectText="Cancelar Compra" ShowSelectButton="True" />
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
            <td colspan="3">
                &nbsp;</td>
            <td>
            &nbsp;</td>
        </tr>
    </table>
</asp:Content>

