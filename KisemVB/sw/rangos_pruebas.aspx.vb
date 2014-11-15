Imports MySql.Data.MySqlClient
Imports System.Data
Partial Class sw_rangos_pruebas
    Inherits System.Web.UI.Page
    Dim ds As New DataSet
    Dim table As DataTable = ds.Tables.Add("Data")
    Dim dsresumen As New DataSet
    Dim tableresumen As DataTable = dsresumen.Tables.Add("Data")
    Dim rango1, rango2, rango3, rango4, rango5, rango6, rango7, rango8, rango9 As Integer
    Dim rango1p, rango2p, rango3p, rango4p, rango5p, rango6p, rango7p, rango8p, rango9p As Integer

    Sub checarango(ByVal idasociado As Integer, ByVal rango As Integer, ByVal nombre As String)
        Session("mensaje") = "El último en checar rango fue el Asociado " & idasociado.ToString & " Rango " & rango.ToString
        Dim de As Date = DateAdd(DateInterval.Day, -35, CDate(Me.de.Text))

        'Dim qry_paquetesypuntos As String = "SELECT comprasdetalle.cantidad, comprasdetalle.paquete, asociados.recorrido, asociados.ladosrecorrido " & _
        '"FROM asociados INNER JOIN compras ON asociados.id=compras.asociado INNER JOIN `comprasdetalle` ON compras.id=comprasdetalle.compra " & _
        '"WHERE asociados.recorrido LIKE '%." & idasociado.ToString & ".%' AND comprasdetalle.paquete BETWEEN 1 AND 3 AND compras.statuspago='PAGADO' AND compras.fecha>='" & de.ToString("yyyy/MM/dd") & "' AND compras.fecha<='" & Me.a.Text & "'"

        'nuevo, incluyendo pago hasta el siguiente lunes
        Dim qry_paquetesypuntos As String = "SELECT comprasdetalle.cantidad, comprasdetalle.paquete, asociados.recorrido, asociados.ladosrecorrido " & _
      "FROM asociados INNER JOIN compras ON asociados.id=compras.asociado INNER JOIN `comprasdetalle` ON compras.id=comprasdetalle.compra " & _
      "WHERE asociados.recorrido LIKE '%." & idasociado.ToString & ".%' AND comprasdetalle.paquete BETWEEN 1 AND 3 AND compras.statuspago='PAGADO' AND compras.fecha>='" & de.ToString("yyyy/MM/dd") & "' AND (compras.fecha<='" & Me.a.Text & "' OR (compras.fechaorden BETWEEN '" & de.ToString("yyyy/MM/dd") & "' AND '" & Me.a.Text & "' AND (compras.fecha='" & DateAdd(DateInterval.Day, 1, CDate(Me.a.Text)).ToString("yyyy/MM/dd") & "' OR compras.fecha='" & DateAdd(DateInterval.Day, 2, CDate(Me.a.Text)).ToString("yyyy/MM/dd") & "' OR compras.fecha='" & DateAdd(DateInterval.Day, 3, CDate(Me.a.Text)).ToString("yyyy/MM/dd") & "'  ))) " & _
      "AND compras.id NOT IN (" & _
                    "Select id FROM compras " & _
                    "WHERE fechaorden < '" & de.ToString("yyyy/MM/dd") & "' " & _
                    "AND fecha < '" & DateAdd(DateInterval.Day, 2, de).ToString("yyyy/MM/dd") & "' " & _
                    ") "

        Dim qry_directosporlado As String = "SELECT Count(asociados.ID) AS directos, asociados.ladopatrocinador AS lado " & _
                                                   "FROM(asociados) " & _
                                                   "WHERE(((asociados.patrocinador)=" & idasociado.ToString & ") AND (asociados.status=1  OR '" & a.Text & "' BETWEEN inicioactivacion AND finactivacion)  ) AND asociados.ptsmes>350 " & _
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


        actualizarango(idasociado, rango, rangoactual, nombre, izq, der, directosizq, directosder, inactivosizq, inactivosder)
        Select Case rango
            Case 1
                rango1 += 1
            Case 2
                rango2 += 1
            Case 3
                rango3 += 1
            Case 4
                rango4 += 1
            Case 5
                rango5 += 1
            Case 6
                rango6 += 1
            Case 7
                rango7 += 1
            Case 8
                rango8 += 1
            Case 9
                rango9 += 1
        End Select
        Select Case rangoactual
            Case 1
                rango1p += 1
            Case 2
                rango2p += 1
            Case 3
                rango3p += 1
            Case 4
                rango4p += 1
            Case 5
                rango5p += 1
            Case 6
                rango6p += 1
            Case 7
                rango7p += 1
            Case 8
                rango8p += 1
            Case 9
                rango9p += 1
        End Select
    End Sub
    Sub actualizarango(ByVal asociado As Integer, ByVal rangotitulo As Integer, ByVal rango As Integer, ByVal nombre As String, ByVal izq As Integer, ByVal der As Integer, ByVal directosizq As Integer, ByVal directosder As Integer, ByVal inactivosizq As Integer, ByVal inactivosder As Integer)


        table.Rows.Add(New Object() {asociado, nombre, rangotitulo, rango, izq, der, directosizq, directosder, inactivosizq, inactivosder})



    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Prueba de Rangos"
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


        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        revisarangos()
    End Sub
    Sub revisarangos()
        table.Columns.Add("asociado", GetType(Integer))
        table.Columns.Add("nombre", GetType(String))
        table.Columns.Add("rangoanterior", GetType(Integer))
        table.Columns.Add("rangonuevo", GetType(Integer))
        table.Columns.Add("izq", GetType(Integer))
        table.Columns.Add("der", GetType(Integer))
        table.Columns.Add("directosizq", GetType(Integer))
        table.Columns.Add("directosder", GetType(Integer))
        table.Columns.Add("inactivosizq", GetType(Integer))
        table.Columns.Add("inactivosder", GetType(Integer))
        tableresumen.Columns.Add("rango", GetType(String))
        tableresumen.Columns.Add("titulo", GetType(Integer))
        tableresumen.Columns.Add("pago", GetType(Integer))


        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""

        strTeamQuery = "SELECT id, rango, CONCAT(nombre, ' ', appaterno) FROM asociados WHERE status=1 AND id>3 "

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()


        While dtrTeam.Read

            checarango(dtrTeam(0), dtrTeam(1), dtrTeam(2))

        End While

        sqlConn.Close()

        tableresumen.Rows.Add(New Object() {"Asociado", rango1, rango1p})
        tableresumen.Rows.Add(New Object() {"Colaborador", rango2, rango2p})
        tableresumen.Rows.Add(New Object() {"Colaborador Ejecutivo", rango3, rango3p})
        tableresumen.Rows.Add(New Object() {"Bronce", rango4, rango4p})
        tableresumen.Rows.Add(New Object() {"Plata", rango5, rango5p})
        tableresumen.Rows.Add(New Object() {"Oro", rango6, rango6p})
        tableresumen.Rows.Add(New Object() {"Diamante", rango7, rango7p})
        tableresumen.Rows.Add(New Object() {"Diamante Ejecutivo", rango8, rango8p})
        tableresumen.Rows.Add(New Object() {"Diamante Internacional", rango9, rango9p})

        Dim viewresumen As DataView = dsresumen.Tables(0).DefaultView
        Me.GridView2.Columns(1).Visible = True
        Me.GridView2.DataSource = viewresumen
        Me.GridView2.DataBind()
        Me.GridView2.Columns(1).Visible = False

        Me.GridView1.Columns(6).Visible = True
        Me.GridView1.Columns(7).Visible = True
        Me.GridView1.Columns(8).Visible = True
        Me.GridView1.Columns(9).Visible = True
        Dim view As DataView = ds.Tables(0).DefaultView
        Me.GridView1.DataSource = view
        Me.GridView1.DataBind()
        Me.GridView1.Columns(6).Visible = False
        Me.GridView1.Columns(7).Visible = False
        Me.GridView1.Columns(8).Visible = False
        Me.GridView1.Columns(9).Visible = False
        Me.GridView1.Visible = False
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)

        ' Get the last name of the selected author from the appropriate
        ' cell in the GridView control.
        Me.GridView1.SelectedIndex = index
        llenadetalle(e.CommandName)
    End Sub
    Sub llenadetalle(ByVal tipo As String)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""

        strTeamQuery = "SELECT id, CONCAT(nombre, ' ', appaterno) AS nombre, inicioactivacion, finactivacion, ptsmes FROM asociados WHERE 1 "
        Select Case tipo
            Case "PatrocinadosIzq"
                strTeamQuery += "And (status=1 OR '" & a.Text & "' BETWEEN inicioactivacion AND finactivacion) AND ladopatrocinador='I' "
            Case "PatrocinadosDer"
                strTeamQuery += "And  (status=1 OR '" & a.Text & "' BETWEEN inicioactivacion AND finactivacion) AND ladopatrocinador='D' "
            Case "InactivosIzq"
                strTeamQuery += "And status=0 AND ladopatrocinador='I' "
            Case "InactivosDer"
                strTeamQuery += "And status=0 AND ladopatrocinador='D' "
        End Select
        strTeamQuery += "And patrocinador=" & Me.GridView1.Rows(Me.GridView1.SelectedIndex).Cells(0).Text
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Me.GridDetalle.DataSource = dtrTeam
        Me.GridDetalle.DataBind()


        sqlConn.Close()
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Select Case e.Row.Cells(2).Text
                Case "1"
                    e.Row.Cells(2).Text = "Asociado"
                Case "2"
                    e.Row.Cells(2).Text = "Colaborador"
                Case "3"
                    e.Row.Cells(2).Text = "Colaborador Ejecutivo"
                Case "4"
                    e.Row.Cells(2).Text = "Bronce"
                Case "5"
                    e.Row.Cells(2).Text = "Plata"
                Case "6"
                    e.Row.Cells(2).Text = "Oro"
                Case "7"
                    e.Row.Cells(2).Text = "Diamante"
                Case "8"
                    e.Row.Cells(2).Text = "Diamante Ejecutivo"
                Case "9"
                    e.Row.Cells(2).Text = "Diamante Internacional"
            End Select
            Select Case e.Row.Cells(3).Text
                Case "1"
                    e.Row.Cells(3).Text = "Asociado"
                Case "2"
                    e.Row.Cells(3).Text = "Colaborador"
                Case "3"
                    e.Row.Cells(3).Text = "Colaborador Ejecutivo"
                Case "4"
                    e.Row.Cells(3).Text = "Bronce"
                Case "5"
                    e.Row.Cells(3).Text = "Plata"
                Case "6"
                    e.Row.Cells(3).Text = "Oro"
                Case "7"
                    e.Row.Cells(3).Text = "Diamante"
                Case "8"
                    e.Row.Cells(3).Text = "Diamante Ejecutivo"
                Case "9"
                    e.Row.Cells(3).Text = "Diamante Internacional"
            End Select
            'checa patrocinados izq y der
            Dim lbkBtn As LinkButton = CType(e.Row.Cells(10).Controls(0), LinkButton)
            lbkBtn.Text = e.Row.Cells(6).Text
            lbkBtn.CommandName = "PatrocinadosIzq"
            Dim lbkBtn2 As LinkButton = CType(e.Row.Cells(11).Controls(0), LinkButton)
            lbkBtn2.Text = e.Row.Cells(7).Text
            lbkBtn2.CommandName = "PatrocinadosDer"
            Dim lbkBtn3 As LinkButton = CType(e.Row.Cells(12).Controls(0), LinkButton)
            lbkBtn3.Text = e.Row.Cells(8).Text
            lbkBtn3.CommandName = "InactivosIzq"
            Dim lbkBtn4 As LinkButton = CType(e.Row.Cells(13).Controls(0), LinkButton)
            lbkBtn4.Text = e.Row.Cells(9).Text
            lbkBtn4.CommandName = "InactivosDer"
        End If
    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        For i = 0 To Me.GridView2.Rows.Count - 1
            Dim lb As LinkButton = CType(GridView2.Rows(i).Controls(0).Controls(0), LinkButton)
            lb.Text = Me.GridView2.Rows(i).Cells(1).Text

        Next
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.SelectedIndexChanged
        Me.GridDetalle.DataSource = ""
        Me.GridDetalle.DataBind()
        Me.GridView1.Visible = True
        Dim renglon As GridViewRow
        For Each renglon In GridView1.Rows
            If renglon.Cells(2).Text = Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(1).Text Or renglon.Cells(3).Text = Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(1).Text Then
                renglon.Visible = True
            Else
                renglon.Visible = False
            End If

        Next

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class
