using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiunsaConsultaArticulos.API.Models.DTOs
{
    public class ConsultaArticuloDTO
    {
        public ArticuloInfoDTO articuloInfo { get; set; }
        public List<OfertaDTO> ofertas { get; set; }
        public string imagenUrl { get; set; }
        public double precioOferta { get; set; }
    }
}
