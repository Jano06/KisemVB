Imports MySql.Data.MySqlClient
Partial Class recuperarpassword
    Inherits System.Web.UI.Page
    Dim passwordstr As String
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If checausuario() Then
            enviamail()
        End If
    End Sub
    Sub enviamail()
        Dim correo As New System.Net.Mail.MailMessage
        correo.From = New System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings("email"))
        correo.To.Add(Me.email.Text)
        correo.Subject = "Recupere su contraseña de Kisem.com.mx"
        correo.Body = "Le reenviamos su contraseña para su oficina virtual, ésta es:" & passwordstr
        correo.IsBodyHtml = False
        correo.Priority = System.Net.Mail.MailPriority.Normal
        Dim smtp As New System.Net.Mail.SmtpClient
        smtp.Credentials = New System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings("email"), System.Configuration.ConfigurationManager.AppSettings("passwordmail"))
        smtp.Host = System.Configuration.ConfigurationManager.AppSettings("hostmail")
        Try
            smtp.Send(correo)
            Me.mensajes.Text = "Mensaje enviado satisfactoriamente"
            Me.numasociado.Text = ""
            Me.email.Text = ""
        Catch ex As Exception
            mensajes.Text = "ERROR: " & ex.Message
        End Try
        Me.mensajes.Visible = True
    End Sub
    Function checausuario() As Boolean
        Dim usuario As Boolean
        'checa en colonos, si está, permisos=2
        Me.mensajes.Visible = False

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, email, password " & _
                                     "FROM asociados " & _
                                     " WHERE id = '" & Me.numasociado.Text & "' AND email='" & Me.email.Text & "' AND status<2"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            usuario = True
            passwordstr = dtrTeam(2)
        End While

        sqlConn.Close()



        'si no está en ninguno, mensaje de error
        If Not usuario Then
            Me.mensajes.Text = "Datos Incorrectos"
            Me.mensajes.Visible = True
            Return False
        Else
            Return True
        End If
    End Function

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Response.Redirect("default.aspx")
    End Sub
End Class
