Imports MySql.Data.MySqlClient
Partial Class sw_comisiones_nuevas
    Inherits System.Web.UI.Page
    Dim idperiodo As Integer
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim comisiones As New Comisiones
        comisiones.Inicio = CDate(Me.de.Text)
        comisiones.Final = CDate(Me.a.Text)
        comisiones.Status = 0
        comisiones.ID = CInt(Me.periodo.Text)
        comisiones.guardanuevo()
        Me.mensajes.Text = "Comisiones generadas con éxito"
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
        objEtiqueta.Text = "Correr Comisiones"
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
        Me.de.Text = de.ToString("dd/M/yyyy")
        Me.a.Text = a.ToString("dd/M/yyyy")
        If Not IsPostBack Then
            defineperiodo()
            Me.periodo.Text = idperiodo.ToString
        End If
    End Sub
    Sub defineperiodo()

        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT MAX(id) FROM periodos WHERE status>0"
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then idperiodo = dtrTeam(0) + 1


        End While

        sqlConn.Close()


    End Sub

End Class
