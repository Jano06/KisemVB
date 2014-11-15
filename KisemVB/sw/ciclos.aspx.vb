Imports MySql.Data
Imports MySql.Data.MySqlClient
Partial Class sw_periodos
    Inherits System.Web.UI.Page

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            inserta()
            llenagrid()
            Me.mensajes.Text = "Registro insertado con éxito"
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
        End Try


    End Sub
    Sub llenagrid()
        Me.GridView2.SelectedIndex = -1
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, inicio,  fin FROM ciclos ORDER BY fin DESC"

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Me.GridView2.DataSource = dtrTeam
        Me.GridView2.DataBind()

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub inserta()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO ciclos(inicio,  fin) VALUES('" & CDate(Me.inicio.Text).ToString("yyyy/MM/dd") & "', '" & CDate(Me.final.Text).ToString("yyyy/MM/dd") & "')"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()



       

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Me.GridView2.DataKeys.Count = 0 Then Me.GridView2.DataKeyNames = New String() {"id"}
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Ciclos de Calificación"
            Dim fecha As Date = Today
            Dim inicio, fin As Date

            For i = 1 To 21
                fecha = DateAdd(DateInterval.Day, -1, fecha)
                If fecha.DayOfWeek = DayOfWeek.Friday Then
                    inicio = DateAdd(DateInterval.Day, 1, fecha)

                    fecha = DateAdd(DateInterval.Month, 2, fecha)
                    fin = fecha
                    For x = 0 To 7

                        If fin.DayOfWeek = DayOfWeek.Friday Then
                            Exit For
                        End If
                        fin = DateAdd(DateInterval.Day, 1, fin)
                    Next
                    Exit For
                End If


            Next
            Me.inicio.Text = inicio.ToString("dd/M/yyyy")
            Me.final.Text = fin.ToString("dd/M/yyyy")
            llenagrid()
        End If
    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow

                Dim ctrlEliminar As LinkButton = CType(e.Row.Cells(2).Controls(0), LinkButton)
                ctrlEliminar.OnClientClick = "return confirm('¿Esta seguro de eliminar este registro?');"



        End Select
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.SelectedIndexChanged
        If Me.GridView2.SelectedIndex > -1 Then
            eliminaciclo(Me.GridView2.DataKeys(Me.GridView2.SelectedIndex).Value)
            llenagrid()
            Me.mensajes.Text = "Registro eliminado con éxito"
        End If
    End Sub
    Sub eliminaciclo(ByVal id As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "DELETE FROM ciclos WHERE id=" & id.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()





        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub

   
End Class
