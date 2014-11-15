Imports MySql.Data.MySqlClient
Partial Class sw_arbol_cambios
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            cambiar()
            Me.mensajes.Text = "Asociado cambiado con éxito"
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
        End Try
    End Sub
    Sub cambiar()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "UPDATE asociados set padre=" & Me.padrenuevo.Text & ", lado='" & Me.lado.Text & "' where id=" & Me.asociado.Text

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)



        sqlConn.Open()
        cmdFetchTeam.ExecuteNonQuery()
        sqlConn.Close()



        strTeamQuery = "SELECT recorrido, ladosrecorrido FROM asociados where id=" & padrenuevo.Text
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader

        Dim recorrido As String
        Dim ladosrecorrido As String

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            recorrido = dtrTeam(0)
            ladosrecorrido = dtrTeam(1)
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        strTeamQuery = "UPDATE asociados SET recorrido='" & recorrido & padrenuevo.Text & "." & "', ladosrecorrido='" & ladosrecorrido & Me.lado.Text & "." & "' where id=" & asociado.Text
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)




        sqlConn.Open()
        cmdFetchTeam.ExecuteNonQuery()

        sqlConn.Close()
        sqlConn.Dispose()



    End Sub
End Class
