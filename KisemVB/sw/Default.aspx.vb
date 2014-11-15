Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Partial Class sw_Default
    Inherits System.Web.UI.Page
    Dim asociados As New List(Of Integer)
    Dim view As New DataView()
    Dim viewpuntos As New DataView()
    Dim viewasociados As New DataView()
    Dim antepasado As Integer = 0
    Dim funciones As New funciones
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
        objEtiqueta.Text = "Bienvenido"
        If Not IsPostBack Then

            Me.nombreasociado.Text = Session("nombreasociado")
            checarango()
            checavolumen()
            checacompra()
            checaestado()
            checaactivos()
            llenaavisos()

        End If


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
            Me.rango.ImageUrl = "img/rangos/grandes/" & dtrTeam(0).ToString & ".png"
            Me.lblnombrerango.Text = dtrTeam(1).ToString
        End While

        sqlConn.Close()
    End Sub
    Sub llenaavisos()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT titulo, texto " & _
                                     "FROM avisos " & _
                                     "ORDER BY id DESC"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            Me.titulo.Text = dtrTeam(0).ToString
            Me.texto.Text = dtrTeam(1).ToString
            Exit While
        End While

        sqlConn.Close()
    End Sub
    Sub checaactivos()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT SUM(status) AS activos, COUNT(status)-SUM(status) AS inactivos " & _
                                     "FROM asociados " & _
                                     "WHERE patrocinador =" & Session("idasociado").ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            Me.activos.Text = dtrTeam(0).ToString
            Me.inactivos.Text = dtrTeam(1).ToString
        End While

        sqlConn.Close()
    End Sub
    Sub checaestado()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT status " & _
                                     "FROM asociados " & _
                                     "WHERE id =" & Session("idasociado").ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If dtrTeam(0) = 1 Then
                Me.estado.Text = "Activo"
            Else
                Me.estado.Text = "Inactivo"
            End If
        End While

        sqlConn.Close()
    End Sub
    Sub checacompra()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT finsc " & _
                                     "FROM asociados " & _
                                     "WHERE id =" & Session("idasociado").ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim fecha As Date
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then fecha = dtrTeam(0)
        End While

        sqlConn.Close()
        Dim dia As Integer = fecha.Day
        Dim de, a As Date
        Dim banderaa, banderade As Integer
        fecha = funciones.validarfecha(dia, Date.Today.Month, Date.Today.Year)
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
        Me.iniciocompra.Text = de.ToString("dd/M/yyyy")
        Me.fincompra.Text = a.ToString("dd/M/yyyy")
    End Sub
    Function comprasteentre(ByVal inicio As Date, ByVal fin As Date) As Boolean
        Dim respuesta As Boolean = False
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id " & _
                                     "FROM compras " & _
                                     "WHERE asociado =" & Session("idasociado").ToString & " AND fecha>='" & inicio.ToString & "' AND fecha<='" & fin.ToString & "'"
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
    Sub checavolumen()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT SUM(porpagar), lado " & _
                                     "FROM puntosasociados " & _
                                     "GROUP BY asociado, lado " & _
                                     "HAVING asociado =" & Session("idasociado").ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            Select Case dtrTeam("lado")
                Case "I"
                    If Not IsDBNull(dtrTeam(0)) Then Me.izquierdo.Text = dtrTeam(0).ToString
                Case "D"
                    If Not IsDBNull(dtrTeam(0)) Then Me.derecho.Text = dtrTeam(0).ToString
            End Select
        End While
        If Me.izquierdo.Text = "" Then Me.izquierdo.Text = "0"
        If Me.derecho.Text = "" Then Me.derecho.Text = "0"
        sqlConn.Close()
    End Sub
End Class
