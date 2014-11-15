Imports MySql.Data.MySqlClient

Partial Class sw_inventarios_detalle
    Inherits System.Web.UI.Page
    Dim funciones As New funciones
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Agregar Productos"
            'funciones.llenabodegas(Me.bodegas, Session("idasociado"), True)
            funciones.llenatodoslosadministradores(Me.Operador)
            llenagrid()
        End If
    End Sub
    Sub llenagrid()
     
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""

        strTeamQuery += "SELECT Sum(inventario.cantidad) AS cantidad, productos.nombre AS producto, productos.detalles, bodegas.nombre AS bodega, bodegas.id AS id "
        strTeamQuery += "FROM productos INNER JOIN inventario ON productos.id = inventario.producto INNER JOIN bodegas ON inventario.bodega=bodegas.id "
        strTeamQuery += " WHERE 1"
        If Session("permisos") = 2 Then
            Dim funciones As New funciones
            Dim bodega = funciones.buscabodega(Session("idasociado"))
            strTeamQuery += " AND bodegas.id=" & bodega.ToString

        End If
        strTeamQuery += " GROUP BY bodegas.nombre, productos.nombre"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Me.GridView1.DataSource = dtrTeam
        Me.GridView1.DataBind()

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub llenagriddetalleant()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Select Case Me.movimiento.SelectedValue
            Case 0
                strTeamQuery = "SELECT inventario_inicio.fecha, productos.nombre AS producto, productos.detalles AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, inventario_inicio.id AS referencia, 1 AS tipo, bodegas.nombre AS bodega, inventario_inicio.cantidad AS cantidad FROM inventario_inicio INNER JOIN productos ON productos.id=inventario_inicio.producto INNER JOIN asociados ON inventario_inicio.usuario=asociados.id INNER JOIN bodegas ON inventario_inicio.bodega=bodegas.id "
                strTeamQuery += " WHERE 1 "
                'If Me.bodegas.SelectedIndex > 0 Then
                'strTeamQuery += " AND inventario_inicio.bodega=" & Me.bodegas.SelectedValue.ToString
                'End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND inventario_inicio.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND inventario_inicio.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND inventario_inicio.usuario=" & Me.Operador.SelectedValue.ToString
                End If
                strTeamQuery += " UNION "
                strTeamQuery += "SELECT inventario_entradas.fecha, productos.nombre AS producto, productos.detalles AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, inventario_entradas.id AS referencia, 2 AS tipo, bodegas.nombre AS bodega, inventario_entradas.cantidad AS cantidad FROM inventario_entradas INNER JOIN productos ON productos.id=inventario_entradas.producto INNER JOIN asociados ON inventario_entradas.usuario=asociados.id INNER JOIN bodegas ON inventario_entradas.bodega=bodegas.id"
                strTeamQuery += " WHERE 1 "
                'If Me.bodegas.SelectedIndex > 0 Then
                ' strTeamQuery += " AND inventario_entradas.bodega=" & Me.bodegas.SelectedValue.ToString
                ' End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND inventario_entradas.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND inventario_entradas.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND inventario_entradas.usuario=" & Me.Operador.SelectedValue.ToString
                End If
                strTeamQuery += " UNION "
                strTeamQuery += "SELECT inventario_movimientos.fecha, productos.nombre AS producto, productos.detalles AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, inventario_movimientos.id AS referencia, 4 AS tipo, bodegas.nombre AS bodega, inventario_movimientos.cantidad AS cantidad FROM inventario_movimientos INNER JOIN productos ON productos.id=inventario_movimientos.producto INNER JOIN asociados ON inventario_movimientos.usuario=asociados.id INNER JOIN bodegas ON inventario_movimientos.a=bodegas.id"
                strTeamQuery += " WHERE 1 "
                'If Me.bodegas.SelectedIndex > 0 Then
                ' strTeamQuery += " AND inventario_movimientos.a=" & Me.bodegas.SelectedValue.ToString
                ' End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND inventario_movimientos.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND inventario_movimientos.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND inventario_movimientos.usuario=" & Me.Operador.SelectedValue.ToString
                End If
                strTeamQuery += " UNION "
                strTeamQuery += "SELECT inventario_salidas.fecha, productos.nombre AS producto, productos.detalles AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, inventario_salidas.id AS referencia, 3 AS tipo, bodegas.nombre AS bodega, inventario_salidas.cantidad AS cantidad FROM inventario_salidas INNER JOIN productos ON productos.id=inventario_salidas.producto INNER JOIN asociados ON inventario_salidas.usuario=asociados.id INNER JOIN bodegas ON inventario_salidas.bodega=bodegas.id"
                strTeamQuery += " WHERE 1 "
                'If Me.bodegas.SelectedIndex > 0 Then
                ' strTeamQuery += " AND inventario_salidas.bodega=" & Me.bodegas.SelectedValue.ToString
                ' End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND inventario_salidas.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND inventario_salidas.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND inventario_salidas.usuario=" & Me.Operador.SelectedValue.ToString
                End If

                'compras
                strTeamQuery += " UNION "
                strTeamQuery += "SELECT compras.fecha, paquetes.nombre AS producto, '' AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, compras.referencia AS referencia, 3 AS tipo, bodegas.nombre AS bodega, paquetesdetalle.cantidad AS cantidad FROM compras INNER JOIN comprasdetalle ON compras.id=comprasdetalle.compra INNER JOIN paquetes ON comprasdetalle.paquete=paquetes.id INNER JOIN paquetesdetalle ON paquetes.id=paquetesdetalle.paquete INNER JOIN asociados ON compras.autor=asociados.id INNER JOIN asociados AS asociados1 ON compras.asociado=asociados1.id INNER JOIN bodegas ON asociados1.bodega=bodegas.id"
                strTeamQuery += " WHERE compras.statuspago='PAGADO' "
                'If Me.bodegas.SelectedIndex > 0 Then
                ' strTeamQuery += " AND asociados1.bodega=" & Me.bodegas.SelectedValue.ToString
                ' End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND compras.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND compras.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND compras.autor=" & Me.Operador.SelectedValue.ToString
                End If



            Case 1
                strTeamQuery = "SELECT inventario_inicio.fecha, productos.nombre AS producto, productos.detalles AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, inventario_inicio.id AS referencia, 1 AS tipo, bodegas.nombre AS bodega, inventario_inicio.cantidad AS cantidad FROM inventario_inicio INNER JOIN productos ON productos.id=inventario_inicio.producto INNER JOIN asociados ON inventario_inicio.usuario=asociados.id INNER JOIN bodegas ON inventario_inicio.bodega=bodegas.id "
                strTeamQuery += " WHERE 1 "
                'If Me.bodegas.SelectedIndex > 0 Then
                ' strTeamQuery += " AND inventario_inicio.bodega=" & Me.bodegas.SelectedValue.ToString
                ' End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND inventario_inicio.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND inventario_inicio.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND inventario_inicio.usuario=" & Me.Operador.SelectedValue.ToString
                End If

            Case 2
                strTeamQuery += "SELECT inventario_entradas.fecha, productos.nombre AS producto, productos.detalles AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, inventario_entradas.id AS referencia, 2 AS tipo, bodegas.nombre AS bodega, inventario_entradas.cantidad AS cantidad FROM inventario_entradas INNER JOIN productos ON productos.id=inventario_entradas.producto INNER JOIN asociados ON inventario_entradas.usuario=asociados.id INNER JOIN bodegas ON inventario_entradas.bodega=bodegas.id"
                strTeamQuery += " WHERE 1 "
                'If Me.bodegas.SelectedIndex > 0 Then
                ' strTeamQuery += " AND inventario_entradas.bodega=" & Me.bodegas.SelectedValue.ToString
                ' End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND inventario_entradas.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND inventario_entradas.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND inventario_entradas.usuario=" & Me.Operador.SelectedValue.ToString
                End If

            Case 3
                strTeamQuery += "SELECT inventario_movimientos.fecha, productos.nombre AS producto, productos.detalles AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, inventario_movimientos.id AS referencia, 4 AS tipo, bodegas.nombre AS bodega, inventario_movimientos.cantidad AS cantidad FROM inventario_movimientos INNER JOIN productos ON productos.id=inventario_movimientos.producto INNER JOIN asociados ON inventario_movimientos.usuario=asociados.id INNER JOIN bodegas ON inventario_movimientos.a=bodegas.id"
                strTeamQuery += " WHERE 1 "
                'If Me.bodegas.SelectedIndex > 0 Then
                ' strTeamQuery += " AND inventario_movimientos.a=" & Me.bodegas.SelectedValue.ToString
                ' End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND inventario_movimientos.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND inventario_movimientos.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND inventario_movimientos.usuario=" & Me.Operador.SelectedValue.ToString
                End If

            Case 4
                strTeamQuery += "SELECT inventario_salidas.fecha, productos.nombre AS producto, productos.detalles AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, inventario_salidas.id AS referencia, 3 AS tipo, bodegas.nombre AS bodega, inventario_salidas.cantidad AS cantidad FROM inventario_salidas INNER JOIN productos ON productos.id=inventario_salidas.producto INNER JOIN asociados ON inventario_salidas.usuario=asociados.id INNER JOIN bodegas ON inventario_salidas.bodega=bodegas.id"
                strTeamQuery += " WHERE 1 "
                'If Me.bodegas.SelectedIndex > 0 Then
                ' strTeamQuery += " AND inventario_salidas.bodega=" & Me.bodegas.SelectedValue.ToString
                ' End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND inventario_salidas.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND inventario_salidas.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND inventario_salidas.usuario=" & Me.Operador.SelectedValue.ToString
                End If

                strTeamQuery += " UNION "
                strTeamQuery += "SELECT compras.fecha, paquetes.nombre AS producto, '' AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, compras.referencia AS referencia, 3 AS tipo, bodegas.nombre AS bodega, paquetesdetalle.cantidad AS cantidad FROM compras INNER JOIN comprasdetalle ON compras.id=comprasdetalle.compra INNER JOIN paquetes ON comprasdetalle.paquete=paquetes.id INNER JOIN paquetesdetalle ON paquetes.id=paquetesdetalle.paquete INNER JOIN asociados ON compras.autor=asociados.id INNER JOIN asociados AS asociados1 ON compras.asociado=asociados1.id INNER JOIN bodegas ON asociados1.bodega=bodegas.id"
                strTeamQuery += " WHERE compras.statuspago='PAGADO' "
                'If Me.bodegas.SelectedIndex > 0 Then
                ' strTeamQuery += " AND asociados1.bodega=" & Me.bodegas.SelectedValue.ToString
                ' End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND compras.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND compras.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND compras.autor=" & Me.Operador.SelectedValue.ToString
                End If


        End Select

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Me.GridViewDetalle.DataSource = dtrTeam
        Me.GridViewDetalle.DataBind()

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
    Sub llenagriddetalle()
        Me.PanelFiltros.Visible = True
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Select Case Me.movimiento.SelectedValue
            Case 0
                strTeamQuery = "SELECT inventario_inicio.fecha, productos.nombre AS producto, productos.detalles AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, inventario_inicio.id AS referencia, 1 AS tipo, bodegas.nombre AS bodega, inventario_inicio.cantidad AS cantidad FROM inventario_inicio INNER JOIN productos ON productos.id=inventario_inicio.producto INNER JOIN asociados ON inventario_inicio.usuario=asociados.id INNER JOIN bodegas ON inventario_inicio.bodega=bodegas.id "
                strTeamQuery += " WHERE 1 "
                'If Me.bodegas.SelectedIndex > 0 Then
                strTeamQuery += " AND inventario_inicio.bodega=" & Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value.ToString
                'End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND inventario_inicio.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND inventario_inicio.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND inventario_inicio.usuario=" & Me.Operador.SelectedValue.ToString
                End If
                strTeamQuery += " UNION "
                strTeamQuery += "SELECT inventario_entradas.fecha, productos.nombre AS producto, productos.detalles AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, inventario_entradas.id AS referencia, 2 AS tipo, bodegas.nombre AS bodega, inventario_entradas.cantidad AS cantidad FROM inventario_entradas INNER JOIN productos ON productos.id=inventario_entradas.producto INNER JOIN asociados ON inventario_entradas.usuario=asociados.id INNER JOIN bodegas ON inventario_entradas.bodega=bodegas.id"
                strTeamQuery += " WHERE 1 "
                'If Me.bodegas.SelectedIndex > 0 Then
                strTeamQuery += " AND inventario_entradas.bodega=" & Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value.ToString
                'End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND inventario_entradas.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND inventario_entradas.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND inventario_entradas.usuario=" & Me.Operador.SelectedValue.ToString
                End If
                'traspasos A
                strTeamQuery += " UNION "
                strTeamQuery += "SELECT inventario_movimientos.fecha, productos.nombre AS producto, productos.detalles AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, inventario_movimientos.id AS referencia, 4 AS tipo, bodegas.nombre AS bodega, inventario_movimientos.cantidad AS cantidad FROM inventario_movimientos INNER JOIN productos ON productos.id=inventario_movimientos.producto INNER JOIN asociados ON inventario_movimientos.usuario=asociados.id INNER JOIN bodegas ON inventario_movimientos.a=bodegas.id"
                strTeamQuery += " WHERE 1 "
                'If Me.bodegas.SelectedIndex > 0 Then
                strTeamQuery += " AND inventario_movimientos.a=" & Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value.ToString
                'End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND inventario_movimientos.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND inventario_movimientos.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND inventario_movimientos.usuario=" & Me.Operador.SelectedValue.ToString
                End If
                'traspasos DE
                strTeamQuery += " UNION "
                strTeamQuery += "SELECT inventario_movimientos.fecha, productos.nombre AS producto, productos.detalles AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, inventario_movimientos.id AS referencia, 4 AS tipo, bodegas.nombre AS bodega, -inventario_movimientos.cantidad AS cantidad FROM inventario_movimientos INNER JOIN productos ON productos.id=inventario_movimientos.producto INNER JOIN asociados ON inventario_movimientos.usuario=asociados.id INNER JOIN bodegas ON inventario_movimientos.a=bodegas.id"
                strTeamQuery += " WHERE 1 "
                'If Me.bodegas.SelectedIndex > 0 Then
                strTeamQuery += " AND inventario_movimientos.de=" & Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value.ToString
                'End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND inventario_movimientos.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND inventario_movimientos.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND inventario_movimientos.usuario=" & Me.Operador.SelectedValue.ToString
                End If

                strTeamQuery += " UNION "
                strTeamQuery += "SELECT inventario_salidas.fecha, productos.nombre AS producto, productos.detalles AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, inventario_salidas.id AS referencia, 3 AS tipo, bodegas.nombre AS bodega, inventario_salidas.cantidad AS cantidad FROM inventario_salidas INNER JOIN productos ON productos.id=inventario_salidas.producto INNER JOIN asociados ON inventario_salidas.usuario=asociados.id INNER JOIN bodegas ON inventario_salidas.bodega=bodegas.id"
                strTeamQuery += " WHERE 1 "
                'If Me.bodegas.SelectedIndex > 0 Then
                strTeamQuery += " AND inventario_salidas.bodega=" & Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value.ToString
                'End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND inventario_salidas.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND inventario_salidas.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND inventario_salidas.usuario=" & Me.Operador.SelectedValue.ToString
                End If

                'compras
                strTeamQuery += " UNION "
                strTeamQuery += "SELECT compras.fecha, paquetes.nombre AS producto, '' AS detalles,  IF(ISNULL(CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno)), 'OV', CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno)) AS operador, compras.referencia AS referencia, 3 AS tipo, bodegas.nombre AS bodega, -paquetesdetalle.cantidad AS cantidad FROM compras INNER JOIN comprasdetalle ON compras.id=comprasdetalle.compra INNER JOIN paquetes ON comprasdetalle.paquete=paquetes.id INNER JOIN paquetesdetalle ON paquetes.id=paquetesdetalle.paquete LEFT JOIN asociados ON compras.autor=asociados.id INNER JOIN asociados AS asociados1 ON compras.asociado=asociados1.id INNER JOIN bodegas ON asociados1.bodega=bodegas.id"
                strTeamQuery += " WHERE compras.statuspago='PAGADO' "
                'If Me.bodegas.SelectedIndex > 0 Then
                strTeamQuery += " AND asociados1.bodega=" & Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value.ToString
                'End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND compras.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND compras.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND compras.autor=" & Me.Operador.SelectedValue.ToString
                End If



            Case 1
                strTeamQuery = "SELECT inventario_inicio.fecha, productos.nombre AS producto, productos.detalles AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, inventario_inicio.id AS referencia, 1 AS tipo, bodegas.nombre AS bodega, inventario_inicio.cantidad AS cantidad FROM inventario_inicio INNER JOIN productos ON productos.id=inventario_inicio.producto INNER JOIN asociados ON inventario_inicio.usuario=asociados.id INNER JOIN bodegas ON inventario_inicio.bodega=bodegas.id "
                strTeamQuery += " WHERE 1 "
                'If Me.bodegas.SelectedIndex > 0 Then
                strTeamQuery += " AND inventario_inicio.bodega=" & Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value.ToString
                'End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND inventario_inicio.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND inventario_inicio.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND inventario_inicio.usuario=" & Me.Operador.SelectedValue.ToString
                End If

            Case 2
                strTeamQuery += "SELECT inventario_entradas.fecha, productos.nombre AS producto, productos.detalles AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, inventario_entradas.id AS referencia, 2 AS tipo, bodegas.nombre AS bodega, inventario_entradas.cantidad AS cantidad FROM inventario_entradas INNER JOIN productos ON productos.id=inventario_entradas.producto INNER JOIN asociados ON inventario_entradas.usuario=asociados.id INNER JOIN bodegas ON inventario_entradas.bodega=bodegas.id"
                strTeamQuery += " WHERE 1 "
                'If Me.bodegas.SelectedIndex > 0 Then
                strTeamQuery += " AND inventario_entradas.bodega=" & Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value.ToString
                'End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND inventario_entradas.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND inventario_entradas.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND inventario_entradas.usuario=" & Me.Operador.SelectedValue.ToString
                End If

            Case 4
                strTeamQuery += "SELECT inventario_movimientos.fecha, productos.nombre AS producto, productos.detalles AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, inventario_movimientos.id AS referencia, 4 AS tipo, bodegas.nombre AS bodega, inventario_movimientos.cantidad AS cantidad FROM inventario_movimientos INNER JOIN productos ON productos.id=inventario_movimientos.producto INNER JOIN asociados ON inventario_movimientos.usuario=asociados.id INNER JOIN bodegas ON inventario_movimientos.a=bodegas.id"
                strTeamQuery += " WHERE 1 "
                'If Me.bodegas.SelectedIndex > 0 Then
                strTeamQuery += " AND inventario_movimientos.a=" & Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value.ToString
                'End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND inventario_movimientos.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND inventario_movimientos.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND inventario_movimientos.usuario=" & Me.Operador.SelectedValue.ToString
                End If
                'traspasos DE
                strTeamQuery += " UNION "
                strTeamQuery += "SELECT inventario_movimientos.fecha, productos.nombre AS producto, productos.detalles AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, inventario_movimientos.id AS referencia, 4 AS tipo, bodegas.nombre AS bodega, -inventario_movimientos.cantidad AS cantidad FROM inventario_movimientos INNER JOIN productos ON productos.id=inventario_movimientos.producto INNER JOIN asociados ON inventario_movimientos.usuario=asociados.id INNER JOIN bodegas ON inventario_movimientos.a=bodegas.id"
                strTeamQuery += " WHERE 1 "
                'If Me.bodegas.SelectedIndex > 0 Then
                strTeamQuery += " AND inventario_movimientos.de=" & Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value.ToString
                'End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND inventario_movimientos.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND inventario_movimientos.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND inventario_movimientos.usuario=" & Me.Operador.SelectedValue.ToString
                End If
            Case 3
                strTeamQuery += "SELECT inventario_salidas.fecha, productos.nombre AS producto, productos.detalles AS detalles, CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno) AS operador, inventario_salidas.id AS referencia, 3 AS tipo, bodegas.nombre AS bodega, -inventario_salidas.cantidad AS cantidad FROM inventario_salidas INNER JOIN productos ON productos.id=inventario_salidas.producto INNER JOIN asociados ON inventario_salidas.usuario=asociados.id INNER JOIN bodegas ON inventario_salidas.bodega=bodegas.id"
                strTeamQuery += " WHERE 1 "
                'If Me.bodegas.SelectedIndex > 0 Then
                strTeamQuery += " AND inventario_salidas.bodega=" & Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value.ToString
                'End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND inventario_salidas.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND inventario_salidas.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND inventario_salidas.usuario=" & Me.Operador.SelectedValue.ToString
                End If

                strTeamQuery += " UNION "
                strTeamQuery += "SELECT compras.fecha, paquetes.nombre AS producto, '' AS detalles,  IF(ISNULL(CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno)), 'OV', CONCAT(asociados.nombre, ' ', asociados.apPaterno, ' ', asociados.apMaterno)) AS operador, compras.referencia AS referencia, 3 AS tipo, bodegas.nombre AS bodega, -paquetesdetalle.cantidad AS cantidad FROM compras INNER JOIN comprasdetalle ON compras.id=comprasdetalle.compra INNER JOIN paquetes ON comprasdetalle.paquete=paquetes.id INNER JOIN paquetesdetalle ON paquetes.id=paquetesdetalle.paquete LEFT JOIN asociados ON compras.autor=asociados.id INNER JOIN asociados AS asociados1 ON compras.asociado=asociados1.id INNER JOIN bodegas ON asociados1.bodega=bodegas.id"
                strTeamQuery += " WHERE compras.statuspago='PAGADO' "
                'If Me.bodegas.SelectedIndex > 0 Then
                strTeamQuery += " AND asociados1.bodega=" & Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value.ToString
                'End If
                If Me.fechade.Text <> "" Then
                    strTeamQuery += " AND compras.fecha>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.fechaa.Text <> "" Then
                    strTeamQuery += " AND compras.fecha<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
                End If
                If Me.Operador.SelectedIndex > 0 Then
                    strTeamQuery += " AND compras.autor=" & Me.Operador.SelectedValue.ToString
                End If


        End Select

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Me.GridViewDetalle.DataSource = dtrTeam
        Me.GridViewDetalle.DataBind()

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewDetalle.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.Cells(5).Text = "1" Then e.Row.Cells(5).Text = "Inicial"
            If e.Row.Cells(5).Text = "2" Then e.Row.Cells(5).Text = "Entrada"
            If e.Row.Cells(5).Text = "3" Then e.Row.Cells(5).Text = "Salida"
            If e.Row.Cells(5).Text = "4" Then e.Row.Cells(5).Text = "Traspaso"
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        llenagriddetalle()

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        llenagriddetalle()

    End Sub
End Class
