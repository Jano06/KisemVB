Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class sw_reporteavancesrangos
    Inherits System.Web.UI.Page
    Dim viewasociados As New DataView()
    Dim iniciociclo, mediociclo, finciclo As Date ' para ciclos de calificación
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Avances por Ciclo"
            llenaciclos()

        End If

    End Sub
    Sub llenaciclos()
        Try
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = "SELECT id,  Date_format(inicio,'%Y/%M/%d') AS inicio,   Date_format(fin,'%Y/%M/%d') AS final FROM ciclos ORDER BY id DESC "
            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read

                Me.ciclos.Items.Add(New ListItem("de: " & dtrTeam("inicio").ToString & " a: " & dtrTeam("final").ToString, dtrTeam("id").ToString))

            End While

            sqlConn.Close()
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim claveciclo As Integer = Me.ciclos.SelectedItem.Value
        Try
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = "SELECT Date_format(inicio,'%Y/%M/%d') AS inicio,   Date_format(medio,'%Y/%M/%d') AS medio,   Date_format(fin,'%Y/%M/%d') AS final FROM ciclos WHERE id=" & claveciclo.ToString
            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read

                iniciociclo = dtrTeam(0)
                mediociclo = dtrTeam(1)
                finciclo = dtrTeam(2)

            End While

            sqlConn.Close()
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try
      
        llenagrid()

    End Sub
    Sub llenavistadeasociados()
        'estado = "Inicia Llena vista de asociados"
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, CONCAT( nombre, ' ', appaterno, ' ', apmaterno ) AS nombre, patrocinador, orden, ptsmes, bono6, rango FROM asociados "
        If Not Me.txtasociado.Text = Nothing Then
            Dim asociado() As String = Split(Me.txtasociado.Text, " ")
            strTeamQuery += " WHERE id=" & asociado(0)
        End If



        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        Dim objAdapter As New MySqlDataAdapter
        objAdapter.SelectCommand = cmdFetchTeam

        sqlConn.Open()
        Dim objDS As New DataSet
        objAdapter.Fill(objDS, "asociados")

        sqlConn.Close()
        sqlConn.Dispose()
        viewasociados = New DataView(objDS.Tables(0))

    End Sub
    Sub llenagrid()
        Me.detalle.Text = ""
        Me.GridView2.SelectedIndex = -1
        llenavistadeasociados()
        Dim ds As New DataSet
        Dim table As DataTable = ds.Tables.Add("Data")
        table.Columns.Add("asociado", GetType(Integer))
        table.Columns.Add("nombre", GetType(String))
        table.Columns.Add("directosd", GetType(Integer))
        table.Columns.Add("directosi", GetType(Integer))
        table.Columns.Add("puntosd", GetType(Integer))
        table.Columns.Add("puntosi", GetType(Integer))
        table.Columns.Add("mispuntos", GetType(Integer))
        table.Columns.Add("directosd2", GetType(Integer))
        table.Columns.Add("directosi2", GetType(Integer))
        table.Columns.Add("puntosd2", GetType(Integer))
        table.Columns.Add("puntosi2", GetType(Integer))
        table.Columns.Add("mispuntos2", GetType(Integer))
        Dim drv As DataRowView
        Dim idasociado As Integer = 0
        For Each drv In viewasociados
            Try



                idasociado = drv("id")

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



                table.Rows.Add(New Object() {idasociado, drv("nombre"), directosder, directosizq, puntosgrupalesder, puntosgrupalesizq, mipuntaje, directosder2, directosizq2, puntosgrupalesder2, puntosgrupalesizq2, mipuntaje2})
            Catch ex As Exception
                'Me.mensajes.text += "-" & idasociado.ToString & ex.Message.ToString
            End Try
        Next
        Dim viewreporte As DataView = ds.Tables(0).DefaultView
        Me.GridView2.DataSource = viewreporte
        Me.GridView2.DataBind()
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.SelectedIndexChanged
        Me.detalle.Text = ""
        llenadetalle()
    End Sub
    Function obtieneporcentajedebalance() As Integer
        Dim porcentajedebalance As Integer = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, CONCAT( nombre, ' ', appaterno, ' ', apmaterno ) AS nombre, patrocinador, orden, ptsmes, bono6, rango FROM asociados "

        '% de balanceo
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        Dim querybalanceo As String = "SELECT porcentajedebalance FROM configuracion"
        cmdFetchTeam = New MySqlCommand(querybalanceo, sqlConn)



        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            porcentajedebalance = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Return porcentajedebalance
    End Function

    Sub llenadetalle()
        'hasta acá
        Dim puntosmensualesporladorango2 As Integer = 4900
        Dim puntosmensualesporladorango3 As Integer = 9100
        Dim puntosmensualesporladorango4 As Integer = 17500
        Dim puntosmensualesporladorango5 As Integer = 34300
        Dim puntosmensualesporladorango6 As Integer = 67900
        Dim puntosmensualesporladorango7 As Integer = 140000
        Dim puntosmensualesporladorango8 As Integer = 420000
        Dim puntosmensualesporladorango9 As Integer = 840000
        Dim balancemes1, balancemes2 As Boolean



        Dim directosder As Integer = CInt(Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(3).Text)
        Dim directosizq As Integer = CInt(Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(2).Text)
        Dim directosder2 As Integer = CInt(Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(8).Text)
        Dim directosizq2 As Integer = CInt(Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(7).Text)
        Dim puntosgrupalesder As Integer = CInt(Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(5).Text)
        Dim puntosgrupalesizq As Integer = CInt(Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(4).Text)
        Dim mipuntaje As Integer = CInt(Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(6).Text)
        Dim puntosgrupalesder2 As Integer = CInt(Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(10).Text)
        Dim puntosgrupalesizq2 As Integer = CInt(Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(9).Text)
        Dim mipuntaje2 As Integer = CInt(Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(11).Text)
        Dim rangoactual As Integer = 1
        Dim puntosgrupales As Integer = puntosgrupalesder + puntosgrupalesizq + mipuntaje
        Dim puntosgrupales2 As Integer = puntosgrupalesder2 + puntosgrupalesizq2 + mipuntaje2

        Dim porcentajedebalance As Integer = obtieneporcentajedebalance




     


        'revisa rango 2
        If puntosgrupales >= puntosmensualesporladorango2 And puntosgrupales2 >= puntosmensualesporladorango2 And directosder >= 1 And directosizq >= 1 And directosder2 >= 1 And directosizq2 >= 1 Then
            'checa ahora el balance
            'mes 1
            If puntosgrupalesder < puntosgrupalesizq Then
                If puntosgrupalesder >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango2) Then
                    balancemes1 = True
                End If
            Else
                If puntosgrupalesizq >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango2) Then
                    balancemes1 = True
                End If
            End If
            'mes 2
            If puntosgrupalesder2 < puntosgrupalesizq2 Then
                If puntosgrupalesder2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango2) Then
                    balancemes2 = True
                End If
            Else
                If puntosgrupalesizq2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango2) Then
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
        Dim rangotexto As String = ""
         Select rangoactual
            Case 1
                rangotexto = "Colaborador "
            Case 2
                rangotexto = "Colaborador Ejecutivo"
            Case 3
                rangotexto = "Bronce"
            Case 4
                rangotexto = "Plata"
            Case 5
                rangotexto = "Oro"
            Case 6
                rangotexto = "Diamante"
            Case 7
                rangotexto = "Diamante Ejecutivo"
            Case 8
                rangotexto = "Diamante Internacional"
        End Select
        Me.detalle.Text = "No pudiste llegar a " & rangotexto
        'agrega rango donde se quedó y cuál es su rango de pago
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT rango, rangopago FROM rangoscambios WHERE asociado=" & Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(0).Text & " AND ciclo=" & Me.ciclos.SelectedItem.Value.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim strrango, strrangopago As Integer
        While dtrTeam.Read
            strrango = dtrTeam(0)
            strrangopago = dtrTeam(1)

        End While

        sqlConn.Close()
        Select Case strrango
            Case 1
                rangotexto = "Asociado "
            Case 2
                rangotexto = "Colaborador "
            Case 3
                rangotexto = "Colaborador Ejecutivo"
            Case 4
                rangotexto = "Bronce"
            Case 5
                rangotexto = "Plata"
            Case 6
                rangotexto = "Oro"
            Case 7
                rangotexto = "Diamante"
            Case 8
                rangotexto = "Diamante Ejecutivo"
            Case 9
                rangotexto = "Diamante Internacional"
        End Select



        Me.detalle.Text += "<br/>Te quedaste en el rango " & rangotexto
        Select Case strrangopago
            Case 1
                rangotexto = "Asociado "
            Case 2
                rangotexto = "Colaborador "
            Case 3
                rangotexto = "Colaborador Ejecutivo"
            Case 4
                rangotexto = "Bronce"
            Case 5
                rangotexto = "Plata"
            Case 6
                rangotexto = "Oro"
            Case 7
                rangotexto = "Diamante"
            Case 8
                rangotexto = "Diamante Ejecutivo"
            Case 9
                rangotexto = "Diamante Internacional"
        End Select
        Me.detalle.Text += "<br/>Se te pagará como " & rangotexto
        If mipuntaje < 700 Then
            Me.detalle.Text += "<br/>No tengo el suficiente volumen personal en el mes 1 "

        End If

        If mipuntaje2 < 700 Then
            Me.detalle.Text += "<br/>No tengo el suficiente volumen personal en el mes 2 "

        End If

        Select Case rangoactual
            Case 1
                If puntosgrupales < puntosmensualesporladorango2 Then
                    Me.detalle.Text += "<br/>No tengo el suficiente volumen grupal en el mes 1 "
                End If
                If puntosgrupales2 < puntosmensualesporladorango2 Then
                    Me.detalle.Text += "<br/>No tengo el suficiente volumen grupal en el mes 2 "
                End If
                If directosder = 0 Or directosizq = 0 Then
                    Me.detalle.Text += "<br/>No tengo Los suficientes invitados en el mes 1 "
                End If
                If directosder2 = 0 Or directosizq2 = 0 Then
                    Me.detalle.Text += "<br/>No tengo Los suficientes invitados en el mes 2 "
                End If
                If puntosgrupalesder < puntosgrupalesizq Then
                    If puntosgrupalesder >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango2) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 1 "
                    End If
                Else
                    If puntosgrupalesizq >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango2) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 1 "
                    End If
                End If
                If puntosgrupalesder2 < puntosgrupalesizq2 Then
                    If puntosgrupalesder2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango2) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 2 "
                    End If
                Else
                    If puntosgrupalesizq2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango2) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 2 "
                    End If
                End If

            Case 2
                If puntosgrupales < puntosmensualesporladorango3 Then
                    Me.detalle.Text += "<br/>No tengo el suficiente volumen grupal en el mes 1 "
                End If
                If puntosgrupales2 < puntosmensualesporladorango3 Then
                    Me.detalle.Text += "<br/>No tengo el suficiente volumen grupal en el mes 2 "
                End If
                If directosder = 0 Or directosizq = 0 Or directosder + directosizq < 3 Then
                    Me.detalle.Text += "<br/>No tengo Los suficientes invitados en el mes 1 "
                End If
                If directosder2 = 0 Or directosizq2 = 0 Or directosder2 + directosizq2 < 3 Then
                    Me.detalle.Text += "<br/>No tengo Los suficientes invitados en el mes 2 "
                End If
                If puntosgrupalesder < puntosgrupalesizq Then
                    If puntosgrupalesder >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango3) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 1 "
                    End If
                Else
                    If puntosgrupalesizq >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango3) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 1 "
                    End If
                End If
                If puntosgrupalesder2 < puntosgrupalesizq2 Then
                    If puntosgrupalesder2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango3) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 2 "
                    End If
                Else
                    If puntosgrupalesizq2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango3) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 2 "
                    End If
                End If

            Case 3
                If puntosgrupales < puntosmensualesporladorango4 Then
                    Me.detalle.Text += "<br/>No tengo el suficiente volumen grupal en el mes 1 "
                End If
                If puntosgrupales2 < puntosmensualesporladorango4 Then
                    Me.detalle.Text += "<br/>No tengo el suficiente volumen grupal en el mes 2 "
                End If
                If directosder = 0 Or directosizq = 0 Or directosder + directosizq < 3 Then
                    Me.detalle.Text += "<br/>No tengo Los suficientes invitados en el mes 1 "
                End If
                If directosder2 = 0 Or directosizq2 = 0 Or directosder2 + directosizq2 < 3 Then
                    Me.detalle.Text += "<br/>No tengo Los suficientes invitados en el mes 2 "
                End If
                If puntosgrupalesder < puntosgrupalesizq Then
                    If puntosgrupalesder >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango4) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 1 "
                    End If
                Else
                    If puntosgrupalesizq >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango4) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 1 "
                    End If
                End If
                If puntosgrupalesder2 < puntosgrupalesizq2 Then
                    If puntosgrupalesder2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango4) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 2 "
                    End If
                Else
                    If puntosgrupalesizq2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango4) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 2 "
                    End If
                End If


            Case 4
                If puntosgrupales < puntosmensualesporladorango5 Then
                    Me.detalle.Text += "<br/>No tengo el suficiente volumen grupal en el mes 1 "
                End If
                If puntosgrupales2 < puntosmensualesporladorango5 Then
                    Me.detalle.Text += "<br/>No tengo el suficiente volumen grupal en el mes 2 "
                End If
                If directosder = 0 Or directosizq = 0 Or directosder + directosizq < 3 Then
                    Me.detalle.Text += "<br/>No tengo Los suficientes invitados en el mes 1 "
                End If
                If directosder2 = 0 Or directosizq2 = 0 Or directosder2 + directosizq2 < 3 Then
                    Me.detalle.Text += "<br/>No tengo Los suficientes invitados en el mes 2 "
                End If
                If puntosgrupalesder < puntosgrupalesizq Then
                    If puntosgrupalesder >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango5) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 1 "
                    End If
                Else
                    If puntosgrupalesizq >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango5) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 1 "
                    End If
                End If
                If puntosgrupalesder2 < puntosgrupalesizq2 Then
                    If puntosgrupalesder2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango5) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 2 "
                    End If
                Else
                    If puntosgrupalesizq2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango5) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 2 "
                    End If
                End If


            Case 5
                If puntosgrupales < puntosmensualesporladorango6 Then
                    Me.detalle.Text += "<br/>No tengo el suficiente volumen grupal en el mes 1 "
                End If
                If puntosgrupales2 < puntosmensualesporladorango6 Then
                    Me.detalle.Text += "<br/>No tengo el suficiente volumen grupal en el mes 2 "
                End If
                If directosder < 2 Or directosizq < 2 Or directosder + directosizq < 4 Then
                    Me.detalle.Text += "<br/>No tengo Los suficientes invitados en el mes 1 "
                End If
                If directosder2 < 2 Or directosizq2 < 2 Or directosder2 + directosizq2 < 4 Then
                    Me.detalle.Text += "<br/>No tengo Los suficientes invitados en el mes 2 "
                End If
                If puntosgrupalesder < puntosgrupalesizq Then
                    If puntosgrupalesder >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango6) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 1 "
                    End If
                Else
                    If puntosgrupalesizq >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango6) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 1 "
                    End If
                End If
                If puntosgrupalesder2 < puntosgrupalesizq2 Then
                    If puntosgrupalesder2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango6) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 2 "
                    End If
                Else
                    If puntosgrupalesizq2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango6) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 2 "
                    End If
                End If

            Case 6
                If puntosgrupales < puntosmensualesporladorango7 Then
                    Me.detalle.Text += "<br/>No tengo el suficiente volumen grupal en el mes 1 "
                End If
                If puntosgrupales2 < puntosmensualesporladorango7 Then
                    Me.detalle.Text += "<br/>No tengo el suficiente volumen grupal en el mes 2 "
                End If
                If directosder < 2 Or directosizq < 2 Or directosder + directosizq < 4 Then
                    Me.detalle.Text += "<br/>No tengo Los suficientes invitados en el mes 1 "
                End If
                If directosder2 < 2 Or directosizq2 < 2 Or directosder2 + directosizq2 < 4 Then
                    Me.detalle.Text += "<br/>No tengo Los suficientes invitados en el mes 2 "
                End If
                If puntosgrupalesder < puntosgrupalesizq Then
                    If puntosgrupalesder >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango7) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 1 "
                    End If
                Else
                    If puntosgrupalesizq >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango7) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 1 "
                    End If
                End If
                If puntosgrupalesder2 < puntosgrupalesizq2 Then
                    If puntosgrupalesder2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango7) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 2 "
                    End If
                Else
                    If puntosgrupalesizq2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango7) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 2 "
                    End If
                End If


            Case 7
                If puntosgrupales < puntosmensualesporladorango8 Then
                    Me.detalle.Text += "<br/>No tengo el suficiente volumen grupal en el mes 1 "
                End If
                If puntosgrupales2 < puntosmensualesporladorango8 Then
                    Me.detalle.Text += "<br/>No tengo el suficiente volumen grupal en el mes 2 "
                End If
                If directosder < 2 Or directosizq < 2 Or directosder + directosizq < 4 Then
                    Me.detalle.Text += "<br/>No tengo Los suficientes invitados en el mes 1 "
                End If
                If directosder2 < 2 Or directosizq2 < 2 Or directosder2 + directosizq2 < 4 Then
                    Me.detalle.Text += "<br/>No tengo Los suficientes invitados en el mes 2 "
                End If
                If puntosgrupalesder < puntosgrupalesizq Then
                    If puntosgrupalesder >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango8) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 1 "
                    End If
                Else
                    If puntosgrupalesizq >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango8) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 1 "
                    End If
                End If
                If puntosgrupalesder2 < puntosgrupalesizq2 Then
                    If puntosgrupalesder2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango8) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 2 "
                    End If
                Else
                    If puntosgrupalesizq2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango8) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 2 "
                    End If
                End If

            Case 8
                If puntosgrupales < puntosmensualesporladorango9 Then
                    Me.detalle.Text += "<br/>No tengo el suficiente volumen grupal en el mes 1 "
                End If
                If puntosgrupales2 < puntosmensualesporladorango9 Then
                    Me.detalle.Text += "<br/>No tengo el suficiente volumen grupal en el mes 2 "
                End If
                If directosder < 2 Or directosizq < 2 Or directosder + directosizq < 4 Then
                    Me.detalle.Text += "<br/>No tengo Los suficientes invitados en el mes 1 "
                End If
                If directosder2 < 2 Or directosizq2 < 2 Or directosder2 + directosizq2 < 4 Then
                    Me.detalle.Text += "<br/>No tengo Los suficientes invitados en el mes 2 "
                End If
                If puntosgrupalesder < puntosgrupalesizq Then
                    If puntosgrupalesder >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango9) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 1 "
                    End If
                Else
                    If puntosgrupalesizq >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango9) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 1 "
                    End If
                End If
                If puntosgrupalesder2 < puntosgrupalesizq2 Then
                    If puntosgrupalesder2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango9) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 2 "
                    End If
                Else
                    If puntosgrupalesizq2 >= (((100 - porcentajedebalance) / 100) * puntosmensualesporladorango9) Then
                    Else
                        Me.detalle.Text += "<br/>No cumplo con la condición de balance en el mes 2 "
                    End If
                End If
        End Select
    End Sub

End Class
