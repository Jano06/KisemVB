<%@ Page Language="VB" MasterPageFile="~/sw/MasterPage.master" AutoEventWireup="false" CodeFile="test.aspx.vb" Inherits="sw_test" title="Página sin título" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
<script language="javascript" type="text/javascript">
 function clickOnce(btn, msg)
        {
            // Comprobamos si se está haciendo una validación
            if (typeof(Page_ClientValidate) == 'function')
            {
                // Si se está haciendo una validación, volver si ésta da resultado false
                if (Page_ClientValidate() == false) { return false; }
            }
           
            // Asegurarse de que el botón sea del tipo button, nunca del tipo submit
            if (btn.getAttribute('type') == 'button')
            {
                // El atributo msg es totalmente opcional.
                // Será el texto que muestre el botón mientras esté deshabilitado
                if (!msg || (msg='undefined')) { msg = 'Loading...'; }
               
                btn.value = msg;

                // La magia verdadera :D
                btn.disabled = true;
            }
           
            return true;
        }
</script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
   <asp:UpdateProgress runat="server" id="PageUpdateProgress">
            <ProgressTemplate>
                Procesando...
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel runat="server" id="Panel">
            <ContentTemplate>
                <asp:Button runat="server" id="UpdateButton" onclick="UpdateButton_Click" text="Update"   UseSubmitBehavior="false" OnClientClick="clickOnce(this, 'Cargando...')"/>
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Content>

