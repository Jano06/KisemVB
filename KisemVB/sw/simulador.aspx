<%@ Page Language="VB" MasterPageFile="~/sw/MasterPage.master" AutoEventWireup="false" CodeFile="simulador.aspx.vb" Inherits="sw_simulador" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
    <tr>
        <td class="titulo" colspan="2">
            Simulador Bono 4<asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:Label ID="mensajes" runat="server" CssClass="error"></asp:Label>
&nbsp;</td>
    </tr>
    <tr>
        <td class="style9">
            &nbsp;</td>
        <td class="style8">
                &nbsp;</td>
    </tr>
    <tr>
        <td class="style9">
            <asp:Label ID="Label1" runat="server" Width="200px"></asp:Label>
        </td>
        <td class="style8">
                &nbsp;</td>
    </tr>
    <tr>
        <td class="style9">
            % Balance:</td>
        <td class="style8">
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="balance" runat="server" MaxLength="50" TabIndex="1" 
                    Width="43px">0</asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="balance_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Numbers" TargetControlID="balance">
            </cc1:FilteredTextBoxExtender>
            %</td>
    </tr>
    <tr>
        <td class="style9">
            $ Compras:</td>
        <td class="style8">
            $<asp:TextBox ID="compras" runat="server" MaxLength="50" TabIndex="2" 
                    Width="100px">0</asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="compras_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Numbers" TargetControlID="compras">
            </cc1:FilteredTextBoxExtender>
        </td>
    </tr>
    <tr>
        <td class="style9">
            Distribución de las compras:</td>
        <td class="texto">
            Paq 1:<asp:TextBox ID="paq_1" runat="server" Width="35px" TabIndex="3">25</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="paq_1_FilteredTextBoxExtender" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="paq_1" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    %&nbsp; Paq 2:<asp:TextBox ID="paq_2" runat="server" Width="35px" 
                TabIndex="4">25</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="paq_2_FilteredTextBoxExtender" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="paq_2" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    %Paq 3:<asp:TextBox ID="paq_3" runat="server" Width="35px" TabIndex="5">25</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="paq_3_FilteredTextBoxExtender" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="paq_3" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    %&nbsp; Paq 4:<asp:TextBox ID="paq_4" runat="server" Width="35px" 
                TabIndex="6">25</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="paq_4_FilteredTextBoxExtender" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="paq_4" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    %</td>
    </tr>
    <tr>
        <td class="style9">
            Rangos</td>
        <td class="style8" rowspan="3">
            <table class="style1" style="width: 238%">
                <tr>
                    <td>
                        Asociado</td>
                    <td>
                        Colaborador</td>
                    <td>
                        Ejecutivo</td>
                    <td>
                        Bronce</td>
                    <td>
                        Plata</td>
                    <td>
                        Oro</td>
                    <td>
                        Diamante</td>
                    <td>
                        D Ejecutivo</td>
                    <td>
                        D Internacional</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="por_1" runat="server" Width="35px" TabIndex="7">14</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="por_1_FilteredTextBoxExtender" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="por_1" 
                            ValidChars=".">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="por_2" runat="server" Width="35px" TabIndex="8">14</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="por_2" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="por_3" runat="server" Width="35px" TabIndex="9">14.5</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="por_3" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="por_4" runat="server" Width="35px" TabIndex="10">15</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="por_4" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="por_5" runat="server" Width="35px" TabIndex="11">16</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="por_5" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="por_6" runat="server" Width="35px" TabIndex="12">17</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="por_6" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="por_7" runat="server" Width="35px" TabIndex="13">18</asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="por_7" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="por_8" runat="server" Width="35px" TabIndex="14">18</asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="por_8" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="por_9" runat="server" Width="35px" TabIndex="15">18</asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="por_9" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="dist_1" runat="server" Width="35px" TabIndex="16">0</asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="dist_1" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="dist_2" runat="server" Width="35px" TabIndex="17">0</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="dist_2" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="dist_3" runat="server" Width="35px" TabIndex="18">0</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="dist_3" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="dist_4" runat="server" Width="35px" TabIndex="19">0</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="dist_4" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="dist_5" runat="server" Width="35px" TabIndex="20">0</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="dist_5" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="dist_6" runat="server" Width="35px" TabIndex="21">0</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="dist_6" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="dist_7" runat="server" Width="35px" TabIndex="22">0</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="dist_7" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="dist_8" runat="server" Width="35px" TabIndex="23">0</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="dist_8" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="dist_9" runat="server" Width="35px" TabIndex="24">0</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="dist_9" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="style9">
            % Pago por Rango:</td>
    </tr>
    <tr>
        <td class="style9">
            % Distribución de los Rangos:</td>
    </tr>
    <tr>
        <td class="style9">
            &nbsp;</td>
        <td class="texto">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style9">
            Aumento por paquete:</td>
        <td class="texto">
            Paq 3:<asp:TextBox ID="aumento_3" runat="server" Width="35px" TabIndex="25">1.5</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="aumento_3_FilteredTextBoxExtender" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="aumento_3" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    %&nbsp; Paq 4:<asp:TextBox ID="aumento_4" runat="server" Width="35px" 
                TabIndex="26">3</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="aumento_4_FilteredTextBoxExtender" runat="server" 
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="aumento_4" 
                            ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                    %</td>
    </tr>
    <tr>
        <td class="style9">
            &nbsp;</td>
        <td class="style8">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style9">
            Monto a Pagar:</td>
        <td class="style8">
            <asp:Label ID="montoapagar" runat="server" Width="200px"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style9">
        </td>
        <td class="style8">
            <asp:Button ID="Button1" runat="server" TabIndex="27" Text="Generar" />
        </td>
    </tr>
    <tr>
        <td class="style9">
            &nbsp;</td>
        <td class="style8">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style9">
        </td>
        <td class="style8">
        </td>
    </tr>
</table>
</asp:Content>

