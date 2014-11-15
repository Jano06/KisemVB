Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class sw_reporte_compras
    Inherits System.Web.UI.Page
    Dim comprastotales, cantidaddecompras As Decimal
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
        objEtiqueta.Text = "Reporte de Compras"
        Dim fecha As Date = Today
        Dim de, a As Date
        Dim banderaa, banderade As Integer
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
        Me.fechade.Text = de.ToString("dd/MM/yyyy")

        Me.fechaa.Text = a.ToString("dd/MM/yyyy")
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            calculatotalcompras()
            llenagrid1()
            Me.GridView1.DataSource = ""
            Me.GridView1.DataBind()

            Me.Panel1.Visible = True
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try

    End Sub
    Sub llenagrid1()
        Try
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = ""
            Dim asociado() As String = Split(Me.TextBox1.Text, " ")
            Dim totalcompras As Decimal = 0
            If Me.totalcompras.Text <> "" Then totalcompras = CDec(Me.totalcompras.Text)
            Dim de, a As Date
            de = CDate(Me.fechade.Text)
            a = CDate(fechaa.Text)
            'strTeamQuery = "SELECT if(ISNULL(paquetes.nombre), 'Paquete 4',paquetes.nombre) AS paquete, Sum(comprasdetalle.cantidad) AS cantidad, Sum(comprasdetalle.costo) AS monto, (Sum(comprasdetalle.cantidad)/1) AS porcentaje, COUNT(comprasdetalle.compra)  "
            'strTeamQuery += "FROM comprasdetalle INNER JOIN compras ON compras.id = comprasdetalle.compra left JOIN paquetes ON paquetes.id = comprasdetalle.paquete "

            strTeamQuery = " SELECT paquetes.nombre AS paquete, Sum( comprasdetalle.cantidad ) AS cantidad, Sum( comprasdetalle.costo ) AS monto, (Sum( comprasdetalle.costo ) /" & totalcompras.ToString & ") AS porcentaje, COUNT( comprasdetalle.compra ) "
            strTeamQuery += "FROM(comprasdetalle) INNER JOIN compras ON compras.id = comprasdetalle.compra INNER JOIN paquetes ON paquetes.id = comprasdetalle.paquete  INNER JOIN asociados ON compras.asociado=asociados.id "


            strTeamQuery += "WHERE compras.statuspago='PAGADO' AND compras.fecha>='" & de.ToString("yyyy/MM/dd") & "' AND compras.fecha<='" & a.ToString("yyyy/MM/dd") & "' "
            If Session("permisos") = 2 Then
                Dim funciones As New funciones
                strTeamQuery += " AND asociados.bodega=" & funciones.buscabodega(Session("idasociado")).ToString
                strTeamQuery += " AND (compras.autor='OV' OR compras.autor='" & Session("idasociado").ToString & "')"
            End If
            If Me.TextBox1.Text <> "" Then

                strTeamQuery += " AND compras.asociado=" & asociado(0) & ""



            End If
            strTeamQuery += " GROUP BY paquete "

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
    Sub calculatotalcompras()
        Try
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = ""
            Dim asociado() As String = Split(Me.TextBox1.Text, " ")
            strTeamQuery = "SELECT SUM( comprasdetalle.cantidad ) , sum( comprasdetalle.costo ) "
            strTeamQuery += "FROM comprasdetalle INNER JOIN compras ON compras.id = comprasdetalle.compra INNER JOIN asociados ON compras.asociado=asociados.id "
            strTeamQuery += "WHERE  compras.statuspago='PAGADO' AND compras.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' AND compras.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
            If Session("permisos") = 2 Then
                Dim funciones As New funciones
                strTeamQuery += " AND asociados.bodega=" & funciones.buscabodega(Session("idasociado")).ToString
                strTeamQuery += " AND (compras.autor='OV' OR compras.autor='" & Session("idasociado").ToString & "')"
            End If

            If Me.TextBox1.Text <> "" Then
                strTeamQuery += " AND asociado=" & asociado(0)
            End If
            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read

                Me.totalcompras.Text = dtrTeam(1).ToString
                If Not IsDBNull(dtrTeam(1)) Then comprastotales = dtrTeam(1)
                Me.numerodecompras.Text = dtrTeam(0).ToString
                If Not IsDBNull(dtrTeam(0)) Then cantidaddecompras = dtrTeam(0)
            End While

            sqlConn.Close()

        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try
    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                ' e.Row.Cells(3).Text = (CDec(e.Row.Cells(2).Text) / CDec(Me.totalcompras.Text)).ToString
        End Select
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        llenagriddetalle()
    End Sub
    Sub llenagriddetalle()
        Dim de, a As Date
        de = CDate(Me.fechade.Text)
        a = CDate(fechaa.Text)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim asociado() As String = Split(Me.TextBox1.Text, " ")
        If Me.TextBox1.Text = "" Then
            'strTeamQuery = "SELECT compras.referencia, asociados.id AS id, CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', apmaterno) AS asociado, compras.total AS monto, compras.fecha AS fecha, compras.fechaorden, compras.puntos, compras.tipopago  " & _
            '                "FROM asociados INNER JOIN compras  ON asociados.ID = compras.asociado " & _
            '                "WHERE compras.statuspago='PAGADO' AND fecha>='" & Me.fechade.Text & "' AND fecha<='" & Me.fechaa.Text & "' "
            strTeamQuery = "SELECT compras.referencia, asociados.id AS id, CONCAT( asociados.nombre, ' ', asociados.appaterno, ' ', apmaterno ) AS asociado, compras.total AS monto, compras.fecha AS fecha, compras.fechaorden, compras.puntos, compras.tipopago, IF( comprasdetalle.paquete =0, comprasdetalle.cantidad * comprasdetalle.costo * 0.84, comprasdetalle.cantidad * comprasdetalle.costo ) AS comisionable, compras.statusentrega, compras.autor "
            strTeamQuery += " FROM(asociados) "
            strTeamQuery += "INNER JOIN compras ON asociados.ID = compras.asociado "
            strTeamQuery += "INNER JOIN comprasdetalle ON compras.id = comprasdetalle.compra "
            strTeamQuery += "WHERE compras.statuspago = 'PAGADO'  AND fecha>='" & de.ToString("yyyy/MM/dd") & "' AND fecha<='" & a.ToString("yyyy/MM/dd") & "' "
        Else

            strTeamQuery = "SELECT compras.referencia, asociados.id AS id, CONCAT( asociados.nombre, ' ', asociados.appaterno, ' ', apmaterno ) AS asociado, compras.total AS monto, compras.fecha AS fecha, compras.fechaorden, compras.puntos, compras.tipopago, IF( comprasdetalle.paquete =0, comprasdetalle.cantidad * comprasdetalle.costo * 0.84, comprasdetalle.cantidad * comprasdetalle.costo ) AS comisionable, compras.statusentrega, compras.autor "
            strTeamQuery += " FROM(asociados) "
            strTeamQuery += "INNER JOIN compras ON asociados.ID = compras.asociado "
            strTeamQuery += "INNER JOIN comprasdetalle ON compras.id = comprasdetalle.compra "
            strTeamQuery += "WHERE compras.statuspago = 'PAGADO'  AND fecha>='" & de.ToString("yyyy/MM/dd") & "' AND fecha<='" & a.ToString("yyyy/MM/dd") & "' AND asociado=" & asociado(0).ToString & " "
        End If
        If Session("permisos") = 2 Then
            Dim funciones As New funciones
            strTeamQuery += " AND asociados.bodega=" & funciones.buscabodega(Session("idasociado")).ToString
            strTeamQuery += " AND (compras.autor='OV' OR compras.autor='" & Session("idasociado").ToString & "')"
        End If
        strTeamQuery += " ORDER BY compras.referencia DESC"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Me.GridView1.DataSource = dtrTeam
        Me.GridView1.DataBind()

        sqlConn.Close()
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If CInt(e.Row.Cells(8).Text) = 210 Then
                e.Row.Cells(3).Text = ""
                e.Row.Cells(6).Text = ""
            End If
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        llenagridcomprasdetalle(CInt(Me.GridView1.Rows(Me.GridView1.SelectedIndex).Cells(0).Text))
    End Sub
    Sub llenagridcomprasdetalle(ByVal compra As Integer)
        If compra = 0 Then Exit Sub
        Dim compratxt As String = Left(compra.ToString, compra.ToString.Length - 1)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
       
        strTeamQuery = "SELECT comprasdetalle.cantidad, comprasdetalle.costo, paquetes.nombre AS paquete " & _
                        "FROM comprasdetalle INNER JOIN paquetes ON comprasdetalle.paquete = paquetes.id " & _
                        "WHERE comprasdetalle.compra=" & compratxt
      
        strTeamQuery += " ORDER BY paquetes.nombre DESC"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Me.GridPaquetes.DataSource = dtrTeam
        Me.GridPaquetes.DataBind()

        sqlConn.Close()
    End Sub

   
    Protected Sub GridView2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.SelectedIndexChanged

    End Sub
End Class
