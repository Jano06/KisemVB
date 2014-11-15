Imports MySql.Data
Imports MySql.Data.MySqlClient
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If Me.CheckBox1.Checked Then
            Me.callepaq.Text = Me.callecasa.Text
            Me.numpaq.Text = Me.numcasa.Text
            Me.interiorpaq.Text = Me.interiorcasa.Text
            Me.coloniapaq.Text = Me.coloniacasa.Text
            Me.cppaq.Text = Me.cpcasa.Text
            Me.estadopaq.SelectedIndex = Me.estadocasa.SelectedIndex
            Me.municipiopaq.Text = Me.municipiocasa.Text
            Me.CiudadPaq.Text = Me.CiudadCasa.Text


            Me.callepaq.Enabled = False
            Me.numpaq.Enabled = False
            Me.interiorpaq.Enabled = False
            Me.coloniapaq.Enabled = False
            Me.cppaq.Enabled = False
            Me.estadopaq.Enabled = False
            Me.municipiopaq.Enabled = False
            Me.CiudadPaq.Enabled = False
        Else
            Me.callepaq.Enabled = True
            Me.numpaq.Enabled = True
            Me.interiorpaq.Enabled = True
            Me.coloniapaq.Enabled = True
            Me.cppaq.Enabled = True
            Me.estadopaq.Enabled = True
            Me.municipiopaq.Enabled = True
            Me.CiudadPaq.Enabled = True
        End If
    End Sub

  

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim objEtiqueta As Label = CType(Master.FindControl("mastertitulo"), Label)
            objEtiqueta.Text = "Alta de Prospectos"
            llenagrid()
            llenadrops()
            Me.nombre.Focus()
            Me.Contrato.Text = elcontrato()


        End If
        
    End Sub
    Sub llenadrops()
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
    Sub llenagrid()

        Me.GridView1.SelectedIndex = -1
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "SELECT id, nombre, ApPaterno, ApMaterno, TelMovil, Email FROM prospectos WHERE patrocinador=" & Session("idasociado").ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim dtrTeam As MySqlDataReader


        sqlConn.Open()
        dtrTeam = cmdFetchTeam.ExecuteReader()
        If dtrTeam.HasRows Then
            Me.misprospectos.Visible = True
        Else
            Me.misprospectos.Visible = False
        End If
        Me.GridView1.DataSource = dtrTeam
        Me.GridView1.DataBind()

        sqlConn.Close()

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow

                Dim ctrlEliminar As LinkButton = CType(e.Row.Cells(5).Controls(0), LinkButton)
                ctrlEliminar.OnClientClick = "return confirm('¿Esta seguro de eliminar este registro?');"



        End Select
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        If Me.GridView1.SelectedIndex > -1 Then
            eliminaprospecto(Me.GridView1.DataKeys(Me.GridView1.SelectedIndex).Value)
            llenagrid()
            Me.mensajes.Text = "Registro eliminado con éxito"
        End If
    End Sub
    Sub eliminaprospecto(ByVal asociado As Integer)
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = "DELETE FROM prospectos WHERE id=" & asociado.ToString

        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)
        sqlConn.Open()


        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()
    End Sub

    

    Protected Sub Acepto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Acepto.CheckedChanged
        If Me.Acepto.Checked Then
            Me.PanelContrato.Visible = False
            Me.PanelAlta.Visible = True
        End If
    End Sub
    Function elcontrato() As String
        Dim respuesta As String = ""
        respuesta = "CONTRATO DE DISTRIBUCION MERCANTIL INDEPENDIENTE QUE CELEBRAN POR UNA PARTE COMO PROVEEDOR 'KISEM DE MEXICO, S. DE R.L. DE C.V.', A QUIEN EN LO  SUCESIVO SE LE DENOMINARA 'KÍSEM' o 'SISTEMA DE PROSPERIDAD KISEM' Y POR LA OTRA PARTE EL FIRMANTE (DE LA SOLICITUD EN EL ANVERSO DE ESTE CONTRATO) COMO COMPRADOR Y DISTRIBUIDOR A QUIEN EN LO SUCESIVO SE LE DENOMINARÁ 'ASOCIADO', QUIENES MANIFIESTAN ESTAR DE ACUERDO EN SUJETARSE A LAS SIGUIENTES DECLARACIONES Y CLÁUSULAS."
        respuesta += vbNewLine & vbNewLine & "I.- D E C L A R A C I O N E S:"
        respuesta += vbNewLine & vbNewLine & "1.-'KISEM' Establece a través  de su representante legal:"
        respuesta += vbNewLine & vbNewLine & "A) Que es una Sociedad de Responsabilidad Limitada constituida legalmente mediante Escritura Pública No. 25,397 el 12 de AGOSTO del 2010, ante la fe del Lic. Enrique Javier Olvera Villaseñor, Titular de la Notaria Público No. 21 de la ciudad de Querétaro, e inscrito en el Registro Público de la Propiedad y del Comercio bajo el folio mercantil electrónico No. 40702-1, y que la celebración de este contrato queda dentro de su objeto social, rigiéndose con las leyes mercantiles de la Republica Mexicana con domicilio en la ciudad de Querétaro, Querétaro, con registro federal de contribuyentes KME-100812-977 y que se dedica además de otras actividades a la venta, distribución y comercialización de diferentes productos de origen natural, alimenticio, cosmético y educativo, de los que de ahora en adelante se les denominará 'PRODUCTOS KISEM'."
        respuesta += vbNewLine & "B) Que ha desarrollado un plan de mercadeo de venta directa basado en redes de contacto persona a persona, con un sistema de ganancia o compensación de varios niveles comúnmente denominado Multinivel o Redes de Mercadeo, cuyas características se especifican a detalle en el Plan de Negocio proporcionado por KISEM, mismo que forma parte integrante del presente contrato."
        respuesta += vbNewLine & "C) Para efectos de este contrato señala como domicilio de KISEM el ubicado en Av. 5 de Febrero No. 305 Int. 207-B Colonia La Capilla, CP 76170 en Querétaro, Querétaro."
        respuesta += vbNewLine & vbNewLine & "2.- El 'Asociado' establece que:"
        respuesta += vbNewLine & vbNewLine & "A) Es una persona física sin ningún impedimento y con capacidad legal para suscribir el presente contrato y está dispuesto a realizar en el momento requerido los trámites gubernamentales y fiscales correspondientes para el buen funcionamiento de su negocio y que se encuentra inscrito ante el Servicio de Administración Tributaria con el Registro Federal de Contribuyentes  _________________, o, en su defecto, se obliga a darse de alta y proporcionar a KISEM su clave de Registro Federal de Contribuyentes."
        respuesta += vbNewLine & "B) Que ha solicitado a KISEM en su calidad de comerciante independiente, ser 'Asociado' no exclusivo, ni subordinado, sin perjuicio de otras actividades comerciales que realiza hoy en día y que cuenta con la experiencia, elementos materiales y humanos propios, así como con las relaciones necesarias para dedicarse de manera independiente a la promoción, distribución y comercio de los Productos KISEM."
        respuesta += vbNewLine & "C) Que llevará a cabo sus actividades de promoción, distribución y comercio de los Productos KISEM de manera independiente, no subordinada, sin derechos exclusivos de ninguna naturaleza en territorio alguno, y sin perjuicio de otras actividades comerciales o de otra índole que efectúa hoy día, ya que como comerciante independiente no está en situación de dependencia de KISEM, ni directa ni indirectamente, en virtud de las relaciones comerciales objeto de este contrato, Entendiéndose que no hay  ninguna relación laboral directa o indirecta con KISEM DE MEXICO, S. DE R.L. DE C.V., ni con los promotores, consumidores, distribuidores, subdistribuidores, comisionistas, representantes, afiliados, patrocinadores o asociados a 'KISEM', y que podrá trabajar libremente como empresario independiente, sin sujetarse a un horario o lugar preestablecido por 'KISEM' quedando a su libre decisión y conveniencia el hacerlo o no sin que esté obligado a presentar algún reporte."
        respuesta += vbNewLine & "D) Que ha leído y revisado el total de las cláusulas y condiciones especificadas de esta solicitud, así como el Plan de Negocio KISEM, y reconoce que forma parte integrante del presente contrato y está de acuerdo con ambos documentos."
        respuesta += vbNewLine & vbNewLine & "3.- Y siendo la voluntad de ambas partes celebrar el presente contrato, están de acuerdo en que se otorgará conforme a las siguientes"
        respuesta += vbNewLine & vbNewLine & "II.- C L Á U S U L A S:"
        respuesta += vbNewLine & vbNewLine & "PRIMERA: KISEM acepta inscribir al 'Asociado' en su registro de Distribuidores Independientes de KISEM, el 'Asociado' tiene el derecho no exclusivo de comprar los productos para su uso personal o para su venta si así lo desea, también podrá promover el 'Sistema de Prosperidad Kísem'"
        respuesta += vbNewLine & vbNewLine & "SEGUNDA: EL 'Asociado' habiendo llenado todos los requisitos y cumpliendo el reglamento de KISEM podrá adquirir los productos al precio establecido de venta para los Asociados según lo establezca KISEM en su política de ventas."
        respuesta += vbNewLine & vbNewLine & "TERCERA: KISEM garantiza una buena calidad en sus productos y una satisfacción total, más no sobre los efectos que ellos produzcan sobre la persona, ya que cada organismo es diferente."
        respuesta += vbNewLine & "El 'Asociado' consumidor deberá ante todo ser honesto y en ningún caso deberá mentir a los nuevos 'Asociados' mencionando datos o información incorrecta que pueda mal entenderse por el cliente. El 'Asociado' deberá al promover los productos,  mencionar que son estrictamente de naturaleza alimentaria y nutricional y no deberá hacer  diagnósticos médicos."
        respuesta += vbNewLine & "En el supuesto caso de que el 'Asociado' falsifique información o incumpla el párrafo anterior y algún prospecto o nuevo 'Asociado' presente por lo anterior alguna reclamación, será exclusiva cuenta del 'Asociado' el atender reclamación comprometiéndose a liberar de cualquier responsabilidad a KISEM."
        respuesta += vbNewLine & vbNewLine & "CUARTA: 'KISEM' podrá conceder a sus 'Asociados' el pago de regalías y bonos por su buen desempeño en su labor como distribuidores. Los porcentajes de las regalías y los montos de los bonos así como la mecánica y los procedimientos respectivos de otorgamiento de dichas regalías y bonos, se encuentran comprendidas en el Plan de Compensación de 'KISEM'.'KISEM' se reserva el derecho de modificar o cambiar en cualquier momento la estructura del plan de mercadotecnia, así como los porcentajes de las regalías y el monto de los bonos manejados en el mismo."
        respuesta += vbNewLine & vbNewLine & "QUINTA:  El 'Asociado' declara que conoce el Plan de Compensación de KISEM, así como los Productos KISEM o en su caso se compromete a conocerlos suficientemente para poder 'recomendar' (tanto los Productos como el Plan de Negocio) a otras personas con honestidad y eficacia. El Distribuidor Independiente percibirá ingresos conforme a lo establecido y previsto en el Plan de Negocio por la distribución de los Productos KISEM al público en general. Se le otorgarán los reembolsos previstos en dicho Plan, los cuales se determinarán tomando como base sus compras directas efectuadas a KISEM, así como las compras de los Asociados que sean parte de su organización o red."
        respuesta += vbNewLine & vbNewLine & "SEXTA: El 'Asociado' deberá de hacer los pedidos y depositar el importe correspondiente a esa compra en la cuenta bancaria indicada por 'KISEM' del banco que se indique previamente, o bien hacer el pago directamente en alguno de los centros de distribución de 'KISEM'. El transporte y los gastos de entrega corren por cuenta del 'Asociado', salvo decisión en contrario."
        respuesta += vbNewLine & vbNewLine & "SÉPTIMA: 'KISEM' Tiene el derecho de modificar cualquier producto a suspenderlo en cualquier momento si así fuera necesario, ya sea para obtener alguna mejora o por causa de fuerza mayor. Si el 'Asociado' tuviera algún pedido pendiente de recibir del producto modificado o cancelado, 'KISEM' se compromete a surtirle un producto equivalente o en su defecto a reembolsarle el importe."
        respuesta += vbNewLine & vbNewLine & "OCTAVA: 'KISEM' le venderá al 'Asociado' los Productos KISEM que este requiera, siempre y cuando KISEM los tenga en existencia en sus almacenes, centros de distribución y puntos de venta. El Asociado deberá liquidar totalmente el importe de sus compras en el momento de realizar su pedido. La propiedad y el riesgo de pérdida de los productos adquiridos conforme a este contrato correrán a cargo del Asociado a partir del momento en que éste reciba la compra de productos que haya hecho a KISEM conforme a su pedido."
        respuesta += vbNewLine & vbNewLine & "NOVENA: El 'Asociado' deberá hacerse cargo de todos los gastos ocasionados por la promoción de los productos y/o de la capacitación,  así como también deberá pagar todos los impuestos inherentes a esta actividad."
        respuesta += vbNewLine & vbNewLine & "DÉCIMA: Los derechos u obligaciones del Asociado que se deriven de este contrato son personales, por lo que no podrán ser cedidos todos o en parte sin el consentimiento previo y por escrito por parte de KISEM. Así como la franquicia, en este caso, es heredable a cualquier persona que el Asociado designe como nuevo propietario de la posición, ya sea mediante testamento o cualquier instrumento legal que el Asociado designe, siempre y cuando el nuevo Asociado cumpla con los requisitos que 'KISEM' estipule en el Plan de Negocio para poder seguir siendo acreedor a los pagos generados."
        respuesta += vbNewLine & vbNewLine & "DÉCIMA PRIMERA: El 'Asociado' está de acuerdo en que una vez entregado el producto, Kísem no acepta devoluciones."
        respuesta += vbNewLine & vbNewLine & "DECÍMA SEGUNDA: El 'Asociado' deberá respetar todos los derechos de propiedad industrial y derechos de autor de 'KÍSEM' y de sus filiales. El 'Asociado'  no podrá utilizar el nombre comercial de 'KISEM' o marcas registradas, bajo los cuales se presentan comercialmente los productos, en ningún medio de publicidad, tarjetas de presentación, carteles, folletos vehículos, chequeras o papelería, sin el previo consentimiento por escrito de Kísem."
        respuesta += vbNewLine & vbNewLine & "DÉCIMA TERCERA: El 'Asociado' no podrá en ningún momento erigirse como representante, agente, sucursal, empleado, funcionario, vendedor o apoderado de 'KISEM'."
        respuesta += vbNewLine & vbNewLine & "DÉCIMA CUARTA: El 'Asociado' se compromete a actuar con honestidad ante sus prospectos y demás compañeros Asociados, realizando sus actividades siempre en equipo y armonía, de acuerdo con las políticas y ética de 'KISEM'."
        respuesta += vbNewLine & vbNewLine & "DÉCIMA QUINTA: Si durante el ejercicio de sus actividades de distribución y comercialización, el Asociado encuentra personas o clientes interesados en la distribución de Productos KISEM, éste podrá invitarlos a que soliciten a KISEM su incorporación como Asociados. En caso de ser aceptados, éstos últimos pasarán a formar parte de su organización o red de distribución independiente bajo su auspicio o patrocinio, sumándose el volumen de sus compras al volumen de compras del Asociado, conforme a lo establecido en el Plan de Negocio."
        respuesta += vbNewLine & vbNewLine & "DÉCIMA SEXTA: El precio de compra del Asociado será regulado por la lista de precios vigente publicada por KISEM a la fecha de efectuar cada compra, aplicable a Productos y materiales de promoción. KISEM se reserva el derecho de modificar su lista de precios en cualquier momento, entrando en vigor en la fecha que se publique dicho cambio"
        respuesta += vbNewLine & vbNewLine & "DÉCIMA SÉPTIMA: KISEM se reserva el derecho de modificar en cualquier momento su Plan de Negocio, hecho que el Distribuidor Independiente acepta y reconoce. En caso que éste último no esté de acuerdo con alguna modificación, tendrá derecho a notificar y solicitar por escrito a KISEM la terminación de este Contrato"
        respuesta += vbNewLine & vbNewLine & "DÉCIMA OCTAVA: La duración de este contrato será indefinida, y podrá darse por terminado a petición de cualquiera de las partes en cualquier momento y sin causa alguna, mediante aviso por escrito proporcionado con 30 días naturales de anticipación, sin responsabilidad alguna para la parte que haya solicitado la terminación, excepto la liquidación de las operaciones pendientes entre ambas partes. En caso que el Asociado dé por terminado este contrato, deberá esperar al menos 6 meses para volver a presentar una nueva solicitud de contrato. La terminación de este contrato podrá hacerse efectiva sin presentar aviso por escrito en caso de que exista algún impedimento o causa de fuerza mayor  que obligue o impida a la empresa continuar con sus actividades comerciales. "
        respuesta += vbNewLine & "En caso de incumplimiento del Distribuidor Independiente del Plan de Negocio o de cualquiera de sus obligaciones derivadas del presente contrato, KISEM podrá dar por terminado de manera inmediata este contrato mediante aviso por escrito y sin necesidad de declaración judicial."
        respuesta += vbNewLine & "Si(el) 'Asociado' no realizara ninguna operación de compra en un periodo consecutivo de 4 meses, automáticamente se le dará de baja de 'KISEM', pudiendo nuevamente ser inscrito como socio al ser patrocinado por segunda ocasión por su mismo patrocinador o por otro Socio Consumidor, en una nueva posición y nuevo ID, sin embargo perdería la totalidad de sus derechos sobre las regalías y/o bonos que genere su red descendente."
        respuesta += vbNewLine & vbNewLine & "DÉCIMA NOVENA: El 'Asociado' acepta de KISEM que se le retengan los impuestos correspondientes al ISR, IVA, etc., derivados de las comisión y/bonificaciones o pagos y demás actos comerciales  y de promoción gravables que realice de acuerdo con las disposiciones fiscales vigentes salvo decisión en contrario (por escrito) emitida por el 'Asociado'."
        respuesta += vbNewLine & "De acuerdo con lo anterior, comunico mi opción (firmando tal decisión en la última hoja de este contrato) por el régimen fiscal de mis ingresos que recibiré de la empresa Kísem de México, S. de R.L. de C.V., los cuales tendrán el tratamiento de 'Actividades Empresariales que se asimilan opcionalmente a salarios' de acuerdo al artículo 110, fracción VI de la Ley de Impuestos sobre la Renta en vigor, Acepto la retención del Impuesto Sobre la Renta que me corresponda de acuerdo a la tabla del Art. 113 de la Ley del ISR en vigor."
        respuesta += vbNewLine & "En caso de que el 'Asociado' desee que no le hagan dicha retención, deberá de entregarle a Kísem recibo de honorarios o factura por el valor del pago entregado."
        respuesta += vbNewLine & "Kísem enterará al Servicio de Administración Tributaria, dichos impuestos retenidos entregándole al 'Asociado' el comprobante de retención respectivo cuando él así lo solicite."
        respuesta += vbNewLine & vbNewLine & "VIGÉSIMA: El 'Asociado' da su consentimiento pleno para que las regalías y/o bonos o premios en efectivo derivadas de su promoción objeto de este contrato, le sean depositados en una cuanta bancaria específica (de acuerdo a las indicaciones de KISEM). Dándose por parte del 'Distribuidor' la aceptación de este instrumento de pago como válido, siendo el comprobante de depósito y/o estado de cuenta que el banco le otorgue a 'KISEM', un instrumento válido para todos los efectos legales que correspondan. A dicho depósito se le retendrán los impuestos correspondientes mencionados en el párrafo anterior."
        respuesta += vbNewLine & vbNewLine & "VIGÉSIMA PRIMERA: LEYES Y JURISDICCIÓN: Las partes se regirán por lo dispuesto en éste contrato mercantil y en lo establecido por el Código Civil para el estado de Querétaro. Para todo lo relativo con la interpretación y cumplimiento del presente contrato, las partes se someten expresamente a la jurisdicción de los tribunales de la Ciudad de Querétaro, Querétaro, México, renunciando expresamente a cualquier fuero de domicilio actual o futuro que pudiese corresponderle. De igual manera para todo lo no previsto en este contrato, las partes se sujetan al Plan de Negocio, o en su defecto al Código de Comercio vigente en el estado de Querétaro."
        respuesta += vbNewLine & vbNewLine & "VIGÉSIMA SEGUNDA: Todo aviso o notificación relacionada con este contrato se hará por escrito y será enviado por correo certificado y con porte pagado a los domicilios especificados al inicio del presente contrato, salvo que exista notificación por escrito de cambio de domicilio de cualquiera de las partes."
        respuesta += vbNewLine & vbNewLine & "VIGÉSIMA TERCERA: Si alguna de las cláusulas de de este contrato fuera declarada nula, o sin efecto legal mediante sentencia judicial o resolución de autoridad competente, las demás cláusulas no dejarán de tener pleno valor y vigencia."
        respuesta += vbNewLine & vbNewLine & "VIGÉSIMA CUARTA: Bajo protesta de decir verdad, ambas partes manifiestan que el presente contrato y Plan de Negocio carece de vicios como son error, dolo u omisión. "
        Return respuesta
    End Function

   

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        confirma()
        Me.PanelAlta.Visible = False
        Me.PanelConfirmación.Visible = True
    End Sub
    Sub confirma()
        If Me.consumidor.Checked Then
            Me.tipo.Text = "CONSUMIDOR"
        Else
            Me.tipo.Text = "ASOCIADO"
        End If
        Me.lbl_nombre.Text = UCase(Me.nombre.Text)
        Me.lbl_apmaterno.Text = UCase(Me.apmat.Text)
        Me.lbl_appaterno.Text = UCase(Me.appat.Text)
        Me.lbl_fnac.Text = UCase(Me.fechanac.Text)
        If Me.Soltero.Checked Then Me.Edocivil.Text = UCase("SOLTERO")
        If Me.Casado.Checked Then Me.Edocivil.Text = UCase("CASADO")
        If Me.Divorciado.Checked Then Me.Edocivil.Text = UCase("DIVORCIADO")
        If Me.UnionLibre.Checked Then Me.Edocivil.Text = UCase("UNIÓN LIBRE")
        Me.lbl_rfc.Text = UCase(Me.rfc.Text)
        Me.lbl_curp.Text = UCase(Me.curp.Text)
        Me.lbl_compania.Text = UCase(Me.compania.Text)
        Me.lbl_telefonolocal.Text = UCase(Me.telefono.Text)
        Me.lbl_telmovil.Text = UCase(Me.celular.Text)
        Me.lbl_nextel.Text = UCase(Me.nextel.Text)
        Me.lbl_email.Text = UCase(Me.email.Text)
        Me.lbl_pais.Text = UCase(Me.pais.Text)
        Me.lbl_idioma.Text = UCase(Me.idioma.Text)
        Me.lbl_calle.Text = UCase(Me.callecasa.Text)
        Me.lbl_numero.Text = UCase(Me.numcasa.Text)
        Me.lbl_interior.Text = UCase(Me.interiorcasa.Text)
        Me.lbl_colonia.Text = UCase(Me.coloniacasa.Text)
        Me.lbl_cp.Text = UCase(Me.cpcasa.Text)
        Me.lbl_estado.Text = UCase(Me.estadocasa.SelectedItem.Text)
        Me.lbl_municipio.Text = UCase(Me.municipiocasa.Text)
        Me.lbl_ciudad.Text = UCase(Me.CiudadCasa.Text)

        Me.lbl_callepaq.Text = UCase(Me.callepaq.Text)
        Me.lbl_numeropaq.Text = UCase(Me.numpaq.Text)
        Me.lbl_interiorpaq.Text = UCase(Me.interiorpaq.Text)
        Me.lbl_coloniapaq.Text = UCase(Me.coloniapaq.Text)
        Me.lbl_cppaq.Text = UCase(Me.cppaq.Text)
        Me.lbl_estadopaq.Text = UCase(Me.estadopaq.SelectedItem.Text)
        Me.lbl_municipiopaq.Text = UCase(Me.municipiopaq.Text)
        Me.lbl_ciudadpaq.Text = UCase(Me.CiudadPaq.Text)
    End Sub
    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Dim tipo As Integer = 2
        If Me.asociado.Checked Then tipo = 1
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("conexionKisem").ConnectionString
        Dim sqlConn As MySqlConnection = New MySqlConnection(strConnection)
        Dim strTeamQuery As String = ""
        Dim cmdFetchTeam As MySqlCommand = New MySqlCommand(strTeamQuery, sqlConn)

        Dim fechadenacimiento As Date
        If IsDate(Me.fechanac.Text) Then
            fechadenacimiento = CDate(Me.fechanac.Text)
        Else
            Me.mensajes.Text = "Fecha de Nacimiento Inválida"
            Exit Sub
        End If
        Dim edocivil As String
        If Me.Soltero.Checked Then edocivil = "SOLTERO"
        If Me.Casado.Checked Then edocivil = "CASADO"
        If Me.Divorciado.Checked Then edocivil = "DIVORCIADO"
        If Me.UnionLibre.Checked Then edocivil = "UNIÓN LIBRE"

        sqlConn.Open()
        strTeamQuery = "INSERT INTO prospectos (`nombre`, `ApPaterno` ,`ApMaterno`, `FNac`, `RFC`, `CURP`, `Compania`, `TelLocal`, `TelMovil`, `Nextel`, `Email`, `Alias`, `Pais`, `Idioma`, `CalleCasa`, `NumCasa`, `IntCasa`, `ColCasa`, `CPCasa`, `MunicipioCasa`, `EstadoCasa`, `CallePaq`, `NumPaq`, `IntPaq`, `ColPaq`, `CPPaq`, `MunicipioPaq`, `EstadoPaq`, `Tipo`, `Patrocinador`, ciudadcasa, ciudadpaq, EstadoCivil, padre, lado   ) VALUES ('" & UCase(Me.nombre.Text) & "', '" & UCase(Me.appat.Text) & "', '" & UCase(Me.apmat.Text) & "', '" & fechadenacimiento.Year.ToString & "/" & fechadenacimiento.Month.ToString & "/" & fechadenacimiento.Day.ToString & "', '" & UCase(Me.rfc.Text) & "', '" & UCase(Me.curp.Text) & "', '" & UCase(Me.compania.Text) & "', '" & Me.telefono.Text & "', '" & Me.celular.Text & "', '" & Me.nextel.Text & "', '" & Me.email.Text & "', '" & Me.alias.Text & "', '" & UCase(Me.pais.Text) & "', '" & UCase(Me.idioma.Text) & "', '" & UCase(Me.callecasa.Text) & "', '" & UCase(Me.numcasa.Text) & "', '" & UCase(Me.interiorcasa.Text) & "', '" & UCase(Me.coloniacasa.Text) & "', '" & Me.cpcasa.Text & "', '" & UCase(Me.municipiocasa.Text) & "', '" & UCase(Me.estadocasa.Text) & "', '" & UCase(Me.callepaq.Text) & "', '" & UCase(Me.numpaq.Text) & "', '" & UCase(Me.interiorpaq.Text) & "', '" & UCase(Me.coloniapaq.Text) & "', '" & Me.cppaq.Text & "', '" & UCase(Me.municipiopaq.Text) & "', '" & UCase(Me.estadopaq.Text) & "', " & tipo.ToString & ", " & Session("idasociado").ToString & ", '" & Me.CiudadCasa.Text & "', '" & Me.CiudadPaq.Text & "', '" & edocivil & "', 0,'')"

        cmdFetchTeam = New MySqlCommand(strTeamQuery, sqlConn)
        cmdFetchTeam.ExecuteNonQuery()


        sqlConn.Close()

        Me.mensajes.Text = "Registro insertado con éxito"
        Me.CheckBox1.Checked = False
        Me.callepaq.Enabled = True
        Me.numpaq.Enabled = True
        Me.interiorpaq.Enabled = True
        Me.coloniapaq.Enabled = True
        Me.cppaq.Enabled = True
        Me.estadopaq.Enabled = True
        Me.municipiopaq.Enabled = True
        Me.CiudadPaq.Enabled = True
        Me.callepaq.Text = ""
        Me.numpaq.Text = ""
        Me.interiorpaq.Text = ""
        Me.coloniapaq.Text = ""
        Me.cppaq.Text = ""
        Me.CiudadPaq.Text = ""
        Me.municipiopaq.Text = ""
        Me.Casado.Checked = True

        Dim funciones As New funciones
        funciones.limpiacampos(Me)
        Response.Redirect("arbolcolocacion.aspx?nuevo=1")
    End Sub

    Protected Sub ImageButton3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton3.Click
        Me.PanelConfirmación.Visible = False
        Me.PanelAlta.Visible = True
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Dim funciones As New funciones
        funciones.abrenuevaventana("descargas/CONTRATO_DE_ASOCIADOS_KISEM.pdf", Me)
    End Sub
End Class
