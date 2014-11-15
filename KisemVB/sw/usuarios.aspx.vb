Imports MySql.Data.MySqlClient

Partial Class sw_usuarios
    Inherits System.Web.UI.Page
    Dim funciones As New funciones

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Me.GridView1.DataKeys.Count = 0 Then Me.GridView1.DataKeyNames = New String() {"id"}
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Usuarios Administradores"
            llenagrid()
        End If
    End Sub
    Sub llenagrid()
        Me.GridView1.SelectedIndex = -1
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        strTeamQuery += "SELECT id, CONCAT(nombre, ' ', appaterno, ' ', apmaterno) AS usuario, nivel "
        strTeamQuery += "FROM asociados  "
        strTeamQuery += "WHERE nivel>0 ORDER BY usuario"

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Me.GridView1.DataSource = dtrTeam
        Me.GridView1.DataBind()

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        insertausuario()
        llenagrid()
        funciones.limpiacampos(Me)
        Me.mensajes.Text = "Registro insertado con éxito"
    End Sub
    Sub insertausuario()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim asociado() As String = Split(Me.TextBox1.Text, " ")
        'nivel
        Dim nivel As Integer = 0
        If Me.super.Checked Then nivel = 1
        If Me.administrador.Checked Then nivel = 2
    
        sqlConn.Open()
        strTeamQuery = "UPDATE asociados SET nivel=" & nivel.ToString & " WHERE id=" & asociado(0)

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow

                Dim ctrlEliminar As LinkButton = CType(e.Row.Cells(2).Controls(0), LinkButton)

                ctrlEliminar.OnClientClick = "return confirm('¿Esta seguro de eliminar este registro?');"

                If Me.GridView1.DataKeys(e.Row.RowIndex).Value = Session("idasociado") Then
                    ctrlEliminar.Text = ""
                End If
                Select Case e.Row.Cells(1).Text
                    Case "1"
                        e.Row.Cells(1).Text = "Súper Usuario"
                    Case "2"
                        e.Row.Cells(1).Text = "Administrador"
                    Case "3"
                        e.Row.Cells(1).Text = "Centro de Canje"
                End Select
                
        End Select
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        eliminausuario()
        llenagrid()
        Me.mensajes.Text = "Registro eliminado con éxito"
    End Sub
    Sub eliminausuario()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
          sqlConn.Open()
        strTeamQuery = "UPDATE asociados SET nivel=0 WHERE id=" & Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
End Class
