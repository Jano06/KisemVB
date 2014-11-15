Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class sw_comprasnuevas
    Inherits System.Web.UI.Page
    Dim funciones As New funciones
    Sub insertacompraanterior()
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
        Dim id As Integer = Session("idasociado")
        Dim activacion As Integer = 1
        Dim bandera As Integer = 0
        For cont = 0 To idpaq.Count - 1
            'checa si está activo
            Dim activo As Boolean = checaactivo()
            Dim compraactivadora As Integer = 0
            Dim excedente As Integer = 0
            If activo Then
                If funciones.esmisemana(Session("idasociado")) Then
                    'sí es mi semana

                Else
                    'no es mi semana
                    'actívalo y pon fechas



                End If
            Else
                'ESTÁ INACTIVO
                If funciones.esmisemana(Session("idasociado")) Then
                    'sí es mi semana
                    Dim misiguienteactivacion As Date = funciones.misiguienteactivacion(Session("idasociado"))
                    If Date.Today >= misiguienteactivacion Then
                        activaasociado(puntos(puntos.Count - 1))
                    Else
                        'pregunta qué período activa, el presente o el siguiente

                    End If
                Else
                    'no es mi semana
                    'actívalo y pon fechas
                    activaasociado(puntos(puntos.Count - 1))
                    compraactivadora = 1


                End If
            End If



            sqlConn.Open()

            strTeamQuery = "INSERT INTO compras(asociado, paquete, cantidad, puntos, costo, fecha, activacion, excedente, autor) VALUES(" & id.ToString & ", " & idpaq(cont).ToString & ", " & cantidades(cont).ToString & ", " & puntos(cont).ToString & ", " & costo(cont).ToString & ", '" & Today.Year & "/" & Today.Month & "/" & Today.Day & "', " & compraactivadora.ToString & ", " & excedente.ToString & ", 'OV')"



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
            If puntos(cont) > 0 Then
                If compraactivadora = 0 Then
                    If idpaq(cont) = 3 Then subepuntos(compra, puntos(cont) - 200, id)
                Else
                    subepuntos(compra, puntos(cont), id)
                End If

            End If
            Dim bodega As Integer = funciones.buscabodega(1)
            funciones.descuentadebodegas(bodega, idpaq(cont), cantidades(cont))
        Next

    End Sub
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
            ' iva += envio * System.Configuration.ConfigurationManager.AppSettings("iva")
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
    Sub extiendeactivaciondeasociado(ByVal puntos As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim finactivacion As Date = funciones.misiguienteactivacion(Session("idasociado"))
        finactivacion = DateAdd(DateInterval.Day, -1, finactivacion)
        finactivacion = DateAdd(DateInterval.Month, 1, finactivacion)
        sqlConn.Open()
        strTeamQuery = "UPDATE asociados SET  inicioactivacion='" & Date.Today.Year.ToString & "/" & Date.Today.Month.ToString & "/" & Date.Today.Day.ToString & "', finactivacion='" & finactivacion.Year.ToString & "/" & finactivacion.Month.ToString & "/" & finactivacion.Day.ToString & "', PtsMes=" & puntos.ToString & " WHERE id=" & Session("idasociado").ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub activaasociado(ByVal puntos As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim finactivacion As Date = funciones.misiguienteactivacion(Session("idasociado"))
        finactivacion = DateAdd(DateInterval.Day, -1, finactivacion)
        sqlConn.Open()
        strTeamQuery = "UPDATE asociados SET status=1, inicioactivacion='" & Date.Today.Year.ToString & "/" & Date.Today.Month.ToString & "/" & Date.Today.Day.ToString & "', finactivacion='" & finactivacion.Year.ToString & "/" & finactivacion.Month.ToString & "/" & finactivacion.Day.ToString & "', PtsMes=" & puntos.ToString & " WHERE id=" & Session("idasociado").ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Function checaactivo() As Boolean
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT status FROM asociados WHERE id=" & Session("idasociado").ToString
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
        If id = 0 Then
            Return False
        Else
            Return True
        End If
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
        sqlConn.Dispose()
        If padre > 10 Then 'para evitar padres que no están dados de alta
            sqlConn.Open()
            strTeamQuery = "INSERT INTO puntosasociados(asociado, compra, lado, puntos, status, porpagar) VALUES(" & padre.ToString & ", " & compra.ToString & ", '" & lado & "', " & puntos.ToString & ", 0, " & puntos.ToString & ")"

            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            cmdFetchTeam.ExecuteNonQuery()


            sqlConn.Close()
            sqlConn.Dispose()
        End If


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
        sqlConn.Dispose()
        If abuelo > 0 Then subepuntos(compra, puntos, padre)
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Compras"
            'llenagridpaquetes()
            funciones.llenaformadepago(Me.formadepago, Session("permisos"))

        End If
    End Sub
    Sub llenagracias()
        Me.gracias.Text = Server.HtmlEncode(mensajedegracias).Replace(vbCrLf, "<br />")
        Me.oc.Text = recuperareferencia(Session("idasociado")).ToString
        Me.oc1.Text = Me.oc.Text

        Me.PanelResumen.Visible = True
        Me.cancelar1.Visible = False
        Me.confirmar1.Visible = False
    End Sub
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
    Sub llenatablapaquetes()
        'toma info de paquetes
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, costo, nombre FROM paquetes WHERE id>=1 and id<4"
       
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

        Dim id As String = sender.id.ToString



        Dim contenedor As ContentPlaceHolder = Me.Master.FindControl("ContentPlaceHolder1")
        Dim cantidad As TextBox = CType(contenedor.FindControl("cantidad" & id), TextBox)
        Dim foto As Image = CType(contenedor.FindControl("foto" & id), Image)
        Dim nombre As Label = CType(contenedor.FindControl("nombre" & id), Label)

        anadiracarrito(nombre.Text, CInt(cantidad.Text), CDec(foto.AlternateText), CInt(id))




       

    End Sub
    Sub anadiracarrito(ByVal paquete As String, ByVal cantidad As Integer, ByVal precio As Decimal, ByVal idpaquete As Integer)
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
        mostrarcarrito()

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
    Sub insertacompra(ByVal paquetes As List(Of Integer), ByVal puntos As List(Of Integer), ByVal costo As List(Of Decimal), ByVal cantidades As List(Of Integer), ByVal activadora As Integer, ByVal excedente As Integer, Optional ByVal activacionafuturo As Integer = 0)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim id As Integer = Session("idasociado")
        Dim puntostotales As Integer = 0
        Dim costototal As Decimal = 0
        For i = 0 To puntos.Count - 1
            puntostotales += puntos(i) * cantidades(i)
            costototal += costo(i) * cantidades(i)
        Next

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

        Dim inicioactivacion As Date
        Dim finactivacion As Date

        If activadora = 1 And activacionafuturo = 0 Then
            Dim fechatemp As Date = funciones.misiguienteactivacion(Session("idasociado"))
            finactivacion = DateAdd(DateInterval.Day, -1, fechatemp)
           
            inicioactivacion = Today
        End If
        If activacionafuturo > 0 Then
            Dim misiguienteactivacion As Date = funciones.misiguienteactivacion(Session("idasociado"))
            finactivacion = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, misiguienteactivacion))
            inicioactivacion = misiguienteactivacion
        End If
        Dim iva As Decimal = calculaiva()
        Dim total As Decimal = calculatotal()
        Dim envio As Decimal = 0
        If Me.entrega.SelectedItem.Text = "Domicilio" Then
            envio = funciones.costoenvio
            envio = envio * (1 - System.Configuration.ConfigurationManager.AppSettings("iva"))
        End If
        sqlConn.Open()
        Dim fechaentrega As String = ""
        If statuspago = "PAGADO" Then
            fechaentrega = Today.Year & "/" & Today.Month & "/" & Today.Day
            If inicioactivacion > CDate("0001/01/01") Then
                strTeamQuery = "INSERT INTO compras(asociado, puntos,  fechaorden, activacion, excedente, statuspago, statusentrega, inicioactivacion, finactivacion, tipopago, tipoentrega, iva, envio, total, fecha, autor) VALUES(" & id.ToString & ", " & puntostotales.ToString & ", '" & Today.Year & "/" & Today.Month & "/" & Today.Day & "', " & activadora.ToString & ", " & excedente.ToString & ", '" & statuspago & "', '" & statusentrega & "', '" & inicioactivacion.ToString("yyyy/MM/dd") & "', '" & finactivacion.ToString("yyyy/MM/dd") & "', '" & tipopago & "', '" & tipoentrega & "', " & iva.ToString & ", " & envio.ToString & ", " & total.ToString & ", '" & fechaentrega & "', 'OV')"
            Else
                strTeamQuery = "INSERT INTO compras(asociado, puntos,  fechaorden, activacion, excedente, statuspago, statusentrega, tipopago, tipoentrega, iva, envio, total, fecha, autor) VALUES(" & id.ToString & ", " & puntostotales.ToString & ", '" & Today.Year & "/" & Today.Month & "/" & Today.Day & "', " & activadora.ToString & ", " & excedente.ToString & ", '" & statuspago & "', '" & statusentrega & "', '" & tipopago & "', '" & tipoentrega & "', " & iva.ToString & ", " & envio.ToString & ", " & total.ToString & ", '" & fechaentrega & "', 'OV')"
            End If
        Else
            If inicioactivacion > CDate("0001/01/01") Then
                strTeamQuery = "INSERT INTO compras(asociado, puntos,  fechaorden, activacion, excedente, statuspago, statusentrega, inicioactivacion, finactivacion, tipopago, tipoentrega, iva, envio, total, autor) VALUES(" & id.ToString & ", " & puntostotales.ToString & ", '" & Today.Year & "/" & Today.Month & "/" & Today.Day & "', " & activadora.ToString & ", " & excedente.ToString & ", '" & statuspago & "', '" & statusentrega & "', '" & inicioactivacion.ToString("yyyy/MM/dd") & "', '" & finactivacion.ToString("yyyy/MM/dd") & "', '" & tipopago & "', '" & tipoentrega & "', " & iva.ToString & ", " & envio.ToString & ", " & total.ToString & ", 'OV')"
            Else
                strTeamQuery = "INSERT INTO compras(asociado, puntos,  fechaorden, activacion, excedente, statuspago, statusentrega, tipopago, tipoentrega, iva, envio, total, autor) VALUES(" & id.ToString & ", " & puntostotales.ToString & ", '" & Today.Year & "/" & Today.Month & "/" & Today.Day & "', " & activadora.ToString & ", " & excedente.ToString & ", '" & statuspago & "', '" & statusentrega & "', '" & tipopago & "', '" & tipoentrega & "', " & iva.ToString & ", " & envio.ToString & ", " & total.ToString & ", 'OV')"
            End If
        End If





        'cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        'cmdFetchTeam.ExecuteNonQuery()


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
                If paquetes(i) = 3 And excedente = 1 Then
                    puntostotales += cantidades(i) * puntos(i) - 200 * cantidades(i)
                Else
                    puntostotales += cantidades(i) * puntos(i)
                End If
                'descuenta de bodegas
                Dim bodega As Integer = funciones.buscabodega(Session("idasociado"))
                funciones.descuentadebodegas(bodega, paquetes(i), cantidades(i))
            Next
            funciones.subepuntos(compra, puntostotales, Session("idasociado"))

        End If



        Dim correo As New mails
        correo.enviamaildecompra(compra, paquetes, cantidades, puntos, costo, Me.entrega.SelectedItem.Text, envio, iva, referencia)
        'Dim mail As String = correo.micorreo(Session("idasociado"))
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
        'body.Append("<br/><span style='font-weight:bold;'>" & Session("idasociado") & " " & Session("nombreasociado") & " tu compra fue exitosa.</span>")
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

        'If Mail <> "" Then
        'correo.enviacorreo(Mail, "Orden " & referencia.ToString & " realizada exitosamente", htmbody.ToString)
        'End If





        Me.importe.Text = calculatotal.ToString("$0.00")
        limpiacarrito()


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


        Dim activo As Boolean = checaactivo()
        Dim compraactivadora As Integer = 0
        Dim excedente As Integer = 0
        If activo Then
            If funciones.esmisemana(Session("idasociado")) Then
                'sí es mi semana 
                'checa si es antes de fecha de activación
                ' si sí, pregunta si excedente o activación de la siguiente semana
                ' si no, simplemente excedente
                Dim misiguienteactivacion As Date = funciones.misiguienteactivacion(Session("idasociado"))
                Dim mifindeactivacion As Date = funciones.mifindeactivacion(Session("idasociado"))
                'If Date.Today >= misiguienteactivacion Or misiguienteactivacion < mifindeactivacion Then
                If Date.Today < DateAdd(DateInterval.Day, -7, mifindeactivacion) Then

                    insertacompra(idpaq, puntos, costo, cantidades, 0, 1)
                    llenagracias()
                    Me.PanelGracias.Visible = True

                Else
                    'pregunta qué período activa, el presente o el siguiente
                    Me.PanelPregunta2.Visible = True


                End If

            Else
                'no es mi semana
                'excedente

                insertacompra(idpaq, puntos, costo, cantidades, 0, 1)
                llenagracias()
                Me.PanelGracias.Visible = True
            End If
        Else
            'ESTÁ INACTIVO
            If funciones.esmisemana(Session("idasociado")) Then
                'sí es mi semana
                Dim misiguienteactivacion As Date = funciones.misiguienteactivacion(Session("idasociado"))

                'If Date.Today >= misiguienteactivacion Then
                If Not funciones.activacionenloquerestadelasemana(Session("idasociado")) Then
                    If Me.formadepago.SelectedItem.Text = "Efectivo" Then activaasociado(puntos(puntos.Count - 1))
                    compraactivadora = 1
                    insertacompra(idpaq, puntos, costo, cantidades, compraactivadora, 0)
                    llenagracias()
                    Me.PanelGracias.Visible = True
                Else
                    'pregunta qué período activa, el presente o el siguiente
                    Me.PanelPregunta1.Visible = True
                    Me.siguiente.Text = "Del " & misiguienteactivacion.ToString("yyyy/MM/dd") & " al " & DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, misiguienteactivacion)).ToString("yyyy/MM/dd")
                    Me.actual.Text = "Del " & DateAdd(DateInterval.Month, -1, misiguienteactivacion).ToString("yyyy/MM/dd") & " al " & DateAdd(DateInterval.Day, -1, misiguienteactivacion).ToString("yyyy/MM/dd")

                End If
            Else
                'no es mi semana
                'actívalo y pon fechas
                Dim misiguienteactivacion As Date = funciones.misiguienteactivacion(Session("idasociado"))
                Me.siguiente.Text = "Del " & misiguienteactivacion.ToString("yyyy/MM/dd") & " al " & DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, misiguienteactivacion)).ToString("yyyy/MM/dd")
                Me.actual.Text = "Del " & DateAdd(DateInterval.Month, -1, misiguienteactivacion).ToString("yyyy/MM/dd") & " al " & DateAdd(DateInterval.Day, -1, misiguienteactivacion).ToString("yyyy/MM/dd")

                If Me.formadepago.SelectedItem.Text = "Efectivo" Then activaasociado(puntos(puntos.Count - 1))
                compraactivadora = 1
                insertacompra(idpaq, puntos, costo, cantidades, compraactivadora, 0)
                llenagracias()
                Me.PanelGracias.Visible = True

            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        llenatablapaquetes()
    End Sub

    
    Protected Sub continuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles continuar.Click
        Me.PanelCarrito.Visible = False
        Me.PanelComprar.Visible = True
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
    End Sub
    Protected Sub cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelar.Click
        limpiacarrito()
        Me.PanelCarrito.Visible = False
        Me.PanelComprar.Visible = True
    End Sub

    Protected Sub confirmar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles confirmar.Click
        Me.PanelCarrito.Visible = False
        Me.PanelDatos.Visible = True
        llenadatos()

    End Sub
    Sub llenadatos()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT CONCAT(nombre, ' ', appaterno, ' ', apmaterno) AS nombre, CONCAT(callepaq, ' ', numpaq, ' ', intpaq) AS direccion, colpaq, cppaq, municipiopaq, estadopaq FROM asociados WHERE id=" & Session("idasociado").ToString

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

    Protected Sub cancelar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelar0.Click
        limpiacarrito()
        Me.PanelDatos.Visible = False
        Me.PanelComprar.Visible = True
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

    Protected Sub cancelar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelar1.Click, cancelar2.Click, cancelar3.Click
        limpiacarrito()
        Me.PanelResumen.Visible = False
        Me.PanelPregunta1.Visible = False
        Me.PanelPregunta2.Visible = False
        Me.PanelComprar.Visible = True

    End Sub

    Protected Sub confirmar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles confirmar1.Click
        Me.PanelResumen.Visible = False
        procesacompra()

    End Sub

    Protected Sub confirmar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles confirmar2.Click
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
        If Me.actual.Checked Then
            If Me.formadepago.SelectedItem.Text = "Efectivo" Then activaasociado(puntos(puntos.Count - 1))

            insertacompra(idpaq, puntos, costo, cantidades, 1, 0)
        Else
            Dim fechas() As String = Split(Me.siguiente.Text, " ")
            If Me.formadepago.SelectedItem.Text = "Efectivo" Then activaasociadoafuturo(puntos(puntos.Count - 1), CDate(fechas(1)), CDate(fechas(3)))

            insertacompra(idpaq, puntos, costo, cantidades, 1, 0, 1)
        End If
        Me.PanelPregunta1.Visible = False
        llenagracias()
        Me.PanelGracias.Visible = True
    End Sub
    Sub activaasociadoafuturo(ByVal puntos As Integer, ByVal inicio As Date, ByVal fin As Date)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()
        strTeamQuery = "UPDATE asociados SET status=0, inicioactivacion='" & inicio.Year.ToString & "/" & inicio.Month.ToString & "/" & inicio.Day.ToString & "', finactivacion='" & fin.Year.ToString & "/" & fin.Month.ToString & "/" & fin.Day.ToString & "', PtsMes=" & puntos.ToString & " WHERE id=" & Session("idasociado").ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        sqlConn.Dispose()
    End Sub

   
    Protected Sub confirmar3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles confirmar3.Click
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
        If Me.activacionsiguienteperiodo.Checked Then
            If Me.formadepago.SelectedItem.Text = "Efectivo" Then extiendeactivaciondeasociado(puntos(puntos.Count - 1))

            insertacompra(idpaq, puntos, costo, cantidades, 1, 0, 1)
        Else
          
            insertacompra(idpaq, puntos, costo, cantidades, 0, 1)
        End If
        Me.PanelPregunta2.Visible = False
        llenagracias()
        Me.PanelGracias.Visible = True
    End Sub
End Class
