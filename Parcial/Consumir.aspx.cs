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
            Console.WriteLine(listaCovid);
            edadesLeve();
            edadMaximaFallecido();
            localidadMasFallecidos();
            localidadMenosFallecidos();
            fechaRecuperados();
            ubicacionRecuperados();
            ubicacionFallecidos();
        }

        private void edadesLeve()
        {
            //1.Encontrar las edades que estén estado LEVE
            var resultado = (from data in listaCovid
                             where data.ESTADO == "Leve"
                             select data.EDAD);
            Console.WriteLine(resultado);
        }

        private void edadMaximaFallecido()
        {
            //2. Encontrar las MAXIMA edades que estén estado FALLECIDO
            var resultado = (from data in listaCovid
                             where data.ESTADO == "Fallecido"
                             select data.EDAD).Max(dataFallecido => dataFallecido);
            Console.WriteLine(resultado);
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

        private void fechaRecuperados()
        {
            //10. Presentar la fecha de recuperación de los pacientes recuperados
            var resultado = (from data in listaCovid
                             where data.ESTADO == "Recuperado"
                             select data.FECHA_DIAGNOSTICO);
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
                dataCovid.FECHA_DE_INICIO_DE_SINTOMAS = res.FECHA_DE_INICIO_DE_SINTOMAS;
                dataCovid.FECHA_DIAGNOSTICO = res.FECHA_DIAGNOSTICO;
                dataCovid.CIUDAD = res.CIUDAD;
                dataCovid.LOCALIDAD_ASIS = res.LOCALIDAD_ASIS;
                dataCovid.EDAD = res.EDAD;
                dataCovid.UNI_MED = res.UNI_MED;
                dataCovid.SEXO = res.SEXO;
                dataCovid.FUENTE_O_TIPO_DE_CONTAGIO = res.FUENTE_O_TIPO_DE_CONTAGIO;
                dataCovid.UBICACION = res.UBICACION;
                dataCovid.ESTADO = res.ESTADO;

                listaCovid.Add(dataCovid);
                Console.WriteLine(res.EDAD);

            }
            Console.WriteLine(listaCovid);
            //dynamic ab = respuesta.result.records[0];
            //dynamic abc = respuesta.result.records[1];
            //dynamic abcd = respuesta.result.records[4];
            //Console.WriteLine(ab.EDAD);
            //Console.WriteLine(abc);
            //Console.WriteLine(abcd);
            //pictureBox1.ImageLocation = respuesta.data[1].avatar.ToString();
            //txtNombreGET.Text = respuesta.data[1].first_name.ToString();
            //txtApellidoGET.Text = respuesta.data[1].last_name.ToString();
            //txtEmail.Text = respuesta.data[1].email.ToString();
        }

        //public class DatosCovid
        //{
        //    public string EDAD { get; set; }
        //    public string FECHA_DIAGNOSTICO { get; set; }
        //    public string CASO { get; set; }
        //    public string FUENTE_O_TIPO_DE_CONTAGIO { get; set; }
        //    public string SEXO { get; set; }
        //    public string CIUDAD { get; set; }
        //    public string FECHA_DE_INICIO_DE_SINTOMAS { get; set; }
        //    public string UBICACION { get; set; }
        //    public string _id { get; set; }
        //    public string ESTADO { get; set; }
        //    public string LOCALIDAD_ASIS { get; set; }
        //    public string UNI_MED { get; set; }
        //}

    }
}