Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Partial Class sw_mispuntos
    Inherits System.Web.UI.Page
    Dim pag As Integer
    Dim funciones As New funciones
    Dim inicio, final As Date
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            pag = CInt(Request.QueryString("page"))
            If pag = 0 Then
                Me.PagAnterior.Enabled = False
            End If
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Mis Pagos"

            llenaresumen()
        End If

    End Sub
    Sub llenaresumen()
        If pag = 0 Then
            Me.ImageButton1.Enabled = False
            Me.PagAnterior.Enabled = False
        Else
            Me.ImageButton1.Enabled = True
            Me.PagAnterior.Enabled = True
        End If
        Dim inicio As Integer = pag * 10
        Dim fin As Integer = 10
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        'Dim strTeamQuery As String = "SELECT id AS corte, IF(DATE_FORMAT(inicio, '%M')=DATE_FORMAT(final, '%M'), CONCAT( DATE_FORMAT(inicio, '%d') , ' al ', DATE_FORMAT(final, '%d/%M/%Y')  ),CONCAT( DATE_FORMAT(inicio, '%d/%M/%Y') , ' al ', DATE_FORMAT(final, '%d/%M/%Y')  )) AS periodo  FROM periodos WHERE  id>=196 ORDER BY id DESC LIMIT " & inicio.ToString & ", " & fin.ToString
        Dim strTeamQuery As String = "SELECT id AS corte, IF(DATE_FORMAT(inicio, '%M')=DATE_FORMAT(final, '%M'), CONCAT( DATE_FORMAT(inicio, '%d') , ' al ', DATE_FORMAT(final, '%d/%M/%Y')  ),CONCAT( DATE_FORMAT(inicio, '%d/%M/%Y') , ' al ', DATE_FORMAT(final, '%d/%M/%Y')  )) AS periodo FROM periodos  inner join pagos on periodos.id=pagos.corte WHERE  id>=196  and pagos.asociado=" & Session("idasociado").ToString & " ORDER BY id DESC LIMIT " & inicio.ToString & ", " & fin.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        If Not dtrTeam.HasRows Then
            Me.mensajes.Text = "Usted aún no tiene ganancias en nuestra empresa"
            Me.PagAnterior.Visible = False
            Me.PagSiguiente.Visible = False
        End If
        Me.GridViewresumen.DataSource = dtrTeam
        Me.GridViewresumen.DataBind()

        sqlConn.Close()
    End Sub
    Protected Sub PagAnterior_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles PagAnterior.Click
        pag = CInt(Request.QueryString("page"))
        pag = pag - 1
        Response.Redirect("mispuntos.aspx?page=" & pag.ToString)
    End Sub

    Protected Sub PagSiguiente_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles PagSiguiente.Click
        pag = CInt(Request.QueryString("page"))
        pag = pag + 1
        Response.Redirect("mispuntos.aspx?page=" & pag.ToString)
    End Sub
    Protected Sub GridViewresumen_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewresumen.DataBound
        Me.GridViewresumen.Columns(1).Visible = False
        If Me.GridViewresumen.Rows.Count < 10 Then
            Me.PagSiguiente.Enabled = False
            Me.ImageButton2.Enabled = False
        Else
            Me.PagSiguiente.Enabled = True
            Me.ImageButton2.Enabled = True
        End If
    End Sub

    Protected Sub GridViewresumen_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewresumen.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbkBtn As LinkButton = CType(e.Row.Cells(2).Controls(0), LinkButton)
            lbkBtn.Text = e.Row.Cells(1).Text
           
        End If
    End Sub

    Protected Sub GridViewresumen_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewresumen.SelectedIndexChanged
        llenadetalle(Me.GridViewresumen.Rows(Me.GridViewresumen.SelectedIndex).Cells(0).Text)
    End Sub
    Sub llenadetalle(ByVal periodo As String)

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim bandera As Boolean = False
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        Dim ds As New DataSet
        Dim table As DataTable = ds.Tables.Add("Data")
        table.Columns.Add("descripcion", GetType(String))
        table.Columns.Add("izquierdos", GetType(Integer))
        table.Columns.Add("derechos", GetType(Integer))
        Dim inicialesi, inicialesd, nuevosi, nuevosd, pagados As Integer
        Dim porcentaje As Decimal
        strTeamQuery = "SELECT inicialesi, inicialesd, nuevosi, nuevosd, pagados, porcentaje " & _
                "FROM puntosdetalle   " & _
                "WHERE corte=" & periodo & " AND asociado=" & Session("idasociado").ToString

        sqlConn.Open()
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            inicialesi = dtrTeam(0)
            inicialesd = dtrTeam(1)
            nuevosi = dtrTeam(2)
            nuevosd = dtrTeam(3)
            pagados = dtrTeam(4)
            porcentaje = dtrTeam(5)
            bandera = True
        End While

        sqlConn.Close()
        If bandera Then
            table.Rows.Add(New Object() {"Puntos Iniciales", inicialesi, inicialesd})
            table.Rows.Add(New Object() {"Puntos Nuevos", nuevosi, nuevosd})
            table.Rows.Add(New Object() {"Subtotal en Puntos", inicialesi + nuevosi, inicialesd + nuevosd})


            table.Rows.Add(New Object() {"Puntos Pagados", pagados, pagados})
            table.Rows.Add(New Object() {"Puntos Finales", inicialesi + nuevosi - pagados, inicialesd + nuevosd - pagados})
        Else
            Exit Sub
            ' no hay pago en ese periodo
            pagados = 0
            strTeamQuery = "SELECT inicio, final " & _
                "FROM periodos " & _
                "WHERE id=" & periodo
            Dim inicio, final As Date

            sqlConn.Open()
            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            dtrTeam = cmdFetchTeam.ExecuteReader()
            While dtrTeam.Read
                inicio = dtrTeam(0)
                final = dtrTeam(1)
            End While

            sqlConn.Close()
            'busca ahora los puntos de esa semana

            strTeamQuery = "SELECT  sum( if( `Lado` = 'D', `PorPagar` , 0 ) ) AS D, sum( if( `Lado` = 'I', `PorPagar` , 0 ) ) AS I "
            strTeamQuery += "FROM `puntosasociados`  INNER JOIN compras ON puntosasociados.compra=compras.id "
            strTeamQuery += "WHERE  ((compras.fecha<='" & final.ToString("yyyy/MM/dd") & "') OR(compras.fecha='" & DateAdd(DateInterval.Day, 3, final).ToString("yyyy/MM/dd") & "' AND compras.fechaorden='" & final.ToString("yyyy/MM/dd") & "')) AND compras.statuspago='PAGADO' AND puntosasociados.asociado=" & Session("idasociado").ToString
            strTeamQuery += " GROUP BY puntosasociados.Asociado  "



            sqlConn.Open()
            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            dtrTeam = cmdFetchTeam.ExecuteReader()
            While dtrTeam.Read
                nuevosd = dtrTeam(0)
                nuevosi = dtrTeam(1)
            End While

            sqlConn.Close()

            'busca iniciales
            strTeamQuery = "SELECT  sum( if( `Lado` = 'D', puntosasociados.Puntos , 0 ) ) AS D, sum( if( `Lado` = 'I', puntosasociados.Puntos , 0 ) ) AS I "
            strTeamQuery += "FROM `puntosasociados`  INNER JOIN compras ON puntosasociados.compra=compras.id "
            strTeamQuery += "WHERE  ((compras.fecha<'" & inicio.ToString("yyyy/MM/dd") & "') OR(compras.fecha='" & DateAdd(DateInterval.Day, 2, inicio).ToString("yyyy/MM/dd") & "' AND compras.fechaorden='" & DateAdd(DateInterval.Day, -1, inicio).ToString("yyyy/MM/dd") & "')) AND compras.statuspago='PAGADO' AND puntosasociados.asociado=" & Session("idasociado").ToString
            strTeamQuery += " GROUP BY `Asociado`  "



            sqlConn.Open()
            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
            dtrTeam = cmdFetchTeam.ExecuteReader()
            While dtrTeam.Read
                inicialesd = dtrTeam(0)
                inicialesi = dtrTeam(1)
            End While

            sqlConn.Close()

            table.Rows.Add(New Object() {"Puntos Iniciales", inicialesi, inicialesd})
            table.Rows.Add(New Object() {"Puntos Nuevos", nuevosi, nuevosd})
            table.Rows.Add(New Object() {"Subtotal en Puntos", inicialesi + nuevosi, inicialesd + nuevosd})


            table.Rows.Add(New Object() {"Puntos Pagados", pagados, pagados})
            table.Rows.Add(New Object() {"Puntos Finales", inicialesi + nuevosi - pagados, inicialesd + nuevosd - pagados})
        End If




        Dim view As DataView = ds.Tables(0).DefaultView
        Me.GridBono4.Columns(1).Visible = True
        Me.GridBono4.Columns(2).Visible = True
        Me.GridBono4.DataSource = view
        Me.GridBono4.DataBind()

    End Sub
  

    Protected Sub GridBono4_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridBono4.RowCommand


        'llenacomprasvolumen(e.CommandName)


    End Sub
    Sub llenacomprasvolumen(ByVal lado As String)
        If lado = "Izq" Then
            Me.labelladodetalle.Text = "Compras del lado Izquierdo"
        Else
            Me.labelladodetalle.Text = "Compras del lado Derecho"
        End If
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT inicio, final FROM periodos WHERE status=1 AND id=" '& Me.periodo.Text
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read

            inicio = dtrTeam(0)
            final = dtrTeam(1)

        End While

        sqlConn.Close()
        strTeamQuery = "SELECT compras.asociado, CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) AS nombre, compras.referencia AS compra, compras.fecha AS fecha, puntosasociados.puntos AS volumen " & _
 "FROM compras INNER JOIN asociados ON compras.asociado=asociados.id INNER JOIN puntosasociados ON compras.id=puntosasociados.compra " & _
         "WHERE(compras.puntos > 240) " & _
 "AND compras.statuspago='PAGADO'  " & _
 "AND (compras.fecha>='" & inicio.ToString("yyyy/M/dd") & "'  " & _
 "AND  compras.fecha<='" & final.ToString("yyyy/M/dd") & "'  " & _
 "AND NOT (compras.fecha='" & DateAdd(DateInterval.Day, 2, inicio).ToString("yyyy/M/dd") & "' AND compras.fechaorden='" & DateAdd(DateInterval.Day, -1, inicio).ToString("yyyy/M/dd") & "' )" & _
 "OR (compras.fecha='" & DateAdd(DateInterval.Day, 3, final).ToString("yyyy/M/dd") & "' AND compras.fechaorden='" & final.ToString("yyyy/M/dd") & "')) " & _
 "AND puntosasociados.asociado= " & Session("idasociado").ToString & " " & _
 "AND puntosasociados.lado='" & Left(lado, 1) & "' "
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Me.GridDetalleBono4.DataSource = dtrTeam

        Me.GridDetalleBono4.DataBind()


        sqlConn.Close()
    End Sub
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Response.Redirect("mispuntos.aspx")
    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Dim paginafinal As Integer = 0
        Dim numpagos As Integer = funciones.numpagos(Session("idasociado"))
        paginafinal = numpagos / 10
        If numpagos Mod 10 > 0 Then paginafinal += 1
        paginafinal -= 1
        Response.Redirect("mispuntos.aspx?page=" & paginafinal.ToString)
    End Sub
End Class
