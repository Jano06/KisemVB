Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Partial Class sw_miscompras
    Inherits System.Web.UI.Page
    Dim comprastotales, cantidaddecompras As Decimal
    Dim funciones As New funciones
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Mis Compras"
            Dim fecha As Date = Today
            llenafechas(Today)
            llenagrid1()
        End If

    End Sub
    Sub llenafechas(ByVal fecha As Date)
        Dim fechade As Date = DateAdd(DateInterval.Day, -(fecha.Day) + 1, fecha)
        Dim fechaa As Date = DateAdd(DateInterval.Day, Date.DaysInMonth(fecha.Year, fecha.Month) - fecha.Day, fecha)
        'Me.fechade.Text = fechade.ToString("yyyy/MM/dd")
        'Me.fechaa.Text = fechaa.ToString("yyyy/MM/dd")
        Me.fechade.Text = fechade.ToString("dd/MM/yyyy")
        Me.fechaa.Text = fechaa.ToString("dd/MM/yyyy")
    End Sub
    Sub llenagrid1()
        Try
            Me.GridView2.Columns(7).Visible = True
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = ""
            Dim asociado As String = Session("idasociado").ToString
            Dim de As Date = CDate(Me.fechade.Text)
            Dim a As Date = CDate(Me.fechaa.Text)

            'strTeamQuery = "SELECT paquetes.nombre AS paquete, Sum(compras.cantidad) AS cantidad, Sum(compras.costo) AS monto, (Sum(compras.cantidad)/" & cantidaddecompras.ToString & ") AS porcentaje, COUNT(compras.fecha), COUNT(compras.asociado)   " & _
            '                "FROM paquetes INNER JOIN compras ON paquetes.id = compras.paquete " & _
            '                "WHERE ( ((compras.fecha)>='" & Me.fechade.Text & "') AND ((compras.fecha)<='" & Me.fechaa.Text & "') AND ((compras.asociado)=" & asociado.ToString & "))" & _
            '                "GROUP BY paquetes.nombre "

            strTeamQuery = "SELECT id, fechaorden, fecha, puntos, total, statuspago, statusentrega, referencia  " & _
                            "FROM compras " & _
                            "WHERE ( ((compras.fechaorden)>='" & de.ToString("yyyy/MM/dd") & "') AND ((compras.fechaorden)<='" & a.ToString("yyyy/MM/dd") & "') AND ((compras.asociado)=" & asociado.ToString & "))" & _
                            "ORDER BY id "


            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            Me.GridView2.DataSource = dtrTeam
            Me.GridView2.DataBind()

            sqlConn.Close()

        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try
    End Sub

    Protected Sub GridView2_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.DataBound
        Me.GridView2.Columns(7).Visible = False
    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                If IsDate(e.Row.Cells(2).Text) Then
                    e.Row.Cells(8).Text = funciones.periodocomisionable(CDate(e.Row.Cells(2).Text))
                End If
                Dim lbkBtn As LinkButton = CType(e.Row.Cells(9).Controls(0), LinkButton)
                lbkBtn.Text = e.Row.Cells(7).Text

                'e.Row.Cells(3).Text = (Val(e.Row.Cells(3).Text) / cantidaddecompras).ToString
        End Select
    End Sub

  

    Protected Sub ImageButton5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles actualizar.Click
        Try
            'calculatotalcompras()
            llenagrid1()
         
            'Me.Panel1.Visible = True
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try
    End Sub

    Protected Sub primera_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles primera.Click
        llenafechas(funciones.primercompra(Session("idasociado")))
        llenagrid1()
    End Sub

    Protected Sub ultima_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ultima.Click
        llenafechas(funciones.ultimacompra(Session("idasociado")))
        llenagrid1()
    End Sub

    Protected Sub anterior_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles anterior.Click
        llenafechas(DateAdd(DateInterval.Day, -30, CDate(Me.fechade.Text)))
        llenagrid1()
    End Sub

    Protected Sub siguiente_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles siguiente.Click
        llenafechas(DateAdd(DateInterval.Day, 30, CDate(Me.fechade.Text)))
        llenagrid1()
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.SelectedIndexChanged
        llenadetalle(Me.GridView2.DataKeys(Me.GridView2.SelectedIndex).Value)
    End Sub
    Sub llenadetalle(ByVal compra As Integer)
        Try
            Me.direccion1.Text = ""
            Me.direccion2.Text = ""
            If Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(5).Text = "PENDIENTE" Then
                Me.status.Text = "PENDIENTE DE PAGO"
            Else
                If Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(6).Text = "PENDIENTE" Then
                    Me.status.Text = "PENDIENTE DE ENTREGA"
                Else
                    Me.status.Text = "ENTREGADO"
                End If
            End If
            Me.orden.Text = Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(0).Text
            Me.periodo.Text = Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(8).Text
            Me.nombreDistribuidor.Text = Session("nombreasociado")
            Me.numDistribuidor.Text = Session("idasociado").ToString

            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = ""
            Dim asociado As String = Session("idasociado").ToString



            strTeamQuery = "SELECT callepaq, numpaq, intpaq, colpaq, municipiopaq, estadopaq, ciudadpaq  " & _
                            "FROM asociados " & _
                            "WHERE id=" & asociado.ToString


            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read
                If Not IsDBNull(dtrTeam(0)) Then Me.direccion1.Text = dtrTeam(0)
                If Not IsDBNull(dtrTeam(1)) Then Me.direccion1.Text += " " & dtrTeam(1)
                If Not IsDBNull(dtrTeam(2)) And dtrTeam(2) <> "" Then Me.direccion1.Text += " - " & dtrTeam(2)
                If Not IsDBNull(dtrTeam(3)) Then Me.direccion2.Text = dtrTeam(3)
                If Not IsDBNull(dtrTeam(4)) And dtrTeam(4) <> "" Then Me.direccion2.Text += ", " & dtrTeam(4)
                If Not IsDBNull(dtrTeam(5)) And dtrTeam(5) <> "" Then Me.direccion2.Text += ", " & UCase(dtrTeam(5))
                If Not IsDBNull(dtrTeam(6)) And dtrTeam(6) <> "" Then Me.direccion2.Text += ", " & UCase(dtrTeam(6))
            End While

            sqlConn.Close()

            'DATOS DE LA COMPRA


            strTeamQuery = "SELECT compras.autor, concat(asociados.nombre, ' ', asociados.appaterno) AS nombre  " & _
                            "FROM compras left join asociados on compras.autor=asociados.id " & _
                            "WHERE compras.id=" & Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(0).Text


            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read
                If Not IsDBNull(dtrTeam(0)) Then
                    If dtrTeam(0) = "" Then Me.operador.Text = ""
                    If dtrTeam(0) = "OV" Then
                        Me.operador.Text = "OFICINA VIRTUAL"
                        Me.origen.Text = "OFICINA VIRTUAL"
                    End If

                    If IsNumeric(dtrTeam(0)) Then
                        Me.operador.Text = dtrTeam(1)
                        Me.origen.Text = "ADMINISTRADOR"
                    End If

                End If

            End While

            sqlConn.Close()

            'Detalle de la compra

          
            strTeamQuery = "SELECT paquetes.codigo, paquetes.nombre, comprasdetalle.cantidad, comprasdetalle.costo, comprasdetalle.puntos " & _
                            "FROM comprasdetalle inner join paquetes on comprasdetalle.paquete=paquetes.id " & _
                            "WHERE comprasdetalle.compra=" & Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(0).Text


            cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)


            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read
                If Not IsDBNull(dtrTeam(0)) Then Me.codigoArticulo.Text = UCase(dtrTeam(0))
                If Not IsDBNull(dtrTeam(1)) Then Me.descripcion.Text = UCase(dtrTeam(1))
                If Not IsDBNull(dtrTeam(2)) Then Me.cantidad.Text = dtrTeam(2)
                If Not IsDBNull(dtrTeam(3)) Then Me.precioUnitario.Text = CDec(dtrTeam(3)).ToString("c")
                If Not IsDBNull(dtrTeam(4)) Then Me.puntaje.Text = dtrTeam(4)
                Me.total.Text = (CDec(Me.cantidad.Text) * CDec(Me.precioUnitario.Text)).ToString("c")

            End While

            sqlConn.Close()

            Me.fechaorden.Text = Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(1).Text
            Me.fechapago.Text = Me.GridView2.Rows(Me.GridView2.SelectedIndex).Cells(2).Text

            Me.PanelDetalle.Visible = True
            Me.PanelResumen.Visible = False
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try

       
    End Sub

   

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Me.GridView2.SelectedIndex = -1
        Me.PanelDetalle.Visible = False
        Me.PanelResumen.Visible = True
    End Sub
End Class
