
Partial Class sw_MasterMaqueta
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        If Session("idasociado") = Nothing Then
            Response.Redirect("salir.aspx")
        End If
        Select Case Session("permisos")
            Case 0
                Me.menuasociado.Visible = True
            Case 1
                Me.Menuadmin.Visible = True

        End Select

        Me.Page.Title = "Kisem. Software de Administración"

    End Sub
End Class

