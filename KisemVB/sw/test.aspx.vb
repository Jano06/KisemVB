Imports System.Data
Partial Class sw_test
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'llenagrafica()
        End If
    End Sub
    Sub llenagrafica()
        Dim ds As New DataSet
        Dim table As DataTable = ds.Tables.Add("Data")
        table.Columns.Add("x", GetType(String))
        table.Columns.Add("cantidad", GetType(Integer))
        table.Rows.Add(New Object() {"Total Ventas", 100})
        table.Rows.Add(New Object() {"Total Comisiones", 50})
        table.Rows.Add(New Object() {"Bono 1", 10})
        table.Rows.Add(New Object() {"Bono 2", 12})
        table.Rows.Add(New Object() {"Bono 3", 11})
        Dim view As DataView = ds.Tables(0).DefaultView
        'Me.Chart1.Series("Series1").Points.DataBindXY(view, "x", view, "cantidad")
    End Sub

    Protected Sub UpdateButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdateButton.Click
        System.Threading.Thread.Sleep(5000)
        Me.Label1.Text = "Proceso finalizado"
    End Sub
End Class
