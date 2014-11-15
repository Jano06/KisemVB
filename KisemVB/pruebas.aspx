<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pruebas.aspx.vb" Inherits="pruebas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <table class="style1">
        <tr>
            <td>
                Retención
    <asp:Button ID="Button1" runat="server" Text="Button" />
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
&nbsp;<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Hijos dinámicos
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" Text="Button" />
                <br />
                Hijos:
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                <br />
                Nietos:
                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                <br />
                Bisnietos:
                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    
    </form>
</body>
</html>
