<%@ Page Language="VB" MasterPageFile="~/sw/MasterPage.master" AutoEventWireup="false" CodeFile="tablas.aspx.vb" Inherits="sw_tablas" title="Página sin título" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

    .style3
    {
        width: 63px;
        height: 30px;
    }
    .style4
    {
        height: 30px;
    }
        .style2
        {
            width: 63px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
    <tr>
        <td class="style3">
            Tabla:</td>
        <td class="style4">
            <asp:DropDownList ID="DropTablas" runat="server" 
                Width="170px">
            </asp:DropDownList>
            &nbsp;Límite
            <asp:TextBox ID="TextBox1" runat="server">1000</asp:TextBox>
            <asp:Button ID="Button2" runat="server" Text="Muestra" />
            <asp:Button ID="Button1" runat="server" Text="Actualiza" />
            <asp:Button ID="Button3" runat="server" Text="Actualiza Mails" />
            <asp:Button ID="Button4" runat="server" Text="Borracompras" />
        </td>
    </tr>
    <tr>
        <td class="style2">
            &nbsp;</td>
        <td>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
        </td>
    </tr>
</table>
</asp:Content>

