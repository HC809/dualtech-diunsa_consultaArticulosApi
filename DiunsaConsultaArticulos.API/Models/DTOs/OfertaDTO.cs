using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiunsaConsultaArticulos.API.Models.DTOs
{
    public class OfertaDTO
    {
        public string tienda { get; set; }
        public string promocion { get; set; }
        public double precioOferta { get; set; }
        public double precioOfertaConIsv { get; set; }
    }
}
