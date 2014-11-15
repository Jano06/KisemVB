Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Public Class Asociados
    Dim HijosDinamicosValue As New List(Of String)
    Function HijosDinamicos(ByVal asociado As Integer) As List(Of String)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""

        'para incluir el lunes

        strTeamQuery = " SELECT id, status FROM asociados WHERE patrocinador=" & asociado.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader




        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            If dtrTeam(1) = 1 Then
                HijosDinamicosValue.Add(dtrTeam(0))
            Else
                HijosDinamicos(dtrTeam(0))
            End If

        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Return HijosDinamicosValue

    End Function

End Class
