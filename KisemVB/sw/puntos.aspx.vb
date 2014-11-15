Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Partial Class sw_puntos
    Inherits System.Web.UI.Page

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        calculapuntos()

    End Sub
    Sub calculapuntos()
        Try
            Dim ds As New DataSet
            Dim table As DataTable = ds.Tables.Add("Data")
            table.Columns.Add("id", GetType(String))
            table.Columns.Add("asociado", GetType(String))
            table.Columns.Add("izquierdo", GetType(Integer))
            table.Columns.Add("derecho", GetType(Integer))

            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = ""
            If Not Me.txtasociado.Text = Nothing Then
                Dim asociado() As String = Split(Me.txtasociado.Text, " ")
                strTeamQuery = "SELECT asociados.id, Sum(puntosasociados.porpagar) AS SumOfporpagar, puntosasociados.lado, CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) " & _
                                "FROM asociados INNER JOIN puntosasociados ON asociados.ID = puntosasociados.asociado " & _
                                "GROUP BY asociados.ID, puntosasociados.lado " & _
                                "HAVING (((asociados.ID)=" & asociado(0) & ")) " & _
                                "ORDER BY asociados.ID;"
            Else
                strTeamQuery = "SELECT asociados.id, Sum(puntosasociados.porpagar) AS SumOfporpagar, puntosasociados.lado, CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) " & _
                                "FROM asociados INNER JOIN puntosasociados ON asociados.ID = puntosasociados.asociado " & _
                                "GROUP BY asociados.ID, puntosasociados.lado " & _
                                "ORDER BY asociados.ID;"
            End If
            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()
            Dim izquierdo, derecho, idasociado, cont As Integer
            Dim nombre As String = ""
            While dtrTeam.Read
                cont += 1

                If idasociado <> dtrTeam(0) And cont > 1 Then
                    table.Rows.Add(New Object() {idasociado, nombre, izquierdo, derecho})
                    izquierdo = 0
                    derecho = 0
                End If
                nombre = dtrTeam(3)
                Select Case dtrTeam(2)
                    Case "I"
                        izquierdo = dtrTeam(1)
                    Case "D"
                        derecho = dtrTeam(1)

                End Select

                idasociado = dtrTeam(0)
            End While
            table.Rows.Add(New Object() {idasociado.ToString, nombre, izquierdo, derecho})
            sqlConn.Close()
            Dim view As DataView = ds.Tables(0).DefaultView
            Me.GridView1.DataSource = view
            Me.GridView1.DataBind()
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
        objEtiqueta.Text = "Reporte de Puntos"
    End Sub
End Class
