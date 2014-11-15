Imports MySql.Data.MySqlClient

Partial Class sw_pagosrangos
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Me.GridView1.DataKeys.Count = 0 Then
            Me.GridView1.DataKeyNames = New String() {"idrango", "idpaquete"}

        End If

        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Pagos por Rango"
            llenagrid()
        End If
    End Sub
    Sub llenagrid()
        Me.GridView1.Columns(2).Visible = True
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT rangos.id AS idrango, rangos.nombre AS rango, paquetes.id AS idpaquete, paquetes.nombre AS paquete, pagorangos.porcentaje AS pago " & _
                                    "FROM rangos INNER JOIN (paquetes INNER JOIN pagorangos ON paquetes.id = pagorangos.Paquete) ON rangos.id = pagorangos.Rango " & _
                                    "ORDER BY rangos.id, paquetes.id "
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Me.GridView1.DataSource = dtrTeam
        Me.GridView1.DataBind()

        sqlConn.Close()
        Me.GridView1.Columns(2).Visible = False
    End Sub

    

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim cb As TextBox = e.Row.FindControl("TextBox1")
            cb.Text = Decimal.Round((CDec(e.Row.Cells(2).Text) * 100), 1).ToString & "%"
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Me.GridView1.Columns(2).Visible = False
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        actualizapagos()
        llenagrid()

        Me.mensajes.Text = "Registro actualizado con éxito"
    End Sub
    Sub actualizapagos()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim cont As Integer = 0
        For Each row In Me.GridView1.Rows
            Dim cb As TextBox = row.FindControl("TextBox1")
            Dim porcentaje As Decimal = CDec(cb.Text.TrimEnd("%")) / 100
            sqlConn.Open()
            strTeamQuery = "UPDATE pagorangos SET porcentaje=" & porcentaje.ToString & " WHERE rango=" & Me.GridView1.DataKeys(cont).Values(0).ToString & " AND paquete=" & Me.GridView1.DataKeys(cont).Values(1).ToString

            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            cmdFetchTeam.ExecuteNonQuery()


            sqlConn.Close()
            sqlConn.Dispose()
            cont += 1
        Next
    End Sub
End Class
