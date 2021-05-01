<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Consumir.aspx.cs" Inherits="Parcial.Consumir" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnCargarDatos" runat="server" OnClick="btnCargarDAtos" Text="Cargar Datos" />
            <asp:Button ID="btnBorrar" runat="server" OnClick="btnBorrar_Click" Text="Limpiar " />
            <br />
            <asp:Button ID="Button1" runat="server"  Text="EdadesLeve" OnClick="Button1_Click1" />
            <asp:Button ID="btnEdadMaxFallecido" runat="server"  Text="MaxEdadFallecido" OnClick="btnEdadMaxFallecido_Click" />
            <asp:Button ID="btnLocalidadMasFallecidos" runat="server"  Text="Localidad mas Fallecidos" OnClick="btnLocalidadMasFallecidos_Click"  />
            <asp:Button ID="btnLocalidadMenosFallecidos" runat="server"  Text="Localidad menos Fallecidos" OnClick="btnLocalidadMenosFallecidos_Click" />
            <asp:Button ID="btnFechaMasMuertes" runat="server"  Text="Fecha mas muertes" OnClick="btnFechaMasMuertes_Click" />
            <asp:Button ID="btnFechaRecuperados" runat="server"  Text="Fecha Recuperados" OnClick="btnFechaRecuperados_Click" />
            <asp:Button ID="btnFechaAsintomaticos" runat="server"  Text="Asintomaticos" OnClick="btnFechaAsintomaticos_Click"/>
            <asp:Button ID="btnCantidadLocalidad" runat="server"  Text="Contagios por localidad" OnClick="btnCantidadLocalidad_Click"/>
            <asp:Button ID="btSexoFecha" runat="server"  Text="Contagios de sexo por mes" OnClick="btSexoFecha_Click"/>
            <asp:Button ID="btnTipoContagioFecha" runat="server"  Text="Contagios por tipo por mes" OnClick="btnTipoContagioFecha_Click"/>
            <asp:Button ID="btnRecuperadosUbicacion" runat="server"  Text="Recuperados por ubicacion" OnClick="btnRecuperadosUbicacion_Click"/>
            <asp:Button ID="btnFallecidosUbicacion" runat="server"  Text="Fallecidos por ubicacion" OnClick="btnFallecidosUbicacion_Click"/>
            <asp:Button ID="btnLeveXMes" runat="server"  Text="Tencencia Leve" OnClick="btnLeveXMes_Click"/>
            <asp:Button ID="btnFallecidoXMes" runat="server"  Text="Tencencia Fallecido" OnClick="btnFallecidoXMes_Click"/>
            <asp:Button ID="btnRecuperadoXMes" runat="server"  Text="Tencencia Recuerado" OnClick="btnRecuperadoXMes_Click"/>
            <asp:Button ID="btnDiferenciasCasos" runat="server"  Text="Diferencias Casos Febreros y Marzo" OnClick="btnDiferenciasCasos_Click"/>


            <br />
            <asp:Label ID="labelTitulo" runat="server"></asp:Label>
            <asp:Label ID="labelResultado" runat="server"></asp:Label>

            <asp:GridView ID="gridCovid" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="_id" HeaderText="_id" />
                    <asp:BoundField DataField="CASO" HeaderText="CASO" />
                    <asp:BoundField DataField="FECHA_DE_INICIO_DE_SINTOMAS" HeaderText="FECHA_DE_INICIO_DE_SINTOMAS" />
                    <asp:BoundField DataField="FECHA_DIAGNOSTICO" HeaderText="FECHA_DIAGNOSTICO" />
                    <asp:BoundField DataField="CIUDAD" HeaderText="CIUDAD" />
                    <asp:BoundField DataField="LOCALIDAD_ASIS" HeaderText="LOCALIDAD_ASIS" />
                    <asp:BoundField DataField="EDAD" HeaderText="EDAD" />
                    <asp:BoundField DataField="UNI_MED" HeaderText="UNI_MED" />
                    <asp:BoundField DataField="SEXO" HeaderText="SEXO" />
                    <asp:BoundField DataField="FUENTE_O_TIPO_DE_CONTAGIO" HeaderText="FUENTE_O_TIPO_DE_CONTAGIO" />
                    <asp:BoundField DataField="UBICACION" HeaderText="UBICACION" />
                    <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
