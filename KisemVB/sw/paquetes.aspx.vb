Imports MySql.Data.MySqlClient
Partial Class sw_paquetes
    Inherits System.Web.UI.Page
    Dim funciones As New funciones
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Me.GridView1.DataKeys.Count = 0 Then Me.GridView1.DataKeyNames = New String() {"id"}
        If Me.GridView2.DataKeys.Count = 0 Then Me.GridView2.DataKeyNames = New String() {"id"}
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Paquetes"
            llenagrid()
            llenaproductos()
        End If
    End Sub
    Sub llenaproductos()
        Try
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = "SELECT id,  nombre AS producto FROM productos    ORDER BY nombre "
            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            Me.GridView2.DataSource = dtrTeam
            Me.GridView2.DataBind()

            sqlConn.Close()
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try
    End Sub
    Sub llenagrid()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, nombre AS producto, costo, puntos FROM paquetes ORDER BY nombre "
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Me.GridView1.DataSource = dtrTeam
        Me.GridView1.DataBind()

        sqlConn.Close()
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        insertapaquete()
        llenagrid()
        funciones.limpiacampos(Me)
        Me.mensajes.Text = "Registro Insertado con éxito"
    End Sub
    Function ultimoid() As Integer
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT MAX(id) FROM paquetes "
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim respuesta As Integer = 0
        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            respuesta = dtrTeam(0)
        End While
        Me.GridView1.DataSource = dtrTeam
        Me.GridView1.DataBind()

        sqlConn.Close()
        Return respuesta

    End Function
    Sub insertapaquete()
        Dim idpaquetenuevo As Integer = ultimoid + 1
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)



        sqlConn.Open()
        strTeamQuery = "INSERT INTO paquetes (id, `nombre`, `costo`, puntos) VALUES (" & idpaquetenuevo.ToString & ",'" & Me.nombre.Text & "', " & Me.costo.Text & ", " & Me.puntos.Text & ")"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()

        sqlConn.Open()
        strTeamQuery = "SELECT MAX(id) FROM paquetes"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        Dim id As Integer = 0

        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then id = dtrTeam(0)
        End While

        sqlConn.Close()

        Dim idprod As New List(Of Integer)
        Dim cantidades As New List(Of Integer)
        Dim cont As Integer = 0
        For Each row In Me.GridView2.Rows
            Dim cb As TextBox = row.FindControl("TextBox1")

            If CInt(cb.Text) > 0 Then
                idprod.Add(Me.GridView2.DataKeys(cont).Value)

                cantidades.Add(CInt(cb.Text))
            End If
            cont += 1


        Next
        For cont = 0 To idprod.Count - 1

            sqlConn.Open()
            strTeamQuery = "INSERT INTO paquetesdetalle(paquete, producto, cantidad) VALUES(" & id.ToString & ", " & idprod(cont).ToString & ", " & cantidades(cont).ToString & ")"


            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            cmdFetchTeam.ExecuteNonQuery()


            sqlConn.Close()
            sqlConn.Dispose()


        Next
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        llenabodega(Me.GridView1.SelectedIndex)
    End Sub
    Sub llenabodega(ByVal renglon As Integer)
        For Each row In Me.GridView2.Rows
            Dim cb As TextBox = row.FindControl("TextBox1")
            cb.Text = 0


        Next
        Me.Button2.Visible = True
        Me.Button3.Visible = True
        Me.Button1.Visible = False
        Me.nombre.Text = Server.HtmlDecode(Me.GridView1.Rows(renglon).Cells(0).Text)
        Me.costo.Text = CInt(Server.HtmlDecode(Me.GridView1.Rows(renglon).Cells(1).Text))
        Me.puntos.Text = Server.HtmlDecode(Me.GridView1.Rows(renglon).Cells(2).Text)


        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT producto, cantidad FROM paquetesdetalle WHERE paquete=" & Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            Dim cont As Integer = 0
            For Each row In Me.GridView2.Rows
                Dim cb As TextBox = row.FindControl("TextBox1")

                If Me.GridView2.DataKeys(cont).Value = dtrTeam(0) Then
                    cb.Text = dtrTeam(1).ToString
                End If



                cont += 1




            Next
        End While

        sqlConn.Close()



        
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Button2.Visible = False
        Me.Button3.Visible = False
        Me.Button1.Visible = True
        Me.GridView1.SelectedIndex = -1
        funciones.limpiacampos(Me)
        For Each row In Me.GridView2.Rows
            Dim cb As TextBox = row.FindControl("TextBox1")
            cb.Text = 0


        Next
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        actualiza()
        llenagrid()
        For Each row In Me.GridView2.Rows
            Dim cb As TextBox = row.FindControl("TextBox1")
            cb.Text = 0


        Next
        Me.mensajes.Text = "Registro actualizado con éxito"
        Me.Button2.Visible = False
        Me.Button3.Visible = False
        Me.Button1.Visible = True
        Me.GridView1.SelectedIndex = -1
        funciones.limpiacampos(Me)
    End Sub
    Sub actualiza()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim id As String = Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value.ToString

        sqlConn.Open()
        strTeamQuery = "UPDATE paquetes SET `nombre`='" & Me.nombre.Text & "', `costo`=" & Me.costo.Text & ", puntos=" & Me.puntos.Text & " WHERE id=" & id

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()

        'borra detalle anterior
        sqlConn.Open()
        strTeamQuery = "DELETE FROM paquetesdetalle WHERE paquete=" & id


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()

        Dim idprod As New List(Of Integer)
        Dim cantidades As New List(Of Integer)
        Dim cont As Integer = 0
        For Each row In Me.GridView2.Rows
            Dim cb As TextBox = row.FindControl("TextBox1")

            If CInt(cb.Text) > 0 Then
                idprod.Add(Me.GridView2.DataKeys(cont).Value)

                cantidades.Add(CInt(cb.Text))
            End If
            cont += 1


        Next
        For cont = 0 To idprod.Count - 1

            sqlConn.Open()
            strTeamQuery = "INSERT INTO paquetesdetalle(paquete, producto, cantidad) VALUES(" & id.ToString & ", " & idprod(cont).ToString & ", " & cantidades(cont).ToString & ")"


            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            cmdFetchTeam.ExecuteNonQuery()


            sqlConn.Close()
            sqlConn.Dispose()


        Next
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow

                Dim ctrlEliminar As LinkButton = CType(e.Row.Cells(4).Controls(0), LinkButton)
                ctrlEliminar.OnClientClick = "return confirm('¿Esta seguro de eliminar este registro?');"



        End Select
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        eliminapaquete(e.RowIndex)
        llenagrid()
        Me.Button2.Visible = False
        Me.Button3.Visible = False
        Me.Button1.Visible = True
        Me.GridView1.SelectedIndex = -1
        funciones.limpiacampos(Me)
        Me.mensajes.Text = "Registro eliminado con éxito"
    End Sub
    Sub eliminapaquete(ByVal indice As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim id As String = Me.GridView1.DataKeys(indice).Value.ToString

        Dim strTeamQuery As String = "DELETE FROM paquetes WHERE id=" & id

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
End Class
