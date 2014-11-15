Imports MySql.Data.MySqlClient

Partial Class sw_asociados
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Me.TextBox1.Focus()

            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Asociados"
            'llenagrid()
            Dim funciones As New funciones
            funciones.llenabodegas(Me.bodegas, Session("idasociado"))
            funciones.llenarangos(Me.Rangos)
        End If
    End Sub
    Sub llenagrid()
        Try
            For i = 0 To Me.GridView1.Columns.Count - 1
                Me.GridView1.Columns(i).Visible = True
            Next
            Dim asociado() As String = Split(Me.TextBox1.Text, " ")
            Me.GridView1.SelectedIndex = -1
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
            Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
            Dim strTeamQuery As String = ""
            strTeamQuery += "SELECT asociados.id, CONCAT(asociados.nombre, ' ', asociados.appaterno, ' ', asociados.apmaterno) AS nombre, asociados.rfc, CONCAT(asociados.callecasa, ' ', asociados.numcasa) AS calleynumero, asociados.colcasa, asociados.municipiocasa, asociados.ciudadcasa, asociados.estadocasa, asociados.cpcasa, asociados.tellocal, asociados.telmovil, asociados.fnac, asociados.finsc, asociados.email, asociados.patrocinador, CONCAT(asociados1.nombre, ' ', asociados1.appaterno, ' ', asociados1.apmaterno) AS nombrepatrocinador, rangos.nombre AS rango, asociados.status, asociados.inicioactivacion, asociados.bono6 "
            strTeamQuery += "FROM asociados INNER JOIN rangos ON asociados.rango=rangos.id INNER JOIN asociados AS asociados1 ON asociados.patrocinador=asociados1.id "


            strTeamQuery += " WHERE asociados.id>3 "
            If Me.TextBox1.Text <> "" Then
                strTeamQuery += " AND asociados.id=" & asociado(0)
            End If
            If Me.estado.SelectedIndex > 0 Then
                strTeamQuery += " AND asociados.estadocasa='" & Me.estado.SelectedItem.Text & "'"
            End If
            If Me.Activos.Checked Then strTeamQuery += " AND asociados.status=1 "
            If Me.Inactivos.Checked Then strTeamQuery += " AND asociados.status=0 "
            If Me.Suspendidos.Checked Then strTeamQuery += " AND asociados.status=2 "
            If Me.fechade.Text <> "" Then strTeamQuery += " AND asociados.finsc>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
            If Me.fechaa.Text <> "" Then strTeamQuery += " AND asociados.finsc<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
            If Me.FechaNacde.Text <> "" Then strTeamQuery += " AND asociados.fnac>='" & CDate(Me.FechaNacde.Text).ToString("yyyy/MM/dd") & "' "
            If Me.fechanaca.Text <> "" Then strTeamQuery += " AND asociados.fnac<='" & CDate(Me.fechanaca.Text).ToString("yyyy/MM/dd") & "' "
            If Me.Rangos.SelectedIndex > 0 Then strTeamQuery += " AND asociados.rango= " & Me.Rangos.SelectedValue.ToString & " "
            If Me.estado.SelectedIndex > 0 Then strTeamQuery += " AND asociados.estadocasa LIKE '%" & Me.estado.SelectedItem.Text & "%' "
            If Me.Ciudad.Text <> "" Then strTeamQuery += " AND asociados.ciudadcasa LIKE '%" & Me.Ciudad.Text & "%' "

            strTeamQuery += " ORDER BY asociados.id"

            Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

            Dim dtrTeam As MySqlDataReader


            sqlConn.Open()
            dtrTeam = cmdFetchTeam.ExecuteReader()
            Me.GridView1.DataSource = dtrTeam
            Me.GridView1.DataBind()

            sqlConn.Close()
            sqlConn.Dispose()
        Catch ex As Exception
            Me.mensajes.Text = ex.Message.ToString
        End Try

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Panel1.Visible = False
        llenagrid()

    End Sub

    Protected Sub GridView1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DataBound
        If Not Me.nombre_chk.Checked Then
            Me.GridView1.Columns(1).Visible = False
        Else
            Me.GridView1.Columns(1).Visible = True
        End If
        If Not Me.rfc_chk.Checked Then
            Me.GridView1.Columns(2).Visible = False
        Else
            Me.GridView1.Columns(2).Visible = True
        End If
        If Not Me.calleynum_chk.Checked Then
            Me.GridView1.Columns(3).Visible = False
        Else
            Me.GridView1.Columns(3).Visible = True
        End If
        If Not Me.colonia_chk.Checked Then
            Me.GridView1.Columns(4).Visible = False
        Else
            Me.GridView1.Columns(4).Visible = True
        End If
        If Not Me.ciudad_chk.Checked Then
            Me.GridView1.Columns(5).Visible = False
        Else
            Me.GridView1.Columns(5).Visible = True
        End If
        If Not Me.municipio_chk.Checked Then
            Me.GridView1.Columns(6).Visible = False
        Else
            Me.GridView1.Columns(6).Visible = True
        End If
        If Not Me.Estado_chk.Checked Then
            Me.GridView1.Columns(7).Visible = False
        Else
            Me.GridView1.Columns(7).Visible = True
        End If
        If Not Me.CP_chk.Checked Then
            Me.GridView1.Columns(8).Visible = False
        Else
            Me.GridView1.Columns(8).Visible = True
        End If
        If Not Me.TelFijo_chk.Checked Then
            Me.GridView1.Columns(9).Visible = False
        Else
            Me.GridView1.Columns(9).Visible = True
        End If
        If Not Me.TelMovil_chk.Checked Then
            Me.GridView1.Columns(10).Visible = False
        Else
            Me.GridView1.Columns(10).Visible = True
        End If
        If Not Me.FechaNac_chk.Checked Then
            Me.GridView1.Columns(11).Visible = False
        Else
            Me.GridView1.Columns(11).Visible = True
        End If
        If Not Me.FechaInsc_chk.Checked Then
            Me.GridView1.Columns(12).Visible = False
        Else
            Me.GridView1.Columns(12).Visible = True
        End If
        If Not Me.Email_chk.Checked Then
            Me.GridView1.Columns(13).Visible = False
        Else
            Me.GridView1.Columns(13).Visible = True
        End If
        If Not Me.NumPatrocinador_chk.Checked Then
            Me.GridView1.Columns(14).Visible = False
        Else
            Me.GridView1.Columns(14).Visible = True
        End If
        If Not Me.NomPatrocinador_chk.Checked Then
            Me.GridView1.Columns(15).Visible = False
        Else
            Me.GridView1.Columns(15).Visible = True
        End If
        If Not Me.Rango_chk.Checked Then
            Me.GridView1.Columns(16).Visible = False
        Else
            Me.GridView1.Columns(16).Visible = True
        End If
        If Not Me.Status_chk.Checked Then
            Me.GridView1.Columns(17).Visible = False
        Else
            Me.GridView1.Columns(17).Visible = True
        End If
        If Not Me.UltimaCompra_chk.Checked Then
            Me.GridView1.Columns(18).Visible = False
        Else
            Me.GridView1.Columns(18).Visible = True
        End If
        If Not Me.Bono6_chk.Checked Then
            Me.GridView1.Columns(19).Visible = False
        Else
            Me.GridView1.Columns(19).Visible = True
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(17).Text = "1" Then e.Row.Cells(17).Text = "Activo"
            If e.Row.Cells(17).Text = "0" Then e.Row.Cells(17).Text = "Inactivo"
            If e.Row.Cells(17).Text = "2" Then e.Row.Cells(17).Text = "Suspendido"
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        If Me.GridView1.SelectedIndex > -1 Then
            llenadrops()
            llenadatos()

            Me.Panel1.Visible = True
        Else

            Me.Panel1.Visible = False
        End If

    End Sub
    Sub llenadrops()
        Me.estadocasa.Items.Clear()
        Me.estadopaq.Items.Clear()
        Me.estadocasa.Items.Add("Aguascalientes")
        Me.estadocasa.Items.Add("Baja California")
        Me.estadocasa.Items.Add("Baja California Sur")
        Me.estadocasa.Items.Add("Campeche")
        Me.estadocasa.Items.Add("Chiapas")
        Me.estadocasa.Items.Add("Chihuahua")
        Me.estadocasa.Items.Add("Coahuila")
        Me.estadocasa.Items.Add("Colima")
        Me.estadocasa.Items.Add("Distrito Federal")
        Me.estadocasa.Items.Add("Durango")
        Me.estadocasa.Items.Add("Estado de México")
        Me.estadocasa.Items.Add("Guanajuato")
        Me.estadocasa.Items.Add("Guerrero")
        Me.estadocasa.Items.Add("Hidalgo")
        Me.estadocasa.Items.Add("Jalisco")
        Me.estadocasa.Items.Add("Michoacán")
        Me.estadocasa.Items.Add("Morelos")
        Me.estadocasa.Items.Add("Nayarit")
        Me.estadocasa.Items.Add("Nuevo León")
        Me.estadocasa.Items.Add("Oaxaca")
        Me.estadocasa.Items.Add("Puebla")
        Me.estadocasa.Items.Add("Querétaro")
        Me.estadocasa.Items.Add("Quintana Roo")
        Me.estadocasa.Items.Add("San Luis Potosí")
        Me.estadocasa.Items.Add("Sinaloa")
        Me.estadocasa.Items.Add("Sonora")
        Me.estadocasa.Items.Add("Tabasco")
        Me.estadocasa.Items.Add("Tamaulipas")
        Me.estadocasa.Items.Add("Tlaxcala")
        Me.estadocasa.Items.Add("Veracruz")
        Me.estadocasa.Items.Add("Yucatan")
        Me.estadocasa.Items.Add("Zacatecas")
        Me.estadopaq.Items.Add("Aguascalientes")
        Me.estadopaq.Items.Add("Baja California")
        Me.estadopaq.Items.Add("Baja California Sur")
        Me.estadopaq.Items.Add("Campeche")
        Me.estadopaq.Items.Add("Chiapas")
        Me.estadopaq.Items.Add("Chihuahua")
        Me.estadopaq.Items.Add("Coahuila")
        Me.estadopaq.Items.Add("Colima")
        Me.estadopaq.Items.Add("Distrito Federal")
        Me.estadopaq.Items.Add("Durango")
        Me.estadopaq.Items.Add("Estado de México")
        Me.estadopaq.Items.Add("Guanajuato")
        Me.estadopaq.Items.Add("Guerrero")
        Me.estadopaq.Items.Add("Hidalgo")
        Me.estadopaq.Items.Add("Jalisco")
        Me.estadopaq.Items.Add("Michoacán")
        Me.estadopaq.Items.Add("Morelos")
        Me.estadopaq.Items.Add("Nayarit")
        Me.estadopaq.Items.Add("Nuevo León")
        Me.estadopaq.Items.Add("Oaxaca")
        Me.estadopaq.Items.Add("Puebla")
        Me.estadopaq.Items.Add("Querétaro")
        Me.estadopaq.Items.Add("Quintana Roo")
        Me.estadopaq.Items.Add("San Luis Potosí")
        Me.estadopaq.Items.Add("Sinaloa")
        Me.estadopaq.Items.Add("Sonora")
        Me.estadopaq.Items.Add("Tabasco")
        Me.estadopaq.Items.Add("Tamaulipas")
        Me.estadopaq.Items.Add("Tlaxcala")
        Me.estadopaq.Items.Add("Veracruz")
        Me.estadopaq.Items.Add("Yucatan")
        Me.estadopaq.Items.Add("Zacatecas")

    End Sub
    Sub llenadatos()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT * FROM asociados WHERE id=" & Me.GridView1.Rows(Me.GridView1.SelectedIndex).Cells(0).Text
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim bodega As Integer = 0
        While dtrTeam.Read

            If Not IsDBNull(dtrTeam("nombre")) Then Me.nombre.Text = dtrTeam("nombre")
            If Not IsDBNull(dtrTeam("appaterno")) Then Me.appat.Text = dtrTeam("appaterno").ToString
            If Not IsDBNull(dtrTeam("apmaterno")) Then Me.apmat.Text = dtrTeam("apmaterno")
            If Not IsDBNull(dtrTeam("fnac")) Then
                Dim nacimiento As Date = dtrTeam("fnac")
                Me.fechanac.Text = nacimiento.ToString("dd/MM/yyyy")
            End If
           
            If Not IsDBNull(dtrTeam("rfc")) Then Me.rfc.Text = dtrTeam("rfc")
            If Not IsDBNull(dtrTeam("curp")) Then Me.curp.Text = dtrTeam("curp")
            If Not IsDBNull(dtrTeam("pais")) Then
                For i = 0 To Me.pais.Items.Count - 1
                    Me.pais.SelectedIndex = i
                    If Me.pais.SelectedItem.Text = dtrTeam("pais") Then Exit For
                Next
            End If

          
            If Not IsDBNull(dtrTeam("tellocal")) Then Me.telefono.Text = dtrTeam("tellocal")
            If Not IsDBNull(dtrTeam("telmovil")) Then Me.celular.Text = dtrTeam("telmovil")
            If Not IsDBNull(dtrTeam("nextel")) Then Me.nextel.Text = dtrTeam("nextel")
            If Not IsDBNull(dtrTeam("email")) Then Me.email.Text = dtrTeam("email")
            If Not IsDBNull(dtrTeam("callecasa")) Then Me.callecasa.Text = dtrTeam("callecasa")
            If Not IsDBNull(dtrTeam("numcasa")) Then Me.numcasa.Text = dtrTeam("numcasa")
            If Not IsDBNull(dtrTeam("intcasa")) Then Me.interiorcasa.Text = dtrTeam("intcasa")
            If Not IsDBNull(dtrTeam("colcasa")) Then Me.coloniacasa.Text = dtrTeam("colcasa")
            If Not IsDBNull(dtrTeam("cpcasa")) Then Me.cpcasa.Text = dtrTeam("cpcasa")
            If Not IsDBNull(dtrTeam("municipiocasa")) Then Me.municipiocasa.Text = dtrTeam("municipiocasa")
            If Not IsDBNull(dtrTeam("estadocasa")) Then
                For i = 0 To Me.estadocasa.Items.Count - 1
                    Me.estadocasa.SelectedIndex = i
                    If UCase(Me.estadocasa.SelectedItem.Text) = dtrTeam("estadocasa") Then Exit For
                Next

            End If

            If Not IsDBNull(dtrTeam("callepaq")) Then Me.callepaq.Text = dtrTeam("callepaq")
            If Not IsDBNull(dtrTeam("numpaq")) Then Me.numpaq.Text = dtrTeam("numpaq")
            If Not IsDBNull(dtrTeam("intpaq")) Then Me.interiorpaq.Text = dtrTeam("intpaq")
            If Not IsDBNull(dtrTeam("colpaq")) Then Me.coloniapaq.Text = dtrTeam("colpaq")
            If Not IsDBNull(dtrTeam("cppaq")) Then Me.cppaq.Text = dtrTeam("cppaq")
            If Not IsDBNull(dtrTeam("municipiopaq")) Then Me.municipiopaq.Text = dtrTeam("municipiopaq")
            If Not IsDBNull(dtrTeam("estadopaq")) Then
                For i = 0 To Me.estadopaq.Items.Count - 1
                    Me.estadopaq.SelectedIndex = i
                    If UCase(Me.estadopaq.SelectedItem.Text) = dtrTeam("estadopaq") Then Exit For
                Next
            End If

             If Not IsDBNull(dtrTeam("factura")) Then
                If dtrTeam("factura") > 0 Then
                    Me.factura.Checked = True
                End If
            End If
            If Not IsDBNull(dtrTeam("bodega")) Then
                For i = 0 To Me.bodegas.Items.Count - 1
                    Me.bodegas.SelectedIndex = i
                    If Me.bodegas.SelectedItem.Value = dtrTeam("bodega") Then Exit For
                Next
            End If
            If dtrTeam("status") = 0 Then
                Me.Suspendido.Checked = False
                Me.Activo.Checked = False
                Me.Inactivo.Checked = True
            End If
            If dtrTeam("status") = 2 Then
                Me.Inactivo.Checked = False
                Me.Activo.Checked = False
                Me.Suspendido.Checked = True
            End If
            If Not IsDBNull(dtrTeam("inicioactivacion")) Then
                Dim inicioactivacion As Date = dtrTeam("inicioactivacion")
                Me.ActivoDe.Text = inicioactivacion.ToString("dd/MM/yyyy")
            End If
            If Not IsDBNull(dtrTeam("finactivacion")) Then
                Dim finactivacion As Date = dtrTeam("finactivacion")
                Me.Activoa.Text = finactivacion.ToString("dd/MM/yyyy")
            End If
        End While

        sqlConn.Close()
    End Sub
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Dim reporte As New funciones
        Dim asociado() As String = Split(Me.TextBox1.Text, " ")
        Dim strTeamQuery As String = ""
        



        strTeamQuery = "SELECT asociados.id AS Numero"
        If Me.nombre_chk.Checked Then
            strTeamQuery += ", asociados.nombre  AS Nombre"
            strTeamQuery += ", asociados.appaterno AS 'Apellido Paterno'"
            strTeamQuery += ", asociados.apmaterno AS 'Apellido Materno'"
        End If

        If Me.rfc_chk.Checked Then strTeamQuery += ", asociados.rfc AS RFC"
        If Me.calleynum_chk.Checked Then strTeamQuery += ", CONCAT(asociados.callecasa, ' ', asociados.numcasa) AS 'Calle y numero'"
        If Me.colonia_chk.Checked Then strTeamQuery += ", asociados.colcasa AS Colonia"
        If Me.ciudad_chk.Checked Then strTeamQuery += ", asociados.ciudadcasa AS Ciudad"
        If Me.municipio_chk.Checked Then strTeamQuery += ", asociados.municipiocasa AS Municipio"
        If Me.Estado_chk.Checked Then strTeamQuery += ", asociados.estadocasa AS Estado"
        If Me.CP_chk.Checked Then strTeamQuery += ", asociados.cpcasa AS CP"
        If Me.TelFijo_chk.Checked Then strTeamQuery += ", asociados.tellocal AS 'Tel Fijo'"
        If Me.TelMovil_chk.Checked Then strTeamQuery += ", asociados.telmovil AS Movil"
        If Me.FechaNac_chk.Checked Then strTeamQuery += ", asociados.fnac AS 'Fecha de Nacimiento'"
        If Me.FechaInsc_chk.Checked Then strTeamQuery += ", asociados.finsc AS 'Fecha de Inscripcion'"
        If Me.Email_chk.Checked Then strTeamQuery += ", asociados.email AS Email"
        If Me.NumPatrocinador_chk.Checked Then strTeamQuery += ", asociados.patrocinador AS Patrocinador"
        If Me.NomPatrocinador_chk.Checked Then strTeamQuery += ", CONCAT(asociados1.nombre, ' ', asociados1.appaterno, ' ', asociados1.apmaterno) AS 'Nombre Patrocinador'"
        If Me.Rango_chk.Checked Then strTeamQuery += ", rangos.nombre AS Rango"
        If Me.Status_chk.Checked Then strTeamQuery += ", asociados.status AS Status"
        If Me.UltimaCompra_chk.Checked Then strTeamQuery += ", asociados.inicioactivacion AS 'Inicio Activacion'"
        If Me.Bono6_chk.Checked Then strTeamQuery += ", asociados.bono6 AS 'Bono 6'"
        strTeamQuery += " FROM asociados INNER JOIN rangos ON asociados.rango=rangos.id INNER JOIN asociados AS asociados1 ON asociados.patrocinador=asociados1.id "


        strTeamQuery += " WHERE asociados.id>3 "
        If Me.TextBox1.Text <> "" Then
            strTeamQuery += " AND asociados.id=" & asociado(0)
        End If
        If Me.estado.SelectedIndex > 0 Then
            strTeamQuery += " AND asociados.estadocasa='" & Me.estado.SelectedItem.Text & "'"
        End If
        If Me.Activos.Checked Then strTeamQuery += " AND asociados.status=1 "
        If Me.Inactivos.Checked Then strTeamQuery += " AND asociados.status=0 "
        If Me.Suspendidos.Checked Then strTeamQuery += " AND asociados.status=2 "
        If Me.fechade.Text <> "" Then strTeamQuery += " AND asociados.finsc>='" & CDate(Me.fechade.Text).ToString("yyyy/MM/dd") & "' "
        If Me.fechaa.Text <> "" Then strTeamQuery += " AND asociados.finsc<='" & CDate(Me.fechaa.Text).ToString("yyyy/MM/dd") & "' "
        If Me.FechaNacde.Text <> "" Then strTeamQuery += " AND asociados.fnac>='" & CDate(Me.FechaNacde.Text).ToString("yyyy/MM/dd") & "' "
        If Me.fechanaca.Text <> "" Then strTeamQuery += " AND asociados.fnac<='" & CDate(Me.fechanaca.Text).ToString("yyyy/MM/dd") & "' "
        If Me.Rangos.SelectedIndex > 0 Then strTeamQuery += " AND asociados.rango= " & Me.Rangos.SelectedValue.ToString & " "
        If Me.estado.SelectedIndex > 0 Then strTeamQuery += " AND asociados.estadocasa LIKE '%" & Me.estado.SelectedItem.Text & "%' "
        If Me.Ciudad.Text <> "" Then strTeamQuery += " AND asociados.ciudadcasa LIKE '%" & Me.Ciudad.Text & "%' "

        strTeamQuery += " ORDER BY asociados.id"

        reporte.exportaexcel("Asociados", strTeamQuery)

    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        reinicia()
    End Sub
    Sub reinicia()
        Me.callepaq.Enabled = True
        Me.numpaq.Enabled = True
        Me.interiorpaq.Enabled = True
        Me.coloniapaq.Enabled = True
        Me.cppaq.Enabled = True
        Me.estadopaq.Enabled = True
        Me.municipiopaq.Enabled = True
        Me.callepaq.Text = ""
        Me.numpaq.Text = ""
        Me.interiorpaq.Text = ""
        Me.coloniapaq.Text = ""
        Me.cppaq.Text = ""

        Me.municipiopaq.Text = ""
        Dim funciones As New funciones
        funciones.limpiacampos(Me)
        Me.Panel1.Visible = False
        Me.GridView1.SelectedIndex = -1
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        guardadatos()
        reinicia()
        Me.mensajes.Text = "Registro actualizado con éxito"
    End Sub
    Sub guardadatos()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim facturaint As Integer = 0
        If Me.factura.Checked Then facturaint = 1
        Dim status As Integer = 0
        If Me.Activo.Checked Then
            status = 1
        Else
            If Me.Inactivo.Checked Then
                status = 0
            Else
                status = 2
            End If
        End If
        Dim strTeamQuery As String = "UPDATE asociados SET nombre='" & UCase(Me.nombre.Text) & "', appaterno='" & UCase(Me.appat.Text) & "', apmaterno='" & UCase(Me.apmat.Text) & "', " & _
        "fnac='" & CDate(Me.fechanac.Text).ToString("yyyy/MM/dd") & "', " & _
        "rfc='" & UCase(Me.rfc.Text) & "', " & _
        "curp='" & UCase(Me.curp.Text) & "', " & _
        "pais='" & UCase(Me.pais.Text) & "', " & _
        "tellocal='" & UCase(Me.telefono.Text) & "', " & _
        "telmovil='" & UCase(Me.celular.Text) & "', " & _
        "nextel='" & UCase(Me.nextel.Text) & "', " & _
        "email='" & Me.email.Text & "', " & _
        "callecasa='" & UCase(Me.callecasa.Text) & "', " & _
        "numcasa='" & UCase(Me.numcasa.Text) & "', " & _
        "intcasa='" & UCase(Me.interiorcasa.Text) & "', " & _
        "colcasa='" & UCase(Me.coloniacasa.Text) & "', " & _
        "cpcasa='" & UCase(Me.cpcasa.Text) & "', " & _
        "municipiocasa='" & UCase(Me.municipiocasa.Text) & "', " & _
        "estadocasa='" & UCase(Me.estadocasa.Text) & "', " & _
        "callepaq='" & UCase(Me.callepaq.Text) & "', " & _
        "numpaq='" & UCase(Me.numpaq.Text) & "', " & _
        "intpaq='" & UCase(Me.interiorpaq.Text) & "', " & _
        "colpaq='" & UCase(Me.coloniapaq.Text) & "', " & _
        "cppaq='" & UCase(Me.cppaq.Text) & "', " & _
        "municipiopaq='" & UCase(Me.municipiopaq.Text) & "', " & _
        "estadopaq='" & UCase(Me.estadopaq.Text) & "', " & _
        "factura='" & facturaint.ToString & "', " & _
        "bodega='" & Me.bodegas.SelectedItem.Value.ToString & "', " & _
         "inicioactivacion='" & CDate(Me.ActivoDe.Text).ToString("yyyy/MM/dd") & "', " & _
         "finactivacion='" & CDate(Me.Activoa.Text).ToString("yyyy/MM/dd") & "', " & _
         "status='" & status.ToString & "' " & _
        " WHERE id=" & Me.GridView1.Rows(Me.GridView1.SelectedIndex).Cells(0).Text
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)




        sqlConn.Open()
        cmdFetchTeam.ExecuteNonQuery()




        sqlConn.Close()
    End Sub
End Class
