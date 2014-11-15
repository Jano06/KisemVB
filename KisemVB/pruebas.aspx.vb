Imports MySql.Data.MySqlClient
Imports System.Web.Mail
Partial Class pruebas
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim funciones As New funciones
        Me.Label1.Text = funciones.calculaisr(CDec(Me.TextBox1.Text)).ToString
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Label2.Text = ""
        Dim aso As New Asociados
        Dim hijos As List(Of String) = aso.HijosDinamicos(CInt(Me.TextBox2.Text))
        For i = 0 To hijos.Count - 1
            Me.Label2.Text += hijos(i)
            If i < hijos.Count - 1 Then
                Me.Label2.Text += ", "
            End If

        Next

        Dim nietos As New List(Of String)
        For i = 0 To hijos.Count - 1
            Dim asociadosnietos As New Asociados
            Dim nietotemp As List(Of String) = asociadosnietos.HijosDinamicos(hijos(i))
            For x = 0 To nietotemp.Count - 1
                nietos.Add(nietotemp(x))

            Next
        Next


    End Sub
   
End Class
