<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="comisiones.aspx.vb" Inherits="sw_comisiones2" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style9
    {
        height: 22px;
    }
        .style10
        {
            width: 141px;
        }
        </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
     <table align="left" class="texto">
        <tr>
            <td class="titulobienvenida">
            <span  class="titulobienvenida">Comisiones</span>&nbsp;<br />
                                           
                                            &nbsp;
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                        <ContentTemplate>
                                         <asp:Label ID="mensajes" runat="server" CssClass="titulo"></asp:Label>
                                            <p class="textonuevo">
                                                <span  class="texto">Correr comisiones del período de
                                            <asp:TextBox ID="de" runat="server" MaxLength="50" TabIndex="4" 
                    Width="150px" 
    ValidationGroup="pago"></asp:TextBox>
                                            <cc1:CalendarExtender ID="TextBox26_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="de" Format="dd/M/yyyy">
                    </cc1:CalendarExtender>
                                            &nbsp;a&nbsp;
                                            <asp:TextBox ID="a" runat="server" MaxLength="50" TabIndex="4" 
                    Width="150px" 
    ValidationGroup="pago"></asp:TextBox> <cc1:CalendarExtender ID="TextBox26_CalendarExtender0" runat="server" 
                    Enabled="True" TargetControlID="a" Format="dd/M/yyyy">
                    </cc1:CalendarExtender><asp:Button ID="Button1" runat="server" TabIndex="30" 
    Text="Iniciar" ValidationGroup="pago" style="width: 50px" /></span></p>
                                            <p>
                                                &nbsp;Número de siguiente período:
                                                <asp:Label ID="periodo" runat="server"></asp:Label>
                                                <asp:Button ID="Button3" runat="server" Text="No oprimir" />
                                            </p>
                                            <cc1:ConfirmButtonExtender ID="Button1_ConfirmButtonExtender" runat="server" 
                        ConfirmText="¿Seguro que desea correr las comisiones del período seleccionado?" 
                        Enabled="True" TargetControlID="Button1">
                                            </cc1:ConfirmButtonExtender>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <br />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ValidationGroup="pago" />
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="de" Display="None" 
                        ErrorMessage="Fecha inicial es un campo obligatorio" 
    ValidationGroup="pago"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="a" Display="None" 
                        ErrorMessage="Fecha Final es un campo obligatorio" 
    ValidationGroup="pago"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="de" 
                        Display="None" ErrorMessage="Fecha Inicial inválida" MaximumValue="1/1/2043" 
                        MinimumValue="1/1/1900" Type="Date" 
    ValidationGroup="pago"></asp:RangeValidator>
                                            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="a" 
                        Display="None" ErrorMessage="Fecha Final inválida" MaximumValue="1/1/2043" 
                        MinimumValue="1/1/1900" Type="Date" 
    ValidationGroup="pago"></asp:RangeValidator>
                                        </ContentTemplate>
    </asp:UpdatePanel>
 
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="overlay" style="bottom: 0px; top: 0px; right: 0px; left: 0px" />
            <div class="overlayContent">
                <h2>Procesando Comisiones...</h2>
                <img src="img/ajax-loader.gif" width="126" height="22"  alt="Loading" border="0" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress></td>
                                    <td class="titulo">
                                        &nbsp;</td>
                                </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server" Visible="False">
                    <table style="width: 823px">
                        <tr>
                            <td class="style9">
                                <table class="style1">
                                    <tr>
                                        <td>
                                            Total de Compras</td>
                                        <td class="style10">
                                            $<asp:Label ID="totalcompras" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td rowspan="12">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Total de Comisiones</td>
                                        <td class="style10">
                                            $<asp:Label ID="totalcomisiones" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Bono 1</td>
                                        <td class="style10">
                                            $<asp:Label ID="lblbono1" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono1" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Bono 2</td>
                                        <td class="style10">
                                            $<asp:Label ID="lblbono2" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono2" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Bono 3</td>
                                        <td class="style10">
                                            $<asp:Label ID="lblbono3" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono3" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Bono 4</td>
                                        <td class="style10">
                                            $<asp:Label ID="lblbono4" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono4" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Bono 5</td>
                                        <td class="style10">
                                            $<asp:Label ID="lblbono5" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono5" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Bono 6</td>
                                        <td class="style10">
                                            $<asp:Label ID="lblbono6" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono6" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Bono 7</td>
                                        <td class="style10">
                                            $<asp:Label ID="lblbono7" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono7" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Bono 8</td>
                                        <td class="style10">
                                            $<asp:Label ID="lblbono8" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="porcbono8" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button2" runat="server" TabIndex="30" 
    Text="Ver Detalle" ValidationGroup="pago" />
                                        </td>
                                        <td class="style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td class="style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
                                    <td class="titulo">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
            
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
            
                <br />
            </td>
        </tr>
    </table>
</asp:Content>

