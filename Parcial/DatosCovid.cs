using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parcial
{
    public class DatosCovid
    {
        public int _id { get; set; }
        public int CASO { get; set; }
        public string FECHA_DIAGNOSTICO { get; set; }
        public string FECHA_DE_INICIO_DE_SINTOMAS { get; set; }
        public string CIUDAD { get; set; }
        public string LOCALIDAD_ASIS { get; set; }
        public int EDAD { get; set; }
        public int UNI_MED { get; set; }
        public string SEXO { get; set; }
        public string FUENTE_O_TIPO_DE_CONTAGIO { get; set; }
        public string UBICACION { get; set; }
        public string ESTADO { get; set; }
    }
}