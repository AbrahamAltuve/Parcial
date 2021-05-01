using Newtonsoft.Json;
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
            //edadesLeve();
            //edadMaximaFallecido();
            //localidadMasFallecidos();
            //localidadMenosFallecidos();
            //fechaMasMuertes();
            //contagioXLocalidad();
            //fechaRecuperados();
            //ubicacionRecuperados();
            //ubicacionFallecidos();
            //asintomaticosXMes();
            //mesXSexo();
        }

        private void edadesLeve()
        {
            //1.Encontrar las edades que estén estado LEVE
            //Por motivos de que al cargar los datos de la API existen poca probabilidad de que traiga datos con estado Leve se cambio por Recuperado
            var resultado = (from data in listaCovid
                             where data.ESTADO == "Leve"
                             select data);
            //Console.WriteLine(resultado);
            pintarGrilla(resultado);
        }

        private void edadMaximaFallecido()
        {
            //2. Encontrar las MAXIMA edades que estén estado FALLECIDO
            var resultado = (from data in listaCovid
                             where data.ESTADO == "Fallecido"
                             select data.EDAD).Max(dataFallecido => dataFallecido);
            //Console.WriteLine(resultado);
            pintarLabel("Maxima edad fallecido", resultado.ToString());
        }

        private void localidadMasFallecidos()
        {
            //3. Encontrar la localidad que presenta mayores fallecidos 
            var resultado = (from data in listaCovid
                             where data.ESTADO == "Fallecido"
                             select data.LOCALIDAD_ASIS).Max(dataFallecido => dataFallecido);
            //Console.WriteLine(resultado);
            pintarLabel("Localidad mas fallecidos", resultado.ToString());
        }

        private void localidadMenosFallecidos()
        {
            //4. Encontrar la localidad que presenta menores fallecidos 
            var resultado = (from data in listaCovid
                             where data.ESTADO == "Fallecido"
                             select data.LOCALIDAD_ASIS).Min(dataFallecido => dataFallecido);
            //Console.WriteLine(resultado);
            pintarLabel("Localidad menos fallecidos", resultado.ToString());
        }

        private void asintomaticosXMes()
        {
            //5. Identificar el aumento de casos asintomáticos mes a mes.
            var resultado = (from data in listaCovid
                             where data.FECHA_DE_INICIO_DE_SINTOMAS == null
                             orderby data.FECHA_DIAGNOSTICO descending
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
            //Console.WriteLine(resultado);
            pintarLabel("Fecha mas muertes", resultado.ToString());
        }

        private void contagioXLocalidad()
        {
            //7. Realizar un comparativo de las localidades por tipo de contagio
            var resultado = (from data in listaCovid
                             group data by data.LOCALIDAD_ASIS into dataLocalidad
                             select dataLocalidad);
            Console.WriteLine(resultado);
            string localidades = "";
            foreach (var res in resultado)
            {
                localidades += "<br />" + res.Key + ":" + res.Count() ;
                
                Console.WriteLine(localidades);
            }
            pintarGrilla(null);
            pintarLabel("Contagios por localidades",localidades);
            //pintarGrilla(resultado);
        }

        private void mesXSexo()
        {
            //8. Presentar el indicador mes a mes por sexo
            var resultado = (from data in listaCovid
                            group data by new { data.FECHA_DIAGNOSTICO.Month , data.FECHA_DIAGNOSTICO.Year, data.SEXO } into dataFecha 
                            select new { Month = dataFecha.Key.Month, Year = dataFecha.Key.Year, Sexo = dataFecha.Key.SEXO, Cantidad = dataFecha.Count() } );

            Console.WriteLine(resultado);

            string sexo = "";
            foreach (var res in resultado)
            {
                sexo += "<br />" + res.Month + "/" + res.Year + ":" +res.Sexo+"-"+res.Cantidad;

                Console.WriteLine(res);
            }
            pintarGrilla(null);
            pintarLabel("Contagios de mes por sexo", sexo);

        }
        private void fuenteXmes()
        {
            //9. Presentar el indicador mes a mes por fuente contagio
            var resultado = (from data in listaCovid
                             group data by new { data.FECHA_DIAGNOSTICO.Month, data.FECHA_DIAGNOSTICO.Year, data.FUENTE_O_TIPO_DE_CONTAGIO } into dataFecha
                             select new { Month = dataFecha.Key.Month, Year = dataFecha.Key.Year, TipoContagio = dataFecha.Key.FUENTE_O_TIPO_DE_CONTAGIO, Cantidad = dataFecha.Count() });

            Console.WriteLine(resultado);

            string sexo = "";
            foreach (var res in resultado)
            {
                sexo += "<br />" + res.Month + "/" + res.Year + ":" + res.TipoContagio + "-" + res.Cantidad;

                Console.WriteLine(res);
            }
            pintarGrilla(null);
            pintarLabel("Contagios de mes por tipo de cotagio", sexo);

        }

        private void fechaRecuperados()
        {
            //10. Presentar la fecha de recuperación de los pacientes recuperados
            var resultado = (from data in listaCovid
                             where data.ESTADO == "Recuperado"
                             select data);
            //Console.WriteLine(resultado);
            pintarGrilla(resultado);
        }

        private void ubicacionRecuperados()
        {
            //14. Mostrar los recuperados por ubicación
            var resultado = (from data in listaCovid
                             where data.ESTADO == "Recuperado"
                             group data by data.LOCALIDAD_ASIS into dataUbicacion
                             select new { Localidad = dataUbicacion.Key, Cantidad = dataUbicacion.Count() } );

            Console.WriteLine(resultado);
            string localidad = "";
            foreach (var res in resultado)
            {
                localidad += "<br />" + res.Localidad + ":" + res.Cantidad;

                Console.WriteLine(res);
            }
            pintarGrilla(null);
            pintarLabel("Recuperados por ubicacion", localidad);
        }

        private void ubicacionFallecidos()
        {
            //15. Mostrar los fallecidos por ubicación
            var resultado = (from data in listaCovid
                             where data.ESTADO == "Fallecido"
                             group data by data.LOCALIDAD_ASIS into dataUbicacion
                             select new { Localidad = dataUbicacion.Key, Cantidad = dataUbicacion.Count() });
            Console.WriteLine(resultado);
            string localidad = "";
            foreach (var res in resultado)
            {
                localidad += "<br />" + res.Localidad + ":" + res.Cantidad;

                Console.WriteLine(res);
            }
            pintarGrilla(null);
            pintarLabel("Fallecidos por ubicacion", localidad);
        }

        private void getDataFromApi()
        {
            string url = "https://datosabiertos.bogota.gov.co/api/3/action/datastore_search?resource_id=b64ba3c4-9e41-41b8-b3fd-2da21d627558&limit=5000";

            dynamic respuesta = api.Get(url);
            foreach (var res in respuesta.result.records)
            {
                DatosCovid dataCovid = new DatosCovid();
                dataCovid._id = res._id;
                dataCovid.CASO = res.CASO;
                dataCovid.FECHA_DE_INICIO_DE_SINTOMAS = res.FECHA_DE_INICIO_DE_SINTOMAS; //null ; //Convert.ToDateTime(fechaSintomas);
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

        protected void btnLocalidadMasFallecidos_Click(object sender, EventArgs e)
        {
            localidadMasFallecidos();
        }

        protected void btnLocalidadMenosFallecidos_Click(object sender, EventArgs e)
        {
            localidadMenosFallecidos();
        }

        protected void btnFechaRecuperados_Click(object sender, EventArgs e)
        {
            fechaRecuperados();
        }

        protected void btnFechaMasMuertes_Click(object sender, EventArgs e)
        {
            fechaMasMuertes();
        }

        protected void btnFechaAsintomaticos_Click(object sender, EventArgs e)
        {
            asintomaticosXMes();
        }

        protected void btnCantidadLocalidad_Click(object sender, EventArgs e)
        {
            contagioXLocalidad();
        }

        protected void btSexoFecha_Click(object sender, EventArgs e)
        {
            mesXSexo();
        }

        protected void btnTipoContagioFecha_Click(object sender, EventArgs e)
        {
            fuenteXmes();
        }

        protected void btnRecuperadosUbicacion_Click(object sender, EventArgs e)
        {
            ubicacionRecuperados();
        }

        protected void btnFallecidosUbicacion_Click(object sender, EventArgs e)
        {
            ubicacionFallecidos();
        }
    }
}