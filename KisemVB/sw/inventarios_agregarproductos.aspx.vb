Imports MySql.Data.MySqlClient

Partial Class sw_inventarios_agregarproductos
    Inherits System.Web.UI.Page
    Dim funciones As New funciones

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Me.GridView1.DataKeys.Count = 0 Then Me.GridView1.DataKeyNames = New String() {"id"}
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Agregar Productos"
            funciones.llenabodegas(Me.bodegas, Session("idasociado"))
            funciones.llenaproductos(Me.productos)
            llenagrid()
        End If
    End Sub
    Sub llenagrid()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT productos.id, productos.nombre AS producto, inventario.cantidad FROM inventario INNER JOIN productos ON productos.id=inventario.producto WHERE inventario.bodega=" & Me.bodegas.SelectedItem.Value.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Me.GridView1.DataSource = dtrTeam
        Me.GridView1.DataBind()

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub

    Protected Sub bodegas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles bodegas.SelectedIndexChanged
        llenagrid()

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow

                Dim ctrlEliminar As LinkButton = CType(e.Row.Cells(2).Controls(0), LinkButton)
                ctrlEliminar.OnClientClick = "return confirm('¿Esta seguro de eliminar este registro?');"



        End Select
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Not yaestaproducto Then
            agregaproducto()
            llenagrid()
            funciones.limpiacampos(Me)
            Me.mensajes.Text = "Registro insertado con éxito"
        Else
            Me.mensajes.Text = "El producto seleccionado ya se encuentra dado de alta en la bodega seleccionada"
        End If


    End Sub
    Function yaestaproducto() As Boolean
        Dim respuesta As Boolean = False
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT producto FROM inventario  WHERE bodega=" & Me.bodegas.SelectedItem.Value.ToString & " AND producto=" & Me.productos.SelectedItem.Value.ToString

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
    Sub agregaproducto()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)



        sqlConn.Open()
        strTeamQuery = "INSERT INTO inventario (`bodega`, `producto`, `cantidad`) VALUES (" & Me.bodegas.SelectedItem.Value.ToString & ", " & Me.productos.SelectedItem.Value.ToString & ", " & Me.cantidad.Text & ")"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()

        sqlConn.Open()
        strTeamQuery = "INSERT INTO inventario_inicio  (`bodega`, `producto`, `cantidad`, `fecha`, `usuario`) VALUES (" & Me.bodegas.SelectedItem.Value.ToString & ", " & Me.productos.SelectedItem.Value.ToString & ", " & Me.cantidad.Text & ", '" & Today.ToString("yyyy/MM/dd") & "', " & Session("idasociado").ToString & " )"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()


    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        eliminaproducto(Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value)
        llenagrid()
        funciones.limpiacampos(Me)
        Me.GridView1.SelectedIndex = -1
        Me.mensajes.Text = "Registro Eliminado con Éxito"
    End Sub
    Sub eliminaproducto(ByVal producto As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)



        sqlConn.Open()
        strTeamQuery = "DELETE FROM inventario WHERE bodega=" & Me.bodegas.SelectedItem.Value.ToString & " AND producto=" & producto.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
End Class
