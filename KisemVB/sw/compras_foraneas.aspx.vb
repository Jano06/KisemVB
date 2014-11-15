Imports MySql.Data.MySqlClient
Partial Class sw_compras_foraneas
    Inherits System.Web.UI.Page

    Dim funciones As New funciones
    Dim mensajedeerror As String
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Compras Foráneas"


        End If
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

        strTeamQuery = "INSERT INTO asociados (`nombre`, `ApPaterno` ,`ApMaterno`, `FNac`, `RFC`, `CURP`, `Compania`, `TelLocal`, `TelMovil`, `Nextel`, `Email`, `Alias`, `Pais`, `Idioma`, `CalleCasa`, `NumCasa`, `IntCasa`, `ColCasa`, `CPCasa`, `MunicipioCasa`, `EstadoCasa`, `CallePaq`, `NumPaq`, `IntPaq`, `ColPaq`, `CPPaq`, `MunicipioPaq`, `EstadoPaq`, `Tipo`, `FInsc`, `Patrocinador`, `Padre`, `Lado`, `Orden`, `Rango`,  `Status`, `PtsMes`, rangopago, historia, recorrido, ladosrecorrido, ladopatrocinador, nivel, bodega, inicioactivacion, finactivacion, password, ciudadcasa, ciudadpaq, estadocivil  ) VALUES ('" & nombre & "', '" & appaterno & "', '" & apmaterno & "', '" & fnac & "', '" & rfc & "', '" & curp & "', '" & compania & "', '" & tellocal & "', '" & telmovil & "', '" & nextel & "', '" & email & "', '" & asociadoalias & "', '" & pais & "', '" & idioma & "', '" & callecasa & "', '" & numcasa & "', '" & intcasa & "', '" & colcasa & "', '" & cpcasa & "', '" & municipiocasa & "', '" & Trim(estadocasa) & "', '" & callepaq & "', '" & numpaq & "', '" & intpaq & "', '" & colpaq & "', '" & cppaq & "', '" & municipiopaq & "', '" & Trim(estadopaq) & "',  " & tipo.ToString & ", '" & Today.Year.ToString & "/" & Today.Month.ToString & "/" & Today.Day.ToString & "', " & patrocinador.ToString & ", " & idpadre.ToString & ", '" & UCase(lado) & "', " & orden(patrocinador).ToString & ", 1," & status.ToString & "," & puntos.ToString & ", 1, '" & historia(patrocinador) & "', '" & recorridostr & "', '" & ladosrecorridostr & "', '" & ladopatrocinadorstr & "', 0, " & bodega.ToString & ", '" & Today.Year.ToString & "/" & Today.Month.ToString & "/" & Today.Day.ToString & "', '" & fechafinactivacion.Year.ToString & "/" & fechafinactivacion.Month.ToString & "/" & fechafinactivacion.Day.ToString & "', '" & password & "', '" & CiudadCasa & "', '" & CiudadPaq & "', '" & estadocivil & "')"



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

        If activacion = 1 Then
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
        ' actualiza compra
        Dim asociado = funciones.quienhacelacompra(compra)
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
        Dim bodega As Integer = funciones.buscabodega(Session("idasociado"))
        sqlConn.Open()
        strTeamQuery = "SELECT  cantidad, paquete FROM comprasdetalle WHERE compra=" & compra.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        dtrTeam = cmdFetchTeam.ExecuteReader()


        While dtrTeam.Read

            funciones.descuentadebodegas(bodega, dtrTeam(1), dtrTeam(0))



        End While
        sqlConn.Close()
    End Sub
   



    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim compra As Integer = infook()
        If compra > 0 Then


            If Me.pagada.Checked Or Me.entregada.Checked Then
                If Me.pagada.Checked Then actualizapagado(compra)
                If Me.entregada.Checked Then actualizaentregado(compra)
                Me.mensajes.Text = "Registro realizado con éxito"
            Else
                Me.mensajes.Text = "La compra debe ser entregada y/o pagada"
            End If
        Else
            Me.mensajes.Text = mensajedeerror
        End If
    End Sub
    Function infook() As Integer
        Dim respuesta As Integer = 0
        Dim entregado, pagado As String
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, statuspago, statusentrega FROM compras WHERE referencia=" & Me.referencia.Text & " AND asociado=" & Me.distribuidor.Text
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            respuesta = dtrTeam(0)
            entregado = dtrTeam(2)
            pagado = dtrTeam(1)

        End While

        sqlConn.Close()
        If respuesta = 0 Then mensajedeerror = "Datos de compra inválidos."
        If Me.entregada.Checked And entregado = "ENTREGADO" Then
            mensajedeerror += " Compra ya entregada."
            respuesta = 0
        End If

        If Me.pagada.Checked And pagado = "PAGADO" Then
            mensajedeerror += " Compra ya pagada."
            respuesta = 0
        End If


        Return respuesta
    End Function
End Class
