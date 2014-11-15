
Partial Class sw_salir
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Session.Clear()
        Response.Redirect("../default.aspx")
    End Sub
End Class
