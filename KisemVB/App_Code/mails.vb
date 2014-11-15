Imports Microsoft.VisualBasic
Imports System.Web.Mail
Imports MySql.Data.MySqlClient

Public Class mails
    Sub enviamaildecompra(ByVal compra As Integer, ByVal paquetes As List(Of Integer), ByVal cantidades As List(Of Integer), ByVal puntos As List(Of Integer), ByVal costos As List(Of Decimal), ByVal envio As String, ByVal costoenvio As Decimal, ByVal iva As Decimal, ByVal referencia As Integer)
        Dim puntostotales As Integer = 0
        Dim subtotal As Decimal = 0
        For i = 0 To cantidades.Count - 1
            puntostotales += puntos(i)
            subtotal += costos(i)
        Next
        Dim funciones As New funciones
        Dim numeroasociado As Integer = funciones.quienhacelacompra(compra)
        Dim nombreasociado, mail As String
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT  concat(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) AS nombre, asociados.email FROM asociados  WHERE asociados.id=" & numeroasociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then nombreasociado = dtrTeam(0)
            If Not IsDBNull(dtrTeam(1)) Then mail = dtrTeam(1)

        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Dim mensaje As String = mensajedecorreocompra()
        Dim a() As String = Split(mensaje, vbCrLf)
        Dim htmbody As New System.Text.StringBuilder()
        Dim body As New System.Text.StringBuilder()
        Dim x As Integer = 0

        body.Append("<table width='800' style='text-align:justify';><tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        For i = LBound(a) To UBound(a)

            With body

                If a(i) = "" Then

                    ' .Append("<p style='margin-top: 0; margin-bottom: 0'>&nbsp;</p>")
                    '.Append("<br/>")
                Else

                    Dim cadena As String = Nothing
                    Dim caux As String = Nothing
                    caux = a(i)

                    For x = 0 To (caux.Length - 1)

                        If caux.Chars(x) = " " Then
                            cadena = cadena & " "
                        Else
                            cadena = cadena & caux.Chars(x)
                        End If
                    Next

                    If i = 0 Then
                        .Append("<span>" & cadena & "</span>")
                    Else
                        .Append("<br/><br/><span>" & cadena & "</span>")
                    End If

                End If

            End With
        Next
        body.Append("</td></tr></table>")
        body.Append("<br/><br/>")
        body.Append("<table width='500'>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("Asociado")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & UCase(nombreasociado) & "</span>")
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("No de Socio")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & numeroasociado.ToString & "</span>")
        body.Append("</td>")
        body.Append("</tr>")

        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("No Orden")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & referencia.ToString & "</span>")
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("Total de Volumen")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & puntostotales.ToString & "</span>")
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("Fecha")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & Date.Today.ToString("dd/MM/yyyy") & "</span>")
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("Método de Envío")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & envio & "</span>")
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("</table>")
        body.Append("<br/><br/>")
        body.Append("<table width='800'>")
        body.Append("<tr style='background-color:#222222; color:#ffffff'>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Cantidad")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Código")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Descripción")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Total de PP")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Precio Unitario")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Importe")
        body.Append("</td>")
        body.Append("</tr>")
        For i = 0 To cantidades.Count - 1


            body.Append("<tr>")
            body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
            body.Append(cantidades(i).ToString)
            body.Append("</td>")
            body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
            body.Append(codigodepaquete(paquetes(i)))
            body.Append("</td>")
            body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
            body.Append(nombredepaquete(paquetes(i)))
            body.Append("</td>")
            body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
            body.Append(puntos(i).ToString)
            body.Append("</td>")
            body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
            body.Append(costos(i).ToString)
            body.Append("</td>")
            body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
            body.Append((costos(i) * cantidades(i)).ToString)
            body.Append("</td>")
            body.Append("</tr>")
        Next
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Subtotal")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(subtotal.ToString)
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Envío")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(costoenvio.ToString)
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("IVA")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(iva.ToString)
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Gran Total")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append((subtotal + costoenvio + iva).ToString)
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("</table>")
        body.Append("<br/><br/>")
        'formas de pago
        body.Append("<table width='800'>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:18px; font-weight:bold;' colspan=3>")
        body.Append("Formas de Pago")
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Banco")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Cuenta")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Número de Referencia")
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("SCOTIABANK")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("03504208749")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append(referencia.ToString)
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("BANORTE")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("0870626045")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append(referencia.ToString)
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("</table>")
        body.Append("<br/><br/>Atentamente. El Equipo de Kísem de México.")
        With htmbody

            .Append("<html>")
            .Append("<head> ")
            .Append("<meta name='ProgId' content='FrontPage.Editor.Document'>")
            .Append("<meta http-equiv='Content-Type' content='text/html; charset=windows-1252'>")
            .Append("<title>Correo Electonico Autogenerado </title>")
            .Append("</head>")
            .Append("<body style='font-family:Arial, Helvetica, sans-serif;'>")
            .Append(body.ToString) ' y aqui va nuestro contenido del mail 


            .Append("</body>")
            .Append("</html>")

        End With


        enviacorreo(mail, " Su orden con número " & referencia.ToString & " fue realizada exitosamente", htmbody.ToString)

    End Sub
    Function codigodepaquete(ByVal paquete As Integer) As String
        Dim respuesta As String = ""
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT codigo FROM paquetes WHERE id=" & paquete.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then respuesta = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()


        Return respuesta
    End Function
    Function nombredepaquete(ByVal paquete As Integer) As String
        Dim respuesta As String = ""
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT nombre FROM paquetes WHERE id=" & paquete.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then respuesta = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()


        Return respuesta
    End Function
    Sub enviamaildebienvenida(ByVal numeroasociado As Integer)
        Dim nombreasociado As String
        Dim numeropatrocinador As Integer
        Dim nombrepatrocinador As String
        Dim password As String
        Dim mail, mailpatrocinador As String
        Dim lado As String
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT asociados.lado, concat(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) AS nombre, asociados.patrocinador, asociados.password, asociados.email, concat(patrocinadores.nombre, ' ', patrocinadores.appaterno, ' ', patrocinadores.apmaterno) AS nombrepatrocinador, patrocinadores.email FROM asociados INNER JOIN asociados AS patrocinadores ON asociados.patrocinador=patrocinadores.id WHERE asociados.id=" & numeroasociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then lado = dtrTeam(0)
            If Not IsDBNull(dtrTeam(1)) Then nombreasociado = dtrTeam(1)
            If Not IsDBNull(dtrTeam(2)) Then numeropatrocinador = dtrTeam(2)
            If Not IsDBNull(dtrTeam(3)) Then password = dtrTeam(3)
            If Not IsDBNull(dtrTeam(4)) Then mail = dtrTeam(4)
            If Not IsDBNull(dtrTeam(5)) Then nombrepatrocinador = dtrTeam(5)
            If Not IsDBNull(dtrTeam(6)) Then mailpatrocinador = dtrTeam(6)
        End While
        If lado = "D" Then
            lado = "Derecho"
        Else
            lado = "Izquierdo"
        End If
        sqlConn.Close()
        sqlConn.Dispose()
        Dim aviso As String = mensajedeavisodeprivacidad()
        Dim mensaje As String = mensajedebienvenida()
        Dim a() As String = Split(mensaje, vbCrLf)
        Dim htmbody As New System.Text.StringBuilder()
        Dim body As New System.Text.StringBuilder()
        Dim x As Integer = 0

        body.Append("<table width='800' style='text-align:justify;'><tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("Estimado <span style='font-weight:bold;'>" & UCase(nombreasociado) & "</span>")
        body.Append("</tr></td>")
        body.Append("<tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        For i = LBound(a) To UBound(a)

            With body

                If a(i) = "" Then

                Else

                    Dim cadena As String = Nothing
                    Dim caux As String = Nothing
                    caux = a(i)

                    For x = 0 To (caux.Length - 1)

                        If caux.Chars(x) = " " Then
                            cadena = cadena & " "
                        Else
                            cadena = cadena & caux.Chars(x)
                        End If
                    Next

                    If i = 0 Then
                        .Append("<span>" & cadena & "</span>")
                    Else
                        .Append("<br/><br/><span>" & cadena & "</span>")
                    End If

                End If

            End With
        Next
        body.Append("</td></tr></table>")
        body.Append("<br/><br/>")
        body.Append("<table width='500'>")

        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;' colspan=2>")
        body.Append("<span style='font-weight:bold;'>INFORMACIÓN PERSONAL</span><br/>")
        body.Append("</td>")
        body.Append("</tr>")

        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("No de Usuario")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & numeroasociado.ToString & "</span>")
        body.Append("</td>")
        body.Append("</tr>")

        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("Contraseña")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & password.ToString & "</span>")
        body.Append("</td>")
        body.Append("</tr>")

        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("Fecha de Ingreso")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & Date.Today.ToString("dd/MM/yyyy") & "</span>")
        body.Append("</td>")
        body.Append("</tr>")

        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("Colocación")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & lado & "</span>")
        body.Append("</td>")
        body.Append("</tr>")

        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("No de Patrocinador")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & numeropatrocinador.ToString & "</span>")
        body.Append("</td>")
        body.Append("</tr>")

        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("Nombre de Patrocinador")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & UCase(nombrepatrocinador).ToString & "</span>")
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("</table>")
        body.Append("<br/><br/>")
        body.Append("<span style='font-weight:bold; font-size:18px; font-style:italic;'>'Somos lo que hacemos día a día. De modo que la excelencia no es un acto, sino un hábito.' </span><br/>")
        body.Append("<span style='font-weight:bold; font-size:18px;'>Aristóteles</span><br/>")
        body.Append("<br/><br/>")

        'Aviso de privacidad
        a = Split(aviso, vbCrLf)
        body.Append("<table width='800' style='text-align:justify;'><tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>AVISO DE PRIVACIDAD</span>")
        body.Append("</tr></td>")
        body.Append("<tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:10px;'>")
        For i = LBound(a) To UBound(a)

            With body

                If a(i) = "" Then

                Else

                    Dim cadena As String = Nothing
                    Dim caux As String = Nothing
                    caux = a(i)

                    For x = 0 To (caux.Length - 1)

                        If caux.Chars(x) = " " Then
                            cadena = cadena & " "
                        Else
                            cadena = cadena & caux.Chars(x)
                        End If
                    Next

                    If i = 0 Then
                        .Append("<span>" & cadena & "</span>")
                    Else
                        .Append("<br/><br/><span>" & cadena & "</span>")
                    End If

                End If

            End With
        Next
        body.Append("</td></tr></table>")


        body.Append("<br/><br/>Atentamente. El Equipo de Kísem de México.")
        With htmbody

            .Append("<html>")
            .Append("<head> ")
            .Append("<meta name='ProgId' content='FrontPage.Editor.Document'>")
            .Append("<meta http-equiv='Content-Type' content='text/html; charset=windows-1252'>")
            .Append("<title>Correo Electonico Autogenerado </title>")
            .Append("</head>")
            .Append("<body style='font-family:Arial, Helvetica, sans-serif;'>")
            .Append(body.ToString) ' y aqui va nuestro contenido del mail 


            .Append("</body>")
            .Append("</html>")

        End With


        enviacorreo(mail, " Bienvenido a Kisem ", htmbody.ToString)

        'ahora al patrocinador
        Dim htmbodypatrocinador As New System.Text.StringBuilder()
        Dim bodypatrocinador As New System.Text.StringBuilder()
        With bodypatrocinador
            .Append("<table width='800' style='text-align:justify; padding:10;'>")
            .Append("<tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
            .Append("Estimado <span style='font-weight:bold;'>" & UCase(nombrepatrocinador) & "</span>")
            .Append("</tr></td>")
            .Append("<tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
            .Append("Nos es grato informarle que se ha integrado a su organización un (a) nuevo (a) líder, su nombre es:<br/><span style='font-weight:bold;'>" & nombreasociado & "</span>")
            .Append("</tr></td>")
            .Append("<tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
            .Append("Le invitamos a darle el apoyo que requiere para desarrollar con gran éxito su negocio, le recordamos que usted forma parte de su línea de auspicio y será con usted con quien se apoye para su aprendizaje.")
            .Append("</tr></td>")
            .Append("<tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
            .Append("Por lo mismo, es importante que usted se apegue al sistema educativo que se le ofrece dentro de Kísem de México, con la finalidad de saber guiar a sus asociados a la cima del éxito.")
            .Append("</tr></td>")
            .Append("<tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
            .Append("Le felicitamos por el compromiso que adquiere y le invitamos a ser el patrocinador que usted mismo necesita.")
            .Append("</tr></td>")
            .Append("<tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
            .Append(" Atte. El Equipo de Kísem de México.")
            .Append("</tr></td>")
        End With
        With htmbodypatrocinador

            .Append("<html>")
            .Append("<head> ")
            .Append("<meta name='ProgId' content='FrontPage.Editor.Document'>")
            .Append("<meta http-equiv='Content-Type' content='text/html; charset=windows-1252'>")
            .Append("<title>Correo Electonico Autogenerado </title>")
            .Append("</head>")
            .Append("<body style='font-family:Arial, Helvetica, sans-serif;'>")
            .Append(bodypatrocinador.ToString) ' y aqui va nuestro contenido del mail 


            .Append("</body>")
            .Append("</html>")

        End With
        enviacorreo(mailpatrocinador, "Felicidades, tienes un nuevo asociado", htmbodypatrocinador.ToString)
    End Sub
    Sub enviamaildecomprainscripcion(ByVal referencia As Integer, ByVal nombreprospecto As String, ByVal numeropatrocinador As Integer, ByVal nombrepatrocinador As String, ByVal paquetes As List(Of Integer), ByVal cantidades As List(Of Integer), ByVal puntos As List(Of Integer), ByVal costos As List(Of Decimal), ByVal email As String, ByVal envio As String, ByVal costoenvio As Decimal, ByVal iva As Decimal)
        Dim puntostotales As Integer = 0
        Dim subtotal As Decimal = 0
        For i = 0 To cantidades.Count - 1
            puntostotales += puntos(i)
            subtotal += costos(i)
        Next

        Dim mensaje As String = mensajedecorreocompra()
        Dim a() As String = Split(mensaje, vbCrLf)
        Dim htmbody As New System.Text.StringBuilder()
        Dim body As New System.Text.StringBuilder()
        Dim x As Integer = 0

        body.Append("<table width='800' style='text-align:justify';><tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        For i = LBound(a) To UBound(a)

            With body

                If a(i) = "" Then

                    ' .Append("<p style='margin-top: 0; margin-bottom: 0'>&nbsp;</p>")
                    '.Append("<br/>")
                Else

                    Dim cadena As String = Nothing
                    Dim caux As String = Nothing
                    caux = a(i)

                    For x = 0 To (caux.Length - 1)

                        If caux.Chars(x) = " " Then
                            cadena = cadena & " "
                        Else
                            cadena = cadena & caux.Chars(x)
                        End If
                    Next

                    If i = 0 Then
                        .Append("<span>" & cadena & "</span>")
                    Else
                        .Append("<br/><br/><span>" & cadena & "</span>")
                    End If

                End If

            End With
        Next
        body.Append("</td></tr></table>")
        body.Append("<br/><br/>")
        body.Append("<table width='500'>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("Invitado")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & UCase(nombreprospecto) & "</span>")
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("No de Patrocinador")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & numeropatrocinador.ToString & "</span>")
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("Nombre de Patrocinador")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & UCase(nombrepatrocinador).ToString & "</span>")
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("No Orden")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & referencia.ToString & "</span>")
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("Total de Volumen")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & puntostotales.ToString & "</span>")
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("Fecha")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & Date.Today.ToString("dd/MM/yyyy") & "</span>")
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("Método de Envío")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("<span style='font-weight:bold;'>" & envio & "</span>")
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("</table>")
        body.Append("<br/><br/>")
        body.Append("<table width='800'>")
        body.Append("<tr style='background-color:#222222; color:#ffffff'>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Cantidad")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Código")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Descripción")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Total de PP")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Precio Unitario")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Importe")
        body.Append("</td>")
        body.Append("</tr>")
        For i = 0 To cantidades.Count - 1


            body.Append("<tr>")
            body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
            body.Append(cantidades(i).ToString)
            body.Append("</td>")
            body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
            body.Append(codigodepaquete(paquetes(i)))
            body.Append("</td>")
            body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
            body.Append(nombredepaquete(paquetes(i)))
            body.Append("</td>")
            body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
            body.Append(puntos(i).ToString)
            body.Append("</td>")
            body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
            body.Append(costos(i).ToString)
            body.Append("</td>")
            body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
            body.Append((costos(i) * cantidades(i)).ToString)
            body.Append("</td>")
            body.Append("</tr>")
        Next
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Subtotal")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(subtotal.ToString)
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Envío")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(costoenvio.ToString)
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("IVA")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(iva.ToString)
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append(" ")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Gran Total")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append((subtotal + costoenvio + iva).ToString)
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("</table>")
        body.Append("<br/><br/>")
        'formas de pago
        body.Append("<table width='800'>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:18px; font-weight:bold;' colspan=3>")
        body.Append("Formas de Pago")
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Banco")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Cuenta")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;'>")
        body.Append("Número de Referencia")
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("SCOTIABANK")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("03504208749")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append(referencia.ToString)
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("<tr>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("BANORTE")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append("0870626045")
        body.Append("</td>")
        body.Append("<td style='font-family:Arial, Helvetica, sans-serif; font-size:14px;'>")
        body.Append(referencia.ToString)
        body.Append("</td>")
        body.Append("</tr>")
        body.Append("</table>")
        body.Append("<br/><br/>Atentamente. El Equipo de Kísem de México.")
        With htmbody

            .Append("<html>")
            .Append("<head> ")
            .Append("<meta name='ProgId' content='FrontPage.Editor.Document'>")
            .Append("<meta http-equiv='Content-Type' content='text/html; charset=windows-1252'>")
            .Append("<title>Correo Electonico Autogenerado </title>")
            .Append("</head>")
            .Append("<body style='font-family:Arial, Helvetica, sans-serif;'>")
            .Append(body.ToString) ' y aqui va nuestro contenido del mail 


            .Append("</body>")
            .Append("</html>")

        End With


        enviacorreo(email, " Su orden con número " & referencia.ToString & " fue realizada exitosamente", htmbody.ToString)

    End Sub
    Function minombre(ByVal id As Integer) As String
        Dim respuesta As String = ""
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT CONCAT(nombre, ' ', appaterno, ' ', apmaterno) AS nombre FROM asociados WHERE id=" & id.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cont As Integer = 0
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then respuesta = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()


        Return respuesta
    End Function
    Sub enviacorreo(ByVal destinatario As String, ByVal asunto As String, ByVal cuerpo As String)

        Try
            Dim correo As New System.Net.Mail.MailMessage
            correo.From = New System.Net.Mail.MailAddress("atencionaclientes@kisem.com.mx")
            correo.To.Add(destinatario)
            correo.Subject = asunto
            correo.Body = cuerpo
            correo.IsBodyHtml = True
            correo.Priority = System.Net.Mail.MailPriority.Normal

            Dim smtp As New System.Net.Mail.SmtpClient
            smtp.Host = "kisem.com.mx"
            smtp.Credentials = New System.Net.NetworkCredential("atencionaclientes@kisem.com.mx", "Atencionaclientes1")
            smtp.Port = 587

            smtp.Send(correo)
            'LabelError.Text = "Mensaje enviado satisfactoriamente"
        Catch ex As Exception
            'LabelError.Text = "ERROR: " & ex.Message
        End Try
    End Sub
    Function micorreo(ByVal id As Integer) As String
        Dim respuesta As String = ""
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT email FROM asociados WHERE id=" & id.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cont As Integer = 0
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then respuesta = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()


        Return respuesta
    End Function
    Function correodeprospecto(ByVal id As Integer) As String
        Dim respuesta As String = ""
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT email FROM prospectos WHERE id=" & id.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cont As Integer = 0
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then respuesta = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()


        Return respuesta
    End Function
    Function mensajedeavisodeprivacidad() As String
        Dim respuesta As String = ""
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT avisodeprivacidad FROM configuracion "
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cont As Integer = 0
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then respuesta = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()


        Return respuesta
    End Function

    Function mensajedecorreocompra() As String
        Dim respuesta As String = ""
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT mensajecompra FROM configuracion "
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cont As Integer = 0
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then respuesta = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()


        Return respuesta
    End Function
    Function mensajedebienvenida() As String
        Dim respuesta As String = ""
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT mensajebienvenida FROM configuracion "
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cont As Integer = 0
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then respuesta = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()


        Return respuesta
    End Function
End Class
