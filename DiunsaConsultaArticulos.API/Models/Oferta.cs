using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiunsaConsultaArticulos.API.Models
{
    public class Oferta
    {
        public string DESCRIPCION { get; set; }
        public string DESCRIPCION_PROMOCION { get; set; }
        public double PRECIO_OFERTA { get; set; }
        public double PRECIO_OFERTA_ISV { get; set; }
    }
}
