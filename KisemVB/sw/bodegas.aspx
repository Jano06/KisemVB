<%@ Page Language="VB" MasterPageFile="~/sw/MasterMaqueta.master" AutoEventWireup="false" CodeFile="bodegas.aspx.vb" Inherits="sw_bodegas" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style8
        {
            width: 147px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" class="texto">
        <tr>
            <td class="titulobienvenida" colspan="2">
                Bodegas<asp:Label ID="mensajes" runat="server" CssClass="exito"></asp:Label>
&nbsp;</td>
            <td class="titulo">
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
                Número de Administrador:</td>
            <td class="style8">
                <asp:TextBox ID="administrador" runat="server" MaxLength="50" TabIndex="16" 
                    Width="350px"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="administrador_FilteredTextBoxExtender" 
                    runat="server" Enabled="True" FilterType="Numbers" 
                    TargetControlID="administrador">
                </cc1:FilteredTextBoxExtender>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                    ControlToValidate="administrador" EnableClientScript="true" 
                    ErrorMessage="Administrador es un campo obligatorio" Width="287px"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" TabIndex="30" Text="Insertar" />
&nbsp;<asp:Button ID="Button3" runat="server" Text="Actualizar" Visible="False" />
&nbsp;<asp:Button ID="Button2" runat="server" Text="Cancelar" UseSubmitBehavior="False" 
                    Visible="False" />
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
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="899px">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="Id" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="calle" HeaderText="Calle" />
                        <asp:BoundField DataField="numero" HeaderText="Número" />
                        <asp:BoundField DataField="interior" HeaderText="Interior" />
                        <asp:BoundField DataField="colonia" HeaderText="Colonia" />
                        <asp:BoundField DataField="cp" HeaderText="CP" />
                        <asp:BoundField DataField="municipio" HeaderText="Municipio" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                        <asp:BoundField DataField="administrador" HeaderText="Administrador" />
                        <asp:CommandField SelectText="Editar" ShowSelectButton="True" />
                        <asp:CommandField ShowDeleteButton="True" />
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
        <tr>
            <td>
                &nbsp;</td>
            <td class="style8">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

