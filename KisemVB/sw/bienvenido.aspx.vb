Imports MySql.Data.MySqlClient
Imports System.Data
Partial Class sw_Default2
    Inherits System.Web.UI.Page
    Dim funciones As New funciones
    Dim mirango As Integer
    Dim de, a As Date
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
       
        Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
        objEtiqueta.Text = "Bienvenido"
        If Not IsPostBack Then

            Me.nombreasociado.Text = Session("nombreasociado")
            Me.idasociado.Text = Session("idasociado")
            checarango()
            checaactividad()
            'funciones.requisitosrango(mirango + 1, Me.activosporlado, Me.activosenorganizacion, Me.volumengrupal, Me.volumenpersonal, Me.porcentajedebalance, Me.siguienterango)
            requisitosrango(mirango + 1)

            checapatrocinador()
           
            cuantofaltaparalafecha(funciones.proximosabado)
            'mis logros
            cuantofaltaparaelcierre(funciones.cierredecalificacion)
            llenagridrequisitos()
        End If

    End Sub
    Sub requisitosrango(ByVal rango As Integer)
        Select Case rango
            Case 2
                Me.activosporlado.Text = "3 Asociados activos o más por lado"
                Me.activosenorganizacion.Text = "1 Patrocinado directo o más de cada lado"
                Me.siguienterango.Text = "Colaborador"
                Me.emprendedoresporlado.Text = "2 Emprendedores/Empresarios directos o más de un lado y 1 Emprendedor/Empresario directo o más del otro lado"

            Case 3
                Me.activosporlado.Text = "10 Asociados activos o más por lado"
                Me.activosenorganizacion.Text = "1 Patrocinado directo o más de cada lado"
                Me.siguienterango.Text = "Colaborador Ejecutivo"
                Me.emprendedoresporlado.Text = "2 Emprendedores/Empresarios directos o más de un lado y 1 Emprendedor/Empresario directo o más del otro lado"
                Me.puntajeenorganizacion.Text = "Un volúmen total de puntos para calificación de 550 y mínimo 165 puntos en el lado menor"
            Case 4
                Me.activosporlado.Text = "20 Asociados activos o más por lado"
                Me.activosenorganizacion.Text = "2 Patrocinados directos o más de un lado y 1 patrocinado directo o más del otro lado"
                Me.siguienterango.Text = "Bronce"
                Me.emprendedoresporlado.Text = "2 Emprendedores/Empresarios directos o más de un lado y 1 Emprendedor/Empresario directo o más del otro lado"
                Me.puntajeenorganizacion.Text = "Un volúmen total de puntos para calificación de 1300 y mínimo 390 puntos en el lado menor"
            Case 5
                Me.activosporlado.Text = "50 Asociados activos o más por lado"
                Me.activosenorganizacion.Text = "2 Patrocinados directos o más de un lado y 1 patrocinado directo o más del otro lado"
                Me.siguienterango.Text = "Plata"
                Me.emprendedoresporlado.Text = "2 Emprendedores/Empresarios directos o más de un lado y 1 Emprendedor/Empresario directo o más del otro lado"
                Me.puntajeenorganizacion.Text = "Un volúmen total de puntos para calificación de 2500 y mínimo 750 puntos en el lado menor"
            Case 6
                Me.activosporlado.Text = "75 Asociados activos o más por lado"
                Me.activosenorganizacion.Text = "2 Patrocinados directos o más de cada lado "
                Me.siguienterango.Text = "Oro"
                Me.emprendedoresporlado.Text = "2 Emprendedores/Empresarios directos por lado "
                Me.puntajeenorganizacion.Text = "Un volúmen total de puntos para calificación de 5600 y mínimo 1680 puntos en el lado menor"

            Case 7
                Me.activosporlado.Text = "160 Asociados activos o más por lado"
                Me.activosenorganizacion.Text = "2 Patrocinados directos o más de cada lado "
                Me.siguienterango.Text = "Diamante"
                Me.emprendedoresporlado.Text = "2 Emprendedores/Empresarios directos por lado "
                Me.puntajeenorganizacion.Text = "Un volúmen total de puntos para calificación de 11250 y mínimo 3375 puntos en el lado menor"

            Case 8
                Me.activosporlado.Text = "400 Asociados activos o más por lado"
                Me.activosenorganizacion.Text = "2 Patrocinados directos o más de cada lado "
                Me.siguienterango.Text = "Diamante Ejecutivo"
                Me.emprendedoresporlado.Text = "2 Emprendedores/Empresarios directos por lado "
                Me.puntajeenorganizacion.Text = "Un volúmen total de puntos para calificación de 22500 y mínimo 9000 puntos en el lado menor"

            Case 9
                Me.activosporlado.Text = "800 Asociados activos o más por lado"
                Me.activosenorganizacion.Text = "2 Patrocinados directos o más de cada lado "
                Me.siguienterango.Text = "Diamante Internacional"
                Me.emprendedoresporlado.Text = "2 Emprendedores/Empresarios directos por lado "
                Me.puntajeenorganizacion.Text = "Un volúmen total de puntos para calificación de 45000 y mínimo 18000 puntos en el lado menor"

        End Select

    End Sub
    Sub checaactividad()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT status, finactivacion " & _
                                     "FROM asociados   " & _
                                     "WHERE id =" & Session("idasociado").ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim status As Integer = 0
        Dim fin As Date
        While dtrTeam.Read
            status = dtrTeam(0)
            If IsDate(dtrTeam(1)) Then fin = dtrTeam(1)

        End While

        sqlConn.Close()

        Select Case status
            Case 0
                Me.estado.Text = "Inactivo."
            Case 1
                Me.estado.Text = "Activo."
            Case 2
                Me.estado.Text = "Suspendido."

        End Select
        If status = 0 And IsDate(fin) And fin > CDate("1910/01/01") Then
            Me.inactividad.Text = "Llevas inactivo " & DateDiff(DateInterval.Day, fin, Today).ToString & " días"
        End If
    End Sub
    Sub cuantofaltaparalafecha(ByVal fecha As Date)
        Dim dias As Integer = DateDiff(DateInterval.Day, Today, fecha) - 1
        Dim horas As Integer = 23 - Now.Hour
        Dim minutos As Integer = 59 - Now.Minute
        Dim segundos = 59 - Now.Second
        Me.numdias.Text = dias.ToString
        Me.numhoras.Text = horas.ToString
        Me.numminutos.Text = minutos.ToString
        Me.numsegundos.Text = segundos.ToString

    End Sub
    Sub cuantofaltaparaelcierre(ByVal fecha As Date)
        Dim dias As Integer = DateDiff(DateInterval.Day, Today, fecha)
        Dim horas As Integer = 23 - Now.Hour
        Dim minutos As Integer = 59 - Now.Minute
        Dim segundos = 59 - Now.Second
        Me.numdias0.Text = dias.ToString
        Me.numhoras0.Text = horas.ToString
        Me.numminutos0.Text = minutos.ToString
        Me.numsegundos0.Text = segundos.ToString

    End Sub


    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim segundos As Integer = CInt(Me.numsegundos.Text)
        Dim dias As Integer = CInt(Me.numdias.Text)
        Dim horas As Integer = CInt(Me.numhoras.Text)
        Dim minutos As Integer = CInt(Me.numminutos.Text)
        segundos -= 1
        If segundos = -0 Then
            segundos = 59
            minutos -= 1
        End If
        If minutos = -1 Then
            minutos = 59
            horas -= 1
        End If
        If horas = -1 Then
            horas = 23
            dias -= 1
        End If
        If dias = -1 Then
            dias = 6
            horas = 23
            minutos = 59
            segundos = 59
        End If
        Me.numdias.Text = dias.ToString
        Me.numhoras.Text = horas.ToString
        Me.numminutos.Text = minutos.ToString
        Me.numsegundos.Text = segundos.ToString

     
    End Sub
    Sub checarango()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT rangos.id, rangos.nombre " & _
                                     "FROM asociados INNER JOIN rangos ON asociados.rango=rangos.id " & _
                                     "WHERE asociados.id =" & Session("idasociado").ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            mirango = dtrTeam(0)
            Me.lblnombrerango.Text = dtrTeam(1).ToString
            Me.rango.ImageUrl = "img/rangos/" & dtrTeam(0).ToString & ".png"

        End While

        sqlConn.Close()
        strTeamQuery = "SELECT rangos.id, rangos.nombre " & _
                                   "FROM asociados INNER JOIN rangos ON asociados.rangopago=rangos.id " & _
                                   "WHERE asociados.id =" & Session("idasociado").ToString
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            Me.imgrangopago.ImageUrl = "img/rangos/" & dtrTeam(0).ToString & ".png"

            Me.lblrangopago.Text = dtrTeam(1).ToString
        End While

        sqlConn.Close()


    End Sub
    Sub checapatrocinador()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT patrocinadores.nombre, patrocinadores.id, patrocinadores.appaterno , patrocinadores.apmaterno " & _
                                     "FROM asociados INNER JOIN asociados AS patrocinadores ON asociados.patrocinador=patrocinadores.id " & _
                                     "WHERE asociados.id =" & Session("idasociado").ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read

            Me.patrocinador.Text = dtrTeam(1).ToString & " " & dtrTeam(0).ToString & " " & dtrTeam(2).ToString & " " & dtrTeam(3).ToString
        End While

        sqlConn.Close()
    End Sub
    Sub checacompra()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT finsc, status " & _
                                     "FROM asociados " & _
                                     "WHERE id =" & Session("idasociado").ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim fecha As Date
        Dim status As Integer = 0
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then fecha = dtrTeam(0)
            If Not IsDBNull(dtrTeam(1)) Then status = dtrTeam(1)
        End While

        sqlConn.Close()
        'fecha de activación
        Dim fechadeactivacion, fechatemp As Date
        If fecha.Day < Date.Today.Day And status = 1 Then
            'el próximo mes
            fechatemp = DateAdd(DateInterval.Month, 1, Today)
            fechadeactivacion = funciones.validarfecha(fecha.Day, fechatemp.Month, fechatemp.Year)
        Else
            'este mes
            fechadeactivacion = funciones.validarfecha(fecha.Day, Today.Month, Today.Year)
        End If



        Me.calificacion.Text = fechadeactivacion.ToString("dd/MMMM/yyyy")


        Me.Calendar1.VisibleDate = fechadeactivacion

        Dim dia As Integer = fecha.Day

        Dim banderaa, banderade As Integer

        'fecha = funciones.validarfecha(dia, Date.Today.Month, Date.Today.Year)
        fecha = fechadeactivacion
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
        If a < Date.Today Or comprasteentre(de, a) Then
            banderaa = 0
            banderade = 0
            fecha = CDate(Date.Today.Year.ToString & "/" & Date.Today.Month.ToString & "/" & dia.ToString)
            fecha = DateAdd(DateInterval.Month, 1, fecha)
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
        End If
        Me.periodode.Text = de.ToString("dd/MMMM/yyyy")
        Me.periodoa.Text = a.ToString("dd/MMMM/yyyy")
        Me.Calendar1.SelectedDate = de
    End Sub
    Function comprasteentre(ByVal inicio As Date, ByVal fin As Date) As Boolean
        Dim respuesta As Boolean = False
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id " & _
                                     "FROM compras " & _
                                     "WHERE asociado =" & Session("idasociado").ToString & " AND fecha>='" & inicio.ToString("yyyy/MM/dd") & "' AND fecha<='" & fin.ToString("yyyy/MM/dd") & "'"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            respuesta = True
        End While

        sqlConn.Close()


        Return respuesta
    End Function
    Sub checaorganizacion()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT COUNT( id ) , lado " & _
                                     "FROM `asociados` " & _
                                     "WHERE patrocinador =" & Session("idasociado").ToString & " AND status=1 " & _
                                     "GROUP BY lado"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(1)) Then
                If dtrTeam(1) = "D" Then
                    Me.activosder.Text = dtrTeam(0)
                Else
                    Me.activosizq.Text = dtrTeam(0)
                End If
            End If
        End While

        sqlConn.Close()

        'en mi organización
        Dim qry_miarbol As String = "SELECT id, recorrido, ladosrecorrido FROM asociados WHERE status=1 AND recorrido LIKE '%." & Session("idasociado").ToString & ".%'"
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
                If recorridoarray(posicion) = Session("idasociado").ToString Then
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
        Me.asociadosactivosizq.Text = izq.ToString
        Me.asociadosactivosder.Text = der.ToString



    End Sub
    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Me.Panel1.Visible = False
        Me.Panel2.Visible = True
        Me.Panel3.Visible = False
        Me.Panel4.Visible = False
        Me.PanelAvisos.Visible = False
        checacompra()
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Me.Panel1.Visible = True
        Me.Panel2.Visible = False
        Me.Panel3.Visible = False
        Me.Panel4.Visible = False
        Me.PanelAvisos.Visible = False
    End Sub

    Protected Sub ImageButton3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton3.Click
        Me.Panel1.Visible = False
        Me.Panel2.Visible = False
        Me.Panel3.Visible = True
        Me.Panel4.Visible = False
        Me.PanelAvisos.Visible = False
        checaorganizacion()
    End Sub

    Protected Sub Calendar1_DayRender(ByVal sender As Object, _
        ByVal e As DayRenderEventArgs) Handles Calendar1.DayRender
        ' Display vacation dates in yellow boxes with purple borders.
        Dim vacationStyle As New Style()
        With vacationStyle
            .BackColor = System.Drawing.Color.Blue
            .ForeColor = Drawing.Color.White

            '  .BorderColor = System.Drawing.Color.Purple
            ' .BorderWidth = New Unit(3)
        End With

        ' Display weekend dates in green boxes.
        Dim weekendStyle As New Style()
        '  weekendStyle.BackColor = System.Drawing.Color.Green

        ' Vacation is from Nov 23, 2005 to Nov 30, 2005.
        If ((e.Day.Date >= de) _
                And (e.Day.Date <= a)) Then
            e.Cell.ApplyStyle(vacationStyle)
        ElseIf (e.Day.IsWeekend) Then
            e.Cell.ApplyStyle(weekendStyle)
        End If
    End Sub

    Protected Sub ImageButton4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton4.Click
        Me.Panelquemefalta.Visible = True
        llenaquemefalta()

    End Sub
    Sub llenaquemefalta()
        funciones.misdecendientes(Session("idasociado"), Me.activosenmiarbolizq, Me.activosenmiarbolder)
        Dim faltanizq, faltander, faltanizqpatrocinados, faltanderpatrocinados, misactivosizq, misactivosder As Integer
        misactivosizq = CInt(Me.activosizq.Text)
        misactivosder = CInt(Me.activosder.Text)
        Select Case Me.siguienterango.Text
            Case "Colaborador"
                faltanizq = 3 - CInt(Me.activosenmiarbolizq.Text)
                If faltanizq < 0 Then faltanizq = 0
                faltander = 3 - CInt(Me.activosenmiarbolder.Text)
                If faltander < 0 Then faltander = 0
                faltanizqpatrocinados = 1 - CInt(Me.activosizq.Text)
                If faltanizqpatrocinados < 0 Then faltanizqpatrocinados = 0
                faltanderpatrocinados = 1 - CInt(Me.activosder.Text)
                If faltanderpatrocinados < 0 Then faltanderpatrocinados = 0
                If faltanizq > 0 Then
                    If faltanizq = 1 Then
                        Me.activosporladofalta.Text = "Te falta " & faltanizq.ToString & " Asociado activo del lado izquierdo "
                    Else
                        Me.activosporladofalta.Text = "Te faltan " & faltanizq.ToString & " Asociados activos del lado izquierdo "
                    End If
                End If
                If faltander > 0 Then
                    If Me.activosporladofalta.Text <> "" Then
                        Me.activosporladofalta.Text += " y "

                    End If

                    If faltander = 1 Then
                        Me.activosporladofalta.Text += "Te falta " & faltander.ToString & " Asociado activo del lado derecho "
                    Else
                        Me.activosporladofalta.Text += "Te faltan " & faltander.ToString & " Asociados activos del lado derecho "
                    End If
                End If


                If faltanizqpatrocinados > 0 Then
                    If faltanizqpatrocinados = 1 Then
                        Me.activosenorganizacionfalta.Text = "Te falta " & faltanizqpatrocinados.ToString & "  Patrocinado directo del lado izquierdo "
                    Else
                        Me.activosenorganizacionfalta.Text = "Te faltan " & faltanizqpatrocinados.ToString & "  Patrocinados directos del lado izquierdo "
                    End If
                End If
                If faltanderpatrocinados > 0 Then
                    If Me.activosenorganizacionfalta.Text <> "" Then Me.activosenorganizacionfalta.Text += " y "
                    If faltander = 1 Then
                        Me.activosenorganizacionfalta.Text += "Te falta " & faltanderpatrocinados.ToString & " Patrocinado directo del lado derecho "
                    Else
                        Me.activosenorganizacionfalta.Text += "Te faltan " & faltanderpatrocinados.ToString & " Patrocinados directos del lado derecho "
                    End If
                End If


            Case "Colaborador Ejecutivo"
                faltanizq = 10 - CInt(Me.activosenmiarbolizq.Text)
                If faltanizq < 0 Then faltanizq = 0
                faltander = 10 - CInt(Me.activosenmiarbolder.Text)
                If faltander < 0 Then faltander = 0
                faltanizqpatrocinados = 1 - CInt(Me.activosizq.Text)
                If faltanizqpatrocinados < 0 Then faltanizqpatrocinados = 0
                faltanderpatrocinados = 1 - CInt(Me.activosder.Text)
                If faltanderpatrocinados < 0 Then faltanderpatrocinados = 0
                If faltanizq > 0 Then
                    If faltanizq = 1 Then
                        Me.activosporladofalta.Text = "Te falta " & faltanizq.ToString & " Asociado activo del lado izquierdo "
                    Else
                        Me.activosporladofalta.Text = "Te faltan " & faltanizq.ToString & " Asociados activos del lado izquierdo "
                    End If
                End If
                If faltander > 0 Then
                    If Me.activosporladofalta.Text <> "" Then Me.activosporladofalta.Text += " y "
                    If faltander = 1 Then
                        Me.activosporladofalta.Text += "Te falta " & faltander.ToString & " Asociado activo del lado derecho "
                    Else
                        Me.activosporladofalta.Text += "Te faltan " & faltander.ToString & " Asociados activos del lado derecho "
                    End If
                End If


                If faltanizqpatrocinados > 0 Then
                    If faltanizqpatrocinados = 1 Then
                        Me.activosenorganizacionfalta.Text = "Te falta " & faltanizqpatrocinados.ToString & "  Patrocinado directo del lado izquierdo "
                    Else
                        Me.activosenorganizacionfalta.Text = "Te faltan " & faltanizqpatrocinados.ToString & "  Patrocinados directos del lado izquierdo "
                    End If
                End If
                If faltanderpatrocinados > 0 Then
                    If Me.activosenorganizacionfalta.Text <> "" Then Me.activosenorganizacionfalta.Text += " y "
                    If faltander = 1 Then
                        Me.activosenorganizacionfalta.Text += "Te falta " & faltanderpatrocinados.ToString & " Patrocinado directo del lado derecho "
                    Else
                        Me.activosenorganizacionfalta.Text += "Te faltan " & faltanderpatrocinados.ToString & " Patrocinados directos del lado derecho "
                    End If
                End If

            Case "Bronce"
                faltanizq = 20 - CInt(Me.activosenmiarbolizq.Text)
                If faltanizq < 0 Then faltanizq = 0
                faltander = 20 - CInt(Me.activosenmiarbolder.Text)
                If faltander < 0 Then faltander = 0
                If misactivosizq + misactivosder < 3 Or misactivosder = 0 Or misactivosizq = 0 Then
                    Me.activosenorganizacionfalta.Text = "No cumples con el requisito de 2 Activos de 1 lado y 1 Activo del otro lado"
                End If
                If faltanizq > 0 Then
                    If faltanizq = 1 Then
                        Me.activosporladofalta.Text = "Te falta " & faltanizq.ToString & " Asociado activo del lado izquierdo "
                    Else
                        Me.activosporladofalta.Text = "Te faltan " & faltanizq.ToString & " Asociados activos del lado izquierdo "
                    End If
                End If
                If faltander > 0 Then
                    If Me.activosporladofalta.Text <> "" Then Me.activosporladofalta.Text += " y "
                    If faltander = 1 Then
                        Me.activosporladofalta.Text += "Te falta " & faltander.ToString & " Asociado activo del lado derecho "
                    Else
                        Me.activosporladofalta.Text += "Te faltan " & faltander.ToString & " Asociados activos del lado derecho "
                    End If
                End If

            Case "Plata"
                faltanizq = 50 - CInt(Me.activosenmiarbolizq.Text)
                If faltanizq < 0 Then faltanizq = 0
                faltander = 50 - CInt(Me.activosenmiarbolder.Text)
                If faltander < 0 Then faltander = 0
                If misactivosizq + misactivosder < 3 Or misactivosder = 0 Or misactivosizq = 0 Then
                    Me.activosenorganizacionfalta.Text = "No cumples con el requisito de 2 Activos de 1 lado y 1 Activo del otro lado"
                End If
                If faltanizq > 0 Then
                    If faltanizq = 1 Then
                        Me.activosporladofalta.Text = "Te falta " & faltanizq.ToString & " Asociado activo del lado izquierdo "
                    Else
                        Me.activosporladofalta.Text = "Te faltan " & faltanizq.ToString & " Asociados activos del lado izquierdo "
                    End If
                End If
                If faltander > 0 Then
                    If Me.activosporladofalta.Text <> "" Then Me.activosporladofalta.Text += " y "
                    If faltander = 1 Then
                        Me.activosporladofalta.Text += "Te falta " & faltander.ToString & " Asociado activo del lado derecho "
                    Else
                        Me.activosporladofalta.Text += "Te faltan " & faltander.ToString & " Asociados activos del lado derecho "
                    End If
                End If

            Case "Oro"
                faltanizq = 75 - CInt(Me.activosenmiarbolizq.Text)
                If faltanizq < 0 Then faltanizq = 0
                faltander = 75 - CInt(Me.activosenmiarbolder.Text)
                If faltander < 0 Then faltander = 0
                faltanizqpatrocinados = 2 - CInt(Me.activosizq.Text)
                If faltanizqpatrocinados < 0 Then faltanizqpatrocinados = 0
                faltanderpatrocinados = 2 - CInt(Me.activosder.Text)
                If faltanderpatrocinados < 0 Then faltanderpatrocinados = 0
                If faltanizq > 0 Then
                    If faltanizq = 1 Then
                        Me.activosporladofalta.Text = "Te falta " & faltanizq.ToString & " Asociado activo del lado izquierdo "
                    Else
                        Me.activosporladofalta.Text = "Te faltan " & faltanizq.ToString & " Asociados activos del lado izquierdo "
                    End If
                End If
                If faltander > 0 Then
                    If Me.activosporladofalta.Text <> "" Then Me.activosporladofalta.Text += " y "
                    If faltander = 1 Then
                        Me.activosporladofalta.Text += "Te falta " & faltander.ToString & " Asociado activo del lado derecho "
                    Else
                        Me.activosporladofalta.Text += "Te faltan " & faltander.ToString & " Asociados activos del lado derecho "
                    End If
                End If


                If faltanizqpatrocinados > 0 Then
                    If faltanizqpatrocinados = 1 Then
                        Me.activosenorganizacionfalta.Text = "Te falta " & faltanizqpatrocinados.ToString & "  Patrocinado directo del lado izquierdo "
                    Else
                        Me.activosenorganizacionfalta.Text = "Te faltan " & faltanizqpatrocinados.ToString & "  Patrocinados directos del lado izquierdo "
                    End If
                End If
                If faltanderpatrocinados > 0 Then
                    If Me.activosenorganizacionfalta.Text <> "" Then Me.activosenorganizacionfalta.Text += " y "
                    If faltander = 1 Then
                        Me.activosenorganizacionfalta.Text += "Te falta " & faltanderpatrocinados.ToString & " Patrocinado directo del lado derecho "
                    Else
                        Me.activosenorganizacionfalta.Text += "Te faltan " & faltanderpatrocinados.ToString & " Patrocinados directos del lado derecho "
                    End If
                End If
            Case "Diamante"
                faltanizq = 160 - CInt(Me.activosenmiarbolizq.Text)
                If faltanizq < 0 Then faltanizq = 0
                faltander = 160 - CInt(Me.activosenmiarbolder.Text)
                If faltander < 0 Then faltander = 0
                faltanizqpatrocinados = 2 - CInt(Me.activosizq.Text)
                If faltanizqpatrocinados < 0 Then faltanizqpatrocinados = 0
                faltanderpatrocinados = 2 - CInt(Me.activosder.Text)
                If faltanderpatrocinados < 0 Then faltanderpatrocinados = 0
                If faltanizq > 0 Then
                    If faltanizq = 1 Then
                        Me.activosporladofalta.Text = "Te falta " & faltanizq.ToString & " Asociado activo del lado izquierdo "
                    Else
                        Me.activosporladofalta.Text = "Te faltan " & faltanizq.ToString & " Asociados activos del lado izquierdo "
                    End If
                End If
                If faltander > 0 Then
                    If Me.activosporladofalta.Text <> "" Then Me.activosporladofalta.Text += " y "
                    If faltander = 1 Then
                        Me.activosporladofalta.Text += "Te falta " & faltander.ToString & " Asociado activo del lado derecho "
                    Else
                        Me.activosporladofalta.Text += "Te faltan " & faltander.ToString & " Asociados activos del lado derecho "
                    End If
                End If


                If faltanizqpatrocinados > 0 Then
                    If faltanizqpatrocinados = 1 Then
                        Me.activosenorganizacionfalta.Text = "Te falta " & faltanizqpatrocinados.ToString & "  Patrocinado directo del lado izquierdo "
                    Else
                        Me.activosenorganizacionfalta.Text = "Te faltan " & faltanizqpatrocinados.ToString & "  Patrocinados directos del lado izquierdo "
                    End If
                End If
                If faltanderpatrocinados > 0 Then
                    If Me.activosenorganizacionfalta.Text <> "" Then Me.activosenorganizacionfalta.Text += " y "
                    If faltander = 1 Then
                        Me.activosenorganizacionfalta.Text += "Te falta " & faltanderpatrocinados.ToString & " Patrocinado directo del lado derecho "
                    Else
                        Me.activosenorganizacionfalta.Text += "Te faltan " & faltanderpatrocinados.ToString & " Patrocinados directos del lado derecho "
                    End If
                End If
            Case "Diamante Ejecutivo"
                faltanizq = 400 - CInt(Me.activosenmiarbolizq.Text)
                If faltanizq < 0 Then faltanizq = 0
                faltander = 400 - CInt(Me.activosenmiarbolder.Text)
                If faltander < 0 Then faltander = 0
                faltanizqpatrocinados = 2 - CInt(Me.activosizq.Text)
                If faltanizqpatrocinados < 0 Then faltanizqpatrocinados = 0
                faltanderpatrocinados = 2 - CInt(Me.activosder.Text)
                If faltanderpatrocinados < 0 Then faltanderpatrocinados = 0
                If faltanizq > 0 Then
                    If faltanizq = 1 Then
                        Me.activosporladofalta.Text = "Te falta " & faltanizq.ToString & " Asociado activo del lado izquierdo "
                    Else
                        Me.activosporladofalta.Text = "Te faltan " & faltanizq.ToString & " Asociados activos del lado izquierdo "
                    End If
                End If
                If faltander > 0 Then
                    If Me.activosporladofalta.Text <> "" Then Me.activosporladofalta.Text += " y "
                    If faltander = 1 Then
                        Me.activosporladofalta.Text += "Te falta " & faltander.ToString & " Asociado activo del lado derecho "
                    Else
                        Me.activosporladofalta.Text += "Te faltan " & faltander.ToString & " Asociados activos del lado derecho "
                    End If
                End If


                If faltanizqpatrocinados > 0 Then
                    If faltanizqpatrocinados = 1 Then
                        Me.activosenorganizacionfalta.Text = "Te falta " & faltanizqpatrocinados.ToString & "  Patrocinado directo del lado izquierdo "
                    Else
                        Me.activosenorganizacionfalta.Text = "Te faltan " & faltanizqpatrocinados.ToString & "  Patrocinados directos del lado izquierdo "
                    End If
                End If
                If faltanderpatrocinados > 0 Then
                    If Me.activosenorganizacionfalta.Text <> "" Then Me.activosenorganizacionfalta.Text += " y "
                    If faltander = 1 Then
                        Me.activosenorganizacionfalta.Text += "Te falta " & faltanderpatrocinados.ToString & " Patrocinado directo del lado derecho "
                    Else
                        Me.activosenorganizacionfalta.Text += "Te faltan " & faltanderpatrocinados.ToString & " Patrocinados directos del lado derecho "
                    End If
                End If
            Case "Diamante Internacional"
                faltanizq = 800 - CInt(Me.activosenmiarbolizq.Text)
                If faltanizq < 0 Then faltanizq = 0
                faltander = 800 - CInt(Me.activosenmiarbolder.Text)
                If faltander < 0 Then faltander = 0
                faltanizqpatrocinados = 2 - CInt(Me.activosizq.Text)
                If faltanizqpatrocinados < 0 Then faltanizqpatrocinados = 0
                faltanderpatrocinados = 2 - CInt(Me.activosder.Text)
                If faltanderpatrocinados < 0 Then faltanderpatrocinados = 0
                If faltanizq > 0 Then
                    If faltanizq = 1 Then
                        Me.activosporladofalta.Text = "Te falta " & faltanizq.ToString & " Asociado activo del lado izquierdo "
                    Else
                        Me.activosporladofalta.Text = "Te faltan " & faltanizq.ToString & " Asociados activos del lado izquierdo "
                    End If
                End If
                If faltander > 0 Then
                    If Me.activosporladofalta.Text <> "" Then Me.activosporladofalta.Text += " y "
                    If faltander = 1 Then
                        Me.activosporladofalta.Text += "Te falta " & faltander.ToString & " Asociado activo del lado derecho "
                    Else
                        Me.activosporladofalta.Text += "Te faltan " & faltander.ToString & " Asociados activos del lado derecho "
                    End If
                End If


                If faltanizqpatrocinados > 0 Then
                    If faltanizqpatrocinados = 1 Then
                        Me.activosenorganizacionfalta.Text = "Te falta " & faltanizqpatrocinados.ToString & "  Patrocinado directo del lado izquierdo "
                    Else
                        Me.activosenorganizacionfalta.Text = "Te faltan " & faltanizqpatrocinados.ToString & "  Patrocinados directos del lado izquierdo "
                    End If
                End If
                If faltanderpatrocinados > 0 Then
                    If Me.activosenorganizacionfalta.Text <> "" Then Me.activosenorganizacionfalta.Text += " y "
                    If faltander = 1 Then
                        Me.activosenorganizacionfalta.Text += "Te falta " & faltanderpatrocinados.ToString & " Patrocinado directo del lado derecho "
                    Else
                        Me.activosenorganizacionfalta.Text += "Te faltan " & faltanderpatrocinados.ToString & " Patrocinados directos del lado derecho "
                    End If
                End If
        End Select
    End Sub

  
    Protected Sub ImageButton5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton5.Click
        Me.Panel1.Visible = False
        Me.Panel2.Visible = False
        Me.Panel3.Visible = False
        Me.Panel4.Visible = True
        Me.PanelAvisos.Visible = False
    End Sub

    Protected Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        'para cierre
        Dim segundos0 As Integer = CInt(Me.numsegundos0.Text)
        Dim dias0 As Integer = CInt(Me.numdias0.Text)
        Dim horas0 As Integer = CInt(Me.numhoras0.Text)
        Dim minutos0 As Integer = CInt(Me.numminutos0.Text)
        segundos0 -= 1
        If segundos0 = -0 Then
            segundos0 = 59
            minutos0 -= 1
        End If
        If minutos0 = -1 Then
            minutos0 = 59
            horas0 -= 1
        End If
        If horas0 = -1 Then
            horas0 = 23
            dias0 -= 1
        End If
        If dias0 = -1 Then
            dias0 = 6
            horas0 = 23
            minutos0 = 59
            segundos0 = 59
        End If
        Me.numdias0.Text = dias0.ToString
        Me.numhoras0.Text = horas0.ToString
        Me.numminutos0.Text = minutos0.ToString
        Me.numsegundos0.Text = segundos0.ToString
    End Sub
    Sub llenagridrequisitos()
        Me.GridRequisitos.Columns(1).Visible = True
        Dim inicio As Date = "2014/08/02"
        Dim fin As Date = "2014/09/02"
        Dim ds As New DataSet
        Dim table As DataTable = ds.Tables.Add("Data")
        table.Columns.Add("requisito", GetType(String))
        table.Columns.Add("total", GetType(Integer))

        'Dim qry_paquetesypuntos As String = "SELECT comprasdetalle.cantidad, comprasdetalle.paquete, asociados.recorrido, asociados.ladosrecorrido " & _
        '"FROM asociados INNER JOIN compras ON asociados.id=compras.asociado INNER JOIN `comprasdetalle` ON compras.id=comprasdetalle.compra " & _
        '"WHERE asociados.recorrido LIKE '%." & Session("idasociado").ToString & ".%' AND comprasdetalle.paquete BETWEEN 1 AND 3 AND compras.statuspago='PAGADO' AND compras.fecha>='" & inicio.ToString("yyyy/MM/dd") & "' AND compras.fecha<='" & fin.ToString("yyyy/MM/dd") & "'"

        'nuevo, incluyendo pago hasta el siguiente lunes
        Dim qry_paquetesypuntos As String = "SELECT comprasdetalle.cantidad, comprasdetalle.paquete, asociados.recorrido, asociados.ladosrecorrido, compras.id, asociados.id, asociados.lado  " & _
      "FROM asociados INNER JOIN compras ON asociados.id=compras.asociado INNER JOIN `comprasdetalle` ON compras.id=comprasdetalle.compra " & _
      "WHERE asociados.recorrido LIKE '%." & Session("idasociado").ToString & ".%' AND comprasdetalle.paquete BETWEEN 1 AND 3 AND compras.statuspago='PAGADO' AND compras.fecha>='" & inicio.ToString("yyyy/MM/dd") & "' AND (compras.fecha<='" & fin.ToString("yyyy/MM/dd") & "' OR (compras.fechaorden BETWEEN '" & inicio.ToString("yyyy/MM/dd") & "' AND '" & fin.ToString("yyyy/MM/dd") & "' AND (compras.fecha='" & DateAdd(DateInterval.Day, 1, fin).ToString("yyyy/MM/dd") & "' OR compras.fecha='" & DateAdd(DateInterval.Day, 2, fin).ToString("yyyy/MM/dd") & "' OR compras.fecha='" & DateAdd(DateInterval.Day, 3, fin).ToString("yyyy/MM/dd") & "'  ))) " & _
      "AND compras.id NOT IN (" & _
                    "Select id FROM compras " & _
                    "WHERE fechaorden < '" & inicio.ToString("yyyy/MM/dd") & "' " & _
                    "AND fecha < '" & DateAdd(DateInterval.Day, 2, inicio).ToString("yyyy/MM/dd") & "' " & _
                    ") "





        Dim qry_directosporlado As String = "SELECT Count(asociados.ID) AS directos, asociados.ladopatrocinador AS lado " & _
                                                   "FROM(asociados) " & _
                                                   "WHERE(((asociados.patrocinador)=" & Session("idasociado").ToString & ") AND (asociados.status=1  OR '" & fin.ToString("yyyy/MM/dd") & "' BETWEEN inicioactivacion AND finactivacion)  ) AND asociados.ptsmes>350 " & _
                                                   "GROUP BY asociados.patrocinador, asociados.ladopatrocinador " & _
                                                   "ORDER BY asociados.patrocinador, asociados.ladopatrocinador ;"
        Dim qry_inactivosporlado As String = "SELECT Count(asociados.ID) AS directos, asociados.ladopatrocinador AS lado " & _
                                                   "FROM(asociados) " & _
                                                   "WHERE(((asociados.patrocinador)=" & Session("idasociado").ToString & ") AND (asociados.status=0  )  ) " & _
                                                   "GROUP BY asociados.patrocinador, asociados.ladopatrocinador " & _
                                                   "ORDER BY asociados.patrocinador, asociados.ladopatrocinador ;"

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(qry_paquetesypuntos, sqlConn)

        Dim dtrTeam As MySqlDataReader
        Dim comprasderechas As String = ""

        'directos por lado período 1
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim recorrido, ladosrecorrido As String
        Dim recorridoarray(), ladosarray() As String
        Dim izq, der As Integer
        While dtrTeam.Read
            Dim asociadoid As Integer = dtrTeam(5)


            recorrido = dtrTeam(2)

            ladosrecorrido = dtrTeam(3)

            recorridoarray = Split(recorrido, ".")
            ladosarray = Split(ladosrecorrido, ".")
            Dim posicion As Integer = 0
            For posicion = 0 To recorridoarray.Length - 1
                If recorridoarray(posicion) = Session("idasociado").ToString Then
                    Exit For
                End If


            Next
            'posicion += 1
            If ladosarray(posicion) <> "" Then
                If UCase(ladosarray(posicion)) = "D" Then
                    Select Case dtrTeam(1)
                        Case 1
                            der += 25 * dtrTeam(0)
                        Case 2
                            der += 50 * dtrTeam(0)
                        Case 3
                            der += 75 * dtrTeam(0)
                    End Select


                    comprasderechas += dtrTeam(4).ToString & ", "


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
            Else
                If UCase(dtrTeam(6)) = "D" Then
                    Select Case dtrTeam(1)
                        Case 1
                            der += 25 * dtrTeam(0)
                        Case 2
                            der += 50 * dtrTeam(0)
                        Case 3
                            der += 75 * dtrTeam(0)
                    End Select


                    comprasderechas += dtrTeam(4).ToString & ", "


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

        table.Rows.Add(New Object() {"Puntos Izquierdos", izq})
        table.Rows.Add(New Object() {"Puntos Derechos", der})
        table.Rows.Add(New Object() {"Puntos Totales", izq + der})
        table.Rows.Add(New Object() {"Empresarios/Emprendedores Izquierdos", directosizq})
        table.Rows.Add(New Object() {"Empresarios/Emprendedores Derechos", directosder})
        table.Rows.Add(New Object() {"Inactivos Izquierdos", inactivosizq})
        table.Rows.Add(New Object() {"Inactivos Derechos", inactivosder})
        Dim view As DataView = ds.Tables(0).DefaultView
        Me.GridRequisitos.DataSource = view
        Me.GridRequisitos.DataBind()
        'actualizarango(idasociado, rango, rangoactual, nombre, izq, der, directosizq, directosder, inactivosizq, inactivosder)
    End Sub
    Sub llenagridrequisitosAnterior()
        Me.GridRequisitos.Columns(1).Visible = True
        Dim inicio As Date = funciones.iniciodecalificacion
        Dim fin As Date = funciones.cierredecalificacion
        Dim ds As New DataSet
        Dim table As DataTable = ds.Tables.Add("Data")
        table.Columns.Add("requisito", GetType(String))
        table.Columns.Add("total", GetType(Integer))

        'Dim qry_paquetesypuntos As String = "SELECT comprasdetalle.cantidad, comprasdetalle.paquete, asociados.recorrido, asociados.ladosrecorrido " & _
        '"FROM asociados INNER JOIN compras ON asociados.id=compras.asociado INNER JOIN `comprasdetalle` ON compras.id=comprasdetalle.compra " & _
        '"WHERE asociados.recorrido LIKE '%." & Session("idasociado").ToString & ".%' AND comprasdetalle.paquete BETWEEN 1 AND 3 AND compras.statuspago='PAGADO' AND compras.fecha>='" & inicio.ToString("yyyy/MM/dd") & "' AND compras.fecha<='" & fin.ToString("yyyy/MM/dd") & "'"

        'nuevo, incluyendo pago hasta el siguiente lunes
        Dim qry_paquetesypuntos As String = "SELECT comprasdetalle.cantidad, comprasdetalle.paquete, asociados.recorrido, asociados.ladosrecorrido, compras.id, asociados.id  " & _
      "FROM asociados INNER JOIN compras ON asociados.id=compras.asociado INNER JOIN `comprasdetalle` ON compras.id=comprasdetalle.compra " & _
      "WHERE asociados.recorrido LIKE '%." & Session("idasociado").ToString & ".%' AND comprasdetalle.paquete BETWEEN 1 AND 3 AND compras.statuspago='PAGADO' AND compras.fecha>='" & inicio.ToString("yyyy/MM/dd") & "' AND (compras.fecha<='" & fin.ToString("yyyy/MM/dd") & "' OR (compras.fechaorden BETWEEN '" & inicio.ToString("yyyy/MM/dd") & "' AND '" & fin.ToString("yyyy/MM/dd") & "' AND (compras.fecha='" & DateAdd(DateInterval.Day, 1, fin).ToString("yyyy/MM/dd") & "' OR compras.fecha='" & DateAdd(DateInterval.Day, 2, fin).ToString("yyyy/MM/dd") & "' OR compras.fecha='" & DateAdd(DateInterval.Day, 3, fin).ToString("yyyy/MM/dd") & "'  ))) " & _
      "AND compras.id NOT IN (" & _
                    "Select id FROM compras " & _
                    "WHERE fechaorden < '" & inicio.ToString("yyyy/MM/dd") & "' " & _
                    "AND fecha < '" & DateAdd(DateInterval.Day, 2, inicio).ToString("yyyy/MM/dd") & "' " & _
                    ") "





        Dim qry_directosporlado As String = "SELECT Count(asociados.ID) AS directos, asociados.ladopatrocinador AS lado " & _
                                                   "FROM(asociados) " & _
                                                   "WHERE(((asociados.patrocinador)=" & Session("idasociado").ToString & ") AND (asociados.status=1  OR '" & fin.ToString("yyyy/MM/dd") & "' BETWEEN inicioactivacion AND finactivacion)  ) AND asociados.ptsmes>350 " & _
                                                   "GROUP BY asociados.patrocinador, asociados.ladopatrocinador " & _
                                                   "ORDER BY asociados.patrocinador, asociados.ladopatrocinador ;"
        Dim qry_inactivosporlado As String = "SELECT Count(asociados.ID) AS directos, asociados.ladopatrocinador AS lado " & _
                                                   "FROM(asociados) " & _
                                                   "WHERE(((asociados.patrocinador)=" & Session("idasociado").ToString & ") AND (asociados.status=0  )  ) " & _
                                                   "GROUP BY asociados.patrocinador, asociados.ladopatrocinador " & _
                                                   "ORDER BY asociados.patrocinador, asociados.ladopatrocinador ;"

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(qry_paquetesypuntos, sqlConn)

        Dim dtrTeam As MySqlDataReader
        Dim comprasderechas As String = ""

        'directos por lado período 1
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim recorrido, ladosrecorrido As String
        Dim recorridoarray(), ladosarray() As String
        Dim izq, der As Integer
        While dtrTeam.Read
            Dim asociadoid As Integer = dtrTeam(5)


            recorrido = dtrTeam(2)

            ladosrecorrido = dtrTeam(3)

            recorridoarray = Split(recorrido, ".")
            ladosarray = Split(ladosrecorrido, ".")
            Dim posicion As Integer = 0
            For posicion = 0 To recorridoarray.Length - 1
                If recorridoarray(posicion) = Session("idasociado").ToString Then
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


                comprasderechas += dtrTeam(4).ToString & ", "


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

        table.Rows.Add(New Object() {"Puntos Izquierdos", izq})
        table.Rows.Add(New Object() {"Puntos Derechos", der})
        table.Rows.Add(New Object() {"Puntos Totales", izq + der})
        table.Rows.Add(New Object() {"Empresarios/Emprendedores Izquierdos", directosizq})
        table.Rows.Add(New Object() {"Empresarios/Emprendedores Derechos", directosder})
        table.Rows.Add(New Object() {"Inactivos Izquierdos", inactivosizq})
        table.Rows.Add(New Object() {"Inactivos Derechos", inactivosder})
        Dim view As DataView = ds.Tables(0).DefaultView
        Me.GridRequisitos.DataSource = view
        Me.GridRequisitos.DataBind()
        'actualizarango(idasociado, rango, rangoactual, nombre, izq, der, directosizq, directosder, inactivosizq, inactivosder)
    End Sub

    Protected Sub GridRequisitos_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridRequisitos.DataBound
        Me.GridRequisitos.Columns(1).Visible = False
    End Sub

    Protected Sub GridRequisitos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridRequisitos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbkBtn As LinkButton = CType(e.Row.Cells(2).Controls(0), LinkButton)
            lbkBtn.Text = e.Row.Cells(1).Text
            If e.Row.RowIndex < 3 Then
                lbkBtn.Enabled = False

            End If

        End If

    End Sub

    Protected Sub GridRequisitos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridRequisitos.SelectedIndexChanged
        llenagridavances()
    End Sub
    Sub llenagridavances()
        Dim fin As Date = funciones.cierredecalificacion
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim tipo As String
        strTeamQuery = "SELECT id, CONCAT(nombre, ' ', appaterno) AS nombre, inicioactivacion, finactivacion, ptsmes FROM asociados WHERE 1 "
        Select Case Me.GridRequisitos.SelectedIndex
            Case 3
                tipo = "PatrocinadosIzq"
            Case 4
                tipo = "PatrocinadosDer"
            Case 5
                tipo = "InactivosIzq"
            Case 6
                tipo = "InactivosDer"
        End Select
        Select Case tipo
            Case "PatrocinadosIzq"
                strTeamQuery += "And (status=1 OR '" & fin.ToString("yyyy/MM/dd") & "' BETWEEN inicioactivacion AND finactivacion) AND ladopatrocinador='I' "
            Case "PatrocinadosDer"
                strTeamQuery += "And  (status=1 OR '" & fin.ToString("yyyy/MM/dd") & "' BETWEEN inicioactivacion AND finactivacion) AND ladopatrocinador='D' "
            Case "InactivosIzq"
                strTeamQuery += "And status=0 AND ladopatrocinador='I' "
            Case "InactivosDer"
                strTeamQuery += "And status=0 AND ladopatrocinador='D' "
        End Select
        strTeamQuery += "And patrocinador=" & Session("idasociado").ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Me.GridDetalle.DataSource = dtrTeam
        Me.GridDetalle.DataBind()


        sqlConn.Close()
    End Sub
End Class
