
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Public Property tituloenmaster() As String
        Get
            Return Me.mastertitulo.Text
        End Get
        Set(ByVal value As String)
            Me.mastertitulo.Text = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("permisos") = 0 Then
            Me.MenuAsociado.Visible = True
        End If

        If Session("permisos") = 1 Then
            Me.MenuAdmin.Visible = True
        End If
    End Sub



End Class

