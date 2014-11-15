<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="compras_entrega.aspx.vb" Inherits="sw_compras_entrega" title="Página sin título" %>

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
            Entrega de compras. 
            <br />
            <asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
            <br />
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td class="textonuevo2">
            Mostrar
            <asp:RadioButton ID="todos" runat="server" AutoPostBack="True" 
                GroupName="MOSTRAR" Text="Todos" />
&nbsp;<asp:RadioButton ID="pendientes" runat="server" AutoPostBack="True" Checked="True" 
                GroupName="MOSTRAR" Text="Pendientes" />
        &nbsp;</td>
        <td class="textonuevo">
            <asp:Button ID="primero" runat="server" Text="&lt;&lt;" />
&nbsp;<asp:Button ID="anterior" runat="server" Text="&lt;" />
        &nbsp;de
            <asp:Label ID="de" runat="server"></asp:Label>
        </td>
        <td class="textonuevo2">
            a
            <asp:Label ID="a" runat="server"></asp:Label>
        &nbsp;<asp:Button ID="siguiente" runat="server" Text="&gt;" />
&nbsp;<asp:Button ID="ultimo" runat="server" Text="&gt;&gt;" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="textonuevo2">
            Referencia:<asp:TextBox ID="referencia" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="referencia_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Numbers" TargetControlID="referencia">
            </cc1:FilteredTextBoxExtender>
&nbsp;<asp:Button ID="Button2" runat="server" Text="Buscar" />
&nbsp;<asp:Button ID="Button3" runat="server" Text="Cancelar" />
        &nbsp;</td>
        <td class="textonuevo">
            &nbsp;</td>
        <td class="textonuevo2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="textonuevo2" colspan="3">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="4">
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
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="Pagado" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox2" runat="server" Text="Entregado" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox3" runat="server" Text="Cancelado" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                <br />
                <asp:Panel ID="Panel1" runat="server" BackColor="White" 
                    CssClass="overtextonuevo">
                </asp:Panel>
                </td>
    </tr>
    <tr>
        <td colspan="4" class="textonuevo">
                <asp:Button ID="Button1" runat="server" Text="Guardar" />
                </td>
    </tr>
    <tr>
        <td colspan="3">
                <asp:GridView ID="GridDetalle" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="1000px" 
                CssClass="texto">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                        <asp:BoundField DataField="paquete" HeaderText="Paquete" />
                        <asp:BoundField DataField="costo" HeaderText="Costo" />
                        <asp:BoundField DataField="puntos" HeaderText="Puntos" />
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                </td>
        <td>
            &nbsp;</td>
    </tr>
</table>
</asp:Content>

