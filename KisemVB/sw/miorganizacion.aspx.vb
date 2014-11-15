Imports MySql.Data.MySqlClient
Partial Class sw_miorganizacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Me.fecha.Text = Today.Year.ToString & "/" & Today.Month.ToString & "/" & Today.Day.ToString
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Mi Organización"

            llenagridunico()

        End If
    End Sub
    Sub llenagridunico()
        'Me.GridUnico.Columns(3).Visible = True
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        strTeamQuery = "SELECT asociados.id AS asociado, IF(asociados.ladopatrocinador='I', CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno),'') AS nombreizq, IF(asociados.ladopatrocinador='D', CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno),'') AS nombreder, rangos.nombre AS rango, MAX(compras.fecha) AS fecha, asociados.status "
        strTeamQuery += "FROM asociados LEFT JOIN compras ON asociados.id=compras.asociado INNER JOIN rangos ON asociados.rango=rangos.id "
        strTeamQuery += "WHERE asociados.patrocinador=" & Session("idasociado").ToString & " AND asociados.finsc<='" & Me.fecha.Text & "' "
        strTeamQuery += "GROUP BY asociados.id "
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader




        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        Me.GridUnico.DataSource = dtrTeam
        Me.GridUnico.DataBind()

        sqlConn.Close()
        sqlConn.Dispose()
        '    Me.GridUnico.Columns(3).Visible = False
    End Sub
  
 

    
    Protected Sub fecha_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles fecha.TextChanged
        If IsDate(Me.fecha.Text) Then
            llenagridunico()
        Else
            Me.mensajes.Text = "Fecha errónea"
        End If
    End Sub

   
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Dim reporte As New funciones
        reporte.exportaexcel("Mi Organizacion", "SELECT asociados.id AS 'No de Distribuidor', if( asociados.Lado = 'I', CONCAT( asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) , '' ) AS Izquierdo, if( asociados.Lado = 'D', CONCAT( asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno ) , '' ) AS Derecho, if( asociados.status =1, 'Activo', 'Inactivo' ) AS Estatus, rangos.nombre AS Rango FROM(asociados) INNER JOIN rangos ON asociados.rango = rangos.id WHERE patrocinador =" & Session("idasociado").ToString)
        
    End Sub

    Protected Sub GridUnico_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridUnico.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(3).Text <> 1 Then
                e.Row.BackColor = Drawing.Color.DarkGray
                e.Row.ForeColor = System.Drawing.Color.Black

            End If
            Select Case e.Row.Cells(3).Text
                Case "1"
                    e.Row.Cells(3).Text = "Activo"
                Case "2"
                    e.Row.Cells(3).Text = "Suspendido"
                Case "0"
                    e.Row.Cells(3).Text = "Inactivo"
            End Select

        End If
    End Sub

    Protected Sub GridUnico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridUnico.SelectedIndexChanged

    End Sub
End Class
