Imports MySql.Data.MySqlClient
Partial Class sw_bodegas
    Inherits System.Web.UI.Page
    Dim funciones As New funciones
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If funciones.existeasociado(CInt(Me.administrador.Text)) Then
            insertabodega()
            actualizabodegaadministrador()
            llenagrid()
            funciones.limpiacampos(Me)
            Me.mensajes.Text = "Registro Insertado con éxito"
        Else
            Me.mensajes.Text = "Administrador inexistente"
        End If

    End Sub
    Sub llenagrid()
        Me.GridView1.Columns(0).Visible = True
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, `nombre`, `calle` ,`numero`, `interior`, `colonia`, `cp`, `municipio`, `estado`, administrador FROM bodegas ORDER BY nombre"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Me.GridView1.DataSource = dtrTeam
        Me.GridView1.DataBind()

        sqlConn.Close()
        Me.GridView1.Columns(0).Visible = False
    End Sub
    Sub actualizabodegaadministrador()
        Dim bodega As Integer = recuperabodega()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)



        sqlConn.Open()
        strTeamQuery = "UPDATE asociados SET bodega=" & bodega & " WHERE id=" & Me.administrador.Text

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
    Function recuperabodega() As Integer
        Dim respuesta As Integer = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT MAX(id) FROM bodegas"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        Dim padre As Integer = 0
        Dim lado As String = ""
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            respuesta = dtrTeam(0)
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Return respuesta
    End Function
    Sub insertabodega()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)



        sqlConn.Open()
        strTeamQuery = "INSERT INTO bodegas (`nombre`, `calle` ,`numero`, `interior`, `colonia`, `cp`, `municipio`, `estado`, administrador) VALUES ('" & Me.nombre.Text & "', '" & Me.callecasa.Text & "', '" & Me.numcasa.Text & "', '" & Me.interiorcasa.Text & "', '" & Me.coloniacasa.Text & "', '" & Me.cpcasa.Text & "', '" & Me.municipiocasa.Text & "', '" & Me.estadocasa.SelectedItem.Text & "', " & Me.administrador.Text & ")"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Bodegas"
            llenagrid()
        End If
        If Me.GridView1.DataKeys.Count = 0 Then Me.GridView1.DataKeyNames = New String() {"id"}
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow

                Dim ctrlEliminar As LinkButton = CType(e.Row.Cells(11).Controls(0), LinkButton)
                ctrlEliminar.OnClientClick = "return confirm('¿Esta seguro de eliminar este registro?');"



        End Select
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        eliminabodega(e.RowIndex)
        llenagrid()
        Me.Button2.Visible = False
        Me.Button3.Visible = False
        Me.Button1.Visible = True
        Me.GridView1.SelectedIndex = -1
        funciones.limpiacampos(Me)
        Me.mensajes.Text = "Registro eliminado con éxito"
    End Sub
    Sub eliminabodega(ByVal indice As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Me.GridView1.Columns(0).Visible = True
        Dim id As String = Me.GridView1.Rows(indice).Cells(0).Text
        Me.GridView1.Columns(0).Visible = False
        Dim strTeamQuery As String = "DELETE FROM bodegas WHERE id=" & id

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        llenabodega(Me.GridView1.SelectedIndex)
    End Sub
    Sub llenabodega(ByVal renglon As Integer)
        Me.Button2.Visible = True
        Me.Button3.Visible = True
        Me.Button1.Visible = False
        Me.nombre.Text = Server.HtmlDecode(Me.GridView1.Rows(renglon).Cells(1).Text)
        Me.callecasa.Text = Server.HtmlDecode(Me.GridView1.Rows(renglon).Cells(2).Text)
        Me.numcasa.Text = Server.HtmlDecode(Me.GridView1.Rows(renglon).Cells(3).Text)
        Me.interiorcasa.Text = Server.HtmlDecode(Me.GridView1.Rows(renglon).Cells(4).Text)
        Me.coloniacasa.Text = Server.HtmlDecode(Me.GridView1.Rows(renglon).Cells(5).Text)
        Me.cpcasa.Text = Server.HtmlDecode(Me.GridView1.Rows(renglon).Cells(6).Text)
        Me.municipiocasa.Text = Server.HtmlDecode(Me.GridView1.Rows(renglon).Cells(7).Text)
        Me.administrador.Text = Server.HtmlDecode(Me.GridView1.Rows(renglon).Cells(9).Text)
        For i = 0 To Me.estadocasa.Items.Count - 1
            Me.estadocasa.SelectedIndex = i
            If Me.estadocasa.SelectedItem.Text = Server.HtmlDecode(Me.GridView1.Rows(renglon).Cells(8).Text) Then Exit For
        Next


    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Button2.Visible = False
        Me.Button3.Visible = False
        Me.Button1.Visible = True
        Me.GridView1.SelectedIndex = -1
        funciones.limpiacampos(Me)
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        actualiza()
        llenagrid()
        Me.mensajes.Text = "Registro actualizado con éxito"
        Me.Button2.Visible = False
        Me.Button3.Visible = False
        Me.Button1.Visible = True
        Me.GridView1.SelectedIndex = -1
        funciones.limpiacampos(Me)
    End Sub
    Sub actualiza()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Me.GridView1.Columns(0).Visible = True
        Dim id As String = Me.GridView1.Rows(Me.GridView1.SelectedIndex).Cells(0).Text
        Me.GridView1.Columns(0).Visible = False
        sqlConn.Open()
        strTeamQuery = "UPDATE bodegas SET `nombre`='" & Me.nombre.Text & "', `calle`='" & Me.callecasa.Text & "' ,`numero`='" & Me.numcasa.Text & "', `interior`='" & Me.interiorcasa.Text & "', `colonia`='" & Me.coloniacasa.Text & "', `cp`='" & Me.cpcasa.Text & "', `municipio`='" & Me.municipiocasa.Text & "', `estado`='" & Me.estadocasa.SelectedItem.Text & "', administrador=" & Me.administrador.Text & " WHERE id=" & id

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
End Class
