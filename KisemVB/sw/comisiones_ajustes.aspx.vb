Imports MySql.Data.MySqlClient

Partial Class sw_comisiones_ajustes
    Inherits System.Web.UI.Page
    Dim funciones As New funciones

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Mis Pagos"
            funciones.llenabonos(Me.bonos, False, True)
            llenaperiodos()

        End If
    End Sub
    Sub llenaperiodos()
        Try
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = "SET lc_time_names = 'es_UY'; SELECT id,  Date_format(inicio,'%Y/%M/%d') AS inicio,   Date_format(final,'%Y/%M/%d') AS final, status FROM periodos WHERE status>0 ORDER BY id DESC "
            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()
            Dim liberado As String
            While dtrTeam.Read
                If dtrTeam("status") = 2 Then
                    liberado = " (No Liberado) "
                Else
                    liberado = ""
                End If
                Me.cortes.Items.Add(New ListItem(dtrTeam("id").ToString & " de: " & dtrTeam("inicio").ToString & " a: " & dtrTeam("final").ToString & liberado, dtrTeam("id").ToString))

            End While

            sqlConn.Close()
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try

    End Sub

    Protected Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim asociado() As String = Split(Me.TextBox1.Text, " ")
        If IsNumeric(asociado(0)) Then
            If funciones.existeasociado(CInt(asociado(0))) Then
                actualizabono(CInt(asociado(0)))
                llenagrid()
            Else
                Me.mensajes.Text = "No existe asociado"
            End If



        Else
            Me.mensajes.Text = "Error en el número de asociado"
        End If

    End Sub
    Sub actualizabono(ByVal asociado As Integer)
        Dim mispuntos As Integer = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT inicio, final FROM periodos WHERE id=" & Me.cortes.SelectedItem.Value.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim de, a As Date
        While dtrTeam.Read

            If Not IsDBNull(dtrTeam(0)) Then de = dtrTeam(0)
            If Not IsDBNull(dtrTeam(0)) Then a = dtrTeam(1)

        End While

        sqlConn.Close()

        If Me.bonos.selectedvalue = 4 Then

            strTeamQuery = "SELECT mispuntos FROM pagos WHERE bono=4 AND asociado=" & asociado.ToString & " AND corte=" & Me.cortes.SelectedItem.Value.ToString

            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)



            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read

                If Not IsDBNull(dtrTeam(0)) Then mispuntos = dtrTeam(0)


            End While

            sqlConn.Close()
        End If

        sqlConn.Open()

        strTeamQuery = "DELETE FROM pagos WHERE asociado=" & asociado.ToString & " AND bono=" & Me.bonos.SelectedValue.ToString & " AND corte=" & Me.cortes.SelectedValue.ToString
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()

        sqlConn.Open()
        strTeamQuery = "INSERT INTO pagos (asociado, bono, corte, monto, de, a, status, mispuntos) VALUES(" & asociado.ToString & ", " & Me.bonos.SelectedValue.ToString & ", " & Me.cortes.SelectedValue.ToString & ", " & Me.monto.Text & ", '" & de.ToString("yyyy/MM/dd") & "', '" & a.ToString("yyyy/MM/dd") & "', 1, " & mispuntos.ToString & ")"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
        llenagrid()
    End Sub
    Sub llenagrid()
        Me.GridView1.SelectedIndex = -1
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT pagos.asociado, CONCAT( asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) AS nombre, pagos.bono, pagos.monto, pagos.de, pagos.a, pagos.corte FROM pagos INNER JOIN asociados ON pagos.asociado=asociados.id WHERE pagos.corte=" & Me.cortes.SelectedItem.Value.ToString
        If Not Me.TextBox1.Text = Nothing Then
            Dim asociado() As String = Split(Me.TextBox1.Text, " ")
            strTeamQuery += " AND pagos.asociado=" & asociado(0)
        End If
        strTeamQuery += " ORDER BY pagos.asociado ASC, pagos.bono ASC"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Me.GridView1.DataSource = dtrTeam
        Me.GridView1.DataBind()

        sqlConn.Close()
    End Sub
End Class
