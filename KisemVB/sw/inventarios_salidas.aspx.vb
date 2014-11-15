Imports MySql.Data.MySqlClient

Partial Class sw_inventarios_salidas
    Inherits System.Web.UI.Page
    Dim funciones As New funciones

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Me.GridView1.DataKeys.Count = 0 Then Me.GridView1.DataKeyNames = New String() {"id", "idprod", "idbodegaprov"}
        If Not IsPostBack Then
            Me.fecha.Text = Today.ToString("dd/MM/yyyy")
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Salidas de Producto"
            funciones.llenabodegas(Me.bodegasprov, Session("idasociado"))

            funciones.llenaproductos(Me.productos)
            llenagrid()
        End If
    End Sub
    Sub llenagrid()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        strTeamQuery += "SELECT productos.id AS idprod, bodegas.id AS idbodegaprov, inventario_salidas.id, bodegas.nombre AS bodega, CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', apmaterno) AS usuario, inventario_salidas.fecha, productos.nombre AS producto, inventario_salidas.cantidad "
        strTeamQuery += "FROM asociados INNER JOIN (bodegas INNER JOIN (productos INNER JOIN inventario_salidas ON productos.id = inventario_salidas.producto) ON bodegas.id = inventario_salidas.bodega) ON asociados.id = inventario_salidas.usuario "
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

            agregasalida()
            actualizainventarios()

            llenagrid()
            funciones.limpiacampos(Me)
            Me.mensajes.Text = "Registro insertado con éxito"
        Else
            Me.mensajes.Text = "No hay suficiente producto para hacer el movimiento"
        End If




    End Sub
    Sub regresacantidad(ByVal producto As Integer, ByVal proveedor As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)


        'proveedor
        sqlConn.Open()
        strTeamQuery = "UPDATE inventario SET cantidad=cantidad+" & Me.GridView1.Rows(Me.GridView1.SelectedIndex).Cells(3).Text & " WHERE bodega=" & proveedor.ToString & " AND producto=" & producto.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
       
    End Sub
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
    Sub agregasalida()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)



        sqlConn.Open()
        strTeamQuery = "INSERT INTO inventario_salidas (`bodega`, `producto`, `cantidad`, `fecha`, `usuario`) VALUES (" & Me.bodegasprov.SelectedItem.Value.ToString & ", " & Me.productos.SelectedItem.Value.ToString & ", " & Me.cantidad.Text & ", '" & CDate(Me.fecha.Text).ToString("yyyy/MM/dd") & "', " & Session("idasociado").ToString & " )"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        eliminasalida(Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Values(0))
        regresacantidad(Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Values(1), Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Values(2))

        llenagrid()
        funciones.limpiacampos(Me)
        Me.GridView1.SelectedIndex = -1
        Me.mensajes.Text = "Registro Eliminado con Éxito"
     

    End Sub
  
    Sub eliminasalida(ByVal salida As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)



        sqlConn.Open()
        strTeamQuery = "DELETE FROM inventario_salidas WHERE id=" & salida.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub

End Class
