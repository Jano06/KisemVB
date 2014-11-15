<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="arbol_cambios.aspx.vb" Inherits="sw_arbol_cambios" title="Página sin título" %>

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
        <td class="titulo" colspan="2">
            Cambios en Árbol<br />
            <asp:Label ID="mensajes" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="textonuevo">
            Asociado</td>
        <td>
        <asp:TextBox ID="asociado" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="asociado_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Numbers" TargetControlID="asociado">
            </cc1:FilteredTextBoxExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="asociado" ErrorMessage="Campo necesario"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="textonuevo">
            Padre Nuevo</td>
        <td>
        <asp:TextBox ID="padrenuevo" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="padrenuevo_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Numbers" TargetControlID="padrenuevo">
            </cc1:FilteredTextBoxExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="padrenuevo" ErrorMessage="Campo necesario"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="textonuevo">
            Lado</td>
        <td>
        <asp:DropDownList ID="lado" runat="server">
            <asp:ListItem>I</asp:ListItem>
            <asp:ListItem>D</asp:ListItem>
        </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Cambiar" />
        </td>
    </tr>
</table>
</asp:Content>

