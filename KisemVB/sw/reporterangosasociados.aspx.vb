Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class sw_reporterangosasociados
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Reporte de Rangos"
            If Me.GridView2.DataKeys.Count = 0 Then Me.GridView2.DataKeyNames = New String() {"rangoid"}
            llenagrid()
            Me.GridView2.Columns(1).Visible = False
        End If
    End Sub
    Sub llenagrid()
        Dim rangosid As New List(Of Integer)
        Dim rangos As New List(Of String)
        Dim titulos As New List(Of Integer)
        Dim pagos As New List(Of Integer)
        Dim ds As New DataSet
        Dim table As DataTable = ds.Tables.Add("Data")
        table.Columns.Add("rango", GetType(String))
        table.Columns.Add("rangoid", GetType(Integer))
        table.Columns.Add("titulo", GetType(Integer))
        table.Columns.Add("pago", GetType(Integer))
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "" & _
        "SELECT rangos.nombre AS rango, Count(asociados.Rango) AS titulo, rangos.id " & _
        "FROM rangos LEFT JOIN asociados ON rangos.id = asociados.Rango " & _
        "GROUP BY rangos.nombre, rangos.id " & _
        "ORDER BY rangos.id;"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader




        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            rangosid.Add(dtrTeam(2))
            rangos.Add(dtrTeam(0))
            titulos.Add(dtrTeam(1))
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        strTeamQuery = "" & _
       "SELECT rangos.nombre AS rango, Count(asociados.Rangopago) AS pago, rangos.id " & _
       "FROM rangos LEFT JOIN asociados ON rangos.id = asociados.Rangopago " & _
       "GROUP BY rangos.nombre, rangos.id " & _
       "ORDER BY rangos.id;"
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)

      



        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            
            pagos.Add(dtrTeam(1))
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        For i = 0 To rangosid.Count - 1
            table.Rows.Add(New Object() {rangos(i), rangosid(i), titulos(i), pagos(i)})
        Next
        Dim view As DataView = ds.Tables(0).DefaultView
        Me.GridView2.DataSource = view
        Me.GridView2.DataBind()

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
        Dim strTeamQuery As String = "SELECT id, CONCAT(nombre, ' ', appaterno, ' ', apmaterno) AS nombre FROM asociados WHERE rango= " & Me.GridView2.DataKeys(Me.GridView2.SelectedIndex).Value.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Me.gridrangotitulo.DataSource = dtrTeam
        Me.gridrangotitulo.DataBind()

        sqlConn.Close()
        'rangopagos
        strTeamQuery = "SELECT id, CONCAT(nombre, ' ', appaterno, ' ', apmaterno) AS nombre FROM asociados WHERE rangopago= " & Me.GridView2.DataKeys(Me.GridView2.SelectedIndex).Value.ToString
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)



        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Me.gridrangopago.DataSource = dtrTeam
        Me.gridrangopago.DataBind()

        sqlConn.Close()
        Me.Label1.Visible = True
        Me.Label2.Visible = True
    End Sub
End Class
