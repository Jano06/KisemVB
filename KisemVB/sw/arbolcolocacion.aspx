<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="arbolcolocacion.aspx.vb" Inherits="sw_arbolcolocacion" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style1
        {
            width: 900px;
        }
.posicion2{
	width:100px; 
	height:100px; 
	background-color:#fff; 
	float:right;
	text-align:center;
	vertical-align:bottom;
	
}
.posicion3{
	width:100px; 
	height:100px; 
	background-color:#fff; 
	float:left;
	text-align:center;
	vertical-align:bottom;
}
.topecentro{
	 width:300px; 
	 height:100px; 
	 background-color:#fff; 
	 float:left; 
	 text-align:center;
	 vertical-align:bottom;
}
.topeizq{
	 width:250px; 
	 height:100px; 
	 background-color:#fff; 
	 float:left;
	 vertical-align:bottom;
	
	}
.topeder{
	 width:250px; 
	 height:100px; 
	 background-color:#fff; 
	 float:right;
	 text-align:right;
	 vertical-align:bottom;
	}
.topecentro1{
	 width:300px; 
	 height:150px; 
	 background-color:#fff; 
	 float:left; 
	 text-align:center;
	 vertical-align:bottom;
}
.topeizq1{
	 width:250px; 
	 height:150px; 
	 background-color:#fff; 
	 float:left;
	 vertical-align:bottom;
	 
	}
.topeder1{
	 width:250px; 
	 height:150px; 
	 background-color:#fff; 
	 float:right;
	 text-align:right;
	 vertical-align:bottom;
	}

.renglon1{
	width:800px; 
	height:150px; 
	background-color:#fff;
	vertical-align:bottom;
	
	}
.renglon{
	width:800px; 
	height:100px; 
	background-color:#fff;
	vertical-align:bottom;
	}
.doble{
	width:100px;
	background-color:#fff;
	border:#fff;
	float:left;
	text-align:center;
	height:100px;
	vertical-align:bottom;
	}
.doblemitad{
	width:50px;
	background-color:#fff;
	border:#fff;
	float:left;
	text-align:center;
	height:100px;
	vertical-align:bottom;
	}
.individual{
	width:50px;
	background-color:#fff;
	border:#fff;
	float:left;
	text-align:center;
	height:100px;
	vertical-align:bottom;
	}
.individualcorto{
	width:50px;
	background-color:#fff;
	border:#fff;
	float:left;
	text-align:center;
	height:100px;
	vertical-align:bottom;
	}
.individualmitadcorto{
	width:25px;
	background-color:#fff;
	border:#fff;
	float:left;
	text-align:center;
	height:100px;
	vertical-align:bottom;
	}
.individualmitad{
	width:25px;
	background-color:#fff;
	border:#fff;
	float:left;
	text-align:center;
	height:100px;
	vertical-align:bottom;
	}
        .style2
        {
            width: 100%;
            height: 100%;
        }
        .style3
        {
            width: 100%;
        }
        .style4
        {
            width: 439px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
    <tr>
        <td align="center">
            <img alt="" src="img/hilda/titulos/arbol.jpg" 
                style="width: 252px; height: 33px" /></td>
    </tr>
    <tr>
        <td align="center">
            <asp:Panel ID="PanelNuevo" runat="server" Visible="False">
                <asp:Label ID="mensaje" runat="server" 
    CssClass="titulobienvenidacentrado" 
    Text="Ubica la posición donde colocarás a tu prospecto, haciendo click en "></asp:Label>
                <img alt="" src="img/rangoshilda/0.png" style="width: 30px; height: 50px" />
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td class="celdamarco">
            <table class="style3">
                <tr>
                    <td align=center colspan="2">
                                    <img alt="" src="img/hilda/arbol/rangostitulo.png" 
                                        style="width: 321px; height: 34px" /></td>
                </tr>
                <tr>
                    <td align=center colspan="2">
                                    &nbsp;</td>
                </tr>
                <tr>
                    <td align=center colspan="2">
                                    <img alt="" src="img/rangoshilda/lineaderangos.png" 
                                        style="width: 596px; height: 59px" /></td>
                                               </tr>
                <tr>
                    <td align=center class="style4">
                                    &nbsp;</td>
                                                   <td align="center">
                                                       &nbsp;</td>
                                               </tr>
                                               <tr>
                                                   <td colspan="2" align="center">
            <table width="800" cellspacing="0" cellpadding="0">
  <tr>
    <td>
    	<div class="renglon1">
        	<div class="topeizq1">
                                    
                                    <table class="style3">
                                        <tr>
                                            <td>
                                                <img alt="" src="img/hilda/arbol/puntosizquierdos.png" 
                                                    style="width: 240px; height: 35px" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="puntosizq" runat="server" CssClass="titulobienvenida"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
            </div>
            <div class="topecentro1">
                                    <asp:ImageButton ID="ImageButton1" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/1.png" />
                                    <br />
                                    <asp:Label width="40px"  ID="alias1" runat="server" CssClass="textochico">ALI000</asp:Label>
                                </div>
            <div class="topeder1">
                                    <table class="style3">
                                        <tr>
                                            <td>
                                                <img alt="" src="img/hilda/arbol/puntosderechos.png" 
                                                    style="width: 240px; height: 35px" /></td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="puntosder" runat="server" CssClass="titulobienvenida"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                </div>
        </div>
    </td>
  </tr>
  <tr>
    <td>
    	<div class="renglon">
        	<div class="topeizq">
            	<div class="posicion2">
            	     <table cellpadding="0" cellspacing="0" class="style2">
                            <tr>
                                <td valign="bottom" 
                                    style="background-image:url('img/rangoshilda/lineader.jpg');">
                        <asp:ImageButton ID="ImageButton2" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias2" runat="server"></asp:Label>
                                   </td>
                        </tr>
                        </table>
                                </div>
                                    <asp:ImageButton ID="ImageButton33" runat="server" 
                                ImageUrl="~/sw/img/hilda/arbol/fondoizq.png" 
                                        ToolTip="Ir al Fondo de la Izquierda" />
            </div>
            <div class="topecentro">
                <img alt="" src="img/rangoshilda/r1.jpg" style="width: 300px; height: 100px" /></div>
            <div class="topeder">
            	<div class="posicion3">
                        <table cellpadding="0" cellspacing="0" class="style2">
                            <tr>
                                <td valign="bottom" 
                                    style="background-image: url('img/rangoshilda/lineaizq.jpg'); background-repeat: no-repeat">
                                    <asp:ImageButton ID="ImageButton3" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias3" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                        
                                </div>
                                    <asp:ImageButton ID="ImageButton32" runat="server" 
                                ImageUrl="~/sw/img/hilda/arbol/fondoder.png" ToolTip="Ir al Fondo de la Derecha" />
            </div>
        </div>
    
    </td>
  </tr>
  <tr>
    <td>
   	  <div class="renglon">
        	<div class="doblemitad"></div>
            <div class="doble">
                 <table cellpadding="0" cellspacing="0" class="style2">
                            <tr>
                                <td valign="bottom" style="background-image: url('img/rangoshilda/lineader.jpg'); background-repeat: no-repeat">
                        <asp:ImageButton ID="ImageButton4" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                        <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias4" runat="server"></asp:Label>
                         </td>
  </tr>
                        </table>
                                            </div>
            <div class="doble">
                <img alt="" src="img/rangoshilda/r2.jpg" style="width: 100px; height: 100px" /></div>
            <div class="doble">
                <table cellpadding="0" cellspacing="0" class="style2">
                            <tr>
                                <td valign="bottom" style="background-image: url('img/rangoshilda/lineaizq.jpg'); background-repeat: no-repeat">
                        
                       
                        <asp:ImageButton ID="ImageButton5" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias5" runat="server"></asp:Label>
                          </td>
                        </tr>
                        </table>                    
                        </div>
            <div class="doble"></div>
            <div class="doble">
                <table cellpadding="0" cellspacing="0" class="style2">
                            <tr>
                                <td valign="bottom" style="background-image: url('img/rangoshilda/lineader.jpg'); background-repeat: no-repeat">
                        
                         
                        <asp:ImageButton ID="ImageButton6" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                        <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias6" runat="server"></asp:Label>
                         </td>
                    </tr>
                        </table>                   
                        </div>
            <div class="doble">
                <img alt="" src="img/rangoshilda/r2.jpg" style="width: 100px; height: 100px" /></div>
            <div class="doble">
                <table cellpadding="0" cellspacing="0" class="style2">
                            <tr>
                                <td valign="bottom" style="background-image: url('img/rangoshilda/lineaizq.jpg'); background-repeat: no-repeat">
                        
                         
                        <asp:ImageButton ID="ImageButton7" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                        <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias7" runat="server"></asp:Label>
                        </td>
  </tr>
                        </table>                    </div>
            <div class="doblemitad"></div>
        </div>
    </td>
  </tr>
  <tr>
    <td>
   	  <div class="renglon">
        	<div class="individualmitadcorto"></div>
            <div class="individualcorto">
                <table cellpadding="0" cellspacing="0" class="style2">
                            <tr>
                                <td valign="bottom">
                        
                        
                        <asp:ImageButton ID="ImageButton8" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                                <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias8" runat="server"></asp:Label>
                         </td>
  </tr>
                        </table>                    </div>
            <div class="individualcorto">
                <img alt="" src="img/rangoshilda/r3.jpg" style="width: 50px; height: 100px" /></div>
            <div class="individualcorto">
                <table cellpadding="0" cellspacing="0" class="style2">
                            <tr>
                                <td valign="bottom">
                        
                        
                        <asp:ImageButton ID="ImageButton9" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias9" runat="server"></asp:Label>
                         </td>
  </tr>
                        </table>                    </div>
            <div class="individualcorto"></div>
            <div class="individualcorto">
                <table cellpadding="0" cellspacing="0" class="style2">
                            <tr>
                                <td valign="bottom">
                        
                       
                        <asp:ImageButton ID="ImageButton10" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias10" runat="server"></asp:Label>
                          </td>
  </tr>
                        </table>                    </div>
            <div class="individualcorto">
                <img alt="" src="img/rangoshilda/r3.jpg" style="width: 50px; height: 100px" /></div>
            <div class="individualcorto">
                <table cellpadding="0" cellspacing="0" class="style2">
                            <tr>
                                <td valign="bottom">
                        
                        
                        <asp:ImageButton ID="ImageButton11" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias11" runat="server"></asp:Label>
                         </td>
  </tr>
                        </table>                    </div>
            <div class="individualcorto"></div>
            <div class="individualcorto">
            <table cellpadding="0" cellspacing="0" class="style2">
                            <tr>
                                <td valign="bottom">
                        
                        
                        <asp:ImageButton ID="ImageButton12" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias12" runat="server"></asp:Label>
                          </td>
  </tr>
                        </table>                   </div>
            <div class="individualcorto">
                <img alt="" src="img/rangoshilda/r3.jpg" style="width: 50px; height: 100px" /></div>
            <div class="individualcorto">
                <table cellpadding="0" cellspacing="0" class="style2">
                            <tr>
                                <td valign="bottom">
                        
                        
                        <asp:ImageButton ID="ImageButton13" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias13" runat="server"></asp:Label>
                         </td>
  </tr>
                        </table>                    </div>
            <div class="individualcorto"></div>
            <div class="individualcorto">
            <table cellpadding="0" cellspacing="0" class="style2">
                            <tr>
                                <td valign="bottom">
                        
                        
                        <asp:ImageButton ID="ImageButton14" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias14" runat="server"></asp:Label>
                         </td>
  </tr>
                        </table>                    </div>
            <div class="individualcorto">
                <img alt="" src="img/rangoshilda/r3.jpg" style="width: 50px; height: 100px" /></div>
            <div class="individualcorto">
                <table cellpadding="0" cellspacing="0" class="style2">
                            <tr>
                                <td valign="bottom">
                        
                         
                        <asp:ImageButton ID="ImageButton15" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias15" runat="server"></asp:Label>
                       </td>
  </tr>
                        </table>                     </div>
            <div class="individualmitadcorto"></div>
        </div></td>
  </tr>
  <tr>
    <td>
    	<div class="renglon">
        	<div class="individual">
            	<table width="100%" height="100%" cellspacing="0" cellpadding="0">
                	<tr>
                    	<td colspan=2 valign=top>
                        <asp:ImageButton ID="ImageButton16" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias16" runat="server"></asp:Label>
                                </td>
                    </tr>
                    <tr>
                    	<td>
                        <asp:ImageButton ID="I1" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
                        </td>
                        <td>
                        <asp:ImageButton ID="D1" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="individual">
            	<table width="100%" height="100%" cellspacing="0" cellpadding="0">
                	<tr>
                    	<td colspan=2 valign=top>
                        <asp:ImageButton ID="ImageButton17" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias17" runat="server"></asp:Label>
                                </td>
                    </tr>
                    <tr>
                    	<td>
                        <asp:ImageButton ID="I2" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
                        </td>
                        <td>
                        <asp:ImageButton ID="D2" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="individual">
            	<table width="100%" height="100%" cellspacing="0" cellpadding="0">
                	<tr>
                    	<td colspan=2 valign=top>
                        <asp:ImageButton ID="ImageButton18" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" CssClass="textochico"  ID="alias18" runat="server" Height="16px">ALI000</asp:Label>
                                </td>
                    </tr>
                    <tr>
                    	<td>
                        <asp:ImageButton ID="I3" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
                        </td>
                        <td>
                        <asp:ImageButton ID="D3" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="individual">
            	<table width="100%" height="100%" cellspacing="0" cellpadding="0">
                	<tr>
                    	<td colspan=2 valign=top>
                        <asp:ImageButton ID="ImageButton19" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias19" runat="server"></asp:Label>
                                </td>
                    </tr>
                    <tr>
                    	<td>
                        <asp:ImageButton ID="I4" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
                        </td>
                        <td>
                        <asp:ImageButton ID="D4" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="individual">
            	<table width="100%" height="100%" cellspacing="0" cellpadding="0">
                	<tr>
                    	<td colspan=2 valign=top>
                        <asp:ImageButton ID="ImageButton20" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias20" runat="server"></asp:Label>
                                </td>
                    </tr>
                    <tr>
                    	<td>
                        <asp:ImageButton ID="I5" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
                        </td>
                        <td>
                        <asp:ImageButton ID="D5" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="individual">
            	<table width="100%" height="100%" cellspacing="0" cellpadding="0">
                	<tr>
                    	<td colspan=2 valign=top>
                        <asp:ImageButton ID="ImageButton21" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias21" runat="server"></asp:Label>
                                </td>
                    </tr>
                    <tr>
                    	<td>
                        <asp:ImageButton ID="I6" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
                        </td>
                        <td>
                        <asp:ImageButton ID="D6" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="individual">
            	<table width="100%" height="100%" cellspacing="0" cellpadding="0">
                	<tr>
                    	<td colspan=2 valign=top>
                        <asp:ImageButton ID="ImageButton22" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias22" runat="server"></asp:Label>
                                </td>
                    </tr>
                    <tr>
                    	<td>
                        <asp:ImageButton ID="I7" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
                        </td>
                        <td>
                        <asp:ImageButton ID="D7" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="individual">
            	<table width="100%" height="100%" cellspacing="0" cellpadding="0">
                	<tr>
                    	<td colspan=2 valign=top>
                        <asp:ImageButton ID="ImageButton23" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias23" runat="server"></asp:Label>
                                </td>
                    </tr>
                    <tr>
                    	<td>
                        <asp:ImageButton ID="I8" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
                        </td>
                        <td>
                        <asp:ImageButton ID="D8" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="individual">
            	<table width="100%" height="100%" cellspacing="0" cellpadding="0">
                	<tr>
                    	<td colspan=2 valign=top>
                        <asp:ImageButton ID="ImageButton24" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias24" runat="server"></asp:Label>
                                </td>
                    </tr>
                    <tr>
                    	<td>
                        <asp:ImageButton ID="I9" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
                        </td>
                        <td>
                        <asp:ImageButton ID="D9" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="individual">
            	<table width="100%" height="100%" cellspacing="0" cellpadding="0">
                	<tr>
                    	<td colspan=2 valign=top>
                        <asp:ImageButton ID="ImageButton25" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias25" runat="server"></asp:Label>
                                </td>
                    </tr>
                    <tr>
                    	<td>
                        <asp:ImageButton ID="I10" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
                        </td>
                        <td>
                        <asp:ImageButton ID="D10" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="individual">
            	<table width="100%" height="100%" cellspacing="0" cellpadding="0">
                	<tr>
                    	<td colspan=2 valign=top>
                        <asp:ImageButton ID="ImageButton26" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias26" runat="server"></asp:Label>
                                </td>
                    </tr>
                    <tr>
                    	<td>
                        <asp:ImageButton ID="I11" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
                        </td>
                        <td>
                        <asp:ImageButton ID="D11" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="individual">
            	<table width="100%" height="100%" cellspacing="0" cellpadding="0">
                	<tr>
                    	<td colspan=2 valign=top>
                        <asp:ImageButton ID="ImageButton27" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias27" runat="server"></asp:Label>
                                </td>
                    </tr>
                    <tr>
                    	<td>
                        <asp:ImageButton ID="I12" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
                        </td>
                        <td>
                        <asp:ImageButton ID="D12" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="individual">
            	<table width="100%" height="100%" cellspacing="0" cellpadding="0">
                	<tr>
                    	<td colspan=2 valign=top>
                        <asp:ImageButton ID="ImageButton28" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias28" runat="server"></asp:Label>
                                </td>
                    </tr>
                    <tr>
                    	<td>
                        <asp:ImageButton ID="I13" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
                        </td>
                        <td>
                        <asp:ImageButton ID="D13" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="individual">
            	<table width="100%" height="100%" cellspacing="0" cellpadding="0">
                	<tr>
                    	<td colspan=2 valign=top>
                        <asp:ImageButton ID="ImageButton29" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias29" runat="server"></asp:Label>
                                </td>
                    </tr>
                    <tr>
                    	<td>
                        <asp:ImageButton ID="I14" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
                        </td>
                        <td>
                        <asp:ImageButton ID="D14" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="individual">
            	<table width="100%" height="100%" cellspacing="0" cellpadding="0">
                	<tr>
                    	<td colspan=2 valign=top>
                        <asp:ImageButton ID="ImageButton30" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias30" runat="server"></asp:Label>
                                </td>
                    </tr>
                    <tr>
                    	<td>
                        <asp:ImageButton ID="I15" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
                        </td>
                        <td>
                        <asp:ImageButton ID="D15" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="individual">
            	<table width="100%" height="100%" cellspacing="0" cellpadding="0">
                	<tr>
                    	<td colspan=2 valign=top>
                        <asp:ImageButton ID="ImageButton31" runat="server" 
                    ImageUrl="~/sw/img/rangoshilda/0.png" />
                                    <br />
                        <asp:Label width="40px" text="ALI000" CssClass="textochico"  ID="alias31" runat="server"></asp:Label>
                                </td>
                    </tr>
                    <tr>
                    	<td>
                        <asp:ImageButton ID="I16" runat="server" ImageUrl="~/sw/img/bajaunoizq.png" />
                        </td>
                        <td>
                        <asp:ImageButton ID="D16" runat="server" ImageUrl="~/sw/img/bajaunoder.png" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </td>
  </tr>
</table>
                                                   </td>
                                               </tr>
                                               <tr>
                                                   <td class="style4">
                                                       &nbsp;</td>
                                                   <td>
                                                       &nbsp;</td>
                                               </tr>
                                               <tr>
                                                   <td align="center" class="style4">
                                    <asp:ImageButton ID="btn_inicio" runat="server" 
                                        ImageUrl="~/sw/img/hilda/arbol/inicio.png" />
                                    <asp:ImageButton ID="Subir1" runat="server" ImageUrl="~/sw/img/hilda/arbol/subir1.png" 
                    ToolTip="Subir un Nivel" />
&nbsp;<asp:ImageButton ID="Subir2" runat="server" ImageUrl="~/sw/img/hilda/arbol/subir2.png" 
                    ToolTip="Subir Dos Niveles" />
&nbsp;<asp:ImageButton ID="Subir3" runat="server" ImageUrl="~/sw/img/hilda/arbol/subir3.png" 
                    ToolTip="Subir Tres Niveles" />
&nbsp;<asp:ImageButton ID="Subir4" runat="server" ImageUrl="~/sw/img/hilda/arbol/subir4.png" 
                    ToolTip="Subir Cuatro Niveles" />
&nbsp;</td>
                                                   <td>
                        <span class="textonuevo">Número de Asociado</span><asp:TextBox ID="asociado" 
                    runat="server" ValidationGroup="buscar"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="asociado_FilteredTextBoxExtender" 
                    runat="server" Enabled="True" FilterType="Numbers" TargetControlID="asociado">
                        </cc1:FilteredTextBoxExtender>
                        <asp:Button ID="Button1" runat="server" Text="Buscar" 
                    ValidationGroup="buscar" />
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="asociado" ErrorMessage="Dato Necesario" 
                    ValidationGroup="buscar"></asp:RequiredFieldValidator>
                        <asp:Label ID="error" runat="server" CssClass="error"></asp:Label>
                                                   </td>
                                               </tr>
                                           </table>
               &nbsp;</td>
    </tr>
</table>
</asp:Content>

