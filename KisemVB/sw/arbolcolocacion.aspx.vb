Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Partial Class sw_arbolcolocacion
    Inherits System.Web.UI.Page
    Dim funciones As New funciones
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Árbol Binario"

            llena_arbol(Session("idasociado"))
            If Request.QueryString("nuevo") = 1 Then
                Me.PanelNuevo.Visible = True
            End If
        End If

    End Sub
    Sub llena_arbol(ByVal tope As Integer)
        If tope = Session("idasociado") Then
            Me.btn_inicio.Enabled = False
            Me.Subir1.Enabled = False
            Me.Subir2.Enabled = False
            Me.Subir3.Enabled = False
            Me.Subir4.Enabled = False
        Else
            Me.btn_inicio.Enabled = True
            Me.Subir1.Enabled = True
            Me.Subir2.Enabled = True
            Me.Subir3.Enabled = True
            Me.Subir4.Enabled = True
        End If
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        'Dim strTeamQuery As String = "SELECT id, CONCAT(nombre, ' ', appaterno, ' ', apmaterno) AS nombre, rango, padre, lado, status, alias FROM asociados WHERE id>=" & tope.ToString '& " LIMIT 31"
        Dim strTeamQuery As String = "SELECT id, CONCAT(nombre, ' ', appaterno, ' ', apmaterno) AS nombre, rango, padre, lado, status, alias, DATE_FORMAT( finsc, '%d/%M/%Y' ) AS finsc, patrocinador FROM asociados WHERE id=" & tope.ToString & " or recorrido like '%." & tope.ToString & ".%' order by  CHAR_LENGTH(ladosrecorrido)"

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim objAdapter As New MySqlDataAdapter
        objAdapter.SelectCommand = cmdFetchTeam
        sqlConn.Open()
        Dim objDS As New DataSet
        objAdapter.Fill(objDS, "asociados")
        sqlConn.Close()
        Dim view As New DataView(objDS.Tables(0))
        Dim nombres, aliases, inscripciones As New List(Of String)
        Dim ids, rangos, status, patrocinadores As New List(Of Integer)
        view.RowFilter = "id=" & tope.ToString
        Dim drv As DataRowView
        For Each drv In view
            rangos.Add(drv("rango"))
            ids.Add(drv("id"))
            nombres.Add(drv("nombre"))
            aliases.Add(drv("alias"))
            status.Add(drv("status"))
            inscripciones.Add(drv("finsc").ToString)
            patrocinadores.Add(drv("patrocinador"))
        Next
        Dim cont As Integer = 0
        For cont = 0 To 15
            If ids(cont) = 0 Or ids(cont) = -1 Then
                ids.Add(-1)
                ids.Add(-1)
                rangos.Add(-1)
                rangos.Add(-1)
                status.Add(-1)
                status.Add(-1)
                nombres.Add("")
                nombres.Add("")
                aliases.Add("")
                aliases.Add("")
                patrocinadores.Add(-1)
                patrocinadores.Add(-1)
                inscripciones.Add("")
                inscripciones.Add("")
            Else
                view.RowFilter = "padre=" & ids(cont).ToString
                view.Sort = "lado DESC"
                Dim indice As Integer = 0
                Dim renglones As Integer = view.Count
                Select Case renglones
                    Case 0
                        ids.Add(0)
                        ids.Add(0)
                        rangos.Add(0)
                        rangos.Add(0)
                        nombres.Add("")
                        nombres.Add("")
                        status.Add(-1)
                        status.Add(-1)
                        aliases.Add("")
                        aliases.Add("")
                        patrocinadores.Add(-1)
                        patrocinadores.Add(-1)
                        inscripciones.Add("")
                        inscripciones.Add("")
                    Case 1
                        For Each drv In view
                            If UCase(drv("lado")) = "I" Then
                                ids.Add(drv("id"))
                                rangos.Add(drv("rango"))
                                nombres.Add(drv("nombre"))
                                status.Add(drv("status"))
                                aliases.Add(drv("alias"))
                                patrocinadores.Add(drv("patrocinador"))
                                inscripciones.Add(drv("finsc"))
                                ids.Add(0)
                                rangos.Add(0)
                                nombres.Add("")
                                status.Add(-1)
                                aliases.Add("")
                                patrocinadores.Add(-1)
                                inscripciones.Add("")
                            Else
                                ids.Add(0)
                                rangos.Add(0)
                                nombres.Add("")
                                status.Add(-1)
                                aliases.Add("")
                                ids.Add(drv("id"))
                                patrocinadores.Add(-1)
                                inscripciones.Add("")
                                rangos.Add(drv("rango"))
                                nombres.Add(drv("nombre"))
                                status.Add(drv("status"))
                                aliases.Add(drv("alias"))
                                patrocinadores.Add(drv("patrocinador"))
                                inscripciones.Add(drv("finsc"))
                            End If
                        Next
                    Case Else
                        For Each drv In view
                            ids.Add(drv("id"))
                            rangos.Add(drv("rango"))
                            nombres.Add(drv("nombre"))
                            status.Add(drv("status"))
                            aliases.Add(drv("alias"))
                            patrocinadores.Add(drv("patrocinador"))
                            inscripciones.Add(drv("finsc"))
                        Next
                End Select
            End If
        Next
        'busca imagebutton
        Dim contenedor As ContentPlaceHolder = Me.Master.FindControl("ContentPlaceHolder1")
        Dim contador As Integer = 1
        For contador = 1 To 31

            Dim boton As ImageButton = contenedor.FindControl("ImageButton" & contador)
            Dim grande As String = ""
            If contador = 1 Then grande = "grandes/"
            If ids(contador - 1) > 0 Then

                'para tooltip
                Dim statusstr As String = ""
                Select Case status(contador - 1)
                    Case 1
                        statusstr = "Activo"
                    Case 0
                        statusstr = "Inactivo"
                    Case 2
                        statusstr = "Suspendido"
                    Case Else
                        statusstr = "Indefinido"
                End Select

                Dim rangostr As String = ""
                Select Case rangos(contador - 1)
                    Case 1
                        rangostr = "Asociado"
                    Case 2
                        rangostr = "Colaborador"
                    Case 3
                        rangostr = "Colaborador Ejecutivo"
                    Case 4
                        rangostr = "Bronce"
                    Case 5
                        rangostr = "Plata"
                    Case 6
                        rangostr = "Oro"
                    Case 7
                        rangostr = "Diamante"
                    Case 8
                        rangostr = "Diamante Ejecutivo"
                    Case 9
                        rangostr = "Diamante Internacional"
                    Case Else
                        rangostr = "Indefinido"
                End Select

                boton.ToolTip = "Alias: " & aliases(contador - 1) & Environment.NewLine & "Nombre: " & nombres(contador - 1) & Environment.NewLine & "Usuario: " & ids(contador - 1) & Environment.NewLine & "Rango: " & rangostr & Environment.NewLine & "Status: " & statusstr & Environment.NewLine & "Patrocinador: " & patrocinadores(contador - 1) & Environment.NewLine & "Fecha de Inscripción: " & inscripciones(contador - 1) & Environment.NewLine

                If status(contador - 1) = 1 Then
                    boton.ImageUrl = "img/rangoshilda/" & grande & rangos(contador - 1) & ".png"
                Else
                    boton.ImageUrl = "img/rangoshilda/" & grande & rangos(contador - 1) & "i.png"
                End If
                If contador >= 16 Then
                    buscahijos(ids(contador - 1), contador)
                End If
            Else
                boton.ImageUrl = "img/rangoshilda/" & grande & ids(contador - 1) & ".png"
                boton.ToolTip = ""
                If contador >= 16 Then
                    Dim contenedor2 As ContentPlaceHolder = Me.Master.FindControl("ContentPlaceHolder1")
                    Dim indice As Integer = contador - 15
                    Dim btni As ImageButton = contenedor2.FindControl("I" & indice)
                    Dim btnd As ImageButton = contenedor2.FindControl("D" & indice)
                    btni.Visible = False
                    btnd.Visible = False
                End If
            End If
            If ids(contador - 1) = -1 Then
                boton.Enabled = False
                If contador >= 16 Then
                    Dim contenedor2 As ContentPlaceHolder = Me.Master.FindControl("ContentPlaceHolder1")
                    Dim indice As Integer = contador - 15
                    Dim btni As ImageButton = contenedor2.FindControl("I" & indice)
                    Dim btnd As ImageButton = contenedor2.FindControl("D" & indice)
                    btni.Visible = False
                    btnd.Visible = False
                End If
            Else
                boton.Enabled = True
            End If
        Next
        For contador = 1 To 31
            Dim aliaslabel As Label = contenedor.FindControl("alias" & contador)
            aliaslabel.Text = aliases(contador - 1)
            Dim etiquetastyle As New Style()
            With etiquetastyle
                If ids(contador - 1) > 0 Then
                    .BorderColor = System.Drawing.Color.DarkBlue
                    .BorderWidth = New Unit(1)


                    .ForeColor = Drawing.Color.DarkBlue
                    .CssClass = "textochico"
                    .Width = 45
                    .BackColor = Drawing.Color.White
                    aliaslabel.Visible = True
                Else
                    aliaslabel.Visible = False
                End If
            End With
            aliaslabel.ApplyStyle(etiquetastyle)
        Next

        Me.ImageButton1.Enabled = False
        Session("idsarbol") = ids
        buscapuntos(tope)
    End Sub
    Sub buscapuntos(ByVal asociado As Integer)
        Me.puntosder.Text = "0"
        Me.puntosizq.Text = "0"
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT SUM( porpagar ) , lado FROM(puntosasociados) WHERE(asociado)=" & asociado.ToString & " GROUP BY lado"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
      
        While dtrTeam.Read
            If dtrTeam(1) = "D" Then
                Me.puntosder.Text = dtrTeam(0).ToString
            End If

            If dtrTeam(1) = "I" Then
                Me.puntosizq.Text = dtrTeam(0).ToString
            End If

        End While

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub buscahijos(ByVal asociado As Integer, ByVal posicion As Integer)
        posicion -= 15
        Dim izq, der As Boolean
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT lado, id FROM asociados WHERE padre=" & asociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim cont As Integer = 0
        Dim idhijoi, idhijod As Integer
        While dtrTeam.Read
            If dtrTeam(0) = "D" Then
                der = True
                idhijod = dtrTeam(1)
            End If

            If dtrTeam(0) = "I" Then
                izq = True
                idhijoi = dtrTeam(1)
            End If

        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Dim contenedor2 As ContentPlaceHolder = Me.Master.FindControl("ContentPlaceHolder1")

        Dim btni As ImageButton = contenedor2.FindControl("I" & posicion)
        Dim btnd As ImageButton = contenedor2.FindControl("D" & posicion)
        btni.CommandArgument = idhijoi.ToString
        btnd.CommandArgument = idhijod.ToString
        btni.ToolTip = "Asociado " & idhijoi.ToString
        btnd.ToolTip = "Asociado " & idhijod.ToString
        btni.Visible = izq
        btnd.Visible = der
    End Sub
  
    Sub accionboton(ByVal lugar As Integer)
        Dim contenedor As ContentPlaceHolder = Me.Master.FindControl("ContentPlaceHolder1")
        Dim boton As ImageButton = contenedor.FindControl("ImageButton" & lugar)
        If Session("idsarbol")(lugar - 1) > 0 Then
            llena_arbol(Session("idsarbol")(lugar - 1))
        Else
            If lugar Mod 2 = 0 Then
                Session("padre") = Session("idsarbol")((lugar / 2) - 1)
                Session("lado") = 1
            Else
                Session("padre") = Session("idsarbol")(((lugar - 1) / 2) - 1)
                Session("lado") = 2
            End If

            'Response.Redirect("inscripciones_compra.aspx")
            Response.Redirect("compradeinscripcion.aspx")
        End If
    End Sub
#Region "botones"

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click, ImageButton3.Click, ImageButton4.Click, ImageButton5.Click, ImageButton6.Click, ImageButton7.Click, ImageButton8.Click, ImageButton9.Click, ImageButton10.Click, ImageButton11.Click, ImageButton12.Click, ImageButton13.Click, ImageButton14.Click, ImageButton15.Click, ImageButton16.Click, ImageButton17.Click, ImageButton18.Click, ImageButton19.Click, ImageButton20.Click, ImageButton21.Click, ImageButton22.Click, ImageButton23.Click, ImageButton24.Click, ImageButton25.Click, ImageButton26.Click, ImageButton27.Click, ImageButton28.Click, ImageButton29.Click, ImageButton30.Click, ImageButton31.Click

        Dim b As ImageButton = CType(sender, ImageButton)
        Dim x As String = b.ID

        x = Mid(x, 12, x.Length - 11)
        accionboton(CInt(x))
    End Sub

#End Region

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_inicio.Click
        llena_arbol(Session("idasociado"))

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.error.Text = ""
        If Me.asociado.Text <> Session("idasociado").ToString Then
            If estaenmiarbol(CInt(Me.asociado.Text)) Then
                llena_arbol(CInt(Me.asociado.Text))
            Else
                Me.error.Text = "No se encuentra al asociado"
            End If
        Else
            llena_arbol(CInt(Me.asociado.Text))
        End If

    End Sub
    Function estaenmiarbol(ByVal asociado As Integer) As Boolean
        Dim respuesta As Boolean = False
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""

        strTeamQuery = "SELECT id FROM asociados WHERE id =" & asociado.ToString & " AND recorrido LIKE '%." & Session("idasociado").ToString & ".%'"

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

    Protected Sub ImageButton32_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton32.Click
        llena_arbol(funciones.extremaderecha(Session("idsarbol")(0)))
    End Sub

    Protected Sub ImageButton33_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton33.Click
        llena_arbol(funciones.extremaizquierda(Session("idsarbol")(0)))
    End Sub

    Protected Sub Subir1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Subir1.Click
        llena_arbol(funciones.subeniveles(Session("idsarbol")(0), Session("idasociado")))
    End Sub

    Protected Sub Subir2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Subir2.Click
        llena_arbol(funciones.subeniveles(Session("idsarbol")(0), Session("idasociado"), 2))
    End Sub

    Protected Sub Subir3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Subir3.Click
        llena_arbol(funciones.subeniveles(Session("idsarbol")(0), Session("idasociado"), 3))
    End Sub

    Protected Sub Subir4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Subir4.Click
        llena_arbol(funciones.subeniveles(Session("idsarbol")(0), Session("idasociado"), 4))
    End Sub

    Protected Sub I1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles I1.Click, I2.Click, I3.Click, I4.Click, I5.Click, I6.Click, I7.Click, I8.Click, I9.Click, I10.Click, I11.Click, I12.Click, I13.Click, I14.Click, I15.Click, I16.Click, D1.Click, D2.Click, D3.Click, D4.Click, D5.Click, D6.Click, D7.Click, D8.Click, D9.Click, D10.Click, D11.Click, D12.Click, D13.Click, D14.Click, D15.Click, D16.Click
        Dim boton As ImageButton = CType(sender, ImageButton)
        Dim id As Integer = CInt(boton.CommandArgument)
        llena_arbol(id)
    End Sub
End Class
