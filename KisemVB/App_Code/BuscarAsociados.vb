Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports MySql.Data
Imports MySql.Data.MySqlClient
' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la siguiente línea.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class BuscarAsociados
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function ObtListaAsociados(ByVal prefixText As String) As String()
        Dim lista As New List(Of String)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, nombre, appaterno, apmaterno FROM asociados WHERE nombre LIKE '%" & prefixText & "%' OR appaterno LIKE  '%" & prefixText & "%' OR  apmaterno LIKE  '%" & prefixText & "%'"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read

            lista.Add(dtrTeam.Item("id") & " " & dtrTeam.Item("nombre") & " " & dtrTeam.Item("appaterno") & " " & dtrTeam.Item("apmaterno"))

        End While

        sqlConn.Close()
        Return lista.ToArray
    End Function

End Class
