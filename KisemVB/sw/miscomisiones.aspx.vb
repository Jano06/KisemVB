Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Media
Partial Class sw_miscomisiones
    Inherits System.Web.UI.Page
    Dim asociados As New List(Of Integer)
    Dim view As New DataView()
    Dim viewpuntos As New DataView()
    Dim viewasociados As New DataView()
    Dim antepasado As Integer = 0
    Dim raiz As Integer
    Dim estado As String = ""
    Dim idperiodo As Integer
    Dim iniciociclo, finciclo As Date ' para ciclos de calificación
    Dim totalganancias As Decimal
    'tabla para llenar grid
    Dim ds As New DataSet
    Dim table As DataTable = ds.Tables.Add("Data")
    Dim pago1bono6, pago2bono6, pago1bono7, pago2bono7, pago1bono8, pago2bono8, pagominimobono678 As Decimal
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try
            Dim func As New funciones
            If func.mistatus(Session("idasociado")) = 0 Then
                Me.recordatorio.Text = "Recuerda que para cobrar tus ganancias, debes activarte antes del próximo viernes"
            End If
            table.Columns.Add("asociado", GetType(Integer))
            table.Columns.Add("bono", GetType(String))
            table.Columns.Add("monto", GetType(Decimal))
            table.Columns.Add("de", GetType(String))
            table.Columns.Add("a", GetType(String))
            table.Columns.Add("cantidad1", GetType(Integer))
            table.Columns.Add("cantidad2", GetType(Integer))
            llenavistadeasociados()

            ''estado = "Borra comisiones sin terminar"
            ' borracomisionesparciales()
            ''estado = "Genera periodo"
            'defineperiodo()
            ''estado = "Inserta periodo"
            ' insertaperiodo()
            ''estado = "Corre comisiones"
            correcomisiones()
            llenagrid()
            ''estado = "Abandera comisiones"
            'abanderacomisiones()
            'revisa ciclos

            Me.mensajes.Text = "Comisiones calculadas con éxito"
            Me.mensajes.Visible = True
            Me.Button1.Enabled = True
            SystemSounds.Exclamation.Play()
        Catch ex As Exception
            Me.mensajes.Text = estado & ex.Message.ToString
            Me.mensajes.Visible = True
        End Try


        'Me.Image1.Visible = False
    End Sub
    Sub llenagrid()
        Dim viewgrid As DataView = ds.Tables(0).DefaultView
        viewgrid.RowFilter = "asociado=" & Session("idasociado").ToString
        Me.GridView1.DataSource = viewgrid
        Me.GridView1.DataBind()
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then

            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Mis Comisiones"
           
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
            Me.de.Text = DateAdd(DateInterval.Day, 7, de).ToString("dd/MMMM/yyyy")
            Me.a.Text = DateAdd(DateInterval.Day, 7, a).ToString("dd/MMMM/yyyy")
          


        End If
    End Sub
#Region "Ciclos"
  
    
 

#End Region
#Region "comisiones"


   
  
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
        Dim strTeamQuery As String = "SELECT MIN(id) FROM asociados WHERE id=" & Session("idasociado").ToString
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
    Sub correcomisiones()

        estado += "Inicia correcomisiones. "

        Try
            'estado = "Busca raíz"
            buscaraiz()

            'estado = "Conecta para sacar asociados activos"
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = "SELECT id, rango, ptsmes FROM asociados WHERE id=" & Session("idasociado").ToString
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
            bono3()

            'estado = "Inicia Bono 4"
            bono4()

            'estado = "Inicia Bono 5"
            bono5()
            'estado = "Inicia Bono 6"
            bono6(Session("idasociado"))
            'bono 6 de mis hijos y nietos
            strTeamQuery = "SELECT id, historia FROM asociados WHERE historia LIKE '%." & Session("idasociado").ToString & ".%'"
            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)



            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read
                If Not IsDBNull(dtrTeam(0)) Then
                    Dim historia() As String = Split(dtrTeam(1).ToString, ".")
                    If historia.Length > 2 Then
                        If historia(historia.Length - 3) = Session("idasociado") Or historia(historia.Length - 2) = Session("idasociado") Then
                            bono6(dtrTeam(0))
                        End If
                    Else
                        If historia(historia.Length - 2) = Session("idasociado") Then
                            bono6(dtrTeam(0))
                        End If
                    End If

                End If



            End While

            sqlConn.Close()

            'estado = "Inicia Bono 7"
            bono7(Session("idasociado"))
            strTeamQuery = "SELECT id FROM asociados WHERE patrocinador =" & Session("idasociado").ToString
            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)



            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read
                bono7(dtrTeam(0))
            End While
            sqlConn.Close()
            'estado = "Inicia Bono 8"
            bono8(Session("idasociado"))

        Catch ex As Exception
            Me.mensajes.Text = estado & ex.Message.ToString
            Me.mensajes.Visible = True
        End Try
        estado += "Termina correcomisiones. "
    End Sub

    Sub defineperiodo()
        estado += "Inicia defineperiodo. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT MAX(id) FROM periodos WHERE status=1"
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
   
    Sub bono1()
        estado += "Inicia Bono1. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT COUNT(id) FROM compras WHERE asociado=" & ID.ToString & " AND paquete=3 AND excedente=1 AND fecha>='" & CDate(de.Text).ToString("yyyy/MM/dd") & "' AND fecha <='" & CDate(a.Text).ToString("yyyy/MM/dd") & "';"
        strTeamQuery = ""
        'para incluir el lunes
        Dim fechafinalperiodo As Date = CDate(Me.a.Text)
        fechafinalperiodo = DateAdd(DateInterval.Day, 3, fechafinalperiodo)
        Dim fechafinalperiodoanterior As Date = DateAdd(DateInterval.Day, -1, CDate(Me.de.Text))
        Dim fechalunesanterior As Date = DateAdd(DateInterval.Day, 2, CDate(Me.de.Text))
        strTeamQuery = " SELECT Count( compras.ID ) AS CountOfID, compras.asociado " & _
                      "FROM(compras) INNER JOIN comprasdetalle ON compras.id = comprasdetalle.compra " & _
                      "WHERE(comprasdetalle.paquete = 3) AND compras.excedente=1  AND compras.asociado=" & Session("idasociado").ToString & " " & _
                      "AND compras.fecha >= '" & CDate(de.Text).tostring("yyyy/MM/dd") & "' " & _
                      "AND compras.fecha <= '" & fechafinalperiodo.Year.ToString & "/" & fechafinalperiodo.Month.ToString & "/" & fechafinalperiodo.Day.ToString & "' " & _
                      "AND compras.statuspago = 'PAGADO' " & _
                      "AND compras.fechaorden <= '" & CDate(a.Text).tostring("yyyy/MM/dd") & "' " & _
                      "AND compras.id NOT IN (" & _
                      "Select id FROM compras " & _
                      "WHERE fechaorden <= '" & fechafinalperiodoanterior.Year.ToString & "/" & fechafinalperiodoanterior.Month.ToString & "/" & fechafinalperiodoanterior.Day.ToString & "' " & _
                      "AND fecha >= '" & CDate(de.Text).tostring("yyyy/MM/dd") & "' " & _
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
    Sub bono1anterior()
        estado += "Inicia Bono1. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT COUNT(id) FROM compras WHERE asociado=" & ID.ToString & " AND paquete=3 AND activacion=0 AND fecha>='" & CDate(de.Text).tostring("yyyy/MM/dd") & "' AND fecha <='" & CDate(a.Text).tostring("yyyy/MM/dd") & "';"
        strTeamQuery = "SELECT Count(compras.ID) AS CountOfID, compras.asociado " & _
                       "FROM(compras) " & _
                        "GROUP BY compras.asociado, compras.paquete, compras.activacion, compras.fecha, compras.fecha " & _
                        "HAVING compras.paquete=3 AND compras.activacion=0 AND compras.fecha>='" & CDate(de.Text).tostring("yyyy/MM/dd") & "' AND compras.fecha<='" & CDate(a.Text).tostring("yyyy/MM/dd") & "' AND compras.asociado=" & Session("idasociado").ToString





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
        Dim montobono1 As Integer = 200
        table.Rows.Add(New Object() {asociado, "Bono 1", (cuenta * montobono1).ToString, de.Text, a.Text, 0, 0})

    End Sub
    Sub bono2()
        estado += "Inicia bono2. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT asociados.id, asociados.orden, asociados.patrocinador, compras.puntos  FROM asociados INNER JOIN compras ON asociados.id=compras.asociado WHERE compras.fecha<='" & CDate(a.Text).tostring("yyyy/MM/dd") & "' AND compras.fecha>='" & CDate(de.Text).tostring("yyyy/MM/dd") & "'  AND compras.inscripcion>0  AND patrocinador=" & Session("idasociado").ToString & " ORDER BY patrocinador DESC;"
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
    Sub bono2anterior()
        estado += "Inicia bono2. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, orden, patrocinador FROM asociados WHERE FInsc<='" & CDate(a.Text).tostring("yyyy/MM/dd") & "' AND FInsc>='" & CDate(de.Text).tostring("yyyy/MM/dd") & "' AND patrocinador=" & Session("idasociado").ToString
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
    Sub insertabono2(ByVal asociado As Integer, ByVal bonoa As Integer, ByVal bonob As Integer, Optional ByVal bonopuntos As Integer = 0)
        Dim montobono2a As Integer = 60
        Dim montobono2b As Integer = 120

        table.Rows.Add(New Object() {asociado, "Bono 2", ((bonoa * montobono2a) + (bonob * montobono2b) + bonopuntos).ToString, de.Text, a.Text, 0, 0})
    End Sub
    Sub insertabono2anterior(ByVal asociado As Integer, ByVal bonoa As Integer, ByVal bonob As Integer)
        Dim montobono2a As Integer = 60
        Dim montobono2b As Integer = 120
        table.Rows.Add(New Object() {asociado, "Bono 2", ((bonoa * montobono2a) + (bonob * montobono2b)).ToString, de.Text, a.Text, 0, 0})

    End Sub
    Sub bono3()
        estado += "Inicia bono3. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, bono6  " & _
                        "FROM asociados   " & _
                        "WHERE Orden<3 AND  FInsc<='" & CDate(a.Text).tostring("yyyy/MM/dd") & "' AND FInsc>='" & CDate(de.Text).tostring("yyyy/MM/dd") & "' AND bono6=" & Session("idasociado").ToString & " " & _
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
    Sub bono3anterior()
        estado += "Inicia bono3. "
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT asociados.ID AS padre, asociados.patrocinador AS abuelo, asociados_1.ID AS asociado, asociados_1.FInsc, asociados_1.Orden, asociados.Orden " & _
                        "FROM asociados INNER JOIN asociados AS asociados_1 ON asociados.ID = asociados_1.patrocinador " & _
                        "WHERE asociados_1.Orden<3 AND asociados.Orden>2 AND  asociados_1.FInsc<='" & CDate(a.Text).tostring("yyyy/MM/dd") & "' AND asociados_1.FInsc>='" & CDate(de.Text).tostring("yyyy/MM/dd") & "' AND asociados.patrocinador= " & Session("idasociado").ToString

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
    Sub insertabono3(ByVal asociado As Integer, ByVal cantidad As Integer)
        Dim montobono3 As Integer = 60
        table.Rows.Add(New Object() {asociado, "Bono 3", (cantidad * montobono3).ToString, de.Text, a.Text, 0, 0})

       
    End Sub

    Sub bono4()
        estado += "Inicia bono4. "
        llenavistadeporcentajes()
        llenavistadepuntos()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader



        Dim inicio As Date = CDate(Me.de.Text)
        Dim fin As Date = CDate(Me.a.Text)
        strTeamQuery = "SELECT puntosasociados.Asociado , sum( if( puntosasociados.Lado = 'D', `PorPagar` , 0 ) ) AS D, sum( if( puntosasociados.Lado = 'I', `PorPagar` , 0 ) ) AS I "
        strTeamQuery += "FROM `puntosasociados`  INNER JOIN compras ON puntosasociados.compra=compras.id INNER JOIN asociados ON puntosasociados.asociado = asociados.id "
        strTeamQuery += "WHERE  compras.fecha<='" & fin.Year.ToString & "/" & fin.Month.ToString & "/" & fin.Day.ToString & "' AND compras.statuspago='PAGADO' AND (puntosasociados.asociado=" & Session("idasociado").ToString & " OR asociados.patrocinador=" & Session("idasociado").ToString & " ) "
        strTeamQuery += "GROUP BY puntosasociados.Asociado  "
        strTeamQuery += "HAVING  (D > 0) AND (I >0)"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim bono As Integer = 0

        While dtrTeam.Read
            If dtrTeam(2) >= dtrTeam(1) Then
                insertabono4(dtrTeam(0), dtrTeam(1), "D")
            Else
                insertabono4(dtrTeam(0), dtrTeam(2), "I")
            End If
        End While

        sqlConn.Close()
        sqlConn.Dispose()


        estado += "Termina bono4. "




    End Sub

    Sub bono4anterior()
        estado += "Inicia bono4. "
        llenavistadeporcentajes()
        llenavistadepuntos()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader



        strTeamQuery = "SELECT puntosasociados.`Asociado` , sum( if( puntosasociados.`Lado` = 'D', puntosasociados.`PorPagar` , 0 ) ) AS D, sum( if( puntosasociados.`Lado` = 'I', puntosasociados.`PorPagar` , 0 ) ) AS I, asociados.patrocinador " & _
        "FROM(puntosasociados) " & _
        "INNER JOIN asociados ON puntosasociados.asociado = asociados.id " & _
        "GROUP BY puntosasociados.`Asociado` " & _
        "HAVING(D > 0) AND (I >0) AND (asociados.patrocinador = " & Session("idasociado").ToString & " OR puntosasociados.`Asociado` =" & Session("idasociado").ToString & ")"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim bono As Integer = 0

        While dtrTeam.Read
            If dtrTeam(2) >= dtrTeam(1) Then
                insertabono4(dtrTeam(0), dtrTeam(1), "D")
            Else
                insertabono4(dtrTeam(0), dtrTeam(2), "I")
            End If
        End While

        sqlConn.Close()
        sqlConn.Dispose()


        estado += "Termina bono4. "




    End Sub
    Sub insertabono4(ByVal asociado As Integer, ByVal puntos As Integer, ByVal lado As String)
        'recoge % pago asociado


        Dim porcentaje As Decimal = porcentajedepago(asociado)
        table.Rows.Add(New Object() {asociado, "Bono 4", (puntos * porcentaje).ToString, de.Text, a.Text, 0, 0})
     


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
        Dim strTeamQuery As String = "SELECT id, patrocinador, orden, ptsmes, bono6, rango FROM asociados WHERE id=" & Session("idasociado").ToString



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
    Sub bono5()
        estado += "Inicia bono5. "
        Dim porcentajebono5 As Decimal = 0.2
        Dim cuenta As Decimal = 0
        Dim tope As Integer = table.Rows.Count - 1
        For i = 0 To tope
            If table(i)(0).ToString <> Session("idasociado").ToString Then
                cuenta += CDec(table(i)(2).ToString) * porcentajebono5
                'table.Rows(i).Delete()
                'i -= 1
                'tope -= 1
            End If

        Next
        If cuenta > 0 Then insertabono5(Session("idasociado"), cuenta)
        estado += "Termina bono5. "
    End Sub
    Sub insertabono5(ByVal asociado As Integer, ByVal cantidad As Decimal)



        table.Rows.Add(New Object() {asociado, "Bono 5", cantidad.ToString, de.Text, a.Text, 0, 0})
    End Sub
   
    Sub bono6(ByVal asociado As Integer)
        buscavaloresbono6()
        'para incluir el lunes
        Dim fechafinalperiodo As Date = CDate(Me.a.Text)
        fechafinalperiodo = DateAdd(DateInterval.Day, 3, fechafinalperiodo)
        Dim fechafinalperiodoanterior As Date = DateAdd(DateInterval.Day, -1, CDate(Me.de.Text))
        Dim fechalunesanterior As Date = DateAdd(DateInterval.Day, 2, CDate(Me.de.Text))
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = " SELECT asociados.ID AS Asociado, asociados.Orden AS orden, asociados.patrocinador AS patrocinador, compras.ID AS Compra, compras.fecha AS fecha, comprasdetalle.cantidad AS cantidad, comprasdetalle.paquete, asociados.bono6 AS bono6, asociados.ptsmes  " & _
                      "FROM asociados INNER JOIN compras ON asociados.ID = compras.asociado INNER JOIN comprasdetalle ON compras.id=comprasdetalle.compra " & _
                      "WHERE  comprasdetalle.paquete>0 AND compras.excedente=0  AND compras.statuspago='PAGADO' AND compras.inscripcion=0    " & _
                      " AND asociados.patrocinador =" & asociado.ToString & " OR asociados.patrocinador IN ( Select id FROM(asociados) WHERE(patrocinador = " & asociado.ToString & ")) " & _
                      "AND compras.fecha >= '" & CDate(de.Text).tostring("yyyy/MM/dd") & "' " & _
                      "AND compras.fecha <= '" & fechafinalperiodo.Year.ToString & "/" & fechafinalperiodo.Month.ToString & "/" & fechafinalperiodo.Day.ToString & "' " & _
                      "AND compras.fechaorden <= '" & CDate(a.Text).tostring("yyyy/MM/dd") & "' " & _
                      "AND compras.id NOT IN (" & _
                      "Select id FROM compras " & _
                      "WHERE fechaorden <= '" & fechafinalperiodoanterior.Year.ToString & "/" & fechafinalperiodoanterior.Month.ToString & "/" & fechafinalperiodoanterior.Day.ToString & "' " & _
                      "AND fecha >= '" & CDate(de.Text).tostring("yyyy/MM/dd") & "' " & _
                      "AND fecha <= '" & fechalunesanterior.Year.ToString & "/" & fechalunesanterior.Month.ToString & "/" & fechalunesanterior.Day.ToString & "' " & _
                      ") " & _
                      "ORDER BY compras.ID DESC;"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader




        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Dim cantidad1, cantidad2 As Integer
        While dtrTeam.Read
            If dtrTeam(6) = 1 Then
                cantidad1 += dtrTeam(5)
            Else
                cantidad2 += dtrTeam(55)
            End If


        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If cantidad1 > 0 Or cantidad2 > 0 Then
            insertabono6(asociado, cantidad1, cantidad2)
        End If


        estado += "Termina bono6. "

    End Sub
    Sub bono6anterior(ByVal asociado As Integer)
        buscavaloresbono6()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        strTeamQuery += " SELECT compras.id, comprasdetalle.paquete, comprasdetalle.cantidad "
        strTeamQuery += " FROM `compras` INNER JOIN asociados ON asociados.id=compras.asociado INNER JOIN comprasdetalle ON compras.id=comprasdetalle.compra "
        strTeamQuery += " WHERE(asociados.bono6 = " & asociado.ToString & ") "
        strTeamQuery += " AND compras.fecha >= '" & CDate(de.Text).tostring("yyyy/MM/dd") & "' "
        strTeamQuery += " AND compras.fecha <= '" & CDate(a.Text).tostring("yyyy/MM/dd") & "' "
        strTeamQuery += " AND comprasdetalle.paquete>0 "
        strTeamQuery += " AND `StatusPago` = 'PAGADO'"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader




        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Dim cantidad1, cantidad2 As Integer
        While dtrTeam.Read
            If dtrTeam(1) = 1 Then
                cantidad1 += dtrTeam(2)
            Else
                cantidad2 += dtrTeam(2)
            End If


        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If cantidad1 > 0 Or cantidad2 > 0 Then
            insertabono6(asociado, cantidad1, cantidad2)
        End If


        estado += "Termina bono6. "

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
    Sub insertabono6(ByVal asociado As Integer, ByVal cantidad1 As Integer, ByVal cantidad2 As Integer)
        'inserta en tabla pagos
        Dim montobono As Decimal = 0

        Dim puntospersonales As Integer = misptsmes(asociado)
        If puntospersonales > 350 Then
            montobono = ((cantidad1 * pago1bono6) + (cantidad2 * pago2bono6))
        Else
            montobono = (cantidad1 + cantidad2) * pagominimobono678
        End If
        table.Rows.Add(New Object() {asociado, "Bono 6", montobono.ToString, de.Text, a.Text, cantidad1, cantidad2})

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




   
    Sub bono7(ByVal asociado As Integer)
        buscavaloresbono7()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "" & _
        "SELECT id FROM asociados WHERE patrocinador=" & asociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader




        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cantidad1, cantidad2 As Integer
        While dtrTeam.Read

            Dim tope As Integer = table.Rows.Count - 1
            For i = 0 To tope
                If table(i)(0).ToString = dtrTeam(0).ToString And table(i)(1).ToString = "Bono 6" Then
                    cantidad1 += CDec(table(i)(5).ToString)
                    cantidad2 += CDec(table(i)(6).ToString)
                End If

            Next
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If cantidad1 > 0 Or cantidad2 > 0 Then insertabono7(asociado, cantidad1, cantidad2)

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
        Dim montobono As Decimal = 0

        Dim puntospersonales As Integer = misptsmes(asociado)
        If puntospersonales > 350 Then
            montobono = (cantidad1 * pago1bono7 + cantidad2 * pago2bono7).ToString
        Else
            montobono = ((cantidad1 + cantidad2) * pagominimobono678).ToString

        End If
        table.Rows.Add(New Object() {asociado, "Bono 7", montobono.ToString, de.Text, a.Text, cantidad1, cantidad2})
    End Sub
    Sub insertabono8(ByVal asociado As Integer, ByVal cantidad1 As Integer, ByVal cantidad2 As Integer)
        ' busca sus puntos de asociado
        ' busca sus puntos de asociado
        Dim montobono As Decimal = 0

        Dim puntospersonales As Integer = misptsmes(asociado)
        If puntospersonales > 350 Then
            montobono = (cantidad1 * pago1bono8 + cantidad2 * pago2bono8).ToString
        Else
            montobono = ((cantidad1 + cantidad2) * pagominimobono678).ToString

        End If

        table.Rows.Add(New Object() {asociado, "Bono 8", montobono.ToString, de.Text, a.Text, cantidad1, cantidad2})

    End Sub
    Sub bono8(ByVal asociado As Integer)
        estado += "Inicia bono8. "
        buscavaloresbono8()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "" & _
        "SELECT id FROM asociados WHERE patrocinador=" & asociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader




        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cantidad1, cantidad2 As Integer
        While dtrTeam.Read

            Dim tope As Integer = table.Rows.Count - 1
            For i = 0 To tope
                If table(i)(0).ToString = dtrTeam(0).ToString And table(i)(1).ToString = "Bono 7" Then
                    cantidad1 += CDec(table(i)(5).ToString)
                    cantidad2 += CDec(table(i)(6).ToString)

                End If

            Next
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        If cantidad1 > 0 Or cantidad2 > 0 Then insertabono8(asociado, cantidad1, cantidad2)


        estado += "Termina bono8. "

    End Sub
#End Region
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

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            totalganancias += CDec(e.Row.Cells(2).Text)
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = totalganancias.ToString("c")
            e.Row.Cells(1).Text = "Totales"
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class
