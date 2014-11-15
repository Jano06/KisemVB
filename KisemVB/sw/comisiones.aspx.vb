Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Media
Partial Class sw_comisiones2
    Inherits System.Web.UI.Page
    Dim asociados, asociadosactivoseinactivos As New List(Of Integer)
    Dim view As New DataView()
    Dim viewpuntos As New DataView()
    Dim viewasociados As New DataView()
    Dim antepasado As Integer = 0
    Dim raiz As Integer
    Dim estado As String = ""
    Dim idperiodo As Integer
    Dim idciclo As Integer = 0
    Dim iniciociclo, mediociclo, finciclo As Date ' para ciclos de calificación
    Dim funciones As New funciones
    Dim pago1bono6, pago2bono6, pago1bono7, pago2bono7, pago1bono8, pago2bono8, pagominimobono678 As Decimal
#Region "Eventos de página"
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try
            If CDate(Me.de.Text) >= CDate(Me.a.Text) Then
                Me.mensajes.Text = "La fecha de inicio debe ser menor a la fecha final"
                Exit Sub
            End If
            Session("errorcomisiones") = ""

            eliminainactivosde4meses()
            flush()
            llenavistadeasociados()
            If terminaciclo() Then
                mueverangos()
            End If

            ''estado = "Borra comisiones sin terminar"
            borracomisionesparciales()
            ''estado = "Genera periodo"
            defineperiodo()
            'idperiodo = 13
            ''estado = "Inserta periodo"
            insertaperiodo()
            ''estado = "Corre comisiones"
            correcomisiones()
            ''estado = "Abandera comisiones"
            abanderacomisiones()
            'revisa ciclos




            Me.mensajes.Text = "Comisiones generadas con éxito" '& Session("mensaje")
            Me.mensajes.Visible = True

            Me.Button1.Enabled = True
            'calcula los pagos
            'calculatotalcompras()
            'calculacomisiones()
            'grafica()
            Me.Panel1.Visible = True
            Me.periodo.Text = (CInt(Me.periodo.Text) + 1).ToString


            SystemSounds.Exclamation.Play()
        Catch ex As Exception
            Me.mensajes.Text = Session("errorcomisiones")
            'Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try


        'Me.Image1.Visible = False
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            calculatotalcompras()
            calculacomisiones()
            'grafica()
            llenagrid()
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
        objEtiqueta.Text = "Correr Comisiones"
        Dim fecha As Date = Today
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
        Me.de.Text = de.ToString("dd/M/yyyy")
        Me.a.Text = a.ToString("dd/M/yyyy")
        If Not IsPostBack Then
            defineperiodo()
            Me.periodo.Text = idperiodo.ToString
        End If
    End Sub
    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        bono4(3510)

    End Sub
#End Region


#Region "Ciclos"
    Sub mueverangos()
        estado += "Inicia mueverangos. "
        Dim drv As DataRowView
        Session("mensaje") = ""
        viewasociados.RowFilter = ""
        For Each drv In viewasociados

            checarango(drv("id"), drv("rango"))
        Next
        ' Session("mensaje") &= " Num de asociados para mover rango " & viewasociados.Count
        estado += "termina mueverangos. "
    End Sub
    Sub checarango(ByVal idasociado As Integer, ByVal rango As Integer)
        Session("mensaje") = "El último en checar rango fue el Asociado " & idasociado.ToString & " Rango " & rango.ToString
        Dim de As Date = iniciociclo
        'este es el anterior, antes de agregar el pago hasta el lunes
        'Dim qry_paquetesypuntos As String = "SELECT comprasdetalle.cantidad, comprasdetalle.paquete, asociados.recorrido, asociados.ladosrecorrido " & _
        '"FROM asociados INNER JOIN compras ON asociados.id=compras.asociado INNER JOIN `comprasdetalle` ON compras.id=comprasdetalle.compra " & _
        '"WHERE asociados.recorrido LIKE '%." & idasociado.ToString & ".%' AND comprasdetalle.paquete BETWEEN 1 AND 3 AND compras.statuspago='PAGADO' AND compras.fecha>='" & de.ToString("yyyy/MM/dd") & "' AND compras.fecha<='" & cdate(Me.a.Text).ToString("yyyy/MM/dd")  & "'"

        'Nueva qry, agregando el pago hasta el lunes siguiente del fin de ciclo
        Dim qry_paquetesypuntos As String = "SELECT comprasdetalle.cantidad, comprasdetalle.paquete, asociados.recorrido, asociados.ladosrecorrido " & _
        "FROM asociados INNER JOIN compras ON asociados.id=compras.asociado INNER JOIN `comprasdetalle` ON compras.id=comprasdetalle.compra " & _
        "WHERE asociados.recorrido LIKE '%." & idasociado.ToString & ".%' AND comprasdetalle.paquete BETWEEN 1 AND 3 AND compras.statuspago='PAGADO' AND compras.fecha>='" & de.ToString("yyyy/MM/dd") & "' AND (compras.fecha<='" & CDate(Me.a.Text).ToString("yyyy/MM/dd") & "' OR (compras.fechaorden BETWEEN '" & de.ToString("yyyy/MM/dd") & "' AND '" & CDate(Me.a.Text).ToString("yyyy/MM/dd") & "' AND (compras.fecha='" & DateAdd(DateInterval.Day, 1, CDate(Me.a.Text)).ToString("yyyy/MM/dd") & "' OR compras.fecha='" & DateAdd(DateInterval.Day, 2, CDate(Me.a.Text)).ToString("yyyy/MM/dd") & "' OR compras.fecha='" & DateAdd(DateInterval.Day, 3, CDate(Me.a.Text)).ToString("yyyy/MM/dd") & "'  ))) " & _
        "AND compras.id NOT IN (" & _
                      "Select id FROM compras " & _
                      "WHERE fechaorden < '" & de.ToString("yyyy/MM/dd") & "' " & _
                      "AND fecha < '" & DateAdd(DateInterval.Day, 2, de).ToString("yyyy/MM/dd") & "' " & _
                      ") "






        Dim qry_directosporlado As String = "SELECT Count(asociados.ID) AS directos, asociados.ladopatrocinador AS lado " & _
                                                   "FROM(asociados) " & _
                                                   "WHERE(((asociados.patrocinador)=" & idasociado.ToString & ") AND (asociados.status=1  OR '" & CDate(a.Text).ToString("yyyy/MM/dd") & "' BETWEEN inicioactivacion AND finactivacion)  ) AND asociados.ptsmes>350 " & _
                                                   "GROUP BY asociados.patrocinador, asociados.ladopatrocinador " & _
                                                   "ORDER BY asociados.patrocinador, asociados.ladopatrocinador ;"
        Dim qry_inactivosporlado As String = "SELECT Count(asociados.ID) AS directos, asociados.ladopatrocinador AS lado " & _
                                                   "FROM(asociados) " & _
                                                   "WHERE(((asociados.patrocinador)=" & idasociado.ToString & ") AND (asociados.status=0  )  ) " & _
                                                   "GROUP BY asociados.patrocinador, asociados.ladopatrocinador " & _
                                                   "ORDER BY asociados.patrocinador, asociados.ladopatrocinador ;"

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(qry_paquetesypuntos, sqlConn)

        Dim dtrTeam As MySqlDataReader

        'directos por lado período 1
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim recorrido, ladosrecorrido As String
        Dim recorridoarray(), ladosarray() As String
        Dim izq, der As Integer
        While dtrTeam.Read



            recorrido = dtrTeam(2)

            ladosrecorrido = dtrTeam(3)

            recorridoarray = Split(recorrido, ".")
            ladosarray = Split(ladosrecorrido, ".")
            Dim posicion As Integer = 0
            For posicion = 0 To recorridoarray.Length - 1
                If recorridoarray(posicion) = idasociado.ToString Then
                    Exit For
                End If


            Next
            If UCase(ladosarray(posicion)) = "D" Then
                Select Case dtrTeam(1)
                    Case 1
                        der += 25 * dtrTeam(0)
                    Case 2
                        der += 50 * dtrTeam(0)
                    Case 3
                        der += 75 * dtrTeam(0)
                End Select





            Else
                Select Case dtrTeam(1)
                    Case 1
                        izq += 25 * dtrTeam(0)
                    Case 2
                        izq += 50 * dtrTeam(0)
                    Case 3
                        izq += 75 * dtrTeam(0)
                End Select
            End If
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Dim totales As Integer = der + izq
        Dim ladomenor As Integer
        If der > izq Then
            ladomenor = izq
        Else
            ladomenor = der
        End If

        'directos
        cmdFetchTeam = New MySqlCommand(qry_directosporlado, sqlConn)



        'directos por lado 
        Dim directosizq, directosder, directostotales As Integer
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If dtrTeam(1).ToString = "D" Then
                directosder = dtrTeam(0)
            Else
                directosizq = dtrTeam(0)
            End If
        End While
        sqlConn.Close()
        directostotales = directosder + directosizq
        'inactivos
        cmdFetchTeam = New MySqlCommand(qry_inactivosporlado, sqlConn)
        Dim inactivosizq, inactivosder As Integer
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If dtrTeam(1).ToString = "D" Then
                inactivosder = dtrTeam(0)
            Else
                inactivosizq = dtrTeam(0)
            End If
        End While
        sqlConn.Close()

        Dim rangoactual As Integer = 1
        'colaborador 
        If directostotales >= 3 And directosizq >= 1 And directosder >= 1 Then rangoactual = 2
        If directosder >= 1 And directosizq >= 1 And izq >= 10 And der >= 10 And totales >= 550 And ladomenor >= 165 Then rangoactual = 3
        If (directosder + directosizq) >= 3 And directosder >= 1 And directosizq >= 1 And izq >= 20 And der >= 20 And totales >= 1300 And ladomenor >= 390 Then rangoactual = 4
        If (directosder + directosizq) >= 3 And directosder >= 1 And directosizq >= 1 And izq >= 50 And der >= 50 And totales >= 2500 And ladomenor >= 750 Then rangoactual = 5
        If directosder >= 2 And directosizq >= 2 And izq >= 75 And der >= 75 And totales >= 5600 And ladomenor >= 1680 Then rangoactual = 6
        If directosder >= 2 And directosizq >= 2 And izq >= 160 And der >= 160 And totales >= 11250 And ladomenor >= 3375 Then rangoactual = 7
        If directosder >= 2 And directosizq >= 2 And izq >= 400 And der >= 400 And totales >= 22500 And ladomenor >= 9000 Then rangoactual = 8
        If directosder >= 2 And directosizq >= 2 And izq >= 800 And der >= 800 And totales >= 45000 And ladomenor >= 18000 Then rangoactual = 9


        actualizarango(idasociado, rango, rangoactual)


    End Sub
  
    Sub actualizarango(ByVal asociado As Integer, ByVal rangotitulo As Integer, ByVal rango As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        If rangotitulo < rango Then
            strTeamQuery = "UPDATE asociados SET rango=" & rango.ToString & ", rangopago=" & rango.ToString & " WHERE id=" & asociado.ToString
        Else
            strTeamQuery = "UPDATE asociados SET  rangopago=" & rango.ToString & " WHERE id=" & asociado.ToString
        End If

        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()

        'inserta cambio
        If rangotitulo < rango Then
            strTeamQuery = "INSERT INTO rangoscambios (ciclo, asociado, rango, rangopago) VALUES (" & idciclo.ToString & "," & asociado.ToString & "," & rango.ToString & "," & rango.ToString & ")"

        Else
            strTeamQuery = "INSERT INTO rangoscambios (ciclo, asociado, rango, rangopago) VALUES (" & idciclo.ToString & "," & asociado.ToString & "," & rangotitulo.ToString & "," & rango.ToString & ")"

        End If
        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()

    End Sub
    Function terminaciclo() As Boolean
        estado += "Inicia terminaciclo. "
        Dim respuesta As Boolean = False

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT inicio, fin, id  FROM ciclos WHERE fin>='" & CDate(Me.de.Text).ToString("yyyy/MM/dd") & "' AND  fin<='" & CDate(Me.a.Text).ToString("yyyy/MM/dd") & "'"

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            respuesta = True
            iniciociclo = dtrTeam(0)

            finciclo = dtrTeam(1)
            idciclo = dtrTeam(2)
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        estado += "Termina terminaciclo. "
        Return respuesta
    End Function
#End Region
#Region "comisiones"
    Sub flush()
        'elimina puntos de asociados inactivos desde el viernes pasado
        '1 calcula fecha de viernes anterior
        Dim fechaflush As Date = DateAdd(DateInterval.Month, -1, CDate(Me.de.Text))

        '2 si alguien tiene finactivacion<viernes anterior, le borra todos los puntos
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        strTeamQuery = "DELETE FROM puntosasociados WHERE asociado IN ( SELECT id FROM asociados WHERE finactivacion<'" & fechaflush.ToString("yyyy/MM/dd") & "')"

        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()


    End Sub
    Sub eliminainactivosde4meses()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim fechadesactivacion As Date = DateAdd(DateInterval.Month, -4, Date.Today)
        Dim strTeamQuery As String = "UPDATE asociados SET status=2 WHERE finactivacion<'" & fechadesactivacion.Year.ToString & "/" & fechadesactivacion.Month.ToString & "/" & fechadesactivacion.Day.ToString & "'"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub

    Sub abanderacomisiones()
        estado += "Inicia abandera comisiones. "
        'inserta en tabla pagos
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "UPDATE pagos SET status=1 WHERE status=0"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
        strTeamQuery = "UPDATE periodos SET status=2 WHERE id=" & idperiodo.ToString
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
        estado += "termina abanderacomisiones. "
    End Sub
    Sub borracomisionesparciales()
        estado += "Inicia borracomisiones parciales. "
        'inserta en tabla pagos
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "DELETE FROM pagos WHERE status=0"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
        strTeamQuery = "DELETE FROM periodos WHERE status=0"
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
        estado += "Termina borracomisiones parciales. "
    End Sub
    Sub cuentaasociados()
        estado += "Inicia Cuenta asociados. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT COUNT(id) FROM asociados WHERE status=1"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cuenta As Integer = 0
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then cuenta = dtrTeam(0)


        End While

        sqlConn.Close()
        sqlConn.Dispose()
        'Me.activos.Text = cuenta.ToString
        estado += "Termina Cuenta asociados. "
    End Sub
    Sub buscaraiz()
        estado += "Inicia Busca raíz. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT MIN(id) FROM asociados"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cuenta As Integer = 0
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then raiz = dtrTeam(0)


        End While

        sqlConn.Close()
        sqlConn.Dispose()
        estado += "Termina busca raíz. "
    End Sub
    Sub recogetodoslosasociados()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        'Dim strTeamQuery As String = "SELECT id, rango, ptsmes FROM asociados WHERE status=1  OR '" & a.Text & "' BETWEEN inicioactivacion AND finactivacion  ORDER BY id DESC"
        'nuevo para evitar que se activen el lunes
        Dim strTeamQuery As String = "SELECT id FROM asociados WHERE status<2  ORDER BY id ASC"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            asociadosactivoseinactivos.Add(dtrTeam("id"))




        End While

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub correcomisiones()

        estado += "Inicia correcomisiones. "

        Try
            'estado = "Busca raíz"
            buscaraiz()
            recogetodoslosasociados()
            'estado = "Conecta para sacar asociados activos"
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            'Dim strTeamQuery As String = "SELECT id, rango, ptsmes FROM asociados WHERE status=1  OR '" & a.Text & "' BETWEEN inicioactivacion AND finactivacion  ORDER BY id DESC"
            'nuevo para evitar que se activen el lunes
            Dim strTeamQuery As String = "SELECT id, rango, ptsmes FROM asociados WHERE status=1  OR '" & CDate(a.Text).ToString("yyyy/MM/dd") & "' BETWEEN inicioactivacion AND finactivacion  ORDER BY id DESC"
            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read
                asociados.Add(dtrTeam("id"))




            End While

            sqlConn.Close()
            sqlConn.Dispose()
            'estado = "Inicia Bono 1"

            bono1()

            'estado = "Inicia Bono 2"
            bono2()

            'estado = "Inicia Bono 3"
            'bono3()

            estado = "Inicia Bono 4"
            bono4()

            'estado = "Inicia Bono 5"
            bono5()
            'estado = "Inicia Bono 6"
            bono6()
            'estado = "Inicia Bono 7"
            bono7()
            'estado = "Inicia Bono 8"
            bono8()

            insertadetallesbono4nopagados()

        Catch ex As Exception
            Me.mensajes.Text = estado & ex.Message.ToString
            Me.mensajes.Visible = True
            Session("errorcomisiones") = estado & ex.Message.ToString
        End Try
        estado += "Termina correcomisiones. "
    End Sub
    Sub insertadetallesbono4nopagados()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        Dim inicio As Date = CDate(Me.de.Text)
        Dim fin As Date = CDate(Me.a.Text)

        For i = 0 To asociadosactivoseinactivos.Count - 1
            If asociadosactivoseinactivos(i) > 0 Then





                strTeamQuery = "SELECT puntosasociados.Asociado , sum( if( `Lado` = 'D', `PorPagar` , 0 ) ) AS D, sum( if( `Lado` = 'I', `PorPagar` , 0 ) ) AS I "
                strTeamQuery += "FROM `puntosasociados`  INNER JOIN compras ON puntosasociados.compra=compras.id "
                strTeamQuery += "WHERE  ((compras.fecha<='" & fin.Year.ToString & "/" & fin.Month.ToString & "/" & fin.Day.ToString & "') OR(compras.fecha='" & DateAdd(DateInterval.Day, 3, fin).ToString("yyyy/MM/dd") & "' AND compras.fechaorden='" & fin.ToString("yyyy/MM/dd") & "')) AND compras.statuspago='PAGADO' AND puntosasociados.asociado=" & asociadosactivoseinactivos(i).ToString & " "
                strTeamQuery += "GROUP BY `Asociado`  "
             
                cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                sqlConn.Open()
                dtrTeam = cmdFetchTeam.ExecuteReader()
                Dim bono As Integer = 0

                While dtrTeam.Read

                    If dtrTeam(2) >= dtrTeam(1) Then
                        insertadetallebono4(asociadosactivoseinactivos(i), idperiodo, dtrTeam(1), dtrTeam(2), 0)

                    Else
                        insertadetallebono4(asociadosactivoseinactivos(i), idperiodo, dtrTeam(2), dtrTeam(1), 0)
                    End If


                End While

                sqlConn.Close()
                sqlConn.Dispose()







            End If
        Next





    End Sub
    Sub defineperiodo()
        estado += "Inicia defineperiodo. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT MAX(id) FROM periodos WHERE status>0"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then idperiodo = dtrTeam(0)


        End While

        sqlConn.Close()
        idperiodo += 1
        estado += "Termina defineperiodo. "
    End Sub
    Sub insertaperiodo()
        estado += "Inicia insertaperiodo. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        strTeamQuery = "INSERT INTO periodos(id, inicio, final, status) VALUES(" & idperiodo.ToString & ", '" & CDate(de.Text).ToString("yyyy/MM/dd") & "', '" & CDate(a.Text).ToString("yyyy/MM/dd") & "', 0)"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        estado += "termina insertaperiodo. "
    End Sub
  
    Function estaactivo(ByVal asociado As Integer) As Boolean
        Dim respuesta As Boolean = False
        For i = 0 To asociados.Count - 1
            If asociado = asociados(i) Then
                respuesta = True
                Exit For
            End If

        Next



        Return respuesta
    End Function
   
    Sub bono1()
        estado += "Inicia Bono1. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""

        'para incluir el lunes
        Dim fechafinalperiodo As Date = CDate(Me.a.Text)
        fechafinalperiodo = DateAdd(DateInterval.Day, 3, fechafinalperiodo)
        Dim fechafinalperiodoanterior As Date = DateAdd(DateInterval.Day, -1, CDate(Me.de.Text))
        Dim fechalunesanterior As Date = DateAdd(DateInterval.Day, 2, CDate(Me.de.Text))
        strTeamQuery = " SELECT Count( compras.ID ) AS CountOfID, compras.asociado " & _
                      "FROM(compras) INNER JOIN comprasdetalle ON compras.id = comprasdetalle.compra " & _
                      "WHERE(comprasdetalle.paquete = 3) AND compras.excedente=1 " & _
                      "AND compras.fecha >= '" & CDate(de.Text).ToString("yyyy/MM/dd") & "' " & _
                      "AND compras.fecha <= '" & fechafinalperiodo.Year.ToString & "/" & fechafinalperiodo.Month.ToString & "/" & fechafinalperiodo.Day.ToString & "' " & _
                      "AND compras.statuspago = 'PAGADO' " & _
                      "AND compras.fechaorden <= '" & CDate(a.Text).ToString("yyyy/MM/dd") & "' " & _
                      "AND compras.id NOT IN (" & _
                      "Select id FROM compras " & _
                      "WHERE fechaorden <= '" & fechafinalperiodoanterior.Year.ToString & "/" & fechafinalperiodoanterior.Month.ToString & "/" & fechafinalperiodoanterior.Day.ToString & "' " & _
                      "AND fecha >= '" & CDate(de.Text).ToString("yyyy/MM/dd") & "' " & _
                      "AND fecha <= '" & fechalunesanterior.Year.ToString & "/" & fechalunesanterior.Month.ToString & "/" & fechalunesanterior.Day.ToString & "' " & _
                      ") " & _
                      "GROUP BY compras.asociado"



        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cuenta As Integer = 0
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then insertabono1(dtrTeam(1), dtrTeam(0))


        End While

        sqlConn.Close()
        sqlConn.Dispose()
        estado += "Termina Bono1. "
    End Sub
    Sub insertabono1(ByVal asociado As Integer, ByVal cuenta As Integer)
        If Not estaactivo(asociado) Then
            Exit Sub
        End If
        Dim factura As Integer = 0

        Dim mispuntos As Integer = mispuntosdelmes(asociado)
        Dim montobono1 As Integer = 200
        Dim retencionisr, iva, retencioniva As Decimal
        If funciones.facturo(asociado) Then
            factura = 1
            retencionisr = 0
            iva = cuenta * montobono1 * System.Configuration.ConfigurationManager.AppSettings("iva")
            retencioniva = cuenta * montobono1 * 0.1
        Else
            retencionisr = funciones.calculaisr(cuenta * montobono1)

            iva = 0
            retencioniva = 0
        End If

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos, factura, retencionisr, iva, retencioniva) VALUES(" & asociado.ToString & ", '" & CDate(de.Text).ToString("yyyy/MM/dd") & "', '" & CDate(a.Text).ToString("yyyy/MM/dd") & "', 1, " & (cuenta * montobono1).ToString & ", " & idperiodo.ToString & ",0, " & mispuntos.ToString & ", " & factura.ToString & ", " & retencionisr.ToString & ", " & iva.ToString & ", " & retencioniva.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub bono2()
        estado += "Inicia bono2. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT asociados.id, asociados.orden, asociados.patrocinador, compras.puntos  FROM asociados INNER JOIN compras ON asociados.id=compras.asociado WHERE compras.fecha<='" & CDate(a.Text).ToString("yyyy/MM/dd") & "' AND compras.fecha>='" & CDate(de.Text).ToString("yyyy/MM/dd") & "'  AND compras.inscripcion>0  ORDER BY patrocinador DESC;"

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim bonoa As Integer = 0
        Dim bonob As Integer = 0
        Dim bonopuntos As Integer = 0
        Dim id As Integer = 0
        While dtrTeam.Read
            If dtrTeam(2) <> id And id > 0 Then
                insertabono2(id, bonoa, bonob, bonopuntos)
                bonoa = 0
                bonob = 0
                bonopuntos = 0
                id = dtrTeam(2)
                If dtrTeam(1) < 3 Then
                    bonoa += 1
                Else
                    bonob += 1
                End If
                If dtrTeam(3) = 650 Then bonopuntos += 50
                If dtrTeam(3) = 900 Then bonopuntos += 100
            Else
                id = dtrTeam(2)
                If dtrTeam(1) < 3 Then
                    bonoa += 1
                Else
                    bonob += 1
                End If
                If dtrTeam(3) = 650 Then bonopuntos += 50
                If dtrTeam(3) = 900 Then bonopuntos += 100
            End If



        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If id >= raiz Then insertabono2(id, bonoa, bonob, bonopuntos)
        estado += "Termina bono2. "
    End Sub
  
    Sub insertabono2(ByVal asociado As Integer, ByVal bonoa As Integer, ByVal bonob As Integer, Optional ByVal bonopuntos As Integer = 0)
        If Not estaactivo(asociado) Then
            Exit Sub
        End If
        Dim factura As Integer = 0
       
        Dim montobono2a As Integer = 220
        Dim montobono2b As Integer = 220
        Dim mispuntos As Integer = mispuntosdelmes(asociado)
        Dim retencionisr, iva, retencioniva, total As Decimal
        total = (bonoa * montobono2a) + (bonob * montobono2b)
        If funciones.facturo(asociado) Then
            factura = 1
            retencionisr = 0
            iva = total * System.Configuration.ConfigurationManager.AppSettings("iva")
            retencioniva = total * 0.1
        Else
            retencionisr = funciones.calculaisr(total)

            iva = 0
            retencioniva = 0
        End If



        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        'Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos) VALUES(" & asociado.ToString & ", '" & CDate(de.Text).ToString("yyyy/MM/dd") & "', '" & CDate(a.Text).ToString("yyyy/MM/dd") & "', 2, " & ((bonoa * montobono2a) + (bonob * montobono2b) + bonopuntos).ToString & ", " & idperiodo.ToString & ",0, " & mispuntos.ToString & ")"
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos, factura, retencionisr, iva, retencioniva) VALUES(" & asociado.ToString & ", '" & CDate(de.Text).ToString("yyyy/MM/dd") & "', '" & CDate(a.Text).ToString("yyyy/MM/dd") & "', 2, " & ((bonoa * montobono2a) + (bonob * montobono2b)).ToString & ", " & idperiodo.ToString & ",0, " & mispuntos.ToString & ", " & factura.ToString & ", " & retencionisr.ToString & ", " & iva.ToString & ", " & retencioniva.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
  
    Sub bono3()
        estado += "Inicia bono3. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, bono6  " & _
                        "FROM asociados   " & _
                        "WHERE Orden<3 AND  FInsc<='" & CDate(a.Text).ToString("yyyy/MM/dd") & "' AND FInsc>='" & CDate(de.Text).ToString("yyyy/MM/dd") & "' " & _
                        "ORDER BY bono6 "
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim bono As Integer = 0
        Dim id As Integer = 0
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(1)) Then

                If dtrTeam(1) <> id And id > 0 Then
                    insertabono3(id, bono)
                    bono = 0

                    id = dtrTeam(1)
                    bono += 1
                Else
                    id = dtrTeam(1)
                    bono += 1
                End If

            End If
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If id >= raiz Then insertabono3(id, bono)
        estado += "termina bono3. "
    End Sub

    Sub insertabono3(ByVal asociado As Integer, ByVal cantidad As Integer)
        If Not estaactivo(asociado) Then
            Exit Sub
        End If
        Dim factura As Integer = 0
        
        Dim montobono3 As Integer = 60
        Dim mispuntos As Integer = mispuntosdelmes(asociado)
        Dim retencionisr, iva, retencioniva, total As Decimal
        total = (cantidad * montobono3)
        If funciones.facturo(asociado) Then
            factura = 1
            retencionisr = 0
            iva = total * System.Configuration.ConfigurationManager.AppSettings("iva")
            retencioniva = total * 0.1
        Else
            retencionisr = funciones.calculaisr(total)

            iva = 0
            retencioniva = 0
        End If
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos, factura, retencionisr, iva, retencioniva) VALUES(" & asociado.ToString & ", '" & CDate(de.Text).ToString("yyyy/MM/dd") & "', '" & CDate(a.Text).ToString("yyyy/MM/dd") & "', 3, " & (cantidad * montobono3).ToString & ", " & idperiodo.ToString & ",0, " & mispuntos.ToString & ", " & factura.ToString & ", " & retencionisr.ToString & ", " & iva.ToString & ", " & retencioniva.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
  

    Sub bono4(Optional ByVal asociadoarevisar As Integer = 0)
        estado += "Inicia bono4. "
        llenavistadeporcentajes()
        llenavistadepuntos()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader



        'strTeamQuery = "SELECT `Asociado` , sum( if( `Lado` = 'D', `PorPagar` , 0 ) ) AS D, sum( if( `Lado` = 'I', `PorPagar` , 0 ) ) AS I " & _
        '                "FROM `puntosasociados` " & _
        '                "GROUP BY `Asociado` " & _
        '                "HAVING(D > 0) AND (I >0) "
        Dim inicio As Date = CDate(Me.de.Text)
        Dim fin As Date = CDate(Me.a.Text)

        strTeamQuery = "SELECT puntosasociados.Asociado , sum( if( `Lado` = 'D', `PorPagar` , 0 ) ) AS D, sum( if( `Lado` = 'I', `PorPagar` , 0 ) ) AS I "
        strTeamQuery += "FROM `puntosasociados`  INNER JOIN compras ON puntosasociados.compra=compras.id "
        strTeamQuery += "WHERE  ((compras.fecha<='" & fin.Year.ToString & "/" & fin.Month.ToString & "/" & fin.Day.ToString & "') OR(compras.fecha='" & DateAdd(DateInterval.Day, 3, fin).ToString("yyyy/MM/dd") & "' AND compras.fechaorden='" & fin.ToString("yyyy/MM/dd") & "')) AND compras.statuspago='PAGADO' "
        If asociadoarevisar > 0 Then
            strTeamQuery += " AND puntosasociados.asociado=" & asociadoarevisar.ToString & " "
        End If
        strTeamQuery += "GROUP BY `Asociado`  "
        strTeamQuery += "HAVING  (D > 0) AND (I >0)"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim bono As Integer = 0

        While dtrTeam.Read

            If dtrTeam(2) >= dtrTeam(1) Then
                insertabono4(dtrTeam(0), dtrTeam(1), "D", dtrTeam(2))
            Else
                insertabono4(dtrTeam(0), dtrTeam(2), "I", dtrTeam(1))
            End If


        End While

        sqlConn.Close()
        sqlConn.Dispose()


        estado += "Termina bono4. "




    End Sub
    Sub insertadetallebono4(ByVal asociado As Integer, ByVal corte As Integer, ByVal puntosfinalesi As Integer, ByVal puntosfinalesd As Integer, ByVal porcentajedepago As Decimal)
        'sacamos los puntos de la semana
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim nuevosi, nuevosd As Integer
        Dim dtrTeam As MySqlDataReader
        Dim hasta As Date = CDate(Me.a.Text)
        Dim de As Date = CDate(Me.de.Text)

        strTeamQuery = "SELECT SUM(puntosasociados.porpagar), puntosasociados.lado FROM puntosasociados INNER JOIN compras ON compras.id=puntosasociados.compra WHERE ((compras.fecha<='" & hasta.ToString("yyyy/MM/dd") & "' AND compras.fecha>='" & de.ToString("yyyy/MM/dd") & "' AND NOT (compras.fecha='" & DateAdd(DateInterval.Day, 2, de).ToString("yyyy/MM/dd") & "' AND compras.fechaorden='" & DateAdd(DateInterval.Day, -1, de).ToString("yyyy/MM/dd") & "') ) OR(compras.fecha='" & DateAdd(DateInterval.Day, 3, hasta).ToString("yyyy/MM/dd") & "' AND compras.fechaorden='" & hasta.ToString("yyyy/MM/dd") & "')) AND compras.statuspago='PAGADO' AND puntosasociados.asociado=" & asociado.ToString & " GROUP BY puntosasociados.lado"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim bono As Integer = 0

        While dtrTeam.Read

            If UCase(dtrTeam(1)) = "D" Then
                nuevosd = dtrTeam(0)
            Else
                nuevosi = dtrTeam(0)
            End If


        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Dim pagados As Integer = 0
        If puntosfinalesd > puntosfinalesi Then
            pagados = puntosfinalesi
        Else
            pagados = puntosfinalesd
        End If
        porcentajedepago = porcentajedepago * 100
     
        'busca puntosfinales  para registro actual



        strTeamQuery = "SELECT (inicialesi+nuevosi-pagados) AS finalesi, (inicialesd+nuevosd-pagados) AS finalesd FROM puntosdetalle  WHERE asociado=" & asociado.ToString & " AND corte=" & (corte - 1).ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim bandera As Boolean = False
        Dim inicialesi, inicialesd As Integer
        While dtrTeam.Read

            inicialesi = dtrTeam(0)
            inicialesd = dtrTeam(1)
            bandera = True
        End While
        If inicialesd <= 0 Then inicialesd = 0
        If inicialesi <= 0 Then inicialesi = 0
        sqlConn.Close()
        If Not bandera Then


            strTeamQuery = "SELECT SUM(puntosasociados.porpagar), puntosasociados.lado FROM puntosasociados INNER JOIN compras ON puntosasociados.compra=compras.id WHERE puntosasociados.asociado=" & asociado.ToString & " AND (compras.fecha<='" & DateAdd(DateInterval.Day, -1, CDate(Me.de.Text)).ToString("yyyy/MM/dd") & "' OR (compras.fecha='" & DateAdd(DateInterval.Day, 2, CDate(Me.de.Text)).ToString("yyyy/MM/dd") & "' AND compras.fechaorden='" & DateAdd(DateInterval.Day, -1, CDate(Me.de.Text)).ToString("yyyy/MM/dd") & "')) GROUP BY puntosasociados.lado "

            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()
            While dtrTeam.Read
                If dtrTeam(1) = "I" Then
                    inicialesi = dtrTeam(0)
                Else
                    inicialesd = dtrTeam(0)
                End If

            End While

            sqlConn.Close()
        End If




        'hasta acá
        strTeamQuery = "INSERT INTO puntosdetalle (asociado, inicialesi, inicialesd, nuevosi, nuevosd, pagados, porcentaje, corte) VALUES (" & asociado.ToString & "," & inicialesi.ToString & "," & inicialesd.ToString & "," & nuevosi.ToString & "," & nuevosd.ToString & "," & pagados.ToString & "," & porcentajedepago.ToString & "," & corte.ToString & ")"
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
        If porcentajedepago = 0 Then Exit Sub
        For i = 0 To asociadosactivoseinactivos.Count - 1
            If asociadosactivoseinactivos(i) = asociado Then
                asociadosactivoseinactivos(i) = 0
            End If
        Next
    End Sub
    Sub insertadetallebono4_16dic2013(ByVal asociado As Integer, ByVal corte As Integer, ByVal puntosfinalesi As Integer, ByVal puntosfinalesd As Integer, ByVal porcentajedepago As Decimal)
        'sacamos los puntos de la semana
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim nuevosi, nuevosd As Integer
        Dim dtrTeam As MySqlDataReader
        Dim hasta As Date = CDate(Me.a.Text)
        Dim de As Date = CDate(Me.de.Text)

        ' 4/12 2013 strTeamQuery = "SELECT SUM(puntosasociados.puntos), puntosasociados.lado FROM puntosasociados INNER JOIN compras ON compras.id=puntosasociados.compra WHERE compras.fecha>='" & de.Text & "' AND compras.fecha <='" & hasta.ToString("yyyy/MM/dd") & "' AND compras.statuspago='PAGADO' AND puntosasociados.asociado=" & asociado.ToString & " GROUP BY puntosasociados.lado"
        strTeamQuery = "SELECT SUM(puntosasociados.puntos), puntosasociados.lado FROM puntosasociados INNER JOIN compras ON compras.id=puntosasociados.compra WHERE ((compras.fecha<='" & hasta.ToString("yyyy/MM/dd") & "' AND compras.fecha>='" & de.ToString("yyyy/MM/dd") & "') OR(compras.fecha='" & DateAdd(DateInterval.Day, 3, hasta).ToString("yyyy/MM/dd") & "' AND compras.fechaorden='" & hasta.ToString("yyyy/MM/dd") & "')) AND compras.statuspago='PAGADO' AND puntosasociados.asociado=" & asociado.ToString & " GROUP BY puntosasociados.lado"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim bono As Integer = 0

        While dtrTeam.Read

            If UCase(dtrTeam(1)) = "D" Then
                nuevosd = dtrTeam(0)
            Else
                nuevosi = dtrTeam(0)
            End If


        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Dim pagados As Integer = 0
        If puntosfinalesd > puntosfinalesi Then
            pagados = puntosfinalesi
        Else
            pagados = puntosfinalesd
        End If
        porcentajedepago = porcentajedepago * 100
        '        strTeamQuery = "INSERT INTO puntosdetalle (asociado, inicialesi, inicialesd, nuevosi, nuevosd, pagados, porcentaje, corte) VALUES (" & asociado.ToString & "," & (puntosfinalesi - nuevosi).ToString & "," & (puntosfinalesd - nuevosd).ToString & "," & nuevosi.ToString & "," & nuevosd.ToString & "," & pagados.ToString & "," & porcentajedepago.ToString & "," & corte.ToString & ")"

        'busca puntosfinales  para registro actual
        strTeamQuery = "SELECT inicialesi, inicialesd, nuevosi, nuevosd, pagados FROM puntosdetalle WHERE asociado=" & asociado.ToString & " ORDER BY corte DESC"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim inicialesi, inicialesd As Integer
        While dtrTeam.Read

            inicialesi = dtrTeam(0) + dtrTeam(2) - dtrTeam(4)
            inicialesd = dtrTeam(1) + dtrTeam(3) - dtrTeam(4)
            Exit While
        End While

        sqlConn.Close()


        'hasta acá
        strTeamQuery = "INSERT INTO puntosdetalle (asociado, inicialesi, inicialesd, nuevosi, nuevosd, pagados, porcentaje, corte) VALUES (" & asociado.ToString & "," & inicialesi.ToString & "," & inicialesd.ToString & "," & nuevosi.ToString & "," & nuevosd.ToString & "," & pagados.ToString & "," & porcentajedepago.ToString & "," & corte.ToString & ")"
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
  
    Sub insertabono4(ByVal asociado As Integer, ByVal puntos As Integer, ByVal lado As String, ByVal puntoscontrarios As Integer)
        Dim porcentaje As Decimal = porcentajedepago(asociado)
        'detalle de bono 4
        Dim factura As Integer = 0
        If funciones.facturo(asociado) Then
            factura = 1
        End If
        Dim derecho As Integer = 0
        Dim izquierdo As Integer = 0
        Dim puntosparadetalle = puntos
        If lado = "D" Then
            derecho = puntosparadetalle
            izquierdo = puntoscontrarios
        Else
            derecho = puntoscontrarios
            izquierdo = puntosparadetalle
        End If



        Dim inicio As Date = CDate(Me.de.Text)
        Dim fin As Date = DateAdd(DateInterval.Day, 3, CDate(Me.a.Text))

        'recoge % pago asociado
        If Not estaactivo(asociado) Then
            Exit Sub
        End If
        insertadetallebono4(asociado, idperiodo, izquierdo, derecho, porcentaje)
        Dim mispuntos As Integer = mispuntosdelmes(asociado)
        Dim retencionisr, iva, retencioniva, total As Decimal
        total = puntos * porcentaje
        If funciones.facturo(asociado) Then
            factura = 1
            retencionisr = 0
            iva = total * System.Configuration.ConfigurationManager.AppSettings("iva")
            retencioniva = total * 0.1
        Else
            retencionisr = funciones.calculaisr(total)

            iva = 0
            retencioniva = 0
        End If
        'inserta pago
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos, factura, retencionisr, iva, retencioniva) VALUES(" & asociado.ToString & ", '" & CDate(de.Text).ToString("yyyy/MM/dd") & "', '" & CDate(a.Text).ToString("yyyy/MM/dd") & "', 4, " & (puntos * porcentaje).ToString & ", " & idperiodo.ToString & ",0, " & mispuntos.ToString & ", " & factura.ToString & ", " & retencionisr.ToString & ", " & iva.ToString & ", " & retencioniva.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
        'actualiza a pagados los puntos del lado indicado y de las fechas indicadas
        'strTeamQuery = "UPDATE puntosasociados SET status=1, porpagar=0 WHERE lado='" & lado & "' AND asociado=" & asociado.ToString
        '11/11/2013 strTeamQuery = "UPDATE puntosasociados  INNER JOIN compras ON puntosasociados.compra=compras.id SET puntosasociados.status=1, puntosasociados.porpagar=0 WHERE puntosasociados.lado='" & lado & "' AND puntosasociados.asociado=" & asociado.ToString & "  AND  compras.fecha<='" & fin.Year.ToString & "/" & fin.Month.ToString & "/" & fin.Day.ToString & "'  "
        '4/12/2013 strTeamQuery = "UPDATE puntosasociados  INNER JOIN compras ON puntosasociados.compra=compras.id SET  puntosasociados.porpagaranterior=puntosasociados.porpagar, corte=" & idperiodo.ToString & ", puntosasociados.status=1, puntosasociados.porpagar=0 WHERE puntosasociados.lado='" & lado & "' AND puntosasociados.asociado=" & asociado.ToString & "  AND  compras.fecha<='" & fin.Year.ToString & "/" & fin.Month.ToString & "/" & fin.Day.ToString & "'  "
        fin = CDate(Me.a.Text)
        strTeamQuery = "UPDATE puntosasociados  INNER JOIN compras ON puntosasociados.compra=compras.id SET  puntosasociados.porpagaranterior=puntosasociados.porpagar, corte=" & idperiodo.ToString & ", puntosasociados.status=1, puntosasociados.porpagar=0 WHERE puntosasociados.lado='" & lado & "' AND puntosasociados.asociado=" & asociado.ToString & "  AND ((compras.fecha<='" & fin.ToString("yyyy/MM/dd") & "') OR(compras.fecha='" & DateAdd(DateInterval.Day, 3, fin).ToString("yyyy/MM/dd") & "' AND compras.fechaorden='" & fin.ToString("yyyy/MM/dd") & "'))  "


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
        'actualiza los puntos del otro lado, haciendo la cuenta
        Dim ladocontrario As String
        If lado = "D" Then
            ladocontrario = "I"
        Else
            ladocontrario = "D"
        End If
        'strTeamQuery = "SELECT id, status, porpagar FROM puntosasociados WHERE asociado=" & asociado.ToString & " AND lado='" & ladocontrario & "' AND status<>1"
        '4/12/2013 strTeamQuery = "SELECT puntosasociados.id, puntosasociados.status, puntosasociados.porpagar FROM puntosasociados  INNER JOIN compras ON puntosasociados.compra=compras.id WHERE puntosasociados.asociado=" & asociado.ToString & " AND puntosasociados.lado='" & ladocontrario & "' AND puntosasociados.status<>1  AND  compras.fecha<='" & fin.Year.ToString & "/" & fin.Month.ToString & "/" & fin.Day.ToString & "'"
        strTeamQuery = "SELECT puntosasociados.id, puntosasociados.status, puntosasociados.porpagar FROM puntosasociados  INNER JOIN compras ON puntosasociados.compra=compras.id WHERE puntosasociados.asociado=" & asociado.ToString & " AND puntosasociados.lado='" & ladocontrario & "' AND puntosasociados.status<>1  AND  ((compras.fecha<='" & fin.ToString("yyyy/MM/dd") & "') OR(compras.fecha='" & DateAdd(DateInterval.Day, 3, fin).ToString("yyyy/MM/dd") & "' AND compras.fechaorden='" & fin.ToString("yyyy/MM/dd") & "'))"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        dtrTeam = cmdFetchTeam.ExecuteReader
        While dtrTeam.Read
            If puntos >= dtrTeam(2) Then
                actualizastatuspuntos(dtrTeam(0), 1)
                puntos -= dtrTeam(2)
                If puntos = 0 Then Exit While
            Else
                If dtrTeam(2) - puntos = 0 Then
                    actualizastatuspuntos(dtrTeam(0), 1)
                Else
                    actualizastatuspuntos(dtrTeam(0), 2, dtrTeam(2) - puntos)
                End If


                Exit While
            End If
        End While

        sqlConn.Close()
        sqlConn.Dispose()
       
    End Sub
    Sub actualizastatuspuntos(ByVal id As Integer, ByVal status As Integer, Optional ByVal puntos As Integer = 0)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "UPDATE puntosasociados SET porpagaranterior=porpagar, corte=" & idperiodo.ToString & ", status=" & status.ToString & ", porpagar=" & puntos.ToString & " WHERE id=" & id.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub

    Function porcentajedepago(ByVal asociado As Integer) As Decimal
        Dim porcentajeapagar As Decimal
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        strTeamQuery = "SELECT rangopago, ptsmes FROM asociados WHERE id=" & asociado.ToString
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Dim rango, puntos, paquete As Integer
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then rango = dtrTeam(0)
            If Not IsDBNull(dtrTeam(1)) Then puntos = dtrTeam(1)
        End While
        If puntos = 650 Then puntos = 700
        If puntos = 900 Then puntos = 1000
        sqlConn.Close()
        sqlConn.Dispose()
        Dim drv As DataRowView
        For Each drv In viewpuntos
            If puntos = drv("puntos") Then
                paquete = drv("id")
                Exit For
            End If
        Next


        view.RowFilter = "rango=" & rango.ToString & " AND paquete=" & paquete.ToString

        For Each drv In view
            porcentajeapagar = drv("porcentaje")
        Next
        Return porcentajeapagar
    End Function
    Sub llenavistadeporcentajes()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT rango, paquete, porcentaje FROM pagorangos"



        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        Dim objAdapter As New MySqlDataAdapter
        objAdapter.SelectCommand = cmdFetchTeam

        sqlConn.Open()
        Dim objDS As New DataSet
        objAdapter.Fill(objDS, "pagorangos")

        sqlConn.Close()
        sqlConn.Dispose()
        view = New DataView(objDS.Tables(0))
    End Sub
    Sub llenavistadepuntos()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, puntos FROM paquetes"



        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        Dim objAdapter As New MySqlDataAdapter
        objAdapter.SelectCommand = cmdFetchTeam

        sqlConn.Open()
        Dim objDS As New DataSet
        objAdapter.Fill(objDS, "paquetes")

        sqlConn.Close()
        sqlConn.Dispose()
        viewpuntos = New DataView(objDS.Tables(0))
    End Sub
    Sub llenavistadeasociados()
        'estado = "Inicia Llena vista de asociados"
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, patrocinador, orden, ptsmes, bono6, rango, rangopago FROM asociados WHERE status=1 OR '" & CDate(a.Text).ToString("yyyy/MM/dd") & "' BETWEEN inicioactivacion AND finactivacion "



        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        Dim objAdapter As New MySqlDataAdapter
        objAdapter.SelectCommand = cmdFetchTeam

        sqlConn.Open()
        Dim objDS As New DataSet
        objAdapter.Fill(objDS, "paquetes")

        sqlConn.Close()
        sqlConn.Dispose()
        viewasociados = New DataView(objDS.Tables(0))
        estado += "Termina  Llena vista de asociados"
    End Sub
    Function recuperaporcentajebono5() As Decimal
        Dim porcentajeapagar As Decimal = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        strTeamQuery = "SELECT porcentajebono5 FROM configuracion"
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()


        While dtrTeam.Read
            porcentajeapagar = dtrTeam(0) / 100
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        Return porcentajeapagar
    End Function
    Sub bono5()
        estado += "Inicia bono5. "

        Dim porcentajebono5 As Decimal = recuperaporcentajebono5()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT asociados.patrocinador, pagos.asociado, pagos.bono, pagos.monto, pagos.de, pagos.a " & _
                                        "FROM asociados INNER JOIN pagos ON asociados.ID = pagos.asociado " & _
                                        "WHERE  pagos.de>='" & CDate(Me.de.Text).ToString("yyyy/MM/dd") & "' AND pagos.a<='" & CDate(Me.a.Text).ToString("yyyy/MM/dd") & "' AND pagos.bono=4 " & _
                                        "ORDER BY asociados.patrocinador DESC; "

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim bono As Decimal = 0
        Dim id As Integer = 0
        While dtrTeam.Read
            If dtrTeam(0) > 0 Then


                If dtrTeam(0) <> id And id > 0 Then
                    insertabono5(id, bono)
                    bono = 0

                    id = dtrTeam(0)
                    bono += dtrTeam(3) * porcentajebono5
                Else
                    id = dtrTeam(0)
                    bono += dtrTeam(3) * porcentajebono5
                End If
            End If


        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If id > raiz Then insertabono5(id, bono)
        estado += "Termina bono5. "
    End Sub
    Sub insertabono5(ByVal asociado As Integer, ByVal cantidad As Decimal)
        If Not estaactivo(asociado) Then
            Exit Sub
        End If
        Dim factura As Integer = 0
        If funciones.facturo(asociado) Then
            factura = 1
        End If
        Dim mispuntos As Integer = mispuntosdelmes(asociado)
        Dim retencionisr, iva, retencioniva, total As Decimal
        total = cantidad
        If funciones.facturo(asociado) Then
            factura = 1
            retencionisr = 0
            iva = total * System.Configuration.ConfigurationManager.AppSettings("iva")
            retencioniva = total * 0.1
        Else
            retencionisr = funciones.calculaisr(total)

            iva = 0
            retencioniva = 0
        End If
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos, factura, retencionisr, iva, retencioniva) VALUES(" & asociado.ToString & ", '" & CDate(de.Text).ToString("yyyy/MM/dd") & "', '" & CDate(a.Text).ToString("yyyy/MM/dd") & "', 5, " & cantidad.ToString & ", " & idperiodo.ToString & ",0, " & mispuntos.ToString & ", " & factura.ToString & ", " & retencionisr.ToString & ", " & iva.ToString & ", " & retencioniva.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub borrabono6()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "DELETE FROM bono6 "
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub buscavaloresbono6()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT pago1bono6, pago2bono6, pagominimo678 " & _
                                        "FROM configuracion "

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            pago1bono6 = dtrTeam(0)
            pago2bono6 = dtrTeam(1)
            pagominimobono678 = dtrTeam(2)

        End While

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub buscavaloresbono7()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT pago1bono7, pago2bono7 " & _
                                        "FROM configuracion "

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            pago1bono7 = dtrTeam(0)
            pago2bono7 = dtrTeam(1)


        End While

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub buscavaloresbono8()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT pago1bono8, pago2bono8 " & _
                                        "FROM configuracion "

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            pago1bono8 = dtrTeam(0)
            pago2bono8 = dtrTeam(1)


        End While

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
  
    Sub bono612marzo2014()

        estado += "Inicia bono6. "
        buscavaloresbono6()
        'borra tabla temporal bono 6
        borrabono6()
        'creamos tabla para agregarle las compras a cada asociado
        Dim ds As New DataSet
        Dim table As DataTable = ds.Tables.Add("Data")
        table.Columns.Add("asociado", GetType(Integer))
        table.Columns.Add("cantidad", GetType(Integer))
        table.Columns.Add("paquete", GetType(Integer))
        table.Columns.Add("compra", GetType(Integer))
        table.Columns.Add("ptsmes", GetType(Integer))
        'select de compras
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "" & _
        "SELECT asociados.ID AS Asociado, asociados.Orden AS orden, asociados.patrocinador AS patrocinador, compras.ID AS Compra, compras.fecha AS fecha, compras.cantidad AS cantidad, compras.paquete, asociados.bono6 AS bono6, asociados.ptsmes " & _
        "FROM asociados INNER JOIN compras ON asociados.ID = compras.asociado " & _
        "WHERE compras.Fecha >= '" & CDate(de.Text).ToString("yyyy/MM/dd") & "' AND compras.Fecha <= '" & CDate(a.Text).ToString("yyyy/MM/dd") & "' AND compras.total>240 AND compras.excedente=0  AND compras.statuspago='PAGADO' " & _
        "ORDER BY compras.ID DESC;"

        'para incluir el lunes
        Dim fechafinalperiodo As Date = CDate(Me.a.Text)
        fechafinalperiodo = DateAdd(DateInterval.Day, 3, fechafinalperiodo)
        Dim fechafinalperiodoanterior As Date = DateAdd(DateInterval.Day, -1, CDate(Me.de.Text))
        Dim fechalunesanterior As Date = DateAdd(DateInterval.Day, 2, CDate(Me.de.Text))
        strTeamQuery = " SELECT asociados.ID AS Asociado, asociados.Orden AS orden, asociados.patrocinador AS patrocinador, compras.ID AS Compra, compras.fecha AS fecha, comprasdetalle.cantidad AS cantidad, comprasdetalle.paquete, asociados.bono6 AS bono6, asociados.ptsmes  " & _
                      "FROM asociados INNER JOIN compras ON asociados.ID = compras.asociado INNER JOIN comprasdetalle ON compras.id=comprasdetalle.compra " & _
                      "WHERE  comprasdetalle.paquete>0 AND compras.excedente=0  AND compras.statuspago='PAGADO' AND (compras.inscripcion=0 OR compras.puntos=350)" & _
                      "AND ((compras.fecha >= '" & CDate(de.Text).ToString("yyyy/MM/dd") & "' " & _
                      "AND compras.fecha <= '" & DateAdd(DateInterval.Day, -3, fechafinalperiodo).ToString("yyyy/MM/dd") & "') " & _
                      "OR(compras.fecha = '" & fechafinalperiodo.ToString("yyyy/MM/dd") & "') AND compras.fechaorden = '" & DateAdd(DateInterval.Day, -3, fechafinalperiodo).ToString("yyyy/MM/dd") & "')" & _
                      "AND compras.id NOT IN (" & _
                      "Select id FROM compras " & _
                      "WHERE fechaorden = '" & fechafinalperiodoanterior.Year.ToString & "/" & fechafinalperiodoanterior.Month.ToString & "/" & fechafinalperiodoanterior.Day.ToString & "' " & _
                      "AND fecha = '" & fechalunesanterior.Year.ToString & "/" & fechalunesanterior.Month.ToString & "/" & fechalunesanterior.Day.ToString & "' " & _
                      ") " & _
                      "ORDER BY compras.ID DESC;"


        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader




        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            table.Rows.Add(New Object() {dtrTeam(7), dtrTeam(5), dtrTeam(6), dtrTeam(3), dtrTeam(8)})

        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Dim viewbono6 As DataView = ds.Tables(0).DefaultView
        viewbono6.Sort = "asociado ASC"
        Dim drv As DataRowView
        Dim idasociado As Integer = 0
        Dim cantidad1 As Integer = 0
        Dim cantidad2 As Integer = 0
        Dim ptsmes As Integer = 0
        For Each drv In viewbono6

            If drv("asociado") <> idasociado And idasociado > 0 Then

                If idasociado >= raiz Then
                    insertabono6(idasociado, cantidad1, cantidad2, mispuntosdelmes(idasociado))
                End If


                idasociado = drv("asociado")
                cantidad1 = 0
                cantidad2 = 0
                ptsmes = 0
            Else
                idasociado = drv("asociado")

            End If
            If drv("paquete") > 1 Then
                cantidad2 += drv("cantidad")
            Else
                cantidad1 += drv("cantidad")
            End If
        Next
        If idasociado >= raiz Then insertabono6(idasociado, cantidad1, cantidad2, ptsmes)
        estado += "Termina bono6. "

    End Sub
    Sub bono6(Optional ByVal asociadobono As Integer = 0)

        estado += "Inicia bono6. "
        buscavaloresbono6()
        'borra tabla temporal bono 6
        borrabono6()
        'creamos tabla para agregarle las compras a cada asociado
        Dim ds As New DataSet
        Dim table As DataTable = ds.Tables.Add("Data")
        table.Columns.Add("asociado", GetType(Integer))
        table.Columns.Add("cantidad", GetType(Integer))
        table.Columns.Add("paquete", GetType(Integer))
        table.Columns.Add("compra", GetType(Integer))
        table.Columns.Add("ptsmes", GetType(Integer))
        'select de compras
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""

        'para incluir el lunes
        Dim fechafinalperiodo As Date = CDate(Me.a.Text)
        fechafinalperiodo = DateAdd(DateInterval.Day, 3, fechafinalperiodo)
        Dim fechafinalperiodoanterior As Date = DateAdd(DateInterval.Day, -1, CDate(Me.de.Text))
        Dim fechalunesanterior As Date = DateAdd(DateInterval.Day, 2, CDate(Me.de.Text))
        If asociadobono = 0 Then
            strTeamQuery = " SELECT asociados.ID AS Asociado, asociados.Orden AS orden, asociados.patrocinador AS patrocinador, compras.ID AS Compra, compras.fecha AS fecha, 1 AS cantidad, compras.puntos AS paquete, asociados.bono6 AS bono6, asociados.ptsmes  " & _
                    "FROM asociados INNER JOIN compras ON asociados.ID = compras.asociado  " & _
                    "WHERE  compras.puntos>=350 AND compras.excedente=0  AND compras.statuspago='PAGADO' AND (compras.inscripcion=0 OR compras.puntos=350)" & _
                    "AND ((compras.fecha >= '" & CDate(de.Text).ToString("yyyy/MM/dd") & "' " & _
                    "AND compras.fecha <= '" & DateAdd(DateInterval.Day, -3, fechafinalperiodo).ToString("yyyy/MM/dd") & "') " & _
                    "OR(compras.fecha = '" & fechafinalperiodo.ToString("yyyy/MM/dd") & "') AND compras.fechaorden = '" & DateAdd(DateInterval.Day, -3, fechafinalperiodo).ToString("yyyy/MM/dd") & "')" & _
                    "AND compras.id NOT IN (" & _
                    "Select id FROM compras " & _
                    "WHERE fechaorden = '" & fechafinalperiodoanterior.Year.ToString & "/" & fechafinalperiodoanterior.Month.ToString & "/" & fechafinalperiodoanterior.Day.ToString & "' " & _
                    "AND fecha = '" & fechalunesanterior.Year.ToString & "/" & fechalunesanterior.Month.ToString & "/" & fechalunesanterior.Day.ToString & "' " & _
                    ") " & _
                    "ORDER BY compras.ID DESC;"
        Else
            strTeamQuery = " SELECT asociados.ID AS Asociado, asociados.Orden AS orden, asociados.patrocinador AS patrocinador, compras.ID AS Compra, compras.fecha AS fecha, 1 AS cantidad, compras.puntos AS paquete, asociados.bono6 AS bono6, asociados.ptsmes  " & _
                    "FROM asociados INNER JOIN compras ON asociados.ID = compras.asociado  " & _
                    "WHERE asociados.bono6=" & asociadobono.ToString & " AND compras.puntos>=350 AND compras.excedente=0  AND compras.statuspago='PAGADO' AND (compras.inscripcion=0 OR compras.puntos=350)" & _
                    "AND ((compras.fecha >= '" & CDate(de.Text).ToString("yyyy/MM/dd") & "' " & _
                    "AND compras.fecha <= '" & DateAdd(DateInterval.Day, -3, fechafinalperiodo).ToString("yyyy/MM/dd") & "') " & _
                    "OR(compras.fecha = '" & fechafinalperiodo.ToString("yyyy/MM/dd") & "') AND compras.fechaorden = '" & DateAdd(DateInterval.Day, -3, fechafinalperiodo).ToString("yyyy/MM/dd") & "')" & _
                    "AND compras.id NOT IN (" & _
                    "Select id FROM compras " & _
                    "WHERE fechaorden = '" & fechafinalperiodoanterior.Year.ToString & "/" & fechafinalperiodoanterior.Month.ToString & "/" & fechafinalperiodoanterior.Day.ToString & "' " & _
                    "AND fecha = '" & fechalunesanterior.Year.ToString & "/" & fechalunesanterior.Month.ToString & "/" & fechalunesanterior.Day.ToString & "' " & _
                    ") " & _
                    "ORDER BY compras.ID DESC;"
        End If
      


        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader




        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            table.Rows.Add(New Object() {dtrTeam(7), dtrTeam(5), dtrTeam(6), dtrTeam(3), dtrTeam(8)})

        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Dim viewbono6 As DataView = ds.Tables(0).DefaultView
        viewbono6.Sort = "asociado ASC"
        Dim drv As DataRowView
        Dim idasociado As Integer = 0
        Dim cantidad1 As Integer = 0
        Dim cantidad2 As Integer = 0
        Dim ptsmes As Integer = 0
        For Each drv In viewbono6
            If Not IsDBNull(drv("asociado")) Then


                If drv("asociado") <> idasociado And idasociado > 0 Then

                    If idasociado >= raiz Then
                        insertabono6(idasociado, cantidad1, cantidad2, mispuntosdelmes(idasociado))
                    End If


                    idasociado = drv("asociado")
                    cantidad1 = 0
                    cantidad2 = 0
                    ptsmes = 0
                Else
                    idasociado = drv("asociado")

                End If
            End If
            If drv("paquete") > 350 Then
                cantidad2 += drv("cantidad")
            Else
                cantidad1 += drv("cantidad")
            End If
        Next
        If idasociado >= raiz Then insertabono6(idasociado, cantidad1, cantidad2, ptsmes)
        estado += "Termina bono6. "

    End Sub
    Function buscaantepasadomayoratres(ByVal asociado As Integer) As Integer
        Dim drv As DataRowView
        viewasociados.RowFilter = "id=" & asociado.ToString
        For Each drv In viewasociados
            Return drv("bono6")

        Next
    End Function

 
    Function misptsmes(ByVal asociado As Integer) As Integer
        Dim respuesta As Integer = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT ptsmes FROM asociados WHERE id=" & asociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then respuesta = dtrTeam(0)

        End While

        sqlConn.Close()
        sqlConn.Dispose()



        Return respuesta

    End Function
    Sub insertabono6(ByVal asociado As Integer, ByVal cantidad1 As Integer, ByVal cantidad2 As Integer, ByVal puntospersonales As Integer)
        Dim factura As Integer = 0
        If funciones.facturo(asociado) Then
            factura = 1
        End If

        Dim montobono As Decimal = 0
        puntospersonales = misptsmes(asociado)
        If puntospersonales > 350 Then
            montobono = ((cantidad1 * pago1bono6) + (cantidad2 * pago2bono6))
        Else
            montobono = (cantidad1 + cantidad2) * pagominimobono678
        End If
        'inserta en tabla pagos
        Dim mispuntos As Integer = mispuntosdelmes(asociado)

        Dim retencionisr, iva, retencioniva, total As Decimal
        total = montobono
        If funciones.facturo(asociado) Then
            factura = 1
            retencionisr = 0
            iva = total * System.Configuration.ConfigurationManager.AppSettings("iva")
            retencioniva = total * 0.1
        Else
            retencionisr = funciones.calculaisr(total)

            iva = 0
            retencioniva = 0
        End If


        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos, factura, retencionisr, iva, retencioniva) VALUES(" & asociado.ToString & ", '" & CDate(de.Text).ToString("yyyy/MM/dd") & "', '" & CDate(a.Text).ToString("yyyy/MM/dd") & "', 6, " & montobono.ToString & ", " & idperiodo.ToString & ",0, " & mispuntos.ToString & ", " & factura.ToString & ", " & retencionisr.ToString & ", " & iva.ToString & ", " & retencioniva.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        If estaactivo(asociado) Then



            sqlConn.Open()


            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            cmdFetchTeam.ExecuteNonQuery()


            sqlConn.Close()
            sqlConn.Dispose()
        End If
        'inserta en tabla temporal bono6
        strTeamQuery = "INSERT INTO bono6(asociado, compras1, compras2, patrocinador) VALUES(" & asociado.ToString & ", " & cantidad1.ToString & ", " & cantidad2.ToString & ", " & quienesmipatrocinador(asociado).ToString & " )"
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Function quienesmipatrocinador(ByVal asociado As Integer) As Integer
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT  patrocinador FROM asociados WHERE id=" & asociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            Return dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()
    End Function




    Sub borrabono7()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "DELETE FROM bono7 "
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()

    End Sub
    Sub bono7()
        buscavaloresbono7()

        estado += "Inicia bono7. "
        'borra tabla temporal bono 6
        borrabono7()
        'creamos tabla para agregarle las compras a cada asociado

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT patrocinador, SUM(compras1), SUM(compras2) FROM bono6 GROUP BY patrocinador"

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader




        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            If dtrTeam(0) >= raiz Then insertabono7(dtrTeam(0), dtrTeam(1), dtrTeam(2))



        End While

        sqlConn.Close()
        sqlConn.Dispose()

        estado += "Termina bono7. "

    End Sub
   
    Function mispuntosdelmes(ByVal asociado As Integer) As Integer
        Dim drv As DataRowView
        viewasociados.RowFilter = "id=" & asociado.ToString
        For Each drv In viewasociados
            Return drv("ptsmes")

        Next
    End Function
    Sub insertabono7(ByVal asociado As Integer, ByVal cantidad1 As Integer, ByVal cantidad2 As Integer)
        Dim factura As Integer = 0
        If funciones.facturo(asociado) Then
            factura = 1
        End If
        ' busca sus puntos de asociado
        Dim mispuntos As Integer = mispuntosdelmes(asociado)
        Dim monto As String
        If mispuntos > 350 Then
            monto = (cantidad1 * pago1bono7 + cantidad2 * pago2bono7).ToString
        Else
            monto = ((cantidad1 + cantidad2) * pagominimobono678).ToString

        End If
        'inserta en tabla pagos
        Dim mispuntosdelm As Integer = mispuntosdelmes(asociado)

        Dim retencionisr, iva, retencioniva, total As Decimal
        total = monto
        If funciones.facturo(asociado) Then
            factura = 1
            retencionisr = 0
            iva = total * System.Configuration.ConfigurationManager.AppSettings("iva")
            retencioniva = total * 0.1
        Else
            retencionisr = funciones.calculaisr(total)

            iva = 0
            retencioniva = 0
        End If


        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos, factura, retencionisr, iva, retencioniva) VALUES(" & asociado.ToString & ", '" & CDate(de.Text).ToString("yyyy/MM/dd") & "', '" & CDate(a.Text).ToString("yyyy/MM/dd") & "', 7, " & monto.ToString & ", " & idperiodo.ToString & ",0, " & mispuntosdelm.ToString & ", " & factura.ToString & ", " & retencionisr.ToString & ", " & iva.ToString & ", " & retencioniva.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        If estaactivo(asociado) Then


            sqlConn.Open()


            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            cmdFetchTeam.ExecuteNonQuery()


            sqlConn.Close()
            sqlConn.Dispose()
        End If
        'inserta en tabla temporal bono7
        strTeamQuery = "INSERT INTO bono7(asociado, compras1, compras2, patrocinador) VALUES(" & asociado.ToString & ", " & cantidad1.ToString & ", " & cantidad2.ToString & ", " & quienesmipatrocinador(asociado).ToString & " )"
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()


    End Sub
    Sub insertabono8(ByVal asociado As Integer, ByVal cantidad1 As Integer, ByVal cantidad2 As Integer)
        If Not estaactivo(asociado) Then
            Exit Sub
        End If
        Dim factura As Integer = 0
        If funciones.facturo(asociado) Then
            factura = 1
        End If
        ' busca sus puntos de asociado
        Dim mispuntos As Integer = mispuntosdelmes(asociado)
        Dim monto As String
        If mispuntos > 350 Then
            monto = (cantidad1 * pago1bono8 + cantidad2 * pago2bono8).ToString
        Else
            monto = ((cantidad1 + cantidad2) * pagominimobono678).ToString

        End If
        'inserta en tabla pagos
        Dim retencionisr, iva, retencioniva, total As Decimal
        total = monto
        If funciones.facturo(asociado) Then
            factura = 1
            retencionisr = 0
            iva = total * System.Configuration.ConfigurationManager.AppSettings("iva")
            retencioniva = total * 0.1
        Else
            retencionisr = funciones.calculaisr(total)

            iva = 0
            retencioniva = 0
        End If
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos, factura, retencionisr, iva, retencioniva) VALUES(" & asociado.ToString & ", '" & CDate(de.Text).ToString("yyyy/MM/dd") & "', '" & CDate(a.Text).ToString("yyyy/MM/dd") & "', 8, " & monto & ", " & idperiodo.ToString & ",0, " & mispuntos.ToString & ", " & factura.ToString & ", " & retencionisr.ToString & ", " & iva.ToString & ", " & retencioniva.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()

    End Sub
    Sub bono8()
        buscavaloresbono8()
        estado += "Inicia bono8. "
        'creamos tabla para agregarle las compras a cada asociado

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT patrocinador, SUM(compras1), SUM(compras2) FROM bono7 GROUP BY patrocinador"

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader




        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        'Dim idasociado As Integer = 0
        'Dim cantidad1, cantidad2 As Integer
        While dtrTeam.Read
            'If dtrTeam("patrocinador") <> idasociado Then
            'If idasociado >= raiz Then insertabono8(idasociado, cantidad1, cantidad2)

            'idasociado = dtrTeam("patrocinador")
            'cantidad1 = 0
            'cantidad2 = 0

            'End If

            'cantidad2 += dtrTeam("compras2")

            'cantidad1 += dtrTeam("compras1")
            If dtrTeam(0) >= raiz Then insertabono8(dtrTeam(0), dtrTeam(1), dtrTeam(2))
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        'If idasociado >= raiz Then insertabono8(idasociado, cantidad1, cantidad2)

        estado += "Termina bono8. "

    End Sub
  
    Function miladomayor(ByVal asociado As Integer) As String
        Dim respuesta As String = ""
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT SUM(porpagar), lado FROM puntosasociados WHERE asociado=" & asociado.ToString & " GROUP BY lado"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim izq, der As Integer
        izq = 0
        der = 0
        While dtrTeam.Read
            If dtrTeam(1) = "I" Then
                izq = dtrTeam(0)
            Else
                der = dtrTeam(0)
            End If
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If izq > der Then
            respuesta = "I"
        Else
            respuesta = "D"
        End If

        Return respuesta
    End Function
  
#End Region

#Region "Papelera"
    Sub temporaldesactivaalpasado()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        strTeamQuery = "UPDATE asociados SET status=0 "


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()

        strTeamQuery = "UPDATE asociados SET status=1 "
        Dim de As String = Me.de.Text
        Select Case de
            Case "2013/3/16"
                strTeamQuery += "WHERE id=11  OR id=202  OR id=133  OR id=206  OR id=205  OR id=1142  OR id=887  OR id=1150  OR id=219  OR id=777  OR id=1202  OR id=1432  OR id=1045  OR id=1257  OR id=217  OR id=120  OR id=826  OR id=1490  OR id=1329  OR id=1534  OR id=1051  OR id=1532  OR id=1152  OR id=1624  OR id=121  OR id=798  OR id=915  OR id=1438  OR id=827  OR id=1260  OR id=1342  OR id=1005  OR id=1714  OR id=203  OR id=1549  OR id=852  OR id=1737  OR id=825  OR id=1311  OR id=1562  OR id=1577  OR id=204  OR id=1444  OR id=29  OR id=1048  OR id=1628  OR id=1187  OR id=1457  OR id=1118  OR id=1503  OR id=227  OR id=1402  OR id=818  OR id=1316  OR id=1325  OR id=889  OR id=228  OR id=1239  OR id=1176  OR id=1741  OR id=1738  OR id=245  OR id=1658  OR id=1677  OR id=1678  OR id=987  OR id=1258  OR id=1401  OR id=15  OR id=1263  OR id=142  OR id=229  OR id=1548  OR id=543  OR id=514  OR id=1261 "

            Case "2013/3/23"
                strTeamQuery += "WHERE id=202  OR id=11  OR id=206  OR id=205  OR id=827  OR id=1700  OR id=777  OR id=887  OR id=1099  OR id=1548  OR id=1534  OR id=120  OR id=1202  OR id=230  OR id=1152  OR id=219  OR id=1342  OR id=1532  OR id=818  OR id=1142  OR id=29  OR id=1187  OR id=1748  OR id=1005  OR id=1045  OR id=826  OR id=217  OR id=1438  OR id=825  OR id=303  OR id=1315  OR id=1694  OR id=311  OR id=1314  OR id=1650  OR id=1359  OR id=1562  OR id=1577  OR id=816  OR id=204  OR id=1681  OR id=1490  OR id=1444  OR id=360  OR id=1144  OR id=1454  OR id=329  OR id=1051  OR id=203  OR id=514  OR id=1537  OR id=872  OR id=1588  OR id=509  OR id=1013  OR id=1635  OR id=1703  OR id=1720  OR id=1754  OR id=910  OR id=1210  OR id=17  OR id=856  OR id=1466  OR id=800  OR id=15  OR id=1482  OR id=1432  OR id=1680 "

            Case "2013/3/30"
                strTeamQuery += "WHERE id=11  OR id=202  OR id=206  OR id=827  OR id=1700  OR id=205  OR id=219  OR id=230  OR id=120  OR id=1748  OR id=133  OR id=1142  OR id=1152  OR id=887  OR id=1045  OR id=1187  OR id=1750  OR id=1749  OR id=1051  OR id=217  OR id=1005  OR id=1720  OR id=777  OR id=1150  OR id=1345  OR id=1202  OR id=798  OR id=29  OR id=204  OR id=831  OR id=1286  OR id=1454  OR id=1707  OR id=1762  OR id=1779  OR id=747  OR id=1532  OR id=1482  OR id=1099  OR id=1559  OR id=1067  OR id=1261  OR id=1263  OR id=1315  OR id=825  OR id=816  OR id=17  OR id=826  OR id=739  OR id=915  OR id=1257  OR id=1744  OR id=1184  OR id=1185  OR id=1751  OR id=1743  OR id=1226  OR id=1666  OR id=1698  OR id=1755  OR id=1764  OR id=1766  OR id=1633  OR id=1765  OR id=1767  OR id=509  OR id=1342  OR id=1402  OR id=15  OR id=1210  OR id=1647  OR id=203  OR id=1314  OR id=986  OR id=1401  OR id=1258 "
            Case "2013/4/06"
                strTeamQuery += "WHERE id=11  OR id=206  OR id=202  OR id=827  OR id=1700  OR id=205  OR id=887  OR id=120  OR id=219  OR id=142  OR id=1142  OR id=1152  OR id=777  OR id=230  OR id=1045  OR id=860  OR id=1748  OR id=1532  OR id=204  OR id=217  OR id=1178  OR id=1534  OR id=816  OR id=1548  OR id=826  OR id=203  OR id=798  OR id=1005  OR id=1482  OR id=1187  OR id=1720  OR id=1750  OR id=1749  OR id=1537  OR id=1202  OR id=1765  OR id=1767  OR id=1764  OR id=1766  OR id=739  OR id=1490  OR id=1698  OR id=1752  OR id=791  OR id=1762  OR id=1694  OR id=1804  OR id=1315  OR id=1067  OR id=1650  OR id=220  OR id=1754  OR id=133  OR id=1786  OR id=1744  OR id=1777  OR id=1345  OR id=1260  OR id=245  OR id=1442  OR id=1438  OR id=227  OR id=915  OR id=1737  OR id=1185  OR id=1141  OR id=1226  OR id=1779  OR id=15  OR id=1257  OR id=1469  OR id=1470  OR id=1739  OR id=1603  OR id=1819  OR id=1025  OR id=1812  OR id=1778  OR id=1645  OR id=986  OR id=1099  OR id=1503  OR id=509  OR id=121  OR id=17  OR id=1342  OR id=1466  OR id=1468  OR id=1714  OR id=1467 "
            Case "2013/4/13"
                strTeamQuery += "WHERE id=11  OR id=206  OR id=202  OR id=827  OR id=205  OR id=1700  OR id=120  OR id=1152  OR id=1142  OR id=1548  OR id=887  OR id=1534  OR id=142  OR id=777  OR id=219  OR id=1532  OR id=1744  OR id=29  OR id=1202  OR id=1315  OR id=1187  OR id=204  OR id=1650  OR id=915  OR id=818  OR id=217  OR id=1803  OR id=1005  OR id=798  OR id=860  OR id=872  OR id=1099  OR id=825  OR id=1639  OR id=1754  OR id=1045  OR id=1345  OR id=133  OR id=852  OR id=1785  OR id=1800  OR id=203  OR id=245  OR id=1561  OR id=1457  OR id=1675  OR id=1438  OR id=747  OR id=1342  OR id=227  OR id=1588  OR id=1051  OR id=1703  OR id=1815  OR id=134  OR id=1777  OR id=303  OR id=826  OR id=1184  OR id=1210  OR id=1314  OR id=1185  OR id=1467  OR id=121  OR id=1466  OR id=1441  OR id=514  OR id=816  OR id=1819  OR id=1490  OR id=889  OR id=1829  OR id=1141  OR id=1755  OR id=1739  OR id=1594  OR id=1629  OR id=1830  OR id=1795  OR id=1827  OR id=1847  OR id=1674  OR id=1747  OR id=1736  OR id=509  OR id=831  OR id=1468  OR id=628  OR id=1482 "

            Case "2013/4/20"
                strTeamQuery += "WHERE id=827  OR id=206  OR id=1700  OR id=11  OR id=202  OR id=205  OR id=120  OR id=777  OR id=219  OR id=1785  OR id=887  OR id=1045  OR id=1756  OR id=121  OR id=1051  OR id=204  OR id=818  OR id=1142  OR id=1744  OR id=1005  OR id=1804  OR id=1345  OR id=1698  OR id=1639  OR id=1786  OR id=1532  OR id=1874  OR id=816  OR id=826  OR id=1624  OR id=217  OR id=1565  OR id=1534  OR id=1152  OR id=1482  OR id=798  OR id=1099  OR id=1314  OR id=1645  OR id=1861  OR id=373  OR id=1772  OR id=1840  OR id=1860  OR id=1809  OR id=227  OR id=1754  OR id=1562  OR id=1577  OR id=1628  OR id=825  OR id=872  OR id=133  OR id=1573  OR id=1827  OR id=1755  OR id=1747  OR id=1720  OR id=1144  OR id=203  OR id=1751  OR id=1588  OR id=245  OR id=228  OR id=230  OR id=1067  OR id=1185  OR id=1187  OR id=1178  OR id=1444  OR id=1141  OR id=1876  OR id=915  OR id=1438  OR id=965  OR id=1342  OR id=509  OR id=1548  OR id=15  OR id=986  OR id=1454 "

            Case "2013/4/27"
                strTeamQuery += "WHERE id=11  OR id=202  OR id=206  OR id=133  OR id=827  OR id=205  OR id=1700  OR id=120  OR id=1286  OR id=219  OR id=1548  OR id=29  OR id=1544  OR id=1532  OR id=1534  OR id=777  OR id=1152  OR id=1666  OR id=1263  OR id=1827  OR id=1745  OR id=217  OR id=1142  OR id=1639  OR id=245  OR id=1770  OR id=1187  OR id=1261  OR id=887  OR id=1754  OR id=1045  OR id=1005  OR id=798  OR id=1099  OR id=1744  OR id=1051  OR id=1482  OR id=739  OR id=311  OR id=1873  OR id=1846  OR id=1562  OR id=1577  OR id=1785  OR id=915  OR id=1345  OR id=818  OR id=1863  OR id=1260  OR id=303  OR id=1537  OR id=1739  OR id=509  OR id=1141  OR id=825  OR id=1316  OR id=1466  OR id=230  OR id=816  OR id=872  OR id=1876  OR id=17  OR id=204  OR id=1683  OR id=1470  OR id=1832  OR id=1207  OR id=1893  OR id=1559  OR id=826  OR id=607  OR id=1444  OR id=1438  OR id=329  OR id=15  OR id=1342  OR id=203  OR id=1795  OR id=1860  OR id=1874  OR id=1442 "

            Case "2013/5/04"
                strTeamQuery += "WHERE id=11  OR id=206  OR id=827  OR id=205  OR id=1700  OR id=202  OR id=1894  OR id=120  OR id=219  OR id=777  OR id=1639  OR id=1152  OR id=1534  OR id=887  OR id=1099  OR id=1548  OR id=133  OR id=1144  OR id=1045  OR id=29  OR id=1532  OR id=1482  OR id=1142  OR id=217  OR id=1650  OR id=1698  OR id=1051  OR id=1342  OR id=1628  OR id=798  OR id=1345  OR id=860  OR id=1187  OR id=826  OR id=1490  OR id=1261  OR id=1005  OR id=739  OR id=1872  OR id=142  OR id=1928  OR id=1909  OR id=203  OR id=1744  OR id=303  OR id=816  OR id=245  OR id=1013  OR id=1739  OR id=1675  OR id=1762  OR id=204  OR id=1257  OR id=1389  OR id=1438  OR id=818  OR id=1537  OR id=278  OR id=1815  OR id=1927  OR id=1226  OR id=825  OR id=1457  OR id=1316  OR id=1588  OR id=514  OR id=1694  OR id=1504  OR id=1838  OR id=1920  OR id=1388  OR id=1922  OR id=1846  OR id=121  OR id=1503  OR id=1402  OR id=831  OR id=17  OR id=380  OR id=941  OR id=1442  OR id=1258  OR id=15  OR id=1401 "

            Case "2013/5/11"
                strTeamQuery += "WHERE id=11  OR id=206  OR id=205  OR id=827  OR id=133  OR id=202  OR id=1700  OR id=1263  OR id=120  OR id=219  OR id=1152  OR id=29  OR id=1142  OR id=1666  OR id=1187  OR id=818  OR id=217  OR id=777  OR id=1548  OR id=887  OR id=1860  OR id=1532  OR id=373  OR id=860  OR id=1045  OR id=1559  OR id=1261  OR id=1752  OR id=245  OR id=1005  OR id=278  OR id=1534  OR id=1466  OR id=204  OR id=303  OR id=872  OR id=1850  OR id=1913  OR id=1544  OR id=121  OR id=1359  OR id=1561  OR id=1815  OR id=1846  OR id=1286  OR id=1720  OR id=1099  OR id=1345  OR id=1744  OR id=1808  OR id=826  OR id=1457  OR id=1184  OR id=739  OR id=1467  OR id=1927  OR id=1150  OR id=1739  OR id=142  OR id=1639  OR id=1804  OR id=1314  OR id=1754  OR id=1185  OR id=941  OR id=1588  OR id=1647  OR id=1468  OR id=1257  OR id=1051  OR id=1482  OR id=1895  OR id=1768  OR id=1855  OR id=1921  OR id=1438  OR id=816  OR id=15  OR id=1402  OR id=1342  OR id=825  OR id=1013  OR id=227  OR id=1258  OR id=1401  OR id=17  OR id=1747 "

            Case "2013/5/18"
                strTeamQuery += "WHERE id=206  OR id=11  OR id=205  OR id=827  OR id=1700  OR id=202  OR id=219  OR id=777  OR id=120  OR id=1099  OR id=1144  OR id=1142  OR id=887  OR id=133  OR id=1894  OR id=29  OR id=1152  OR id=1997  OR id=798  OR id=1345  OR id=217  OR id=1639  OR id=1150  OR id=1187  OR id=1698  OR id=1772  OR id=1950  OR id=1454  OR id=245  OR id=1045  OR id=1534  OR id=121  OR id=1860  OR id=1532  OR id=1316  OR id=818  OR id=1662  OR id=1951  OR id=311  OR id=1559  OR id=303  OR id=826  OR id=1962  OR id=278  OR id=1263  OR id=872  OR id=1438  OR id=1261  OR id=1624  OR id=1342  OR id=1005  OR id=1628  OR id=1778  OR id=142  OR id=377  OR id=1051  OR id=1314  OR id=1548  OR id=1562  OR id=1577  OR id=220  OR id=1490  OR id=915  OR id=1184  OR id=1537  OR id=1752  OR id=1868  OR id=1482  OR id=825  OR id=941  OR id=1955  OR id=2003  OR id=1616  OR id=1982  OR id=1984  OR id=2009  OR id=2016  OR id=1963  OR id=1980  OR id=1995  OR id=1945  OR id=1960  OR id=1992  OR id=2008  OR id=1927  OR id=1257  OR id=1341  OR id=1432  OR id=607  OR id=1457  OR id=1013  OR id=1256  OR id=17  OR id=15  OR id=1504 "

            Case "2013/5/25"
                strTeamQuery += "WHERE id=205  OR id=206  OR id=11  OR id=827  OR id=202  OR id=1700  OR id=777  OR id=217  OR id=887  OR id=29  OR id=133  OR id=1099  OR id=1152  OR id=1511  OR id=219  OR id=120  OR id=1142  OR id=142  OR id=860  OR id=1402  OR id=1144  OR id=818  OR id=1997  OR id=1639  OR id=1005  OR id=1534  OR id=816  OR id=1995  OR id=1257  OR id=1345  OR id=1532  OR id=1698  OR id=2058  OR id=2009  OR id=1951  OR id=1744  OR id=1962  OR id=303  OR id=1977  OR id=2022  OR id=1753  OR id=1187  OR id=1548  OR id=1546  OR id=1150  OR id=1629  OR id=872  OR id=1438  OR id=2010  OR id=1894  OR id=1048  OR id=941  OR id=1772  OR id=2014  OR id=1858  OR id=2040  OR id=2041  OR id=2029  OR id=798  OR id=1401  OR id=826  OR id=1849  OR id=1441  OR id=1817  OR id=1785  OR id=1342  OR id=1950  OR id=1808  OR id=1616  OR id=1739  OR id=1045  OR id=1490  OR id=278  OR id=1051  OR id=1989  OR id=1316  OR id=1258  OR id=1562  OR id=1577  OR id=1809  OR id=1752  OR id=2008  OR id=1703  OR id=1178  OR id=1754  OR id=1645  OR id=1946  OR id=17  OR id=1804  OR id=1850  OR id=2034  OR id=1581  OR id=1613  OR id=2056  OR id=2030  OR id=2046  OR id=204  OR id=915  OR id=1457  OR id=1503  OR id=1286  OR id=2003  OR id=1261  OR id=1747  OR id=1263  OR id=311  OR id=329  OR id=1256  OR id=230  OR id=1432  OR id=825 "

            Case "2013/6/01"
                strTeamQuery += "WHERE id=206  OR id=11  OR id=133  OR id=827  OR id=205  OR id=202  OR id=1700  OR id=1544  OR id=120  OR id=777  OR id=1263  OR id=1286  OR id=29  OR id=219  OR id=245  OR id=1534  OR id=1152  OR id=1142  OR id=1992  OR id=1342  OR id=303  OR id=1261  OR id=1951  OR id=1639  OR id=818  OR id=798  OR id=887  OR id=1532  OR id=2071  OR id=1894  OR id=16  OR id=1045  OR id=217  OR id=739  OR id=1666  OR id=1548  OR id=872  OR id=1744  OR id=1962  OR id=1504  OR id=860  OR id=1838  OR id=1949  OR id=826  OR id=1051  OR id=142  OR id=1955  OR id=1977  OR id=1899  OR id=1433  OR id=852  OR id=2066  OR id=1407  OR id=1950  OR id=2040  OR id=1490  OR id=121  OR id=509  OR id=1438  OR id=915  OR id=1210  OR id=1345  OR id=1325  OR id=1969  OR id=1921  OR id=1898  OR id=1482  OR id=1628  OR id=17  OR id=514  OR id=1187  OR id=1832  OR id=1754  OR id=1099  OR id=1257  OR id=1650  OR id=1755  OR id=1989  OR id=1537  OR id=1184  OR id=1226  OR id=1913  OR id=1454  OR id=15  OR id=332  OR id=1785  OR id=1150  OR id=1005  OR id=2034  OR id=941  OR id=1935  OR id=2103  OR id=1931  OR id=1976  OR id=2031  OR id=329  OR id=2067  OR id=2090  OR id=2092  OR id=1503  OR id=825  OR id=816  OR id=831  OR id=1770  OR id=1594  OR id=595  OR id=1647  OR id=1815  OR id=1850 "

            Case "2013/6/08"
                strTeamQuery += "WHERE id=206  OR id=11  OR id=205  OR id=777  OR id=827  OR id=202  OR id=29  OR id=1700  OR id=1639  OR id=887  OR id=120  OR id=1921  OR id=219  OR id=1263  OR id=133  OR id=1969  OR id=1894  OR id=217  OR id=1756  OR id=1316  OR id=1152  OR id=818  OR id=278  OR id=1534  OR id=1099  OR id=1532  OR id=1142  OR id=2086  OR id=1962  OR id=816  OR id=1678  OR id=303  OR id=1490  OR id=2037  OR id=860  OR id=739  OR id=1548  OR id=1345  OR id=798  OR id=1261  OR id=1951  OR id=1187  OR id=2056  OR id=826  OR id=1013  OR id=543  OR id=1045  OR id=1256  OR id=915  OR id=1772  OR id=229  OR id=2040  OR id=2088  OR id=142  OR id=1503  OR id=1144  OR id=1511  OR id=1051  OR id=1650  OR id=1482  OR id=1005  OR id=1438  OR id=1945  OR id=1845  OR id=1210  OR id=1953  OR id=1150  OR id=1457  OR id=204  OR id=1432  OR id=1881  OR id=1647  OR id=1389  OR id=1675  OR id=1694  OR id=1950  OR id=1645  OR id=2077  OR id=2111  OR id=2014  OR id=1757  OR id=1859  OR id=2124  OR id=825  OR id=1243  OR id=1342  OR id=1314  OR id=17  OR id=1315  OR id=227  OR id=1977  OR id=1442  OR id=1613  OR id=15  OR id=1441 "

            Case "2013/6/15"
                strTeamQuery += "WHERE id=11  OR id=206  OR id=827  OR id=205  OR id=133  OR id=202  OR id=1700  OR id=120  OR id=887  OR id=777  OR id=219  OR id=1629  OR id=29  OR id=1142  OR id=1534  OR id=1152  OR id=1992  OR id=1532  OR id=217  OR id=798  OR id=1804  OR id=2059  OR id=1894  OR id=1325  OR id=1099  OR id=1548  OR id=1951  OR id=1838  OR id=1263  OR id=303  OR id=1860  OR id=1639  OR id=915  OR id=816  OR id=1756  OR id=1616  OR id=739  OR id=1438  OR id=1005  OR id=1342  OR id=1645  OR id=799  OR id=1111  OR id=1858  OR id=1989  OR id=1013  OR id=872  OR id=1991  OR id=2141  OR id=2150  OR id=1045  OR id=121  OR id=1744  OR id=1257  OR id=1945  OR id=1144  OR id=628  OR id=1950  OR id=1187  OR id=1261  OR id=826  OR id=514  OR id=818  OR id=1669  OR id=2103  OR id=1832  OR id=940  OR id=245  OR id=1260  OR id=825  OR id=1537  OR id=278  OR id=311  OR id=1490  OR id=204  OR id=2155  OR id=1666  OR id=1594  OR id=1544  OR id=17  OR id=1286  OR id=1466  OR id=227  OR id=1624  OR id=1467  OR id=1969  OR id=1316  OR id=1561  OR id=1562  OR id=1577  OR id=1628  OR id=1960  OR id=1913  OR id=2149  OR id=15  OR id=1504  OR id=1573  OR id=1739  OR id=2168  OR id=2142  OR id=1845  OR id=2070  OR id=919  OR id=1150  OR id=607  OR id=1457  OR id=1454  OR id=1256  OR id=228  OR id=1468  OR id=1785 "

            Case "2013/6/22"
                strTeamQuery += "WHERE id=206  OR id=205  OR id=11  OR id=827  OR id=202  OR id=1700  OR id=777  OR id=120  OR id=133  OR id=1744  OR id=887  OR id=1639  OR id=1534  OR id=217  OR id=1546  OR id=1950  OR id=1532  OR id=1142  OR id=1099  OR id=798  OR id=1144  OR id=1263  OR id=1482  OR id=1894  OR id=1005  OR id=1548  OR id=219  OR id=1544  OR id=2009  OR id=1698  OR id=245  OR id=29  OR id=2151  OR id=1785  OR id=1257  OR id=1846  OR id=1997  OR id=1832  OR id=2150  OR id=1454  OR id=1899  OR id=1345  OR id=818  OR id=2159  OR id=1992  OR id=1969  OR id=1511  OR id=1261  OR id=2046  OR id=1694  OR id=2088  OR id=1953  OR id=2158  OR id=1150  OR id=303  OR id=826  OR id=1051  OR id=1402  OR id=825  OR id=1960  OR id=1804  OR id=121  OR id=311  OR id=915  OR id=278  OR id=1152  OR id=2178  OR id=1647  OR id=1629  OR id=2008  OR id=1666  OR id=1662  OR id=1562  OR id=1577  OR id=1184  OR id=1703  OR id=2155  OR id=2024  OR id=1624  OR id=509  OR id=1457  OR id=816  OR id=1985  OR id=2187  OR id=2175  OR id=2143  OR id=1901  OR id=2190  OR id=940  OR id=1910  OR id=2162  OR id=1401  OR id=1250  OR id=1045  OR id=1438  OR id=831  OR id=2003  OR id=1432  OR id=1258  OR id=1342  OR id=1178  OR id=1962  OR id=1770  OR id=747  OR id=2149  OR id=1838  OR id=1013 "

            Case "2013/6/29"
                strTeamQuery += "WHERE id=11  OR id=827  OR id=206  OR id=205  OR id=133  OR id=120  OR id=887  OR id=202  OR id=219  OR id=872  OR id=1700  OR id=1263  OR id=29  OR id=1142  OR id=777  OR id=217  OR id=1261  OR id=798  OR id=1316  OR id=303  OR id=1544  OR id=1005  OR id=2150  OR id=1720  OR id=825  OR id=1744  OR id=1565  OR id=1639  OR id=860  OR id=1045  OR id=818  OR id=1152  OR id=915  OR id=1666  OR id=1099  OR id=1534  OR id=1490  OR id=1389  OR id=1832  OR id=852  OR id=1894  OR id=1150  OR id=1621  OR id=1949  OR id=1772  OR id=1144  OR id=1698  OR id=2219  OR id=1532  OR id=1260  OR id=1345  OR id=816  OR id=17  OR id=1051  OR id=121  OR id=1548  OR id=1184  OR id=1511  OR id=1342  OR id=1950  OR id=1963  OR id=1745  OR id=2142  OR id=2070  OR id=941  OR id=2158  OR id=2130  OR id=1991  OR id=826  OR id=245  OR id=739  OR id=1785  OR id=1257  OR id=1616  OR id=1187  OR id=1662  OR id=1977  OR id=1995  OR id=1945  OR id=1989  OR id=1838  OR id=1703  OR id=1537  OR id=1969  OR id=2151  OR id=1226  OR id=1454  OR id=15  OR id=1992  OR id=2067  OR id=1901  OR id=2167  OR id=2185  OR id=1667  OR id=1935  OR id=2085  OR id=2121  OR id=2238  OR id=1768  OR id=2197  OR id=1841  OR id=2159  OR id=2166  OR id=2223  OR id=831  OR id=1256  OR id=1325  OR id=1815  OR id=2149  OR id=1921  OR id=1504  OR id=1573  OR id=1594  OR id=747 "

            Case "2013/7/06"
                strTeamQuery += "WHERE id=206  OR id=11  OR id=205  OR id=777  OR id=133  OR id=202  OR id=827  OR id=1894  OR id=120  OR id=887  OR id=1639  OR id=1950  OR id=1902  OR id=2130  OR id=1534  OR id=1263  OR id=1532  OR id=1700  OR id=1316  OR id=29  OR id=1992  OR id=798  OR id=1261  OR id=1142  OR id=825  OR id=915  OR id=1548  OR id=1935  OR id=219  OR id=1099  OR id=2150  OR id=1152  OR id=217  OR id=1144  OR id=2279  OR id=826  OR id=2213  OR id=1005  OR id=303  OR id=329  OR id=1945  OR id=2178  OR id=1650  OR id=2149  OR id=2142  OR id=1858  OR id=2072  OR id=1698  OR id=142  OR id=2255  OR id=2245  OR id=1744  OR id=1757  OR id=2151  OR id=816  OR id=1482  OR id=1013  OR id=245  OR id=1838  OR id=1325  OR id=2159  OR id=1150  OR id=2076  OR id=1647  OR id=1841  OR id=1739  OR id=278  OR id=739  OR id=1720  OR id=860  OR id=1629  OR id=1051  OR id=1832  OR id=2250  OR id=1995  OR id=2061  OR id=1187  OR id=1511  OR id=514  OR id=121  OR id=1845  OR id=1675  OR id=2158  OR id=1552  OR id=1561  OR id=1804  OR id=2103  OR id=1969  OR id=2037  OR id=2122  OR id=2256  OR id=2269  OR id=2181  OR id=2208  OR id=2265  OR id=2229  OR id=2291  OR id=2209  OR id=1666  OR id=17  OR id=628  OR id=1045  OR id=831  OR id=1544  OR id=1953  OR id=229  OR id=1442  OR id=1594  OR id=543  OR id=747  OR id=15  OR id=1785  OR id=1989  OR id=1694 "

            Case "2013/7/13"
                strTeamQuery += "WHERE id=11  OR id=206  OR id=133  OR id=205  OR id=120  OR id=827  OR id=777  OR id=798  OR id=1263  OR id=887  OR id=202  OR id=1142  OR id=915  OR id=1894  OR id=29  OR id=1639  OR id=2328  OR id=1261  OR id=303  OR id=219  OR id=1534  OR id=1099  OR id=1902  OR id=121  OR id=1700  OR id=816  OR id=2248  OR id=2291  OR id=1662  OR id=1548  OR id=1144  OR id=1950  OR id=1150  OR id=1735  OR id=739  OR id=217  OR id=826  OR id=1666  OR id=2142  OR id=1744  OR id=1316  OR id=1532  OR id=919  OR id=2004  OR id=1565  OR id=1845  OR id=1969  OR id=2150  OR id=1832  OR id=825  OR id=245  OR id=373  OR id=2292  OR id=2317  OR id=1698  OR id=2307  OR id=2245  OR id=2267  OR id=1992  OR id=628  OR id=1951  OR id=1325  OR id=1629  OR id=2103  OR id=1841  OR id=1770  OR id=2229  OR id=1342  OR id=2151  OR id=1913  OR id=2280  OR id=2197  OR id=2166  OR id=2256  OR id=1544  OR id=1537  OR id=941  OR id=311  OR id=1838  OR id=278  OR id=1005  OR id=818  OR id=2171  OR id=2266  OR id=2208  OR id=16  OR id=905  OR id=1466  OR id=1785  OR id=1467  OR id=2219  OR id=204  OR id=872  OR id=2179  OR id=514  OR id=1504  OR id=1594  OR id=2149  OR id=15  OR id=1935  OR id=2237  OR id=2270  OR id=2337  OR id=2242  OR id=2260  OR id=1677  OR id=2311  OR id=1945  OR id=2268  OR id=2000  OR id=2309  OR id=2257  OR id=1482  OR id=1457  OR id=1345  OR id=607  OR id=1045  OR id=1490  OR id=229  OR id=17  OR id=747  OR id=1468  OR id=1757  OR id=2250  OR id=1960 "

            Case "2013/7/20"
                strTeamQuery += "WHERE id=11  OR id=206  OR id=133  OR id=205  OR id=202  OR id=827  OR id=120  OR id=887  OR id=777  OR id=1902  OR id=798  OR id=1263  OR id=1099  OR id=2328  OR id=1700  OR id=1144  OR id=1662  OR id=1534  OR id=1997  OR id=1142  OR id=1639  OR id=915  OR id=1261  OR id=219  OR id=217  OR id=2059  OR id=2329  OR id=816  OR id=1532  OR id=1548  OR id=1894  OR id=121  OR id=2317  OR id=1838  OR id=1921  OR id=825  OR id=1316  OR id=1546  OR id=1629  OR id=1945  OR id=1950  OR id=1005  OR id=1257  OR id=29  OR id=1744  OR id=2010  OR id=1832  OR id=2122  OR id=1482  OR id=1698  OR id=1745  OR id=2295  OR id=2296  OR id=852  OR id=2076  OR id=2260  OR id=2259  OR id=2330  OR id=2023  OR id=2207  OR id=1152  OR id=1454  OR id=1845  OR id=1960  OR id=1150  OR id=1594  OR id=1111  OR id=1735  OR id=1753  OR id=1040  OR id=1388  OR id=2167  OR id=2190  OR id=1951  OR id=245  OR id=1051  OR id=278  OR id=2009  OR id=1989  OR id=860  OR id=1402  OR id=2103  OR id=2022  OR id=2270  OR id=1669  OR id=1490  OR id=1660  OR id=1562  OR id=1577  OR id=1841  OR id=2150  OR id=303  OR id=1666  OR id=2157  OR id=2116  OR id=2252  OR id=2257  OR id=1846  OR id=2045  OR id=2178  OR id=2246  OR id=2240  OR id=2185  OR id=2333  OR id=2358  OR id=2367  OR id=1935  OR id=1468  OR id=2305  OR id=2370  OR id=2316  OR id=2236  OR id=818  OR id=826  OR id=1457  OR id=1286  OR id=1401  OR id=15  OR id=1258  OR id=1342  OR id=514  OR id=1785  OR id=1467  OR id=17  OR id=1432  OR id=1544  OR id=2008  OR id=2149  OR id=1466 "

            Case "2013/7/27"
                strTeamQuery += "WHERE id=11  OR id=133  OR id=206  OR id=205  OR id=1263  OR id=777  OR id=827  OR id=1142  OR id=120  OR id=202  OR id=29  OR id=219  OR id=1841  OR id=887  OR id=1894  OR id=1639  OR id=1261  OR id=825  OR id=217  OR id=2328  OR id=1209  OR id=1700  OR id=816  OR id=2009  OR id=1286  OR id=1490  OR id=1902  OR id=2266  OR id=1099  OR id=1051  OR id=798  OR id=2267  OR id=1316  OR id=2355  OR id=2329  OR id=1150  OR id=1950  OR id=2150  OR id=1144  OR id=1005  OR id=1935  OR id=1482  OR id=1503  OR id=1013  OR id=2008  OR id=1045  OR id=941  OR id=2304  OR id=1991  OR id=1989  OR id=1698  OR id=2345  OR id=1662  OR id=2114  OR id=1858  OR id=2176  OR id=554  OR id=2088  OR id=2330  OR id=860  OR id=303  OR id=1744  OR id=1955  OR id=2225  OR id=1544  OR id=1945  OR id=1953  OR id=2256  OR id=2149  OR id=1534  OR id=17  OR id=1532  OR id=2142  OR id=2024  OR id=1667  OR id=2367  OR id=2372  OR id=1969  OR id=142  OR id=1951  OR id=1846  OR id=2229  OR id=278  OR id=1977  OR id=1997  OR id=2216  OR id=2076  OR id=1152  OR id=1647  OR id=2123  OR id=1785  OR id=15  OR id=2003  OR id=1342  OR id=245  OR id=1345  OR id=826  OR id=1565  OR id=2208  OR id=1703  OR id=1770  OR id=2000  OR id=2257  OR id=1845  OR id=2130  OR id=1457  OR id=1739  OR id=2402  OR id=2404  OR id=2370  OR id=2326  OR id=2376  OR id=1542  OR id=311  OR id=329  OR id=514  OR id=1454  OR id=2250  OR id=1666  OR id=915 "

            Case "2013/8/03"
                strTeamQuery += "WHERE id=206  OR id=11  OR id=133  OR id=827  OR id=205  OR id=2452  OR id=1950  OR id=887  OR id=1894  OR id=777  OR id=1263  OR id=120  OR id=2328  OR id=739  OR id=1316  OR id=798  OR id=29  OR id=1902  OR id=1142  OR id=1935  OR id=2150  OR id=1951  OR id=1662  OR id=219  OR id=1261  OR id=1992  OR id=1700  OR id=1993  OR id=15  OR id=2076  OR id=1639  OR id=303  OR id=826  OR id=1099  OR id=1490  OR id=1286  OR id=202  OR id=2329  OR id=2142  OR id=2072  OR id=860  OR id=2389  OR id=2130  OR id=2485  OR id=1144  OR id=217  OR id=1757  OR id=1901  OR id=2149  OR id=1345  OR id=2453  OR id=2489  OR id=2246  OR id=1184  OR id=245  OR id=329  OR id=2318  OR id=2391  OR id=2086  OR id=2203  OR id=1359  OR id=2185  OR id=2208  OR id=2260  OR id=2324  OR id=2417  OR id=1045  OR id=941  OR id=1013  OR id=1152  OR id=852  OR id=1945  OR id=1438  OR id=915  OR id=1544  OR id=1542  OR id=2124  OR id=2316  OR id=2204  OR id=2355  OR id=1482  OR id=1989  OR id=1389  OR id=2330  OR id=1969  OR id=825  OR id=2460  OR id=2467  OR id=2478  OR id=2497  OR id=2491  OR id=2486  OR id=1841  OR id=1342  OR id=1744  OR id=2370  OR id=1466  OR id=2016  OR id=2259  OR id=2268  OR id=1845  OR id=1629  OR id=1209  OR id=1005  OR id=1770  OR id=2302  OR id=2443  OR id=2393  OR id=2362  OR id=2446  OR id=2448  OR id=2263  OR id=2415  OR id=2265  OR id=509  OR id=2492  OR id=2458  OR id=2461  OR id=2500  OR id=2498  OR id=2457  OR id=2468  OR id=2475  OR id=2477  OR id=2454  OR id=2455  OR id=2470  OR id=2471  OR id=2472  OR id=2067  OR id=121  OR id=1503  OR id=1150  OR id=1666  OR id=2402  OR id=747  OR id=229  OR id=1178  OR id=2070  OR id=17  OR id=2256 "

            Case "2013/8/10"
                strTeamQuery += "WHERE id=11  OR id=206  OR id=205  OR id=887  OR id=133  OR id=827  OR id=1142  OR id=26  OR id=120  OR id=217  OR id=2452  OR id=1950  OR id=777  OR id=1894  OR id=2267  OR id=1639  OR id=1700  OR id=202  OR id=1841  OR id=2329  OR id=1209  OR id=2328  OR id=2447  OR id=798  OR id=1263  OR id=1005  OR id=219  OR id=1316  OR id=826  OR id=29  OR id=377  OR id=1992  OR id=1144  OR id=1099  OR id=1902  OR id=915  OR id=1150  OR id=2266  OR id=2072  OR id=1891  OR id=1744  OR id=2229  OR id=739  OR id=2453  OR id=1490  OR id=2076  OR id=2485  OR id=816  OR id=1261  OR id=2086  OR id=1745  OR id=2367  OR id=872  OR id=1945  OR id=825  OR id=2322  OR id=2419  OR id=2543  OR id=860  OR id=1804  OR id=1770  OR id=1482  OR id=245  OR id=2190  OR id=1454  OR id=2370  OR id=1935  OR id=2304  OR id=303  OR id=2393  OR id=1735  OR id=2045  OR id=278  OR id=1921  OR id=1757  OR id=1051  OR id=2130  OR id=1991  OR id=1511  OR id=1537  OR id=15  OR id=1045  OR id=1504  OR id=2389  OR id=1043  OR id=1466  OR id=1832  OR id=2268  OR id=2475  OR id=2491  OR id=2255  OR id=2256  OR id=1467  OR id=1438  OR id=1286  OR id=2458  OR id=2449  OR id=2533  OR id=2386  OR id=2504  OR id=2505  OR id=2432  OR id=1846  OR id=2077  OR id=2127  OR id=2429  OR id=2262  OR id=2525  OR id=2527  OR id=2541  OR id=2544  OR id=2526  OR id=2446  OR id=1257  OR id=1345  OR id=1629  OR id=1152  OR id=1013  OR id=831  OR id=1342  OR id=1953  OR id=1666  OR id=1468  OR id=1969  OR id=747  OR id=509  OR id=17  OR id=1546  OR id=2250  OR id=2489  OR id=1785  OR id=905 "

            Case "2013/8/17"
                strTeamQuery += "WHERE id=11  OR id=206  OR id=133  OR id=26  OR id=205  OR id=827  OR id=887  OR id=1142  OR id=120  OR id=219  OR id=1902  OR id=1263  OR id=2086  OR id=202  OR id=1841  OR id=1894  OR id=1700  OR id=1209  OR id=217  OR id=1099  OR id=1969  OR id=798  OR id=121  OR id=2384  OR id=1639  OR id=29  OR id=1342  OR id=777  OR id=1950  OR id=2328  OR id=1144  OR id=1261  OR id=1150  OR id=825  OR id=915  OR id=1629  OR id=1744  OR id=2447  OR id=1045  OR id=1316  OR id=2266  OR id=1921  OR id=1935  OR id=816  OR id=2150  OR id=1997  OR id=1953  OR id=2059  OR id=1832  OR id=2391  OR id=2543  OR id=2213  OR id=2114  OR id=2581  OR id=2439  OR id=852  OR id=2242  OR id=2417  OR id=2094  OR id=1542  OR id=2151  OR id=1325  OR id=2255  OR id=1040  OR id=2008  OR id=1345  OR id=1992  OR id=1013  OR id=826  OR id=2304  OR id=2344  OR id=2487  OR id=1546  OR id=2452  OR id=1989  OR id=1951  OR id=1286  OR id=142  OR id=2076  OR id=2229  OR id=204  OR id=1438  OR id=1257  OR id=2305  OR id=2453  OR id=2443  OR id=1111  OR id=2267  OR id=1770  OR id=739  OR id=1785  OR id=278  OR id=2256  OR id=2306  OR id=2185  OR id=1537  OR id=1152  OR id=1388  OR id=1991  OR id=2178  OR id=1845  OR id=1677  OR id=2111  OR id=2572  OR id=2580  OR id=2433  OR id=2353  OR id=2369  OR id=2403  OR id=2404  OR id=1985  OR id=220  OR id=2130  OR id=1482  OR id=1735  OR id=2003  OR id=2072  OR id=2085  OR id=1511  OR id=1258  OR id=17  OR id=1666  OR id=2250  OR id=2486  OR id=1504  OR id=1005 "

            Case "2013/8/24"
                strTeamQuery += "WHERE id=11  OR id=17  OR id=26  OR id=29  OR id=120  OR id=133  OR id=202  OR id=205  OR id=206  OR id=217  OR id=219  OR id=245  OR id=278  OR id=303  OR id=311  OR id=329  OR id=412  OR id=514  OR id=607  OR id=739  OR id=777  OR id=798  OR id=816  OR id=825  OR id=826  OR id=827  OR id=872  OR id=887  OR id=905  OR id=915  OR id=919  OR id=1005  OR id=1013  OR id=1043  OR id=1051  OR id=1099  OR id=1142  OR id=1144  OR id=1150  OR id=1152  OR id=1209  OR id=1210  OR id=1261  OR id=1263  OR id=1286  OR id=1316  OR id=1323  OR id=1342  OR id=1438  OR id=1454  OR id=1467  OR id=1490  OR id=1504  OR id=1511  OR id=1537  OR id=1546  OR id=1603  OR id=1629  OR id=1639  OR id=1647  OR id=1662  OR id=1666  OR id=1700  OR id=1735  OR id=1744  OR id=1770  OR id=1785  OR id=1832  OR id=1841  OR id=1845  OR id=1846  OR id=1850  OR id=1894  OR id=1901  OR id=1902  OR id=1921  OR id=1950  OR id=1969  OR id=1989  OR id=1992  OR id=1997  OR id=2008  OR id=2009  OR id=2024  OR id=2037  OR id=2059  OR id=2072  OR id=2076  OR id=2086  OR id=2116  OR id=2122  OR id=2130  OR id=2150  OR id=2229  OR id=2248  OR id=2250  OR id=2256  OR id=2257  OR id=2261  OR id=2266  OR id=2267  OR id=2291  OR id=2304  OR id=2305  OR id=2317  OR id=2328  OR id=2329  OR id=2330  OR id=2357  OR id=2369  OR id=2370  OR id=2389  OR id=2392  OR id=2393  OR id=2400  OR id=2419  OR id=2429  OR id=2432  OR id=2444  OR id=2446  OR id=2452  OR id=2453  OR id=2458  OR id=2475  OR id=2485  OR id=2486  OR id=2487  OR id=2489  OR id=2495  OR id=2516  OR id=2535  OR id=2539  OR id=2541  OR id=2546  OR id=2549  OR id=2570  OR id=2595  OR id=2599  OR id=2606  OR id=2613  OR id=2614  OR id=2615  OR id=2643  OR id=2644  OR id=2645  OR id=2647  OR id=2650  OR id=2656 "


        End Select
        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub

    Dim comprastotales As Decimal
    Dim varbono1, varbono2, varbono3, varbono4, varbono5, varbono6, varbono7, varbono8 As Decimal

    Sub calculacomisiones()
        Try
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = ""

            strTeamQuery = "SELECT SUM(monto), bono FROM pagos GROUP BY bono, corte, status HAVING Corte =" & Me.periodo.Text & " AND status=1"

            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()
            Dim bono1, bono2, bono3, bono4, bono5, bono6, bono7, bono8 As Decimal


            While dtrTeam.Read
                Select Case dtrTeam(1)
                    Case 1
                        bono1 = dtrTeam(0)
                    Case 2
                        bono2 = dtrTeam(0)
                    Case 3
                        bono3 = dtrTeam(0)
                    Case 4
                        bono4 = dtrTeam(0)
                    Case 5
                        bono5 = dtrTeam(0)
                    Case 6
                        bono6 = dtrTeam(0)
                    Case 7
                        bono7 = dtrTeam(0)
                    Case 8
                        bono8 = dtrTeam(0)
                End Select


            End While

            sqlConn.Close()
            varbono1 = bono1
            varbono2 = bono2
            varbono3 = bono3
            varbono4 = bono4
            varbono5 = bono5
            varbono6 = bono6
            varbono7 = bono7
            varbono8 = bono8

            Me.lblbono1.Text = bono1.ToString
            Me.lblbono2.Text = bono2.ToString
            Me.lblbono3.Text = bono3.ToString
            Me.lblbono4.Text = bono4.ToString
            Me.lblbono5.Text = bono5.ToString
            Me.lblbono6.Text = bono6.ToString
            Me.lblbono7.Text = bono7.ToString()
            Me.lblbono8.Text = bono8.ToString
            Me.totalcomisiones.Text = (bono1 + bono2 + bono3 + bono4 + bono5 + bono6 + bono7 + bono8).ToString
            porcbono1.Text = ((bono1 / (Me.totalcomisiones.Text)) * 100).ToString("####0.00") & "%"
            porcbono2.Text = ((bono2 / (Me.totalcomisiones.Text)) * 100).ToString("####0.00") & "%"
            porcbono3.Text = ((bono3 / (Me.totalcomisiones.Text)) * 100).ToString("####0.00") & "%"
            porcbono4.Text = ((bono4 / (Me.totalcomisiones.Text)) * 100).ToString("####0.00") & "%"
            porcbono5.Text = ((bono5 / (Me.totalcomisiones.Text)) * 100).ToString("####0.00") & "%"
            porcbono6.Text = ((bono6 / (Me.totalcomisiones.Text)) * 100).ToString("####0.00") & "%"
            porcbono7.Text = ((bono7 / (Me.totalcomisiones.Text)) * 100).ToString("####0.00") & "%"
            porcbono8.Text = ((bono8 / (Me.totalcomisiones.Text)) * 100).ToString("####0.00") & "%"
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString & estado
            Me.mensajes.Visible = True
        End Try
    End Sub
    Sub calculatotalcompras()
        Try
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = "SELECT inicio, final FROM periodos WHERE id=" & Me.periodo.Text

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
            'busca total

            strTeamQuery = "SELECT SUM(costo) FROM compras WHERE fecha>='" & de.ToString("yyyy/M/d") & "' AND fecha<='" & a.ToString("yyyy/M/d") & "'"

            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)



            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read

                If Not IsDBNull(dtrTeam(0)) Then
                    Me.totalcompras.Text = dtrTeam(0).ToString
                    comprastotales = dtrTeam(0)
                End If


            End While

            sqlConn.Close()
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString & estado
            Me.mensajes.Visible = True
        End Try
    End Sub

    Sub llenagrid()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT pagos.asociado, CONCAT( asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) AS nombre, pagos.bono, pagos.monto, pagos.de, pagos.a, pagos.corte FROM pagos INNER JOIN asociados ON pagos.asociado=asociados.id WHERE pagos.corte=" & Me.periodo.Text & " ORDER BY pagos.asociado ASC, pagos.bono ASC"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Me.GridView1.DataSource = dtrTeam
        Me.GridView1.DataBind()

        sqlConn.Close()
    End Sub
    Sub checarango25nov2013(ByVal idasociado As Integer, ByVal rango As Integer)
        Session("mensaje") = "El último en checar rango fue el Asociado " & idasociado.ToString & " Rango " & rango.ToString
        Dim directosder, directosizq As Integer
        Dim qry_directosporlado As String = "SELECT Count(asociados.ID) AS directos, asociados.ladopatrocinador AS lado " & _
                                                                  "FROM(asociados) " & _
                                                                  "WHERE asociados.patrocinador=" & idasociado.ToString & " AND (asociados.status=1  OR '" & CDate(a.Text).ToString("yyyy/MM/dd") & "' BETWEEN inicioactivacion AND finactivacion)  " & _
                                                                  "GROUP BY asociados.patrocinador, asociados.ladopatrocinador " & _
                                                                  "ORDER BY asociados.patrocinador, asociados.ladopatrocinador ;"


        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(qry_directosporlado, sqlConn)

        Dim dtrTeam As MySqlDataReader

        'directos por lado período 1
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            Select Case UCase(dtrTeam(1))
                Case "D"
                    directosder = dtrTeam(0)
                Case "I"
                    directosizq = dtrTeam(0)
            End Select
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        'en mi organización
        Dim qry_miarbol As String = "SELECT id, recorrido, ladosrecorrido FROM asociados WHERE (status=1  OR '" & CDate(a.Text).ToString("yyyy/MM/dd") & "' BETWEEN inicioactivacion AND finactivacion)  AND recorrido LIKE '%." & idasociado.ToString & ".%'"
        cmdFetchTeam = New MySqlCommand(qry_miarbol, sqlConn)


        'directos por lado período 1
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim recorrido, ladosrecorrido As String
        Dim recorridoarray(), ladosarray() As String
        Dim izq, der As Integer
        While dtrTeam.Read
            If dtrTeam(0) = 471 Then
                Dim x As Integer = dtrTeam(0)
            End If



            recorrido = dtrTeam(1)

            ladosrecorrido = dtrTeam(2)

            recorridoarray = Split(recorrido, ".")
            ladosarray = Split(ladosrecorrido, ".")
            Dim posicion As Integer = 0
            For posicion = 0 To recorridoarray.Length - 1
                If recorridoarray(posicion) = idasociado.ToString Then
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

        'hasta acá



        Dim rangoactual As Integer = 1
        'colaborador 
        If directosder >= 1 And directosizq >= 1 And izq >= 3 And der >= 3 Then rangoactual = 2
        If directosder >= 1 And directosizq >= 1 And izq >= 10 And der >= 10 Then rangoactual = 3
        If (directosder + directosizq) >= 3 And directosder >= 1 And directosizq >= 1 And izq >= 20 And der >= 20 Then rangoactual = 4
        If (directosder + directosizq) >= 3 And directosder >= 1 And directosizq >= 1 And izq >= 50 And der >= 50 Then rangoactual = 5
        If directosder >= 2 And directosizq >= 2 And izq >= 75 And der >= 75 Then rangoactual = 6
        If directosder >= 2 And directosizq >= 2 And izq >= 160 And der >= 160 Then rangoactual = 7
        If directosder >= 2 And directosizq >= 2 And izq >= 400 And der >= 400 Then rangoactual = 8
        If directosder >= 2 And directosizq >= 2 And izq >= 800 And der >= 800 Then rangoactual = 9

        actualizarango(idasociado, rango, rangoactual)


    End Sub
    Sub checarango10oct2013(ByVal idasociado As Integer, ByVal rango As Integer)
        Session("mensaje") = "El último en checar rango fue el Asociado " & idasociado.ToString & " Rango " & rango.ToString

        Dim puntosgrupalesizq, puntosgrupalesder, directosizq, directosder, mipuntaje, puntosgrupalesizq2, puntosgrupalesder2, directosizq2, directosder2, mipuntaje2 As Integer
        Dim qry_directosporlado As String = "SELECT Count(asociados.ID) AS directos, asociados.ladopatrocinador AS lado " & _
                                                                  "FROM(asociados) " & _
                                                                  "WHERE(((asociados.patrocinador)=" & idasociado.ToString & ") AND asociados.status=1 AND asociados.finsc<='" & mediociclo.ToString("yyyy/M/dd") & "') " & _
                                                                  "GROUP BY asociados.patrocinador, asociados.ladopatrocinador " & _
                                                                  "ORDER BY asociados.patrocinador, asociados.ladopatrocinador ;"
        Dim qry_directosporlado2 As String = "SELECT Count(asociados.ID) AS directos, asociados.ladopatrocinador AS lado " & _
                                                   "FROM(asociados) " & _
                                                   "WHERE(((asociados.patrocinador)=" & idasociado.ToString & ") AND asociados.status=1 AND asociados.finsc<='" & finciclo.ToString("yyyy/M/dd") & "') " & _
                                                   "GROUP BY asociados.patrocinador, asociados.ladopatrocinador " & _
                                                   "ORDER BY asociados.patrocinador, asociados.ladopatrocinador ;"

        Dim qry_puntosdegrupo As String = "SELECT compras.puntos, asociados.recorrido, asociados.ladosrecorrido " & _
                                            "FROM asociados INNER JOIN compras ON asociados.ID = compras.asociado " & _
                                            "WHERE compras.puntos>0 AND compras.statuspago='PAGADO' AND asociados.historia Like '%." & idasociado.ToString & ".%' AND compras.fecha>='" & iniciociclo.ToString("yyyy/M/dd") & "' AND compras.fecha <='" & mediociclo.ToString("yyyy/M/dd") & "' "
        Dim qry_puntosdegrupo2 As String = "SELECT compras.puntos, asociados.recorrido, asociados.ladosrecorrido " & _
                                                           "FROM asociados INNER JOIN compras ON asociados.ID = compras.asociado " & _
                                                           "WHERE compras.puntos>0 AND compras.statuspago='PAGADO' AND asociados.historia Like '%." & idasociado.ToString & ".%' AND compras.fecha>'" & mediociclo.ToString("yyyy/M/dd") & "' AND compras.fecha <='" & finciclo.ToString("yyyy/M/dd") & "' "


        'Dim qry_mipuntaje = "SELECT SUM(compras.puntos) FROM compras WHERE asociado=" & idasociado.ToString & " AND compras.statuspago='PAGADO' AND fecha>='" & iniciociclo.ToString("yyyy/M/dd") & "' AND fecha <='" & mediociclo.ToString("yyyy/M/dd") & "'"
        'Dim qry_mipuntaje2 = "SELECT SUM(compras.puntos) FROM compras WHERE asociado=" & idasociado.ToString & " AND compras.statuspago='PAGADO' AND fecha>'" & mediociclo.ToString("yyyy/M/dd") & "' AND fecha <='" & finciclo.ToString("yyyy/M/dd") & "'"
        'Dim qry_mipuntaje = "SELECT SUM(compras.puntos) FROM compras WHERE asociado=" & idasociado.ToString & " AND compras.statuspago='PAGADO' AND inicioactivacion>='" & iniciociclo.ToString("yyyy/M/dd") & "' AND inicioactivacion <='" & mediociclo.ToString("yyyy/M/dd") & "'"
        'Dim qry_mipuntaje2 = "SELECT SUM(compras.puntos) FROM compras WHERE asociado=" & idasociado.ToString & " AND compras.statuspago='PAGADO' AND inicioactivacion>'" & mediociclo.ToString("yyyy/M/dd") & "' AND inicioactivacion <='" & finciclo.ToString("yyyy/M/dd") & "'"
        Dim qry_mipuntaje = "SELECT SUM(compras.puntos) FROM compras WHERE asociado=" & idasociado.ToString & " AND compras.statuspago='PAGADO' AND inicioactivacion<='" & mediociclo.ToString("yyyy/M/dd") & "' AND finactivacion >='" & mediociclo.ToString("yyyy/M/dd") & "'"
        Dim qry_mipuntaje2 = "SELECT SUM(compras.puntos) FROM compras WHERE asociado=" & idasociado.ToString & " AND compras.statuspago='PAGADO' AND inicioactivacion<='" & finciclo.ToString("yyyy/M/dd") & "' AND finactivacion>='" & finciclo.ToString("yyyy/M/dd") & "'"


        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(qry_directosporlado, sqlConn)

        Dim dtrTeam As MySqlDataReader

        'directos por lado período 1
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            Select Case UCase(dtrTeam(1))
                Case "D"
                    directosder = dtrTeam(0)
                Case "I"
                    directosizq = dtrTeam(0)
            End Select
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        'directos por lado período 2
        cmdFetchTeam = New MySqlCommand(qry_directosporlado2, sqlConn)



        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            Select Case UCase(dtrTeam(1))
                Case "D"
                    directosder2 = dtrTeam(0)
                Case "I"
                    directosizq2 = dtrTeam(0)
            End Select
        End While

        sqlConn.Close()
        sqlConn.Dispose()




        'puntos de grupo 1
        cmdFetchTeam = New MySqlCommand(qry_puntosdegrupo, sqlConn)



        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        puntosgrupalesder = 0

        puntosgrupalesizq = 0
        While dtrTeam.Read
            Dim lado As String
            Dim recorrido() As String = Split(dtrTeam(1), ".")
            Dim lados() As String = Split(dtrTeam(2), ".")
            For i = 0 To recorrido.Length - 1
                If recorrido(i) <> "" Then
                    If Not IsNumeric(recorrido(i)) Then recorrido(i) = funciones.solonumeros(recorrido(i))

                    If CInt(recorrido(i)) = idasociado Then
                        lado = lados(i)
                        Exit For
                    End If
                End If





            Next


            Select Case UCase(lado)
                Case "D"
                    puntosgrupalesder += dtrTeam(0)
                Case "I"
                    puntosgrupalesizq += dtrTeam(0)
            End Select
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        'puntos de grupo 2
        cmdFetchTeam = New MySqlCommand(qry_puntosdegrupo2, sqlConn)



        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        puntosgrupalesder2 = 0

        puntosgrupalesizq2 = 0
        While dtrTeam.Read
            Dim lado As String
            Dim recorrido() As String = Split(dtrTeam(1), ".")
            Dim lados() As String = Split(dtrTeam(2), ".")
            For i = 0 To recorrido.Length - 1
                If recorrido(i) <> "" Then


                    If Not IsNumeric(recorrido(i)) Then
                        Dim funciones As New funciones
                        recorrido(i) = funciones.devuelvenumero(recorrido(i)).ToString
                    End If
                    If CInt(recorrido(i)) = idasociado Then
                        lado = lados(i)
                        Exit For
                    End If
                End If





            Next


            Select Case UCase(lado)
                Case "D"
                    puntosgrupalesder2 += dtrTeam(0)
                Case "I"
                    puntosgrupalesizq2 += dtrTeam(0)
            End Select
        End While

        sqlConn.Close()
        sqlConn.Dispose()


        ' mi puntaje 1
        cmdFetchTeam = New MySqlCommand(qry_mipuntaje, sqlConn)



        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then
                mipuntaje = dtrTeam(0)
            Else
                mipuntaje = 0
            End If
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        ' mi puntaje 2
        cmdFetchTeam = New MySqlCommand(qry_mipuntaje2, sqlConn)



        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then
                mipuntaje2 = dtrTeam(0)
            Else
                mipuntaje2 = 0
            End If
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        '% de balanceo
        Dim puntosmensualesporladorango2 As Integer = 0
        Dim puntosmensualesporladorango3 As Integer = 0
        Dim puntosmensualesporladorango4 As Integer = 0
        Dim puntosmensualesporladorango5 As Integer = 0
        Dim puntosmensualesporladorango6 As Integer = 0
        Dim puntosmensualesporladorango7 As Integer = 0
        Dim puntosmensualesporladorango8 As Integer = 0
        Dim puntosmensualesporladorango9 As Integer = 0
        Dim porcentajedebalance As Integer = 0
        Dim porcentajedebalancecolaborador As Integer = 0
        Dim querybalanceo As String = "SELECT porcentajedebalance, porcentajedebalancerango2, puntosrango2, puntosrango3, puntosrango4, puntosrango5, puntosrango6, puntosrango7, puntosrango8, puntosrango9 FROM configuracion"
        cmdFetchTeam = New MySqlCommand(querybalanceo, sqlConn)



        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            porcentajedebalance = dtrTeam(0)
            porcentajedebalancecolaborador = dtrTeam(1)
            puntosmensualesporladorango2 = dtrTeam(2)
            puntosmensualesporladorango3 = dtrTeam(3)
            puntosmensualesporladorango4 = dtrTeam(4)
            puntosmensualesporladorango5 = dtrTeam(5)
            puntosmensualesporladorango6 = dtrTeam(6)
            puntosmensualesporladorango7 = dtrTeam(7)
            puntosmensualesporladorango8 = dtrTeam(8)
            puntosmensualesporladorango9 = dtrTeam(9)
        End While

        sqlConn.Close()
        sqlConn.Dispose()



        'hasta acá

        Dim balancemes1, balancemes2 As Boolean


        Dim rangoactual As Integer = 1
        Dim puntosgrupales As Integer = puntosgrupalesder + puntosgrupalesizq + mipuntaje
        Dim puntosgrupales2 As Integer = puntosgrupalesder2 + puntosgrupalesizq2 + mipuntaje2
        If mipuntaje >= 700 And mipuntaje2 >= 700 Then


            'revisa rango 2
            If puntosgrupales >= puntosmensualesporladorango2 And puntosgrupales2 >= puntosmensualesporladorango2 And directosder >= 1 And directosizq >= 1 And directosder2 >= 1 And directosizq2 >= 1 Then
                'checa ahora el balance
                'mes 1
                If puntosgrupalesder < puntosgrupalesizq Then
                    If puntosgrupalesder >= (((100 - porcentajedebalancecolaborador) / 100) * puntosmensualesporladorango2) Then
                        balancemes1 = True
                    End If
                Else
                    If puntosgrupalesizq >= (((100 - porcentajedebalancecolaborador) / 100) * puntosmensualesporladorango2) Then
                        balancemes1 = True
                    End If
                End If
                'mes 2
                If puntosgrupalesder2 < puntosgrupalesizq2 Then
                    If puntosgrupalesder2 >= (((100 - porcentajedebalancecolaborador) / 100) * puntosmensualesporladorango2) Then
                        balancemes2 = True
                    End If
                Else
                    If puntosgrupalesizq2 >= (((100 - porcentajedebalancecolaborador) / 100) * puntosmensualesporladorango2) Then
                        balancemes2 = True
                    End If
                End If
                If balancemes1 And balancemes2 Then
                    rangoactual = 2 'colaborador
                End If

            End If

            balancemes1 = balancemes2 = False
            'revisa rango 3
            If puntosgrupales >= puntosmensualesporladorango3 And puntosgrupales2 >= puntosmensualesporladorango3 And ((directosder >= 2 And directosizq >= 1) Or (directosder >= 1 And directosizq >= 2)) And ((directosder2 >= 2 And directosizq2 >= 1) Or (directosder2 >= 1 And directosizq2 >= 2)) Then
                'checa ahora el balance
                'mes 1
                If puntosgrupalesder < puntosgrupalesizq Then
                    If puntosgrupalesder >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango3) Then
                        balancemes1 = True
                    End If
                Else
                    If puntosgrupalesizq >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango3) Then
                        balancemes1 = True
                    End If
                End If
                'mes 2
                If puntosgrupalesder2 < puntosgrupalesizq2 Then
                    If puntosgrupalesder2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango3) Then
                        balancemes2 = True
                    End If
                Else
                    If puntosgrupalesizq2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango3) Then
                        balancemes2 = True
                    End If
                End If
                If balancemes1 And balancemes2 Then
                    rangoactual = 3 'colaborador ejecutivo
                End If

            End If
            balancemes1 = balancemes2 = False
            'revisa rango 4
            If puntosgrupales >= puntosmensualesporladorango4 And puntosgrupales2 >= puntosmensualesporladorango4 And ((directosder >= 2 And directosizq >= 1) Or (directosder >= 1 And directosizq >= 2)) And ((directosder2 >= 2 And directosizq2 >= 1) Or (directosder2 >= 1 And directosizq2 >= 2)) Then
                'checa ahora el balance
                'mes 1
                If puntosgrupalesder < puntosgrupalesizq Then
                    If puntosgrupalesder >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango4) Then
                        balancemes1 = True
                    End If
                Else
                    If puntosgrupalesizq >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango4) Then
                        balancemes1 = True
                    End If
                End If
                'mes 2
                If puntosgrupalesder2 < puntosgrupalesizq2 Then
                    If puntosgrupalesder2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango4) Then
                        balancemes2 = True
                    End If
                Else
                    If puntosgrupalesizq2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango4) Then
                        balancemes2 = True
                    End If
                End If
                If balancemes1 And balancemes2 Then
                    rangoactual = 4 'bronce
                End If

            End If
            balancemes1 = balancemes2 = False
            'revisa rango 5
            If puntosgrupales >= puntosmensualesporladorango5 And puntosgrupales2 >= puntosmensualesporladorango5 And ((directosder >= 2 And directosizq >= 1) Or (directosder >= 1 And directosizq >= 2)) And ((directosder2 >= 2 And directosizq2 >= 1) Or (directosder2 >= 1 And directosizq2 >= 2)) Then
                'checa ahora el balance
                'mes 1
                If puntosgrupalesder < puntosgrupalesizq Then
                    If puntosgrupalesder >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango5) Then
                        balancemes1 = True
                    End If
                Else
                    If puntosgrupalesizq >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango5) Then
                        balancemes1 = True
                    End If
                End If
                'mes 2
                If puntosgrupalesder2 < puntosgrupalesizq2 Then
                    If puntosgrupalesder2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango5) Then
                        balancemes2 = True
                    End If
                Else
                    If puntosgrupalesizq2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango5) Then
                        balancemes2 = True
                    End If
                End If
                If balancemes1 And balancemes2 Then
                    rangoactual = 5 'plata
                End If

            End If
            balancemes1 = balancemes2 = False
            'revisa rango 6
            If puntosgrupales >= puntosmensualesporladorango6 And puntosgrupales2 >= puntosmensualesporladorango6 And (directosder >= 2 And directosizq >= 2) And (directosder2 >= 2 And directosizq2 >= 2) Then
                'checa ahora el balance
                'mes 1
                If puntosgrupalesder < puntosgrupalesizq Then
                    If puntosgrupalesder >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango6) Then
                        balancemes1 = True
                    End If
                Else
                    If puntosgrupalesizq >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango6) Then
                        balancemes1 = True
                    End If
                End If
                'mes 2
                If puntosgrupalesder2 < puntosgrupalesizq2 Then
                    If puntosgrupalesder2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango6) Then
                        balancemes2 = True
                    End If
                Else
                    If puntosgrupalesizq2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango6) Then
                        balancemes2 = True
                    End If
                End If
                If balancemes1 And balancemes2 Then
                    rangoactual = 6 'Oro
                End If

            End If
            balancemes1 = balancemes2 = False

            'revisa rango 7
            If puntosgrupales >= puntosmensualesporladorango7 And puntosgrupales2 >= puntosmensualesporladorango7 And (directosder >= 2 And directosizq >= 2) And (directosder2 >= 2 And directosizq2 >= 2) Then
                'checa ahora el balance
                'mes 1
                If puntosgrupalesder < puntosgrupalesizq Then
                    If puntosgrupalesder >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango7) Then
                        balancemes1 = True
                    End If
                Else
                    If puntosgrupalesizq >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango7) Then
                        balancemes1 = True
                    End If
                End If
                'mes 2
                If puntosgrupalesder2 < puntosgrupalesizq2 Then
                    If puntosgrupalesder2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango7) Then
                        balancemes2 = True
                    End If
                Else
                    If puntosgrupalesizq2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango7) Then
                        balancemes2 = True
                    End If
                End If
                If balancemes1 And balancemes2 Then
                    rangoactual = 7 'Diamante
                End If

            End If
            balancemes1 = balancemes2 = False

            'revisa rango 8
            If puntosgrupales >= puntosmensualesporladorango8 And puntosgrupales2 >= puntosmensualesporladorango8 And (directosder >= 2 And directosizq >= 2) And (directosder2 >= 2 And directosizq2 >= 2) Then
                'checa ahora el balance
                'mes 1
                If puntosgrupalesder < puntosgrupalesizq Then
                    If puntosgrupalesder >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango8) Then
                        balancemes1 = True
                    End If
                Else
                    If puntosgrupalesizq >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango8) Then
                        balancemes1 = True
                    End If
                End If
                'mes 2
                If puntosgrupalesder2 < puntosgrupalesizq2 Then
                    If puntosgrupalesder2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango8) Then
                        balancemes2 = True
                    End If
                Else
                    If puntosgrupalesizq2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango8) Then
                        balancemes2 = True
                    End If
                End If
                If balancemes1 And balancemes2 Then
                    rangoactual = 8 'Diamante Ejecutivo
                End If

            End If
            balancemes1 = balancemes2 = False

            'revisa rango 9
            If puntosgrupales >= puntosmensualesporladorango9 And puntosgrupales2 >= puntosmensualesporladorango9 And (directosder >= 2 And directosizq >= 2) And (directosder2 >= 2 And directosizq2 >= 2) Then
                'checa ahora el balance
                'mes 1
                If puntosgrupalesder < puntosgrupalesizq Then
                    If puntosgrupalesder >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango9) Then
                        balancemes1 = True
                    End If
                Else
                    If puntosgrupalesizq >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango9) Then
                        balancemes1 = True
                    End If
                End If
                'mes 2
                If puntosgrupalesder2 < puntosgrupalesizq2 Then
                    If puntosgrupalesder2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango9) Then
                        balancemes2 = True
                    End If
                Else
                    If puntosgrupalesizq2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango9) Then
                        balancemes2 = True
                    End If
                End If
                If balancemes1 And balancemes2 Then
                    rangoactual = 9 'Diamante Internacional
                End If

            End If
            balancemes1 = balancemes2 = False
        End If

        If rangoactual <> rango Then
            actualizarango(idasociado, rango, rangoactual)
        End If

    End Sub
    Sub checarangoanterior(ByVal idasociado As Integer, ByVal rango As Integer)

        Dim puntosgrupalesizq, puntosgrupalesder, directosizq, directosder, mipuntaje As Integer

        Dim qry_directosporlado As String = "SELECT Count(asociados.ID) AS directos, asociados.ladopatrocinador AS lado " & _
                                                   "FROM(asociados) " & _
                                                   "WHERE(((asociados.patrocinador)=" & idasociado.ToString & ") AND asociados.status=1) " & _
                                                   "GROUP BY asociados.patrocinador, asociados.ladopatrocinador " & _
                                                   "ORDER BY asociados.patrocinador, asociados.ladopatrocinador ;"

        Dim qry_puntosdegrupo As String = "SELECT compras.puntos, asociados.recorrido, asociados.ladosrecorrido " & _
                                                 "FROM asociados INNER JOIN compras ON asociados.ID = compras.asociado " & _
                                                 "WHERE compras.puntos>0 AND asociados.historia Like '%." & idasociado.ToString & ".%' AND compras.fecha>='" & iniciociclo.ToString("yyyy/M/dd") & "' AND compras.fecha <='" & finciclo.ToString("yyyy/M/dd") & "' "

        Dim qry_mipuntaje = "SELECT SUM(compras.puntos) FROM compras WHERE asociado=" & idasociado.ToString & " AND fecha>='" & iniciociclo.ToString("yyyy/M/dd") & "' AND fecha <='" & finciclo.ToString("yyyy/M/dd") & "'"

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(qry_directosporlado, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            Select Case UCase(dtrTeam(1))
                Case "D"
                    directosder = dtrTeam(0)
                Case "I"
                    directosizq = dtrTeam(0)
            End Select
        End While

        sqlConn.Close()
        sqlConn.Dispose()


        cmdFetchTeam = New MySqlCommand(qry_puntosdegrupo, sqlConn)



        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        puntosgrupalesder = 0

        puntosgrupalesizq = 0
        While dtrTeam.Read
            Dim lado As String
            Dim recorrido() As String = Split(dtrTeam(1), ".")
            Dim lados() As String = Split(dtrTeam(2), ".")
            For i = 0 To recorrido.Length - 1
                If recorrido(i) <> "" Then
                    If CInt(recorrido(i)) = idasociado Then
                        lado = lados(i)
                        Exit For
                    End If
                End If





            Next


            Select Case UCase(lado)
                Case "D"
                    puntosgrupalesder += dtrTeam(0)
                Case "I"
                    puntosgrupalesizq += dtrTeam(0)
            End Select
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        cmdFetchTeam = New MySqlCommand(qry_mipuntaje, sqlConn)



        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then
                mipuntaje = dtrTeam(0)
            Else
                mipuntaje = 0
            End If
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        If puntosgrupalesder > puntosgrupalesizq Then
            puntosgrupalesder += mipuntaje
        Else
            puntosgrupalesizq += mipuntaje
        End If

        '% de balanceo
        Dim porcentajedebalance As Integer = 0
        Dim querybalanceo As String = "SELECT porcentajedebalance FROM configuracion"
        cmdFetchTeam = New MySqlCommand(querybalanceo, sqlConn)



        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            porcentajedebalance = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()



        'hasta acá
        Dim rangoactual As Integer = 1
        Dim puntosgrupales As Integer = puntosgrupalesder + puntosgrupalesizq

        If puntosgrupales >= 9800 And directosder >= 1 And directosizq >= 1 Then
            'checa ahora el balance
            If puntosgrupalesder < puntosgrupalesizq Then

            Else

            End If
            rangoactual = 2 'colaborador
        End If
        If puntosgrupales >= 18200 And ((directosder >= 2 And directosizq >= 1) Or (directosder >= 1 And directosizq >= 2)) Then
            rangoactual = 3 'colaborador ejecutivo
        End If
        If puntosgrupales >= 35000 And ((directosder >= 2 And directosizq >= 1) Or (directosder >= 1 And directosizq >= 2)) Then
            rangoactual = 4 'bronce
        End If
        If puntosgrupales >= 68600 And ((directosder >= 2 And directosizq >= 1) Or (directosder >= 1 And directosizq >= 2)) Then
            rangoactual = 5 'plata
        End If
        If puntosgrupales >= 135800 And directosder >= 2 And directosizq >= 2 Then
            rangoactual = 6 'oro
        End If
        If puntosgrupales >= 280000 And directosder >= 2 And directosizq >= 2 Then
            rangoactual = 7 'diamante
        End If
        If puntosgrupales >= 840000 And directosder >= 2 And directosizq >= 2 Then
            rangoactual = 8 'diamante ejecutivo
        End If
        If puntosgrupales >= 1680000 And directosder >= 2 And directosizq >= 2 Then
            rangoactual = 8 'diamante internacional
        End If
        If rangoactual <> rango Then
            actualizarango(idasociado, rango, rangoactual)
        End If

    End Sub
    Sub bono1anterior(ByVal id As Integer)
        Dim montobono1 As Integer = 200
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT COUNT(id) FROM compras WHERE asociado=" & id.ToString & " AND paquete=3 AND activacion=0 AND fecha>='" & CDate(de.Text).ToString("yyyy/MM/dd") & "' AND fecha <='" & CDate(a.Text).ToString("yyyy/MM/dd") & "';"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cuenta As Integer = 0
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then cuenta = dtrTeam(0)


        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If cuenta > 0 Then
            sqlConn.Open()
            strTeamQuery = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status) VALUES(" & id.ToString & ", '" & CDate(de.Text).ToString("yyyy/MM/dd") & "', '" & CDate(a.Text).ToString("yyyy/MM/dd") & "', 1, " & (cuenta * montobono1).ToString & ", " & idperiodo.ToString & ",0)"

            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            cmdFetchTeam.ExecuteNonQuery()


            sqlConn.Close()
            sqlConn.Dispose()
        End If
    End Sub
    Sub bono1antesdeincluirellunes()
        estado += "Inicia Bono1. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""


        'antes de incluir el lunes
        strTeamQuery = "SELECT Count(compras.ID) AS CountOfID, compras.asociado " & _
                       "FROM(compras) " & _
                        "GROUP BY compras.asociado, compras.paquete, compras.activacion, compras.fecha, compras.fecha, compras.statuspago " & _
                        "HAVING compras.paquete=3 AND compras.activacion=0 AND compras.fecha>='" & CDate(de.Text).ToString("yyyy/MM/dd") & "' AND compras.fecha<='" & CDate(a.Text).ToString("yyyy/MM/dd") & "' AND compras.statuspago='PAGADO';"




        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cuenta As Integer = 0
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then insertabono1(dtrTeam(1), dtrTeam(0))


        End While

        sqlConn.Close()
        sqlConn.Dispose()
        estado += "Termina Bono1. "
    End Sub
    Sub bono2incluyendolunes()
        estado += "Inicia bono2. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT asociados.id, asociados.orden, asociados.patrocinador, compras.puntos  FROM asociados INNER JOIN compras ON asociados.id=compras.asociado WHERE compras.fecha<='" & CDate(a.Text).ToString("yyyy/MM/dd") & "' AND compras.fecha>='" & CDate(de.Text).ToString("yyyy/MM/dd") & "'  AND compras.inscripcion>0 ORDER BY patrocinador DESC;"
        strTeamQuery = ""


        'para incluir el lunes
        Dim fechafinalperiodo As Date = CDate(Me.a.Text)
        fechafinalperiodo = DateAdd(DateInterval.Day, 3, fechafinalperiodo)
        Dim fechafinalperiodoanterior As Date = DateAdd(DateInterval.Day, -1, CDate(Me.de.Text))
        Dim fechalunesanterior As Date = DateAdd(DateInterval.Day, 2, CDate(Me.de.Text))
        strTeamQuery = "SELECT asociados.id, asociados.orden, asociados.patrocinador, compras.puntos  " & _
                      "FROM asociados INNER JOIN compras ON asociados.id=compras.asociado  " & _
                      "WHERE compras.inscripcion>0 " & _
                      "AND compras.fecha >= '" & CDate(de.Text).ToString("yyyy/MM/dd") & "' " & _
                      "AND compras.fecha <= '" & fechafinalperiodo.Year.ToString & "/" & fechafinalperiodo.Month.ToString & "/" & fechafinalperiodo.Day.ToString & "' " & _
                      "AND compras.statuspago = 'PAGADO' " & _
                      "AND compras.fechaorden <= '" & CDate(a.Text).ToString("yyyy/MM/dd") & "' " & _
                      "AND compras.id NOT IN (" & _
                      "Select id FROM compras " & _
                      "WHERE fechaorden <= '" & fechafinalperiodoanterior.Year.ToString & "/" & fechafinalperiodoanterior.Month.ToString & "/" & fechafinalperiodoanterior.Day.ToString & "' " & _
                      "AND fecha >= '" & CDate(de.Text).ToString("yyyy/MM/dd") & "' " & _
                      "AND fecha <= '" & fechalunesanterior.Year.ToString & "/" & fechalunesanterior.Month.ToString & "/" & fechalunesanterior.Day.ToString & "' " & _
                      ") " & _
                      "GROUP BY compras.asociado"


        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim bonoa As Integer = 0
        Dim bonob As Integer = 0
        Dim bonopuntos As Integer = 0
        Dim id As Integer = 0
        While dtrTeam.Read
            If dtrTeam(2) <> id And id > 0 Then
                insertabono2(id, bonoa, bonob, bonopuntos)
                bonoa = 0
                bonob = 0
                bonopuntos = 0
                id = dtrTeam(2)
                If dtrTeam(1) < 3 Then
                    bonoa += 1
                Else
                    bonob += 1
                End If
                If dtrTeam(3) = 700 Then bonopuntos += 50
                If dtrTeam(3) = 1000 Then bonopuntos += 100
            Else
                id = dtrTeam(2)
                If dtrTeam(1) < 3 Then
                    bonoa += 1
                Else
                    bonob += 1
                End If
                If dtrTeam(3) = 700 Then bonopuntos += 50
                If dtrTeam(3) = 1000 Then bonopuntos += 100
            End If



        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If id >= raiz Then insertabono2(id, bonoa, bonob, bonopuntos)
        estado += "Termina bono2. "
    End Sub
    Sub bono28octubre()

        estado += "Inicia bono2. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, orden, patrocinador FROM asociados WHERE FInsc<='" & CDate(a.Text).ToString("yyyy/MM/dd") & "' AND FInsc>='" & CDate(de.Text).ToString("yyyy/MM/dd") & "' ORDER BY patrocinador DESC;"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim bonoa As Integer = 0
        Dim bonob As Integer = 0
        Dim id As Integer = 0
        While dtrTeam.Read
            If dtrTeam(2) <> id And id > 0 Then
                insertabono2(id, bonoa, bonob)
                bonoa = 0
                bonob = 0
                id = dtrTeam(2)
                If dtrTeam(1) < 3 Then
                    bonoa += 1
                Else
                    bonob += 1
                End If
            Else
                id = dtrTeam(2)
                If dtrTeam(1) < 3 Then
                    bonoa += 1
                Else
                    bonob += 1
                End If
            End If



        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If id >= raiz Then insertabono2(id, bonoa, bonob)
        estado += "Termina bono2. "
    End Sub
    Sub insertabono28octubre(ByVal asociado As Integer, ByVal bonoa As Integer, ByVal bonob As Integer)
        If Not estaactivo(asociado) Then
            Exit Sub
        End If
        Dim montobono2a As Integer = 60
        Dim montobono2b As Integer = 120
        Dim mispuntos As Integer = mispuntosdelmes(asociado)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos) VALUES(" & asociado.ToString & ", '" & CDate(de.Text).ToString("yyyy/MM/dd") & "', '" & CDate(a.Text).ToString("yyyy/MM/dd") & "', 2, " & ((bonoa * montobono2a) + (bonob * montobono2b)).ToString & ", " & idperiodo.ToString & ",0, " & mispuntos.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub bono3anterior()
        estado += "Inicia bono3. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT asociados.ID AS padre, asociados.patrocinador AS abuelo, asociados_1.ID AS asociado, asociados_1.FInsc, asociados_1.Orden, asociados.Orden " & _
                        "FROM asociados INNER JOIN asociados AS asociados_1 ON asociados.ID = asociados_1.patrocinador " & _
                        "WHERE asociados_1.Orden<3 AND asociados.Orden>2 AND  asociados_1.FInsc<='" & CDate(a.Text).ToString("yyyy/MM/dd") & "' AND asociados_1.FInsc>='" & CDate(de.Text).ToString("yyyy/MM/dd") & "' " & _
                        "ORDER BY asociados.patrocinador DESC"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim bono As Integer = 0
        Dim id As Integer = 0
        While dtrTeam.Read
            If dtrTeam(1) <> id And id > 0 Then
                insertabono3(id, bono)
                bono = 0

                id = dtrTeam(1)
                bono += 1
            Else
                id = dtrTeam(1)
                bono += 1
            End If



        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If id >= raiz Then insertabono3(id, bono)
        estado += "termina bono3. "
    End Sub
    Sub bono4doble()
        llenavistadeporcentajes()
        llenavistadepuntos()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        Dim inicio As Date = CDate(Me.de.Text)
        Dim fin As Date = CDate(Me.a.Text)
        strTeamQuery = "SELECT puntosasociados.Asociado , sum( if( `Lado` = 'D', `PorPagar` , 0 ) ) AS D, sum( if( `Lado` = 'I', `PorPagar` , 0 ) ) AS I "
        strTeamQuery += "FROM `puntosasociados`  INNER JOIN compras ON puntosasociados.compra=compras.id "
        strTeamQuery += "WHERE   compras.fecha<='" & fin.Year.ToString & "/" & fin.Month.ToString & "/" & fin.Day.ToString & "' "
        strTeamQuery += "GROUP BY `Asociado`  "
        strTeamQuery += "HAVING  (D > 0) AND (I >0)"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim bono As Integer = 0

        While dtrTeam.Read


            If dtrTeam(2) >= dtrTeam(1) Then
                If dtrTeam(2) >= dtrTeam(1) * 2 Then
                    insertabono4(dtrTeam(0), dtrTeam(1) * 2, "D", dtrTeam(2))
                Else

                    insertabono4(dtrTeam(0), dtrTeam(2), "I", dtrTeam(1))
                End If

            Else
                If dtrTeam(1) >= dtrTeam(2) * 2 Then
                    insertabono4(dtrTeam(0), dtrTeam(2) * 2, "I", dtrTeam(1))
                Else

                    insertabono4(dtrTeam(0), dtrTeam(1), "D", dtrTeam(2))
                End If



            End If


        End While

        sqlConn.Close()
        sqlConn.Dispose()


        estado += "Termina bono4. "


    End Sub
    Sub insertadetallebono4doble(ByVal asociado As Integer, ByVal corte As Integer, ByVal puntosfinalesi As Integer, ByVal puntosfinalesd As Integer, ByVal porcentajedepago As Decimal)
        'sacamos los puntos de la semana
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim nuevosi, nuevosd As Integer
        Dim dtrTeam As MySqlDataReader



        strTeamQuery = "SELECT SUM(puntosasociados.puntos), puntosasociados.lado FROM puntosasociados INNER JOIN compras ON compras.id=puntosasociados.compra WHERE compras.fecha>='" & CDate(de.Text).ToString("yyyy/MM/dd") & "' AND compras.fecha <='" & CDate(Me.a.Text).ToString("yyyy/MM/dd") & "' AND compras.statuspago='PAGADO' AND puntosasociados.asociado=" & asociado.ToString & " GROUP BY puntosasociados.lado"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim bono As Integer = 0

        While dtrTeam.Read

            If UCase(dtrTeam(1)) = "D" Then
                nuevosd = dtrTeam(0)
            Else
                nuevosi = dtrTeam(0)
            End If


        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Dim pagados As Integer = 0
        If puntosfinalesd > puntosfinalesi Then
            pagados = puntosfinalesd
        Else
            pagados = puntosfinalesi
        End If

        'iniciales
        strTeamQuery = "SELECT SUM(puntosasociados.porpagar), puntosasociados.lado FROM puntosasociados INNER JOIN compras ON compras.id=puntosasociados.compra WHERE compras.fecha <'" & CDate(Me.de.Text).ToString("yyyy/MM/dd") & "' AND compras.statuspago='PAGADO' AND puntosasociados.asociado=" & asociado.ToString & " GROUP BY puntosasociados.lado"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim inicialesi, inicialesd As Integer

        While dtrTeam.Read

            If UCase(dtrTeam(1)) = "D" Then
                inicialesd = dtrTeam(0)
            Else
                inicialesi = dtrTeam(0)
            End If


        End While

        sqlConn.Close()
        sqlConn.Dispose()


        porcentajedepago = porcentajedepago * 100
        strTeamQuery = "INSERT INTO puntosdetalle (asociado, inicialesi, inicialesd, nuevosi, nuevosd, pagados, porcentaje, corte) VALUES (" & asociado.ToString & "," & (inicialesi).ToString & "," & (inicialesd).ToString & "," & (nuevosi).ToString & "," & (nuevosd).ToString & "," & pagados.ToString & "," & porcentajedepago.ToString & "," & corte.ToString & ")"
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub insertabono4doble(ByVal asociado As Integer, ByVal puntos As Integer, ByVal lado As String, ByVal puntoscontrarios As Integer, ByVal normal As Boolean)
        Dim ladocontrario As String
        If lado = "D" Then
            ladocontrario = "I"
        Else
            ladocontrario = "D"
        End If
        Dim inicio As Date = CDate(Me.de.Text)
        Dim fin As Date = CDate(Me.a.Text)
        Dim puntosparadetalle = puntos
        'recoge % pago asociado
        If Not estaactivo(asociado) Then
            Exit Sub
        End If
        Dim mispuntos As Integer = mispuntosdelmes(asociado)
        Dim porcentaje As Decimal = porcentajedepago(asociado)
        'inserta pago
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos) VALUES(" & asociado.ToString & ", '" & CDate(de.Text).ToString("yyyy/MM/dd") & "', '" & CDate(a.Text).ToString("yyyy/MM/dd") & "', 4, " & (puntos * porcentaje).ToString & ", " & idperiodo.ToString & ",0, " & mispuntos.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
        'actualiza a pagados los puntos del lado indicado y de las fechas indicadas

        strTeamQuery = "UPDATE puntosasociados  INNER JOIN compras ON puntosasociados.compra=compras.id SET puntosasociados.status=1, puntosasociados.porpagar=0 WHERE puntosasociados.lado='" & lado & "' AND puntosasociados.asociado=" & asociado.ToString & " AND compras.fecha>='" & inicio.Year.ToString & "/" & inicio.Month.ToString & "/" & inicio.Day.ToString & "' AND  compras.fecha<='" & fin.Year.ToString & "/" & fin.Month.ToString & "/" & fin.Day.ToString & "'  "


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
        'actualiza los puntos del otro lado, haciendo la cuenta

        If normal Then
            strTeamQuery = "SELECT puntosasociados.id, puntosasociados.status, puntosasociados.porpagar FROM puntosasociados  INNER JOIN compras ON puntosasociados.compra=compras.id WHERE puntosasociados.asociado=" & asociado.ToString & " AND puntosasociados.lado='" & ladocontrario & "' AND puntosasociados.status<>1  AND compras.fecha>='" & inicio.Year.ToString & "/" & inicio.Month.ToString & "/" & inicio.Day.ToString & "' AND  compras.fecha<='" & fin.Year.ToString & "/" & fin.Month.ToString & "/" & fin.Day.ToString & "'"
        Else
            strTeamQuery = "SELECT puntosasociados.id, puntosasociados.status, puntosasociados.porpagar FROM puntosasociados  INNER JOIN compras ON puntosasociados.compra=compras.id WHERE puntosasociados.asociado=" & asociado.ToString & " AND puntosasociados.lado='" & ladocontrario & "' AND puntosasociados.status<>1  AND compras.fecha>='" & inicio.Year.ToString & "/" & inicio.Month.ToString & "/" & inicio.Day.ToString & "' AND  compras.fecha<='" & fin.Year.ToString & "/" & fin.Month.ToString & "/" & fin.Day.ToString & "'"
            puntos = puntos / 2
        End If

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        dtrTeam = cmdFetchTeam.ExecuteReader
        While dtrTeam.Read
            If puntos >= dtrTeam(2) Then
                actualizastatuspuntos(dtrTeam(0), 1)
                puntos -= dtrTeam(2)
                If puntos = 0 Then Exit While
            Else
                If dtrTeam(2) - puntos = 0 Then
                    actualizastatuspuntos(dtrTeam(0), 1)
                Else
                    actualizastatuspuntos(dtrTeam(0), 2, dtrTeam(2) - puntos)
                End If


                Exit While
            End If
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        'detalle de bono 4
        Dim derecho As Integer = 0
        Dim izquierdo As Integer = 0

        If lado = "D" Then
            If normal Then
                izquierdo = puntosparadetalle
                derecho = puntosparadetalle / 2
            Else
                derecho = puntosparadetalle
                izquierdo = puntosparadetalle / 2
            End If

        Else
            If normal Then
                derecho = puntosparadetalle
                izquierdo = puntosparadetalle / 2
            Else
                izquierdo = puntosparadetalle
                derecho = puntosparadetalle / 2
            End If



        End If
        insertadetallebono4(asociado, idperiodo, izquierdo, derecho, porcentaje)

    End Sub
    Sub bono6antesdeincluirellunes()

        estado += "Inicia bono6. "
        buscavaloresbono6()
        'borra tabla temporal bono 6
        borrabono6()
        'creamos tabla para agregarle las compras a cada asociado
        Dim ds As New DataSet
        Dim table As DataTable = ds.Tables.Add("Data")
        table.Columns.Add("asociado", GetType(Integer))
        table.Columns.Add("cantidad", GetType(Integer))
        table.Columns.Add("paquete", GetType(Integer))
        table.Columns.Add("compra", GetType(Integer))
        table.Columns.Add("ptsmes", GetType(Integer))
        'select de compras
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "" & _
        "SELECT asociados.ID AS Asociado, asociados.Orden AS orden, asociados.patrocinador AS patrocinador, compras.ID AS Compra, compras.fecha AS fecha, compras.cantidad AS cantidad, compras.paquete, asociados.bono6 AS bono6, asociados.ptsmes " & _
        "FROM asociados INNER JOIN compras ON asociados.ID = compras.asociado " & _
        "WHERE compras.Fecha >= '" & CDate(de.Text).ToString("yyyy/MM/dd") & "' AND compras.Fecha <= '" & CDate(a.Text).ToString("yyyy/MM/dd") & "' AND compras.total>240 AND compras.excedente=0  AND compras.statuspago='PAGADO' " & _
        "ORDER BY compras.ID DESC;"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader




        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            table.Rows.Add(New Object() {dtrTeam(7), dtrTeam(5), dtrTeam(6), dtrTeam(3), dtrTeam(8)})

        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Dim viewbono6 As DataView = ds.Tables(0).DefaultView
        viewbono6.Sort = "asociado DESC"
        Dim drv As DataRowView
        Dim idasociado As Integer = 0
        Dim cantidad1 As Integer = 0
        Dim cantidad2 As Integer = 0
        Dim ptsmes As Integer = 0
        For Each drv In viewbono6

            If drv("asociado") <> idasociado Then
                ptsmes = drv("ptsmes")
                If idasociado >= raiz Then insertabono6(idasociado, cantidad1, cantidad2, ptsmes)

                idasociado = drv("asociado")
                cantidad1 = 0
                cantidad2 = 0
                ptsmes = 0
            End If
            If drv("paquete") > 1 Then
                cantidad2 += drv("cantidad")
            Else
                cantidad1 += drv("cantidad")
            End If
        Next
        If idasociado >= raiz Then insertabono6(idasociado, cantidad1, cantidad2, ptsmes)
        estado += "Termina bono6. "

    End Sub
    Sub bono6anterior()
        'borra tabla temporal bono 6
        borrabono6()
        'creamos tabla para agregarle las compras a cada asociado
        Dim ds As New DataSet
        Dim table As DataTable = ds.Tables.Add("Data")
        table.Columns.Add("asociado", GetType(Integer))
        table.Columns.Add("cantidad", GetType(Integer))
        table.Columns.Add("paquete", GetType(Integer))
        table.Columns.Add("compra", GetType(Integer))
        'select de compras
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "" & _
        "SELECT asociados.ID AS Asociado, asociados.Orden AS orden, asociados.patrocinador AS patrocinador, compras.ID AS Compra, compras.fecha AS fecha, compras.cantidad AS cantidad, compras.paquete " & _
        "FROM asociados INNER JOIN compras ON asociados.ID = compras.asociado " & _
        "WHERE compras.Fecha >= '" & CDate(de.Text).ToString("yyyy/MM/dd") & "' AND compras.Fecha <= '" & CDate(a.Text).ToString("yyyy/MM/dd") & "' AND paquete>0 " & _
        "ORDER BY compras.ID DESC;"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader




        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            If dtrTeam(1) > 2 Then
                table.Rows.Add(New Object() {dtrTeam(2), dtrTeam(5), dtrTeam(6), dtrTeam(3)})
            Else
                antepasado = buscaantepasadomayoratres(dtrTeam(2))
                If antepasado > 0 Then table.Rows.Add(New Object() {antepasado, dtrTeam(5), dtrTeam(6), dtrTeam(3)})
            End If
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Dim viewbono6 As DataView = ds.Tables(0).DefaultView
        viewbono6.Sort = "asociado DESC"
        Dim drv As DataRowView
        Dim idasociado As Integer = 0
        Dim cantidad1 As Integer = 0
        Dim cantidad2 As Integer = 0

        For Each drv In viewbono6

            If drv("asociado") <> idasociado Then

                '  If idasociado > raiz Then insertabono6(idasociado, cantidad1, cantidad2)

                idasociado = drv("asociado")
                cantidad1 = 0
                cantidad2 = 0

            End If
            If drv("paquete") > 1 Then
                cantidad2 += drv("cantidad")
            Else
                cantidad1 += drv("cantidad")
            End If
        Next
        'If idasociado > raiz Then insertabono6(idasociado, cantidad1, cantidad2)



    End Sub
    Sub buscaantepasadomayoratresanterior(ByVal asociado As Integer)

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT orden, patrocinador FROM asociados WHERE id=" & asociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If dtrTeam(0) > 2 Then
                antepasado = dtrTeam(1)

                Exit While
            Else
                buscaantepasadomayoratres(dtrTeam(1))
            End If
        End While

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub bono7anterior()
        buscavaloresbono7()

        estado += "Inicia bono7. "
        'borra tabla temporal bono 6
        borrabono7()
        'creamos tabla para agregarle las compras a cada asociado

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT patrocinador, compras1, compras2 FROM bono6 WHERE patrocinador>0 ORDER BY patrocinador DESC"

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader




        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim idasociado As Integer = 0
        Dim cantidad1, cantidad2 As Integer
        While dtrTeam.Read
            If dtrTeam("patrocinador") <> idasociado Then
                If idasociado >= raiz Then insertabono7(idasociado, cantidad1, cantidad2)

                idasociado = dtrTeam("patrocinador")
                cantidad1 = 0
                cantidad2 = 0

            End If

            cantidad2 += dtrTeam("compras2")

            cantidad1 += dtrTeam("compras1")

        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If idasociado >= raiz Then insertabono7(idasociado, cantidad1, cantidad2)

        estado += "Termina bono7. "

    End Sub

    Sub bono9()

        estado += "Inicia bono9. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT compras.asociado, compras.paquete, asociados.patrocinador  FROM compras INNER JOIN asociados ON compras.asociado=asociados.id WHERE compras.fecha<='" & CDate(a.Text).ToString("yyyy/MM/dd") & "' AND compras.fecha>='" & CDate(de.Text).ToString("yyyy/MM/dd") & "' AND compras.statuspago='PAGADO'  ORDER BY compras.asociado, compras.paquete;"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim compras2 As Integer = 0
        Dim compras3 As Integer = 0
        Dim id As Integer = 0
        Dim patrocinador As Integer = 0
        While dtrTeam.Read

            If dtrTeam(0) <> id And id > 0 And (compras2 > 0 Or compras3 > 0) Then
                insertabono9(patrocinador, compras2, compras3)
                compras2 = 0
                compras3 = 0
                id = 0
            End If
            If dtrTeam(1) = 0 Then
                'primera condición para bono
                id = dtrTeam(0)
                patrocinador = dtrTeam(2)
            Else
                If id > 0 Then
                    If dtrTeam(1) = 2 Then compras2 += 1
                    If dtrTeam(1) = 3 Then compras3 += 1

                End If
            End If





        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If id >= raiz Then insertabono9(patrocinador, compras2, compras3)
        estado += "Termina bono9. "
    End Sub
    Sub insertabono9(ByVal asociado As Integer, ByVal compras2 As Integer, ByVal compras3 As Integer)
        If Not estaactivo(asociado) Then
            Exit Sub
        End If
        Dim factura As Integer = 0
        If funciones.facturo(asociado) Then
            factura = 1
        End If
        Dim mispuntos As Integer = mispuntosdelmes(asociado)
        If mispuntos < 700 Then
            Exit Sub
        End If
        Dim montobono2a As Integer = 50
        Dim montobono2b As Integer = 100

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos, factura) VALUES(" & asociado.ToString & ", '" & CDate(de.Text).ToString("yyyy/MM/dd") & "', '" & CDate(a.Text).ToString("yyyy/MM/dd") & "', 9, " & ((compras2 * montobono2a) + (compras3 * montobono2b)).ToString & ", " & idperiodo.ToString & ",0, " & mispuntos.ToString & ", " & factura.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub bono10()
        estado += "Inicia bono10. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT bono10rango5, bono10rango6, bono10rango7, bono10rango8, bono10rango9  FROM configuracion"

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim rango5, rango6, rango7, rango8, rango9 As Decimal
        While dtrTeam.Read
            rango5 = dtrTeam(0)
            rango6 = dtrTeam(1)
            rango7 = dtrTeam(2)
            rango8 = dtrTeam(3)
            rango9 = dtrTeam(4)

        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If rango5 > 0 Then calculabono10(5, rango5)
        If rango6 > 0 Then calculabono10(6, rango6)
        If rango7 > 0 Then calculabono10(7, rango7)
        If rango8 > 0 Then calculabono10(8, rango8)
        If rango9 > 0 Then calculabono10(9, rango9)
        estado += "Termina bono10. "
    End Sub
    Sub calculabono10(ByVal rango As Integer, ByVal porcentaje As Decimal)
        porcentaje = porcentaje / 100
        Dim drv As DataRowView
        Dim inicio As Date = CDate(Me.de.Text)
        Dim fin As Date = CDate(Me.a.Text)
        viewasociados.RowFilter = " rangopago=" & rango
        For Each drv In viewasociados

            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = ""
            strTeamQuery = "SELECT SUM( puntosasociados.puntos ) , puntosasociados.lado "
            strTeamQuery += "FROM(puntosasociados) INNER JOIN compras ON puntosasociados.compra = compras.id "
            strTeamQuery += "WHERE(puntosasociados.asociado = " & drv("id").ToString & ") AND compras.fecha >= '" & inicio.Year.ToString & "/" & inicio.Month.ToString & "/" & inicio.Day.ToString & "' AND  compras.fecha<='" & fin.Year.ToString & "/" & fin.Month.ToString & "/" & fin.Day.ToString & "' "
            strTeamQuery += "GROUP BY puntosasociados.lado"
            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()
            Dim izq, der As Integer
            izq = 0
            der = 0
            While dtrTeam.Read
                If dtrTeam(1) = "I" Then
                    izq = dtrTeam(0)
                Else
                    der = dtrTeam(0)
                End If
            End While

            sqlConn.Close()
            sqlConn.Dispose()

            If miladomayor(drv("id")) = "I" Then
                insertabono10(drv("id"), izq * porcentaje)
            Else
                insertabono10(drv("id"), der * porcentaje)
            End If


        Next
    End Sub
    Sub insertabono10(ByVal asociado As Integer, ByVal monto As Decimal)
        Dim factura As Integer = 0
        If funciones.facturo(asociado) Then
            factura = 1
        End If


        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, factura) VALUES(" & asociado.ToString & ", '" & CDate(de.Text).ToString("yyyy/MM/dd") & "', '" & CDate(a.Text).ToString("yyyy/MM/dd") & "', 10, " & monto.ToString & ", " & idperiodo.ToString & ",0 , " & factura.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub



#End Region
   
  

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
End Class
