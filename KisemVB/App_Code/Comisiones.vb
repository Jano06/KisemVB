Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.IO
Imports System.Web
Imports System.Configuration

Public Class Comisiones
    Dim view As New DataView()
    Dim viewpuntos As New DataView()
    Dim viewasociados As New DataView()
    Dim pago1bono6, pago2bono6, pago1bono7, pago2bono7, pago1bono8, pago2bono8, pagominimobono678, PagoNivel1Emprendedor, PagoNivel2Emprendedor, PagoNivel3Emprendedor, PagoNivel1Empresario, PagoNivel2Empresario, PagoNivel3Empresario As Decimal
    Dim raiz As Integer = 3
    Private IDValue As Integer
    Private InicioValue As Date
    Private FinalValue As Date
    Private StatusValue As Integer
    Dim funciones As New funciones
    Dim asociados, asociadosactivoseinactivos As New List(Of Integer)

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
    Public Property Status() As Integer
        Get
            Status = StatusValue
        End Get
        Set(ByVal value As Integer)
            StatusValue = value
        End Set
    End Property
#End Region

#Region "Métodos"

    Public Sub New(ByVal Periodo As Integer)
        MyBase.New()
        llenadatos(Periodo)
    End Sub
    Public Sub New()
        MyBase.New()
    End Sub
    Private Sub llenadatos(ByVal Periodo As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT inicio, final, status FROM periodos WHERE id=" & Periodo.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        ID = Periodo
        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cont As Integer = 0
        While dtrTeam.Read

            If Not IsDBNull(dtrTeam(0)) Then Inicio = dtrTeam(0)
            If Not IsDBNull(dtrTeam(1)) Then Final = dtrTeam(1)
            If Not IsDBNull(dtrTeam(2)) Then Status = dtrTeam(2)
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
        strTeamQuery = "INSERT INTO periodos(id, inicio, final, status) VALUES(" & ID.ToString & ", '" & Inicio.ToString("yyyy/MM/dd") & "' , '" & Final.ToString("yyyy/MM/dd") & "', " & Status.ToString & ")"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()

        
        eliminainactivosde4meses()
        flush()
        llenavistadeasociados()
        Dim cicloFinalizado As New Ciclo
        If cicloFinalizado.terminaciclo Then
            cicloFinalizado.mueverangos()
        End If
        borracomisionesparciales()
        buscaasociadosactivos()


        bono1()
        bono2()

        bono4()
        bono5()
        bono6()
        bono7()
        bono8()
        bonoNiveles()
        insertadetallesbono4nopagados()

        abanderacomisiones()
     

    End Sub
    Sub actualiza()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()
        strTeamQuery = "UPDATE periodos SET inicio='" & Inicio.ToString("yyyy/MM/dd") & "', final='" & Final.ToString("yyyy/MM/dd") & "', status= " & Status.ToString & " WHERE id=" & ID.ToString

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
        strTeamQuery = "DELETE FROM periodos  WHERE id=" & ID.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()

    End Sub
    Sub eliminainactivosde4meses()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim fechadesactivacion As Date = DateAdd(DateInterval.Month, -4, Date.Today)
        Dim strTeamQuery As String = "UPDATE asociados SET status=2 WHERE finactivacion<'" & fechadesactivacion.ToString("yyyy/MM/dd") & "'"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()
        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub flush()
        'elimina puntos de asociados inactivos desde el viernes pasado
        '1 calcula fecha de viernes anterior
        Dim fechaflush As Date = DateAdd(DateInterval.Day, -7, Inicio)

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
#Region "Bonos"
    Sub bono1()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""

        'para incluir el lunes
        Dim fechafinalperiodo As Date = Inicio
        fechafinalperiodo = DateAdd(DateInterval.Day, 3, fechafinalperiodo)
        Dim fechafinalperiodoanterior As Date = DateAdd(DateInterval.Day, -1, Final)
        Dim fechalunesanterior As Date = DateAdd(DateInterval.Day, 2, Final)
        strTeamQuery = " SELECT Count( compras.ID ) AS CountOfID, compras.asociado " & _
                      "FROM(compras) INNER JOIN comprasdetalle ON compras.id = comprasdetalle.compra " & _
                      "WHERE(comprasdetalle.paquete = 3) AND compras.excedente=1 " & _
                      "AND compras.fecha >= '" & Inicio.ToString("yyyy/MM/dd") & "' " & _
                      "AND compras.fecha <= '" & DateAdd(DateInterval.Day, 3, Final).ToString("yyyy/MM/dd") & "' " & _
                      "AND compras.statuspago = 'PAGADO' " & _
                      "AND compras.fechaorden <= '" & Final.ToString("yyyy/MM/dd") & "' " & _
                      "AND compras.id NOT IN (" & _
                      "Select id FROM compras " & _
                      "WHERE fechaorden <= '" & DateAdd(DateInterval.Day, -1, Inicio).ToString("yyyy/MM/dd") & "' " & _
                      "AND fecha >= '" & Inicio.ToString("yyyy/MM/dd") & "' " & _
                      "AND fecha <= '" & DateAdd(DateInterval.Day, 3, Inicio).ToString("yyyy/MM/dd") & "' " & _
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
    End Sub
    Sub bono2()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT asociados.id, asociados.orden, asociados.patrocinador, compras.puntos  FROM asociados INNER JOIN compras ON asociados.id=compras.asociado WHERE compras.fecha<='" & Final.ToString("yyyy/MM/dd") & "' AND compras.fecha>='" & Inicio.ToString("yyyy/MM/dd") & "'  AND compras.inscripcion>0  ORDER BY patrocinador DESC;"

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
    End Sub

    Sub bono3()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, bono6  " & _
                        "FROM asociados   " & _
                        "WHERE Orden<3 AND  FInsc<='" & Final.ToString("yyyy/MM/dd") & "' AND FInsc>='" & Inicio.ToString("yyyy/MM/dd") & "' " & _
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
    End Sub
    Sub bono4()
        llenavistadeporcentajes()
        llenavistadepuntos()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        strTeamQuery = "SELECT puntosasociados.Asociado , sum( if( `Lado` = 'D', `PorPagar` , 0 ) ) AS D, sum( if( `Lado` = 'I', `PorPagar` , 0 ) ) AS I "
        strTeamQuery += "FROM `puntosasociados`  INNER JOIN compras ON puntosasociados.compra=compras.id "
        strTeamQuery += "WHERE  ((compras.fecha<='" & Final.ToString("yyyy/MM/dd") & "') OR(compras.fecha='" & DateAdd(DateInterval.Day, 3, Final).ToString("yyyy/MM/dd") & "' AND compras.fechaorden='" & Final.ToString("yyyy/MM/dd") & "')) AND compras.statuspago='PAGADO' "
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





    End Sub

    Sub bono5()
        Dim porcentajebono5 As Decimal = recuperaporcentajebono5()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT asociados.patrocinador, pagos.asociado, pagos.bono, pagos.monto, pagos.de, pagos.a " & _
                                        "FROM asociados INNER JOIN pagos ON asociados.ID = pagos.asociado " & _
                                        "WHERE  pagos.de>='" & Inicio.ToString("yyyy/MM/dd") & "' AND pagos.a<='" & Final.ToString("yyyy/MM/dd") & "' AND pagos.bono=4 " & _
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
    End Sub

    Sub bono6(Optional ByVal asociadobono As Integer = 0)
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
        Dim fechafinalperiodo As Date = Final
        fechafinalperiodo = DateAdd(DateInterval.Day, 3, fechafinalperiodo)
        Dim fechafinalperiodoanterior As Date = DateAdd(DateInterval.Day, -1, Inicio)
        Dim fechalunesanterior As Date = DateAdd(DateInterval.Day, 2, Inicio)
        If asociadobono = 0 Then
            strTeamQuery = " SELECT asociados.ID AS Asociado, asociados.Orden AS orden, asociados.patrocinador AS patrocinador, compras.ID AS Compra, compras.fecha AS fecha, 1 AS cantidad, compras.puntos AS paquete, asociados.bono6 AS bono6, asociados.ptsmes  " & _
                    "FROM asociados INNER JOIN compras ON asociados.ID = compras.asociado  " & _
                    "WHERE  compras.puntos>=350 AND compras.excedente=0  AND compras.statuspago='PAGADO' AND (compras.inscripcion=0 OR compras.puntos=350)" & _
                    "AND compras.fechaorden <= '2014/08/15' " & _
                    "AND ((compras.fecha >= '" & Inicio.ToString("yyyy/MM/dd") & "' " & _
                    "AND compras.fecha <= '" & DateAdd(DateInterval.Day, -3, fechafinalperiodo).ToString("yyyy/MM/dd") & "') " & _
                    "OR(compras.fecha = '" & fechafinalperiodo.ToString("yyyy/MM/dd") & "') AND compras.fechaorden = '" & DateAdd(DateInterval.Day, -3, fechafinalperiodo).ToString("yyyy/MM/dd") & "')" & _
                    "AND compras.id NOT IN (" & _
                    "Select id FROM compras " & _
                    "WHERE fechaorden = '" & fechafinalperiodoanterior.ToString("yyyy/MM/dd") & "' " & _
                    "AND fecha = '" & fechalunesanterior.ToString("yyyy/MM/dd") & "' " & _
                    ") " & _
                    "ORDER BY compras.ID DESC;"
        Else
            strTeamQuery = " SELECT asociados.ID AS Asociado, asociados.Orden AS orden, asociados.patrocinador AS patrocinador, compras.ID AS Compra, compras.fecha AS fecha, 1 AS cantidad, compras.puntos AS paquete, asociados.bono6 AS bono6, asociados.ptsmes  " & _
                    "FROM asociados INNER JOIN compras ON asociados.ID = compras.asociado  " & _
                    "WHERE asociados.bono6=" & asociadobono.ToString & " AND compras.puntos>=350 AND compras.excedente=0  AND compras.statuspago='PAGADO' AND (compras.inscripcion=0 OR compras.puntos=350)" & _
                    "AND compras.fechaorden <= '2014/08/15' " & _
                    "AND ((compras.fecha >= '" & Inicio.ToString("yyyy/MM/dd") & "' " & _
                    "AND compras.fecha <= '" & DateAdd(DateInterval.Day, -3, fechafinalperiodo).ToString("yyyy/MM/dd") & "') " & _
                    "OR(compras.fecha = '" & fechafinalperiodo.ToString("yyyy/MM/dd") & "') AND compras.fechaorden = '" & DateAdd(DateInterval.Day, -3, fechafinalperiodo).ToString("yyyy/MM/dd") & "')" & _
                    "AND compras.id NOT IN (" & _
                    "Select id FROM compras " & _
                    "WHERE fechaorden = '" & fechafinalperiodoanterior.ToString("yyyy/MM/dd") & "' " & _
                    "AND fecha = '" & fechalunesanterior.ToString("yyyy/MM/dd") & "' " & _
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

    End Sub
    Sub bono7()
        buscavaloresbono7()

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


    End Sub

    Sub bono8()
        buscavaloresbono8()
    
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT patrocinador, SUM(compras1), SUM(compras2) FROM bono7 GROUP BY patrocinador"

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader




        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim idasociado As Integer = 0
        Dim cantidad1, cantidad2 As Integer
        While dtrTeam.Read
            'If dtrTeam("patrocinador") <> idasociado Then
            'If idasociado >= raiz Then insertabono8(idasociado, cantidad1, cantidad2)

            'idasociado = dtrTeam("patrocinador")
            'cantidad1 = 0
            'cantidad2 = 0

            'End If

            'cantidad2 += dtrTeam(2)

            'cantidad1 += dtrTeam(1)
            If dtrTeam(0) >= raiz Then insertabono8(dtrTeam(0), dtrTeam(1), dtrTeam(2))
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        ' If idasociado >= raiz Then insertabono8(idasociado, cantidad1, cantidad2)

    End Sub
    Sub bonoNiveles()
        borraTablaBono11()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        buscavaloresbononiveles()

        Dim fechafinalperiodo As Date = Final
        fechafinalperiodo = DateAdd(DateInterval.Day, 3, fechafinalperiodo)
        Dim fechafinalperiodoanterior As Date = DateAdd(DateInterval.Day, -1, Inicio)
        Dim fechalunesanterior As Date = DateAdd(DateInterval.Day, 2, Inicio)
        strTeamQuery = " SELECT compras.asociado, comprasdetalle.cantidad, comprasdetalle.paquete " & _
                    "FROM compras INNER JOIN comprasdetalle ON compras.ID = comprasdetalle.compra " & _
                    "WHERE  comprasdetalle.paquete>1 AND compras.excedente=0  AND compras.statuspago='PAGADO'  " & _
                    "AND compras.fechaorden > '2014/08/15' " & _
                    "AND ((compras.fecha >= '" & Inicio.ToString("yyyy/MM/dd") & "' " & _
                    "AND compras.fecha <= '" & DateAdd(DateInterval.Day, -3, fechafinalperiodo).ToString("yyyy/MM/dd") & "') " & _
                    "OR(compras.fecha = '" & fechafinalperiodo.ToString("yyyy/MM/dd") & "') AND compras.fechaorden = '" & DateAdd(DateInterval.Day, -3, fechafinalperiodo).ToString("yyyy/MM/dd") & "')" & _
                    "AND compras.id NOT IN (" & _
                    "Select id FROM compras " & _
                    "WHERE fechaorden = '" & fechafinalperiodoanterior.ToString("yyyy/MM/dd") & "' " & _
                    "AND fecha = '" & fechalunesanterior.ToString("yyyy/MM/dd") & "' " & _
                    ") " & _
                    "ORDER BY compras.ID ASC;"



        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cuenta As Integer = 0
        While dtrTeam.Read
            ProcesaBonoNiveles(dtrTeam(0), dtrTeam(2), dtrTeam(1), 0)


        End While

        sqlConn.Close()
        sqlConn.Dispose()
        sumaBono11()
    End Sub


#Region "Inserta Bonos"
    Sub sumaBono11()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader

        strTeamQuery = "SELECT SUM(monto), SUM(retencionisr), SUM(iva), SUM(retencioniva), asociado, sum(factura) FROM bono11  GROUP BY asociado "

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim bono As Integer = 0

        While dtrTeam.Read

            Dim factura As Integer = 0
            If dtrTeam(5) > 0 Then factura = 1
            strTeamQuery = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos, factura, retencionisr, iva, retencioniva) VALUES(" & dtrTeam(4).ToString & ", '" & Inicio.ToString("yyyy/MM/dd") & "', '" & Final.ToString("yyyy/MM/dd") & "', 11, " & dtrTeam(0) & ", " & ID.ToString & ",0, 0, " & factura.ToString & ", " & dtrTeam(1).ToString & ", " & dtrTeam(2).ToString & ", " & dtrTeam(3).ToString & ")"
            Dim sqlConn2 As MySqlConnection = New MySqlConnection(strConnection)
            Dim cmdFetchTeam2 As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn2)



            sqlConn2.Open()



            cmdFetchTeam2.ExecuteNonQuery()


            sqlConn2.Close()
            sqlConn2.Dispose()


        End While

        sqlConn.Close()
        sqlConn.Dispose()
        
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
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos, factura, retencionisr, iva, retencioniva) VALUES(" & asociado.ToString & ", '" & Inicio.ToString("yyyy/MM/dd") & "', '" & Final.ToString("yyyy/MM/dd") & "', 1, " & (cuenta * montobono1).ToString & ", " & ID.ToString & ",0, " & mispuntos.ToString & ", " & factura.ToString & ", " & retencionisr.ToString & ", " & iva.ToString & ", " & retencioniva.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
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
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos, factura, retencionisr, iva, retencioniva) VALUES(" & asociado.ToString & ", '" & Inicio.ToString("yyyy/MM/dd") & "', '" & Final.ToString("yyyy/MM/dd") & "', 2, " & ((bonoa * montobono2a) + (bonob * montobono2b)).ToString & ", " & ID.ToString & ",0, " & mispuntos.ToString & ", " & factura.ToString & ", " & retencionisr.ToString & ", " & iva.ToString & ", " & retencioniva.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
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
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos, factura, retencionisr, iva, retencioniva) VALUES(" & asociado.ToString & ", '" & Inicio.ToString("yyyy/MM/dd") & "', '" & Final.ToString("yyyy/MM/dd") & "', 3, " & (cantidad * montobono3).ToString & ", " & ID.ToString & ",0, " & mispuntos.ToString & ", " & factura.ToString & ", " & retencionisr.ToString & ", " & iva.ToString & ", " & retencioniva.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub insertadetallebono4(ByVal asociado As Integer, ByVal corte As Integer, ByVal puntosfinalesi As Integer, ByVal puntosfinalesd As Integer, ByVal porcentajedepago As Decimal)
        'sacamos los puntos de la semana
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim nuevosi, nuevosd As Integer
        Dim dtrTeam As MySqlDataReader
        Dim hasta As Date = Final
        Dim de As Date = Inicio

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


            strTeamQuery = "SELECT SUM(puntosasociados.porpagar), puntosasociados.lado FROM puntosasociados INNER JOIN compras ON puntosasociados.compra=compras.id WHERE puntosasociados.asociado=" & asociado.ToString & " AND (compras.fecha<='" & DateAdd(DateInterval.Day, -1, Inicio).ToString("yyyy/MM/dd") & "' OR (compras.fecha='" & DateAdd(DateInterval.Day, 2, Inicio).ToString("yyyy/MM/dd") & "' AND compras.fechaorden='" & DateAdd(DateInterval.Day, -1, Inicio).ToString("yyyy/MM/dd") & "')) GROUP BY puntosasociados.lado "

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




        Dim fin As Date = DateAdd(DateInterval.Day, 3, Final)

        'recoge % pago asociado
        If Not estaactivo(asociado) Then
            Exit Sub
        End If
        insertadetallebono4(asociado, ID, izquierdo, derecho, porcentaje)
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
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos, factura, retencionisr, iva, retencioniva) VALUES(" & asociado.ToString & ", '" & Inicio.ToString("yyyy/MM/dd") & "', '" & Final.ToString("yyyy/MM/dd") & "', 4, " & (puntos * porcentaje).ToString & ", " & ID.ToString & ",0, " & mispuntos.ToString & ", " & factura.ToString & ", " & retencionisr.ToString & ", " & iva.ToString & ", " & retencioniva.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
        fin = Final
        strTeamQuery = "UPDATE puntosasociados  INNER JOIN compras ON puntosasociados.compra=compras.id SET  puntosasociados.porpagaranterior=puntosasociados.porpagar, corte=" & ID.ToString & ", puntosasociados.status=1, puntosasociados.porpagar=0 WHERE puntosasociados.lado='" & lado & "' AND puntosasociados.asociado=" & asociado.ToString & "  AND ((compras.fecha<='" & fin.ToString("yyyy/MM/dd") & "') OR(compras.fecha='" & DateAdd(DateInterval.Day, 3, fin).ToString("yyyy/MM/dd") & "' AND compras.fechaorden='" & fin.ToString("yyyy/MM/dd") & "'))  "


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
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos, factura, retencionisr, iva, retencioniva) VALUES(" & asociado.ToString & ", '" & Inicio.ToString("yyyy/MM/dd") & "', '" & Final.ToString("yyyy/MM/dd") & "', 5, " & cantidad.ToString & ", " & ID.ToString & ",0, " & mispuntos.ToString & ", " & factura.ToString & ", " & retencionisr.ToString & ", " & iva.ToString & ", " & retencioniva.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub insertabono6(ByVal asociado As Integer, ByVal cantidad1 As Integer, ByVal cantidad2 As Integer, ByVal puntospersonales As Integer)
        Dim factura As Integer = 0
        If funciones.facturo(asociado) Then
            factura = 1
        End If

        Dim montobono As Decimal = 0
        puntospersonales = mispuntosdelmes(asociado)
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
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos, factura, retencionisr, iva, retencioniva) VALUES(" & asociado.ToString & ", '" & Inicio.ToString("yyyy/MM/dd") & "', '" & Final.ToString("yyyy/MM/dd") & "', 6, " & montobono.ToString & ", " & ID.ToString & ",0, " & mispuntos.ToString & ", " & factura.ToString & ", " & retencionisr.ToString & ", " & iva.ToString & ", " & retencioniva.ToString & ")"
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
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos, factura, retencionisr, iva, retencioniva) VALUES(" & asociado.ToString & ", '" & Inicio.ToString("yyyy/MM/dd") & "', '" & Final.ToString("yyyy/MM/dd") & "', 7, " & monto.ToString & ", " & ID.ToString & ",0, " & mispuntosdelm.ToString & ", " & factura.ToString & ", " & retencionisr.ToString & ", " & iva.ToString & ", " & retencioniva.ToString & ")"
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
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, corte, status, mispuntos, factura, retencionisr, iva, retencioniva) VALUES(" & asociado.ToString & ", '" & Inicio.ToString("yyyy/MM/dd") & "', '" & Final.ToString("yyyy/MM/dd") & "', 8, " & monto & ", " & ID.ToString & ",0, " & mispuntos.ToString & ", " & factura.ToString & ", " & retencionisr.ToString & ", " & iva.ToString & ", " & retencioniva.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()

    End Sub
    Sub ProcesaBonoNiveles(ByVal asociado As Integer, ByVal paquete As Integer, ByVal cantidad As Integer, ByVal nivel As Integer)

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT patrocinador, status FROM asociados WHERE id=" & asociado.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If dtrTeam(0) = 3 Then Exit While
            If dtrTeam(1) = 1 Then

                nivel += 1
                'inserta en bd
                insertaBonoNiveles(dtrTeam(0), cantidad, paquete, nivel)
            End If
            If nivel = 3 Then Exit While
            ProcesaBonoNiveles(dtrTeam(0), paquete, cantidad, nivel)
        End While

        sqlConn.Close()
        sqlConn.Dispose()

    End Sub
    Sub insertaBonoNiveles(ByVal asociado As Integer, ByVal cantidad As Integer, ByVal paquete As Integer, ByVal nivel As Integer)
        Dim monto As Decimal = 0
        Select Case nivel
            Case 1
                Select Case paquete
                    Case 2
                        monto = cantidad * PagoNivel1Emprendedor
                    Case 3
                        monto = cantidad * PagoNivel1Empresario
                End Select
            Case 2
                Select Case paquete
                    Case 2
                        monto = cantidad * PagoNivel2Emprendedor
                    Case 3
                        monto = cantidad * PagoNivel2Empresario
                End Select
            Case 3
                Select Case paquete
                    Case 2
                        monto = cantidad * PagoNivel3Emprendedor
                    Case 3
                        monto = cantidad * PagoNivel3Empresario
                End Select

        End Select
        'inserta pago en bd
        Dim factura As Integer = 0
        If funciones.facturo(asociado) Then
            factura = 1
        End If
        ' busca sus puntos de asociado
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
        Dim strTeamQuery As String = "INSERT INTO bono11(asociado, de, a, bono, monto, corte, status, mispuntos, factura, retencionisr, iva, retencioniva) VALUES(" & asociado.ToString & ", '" & Inicio.ToString("yyyy/MM/dd") & "', '" & Final.ToString("yyyy/MM/dd") & "', 11, " & monto & ", " & ID.ToString & ",0, 0, " & factura.ToString & ", " & retencionisr.ToString & ", " & iva.ToString & ", " & retencioniva.ToString & ")"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
#End Region

#Region "Auxiliares"
    Sub abanderacomisiones()

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
        strTeamQuery = "UPDATE periodos SET status=2 WHERE id=" & ID.ToString
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()

    End Sub
    Sub insertadetallesbono4nopagados()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        Dim inicio As Date = inicio
        Dim fin As Date = Final

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
                        insertadetallebono4(asociadosactivoseinactivos(i), ID, dtrTeam(1), dtrTeam(2), 0)

                    Else
                        insertadetallebono4(asociadosactivoseinactivos(i), ID, dtrTeam(2), dtrTeam(1), 0)
                    End If


                End While

                sqlConn.Close()
                sqlConn.Dispose()







            End If
        Next





    End Sub
    Sub defineperiodo()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT MAX(id) FROM periodos WHERE status>0"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then ID = dtrTeam(0)


        End While

        sqlConn.Close()


    End Sub
  
    Sub borracomisionesparciales()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "DELETE FROM pagos WHERE status=0"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
        strTeamQuery = "DELETE FROM periodos WHERE status=0 AND id<>" & ID.ToString
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()

    End Sub
    Sub borraTablaBono11()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "DELETE FROM bono11 "
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


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
    Sub actualizastatuspuntos(ByVal idPuntos As Integer, ByVal status As Integer, Optional ByVal puntos As Integer = 0)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "UPDATE puntosasociados SET porpagaranterior=porpagar, corte=" & ID.ToString & ", status=" & status.ToString & ", porpagar=" & puntos.ToString & " WHERE id=" & idPuntos.ToString
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
    Sub buscavaloresbononiveles()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT PagoNivel1Emprendedor, PagoNivel2Emprendedor, PagoNivel3Emprendedor, PagoNivel1Empresario, PagoNivel2Empresario, PagoNivel3Empresario " & _
                                        "FROM configuracion "

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            PagoNivel1Emprendedor = dtrTeam(0)
            PagoNivel2Emprendedor = dtrTeam(1)
            PagoNivel3Emprendedor = dtrTeam(2)
            PagoNivel1Empresario = dtrTeam(3)
            PagoNivel2Empresario = dtrTeam(4)
            PagoNivel3Empresario = dtrTeam(5)


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
    Sub buscaasociadosactivos()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        'Dim strTeamQuery As String = "SELECT id, rango, ptsmes FROM asociados WHERE status=1  OR '" & a.Text & "' BETWEEN inicioactivacion AND finactivacion  ORDER BY id DESC"
        'nuevo para evitar que se activen el lunes
        Dim strTeamQuery As String = "SELECT id, rango, ptsmes FROM asociados WHERE status=1  OR '" & Final.ToString("yyyy/MM/dd") & "' BETWEEN inicioactivacion AND finactivacion  ORDER BY id DESC"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            asociados.Add(dtrTeam("id"))




        End While

        sqlConn.Close()
        sqlConn.Dispose()

    End Sub
    Function mispuntosdelmes(ByVal asociado As Integer) As Integer
        Dim drv As DataRowView
        viewasociados.RowFilter = "id=" & asociado.ToString
        For Each drv In viewasociados
            Return drv("ptsmes")

        Next
    End Function
    Sub llenavistadeasociados()
        'estado = "Inicia Llena vista de asociados"
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, patrocinador, orden, ptsmes, bono6, rango, rangopago FROM asociados WHERE status=1 OR '" & Final.ToString("yyyy/MM/dd") & "' BETWEEN inicioactivacion AND finactivacion "



        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        Dim objAdapter As New MySqlDataAdapter
        objAdapter.SelectCommand = cmdFetchTeam

        sqlConn.Open()
        Dim objDS As New DataSet
        objAdapter.Fill(objDS, "paquetes")

        sqlConn.Close()
        sqlConn.Dispose()
        viewasociados = New DataView(objDS.Tables(0))

    End Sub
#End Region
    
#End Region




#End Region

End Class
