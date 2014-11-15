Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Partial Class sw_mispagos
    Inherits System.Web.UI.Page
    Dim comprastotales As Decimal
    Dim varbono1, varbono2, varbono3, varbono4, varbono5, varbono6, varbono7, varbono8, varbono9, varbono10 As Decimal
    Dim inicio, final As Date
    Dim mispuntos As Integer
    Dim pago1bono6, pago2bono6, pago1bono7, pago2bono7, pago1bono8, pago2bono8, pagominimobono678 As Decimal
    Dim funciones As New funciones
    Dim pag As Integer
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            pag = CInt(Request.QueryString("page"))
            If pag = 0 Then
                Me.PagAnterior.Enabled = False
            End If
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Mis Pagos"
            llenaperiodos()
            llenaresumen()
        End If

    End Sub
    Sub llenaresumen()

        Dim inicio As Integer = pag * 10
        Dim fin As Integer = 10
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        'Dim strTeamQuery As String = "SELECT corte, SUM(monto) AS monto, CONCAT( DATE_FORMAT(de, '%d/%M/%Y') , ' al ', DATE_FORMAT(a, '%d/%M/%Y')  ) AS periodo FROM pagos INNER JOIN periodos ON pagos.corte=periodos.id WHERE  pagos.asociado=" & Session("idasociado").ToString & " AND periodos.status=1 GROUP BY corte, periodo ORDER BY corte DESC LIMIT " & inicio.ToString & ", " & fin.ToString
        Dim strTeamQuery As String = "SELECT corte, SUM(monto) AS monto, IF(DATE_FORMAT(de, '%M')=DATE_FORMAT(a, '%M'), CONCAT( DATE_FORMAT(de, '%d') , ' al ', DATE_FORMAT(a, '%d/%M/%Y')  ),CONCAT( DATE_FORMAT(de, '%d/%M/%Y') , ' al ', DATE_FORMAT(a, '%d/%M/%Y')  )) AS periodo, factura  FROM pagos INNER JOIN periodos ON pagos.corte=periodos.id WHERE pagos.asociado=" & Session("idasociado").ToString & " AND periodos.status=1 GROUP BY corte, periodo HAVING SUM(monto)>0 ORDER BY corte DESC LIMIT " & inicio.ToString & ", " & fin.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        If Not dtrTeam.HasRows Then
            Me.mensajes.Text = "Usted aún no tiene ganancias en nuestra empresa"
        End If
        Me.GridViewresumen.DataSource = dtrTeam
        Me.GridViewresumen.DataBind()

        sqlConn.Close()
    End Sub
    Sub llenaperiodos()
        Try
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = "SELECT id,  Date_format(inicio,'%Y/%M/%d') AS inicio,   Date_format(final,'%Y/%M/%d') AS final FROM periodos WHERE status=1 ORDER BY id DESC "
            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read

                Me.DropDownList1.Items.Add(New ListItem(dtrTeam("id").ToString & " de: " & dtrTeam("inicio").ToString & " a: " & dtrTeam("final").ToString, dtrTeam("id").ToString))

            End While

            sqlConn.Close()
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
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
    Sub llenagrid()
        Me.GridViewresumen.Columns(6).Visible = True
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT pagos.asociado, CONCAT( asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) AS nombre, pagos.bono, pagos.monto, pagos.de, pagos.a, pagos.corte FROM pagos INNER JOIN asociados ON pagos.asociado=asociados.id WHERE pagos.corte=" & Me.DropDownList1.SelectedItem.Value.ToString & " AND pagos.asociado=" & Session("idasociado").ToString & " ORDER BY pagos.asociado ASC, pagos.bono ASC"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Me.GridView1.DataSource = dtrTeam
        Me.GridView1.DataBind()

        sqlConn.Close()
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        calculatotalcompras()
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

            Dim asociado As String = Session("idasociado").ToString
            strTeamQuery = "SELECT SUM(monto), bono FROM pagos GROUP BY bono, corte, status, asociado HAVING Corte =" & Me.GridViewresumen.Rows(Me.GridViewresumen.SelectedIndex).Cells(0).Text & " AND status=1 AND asociado=" & asociado

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

            Me.bono1.Text = bono1.ToString
            Me.bono2.Text = bono2.ToString
            Me.bono3.Text = bono3.ToString
            Me.bono4.Text = bono4.ToString
            Me.bono5.Text = bono5.ToString
            Me.bono6.Text = bono6.ToString
            Me.bono7.Text = bono7.ToString()
            Me.bono8.Text = bono8.ToString
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
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try
    End Sub
    Sub calculatotalcompras()
        Try
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = "SELECT inicio, final FROM periodos WHERE id=" & Me.GridViewresumen.Rows(Me.GridViewresumen.SelectedIndex).Cells(0).Text

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

            Dim asociado As String = Session("idasociado").ToString
            strTeamQuery = "SELECT SUM(total) FROM compras WHERE statuspago='PAGADO' AND fecha>='" & de.ToString("yyyy/M/d") & "' AND fecha<='" & a.ToString("yyyy/M/d") & "' AND asociado=" & asociado

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
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try
    End Sub
    Sub llenadetalle()
        Try
            Me.GridViewGanancias.SelectedIndex = -1
            Me.GridViewGanancias.Columns(1).Visible = True
            Dim ds As New DataSet
            Dim table As DataTable = ds.Tables.Add("Data")
            table.Columns.Add("bono", GetType(String))
            table.Columns.Add("ganancias", GetType(Decimal))


            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = ""

            Dim asociado As String = Session("idasociado").ToString
            strTeamQuery = "SELECT SUM(monto), bono FROM pagos GROUP BY bono, corte, status, asociado HAVING Corte =" & Me.GridViewresumen.Rows(Me.GridViewresumen.SelectedIndex).Cells(0).Text & " AND status=1 AND asociado=" & asociado

            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()
            Dim bono1, bono2, bono3, bono4, bono5, bono6, bono7, bono8, bono9, total As Decimal


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
            total = bono1 + bono2 + bono3 + bono4 + bono5 + bono6 + bono7 + bono8 + bono9
            table.Rows.Add(New Object() {"Bono Excedente", varbono1})
            table.Rows.Add(New Object() {"Bono por Inscripción", varbono2})
            table.Rows.Add(New Object() {"Bono por Inscripción Infinito", varbono3})
            table.Rows.Add(New Object() {"Bono por Igualación de Volumen", varbono4})
            table.Rows.Add(New Object() {"Bono de Seguimiento", varbono5})
            table.Rows.Add(New Object() {"Bono de Bienestar", varbono6})
            table.Rows.Add(New Object() {"Bono Guía", varbono7})
            table.Rows.Add(New Object() {"Bono por Desarrollo de Red", varbono8})
            table.Rows.Add(New Object() {"Bono Global por Avance de Rango", varbono9})
            table.Rows.Add(New Object() {"Subtotal", total})
            Dim view As DataView = ds.Tables(0).DefaultView
            Me.GridViewGanancias.DataSource = view
            Me.GridViewGanancias.DataBind()
            calculaimpuestos(total)
            Me.periodo.Text = Me.GridViewresumen.Rows(Me.GridViewresumen.SelectedIndex).Cells(0).Text
            Me.periodotext.Text = "Periodo"
            Me.dea.Text = " del " & Me.GridViewresumen.Rows(Me.GridViewresumen.SelectedIndex).Cells(6).Text
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try



    End Sub
    Sub calculaimpuestos(ByVal total As Decimal)
        Try
            Dim ds As New DataSet
            Dim table As DataTable = ds.Tables.Add("Data")
            table.Columns.Add("Impuesto", GetType(String))
            table.Columns.Add("monto", GetType(Decimal))
            Dim isr, iva As Decimal
            If IsNumeric(Me.GridViewresumen.Rows(Me.GridViewresumen.SelectedIndex).Cells(2).Text) Then isr = CDec(Me.GridViewresumen.Rows(Me.GridViewresumen.SelectedIndex).Cells(2).Text)
            If IsNumeric(Me.GridViewresumen.Rows(Me.GridViewresumen.SelectedIndex).Cells(4).Text) Then iva = CDec(Me.GridViewresumen.Rows(Me.GridViewresumen.SelectedIndex).Cells(4).Text)

            table.Rows.Add(New Object() {"Retención ISR", isr})
            table.Rows.Add(New Object() {"Retención IVA", iva})
            table.Rows.Add(New Object() {"TOTAL", total - (iva + isr)})
            Dim view As DataView = ds.Tables(0).DefaultView
            Me.GridViewimpuestos.DataSource = view
            Me.GridViewimpuestos.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridViewresumen_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewresumen.DataBound
        Me.GridViewresumen.Columns(6).Visible = False
        If Me.GridViewresumen.Rows.Count < 10 Then
            Me.PagSiguiente.Enabled = False
            Me.ImageButton2.Enabled = False
        Else
            Me.PagSiguiente.Enabled = True
            Me.ImageButton2.Enabled = True
        End If


    End Sub

    Protected Sub GridViewresumen_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewresumen.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbkBtn As LinkButton = CType(e.Row.Cells(7).Controls(0), LinkButton)
            lbkBtn.Text = e.Row.Cells(6).Text
            If e.Row.Cells(2).Text = "1" Then
                e.Row.Cells(3).Text = (CDec(e.Row.Cells(1).Text) * System.Configuration.ConfigurationManager.AppSettings("iva")).ToString("C")
                e.Row.Cells(4).Text = (CDec(e.Row.Cells(1).Text) * 0.1).ToString("C")
                e.Row.Cells(5).Text = (CDec(e.Row.Cells(1).Text) + CDec(e.Row.Cells(3).Text) - CDec(e.Row.Cells(4).Text)).ToString("C")
                e.Row.Cells(2).Text = ""
            Else
                e.Row.Cells(2).Text = funciones.calculaisr(CDec(e.Row.Cells(1).Text)).ToString("C")
                e.Row.Cells(5).Text = (CDec(e.Row.Cells(1).Text) - CDec(e.Row.Cells(2).Text)).ToString("C")
            End If
        End If
    End Sub
    Protected Sub GridView2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewresumen.SelectedIndexChanged
        llenadetalle()
        PanelDetalle.Visible = True
        PanelResumen.Visible = False
    End Sub

    Protected Sub anterior_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles anterior.Click
        PanelDetalle.Visible = False
        PanelResumen.Visible = True
        Me.GridViewresumen.SelectedIndex = -1
        ocultadetalles()
    End Sub

    Protected Sub GridViewGanancias_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewGanancias.DataBound
        Me.GridViewGanancias.Columns(1).Visible = False

    End Sub

    Protected Sub GridViewGanancias_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewGanancias.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbkBtn As LinkButton = CType(e.Row.Cells(2).Controls(0), LinkButton)
            lbkBtn.Text = e.Row.Cells(1).Text
            If IsNumeric(e.Row.Cells(1).Text) Then
                If CDec(e.Row.Cells(1).Text) = 0 Or e.Row.RowIndex = 9 Or e.Row.RowIndex = 8 Then
                    lbkBtn.Enabled = False
                End If
            End If
            If CInt(Me.GridViewresumen.Rows(Me.GridViewresumen.SelectedIndex).Cells(0).Text) < 196 Then
                lbkBtn.Enabled = False
            End If
        End If
    End Sub

    Protected Sub GridViewGanancias_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewGanancias.SelectedIndexChanged

        detallebono(Session("idasociado"), Me.GridViewGanancias.SelectedIndex + 1, CInt(Me.GridViewresumen.Rows(Me.GridViewresumen.SelectedIndex).Cells(0).Text))
    End Sub
#Region "Detalle de pagos"
    Function mispuntosencorte(ByVal corte As Integer, ByVal asociado As Integer) As Integer
        Dim respuesta As Integer = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT MAX(mispuntos) FROM pagos WHERE status=1 AND corte=" & corte.ToString & " AND asociado=" & asociado.ToString
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
        Dim strTeamQuery As String = "SELECT inicio, final FROM periodos WHERE  id=" & corte.ToString
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
                'strTeamQuery = "SELECT referencia AS compra, fecha, total AS monto FROM compras WHERE statuspago='PAGADO' AND excedente=1 AND fecha>='" & inicio.ToString("yyyy/M/dd") & "' AND  fecha<='" & final.ToString("yyyy/M/dd") & "' AND asociado=" & asociado.ToString
                'strTeamQuery = "SELECT compras.referencia AS compra, compras.fecha, compras.total AS monto, SUM(comprasdetalle.cantidad)*200 AS ganancia FROM compras inner join comprasdetalle on compras.id=comprasdetalle.compra WHERE comprasdetalle.paquete=3 AND compras.statuspago='PAGADO' AND compras.excedente=1 AND compras.fecha>='" & inicio.ToString("yyyy/M/dd") & "' AND  compras.fecha<='" & final.ToString("yyyy/M/dd") & "' AND compras.asociado=" & asociado.ToString & " GROUP BY compras.referencia, compras.fecha, compras.total, comprasdetalle.cantidad, comprasdetalle.paquete, compras.statuspago,  compras.excedente, compras.fecha, compras.asociado"
                strTeamQuery = "SELECT compras.referencia AS compra, compras.fecha, compras.total AS monto, SUM(comprasdetalle.cantidad)*200 AS ganancia FROM compras inner join comprasdetalle on compras.id=comprasdetalle.compra WHERE comprasdetalle.paquete=3 AND compras.statuspago='PAGADO' AND compras.excedente=1 AND ((compras.fecha>='" & inicio.ToString("yyyy/M/dd") & "' AND  compras.fecha<='" & final.ToString("yyyy/M/dd") & "') OR (compras.fecha='" & DateAdd(DateInterval.Day, 3, final).ToString("yyyy/M/dd") & "' AND compras.fechaorden<='" & final.ToString("yyyy/M/dd") & "')) AND compras.asociado=" & asociado.ToString & " GROUP BY compras.referencia, compras.fecha, compras.total, comprasdetalle.cantidad, comprasdetalle.paquete, compras.statuspago,  compras.excedente, compras.fecha, compras.asociado"
                sqlConn.Open()
                cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                dtrTeam = cmdFetchTeam.ExecuteReader()

                Me.GridBono1.DataSource = dtrTeam
                Me.GridBono1.DataBind()

                sqlConn.Close()

            Case 2
                Me.labelidasociado2.Text = asociado.ToString
                'strTeamQuery = "SELECT id, CONCAT(nombre, ' ', appaterno, ' ', apmaterno) AS nombre, finsc AS fecha, orden FROM asociados WHERE  finsc>='" & inicio.ToString("yyyy/M/dd") & "' AND  finsc<='" & final.ToString("yyyy/M/dd") & "' AND patrocinador=" & asociado.ToString
                strTeamQuery = "SELECT asociados.id, CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) AS nombre, asociados.finsc AS fecha, asociados.orden, IF(asociados.orden<3,60+IF(compras.puntos=650,50,IF(compras.puntos=900,100,0)), 120+IF(compras.puntos=650,50,IF(compras.puntos=900,100,0))) AS ganancia FROM asociados INNER JOIN compras ON asociados.id=compras.asociado WHERE  asociados.finsc>='" & inicio.ToString("yyyy/M/dd") & "' AND  finsc<='" & final.ToString("yyyy/M/dd") & "' AND asociados.patrocinador=" & asociado.ToString & "  and compras.inscripcion>0 "
                sqlConn.Open()
                cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                dtrTeam = cmdFetchTeam.ExecuteReader()

                Me.GridBono2.DataSource = dtrTeam
                Me.GridBono2.DataBind()

                sqlConn.Close()

            Case 3
                Me.labelidasociado3.Text = asociado.ToString
                strTeamQuery = "SELECT id AS asociado,  CONCAT(nombre, ' ', appaterno, ' ', apmaterno) AS nombre, finsc AS fecha, orden, patrocinador, 60 AS ganancia  " & _
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

            Case 5
                Me.labelidasociado5.Text = asociado.ToString

                strTeamQuery = "SELECT pagos.asociado,  CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) AS nombre, pagos.monto, pagos.monto*.2 AS ganancia "
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

                ' strTeamQuery = "SELECT compras.asociado, CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) AS nombre, compras.referencia AS compra, compras.fecha AS fecha, compras.total AS monto " & _
                '        "FROM compras INNER JOIN asociados ON compras.asociado=asociados.id " & _
                '        "WHERE compras.puntos>0 AND compras.statuspago='PAGADO' AND compras.fecha>='" & inicio.ToString("yyyy/M/dd") & "' AND  compras.fecha<='" & final.ToString("yyyy/M/dd") & "' AND asociados.bono6=" & asociado.ToString
                'para incluir el lunes
                Dim fechafinalperiodo As Date = final
                fechafinalperiodo = DateAdd(DateInterval.Day, 3, fechafinalperiodo)
                Dim fechafinalperiodoanterior As Date = DateAdd(DateInterval.Day, -1, inicio)
                Dim fechalunesanterior As Date = DateAdd(DateInterval.Day, 2, inicio)

                strTeamQuery = "SELECT compras.asociado, CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) AS nombre, compras.referencia AS compra, compras.fecha AS fecha, compras.total AS monto, compras.puntos AS ganancia " & _
                                       "FROM asociados INNER JOIN compras ON asociados.ID = compras.asociado INNER JOIN comprasdetalle ON compras.id=comprasdetalle.compra  " & _
                               "WHERE  comprasdetalle.paquete>0 AND compras.excedente=0  AND compras.statuspago='PAGADO' AND compras.inscripcion=0 " & _
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
                strTeamQuery += "WHERE(compras.puntos > 0) "
                strTeamQuery += "AND compras.statuspago = 'PAGADO' "
                strTeamQuery += "AND compras.excedente = 0  AND compras.inscripcion=0 "
                strTeamQuery += "AND compras.fecha >= '" & inicio.ToString("yyyy/M/dd") & "' "
                strTeamQuery += "AND compras.fecha <= '" & final.ToString("yyyy/M/dd") & "' "
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

                strTeamQuery = "SELECT compras.asociado, CONCAT( asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) AS nombre, compras.referencia AS compra, compras.fecha AS fecha, compras.total AS monto, asociados.patrocinador, compras.puntos  "
                strTeamQuery += "FROM(compras) "
                strTeamQuery += "INNER JOIN asociados ON compras.asociado = asociados.id "
                strTeamQuery += "INNER JOIN asociados AS asociados_1 ON asociados.bono6 = asociados_1.id "
                strTeamQuery += "WHERE(compras.puntos > 0) "
                strTeamQuery += "AND compras.statuspago = 'PAGADO' "
                strTeamQuery += "AND compras.excedente = 0  "
                strTeamQuery += "AND (compras.inscripcion = 0 OR compras.puntos=350 ) "
                strTeamQuery += "AND compras.fecha >= '" & inicio.ToString("yyyy/M/dd") & "' "
                strTeamQuery += "AND compras.fecha <= '" & final.ToString("yyyy/M/dd") & "' "
                strTeamQuery += " And ("
                For i = 0 To directos.Length - 1
                    If i = 0 Then
                        strTeamQuery += " asociados_1.patrocinador =  " & directos(i).ToString
                    Else
                        strTeamQuery += " OR asociados_1.patrocinador =  " & directos(i).ToString

                    End If

                Next
                strTeamQuery += " )"
                'nuevo
                strTeamQuery = "SELECT compras.asociado, CONCAT( asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) AS nombre, compras.referencia AS compra, compras.fecha AS fecha, compras.total AS monto, asociados.patrocinador, compras.puntos AS puntos  "
                strTeamQuery += "FROM(compras) "
                strTeamQuery += "INNER JOIN asociados ON compras.asociado = asociados.id "
                strTeamQuery += "INNER JOIN asociados AS asociados_1 ON asociados.bono6 = asociados_1.id "
                strTeamQuery += "WHERE(compras.puntos > 0) "
                strTeamQuery += "AND compras.statuspago = 'PAGADO' "
                strTeamQuery += "AND compras.excedente = 0 "
                strTeamQuery += "AND compras.fecha >= '" & inicio.ToString("yyyy/M/dd") & "' "
                strTeamQuery += "AND compras.fecha <= '" & final.ToString("yyyy/M/dd") & "' "
                strTeamQuery += " And ("
                For i = 0 To directos.Length - 1
                    If i = 0 Then
                        strTeamQuery += " asociados_1.patrocinador =  " & directos(i).ToString
                    Else
                        strTeamQuery += " OR asociados_1.patrocinador =  " & directos(i).ToString

                    End If

                Next
                strTeamQuery += " )"
                '11 feb 2014
                strTeamQuery = "SELECT compras.asociado, CONCAT( asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) AS nombre, compras.referencia AS compra, compras.fecha AS fecha, compras.total AS monto, asociados.patrocinador, compras.puntos AS puntos  "
                strTeamQuery += "FROM(compras) "
                strTeamQuery += "INNER JOIN asociados ON compras.asociado = asociados.id "
                strTeamQuery += "INNER JOIN asociados AS asociados_1 ON asociados.bono6 = asociados_1.id "
                strTeamQuery += "WHERE(compras.puntos > 0) "
                strTeamQuery += "AND compras.statuspago = 'PAGADO' "
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
        End Select

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
            End Select
        End If
    End Sub
#End Region
#Region "Grids detalle"
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
        Dim strTeamQuery As String = "SELECT inicio, final FROM periodos WHERE status=1 AND id=" & Me.periodo.Text
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
 "AND puntosasociados.asociado= " & Session("idasociado").ToString & " " & _
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
            e.Row.Cells(5).Text = funcion.montodepagobono6(CInt(Left(e.Row.Cells(2).Text, e.Row.Cells(2).Text.Length - 1)), mispuntos, pago1bono6, pago2bono6, pagominimobono678).ToString("c")




        End If
    End Sub



    Protected Sub GridBono7_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridBono7.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim funcion As New funciones
            e.Row.Cells(5).Text = funcion.montodepagobono7(CInt(Left(e.Row.Cells(2).Text, e.Row.Cells(2).Text.Length - 1)), mispuntos, pago1bono7, pago2bono7, pagominimobono678).ToString("c")



        End If
    End Sub

    Protected Sub GridBono8_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridBono8.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim funcion As New funciones
            e.Row.Cells(5).Text = funcion.montodepagobono8(CInt(Left(e.Row.Cells(2).Text, e.Row.Cells(2).Text.Length - 1)), mispuntos, pago1bono8, pago2bono8, pagominimobono678).ToString("c")




        End If
    End Sub
#End Region

    Protected Sub GridBono4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridBono4.SelectedIndexChanged

    End Sub

    Protected Sub GridBono8_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridBono8.SelectedIndexChanged

    End Sub

    Protected Sub GridBono6_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridBono6.SelectedIndexChanged

    End Sub

  
    Protected Sub PagAnterior_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles PagAnterior.Click
        pag = CInt(Request.QueryString("page"))
        pag = pag - 1
        Response.Redirect("mispagos.aspx?page=" & pag.ToString)
    End Sub

    Protected Sub PagSiguiente_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles PagSiguiente.Click
        pag = CInt(Request.QueryString("page"))
        pag = pag + 1
        Response.Redirect("mispagos.aspx?page=" & pag.ToString)
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Response.Redirect("mispagos.aspx")
    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Dim paginafinal As Integer = 0
        Dim numpagos As Integer = funciones.numpagos(Session("idasociado"))
        paginafinal = numpagos / 10
        If numpagos Mod 10 > 0 Then paginafinal += 1
        paginafinal -= 1
        Response.Redirect("mispagos.aspx?page=" & paginafinal.ToString)
    End Sub
End Class
