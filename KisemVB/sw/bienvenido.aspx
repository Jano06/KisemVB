<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="bienvenido.aspx.vb" Inherits="sw_Default2" title="Página sin título" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 990px;
            padding:0;
            
        }
        .style3
        {
            text-align: justify;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: Black;
            vertical-align: top;
            width: 857px;
        }
        .style4
        {
            width: 857px;
        }
        .style5
        {
            text-align: justify;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: Black;
            vertical-align: top;
            width: 52px;
        }
        .style6
        {
        }
        .style7
        {
            width: 108%;
        }
        .style8
        {
            width: 911px;
        }
        .style9
        {
            border: thin solid Silver;
            width: 911px;
        }
        .style10
        {
            text-align: justify;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: Black;
            vertical-align: top;
            width: 916px;
        }
        .style11
        {
            width: 916px;
        }
        .style12
        {
            text-align: justify;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: Black;
            vertical-align: top;
            width: 265px;
        }
        .style13
        {
            text-align: justify;
            font-family: AvantGarde LT Book;
            font-size: x-large;
            font-weight: bold;
            color: #2b5076;
            vertical-align: top;
            width: 265px;
        }
        .style14
        {
            text-align: center;
            font-family: AvantGarde LT Book;
            font-size: x-large;
            font-weight: bold;
            color: #2b5076;
            vertical-align: top;
            width: 265px;
        }
        .style15
        {
            width: 265px;
        }
        .style16
        {
            text-align: justify;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: Black;
            vertical-align: top;
            width: 34px;
        }
        .style17
        {
            text-align: justify;
            font-family: AvantGarde LT Book;
            font-size: x-large;
            font-weight: bold;
            color: #2b5076;
            vertical-align: top;
            width: 34px;
        }
        .style18
        {
            width: 34px;
        }
        .style19
        {
            width: 242px;
        }
        .style20
        {
            text-align: justify;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: Black;
            vertical-align: top;
            width: 537px;
        }
        .style21
        {
            text-align: justify;
            font-family: AvantGarde LT Book;
            font-size: x-large;
            font-weight: bold;
            color: #2b5076;
            vertical-align: top;
            width: 537px;
        }
        .style22
        {
            text-align: center;
            font-family: AvantGarde LT Book;
            font-size: x-large;
            font-weight: bold;
            color: #2b5076;
            vertical-align: top;
            }
        .style23
        {
        }
        .style24
        {
        }
        .style25
        {
            width: 153px;
        }
        .style26
        {
            width: 309px;
        }
        .style27
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
        <tr>
            <td class="style8">
                <asp:ImageButton ID="ImageButton1" runat="server" 
                    ImageUrl="~/sw/img/hilda/barra inicio/barra-inicio_01.jpg" />
                <asp:ImageButton ID="ImageButton2" runat="server" 
                    ImageUrl="~/sw/img/hilda/barra inicio/barra-inicio_02.jpg" />
                <asp:ImageButton ID="ImageButton3" runat="server" 
                    ImageUrl="~/sw/img/hilda/barra inicio/barra-inicio_03.jpg" />
                <asp:ImageButton ID="ImageButton5" runat="server" 
                    ImageUrl="~/sw/img/hilda/barra inicio/barra-inicio_04.jpg" />
            </td>
        </tr>
        <tr>
            <td class="style8">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style9" 
                >
                <asp:Panel ID="PanelAvisos" runat="server">
                <table width="100%">
                    <tr>
                    <td align=center>
                      
                        <asp:ImageMap ID="ImageMap1" runat="server" 
                            ImageUrl="~/sw/img/avisos/aviso4.jpg">
                             <asp:RectangleHotSpot AlternateText="Descarga el Manual Empresarial" 
        Bottom="392" HotSpotMode="Navigate" Left="280" NavigateUrl="~/sw/descargas/Manual_Empresarial.pdf" 
        Right="392" Top="264" Target="_blank" />
                             <asp:RectangleHotSpot AlternateText="Descarga el tríptico X-Tracel" 
        Bottom="392" HotSpotMode="Navigate" Left="392" NavigateUrl="~/sw/descargas/Triptico_X-tracel.pdf" 
        Right="520" Top="264" Target="_blank" />
                        </asp:ImageMap>
                        <br />
                        &nbsp;</td>
                    </tr>
                    <tr>
                    <td align=center>
                         <img alt="" src="img/avisos/aviso2.jpg" style="width: 807px; height: 431px" />
                    </td>
                    </tr>
                </table>
                    
                   
                </asp:Panel>
                <asp:Panel ID="Panel1" runat="server" Width="981px" Visible="False">
                    <table cellpadding="0" cellspacing="0" class="style7">
                        <tr>
                            <td style="background-image:url('img/hilda/fondos/infoprincipal.jpg'); background-position:bottom; background-repeat:no-repeat;">
                                <table class="subtitulo">
                                    <tr>
                                        <td class="style5">
                                            &nbsp;</td>
                                        <td class="style20">
                                            &nbsp;</td>
                                        <td class="style16">
                                            &nbsp;</td>
                                        <td class="style12">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;</td>
                                        <td class="style21">
                                            &nbsp;<asp:Label ID="nombreasociado" runat="server"></asp:Label>
                                        </td>
                                        <td class="style17">
                                            &nbsp;</td>
                                        <td class="style13">
                                            Número de Socio
                                            <asp:Label ID="idasociado" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;</td>
                                        <td class="style21">
                                            &nbsp;</td>
                                        <td class="style17">
                                            &nbsp;</td>
                                        <td class="style13">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;</td>
                                        <td class="titulobienvenidacentrado" colspan="3">
                                            <img alt="" src="img/letreros/Letrero%20bienvenida.png" 
                                                style="width: 758px; height: 26px" /><br />
                                            <img alt="" src="img/letreros/linea%20divisora.png" 
                                                style="width: 800px; height: 1px" /></td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;</td>
                                        <td class="titulobienvenidacentrado" colspan="2">
                                            &nbsp;</td>
                                        <td class="style14">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;</td>
                                        <td class="titulobienvenidacentrado" colspan="3">
                                            Te invitamos a conocer las nuevas herramientas con las que cuentas para seguir 
                                            el desarrollo de tu negocio.</td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;</td>
                                        <td class="titulobienvenidacentrado" colspan="2">
                                            &nbsp;</td>
                                        <td class="style14">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td align="left" colspan="3">
                                            <table style="width: 721px" >
                                                <tr>
                                                    <td colspan="2" valign=bottom>
                                                        <img alt="" src="img/letreros/Importancia.png" 
                                                            style="width: 334px; height: 71px" /></td>
                                                        <td rowspan="3">
                                                            <asp:Image ID="rango" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style19">
                                                        &nbsp;</td>
                                                    <td class="style25">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="style19" valign=bottom>
                                                        Tu Rango Actual:
                                                    </td>
                                                    <td class="style25" valign=bottom>
                                                        <asp:Label ID="lblrangopago" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style19" valign=bottom>
                                                        Tu Máximo Rango Alcanzado: </td>
                                                    <td class="style25" valign=bottom>
                                                        <asp:Label ID="lblnombrerango" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="style26">
                                                        <asp:Image ID="imgrangopago" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style19">
                                                        Tu Patrocinador es: </td>
                                                    <td class="style24" colspan="2">
                                                        <asp:Label ID="patrocinador" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style19">
                                                        Tu Status es: &nbsp;</td>
                                                    <td class="style23" colspan="2">
                                                        <asp:Label ID="estado" runat="server"></asp:Label>
                                                        &nbsp;<asp:Label ID="inactividad" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style22">
                                            </td>
                                        <td class="style18">
                                            &nbsp;</td>
                                        <td class="style15">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server" Visible="False" Width="981px">
                     <table cellpadding="0" cellspacing="0" class="style7">
                        <tr>
                            <td style="background-image:url('img/hilda/fondos/proxcompra.jpg'); background-position:bottom; background-position:left; background-repeat:no-repeat;">
                                <table class="subtitulo">
                                    <tr>
                                        <td class="style5">
                                            &nbsp;</td>
                                        <td class="style10">
                                            <asp:Label ID="Label1" runat="server" Width="500px"></asp:Label>
                                        </td>
                                        <td class="style3">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;</td>
                                        <td class="style10">
                                            FECHA DE PRÓXIMA COMPRA</td>
                                        <td class="style3" rowspan="5">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="style6" colspan="2">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style11">
                                            Día de calificación:
                                            <asp:Label ID="calificacion" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style11">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style11">
                                            Período correspondiente: Del
                                            <asp:Label ID="periodode" runat="server"></asp:Label>
                                            &nbsp;al
                                            <asp:Label ID="periodoa" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style11">
                                            &nbsp;</td>
                                        <td class="style4">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style11">
                                            <asp:Calendar ID="Calendar1" runat="server" NextMonthText="" PrevMonthText="" 
                                                SelectionMode="None" SelectWeekText="" ShowNextPrevMonth="False" 
                                                FirstDayOfWeek="Saturday">
                                            </asp:Calendar>
                                        </td>
                                        <td class="style4">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style11">
                                            &nbsp;</td>
                                        <td class="style4">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style11">
                                            &nbsp;</td>
                                        <td class="style4">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style11">
                                            &nbsp;</td>
                                        <td class="style4">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style11">
                                            &nbsp;</td>
                                        <td class="style4">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style11">
                                            &nbsp;</td>
                                        <td class="style4">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                     </table>       
                </asp:Panel>
                <asp:Panel ID="Panel3" runat="server" Visible="False" Width="979px">
                     <table cellpadding="0" cellspacing="0" class="style7">
                        <tr>
                            <td style="background-image:url('img/hilda/fondos/miorganizacion.jpg'); background-position:bottom; background-repeat:no-repeat;">
                                <table class="subtitulo">
                                    <tr>
                                        <td class="style5">
                                            &nbsp;</td>
                                        <td class="style3">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;</td>
                                        <td class="style3">
                                            MI ORGANIZACIÓN</td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="style6" colspan="2">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style4">
                                            <asp:Label ID="activosizq" runat="server">0</asp:Label>
                                            &nbsp;Patrocinados activos del lado Izquierdo</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style4">
                                            <asp:Label ID="activosder" runat="server">0</asp:Label>
                                            &nbsp;Patrocinados activos del lado Derecho</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style4">
                                            <asp:Label ID="asociadosactivosizq" runat="server">0</asp:Label>
                                            &nbsp;Asociados activos del lado Izquierdo</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style4">
                                            <asp:Label ID="asociadosactivosder" runat="server">0</asp:Label>
                                            &nbsp;Asociados activos del lado Derecho</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style4">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style4">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:Timer ID="Timer1" runat="server" Interval="1000">
                                                    </asp:Timer>
                                                    Faltan
                                                    <asp:Label ID="numdias" runat="server"></asp:Label>
                                                    &nbsp;<asp:Label ID="dias" runat="server" Text="días"></asp:Label>
                                                    ,
                                                    <asp:Label ID="numhoras" runat="server"></asp:Label>
                                                    &nbsp;<asp:Label ID="horas" runat="server" Text="horas"></asp:Label>
                                                    ,
                                                    <br />
                                                    <asp:Label ID="numminutos" runat="server"></asp:Label>
                                                    &nbsp;<asp:Label ID="minutos" runat="server" Text="minutos"></asp:Label>
                                                    &nbsp;y
                                                    <asp:Label ID="numsegundos" runat="server"></asp:Label>
                                                    &nbsp;<asp:Label ID="segundos" runat="server" Text="segundos"></asp:Label>
                                                    &nbsp;para cierre semanal.
                                                    <br />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style4">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style4">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style4">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                      </table>
                </asp:Panel>
                
                <asp:Panel ID="Panel4" runat="server" Visible="False">
                    <table cellpadding="0" cellspacing="0" class="style7">
                        <tr>
                            <td style="background-image:url('img/hilda/fondos/miorganizacion.jpg'); background-position:bottom; background-repeat:no-repeat;">
                                <table class="subtitulo">
                                    <tr>
                                        <td class="style5">
                                            &nbsp;</td>
                                        <td class="style3">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;</td>
                                        <td class="style3">
                                            MIS LOGROS ALCANZADOS</td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="style6" colspan="2">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style4">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                             <ContentTemplate>
                                                 Faltan&nbsp;<asp:Label ID="numdias0" runat="server"></asp:Label>
                                                 &nbsp;<asp:Label ID="dias0" runat="server" Text="días"></asp:Label>
                                                 ,
                                            <asp:Label ID="numhoras0" runat="server"></asp:Label>
                                                 &nbsp;<asp:Label ID="horas0" runat="server" Text="horas"></asp:Label>
                                                 ,
                                            <br />
                                            <asp:Label ID="numminutos0" runat="server"></asp:Label>
                                                 &nbsp;<asp:Label ID="minutos0" runat="server" Text="minutos"></asp:Label>
                                                 &nbsp;y
                                            <asp:Label ID="numsegundos0" runat="server"></asp:Label>
                                                 &nbsp;<asp:Label ID="segundos0" runat="server" Text="segundos"></asp:Label>
                                                 &nbsp;para cierre de ciclo de calificación.
                                            <asp:Timer ID="Timer2" runat="server" Interval="1000">
                                            </asp:Timer>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style4">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style4">
                                            <table class="style27">
                                                <tr>
                                                    <td valign="top">
                                                        <asp:GridView ID="GridRequisitos" runat="server" AutoGenerateColumns="False" 
                                                            CellPadding="4" CssClass="textogrid" ForeColor="#333333" GridLines="None" 
                                                            Width="240px">
                                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                            <Columns>
                                                                <asp:BoundField DataField="requisito" />
                                                                <asp:BoundField DataField="total" HeaderText="Total" />
                                                                <asp:CommandField SelectText="detalle" ShowSelectButton="True" 
                                                                    HeaderText="Total" />
                                                            </Columns>
                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                            <EditRowStyle BackColor="#999999" />
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:GridView ID="GridDetalle" runat="server" AutoGenerateColumns="False" 
                                                            CellPadding="4" CssClass="textogrid" ForeColor="#333333" GridLines="None" 
                                                            Width="800px">
                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                            <Columns>
                                                                <asp:BoundField DataField="id" HeaderText="Asociado" />
                                                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                                                <asp:BoundField DataField="inicioactivacion" DataFormatString="{0:dd/MMMM/yyyy}" 
                                                                    HeaderText="Inicio Activación" />
                                                                <asp:BoundField DataField="finactivacion" DataFormatString="{0:dd/MMMM/yyyy}" 
                                                                    HeaderText="Fin Activación" />
                                                                <asp:BoundField DataField="ptsmes" HeaderText="Puntos del Mes" />
                                                            </Columns>
                                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                            <EditRowStyle BackColor="#999999" />
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style4">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style4">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style4">
                                            REQUISITOS PARA ALCANZAR SU SIGUIENTE RANGO
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;</td>
                                        <td class="style4">
                                            <asp:Panel ID="PanelRequisitos" runat="server" Visible="true" Width="711px">
                                                <table class="style7">
                                                    <tr>
                                                        <td>
                                                            Siguiente Rango:
                                                            <asp:Label ID="siguienterango" runat="server"></asp:Label>
                                                            &nbsp;<asp:ImageButton ID="ImageButton4" runat="server" 
                                                                ImageUrl="~/sw/img/hilda/botones/quefalta.jpg" Visible="False" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="emprendedoresporlado" runat="server"></asp:Label>
                                                            <asp:Label ID="activosporlado" runat="server" Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="puntajeenorganizacion" runat="server"></asp:Label>
                                                            <asp:Label ID="activosenorganizacion" runat="server" Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panelquemefalta" runat="server" Visible="False" Width="708px">
                                                                <table class="style7">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="activosporladofalta" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="activosenorganizacionfalta" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="activosenmiarbolizq" runat="server" Visible="False"></asp:Label>
                                                                            <asp:Label ID="activosenmiarbolder" runat="server" Visible="False"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                
            </td>
        </tr>
        <tr>
            <td align="left" class="style8">
                <img alt="" src="img/hilda/sombra.png" style="width: 323px; height: 80px" /></td>
        </tr>
    </table>
</asp:Content>

