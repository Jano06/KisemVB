
Partial Class sw_principal
    Inherits System.Web.UI.MasterPage
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        If Session("idasociado") = Nothing Then
            Response.Redirect("salir.aspx")
        End If
        Me.lblnombreasociado.Text = Session("nombreasociado").ToString
        Select Case Session("permisos")
            Case 0 ' asociado
                Me.MenuAsociado.Visible = True
            Case 1 ' súper usuario
                Me.Menuadmin.Visible = True
                habilitatodo()
            Case 2 ' administrador
                Me.Menuadmin.Visible = True
                inhabilitapaneles()
                Me.AccordionPaneInventarios.Visible = False
                Me.AccordionPaneInventarios2.Visible = True
            Case Else
                Me.MenuAsociado.Visible = True
        End Select

        Me.Page.Title = "Kisem. Software de Administración"

    End Sub
    Sub inhabilita()
        Dim arreglo As Integer()
        arreglo = Session("menu")
        Dim menu As Menu = Me.AccordionAdmin.FindControl("MenuAdministrador")
        For Each item In menu.Items
            Dim bandera As Boolean = False
            For i = 0 To arreglo.Length - 2
                If item.value = arreglo(i) Then
                    bandera = True
                    Exit For
                End If
            Next
            If Not bandera Then
                item.enabled = False
                item.text = ""
            End If
        Next
        Dim menuReportes As Menu = Me.AccordionPaneReportes.FindControl("MenuReportes")
        For Each itemreportes In menuReportes.Items
            Dim bandera As Boolean = False
            For i = 0 To arreglo.Length - 2
                If itemreportes.value = arreglo(i) Then
                    bandera = True
                    Exit For
                End If
            Next
            If Not bandera Then
                itemreportes.enabled = False
                itemreportes.text = ""
            End If
        Next
       
    End Sub
    Sub habilitatodo()
        For i = 1 To 26
            Try
                Dim panel As Panel = Me.AccordionAdmin.FindControl("Panel" & i.ToString)

                panel.Visible = True

            Catch ex As Exception

            End Try




        Next
        For i = 1 To 26
            Try

                Dim panel2 As Panel = Me.AccordionPaneReportes.FindControl("Panel" & i.ToString)

                panel2.Visible = True
            Catch ex As Exception

            End Try




        Next
    End Sub
    Sub inhabilitapaneles()
        Dim arreglo As Integer()
        arreglo = Session("menu")
      
        For i = 0 To arreglo.Length - 1
            Try
                Dim panel As Panel = Me.AccordionAdmin.FindControl("Panel" & arreglo(i).ToString)

                panel.Visible = True
              
            Catch ex As Exception

            End Try

        Next
        For i = 0 To arreglo.Length - 1
            Try
               
                Dim panel2 As Panel = Me.AccordionPaneReportes.FindControl("Panel" & arreglo(i).ToString)

                panel2.Visible = True
            Catch ex As Exception
                Dim r As Integer = 0
            End Try

        Next
    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        If Me.menuasociado.Visible Then
            If Me.menuasociado.SelectedIndex = 0 Then
                Me.menuasociado.SelectedIndex = 0
            End If
        Else

            If Me.Menuadmin.SelectedIndex = 0 Then
                Me.Menuadmin.SelectedIndex = 0
            End If
        End If

    End Sub

    
End Class

