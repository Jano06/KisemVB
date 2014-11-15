Imports MySql.Data
Imports MySql.Data.MySqlClient
Partial Class mantenimiento
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim func As New funciones
        If Not func.EnMantenimiento Then
            Response.Redirect("default.aspx")

        End If
    End Sub
   

End Class
