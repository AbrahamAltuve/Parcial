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

        protected void Page_Load(object sender, EventArgs e)
        {
            vamoAVer2();
        }

        private static void GetItems()
        {
            var url = $"https://datosabiertos.bogota.gov.co/api/3/action/datastore_search?resource_id=b64ba3c4-9e41-41b8-b3fd-2da21d627558";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            Console.WriteLine(responseBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("VAlio");
            }
        }

        public void vamoAVer()
        {
            string url = $"https://datosabiertos.bogota.gov.co/api/3/action/datastore_search?resource_id=b64ba3c4-9e41-41b8-b3fd-2da21d627558&limit=5";
            var json = new WebClient().DownloadString(url);
            dynamic prueba = JsonConvert.DeserializeObject(json);
            Console.WriteLine(json);
            Console.WriteLine(prueba);
        }

        private void vamoAVer2()
        {
            string url = "https://datosabiertos.bogota.gov.co/api/3/action/datastore_search?resource_id=b64ba3c4-9e41-41b8-b3fd-2da21d627558&limit=5";
            var listaCovid = new List<DatosCovid>();
            dynamic respuesta = api.Get(url);
            foreach (var res in respuesta.result.records)
            {
                DatosCovid dataCovid = new DatosCovid();
                dataCovid.CASO = res.CASO;
                dataCovid.CIUDAD = res.CIUDAD;
                dataCovid.EDAD = res.EDAD;
                dataCovid.ESTADO = res.ESTADO;
                dataCovid.FECHA_DE_INICIO_DE_SINTOMAS = res.FECHA_DE_INICIO_DE_SINTOMAS;
                dataCovid.FECHA_DIAGNOSTICO = res.FECHA_DIAGNOSTICO;
                dataCovid.FUENTE_O_TIPO_DE_CONTAGIO = res.FUENTE_O_TIPO_DE_CONTAGIO;
                dataCovid.LOCALIDAD_ASIS = res.LOCALIDAD_ASIS;
                dataCovid.SEXO = res.SEXO;
                dataCovid.UBICACION = res.UBICACION;
                dataCovid.UNI_MED = res.UNI_MED;
                dataCovid._id = res._id;
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

        public class DatosCovid
        {
            public string EDAD { get; set; }
            public string FECHA_DIAGNOSTICO { get; set; }
            public string CASO { get; set; }
            public string FUENTE_O_TIPO_DE_CONTAGIO { get; set; }
            public string SEXO { get; set; }
            public string CIUDAD { get; set; }
            public string FECHA_DE_INICIO_DE_SINTOMAS { get; set; }
            public string UBICACION { get; set; }
            public string _id { get; set; }
            public string ESTADO { get; set; }
            public string LOCALIDAD_ASIS { get; set; }
            public string UNI_MED { get; set; }
        }

    }
}