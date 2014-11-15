Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient

Public Class Ciclo
    Private IDValue As Integer
    Private InicioValue As Date
    Private FinalValue As Date


#Region "Propiedades"
    Public Property ID() As Integer
        Get
            ID = IDValue
        End Get
        Set(ByVal value As Integer)
            IDValue = value
        End Set
    End Property

    Public Property Inicio() As Date
        Get
            Inicio = InicioValue
        End Get
        Set(ByVal value As Date)
            InicioValue = value
        End Set
    End Property
    Public Property Final() As Date
        Get
            Final = FinalValue
        End Get
        Set(ByVal value As Date)
            FinalValue = value
        End Set
    End Property
   
#End Region
#Region "Métodos"

    Public Sub New(ByVal Ciclo As Integer)
        MyBase.New()
        llenadatos(Ciclo)
    End Sub
    Public Sub New()
        MyBase.New()
    End Sub
    Private Sub llenadatos(ByVal Ciclo As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT inicio, final FROM ciclos WHERE id=" & Ciclo.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        ID = Ciclo
        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cont As Integer = 0
        While dtrTeam.Read

            If Not IsDBNull(dtrTeam(0)) Then Inicio = dtrTeam(0)
            If Not IsDBNull(dtrTeam(1)) Then Final = dtrTeam(1)
        End While

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub

    Sub guardanuevo()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()
        strTeamQuery = "INSERT INTO ciclos(inicio, final) VALUES('" & Inicio.ToString("yyyy/MM/dd") & "' , '" & Final.ToString("yyyy/MM/dd") & "')"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()

        Dim dtrTeam As MySqlDataReader
        strTeamQuery = "SELECT MAX(id) FROM ciclos"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read

            ID = dtrTeam(0)

        End While

        sqlConn.Close()
        sqlConn.Dispose()

    End Sub
    Sub actualiza()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()
        strTeamQuery = "UPDATE ciclos SET inicio='" & Inicio.ToString("yyyy/MM/dd") & "', final='" & Final.ToString("yyyy/MM/dd") & "' WHERE id=" & ID.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub

    Sub elimina()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()
        strTeamQuery = "DELETE FROM ciclos  WHERE id=" & ID.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()

    End Sub
   


#End Region
#Region "Funciones"
    Function terminaciclo() As Boolean

        Dim respuesta As Boolean = False

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT inicio, fin, id  FROM ciclos WHERE fin>='" & Inicio.ToString("yyyy/MM/dd") & "' AND  fin<='" & Final.ToString("yyyy/MM/dd") & "'"

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            respuesta = True
            Inicio = dtrTeam(0)

            Final = dtrTeam(1)
            ID = dtrTeam(2)
        End While

        sqlConn.Close()
        sqlConn.Dispose()


        Return respuesta
    End Function


    Sub mueverangos()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, rango  FROM asociados WHERE status=1"

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            checarango(dtrTeam(0), dtrTeam(1))
        End While

        sqlConn.Close()
        sqlConn.Dispose()

    End Sub
    Sub checarango(ByVal idasociado As Integer, ByVal rango As Integer)

        Dim de As Date = Inicio
        'Nueva qry, agregando el pago hasta el lunes siguiente del fin de ciclo
        Dim qry_paquetesypuntos As String = "SELECT comprasdetalle.cantidad, comprasdetalle.paquete, asociados.recorrido, asociados.ladosrecorrido " & _
        "FROM asociados INNER JOIN compras ON asociados.id=compras.asociado INNER JOIN `comprasdetalle` ON compras.id=comprasdetalle.compra " & _
        "WHERE asociados.recorrido LIKE '%." & idasociado.ToString & ".%' AND comprasdetalle.paquete BETWEEN 1 AND 3 AND compras.statuspago='PAGADO' AND compras.fecha>='" & Inicio.ToString("yyyy/MM/dd") & "' AND (compras.fecha<='" & Final.ToString("yyyy/MM/dd") & "' OR (compras.fechaorden BETWEEN '" & de.ToString("yyyy/MM/dd") & "' AND '" & Final.ToString("yyyy/MM/dd") & "' AND (compras.fecha='" & DateAdd(DateInterval.Day, 1, Final).ToString("yyyy/MM/dd") & "' OR compras.fecha='" & DateAdd(DateInterval.Day, 2, Final).ToString("yyyy/MM/dd") & "' OR compras.fecha='" & DateAdd(DateInterval.Day, 3, Final) & "'  ))) " & _
        "AND compras.id NOT IN (" & _
                      "Select id FROM compras " & _
                      "WHERE fechaorden < '" & Inicio.ToString("yyyy/MM/dd") & "' " & _
                      "AND fecha < '" & DateAdd(DateInterval.Day, 2, de).ToString("yyyy/MM/dd") & "' " & _
                      ") "


        Dim qry_directosporlado As String = "SELECT Count(asociados.ID) AS directos, asociados.ladopatrocinador AS lado " & _
                                                   "FROM(asociados) " & _
                                                   "WHERE(((asociados.patrocinador)=" & idasociado.ToString & ") AND (asociados.status=1  OR '" & Final.ToString("yyyy/MM/dd") & "' BETWEEN inicioactivacion AND finactivacion)  ) AND asociados.ptsmes>350 " & _
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
            strTeamQuery = "INSERT INTO rangoscambios (ciclo, asociado, rango, rangopago) VALUES (" & ID.ToString & "," & asociado.ToString & "," & rango.ToString & "," & rango.ToString & ")"

        Else
            strTeamQuery = "INSERT INTO rangoscambios (ciclo, asociado, rango, rangopago) VALUES (" & ID.ToString & "," & asociado.ToString & "," & rangotitulo.ToString & "," & rango.ToString & ")"

        End If
        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()

    End Sub


#End Region




End Class
