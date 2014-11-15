
Partial Class sw_simulador
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Me.balance.Focus()

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.mensajes.Visible = False
        Try
            If Val(paq_1.Text) + Val(paq_2.Text) + Val(paq_3.Text) + Val(paq_4.Text) <> 100 Then
                Me.mensajes.Text = "La distribución de compras de los paquetes deben sumar 100%. "
                Me.mensajes.Visible = True
                Exit Sub
            End If
            If Val(dist_1.Text) + Val(dist_2.Text) + Val(dist_3.Text) + Val(dist_4.Text) + Val(dist_5.Text) + Val(dist_6.Text) + Val(dist_7.Text) + Val(dist_8.Text) + Val(dist_9.Text) <> 100 Then
                Me.mensajes.Text = "La distribución de rangos de los asociados deben sumar 100%. "
                Me.mensajes.Visible = True
                Exit Sub
            End If
            Dim montocompras As Decimal = Val(Me.compras.Text)
            Dim monto As Decimal
            Dim porcentaje1 As Decimal = Val(por_1.Text) / 100
            Dim porcentaje2 As Decimal = Val(por_2.Text) / 100
            Dim porcentaje3 As Decimal = Val(por_3.Text) / 100
            Dim porcentaje4 As Decimal = Val(por_4.Text) / 100
            Dim porcentaje5 As Decimal = Val(por_5.Text) / 100
            Dim porcentaje6 As Decimal = Val(por_6.Text) / 100
            Dim porcentaje7 As Decimal = Val(por_7.Text) / 100
            Dim porcentaje8 As Decimal = Val(por_8.Text) / 100
            Dim porcentaje9 As Decimal = Val(por_9.Text) / 100

            Dim distribucion1 As Decimal = Val(dist_1.Text) / 100
            Dim distribucion2 As Decimal = Val(dist_2.Text) / 100
            Dim distribucion3 As Decimal = Val(dist_3.Text) / 100
            Dim distribucion4 As Decimal = Val(dist_4.Text) / 100
            Dim distribucion5 As Decimal = Val(dist_5.Text) / 100
            Dim distribucion6 As Decimal = Val(dist_6.Text) / 100
            Dim distribucion7 As Decimal = Val(dist_7.Text) / 100
            Dim distribucion8 As Decimal = Val(dist_8.Text) / 100
            Dim distribucion9 As Decimal = Val(dist_9.Text) / 100

            'distribución de rangos y % contra compras
            monto = distribucion1 * montocompras * porcentaje1 + distribucion2 * montocompras * porcentaje2 + distribucion3 * montocompras * porcentaje3 + distribucion4 * montocompras * porcentaje4 + distribucion5 * montocompras * porcentaje5 + distribucion6 * montocompras * porcentaje6 + distribucion7 * montocompras * porcentaje7 + distribucion8 * montocompras * porcentaje8 + distribucion9 * montocompras * porcentaje9

            'se le aumenta compras paq 3 y 4

            monto += Val(Me.aumento_3.Text) / 100 * Val(Me.paq_3.Text) / 100 * montocompras + Val(Me.aumento_4.Text) / 100 * Val(Me.paq_4.Text) / 100 * montocompras

            '% de balance
            monto = monto * Val(balance.Text) / 100



            Me.montoapagar.Text = "$ " & monto.ToString




        Catch ex As Exception
            Me.mensajes.Text = ex.ToString
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
End Class
