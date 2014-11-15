Imports MySql.Data.MySqlClient

Partial Class sw_avisos
    Inherits System.Web.UI.Page
    Dim funciones As New funciones

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Me.GridView1.DataKeys.Count = 0 Then Me.GridView1.DataKeyNames = New String() {"id"}
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Avisos"
            llenagrid()
        End If
    End Sub
    Sub llenagrid()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, titulo, texto FROM avisos ORDER BY id DESC"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Me.GridView1.DataSource = dtrTeam
        Me.GridView1.DataBind()

        sqlConn.Close()
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        guardaaviso()
        llenagrid()
        funciones.limpiacampos(Me)
        Me.mensajes.Text = "Registro Insertado con Éxito"
    End Sub
    Sub guardaaviso()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)



        sqlConn.Open()
        strTeamQuery = "INSERT INTO avisos (`titulo`, `texto`) VALUES ( '" & Me.titulo.Text & "', '" & Me.texto.Text & "')"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        llenaaviso(Me.GridView1.SelectedIndex)
    End Sub
    Sub llenaaviso(ByVal renglon As Integer)
        
        Me.Button2.Visible = True
        Me.Button3.Visible = True
        Me.Button1.Visible = False
        Me.titulo.Text = Server.HtmlDecode(Me.GridView1.Rows(renglon).Cells(0).Text)
        Me.texto.Text = Server.HtmlDecode(Me.GridView1.Rows(renglon).Cells(1).Text)



      




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

        Dim id As String = Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value.ToString

        sqlConn.Open()
        strTeamQuery = "UPDATE avisos SET `titulo`='" & Me.titulo.Text & "', `texto`='" & Me.texto.Text & "' WHERE id=" & id

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()

      
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow

                Dim ctrlEliminar As LinkButton = CType(e.Row.Cells(3).Controls(0), LinkButton)
                ctrlEliminar.OnClientClick = "return confirm('¿Esta seguro de eliminar este registro?');"



        End Select
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        eliminaaviso(e.RowIndex)
        llenagrid()
        Me.Button2.Visible = False
        Me.Button3.Visible = False
        Me.Button1.Visible = True
        Me.GridView1.SelectedIndex = -1
        funciones.limpiacampos(Me)
        Me.mensajes.Text = "Registro eliminado con éxito"
    End Sub
    Sub eliminaaviso(ByVal indice As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim id As String = Me.GridView1.DataKeys(indice).Value.ToString

        Dim strTeamQuery As String = "DELETE FROM avisos WHERE id=" & id

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
End Class
