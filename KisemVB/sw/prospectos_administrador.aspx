<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="prospectos_administrador.aspx.vb" Inherits="sw_prospectos_administrador" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style8
        {
            width: 147px;
        }
        .style3
        {
            height: 22px;
        }
        .style4
        {
            height: 22px;
            width: 33px;
        }
        .style2
        {
            width: 184px;
        }
        .style7
        {
            width: 106px;
        }
        .style5
        {
            width: 33px;
        }
        .style6
        {
            text-align: justify;
            font-family: Arial;
            font-size: x-small;
            color: black;
            vertical-align: bottom;
            width: 33px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
        <tr>
            <td class="titulobienvenida" colspan="2">
                Alta de Prospectos<br />
                <asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
            <td class="titulo">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="textonuevo" colspan="2">
                <asp:RadioButton ID="asociado" runat="server" Checked="True" GroupName="tipo" 
                    Text="Asociado" />
&nbsp;<asp:RadioButton ID="consumidor" runat="server" GroupName="tipo" Text="Consumidor" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Width="200px"></asp:Label>
            </td>
            <td class="style8">
                &nbsp;</td>
            <td>
                <asp:Label ID="Label4" runat="server" Width="350px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="textonuevo">
                Nombre:</td>
            <td class="style8">
                <asp:TextBox ID="nombre" runat="server" MaxLength="50" TabIndex="1" 
                    Width="350px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="nombre" EnableClientScript="true" 
                    ErrorMessage="Nombre es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="textonuevo">
                Apellido Paterno:</td>
            <td class="style8">
                <asp:TextBox ID="appat" runat="server" MaxLength="50" TabIndex="2" 
                    Width="350px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="appat" EnableClientScript="true" 
                    ErrorMessage="Apellido Paterno es un campo obligatorio"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="textonuevo">
                Apellido Materno:</td>
            <td class="style8">
                <asp:TextBox ID="apmat" runat="server" MaxLength="50" TabIndex="3" 
                    Width="350px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="apmat" EnableClientScript="true" 
                    ErrorMessage="Apellido Materno es un campo obligatorio"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="textonuevo">
                Fecha de Nacimiento:</td>
            <td>
                <asp:TextBox ID="fechanac" runat="server" MaxLength="50" TabIndex="4" 
                    Width="150px"></asp:TextBox>
                <cc1:CalendarExtender ID="TextBox26_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="fechanac" Format="yyyy/M/dd">
                </cc1:CalendarExtender>
                <asp:RangeValidator ID="RangeValidator2" runat="server" 
                    ControlToValidate="fechanac" ErrorMessage="Fecha inválida" 
                    MaximumValue="1/1/2043" MinimumValue="1/1/1900" Type="Date" 
                    ></asp:RangeValidator>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="fechanac" ErrorMessage="Fecha es un campo obligatorio" 
                   ></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="textonuevo">
                RFC:</td>
            <td class="style8">
                <asp:TextBox ID="rfc" runat="server" MaxLength="50" TabIndex="5" 
                    Width="350px"></asp:TextBox>
            </td>
            <td class="textochico">
                opcional</td>
        </tr>
        <tr>
            <td class="textonuevo">
                CURP:</td>
            <td class="style8">
                <asp:TextBox ID="curp" runat="server" MaxLength="50" TabIndex="6" 
                    Width="350px"></asp:TextBox>
            </td>
            <td class="textochico">
                opcional</td>
        </tr>
        <tr>
            <td class="textonuevo">
                Compañía:</td>
            <td class="style8">
                <asp:TextBox ID="compania" runat="server" MaxLength="50" TabIndex="7" 
                    Width="350px"></asp:TextBox>
            </td>
            <td class="textochico">
                opcional</td>
        </tr>
        <tr>
            <td class="textonuevo">
                Teléfono Local:</td>
            <td class="style8">
                <asp:TextBox ID="telefono" runat="server" MaxLength="50" TabIndex="8" 
                    Width="350px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="telefono" EnableClientScript="true" 
                    ErrorMessage="Teléfono es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="textonuevo">
                Teléfono Móvil:</td>
            <td class="style8">
                <asp:TextBox ID="celular" runat="server" MaxLength="50" TabIndex="9" 
                    Width="350px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="celular" EnableClientScript="true" 
                    ErrorMessage="Móvil es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="textonuevo">
                Nextel:</td>
            <td class="style8">
                <asp:TextBox ID="nextel" runat="server" MaxLength="50" TabIndex="10" 
                    Width="350px"></asp:TextBox>
            </td>
            <td class="textochico">
                opcional</td>
        </tr>
        <tr>
            <td class="textonuevo">
                Email:</td>
            <td class="style8">
                <asp:TextBox ID="email" runat="server" MaxLength="50" TabIndex="11" 
                    Width="350px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="email" EnableClientScript="true" 
                    ErrorMessage="Email es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="textonuevo">
                País:</td>
            <td class="style8">
                <asp:DropDownList ID="pais" runat="server" TabIndex="13" Width="350px">
                    <asp:ListItem>México</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="textonuevo">
                Idioma:</td>
            <td class="style8">
                <asp:DropDownList ID="idioma" runat="server" TabIndex="14" Width="350px">
                    <asp:ListItem>Español</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="subtitulo" colspan="2">
                &nbsp;</td>
            <td class="subtitulo">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="subtitulo" colspan="2">
                Dirección<asp:TextBox ID="alias" runat="server" MaxLength="50" TabIndex="12" 
                    Width="350px" Visible="False"></asp:TextBox>
            </td>
            <td class="subtitulo">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="textonuevo">
                Calle:</td>
            <td class="style8">
                <asp:TextBox ID="callecasa" runat="server" MaxLength="50" TabIndex="15" 
                    Width="350px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                    ControlToValidate="callecasa" EnableClientScript="true" 
                    ErrorMessage="Calle es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="textonuevo">
                Número:</td>
            <td class="style8">
                <asp:TextBox ID="numcasa" runat="server" MaxLength="50" TabIndex="16" 
                    Width="350px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                    ControlToValidate="numcasa" EnableClientScript="true" 
                    ErrorMessage="Número es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="textonuevo">
                Interior:</td>
            <td class="style8">
                <asp:TextBox ID="interiorcasa" runat="server" MaxLength="50" TabIndex="17" 
                    Width="350px"></asp:TextBox>
            </td>
            <td class="textochico">
                opcional</td>
        </tr>
        <tr>
            <td class="textonuevo">
                Colonia:</td>
            <td class="style8">
                <asp:TextBox ID="coloniacasa" runat="server" MaxLength="50" TabIndex="18" 
                    Width="350px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                    ControlToValidate="coloniacasa" EnableClientScript="true" 
                    ErrorMessage="Colonia es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="textonuevo">
                Código Postal:</td>
            <td class="style8">
                <asp:TextBox ID="cpcasa" runat="server" MaxLength="50" TabIndex="19" 
                    Width="350px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                    ControlToValidate="cpcasa" EnableClientScript="true" 
                    ErrorMessage="CP es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="textonuevo">
                Estado:</td>
            <td class="style8">
                <asp:DropDownList ID="estadocasa" runat="server" Width="350px" TabIndex="20">
                    <asp:ListItem>	Aguascalientes 	
                    </asp:ListItem>
                    <asp:ListItem>	Baja California 	
                    </asp:ListItem>
                    <asp:ListItem>	Baja California Sur 	
                    </asp:ListItem>
                    <asp:ListItem>	Campeche 	
                    </asp:ListItem>
                    <asp:ListItem>	Chiapas 	
                    </asp:ListItem>
                    <asp:ListItem>	Chihuahua 	
                    </asp:ListItem>
                    <asp:ListItem>	Coahuila 	
                    </asp:ListItem>
                    <asp:ListItem>	Colima 	
                    </asp:ListItem>
                    <asp:ListItem>	Distrito Federal 	
                    </asp:ListItem>
                    <asp:ListItem>	Durango 	
                    </asp:ListItem>
                    <asp:ListItem>	Estado de México 	
                    </asp:ListItem>
                    <asp:ListItem>	Guanajuato 	
                    </asp:ListItem>
                    <asp:ListItem>	Guerrero 	
                    </asp:ListItem>
                    <asp:ListItem>	Hidalgo 	
                    </asp:ListItem>
                    <asp:ListItem>	Jalisco 	
                    </asp:ListItem>
                    <asp:ListItem>	Michoacán 	
                    </asp:ListItem>
                    <asp:ListItem>	Morelos 	
                    </asp:ListItem>
                    <asp:ListItem>	Nayarit 	
                    </asp:ListItem>
                    <asp:ListItem>	Nuevo León 	
                    </asp:ListItem>
                    <asp:ListItem>	Oaxaca 	
                    </asp:ListItem>
                    <asp:ListItem>	Puebla 	
                    </asp:ListItem>
                    <asp:ListItem>	Querétaro 	
                    </asp:ListItem>
                    <asp:ListItem>	Quintana Roo 	
                    </asp:ListItem>
                    <asp:ListItem>	San Luis Potosí 	
                    </asp:ListItem>
                    <asp:ListItem>	Sinaloa 	
                    </asp:ListItem>
                    <asp:ListItem>	Sonora 	
                    </asp:ListItem>
                    <asp:ListItem>	Tabasco 	
                    </asp:ListItem>
                    <asp:ListItem>	Tamaulipas 	
                    </asp:ListItem>
                    <asp:ListItem>	Tlaxcala 	
                    </asp:ListItem>
                    <asp:ListItem>	Veracruz 	
                    </asp:ListItem>
                    <asp:ListItem>	Yucatan 	
                    </asp:ListItem>
                    <asp:ListItem>	Zacatecas	
                    </asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="textonuevo">
                Municipio:</td>
            <td class="style8">
                <asp:TextBox ID="municipiocasa" runat="server" MaxLength="50" TabIndex="21" 
                    Width="350px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                    ControlToValidate="municipiocasa" EnableClientScript="true" 
                    ErrorMessage="Municipio es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="textonuevo">
                Ciudad</td>
            <td class="style8">
                <asp:TextBox ID="CiudadCasa" runat="server" MaxLength="50" TabIndex="21"  style="text-transform :uppercase" 
                    Width="350px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="subtitulo" colspan="2">
                &nbsp;</td>
            <td class="subtitulo">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:UpdatePanel ID="Panel1" runat="server">
                    <ContentTemplate>
                        <table cellpadding="1" cellspacing="2" class="texto">
                            <tr>
                                <td class="style3" colspan="2">
                                    <span class=subtitulo>Dirección de Paquetería</span>
                                    <asp:CheckBox ID="CheckBox1" runat="server" CssClass="texto" TabIndex="22" 
                                        Text="Misma dirección" AutoPostBack="True" />
                                </td>
                                <td class="style4">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <asp:Label ID="Label2" runat="server" Width="200px"></asp:Label>
                                </td>
                                <td class="style7">
                                    &nbsp;</td>
                                <td class="style5">
                                    <asp:Label ID="Label3" runat="server" Width="350px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="textonuevo">
                                    Calle:</td>
                                <td class="style7">
                                    <asp:TextBox ID="callepaq" runat="server" MaxLength="50" TabIndex="23" 
                                        Width="350px"></asp:TextBox>
                                </td>
                                <td class="style5">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                        ControlToValidate="callepaq" EnableClientScript="true" 
                                        ErrorMessage="Calle es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textonuevo">
                                    Número:</td>
                                <td class="style7">
                                    <asp:TextBox ID="numpaq" runat="server" MaxLength="50" TabIndex="24" 
                                        Width="350px"></asp:TextBox>
                                </td>
                                <td class="style5">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                        ControlToValidate="numpaq" EnableClientScript="true" 
                                        ErrorMessage="Número es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textonuevo">
                                    Interior:</td>
                                <td class="style7">
                                    <asp:TextBox ID="interiorpaq" runat="server" MaxLength="50" TabIndex="25" 
                                        Width="350px"></asp:TextBox>
                                </td>
                                <td class="style6">
                                    opcional</td>
                            </tr>
                            <tr>
                                <td class="textonuevo">
                                    Colonia:</td>
                                <td class="style7">
                                    <asp:TextBox ID="coloniapaq" runat="server" MaxLength="50" TabIndex="26" 
                                        Width="350px"></asp:TextBox>
                                </td>
                                <td class="style5">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                                        ControlToValidate="coloniapaq" EnableClientScript="true" 
                                        ErrorMessage="Colonia es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textonuevo">
                                    Código Postal:</td>
                                <td class="style7">
                                    <asp:TextBox ID="cppaq" runat="server" MaxLength="50" TabIndex="27" 
                                        Width="350px"></asp:TextBox>
                                </td>
                                <td class="style5">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                                        ControlToValidate="cppaq" EnableClientScript="true" 
                                        ErrorMessage="CP es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textonuevo">
                                    Estado:</td>
                                <td class="style7">
                                    <asp:DropDownList ID="estadopaq" runat="server" Width="350px" TabIndex="28">
                                        <asp:ListItem>Aguascalientes&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Baja California&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Baja California Sur&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Campeche&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Chiapas&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Chihuahua&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Coahuila&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Colima&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Distrito Federal&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Durango&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Estado de México&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Guanajuato&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Guerrero&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Hidalgo&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Jalisco&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Michoacán&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Morelos&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Nayarit&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Nuevo León&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Oaxaca&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Puebla&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Querétaro&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Quintana Roo&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>San Luis Potosí&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Sinaloa&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Sonora&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Tabasco&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Tamaulipas&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Tlaxcala&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Veracruz&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Yucatan&nbsp;
                                        </asp:ListItem>
                                        <asp:ListItem>Zacatecas
                                        </asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="style5">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="textonuevo">
                                    Municipio:</td>
                                <td class="style7">
                                    <asp:TextBox ID="municipiopaq" runat="server" MaxLength="50" TabIndex="29" 
                                        Width="350px"></asp:TextBox>
                                </td>
                                <td class="style5">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                                        ControlToValidate="municipiopaq" EnableClientScript="true" 
                                        ErrorMessage="Municipio es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textonuevo">
                                    Ciudad</td>
                                <td class="style7">
                                    <asp:TextBox ID="CiudadPaq" runat="server" MaxLength="50" 
                                        style="text-transform :uppercase" TabIndex="21" Width="350px"></asp:TextBox>
                                </td>
                                <td class="style5">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td class="style8">
                <asp:Button ID="Button1" runat="server" TabIndex="30" Text="Guardar" 
                    style="height: 26px" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

