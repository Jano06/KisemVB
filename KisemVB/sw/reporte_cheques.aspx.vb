Imports MySql.Data.MySqlClient
Imports System.Data
Partial Class sw_reporte_cheques
    Inherits System.Web.UI.Page
    Dim corte As Integer
    Dim funciones As New funciones
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Reporte de Cheques"
            corte = CInt(Request.QueryString("corte"))
            If Not corte > 0 Then
                Response.Redirect("pagos.aspx")
            Else
                llenagrid()
            End If
        End If
    End Sub
    Sub llenagrid()
        Dim ds As New DataSet
        Dim table As DataTable = ds.Tables.Add("Data")
        table.Columns.Add("id", GetType(Integer))
        table.Columns.Add("nombre", GetType(String))
        table.Columns.Add("rangopago", GetType(String))
        table.Columns.Add("incentivos", GetType(Decimal))
        table.Columns.Add("isr", GetType(Decimal))
        table.Columns.Add("iva", GetType(Decimal))
        table.Columns.Add("retencioniva", GetType(Decimal))
        table.Columns.Add("importe", GetType(Decimal))
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT asociados.id, CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) AS nombre, rangos.nombre AS rangopago, SUM(pagos.monto) AS incentivos, pagos.factura " & _
                                     "FROM asociados INNER JOIN rangos ON asociados.rangopago=rangos.id INNER JOIN pagos ON asociados.id=pagos.asociado " & _
                                     "WHERE pagos.status=1 AND pagos.corte=" & corte.ToString & _
                                     " GROUP BY asociados.id, asociados.nombre, asociados.appaterno, asociados.apmaterno, rangos.nombre, pagos.factura " & _
                                     " HAVING SUM(pagos.monto)>0 "

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader
        Dim isr, iva, retencioniva, importe As Decimal
      


        sqlConn.Open()
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            If dtrTeam(4) = 0 Then
                'isr
                If dtrTeam(3) > 0 Then isr = funciones.calculaisr(dtrTeam(3))
                importe = dtrTeam(3) - isr
                iva = 0
                retencioniva = 0
            Else
                'factura
                iva = dtrTeam(3) * System.Configuration.ConfigurationManager.AppSettings("iva")
                retencioniva = dtrTeam(3) * 0.1
                importe = dtrTeam(3) + iva - retencioniva
                isr = 0
            End If

            table.Rows.Add(New Object() {dtrTeam(0), dtrTeam(1), dtrTeam(2), dtrTeam(3), isr, iva, retencioniva, importe})
        End While

        sqlConn.Close()
        Dim view As DataView = ds.Tables(0).DefaultView
      
        Me.GridCheques.DataSource = view
        Me.GridCheques.DataBind()

    End Sub
   
End Class
