Imports MySql.Data.MySqlClient
Partial Class sw_exportaraexcel
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        exporta()
        Session("nombrereporte") = ""
        Session("queryreporte") = ""
    End Sub
    Sub exporta2()
        Session("nombrereporte") = "test"
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, nombre, email, password FROM asociados WHERE patrocinador=11 "

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader



        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & Session("nombrereporte") & Date.Now.ToString & ".xls")
        Me.Literal1.Text = "<table border=1>"

        Dim indice As Integer = 0

        While dtrTeam.Read

            If indice = 0 Then
                Me.Literal1.Text += "<tr style='font-size:medium; font-weight:bold;'>"
                For i = 0 To dtrTeam.FieldCount - 1
                    Me.Literal1.Text += "<td>" & dtrTeam.GetName(i) & "</td>"
                Next
                Me.Literal1.Text += "</tr>"

                indice += 1
            End If





            Me.Literal1.Text += "<tr>"
            For i = 0 To dtrTeam.FieldCount - 1
                Me.Literal1.Text += "<td>" & dtrTeam.Item(i).ToString & "</td>"
            Next

            Me.Literal1.Text += "</tr>"

        End While
        Me.Literal1.Text += "</table>"

        sqlConn.Close()
    End Sub
    Sub exporta()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = Session("queryreporte")

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader



        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & Session("nombrereporte") & Date.Now.ToString & ".xls")
        Me.Literal1.Text = "<table border=1>"

        Dim indice As Integer = 0

        While dtrTeam.Read

            If indice = 0 Then
                Me.Literal1.Text += "<tr style='font-size:medium; font-weight:bold;'>"
                For i = 0 To dtrTeam.FieldCount - 1
                    Me.Literal1.Text += "<td>" & Server.HtmlEncode(dtrTeam.GetName(i)) & "</td>"
                Next
                Me.Literal1.Text += "</tr>"

                indice += 1
            End If





            Me.Literal1.Text += "<tr>"
            For i = 0 To dtrTeam.FieldCount - 1
                Me.Literal1.Text += "<td>" & Server.HtmlEncode(dtrTeam.Item(i).ToString) & "</td>"
            Next

            Me.Literal1.Text += "</tr>"

        End While
        Me.Literal1.Text += "</table>"

        sqlConn.Close()
    End Sub
End Class
