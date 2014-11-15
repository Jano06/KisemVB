Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient




Public Class funciones
#Region "Pagos Bonos"
    Function montodepagobono8(ByVal compra As Integer, ByVal mispuntos As Integer, ByVal pago1bono8 As Integer, ByVal pago2bono8 As Integer, ByVal pagominimo As Integer) As Integer
        Dim respuesta As Integer = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT cantidad, paquete " & _
                                        "FROM comprasdetalle WHERE compra= " & compra.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            If mispuntos > 350 Then
                Select Case dtrTeam(1)
                    Case 1
                        respuesta += dtrTeam(0) * pago1bono8
                    Case 2
                        respuesta += dtrTeam(0) * pago2bono8
                    Case 3
                        respuesta += dtrTeam(0) * pago2bono8
                End Select
               

            Else
                respuesta += dtrTeam(0) * pagominimo
            End If

        End While

        sqlConn.Close()
        sqlConn.Dispose()

        Return respuesta

      
    End Function
    Function montodepagobono7(ByVal compra As Integer, ByVal mispuntos As Integer, ByVal pago1bono7 As Integer, ByVal pago2bono7 As Integer, ByVal pagominimo As Integer) As Integer
        Dim respuesta As Integer = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT cantidad, paquete " & _
                                        "FROM comprasdetalle WHERE compra= " & compra.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            If mispuntos > 350 Then
                Select Case dtrTeam(1)
                    Case 1
                        respuesta += dtrTeam(0) * pago1bono7
                    Case 2
                        respuesta += dtrTeam(0) * pago2bono7
                    Case 3
                        respuesta += dtrTeam(0) * pago2bono7
                End Select


            Else
                respuesta += dtrTeam(0) * pagominimo
            End If

        End While

        sqlConn.Close()
        sqlConn.Dispose()

        Return respuesta


    End Function
    Function montodepagobono6(ByVal compra As Integer, ByVal mispuntos As Integer, ByVal pago1bono6 As Integer, ByVal pago2bono6 As Integer, ByVal pagominimo As Integer) As Integer
        Dim respuesta As Integer = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT cantidad, paquete " & _
                                        "FROM comprasdetalle WHERE paquete>0 and compra= " & compra.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            If mispuntos > 350 Then
                Select Case dtrTeam(1)
                    Case 1
                        respuesta += dtrTeam(0) * pago1bono6
                    Case 2
                        respuesta += dtrTeam(0) * pago2bono6
                    Case 3
                        respuesta += dtrTeam(0) * pago2bono6
                End Select


            Else
                respuesta += dtrTeam(0) * pagominimo
            End If

        End While

        sqlConn.Close()
        sqlConn.Dispose()

        Return respuesta


    End Function
#End Region
    Function EnMantenimiento() As Boolean
        'checa en colonos, si está, permisos=2
        Dim respuesta As Boolean = False
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT mantenimiento FROM configuracion"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If dtrTeam(0) = 1 Then
                respuesta = True
            End If
        End While

        sqlConn.Close()
        Return respuesta
    End Function
    Sub abrenuevaventana(ByVal direccion As String, ByVal pagina As Page)


        pagina.ClientScript.RegisterStartupScript(Me.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", direccion))


    End Sub
    Function numpagos(ByVal asociado As Integer) As Integer
        Dim respuesta As Integer = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT count(distinct corte) from pagos WHERE  asociado=" & asociado.ToString & " AND status=1 order by corte asc"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            respuesta = dtrTeam(0)


        End While

        sqlConn.Close()
        sqlConn.Dispose()


        Return respuesta
    End Function
    Sub llenatodoslosadministradores(ByVal drop As DropDownList)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim cmdFetchTeam As MySqlCommand

        Dim dtrTeam As MySqlDataReader
        Dim qry_miarbol As String = "SELECT id, CONCAT( asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) AS nombre FROM asociados WHERE nivel>0 "
        cmdFetchTeam = New MySqlCommand(qry_miarbol, sqlConn)


        'directos por lado período 1
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        drop.Items.Add("Selecciona un administrador...")
        While dtrTeam.Read

            drop.Items.Add(New ListItem(dtrTeam(1), dtrTeam(0)))

        End While

        sqlConn.Close()
        sqlConn.Dispose()

    End Sub
    Sub llenaadministradores(ByVal drop As DropDownList, ByVal miid As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim cmdFetchTeam As MySqlCommand

        Dim dtrTeam As MySqlDataReader
        Dim qry_miarbol As String = "SELECT id, CONCAT( asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) AS nombre FROM asociados WHERE nivel=2 AND id<>" & miid.ToString
        cmdFetchTeam = New MySqlCommand(qry_miarbol, sqlConn)


        'directos por lado período 1
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        drop.Items.Add("Selecciona un administrador...")
        While dtrTeam.Read

            drop.Items.Add(New ListItem(dtrTeam(1), dtrTeam(0)))

        End While

        sqlConn.Close()
        sqlConn.Dispose()

    End Sub
    Sub llenarangos(ByVal drop As DropDownList)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim cmdFetchTeam As MySqlCommand

        Dim dtrTeam As MySqlDataReader
        Dim qry_miarbol As String = "SELECT id, nombre FROM rangos "
        cmdFetchTeam = New MySqlCommand(qry_miarbol, sqlConn)


           sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        drop.Items.Add("Todos")
        While dtrTeam.Read

            drop.Items.Add(New ListItem(dtrTeam(1), dtrTeam(0)))

        End While

        sqlConn.Close()
        sqlConn.Dispose()

    End Sub
    Function iniciodecalificacion() As Date
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim cmdFetchTeam As MySqlCommand

        Dim dtrTeam As MySqlDataReader
        Dim qry_miarbol As String = "SELECT inicio FROM ciclos WHERE CURDATE() BETWEEN inicio AND fin "
        cmdFetchTeam = New MySqlCommand(qry_miarbol, sqlConn)


        'directos por lado período 1
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim respuesta As Date
        While dtrTeam.Read



            respuesta = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Return respuesta
    End Function
    Function cierredecalificacion() As Date
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim cmdFetchTeam As MySqlCommand

        Dim dtrTeam As MySqlDataReader
        Dim qry_miarbol As String = "SELECT fin FROM ciclos WHERE CURDATE() BETWEEN inicio AND fin "
        cmdFetchTeam = New MySqlCommand(qry_miarbol, sqlConn)


        'directos por lado período 1
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim respuesta As Date
        While dtrTeam.Read



            respuesta = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Return respuesta
    End Function
    Function validapadre(ByVal padre As Integer, ByVal lado As String) As Integer
        Dim respuesta As Integer = padre

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim cmdFetchTeam As MySqlCommand

        Dim dtrTeam As MySqlDataReader
        Dim qry_miarbol As String = "SELECT id FROM asociados WHERE padre=" & padre.ToString & " AND lado='" & lado & "'"
        cmdFetchTeam = New MySqlCommand(qry_miarbol, sqlConn)


        'directos por lado período 1
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        If Not dtrTeam.HasRows Then respuesta = padre
        While dtrTeam.Read



            respuesta = validapadre(dtrTeam(0), lado)
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        Return respuesta
    End Function
    Function fechaparaflush() As Date
        Dim respuesta As Date


        Return respuesta
    End Function
    Function facturo(ByVal asociado As Integer) As Boolean
        Dim respuesta As Boolean = False

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim cmdFetchTeam As MySqlCommand

        Dim dtrTeam As MySqlDataReader
        Dim qry_miarbol As String = "SELECT factura FROM asociados WHERE id=" & asociado.ToString
        cmdFetchTeam = New MySqlCommand(qry_miarbol, sqlConn)


        'directos por lado período 1
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read



            If dtrTeam(0) = 1 Then respuesta = True
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        Return respuesta

    End Function
    Function calculaisr(ByVal importe As Decimal) As Decimal
        If importe = 0 Then
            Exit Function
        End If
        Dim respuesta As Decimal = 0
        Dim i As Integer = 0
        Dim indice As Integer
        Dim excedente As Decimal = 0
        Dim limiteInferior() As Decimal = {0.01, 114.25, 969.51, 1703.81, 1980.59, 2371.33, 4782.62, 7538.1, 14391.45, 19188.62, 57565.77}
        Dim limiteSuperior() As Decimal = {114.24, 969.5, 1703.8, 1980.58, 2371.32, 4782.61, 7538.09, 14391.44, 19188.61, 57565.76, 1000000}
        Dim cuotaFija() As Decimal = {0, 2.17, 56.91, 136.85, 181.09, 251.16, 766.15, 1414.28, 3470.25, 5005.35, 18053.63}
        Dim PorcentajeLimiteInterior() As Decimal = {0.0192, 0.064, 0.1088, 0.16, 0.1792, 0.2136, 0.2352, 0.3, 0.32, 0.34, 0.35}
        indice = limiteInferior.Length - 1

        For i = 0 To limiteInferior.Length - 1

            If importe > limiteInferior(indice) Then
                Exit For
            End If
            indice -= 1
        Next

        respuesta = cuotaFija(indice)
        excedente = importe - limiteInferior(indice)
        excedente = excedente * PorcentajeLimiteInterior(indice)
        respuesta += excedente
        Return respuesta
    End Function
    Sub misdecendientes(ByVal asociado As Integer, ByVal izquierdo As Label, ByVal derecho As Label)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim cmdFetchTeam As MySqlCommand

        Dim dtrTeam As MySqlDataReader
        Dim qry_miarbol As String = "SELECT id, recorrido, ladosrecorrido FROM asociados WHERE status=1 AND recorrido LIKE '%." & asociado.ToString & ".%'"
        cmdFetchTeam = New MySqlCommand(qry_miarbol, sqlConn)


        'directos por lado período 1
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim recorrido, ladosrecorrido As String
        Dim recorridoarray(), ladosarray() As String
        Dim izq, der As Integer
        While dtrTeam.Read



            recorrido = dtrTeam(1)

            ladosrecorrido = dtrTeam(2)

            recorridoarray = Split(recorrido, ".")
            ladosarray = Split(ladosrecorrido, ".")
            Dim posicion As Integer = 0
            For posicion = 0 To recorridoarray.Length - 1
                If recorridoarray(posicion) = asociado.ToString Then
                    Exit For
                End If


            Next
            If UCase(ladosarray(posicion)) = "D" Then
                der += 1
            Else
                izq += 1
            End If
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        izquierdo.Text = izq.ToString
        derecho.Text = der.ToString
    End Sub
    Function Crearpassword(ByVal PasswordLength As Integer) As String


        Dim _allowedChars As String = "ABCDEFGHJKLMNPQRSTUVWXYZ"
        Dim password As String = ""
        'Inicializar la clase Random  
        Dim Random As New Random()

        ' generar un random entre 1 y 100  

        For i = 0 To 1
            Dim lugar As Integer = Random.Next(1, _allowedChars.Length)
            password += Mid(_allowedChars, lugar, 1)

        Next
        _allowedChars = "1234567890"
        For i = 0 To 3
            Dim lugar As Integer = Random.Next(1, _allowedChars.Length)
            password += Mid(_allowedChars, lugar, 1)

        Next
        Return password
    End Function
    Function periodocomisionable(ByVal fecha As Date) As String
        Dim respuesta As String = ""
        Dim de, a As Date
        Dim banderaa, banderade As Integer
        For i = 1 To 21
            fecha = DateAdd(DateInterval.Day, -1, fecha)
            If fecha.DayOfWeek = DayOfWeek.Friday And banderade = 0 And banderaa = 1 Then
                de = DateAdd(DateInterval.Day, 1, fecha)
                banderade = 1
            End If

            If fecha.DayOfWeek = DayOfWeek.Friday And banderaa = 0 Then
                a = fecha
                banderaa = 1
            End If


        Next
        de = DateAdd(DateInterval.Day, 7, de)
        a = DateAdd(DateInterval.Day, 7, a)
        respuesta = "Del " & de.ToString("dd/MMMM/yyyy") & " al " & a.ToString("dd/MMMM/yyyy")
        Return respuesta
    End Function

    Sub exportaexcel(ByVal nombrereporte As String, ByVal query As String)
        HttpContext.Current.Session("nombrereporte") = nombrereporte
        HttpContext.Current.Session("queryreporte") = query
        HttpContext.Current.Response.Redirect("exportaraexcel.aspx")
    End Sub
    Sub requisitosrango(ByVal rango As Integer, ByVal activosporlado As Label, ByVal sumaactivos As Label, ByVal volumengrupal As Label, ByVal mipuntaje As Label, ByVal porcentajedebalance As Label, ByVal siguienterango As Label)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT porcentajedebalance, porcentajedebalancerango2, puntosrango2, puntosrango3, puntosrango4, puntosrango5, puntosrango6, puntosrango7, puntosrango8, puntosrango9 FROM configuracion"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim porcentajedebalancevar, porcentajedebalancecolaboradorvar, puntosmensualesporladorango2var, puntosmensualesporladorango3var, puntosmensualesporladorango4var, puntosmensualesporladorango5var, puntosmensualesporladorango6var, puntosmensualesporladorango7var, puntosmensualesporladorango8var, puntosmensualesporladorango9var As Decimal
        While dtrTeam.Read
            porcentajedebalancevar = dtrTeam(0)
            porcentajedebalancecolaboradorvar = dtrTeam(1)
            puntosmensualesporladorango2var = dtrTeam(2)
            puntosmensualesporladorango3var = dtrTeam(3)
            puntosmensualesporladorango4var = dtrTeam(4)
            puntosmensualesporladorango5var = dtrTeam(5)
            puntosmensualesporladorango6var = dtrTeam(6)
            puntosmensualesporladorango7var = dtrTeam(7)
            puntosmensualesporladorango8var = dtrTeam(8)
            puntosmensualesporladorango9var = dtrTeam(9)
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Select Case rango
            Case 2
                porcentajedebalance.Text = porcentajedebalancecolaboradorvar.ToString & "%"
                volumengrupal.Text = puntosmensualesporladorango2var.ToString
                mipuntaje.Text = "700"
                activosporlado.Text = "1"
                sumaactivos.Text = "2"
                siguienterango.Text = "Colaborador"
            Case 3
                porcentajedebalance.Text = porcentajedebalancevar.ToString & "%"
                volumengrupal.Text = puntosmensualesporladorango3var.ToString
                mipuntaje.Text = "700"
                activosporlado.Text = "1"

                sumaactivos.Text = "3"
                siguienterango.Text = "Colaborador Ejecutivo"
            Case 4
                porcentajedebalance.Text = porcentajedebalancevar.ToString & "%"
                volumengrupal.Text = puntosmensualesporladorango4var.ToString
                mipuntaje.Text = "700"
                activosporlado.Text = "1"

                sumaactivos.Text = "3"
                siguienterango.Text = "Bronce"
            Case 5
                porcentajedebalance.Text = porcentajedebalancevar.ToString & "%"
                volumengrupal.Text = puntosmensualesporladorango5var.ToString
                mipuntaje.Text = "700"
                activosporlado.Text = "1"

                sumaactivos.Text = "3"
                siguienterango.Text = "Plata"
            Case 6
                porcentajedebalance.Text = porcentajedebalancevar.ToString & "%"
                volumengrupal.Text = puntosmensualesporladorango6var.ToString
                mipuntaje.Text = "700"
                activosporlado.Text = "2"

                sumaactivos.Text = "4"
                siguienterango.Text = "Oro"
            Case 7
                porcentajedebalance.Text = porcentajedebalancevar.ToString & "%"
                volumengrupal.Text = puntosmensualesporladorango7var.ToString
                mipuntaje.Text = "700"
                activosporlado.Text = "2"

                sumaactivos.Text = "4"
                siguienterango.Text = "Diamante"
            Case 8
                porcentajedebalance.Text = porcentajedebalancevar.ToString & "%"
                volumengrupal.Text = puntosmensualesporladorango8var.ToString
                mipuntaje.Text = "700"
                activosporlado.Text = "2"
                sumaactivos.Text = "4"
                siguienterango.Text = "Diamante Ejecutivo"
            Case 9
                porcentajedebalance.Text = porcentajedebalancevar.ToString & "%"
                volumengrupal.Text = puntosmensualesporladorango9var.ToString
                mipuntaje.Text = "700"
                activosporlado.Text = "2"

                sumaactivos.Text = "4"
                siguienterango.Text = "Diamante Internacional"
        End Select
    End Sub
    Function proximosabado() As Date
        Dim hoy As Integer = Date.Today.DayOfWeek
        Dim faltan As Integer = 6 - hoy
        proximosabado = DateAdd(DateInterval.Day, faltan, Today)
    End Function
    Function subeniveles(ByVal asociado As Integer, ByVal tope As Integer, Optional ByVal niveles As Integer = 1) As Integer
        ' niveles = niveles * 5
        Dim resultado As Integer = asociado
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        For i = 1 To niveles

            sqlConn.Open()
            strTeamQuery = "SELECT padre FROM asociados WHERE id=" & asociado.ToString
            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            dtrTeam = cmdFetchTeam.ExecuteReader()
            If Not dtrTeam.HasRows Then
                Exit For
            End If

            While dtrTeam.Read
                asociado = dtrTeam(0)
            End While

            sqlConn.Close()
            sqlConn.Dispose()
            If asociado = tope Then
                Exit For
            End If


        Next






        Return asociado
    End Function
    Function extremaizquierda(ByVal asociado As Integer) As Integer
        Dim resultado As Integer = asociado
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id FROM asociados WHERE padre=" & asociado.ToString & " AND lado='I'"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cont As Integer = 0
        While dtrTeam.Read
            resultado = extremaizquierda(dtrTeam(0))
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Return resultado
    End Function
    Function extremaderecha(ByVal asociado As Integer) As Integer
        Dim resultado As Integer = asociado
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id FROM asociados WHERE padre=" & asociado.ToString & " AND lado='D'"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cont As Integer = 0
        While dtrTeam.Read
            resultado = extremaderecha(dtrTeam(0))
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Return resultado
    End Function
    Sub desactivaasociados(ByVal ultimafecha As Date)
        Dim diferencia As Integer = DateDiff(DateInterval.Day, ultimafecha, Today)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        For i = 1 To diferencia
            ultimafecha = DateAdd(DateInterval.Day, 1, ultimafecha)

            sqlConn.Open()
            ' strTeamQuery = "UPDATE asociados SET status=0 WHERE DAY(FInsc)=" & ultimafecha.Day.ToString & " AND '" & ultimafecha.Year.ToString & "/" & ultimafecha.Month.ToString & "/" & ultimafecha.Day.ToString & "' NOT BETWEEN inicioactivacion AND finactivacion AND status=1 "
            strTeamQuery = "UPDATE asociados SET status=0 WHERE '" & ultimafecha.ToString("yyyy/MM/dd") & "' NOT BETWEEN inicioactivacion AND finactivacion AND status=1 "
            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            cmdFetchTeam.ExecuteNonQuery()


            sqlConn.Close()
            sqlConn.Dispose()
            sqlConn.Open()
            strTeamQuery = "UPDATE asociados SET status=1 WHERE '" & ultimafecha.ToString("yyyy/MM/dd") & "' BETWEEN inicioactivacion AND finactivacion AND status=0 "
            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            cmdFetchTeam.ExecuteNonQuery()


            sqlConn.Close()
            sqlConn.Dispose()
            sqlConn.Open()
            strTeamQuery = "INSERT INTO desactivaciones (fecha) VALUES('" & ultimafecha.Year.ToString & "/" & ultimafecha.Month.ToString & "/" & ultimafecha.Day.ToString & "')"

            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            cmdFetchTeam.ExecuteNonQuery()


            sqlConn.Close()
            sqlConn.Dispose()



        Next





    End Sub
    Function ultimadesactivacion() As Date
        Dim resultado As Date
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT MAX(fecha) FROM desactivaciones "
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cont As Integer = 0
        While dtrTeam.Read
            resultado = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        Return resultado
    End Function
    Function misdirectos(ByVal asociado As Integer) As Integer()
        Dim resultado(0) As Integer
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id FROM asociados WHERE patrocinador=" & asociado.ToString & " ORDER BY id"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cont As Integer = 0
        While dtrTeam.Read
            resultado(cont) = dtrTeam(0)
            cont += 1
            ReDim Preserve resultado(cont)
        End While
        ReDim Preserve resultado(cont - 1)
        sqlConn.Close()
        sqlConn.Dispose()

        Return resultado
    End Function
    Function solonumeros(ByVal valor As String) As String
        Dim respuesta As String = ""
        For Each letra As Char In valor
            If IsNumeric(letra) Then
                respuesta += letra
            End If


        Next


        Return respuesta
    End Function
    Function reduce(ByVal valor As Integer) As Integer
        Dim unidades As Integer = valor Mod 10
        Dim decenas As Integer = (valor - unidades) / 10
        valor = decenas + unidades
        If valor > 9 Then valor = reduce(valor)

        Return valor
    End Function
    Function referencia(ByVal compra As Integer) As Integer
        Dim respuesta As String = ""
        Dim largo As Integer = compra.ToString.Length
        Dim digver As Integer = 0
        Dim posiciones(largo - 1) As String
        Dim i As Integer = 0
        For Each letra As Char In compra.ToString
            posiciones(i) = letra
            i += 1
        Next

        Dim digito As Integer = 2
        largo -= 1
        For i = 0 To posiciones.Length - 1
            posiciones(largo - i) = posiciones(largo - i) * digito

            If digito = 2 Then
                digito = 1
            Else
                digito = 2
            End If

        Next

        For i = 0 To posiciones.Length - 1
            If posiciones(i) > 9 Then
                posiciones(i) = reduce(posiciones(i))

            End If
            digver += posiciones(i)
        Next
        digver = digver Mod 10
        digver = 10 - digver
        If digver = 10 Then digver = 0
        respuesta = compra & digver.ToString
        Return CInt(respuesta)
    End Function
    Function escompradeinscripcion(ByVal compra As Integer) As Integer
        Dim respuesta As Integer = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT inscripcion FROM compras WHERE id=" & compra.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If dtrTeam(0) > 0 Then respuesta = dtrTeam(0)

        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Return respuesta
    End Function
    Function devuelvenumero(ByVal dato As String) As Integer
        Dim respuesta As String

        For Each letra As Char In dato

            If IsNumeric(letra) Then
                respuesta += letra
            End If
        Next
        Return CInt(respuesta)
    End Function
    Function puntosdelacompra(ByVal compra As Integer) As Integer
        Dim respuesta As Integer = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT puntos, excedente FROM compras WHERE id=" & compra.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            respuesta = dtrTeam(0)
            If dtrTeam(1) = 1 And respuesta = 1000 Then
                respuesta = 800
            End If

        End While

        sqlConn.Close()
        sqlConn.Dispose()

        Return respuesta
    End Function
    Function quienhacelacompra(ByVal compra As Integer) As Integer
        Dim respuesta As Integer = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT asociado FROM compras WHERE id=" & compra.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            respuesta = dtrTeam(0)


        End While

        sqlConn.Close()
        sqlConn.Dispose()

        Return respuesta
    End Function
    Function primercompra(Optional ByVal asociado As Integer = 0) As Date
        Dim fecha As Date
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT MIN(fecha) FROM compras"
        If asociado > 0 Then strTeamQuery += " WHERE asociado=" & asociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then fecha = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Return fecha
    End Function
    Function ultimacompra(Optional ByVal asociado As Integer = 0) As Date
        Dim fecha As Date
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT MAX(fecha) FROM compras"
        If asociado > 0 Then strTeamQuery += " WHERE asociado=" & asociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then fecha = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Return fecha
    End Function

    Sub subepuntos(ByVal compra As Integer, ByVal puntos As Integer, ByVal asociado As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT padre, lado FROM asociados WHERE id=" & asociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        Dim padre As Integer = 0
        Dim lado As String = ""
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then padre = dtrTeam(0)
            If Not IsDBNull(dtrTeam(1)) Then lado = dtrTeam(1)

        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If padre > 10 Then 'para evitar padres que no están dados de alta
            sqlConn.Open()
            strTeamQuery = "INSERT INTO puntosasociados(asociado, compra, lado, puntos, status, porpagar) VALUES(" & padre.ToString & ", " & compra.ToString & ", '" & lado & "', " & puntos.ToString & ", 0, " & puntos.ToString & ")"

            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            cmdFetchTeam.ExecuteNonQuery()


            sqlConn.Close()
            sqlConn.Dispose()
        End If


        strTeamQuery = "SELECT padre, lado FROM asociados WHERE id=" & padre.ToString
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)

        Dim abuelo As Integer = 0
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then abuelo = dtrTeam(0)
            If Not IsDBNull(dtrTeam(1)) Then lado = dtrTeam(1)

        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If padre > 3 Then subepuntos(compra, puntos, padre)
    End Sub


    Sub llenaformadepago(ByVal drop As DropDownList, ByVal nivel As Integer)
        Try

            If nivel > 0 Then
                drop.Items.Add(New ListItem("Efectivo", 0))

            End If
            drop.Items.Add(New ListItem("Depósito Bancario", 1))



        Catch ex As Exception

        End Try
    End Sub
    Function costopaquete(ByVal paquete As Integer) As Decimal
        Dim respuesta As Decimal = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT costo FROM paquetes WHERE id=" & paquete.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            respuesta = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Return respuesta
    End Function
    Function puntospaquete(ByVal paquete As Integer) As Integer
        Dim respuesta As Integer = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT puntos FROM paquetes WHERE id=" & paquete.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            respuesta = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Return respuesta
    End Function
    Function costoenvio() As Decimal
        Dim respuesta As Decimal = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT costoenvio FROM configuracion"

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        Dim contador As Integer = 0
        Dim idpaquetes As New List(Of Integer)
        Dim costopaquetes As New List(Of Decimal)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            respuesta = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Return respuesta
    End Function
    Function validarfecha(ByVal dia As Integer, ByVal mes As Integer, ByVal ano As Integer) As Date
        Dim fecha As Date
        Try
            If mes = 13 Then
                mes = 1
                ano += 1
            End If

            fecha = CDate(ano.ToString & "/" & mes.ToString & "/" & dia.ToString)
        Catch ex As Exception
            fecha = validarfecha(dia - 1, mes, ano)
        End Try

        Return fecha

    End Function
    Function misiguienteactivacion(ByVal asociado As Integer) As Date

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT finsc " & _
                                     "FROM asociados " & _
                                     "WHERE id =" & asociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim fecha As Date
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then fecha = CDate(dtrTeam(0))
        End While

        sqlConn.Close()

       
            Dim dia As Integer = fecha.Day

            'fecha = CDate(Date.Today.Year.ToString & "/" & Date.Today.Month.ToString & "/" & dia.ToString)
            fecha = validarfecha(dia, Date.Today.Month, Date.Today.Year)
            If fecha <= Date.Today Then
                fecha = DateAdd(DateInterval.Month, 1, fecha)


            End If






        Return fecha
    End Function
    Function misiguienteactivacionmodificado(ByVal asociado As Integer) As Date

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT finsc " & _
                                     "FROM asociados " & _
                                     "WHERE id =" & asociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim fecha As Date
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then fecha = CDate(dtrTeam(0))
        End While

        sqlConn.Close()

        'checa si está activo
        strTeamQuery = "SELECT status FROM asociados  WHERE id =" & asociado.ToString
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim status As Integer
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then status = dtrTeam(0)
        End While

        sqlConn.Close()
        If status = 0 Then
            If fecha.Day <= Date.Today.Day Then
                fecha = validarfecha(fecha.Day, Date.Today.Month, Date.Today.Year)
            Else
                Dim fecha2 As Date = DateAdd(DateInterval.Month, -1, Date.Today)
                Dim mes As Integer = fecha2.Month
                Dim anio As Integer = fecha2.Year
                fecha = validarfecha(fecha.Day, mes, anio)
            End If
        Else
            Dim dia As Integer = fecha.Day

            'fecha = CDate(Date.Today.Year.ToString & "/" & Date.Today.Month.ToString & "/" & dia.ToString)
            fecha = validarfecha(dia, Date.Today.Month, Date.Today.Year)
            If fecha <= Date.Today Then
                fecha = DateAdd(DateInterval.Month, 1, fecha)


            End If
        End If





        Return fecha
    End Function
    Function esmisemana(ByVal asociado As Integer) As Boolean
        Dim respuesta As Boolean = False
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT finsc " & _
                                     "FROM asociados " & _
                                     "WHERE id =" & asociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim fecha As Date
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then fecha = dtrTeam(0)
        End While

        sqlConn.Close()
        Dim dia As Integer = fecha.Day
        Dim de, a As Date
        Dim banderaa, banderade As Integer
        If dia <= 6 And Date.Today.Day > 21 Then
            'fecha = CDate(Date.Today.Year.ToString & "/" & (Date.Today.Month + 1).ToString & "/" & dia.ToString)
            fecha = validarfecha(dia, Date.Today.Month + 1, Date.Today.Year)
        Else

            'fecha = CDate(Date.Today.Year.ToString & "/" & Date.Today.Month.ToString & "/" & dia.ToString)
            fecha = validarfecha(dia, Date.Today.Month, Date.Today.Year)
        End If

        For i = 1 To 21
            fecha = DateAdd(DateInterval.Day, -1, fecha)
            If fecha.DayOfWeek = DayOfWeek.Friday And banderade = 0 And banderaa = 1 Then
                de = DateAdd(DateInterval.Day, 1, fecha)
                banderade = 1
            End If

            If fecha.DayOfWeek = DayOfWeek.Friday And banderaa = 0 Then
                a = fecha
                banderaa = 1
            End If


        Next
        de = DateAdd(DateInterval.Day, 7, de)
        a = DateAdd(DateInterval.Day, 7, a)
        If Date.Today >= de And Date.Today <= a Then respuesta = True
        Return respuesta
    End Function
    Function activacionenloquerestadelasemana(ByVal asociado As Integer) As Boolean
        Dim respuesta As Boolean = False
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT finsc " & _
                                     "FROM asociados " & _
                                     "WHERE id =" & asociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim fecha As Date
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then fecha = dtrTeam(0)
        End While

        sqlConn.Close()
        Dim dia As Integer = fecha.Day
        Dim diasem As Integer = Date.Today.DayOfWeek + 2
        If diasem = 7 Then diasem = 0
        If diasem = 8 Then
            Return False
            Exit Function
        End If
        Dim fechatemp As Date = DateAdd(DateInterval.Day, 1, Today)
        For i = diasem To 6
            If fechatemp.Day = fecha.Day Then
                respuesta = True
                Exit For


            End If
            fechatemp = DateAdd(DateInterval.Day, 1, fechatemp)

        Next


      
        Return respuesta
    End Function
    Function existeasociado(ByVal asociado As Integer) As Boolean
        Dim respuesta As Boolean = False
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT status FROM asociados WHERE id=" & asociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            respuesta = True


        End While

        sqlConn.Close()
        sqlConn.Dispose()

        Return respuesta
    End Function
    Function mistatus(ByVal asociado As Integer) As Integer
        Dim respuesta As Integer = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT status FROM asociados WHERE id=" & asociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            respuesta = dtrTeam(0)


        End While

        sqlConn.Close()
        sqlConn.Dispose()

        Return respuesta
    End Function
    Function mifindeactivacion(ByVal asociado As Integer) As Date
        Dim respuesta As Date
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT finactivacion FROM asociados WHERE id=" & asociado.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            respuesta = dtrTeam(0)
        End While

        sqlConn.Close()
        Return respuesta
    End Function
    Function buscabodega(ByVal asociado As Integer) As Integer
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT bodega FROM asociados WHERE id=" & asociado.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim respuesta As Integer = 0
        While dtrTeam.Read
            respuesta = dtrTeam(0)
        End While

        sqlConn.Close()
        Return respuesta
    End Function
    Sub regresaabodega(ByVal bodega As Integer, ByVal paquete As Integer, ByVal cantidad As Integer)
        Dim productos As New List(Of Integer)
        Dim cantidades As New List(Of Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT producto,  cantidad FROM paquetesdetalle WHERE paquete=" & paquete.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            productos.Add(dtrTeam(0))
            cantidades.Add(dtrTeam(1))
        End While


        sqlConn.Close()
        For i = 0 To productos.Count - 1
            sqlConn.Open()
            strTeamQuery = "UPDATE inventario SET cantidad=cantidad+" & (cantidades(i) * cantidad).ToString & " WHERE bodega=" & bodega.ToString & " AND producto=" & productos(i).ToString

            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            cmdFetchTeam.ExecuteNonQuery()
            sqlConn.Close()
        Next


    End Sub
    Sub descuentadebodegas(ByVal bodega As Integer, ByVal paquete As Integer, ByVal cantidad As Integer)
        Dim productos As New List(Of Integer)
        Dim cantidades As New List(Of Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT producto,  cantidad FROM paquetesdetalle WHERE paquete=" & paquete.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            productos.Add(dtrTeam(0))
            cantidades.Add(dtrTeam(1))
        End While


        sqlConn.Close()
        For i = 0 To productos.Count - 1
            sqlConn.Open()
            strTeamQuery = "UPDATE inventario SET cantidad=cantidad-" & (cantidades(i) * cantidad).ToString & " WHERE bodega=" & bodega.ToString & " AND producto=" & productos(i).ToString

            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            cmdFetchTeam.ExecuteNonQuery()
            sqlConn.Close()
        Next


    End Sub
    Function haysuficienteenbodega(ByVal compra As Integer, ByVal bodega As Integer) As Boolean
        Dim respuesta As Boolean = True

        Dim productos As New List(Of Integer)
        Dim cantidades As New List(Of Integer)
        Dim paquetes As New List(Of Integer)
        Dim cantidadespaquetes As New List(Of Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        'Dim strTeamQuery As String = "SELECT producto,  cantidad FROM paquetesdetalle WHERE paquete=" & paquete.ToString
        Dim strTeamQuery As String = "SELECT paquete, cantidad FROM comprasdetalle WHERE compra=" & compra.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            paquetes.Add(dtrTeam(0))
            cantidadespaquetes.Add(dtrTeam(1))
        End While


        sqlConn.Close()
        For i = 0 To paquetes.Count - 1
            strTeamQuery = "SELECT producto,  cantidad FROM paquetesdetalle WHERE paquete=" & paquetes(i).ToString
            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read
                productos.Add(dtrTeam(0))
                cantidades.Add(dtrTeam(1))
            End While


            sqlConn.Close()
        Next

        For i = 0 To productos.Count - 1
            strTeamQuery = "SELECT cantidad FROM inventario WHERE producto=" & productos(i).ToString & " AND bodega=" & bodega.ToString
            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read

                If cantidades(i) > dtrTeam(0) Then respuesta = False
            End While


            sqlConn.Close()
        Next




        Return respuesta
    End Function
    Sub llenacategorias(ByVal drop As DropDownList, Optional ByVal cualquiera As Boolean = False)
        Try
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = "SELECT id,  nombre FROM categorias    ORDER BY nombre "
            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader
            If cualquiera Then
                drop.Items.Add("Cualquiera")
            End If
            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read

                drop.Items.Add(New ListItem(dtrTeam("nombre").ToString, dtrTeam("id").ToString))

            End While

            sqlConn.Close()
        Catch ex As Exception

        End Try
    End Sub
    Sub llenaproductos(ByVal drop As DropDownList, Optional ByVal cualquiera As Boolean = False)
        Try
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = "SELECT id,  nombre FROM productos    ORDER BY nombre "
            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader
            If cualquiera Then
                drop.Items.Add("Cualquiera")
            End If
            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read

                drop.Items.Add(New ListItem(dtrTeam("nombre").ToString, dtrTeam("id").ToString))

            End While

            sqlConn.Close()
        Catch ex As Exception

        End Try
    End Sub
    Function NiveldeUsuario(ByVal asociado As Integer) As Integer
        Try
            Dim respuesta As Integer
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = "SELECT nivel FROM asociados WHERE id=" & asociado.ToString




            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
          
            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read

                respuesta = dtrTeam(0)

            End While

            sqlConn.Close()
            Return respuesta
        Catch ex As Exception

        End Try
    End Function
    Sub llenabodegas(ByVal drop As DropDownList, ByVal usuario As Integer, Optional ByVal cualquiera As Boolean = False)
        Try


            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = "SELECT id,  nombre FROM bodegas "
            If NiveldeUsuario(usuario) = 2 Then
                strTeamQuery += "WHERE id=" & buscabodega(usuario).ToString
            End If


            strTeamQuery += " ORDER BY nombre "
            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
            If cualquiera Then
                drop.Items.Add("Cualquiera")
            End If
            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read

                drop.Items.Add(New ListItem(dtrTeam("nombre").ToString, dtrTeam("id").ToString))

            End While

            sqlConn.Close()
        Catch ex As Exception

        End Try
    End Sub
    Sub llenabonos(ByVal drop As DropDownList, Optional ByVal cualquiera As Boolean = False, Optional ByVal bonoglobal As Boolean = False)
        Try

            If cualquiera Then
                drop.Items.Add("Cualquiera")
            End If

            drop.Items.Add(New ListItem("Bono Excedente", 1))
            drop.Items.Add(New ListItem("Bono por Inscripción", 2))
            drop.Items.Add(New ListItem("Bono por Inscripción Infinito", 3))
            drop.Items.Add(New ListItem("Bono por Igualación de Volumen", 4))
            drop.Items.Add(New ListItem("Bono de Seguimiento", 5))
            drop.Items.Add(New ListItem("Bono de Bienestar", 6))
            drop.Items.Add(New ListItem("Bono Guía", 7))
            drop.Items.Add(New ListItem("Bono por Desarrollo de Red", 8))
            If bonoglobal Then
                drop.Items.Add(New ListItem("Bono Global por Avance de Rango", 9))
            End If
        Catch ex As Exception

        End Try
    End Sub
    Sub limpiacampos(ByVal pagina As Page)
        Dim contenedor As ContentPlaceHolder = pagina.Master.FindControl("ContentPlaceHolder1")


        For Each child As Control In contenedor.Controls
            If TypeOf child Is TextBox Then
                CType(child, TextBox).Text = ""
            Else

            End If
            If TypeOf child Is UpdatePanel Then
                Dim panel As UpdatePanel = contenedor.FindControl("Panel1")
                For Each nieto As Control In panel.Controls
                    If TypeOf nieto Is TextBox Then
                        CType(nieto, TextBox).Text = ""
                    End If
                Next


            End If
        Next
    End Sub

    Sub RevisaCompra(ByVal compra As Integer)
        Try
            Dim activacion, excedente, asociado, puntos, inscripcion As Integer
            Dim inicioactivacion, finactivacion As Date
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = "SELECT asociado, activacion, excedente, puntos, inscripcion, inicioactivacion, finactivacion FROM compras WHERE id=" & compra.ToString

            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()
            While dtrTeam.Read
                asociado = dtrTeam(0)
                activacion = dtrTeam(1)
                excedente = dtrTeam(2)
                puntos = dtrTeam(3)
                inscripcion = dtrTeam(4)
                If Not IsDBNull(dtrTeam(5)) Then inicioactivacion = dtrTeam(5)
                If Not IsDBNull(dtrTeam(6)) Then finactivacion = dtrTeam(6)
            End While

            sqlConn.Close()
            If inscripcion > 0 Then Exit Sub
            Dim activo As Integer = mistatus(asociado)
            If activacion = 1 Then
                'revisa si asociado sigue inactivo
                If activo = 1 Then
                    'si está activo, cambia compra a excedente
                    If inicioactivacion > Today Then Exit Sub

                    sqlConn.Open()
                    strTeamQuery = "UPDATE compras SET activacion=0, excedente=1, inicioactivacion=NULL, finactivacion=NULL WHERE id=" & compra.ToString
                    cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                    cmdFetchTeam.ExecuteNonQuery()


                    sqlConn.Close()
                Else
                    'revisa las fechas
                    If finactivacion < Today Then
                        Dim fechainicio As Date = Today
                        Dim fechafin As Date = DateAdd(DateInterval.Day, -1, misiguienteactivacion(asociado))
                        sqlConn.Open()
                        strTeamQuery = "UPDATE compras SET activacion=1, excedente=0, inicioactivacion='" & fechainicio.ToString("yyyy/MM/dd") & "', finactivacion='" & fechafin.ToString("yyyy/MM/dd") & "' WHERE id=" & compra.ToString
                        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                        cmdFetchTeam.ExecuteNonQuery()
                    End If
                End If
            Else
                'revisa si asociado sigue activo
                If activo = 0 Then
                    'si está inactivo, cambia compra a activación
                    Dim fechainicio As Date = Today
                    Dim fechafin As Date = DateAdd(DateInterval.Day, -1, misiguienteactivacion(asociado))
                    sqlConn.Open()
                    strTeamQuery = "UPDATE compras SET activacion=1, excedente=0, inicioactivacion='" & fechainicio.ToString("yyyy/MM/dd") & "', finactivacion='" & fechafin.ToString("yyyy/MM/dd") & "' WHERE id=" & compra.ToString
                    cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                    cmdFetchTeam.ExecuteNonQuery()


                    sqlConn.Close()
                End If
            End If
            
        Catch ex As Exception

        End Try
    End Sub

End Class
