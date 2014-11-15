<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="ciclos.aspx.vb" Inherits="sw_periodos" title="Página sin título" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style11
        {
            width: 600px;
        }
        .style12
        {
            text-align: right;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: Gray;
            vertical-align: top;
            width: 221px;
        }
        .style13
        {
            width: 221px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
        <tr>
            <td class="titulobienvenida">
                Ciclos de Calificación<asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
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
                        <td class="style12">
                            Fecha Inicio &nbsp;&nbsp;</td>
                        <td>
                            <asp:TextBox ID="inicio" runat="server" CssClass="texto" Width="100px" 
                                Height="25px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="inicio" ErrorMessage="Campo necesario"></asp:RequiredFieldValidator>
                            <cc1:CalendarExtender ID="inicio_CalendarExtender" runat="server" 
                                Enabled="True" Format="dd/M/yyyy" TargetControlID="inicio" >
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="style12">
                            Fecha Final &nbsp;&nbsp;</td>
                        <td>
                            <asp:TextBox ID="final" runat="server" CssClass="texto" Width="100px" Height="25px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="final" ErrorMessage="Campo necesario"></asp:RequiredFieldValidator>
                            <cc1:CalendarExtender ID="final_CalendarExtender" runat="server" 
                                Enabled="True" Format="dd/M/yyyy" TargetControlID="final">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="style13">
                            <asp:Button ID="Button2" runat="server" Text="Registrar" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
&nbsp;&nbsp;<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" ForeColor="#333333" GridLines="None" Width="600px">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:BoundField DataField="inicio" HeaderText="Inicio" 
                                        DataFormatString="{0:dd/MMMM/yyyy}" />
                                    <asp:BoundField DataField="fin" HeaderText="Fin" DataFormatString="{0:dd/MMMM/yyyy}" />
                                    <asp:CommandField SelectText="eliminar" ShowSelectButton="True" />
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
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

