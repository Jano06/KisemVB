Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Partial Class sw_compradeinscripcion
    Inherits System.Web.UI.Page
    Dim antepasado As Integer = 0
    Dim bodega As Integer = 0
    Dim funciones As New funciones

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Me.GridView1.DataKeys.Count = 0 Then Me.GridView1.DataKeyNames = New String() {"id"}
        If Not IsPostBack Then
            Me.idpadre.Text = Session("padre").ToString
            Me.ladopadre.Text = Session("lado").ToString
            limpiacarrito()


            funciones.llenaformadepago(Me.formadepago, Session("permisos"))
            llenagrid()
            If Me.GridView1.Rows.Count < 1 Then

                Me.mensajes.Text = "Usted no tiene ningún prospecto dado de alta"
                Me.mensajes.Visible = True
                Me.panelcomprar.visible = False
            Else
                'llenagridpaquetes()
            End If
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Compra de Inscripción"

            anadiracarrito("Inscripción", 1, funciones.costopaquete(0), 0, 1)
        End If


    End Sub
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        llenatablapaquetes()

    End Sub
    Sub anadiracarrito(ByVal paquete As String, ByVal cantidad As Integer, ByVal precio As Decimal, ByVal idpaquete As Integer, Optional ByVal inicio As Integer = 0)
        If Session("numpaq") = Nothing Then Session("numpaq") = 0
        For i = 0 To Session("numpaq")
            If Session("paquete" & i.ToString) = paquete And Not Session("paquete" & i.ToString) = Nothing Then
                Session("cantidad" & i.ToString) += cantidad
                mostrarcarrito()
                Exit Sub
            End If
        Next
        Dim indice As Integer = Session("numpaq") + 1
        Session("paquete" & indice.ToString) = paquete
        Session("cantidad" & indice.ToString) = cantidad
        Session("precio" & indice.ToString) = precio
        Session("idpaquete" & indice.ToString) = idpaquete
        Session("numpaq") = indice
        If inicio = 0 Then mostrarcarrito()

    End Sub
    Sub mostrarcarrito()
        If Session("numpaq") = 0 Then Exit Sub
        Dim ds As New DataSet
        Dim table As DataTable = ds.Tables.Add("Data")
        table.Columns.Add("cantidad", GetType(Integer))
        table.Columns.Add("paquete", GetType(String))
        table.Columns.Add("precio", GetType(Decimal))
        table.Columns.Add("monto", GetType(Decimal))
        Dim total As Decimal = 0
        For i = 1 To Session("numpaq")
            table.Rows.Add(New Object() {Session("cantidad" & i.ToString), Session("paquete" & i.ToString).ToString, Session("precio" & i.ToString), (Session("cantidad" & i.ToString) * Session("precio" & i.ToString))})
            total += (Session("cantidad" & i.ToString) * Session("precio" & i.ToString))
        Next
        table.Rows.Add(New Object() {Nothing, Nothing, Nothing, total})
        Dim viewcompras As DataView = ds.Tables(0).DefaultView
        Me.GridCarrito.DataSource = viewcompras
        Me.GridCarrito.DataBind()
        Me.PanelCarrito.Visible = True
        Me.PanelComprar.Visible = False
    End Sub
    Sub llenatablapaquetes()
        'toma info de paquetes
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, costo, nombre FROM paquetes WHERE id>=1"

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        Dim contador As Integer = 0
        Dim idpaquetes As New List(Of Integer)
        Dim costopaquetes As New List(Of Decimal)
        Dim nombrepaquetes As New List(Of String)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            contador += 1
            idpaquetes.Add(dtrTeam(0))
            costopaquetes.Add(dtrTeam(1))
            nombrepaquetes.Add(dtrTeam(2))
        End While

        sqlConn.Close()
        sqlConn.Dispose()



        Dim rowCnt As Integer
        ' Current row count
        Dim rowCtr As Integer
        ' Total number of cells (columns).
        Dim cellCtr As Integer
        ' Current cell counter.
        Dim cellCnt As Integer
        cellCnt = 4
        Dim division As Decimal = (contador / cellCnt)

        rowCnt = Decimal.Ceiling(division)
        Dim indice As Integer = 0
        For rowCtr = 1 To rowCnt
            Dim tRow As New TableRow()
            For cellCtr = 1 To cellCnt
                If indice = idpaquetes.Count Then Exit For
                Dim tCell As New TableCell()
                ' Mock up a product ID
                Dim nombre As New Label
                nombre.ID = "nombre" & idpaquetes(indice).ToString
                nombre.Text = nombrepaquetes(indice)
                nombre.CssClass = "titulo"
                tCell.Controls.Add(nombre)
                Dim s As New LiteralControl()
                s.Text = "<br />"
                ' Add to cell.
                tCell.Controls.Add(s)
                Dim foto As New Image
                foto.ID = "foto" & idpaquetes(indice).ToString
                foto.ImageUrl = "img/paquetes/" & idpaquetes(indice).ToString & ".jpg"
                foto.AlternateText = costopaquetes(indice).ToString
                tCell.Controls.Add(foto)
                ' Create literal text as control.


                Dim s2 As New LiteralControl()
                s2.Text = "<br />"
                tCell.Controls.Add(s2)
                Dim cantidad As New TextBox
                cantidad.ID = "cantidad" & idpaquetes(indice).ToString
                cantidad.Text = "1"
                cantidad.Width = 30
                cantidad.Attributes.Add("onkeypress", "javascript:return ValidNum(event);") 'para que acepte solo números

                tCell.Controls.Add(cantidad)

                Dim espacio As New Literal
                espacio.Text = " "
                ' Add to cell.
                tCell.Controls.Add(espacio)
                Dim comprar As New Button
                comprar.ID = idpaquetes(indice).ToString
                comprar.Text = "Comprar"
                comprar.Width = 70
                AddHandler comprar.Click, AddressOf manejabotones
                tCell.Controls.Add(comprar)
                ' Create Hyperlink Web Server control and add to cell.

                ' Add new TableCell object to row.
                tRow.Cells.Add(tCell)
                indice += 1

            Next cellCtr
            ' Add new row to table.
            Table1.Rows.Add(tRow)
        Next rowCtr
    End Sub
    Private Sub manejabotones(ByVal sender As Object, ByVal e As EventArgs)
        Me.mensajes.Text = ""
        If Me.GridView1.SelectedIndex = -1 Then
            Me.mensajes.Text = "Necesita seleccionar un prospecto"
            Exit Sub
        End If

        Me.GridView1.DataSource = ""
        Me.GridView1.DataBind()


        Dim id As String = sender.id.ToString



        Dim contenedor As ContentPlaceHolder = Me.Master.FindControl("ContentPlaceHolder1")
        Dim cantidad As TextBox = CType(contenedor.FindControl("cantidad" & id), TextBox)
        Dim foto As Image = CType(contenedor.FindControl("foto" & id), Image)
        Dim nombre As Label = CType(contenedor.FindControl("nombre" & id), Label)

        anadiracarrito(nombre.Text, CInt(cantidad.Text), CDec(foto.AlternateText), CInt(id))






    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Session("idprospecto") = Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value
        Session("nombreprospecto") = Me.GridView1.Rows(Me.GridView1.SelectedIndex).Cells(0).Text & " " & Me.GridView1.Rows(Me.GridView1.SelectedIndex).Cells(1).Text & " " & Me.GridView1.Rows(Me.GridView1.SelectedIndex).Cells(2).Text
        Me.PanelComprar.Visible = True
    End Sub
    Protected Sub continuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles continuar.Click
        Me.PanelCarrito.Visible = False
        Me.PanelComprar.Visible = True
    End Sub
    Protected Sub cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelar.Click
        limpiacarrito()
        anadiracarrito("Inscripción", 1, funciones.costopaquete(0), 0, 1)


        Me.PanelCarrito.Visible = False
        'Me.PanelComprar.Visible = True
        'llenagrid()
    End Sub
    Protected Sub confirmar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles confirmar.Click
        Me.PanelCarrito.Visible = False
        Me.PanelDatos.Visible = True
        llenadatos()

    End Sub
    Protected Sub cancelar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelar0.Click
        limpiacarrito()
        Me.PanelDatos.Visible = False
        'Me.PanelComprar.Visible = True
        anadiracarrito("Inscripción", 1, funciones.costopaquete(0), 0, 1)
    End Sub
    Sub llenadatos()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT CONCAT(nombre, ' ', appaterno, ' ', apmaterno) AS nombre, CONCAT(callepaq, ' ', numpaq, ' ', intpaq) AS direccion, colpaq, cppaq, municipiopaq, estadopaq FROM prospectos WHERE id=" & Session("idprospecto").ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        Dim contador As Integer = 0
        Dim idpaquetes As New List(Of Integer)
        Dim costopaquetes As New List(Of Decimal)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then Me.nombre.Text = dtrTeam(0)
            If Not IsDBNull(dtrTeam(1)) Then Me.calle.Text = dtrTeam(1)
            If Not IsDBNull(dtrTeam(2)) Then Me.colonia.Text = dtrTeam(2)
            If Not IsDBNull(dtrTeam(3)) Then Me.cp.Text = dtrTeam(3)
            If Not IsDBNull(dtrTeam(4)) Then Me.municipio.Text = dtrTeam(4)
            If Not IsDBNull(dtrTeam(5)) Then Me.estado.Text = dtrTeam(5)
        End While

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub limpiacarrito()
        Dim permisos As Integer = Session("permisos")
        Dim id As Integer = Session("idasociado")
        Dim nombre As String = Session("nombreasociado")
        Dim arreglo As Integer()
        arreglo = Session("menu")
        Session.Clear()
        Session("menu") = arreglo
        Session("permisos") = permisos
        Session("idasociado") = id
        Session("nombreasociado") = nombre
        llenagrid()
    End Sub
    Protected Sub confirmar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles confirmar0.Click
        Me.PanelDatos.Visible = False
        Me.PanelResumen.Visible = True
        llenaresumen()
    End Sub
    Sub llenaresumen()
        Me.nombreasociado.Text = Me.nombre.Text
        Me.calle0.Text = Me.calle.Text
        Me.colonia0.Text = Me.colonia.Text
        Me.cp0.Text = Me.cp.Text
        Me.municipio0.Text = Me.municipio.Text
        Me.estado0.Text = Me.estado.Text
        Me.entrega0.Text = Me.entrega.Text
        Me.formadepago0.Text = Me.formadepago.SelectedItem.Text
        If Session("numpaq") = 0 Then Exit Sub
        Dim ds As New DataSet
        Dim table As DataTable = ds.Tables.Add("Data")
        table.Columns.Add("cantidad", GetType(Integer))
        table.Columns.Add("paquete", GetType(String))
        table.Columns.Add("precio", GetType(Decimal))
        table.Columns.Add("monto", GetType(Decimal))
        Dim total As Decimal = 0
        Dim iva As Decimal = 0
        For i = 1 To Session("numpaq")
            Dim monto As Decimal = CDec(Session("precio" & i.ToString))
            If Session("idpaquete" & i.ToString) = 0 Then
                iva += Session("cantidad" & i.ToString) * Session("precio" & i.ToString) * System.Configuration.ConfigurationManager.AppSettings("iva")
                monto = monto * (1 - System.Configuration.ConfigurationManager.AppSettings("iva"))
            End If

            table.Rows.Add(New Object() {Session("cantidad" & i.ToString), Session("paquete" & i.ToString).ToString, monto, (Session("cantidad" & i.ToString) * monto)})
            total += (Session("cantidad" & i.ToString) * Session("precio" & i.ToString))

        Next
        If Me.entrega.SelectedItem.Text = "Domicilio" Then
            Dim envio As Decimal = funciones.costoenvio
            Dim enviosiniva As Decimal = envio * (1 - System.Configuration.ConfigurationManager.AppSettings("iva"))
            table.Rows.Add(New Object() {1, "Envío", enviosiniva, enviosiniva})
            iva += envio * System.Configuration.ConfigurationManager.AppSettings("iva")
            total += envio
        End If

        table.Rows.Add(New Object() {Nothing, "Subtotal", Nothing, total - iva})
        table.Rows.Add(New Object() {Nothing, "IVA", Nothing, iva})


        table.Rows.Add(New Object() {Nothing, "Total", Nothing, total})
        Dim viewcompras As DataView = ds.Tables(0).DefaultView
        Me.GridCarritoFinal.DataSource = viewcompras
        Me.GridCarritoFinal.DataBind()
    End Sub

    Protected Sub cancelar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelar1.Click
        limpiacarrito()
        Me.PanelResumen.Visible = False
        anadiracarrito("Inscripción", 1, funciones.costopaquete(0), 0, 1)
        'Me.PanelComprar.Visible = True
        'llenagrid()
    End Sub
    Protected Sub confirmar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles confirmar1.Click
        Me.PanelResumen.Visible = False
        procesacompra()

    End Sub
    Sub procesacompra()
        Dim idpaq As New List(Of Integer)
        Dim cantidades As New List(Of Integer)
        Dim puntos As New List(Of Integer)
        Dim costo As New List(Of Decimal)
        For i = 1 To Session("numpaq")
            idpaq.Add(CInt(Session("idpaquete" & i.ToString)))
            puntos.Add(CInt(funciones.puntospaquete(CInt(Session("idpaquete" & i.ToString)))))
            costo.Add(CInt(Session("precio" & i.ToString)))
            cantidades.Add(CInt(Session("cantidad" & i.ToString)))
        Next
        If Me.formadepago.SelectedItem.Text = "Efectivo" Then
            pasaasociado()
            Dim id As Integer = nuevoasociado()
            eliminaprospecto()
            actualizabono6(id)
            insertacompra(idpaq, puntos, costo, cantidades, 1, 0, id)


            'llenagrid()
            Me.mensajes.Text = "Asociado Registrado con Éxito, su número es el " & nuevoasociado.ToString
            Me.mensajes.Visible = True
        Else
            actualizaprospecto()
            insertacompra(idpaq, puntos, costo, cantidades, 1, 0, Session("idasociado"))
            Me.mensajes.Text = "El Asociado quedará registrado al validar la compra, con el respectivo pago"
            Me.mensajes.Visible = True
            llenagracias()
        End If


    End Sub
    Sub actualizaprospecto()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim lado As String = ""
        If Me.ladopadre.Text = "1" Then lado = "I"
        If Me.ladopadre.Text = "2" Then lado = "D"
        Dim strTeamQuery As String = "UPDATE prospectos SET padre=" & Me.idpadre.Text & ", lado='" & lado & "' WHERE id=" & Session("idprospecto").ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
    Sub eliminaprospecto()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "DELETE FROM prospectos WHERE id=" & Session("idprospecto").ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
    Sub insertacompra(ByVal paquetes As List(Of Integer), ByVal puntos As List(Of Integer), ByVal costo As List(Of Decimal), ByVal cantidades As List(Of Integer), ByVal activadora As Integer, ByVal excedente As Integer, ByVal asociado As Integer, Optional ByVal activacionafuturo As Integer = 0)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim id As Integer = asociado
        Dim puntostotales As Integer = 0
        Dim costototal As Decimal = 0
        For i = 0 To puntos.Count - 1
            'cambio para subir menos puntos

            If paquetes(i) = 2 Then puntos(i) = (puntos(i) - 50)
            If paquetes(i) = 3 Then puntos(i) = (puntos(i) - 100)



            puntostotales += puntos(i) * cantidades(i)
            costototal += costo(i) * cantidades(i)
        Next
        Dim inscripcion As Integer = Session("idprospecto")
        'STATUS PAGO
        Dim statuspago As String = "PENDIENTE"
        If formadepago.SelectedItem.Text = "Efectivo" Then
            statuspago = "PAGADO"
        End If
        'tipopago
        Dim tipopago As String = UCase(Me.formadepago.SelectedItem.Text)

        'status entrega
        Dim statusentrega As String = "PENDIENTE"
        If formadepago.SelectedItem.Text = "Efectivo" And Me.entrega.SelectedItem.Text = "Mostrador" Then
            statusentrega = "ENTREGADO"
        End If
        'tipoentrega
        Dim tipoentrega As String = UCase(Me.entrega.SelectedItem.Text)

        'fechas activación

        Dim inicioactivacion As Date = Today
        Dim finactivacion As Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, inicioactivacion))



        Dim iva As Decimal = calculaiva()
        Dim total As Decimal = calculatotal()
        Dim envio As Decimal = 0
        If Me.entrega.SelectedItem.Text = "Domicilio" Then
            envio = funciones.costoenvio
            envio = envio * (1 - System.Configuration.ConfigurationManager.AppSettings("iva"))
        End If
        sqlConn.Open()
        Dim fechaentrega As String = "0000/00/00"
        If statuspago = "PAGADO" Then
            fechaentrega = Today.Year & "/" & Today.Month & "/" & Today.Day

        End If
        If inicioactivacion > CDate("0001/01/01") Then
            strTeamQuery = "INSERT INTO compras(asociado, puntos,  fechaorden, activacion, excedente, statuspago, statusentrega, inicioactivacion, finactivacion, tipopago, tipoentrega, iva, envio, total, fecha, inscripcion, autor) VALUES(" & id.ToString & ", " & puntostotales.ToString & ", '" & Today.Year & "/" & Today.Month & "/" & Today.Day & "', " & activadora.ToString & ", " & excedente.ToString & ", '" & statuspago & "', '" & statusentrega & "', '" & inicioactivacion.ToString("yyyy/MM/dd") & "', '" & finactivacion.ToString("yyyy/MM/dd") & "', '" & tipopago & "', '" & tipoentrega & "', " & iva.ToString & ", " & envio.ToString & ", " & total.ToString & ", '" & fechaentrega & "', " & inscripcion.ToString & ", 'OV')"
        Else
            strTeamQuery = "INSERT INTO compras(asociado, puntos,  fechaorden, activacion, excedente, statuspago, statusentrega, tipopago, tipoentrega, iva, envio, total, fecha, inscripcion, autor) VALUES(" & id.ToString & ", " & puntostotales.ToString & ", '" & Today.Year & "/" & Today.Month & "/" & Today.Day & "', " & activadora.ToString & ", " & excedente.ToString & ", '" & statuspago & "', '" & statusentrega & "', '" & tipopago & "', '" & tipoentrega & "', " & iva.ToString & ", " & envio.ToString & ", " & total.ToString & ", '" & fechaentrega & "', " & inscripcion.ToString & ", 'OV')"
        End If




        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()

        Dim compra As Integer = recuperacompra(id)
        'agrega referencia a compra
        Dim referencia As Integer = funciones.referencia(compra)

        sqlConn.Open()

        strTeamQuery = "UPDATE compras SET referencia=" & referencia.ToString & " WHERE id=" & compra.ToString



        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
        For i = 0 To puntos.Count - 1
            sqlConn.Open()

            strTeamQuery = "INSERT INTO comprasdetalle (compra, cantidad, paquete, costo, puntos) VALUES(" & compra.ToString & ", " & cantidades(i).ToString & "," & paquetes(i).ToString & ", " & costo(i).ToString & ", " & puntos(i).ToString & ")"



            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            cmdFetchTeam.ExecuteNonQuery()


            sqlConn.Close()
            sqlConn.Dispose()
        Next
        If Me.entrega.SelectedItem.Text = "Domicilio" Then
            sqlConn.Open()

            strTeamQuery = "INSERT INTO comprasdetalle (compra, cantidad, paquete, costo, puntos) VALUES(" & compra.ToString & ", 1, -1, " & envio.ToString & ", 0)"



            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            cmdFetchTeam.ExecuteNonQuery()


            sqlConn.Close()
            sqlConn.Dispose()

        End If
        If statuspago = "PAGADO" Then

            puntostotales = 0
            For i = 0 To puntos.Count - 1
                If paquetes(i) = 3 Then
                    puntostotales += cantidades(i) * puntos(i) - 200 * cantidades(i)
                Else
                    puntostotales += cantidades(i) * puntos(i)
                End If
                'descuenta de bodegas
                Dim bodega As Integer = funciones.buscabodega(Session("idasociado"))
                funciones.descuentadebodegas(bodega, paquetes(i), cantidades(i))
            Next
            funciones.subepuntos(compra, puntostotales, asociado)

        End If

        If formadepago.SelectedItem.Text <> "Efectivo" Then

            Dim correo As New mails
            Dim mail As String = correo.correodeprospecto(Session("idprospecto"))
            'Dim mensaje As String = correo.mensajedecorreocompra
            'Dim a() As String = Split(mensaje, vbCrLf)
            'Dim htmbody As New System.Text.StringBuilder()
            'Dim body As New System.Text.StringBuilder()
            'Dim x As Integer = 0


            'For i = LBound(a) To UBound(a)

            'With body

            'If a(i) = "" Then

            'Else

            'Dim cadena As String = Nothing
            'Dim caux As String = Nothing
            'caux = a(i)

            'For x = 0 To (caux.Length - 1)

            'If caux.Chars(x) = " " Then
            'cadena = cadena & "&nbsp;"
            'Else
            'cadena = cadena & caux.Chars(x)
            'End If
            'Next


            '.Append("<br/><span>" & cadena & "</span>")
            'End If

            'End With
            'Next
            'body.Append("<br/><span style='font-weight:bold;'>" & Session("nombreprospecto") & " tu compra fue exitosa.</span>")
            'body.Append("<br/><span>El número de cuenta para depositar en Banorte es 0870626045</span>")
            'body.Append("<br/><span>El número de cuenta para depositar en ScotiaBank es 03504208749</span>")
            'body.Append("<br/><span style='font-weight:bold;'>El monto total de tu compra es de $" & total.ToString & "</span>")
            'body.Append("<br/><span style='font-weight:bold;'>La referencia es la siguiente " & referencia.ToString & "</span>")
            'body.Append("<br/><span style='font-weight:bold;'>La fecha de la orden es " & Today.ToString & "</span>")
            'body.Append("<br/><span style='font-weight:bold;'>El volumen de la compra es " & puntostotales.ToString & "</span>")

            'With htmbody

            '.Append("<html>")
            '.Append("<head> ")
            '.Append("<meta name='ProgId' content='FrontPage.Editor.Document'>")
            '.Append("<meta http-equiv='Content-Type' content='text/html; charset=windows-1252'>")
            '.Append("<title>Correo Electonico Autogenerado </title>")
            '.Append("</head>")
            '.Append("<body style='font-family:Arial, Helvetica, sans-serif;'>")
            '.Append(body.ToString) ' y aqui va nuestro contenido del mail 


            '.Append("</body>")
            '.Append("</html>")

            'End With

            'If mail <> "" Then
            'correo.enviacorreo(mail, "Orden " & referencia.ToString & " realizada exitosamente", htmbody.ToString)
            'End If

            correo.enviamaildecomprainscripcion(referencia, Session("nombreprospecto"), Session("idasociado"), Session("nombreasociado"), paquetes, cantidades, puntos, costo, mail, Me.entrega.SelectedItem.Text, envio, iva)
        End If


        limpiacarrito()


    End Sub
    Sub llenagracias()
        Me.gracias.Text = Server.HtmlEncode(mensajedegracias).Replace(vbCrLf, "<br />")
        Dim referencia As String = ""
        If Me.formadepago.SelectedItem.Text = "Efectivo" Then
            Me.oc.Text = recuperareferencia(nuevoasociado).ToString
        Else
            Me.oc.Text = recuperareferencia(Session("idasociado")).ToString
        End If

        Me.oc1.Text = Me.oc.Text

        Me.PanelResumen.Visible = True
        Me.cancelar1.Visible = False
        Me.confirmar1.Visible = False
        PanelGracias.Visible = True
        Me.GridView1.Visible = False
    End Sub
    Function recuperareferencia(ByVal asociado As Integer) As Integer
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT MAX(referencia) FROM compras WHERE asociado=" & asociado.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        Dim id As Integer
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then id = dtrTeam(0)


        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Return id
    End Function
    Function mensajedegracias() As String
        Dim mensaje As String = ""
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT mensajecompra FROM configuracion "

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then mensaje = dtrTeam(0)

        End While

        sqlConn.Close()
        sqlConn.Dispose()

        Return mensaje
    End Function
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
        sqlConn.Dispose()
        Return id
    End Function
    Function calculaiva() As Decimal
        Dim iva As Decimal
        For i = 1 To Session("numpaq")
            Dim monto As Decimal = CDec(Session("precio" & i.ToString))
            If Session("idpaquete" & i.ToString) = 0 Then
                'iva += Session("cantidad" & i.ToString) * Session("precio" & i.ToString) * System.Configuration.ConfigurationManager.AppSettings("iva")
                iva += (Session("cantidad" & i.ToString) * Session("precio" & i.ToString)) - (Session("cantidad" & i.ToString) * Session("precio" & i.ToString)) / (1 + System.Configuration.ConfigurationManager.AppSettings("iva"))
            End If


        Next
        If Me.entrega.SelectedItem.Text = "Domicilio" Then
            Dim envio As Decimal = funciones.costoenvio
            Dim enviosiniva As Decimal = envio * (1 - System.Configuration.ConfigurationManager.AppSettings("iva"))
            'iva += envio * System.Configuration.ConfigurationManager.AppSettings("iva")
            iva += envio - (envio / (1 + System.Configuration.ConfigurationManager.AppSettings("iva")))
        End If
        Return iva
    End Function
    Function calculatotal() As Decimal
        Dim total As Decimal
        For i = 1 To Session("numpaq")
            Dim monto As Decimal = CDec(Session("precio" & i.ToString))

            total += Session("cantidad" & i.ToString) * Session("precio" & i.ToString)




        Next
        If Me.entrega.SelectedItem.Text = "Domicilio" Then
            Dim envio As Decimal = funciones.costoenvio

            total += envio

        End If
        Return total
    End Function

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
    Sub pasaasociado()
        Dim password As String = funciones.Crearpassword(8)
        Dim nombre, appaterno, apmaterno, fnac, rfc, curp, compania, telmovil, tellocal, nextel, email, asociadoalias, pais, idioma, callecasa, numcasa, intcasa, colcasa, municipiocasa, estadocasa, callepaq, numpaq, intpaq, colpaq, municipiopaq, estadopaq, tipo, patrocinador, cpcasa, cppaq, CiudadCasa, CiudadPaq, estadocivil As String
        Dim lado As String = "I"
        If Me.ladopadre.Text = "2" Then lado = "D"

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT * FROM prospectos WHERE id=" & Session("idprospecto").ToString

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
            If Not IsDBNull(dtrTeam("CiudadCasa")) Then CiudadCasa = dtrTeam("CiudadCasa").ToString
            If Not IsDBNull(dtrTeam("CiudadPaq")) Then CiudadPaq = dtrTeam("CiudadPaq").ToString
            If Not IsDBNull(dtrTeam("EstadoCivil")) Then estadocivil = dtrTeam("EstadoCivil").ToString
        End While

        sqlConn.Close()
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)

        Dim puntos As Integer = 0
        For i = 1 To Session("numpaq")
            puntos += funciones.puntospaquete(Session("idpaquete" & i.ToString))



        Next
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
            strTeamQuery = "INSERT INTO asociados (`nombre`, `ApPaterno` ,`ApMaterno`, `FNac`, `RFC`, `CURP`, `Compania`, `TelLocal`, `TelMovil`, `Nextel`, `Email`, `Alias`, `Pais`, `Idioma`, `CalleCasa`, `NumCasa`, `IntCasa`, `ColCasa`, `CPCasa`, `MunicipioCasa`, `EstadoCasa`, `CallePaq`, `NumPaq`, `IntPaq`, `ColPaq`, `CPPaq`, `MunicipioPaq`, `EstadoPaq`, `Tipo`, `FInsc`, `Patrocinador`, `Padre`, `Lado`, `Orden`, `Rango`,  `Status`, `PtsMes`, rangopago, historia, recorrido, ladosrecorrido, ladopatrocinador, nivel, bodega, inicioactivacion, finactivacion, password, ciudadcasa, ciudadpaq, EstadoCivil ) VALUES ('" & nombre & "', '" & appaterno & "', '" & apmaterno & "', '" & fnac & "', '" & rfc & "', '" & curp & "', '" & compania & "', '" & tellocal & "', '" & telmovil & "', '" & nextel & "', '" & email & "', '" & asociadoalias & "', '" & pais & "', '" & idioma & "', '" & callecasa & "', '" & numcasa & "', '" & intcasa & "', '" & colcasa & "', '" & cpcasa & "', '" & municipiocasa & "', '" & Trim(estadocasa) & "', '" & callepaq & "', '" & numpaq & "', '" & intpaq & "', '" & colpaq & "', '" & cppaq & "', '" & municipiopaq & "', '" & Trim(estadopaq) & "',  " & tipo.ToString & ", '" & Today.Year.ToString & "/" & Today.Month.ToString & "/" & Today.Day.ToString & "', " & Session("idasociado").ToString & ", " & Me.idpadre.Text & ", '" & UCase(lado) & "', " & orden.ToString & ", 1," & status.ToString & "," & puntos.ToString & ", 1, '" & historia() & "', '" & recorridostr & "', '" & ladosrecorridostr & "', '" & ladopatrocinadorstr & "', 0, " & bodega.ToString & ", '" & Today.Year.ToString & "/" & Today.Month.ToString & "/" & Today.Day.ToString & "', '" & fechafinactivacion.Year.ToString & "/" & fechafinactivacion.Month.ToString & "/" & fechafinactivacion.Day.ToString & "', '" & password & "', '" & CiudadCasa & "', '" & CiudadPaq & "', '" & estadocivil & "')"
        Else
            strTeamQuery = "INSERT INTO asociados (`nombre`, `ApPaterno` ,`ApMaterno`, `FNac`, `RFC`, `CURP`, `Compania`, `TelLocal`, `TelMovil`, `Nextel`, `Email`, `Alias`, `Pais`, `Idioma`, `CalleCasa`, `NumCasa`, `IntCasa`, `ColCasa`, `CPCasa`, `MunicipioCasa`, `EstadoCasa`, `CallePaq`, `NumPaq`, `IntPaq`, `ColPaq`, `CPPaq`, `MunicipioPaq`, `EstadoPaq`, `Tipo`, `FInsc`, `Patrocinador`, `Padre`, `Lado`, `Orden`, `Rango`,  `Status`, `PtsMes`, rangopago, historia, recorrido, ladosrecorrido, ladopatrocinador, nivel, bodega, password, ciudadcasa, ciudadpaq, EstadoCivil ) VALUES ('" & nombre & "', '" & appaterno & "', '" & apmaterno & "', '" & fnac & "', '" & rfc & "', '" & curp & "', '" & compania & "', '" & tellocal & "', '" & telmovil & "', '" & nextel & "', '" & email & "', '" & asociadoalias & "', '" & pais & "', '" & idioma & "', '" & callecasa & "', '" & numcasa & "', '" & intcasa & "', '" & colcasa & "', '" & cpcasa & "', '" & municipiocasa & "', '" & Trim(estadocasa) & "', '" & callepaq & "', '" & numpaq & "', '" & intpaq & "', '" & colpaq & "', '" & cppaq & "', '" & municipiopaq & "', '" & Trim(estadopaq) & "',  " & tipo.ToString & ", '" & Today.Year.ToString & "/" & Today.Month.ToString & "/" & Today.Day.ToString & "', " & Session("idasociado").ToString & ", " & Me.idpadre.Text & ", '" & UCase(lado) & "', " & orden.ToString & ", 1," & status.ToString & "," & puntos.ToString & ", 1, '" & historia() & "', '" & recorridostr & "', '" & ladosrecorridostr & "', '" & ladopatrocinadorstr & "', 0, " & bodega.ToString & ", '" & password & "', '" & CiudadCasa & "', '" & CiudadPaq & "', '" & estadocivil & "')"
        End If


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()




        'crea alias
        strTeamQuery = "SELECT MAX(id) FROM asociados WHERE patrocinador=" & Session("idasociado").ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)

        Dim nuevoid As Integer = 0

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            nuevoid = dtrTeam(0)
        End While
        sqlConn.Close()
        Dim aliasstr As String = UCase(Left(nombre, 3)) & nuevoid.ToString
        sqlConn.Open()

        strTeamQuery = "UPDATE asociados SET alias='" & aliasstr & "' WHERE id=" & nuevoid.ToString



        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()

        'para mail
      

        Dim correo As New mails
        correo.enviamaildebienvenida(nuevoid)


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
        Dim strTeamQuery As String = "SELECT ladosrecorrido FROM asociados WHERE id=" & Me.idpadre.Text

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
        Dim strTeamQuery As String = "SELECT recorrido FROM asociados WHERE id=" & Me.idpadre.Text

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then respuesta = dtrTeam(0).ToString
        End While
        sqlConn.Close()



        respuesta += Me.idpadre.Text & "."

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
End Class
