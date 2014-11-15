<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="comisiones_ajustes.aspx.vb" Inherits="sw_comisiones_ajustes" title="Página sin título" %>

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
            Ajuste de Comisiones<br />
            <asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
    </tr>
    <tr>
        <td class="textonuevo">
                Corte</td>
        <td>
            <asp:DropDownList ID="cortes" runat="server" Width="350px">
            </asp:DropDownList>
                    &nbsp;&nbsp;</td>
    </tr>
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
&nbsp;<asp:Button ID="Button5" runat="server" Text="ver Pagos" ValidationGroup="verpagos" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="TextBox1" ErrorMessage="Campo necesario"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="TextBox1" ErrorMessage="Campo necesario" 
                ValidationGroup="verpagos"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="textonuevo">
            Bono</td>
        <td>
            <asp:DropDownList ID="bonos" runat="server" Width="350px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="textonuevo">
            Monto</td>
        <td>
                <asp:TextBox ID="monto" runat="server" MaxLength="50" TabIndex="1" 
                    Width="350px"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="monto_FilteredTextBoxExtender" runat="server" 
                    Enabled="True" FilterType="Numbers" TargetControlID="monto">
                </cc1:FilteredTextBoxExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="monto" ErrorMessage="Campo necesario"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="textonuevo">
            &nbsp;</td>
        <td>
            <asp:Button ID="Button6" runat="server" Text="Actualizar" />
            <cc1:ConfirmButtonExtender ID="Button6_ConfirmButtonExtender" runat="server" 
                ConfirmText="¿Seguro desea modificar la comisión seleccionada?" Enabled="True" 
                TargetControlID="Button6">
            </cc1:ConfirmButtonExtender>
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
                    <asp:BoundField DataField="asociado" HeaderText="Asociado" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="bono" HeaderText="Bono" />
                    <asp:BoundField DataField="monto" DataFormatString="{0:c}" HeaderText="Monto" />
                    <asp:BoundField DataField="de" DataFormatString="{0:dd/MMMM/yyyy}" HeaderText="De" />
                    <asp:BoundField DataField="a" DataFormatString="{0:dd/MMMM/yyyy}" HeaderText="A" />
                    <asp:BoundField DataField="corte" HeaderText="Corte" />
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
</table>
</asp:Content>

