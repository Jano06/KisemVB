<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="misdatos.aspx.vb" Inherits="sw_misdatos" title="Página sin título" %>

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
        
        .style1
        {
            width: 100%;
            padding:0;
            
        }
        .style9
        {
            text-align: right;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: Gray;
            vertical-align: top;
            height: 26px;
        }
        .style10
        {
            width: 147px;
            height: 26px;
        }
        .style11
        {
            text-align: justify;
            font-family: Arial;
            font-size: x-small;
            color: black;
            vertical-align: bottom;
            height: 26px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
    <tr>
        <td align="center">
            <img alt="" src="img/hilda/titulos/editarinfo.jpg" 
                style="width: 252px; height: 33px" /><asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td class="celdamarco">
    <table align="left" class="texto">
    <tr>
        <td>
            &nbsp;</td>
        <td class="style8">
                <asp:Label ID="Label5" runat="server" Width="399px"></asp:Label>
                                                   </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="textonuevo">
                Nombre:</td>
        <td class="style8">
            <asp:TextBox ID="nombre" runat="server" MaxLength="50" TabIndex="1"  style="text-transform :uppercase" 
                    Width="350px" Enabled="False"></asp:TextBox>
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
            <asp:TextBox ID="appat" runat="server" MaxLength="50" TabIndex="2"  style="text-transform :uppercase" 
                    Width="350px" Enabled="False"></asp:TextBox>
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
            <asp:TextBox ID="apmat" runat="server" MaxLength="50" TabIndex="3"  style="text-transform :uppercase" 
                    Width="350px" Enabled="False"></asp:TextBox>
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
        <td class="style8">
            <asp:TextBox ID="fechanac" runat="server" MaxLength="50" TabIndex="4"  style="text-transform :uppercase" 
                    Width="150px" Enabled="False"></asp:TextBox>
            <cc1:CalendarExtender ID="TextBox26_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="fechanac" Format="dd/M/yyyy">
            </cc1:CalendarExtender>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="fechanac" ErrorMessage="Fecha es un campo obligatorio" 
                    ValidationGroup="pago"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidator2" runat="server" 
                    ControlToValidate="fechanac" ErrorMessage="Fecha inválida" 
                    MaximumValue="1/1/2043" MinimumValue="1/1/1900" Type="Date" 
                    ValidationGroup="pago"></asp:RangeValidator>
        </td>
    </tr>
    <tr>
            <td class="textonuevo">
                Estado Civil:</td>
            <td>
                <asp:RadioButton ID="Soltero" runat="server" GroupName="edocivil" 
                    Text="Soltero" />
&nbsp;<asp:RadioButton ID="Casado" runat="server" Checked="True" GroupName="edocivil" 
                    Text="Casado" />
&nbsp;<asp:RadioButton ID="Divorciado" runat="server" GroupName="edocivil" Text="Divorciado" />
&nbsp;<asp:RadioButton ID="UnionLibre" runat="server" GroupName="edocivil" Text="Unión Libre" />
            </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="textonuevo">
                RFC:</td>
        <td class="style8">
            <asp:TextBox ID="rfc" runat="server" MaxLength="50" TabIndex="5"  style="text-transform :uppercase" 
                    Width="350px"></asp:TextBox>
        </td>
        <td class="textochico">
                opcional</td>
    </tr>
    <tr>
        <td class="textonuevo">
                CURP:</td>
        <td class="style8">
            <asp:TextBox ID="curp" runat="server" MaxLength="50" TabIndex="6"  style="text-transform :uppercase" 
                    Width="350px"></asp:TextBox>
        </td>
        <td class="textochico">
                opcional</td>
    </tr>
    <tr>
        <td class="textonuevo">
                Compañía:</td>
        <td class="style8">
            <asp:TextBox ID="compania" runat="server" MaxLength="50" TabIndex="7"  style="text-transform :uppercase" 
                    Width="350px"></asp:TextBox>
        </td>
        <td class="textochico">
                opcional</td>
    </tr>
    <tr>
        <td class="textonuevo">
                Teléfono Local:</td>
        <td class="style8">
            <asp:TextBox ID="telefono" runat="server" MaxLength="10" TabIndex="8"  style="text-transform :uppercase" 
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
            <asp:TextBox ID="celular" runat="server" MaxLength="13" TabIndex="9"  style="text-transform :uppercase" 
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
            <asp:TextBox ID="nextel" runat="server" MaxLength="50" TabIndex="10"  style="text-transform :uppercase" 
                    Width="350px"></asp:TextBox>
        </td>
        <td class="textochico">
                opcional</td>
    </tr>
    <tr>
        <td class="textonuevo">
                Email:</td>
        <td class="style8">
            <asp:TextBox ID="email" runat="server" MaxLength="50" TabIndex="11"  style="text-transform :lowercase" 
                    Width="350px"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="email" EnableClientScript="true" 
                    ErrorMessage="Email es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style9">
                Alias:</td>
        <td class="style10">
            <asp:TextBox ID="alias" runat="server" MaxLength="50" TabIndex="12"  style="text-transform :uppercase" 
                    Width="350px" Enabled="False"></asp:TextBox>
        </td>
        <td class="style11">
                opcional</td>
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
        <td class="subtitulo" colspan="2">
                Dirección</td>
        <td class="subtitulo">
                &nbsp;</td>
    </tr>
    <tr>
        <td class="textonuevo">
                Calle:</td>
        <td class="style8">
            <asp:TextBox ID="callecasa" runat="server" MaxLength="50" TabIndex="15"  style="text-transform :uppercase" 
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
            <asp:TextBox ID="numcasa" runat="server" MaxLength="50" TabIndex="16"  style="text-transform :uppercase" 
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
            <asp:TextBox ID="interiorcasa" runat="server" MaxLength="50" TabIndex="17"  style="text-transform :uppercase" 
                    Width="350px"></asp:TextBox>
        </td>
        <td class="textochico">
                opcional</td>
    </tr>
    <tr>
        <td class="textonuevo">
                Colonia:</td>
        <td class="style8">
            <asp:TextBox ID="coloniacasa" runat="server" MaxLength="50" TabIndex="18"  style="text-transform :uppercase" 
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
            <asp:TextBox ID="cpcasa" runat="server" MaxLength="50" TabIndex="19"  style="text-transform :uppercase" 
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
                                <asp:DropDownList ID="estadocasa" runat="server" 
                Width="350px" TabIndex="28">
                                    <asp:ListItem>Aguascalientes</asp:ListItem>
                                    <asp:ListItem>Baja California</asp:ListItem>
                                    <asp:ListItem>Baja California Sur</asp:ListItem>
                                    <asp:ListItem>Campeche</asp:ListItem>
                                    <asp:ListItem>Chiapas</asp:ListItem>
                                    <asp:ListItem>Chihuahua</asp:ListItem>
                                    <asp:ListItem>Coahuila</asp:ListItem>
                                    <asp:ListItem>Colima</asp:ListItem>
                                    <asp:ListItem>Distrito Federal</asp:ListItem>
                                    <asp:ListItem>Durango</asp:ListItem>
                                    <asp:ListItem>Estado de México</asp:ListItem>
                                    <asp:ListItem>Guanajuato</asp:ListItem>
                                    <asp:ListItem>Guerrero</asp:ListItem>
                                    <asp:ListItem>Hidalgo</asp:ListItem>
                                    <asp:ListItem>Jalisco</asp:ListItem>
                                    <asp:ListItem>Michoacán</asp:ListItem>
                                    <asp:ListItem>Morelos</asp:ListItem>
                                    <asp:ListItem>Nayarit</asp:ListItem>
                                    <asp:ListItem>Nuevo León</asp:ListItem>
                                    <asp:ListItem>Oaxaca</asp:ListItem>
                                    <asp:ListItem>Puebla</asp:ListItem>
                                    <asp:ListItem>Querétaro</asp:ListItem>
                                    <asp:ListItem>Quintana Roo</asp:ListItem>
                                    <asp:ListItem>San Luis Potosí</asp:ListItem>
                                    <asp:ListItem>Sinaloa</asp:ListItem>
                                    <asp:ListItem>Sonora</asp:ListItem>
                                    <asp:ListItem>Tabasco</asp:ListItem>
                                    <asp:ListItem>Tamaulipas</asp:ListItem>
                                    <asp:ListItem>Tlaxcala</asp:ListItem>
                                    <asp:ListItem>Veracruz</asp:ListItem>
                                    <asp:ListItem>Yucatan</asp:ListItem>
                                    <asp:ListItem>Zacatecas</asp:ListItem>
                                </asp:DropDownList>
        </td>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td class="textonuevo">
                Municipio:</td>
        <td class="style8">
            <asp:TextBox ID="municipiocasa" runat="server" MaxLength="50" TabIndex="21"  style="text-transform :uppercase" 
                    Width="350px"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                    ControlToValidate="municipiocasa" EnableClientScript="true" 
                    ErrorMessage="Municipio es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
                &nbsp;</td>
        <td class="style8">
                &nbsp;</td>
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
                                <asp:TextBox ID="callepaq" runat="server" MaxLength="50" TabIndex="23"  style="text-transform :uppercase" 
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
                                <asp:TextBox ID="numpaq" runat="server" MaxLength="50" TabIndex="24"  style="text-transform :uppercase" 
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
                                <asp:TextBox ID="interiorpaq" runat="server" MaxLength="50" TabIndex="25"  style="text-transform :uppercase" 
                                        Width="350px"></asp:TextBox>
                            </td>
                            <td class="style6">
                                    opcional</td>
                        </tr>
                        <tr>
                            <td class="textonuevo">
                                    Colonia:</td>
                            <td class="style7">
                                <asp:TextBox ID="coloniapaq" runat="server" MaxLength="50" TabIndex="26"  style="text-transform :uppercase" 
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
                                <asp:TextBox ID="cppaq" runat="server" MaxLength="50" TabIndex="27"  style="text-transform :uppercase" 
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
                                    <asp:ListItem>Aguascalientes</asp:ListItem>
                                    <asp:ListItem>Baja California</asp:ListItem>
                                    <asp:ListItem>Baja California Sur</asp:ListItem>
                                    <asp:ListItem>Campeche</asp:ListItem>
                                    <asp:ListItem>Chiapas</asp:ListItem>
                                    <asp:ListItem>Chihuahua</asp:ListItem>
                                    <asp:ListItem>Coahuila</asp:ListItem>
                                    <asp:ListItem>Colima</asp:ListItem>
                                    <asp:ListItem>Distrito Federal</asp:ListItem>
                                    <asp:ListItem>Durango</asp:ListItem>
                                    <asp:ListItem>Estado de México</asp:ListItem>
                                    <asp:ListItem>Guanajuato</asp:ListItem>
                                    <asp:ListItem>Guerrero</asp:ListItem>
                                    <asp:ListItem>Hidalgo</asp:ListItem>
                                    <asp:ListItem>Jalisco</asp:ListItem>
                                    <asp:ListItem>Michoacán</asp:ListItem>
                                    <asp:ListItem>Morelos</asp:ListItem>
                                    <asp:ListItem>Nayarit</asp:ListItem>
                                    <asp:ListItem>Nuevo León</asp:ListItem>
                                    <asp:ListItem>Oaxaca</asp:ListItem>
                                    <asp:ListItem>Puebla</asp:ListItem>
                                    <asp:ListItem>Querétaro</asp:ListItem>
                                    <asp:ListItem>Quintana Roo</asp:ListItem>
                                    <asp:ListItem>San Luis Potosí</asp:ListItem>
                                    <asp:ListItem>Sinaloa</asp:ListItem>
                                    <asp:ListItem>Sonora</asp:ListItem>
                                    <asp:ListItem>Tabasco</asp:ListItem>
                                    <asp:ListItem>Tamaulipas</asp:ListItem>
                                    <asp:ListItem>Tlaxcala</asp:ListItem>
                                    <asp:ListItem>Veracruz</asp:ListItem>
                                    <asp:ListItem>Yucatan</asp:ListItem>
                                    <asp:ListItem>Zacatecas</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style5">
                                    &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="textonuevo">
                                    Municipio:</td>
                            <td class="style7">
                                <asp:TextBox ID="municipiopaq" runat="server" MaxLength="50" TabIndex="29"  style="text-transform :uppercase" 
                                        Width="350px"></asp:TextBox>
                            </td>
                            <td class="style5">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                                        ControlToValidate="municipiopaq" EnableClientScript="true" 
                                        ErrorMessage="Municipio es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
                            </td>
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
            <div style="float:left; width: 348px;"><asp:ImageButton ID="ImageButton1" runat="server" 
                ImageUrl="~/sw/img/hilda/botones/guardar.jpg" />
            &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" 
                ImageUrl="~/sw/img/hilda/botones/cancelar.jpg" /></div>
        </td>
        <td>
                &nbsp;</td>
    </tr>
    </table>
                </td>
    </tr>
    <tr>
        <td align="left">
            <img alt="" src="img/hilda/sombra.png" style="width: 323px; height: 80px" /></td>
    </tr>
</table>
</asp:Content>

