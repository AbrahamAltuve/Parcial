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
