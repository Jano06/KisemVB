Imports MySql.Data.MySqlClient

Partial Class sw_inventarios
    Inherits System.Web.UI.Page
    Dim funciones As New funciones

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Inventarios"
            funciones.llenacategorias(Me.categorias, True)
            funciones.llenabodegas(Me.bodegas, Session("idasociado"), True)
            llenagrid()
        End If
    End Sub
    Sub llenagridAnterior()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""

        strTeamQuery += "SELECT Sum(inventario.cantidad) AS cantidad, productos.nombre AS producto "
        strTeamQuery += "FROM productos INNER JOIN inventario ON productos.id = inventario.producto "
        strTeamQuery += " WHERE 1"
        If Me.categorias.SelectedIndex > 0 Then
            strTeamQuery += "  And productos.categoria = " & Me.categorias.SelectedItem.Value.ToString
        End If
        If Me.bodegas.SelectedIndex > 0 Then
            strTeamQuery += "  AND bodega = " & Me.bodegas.SelectedItem.Value.ToString
        End If

        strTeamQuery += " GROUP BY productos.nombre;"


        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Me.GridView1.DataSource = dtrTeam
        Me.GridView1.DataBind()

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub llenagrid()
        For i = 0 To Me.GridView1.Columns.Count - 1
            Me.GridView1.Columns(i).Visible = True
        Next
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""

        strTeamQuery += "SELECT Sum(inventario.cantidad) AS cantidad, productos.nombre AS producto, productos.detalles, bodegas.nombre AS bodega "
        strTeamQuery += "FROM productos INNER JOIN inventario ON productos.id = inventario.producto INNER JOIN bodegas ON inventario.bodega=bodegas.id "
        strTeamQuery += " WHERE 1"
        If Me.categorias.SelectedIndex > 0 Then
            strTeamQuery += "  And productos.categoria = " & Me.categorias.SelectedItem.Value.ToString
        End If
        If Me.bodegas.SelectedIndex > 0 Then
            strTeamQuery += "  AND bodega = " & Me.bodegas.SelectedItem.Value.ToString
        End If
        If Me.Producto.Text <> "" Then
            strTeamQuery += "  AND productos.nombre LIKE '%" & Me.Producto.Text & "%' "
        End If
        strTeamQuery += " GROUP BY productos.nombre"
        If Me.Bodega.Checked Then strTeamQuery += ", inventario.bodega;"

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Me.GridView1.DataSource = dtrTeam
        Me.GridView1.DataBind()

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        llenagrid()

    End Sub

    Protected Sub GridView1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DataBound
        If Not Me.Codigo.Checked Then
            Me.GridView1.Columns(0).Visible = False
        Else
            Me.GridView1.Columns(0).Visible = True
        End If

        If Not Me.Descripcion.Checked Then
            Me.GridView1.Columns(1).Visible = False
        Else
            Me.GridView1.Columns(1).Visible = True
        End If

        If Not Me.Bodega.Checked Then
            Me.GridView1.Columns(2).Visible = False
        Else
            Me.GridView1.Columns(2).Visible = True
        End If

        If Not Me.Existencia.Checked Then
            Me.GridView1.Columns(3).Visible = False
        Else
            Me.GridView1.Columns(3).Visible = True
        End If

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Dim reporte As New funciones
         Dim strTeamQuery As String = ""
        strTeamQuery += "SELECT   "
        If Me.Bodega.Checked Then strTeamQuery += " bodegas.nombre AS bodega, "
        If Me.Codigo.Checked Then strTeamQuery += " productos.nombre AS producto, "
        If Me.Descripcion.Checked Then strTeamQuery += " productos.detalles, "
        If Me.Existencia.Checked Then strTeamQuery += " Sum(inventario.cantidad) AS cantidad, "
        strTeamQuery = Left(strTeamQuery, strTeamQuery.Length - 2)
        strTeamQuery += " FROM productos INNER JOIN inventario ON productos.id = inventario.producto INNER JOIN bodegas ON inventario.bodega=bodegas.id "
        strTeamQuery += " WHERE 1"
        If Me.categorias.SelectedIndex > 0 Then
            strTeamQuery += "  And productos.categoria = " & Me.categorias.SelectedItem.Value.ToString
        End If
        If Me.bodegas.SelectedIndex > 0 Then
            strTeamQuery += "  AND bodega = " & Me.bodegas.SelectedItem.Value.ToString
        End If
        If Me.Producto.Text <> "" Then
            strTeamQuery += "  AND productos.nombre LIKE '%" & Me.Producto.Text & "%' "
        End If
        strTeamQuery += " GROUP BY productos.nombre"
        If Me.Bodega.Checked Then strTeamQuery += ", inventario.bodega;"
        reporte.exportaexcel("Inventario", strTeamQuery)
    End Sub
    Dim totalgrid As Decimal
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            totalgrid += CDec(e.Row.Cells(3).Text)
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(3).Text = totalgrid.ToString("c")
            e.Row.Cells(2).Text = "Totales"
            e.Row.CssClass = "subtitulo"
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class
