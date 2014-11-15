Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class sw_reporteavancerangosciclo
    Inherits System.Web.UI.Page
    Dim viewasociados As New DataView()
    Dim iniciociclo, mediociclo, finciclo As Date ' para ciclos de calificación
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Avances por Ciclo"
            llenaciclos()
            If Me.GridView2.DataKeys.Count = 0 Then Me.GridView2.DataKeyNames = New String() {"rangoid"}

        End If

    End Sub
    Sub llenaciclos()
        Try
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = "SET lc_time_names = 'es_UY'; SELECT id,  Date_format(inicio,'%Y/%M/%d') AS inicio,   Date_format(fin,'%Y/%M/%d') AS final FROM ciclos ORDER BY id DESC "
            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader

            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()

            While dtrTeam.Read

                Me.ciclos.Items.Add(New ListItem("de: " & dtrTeam("inicio").ToString & " a: " & dtrTeam("final").ToString, dtrTeam("id").ToString))

            End While

            sqlConn.Close()
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
            Me.mensajes.Visible = True
        End Try
    End Sub

    Sub llenagrid()
        Me.GridView3.DataSource = ""
        Me.GridView3.DataBind()

        Me.GridView2.Columns(1).Visible = True
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "" & _
        "SELECT rangos.id AS rangoid, rangos.nombre AS rango, COUNT( rangoscambios.rango ) AS cuenta " & _
        "FROM `rangoscambios` INNER JOIN rangos ON rangoscambios.rango = rangos.id " & _
        "WHERE(rangoscambios.ciclo = " & Me.ciclos.SelectedItem.Value.ToString & ") " & _
        "GROUP BY rangoscambios.rango " & _
        "ORDER BY rangoscambios.rango"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader




        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Me.GridView2.DataSource = dtrTeam
        Me.GridView2.DataBind()

        sqlConn.Close()
        sqlConn.Dispose()


        Me.GridView2.Columns(1).Visible = False

    End Sub
    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        For i = 0 To Me.GridView2.Rows.Count - 1
            Dim lb As LinkButton = CType(GridView2.Rows(i).Controls(0).Controls(0), LinkButton)
            lb.Text = Me.GridView2.Rows(i).Cells(1).Text

        Next


    End Sub
    Protected Sub GridView2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.SelectedIndexChanged
        If Me.GridView2.SelectedIndex > -1 Then
            llenagrid2()
        End If
    End Sub
    Sub llenagrid2()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT rangoscambios.asociado AS id, CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) AS nombre FROM asociados INNER JOIN rangoscambios ON asociados.id= rangoscambios.asociado WHERE rangoscambios.ciclo=" & Me.ciclos.SelectedItem.Value.ToString & " AND rangoscambios.rango= " & Me.GridView2.DataKeys(Me.GridView2.SelectedIndex).Value.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Me.GridView3.DataSource = dtrTeam
        Me.GridView3.DataBind()

        sqlConn.Close()
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click

        llenagrid()

    End Sub
End Class
