<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="reporte_cheques.aspx.vb" Inherits="sw_reporte_cheques" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

                
        .style1
        {
            width: 100%;
            padding:0;
            
        }
        
        .style11
        {
            width: 807px;
        }
        
        


        .style8
        {
            width: 147px;
        }
                
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
        <tr>
            <td align="center">
                <img alt="" src="img/hilda/titulos/miscompras.jpg" 
                style="width: 252px; height: 33px" /><asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="celdamarco">
                <table align="left" class="texto">
                    <tr>
                        <td>
                            <table class="style11">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:GridView ID="GridCheques" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" ForeColor="#333333" GridLines="None" 
                            Width="900px" CssClass="textochico">
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>
                                                <asp:BoundField DataField="id" />
                                                <asp:BoundField DataField="nombre" HeaderText="Asociado" />
                                                <asp:BoundField DataField="rangopago" 
                                                    HeaderText="Rango de Pago" />
                                                <asp:BoundField DataField="incentivos" 
                                                        HeaderText="Incentivos" DataFormatString="{0:c}" />
                                                <asp:BoundField DataField="isr" DataFormatString="{0:c}" 
                                                    HeaderText="Retención ISR" />
                                                <asp:BoundField DataField="iva" HeaderText="IVA" DataFormatString="{0:c}" />
                                                <asp:BoundField DataField="retencioniva" HeaderText="Retención IVA" 
                                                    DataFormatString="{0:c}" />
                                                <asp:BoundField DataField="importe" HeaderText="Importe" 
                                                    DataFormatString="{0:c}" />
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
                        <td class="style8">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">
                <img alt="" src="img/hilda/sombra.png" style="width: 323px; height: 80px" /></td>
        </tr>
    </table>
</asp:Content>

