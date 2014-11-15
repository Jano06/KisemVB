Imports MySql.Data.MySqlClient
Partial Class sw_reporte_datospersonales
    Inherits System.Web.UI.Page
    Dim funciones As New funciones
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Reporte de Datos Personales"


        End If
    End Sub
    Sub llenamisdatos()
        Dim asociado As String() = Split(Me.asociado.Text)
        If Not IsNumeric(asociado(0)) Then
            Me.error.Text = "Error en asociado"
            Exit Sub

        End If
        If Not funciones.existeasociado(CInt(asociado(0))) Then
            Me.error.Text = "No existe Asociado"
            Exit Sub

        End If
        Dim id As Integer = CInt(asociado(0))
        Dim patrocinador As Integer = 0
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT * FROM asociados WHERE id=" & id.ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim bodega As Integer = 0
        While dtrTeam.Read

            If Not IsDBNull(dtrTeam("nombre")) Then Me.nombre.Text = dtrTeam("nombre")
            If Not IsDBNull(dtrTeam("appaterno")) Then Me.nombre.Text += " " & dtrTeam("appaterno").ToString
            If Not IsDBNull(dtrTeam("apmaterno")) Then Me.nombre.Text += " " & dtrTeam("apmaterno")
            If Not IsDBNull(dtrTeam("fnac")) Then
                Dim nacimiento As Date = dtrTeam("fnac")
                Me.nacimiento.Text = nacimiento.ToString("dd/MMMM/yyyy")
            End If
            If Not IsDBNull(dtrTeam("finsc")) Then
                Dim inscripcion As Date = dtrTeam("finsc")
                Me.ingreso.Text = inscripcion.ToString("dd/MMMM/yyyy")
            End If
            If Not IsDBNull(dtrTeam("rfc")) Then Me.rfc.Text = dtrTeam("rfc")
            If Not IsDBNull(dtrTeam("curp")) Then Me.curp.Text = dtrTeam("curp")
            If Not IsDBNull(dtrTeam("pais")) Then Me.pais.Text = dtrTeam("pais")
            Me.pais0.Text = Me.pais.Text
            Me.usuario.Text = dtrTeam("id")

            If Not IsDBNull(dtrTeam("password")) Then Me.password.Text = dtrTeam("password")
            If Not IsDBNull(dtrTeam("tellocal")) Then Me.local.Text = dtrTeam("tellocal")
            If Not IsDBNull(dtrTeam("telmovil")) Then Me.movil.Text = dtrTeam("telmovil")
            If Not IsDBNull(dtrTeam("nextel")) Then Me.nextel.Text = dtrTeam("nextel")
            If Not IsDBNull(dtrTeam("email")) Then Me.email.Text = dtrTeam("email")
            If Not IsDBNull(dtrTeam("alias")) Then Me.alias.Text = dtrTeam("alias")
            If Not IsDBNull(dtrTeam("callecasa")) Then Me.calle.Text = dtrTeam("callecasa")
            If Not IsDBNull(dtrTeam("numcasa")) Then Me.numext.Text = dtrTeam("numcasa")
            If Not IsDBNull(dtrTeam("intcasa")) Then Me.numint.Text = dtrTeam("intcasa")
            If Not IsDBNull(dtrTeam("colcasa")) Then Me.colonia.Text = dtrTeam("colcasa")
            If Not IsDBNull(dtrTeam("cpcasa")) Then Me.cp.Text = dtrTeam("cpcasa")
            If Not IsDBNull(dtrTeam("municipiocasa")) Then Me.municipio.Text = dtrTeam("municipiocasa")
            If Not IsDBNull(dtrTeam("estadocasa")) Then estado.Text = dtrTeam("estadocasa")
            If Not IsDBNull(dtrTeam("callepaq")) Then Me.calle0.Text = dtrTeam("callepaq")
            If Not IsDBNull(dtrTeam("numpaq")) Then Me.numext0.Text = dtrTeam("numpaq")
            If Not IsDBNull(dtrTeam("intpaq")) Then Me.numint0.Text = dtrTeam("intpaq")
            If Not IsDBNull(dtrTeam("colpaq")) Then Me.colonia0.Text = dtrTeam("colpaq")
            If Not IsDBNull(dtrTeam("cppaq")) Then Me.cp0.Text = dtrTeam("cppaq")
            If Not IsDBNull(dtrTeam("municipiopaq")) Then Me.municipio0.Text = dtrTeam("municipiopaq")
            If Not IsDBNull(dtrTeam("estadopaq")) Then estado0.Text = dtrTeam("estadopaq")
            If Not IsDBNull(dtrTeam("Tipo")) Then
                If dtrTeam("Tipo") = 0 Then
                    Me.tipo.Text = "Asociado"
                    Me.acceso.Text = "Sí"
                Else
                    Me.tipo.Text = "Consumidor"
                    Me.acceso.Text = "No"
                End If
            End If
            If Not IsDBNull(dtrTeam("bodega")) Then bodega = dtrTeam("bodega")
            If Not IsDBNull(dtrTeam("patrocinador")) Then patrocinador = dtrTeam("patrocinador")
        End While

        sqlConn.Close()

        strTeamQuery = "SELECT nombre FROM bodegas WHERE id=" & bodega.ToString
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)



        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then Me.bodega.Text = dtrTeam(0)
        End While
        sqlConn.Close()
        strTeamQuery = "SELECT CONCAT(id, ' ', nombre, ' ', appaterno, ' ', apmaterno) FROM asociados WHERE id=" & patrocinador.ToString
        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)



        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()

        While dtrTeam.Read
            If Not IsDBNull(dtrTeam(0)) Then Me.patrocinador.Text = Server.HtmlDecode(dtrTeam(0))
        End While
        sqlConn.Close()
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        llenamisdatos()

    End Sub
End Class
