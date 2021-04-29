﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Parcial
{
    public partial class Consumir : System.Web.UI.Page
    {

        Rest api = new Rest();
        List<DatosCovid> listaCovid = new List<DatosCovid>();

        protected void Page_Load(object sender, EventArgs e)
        {
            getDataFromApi();
        }

        private void edadesLeve()
        {
            //1.Encontrar las edades que estén estado LEVE
            var resultado = (from data in listaCovid
                             where data.ESTADO == "Recuperado"
                             select data);
            Console.WriteLine(resultado);
            pintarGrilla(resultado);
        }

        private void edadMaximaFallecido()
        {
            //2. Encontrar las MAXIMA edades que estén estado FALLECIDO
            var resultado = (from data in listaCovid
                             where data.ESTADO == "Fallecido"
                             select data.EDAD).Max(dataFallecido => dataFallecido);
            Console.WriteLine(resultado);
            pintarLabel("Maxima edad fallecido", resultado.ToString());
        }

        private void localidadMasFallecidos()
        {
            //3. Encontrar la localidad que presenta mayores fallecidos mes a mes
            var resultado = (from data in listaCovid
                             where data.ESTADO == "Fallecido"
                             select data.LOCALIDAD_ASIS).Max(dataFallecido => dataFallecido);
            Console.WriteLine(resultado);
        }

        private void localidadMenosFallecidos()
        {
            //4. Encontrar la localidad que presenta menores fallecidos mes a mes
            var resultado = (from data in listaCovid
                             where data.ESTADO == "Fallecido"
                             select data.LOCALIDAD_ASIS).Min(dataFallecido => dataFallecido);
            Console.WriteLine(resultado);
        }

        private void asintomaticosXMes()
        {
            //5. Identificar el aumento de casos asintomáticos mes a mes.

            var resultado = (from data in listaCovid
                             where data.ESTADO == "Desconocido"
                             select data);
            Console.WriteLine(resultado);
            pintarGrilla(resultado);
        }

        private void fechaMasMuertes()
        {
            //6. Cual son las fechas que ha presentado mayores muertes
            var resultado = (from data in listaCovid
                             where data.ESTADO == "Fallecido"
                             select data.FECHA_DIAGNOSTICO).Max(dataFallecido => dataFallecido);
            Console.WriteLine(resultado);
        }

        private void contagioXLocalidad()
        {
            //7. Realizar un comparativo de las localidades por tipo de contagio
            var resultado = (from data in listaCovid
                             group data by data.LOCALIDAD_ASIS into dataLocalidad
                             select dataLocalidad);
            Console.WriteLine(resultado);
            pintarGrilla(resultado);
        }

        private void fechaRecuperados()
        {
            //10. Presentar la fecha de recuperación de los pacientes recuperados
            var resultado = (from data in listaCovid
                             where data.ESTADO == "Recuperado"
                             select data);
            Console.WriteLine(resultado);
        }

        private void ubicacionRecuperados()
        {
            //14. Mostrar los recuperados por ubicación
            var resultado = (from data in listaCovid
                             where data.ESTADO == "Recuperado"
                             group data by data.LOCALIDAD_ASIS into dataUbicacion
                             select dataUbicacion);
            Console.WriteLine(resultado);
        }

        private void ubicacionFallecidos()
        {
            //15. Mostrar los fallecidos por ubicación
            var resultado = (from data in listaCovid
                             where data.ESTADO == "Fallecido"
                             group data by data.LOCALIDAD_ASIS into dataUbicacion
                             select dataUbicacion);
            Console.WriteLine(resultado);
        }

        private void getDataFromApi()
        {
            string url = "https://datosabiertos.bogota.gov.co/api/3/action/datastore_search?resource_id=b64ba3c4-9e41-41b8-b3fd-2da21d627558&limit=500";

            dynamic respuesta = api.Get(url);
            foreach (var res in respuesta.result.records)
            {
                DatosCovid dataCovid = new DatosCovid();
                dataCovid._id = res._id;
                dataCovid.CASO = res.CASO;
                string fechaSintomas = res.FECHA_DE_INICIO_DE_SINTOMAS;
                dataCovid.FECHA_DE_INICIO_DE_SINTOMAS = Convert.ToDateTime(fechaSintomas);
                string fechaDiagnostico = res.FECHA_DIAGNOSTICO;
                dataCovid.FECHA_DIAGNOSTICO = Convert.ToDateTime(fechaDiagnostico);
                dataCovid.CIUDAD = res.CIUDAD;
                dataCovid.LOCALIDAD_ASIS = res.LOCALIDAD_ASIS;
                dataCovid.EDAD = res.EDAD;
                dataCovid.UNI_MED = res.UNI_MED;
                dataCovid.SEXO = res.SEXO;
                dataCovid.FUENTE_O_TIPO_DE_CONTAGIO = res.FUENTE_O_TIPO_DE_CONTAGIO;
                dataCovid.UBICACION = res.UBICACION;
                dataCovid.ESTADO = res.ESTADO;

                listaCovid.Add(dataCovid);

            }
            

        }

        private void pintarGrilla(dynamic data)
        {
            gridCovid.DataSource = data;
            gridCovid.DataBind();
        }

        private void pintarLabel(string titulo, string value)
        {
            labelTitulo.Text = titulo + " : ";
            labelResultado.Text = value;
        }


        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            pintarGrilla(null);
            pintarLabel(null, null);
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            edadesLeve();
        }

        protected void btnCargarDAtos(object sender, EventArgs e)
        {
            pintarGrilla(listaCovid);
        }

        protected void btnEdadMaxFallecido_Click(object sender, EventArgs e)
        {
            edadMaximaFallecido();
        }

        protected void btnAsintomaticos_Click(object sender, EventArgs e)
        {
            contagioXLocalidad();
        }
    }
}