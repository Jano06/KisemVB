<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="compras_foraneas.aspx.vb" Inherits="sw_compras_foraneas" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        
    .style1
    {
        width: 100%;
    }
    .style2
    {
        text-align: left;
        font-family: Arial;
        font-size: medium;
        font-weight: bold;
        color: #666666;
        vertical-align: top;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
    <tr>
        <td class="titulobienvenida" colspan="4">
            Entrega de compras foráneas.
            <br />
            <asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style2">
            &nbsp;</td>
        <td>
            <asp:Label ID="Label1" runat="server" Width="500px"></asp:Label>
        </td>
        <td class="textonuevo2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style2">
            Distribuidor:</td>
        <td>
            <asp:TextBox ID="distribuidor" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="distribuidor_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Numbers" 
                TargetControlID="distribuidor">
            </cc1:FilteredTextBoxExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                    ControlToValidate="distribuidor" EnableClientScript="true" 
                    ErrorMessage="Campo requerido" Width="287px" CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
        <td class="textonuevo2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style2">
            Referencia:</td>
        <td>
            <asp:TextBox ID="referencia" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="referencia_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Numbers" TargetControlID="referencia">
            </cc1:FilteredTextBoxExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                    ControlToValidate="referencia" EnableClientScript="true" 
                    ErrorMessage="Campo requerido" Width="287px" CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
        <td class="textonuevo2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style2">
            &nbsp;</td>
        <td>
            <asp:CheckBox ID="pagada" runat="server" CssClass="texto" Text="Pagada" />
&nbsp;<asp:CheckBox ID="entregada" runat="server" CssClass="texto" Text="Entregada" />
        </td>
        <td class="textonuevo2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style2">
            &nbsp;</td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Procesar" />
        </td>
        <td class="textonuevo2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
</asp:Content>

