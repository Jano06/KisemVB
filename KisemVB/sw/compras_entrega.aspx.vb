Imports MySql.Data.MySqlClient
Partial Class sw_compras_entrega
    Inherits System.Web.UI.Page
    Dim funciones As New funciones
    Dim erroresdeentrega As String

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Compras"

            llenafechas(Date.Today)
        End If
    End Sub
    Sub llenafechas(ByVal fecha As Date)
        Dim fechade As Date = DateAdd(DateInterval.Day, -(fecha.Day) + 1, fecha)
        Dim fechaa As Date = DateAdd(DateInterval.Day, Date.DaysInMonth(fecha.Year, fecha.Month) - fecha.Day, fecha)
        Me.de.Text = fechade.ToString("dd/MMMM/yyyy")
        Me.a.Text = fechaa.ToString("dd/MMMM/yyyy")
        llenagrid(fechade, fechaa)
    End Sub
    Sub llenagrid(ByVal de As Date, ByVal a As Date)
        Me.GridCarrito.SelectedIndex = -1
        Me.GridDetalle.DataSource = ""
        Me.GridDetalle.DataBind()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        'Dim strTeamQuery As String = "SELECT compras.id AS compra, CONCAT(asociados.id, ' ', asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) AS asociado, compras.fechaorden, compras.total AS monto, compras.statusentrega, compras.statuspago " & _
        '"FROM compras INNER JOIN asociados ON compras.asociado=asociados.id " & _
        '"WHERE compras.fechaorden>='" & Me.de.Text & "' AND compras.fechaorden<='" & Me.a.Text & "' "

        Dim strTeamQuery As String = "SELECT compras.id AS compra, IF( compras.inscripcion >0, IF( compras.statuspago = 'PAGADO', CONCAT( asociados.id, ' ', asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) ,  CONCAT( prospectos.nombre, ' ', prospectos.appaterno, ' ', prospectos.apmaterno) ) , CONCAT( asociados.id, ' ', asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) ) AS asociado, compras.fechaorden, compras.total AS monto, compras.statusentrega, compras.statuspago " & _
                                "FROM(compras) INNER JOIN asociados ON compras.asociado = asociados.id LEFT JOIN prospectos ON compras.inscripcion = prospectos.id " & _
                                "WHERE compras.fechaorden >= '" & CDate(Me.de.Text).ToString("yyyy/MM/dd") & "'  AND compras.fechaorden <= '" & CDate(Me.a.Text).ToString("yyyy/MM/dd") & "' "
        If Me.pendientes.Checked Then
            strTeamQuery += "AND (compras.statusentrega='PENDIENTE' OR compras.statuspago='PENDIENTE') "
        End If
        If Session("permisos") = 2 Then
            strTeamQuery += "AND asociados.bodega= " & funciones.buscabodega(Session("idasociado")).ToString & " "
        End If
        strTeamQuery += "ORDER BY compras.id DESC"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Me.GridCarrito.DataSource = dtrTeam
        Me.GridCarrito.DataBind()

        sqlConn.Close()
        sqlConn.Dispose()
        Me.referencia.Text = ""
    End Sub
    Sub llenagrid(ByVal referencia As Integer)
        Me.GridCarrito.SelectedIndex = -1
        Me.GridDetalle.DataSource = ""
        Me.GridDetalle.DataBind()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
      
        Dim strTeamQuery As String = "SELECT compras.id AS compra, IF( compras.inscripcion >0, IF( compras.statuspago = 'PAGADO', CONCAT( asociados.id, ' ', asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) ,  CONCAT( prospectos.nombre, ' ', prospectos.appaterno, ' ', prospectos.apmaterno) ) , CONCAT( asociados.id, ' ', asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) ) AS asociado, compras.fechaorden, compras.total AS monto, compras.statusentrega, compras.statuspago " & _
                                "FROM(compras) INNER JOIN asociados ON compras.asociado = asociados.id LEFT JOIN prospectos ON compras.inscripcion = prospectos.id " & _
                                "WHERE referencia LIKE'%" & Me.referencia.Text & "%' "
       
        strTeamQuery += "ORDER BY compras.id DESC"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Me.GridCarrito.DataSource = dtrTeam
        Me.GridCarrito.DataBind()

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Protected Sub anterior_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles anterior.Click

        llenafechas(DateAdd(DateInterval.Month, -1, CDate(Me.de.Text)))
    End Sub

    Protected Sub siguiente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles siguiente.Click
        llenafechas(DateAdd(DateInterval.Day, 30, CDate(Me.a.Text)))
    End Sub

    Protected Sub primero_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles primero.Click
        llenafechas(funciones.primercompra)
    End Sub

    Protected Sub ultimo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ultimo.Click
        llenafechas(funciones.ultimacompra)
    End Sub

    Protected Sub todos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles todos.CheckedChanged, pendientes.CheckedChanged
        Dim fechade As Date = CDate(Me.de.Text)
        Dim fechaa As Date = CDate(Me.a.Text)
       
        llenagrid(fechade, fechaa)

    End Sub

    Protected Sub GridCarrito_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridCarrito.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then


            'Programmatically reference the PopupControlExtender

            Dim pce As AjaxControlToolkit.PopupControlExtender = CType(e.Row.FindControl("PopupControlExtender1"), AjaxControlToolkit.PopupControlExtender)

            ' Set the BehaviorID
            Dim behaviorID As String = String.Concat("pce", e.Row.RowIndex)
            pce.BehaviorID = behaviorID

            ' Programmatically reference the Image control
            Dim i As Image = CType(e.Row.Cells(1).FindControl("Image1"), Image)

            ' Add the clie nt-side attributes (onmouseover & onmouseout)
            Dim OnMouseOverScript As String = String.Format("$find('{0}').showPopup();", behaviorID)
            Dim OnMouseOutScript As String = String.Format("$find('{0}').hidePopup();", behaviorID)

            i.Attributes.Add("onmouseover", OnMouseOverScript)
            i.Attributes.Add("onmouseout", OnMouseOutScript)
        End If
    End Sub

    Protected Sub GridCarrito_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridCarrito.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(5).Text <> "PENDIENTE" Then
                e.Row.Cells(7).Text = ""
            End If
            If e.Row.Cells(6).Text <> "PENDIENTE" Then
                e.Row.Cells(8).Text = ""
            End If
            If e.Row.Cells(5).Text <> "PENDIENTE" Or e.Row.Cells(6).Text <> "PENDIENTE" Then
                e.Row.Cells(9).Text = ""
            End If
        End If
    End Sub

    Protected Sub GridCarrito_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridCarrito.SelectedIndexChanged
        If Me.GridCarrito.SelectedIndex > -1 Then
            llenagriddetalle()
        End If
    End Sub
    Sub llenagriddetalle()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT comprasdetalle.compra, comprasdetalle.cantidad, paquetes.nombre AS paquete, comprasdetalle.costo, comprasdetalle.puntos " & _
        "FROM comprasdetalle INNER JOIN paquetes ON comprasdetalle.paquete=paquetes.id " & _
        "WHERE comprasdetalle.compra=" & Me.GridCarrito.Rows(Me.GridCarrito.SelectedIndex).Cells(0).Text
       
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Me.GridDetalle.DataSource = dtrTeam
        Me.GridDetalle.DataBind()

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub

    <System.Web.Services.WebMethodAttribute()> <System.Web.Script.Services.ScriptMethodAttribute()> Public Shared Function GetDynamicContent(ByVal contextKey As System.String) As System.String
        Dim sTemp As StringBuilder = New StringBuilder()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT comprasdetalle.compra, comprasdetalle.cantidad, paquetes.nombre AS paquete, comprasdetalle.costo, comprasdetalle.puntos " & _
        "FROM comprasdetalle INNER JOIN paquetes ON comprasdetalle.paquete=paquetes.id " & _
        "WHERE comprasdetalle.compra=" & contextKey

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        sTemp.Append("<table class='overtextonuevo'>")
        sTemp.Append("<tr><td><b>Cantidad</b></td><td><b>Paquete</b></td><td><b>Costo</b></td><td><b>Puntos</b></td></tr>")
        While dtrTeam.Read
            sTemp.Append("<tr><td>" & dtrTeam(1) & "</td><td>" & dtrTeam(2) & "</td><td>" & dtrTeam(3) & "</td><td>" & dtrTeam(4) & "</td></tr>")

        End While

        sqlConn.Close()
        sqlConn.Dispose()


        sTemp.Append("</table>")
        Return sTemp.ToString()
    End Function

   
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim pagados(Me.GridCarrito.Rows.Count), entregados(Me.GridCarrito.Rows.Count), cancelados(Me.GridCarrito.Rows.Count), i As Integer
        Dim row As GridViewRow

        For Each row In Me.GridCarrito.Rows

            Dim cb As CheckBox = row.FindControl("CheckBox1")
            If cb.Checked Then

                pagados(i) = 1
            Else
                pagados(i) = 0
            End If
            Dim cb2 As CheckBox = row.FindControl("CheckBox2")
            If cb2.Checked Then

                entregados(i) = 1
            Else
                entregados(i) = 0
            End If
            Dim cb3 As CheckBox = row.FindControl("CheckBox3")
            If cb3.Checked Then

                cancelados(i) = 1
            Else
                cancelados(i) = 0
            End If
            i += 1
        Next
        Dim indicerecorridoalreves As Integer = pagados.Length - 1

        For i = 0 To pagados.Length - 2
            indicerecorridoalreves -= 1
            If cancelados(indicerecorridoalreves) = 1 Then
                actualizacancelado(CInt(Me.GridCarrito.Rows(indicerecorridoalreves).Cells(0).Text))
            Else
                If pagados(indicerecorridoalreves) = 1 Then
                    actualizapagado(CInt(Me.GridCarrito.Rows(indicerecorridoalreves).Cells(0).Text))
                End If
                If entregados(indicerecorridoalreves) = 1 Then
                    actualizaentregado(CInt(Me.GridCarrito.Rows(indicerecorridoalreves).Cells(0).Text))
                End If
            End If






            'anterior
            'If pagados(i) = 1 Then
            ' actualizapagado(CInt(Me.GridCarrito.Rows(i).Cells(0).Text))
            ' End If
            ' If entregados(i) = 1 Then
            ' actualizaentregado(CInt(Me.GridCarrito.Rows(i).Cells(0).Text))
            ' End If
            ' If cancelados(i) = 1 Then
            ' actualizacancelado(CInt(Me.GridCarrito.Rows(i).Cells(0).Text))
            ' End If
        Next
        

        llenagrid(CDate(Me.de.Text), CDate(Me.a.Text))

        Me.mensajes.Text = "Registros Actualizados con Éxito"
        Me.Literal1.Text = erroresdeentrega

    End Sub
    Sub pasaasociado(ByVal prospecto As Integer, ByVal compra As Integer, ByVal patrocinador As Integer)
        Dim password As String = funciones.Crearpassword(8)
        Dim nombre, appaterno, apmaterno, fnac, rfc, curp, compania, telmovil, tellocal, nextel, email, asociadoalias, pais, idioma, callecasa, numcasa, intcasa, colcasa, municipiocasa, estadocasa, callepaq, numpaq, intpaq, colpaq, municipiopaq, estadopaq, tipo, cpcasa, cppaq, CiudadCasa, CiudadPaq, estadocivil As String
        Dim lado As String = ""
        Dim idpadre, bodega As Integer

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT * FROM prospectos WHERE id=" & prospecto.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim fechadenac As Date
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam("nombre")) Then nombre = dtrTeam("nombre").ToString
            If Not IsDBNull(dtrTeam("ApPaterno")) Then appaterno = dtrTeam("ApPaterno").ToString
            If Not IsDBNull(dtrTeam("ApMaterno")) Then apmaterno = dtrTeam("ApMaterno").ToString
            If Not IsDBNull(dtrTeam("fnac")) Then
                fechadenac = dtrTeam("fnac")
                fnac = fechadenac.ToString("yyyy/MM/dd")
            End If

            If Not IsDBNull(dtrTeam("RFC")) Then rfc = dtrTeam("RFC").ToString
            If Not IsDBNull(dtrTeam("Curp")) Then curp = dtrTeam("Curp").ToString
            If Not IsDBNull(dtrTeam("Compania")) Then compania = dtrTeam("Compania").ToString
            If Not IsDBNull(dtrTeam("TelLocal")) Then tellocal = dtrTeam("TelLocal").ToString
            If Not IsDBNull(dtrTeam("TelMovil")) Then telmovil = dtrTeam("TelMovil").ToString
            If Not IsDBNull(dtrTeam("Nextel")) Then nextel = dtrTeam("Nextel").ToString
            If Not IsDBNull(dtrTeam("email")) Then email = dtrTeam("email").ToString
            If Not IsDBNull(dtrTeam("Alias")) Then asociadoalias = dtrTeam("Alias").ToString
            If Not IsDBNull(dtrTeam("Pais")) Then pais = dtrTeam("Pais").ToString
            If Not IsDBNull(dtrTeam("Idioma")) Then idioma = dtrTeam("Idioma").ToString
            If Not IsDBNull(dtrTeam("CalleCasa")) Then callecasa = dtrTeam("CalleCasa").ToString
            If Not IsDBNull(dtrTeam("NumCasa")) Then numcasa = dtrTeam("NumCasa").ToString
            If Not IsDBNull(dtrTeam("IntCasa")) Then intcasa = dtrTeam("IntCasa").ToString
            If Not IsDBNull(dtrTeam("ColCasa")) Then colcasa = dtrTeam("ColCasa").ToString
            If Not IsDBNull(dtrTeam("MunicipioCasa")) Then municipiocasa = dtrTeam("MunicipioCasa").ToString
            If Not IsDBNull(dtrTeam("EstadoCasa")) Then estadocasa = dtrTeam("EstadoCasa").ToString
            If Not IsDBNull(dtrTeam("CallePaq")) Then callepaq = dtrTeam("CallePaq").ToString
            If Not IsDBNull(dtrTeam("NumPaq")) Then numpaq = dtrTeam("NumPaq").ToString
            If Not IsDBNull(dtrTeam("IntPaq")) Then intpaq = dtrTeam("IntPaq").ToString
            If Not IsDBNull(dtrTeam("ColPaq")) Then colpaq = dtrTeam("ColPaq").ToString
            If Not IsDBNull(dtrTeam("MunicipioPaq")) Then municipiopaq = dtrTeam("MunicipioPaq").ToString
            If Not IsDBNull(dtrTeam("EstadoPaq")) Then estadopaq = dtrTeam("EstadoPaq").ToString
            If Not IsDBNull(dtrTeam("Tipo")) Then tipo = dtrTeam("Tipo").ToString
            If Not IsDBNull(dtrTeam("CPCasa")) Then cpcasa = dtrTeam("CPCasa").ToString
            If Not IsDBNull(dtrTeam("CPPaq")) Then cppaq = dtrTeam("CPPaq").ToString
            If Not IsDBNull(dtrTeam("lado")) Then lado = dtrTeam("lado").ToString
            If Not IsDBNull(dtrTeam("padre")) Then idpadre = dtrTeam("padre").ToString
            If Not IsDBNull(dtrTeam("CiudadCasa")) Then CiudadCasa = dtrTeam("CiudadCasa").ToString
            If Not IsDBNull(dtrTeam("CiudadPaq")) Then CiudadPaq = dtrTeam("CiudadPaq").ToString
            If Not IsDBNull(dtrTeam("EstadoCivil")) Then estadocivil = dtrTeam("EstadoCivil").ToString
        End While

        sqlConn.Close()

        'valida que esté libre el padre
        Dim fun As New funciones
        idpadre = fun.validapadre(idpadre, lado)

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)

        Dim puntos As Integer = funciones.puntosdelacompra(compra)

        Dim status As Integer = 1
        Dim fechafinactivacion As Date = DateAdd(DateInterval.Month, 1, Date.Today)
        fechafinactivacion = DateAdd(DateInterval.Day, -1, fechafinactivacion)


        Dim recorridostr As String = recorrido(idpadre)
        Dim ladosrecorridostr As String = ladosrecorrido(lado, idpadre)
        Dim ladopatrocinadorstr As String = ladopatrocinador(recorridostr, ladosrecorridostr, patrocinador)
        bodega = funciones.buscabodega(patrocinador)
       
        sqlConn.Open()

        strTeamQuery = "INSERT INTO asociados (`nombre`, `ApPaterno` ,`ApMaterno`, `FNac`, `RFC`, `CURP`, `Compania`, `TelLocal`, `TelMovil`, `Nextel`, `Email`, `Alias`, `Pais`, `Idioma`, `CalleCasa`, `NumCasa`, `IntCasa`, `ColCasa`, `CPCasa`, `MunicipioCasa`, `EstadoCasa`, `CallePaq`, `NumPaq`, `IntPaq`, `ColPaq`, `CPPaq`, `MunicipioPaq`, `EstadoPaq`, `Tipo`, `FInsc`, `Patrocinador`, `Padre`, `Lado`, `Orden`, `Rango`,  `Status`, `PtsMes`, rangopago, historia, recorrido, ladosrecorrido, ladopatrocinador, nivel, bodega, inicioactivacion, finactivacion, password, ciudadcasa, ciudadpaq, estadocivil  ) VALUES ('" & nombre & "', '" & appaterno & "', '" & apmaterno & "', '" & fnac & "', '" & rfc & "', '" & curp & "', '" & compania & "', '" & tellocal & "', '" & telmovil & "', '" & nextel & "', '" & email & "', '" & asociadoalias & "', '" & pais & "', '" & idioma & "', '" & callecasa & "', '" & numcasa & "', '" & intcasa & "', '" & colcasa & "', '" & cpcasa & "', '" & municipiocasa & "', '" & Trim(estadocasa) & "', '" & callepaq & "', '" & numpaq & "', '" & intpaq & "', '" & colpaq & "', '" & cppaq & "', '" & municipiopaq & "', '" & Trim(estadopaq) & "',  " & tipo.ToString & ", '" & Today.Year.ToString & "/" & Today.Month.ToString & "/" & Today.Day.ToString & "', " & patrocinador.ToString & ", " & idpadre.ToString & ", '" & UCase(lado) & "', " & orden(patrocinador).ToString & ", 1," & status.ToString & "," & puntos.ToString & ", 1, '" & historia(patrocinador) & "', '" & recorridostr & "', '" & ladosrecorridostr & "', '" & ladopatrocinadorstr & "', 0, " & bodega.ToString & ", '" & Today.ToString("yyyy/MM/dd") & "', '" & DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, Today)).ToString("yyyy/MM/dd") & "', '" & password & "', '" & CiudadCasa & "', '" & CiudadPaq & "', '" & estadocivil & "')"



        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()

        'crea alias
        strTeamQuery = "SELECT MAX(id) FROM asociados WHERE patrocinador=" & patrocinador.ToString
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)

        Dim nuevoid As Integer = 0

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            nuevoid = dtrTeam(0)
        End While
        sqlConn.Close()
        actualizabono6(nuevoid)
        Dim aliasstr As String = UCase(Left(nombre, 3)) & nuevoid.ToString
        sqlConn.Open()

        strTeamQuery = "UPDATE asociados SET alias='" & aliasstr & "' WHERE id=" & nuevoid.ToString



        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()



        'para mail
     
        strTeamQuery = "SELECT nombre, appaterno, apmaterno FROM asociados WHERE id=" & patrocinador.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)

        Dim patronombre As String = ""

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then patronombre = dtrTeam(0)
            If Not IsDBNull(dtrTeam(1)) Then patronombre += " " & dtrTeam(1)
        End While
        sqlConn.Close()
        Dim correo As New mails
        correo.enviamaildebienvenida(nuevoid)

    End Sub
    Dim antepasado As Integer = 0


    Sub buscaantepasadomayoratres(ByVal asociado As Integer)

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

    End Sub
    Sub actualizabono6(ByVal id As Integer)
        buscaantepasadomayoratres(id)
        actualizabono6(id, antepasado)
    End Sub
    Sub actualizabono6(ByVal idasociado As Integer, ByVal idbono6 As Integer)
        'inserta en tabla pagos
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "UPDATE asociados SET bono6=" & idbono6.ToString & " WHERE id=" & idasociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        'inserta en tabla temporal bono6

    End Sub

    Function orden(ByVal patrocinador As Integer) As Integer


        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT MAX(orden) FROM asociados WHERE patrocinador=" & patrocinador.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        Dim ordenarbol As Integer = 0

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then ordenarbol = dtrTeam(0)
        End While

        sqlConn.Close()
        ordenarbol += 1
        Return ordenarbol
    End Function
    Function historia(ByVal patrocinador As Integer) As String
        Dim respuesta As String = ""
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT historia FROM asociados WHERE id=" & patrocinador.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then respuesta = dtrTeam(0).ToString
        End While
        respuesta += Session("idasociado").ToString & "."
        sqlConn.Close()
        Return respuesta
    End Function
    Function ladopatrocinador(ByVal recorrido As String, ByVal ladosrecorrido As String, ByVal patrocinador As Integer) As String
        Dim respuesta As String = ""
        Dim asociados() As String = Split(recorrido, ".")
        Dim lados() As String = Split(ladosrecorrido, ".")
        Dim i As Integer = 0
        For i = 0 To asociados.Length - 1
            If asociados(i) = patrocinador.ToString Then
                Exit For
            End If
        Next

        respuesta = lados(i)

        Return respuesta
    End Function
    Function ladosrecorrido(ByVal lado As String, ByVal padre As Integer) As String
        Dim respuesta As String = ""
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT ladosrecorrido FROM asociados WHERE id=" & padre.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then respuesta = dtrTeam(0).ToString
        End While
        sqlConn.Close()



        respuesta += UCase(lado) & "."

        Return respuesta
    End Function
    Function recorrido(ByVal idpadre As Integer) As String
        Dim respuesta As String = ""
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT recorrido FROM asociados WHERE id=" & idpadre.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then respuesta = dtrTeam(0).ToString
        End While
        sqlConn.Close()



        respuesta += idpadre.ToString & "."

        Return respuesta
    End Function
    Sub cambiadiasociadoencompra(ByVal compra As Integer, ByVal asociado As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

       

        sqlConn.Open()
        strTeamQuery = "UPDATE compras SET asociado=" & asociado.ToString & " WHERE id=" & compra.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()
        sqlConn.Close()
    End Sub
    Sub eliminaprospecto(ByVal id As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)



        sqlConn.Open()
        strTeamQuery = "DELETE FROM prospectos  WHERE id=" & id.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()
        sqlConn.Close()
    End Sub
    Function nuevoasociado(ByVal patrocinador As Integer) As Integer
        Dim respuesta As Integer = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT MAX(id) FROM asociados WHERE patrocinador=" & patrocinador.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then respuesta = dtrTeam(0).ToString
        End While
        sqlConn.Close()
        Return respuesta
    End Function
    Sub actualizapagado(ByVal compra As Integer)
        'antes de hacer cualquier cosa, checa si sigue siendo de activación o excedente, dependiendo de si su autor está activo o no
        Dim funcionescompra As New funciones
        funcionescompra.RevisaCompra(compra)

        'hasta acá
        Dim inscripcion As Integer = funciones.escompradeinscripcion(compra)
        Dim asociado As Integer = funciones.quienhacelacompra(compra)
        Dim puntos As Integer = funciones.puntosdelacompra(compra)
        If inscripcion > 0 Then
            pasaasociado(inscripcion, compra, asociado)
            eliminaprospecto(inscripcion)
            Dim idnuevo As Integer = nuevoasociado(asociado)
            cambiadiasociadoencompra(compra, idnuevo)
            asociado = idnuevo
        End If

        'actualiza compra

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        strTeamQuery = "UPDATE compras SET statuspago='PAGADO', fecha='" & Today.Year & "/" & Today.Month & "/" & Today.Day & "' WHERE id=" & compra.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()
        sqlConn.Close()


        'actualiza parámetros, si es compra de activación
        sqlConn.Open()
        strTeamQuery = "SELECT activacion, inicioactivacion, finactivacion FROM compras  WHERE id=" & compra.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim activacion As Integer
        Dim inicio, fin As Date
        While dtrTeam.Read
            If dtrTeam(0) = 1 Then
                activacion = 1
                inicio = dtrTeam(1)
                fin = dtrTeam(2)
            End If

        End While
        sqlConn.Close()

        If activacion = 1 And inscripcion = 0 Then
            sqlConn.Open()
            strTeamQuery = "SELECT  inicioactivacion, finactivacion FROM asociados WHERE id=" & asociado.ToString

            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            dtrTeam = cmdFetchTeam.ExecuteReader()

            Dim inicioactual, finactual As Date
            While dtrTeam.Read


                inicioactual = dtrTeam(0)
                finactual = dtrTeam(1)


            End While
            sqlConn.Close()
            If finactual >= Date.Today Then
                sqlConn.Open()
                strTeamQuery = "UPDATE asociados SET status=1, finactivacion='" & fin.ToString("yyyy/MM/dd") & "', ptsmes=" & puntos.ToString & "  WHERE id=" & asociado.ToString

                cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                cmdFetchTeam.ExecuteNonQuery()
                sqlConn.Close()
            Else
                sqlConn.Open()
                strTeamQuery = "UPDATE asociados SET  status=1, finactivacion='" & fin.ToString("yyyy/MM/dd") & "', inicioactivacion='" & inicio.ToString("yyyy/MM/dd") & "', ptsmes=" & puntos.ToString & "  WHERE id=" & asociado.ToString

                cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
                cmdFetchTeam.ExecuteNonQuery()
                sqlConn.Close()
            End If
        End If
        'sube puntos

        funciones.subepuntos(compra, puntos, asociado)

    End Sub
    Sub actualizaentregado(ByVal compra As Integer)
        Dim asociado = funciones.quienhacelacompra(compra)
        Dim bodega As Integer = funciones.buscabodega(asociado)
        'revisa si hay producto suficiente
        If Not funciones.haysuficienteenbodega(compra, bodega) Then
            erroresdeentrega += "No se pudo entregar la compra " & compra.ToString & ". No hay producto suficiente <br />"

            Exit Sub
        End If
        ' actualiza compra

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader



        sqlConn.Open()
        strTeamQuery = "UPDATE compras SET statusentrega='ENTREGADO' WHERE id=" & compra.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()
        sqlConn.Close()
        'descuenta de bodega

        sqlConn.Open()
        strTeamQuery = "SELECT  cantidad, paquete FROM comprasdetalle WHERE compra=" & compra.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        dtrTeam = cmdFetchTeam.ExecuteReader()


        While dtrTeam.Read

            funciones.descuentadebodegas(bodega, dtrTeam(1), dtrTeam(0))



        End While
        sqlConn.Close()
    End Sub
    Sub actualizacancelado(ByVal compra As Integer)
        ' actualiza compra
        Dim asociado = funciones.quienhacelacompra(compra)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
      


        sqlConn.Open()
        strTeamQuery = "UPDATE compras SET statuspago='CANCELADO', statusentrega='CANCELADO'  WHERE id=" & compra.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()
        sqlConn.Close()
       
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Me.referencia.Text <> "" Then
            llenagrid(CInt(Me.referencia.Text))
        End If
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim fechade As Date = CDate(Me.de.Text)
        Dim fechaa As Date = CDate(Me.a.Text)

        llenagrid(fechade, fechaa)
    End Sub

    
End Class
