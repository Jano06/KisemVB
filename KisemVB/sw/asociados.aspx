<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="asociados.aspx.vb" Inherits="sw_asociados" title="Página sin título" EnableEventValidation="false" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


        .style12
        {
            text-align: right;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
            color: Gray;
            vertical-align: top;
            height: 26px;
        }
        .style13
        {
            height: 26px;
        }
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
        .style14
        {
            width: 63px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
        <tr>
            <td class="titulobienvenida">
                Asociados<asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;
                </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td class="style12">
            <asp:Label ID="label6" runat="server" Width="178px"></asp:Label>
                        </td>
                        <td class="style13">
            <asp:Label ID="label5" runat="server" Width="450px"></asp:Label>
                        </td>
        <td class="style14">
            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style12">
                            Número de
                Asociado:</td>
                        <td class="style13">
                            <asp:TextBox ID="TextBox1" runat="server" Width="350px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" 
                    enabled="True" servicepath="~/BuscarAsociados.asmx" minimumprefixlength="2" 
                    servicemethod="ObtListaAsociados" enablecaching="true" targetcontrolid="TextBox1" 
                    usecontextkey="True" completionsetcount="10"
                    completioninterval="200">
                            </cc1:AutoCompleteExtender>
                        </td>
        <td class="style14">
            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style12">
                            Fecha de Ingreso:</td>
                        <td class="style13">
                            Fecha Inicio
                            <asp:TextBox ID="fechade" runat="server" CssClass="texto" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="fechade_CalendarExtender" runat="server" 
                                Enabled="True" Format="dd/M/yyyy" TargetControlID="fechade">
                            </cc1:CalendarExtender>
                        &nbsp;Fecha Final &nbsp;&nbsp;<asp:TextBox ID="fechaa" runat="server" CssClass="texto" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="fechaa_CalendarExtender" runat="server" 
                                Enabled="True" Format="dd/M/yyyy" TargetControlID="fechaa">
                            </cc1:CalendarExtender>
                        </td>
        <td class="style14">
            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style12">
                            Fecha de Nacimiento:</td>
                        <td class="style13">
                            Fecha Inicio
                            <asp:TextBox ID="FechaNacde" runat="server" CssClass="texto" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" 
                                Enabled="True" Format="dd/M/yyyy" TargetControlID="fechanacde">
                            </cc1:CalendarExtender>
                        &nbsp;Fecha Final &nbsp;&nbsp;<asp:TextBox ID="fechanaca" runat="server" 
                                CssClass="texto" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" 
                                Enabled="True" Format="dd/M/yyyy" TargetControlID="fechanaca">
                            </cc1:CalendarExtender>
                        </td>
        <td rowspan="3" class="style14">
            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style12">
                            Rango:</td>
                        <td class="style13">
                            <asp:DropDownList ID="Rangos" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            Estado:</td>
                        <td>
                <asp:DropDownList ID="estado" runat="server" Width="350px" TabIndex="20">
                    <asp:ListItem Selected="True">Cualquiera</asp:ListItem>
                    <asp:ListItem>Aguascalientes</asp:ListItem>
<asp:ListItem>Baja California</asp:ListItem>
<asp:ListItem>Baja California Sur 	</asp:ListItem>
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
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            Ciudad</td>
                        <td>
                            <asp:TextBox ID="Ciudad" runat="server" Width="347px"></asp:TextBox>
                        </td>
                        <td class="style14">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
                            Status</td>
                        <td>
                            <asp:RadioButton ID="Todos" runat="server" CssClass="texto" Text="Todos" 
                                Checked="True" GroupName="status" />
                        &nbsp;<asp:RadioButton ID="Activos" runat="server" CssClass="texto" Text="Activos" 
                                GroupName="status" />
                        &nbsp;<asp:RadioButton ID="Inactivos" runat="server" CssClass="texto" 
                                Text="Inactivos" GroupName="status" />
                        &nbsp;<asp:RadioButton ID="Suspendidos" runat="server" CssClass="texto" 
                                Text="Suspendidos" GroupName="status" />
                        </td>
                        <td class="style14">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textonuevo">
            Seleccione las columnas a mostrar:</td>
                        <td colspan="2">
                            <br />
            <asp:CheckBox ID="Numero_chk" runat="server" Checked="True" Text="Número" Enabled="False" />
&nbsp;<asp:CheckBox ID="nombre_chk" runat="server" Checked="True" Text="Nombre" />
&nbsp;
            <asp:CheckBox ID="rfc_chk" runat="server" Text="RFC" />
&nbsp;<asp:CheckBox ID="calleynum_chk" runat="server" Text="Calle y Número" />
                            <asp:CheckBox ID="colonia_chk" runat="server" Text="Colonia" />
                            <br />
                            <br />
                            <asp:CheckBox ID="ciudad_chk" runat="server" Text="Ciudad" />
                            <asp:CheckBox ID="municipio_chk" runat="server" Text="Municipio" />
                            <asp:CheckBox ID="Estado_chk" runat="server" Text="Estado" />
                            <asp:CheckBox ID="CP_chk" runat="server" Text="Código Postal" />
                            <asp:CheckBox ID="TelFijo_chk" runat="server" 
                                Text="Teléfono Fijo" />
                            <br />
                            <br />
                            <asp:CheckBox ID="TelMovil_chk" runat="server" 
                                Text="Teléfono Móvil" />
                            <asp:CheckBox ID="FechaNac_chk" runat="server" 
                                Text="Fecha de Nacimiento" />
                            <asp:CheckBox ID="FechaInsc_chk" runat="server" Checked="True" 
                                Text="Fecha de Inscripción" />
                            <br />
                            <br />
                            <asp:CheckBox ID="Email_chk" runat="server" Checked="True" 
                                Text="Email" />
                            <asp:CheckBox ID="NumPatrocinador_chk" runat="server" Checked="True" 
                                Text="Número de Patrocinador" />
                            <asp:CheckBox ID="NomPatrocinador_chk" runat="server" Checked="True" 
                                Text="Nombre de Patrocinador" />
                            <br />
                            <br />
                            <asp:CheckBox ID="Rango_chk" runat="server" Checked="True" 
                                Text="Rango" />
                            <asp:CheckBox ID="Status_chk" runat="server" Checked="True" 
                                Text="Status" />
                            <asp:CheckBox ID="UltimaCompra_chk" runat="server" Checked="True" 
                                Text="Última Compra" />
                            <asp:CheckBox ID="Bono6_chk" runat="server" Checked="True" 
                                Text="Bono 6" />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="Button2" runat="server" Text="Mostrar" 
                                ValidationGroup="filtro" />
                        &nbsp;<asp:ImageButton ID="ImageButton1" 
                    runat="server" ImageUrl="~/sw/img/hilda/botones/exportaraexcel.jpg" 
                                ValidationGroup="filtro" />
                        </td>
                        <td class="style14">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
&nbsp;&nbsp;<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" ForeColor="#333333" 
                    GridLines="None" Width="1100px">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="Num" />
                        <asp:BoundField DataField="nombre" HeaderText="Asociado" >
                            <ItemStyle Width="300px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="rfc" HeaderText="RFC" />
                        <asp:BoundField DataField="calleynumero" HeaderText="Calle y Número" />
                        <asp:BoundField DataField="colcasa" HeaderText="Colonia" />
                        <asp:BoundField DataField="ciudadcasa" HeaderText="Ciudad" />
                        <asp:BoundField DataField="municipiocasa" HeaderText="Municipio" />
                        <asp:BoundField DataField="estadocasa" HeaderText="Estado" />
                        <asp:BoundField DataField="cpcasa" HeaderText="CP" />
                        <asp:BoundField DataField="tellocal" HeaderText="Teléfono" />
                        <asp:BoundField DataField="telmovil" HeaderText="Móvil" />
                        <asp:BoundField DataField="fnac" HeaderText="Nacimiento" DataFormatString="{0:dd/MMMM/yyyy}"  />
                        <asp:BoundField DataField="finsc" HeaderText="Inscripción" DataFormatString="{0:dd/MMMM/yyyy}"  />
                        <asp:BoundField DataField="email" HeaderText="Email" />
                        <asp:BoundField DataField="patrocinador" HeaderText="Patrocinador" />
                        <asp:BoundField DataField="nombrepatrocinador" HeaderText="Nombre Patrocinador" />
                        <asp:BoundField DataField="rango" HeaderText="Rango" />
                        <asp:BoundField DataField="status" HeaderText="Status" />
                        <asp:BoundField DataField="inicioactivacion" DataFormatString="{0:dd/MMMM/yyyy}" 
                            HeaderText="Última Compra" />
                        <asp:BoundField DataField="bono6" HeaderText="Bono 6" />
                        <asp:CommandField SelectText="Editar" ShowSelectButton="True">
                        </asp:CommandField>
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server" Visible="False">
                    <table align="left" class="texto">
                        <tr>
                            <td class="titulobienvenida" colspan="2">
                                Modificar Asociado</td>
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
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                    Format="dd/MM/yyyy" TargetControlID="fechanac">
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
                            <td class="textonuevo">
                                Bodega:</td>
                            <td class="style8">
                                <asp:DropDownList ID="bodegas" runat="server" TabIndex="20" Width="350px">
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
                                <asp:TextBox ID="cpcasa" runat="server" MaxLength="50" 
                                    style="text-transform :uppercase" TabIndex="19" Width="350px"></asp:TextBox>
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
                                <asp:DropDownList ID="estadocasa" runat="server" TabIndex="20" Width="350px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="textonuevo">
                                Municipio:</td>
                            <td class="style8">
                                <asp:TextBox ID="municipiocasa" runat="server" MaxLength="50" 
                                    style="text-transform :uppercase" TabIndex="21" Width="350px"></asp:TextBox>
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
                            <td colspan="2" class="subtitulo">
                                &nbsp;</td>
                            <td class="subtitulo">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:UpdatePanel ID="Panel2" runat="server">
                                    <ContentTemplate>
                                        <table cellpadding="1" cellspacing="2" class="texto">
                                            <tr>
                                                <td class="style3" colspan="2">
                                                    <span class="subtitulo">Dirección de Paquetería</span>
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
                                                    <asp:TextBox ID="callepaq" runat="server" MaxLength="50" 
                                                        style="text-transform :uppercase" TabIndex="23" Width="350px"></asp:TextBox>
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
                                                    <asp:TextBox ID="numpaq" runat="server" MaxLength="50" 
                                                        style="text-transform :uppercase" TabIndex="24" Width="350px"></asp:TextBox>
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
                                                    <asp:TextBox ID="interiorpaq" runat="server" MaxLength="50" 
                                                        style="text-transform :uppercase" TabIndex="25" Width="350px"></asp:TextBox>
                                                </td>
                                                <td class="style6">
                                                    opcional</td>
                                            </tr>
                                            <tr>
                                                <td class="textonuevo">
                                                    Colonia:</td>
                                                <td class="style7">
                                                    <asp:TextBox ID="coloniapaq" runat="server" MaxLength="50" 
                                                        style="text-transform :uppercase" TabIndex="26" Width="350px"></asp:TextBox>
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
                                                    <asp:TextBox ID="cppaq" runat="server" MaxLength="50" 
                                                        style="text-transform :uppercase" TabIndex="27" Width="350px"></asp:TextBox>
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
                                                    <asp:DropDownList ID="estadopaq" runat="server" TabIndex="28" Width="350px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="style5">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="textonuevo">
                                                    Municipio:</td>
                                                <td class="style7">
                                                    <asp:TextBox ID="municipiopaq" runat="server" MaxLength="50" 
                                                        style="text-transform :uppercase" TabIndex="29" Width="350px"></asp:TextBox>
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
                            <td class="textonuevo">
                                Tipo de Retención:</td>
                            <td>
                                <asp:CheckBox ID="factura" runat="server" Text="Factura" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="textonuevo">
                                Status:</td>
                            <td>
                                <asp:RadioButton ID="Activo" runat="server" Checked="True" 
                                    GroupName="actividad" Text="Activo" />
                                &nbsp;<asp:RadioButton ID="Inactivo" runat="server" GroupName="actividad" 
                                    Text="Inactivo" />
                                &nbsp;<asp:RadioButton ID="Suspendido" runat="server" GroupName="actividad" 
                                    Text="Suspendido" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="textonuevo">
                                Última Activación:</td>
                            <td>
                                De
                                <asp:TextBox ID="ActivoDe" runat="server" MaxLength="50" 
                                    style="text-transform :uppercase" TabIndex="4" Width="150px"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator3" runat="server" 
                                    ControlToValidate="fechanac" ErrorMessage="Fecha inválida" 
                                    MaximumValue="1/1/2043" MinimumValue="1/1/1900" Type="Date"></asp:RangeValidator>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                                    ControlToValidate="ActivoDe" ErrorMessage="Fecha es un campo obligatorio"></asp:RequiredFieldValidator>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                                    TargetControlID="Activode">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="textonuevo">
                                &nbsp;</td>
                            <td>
                                A&nbsp;&nbsp;
                                <asp:TextBox ID="Activoa" runat="server" MaxLength="50" 
                                    style="text-transform :uppercase" TabIndex="4" Width="150px"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator4" runat="server" 
                                    ControlToValidate="fechanac" ErrorMessage="Fecha inválida" 
                                    MaximumValue="1/1/2043" MinimumValue="1/1/1900" Type="Date"></asp:RangeValidator>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                                    ControlToValidate="Activoa" ErrorMessage="Fecha es un campo obligatorio"></asp:RequiredFieldValidator>
                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" 
                                    TargetControlID="Activoa">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="Button1" runat="server" style="height: 26px" TabIndex="30" 
                                    Text="Guardar" />
                                &nbsp;<asp:Button ID="Button3" runat="server" style="height: 26px" 
                                    TabIndex="30" Text="Cancelar" ValidationGroup="cancelar" />
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
            </td>
        </tr>
    </table>
</asp:Content>

