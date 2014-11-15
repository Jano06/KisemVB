<%@ Page Language="VB" MasterPageFile="~/sw/MasterPage.master" AutoEventWireup="false" CodeFile="comisiones3.aspx.vb" Inherits="sw_comisiones" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">



        .style8
        {
            width: 147px;
        }
        .style9
        {
        }
    </style>
    
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
    <tr>
        <td class="titulo" colspan="2">
            Comisiones<asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
        <td class="titulo">
                &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
            
                    Correr comisiones del período de
                    <asp:TextBox ID="de" runat="server" MaxLength="50" TabIndex="4" 
                    Width="150px" 
    ValidationGroup="pago"></asp:TextBox>
                    <cc1:CalendarExtender ID="TextBox26_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="de" Format="yyyy/M/dd">
                    </cc1:CalendarExtender>
                    &nbsp;a&nbsp;
                    <asp:TextBox ID="a" runat="server" MaxLength="50" TabIndex="4" 
                    Width="150px" 
    ValidationGroup="pago"></asp:TextBox>
                    <cc1:CalendarExtender ID="TextBox26_CalendarExtender0" runat="server" 
                    Enabled="True" TargetControlID="a" Format="yyyy/M/dd">
                    </cc1:CalendarExtender>
                    &nbsp;<asp:Button ID="Button1" runat="server" TabIndex="30" 
    Text="Iniciar" ValidationGroup="pago" />
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
                        ErrorMessage="Fecha inicial es un campo obligatorio" ValidationGroup="pago"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="a" Display="None" 
                        ErrorMessage="Fecha Final es un campo obligatorio" ValidationGroup="pago"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="de" 
                        Display="None" ErrorMessage="Fecha Inicial inválida" MaximumValue="1/1/2043" 
                        MinimumValue="1/1/1900" Type="Date" ValidationGroup="pago"></asp:RangeValidator>
                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="a" 
                        Display="None" ErrorMessage="Fecha Final inválida" MaximumValue="1/1/2043" 
                        MinimumValue="1/1/1900" Type="Date" ValidationGroup="pago"></asp:RangeValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToCompare="de" ControlToValidate="a" Display="None" 
                        ErrorMessage="Fecha final debe ser mayor que fecha inicial" 
                        Operator="GreaterThan" ValidationGroup="pago"></asp:CompareValidator>
                    <br />
                
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
                        &nbsp;</td>
                </tr>
            </table>
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
        <td>
                &nbsp;</td>
        <td class="style8">
                &nbsp;</td>
        <td>
                &nbsp;</td>
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
        <td>
                &nbsp;</td>
        <td class="style8">
                &nbsp;</td>
        <td>
                &nbsp;</td>
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
        <td>
                &nbsp;</td>
        <td class="style8">
                &nbsp;</td>
        <td>
                &nbsp;</td>
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
        <td>
                &nbsp;</td>
        <td class="style8">
                &nbsp;</td>
        <td>
                &nbsp;</td>
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
        <td>
                &nbsp;</td>
        <td class="style8">
                &nbsp;</td>
        <td>
                &nbsp;</td>
    </tr>
</table>
</asp:Content>

