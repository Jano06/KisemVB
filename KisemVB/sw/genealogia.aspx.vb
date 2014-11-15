Imports MySql.Data.MySqlClient
Imports System.Data
Partial Class sw_genealogia
    Inherits System.Web.UI.Page

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim asociado() As String = Split(Me.TextBox1.Text, " ")
        If IsNumeric(asociado(0)) Then
            llenagrids()



            Me.mensajes.Text = ""
        Else
            Me.mensajes.Text = "Error con el asociado"

        End If
    End Sub
    Sub llenagrids()
        Dim asociado() As String = Split(Me.TextBox1.Text, " ")
        Dim ds As New DataSet
        Dim tableIzq As DataTable = ds.Tables.Add("tableIzq")
        tableIzq.Columns.Add("id", GetType(String))
        tableIzq.Columns.Add("nombre", GetType(String))
        Dim tableDer As DataTable = ds.Tables.Add("tableDer")
        tableDer.Columns.Add("id", GetType(String))
        tableDer.Columns.Add("nombre", GetType(String))


        'nuevo, incluyendo pago hasta el siguiente lunes
        Dim qry As String = "SELECT id, CONCAT(nombre, ' ', appaterno, ' ', apmaterno) AS nombre, recorrido, ladosrecorrido  " & _
      "FROM asociados  " & _
      "WHERE recorrido LIKE '%." & asociado(0) & ".%'  "



        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(qry, sqlConn)

        Dim dtrTeam As MySqlDataReader
        Dim comprasderechas As String = ""

        'directos por lado período 1
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim recorrido, ladosrecorrido As String
        Dim recorridoarray(), ladosarray() As String
          While dtrTeam.Read
        

            recorrido = dtrTeam(2)

            ladosrecorrido = dtrTeam(3)

            recorridoarray = Split(recorrido, ".")
            ladosarray = Split(ladosrecorrido, ".")
            Dim posicion As Integer = 0
            For posicion = 0 To recorridoarray.Length - 1
                If recorridoarray(posicion) = asociado(0) Then
                    Exit For
                End If


            Next

            If ladosarray(posicion) <> "" Then
                If UCase(ladosarray(posicion)) = "D" Then
                    tableDer.Rows.Add(New Object() {dtrTeam(0), dtrTeam(1)})


                Else
                    tableIzq.Rows.Add(New Object() {dtrTeam(0), dtrTeam(1)})
                End If


            End If

        End While

        sqlConn.Close()
        sqlConn.Dispose()
        If tableIzq.Rows.Count = 0 Then
            tableIzq.Rows.Add(New Object() {"", ""})
        End If
        If tableDer.Rows.Count = 0 Then
            tableDer.Rows.Add(New Object() {"", ""})
        End If
       
        Dim viewDer As DataView = ds.Tables("tableDer").DefaultView
        Me.GridDer.DataSource = viewDer
        Me.GridDer.DataBind()
        Dim viewIzq As DataView = ds.Tables("tableIzq").DefaultView
        Me.GridIzq.DataSource = viewIzq
        Me.GridIzq.DataBind()
    End Sub
End Class
