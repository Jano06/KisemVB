Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class sw_comisiones
    Inherits System.Web.UI.Page
    Dim asociados As New List(Of Integer)
    Dim view As New DataView()
    Dim viewpuntos As New DataView()
    Dim viewasociados As New DataView()
    Dim antepasado As Integer = 0
    Dim raiz As Integer
    Dim estado As String = ""
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Button1.Enabled = False
        'Me.Image1.Visible = True
        ' cuentaasociados()
        Try
            estado = "Borra comisiones sin terminar"
            borracomisionesparciales()
            estado = "Corre comisiones"
            correcomisiones()
            estado = "Abandera comisiones"
            abanderacomisiones()
        Catch ex As Exception
            Me.mensajes.Text = estado & ex.Message.ToString
            Me.mensajes.Visible = True
        End Try

        Me.mensajes.Text = "Comisiones generadas con éxito"
        Me.mensajes.Visible = True
        Me.Button1.Enabled = True
        'Me.Image1.Visible = False
    End Sub
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

    End Sub
    Sub borracomisionesparciales()
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
    End Sub
    Sub cuentaasociados()
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
    End Sub
    Sub buscaraiz()
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
    End Sub
    Sub correcomisiones()



        Try
            estado = "Busca raíz"
            buscaraiz()
            estado = "Llena vista de asociados"
            llenavistadeasociados()
            estado = "Conecta para sacar asociados activos"
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = "SELECT id, rango, ptsmes FROM asociados WHERE status=1 ORDER BY id DESC"
            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read
                asociados.Add(dtrTeam("id"))




            End While

            sqlConn.Close()
            sqlConn.Dispose()
            estado = "Inicia Bono 1"
            For i = 0 To asociados.Count - 1
                bono1(asociados(i))
            Next
            estado = "Inicia Bono 2"
            bono2()
            estado = "Inicia Bono 3"
            bono3()
            estado = "Inicia Bono 4"
            bono4()
            estado = "Inicia Bono 5"
            bono5()
            estado = "Inicia Bono 6"
            bono6()
            estado = "Inicia Bono 7"
            bono7()
            estado = "Inicia Bono 8"
            bono8()

        Catch ex As Exception
            Me.mensajes.Text = estado & ex.Message.ToString
            Me.mensajes.Visible = True
        End Try

    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
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
        Me.de.Text = de.ToString("yyyy/M/dd")
        Me.a.Text = a.ToString("yyyy/M/dd")
    End Sub
    Sub bono1(ByVal id As Integer)
        Dim montobono1 As Integer = 200
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT COUNT(id) FROM compras WHERE asociado=" & id.ToString & " AND paquete=4 AND activacion=0 AND fecha>='" & de.Text & "' AND fecha <='" & a.Text & "';"
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
            strTeamQuery = "INSERT INTO pagos(asociado, de, a, bono, monto, fechacorte, status) VALUES(" & id.ToString & ", '" & de.Text & "', '" & a.Text & "', 1, " & (cuenta * montobono1).ToString & ", '" & Today.Year & "/" & Today.Month & "/" & Today.Day & "',0)"

            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            cmdFetchTeam.ExecuteNonQuery()


            sqlConn.Close()
            sqlConn.Dispose()
        End If
    End Sub
    Sub bono2()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, orden, patrocinador FROM asociados WHERE FInsc<='" & a.Text & "' AND FInsc>='" & de.Text & "' ORDER BY patrocinador DESC;"
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
        If id > raiz Then insertabono2(id, bonoa, bonob)
    End Sub
    Sub insertabono2(ByVal asociado As Integer, ByVal bonoa As Integer, ByVal bonob As Integer)
        Dim montobono2a As Integer = 60
        Dim montobono2b As Integer = 120

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, fechacorte, status) VALUES(" & asociado.ToString & ", '" & de.Text & "', '" & a.Text & "', 2, " & ((bonoa * montobono2a) + (bonob * montobono2b)).ToString & ", '" & Today.Year & "/" & Today.Month & "/" & Today.Day & "',0)"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub bono3()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT asociados.ID AS padre, asociados.patrocinador AS abuelo, asociados_1.ID AS asociado, asociados_1.FInsc, asociados_1.Orden, asociados.Orden " & _
                        "FROM asociados INNER JOIN asociados AS asociados_1 ON asociados.ID = asociados_1.patrocinador " & _
                        "WHERE asociados_1.Orden<3 AND asociados.Orden>2 AND  asociados_1.FInsc<='" & a.Text & "' AND asociados_1.FInsc>='" & de.Text & "' " & _
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
        If id > raiz Then insertabono3(id, bono)
    End Sub
    Sub insertabono3(ByVal asociado As Integer, ByVal cantidad As Integer)
        Dim montobono3 As Integer = 60


        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, fechacorte, status) VALUES(" & asociado.ToString & ", '" & de.Text & "', '" & a.Text & "', 3, " & (cantidad * montobono3).ToString & ", '" & Today.Year & "/" & Today.Month & "/" & Today.Day & "',0)"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub bono4()

        llenavistadeporcentajes()
        llenavistadepuntos()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        For i = 0 To asociados.Count - 1

            strTeamQuery = "SELECT puntosasociados.asociado, puntosasociados.lado, Sum(puntosasociados.puntos) AS SumOfpuntos " & _
                            "FROM(puntosasociados)" & _
                            "GROUP BY puntosasociados.asociado, puntosasociados.lado " & _
                            "HAVING (((puntosasociados.asociado)=" & asociados(i) & "));"
            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()
            Dim bono As Integer = 0
            Dim izq, der As Integer
            While dtrTeam.Read
                If dtrTeam(1) = "i" Or dtrTeam(1) = "I" Then
                    izq = dtrTeam(2)
                Else
                    der = dtrTeam(2)
                End If
            End While

            sqlConn.Close()
            sqlConn.Dispose()
            If der > 0 And izq > 0 Then
                If izq >= der Then
                    insertabono4(asociados(i), der, "D")
                Else
                    insertabono4(asociados(i), izq, "I")
                End If
            End If
            izq = 0
            der = 0
        Next





    End Sub
    Sub insertabono4(ByVal asociado As Integer, ByVal puntos As Integer, ByVal lado As String)
        'recoge % pago asociado


        Dim porcentaje As Decimal = porcentajedepago(asociado)
        'inserta pago
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, fechacorte, status) VALUES(" & asociado.ToString & ", '" & de.Text & "', '" & a.Text & "', 4, " & (puntos * porcentaje).ToString & ", '" & Today.Year & "/" & Today.Month & "/" & Today.Day & "',0)"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
        'actualiza a pagados los puntos del lado indicado
        strTeamQuery = "UPDATE puntosasociados SET status=1, porpagar=0 WHERE lado='" & lado & "' AND asociado=" & asociado.ToString
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
        strTeamQuery = "SELECT id, status, porpagar FROM puntosasociados WHERE asociado=" & asociado.ToString & " AND lado='" & ladocontrario & "' AND status<>1"
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
        Dim strTeamQuery As String = "UPDATE puntosasociados SET status=" & status.ToString & ", porpagar=" & puntos.ToString & " WHERE id=" & id.ToString
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
        strTeamQuery = "SELECT rango, ptsmes FROM asociados WHERE id=" & asociado.ToString
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
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, patrocinador, orden, ptsmes, bono6 FROM asociados WHERE status>0"



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
    Sub bono5()
        Dim porcentajebono5 As Decimal = 0.2
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT asociados.patrocinador, pagos.asociado, pagos.bono, pagos.monto, pagos.de, pagos.a " & _
                                        "FROM asociados INNER JOIN pagos ON asociados.ID = pagos.asociado " & _
                                        "WHERE  pagos.de>='" & Me.de.Text & "' AND pagos.a<='" & Me.a.Text & "' AND pagos.bono=4 " & _
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
    Sub insertabono5(ByVal asociado As Integer, ByVal cantidad As Decimal)



        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, fechacorte, status) VALUES(" & asociado.ToString & ", '" & de.Text & "', '" & a.Text & "', 5, " & cantidad.ToString & ", '" & Today.Year & "/" & Today.Month & "/" & Today.Day & "',0)"
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
    Sub bono6()
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
        "WHERE compras.Fecha >= '" & de.Text & "' AND compras.Fecha <= '" & a.Text & "' AND paquete>0 " & _
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
                If idasociado > raiz Then insertabono6(idasociado, cantidad1, cantidad2)

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
        If idasociado > raiz Then insertabono6(idasociado, cantidad1, cantidad2)



    End Sub
    Function buscaantepasadomayoratres(ByVal asociado As Integer) As Integer
        Dim drv As DataRowView
        viewasociados.RowFilter = "id=" & asociado.ToString
        For Each drv In viewasociados
            Return drv("bono6")

        Next
    End Function

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
    Sub insertabono6(ByVal asociado As Integer, ByVal cantidad1 As Integer, ByVal cantidad2 As Integer)
        'inserta en tabla pagos
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, fechacorte, status) VALUES(" & asociado.ToString & ", '" & de.Text & "', '" & a.Text & "', 6, " & ((cantidad1 + cantidad2) * 50).ToString & ", '" & Today.Year & "/" & Today.Month & "/" & Today.Day & "',0)"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
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
                If idasociado > raiz Then insertabono7(idasociado, cantidad1, cantidad2)

                idasociado = dtrTeam("patrocinador")
                cantidad1 = 0
                cantidad2 = 0

            End If

            cantidad2 += dtrTeam("compras2")

            cantidad1 += dtrTeam("compras1")

        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If idasociado > raiz Then insertabono7(idasociado, cantidad1, cantidad2)



    End Sub
    Function mispuntosdelmes(ByVal asociado As Integer) As Integer
        Dim drv As DataRowView
        viewasociados.RowFilter = "id=" & asociado.ToString
        For Each drv In viewasociados
            Return drv("ptsmes")

        Next
    End Function
    Sub insertabono7(ByVal asociado As Integer, ByVal cantidad1 As Integer, ByVal cantidad2 As Integer)
        ' busca sus puntos de asociado
        Dim mispuntos As Integer = mispuntosdelmes(asociado)
        Dim monto As String
        If mispuntos > 350 Then
            monto = (cantidad1 * 25 + cantidad2 * 50).ToString
        Else
            monto = ((cantidad1 + cantidad2) * 25).ToString

        End If
        'inserta en tabla pagos
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, fechacorte, status) VALUES(" & asociado.ToString & ", '" & de.Text & "', '" & a.Text & "', 7, " & monto & ", '" & Today.Year & "/" & Today.Month & "/" & Today.Day & "',0)"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
        'inserta en tabla temporal bono6
        strTeamQuery = "INSERT INTO bono7(asociado, compras1, compras2, patrocinador) VALUES(" & asociado.ToString & ", " & cantidad1.ToString & ", " & cantidad2.ToString & ", " & quienesmipatrocinador(asociado).ToString & " )"
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub insertabono8(ByVal asociado As Integer, ByVal cantidad1 As Integer, ByVal cantidad2 As Integer)
        ' busca sus puntos de asociado
        Dim mispuntos As Integer = mispuntosdelmes(asociado)
        Dim monto As String
        If mispuntos > 350 Then
            monto = (cantidad1 * 25 + cantidad2 * 50).ToString
        Else
            monto = ((cantidad1 + cantidad2) * 25).ToString

        End If
        'inserta en tabla pagos
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "INSERT INTO pagos(asociado, de, a, bono, monto, fechacorte, status) VALUES(" & asociado.ToString & ", '" & de.Text & "', '" & a.Text & "', 8, " & monto & ", '" & Today.Year & "/" & Today.Month & "/" & Today.Day & "',0)"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()

    End Sub
    Sub bono8()
        
        'creamos tabla para agregarle las compras a cada asociado

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT patrocinador, compras1, compras2 FROM bono7 WHERE patrocinador>0 ORDER BY patrocinador DESC"

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader




        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim idasociado As Integer = 0
        Dim cantidad1, cantidad2 As Integer
        While dtrTeam.Read
            If dtrTeam("patrocinador") <> idasociado Then
                If idasociado > raiz Then insertabono8(idasociado, cantidad1, cantidad2)

                idasociado = dtrTeam("patrocinador")
                cantidad1 = 0
                cantidad2 = 0

            End If

            cantidad2 += dtrTeam("compras2")

            cantidad1 += dtrTeam("compras1")

        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If idasociado > raiz Then insertabono8(idasociado, cantidad1, cantidad2)



    End Sub
   
End Class
