Imports MySql.Data.MySqlClient
Partial Class sw_inventarios_movimientos
    Inherits System.Web.UI.Page
    Dim funciones As New funciones

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Me.GridView1.DataKeys.Count = 0 Then Me.GridView1.DataKeyNames = New String() {"id", "idprod", "idbodegaprov", "idbodegarecep"}
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Movimiento de Productos"
            funciones.llenabodegas(Me.bodegasprov, Session("idasociado"))
            funciones.llenabodegas(Me.bodegasrecep, Session("idasociado"))
            funciones.llenaproductos(Me.productos)
            llenagrid()
            Me.fecha.Text = Today.ToString("dd/MM/yyyy")
        End If
    End Sub
    Sub llenagrid()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        strTeamQuery += "SELECT bodegas.id AS idbodegaprov, bodegas_1.id AS idbodegarecep, productos.id AS idprod, inventario_movimientos.id, bodegas.nombre AS proveedor, bodegas_1.nombre AS receptor, inventario_movimientos.fecha, productos.nombre AS producto, inventario_movimientos.cantidad "
        strTeamQuery += "FROM bodegas AS bodegas_1 INNER JOIN (bodegas INNER JOIN (productos INNER JOIN inventario_movimientos ON productos.id = inventario_movimientos.producto) ON bodegas.id = inventario_movimientos.de) ON bodegas_1.id = inventario_movimientos.a "
        strTeamQuery += "WHERE bodegas.id=" & Me.bodegasprov.SelectedItem.Value.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Me.GridView1.DataSource = dtrTeam
        Me.GridView1.DataBind()

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub

    Protected Sub bodegasprov_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles bodegasprov.SelectedIndexChanged
        llenagrid()

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow

                Dim ctrlEliminar As LinkButton = CType(e.Row.Cells(5).Controls(0), LinkButton)
                ctrlEliminar.OnClientClick = "return confirm('¿Esta seguro de eliminar este registro?');"



        End Select
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If tienesuficiente(Me.bodegasprov.SelectedItem.Value, Me.productos.SelectedItem.Value, CInt(Me.cantidad.Text)) Then
            If Not yaestaproducto() Then
                agregaproducto()



            End If
            agregamovimiento()
            actualizainventarios()
            llenagrid()
            funciones.limpiacampos(Me)
            Me.mensajes.Text = "Registro insertado con éxito"
        Else
            Me.mensajes.Text = "No hay suficiente producto para hacer el movimiento"
        End If

       


    End Sub
    Sub agregaproducto()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)



        sqlConn.Open()
        strTeamQuery = "INSERT INTO inventario (`bodega`, `producto`, `cantidad`) VALUES (" & Me.bodegasrecep.SelectedItem.Value.ToString & ", " & Me.productos.SelectedItem.Value.ToString & ", 0)"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
    Function yaestaproducto() As Boolean
        Dim respuesta As Boolean = False
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT producto FROM inventario  WHERE bodega=" & Me.bodegasrecep.SelectedItem.Value.ToString & " AND producto=" & Me.productos.SelectedItem.Value.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            respuesta = True
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        Return respuesta
    End Function
    Sub actualizainventarios()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        'proveedor
        sqlConn.Open()
        strTeamQuery = "UPDATE inventario SET cantidad=cantidad-" & Me.cantidad.Text & " WHERE bodega=" & Me.bodegasprov.SelectedItem.Value.ToString & " AND producto=" & Me.productos.SelectedItem.Value.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        'receptor
        sqlConn.Open()
        strTeamQuery = "UPDATE inventario SET cantidad=cantidad+" & Me.cantidad.Text & " WHERE bodega=" & Me.bodegasrecep.SelectedItem.Value.ToString & " AND producto=" & Me.productos.SelectedItem.Value.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
    Function tienesuficiente(ByVal bodega As Integer, ByVal producto As Integer, ByVal cantidad As Integer) As Boolean
        Dim respuesta As Boolean = False
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT cantidad FROM inventario  WHERE bodega=" & bodega.ToString & " AND producto=" & producto.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If cantidad <= dtrTeam(0) Then
                respuesta = True
            End If
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        Return respuesta
    End Function
    Sub agregamovimiento()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)



        sqlConn.Open()
        strTeamQuery = "INSERT INTO inventario_movimientos (`de`, `a`, `producto`, `cantidad`, `fecha`, usuario) VALUES (" & Me.bodegasprov.SelectedItem.Value.ToString & ", " & Me.bodegasrecep.SelectedItem.Value.ToString & ", " & Me.productos.SelectedItem.Value.ToString & ", " & Me.cantidad.Text & ", '" & CDate(Me.fecha.Text).ToString("yyyy/MM/dd") & "', " & Session("idasociado").ToString & " )"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        If tienesuficiente(Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Values(3), Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Values(1), CInt(Me.GridView1.Rows(Me.GridView1.SelectedIndex).Cells(4).Text)) Then
            eliminamovimiento(Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Values(0))
            regresacantidad(Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Values(1), Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Values(2), Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Values(3))

            llenagrid()
            funciones.limpiacampos(Me)
            Me.GridView1.SelectedIndex = -1
            Me.mensajes.Text = "Registro Eliminado con Éxito"
        Else
            Me.mensajes.Text = "La bodega ya no tiene suficiente cantidad para devolverla"
        End If

    End Sub
    Sub regresacantidad(ByVal producto As Integer, ByVal proveedor As Integer, ByVal receptor As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        'proveedor
        sqlConn.Open()
        strTeamQuery = "UPDATE inventario SET cantidad=cantidad+" & Me.GridView1.Rows(Me.GridView1.SelectedIndex).Cells(4).Text & " WHERE bodega=" & proveedor.ToString & " AND producto=" & producto.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
        'receptor
        sqlConn.Open()
        strTeamQuery = "UPDATE inventario SET cantidad=cantidad-" & Me.GridView1.Rows(Me.GridView1.SelectedIndex).Cells(4).Text & " WHERE bodega=" & receptor.ToString & " AND producto=" & producto.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
    Sub eliminamovimiento(ByVal movimiento As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)



        sqlConn.Open()
        strTeamQuery = "DELETE FROM inventario_movimientos WHERE id=" & movimiento.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub

    
End Class
