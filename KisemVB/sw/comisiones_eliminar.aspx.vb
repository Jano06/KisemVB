Imports MySql.Data.MySqlClient
Partial Class sw_comisiones_eliminar
    Inherits System.Web.UI.Page
    Dim inicio, fin As Date
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
        objEtiqueta.Text = "Eliminar Comisiones"
        llenagrid()
    End Sub
    Sub llenagrid()
        Me.GridView2.SelectedIndex = -1
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id AS corte, inicio AS de, final AS a FROM periodos WHERE status>0 ORDER BY id DESC"

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Me.GridView2.DataSource = dtrTeam
        Me.GridView2.DataBind()

        sqlConn.Close()
        sqlConn.Dispose()
        If Me.GridView2.Rows.Count = 0 Then
            Me.eliminar.Visible = False
            Me.mensajes.Text = "No hay períodos para eliminar"
        End If
    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow

                Dim ctrlEliminar As LinkButton = CType(e.Row.Cells(3).Controls(0), LinkButton)
                ctrlEliminar.OnClientClick = "return confirm('¿Esta seguro de eliminar este registro?');"



        End Select
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.SelectedIndexChanged
        If Me.GridView2.SelectedIndex > -1 Then
            inicio = CDate(Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(1).Text)
            fin = CDate(Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(2).Text)
            eliminacomisiones()
            llenagrid()
            Me.mensajes.Text = "Registro Eliminado con Éxito"
        End If
    End Sub
    Sub eliminacomisiones()
        'borra periodo
        borraperiodo()
        'elimina pagos
        eliminapagos()
        'devuelve puntos
        devuelvepuntos()

    End Sub
    Sub borraperiodo()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        strTeamQuery = "DELETE FROM periodos  WHERE id=" & Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(0).Text
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub eliminapagos()
        'eliminapagos()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        strTeamQuery = "DELETE FROM pagos  WHERE corte=" & Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(0).Text
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
        'eliminapuntosdetalle
        strTeamQuery = "DELETE FROM puntosdetalle  WHERE corte=" & Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(0).Text
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub devuelvepuntos()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        strTeamQuery = "UPDATE puntosasociados  SET porpagar=porpagaranterior, status=IF(puntos=porpagaranterior, 0, IF(porpagaranterior=0,1,2)), corte=0 WHERE corte= " & Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(0).Text
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub devuelvepuntosanterior()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        strTeamQuery = "UPDATE puntosasociados  INNER JOIN compras ON puntosasociados.compra=compras.id SET puntosasociados.status=0, puntosasociados.porpagar=puntosasociados.puntos WHERE compras.fecha>='" & inicio.Year.ToString & "/" & inicio.Month.ToString & "/" & inicio.Day.ToString & "' AND  compras.fecha<='" & fin.Year.ToString & "/" & fin.Month.ToString & "/" & fin.Day.ToString & "'  "
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub

    Protected Sub eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles eliminar.Click
        For i = 0 To Me.GridView2.Rows.Count - 1
            Me.GridView2.SelectedIndex = i
            inicio = CDate(Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(1).Text)
            fin = CDate(Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(2).Text)

            eliminacomisiones()
           
        Next
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        strTeamQuery = "UPDATE puntosasociados  SET status=0, porpagar=puntos "
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
        llenagrid()
        Me.mensajes.Text = "Registro Eliminado con Éxito"
    End Sub
End Class
