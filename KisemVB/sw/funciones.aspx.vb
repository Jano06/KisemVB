Imports MySql.Data.MySqlClient
Partial Class sw_funciones
    Inherits System.Web.UI.Page
    Dim funciones As New funciones
    Protected Sub btn_eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_eliminar.Click

        'borra anteriores
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "DELETE FROM funcionesadministrador WHERE administrador = " & Me.drp_nombre.SelectedItem.Value.ToString & ";"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
      
        sqlConn.Open()
        cmdFetchTeam.ExecuteNonQuery()
        sqlConn.Close()

        'agrega a array
        Dim esta(39) As Integer
        Dim i As Integer = 0
        Dim row As GridViewRow

        For Each row In Me.grid_funciones.Rows
            Dim cb As CheckBox = row.FindControl("CheckBox1")
            If cb.Checked Then

                esta(i) = 1
            Else
                esta(i) = 0
            End If
            i += 1
        Next
        For i = 1 To 40
            If esta(i - 1) = 1 Then
                strTeamQuery = "INSERT INTO funcionesadministrador (administrador, funcion) VALUES(" & Me.drp_nombre.SelectedItem.Value.ToString & ", " & i.ToString & ")"
                sqlConn.Open()
                cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                cmdFetchTeam.ExecuteNonQuery()
                sqlConn.Close()
            End If
        Next
        llena_grid()
        actualiza_checked()
        Me.mensajes.Text = "Registro actualizado con éxito"







    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then Me.grid_funciones.DataKeyNames = New String() {"id"}
        funciones.llenaadministradores(Me.drp_nombre, Session("idasociado"))
    End Sub
    Protected Sub drp_nombre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drp_nombre.SelectedIndexChanged
        Me.mensajes.Text = ""
        Me.chk_todos.Visible = False
        If Me.drp_nombre.SelectedIndex > 0 Then
            Me.btn_eliminar.Visible = True
            llena_grid()
            actualiza_checked()
            Me.chk_todos.Visible = True
            Me.chk_todos.Text = "Seleccionar Todos"
            Me.chk_todos.Checked = False

        Else
            Me.grid_funciones.DataSource = ""
            Me.grid_funciones.DataBind()
            Me.btn_eliminar.Visible = False
        End If




    End Sub
    Sub actualiza_checked()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()



        Dim seleccionados(0), i As Integer
        i = 0
        strTeamQuery = "SELECT funcion  FROM funcionesadministrador WHERE administrador=" & Me.drp_nombre.SelectedItem.Value.ToString & " ORDER BY funcion ASC; "
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        dtrTeam = cmdFetchTeam.ExecuteReader
        While dtrTeam.Read
            seleccionados(i) = dtrTeam.Item(0)
            i += 1
            ReDim Preserve seleccionados(i)
        End While
        ReDim Preserve seleccionados(i - 1)
        sqlConn.Close()
        Dim b As Integer
        b = 0
        Dim row As GridViewRow
        i = 0
        For Each row In Me.grid_funciones.Rows
            Dim cb As CheckBox = row.FindControl("CheckBox1")
            If seleccionados.Length = 0 Then Exit For
            If Me.grid_funciones.DataKeys(i).Value.ToString = seleccionados(b).ToString Then
                cb.Checked = True
                b += 1
                If b >= seleccionados.Length Then
                    Exit For
                End If
            End If
          

            i += 1
        Next



    End Sub
    Sub llena_grid()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""


        strTeamQuery = "SELECT id, funcion, tipo  FROM funciones ORDER BY tipo, id; "

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Me.grid_funciones.DataSource = dtrTeam
        Me.grid_funciones.DataBind()

        sqlConn.Close()




    End Sub




    Protected Sub chk_todos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_todos.CheckedChanged
        If Me.chk_todos.Checked Then
            Dim row As GridViewRow

            For Each row In Me.grid_funciones.Rows
                Dim cb As CheckBox = row.FindControl("CheckBox1")
                cb.Checked = True
            Next
            Me.chk_todos.Text = "Deseleccionar Todos"
        Else
            Dim row As GridViewRow
            For Each row In Me.grid_funciones.Rows
                Dim cb As CheckBox = row.FindControl("CheckBox1")
                cb.Checked = False
            Next
            Me.chk_todos.Text = "Seleccionar Todos"
        End If
    End Sub

    Protected Sub grid_funciones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grid_funciones.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(1).Text = "1" Then e.Row.Cells(1).Text = "Administrador"
            If e.Row.Cells(1).Text = "2" Then e.Row.Cells(1).Text = "Reportes"
        End If
    End Sub
End Class
