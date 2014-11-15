Imports MySql.Data
Imports MySql.Data.MySqlClient
Partial Class sw_misdatos
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Mis Datos"
            llenamisdatos()

        End If
    End Sub
    Sub llenamisdatos()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT * FROM asociados WHERE id=" & Session("idasociado").ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader

        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        Dim estadocasa, estadopaq As String
        While dtrTeam.Read
            If Not IsDBNull(dtrTeam("nombre")) Then Me.nombre.Text = dtrTeam("nombre")
            If Not IsDBNull(dtrTeam("appaterno")) Then Me.appat.Text = dtrTeam("appaterno")
            If Not IsDBNull(dtrTeam("apmaterno")) Then Me.apmat.Text = dtrTeam("apmaterno")
            If Not IsDBNull(dtrTeam("fnac")) Then
                Dim nacimiento As Date = dtrTeam("fnac")
                Me.fechanac.Text = nacimiento.ToString("dd/MMMM/yyyy")
            End If
            If Not IsDBNull(dtrTeam("rfc")) Then Me.rfc.Text = dtrTeam("rfc")
            If Not IsDBNull(dtrTeam("curp")) Then Me.curp.Text = dtrTeam("curp")
            If Not IsDBNull(dtrTeam("compania")) Then Me.compania.Text = dtrTeam("compania")
            If Not IsDBNull(dtrTeam("tellocal")) Then Me.telefono.Text = dtrTeam("tellocal")
            If Not IsDBNull(dtrTeam("telmovil")) Then Me.celular.Text = dtrTeam("telmovil")
            If Not IsDBNull(dtrTeam("nextel")) Then Me.nextel.Text = dtrTeam("nextel")
            If Not IsDBNull(dtrTeam("email")) Then Me.email.Text = dtrTeam("email")
            If Not IsDBNull(dtrTeam("alias")) Then Me.alias.Text = dtrTeam("alias")
            If Not IsDBNull(dtrTeam("callecasa")) Then Me.callecasa.Text = dtrTeam("callecasa")
            If Not IsDBNull(dtrTeam("numcasa")) Then Me.numcasa.Text = dtrTeam("numcasa")
            If Not IsDBNull(dtrTeam("intcasa")) Then Me.interiorcasa.Text = dtrTeam("intcasa")
            If Not IsDBNull(dtrTeam("colcasa")) Then Me.coloniacasa.Text = dtrTeam("colcasa")
            If Not IsDBNull(dtrTeam("cpcasa")) Then Me.cpcasa.Text = dtrTeam("cpcasa")
            If Not IsDBNull(dtrTeam("municipiocasa")) Then Me.municipiocasa.Text = dtrTeam("municipiocasa")
            If Not IsDBNull(dtrTeam("estadocasa")) Then estadocasa = dtrTeam("estadocasa")
            If Not IsDBNull(dtrTeam("callepaq")) Then Me.callepaq.Text = dtrTeam("callepaq")
            If Not IsDBNull(dtrTeam("numpaq")) Then Me.numpaq.Text = dtrTeam("numpaq")
            If Not IsDBNull(dtrTeam("intpaq")) Then Me.interiorpaq.Text = dtrTeam("intpaq")
            If Not IsDBNull(dtrTeam("colpaq")) Then Me.coloniapaq.Text = dtrTeam("colpaq")
            If Not IsDBNull(dtrTeam("cppaq")) Then Me.cppaq.Text = dtrTeam("cppaq")
            If Not IsDBNull(dtrTeam("municipiopaq")) Then Me.municipiopaq.Text = dtrTeam("municipiopaq")
            If Not IsDBNull(dtrTeam("estadopaq")) Then estadopaq = dtrTeam("estadopaq")
            If Not IsDBNull(dtrTeam("EstadoCivil")) Then
                Me.Soltero.Checked = False
                Me.Casado.Checked = False
                Me.Divorciado.Checked = False
                Me.UnionLibre.Checked = False
                Select Case dtrTeam("EstadoCivil")
                    Case "SOLTERO"
                        Me.Soltero.Checked = True
                    Case "CASADO"
                        Me.Casado.Checked = True
                    Case "DIVORCIADO"
                        Me.Divorciado.Checked = True
                    Case "UNIÓN LIBRE"
                        Me.UnionLibre.Checked = True
                End Select
            End If

        End While

        sqlConn.Close()

        For i = 0 To Me.estadocasa.Items.Count - 1
            Me.estadocasa.SelectedIndex = i
            If Trim(Me.estadocasa.SelectedItem.Text) = Trim(estadocasa) Then Exit For
        Next
        For i = 0 To Me.estadopaq.Items.Count - 1
            Me.estadopaq.SelectedIndex = i
            If Trim(Me.estadopaq.SelectedItem.Text) = Trim(estadopaq) Then Exit For
        Next
    End Sub

   
    Sub modificardatos()
        Dim edocivil As String
        If Me.Soltero.Checked Then edocivil = "SOLTERO"
        If Me.Casado.Checked Then edocivil = "CASADO"
        If Me.Divorciado.Checked Then edocivil = "DIVORCIADO"
        If Me.UnionLibre.Checked Then edocivil = "UNIÓN LIBRE"
        Dim fechadenac As Date = CDate(Me.fechanac.Text)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "UPDATE asociados "
        strTeamQuery += "SET "
        strTeamQuery += "nombre='" & Me.nombre.Text & "', "
        strTeamQuery += "appaterno='" & Me.appat.Text & "', "
        strTeamQuery += "apmaterno='" & Me.apmat.Text & "', "
        strTeamQuery += "fnac='" & fechadenac.ToString("yyyy/MM/dd") & "', "
        strTeamQuery += "rfc='" & Me.rfc.Text & "', "
        strTeamQuery += "curp='" & Me.curp.Text & "', "
        strTeamQuery += "compania='" & Me.compania.Text & "', "
        strTeamQuery += "tellocal='" & Me.telefono.Text & "', "
        strTeamQuery += "telmovil='" & Me.celular.Text & "', "
        strTeamQuery += "nextel='" & Me.nextel.Text & "', "
        strTeamQuery += "email='" & Me.email.Text & "', "
        strTeamQuery += "alias='" & Me.alias.Text & "', "
        strTeamQuery += "callecasa='" & Me.callecasa.Text & "', "
        strTeamQuery += "numcasa='" & Me.numcasa.Text & "', "
        strTeamQuery += "intcasa='" & Me.interiorcasa.Text & "', "
        strTeamQuery += "colcasa='" & Me.coloniacasa.Text & "', "
        strTeamQuery += "cpcasa='" & Me.cpcasa.Text & "', "
        strTeamQuery += "municipiocasa='" & Me.municipiocasa.Text & "', "
        strTeamQuery += "estadocasa='" & estadocasa.SelectedItem.Text & "', "
        strTeamQuery += "callepaq='" & Me.callepaq.Text & "', "
        strTeamQuery += "numpaq='" & Me.numpaq.Text & "', "
        strTeamQuery += "intpaq='" & Me.interiorpaq.Text & "', "
        strTeamQuery += "colpaq='" & Me.coloniapaq.Text & "', "
        strTeamQuery += "cppaq='" & Me.cppaq.Text & "', "
        strTeamQuery += "municipiopaq='" & Me.municipiopaq.Text & "', "
        strTeamQuery += "estadopaq='" & estadopaq.SelectedItem.Text & "', "
        strTeamQuery += "estadocivil='" & edocivil & "' "


        strTeamQuery += "WHERE id=" & Session("idasociado").ToString
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)



        sqlConn.Open()
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        modificardatos()
        Me.mensajes.Text = "Datos modificados con éxito"
        Response.Redirect("misdatospersonales.aspx")
    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        
        Response.Redirect("misdatospersonales.aspx")
    End Sub

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If Me.CheckBox1.Checked Then
            Me.callepaq.Text = Me.callecasa.Text
            Me.numpaq.Text = Me.numcasa.Text
            Me.interiorpaq.Text = Me.interiorcasa.Text
            Me.coloniapaq.Text = Me.coloniacasa.Text
            Me.cppaq.Text = Me.cpcasa.Text
            Me.estadopaq.SelectedIndex = Me.estadocasa.SelectedIndex
            Me.municipiopaq.Text = Me.municipiocasa.Text

            Me.callepaq.Enabled = False
            Me.numpaq.Enabled = False
            Me.interiorpaq.Enabled = False
            Me.coloniapaq.Enabled = False
            Me.cppaq.Enabled = False
            Me.estadopaq.Enabled = False
            Me.municipiopaq.Enabled = False
        Else
            Me.callepaq.Enabled = True
            Me.numpaq.Enabled = True
            Me.interiorpaq.Enabled = True
            Me.coloniapaq.Enabled = True
            Me.cppaq.Enabled = True
            Me.estadopaq.Enabled = True
            Me.municipiopaq.Enabled = True

        End If
    End Sub
End Class
