Imports MySql.Data.MySqlClient
Partial Class sw_configuracion
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Configuración"
            llenadatos()

        End If
    End Sub
    Sub llenadatos()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT costoenvio,  mensajecompra, porcentajedebalance, pago1bono6, pago2bono6, pago1bono7, pago2bono7, pago1bono8, pago2bono8, porcentajedebalancerango2, puntosrango2, puntosrango3, puntosrango4, puntosrango5, puntosrango6, puntosrango7, puntosrango8, puntosrango9, pagominimo678, porcentajebono5, bono10rango5, bono10rango6, bono10rango7, bono10rango8, bono10rango9, mensajebienvenida FROM configuracion "
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read

            Me.envio.Text = dtrTeam(0)
            Me.mensajedecompra.Text = dtrTeam(1)
            Me.balance.Text = dtrTeam(2)
            Me.pago1bono6.Text = dtrTeam(3)
            Me.pago2bono6.Text = dtrTeam(4)
            Me.pago1bono7.Text = dtrTeam(5)
            Me.pago2bono7.Text = dtrTeam(6)
            Me.pago1bono8.Text = dtrTeam(7)
            Me.pago2bono8.Text = dtrTeam(8)
            Me.balancecolaborador.Text = dtrTeam(9)
            Me.puntosrango2.Text = dtrTeam(10)
            Me.puntosrango3.Text = dtrTeam(11)
            Me.puntosrango4.Text = dtrTeam(12)
            Me.puntosrango5.Text = dtrTeam(13)
            Me.puntosrango6.Text = dtrTeam(14)
            Me.puntosrango7.Text = dtrTeam(15)
            Me.puntosrango8.Text = dtrTeam(16)
            Me.puntosrango9.Text = dtrTeam(17)
            Me.pagominimo.Text = dtrTeam(18)
            Me.porcentajebono5.Text = dtrTeam(19)
            Me.bono10rango5.Text = dtrTeam(20)
            Me.bono10rango6.Text = dtrTeam(21)
            Me.bono10rango7.Text = dtrTeam(22)
            Me.bono10rango8.Text = dtrTeam(23)
            Me.bono10rango9.Text = dtrTeam(24)
            If Not IsDBNull(dtrTeam(25)) Then Me.mensajedebienvenida.Text = dtrTeam(25).ToString
        End While

        sqlConn.Close()
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        llenadatos()

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        actualizaconfiguracion()
        Me.mensajes.Text = "Registros actualizados con Éxito"
    End Sub
    Sub actualizaconfiguracion()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

       
        sqlConn.Open()
        strTeamQuery = "UPDATE configuracion SET costoenvio=" & Me.envio.Text & ",  mensajecompra='" & Me.mensajedecompra.Text & "', porcentajedebalance=" & Me.balance.Text & ", pago1bono6=" & Me.pago1bono6.Text & ", pago2bono6=" & Me.pago2bono6.Text & ", pago1bono7=" & Me.pago1bono7.Text & ", pago2bono7=" & Me.pago2bono7.Text & ", pago1bono8=" & Me.pago1bono8.Text & ", pago2bono8=" & Me.pago2bono8.Text & ", porcentajedebalancerango2=" & Me.balancecolaborador.Text & ", puntosrango2=" & Me.puntosrango2.Text & ", puntosrango3=" & Me.puntosrango3.Text & ", puntosrango4=" & Me.puntosrango4.Text & ", puntosrango5=" & Me.puntosrango5.Text & ", puntosrango6=" & Me.puntosrango6.Text & ", puntosrango7=" & Me.puntosrango7.Text & ", puntosrango8=" & Me.puntosrango8.Text & ", puntosrango9=" & Me.puntosrango9.Text & ", pagominimo678=" & Me.pagominimo.Text & ", porcentajebono5=" & Me.porcentajebono5.Text & ", bono10rango5=" & Me.bono10rango5.Text & ", bono10rango6=" & Me.bono10rango6.Text & ", bono10rango7=" & Me.bono10rango7.Text & ", bono10rango8=" & Me.bono10rango8.Text & ", bono10rango9=" & Me.bono10rango9.Text & ", mensajebienvenida='" & Me.mensajedebienvenida.Text & "'"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub
End Class
