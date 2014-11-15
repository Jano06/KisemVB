<%@ Page Language="VB" MasterPageFile="MasterMaqueta.master" AutoEventWireup="false" CodeFile="prospectos_alta.aspx.vb" Inherits="_Default" title="Página sin título"  EnableEventValidation="false" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="kisem.css"/>
    <style type="text/css">
        .style2
        {
            width: 184px;
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
        .style7
        {
            width: 106px;
        }
        .style8
        {
            width: 147px;
        }
        .style9
        {
            width: 100%;
        }
        .style10
        {
            width: 478px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style9">
        <tr>
            <td class="titulobienvenida">
                Alta de Prospectos<br __designer:mapid="c4" />
                <asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
        </tr>
        <tr>
            <td class="titulobienvenida">
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="contrato">Descarga 
                Contrato en formato PDF</asp:LinkButton>
            </td>
        </tr>
    </table>
    <asp:Panel ID="PanelContrato" runat="server">
        <table class="style9">
            <tr>
                <td>
                    <asp:TextBox ID="Contrato" runat="server" CssClass="contrato" Height="400px" 
                        TextMode="MultiLine" Width="800px" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="Acepto" runat="server" AutoPostBack="True" 
                        CssClass="textonuevo" Text="He leido y acepto los términos y condiciones" />
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelAlta" runat="server" Visible="False">
        <table align="left" class="texto" width="800">
            <tr>
                <td class="titulobienvenida" colspan="2">
                    <asp:Label ID="misprospectos" runat="server" Text="Mis Prospectos"></asp:Label>
                </td>
                <td class="titulo">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" CssClass="textogrid" ForeColor="#333333" GridLines="None" 
                    Width="950px" DataKeyNames="id">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ApPaterno" HeaderText="Apellido Paterno" />
                            <asp:BoundField DataField="ApMaterno" HeaderText="Apellido Materno" />
                            <asp:BoundField DataField="TelMovil" HeaderText="Celular" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:CommandField SelectText="Eliminar" ShowSelectButton="True" />
                        </Columns>
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="textonuevo" colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="titulobienvenida" colspan="2">
                    Nuevo prospecto:</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo" colspan="2">
                    <asp:RadioButton ID="asociado" runat="server" Checked="True" GroupName="tipo" 
                    Text="Asociado" />
                    &nbsp;<asp:RadioButton ID="consumidor" runat="server" GroupName="tipo" 
                        Text="Consumidor" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Width="200px"></asp:Label>
                </td>
                <td class="style8">
                    <asp:Label ID="Label5" runat="server" Width="399px"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Width="500px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Nombre:</td>
                <td class="style8">
                    <asp:TextBox ID="nombre" runat="server" MaxLength="50" TabIndex="1"   style="text-transform :uppercase" 
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
                    <asp:TextBox ID="appat" runat="server" MaxLength="50" TabIndex="2"  style="text-transform :uppercase" 
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
                    <asp:TextBox ID="apmat" runat="server" MaxLength="50" TabIndex="3"  style="text-transform :uppercase" 
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
                    <asp:TextBox ID="fechanac" runat="server" MaxLength="50" TabIndex="4"  style="text-transform :uppercase" 
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
                    Estado Civil:</td>
                <td>
                    <asp:RadioButton ID="Soltero" runat="server" GroupName="edocivil" 
                    Text="Soltero" />
                    &nbsp;<asp:RadioButton ID="Casado" runat="server" Checked="True" GroupName="edocivil" 
                    Text="Casado" />
                    &nbsp;<asp:RadioButton ID="Divorciado" runat="server" GroupName="edocivil" 
                        Text="Divorciado" />
                    &nbsp;<asp:RadioButton ID="UnionLibre" runat="server" GroupName="edocivil" 
                        Text="Unión Libre" />
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
                    <asp:TextBox ID="email" runat="server" MaxLength="50" TabIndex="11"   style="text-transform :lowercase" 
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
                    Confirmar
                Email:</td>
                <td class="style8">
                    <asp:TextBox ID="confirmaremail" runat="server" MaxLength="50" TabIndex="12"   style="text-transform :lowercase" 
                    Width="350px" onpaste="return false" oncut="return false" 
                    oncopy="return false"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                    ControlToValidate="confirmaremail" EnableClientScript="true" 
                    ErrorMessage="Confirmar Email es un campo obligatorio" Width="296px"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                ControlToCompare="email" ControlToValidate="confirmaremail" 
                                ErrorMessage="Los Correos deben coincidir"></asp:CompareValidator>
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
                    Dirección<asp:TextBox ID="alias" runat="server" MaxLength="50" TabIndex="12"  style="text-transform :uppercase" 
                    Width="350px" Visible="False"></asp:TextBox>
                </td>
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
                    <asp:DropDownList ID="estadocasa" runat="server" Width="350px" TabIndex="20">
                        
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
                <td class="textonuevo">
                    Ciudad</td>
                <td class="style8">
                    <asp:TextBox ID="CiudadCasa" runat="server" MaxLength="50" TabIndex="22"  style="text-transform :uppercase" 
                    Width="350px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                        ControlToValidate="CiudadCasa" EnableClientScript="true" 
                        ErrorMessage="Ciudad es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
                </td>
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
                                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="texto" TabIndex="23" 
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
                                        <asp:TextBox ID="callepaq" runat="server" MaxLength="50" TabIndex="24"  style="text-transform :uppercase" 
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
                                        <asp:TextBox ID="numpaq" runat="server" MaxLength="50" TabIndex="25"  style="text-transform :uppercase" 
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
                                        <asp:TextBox ID="interiorpaq" runat="server" MaxLength="50" TabIndex="26"  style="text-transform :uppercase" 
                                        Width="350px"></asp:TextBox>
                                    </td>
                                    <td class="style6">
                                        opcional</td>
                                </tr>
                                <tr>
                                    <td class="textonuevo">
                                        Colonia:</td>
                                    <td class="style7">
                                        <asp:TextBox ID="coloniapaq" runat="server" MaxLength="50" TabIndex="27"  style="text-transform :uppercase" 
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
                                        <asp:TextBox ID="cppaq" runat="server" MaxLength="50" TabIndex="28"  style="text-transform :uppercase" 
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
                                        <asp:DropDownList ID="estadopaq" runat="server" Width="350px" TabIndex="29">
                                           
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style5">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="textonuevo">
                                        Municipio:</td>
                                    <td class="style7">
                                        <asp:TextBox ID="municipiopaq" runat="server" MaxLength="50" TabIndex="30"  style="text-transform :uppercase" 
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
                                        style="text-transform :uppercase" TabIndex="31" Width="350px"></asp:TextBox>
                                    </td>
                                    <td class="style5">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
                                            ControlToValidate="CiudadPaq" EnableClientScript="true" 
                                            ErrorMessage="Ciudad es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
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
                <td class="style8" valign="top">
                    &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" 
                        ImageUrl="~/sw/img/hilda/botones/guardar.jpg" />
                    &nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" Width="399px" />
                </td>
                <td>
                    &nbsp;</td>
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
                <td colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelConfirmación" runat="server" Visible="False">
        <table align="left" class="texto" width="800">
            <tr>
                <td class="textonuevo" colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="titulobienvenida" colspan="2">
                    Confirmación de datos</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo" colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Width="200px"></asp:Label>
                </td>
                <td class="style10">
                    <asp:Label ID="Label7" runat="server" Width="399px"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Width="500px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Tipo:</td>
                <td class="style10">
                    <asp:Label ID="tipo" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Nombre:</td>
                <td class="style10">
                    <asp:Label ID="lbl_nombre" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Apellido Paterno:</td>
                <td class="style10">
                    <asp:Label ID="lbl_appaterno" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Apellido Materno:</td>
                <td class="style10">
                    <asp:Label ID="lbl_apmaterno" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Fecha de Nacimiento:</td>
                <td class="style10">
                    <cc1:CalendarExtender ID="TextBox26_CalendarExtender0" runat="server" 
                    Enabled="True" TargetControlID="fechanac" Format="yyyy/M/dd">
                    </cc1:CalendarExtender>
                    <asp:Label ID="lbl_fnac" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Estado Civil:</td>
                <td class="style10">
                    <asp:Label ID="Edocivil" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    RFC:</td>
                <td class="style10">
                    <asp:Label ID="lbl_rfc" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td class="textochico">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    CURP:</td>
                <td class="style10">
                    <asp:Label ID="lbl_curp" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td class="textochico">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Compañía:</td>
                <td class="style10">
                    <asp:Label ID="lbl_compania" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td class="textochico">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Teléfono Local:</td>
                <td class="style10">
                    <asp:Label ID="lbl_telefonolocal" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Teléfono Móvil:</td>
                <td class="style10">
                    <asp:Label ID="lbl_telmovil" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Nextel:</td>
                <td class="style10">
                    <asp:Label ID="lbl_nextel" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td class="textochico">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Email:</td>
                <td class="style10">
                    <asp:Label ID="lbl_email" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    País:</td>
                <td class="style10">
                    <asp:Label ID="lbl_pais" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Idioma:</td>
                <td class="style10">
                    <asp:Label ID="lbl_idioma" runat="server" CssClass="textonuevo2"></asp:Label>
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
                    Dirección</td>
                <td class="subtitulo">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Calle:</td>
                <td class="style10">
                    <asp:Label ID="lbl_calle" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Número:</td>
                <td class="style10">
                    <asp:Label ID="lbl_numero" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Interior:</td>
                <td class="style10">
                    <asp:Label ID="lbl_interior" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td class="textochico">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Colonia:</td>
                <td class="style10">
                    <asp:Label ID="lbl_colonia" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Código Postal:</td>
                <td class="style10">
                    <asp:Label ID="lbl_cp" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Estado:</td>
                <td class="style10">
                    <asp:Label ID="lbl_estado" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Municipio:</td>
                <td class="style10">
                    <asp:Label ID="lbl_municipio" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Ciudad</td>
                <td class="style10">
                    <asp:Label ID="lbl_ciudad" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    &nbsp;</td>
                <td class="style10">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <span class="subtitulo">Dirección de Paquetería</span>&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Calle:</td>
                <td class="style10">
                    <asp:Label ID="lbl_callepaq" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="textonuevo">
                    Número:</td>
                <td class="style7">
                    <asp:Label ID="lbl_numeropaq" runat="server" CssClass="textonuevo2"></asp:Label>
                </td>
                <td class="style5">
                    &nbsp;</td>
            </tr>
            <tr>
                                    <td class="textonuevo">
                                        Interior:</td>
                                    <td class="style7">
                                        <asp:Label ID="lbl_interiorpaq" runat="server" CssClass="textonuevo2"></asp:Label>
                                    </td>
                                    <td class="style6">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="textonuevo">
                                        Colonia:</td>
                                    <td class="style7">
                                        <asp:Label ID="lbl_coloniapaq" runat="server" CssClass="textonuevo2"></asp:Label>
                                    </td>
                                    <td class="style5">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="textonuevo">
                                        Código Postal:</td>
                                    <td class="style7">
                                        <asp:Label ID="lbl_cppaq" runat="server" CssClass="textonuevo2"></asp:Label>
                                    </td>
                                    <td class="style5">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="textonuevo">
                                        Estado:</td>
                                    <td class="style7">
                                        <asp:Label ID="lbl_estadopaq" runat="server" CssClass="textonuevo2"></asp:Label>
                                    </td>
                                    <td class="style5">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="textonuevo">
                                        Municipio:</td>
                                    <td class="style7">
                                        <asp:Label ID="lbl_municipiopaq" runat="server" CssClass="textonuevo2"></asp:Label>
                                    </td>
                                    <td class="style5">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="textonuevo">
                                        Ciudad</td>
                                    <td class="style7">
                                        <asp:Label ID="lbl_ciudadpaq" runat="server" CssClass="textonuevo2"></asp:Label>
                                    </td>
                                    <td class="style5">
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
                    </td>
            </tr>
            <tr>
                <td>
                    
                    &nbsp;</td>
                <td class="style10">
                    &nbsp;&nbsp;<asp:ImageButton ID="ImageButton2" runat="server" 
                        ImageUrl="~/sw/img/hilda/botones/guardar.jpg" />
                    &nbsp;<asp:ImageButton ID="ImageButton3" runat="server" 
                        ImageUrl="~/sw/img/hilda/botones/editarlainformacion.jpg" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style10">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

