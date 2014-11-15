Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Partial Class sw_pagos
    Inherits System.Web.UI.Page
    Dim comprastotales As Decimal
    Dim varbono1, varbono2, varbono3, varbono4, varbono5, varbono6, varbono7, varbono8, varbono9, varbono10, varbono11 As Decimal
    Dim PagoNivel1Emprendedor, PagoNivel2Emprendedor, PagoNivel3Emprendedor, PagoNivel1Empresario, PagoNivel2Empresario, PagoNivel3Empresario As Decimal
    Dim inicio, final As Date
    Dim mispuntos As Integer
    Dim pago1bono6, pago2bono6, pago1bono7, pago2bono7, pago1bono8, pago2bono8, pagominimobono678 As Decimal
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Reporte de Pagos"
            llenaperiodos()
        End If

    End Sub
    Sub llenaperiodos()
        Try
            Me.cortes.Items.Clear()
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = "SET lc_time_names = 'es_UY'; SELECT id,  Date_format(inicio,'%Y/%M/%d') AS inicio,   Date_format(final,'%Y/%M/%d') AS final, status FROM periodos WHERE status=1 OR status=2 ORDER BY id DESC "
            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()
            Dim liberado As String
            While dtrTeam.Read
                If dtrTeam("status") = 2 Then
                    liberado = " (No Liberado) "
                Else
                    liberado = ""
                End If

                Me.cortes.Items.Add(New ListItem(dtrTeam("id").ToString & " de: " & dtrTeam("inicio").ToString & " a: " & dtrTeam("final").ToString & liberado, dtrTeam("id").ToString))

            End While

            sqlConn.Close()
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            ocultadetalles()

            calculatotalcompras()
            calculatotalinscripciones()

            calculacomisiones()
            'grafica()
            llenagrid()
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try

    End Sub
    Sub llenagrid()
        Me.GridView1.SelectedIndex = -1
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT pagos.asociado, CONCAT( asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) AS nombre, pagos.bono, pagos.monto, pagos.de, pagos.a, pagos.corte FROM pagos INNER JOIN asociados ON pagos.asociado=asociados.id WHERE pagos.corte=" & Me.cortes.SelectedItem.Value.ToString
        If Not Me.TextBox1.Text = Nothing Then
            Dim asociado() As String = Split(Me.TextBox1.Text, " ")
            strTeamQuery += " AND pagos.asociado=" & asociado(0)
        End If
        strTeamQuery += " ORDER BY pagos.asociado ASC, pagos.bono ASC"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Me.GridView1.DataSource = dtrTeam
        Me.GridView1.DataBind()

        sqlConn.Close()
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        ocultadetalles()
        If InStr(Me.cortes.SelectedItem.Text, "Liberado") > 0 Then
            Me.btnliberar.Visible = True
        Else
            Me.btnliberar.Visible = False
        End If
        Me.GridView1.DataSource = ""
        Me.GridView1.DataBind()
        Me.GridView1.SelectedIndex = -1
        calculatotalcompras()
        calculatotalinscripciones()

        calculacomisiones()
        'grafica()
        Me.Panel1.Visible = True
    End Sub
    Sub grafica()
        Dim ds As New DataSet
        Dim table As DataTable = ds.Tables.Add("Data")
        table.Columns.Add("x", GetType(String))
        table.Columns.Add("cantidad", GetType(Integer))
        table.Rows.Add(New Object() {"Total Ventas", comprastotales})
        table.Rows.Add(New Object() {"Total Comisiones", varbono1 + varbono2 + varbono3 + varbono4 + varbono5 + varbono6 + varbono7 + varbono8})
        table.Rows.Add(New Object() {"Bono 1", varbono1})
        table.Rows.Add(New Object() {"Bono 2", varbono2})
        table.Rows.Add(New Object() {"Bono 3", varbono3})
        table.Rows.Add(New Object() {"Bono 4", varbono4})
        table.Rows.Add(New Object() {"Bono 5", varbono5})
        table.Rows.Add(New Object() {"Bono 6", varbono6})
        table.Rows.Add(New Object() {"Bono 7", varbono7})
        table.Rows.Add(New Object() {"Bono 8", varbono8})
        Dim view As DataView = ds.Tables(0).DefaultView
        ' Set series chart type
        'Chart1.Series("Series1").ChartType = DataVisualization.Charting.SeriesChartType.Bar

        ' Set series point width
        'Chart1.Series("Series1")("PointWidth") = "0.9"

        ' Show data points labels
        'Chart1.Series("Series1").IsValueShownAsLabel = True

        ' Set data points label style
        'Chart1.Series("Series1")("BarLabelStyle") = "Right"

        ' Show as 3D
        'Chart1.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True

        ' Draw as 3D Cylinder
        'Chart1.Series("Series1")("DrawingStyle") = "Cylinder"

        '        Chart1.Series("Series1").XAxisType = DataVisualization.Charting.AxisType.Primary


        '       Me.Chart1.Series("Series1").Points.DataBindXY(view, "x", view, "cantidad")
    End Sub
    Sub calculacomisiones()
        Try
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = ""
            If Not Me.TextBox1.Text = Nothing Then
                Dim asociado() As String = Split(Me.TextBox1.Text, " ")
                strTeamQuery = "SELECT SUM(monto), bono FROM pagos GROUP BY bono, corte, status, asociado HAVING Corte =" & Me.cortes.SelectedItem.Value.ToString & " AND (status=1 OR status=2) AND asociado=" & asociado(0)
            Else
                strTeamQuery = "SELECT SUM(monto), bono FROM pagos GROUP BY bono, corte, status HAVING Corte =" & Me.cortes.SelectedItem.Value.ToString & " AND (status=1 OR status=2)"
            End If
            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()
            Dim bono1, bono2, bono3, bono4, bono5, bono6, bono7, bono8, bono9, bono10, bono11 As Decimal


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
                    Case 9
                        bono9 = dtrTeam(0)
                    Case 10
                        bono10 = dtrTeam(0)
                    Case 11
                        bono11 = dtrTeam(0)

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
            varbono9 = bono9
            varbono10 = bono10
            varbono11 = bono11
            Me.bono1.Text = bono1.ToString
            Me.bono2.Text = bono2.ToString
            Me.bono3.Text = bono3.ToString
            Me.bono4.Text = bono4.ToString
            Me.bono5.Text = bono5.ToString
            Me.bono6.Text = bono6.ToString
            Me.bono7.Text = bono7.ToString()
            Me.bono8.Text = bono8.ToString
            Me.bono9.Text = bono9.ToString
            Me.bono10.Text = bono10.ToString
            Me.bono11.Text = bono11.ToString
            Me.comisionesinscripcion.Text = (bono2 + bono3).ToString
            Me.comisionesproducto.Text = (bono1 + bono4 + bono5 + bono6 + bono7 + bono8 + bono9 + bono10 + bono11).ToString
            Me.totalcomisiones.Text = (bono1 + bono2 + bono3 + bono4 + bono5 + bono6 + bono7 + bono8 + bono9 + bono10 + bono11).ToString
            porcbono1.Text = ((bono1 / (Me.comisionesproducto.Text)) * 100).ToString("####0.00") & "%"
            porcbono2.Text = ((bono2 / (Me.comisionesinscripcion.Text)) * 100).ToString("####0.00") & "%"
            porcbono3.Text = ((bono3 / (Me.comisionesinscripcion.Text)) * 100).ToString("####0.00") & "%"
            porcbono4.Text = ((bono4 / (Me.comisionesproducto.Text)) * 100).ToString("####0.00") & "%"
            porcbono5.Text = ((bono5 / (Me.comisionesproducto.Text)) * 100).ToString("####0.00") & "%"
            porcbono6.Text = ((bono6 / (Me.comisionesproducto.Text)) * 100).ToString("####0.00") & "%"
            porcbono7.Text = ((bono7 / (Me.comisionesproducto.Text)) * 100).ToString("####0.00") & "%"
            porcbono8.Text = ((bono8 / (Me.comisionesproducto.Text)) * 100).ToString("####0.00") & "%"
            porcbono9.Text = ((bono9 / (Me.comisionesproducto.Text)) * 100).ToString("####0.00") & "%"
            porcbono10.Text = ((bono10 / (Me.comisionesproducto.Text)) * 100).ToString("####0.00") & "%"
            porcbono11.Text = ((bono11 / (Me.comisionesproducto.Text)) * 100).ToString("####0.00") & "%"
            Me.total.Text = (CDec(Me.totalcompras.Text) + CDec(Me.totalinscripciones.Text)).ToString
            If Me.TextBox1.Text = "" Then
                Me.porctotal.Text = Decimal.Round((CDec(Me.totalcomisiones.Text) / CDec(Me.total.Text) * 100), 1).ToString & "%"
                Me.porcproducto.Text = Decimal.Round((CDec(Me.comisionesproducto.Text) / CDec(Me.totalcompras.Text) * 100), 1).ToString & "%"
                Me.porcinscripcion.Text = Decimal.Round((CDec(Me.comisionesinscripcion.Text) / CDec(Me.totalinscripciones.Text) * 100), 1).ToString & "%"

            Else
                Me.porctotal.Text = ""
                Me.porcproducto.Text = ""
                Me.porcinscripcion.Text = ""

            End If

        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try
    End Sub
    Sub calculatotalcompras()
        Try
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = "SELECT inicio, final FROM periodos WHERE id=" & Me.cortes.SelectedItem.Value.ToString

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
            If Not Me.TextBox1.Text = Nothing Then
                Dim asociado() As String = Split(Me.TextBox1.Text, " ")
                strTeamQuery = "Select SUM(comprasdetalle.costo) "
                strTeamQuery += "FROM `comprasdetalle` INNER JOIN compras ON comprasdetalle.compra=compras.id "
                strTeamQuery += "WHERE comprasdetalle.paquete>0 AND compras.`StatusPago`='PAGADO' AND compras.fecha>='" & de.ToString("yyyy/M/d") & "' AND compras.fecha<='" & a.ToString("yyyy/M/d") & "' AND compras.asociado=" & asociado(0)
            Else
                strTeamQuery = "Select SUM(comprasdetalle.costo) "
                strTeamQuery += "FROM `comprasdetalle` INNER JOIN compras ON comprasdetalle.compra=compras.id "
                strTeamQuery += "WHERE comprasdetalle.paquete>0 AND compras.`StatusPago`='PAGADO' AND compras.fecha>='" & de.ToString("yyyy/M/d") & "' AND compras.fecha<='" & a.ToString("yyyy/M/d") & "'"
            End If
            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)



            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read

                If Not IsDBNull(dtrTeam(0)) Then
                    Me.totalcompras.Text = dtrTeam(0).ToString
                    comprastotales = dtrTeam(0)
                Else
                    Me.totalcompras.Text = 0.ToString
                    comprastotales = 0
                End If


            End While

            sqlConn.Close()
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try
    End Sub
    Sub calculatotalinscripciones()
        Try
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = "SELECT inicio, final FROM periodos WHERE id=" & Me.cortes.SelectedItem.Value.ToString

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
            If Not Me.TextBox1.Text = Nothing Then
                Dim asociado() As String = Split(Me.TextBox1.Text, " ")
                strTeamQuery = "Select SUM(comprasdetalle.costo) "
                strTeamQuery += "FROM `comprasdetalle` INNER JOIN compras ON comprasdetalle.compra=compras.id "
                strTeamQuery += "WHERE comprasdetalle.paquete=0 AND compras.`StatusPago`='PAGADO' AND compras.fecha>='" & de.ToString("yyyy/M/d") & "' AND compras.fecha<='" & a.ToString("yyyy/M/d") & "' AND compras.asociado=" & asociado(0)
            Else
                strTeamQuery = "Select SUM(comprasdetalle.costo) "
                strTeamQuery += "FROM `comprasdetalle` INNER JOIN compras ON comprasdetalle.compra=compras.id "
                strTeamQuery += "WHERE comprasdetalle.paquete=0 AND compras.`StatusPago`='PAGADO' AND compras.fecha>='" & de.ToString("yyyy/M/d") & "' AND compras.fecha<='" & a.ToString("yyyy/M/d") & "'"
            End If
            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)



            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read

                If Not IsDBNull(dtrTeam(0)) Then
                    Me.totalinscripciones.Text = dtrTeam(0).ToString
                    comprastotales = dtrTeam(0)
                Else
                    Me.totalinscripciones.Text = 0.ToString
                    comprastotales = 0
                End If


            End While

            sqlConn.Close()
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try
    End Sub
#Region "Detalle de pagos"
    Function mispuntosencorte(ByVal corte As Integer, ByVal asociado As Integer) As Integer
        Dim respuesta As Integer = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT MAX(mispuntos) FROM pagos WHERE (status=1 OR status=2) AND corte=" & corte.ToString & " AND asociado=" & asociado.ToString
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
    Sub detallebono(ByVal asociado As Integer, ByVal bono As Integer, ByVal corte As Integer)
        ocultadetalles(bono)
        mispuntos = mispuntosencorte(corte, asociado)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT inicio, final FROM periodos WHERE (status=1 OR status=2) AND id=" & corte.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read

            inicio = dtrTeam(0)
            final = dtrTeam(1)

        End While

        sqlConn.Close()

        Select Case bono
            Case 1
                Me.labelidasociado1.Text = asociado.ToString
                strTeamQuery = "SELECT referencia AS compra, fecha, total AS monto FROM compras WHERE statuspago='PAGADO' AND excedente=1 AND fecha>='" & inicio.ToString("yyyy/M/dd") & "' AND  fecha<='" & final.ToString("yyyy/M/dd") & "' AND asociado=" & asociado.ToString
                sqlConn.Open()
                cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                dtrTeam = cmdFetchTeam.ExecuteReader()

                Me.GridBono1.DataSource = dtrTeam
                Me.GridBono1.DataBind()

                sqlConn.Close()

            Case 2
                Me.labelidasociado2.Text = asociado.ToString
                strTeamQuery = "SELECT id, CONCAT(nombre, ' ', appaterno, ' ', apmaterno) AS nombre, finsc AS fecha, orden FROM asociados WHERE  finsc>='" & inicio.ToString("yyyy/M/dd") & "' AND  finsc<='" & final.ToString("yyyy/M/dd") & "' AND patrocinador=" & asociado.ToString
                sqlConn.Open()
                cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                dtrTeam = cmdFetchTeam.ExecuteReader()

                Me.GridBono2.DataSource = dtrTeam
                Me.GridBono2.DataBind()

                sqlConn.Close()

            Case 3
                Me.labelidasociado3.Text = asociado.ToString
                strTeamQuery = "SELECT id AS asociado,  CONCAT(nombre, ' ', appaterno, ' ', apmaterno) AS nombre, finsc AS fecha, orden, patrocinador  " & _
                        "FROM asociados   " & _
                        "WHERE Orden<3 AND  FInsc<='" & final.ToString("yyyy/M/dd") & "' AND FInsc>='" & inicio.ToString("yyyy/M/dd") & "' AND bono6=" & asociado.ToString

                sqlConn.Open()
                cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                dtrTeam = cmdFetchTeam.ExecuteReader()

                Me.GridBono3.DataSource = dtrTeam
                Me.GridBono3.DataBind()

                sqlConn.Close()

            Case 4

                Me.labelidasociado4.Text = asociado.ToString
                Dim ds As New DataSet
                Dim table As DataTable = ds.Tables.Add("Data")
                table.Columns.Add("descripcion", GetType(String))
                table.Columns.Add("izquierdos", GetType(Integer))
                table.Columns.Add("derechos", GetType(Integer))
                Dim inicialesi, inicialesd, nuevosi, nuevosd, pagados As Integer
                Dim porcentaje As Decimal
                strTeamQuery = "SELECT inicialesi, inicialesd, nuevosi, nuevosd, pagados, porcentaje " & _
                        "FROM puntosdetalle   " & _
                        "WHERE corte=" & corte.ToString & " AND asociado=" & asociado.ToString

                sqlConn.Open()
                cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                dtrTeam = cmdFetchTeam.ExecuteReader()
                While dtrTeam.Read
                    inicialesi = dtrTeam(0)
                    inicialesd = dtrTeam(1)
                    nuevosi = dtrTeam(2)
                    nuevosd = dtrTeam(3)
                    pagados = dtrTeam(4)
                    porcentaje = dtrTeam(5)
                End While

                sqlConn.Close()
                table.Rows.Add(New Object() {"Puntos Iniciales", inicialesi, inicialesd})
                table.Rows.Add(New Object() {"Puntos Nuevos", nuevosi, nuevosd})
                table.Rows.Add(New Object() {"Subtotal en Puntos", inicialesi + nuevosi, inicialesd + nuevosd})


                table.Rows.Add(New Object() {"Puntos Pagados", pagados, pagados})
                table.Rows.Add(New Object() {"Puntos Finales", inicialesi + nuevosi - pagados, inicialesd + nuevosd - pagados})
             


                Dim view As DataView = ds.Tables(0).DefaultView
                Me.GridBono4.Columns(1).Visible = True
                Me.GridBono4.Columns(2).Visible = True
                Me.GridBono4.DataSource = view
                Me.GridBono4.DataBind()
                Me.porcentajelbl.Text = porcentaje.ToString & "%"
                Me.GridDetalleBono4.DataSource = ""
                Me.GridDetalleBono4.DataBind()
                Me.labelladodetalle.Text = ""
            Case 5
                Me.labelidasociado5.Text = asociado.ToString
                strTeamQuery = "SELECT pagos.asociado,  CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) AS nombre, pagos.monto "
                strTeamQuery += "FROM pagos INNER JOIN asociados ON pagos.asociado=asociados.id "
                strTeamQuery += "WHERE pagos.corte=" & corte.ToString & " AND asociados.patrocinador=" & asociado.ToString & " AND pagos.bono=4 "

                'strTeamQuery = "SELECT DISTINCT puntosdetalle.asociado, CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) AS nombre, (puntosdetalle.pagados*puntosdetalle.porcentaje)/100 AS monto " & _
                '        "FROM puntosdetalle INNER JOIN asociados ON puntosdetalle.asociado=asociados.id " & _
                '        "WHERE puntosdetalle.corte=" & corte.ToString & " AND asociados.patrocinador=" & asociado.ToString

                sqlConn.Open()
                cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                dtrTeam = cmdFetchTeam.ExecuteReader()
                Me.GridBono5.DataSource = dtrTeam
                Me.GridBono5.DataBind()
                sqlConn.Close()

            Case 6
                buscavaloresbono6()
                Me.labelidasociado6.Text = asociado.ToString

                strTeamQuery = "SELECT compras.asociado, CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) AS nombre, compras.referencia AS compra, compras.fecha AS fecha, compras.total AS monto, compras.puntos " & _
                        "FROM compras INNER JOIN asociados ON compras.asociado=asociados.id " & _
                        "WHERE compras.puntos>0 AND compras.statuspago='PAGADO' AND compras.excedente=0 AND compras.fecha>='" & inicio.ToString("yyyy/M/dd") & "' AND  compras.fecha<='" & final.ToString("yyyy/M/dd") & "' AND asociados.bono6=" & asociado.ToString & " AND NOT( compras.fecha='" & DateAdd(DateInterval.Day, 2, inicio).ToString("yyyy/M/dd") & "' AND compras.fechaorden='" & DateAdd(DateInterval.Day, -1, inicio).ToString("yyyy/M/dd") & "' )"
                'nuevo 26 dic 2013
                Dim fechafinalperiodo As Date = final
                fechafinalperiodo = DateAdd(DateInterval.Day, 3, fechafinalperiodo)
                Dim fechafinalperiodoanterior As Date = DateAdd(DateInterval.Day, -1, inicio)
                Dim fechalunesanterior As Date = DateAdd(DateInterval.Day, 2, inicio)

                strTeamQuery = "SELECT compras.asociado, CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) AS nombre, compras.referencia AS compra, compras.fecha AS fecha, compras.total AS monto, compras.puntos " & _
                                       "FROM asociados INNER JOIN compras ON asociados.ID = compras.asociado INNER JOIN comprasdetalle ON compras.id=comprasdetalle.compra  " & _
                               "WHERE  compras.fechaorden <= '2014/08/15' AND comprasdetalle.paquete>0 AND compras.excedente=0  AND compras.statuspago='PAGADO' AND (compras.inscripcion=0 OR compras.puntos=350 )" & _
                                "AND asociados.bono6 = " & asociado.ToString & " " & _
                    "AND compras.fecha >= '" & inicio.ToString("yyyy/M/dd") & "' " & _
                    "AND compras.fecha <= '" & fechafinalperiodo.Year.ToString & "/" & fechafinalperiodo.Month.ToString & "/" & fechafinalperiodo.Day.ToString & "' " & _
                    "AND compras.fechaorden <= '" & final.ToString("yyyy/M/dd") & "' " & _
                    "AND compras.id NOT IN (" & _
                    "Select id FROM compras " & _
                    "WHERE fechaorden <= '" & fechafinalperiodoanterior.Year.ToString & "/" & fechafinalperiodoanterior.Month.ToString & "/" & fechafinalperiodoanterior.Day.ToString & "' " & _
                    "AND fecha >= '" & inicio.ToString("yyyy/M/dd") & "' " & _
                    "AND fecha <= '" & fechalunesanterior.Year.ToString & "/" & fechalunesanterior.Month.ToString & "/" & fechalunesanterior.Day.ToString & "' " & _
                    ") " & _
                    "ORDER BY compras.ID DESC;"

                'nuevos bonos niveles
                strTeamQuery = "SELECT compras.asociado, CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) AS nombre, compras.referencia AS compra, compras.fecha AS fecha, compras.total AS monto, compras.puntos " & _
                                       "FROM asociados INNER JOIN compras ON asociados.ID = compras.asociado INNER JOIN comprasdetalle ON compras.id=comprasdetalle.compra  " & _
                               "WHERE  compras.fechaorden <= '2014/08/15' AND comprasdetalle.paquete>0 AND compras.excedente=0  AND compras.statuspago='PAGADO' AND (compras.inscripcion=0 OR compras.puntos=350 )" & _
                                "AND asociados.bono6 = " & asociado.ToString & " " & _
                    "AND compras.fecha >= '" & inicio.ToString("yyyy/M/dd") & "' " & _
                    "AND compras.fecha <= '" & fechafinalperiodo.Year.ToString & "/" & fechafinalperiodo.Month.ToString & "/" & fechafinalperiodo.Day.ToString & "' " & _
                    "ORDER BY compras.ID DESC;"

                sqlConn.Open()
                cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                dtrTeam = cmdFetchTeam.ExecuteReader()

                Me.GridBono6.DataSource = dtrTeam
                Me.GridBono6.DataBind()

                sqlConn.Close()

            Case 7
                buscavaloresbono7()
                Me.labelidasociado7.Text = asociado.ToString

                strTeamQuery = "SELECT compras.asociado, CONCAT( asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) AS nombre, compras.referencia AS compra, compras.fecha AS fecha, compras.total AS monto, asociados.patrocinador  "
                strTeamQuery += "FROM(compras) "
                strTeamQuery += "INNER JOIN asociados ON compras.asociado = asociados.id "
                strTeamQuery += "INNER JOIN asociados AS asociados_1 ON asociados.bono6 = asociados_1.id "
                strTeamQuery += "WHERE  compras.fechaorden <= '2014/08/15' AND (compras.puntos > 0) "
                strTeamQuery += "AND compras.statuspago = 'PAGADO' "
                strTeamQuery += "AND compras.excedente = 0 "
                strTeamQuery += "AND compras.fecha >= '" & inicio.ToString("yyyy/M/dd") & "' "
                strTeamQuery += "AND compras.fecha <= '" & final.ToString("yyyy/M/dd") & "' "
                strTeamQuery += " And asociados_1.patrocinador =  " & asociado.ToString
                'nuevos niveles

                strTeamQuery = "SELECT compras.asociado, CONCAT( asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) AS nombre, compras.referencia AS compra, compras.fecha AS fecha, compras.total AS monto, asociados.patrocinador  "
                strTeamQuery += "FROM(compras) "
                strTeamQuery += "INNER JOIN asociados ON compras.asociado = asociados.id "
                strTeamQuery += "INNER JOIN asociados AS asociados_1 ON asociados.bono6 = asociados_1.id "
                strTeamQuery += "WHERE  compras.fechaorden <= '2014/08/15' AND (compras.puntos > 0) "
                strTeamQuery += "AND compras.statuspago = 'PAGADO' "
                strTeamQuery += "AND compras.excedente = 0 "
                strTeamQuery += "AND compras.fecha >= '" & inicio.ToString("yyyy/M/dd") & "' "
                strTeamQuery += "AND compras.fecha <= '" & final.ToString("yyyy/M/dd") & "' "
                strTeamQuery += "AND NOT (compras.fechaorden ='" & DateAdd(DateInterval.Day, -1, inicio).ToString("yyyy/M/dd") & "' AND compras.fecha ='" & DateAdd(DateInterval.Day, 2, inicio).ToString("yyyy/M/dd") & "')"

                strTeamQuery += " And asociados_1.patrocinador =  " & asociado.ToString
                sqlConn.Open()
                cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                dtrTeam = cmdFetchTeam.ExecuteReader()

                Me.GridBono7.DataSource = dtrTeam
                Me.GridBono7.DataBind()

                sqlConn.Close()

            Case 8
                buscavaloresbono8()
                Me.labelidasociado8.Text = asociado.ToString
                Dim funciones As New funciones
                Dim directos As Integer() = funciones.misdirectos(asociado)

                strTeamQuery = "SELECT compras.asociado, CONCAT( asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) AS nombre, compras.referencia AS compra, compras.fecha AS fecha, compras.total AS monto, asociados.patrocinador, compras.puntos AS puntos  "
                strTeamQuery += "FROM(compras) "
                strTeamQuery += "INNER JOIN asociados ON compras.asociado = asociados.id "
                strTeamQuery += "INNER JOIN asociados AS asociados_1 ON asociados.bono6 = asociados_1.id "
                strTeamQuery += "WHERE  compras.fechaorden <= '2014/08/15' AND (compras.puntos > 0) "
                strTeamQuery += "AND compras.statuspago = 'PAGADO' "
                strTeamQuery += "AND NOT (compras.fechaorden ='" & DateAdd(DateInterval.Day, -1, inicio).ToString("yyyy/M/dd") & "' AND compras.fecha ='" & DateAdd(DateInterval.Day, 2, inicio).ToString("yyyy/M/dd") & "')"
                strTeamQuery += "AND compras.excedente = 0 "
                strTeamQuery += "AND (compras.inscripcion = 0 OR compras.puntos=350 ) "
                strTeamQuery += "AND ((compras.fecha >= '" & inicio.ToString("yyyy/M/dd") & "' "
                strTeamQuery += "AND compras.fecha <= '" & final.ToString("yyyy/M/dd") & "') OR (compras.fechaorden = '" & final.ToString("yyyy/M/dd") & "' AND compras.fecha = '" & DateAdd(DateInterval.Day, 3, final).ToString("yyyy/M/dd") & "')) "
                strTeamQuery += " And ("
                For i = 0 To directos.Length - 1
                    If i = 0 Then
                        strTeamQuery += " asociados_1.patrocinador =  " & directos(i).ToString
                    Else
                        strTeamQuery += " OR asociados_1.patrocinador =  " & directos(i).ToString

                    End If

                Next
                strTeamQuery += " )"
                sqlConn.Open()
                cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                dtrTeam = cmdFetchTeam.ExecuteReader()

                Me.GridBono8.DataSource = dtrTeam
                Me.GridBono8.DataBind()

                sqlConn.Close()
            Case 11
                Me.labelidasociado11.Text = asociado.ToString
                buscavaloresbononiveles()
                Dim ds As New DataSet
                Dim table As DataTable = ds.Tables.Add("Data")
                table.Columns.Add("compra", GetType(String))
                table.Columns.Add("fecha", GetType(Date))
                table.Columns.Add("puntos", GetType(Integer))
                table.Columns.Add("monto", GetType(Decimal))

                Dim orid As String

                Dim aso As New Asociados
                Dim hijos As List(Of String) = aso.HijosDinamicos(asociado)
                For i = 0 To hijos.Count - 1

                    If i = 0 Then
                        orid = " id=" & hijos(i)
                    Else
                        orid += " OR id=" & hijos(i)
                    End If

                Next



                strTeamQuery = "SELECT referencia, fecha, puntos "
                strTeamQuery += "FROM compras  "
                strTeamQuery += "WHERE  compras.fechaorden > '2014/08/15' AND (compras.puntos > 350) "
                strTeamQuery += "AND compras.statuspago = 'PAGADO' "
                strTeamQuery += "AND NOT (compras.fechaorden ='" & DateAdd(DateInterval.Day, -1, inicio).ToString("yyyy/M/dd") & "' AND compras.fecha ='" & DateAdd(DateInterval.Day, 2, inicio).ToString("yyyy/M/dd") & "')"
                strTeamQuery += "AND compras.excedente = 0 "
                strTeamQuery += "AND ((compras.fecha >= '" & inicio.ToString("yyyy/M/dd") & "' "
                strTeamQuery += "AND compras.fecha <= '" & final.ToString("yyyy/M/dd") & "') OR (compras.fechaorden = '" & final.ToString("yyyy/M/dd") & "' AND compras.fecha = '" & DateAdd(DateInterval.Day, 3, final).ToString("yyyy/M/dd") & "')) "
                strTeamQuery += "AND (" & orid & ")"
                sqlConn.Open()
                cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                dtrTeam = cmdFetchTeam.ExecuteReader()
                While dtrTeam.Read
                    Dim monto As Decimal
                    Select Case dtrTeam(2)
                        Case 700
                            table.Rows.Add(New Object() {dtrTeam(0), dtrTeam(1), dtrTeam(2), PagoNivel1Emprendedor})
                        Case 1000
                            table.Rows.Add(New Object() {dtrTeam(0), dtrTeam(1), dtrTeam(2), PagoNivel1Empresario})

                    End Select
                End While

                sqlConn.Close()



                Dim nietos As New List(Of String)
                For i = 0 To hijos.Count - 1
                    Dim asociadosnietos As New Asociados
                    Dim nietotemp As List(Of String) = asociadosnietos.HijosDinamicos(hijos(i))
                    For x = 0 To nietotemp.Count - 1
                        nietos.Add(nietotemp(x))
                    Next
                Next
                For i = 0 To nietos.Count - 1
                    If i = 0 Then
                        orid = " id=" & nietos(i)
                    Else
                        orid += " OR id=" & nietos(i)
                    End If

                Next


                strTeamQuery = "SELECT referencia, fecha, puntos "
                strTeamQuery += "FROM compras  "
                strTeamQuery += "WHERE  compras.fechaorden > '2014/08/15' AND (compras.puntos > 350) "
                strTeamQuery += "AND compras.statuspago = 'PAGADO' "
                strTeamQuery += "AND NOT (compras.fechaorden ='" & DateAdd(DateInterval.Day, -1, inicio).ToString("yyyy/M/dd") & "' AND compras.fecha ='" & DateAdd(DateInterval.Day, 2, inicio).ToString("yyyy/M/dd") & "')"
                strTeamQuery += "AND compras.excedente = 0 "
                strTeamQuery += "AND ((compras.fecha >= '" & inicio.ToString("yyyy/M/dd") & "' "
                strTeamQuery += "AND compras.fecha <= '" & final.ToString("yyyy/M/dd") & "') OR (compras.fechaorden = '" & final.ToString("yyyy/M/dd") & "' AND compras.fecha = '" & DateAdd(DateInterval.Day, 3, final).ToString("yyyy/M/dd") & "')) "
                strTeamQuery += "AND (" & orid & ")"
                sqlConn.Open()
                cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                dtrTeam = cmdFetchTeam.ExecuteReader()
                While dtrTeam.Read
                    Dim monto As Decimal
                    Select Case dtrTeam(2)
                        Case 700
                            table.Rows.Add(New Object() {dtrTeam(0), dtrTeam(1), dtrTeam(2), PagoNivel2Emprendedor})
                        Case 1000
                            table.Rows.Add(New Object() {dtrTeam(0), dtrTeam(1), dtrTeam(2), PagoNivel2Empresario})

                    End Select
                End While

                sqlConn.Close()



                Dim bisnietos As New List(Of String)
                For i = 0 To nietos.Count - 1
                    Dim asociadosbisnietos As New Asociados
                    Dim bisnietotemp As List(Of String) = asociadosbisnietos.HijosDinamicos(nietos(i))
                    For x = 0 To bisnietotemp.Count - 1
                        bisnietos.Add(bisnietotemp(x))
                    Next
                Next
                For i = 0 To bisnietos.Count - 1
                    If i = 0 Then
                        orid = " id=" & bisnietos(i)
                    Else
                        orid += " OR id=" & bisnietos(i)
                    End If
                Next
                strTeamQuery = "SELECT referencia, fecha, puntos "
                strTeamQuery += "FROM compras  "
                strTeamQuery += "WHERE  compras.fechaorden > '2014/08/15' AND (compras.puntos > 350) "
                strTeamQuery += "AND compras.statuspago = 'PAGADO' "
                strTeamQuery += "AND NOT (compras.fechaorden ='" & DateAdd(DateInterval.Day, -1, inicio).ToString("yyyy/M/dd") & "' AND compras.fecha ='" & DateAdd(DateInterval.Day, 2, inicio).ToString("yyyy/M/dd") & "')"
                strTeamQuery += "AND compras.excedente = 0 "
                strTeamQuery += "AND ((compras.fecha >= '" & inicio.ToString("yyyy/M/dd") & "' "
                strTeamQuery += "AND compras.fecha <= '" & final.ToString("yyyy/M/dd") & "') OR (compras.fechaorden = '" & final.ToString("yyyy/M/dd") & "' AND compras.fecha = '" & DateAdd(DateInterval.Day, 3, final).ToString("yyyy/M/dd") & "')) "
                strTeamQuery += "AND (" & orid & ")"
                sqlConn.Open()
                cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                dtrTeam = cmdFetchTeam.ExecuteReader()
                While dtrTeam.Read
                    Dim monto As Decimal
                    Select Case dtrTeam(2)
                        Case 700
                            table.Rows.Add(New Object() {dtrTeam(0), dtrTeam(1), dtrTeam(2), PagoNivel3Emprendedor})
                        Case 1000
                            table.Rows.Add(New Object() {dtrTeam(0), dtrTeam(1), dtrTeam(2), PagoNivel3Empresario})

                    End Select
                End While

                sqlConn.Close()


                Dim view As DataView = ds.Tables(0).DefaultView
                Me.GridBono11.DataSource = view
                Me.GridBono11.DataBind()

        End Select

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
        Dim strTeamQuery As String = "SELECT pago1bono7, pago2bono7, pagominimo678 " & _
                                        "FROM configuracion "

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            pago1bono7 = dtrTeam(0)
            pago2bono7 = dtrTeam(1)
            pagominimobono678 = dtrTeam(2)

        End While

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub buscavaloresbono8()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT pago1bono8, pago2bono8, pagominimo678 " & _
                                        "FROM configuracion "

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            pago1bono8 = dtrTeam(0)
            pago2bono8 = dtrTeam(1)
            pagominimobono678 = dtrTeam(2)

        End While

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub ocultadetalles(Optional ByVal excepto As Integer = 0)
        Me.Panelbono1.Visible = False
        Me.Panelbono2.Visible = False
        Me.Panelbono3.Visible = False
        Me.Panelbono4.Visible = False
        Me.Panelbono5.Visible = False
        Me.Panelbono6.Visible = False
        Me.Panelbono7.Visible = False
        Me.Panelbono8.Visible = False
        Me.Panelbono11.Visible = False
        If excepto > 0 Then
            Select Case excepto
                Case 1
                    Me.Panelbono1.Visible = True
                Case 2
                    Me.Panelbono2.Visible = True
                Case 3
                    Me.Panelbono3.Visible = True
                Case 4
                    Me.Panelbono4.Visible = True
                Case 5
                    Me.Panelbono5.Visible = True
                Case 6
                    Me.Panelbono6.Visible = True
                Case 7
                    Me.Panelbono7.Visible = True
                Case 8
                    Me.Panelbono8.Visible = True
                Case 11
                    Me.Panelbono11.Visible = True
            End Select
        End If
    End Sub
#End Region

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        detallebono(CInt(Me.GridView1.Rows(Me.GridView1.SelectedIndex).Cells(0).Text), CInt(Me.GridView1.Rows(Me.GridView1.SelectedIndex).Cells(2).Text), CInt(Me.GridView1.Rows(Me.GridView1.SelectedIndex).Cells(6).Text))
    End Sub

    Protected Sub GridBono4_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridBono4.DataBound
        Me.GridBono4.Columns(1).Visible = False
        Me.GridBono4.Columns(2).Visible = False
    End Sub

    Protected Sub GridBono4_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridBono4.RowCommand


        llenacomprasvolumen(e.CommandName)

    
    End Sub
    Sub llenacomprasvolumen(ByVal lado As String)
        If lado = "Izq" Then
            Me.labelladodetalle.Text = "Compras del lado Izquierdo"
        Else
            Me.labelladodetalle.Text = "Compras del lado Derecho"
        End If
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT inicio, final FROM periodos WHERE status>=1 AND id=" & Me.GridView1.Rows(Me.GridView1.SelectedIndex).Cells(6).Text
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read

            inicio = dtrTeam(0)
            final = dtrTeam(1)

        End While

        sqlConn.Close()
        strTeamQuery = "SELECT compras.asociado, CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) AS nombre, compras.referencia AS compra, compras.fecha AS fecha, puntosasociados.puntos AS volumen " & _
"FROM compras INNER JOIN asociados ON compras.asociado=asociados.id INNER JOIN puntosasociados ON compras.id=puntosasociados.compra " & _
        "WHERE(compras.puntos > 240) " & _
"AND compras.statuspago='PAGADO'  " & _
"AND (compras.fecha>='" & inicio.ToString("yyyy/M/dd") & "'  " & _
"AND  compras.fecha<='" & final.ToString("yyyy/M/dd") & "'  " & _
"AND NOT (compras.fecha='" & DateAdd(DateInterval.Day, 2, inicio).ToString("yyyy/M/dd") & "' AND compras.fechaorden='" & DateAdd(DateInterval.Day, -1, inicio).ToString("yyyy/M/dd") & "' )" & _
"OR (compras.fecha='" & DateAdd(DateInterval.Day, 3, final).ToString("yyyy/M/dd") & "' AND compras.fechaorden='" & final.ToString("yyyy/M/dd") & "')) " & _
"AND puntosasociados.asociado= " & Me.GridView1.Rows(Me.GridView1.SelectedIndex).Cells(0).Text & " " & _
"AND puntosasociados.lado='" & Left(lado, 1) & "' "
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)

      
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Me.GridDetalleBono4.DataSource = dtrTeam

        Me.GridDetalleBono4.DataBind()


        sqlConn.Close()
    End Sub
  
    Protected Sub GridBono4_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridBono4.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbkBtn As LinkButton = CType(e.Row.Cells(3).Controls(0), LinkButton)
            lbkBtn.Text = e.Row.Cells(1).Text
            lbkBtn.CommandName = "Izq"
            Dim lbkBtn2 As LinkButton = CType(e.Row.Cells(4).Controls(0), LinkButton)
            lbkBtn2.Text = e.Row.Cells(2).Text
            lbkBtn2.CommandName = "Der"
            If e.Row.RowIndex <> 1 Then
                lbkBtn2.Enabled = False
                lbkBtn.Enabled = False

            End If

        End If
      

               
    End Sub

    Protected Sub GridBono6_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridBono6.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim funcion As New funciones
            e.Row.Cells(5).Text = funcion.montodepagobono6(CInt(Left(e.Row.Cells(2).Text, e.Row.Cells(2).Text.Length - 1)), mispuntos, pago1bono6, pago2bono6, pagominimobono678).ToString

          

        End If
    End Sub

    
    
    Protected Sub GridBono7_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridBono7.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim funcion As New funciones
            e.Row.Cells(5).Text = funcion.montodepagobono7(CInt(Left(e.Row.Cells(2).Text, e.Row.Cells(2).Text.Length - 1)), mispuntos, pago1bono7, pago2bono7, pagominimobono678).ToString

           


        End If
    End Sub

    Protected Sub GridBono8_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridBono8.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim funcion As New funciones
            e.Row.Cells(5).Text = funcion.montodepagobono8(CInt(Left(e.Row.Cells(2).Text, e.Row.Cells(2).Text.Length - 1)), mispuntos, pago1bono8, pago2bono8, pagominimobono678).ToString


        End If
    End Sub

    Protected Sub GridBono8_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridBono8.SelectedIndexChanged

    End Sub

    Protected Sub GridBono4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridBono4.SelectedIndexChanged

    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Response.Redirect("reporte_cheques.aspx?corte=" & Me.cortes.SelectedValue.ToString)
    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim strTeamQuery As String = "SELECT pagos.asociado, CONCAT( asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) AS nombre, pagos.bono, pagos.monto, pagos.de, pagos.a, pagos.corte FROM pagos INNER JOIN asociados ON pagos.asociado=asociados.id WHERE pagos.corte=" & Me.cortes.SelectedItem.Value.ToString
        If Not Me.TextBox1.Text = Nothing Then
            Dim asociado() As String = Split(Me.TextBox1.Text, " ")
            strTeamQuery += " AND pagos.asociado=" & asociado(0)
        End If
        strTeamQuery += " ORDER BY pagos.asociado ASC, pagos.bono ASC"
        Dim reporte As New funciones
        reporte.exportaexcel("Reporte Corte " & Me.cortes.selectedvalue.tostring, strTeamQuery)
    End Sub

    Protected Sub btnliberar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnliberar.Click
        'libera
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "UPDATE periodos SET status=1 WHERE id=" & Me.cortes.SelectedValue.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        Me.btnliberar.Visible = False
        Dim indice As Integer = Me.cortes.SelectedIndex
        llenaperiodos()
        Me.cortes.SelectedIndex = indice
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
End Class
