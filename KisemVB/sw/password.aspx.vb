Imports MySql.Data
Imports MySql.Data.MySqlClient
Partial Class sw_password
    Inherits System.Web.UI.Page

    
    Sub actualizapassword()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "UPDATE asociados SET password='" & Me.password.Text & "' WHERE id=" & Session("idasociado").ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)





        sqlConn.Open()
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
    Function obtienepassword() As String
        Dim respuesta As String = ""
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT password FROM asociados WHERE id=" & Session("idasociado").ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader



        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            respuesta = dtrTeam("password")
        End While


        sqlConn.Close()

        Return respuesta
    End Function

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Me.passwordanterior.Focus()
        Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
        objEtiqueta.Text = "Cambio de Contraseña"
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Me.mensajes.CssClass = "exito"
        Me.mensajes.Text = ""
        If Me.passwordanterior.Text = obtienepassword() Then
            actualizapassword()
            Me.mensajes.CssClass = "exito"
            Me.mensajes.Text = "Registro Actualizado con Éxito"
        Else
            Me.mensajes.CssClass = "error"
            Me.mensajes.Text = "Contraseña Incorrecta"
        End If
    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Me.password.Text = ""
        Me.password0.Text = ""
        Me.passwordanterior.Text = ""
    End Sub
End Class
