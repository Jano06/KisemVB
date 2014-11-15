Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Partial Class sw_inscripciones_compra
    Inherits System.Web.UI.Page
    Dim antepasado As Integer = 0
    Dim bodega As Integer = 0
    Dim funciones As New funciones
    Sub llenagrid()

        Me.GridView1.SelectedIndex = -1
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, nombre, ApPaterno, ApMaterno, TelMovil, Email FROM prospectos WHERE patrocinador=" & Session("idasociado").ToString
      
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
       
        Me.GridView1.DataSource = dtrTeam
        Me.GridView1.DataBind()

        sqlConn.Close()
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Me.GridView1.DataKeys.Count = 0 Then Me.GridView1.DataKeyNames = New String() {"id"}

        llenagrid()
        If Me.GridView1.Rows.Count < 1 Then

            Me.mensajes.Text = "Usted no tiene ningún prospecto dado de alta"
            Me.mensajes.Visible = True
        Else
            llenagridpaquetes()
        End If
        Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
        objEtiqueta.Text = "Compra de Inscripción"
    End Sub
    Sub llenagridpaquetes()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, nombre, puntos, costo FROM paquetes"

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Me.GridView2.DataSource = dtrTeam
        Me.GridView2.DataBind()

        sqlConn.Close()
        For Each row In Me.GridView2.Rows
            Dim cb As TextBox = row.FindControl("TextBox1")

            If row.cells(0).Text = "0" Then
                cb.Text = "1"
                cb.Enabled = False
            End If


            
        Next
    End Sub

    
    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Me.Button1.Visible = True
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        pasaasociado()
        Dim id As Integer = nuevoasociado()

        actualizabono6(id)
        insertacompra(id)

        eliminaprospecto()
        llenagrid()
        Me.mensajes.Text = "Asociado Registrado con Éxito"
        Me.mensajes.Visible = True
    End Sub
    Function nuevoasociado() As Integer
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT MAX(id)  FROM asociados WHERE patrocinador=" & Session("idasociado").ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        Dim id As Integer
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            id = dtrTeam(0)
        End While

        sqlConn.Close()
        Return id
    End Function
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
    Sub insertacompra(ByVal id As Integer)
        Dim idpaq As New List(Of Integer)
        Dim cantidades As New List(Of Integer)
        Dim puntos As New List(Of Integer)
        Dim costo As New List(Of Decimal)
        For Each row In Me.GridView2.Rows
            Dim cb As TextBox = row.FindControl("TextBox1")

            If CInt(cb.Text) > 0 Then
                idpaq.Add(CInt(row.cells(0).Text))
                puntos.Add(CInt(row.cells(2).Text))
                costo.Add(CInt(row.cells(3).Text))
                cantidades.Add(CInt(cb.Text))
            End If



        Next
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim activacion As Integer = 1
        Dim bandera As Integer = 0
        For cont = 0 To idpaq.Count - 1
            If cont > 0 And activacion = 0 And bandera = 0 Then
                activacion = 1
                bandera = 1
            Else
                activacion = 0
            End If
            sqlConn.Open()
            strTeamQuery = "INSERT INTO compras(asociado, paquete, cantidad, puntos, costo, fecha, activacion, autor) VALUES(" & id.ToString & ", " & idpaq(cont).ToString & ", " & cantidades(cont).ToString & ", " & puntos(cont).ToString & ", " & costo(cont).ToString & ", '" & Today.Year & "/" & Today.Month & "/" & Today.Day & "', " & activacion.ToString & ", 'OV')"

            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            cmdFetchTeam.ExecuteNonQuery()


            sqlConn.Close()
            funciones.descuentadebodegas(bodega, idpaq(cont), cantidades(cont))
            Dim compra As Integer = recuperacompra(id)
            'agrega referencia a compra
            Dim referencia As Integer = funciones.referencia(compra)

            sqlConn.Open()

            strTeamQuery = "UPDATE compras SET referencia=" & referencia.ToString & " WHERE id=" & compra.ToString



            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            cmdFetchTeam.ExecuteNonQuery()


            sqlConn.Close()
            sqlConn.Dispose()
            If puntos(cont) > 0 Then subepuntos(compra, puntos(cont), id)
        Next
       
    End Sub
    Function recuperacompra(ByVal asociado As Integer) As Integer
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT MAX(id) FROM compras WHERE asociado=" & asociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        Dim id As Integer
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then id = dtrTeam(0)


        End While

        sqlConn.Close()
        Return id
    End Function
    Sub subepuntos(ByVal compra As Integer, ByVal puntos As Integer, ByVal asociado As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT padre, lado FROM asociados WHERE id=" & asociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
    
        Dim dtrTeam As MySqlDataReader
        Dim padre As Integer = 0
        Dim lado As String = ""
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then padre = dtrTeam(0)
            If Not IsDBNull(dtrTeam(1)) Then lado = dtrTeam(1)

        End While

        sqlConn.Close()


        sqlConn.Open()
        strTeamQuery = "INSERT INTO puntosasociados(asociado, compra, lado, puntos, status, porpagar) VALUES(" & padre.ToString & ", " & compra.ToString & ", '" & lado & "', " & puntos.ToString & ", 0, " & puntos.ToString & ")"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()

        strTeamQuery = "SELECT padre, lado FROM asociados WHERE id=" & padre.ToString
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)

        Dim abuelo As Integer = 0
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then abuelo = dtrTeam(0)
            If Not IsDBNull(dtrTeam(1)) Then lado = dtrTeam(1)

        End While

        sqlConn.Close()
        If abuelo > 3 Then subepuntos(compra, puntos, padre)
    End Sub
    Sub eliminaprospecto()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "DELETE FROM prospectos WHERE id=" & Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
    Sub pasaasociado()
        Dim nombre, appaterno, apmaterno, fnac, rfc, curp, compania, telmovil, tellocal, nextel, email, asociadoalias, pais, idioma, callecasa, numcasa, intcasa, colcasa, municipiocasa, estadocasa, callepaq, numpaq, intpaq, colpaq, municipiopaq, estadopaq, tipo, patrocinador, cpcasa, cppaq, estadocivil As String
        Dim lado As String = "i"
        If Session("lado") = 2 Then lado = "d"

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT * FROM prospectos WHERE id=" & Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value.ToString

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
            If Not IsDBNull(dtrTeam("EstadoCivil")) Then estadocivil = dtrTeam("EstadoCivil").ToString

        End While

        sqlConn.Close()
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)

        Dim puntos As Integer = obtienepuntos()
        Dim status As Integer = 0
        Dim fechafinactivacion As Date = DateAdd(DateInterval.Month, 1, Date.Today)
        fechafinactivacion = DateAdd(DateInterval.Day, -1, fechafinactivacion)
        If puntos > 0 Then
            status = 1

        End If

        Dim recorridostr As String = recorrido()
        Dim ladosrecorridostr As String = ladosrecorrido(lado)
        Dim ladopatrocinadorstr As String = ladopatrocinador(recorridostr, ladosrecorridostr)
        bodega = funciones.buscabodega(Session("idasociado"))
        sqlConn.Open()
        If status = 1 Then
            strTeamQuery = "INSERT INTO asociados (`nombre`, `ApPaterno` ,`ApMaterno`, `FNac`, `RFC`, `CURP`, `Compania`, `TelLocal`, `TelMovil`, `Nextel`, `Email`, `Alias`, `Pais`, `Idioma`, `CalleCasa`, `NumCasa`, `IntCasa`, `ColCasa`, `CPCasa`, `MunicipioCasa`, `EstadoCasa`, `CallePaq`, `NumPaq`, `IntPaq`, `ColPaq`, `CPPaq`, `MunicipioPaq`, `EstadoPaq`, `Tipo`, `FInsc`, `Patrocinador`, `Padre`, `Lado`, `Orden`, `Rango`,  `Status`, `PtsMes`, rangopago, historia, recorrido, ladosrecorrido, ladopatrocinador, nivel, bodega, inicioactivacion, finactivacion, estadocivil ) VALUES ('" & nombre & "', '" & appaterno & "', '" & apmaterno & "', '" & fnac & "', '" & rfc & "', '" & curp & "', '" & compania & "', '" & tellocal & "', '" & telmovil & "', '" & nextel & "', '" & email & "', '" & asociadoalias & "', '" & pais & "', '" & idioma & "', '" & callecasa & "', '" & numcasa & "', '" & intcasa & "', '" & colcasa & "', '" & cpcasa & "', '" & municipiocasa & "', '" & Trim(estadocasa) & "', '" & callepaq & "', '" & numpaq & "', '" & intpaq & "', '" & colpaq & "', '" & cppaq & "', '" & municipiopaq & "', '" & Trim(estadopaq) & "',  " & tipo.ToString & ", '" & Today.Year.ToString & "/" & Today.Month.ToString & "/" & Today.Day.ToString & "', " & Session("idasociado").ToString & ", " & Session("padre").ToString & ", '" & UCase(lado) & "', " & orden.ToString & ", 1," & status.ToString & "," & puntos.ToString & ", 1, '" & historia() & "', '" & recorridostr & "', '" & ladosrecorridostr & "', '" & ladopatrocinadorstr & "', 0, " & bodega.ToString & ", '" & Today.Year.ToString & "/" & Today.Month.ToString & "/" & Today.Day.ToString & "', '" & fechafinactivacion.Year.ToString & "/" & fechafinactivacion.Month.ToString & "/" & fechafinactivacion.Day.ToString & "', '" & estadocivil & "')"
        Else
            strTeamQuery = "INSERT INTO asociados (`nombre`, `ApPaterno` ,`ApMaterno`, `FNac`, `RFC`, `CURP`, `Compania`, `TelLocal`, `TelMovil`, `Nextel`, `Email`, `Alias`, `Pais`, `Idioma`, `CalleCasa`, `NumCasa`, `IntCasa`, `ColCasa`, `CPCasa`, `MunicipioCasa`, `EstadoCasa`, `CallePaq`, `NumPaq`, `IntPaq`, `ColPaq`, `CPPaq`, `MunicipioPaq`, `EstadoPaq`, `Tipo`, `FInsc`, `Patrocinador`, `Padre`, `Lado`, `Orden`, `Rango`,  `Status`, `PtsMes`, rangopago, historia, recorrido, ladosrecorrido, ladopatrocinador, nivel, bodega, estadocivil ) VALUES ('" & nombre & "', '" & appaterno & "', '" & apmaterno & "', '" & fnac & "', '" & rfc & "', '" & curp & "', '" & compania & "', '" & tellocal & "', '" & telmovil & "', '" & nextel & "', '" & email & "', '" & asociadoalias & "', '" & pais & "', '" & idioma & "', '" & callecasa & "', '" & numcasa & "', '" & intcasa & "', '" & colcasa & "', '" & cpcasa & "', '" & municipiocasa & "', '" & Trim(estadocasa) & "', '" & callepaq & "', '" & numpaq & "', '" & intpaq & "', '" & colpaq & "', '" & cppaq & "', '" & municipiopaq & "', '" & Trim(estadopaq) & "',  " & tipo.ToString & ", '" & Today.Year.ToString & "/" & Today.Month.ToString & "/" & Today.Day.ToString & "', " & Session("idasociado").ToString & ", " & Session("padre").ToString & ", '" & UCase(lado) & "', " & orden.ToString & ", 1," & status.ToString & "," & puntos.ToString & ", 1, '" & historia() & "', '" & recorridostr & "', '" & ladosrecorridostr & "', '" & ladopatrocinadorstr & "', 0, " & bodega.ToString & ", '" & estadocivil & "')"
        End If


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
    
    Function ladopatrocinador(ByVal recorrido As String, ByVal ladosrecorrido As String) As String
        Dim respuesta As String = ""
        Dim asociados() As String = Split(recorrido, ".")
        Dim lados() As String = Split(ladosrecorrido, ".")
        Dim i As Integer = 0
        For i = 0 To asociados.Length - 1
            If asociados(i) = Session("idasociado").ToString Then
                Exit For
            End If
        Next

        respuesta = lados(i)

        Return respuesta
    End Function
    Function ladosrecorrido(ByVal lado As String) As String
        Dim respuesta As String = ""
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT ladosrecorrido FROM asociados WHERE id=" & Session("padre").ToString

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
    Function recorrido() As String
        Dim respuesta As String = ""
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT recorrido FROM asociados WHERE id=" & Session("padre").ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then respuesta = dtrTeam(0).ToString
        End While
        sqlConn.Close()



        respuesta += Session("padre").ToString & "."

        Return respuesta
    End Function
    Function historia() As String
        Dim respuesta As String = ""
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT historia FROM asociados WHERE id=" & Session("idasociado").ToString

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
    Function orden() As Integer


        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT MAX(orden) FROM asociados WHERE patrocinador=" & Session("idasociado").ToString

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
    Function obtienepuntos() As Integer
        Dim idpaq As String = ""
        For Each row In Me.GridView2.Rows
            Dim cb As TextBox = row.FindControl("TextBox1")

            If CInt(cb.Text) > 0 Then
                idpaq = row.cells(0).text
            End If



        Next
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT puntos FROM paquetes WHERE id=" & idpaq.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        Dim puntos As Integer = 0

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            puntos = dtrTeam(0)
        End While

        sqlConn.Close()
        Return puntos

    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then Me.mensajes.Visible = False
    End Sub
End Class
