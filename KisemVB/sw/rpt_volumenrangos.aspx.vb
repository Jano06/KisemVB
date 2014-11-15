Imports MySql.Data.MySqlClient
Partial Class sw_rpt_volumenrangos
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
        objEtiqueta.Text = "Reporte de Volumen"
        Dim fecha As Date = Today
        Dim de, a As Date
        Dim banderaa, banderade As Integer
        For i = 1 To 21
            fecha = DateAdd(DateInterval.Day, -1, fecha)
            If fecha.DayOfWeek = DayOfWeek.Friday And banderade = 0 And banderaa = 1 Then
                de = DateAdd(DateInterval.Day, 1, fecha)
                banderade = 1
            End If

            If fecha.DayOfWeek = DayOfWeek.Friday And banderaa = 0 Then
                a = fecha
                banderaa = 1
            End If


        Next
        Me.de.Text = de.ToString("yyyy/M/dd")
        Me.a.Text = a.ToString("yyyy/M/dd")
    End Sub
    Sub generareporte(ByVal idasociado As Integer)
        Dim qry_paquetesypuntos As String = "SELECT comprasdetalle.cantidad, comprasdetalle.paquete, asociados.recorrido, asociados.ladosrecorrido " & _
"FROM asociados INNER JOIN compras ON asociados.id=compras.asociado INNER JOIN `comprasdetalle` ON compras.id=comprasdetalle.compra " & _
 "WHERE asociados.recorrido LIKE '%." & idasociado.ToString & ".%' AND comprasdetalle.paquete BETWEEN 1 AND 3 AND compras.statuspago='PAGADO' AND compras.fecha>='" & Me.de.Text & "' AND compras.fecha<='" & Me.a.Text & "'"

        Dim qry_directosporlado As String = "SELECT Count(asociados.ID) AS directos, asociados.ladopatrocinador AS lado " & _
                                                   "FROM(asociados) " & _
                                                   "WHERE(((asociados.patrocinador)=" & idasociado.ToString & ") AND (asociados.status=1  OR '" & a.Text & "' BETWEEN inicioactivacion AND finactivacion)  ) " & _
                                                   "GROUP BY asociados.patrocinador, asociados.ladopatrocinador " & _
                                                   "ORDER BY asociados.patrocinador, asociados.ladopatrocinador ;"


        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(qry_paquetesypuntos, sqlConn)

        Dim dtrTeam As MySqlDataReader

        'directos por lado período 1
        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim recorrido, ladosrecorrido As String
        Dim recorridoarray(), ladosarray() As String
        Dim izq, der As Integer
        While dtrTeam.Read
            recorrido = dtrTeam(2)

            ladosrecorrido = dtrTeam(3)

            recorridoarray = Split(recorrido, ".")
            ladosarray = Split(ladosrecorrido, ".")
            Dim posicion As Integer = 0
            For posicion = 0 To recorridoarray.Length - 1
                If recorridoarray(posicion) = idasociado.ToString Then
                    Exit For
                End If


            Next
            If UCase(ladosarray(posicion)) = "D" Then
                Select Case dtrTeam(1)
                    Case 1
                        der += 25 * dtrTeam(0)
                    Case 2
                        der += 50 * dtrTeam(0)
                    Case 3
                        der += 75 * dtrTeam(0)
                End Select





            Else
                Select Case dtrTeam(1)
                    Case 1
                        izq += 25 * dtrTeam(0)
                    Case 2
                        izq += 50 * dtrTeam(0)
                    Case 3
                        izq += 75 * dtrTeam(0)
                End Select
            End If
        End While

        sqlConn.Close()
        sqlConn.Dispose()
        Me.volderecho.Text = der.ToString
        Me.volizquierdo.Text = izq.ToString

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim asociado() As String = Split(Me.asociado.Text, " ")
        If IsNumeric(asociado(0)) Then
            generareporte(CInt(asociado(0)))
        End If
    End Sub
End Class
