Imports MySql.Data.MySqlClient

Partial Class sw_compras_cancelacion
    Inherits System.Web.UI.Page
    Dim funciones As New funciones
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Cancelación de Compras"


        End If
    End Sub
   
 
    Sub llenagrid(ByVal compra As Integer)
        Me.GridCarrito.SelectedIndex = -1
      
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim strTeamQuery As String = "SELECT compras.id AS compra, IF( compras.inscripcion >0, IF( compras.statuspago = 'PAGADO', CONCAT( asociados.id, ' ', asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) ,  CONCAT( prospectos.nombre, ' ', prospectos.appaterno, ' ', prospectos.apmaterno) ) , CONCAT( asociados.id, ' ', asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) ) AS asociado, compras.fechaorden, compras.total AS monto, compras.statusentrega, compras.statuspago " & _
                                "FROM(compras) INNER JOIN asociados ON compras.asociado = asociados.id LEFT JOIN prospectos ON compras.inscripcion = prospectos.id " & _
                                "WHERE compras.id=" & Me.compra.Text & " "

        strTeamQuery += "ORDER BY compras.id DESC"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Me.GridCarrito.DataSource = dtrTeam
        Me.GridCarrito.DataBind()

        sqlConn.Close()
        sqlConn.Dispose()
    End Sub
   

  

    Protected Sub GridCarrito_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridCarrito.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then


            'Programmatically reference the PopupControlExtender

            Dim pce As AjaxControlToolkit.PopupControlExtender = CType(e.Row.FindControl("PopupControlExtender1"), AjaxControlToolkit.PopupControlExtender)

            ' Set the BehaviorID
            Dim behaviorID As String = String.Concat("pce", e.Row.RowIndex)
            pce.BehaviorID = behaviorID

            ' Programmatically reference the Image control
            Dim i As Image = CType(e.Row.Cells(1).FindControl("Image1"), Image)

            ' Add the clie nt-side attributes (onmouseover & onmouseout)
            Dim OnMouseOverScript As String = String.Format("$find('{0}').showPopup();", behaviorID)
            Dim OnMouseOutScript As String = String.Format("$find('{0}').hidePopup();", behaviorID)

            i.Attributes.Add("onmouseover", OnMouseOverScript)
            i.Attributes.Add("onmouseout", OnMouseOutScript)
        End If
    End Sub

   

 
  

    <System.Web.Services.WebMethodAttribute()> <System.Web.Script.Services.ScriptMethodAttribute()> Public Shared Function GetDynamicContent(ByVal contextKey As System.String) As System.String
        Dim sTemp As StringBuilder = New StringBuilder()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT comprasdetalle.compra, comprasdetalle.cantidad, paquetes.nombre AS paquete, comprasdetalle.costo, comprasdetalle.puntos " & _
        "FROM comprasdetalle INNER JOIN paquetes ON comprasdetalle.paquete=paquetes.id " & _
        "WHERE comprasdetalle.compra=" & contextKey

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        sTemp.Append("<table class='overtextonuevo'>")
        sTemp.Append("<tr><td><b>Cantidad</b></td><td><b>Paquete</b></td><td><b>Costo</b></td><td><b>Puntos</b></td></tr>")
        While dtrTeam.Read
            sTemp.Append("<tr><td>" & dtrTeam(1) & "</td><td>" & dtrTeam(2) & "</td><td>" & dtrTeam(3) & "</td><td>" & dtrTeam(4) & "</td></tr>")

        End While

        sqlConn.Close()
        sqlConn.Dispose()


        sTemp.Append("</table>")
        Return sTemp.ToString()
    End Function


 
    Sub actualizaentregado(ByVal compra As Integer)
        ' actualiza compra
        Dim asociado = funciones.quienhacelacompra(compra)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        Dim dtrTeam As MySqlDataReader



        sqlConn.Open()
        strTeamQuery = "UPDATE compras SET statusentrega='ENTREGADO' WHERE id=" & compra.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()
        sqlConn.Close()
        'descuenta de bodega
        Dim bodega As Integer = funciones.buscabodega(asociado)
        sqlConn.Open()
        strTeamQuery = "SELECT  cantidad, paquete FROM comprasdetalle WHERE compra=" & compra.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        dtrTeam = cmdFetchTeam.ExecuteReader()


        While dtrTeam.Read

            funciones.descuentadebodegas(bodega, dtrTeam(1), dtrTeam(0))



        End While
        sqlConn.Close()
    End Sub
    Sub actualizacancelado(ByVal compra As Integer)
        ' actualiza compra
        Dim asociado = funciones.quienhacelacompra(compra)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)



        sqlConn.Open()
        strTeamQuery = "UPDATE compras SET statuspago='CANCELADO', statusentrega='CANCELADO'  WHERE id=" & compra.ToString

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()
        sqlConn.Close()

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Me.compra.Text <> "" Then
            llenagrid(CInt(Me.compra.Text))

        End If
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.compra.Text = ""
        Me.GridCarrito.DataSource = ""
        Me.GridCarrito.DataBind()

    End Sub

    Protected Sub GridCarrito_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridCarrito.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                If e.Row.Cells(5).Text <> "CANCELADO" Then
                    Dim ctrlEliminar As LinkButton = CType(e.Row.Cells(7).Controls(0), LinkButton)
                    ctrlEliminar.OnClientClick = "return confirm('¿Esta seguro de eliminar este registro?');"
                Else
                    e.Row.Cells(7).Text = ""
                End If




        End Select
    End Sub

    Protected Sub GridCarrito_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridCarrito.SelectedIndexChanged
        If Me.GridCarrito.SelectedIndex > -1 Then
            procesacancelacion(CInt(Me.GridCarrito.Rows(Me.GridCarrito.SelectedIndex).Cells(0).Text))
            llenagrid(CInt(Me.GridCarrito.Rows(Me.GridCarrito.SelectedIndex).Cells(0).Text))

            Me.GridCarrito.SelectedIndex = -1
            Me.mensajes.Text = "Registro cancelado con éxito"
        End If
    End Sub
    Sub procesacancelacion(ByVal compra As Integer)

        actualizacancelado(compra)
        If Me.GridCarrito.Rows(Me.GridCarrito.SelectedIndex).Cells(5).Text = "PAGADO" Then
            quitapuntos(compra)
            quitaactivacion(compra)
        End If
        If Me.GridCarrito.Rows(Me.GridCarrito.SelectedIndex).Cells(6).Text = "ENTREGADO" Then
            regresaabodega(compra)
        End If

    End Sub
    Sub quitapuntos(ByVal compra As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)



        Dim strTeamQuery As String = "DELETE FROM puntosasociados WHERE compra=" & compra

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
    Sub quitaactivacion(ByVal compra As Integer)
        Dim asociado As Integer = funciones.quienhacelacompra(compra)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)



        Dim strTeamQuery As String = "UPDATE asociados SET status=0, inicioactivacion=DATE_SUB(inicioactivacion,INTERVAL 1 MONTH), finactivacion=DATE_SUB(finactivacion,INTERVAL 1 MONTH) WHERE id=" & asociado.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
    Sub regresaabodega(ByVal compra As Integer)
        Dim asociado As Integer = funciones.quienhacelacompra(compra)
        Dim bodega As Integer = funciones.buscabodega(asociado)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim strTeamQuery As String = "SELECT paquete, cantidad " & _
                                "FROM comprasdetalle " & _
                                "WHERE compra=" & compra 
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        While dtrTeam.Read
            funciones.regresaabodega(bodega, dtrTeam(0), dtrTeam(1))
        End While
        sqlConn.Close()
        sqlConn.Dispose()




    End Sub
End Class
